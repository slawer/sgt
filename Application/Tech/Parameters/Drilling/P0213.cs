using System;
using System.Xml;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Время бурения
    /// </summary>
    public class P0213 : TParameter
    {
        /// <summary>
        /// Константа Над забоем / Бурение / Бурение
        /// </summary>
        public static int НадЗабоем_Бурение_Бурение = 0 + 128 + 6;

        // -----------------------------------------------------------------

        protected static DateTime current_state_time;   // Время, соответствующее текущему значению
        protected static long exact_time_drilling;      // Хранит текущее значение параметра в милисекундах

        protected bool is_change_time;                  // изменить или нет время циркуляции

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор параметра</param>
        public P0213(Guid p_identifier)
            : base(p_identifier, "P0213", "Время бурения")
        {
            simple = false;

            _value = 0;
            is_change_time = false;

            exact_time_drilling = 0;
            current_state_time = DateTime.MinValue;
        }

        /// <summary>
        /// Определяет изменить время циркуляции или нет
        /// </summary>
        public bool IsChangeTime
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return is_change_time;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        is_change_time = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Хранит текущее значение параметра в милисекундах
        /// </summary>
        public long ExactTimeCirculation
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return exact_time_drilling;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return 0;
            }
        }

        /// <summary>
        /// Установить новое время бурения
        /// </summary>
        /// <param name="value">Новое значение времени бурения в тиках</param>
        public void Reset(long value)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    is_change_time = true;                    
                    current_state_time = DateTime.MinValue;
                    exact_time_drilling = value;// *TimeSpan.TicksPerMillisecond;
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Вычисляет текущее значение параметра Время бурения
        /// </summary>
        /// <param name="м1">Состояние процесса бурения в текущий момент</param>
        /// <param name="currentTime">Текущее технологическое время</param>
        public void Calculate(P0206 v1, DateTime currentTime)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    if (is_change_time)
                    {
                        is_change_time = false;
                        current_state_time = currentTime;

                        _value = exact_time_drilling / TimeSpan.TicksPerMillisecond;
                        _value = _value / 3600000;
                    }
                    else
                    {
                        if (float.IsNaN(v1.Value))
                        {
                            current_state_time = DateTime.MinValue;
                        }
                        else
                        {
                            int state = (int)Math.Round(v1.Value);
                            if (state != НадЗабоем_Бурение_Бурение)
                            {
                                current_state_time = DateTime.MinValue;
                            }
                            else
                            {
                                if (current_state_time == DateTime.MinValue)
                                {
                                    current_state_time = currentTime;
                                }
                                else
                                {
                                    DateTime _T = currentTime;
                                    long lT = _T.Ticks - current_state_time.Ticks;

                                    exact_time_drilling += lT;

                                    _value = exact_time_drilling / TimeSpan.TicksPerMillisecond;
                                    _value = _value / 3600000;

                                    current_state_time = _T;
                                }
                            }
                        }
                    }
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        // --------------------------- сохранение параметра ---------------------------

        /// <summary>
        /// Имя узла в котором сохраняется Время, соответствующее текущему значению
        /// </summary>
        protected const string CurrentStateTimeName = "current_state_time";

        /// <summary>
        /// Имя узла в котором сохраняется Хранит текущее значение параметра в милисекундах
        /// </summary>
        protected const string ExactTimeCirculationName = "exact_time_circulation";

        /// <summary>
        /// Имя узла в котором сохраняется изменить или нет время циркуляции
        /// </summary>
        protected const string IsChangeTimeName = "is_change_time";

        /// <summary>
        /// Сохранить параметр в Xml узел
        /// </summary>
        /// <param name="document">XML документ, куда будет добавлен данный XmlNode</param>
        /// <returns>Узел в котором сохранен параметр</returns>
        public override XmlNode Save(XmlDocument document)
        {
            XmlNode root = base.Save(document);
            if (root != null)
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        XmlNode CurrentStateTimeNode = document.CreateElement(CurrentStateTimeName);
                        XmlNode ExactTimeCirculationNode = document.CreateElement(ExactTimeCirculationName);

                        XmlNode IsChangeTimeNode = document.CreateElement(IsChangeTimeName);

                        CurrentStateTimeNode.InnerText = current_state_time.ToString();
                        ExactTimeCirculationNode.InnerText = exact_time_drilling.ToString();

                        IsChangeTimeNode.InnerText = is_change_time.ToString();

                        root.AppendChild(CurrentStateTimeNode);
                        root.AppendChild(ExactTimeCirculationNode);

                        root.AppendChild(IsChangeTimeNode);

                        return root;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Загрузить параметр из Xml узла
        /// </summary>
        /// <param name="Node">Xml узел в котором сохранен параметр</param>        
        public override void Load(XmlNode Node)
        {
            base.Load(Node);
            if (slim.TryEnterWriteLock(500))
            {
                try
                {
                    if (Node != null && Node.HasChildNodes)
                    {
                        if (Node.Name == RootName)
                        {
                            foreach (XmlNode Child in Node.ChildNodes)
                            {
                                switch (Child.Name)
                                {
                                    case CurrentStateTimeName:

                                        try
                                        {
                                            current_state_time = DateTime.Parse(Child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case ExactTimeCirculationName:

                                        try
                                        {
                                            exact_time_drilling = long.Parse(Child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case IsChangeTimeName:

                                        try
                                        {
                                            is_change_time = bool.Parse(Child.InnerText);
                                        }
                                        catch { }
                                        break;

                                }
                            }
                        }
                    }
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        // ----------------------------------------------------------------------------
    }
}