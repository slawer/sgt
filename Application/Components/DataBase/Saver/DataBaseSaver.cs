using System;
using System.Threading;
using System.Collections.Generic;

namespace DataBase
{
    /// <summary>
    /// Реализует сохранение параметров в базу данных
    /// </summary>
    public class DataBaseSaver
    {
        protected List<SaverParameter> parameters;      // буфер значений параметров для сохранения в базе данных
        protected ReaderWriterLockSlim p_slim;          // синхронизатор списка значений сохраняемых в базу данных

        protected DataBaseSaverStates state;            // текущее состояние
        protected ReaderWriterLockSlim s_slim;          // синхронизатор состояния 
        
        protected Timer t_saver;                        // инициирует сохранение параметров в базу даных
        protected ReaderWriterLockSlim t_slim;          // синхронизатор таймера
        
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public DataBaseSaver()
        {
            s_slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
            p_slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

            state = DataBaseSaverStates.Default;

            parameters = new List<SaverParameter>();

            t_saver = new Timer(TimerCallback, null, Timeout.Infinite, 1000);
            t_slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        }

        /// <summary>
        /// Возвращяет текущее состояние меденжера сохранения параметров в базу данных
        /// </summary>
        public DataBaseSaverStates State
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

                return DataBaseSaverStates.Default;
            }

            protected set
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
        /// Запустить сохранение параметров в базу данных
        /// </summary>
        public void Start()
        {
            if (State != DataBaseSaverStates.Started)
            {
                State = DataBaseSaverStates.Started;
                t_saver.Change(0, 1000);
            }
        }

        /// <summary>
        /// Остановить сохранение парамтров в базу данных
        /// </summary>
        public void Stop()
        {
            if (State != DataBaseSaverStates.Stopped)
            {
                State = DataBaseSaverStates.Stopped;
                t_saver.Change(Timeout.Infinite, 1000);
            }
        }

        /// <summary>
        /// Добавить значение параметра на сохранение
        /// </summary>
        /// <param name="Identifier">Идентификатор параметра</param>
        /// <param name="Time">Время поступления значения параметра</param>
        /// <param name="Value">Значение параметра</param>
        public void PushToSave(Guid Identifier, DateTime Time, Single Value)
        {
            switch (State)
            {
                case DataBaseSaverStates.Started:

                    if (p_slim.TryEnterWriteLock(500))
                    {
                        try
                        {
                            parameters.Add(new SaverParameter(Identifier, Time, Value));
                        }
                        finally
                        {
                            p_slim.ExitWriteLock();
                        }
                    }
                    break;

                case DataBaseSaverStates.Stopped:

                    break;

                case DataBaseSaverStates.Default:

                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Реализует сохранение значений параметров в базу данных
        /// </summary>
        /// <param name="state">Не используется</param>
        protected void TimerCallback(object state)
        {
            if (t_slim.TryEnterWriteLock(0))
            {
                try
                {
                    switch (State)
                    {
                        case DataBaseSaverStates.Started:

                            List<SaverParameter> list = GetParameters();
                            if (list != null)
                            {
                                // ------- сохраняем значения параметров -------

                                foreach (SaverParameter parameter in list)
                                {
                                    try
                                    {
                                        if (float.IsNaN(parameter.Value) || float.IsInfinity(parameter.Value) ||
                                            float.IsNegativeInfinity(parameter.Value) || float.IsPositiveInfinity(parameter.Value))
                                        {
                                            DataBase.SaveParameter(parameter.Identifier, parameter.Time, 2e20f);
                                        }
                                        else
                                            DataBase.SaveParameter(parameter.Identifier, parameter.Time, parameter.Value);
                                    }
                                    catch { }
                                }
                            }

                            break;

                        case DataBaseSaverStates.Stopped:

                            break;

                        case DataBaseSaverStates.Default:

                            break;

                        default:
                            break;
                    }
                }
                catch { }
                finally
                {
                    t_slim.ExitWriteLock();
                }
            }
        }

        // --------------- вспомогательные методы ---------------

        /// <summary>
        /// Получить значения параметров
        /// </summary>
        /// <returns>Список значений параметров</returns>
        protected List<SaverParameter> GetParameters()
        {
            if (p_slim.TryEnterWriteLock(300))
            {
                try
                {
                    List<SaverParameter> lst = new List<SaverParameter>();
                    lst.AddRange(parameters.ToArray());

                    parameters.Clear();

                    return lst;
                }
                finally
                {
                    p_slim.ExitWriteLock();
                }
            }

            return null;
        }

        /// <summary>
        /// Реализует хранение информации о сохраняемом параметре
        /// </summary>
        protected class SaverParameter
        {
            private float _value;           // значение параметра
            private DateTime time;          // время сохранения параметра в БД

            private Guid identifier;        // идентификатор параметра

            /// <summary>
            /// Инициализирует новый экземпляр класса
            /// </summary>
            public SaverParameter()
            {
                _value = 0.0f;
                time = DateTime.MinValue;

                identifier = Guid.Empty;
            }

            /// <summary>
            /// Инициализтрует новый экземпляр класса
            /// </summary>
            /// <param name="p_identifier">Идентификатор параметра</param>
            /// <param name="p_value">Значение параметра</param>
            public SaverParameter(Guid p_identifier, float p_value)
            {
                _value = p_value;
                time = DateTime.MinValue;

                identifier = p_identifier;
            }

            /// <summary>
            /// Инициализтрует новый экземпляр класса
            /// </summary>
            /// <param name="p_identifier">Идентификатор параметра</param>
            /// <param name="p_time">Время поступления значения параметра</param>
            /// <param name="p_value">Значение параметра</param>
            public SaverParameter(Guid p_identifier, DateTime p_time, float p_value)
            {
                _value = p_value;
                time = p_time;

                identifier = p_identifier;
            }

            /// <summary>
            /// Возвращяет значение параметра
            /// </summary>
            public float Value
            {
                get { return _value; }
            }

            /// <summary>
            /// Возвращяет время поступления параметра
            /// </summary>
            public DateTime Time
            {
                get { return time; }
            }

            /// <summary>
            /// Возвращяет идентификатор параметра
            /// </summary>
            public Guid Identifier
            {
                get { return identifier; }
            }            
        }

        /// <summary>
        /// Текущее состояние управляющего сохранением параметров в базу данных
        /// </summary>
        public enum DataBaseSaverStates
        {
            /// <summary>
            /// Остановленно сохранение параметров
            /// </summary>
            Stopped,

            /// <summary>
            /// Выполняется сохранение параметров в базу данных
            /// </summary>
            Started,

            /// <summary>
            /// По умолчанию
            /// </summary>
            Default
        }
    }
}