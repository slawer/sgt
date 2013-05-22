using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DataBase
{
    /// <summary>
    /// Реализует сервисные функции работы с базой данных
    /// </summary>
    public class DataBaseProvider
    {
        // ---- Sql запросы к БД ----

        /// <summary>
        /// Определяет имя главной таблицы в базе данных
        /// </summary>
        protected const string t_main = "dbo.t_main";

        /// <summary>
        /// Запрос на получение списка всех баз данных, которые имеются на сервере
        /// </summary>
        protected const string sql_query_select_all_db = "SELECT * FROM sys.databases";

        /// <summary>
        /// Запрос на выборку списка параметров в главной таблице базы данных
        /// </summary>
        protected const string sql_query_select_all_parameters = "Select * From dbo.t_main";

        /// <summary>
        /// Определяет запрос который вставляет служебную информацию в БД (необходима для совместимости)
        /// </summary>
        protected const string sql_query_insert_first_param = "INSERT INTO dbo.t_Param (id, id_param, val_param) VALUES (1, 1, \'DBase.3\')";

        /// <summary>
        /// Определяет запрос на создание главной таблицы базы данных
        /// </summary>
        protected const string sql_query_create_main_table = "CREATE TABLE dbo.t_main(id int PRIMARY KEY NOT NULL, dtCreate float NULL, numbe_prm int NULL, tab_hist varchar(63) NULL, tab_val varchar(63) NULL, guid varchar(128) NOT NULL)";

        /// <summary>
        /// Определяет запрос на создание таблицы времени
        /// </summary>
        protected const string sql_query_create_time_table = "CREATE TABLE dbo.t_measuring(id int Primary Key NOT NULL, val_Time float NULL, val_depth real NULL)";

        /// <summary>
        /// Определяет запрос на создание служебной таблицы
        /// </summary>
        protected const string sql_query_create_param_table = "CREATE TABLE dbo.t_Param(id int primary key NOT NULL, id_param int NULL, val_param varchar(128) NULL)";

        /// <summary>
        /// Определяет шаблон запроса на создание таблицы значений для параметра
        /// </summary>
        protected const string sql_query_create_values_table = "Create Table dbo.{0} (id int primary key not null, val_prm real NULL)";

        /// <summary>
        /// Определяет шаблон запроса на создание таблицы описания параметра
        /// </summary>
        protected const string sql_query_create_history_table = "Create Table dbo.{0} (id int primary key not null, dtCreate float, main_key int, " +
                    "numbe_prm int, name_prm varchar(255), type_prm varchar(31), val_block_up real, val_block_down real, " +
                    "val_avar real, val_max real, val_min real, calibr_1 real,calibr_2 real,calibr_3 real,calibr_4 real,calibr_5 real, " +
                    "calibr_6 real,calibr_7 real, calibr_8 real,calibr_9 real,calibr_10 real,snd_avar varchar(255), snd_max varchar(255), " +
                    "graf_switch int, graf_diapz real, graf_min real, graf_max real, contr_par int, res_str varchar(255), res_float1 real, " +
                    "res_float2 real, res_int1 int, res_int2 int)";

        // --------------------------

        protected DataBaseAdapter adapter;          // адаптер сервера баз данных

        /// <summary>
        /// инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="_adapter">Адаптер для сервреа баз данных</param>
        public DataBaseProvider(DataBaseAdapter _adapter)
        {
            if (_adapter != null)
            {
                adapter = _adapter;
            }
        }

        /// <summary>
        /// Возвращяет адаптер сервера баз данных
        /// </summary>
        public DataBaseAdapter Adapter
        {
            get { return adapter; }
        }

        /// <summary>
        /// Получить список баз данных на сервере баз данных
        /// </summary>
        public string[] DataBases
        {
            get
            {
                SqlConnection connection = null;
                try
                {
                    connection = new SqlConnection();
                    connection.ConnectionString = adapter.ConnectionStringToServer;

                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        SqlCommand cmd = new SqlCommand(sql_query_select_all_db);

                        cmd.Connection = connection;
                        SqlDataReader sql_reader = cmd.ExecuteReader();

                        if (sql_reader != null && !sql_reader.IsClosed)
                        {
                            if (sql_reader.HasRows)
                            {
                                List<string> names = new List<string>();
                                while (sql_reader.Read())
                                {
                                    names.Add(sql_reader.GetString(0));
                                }
                                return
                                    names.ToArray();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
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
                return null;
            }
        }

        /// <summary>
        /// Получить список параметров базы данных.
        /// База данных должна быть загружена.
        /// </summary>
        public DataBaseParameter[] Parameters
        {
            get
            {
                SqlConnection connection = null;
                try
                {
                    connection = new SqlConnection(adapter.ConnectionString);
                    connection.Open();

                    if (connection.State == ConnectionState.Open)
                    {
                        SqlCommand command = new SqlCommand(sql_query_select_all_parameters, connection);

                        SqlDataReader result = command.ExecuteReader();
                        if (result != null)
                        {
                            if (result.IsClosed == false)
                            {
                                List<DataBaseParameter> parameters = new List<DataBaseParameter>();
                                while (result.Read())
                                {
                                    DataBaseParameter db_parameter = new DataBaseParameter();

                                    db_parameter.ID = result.GetInt32(0);
                                    db_parameter.Created = DateTime.FromOADate(result.GetDouble(1));

                                    db_parameter.Numbe_Prm = result.GetInt32(2);
                                    db_parameter.tblHistory = result.GetString(3);

                                    db_parameter.tblValues = result.GetString(4);

                                    try
                                    {
                                        db_parameter.Identifier = new Guid(result.GetString(5));
                                    }
                                    catch
                                    {
                                    }

                                    LoadParameterDescription(db_parameter);
                                    parameters.Add(db_parameter);
                                }

                                return parameters.ToArray();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                    {
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }

                        connection.Dispose();
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Создать базу данных
        /// </summary>
        /// <param name="dataBaseName">Имя создаваемой базы данных</param>
        public void CreateDataBase(string dataBaseName)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(adapter.ConnectionStringToServer);
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    string sql_query = string.Format("CREATE DATABASE {0}", dataBaseName);
                    SqlCommand cmd = connection.CreateCommand();

                    cmd.CommandText = sql_query;
                    cmd.ExecuteNonQuery();

                    // -------- тут необходимо создать таблицы --------

                    DataBaseAdapter _adapter = new DataBaseAdapter(adapter.DataSource, dataBaseName, adapter.UserID, adapter.Password);

                    InsertTable(_adapter, sql_query_create_main_table);     // создаем главную таблицу БД
                    
                    InsertTable(_adapter, sql_query_create_time_table);     // создаем таблицу времен параметров в БД
                    CreateInsexInTimeTable(_adapter);                       // создать индекс для таблицы времен

                    InsertTable(_adapter, sql_query_create_param_table);    // создаем служебную таблицу для БД
                    InitializeDataBase(_adapter);                           // инициализировать базу данных
                }
                else
                    throw new Exception("Не удалось подключиться к серверу.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
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

        /// <summary>
        /// Удалить базу данных
        /// </summary>
        /// <param name="dataBaseName">Имя удаляемой базы данных</param>
        public void DeleteDataBase(string dataBaseName)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(adapter.ConnectionStringToServer);
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    string sql_query = string.Format("DROP DATABASE {0}", dataBaseName);
                    using (SqlCommand cmd = new SqlCommand(sql_query))
                    {

                        cmd.Connection = connection;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
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

        /// <summary>
        /// Добавить параметр в базу данных.
        /// База данных должна быть загруженна.
        /// </summary>
        /// <param name="parameter">Добавляемый параметр</param>
        public void InsertParameterToDataBase(DataBaseParameter parameter)
        {
            SqlConnection connection = null;
            SqlTransaction transaction = null;

            try
            {
                connection = new SqlConnection(adapter.ConnectionString);
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    if (Peek(parameter.Identifier) == false)
                    {
                        if (Peek(parameter.ID) == false)
                        {
                            transaction = connection.BeginTransaction();

                            CreateParameterInMainTable(connection, transaction, parameter);

                            CreateParameterValuesTable(connection, transaction, parameter);
                            CreateParameterHistoryTable(connection, transaction, parameter);                            
                            
                            transaction.Commit();
                            InsertDescriptions(connection, parameter);
                        }
                        else
                            throw new Exception("Параметр с указанным номером существует в БД.");
                    }
                    else
                        throw new Exception("Параметр с указанным Guid существует в БД");
                }
                else
                    throw new Exception("Не удалось подключиться к БД.");
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex1)
                {
                    throw new Exception(ex1.Message, ex1);
                }

                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }

                    connection.Dispose();
                }

                if (transaction != null)
                    transaction.Dispose();
            }
        }

        /// <summary>
        /// Удалить параметр по его идентификатору.
        /// База данных должна быть загруженна.
        /// </summary>
        /// <param name="Identifier">Идентификатор удаляемого параметра</param>
        public void RemoveParameterFromDataBase(Guid Identifier)
        {
            SqlConnection connection = null;
            SqlTransaction transaction = null;

            try
            {
                connection = new SqlConnection(adapter.ConnectionString);
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    transaction = connection.BeginTransaction();

                    using (SqlCommand fParameter = connection.CreateCommand())
                    {
                        fParameter.Transaction = transaction;

                        fParameter.Parameters.Add(new SqlParameter("guid", Identifier.ToString()));
                        fParameter.CommandText = string.Format("Select * From {0} Where guid = @guid", t_main);

                        using (SqlDataReader reader = fParameter.ExecuteReader())
                        {
                            if (reader != null)
                            {
                                if (reader.IsClosed == false)
                                {
                                    string tblHistory = string.Empty;
                                    string tblValues = string.Empty;

                                    while (reader.Read())
                                    {
                                        tblHistory = reader.GetString(3);
                                        tblValues = reader.GetString(4);
                                    }

                                    reader.Close();

                                    if (tblHistory != string.Empty)
                                    {
                                        using (SqlCommand drop_hist = connection.CreateCommand())
                                        {
                                            drop_hist.Transaction = transaction;

                                            drop_hist.CommandText = string.Format("Drop Table {0}", tblHistory);
                                            drop_hist.ExecuteNonQuery();
                                        }
                                    }

                                    if (tblValues != string.Empty)
                                    {
                                        using (SqlCommand drop_values = connection.CreateCommand())
                                        {
                                            drop_values.Transaction = transaction;

                                            drop_values.CommandText = string.Format("Drop Table {0}", tblValues);
                                            drop_values.ExecuteNonQuery();
                                        }
                                    }

                                    using (SqlCommand eraser = connection.CreateCommand())
                                    {
                                        eraser.Transaction = transaction;

                                        eraser.Parameters.Add(new SqlParameter("guid", Identifier.ToString()));
                                        eraser.Parameters[0].SqlDbType = SqlDbType.VarChar;

                                        eraser.CommandText = string.Format("Delete From {0} Where guid = @guid", t_main);
                                        eraser.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }

                    transaction.Commit();
                }
                else
                    throw new Exception("Не удалось подключиться к БД.");
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex1)
                {
                    throw new Exception(ex1.Message, ex1);
                }

                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }

                    connection.Dispose();
                }

                if (transaction != null)
                    transaction.Dispose();
            }
        }

        /// <summary>
        /// Обновить значение свойств параметра.
        /// База данных должна быть загруженна.
        /// </summary>
        /// <param name="Description">Обновляемое свойство параметра</param>
        /// <param name="Identifier">Идентификатор параметра</param>
        public void UpdateParameter(DataBaseDescription Description, Guid Identifier)
        {
            SqlConnection connection = null;
            SqlTransaction transaction = null;

            try
            {
                connection = new SqlConnection(adapter.ConnectionString);
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    if (Peek(Identifier))
                    {
                        transaction = connection.BeginTransaction();

                        using (SqlCommand fParameter = connection.CreateCommand())
                        {
                            fParameter.Transaction = transaction;

                            fParameter.Parameters.Add(new SqlParameter("guid", Identifier.ToString()));
                            fParameter.Parameters[0].SqlDbType = SqlDbType.VarChar;

                            fParameter.CommandText = string.Format("Select * From {0} Where guid = @guid", t_main);

                            using (SqlDataReader reader = fParameter.ExecuteReader())
                            {
                                if (reader != null)
                                {
                                    if (reader.IsClosed == false)
                                    {
                                        string tblHistory = string.Empty;
                                        while (reader.Read())
                                        {
                                            tblHistory = reader.GetString(3);
                                        }

                                        reader.Close();
                                        Description.dtCreate = DateTime.Now;

                                        UpdateParameterHistory(connection, transaction, Description, tblHistory);
                                        transaction.Commit();
                                    }
                                }
                            }
                        }
                    }
                    else
                        throw new Exception("Данного параметра нет в БД.");
                }
                else
                    throw new Exception("Не удалось установить соединение с БД.");
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex1)
                {
                    throw new Exception(ex1.Message, ex1);
                }

                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }

                    connection.Dispose();
                }

                if (transaction != null)
                    transaction.Dispose();
            }
        }

        // ---------------------- методы поддержки -----------------

        /// <summary>
        /// Создать таблицу в базе данных
        /// </summary>
        /// <param name="_adapter">Адаптер для сервера баз данных</param>
        /// <param name="query">SQL запрос в котором создается таблица</param>
        protected void InsertTable(DataBaseAdapter _adapter, string query)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(_adapter.ConnectionString);
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                else
                    throw new Exception("Не удалось подключиться к серверу.Таблицы не созданы.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
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

        /// <summary>
        /// Создать индекс для таблицы времени
        /// </summary>
        /// <param name="_adapter">Адаптер БД</param>
        protected void CreateInsexInTimeTable(DataBaseAdapter _adapter)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(_adapter.ConnectionString);
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    using (SqlCommand command = new SqlCommand("CREATE INDEX time_index ON dbo.t_measuring(val_Time)", connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                else
                    throw new Exception("Не удалось подключиться к серверу.Таблицы не созданы.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
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

        /// <summary>
        /// Инициализировать базу данных
        /// </summary>
        /// <param name="_adapter">Алаптер базы данных</param>
        protected void InitializeDataBase(DataBaseAdapter _adapter)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(_adapter.ConnectionString);
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    using (SqlCommand command = new SqlCommand(sql_query_insert_first_param, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                else
                    throw new Exception("Не удалось подключиться к серверу.Таблицы не созданы.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
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

        /// <summary>
        /// Проверить наличие параметра в БД по его Guid
        /// </summary>
        /// <param name="guid">Идентификатор параметра</param>
        /// <returns>
        /// Булево значение показывающее имеется ли в БД параметр или нет. 
        /// true - в БД имеется параметр с указанным Guid, false - параметр отсутствует в БД.
        /// </returns>
        public bool Peek(Guid guid)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(adapter.ConnectionString);
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("SELECT COUNT (*) FROM dbo.t_main WHERE guid = @guid", connection);

                    SqlParameter p = new SqlParameter("guid", guid.ToString());
                    p.SqlDbType = SqlDbType.VarChar;

                    command.Parameters.Add(p);

                    int count = (Int32)command.ExecuteScalar();     // огонь

                    switch (count)
                    {
                        case 0:

                            return false;

                        case 1:

                            return true;

                        case -1:

                            return false;

                        default:

                            throw new Exception("Значение Guid не корректно. Не может быть два одинаковых идентификатора.");
                    }
                }
                else
                    throw new Exception("Не удалось подключиться к БД.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }

                    connection.Dispose();
                }
            }
        }

        /// <summary>
        /// Проверить наличие параметра в БД по его номеру
        /// </summary>
        /// <param name="numbe_prm">Номер параметра</param>
        /// <returns>
        /// Булево значение показывающее имеется ли в БД параметр или нет. 
        /// true - в БД имеется параметр с указанным Guid, false - параметр отсутствует в БД.
        /// </returns>
        public bool Peek(int numbe_prm)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(adapter.ConnectionString);
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("SELECT COUNT (*) FROM dbo.t_main WHERE numbe_prm = @numbe_prm", connection);

                    SqlParameter p = new SqlParameter("numbe_prm", numbe_prm);
                    p.SqlDbType = SqlDbType.Int;

                    command.Parameters.Add(p);

                    int count = (Int32)command.ExecuteScalar();

                    switch (count)
                    {
                        case 0:

                            return false;

                        case 1:

                            return true;

                        case -1:

                            return false;

                        default:

                            throw new Exception("Значение numbe_prm не корректно. Не может быть два одинаковых идентификатора.");
                    }
                }
                else
                    throw new Exception("Не удалось подключиться к БД.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }

                    connection.Dispose();
                }
            }
        }

        /// <summary>
        /// Получить следующий id записи
        /// </summary>
        /// <param name="table">Таблица в которой искать</param>
        /// <param name="field">Название поля в котором искать</param>
        /// <returns>В случае успеха не отрицательное число</returns>
        protected int GetNextId(string table, string field)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(adapter.ConnectionString);
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    string sql_query = string.Format("SELECT {0} FROM {1} ORDER BY {0} ASC", field, table);

                    SqlCommand command = new SqlCommand(sql_query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader != null && !reader.IsClosed)
                    {
                        if (reader.HasRows)
                        {
                            int last = -1;
                            while (reader.Read())
                            {
                                // ---- перебираем значения полей ----

                                if (last == -1)
                                {
                                    last = reader.GetInt32(0);
                                }
                                else
                                {
                                    int current = reader.GetInt32(0);
                                    if (current > (last + 1))
                                    {
                                        return (last + 1);
                                    }
                                    else
                                        last = current;
                                }
                            }

                            return (last + 1);
                        }
                        else
                            return 0;
                    }
                }
                else
                    throw new Exception("Не удалось подключиться к БД");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }

                    connection.Dispose();
                }
            }

            return -1;
        }

        /// <summary>
        /// Создать запись о параметре в главной таблице БД
        /// </summary>
        /// <param name="connection">Соединение с которым работать</param>
        /// <param name="transaction">Транзакция через которую осуществлять запись в БД</param>
        /// <param name="db_parameter">Параметр, который добавить в БД</param>
        protected void CreateParameterInMainTable(SqlConnection connection, SqlTransaction transaction,
            DataBaseParameter db_parameter)
        {
            try
            {
                db_parameter.ID = GetNextId(t_main, "id");
                db_parameter.Numbe_Prm = db_parameter.ID;

                db_parameter.Created = DateTime.Now;

                db_parameter.tblHistory = string.Format("History_{0}", db_parameter.Numbe_Prm);
                db_parameter.tblValues = string.Format("Values_{0}", db_parameter.Numbe_Prm);

                SqlCommand command = connection.CreateCommand();
                command.Transaction = transaction;

                command.Parameters.Add(new SqlParameter("id", db_parameter.ID));
                command.Parameters.Add(new SqlParameter("dtCreate", db_parameter.Created.ToOADate()));

                command.Parameters.Add(new SqlParameter("numbe_prm", db_parameter.Numbe_Prm));
                command.Parameters.Add(new SqlParameter("tab_hist", db_parameter.tblHistory));

                command.Parameters.Add(new SqlParameter("tab_val", db_parameter.tblValues));
                command.Parameters.Add(new SqlParameter("guid", db_parameter.Identifier.ToString()));

                command.CommandText = string.Format("Insert Into {0} Values (@id, @dtCreate, @numbe_prm, @tab_hist, @tab_val, @guid)", t_main);
                if (command.ExecuteNonQuery() != 1)
                {
                    throw new Exception("Не удалось создать запись параметра в главной таблице БД.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Создать таблицу значений параметра
        /// </summary>
        /// <param name="connection">Соединение с которым работать</param>
        /// <param name="transaction">Транзакция через которую осуществлять запись в БД</param>
        /// <param name="db_parameter">Параметр, который добавить в БД</param>
        protected void CreateParameterValuesTable(SqlConnection connection, SqlTransaction transaction,
            DataBaseParameter db_parameter)
        {
            try
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.Transaction = transaction;

                    command.CommandText = string.Format(sql_query_create_values_table, db_parameter.tblValues);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Создать таблицу описания параметра
        /// </summary>
        /// <param name="connection">Соединение с которым работать</param>
        /// <param name="transaction">Транзакция через которую осуществлять запись в БД</param>
        /// <param name="db_parameter">Параметр, который добавить в БД</param>
        protected void CreateParameterHistoryTable(SqlConnection connection, SqlTransaction transaction,
            DataBaseParameter db_parameter)
        {
            try
            {
                if (db_parameter.Descriptions.Count == 0)
                {
                    DataBaseDescription description = new DataBaseDescription();

                    description.dtCreate = db_parameter.Created;
                    description.ID = 0;

                    description.MainKey = db_parameter.ID;
                    description.NameParameter = string.Empty;

                    description.NumberParameter = db_parameter.Numbe_Prm;
                    description.TypeParameter = string.Empty;

                    description.NameParameter = "Параметр " + db_parameter.ID.ToString();
                    db_parameter.Descriptions.Add(description);
                }
                else
                    foreach (DataBaseDescription desc in db_parameter.Descriptions)
                    {
                        desc.NumberParameter = db_parameter.ID;
                        desc.MainKey = db_parameter.ID;

                        desc.dtCreate = db_parameter.Created;
                    }

                using (SqlCommand tblCommand = connection.CreateCommand())
                {
                    tblCommand.Transaction = transaction;
                    tblCommand.CommandText = string.Format(sql_query_create_history_table, db_parameter.tblHistory);

                    tblCommand.ExecuteNonQuery();
                }                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Добавить описание параметра
        /// </summary>
        /// <param name="connection">Соединение с которым работать</param>
        /// <param name="db_parameter">Параметр описание которого сохранять в базу данных</param>
        protected void InsertDescriptions(SqlConnection connection, DataBaseParameter db_parameter)
        {
            foreach (DataBaseDescription desc in db_parameter.Descriptions)
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.Parameters.Add(new SqlParameter("id", desc.ID));
                    command.Parameters.Add(new SqlParameter("dtCreate", desc.dtCreate.ToOADate()));

                    command.Parameters.Add(new SqlParameter("main_key", desc.MainKey));
                    command.Parameters.Add(new SqlParameter("numbe_prm", desc.NumberParameter));

                    command.Parameters.Add(new SqlParameter("name_prm", desc.NameParameter));
                    command.Parameters.Add(new SqlParameter("type_prm", desc.TypeParameter));

                    command.CommandText = string.Format("Insert Into dbo.{0} Values (@id, @dtCreate, @main_key, @numbe_prm, @name_prm, @type_prm, " + 
                        "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,'','', 0,0,0,0,0, '', 0,0,0,0)", db_parameter.tblHistory);
                    if (command.ExecuteNonQuery() != 1)
                    {
                        throw new Exception("Не удалось добавить описание параметра в БД.");
                    }
                }
            }
        }

        /// <summary>
        /// Добавить свойства параметра. Свойство добавляется как есть, то есть поля не актуализируются
        /// поэтому создавать свойство лучше на основе имеющегося свойства настроенного!!!
        /// </summary>
        /// <param name="connection">Соединение с которым работать</param>
        /// <param name="transaction">Транзакция через которую осуществлять запись в БД</param>
        /// <param name="db_description">Свойство, которое добавить</param>
        /// <param name="table">Таблица, в которую добавить свойство</param>
        protected void UpdateParameterHistory(SqlConnection connection, SqlTransaction transaction,
            DataBaseDescription db_description, string table)
        {
            try
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.Transaction = transaction;

                    db_description.ID = GetNextId(table, "id");
                    db_description.dtCreate = DateTime.Now;

                    command.Parameters.Add(new SqlParameter("id", db_description.ID));
                    command.Parameters.Add(new SqlParameter("dtCreate", db_description.dtCreate.ToOADate()));

                    command.Parameters.Add(new SqlParameter("main_key", db_description.MainKey));
                    command.Parameters.Add(new SqlParameter("numbe_prm", db_description.NumberParameter));

                    command.Parameters.Add(new SqlParameter("name_prm", db_description.NameParameter));
                    command.Parameters.Add(new SqlParameter("type_prm", db_description.TypeParameter));

                    command.CommandText = string.Format("Insert Into dbo.{0} Values (@id, @dtCreate, @main_key, @numbe_prm, @name_prm, @type_prm, ", table);
                    if (command.ExecuteNonQuery() != 1)
                    {
                        throw new Exception("Не удалось добавить описание параметра в БД.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Загрузить описание параметра из БД
        /// </summary>
        /// <param name="db_parameter">Параметр для которого загрузить описание</param>
        private void LoadParameterDescription(DataBaseParameter db_parameter)
        {
            SqlConnection connection = null;
            try
            {
                string sql_query = string.Format("SELECT * FROM {0}", db_parameter.tblHistory);

                connection = new SqlConnection(adapter.ConnectionString);
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    using (SqlCommand command = new SqlCommand(sql_query, connection))
                    {
                        using (SqlDataReader result = command.ExecuteReader())
                        {
                            if (result != null)
                            {
                                if (result.IsClosed == false)
                                {
                                    while (result.Read())
                                    {
                                        DataBaseDescription db_description = new DataBaseDescription();

                                        db_description.ID = result.GetInt32(0);
                                        db_description.dtCreate = DateTime.FromOADate(result.GetDouble(1));

                                        db_description.MainKey = result.GetInt32(2);
                                        db_description.NumberParameter = result.GetInt32(3);

                                        db_description.NameParameter = result.GetString(4);
                                        db_description.TypeParameter = result.GetString(5);

                                        db_parameter.Descriptions.Add(db_description);
                                    }
                                }
                            }
                            else
                                throw new Exception("Не удалось считать данные описания параметра");
                        }
                    }
                }
                else
                    throw new Exception("Не удалось подключиться к серверу БД.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }

                    connection.Dispose();
                }
            }
        }
    }
}