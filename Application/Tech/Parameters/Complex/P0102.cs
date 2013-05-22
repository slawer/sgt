using System;
using System.Xml;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Вес на крюке.
    /// </summary>
    public class P0102 : TParameter
    {
        protected SensorType s_type;                    // тип используемого датчика

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор параметра</param>
        public P0102(Guid p_identifier)
            : base(p_identifier, "P0102", "Вес на крюке")
        {
            simple = false;
        }

        /// <summary>
        /// Определяет тип используемого датчика для измерения Веса на крюке
        /// </summary>
        public SensorType Source
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return s_type;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return SensorType.Default; 
            }

            set
            {
                if (slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        s_type = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }
        /// <summary>
        /// Вычисляет текущее значение параметра Вес на крюке
        /// </summary>
        /// <param name="v1">Вес на крюке Датчик</param>
        /// <param name="v2">Вес на крюке Аналоговый</param>
        public void Calculate(P0001 v1, P0013 v2)
        {
            if (slim.TryEnterWriteLock(100))
            {
                try
                {
                    switch (s_type)
                    {
                        case SensorType.Sensor:

                            _value = v1.Value;
                            break;

                        case SensorType.Analog:

                            _value = v2.Value;
                            break;

                        default:
                            break;
                    }
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Определяет типы датчиков
        /// </summary>
        public enum SensorType
        {
            /// <summary>
            /// Датчик веса
            /// </summary>
            Sensor,

            /// <summary>
            /// Аналоговый сигнал
            /// </summary>
            Analog,

            /// <summary>
            /// Тип датчика не определен
            /// </summary>
            Default
        }

        // --------------------------- сохранение параметра ---------------------------

        /// <summary>
        /// Имя узла в котором сохраняется источник значения для параметра
        /// </summary>
        private const string SourceTypeName = "Source";

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
                        XmlNode SourceTypeNode = document.CreateElement(SourceTypeName);

                        SourceTypeNode.InnerText = s_type.ToString();
                        root.AppendChild(SourceTypeNode);

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
                                    case SourceTypeName:

                                        try
                                        {
                                            s_type = (SensorType)Enum.Parse(typeof(SensorType), Child.InnerText);
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