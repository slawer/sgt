using System;
using System.Threading;

using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace DataBase
{
    /// <summary>
    /// Статический класс, реализующий загруженную базу данных.
    /// Хранение загруженных данных.
    /// </summary>
    public static class DataBase
    {
        private static DataBaseState state;                         // текущее состояние базы данных
        private static ReaderWriterLockSlim s_slim;                 // синхронизатор доступа к состоянию БД

        private static t_measuring measuring;                       // таблица времен базы данных
        private static DataBaseParameters parameters = null;        // Список загруженных параметров из базы данных

        private static DataBaseProvider _provider;                  // класс реализующий сервисные функции работы с базой данных

        /// <summary>
        /// Выполнить инициалтзацию базы данных
        /// </summary>
        internal static void Initialize()
        {
            state = DataBaseState.Default;
            s_slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

            parameters = new DataBaseParameters(1024);
        }

        /// <summary>
        /// Возвращяет текущее состояние базы данных
        /// </summary>
        public static DataBaseState State
        {
            get
            {
                if (s_slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return state;
                    }
                    finally
                    {
                        s_slim.ExitReadLock();
                    }
                }

                return DataBaseState.Default;
            }

            private set
            {
                if (s_slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        state = value;
                    }
                    finally
                    {
                        s_slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Загрузить базу данных
        /// </summary>
        /// <param name="provider">Класс реализующий сервисные функции работы с базой данных</param>
        /// <returns>Состояние базы данных после выполнение операции загрузки БД</returns>
        public static DataBaseState LoadDB(DataBaseProvider provider)
        {
            if (provider != null)
            {
                switch (State)
                {
                    case DataBaseState.Loaded:

                        throw new InvalidOperationException("Не допустимая операция. Перез загрузкой БД, необходимо завершить работу с текущей.");

                    case DataBaseState.Unloaded:
                    case DataBaseState.Default:


                        parameters.Clear();

                        try
                        {
                            DataBaseParameter[] parames = provider.Parameters;
                            if (parames != null)
                            {
                                foreach (DataBaseParameter parameter in parames)
                                {
                                    if (parameter != null)
                                    {
                                        parameters.Insert(parameter);
                                    }
                                }

                                _provider = provider;
                                measuring = t_measuring.Virtualize(provider.Adapter);
                            }

                            State = DataBaseState.Loaded;
                            return State;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message, ex);
                        }

                    default:
                        break;
                }
            }

            return DataBaseState.Default;
        }

        /// <summary>
        /// Закрыть базу данных
        /// </summary>
        public static void CloseDB()
        {
            switch (State)
            {
                case DataBaseState.Loaded:

                    SqlConnection.ClearAllPools();
                    State = DataBaseState.Unloaded;

                    parameters.Clear();

                    break;

                default:
                    break;
            }
        }        

        /// <summary>
        /// Сохранить значение параметра в БД
        /// </summary>
        /// <param name="p_identifier">Идентификатор параметра</param>
        /// <param name="p_time">Время поступления значения параметра</param>
        /// <param name="p_value">Значение параметра</param>
        public static void SaveParameter(Guid p_identifier, DateTime p_time, float p_value)
        {
            SqlConnection connection = null;
            try
            {
                if (!float.IsNaN(p_value) && !float.IsInfinity(p_value) &&
                    !float.IsNegativeInfinity(p_value) && !float.IsPositiveInfinity(p_value))
                {
                    DataBaseParameter parameter = parameters.GetParameter(p_identifier);
                    if (parameter != null)
                    {
                        int index = measuring.GetTimeIndex(p_time.Ticks);
                        if (index > -1)
                        {
                            connection = new SqlConnection(_provider.Adapter.ConnectionString);
                            connection.Open();

                            if (connection.State == ConnectionState.Open)
                            {
                                using (SqlCommand command = connection.CreateCommand())
                                {
                                    if (_provider.Peek(p_identifier) == false)
                                    {
                                        _provider.InsertParameterToDataBase(parameter);
                                    }

                                    command.Parameters.Add(new SqlParameter("id", index));
                                    command.Parameters.Add(new SqlParameter("val", p_value));

                                    command.CommandText = string.Format("Insert Into dbo.{0} (id, val_prm) Values (@id, @val)", parameter.tblValues);
                                    //command.ExecuteNonQuery();

                                    try
                                    {
                                        if (command.ExecuteNonQuery() != 1)
                                        {
                                            if (_provider.Peek(p_identifier) == false)
                                            {
                                                //create_and_save_1(p_identifier, p_time, p_value);// _provider.InsertParameterToDataBase(parameter);
                                            }
                                            //throw new Exception("Не удалось сохранить параметр в БД");
                                        }
                                    }
                                    catch
                                    {
                                        if (_provider.Peek(p_identifier) == false)
                                        {
                                            _provider.InsertParameterToDataBase(parameter);
                                        }
                                    }
                                }
                            }
                            else
                                throw new Exception("Не удалось установить соединение с БД");
                        }
                    }
                    else
                    {
                        // ----- не нашли параметр в списке ----

                        create_and_save(p_identifier, p_time, p_value);
                    }
                }
                else
                {
                    DataBaseParameter parameter = parameters.GetParameter(p_identifier);
                    if (parameter == null)
                    {
                        create_and_save(p_identifier, p_time, p_value);
                    }
                }
            }
            catch { }
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
        /// Создать новый параметр, добавить его в список, создать в базе данных
        /// и сохранить значение данного параметра
        /// </summary>
        /// <param name="p_identifier">Идентификатор параметра</param>
        /// <param name="p_time">Время поступления значения параметра</param>
        /// <param name="p_value">Значение параметра</param>
        private static void create_and_save(Guid p_identifier, DateTime p_time, float p_value)
        {
            SqlConnection connection = null;
            try
            {
                parameters.Insert(new DataBaseParameter(p_identifier));

                DataBaseParameter parameter = parameters.GetParameter(p_identifier);
                if (parameter != null)
                {
                    int index = measuring.GetTimeIndex(p_time.Ticks);
                    if (index > -1)
                    {
                        connection = new SqlConnection(_provider.Adapter.ConnectionString);
                        connection.Open();

                        if (connection.State == ConnectionState.Open)
                        {
                            using (SqlCommand command = connection.CreateCommand())
                            {
                                if (_provider.Peek(p_identifier) == false)
                                {
                                    _provider.InsertParameterToDataBase(parameter);
                                }

                                command.Parameters.Add(new SqlParameter("id", index));
                                command.Parameters.Add(new SqlParameter("val", p_value));

                                command.CommandText = string.Format("Insert Into dbo.{0} (id, val_prm) Values (@id, @val)", parameter.tblValues);
                                if (command.ExecuteNonQuery() != 1)
                                {
                                    throw new Exception("Не удалось сохранить параметр в БД");
                                }
                            }
                        }
                        else
                            throw new Exception("Не удалось установить соединение с БД");
                    }
                }
            }
            catch (Exception ex)
            {
                string mes = ex.Message;
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
        /// Создать новый параметр, добавить его в список, создать в базе данных
        /// и сохранить значение данного параметра
        /// </summary>
        /// <param name="p_identifier">Идентификатор параметра</param>
        /// <param name="p_time">Время поступления значения параметра</param>
        /// <param name="p_value">Значение параметра</param>
        private static void create_and_save_1(Guid p_identifier, DateTime p_time, float p_value)
        {
            SqlConnection connection = null;
            try
            {
                //parameters.Insert(new DataBaseParameter(p_identifier));

                DataBaseParameter par = parameters.GetParameter(p_identifier);
                if (par == null)
                {
                    parameters.Insert(new DataBaseParameter(p_identifier));
                }

                DataBaseParameter parameter = parameters.GetParameter(p_identifier);
                if (parameter != null)
                {
                    int index = measuring.GetTimeIndex(p_time.Ticks);
                    if (index > -1)
                    {
                        connection = new SqlConnection(_provider.Adapter.ConnectionString);
                        connection.Open();

                        if (connection.State == ConnectionState.Open)
                        {
                            using (SqlCommand command = connection.CreateCommand())
                            {
                                if (_provider.Peek(p_identifier) == false)
                                {
                                    _provider.InsertParameterToDataBase(parameter);
                                }

                                command.Parameters.Add(new SqlParameter("id", index));
                                command.Parameters.Add(new SqlParameter("val", p_value));

                                command.CommandText = string.Format("Insert Into dbo.{0} (id, val_prm) Values (@id, @val)", parameter.tblValues);
                                if (command.ExecuteNonQuery() != 1)
                                {
                                    throw new Exception("Не удалось сохранить параметр в БД");
                                }
                            }
                        }
                        else
                            throw new Exception("Не удалось установить соединение с БД");
                    }
                }
            }
            catch { }
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
        /// Реализует таблицу в которой храниться время для значений параметров
        /// </summary>
        private class t_measuring
        {
            private int lastId;                             // последний добавленный идентификатор записи
            private long lastTime;                          // последнее добавленное значение времени в таблицу

            private float lastDept;                         // последнее добавленное значение глубины
            private DataBaseAdapter adapter;                // информация о подключении к базе данных

            /// <summary>
            /// Инициализирует новый экземпляр класса
            /// </summary>
            /// <param name="Adapter">Адаптер БД</param>
            internal t_measuring(DataBaseAdapter Adapter)
            {
                lastId = -1;
                lastDept = -1;

                lastTime = 0;

                adapter = Adapter;
            }

            /// <summary>
            /// Определяет последний добавленный идентификатор записи
            /// </summary>
            public int LastId
            {
                get { return lastId; }
                set { lastId = value; }
            }

            /// <summary>
            /// Определяет последнее добавленное значение времени в таблицу
            /// </summary>
            public long LastTime
            {
                get { return lastTime; }
                set { lastTime = value; }
            }

            /// <summary>
            /// Определяет последнее добавленное значение глубины
            /// </summary>
            public float LastDept
            {
                get { return lastDept; }
                set { lastDept = value; }
            }

            /// <summary>
            /// Проверить корректность времени
            /// </summary>
            /// <param name="time"></param>
            /// <returns>Индекс от указанного времени</returns>
            public int GetTimeIndex(long time)
            {
                DateTime last = DateTime.FromBinary(lastTime);
                DateTime current = DateTime.FromBinary(time);

                try
                {
                    if (last == current)

                        return lastId;

                    else
                        if (last < current)
                        {
                            // ---- необходимо добавить новое время в БД ----

                            InsertNewTime(current, (lastId + 1));

                            lastTime = time;
                            lastId = lastId + 1;

                            return lastId;
                        }
                        else
                            if (last > current)
                            {
                                // ---- не оч правильная ситуация, но обработать нужно ----

                                return -1;
                            }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }

                throw new Exception("Невероятная ситуация!!!! Такого быть не должно в принципе!!!");
            }

            /// <summary>
            /// Добавить новое время в таблицу, хранящей врема поступления значений параметров
            /// </summary>
            /// <param name="time"></param>
            /// <param name="id"></param>
            private void InsertNewTime(DateTime time, int id)
            {
                SqlConnection connection = null;

                try
                {
                    connection = new SqlConnection(adapter.ConnectionString);
                    connection.Open();

                    if (connection.State == ConnectionState.Open)
                    {
                        using (SqlCommand command = connection.CreateCommand())
                        {

                            command.Parameters.Add(new SqlParameter("id", id));
                            command.Parameters.Add(new SqlParameter("val_Time", time.ToOADate()));

                            command.CommandText = string.Format("Insert Into {0} (id, val_Time, val_depth) Values (@id, @val_Time, 0)", "dbo.t_measuring");
                            if (command.ExecuteNonQuery() != 1)
                            {
                                throw new Exception("Не удалось добавить новое время в БД.");
                            }
                        }
                    }
                    else
                        throw new Exception("Не удалось установить соединение с сервером БД");
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
            /// Виртуализируем таблицу времени
            /// </summary>
            /// <param name="Adapter">Объет определяющий подключение к БД</param>
            /// <returns>В случае успеха виртуализировнная таблица времени, в противном случае null</returns>
            public static t_measuring Virtualize(DataBaseAdapter Adapter)
            {
                SqlConnection connection = null;
                try
                {
                    connection = new SqlConnection(Adapter.ConnectionString);
                    connection.Open();

                    if (connection.State == ConnectionState.Open)
                    {
                        using (SqlCommand command = connection.CreateCommand())
                        {

                            command.CommandText = string.Format("Select * From {0} Order By id Desc", "dbo.t_measuring");
                            using (SqlDataReader reader = command.ExecuteReader())
                            {

                                t_measuring measuring = new t_measuring(Adapter);

                                measuring.LastId = 0;
                                measuring.LastTime = DateTime.Now.Ticks;

                                measuring.LastDept = 0.0f;

                                if (reader != null)
                                {
                                    if (reader.IsClosed == false)
                                    {
                                        while (reader.Read())
                                        {
                                            measuring.LastId = reader.GetInt32(0);

                                            DateTime t = DateTime.FromOADate(reader.GetDouble(1));
                                            measuring.LastTime = t.Ticks;

                                            measuring.LastDept = 0.0f;
                                            break;
                                        }
                                    }

                                    return measuring;
                                }
                                else
                                    throw new Exception("Не удалось загрузить таблицу времени из БД");
                            }
                        }
                    }
                    else
                        throw new Exception("Не удалось установить соединение с БД");

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

    /// <summary>
    /// Определяет текущее состояние базы данных
    /// </summary>
    public enum DataBaseState
    {
        /// <summary>
        /// База данных загружена
        /// </summary>
        Loaded,

        /// <summary>
        /// База данных выгружена
        /// </summary>
        Unloaded,

        /// <summary>
        /// По умолчанию. Работа с базой данных есчо не выполнялась.
        /// </summary>
        Default
    }
}