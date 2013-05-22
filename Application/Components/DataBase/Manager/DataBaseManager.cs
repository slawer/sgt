using System;
using System.Data;
using System.Threading;
using System.Data.SqlClient;

namespace DataBase
{
    /// <summary>
    /// Реализует управление базой данных
    /// </summary>
    public class DataBaseManager
    {
        protected DBState state;                // текущее состояние базы данных

        protected DataBaseAdapter adapter;      // настройки подключения к серверу баз данных
        protected DataBaseProvider provider;    // сервисные функции для работы с базой данных

        protected DataBaseSaver saver;          // осуществляет сохранение параметров в базу данных

        protected ReaderWriterLockSlim slim;    // синхронизатор

        protected Timer timer;                  // осуществляет проверку состояние сотояния соединения с сервером бд
        protected Boolean is_valid;             // доступен сервер баз данных или нет

        protected ReaderWriterLockSlim t_slim;  // синхронизатор таймера

        /// <summary>
        /// Возникает когда установлено соединение с сервером баз данных
        /// </summary>
        public event EventHandler ServerConnected;
        
        /// <summary>
        /// Возникает когда соединение с сервером данных не установленно
        /// </summary>
        public event EventHandler ServerDisconneted;

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public DataBaseManager()
        {
            state = DBState.Default;
            
            adapter = new DataBaseAdapter(".", "", "sa", "");
            provider = new DataBaseProvider(adapter);

            saver = new DataBaseSaver();

            is_valid = false;
            timer = new Timer(TimerCallback, null, 0, 5000);            

            slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
            t_slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

            DataBase.Initialize();
        } 

