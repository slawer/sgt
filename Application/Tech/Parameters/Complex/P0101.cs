using System;
using System.Xml;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Крутящий момент ротора
    /// </summary>
    public class P0101 : TParameter
    {
        protected SourceType s_type;            // источник значения для параметра

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P0101(Guid p_identifier)
            : base(p_identifier, "P0101", "Крутящий момент ротора")
        {
            simple = false;
            s_type = SourceType.Analog;            
        }

        /// <summary>
        /// Определяет источник поступления данных для параметра
        /// </summary>
        public SourceType Source
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

                return SourceType.Default;
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
        /// Вычислить значение технологического параметра
        /// </summary>
        /// <param name="p0002">Крутящий момент ротора с Аналогового сигнала</param>
        /// <param name="p0016">Крутящий момент ротора АСУ</param>
        public void Calculate(P0002 p0002, P0016 p0016)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    switch (s_type)
                    {
                        case SourceType.Analog:

                            _value = p0002.Value;
                            break;

                        case SourceType.Asy:

                            _value = p0016.Value;
                            break;

                        case SourceType.Default:
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
        /// Перечисляет возможные источники значения параметра
        /// </summary>
        public enum SourceType
        {
            /// <summary>
            /// Источником значения является аналоговый сигнал
            /// </summary>
            Analog,

            /// <summary>
            /// Источником значения является значение полученное из АСУ
            /// </summary>
            Asy,

            /// <summary>
            /// Источник данных не определен
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
                                            s_type = (SourceType)Enum.Parse(typeof(SourceType), Child.InnerText);
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