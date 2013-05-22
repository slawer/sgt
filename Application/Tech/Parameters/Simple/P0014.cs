using System;
using System.Xml;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Диаметр поршня 1
    /// </summary>
    public class P0014 : TParameter
    {
        protected float val;                    // значение для собственного источника
        protected P0014Source source;           // откуда брать значения для параметра

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P0014(Guid p_identifier)
            : base(p_identifier, "P0014", "Диаметр поршня 1")
        {
            val = float.NaN;
            source = P0014Source.Own;
        }

        /// <summary>
        /// Опреедляет источник данных для параметра
        /// </summary>
        public P0014Source Source
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return source;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return P0014Source.Default;
            }

            set
            {
                if (slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        source = value;
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
        /// <param name="slice">Срез данных</param>
        public override void Calculate(float[] slice)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    switch (source)
                    {
                        case P0014Source.Own:

                            //_value = val;
                            break;

                        case P0014Source.External:

                            if (p_number > -1 && p_number < slice.Length)
                            {
                                _value = slice[p_number];
                            }
                            break;

                        case P0014Source.Default:

                            //_value = val;
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

        // ------------------- сохраняемся/загружаемся ------------------------

        /// <summary>
        /// определяет узел в котором сохраняется значение для собственного источника
        /// </summary>
        protected const string valName = "val";

        /// <summary>
        /// определяет узел в котором сохраняется откуда брать значения для параметра
        /// </summary>
        protected const string sourceName = "source";

        /// <summary>
        /// Сохранить параметр в Xml узел
        /// </summary>
        /// <param name="document">XML документ, куда будет добавлен данный XmlNode</param>
        /// <returns>Узел в котором сохранен параметр</returns>
        public override XmlNode Save(System.Xml.XmlDocument document)
        {
            XmlNode root = base.Save(document);
            if (root != null)
            {
                try
                {
                    if (slim.TryEnterReadLock(100))
                    {
                        try
                        {
                            XmlNode valNode = document.CreateElement(valName);
                            XmlNode sourceNode = document.CreateElement(sourceName);

                            valNode.InnerText = val.ToString();
                            sourceNode.InnerText = source.ToString();

                            root.AppendChild(valNode);
                            root.AppendChild(sourceNode);

                            return root;
                        }
                        finally
                        {
                            slim.ExitReadLock();
                        }
                    }
                }
                catch { }                
            }
            return root;
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
                                    case valName:

                                        try
                                        {
                                            val = float.Parse(Child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case sourceName:

                                        try
                                        {
                                            source = (P0014Source)Enum.Parse(typeof(P0014Source), Child.InnerText);
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
    }

    /// <summary>
    /// Определяет источник данных для параметра Диаметр поршня
    /// </summary>
    public enum P0014Source
    {
        /// <summary>
        /// Источником является сама программа
        /// </summary>
        Own,

        /// <summary>
        /// Источником является внешняя программа
        /// </summary>
        External,

        /// <summary>
        /// По умолчанию. Own
        /// </summary>
        Default
    }
}