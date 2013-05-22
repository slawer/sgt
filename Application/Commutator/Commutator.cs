using System;
using System.Threading;
using System.Net.NetworkInformation;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using Buffering;

using WCF;
using WCF.WCF_Client;

namespace SGT
{
    /// <summary>
    /// Реализует коммутатор, который отвечает за 
    /// соединение, прием и передачу данных от devMan
    /// </summary>
    public partial class Commutator
    {
        /// <summary>
        /// Возникает когда получены параметры от сервера данных и готовы для обработки
        /// </summary>
        public event CommutatorEventHandler onUpdated;

        protected SaverTechnologyData tech_saver;       // реализует сохранение технологических данных

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public Commutator()
        {
            buffer = new RSliceBuffer();
            parameters = new Parameter[256];

            t_slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
            p_slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

            wcf_slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

            for (int i = 0; i < parameters.Length; i++)
            {
                parameters[i] = new Parameter();
            }            

            InitializeWcf();
        }

        /// <summary>
        /// Возвращяет параметры получаемые от сервера данных
        /// </summary>
        public Parameter[] Parameters
        {
            get
            {
                return parameters;
            }
        }

        /// <summary>
        /// Возвращяет текущее состояние подключения к серверу данных
        /// </summary>
        public WcfConnectionState ConnectionState
        {
            get
            {
                switch (DevManClient.State)
                {
                    case WCF.WCF_Client.ConnectionState.Connected:

                        return WcfConnectionState.Conected;

                    case WCF.WCF_Client.ConnectionState.Disconnected:

                        return WcfConnectionState.Disconnected;

                    case WCF.WCF_Client.ConnectionState.Default:

                        return WcfConnectionState.Default;

                    default:
                        break;
                }
                return WcfConnectionState.Disconnected;
            }

            protected set
            {
                if (wcf_slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        wcf_state = value;
                    }
                    finally
                    {
                        wcf_slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Подключиться к серверу данных
        /// </summary>
        public void ConnectToServer()
        {
            try
            {
                try
                {
                    Ping ping = new Ping();
                    PingOptions options = new PingOptions();

                    Uri uri = DevManClient.Uri;
                    PingReply reply = ping.Send(uri.Host, 100);

                    if (reply.Status == IPStatus.Success)
                    {
                        DevManClient.Connect();
                    }
                }
                catch { }
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteToLog(this, new ErrorArgs(ex.Message, ErrorType.NotFatal));
            }
        }
        
        /// <summary>
        /// Возвращяет функцию осуществляющую сохранение технологических данных
        /// </summary>
        public SaverTechnologyData SaverTechData
        {
            get
            {
                return tech_saver;
            }

            set
            {
                tech_saver = value;
            }
        }

        /// <summary>
        /// Адрес сервера данных
        /// </summary>
        public Uri DevManUri
        {
            get
            {
                return DevManClient.Uri;
            }

            set
            {
                DevManClient.Uri = value;
            }
        }

        /// <summary>
        /// проверить состояние сервера WCF
        /// </summary>
        public void PingWcf()
        {
            try
            {
                DevManClient.UpdateParameter(-1, float.NaN);
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteToLog(null, new ErrorArgs(ex.Message, ErrorType.Unknown));
            }
        }

        /// <summary>
        /// Сохранить локальный буфер
        /// </summary>
        public void Serialize()
        {
            try
            {
                string fileName = string.Format("{0}\\{1}", Environment.CurrentDirectory, "file.s");

                FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                BinaryFormatter bf = new BinaryFormatter();

                //сериализация
                bf.Serialize(fs, buffer);

                fs.Close();
            }
            catch { }
        }

        /// <summary>
        /// Загрузить локальный буфер
        /// </summary>
        public void DeSerialize()
        {
            try
            {
                string fileName = string.Format("{0}\\{1}", Environment.CurrentDirectory, "file.s");
                if (File.Exists(fileName))
                {
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                    BinaryFormatter bf = new BinaryFormatter();

                    buffer = (RSliceBuffer)bf.Deserialize(fs);
                    fs.Close();
                }
            }
            catch { }
            finally
            {
                if (buffer == null)
                {
                    buffer = new RSliceBuffer();
                }
            }
        }
    }

    public delegate void SaverTechnologyData(float[] slice);

    /// <summary>
    /// Возможные состояния подключения к серверу данных
    /// </summary>
    public enum WcfConnectionState
    {
        /// <summary>
        /// Подключен к серверу данных
        /// </summary>
        Conected,

        /// <summary>
        /// Не подключен к серверу данных
        /// </summary>
        Disconnected,

        /// <summary>
        /// Стартовое состояние, не осуществлялась работа с сервером данных.(по умолчаню)
        /// </summary>
        Default
    }

    /// <summary>
    /// Интерфейс функции обрабатывающей событие получение среза данных коммутатором
    /// </summary>
    /// <param name="sender">Источник события</param>
    /// <param name="e">Параметры события</param>
    public delegate void CommutatorEventHandler(Object sender, CommutatorEventArgs e);

    /// <summary>
    /// Класс посредством которого передаются данные
    /// </summary>
    public class CommutatorEventArgs : EventArgs
    {
        protected float[] _slice = null;             // передаваемый срез данных

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public CommutatorEventArgs()
            :base()
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="slice">Передаваемый срез данных</param>
        public CommutatorEventArgs(float[] slice)
            : base()
        {
            _slice = slice;
        }

        /// <summary>
        /// Возвращяет срез данных
        /// </summary>
        public Single[] Slice
        {
            get
            {
                return _slice;
            }
        }
    }
}