using System;
using System.Data.SqlClient;

namespace DataBase
{
    /// <summary>
    /// Реализует хранение параметров подключения к серверу баз данных и базе данных
    /// </summary>
    public class DataBaseAdapter
    {
        private string db_server = string.Empty;        // имя сервера БД
        private string db_name = string.Empty;          // каталог к торомому подключиться

        private string db_user = string.Empty;          // учетная запись под которой подключаться
        private string db_password = string.Empty;      // пароль учетной записи

        private String connectionStringToDB;            // строка подключения к базе данных
        private String connectionStringToSrv;           // строка подключения к серверу баз данных
        
        private SqlConnectionStringBuilder builber;     // формирует строку подключения

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public DataBaseAdapter()
        {
            db_server = "localhost";
            builber = new SqlConnectionStringBuilder();
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="Host">Адрес сервера к которому подключиться</param>
        /// <param name="Catalog">Имя БД к которой подключиться</param>
        /// <param name="User">Пользователь БД, от имени которого выполнять работу с БД</param>
        /// <param name="Passcode">Пароль пользователя</param>
        public DataBaseAdapter(string Host, string Catalog, string User, string Passcode)
        {
            db_server = Host;
            db_name = Catalog;

            db_user = User;
            db_password = Passcode;

            builber = new SqlConnectionStringBuilder();
            InitizlizeConnectionStrings();
        }

        /// <summary>
        /// Определяет имя сервера БД (localhost)
        /// </summary>
        public string DataSource
        {
            get { return db_server; }
            set 
            {
                db_server = value;
                InitizlizeConnectionStrings();
            }
        }

        /// <summary>
        /// Определяет каталог к торомому подключиться (имя БД с которой работать)
        /// </summary>
        public string InitialCatalog
        {
            get { return db_name; }
            set 
            { 
                db_name = value;
                InitizlizeConnectionStrings();
            }
        }

        /// <summary>
        /// Определяет пользователя БД, от имени которого выполнять работу с БД
        /// </summary>
        public string UserID
        {
            get { return db_user; }
            set 
            { 
                db_user = value;
                InitizlizeConnectionStrings();
            }
        }

        /// <summary>
        /// Определяет пароль пользователя, от имени которого осуществляется работа с БД
        /// </summary>
        public string Password
        {
            get { return db_password; }
            set 
            { 
                db_password = value;
                InitizlizeConnectionStrings();
            }
        }

        /// <summary>
        /// Возвращяет строку подключения к серверу БД
        /// </summary>
        public string ConnectionStringToServer
        {
            get
            {
                try
                {
                    return connectionStringToSrv;
                }
                catch { }
                return string.Empty;
            }
        }

        /// <summary>
        /// Возвращяет строку подключения к БД
        /// </summary>
        public string ConnectionString
        {
            get
            {
                try
                {
                    return connectionStringToDB;
                }
                catch { }
                return string.Empty;
            }
        }

        /// <summary>
        /// Инициализировать строки подключения и строителя строк подключения
        /// </summary>
        private void InitizlizeConnectionStrings()
        {
            builber.DataSource = DataSource;
            builber.InitialCatalog = String.Empty;

            builber.UserID = UserID;
            builber.Password = Password;

            builber.IntegratedSecurity = true;
            connectionStringToSrv = builber.ConnectionString;

            builber.IntegratedSecurity = false;
            builber.InitialCatalog = InitialCatalog;

            connectionStringToDB = builber.ConnectionString;
        }
    }
}