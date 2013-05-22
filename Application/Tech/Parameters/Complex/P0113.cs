using System;
using System.Xml;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Расход на входе по Ходам Насоса
    /// </summary>
    public class P0113 : TParameter
    {
        protected SourceFlow _source;               // источник данных для вычисления параметра

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P0113(Guid p_identifier)
            : base(p_identifier, "P0113", "Расход на входе по Ходам Насоса")
        {
            simple = false;
            _source = SourceFlow.Analog;
        }

        /// <summary>
        /// Определяет источник данных для вычисления параметра
        /// </summary>
        public SourceFlow Source
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

                return SourceFlow.Default;
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
        /// Вычисляет текущее значение параметра Расход на входе по Ходам Насоса
        /// </summary>
        /// <param name="v1">Расход на входе по Ходам Насоса с Аналогового сигнала</param>
        /// <param name="v2">Расход на входе по Ходам Насоса АСУ</param>
        public void Calculate(P0109 v1, P0112 v2)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    switch (_source)
                    {
                        case SourceFlow.Analog:

                            _value = v1.Value;
                            break;

                        case SourceFlow.Asy:

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
                                            _source = (SourceFlow)Enum.Parse(typeof(SourceFlow), Child.InnerText);
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
        /// Определяет источник, который выбирается для вычисления Расход на входе по Ходам Насоса
        /// </summary>
        public enum SourceFlow
        {
            /// <summary>
            /// Аналоговый датчик
            /// </summary>
            Analog,

            /// <summary>
            /// АСУ Буровой
            /// </summary>
            Asy,

            /// <summary>
            /// Источник не определен.
            /// </summary>
            Default
        }
    }
}