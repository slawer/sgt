using System;
using System.Xml;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Ходы Насоса 2
    /// </summary>
    public class P0117 : TParameter
    {
        protected SourceMoving _source;     // источник, который выбирается для вычисления Параметра Ходы Насоса 2

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P0117(Guid p_identifier)
            : base(p_identifier, "P0117", "Ходы Насоса 2")
        {
            simple = false;
            _source = SourceMoving.Analog;
        }

        /// <summary>
        /// Определяет источник, который выбирается для вычисления Параметра Ходы Насоса 2
        /// </summary>
        public SourceMoving Source
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return _source;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return SourceMoving.Default;
            }

            set
            {
                if (slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        _source = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Вычисляет текущее значение Параметр Ходы Насоса 2
        /// </summary>
        /// <param name="v1">Параметр Ходы Насоса 2 с Аналогового сигнала</param>
        /// <param name="v2">Параметр Ходы Насоса 2 АСУ</param>
        public void Calculate(P08_1 v1, P11_1 v2)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    switch (_source)
                    {
                        case SourceMoving.Analog:

                            _value = v1.Value;
                            break;

                        case SourceMoving.Asy:

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

                        SourceTypeNode.InnerText = _source.ToString();
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
                                            _source = (SourceMoving)Enum.Parse(typeof(SourceMoving), Child.InnerText);
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

        /// <summary>
        /// Определяет источник, который выбирается для вычисления Параметра Ходы Насоса 2
        /// </summary>
        public enum SourceMoving
        {
            /// <summary>
            /// Источником является аналоговый сигнал
            /// </summary>
            Analog,

            /// <summary>
            /// Источником является АСУ Буровой
            /// </summary>
            Asy,

            /// <summary>
            /// Источник не определен
            /// </summary>
            Default
        }
    }
}