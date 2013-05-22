using System;
using System.Xml;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Обороты ротора
    /// </summary>
    public class P0110 : TParameter
    {
        protected SourceRotor _source;              // источник, который выбирается для вычисления Обороты ротора 

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P0110(Guid p_identifier)
            : base(p_identifier, "P0110", "Обороты ротора")
        {
            simple = false;
            _source = SourceRotor.Analog;
        }

        /// <summary>
        /// Определяет источник, который выбирается для вычисления оборотов ротора
        /// </summary>
        public SourceRotor Source
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

                return SourceRotor.Default;
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
        /// Вычисляет текущее значение параметра Обороты ротора 
        /// </summary>
        /// <param name="v1">Обороты ротора с Аналогового сигнала</param>
        /// <param name="v2">Обороты ротора АСУ</param>
        /// <param name="v3">Обороты СВП</param>
        public void Calculate(P0015 v1, P0017 v2, P0018 v3)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    switch (Source)
                    {
                        case SourceRotor.Analog:

                            _value = v1.Value;
                            break;

                        case SourceRotor.Asy:

                            _value = v2.Value;
                            break;

                        case SourceRotor.Svp:

                            _value = v3.Value;
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
                                            _source = (SourceRotor)Enum.Parse(typeof(SourceRotor), Child.InnerText);
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
        ///  Определяет источник, который выбирается для вычисления Обороты ротора 
        /// </summary>
        public enum SourceRotor
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
            /// Обороты СВП
            /// </summary>
            Svp,

            /// <summary>
            /// Источник не определен
            /// </summary>
            Default
        }
    }
}