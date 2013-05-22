using System;
using System.Xml;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Изменение расхода на выходе
    /// </summary>
    public class P0105 : TParameter
    {
        protected float starting_point;             // точка отсчета
        protected DateTime time_fixation;           // Определяет время фиксации точки отсчёта

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P0105(Guid p_identifier)
            : base(p_identifier, "P0105", "Изменение расхода на выходе")
        {
            simple = false;

            starting_point = float.NaN;
            time_fixation = DateTime.MinValue;
        }

        /// <summary>
        /// Определяет точку отсчета
        /// </summary>
        public float StartingPoint
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return starting_point;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return float.NaN;
            }

            set
            {
                if (slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        starting_point = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Вычисляет значение параметра Изменение расхода на выходе
        /// </summary>
        /// <param name="v1">Реализует параметр Поток на выходе Датчик</param>
        public void Calculate(P0003 v1)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    if (float.IsNaN(v1.Value))
                    {
                        _value = float.NaN;
                    }
                    else
                    {
                        if (float.IsNaN(starting_point))
                        {
                            starting_point = v1.Value;
                        }

                        _value = v1.Value - starting_point;
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
        /// Имя узла в котором сохраняется период усреднения 
        /// </summary>
        protected const string StartingPointName = "starting_point";

        /// <summary>
        /// Имя узла в котором сохраняется время фиксации значения параметра
        /// </summary>
        protected const string TimeFixationName = "time_fixation";

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
                        XmlNode StartingPointNode = document.CreateElement(StartingPointName);
                        XmlNode TimeFixationNode = document.CreateElement(TimeFixationName);

                        StartingPointNode.InnerText = starting_point.ToString();
                        TimeFixationNode.InnerText = time_fixation.ToString();

                        root.AppendChild(StartingPointNode);
                        root.AppendChild(TimeFixationNode);

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
                                    case StartingPointName:

                                        try
                                        {
                                            starting_point = float.Parse(Child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case TimeFixationName:

                                        try
                                        {
                                            time_fixation = DateTime.Parse(Child.InnerText);
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
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        // ----------------------------------------------------------------------------
    }
}