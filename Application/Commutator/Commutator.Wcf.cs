using System;
using System.Threading;

using WCF;
using WCF.WCF_Client;

using Tcp;
using Buffering;

namespace SGT
{
    /// <summary>
    /// Реализует коммутатор, который отвечает за 
    /// соединение, прием и передачу данных от devMan
    /// </summary>
    public partial class Commutator
    {
        protected ReaderWriterLockSlim t_slim;                                  // синхронизатор доступа к времи поступления данных

        protected ReaderWriterLockSlim wcf_slim;                                // синхронизатор доступа к состоянию соединения
        protected WcfConnectionState wcf_state = WcfConnectionState.Default;    // состояние соединения к devMan

        /// <summary>
        /// Выполнить инициализацию подсистему WCF
        /// </summary>
        protected void InitializeWcf()
        {
            client = new devTcpManager();

            DevManClient.onConnected += new EventHandler(DevManClient_onConnected);
            DevManClient.onDisconnected += new EventHandler(DevManClient_onDisconnected);

            DevManClient.onReceive += new ReceivedEventHandler(DevManClient_onReceive);
        }

        /// <summary>
        /// Подключились к серверу данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DevManClient_onConnected(object sender, EventArgs e)
        {
            try
            {
                ConnectionState = WcfConnectionState.Conected;

                if (client.Client.Connected)
                {
                    client.Client.Disconnect();
                }

                client.Client.Port = 56000;
                client.Client.Host = DevManClient.DevManUri.Host;

                client.Client.Connect();
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteToLog(this, new ErrorArgs(ex.Message, ErrorType.NotFatal));
            }
        }

        /// <summary>
        /// Разорванно соединение с сервером данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DevManClient_onDisconnected(object sender, EventArgs e)
        {
            try
            {
                ConnectionState = WcfConnectionState.Disconnected;
                if (client.Client.Connected)
                {
                    client.Client.Disconnect();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteToLog(this, new ErrorArgs(ex.Message, ErrorType.NotFatal));
            }
        }

        private DateTime time_data_acquisition;                         // время получения данных от сервера данных

        private DateTime lastTime = DateTime.MinValue;                  // для вычисления интервала тишины
        private TimeSpan tInterval = new TimeSpan(0, 0, 0, 0, 300);     // время тишины для приема данных от сервера данных

        /// <summary>
        /// Определяет время получения данных от сервера данных
        /// </summary>
        public DateTime TimeDatAacquisition
        {
            get
            {
                if (t_slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return time_data_acquisition;
                    }
                    finally
                    {
                        t_slim.ExitReadLock();
                    }
                }

                return DateTime.MinValue;
            }

            protected set
            {
                if (t_slim.TryEnterReadLock(100))
                {
                    try
                    {
                        time_data_acquisition = value;
                    }
                    finally
                    {
                        t_slim.ExitReadLock();
                    }
                }

            }
        }

        /// <summary>
        /// Получили данные от devMan
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Параметры события</param>
        protected void DevManClient_onReceive(object sender, ReceivedEventArgs e)
        {
            try
            {
                DateTime now = DateTime.Now;
                TimeDatAacquisition = now;

                if (now > lastTime)
                {
                    TimeSpan interval = now - lastTime;
                    if (interval.Ticks > tInterval.Ticks)
                    {
                        lastTime = now;
                        foreach (Parameter parameter in parameters)
                        {
                            PDescription channel = parameter.Channel;
                            if (channel != null)
                            {
                                if (channel.Number >= 0 && channel.Number < e.Slice.Length)
                                {
                                    CommutatorParameter.setCurrent(parameter, e.Slice[channel.Number]);
                                    e.Slice[channel.Number] = parameter.CalculatedValue;
                                }
                            }
                        }

                        SgtApplication app = SgtApplication.CreateInstance();
                        if (app != null)
                        {
                            app.Technology.Calculate(this, new CommutatorEventArgs(e.Slice));
                            if (tech_saver != null) tech_saver(e.Slice);                            
                        }
                        
                        buffer.Append(new Slice(DateTime.Now, e.Slice));
                        UpdateTechnologyParameters();

                        if (onUpdated != null)
                        {
                            onUpdated(this, new CommutatorEventArgs(e.Slice));
                        }
                    }
                }
                else
                    lastTime = now;
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteToLog(sender, new ErrorArgs(ex.Message, ErrorType.NotFatal));
            }
        }

        /// <summary>
        /// Передать технологически параметры серверу данных
        /// </summary>
        protected void UpdateTechnologyParameters()
        {
            try
            {
                TParameter[] t_prms = SgtApplication.CreateInstance().Technology.Parameters;
                if (t_prms != null)
                {
                    foreach (TParameter parameter in t_prms)
                    {
                        if (parameter != null && parameter.IsSimple == false)
                        {
                            int dev_number = NumberTechOnDev(parameter.SNumber);
                            if (dev_number != -1)
                            {
                                DevManClient.UpdateParameter(dev_number, parameter.Value);
                            }
                        }
                        else
                            if (parameter != null && parameter.IsSimple == true)
                            {
                                switch (parameter.UnigueClassName)
                                {
                                    case "P0014":

                                        try
                                        {
                                            P0014 par = parameter as P0014;
                                            if (par != null && par.Source == P0014Source.Own)
                                            {
                                                int dev_number = NumberTechOnDev(par.SNumber);
                                                if (dev_number != -1)
                                                {
                                                    DevManClient.UpdateParameter(dev_number, parameter.Value);
                                                }
                                            }
                                        }
                                        catch { }
                                        break;

                                    case "P14_1":

                                        try
                                        {
                                            P14_1 par = parameter as P14_1;
                                            if (par != null && par.Source == P0014Source.Own)
                                            {
                                                int dev_number = NumberTechOnDev(par.SNumber);
                                                if (dev_number != -1)
                                                {
                                                    DevManClient.UpdateParameter(dev_number, parameter.Value);
                                                }
                                            }
                                        }
                                        catch { }
                                        break;

                                    default:
                                        break;
                                }
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteToLog(this, new ErrorArgs(ex.Message, ErrorType.NotFatal));
            }
        }

        /// <summary>
        /// Найти номер параметра на сервере данных, который необходимо обновить
        /// </summary>
        /// <param name="t_number">Номер параметра</param>
        /// <returns>Номер параметра который нужно обновить на сервере данных</returns>
        protected int NumberTechOnDev(int t_number)
        {
            try
            {
                if (t_number != -1)
                {
                    foreach (Parameter parameter in parameters)
                    {
                        PDescription channel = parameter.Channel;
                        if (channel != null && channel.Number == t_number)
                        {
                            //if (channel.Type == DeviceManager.FormulaType.Capture)
                            {
                                return channel.Number;
                            }
                        }
                    }
                }

                return -1;
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteToLog(this, new ErrorArgs(ex.Message, ErrorType.NotFatal));
            }
            return -1;
        }

        /// <summary>
        /// Вспомогательный класс для коммутатора.
        /// Неоходим для присваивания нового значения параметру.
        /// </summary>
        protected class CommutatorParameter : Parameter
        {
            public static void setCurrent(Parameter parameter, Single value)
            {
                Parameter.SetCurrentValue(parameter, value);
            }
        }
    }
}