using System;
using System.Xml;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Расход на входе
    /// </summary>
    public class P0114 : TParameter
    {
        protected SourceFlow _source;               // источник для вычисления расхода

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P0114(Guid p_identifier)
            : base(p_identifier, "P0114", "Расход на входе")
        {
            simple = false;
            _source = SourceFlow.Moving;
        }

        /// <summary>
        /// Определяет источник, который выбирается для вычисления Расход на входе
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
        /// Вычисляет текущее значение параметра Расход на входе
        /// </summary>
        /// <param name="v1">Расход на входе Датчик</param>
        /// <param name="v2">Расход на входе по Ходам Насоса</param>
        public void Calculate(P0010 v1, P0113 v2)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    switch (_source)
                    {
                        case SourceFlow.Moving:

                            _value = v2.Value;
                            break;

                        case SourceFlow.Pump:

                            _value = v1.Value;
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
        /// Определяет источник, который выбирается для вычисления Расход на входе
        /// </summary>
        public enum SourceFlow
        {
            /// <summary>
            /// По Ходам Насоса (АСУ, Аналоговвый)
            /// </summary>
            Moving,

            /// <summary>
            /// Датчик расхода
            /// </summary>
            Pump,

            /// <summary>
            /// Источник не определен
            /// </summary>
            Default
        }
    }
}