        /// <summary>
        /// Осуществляет проверку подключения к серверу БД
        /// </summary>
        public Boolean IsConnectValid
        {
            get
            {
                SqlConnection connection = null;
                try
                {
                    connection = new SqlConnection(adapter.ConnectionStringToServer);
                    connection.Open();

                    if (connection.State == ConnectionState.Open)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    if (connection != null)
                    {
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                            SqlConnection.ClearPool(connection);
                        }

                        connection.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// Осуществляет проверку подключения к серверу БД
        /// </summary>
        public Boolean IsConnectValidNotClearPool
        {
            get
            {
                SqlConnection connection = null;
                try
                {
                    connection = new SqlConnection(adapter.ConnectionStringToServer);
                    connection.Open();

                    if (connection.State == ConnectionState.Open)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    if (connection != null)
                    {
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                            //SqlConnection.ClearPool(connection);
                        }

                        connection.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет флаг доступности сервера баз данных
        /// </summary>
        protected Boolean IsSrvValid
        {
            get
            {
                if (t_slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return is_valid;
                    }
                    finally
                    {
                        t_slim.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (t_slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        is_valid = value;
                    }
                    finally
                    {
                        t_slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет текущее состояние БД
        /// </summary>
        public DBState State
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return state;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return DBState.Default;
            }

            protected set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        state = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет имя сервера БД (localhost)
        /// </summary>
        public string DataSource
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return adapter.DataSource;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return string.Empty;
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        if (state == DBState.Default)
                        {
                            adapter.DataSource = value;
                        }
                        else
                            throw new InvalidOperationException("Текущее состояние не позволяет " 
                                + "присвоить значение данному свойству");
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет каталог к торомому подключиться (имя БД с которой работать)
        /// </summary>
        public string InitialCatalog
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return adapter.InitialCatalog;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return string.Empty;
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        if (state == DBState.Default)
                        {
                            adapter.InitialCatalog = value;
                        }
                        else
                            throw new InvalidOperationException("Текущее состояние не позволяет "
                                + "присвоить значение данному свойству");
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет пользователя БД, от имени которого выполнять работу с БД
        /// </summary>
        public string UserID
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return adapter.UserID;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return string.Empty;
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        if (state == DBState.Default)
                        {
                            adapter.UserID = value;
                        }
                        else
                            throw new InvalidOperationException("Текущее состояние не позволяет "
                                + "присвоить значение данному свойству");
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет пароль пользователя, от имени которого осуществляется работа с БД
        /// </summary>
        public string Password
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return adapter.Password;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return string.Empty;
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        if (state == DBState.Default)
                        {
                            adapter.Password = value;
                        }
                        else
                            throw new InvalidOperationException("Текущее состояние не позволяет "
                                + "присвоить значение данному свойству");
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Возвращяет список БД, которые имеются на сервере
        /// </summary>
        public string[] DataBases
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return provider.DataBases;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Создать новую БД
        /// </summary>
        /// <param name="dbName">Имя создаваемой БД</param>
        public void CreateBD(string dbName)
        {
            if (slim.TryEnterReadLock(1000))
            {
                try
                {
                    if (IsSrvValid)
                    {
                        if (state == DBState.Default)
                        {
                            provider.CreateDataBase(dbName);
                        }
                        else
                            throw new InvalidOperationException("Текущее состояние не позволяет "
                                + "создать базу данных");
                    }
                }
                finally
                {
                    slim.ExitReadLock();
                }
            }
        }

        /// <summary>
        /// Удалить БД
        /// </summary>
        /// <param name="dbName">Имя удаляемой БД</param>
        public void RemoveDB(string dbName)
        {
            if (slim.TryEnterReadLock(300))
            {
                try
                {
                    if (IsSrvValid)
                    {
                        if (state == DBState.Default)
                        {
                            provider.DeleteDataBase(dbName);
                        }
                        else
                            throw new InvalidOperationException("Текущее состояние не позволяет удалять БД");
                    }
                }
                finally
                {
                    slim.ExitReadLock();
                }
            }
        }

        /// <summary>
        /// Загрузить БД
        /// </summary>
        /// <param name="dbName">Имя загружаемой БД</param>
        public void LoadDB(string dbName)
        {
            if (slim.TryEnterWriteLock(500))
            {
                string old_name_db = string.Empty;

                try
                {
                    if (IsSrvValid)
                    {
                        old_name_db = adapter.InitialCatalog;
                        if (state == DBState.Default)
                        {
                            adapter.InitialCatalog = dbName;
                            provider.Adapter.InitialCatalog = dbName;

                            DataBase.LoadDB(provider);
                            state = DBState.Loaded;
                        }
                    }
                }
                catch (Exception ex)
                {
                    adapter.InitialCatalog = old_name_db;
                    provider.Adapter.InitialCatalog = old_name_db;

                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Закрыть загруженную БД
        /// </summary>
        public void CloseDB()
        {
            if (slim.TryEnterWriteLock(1000))
            {
                try
                {
                    if (state == DBState.Loaded || state == DBState.Saving)
                    {
                        DataBase.CloseDB();
                        state = DBState.Default;
                    }
                    else
                        throw new InvalidOperationException("База данных не загружена.");
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Начать сохранение параметров в базу данных
        /// </summary>
        public void TurnOnSave()
        {
            if (slim.TryEnterWriteLock(500))
            {
                try
                {
                    if (state == DBState.Loaded)
                    {
                        saver.Start();
                        if (saver.State == DataBaseSaver.DataBaseSaverStates.Started)
                        {
                            state = DBState.Saving;
                        }
                    }
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Остановить сохранение параметров в базу данных
        /// </summary>
        public void TurnOffSave()
        {
            if (slim.TryEnterWriteLock(500))
            {
                try
                {
                    if (state == DBState.Saving)
                    {                        
                        saver.Stop();
                        if (saver.State == DataBaseSaver.DataBaseSaverStates.Stopped)
                        {
                            state = DBState.Loaded;
                        }
                    }
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Добавить значение параметра на сохранение
        /// </summary>
        /// <param name="Identifier">Идентификатор параметра</param>
        /// <param name="Time">Время поступления значения параметра</param>
        /// <param name="Value">Значение параметра</param>
        public void ToSaveParameter(Guid Identifier, DateTime Time, Single Value)
        {
            try
            {
                saver.PushToSave(Identifier, Time, Value);
            }
            catch { }
        }

        // --------- вспомогательные методы -----------

        /// <summary>
        /// Осуществляет проверку соединения с сервером баз данных
        /// </summary>
        /// <param name="state">Не используется</param>
        protected void TimerCallback(object state)
        {
            try
            {
                IsSrvValid = IsConnectValid;

                if (IsSrvValid)
                {
                    ServerConnected(this, EventArgs.Empty);
                }
                else
                    ServerDisconneted(this, EventArgs.Empty);
            }
            catch { }
        }
    }

    /// <summary>
    /// Перечисление состояний БД
    /// </summary>
    public enum DBState
    {
        /// <summary>
        /// Загруженна БД
        /// </summary>
        Loaded,

        /// <summary>
        /// Выполняется сохранение значений параметров в БД
        /// </summary>
        Saving,

        /// <summary>
        /// По умолчанию.
        /// </summary>
        Default
    }
}