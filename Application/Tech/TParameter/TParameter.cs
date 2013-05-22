using System;
using System.Xml;
using System.Threading;

using Buffering;

namespace SGT
{
    /// <summary>
    /// Реализует технологический параметр
    /// </summary>
    public partial class TParameter : ITechParameter
    {
        protected float _value;                 // текущее значение параметра
        protected Guid identifier;              // идентификатор технологического параметра

        protected int p_number;                 // определяет номер параметра , который явлется источником данных для параметра
        protected int s_number;                 // определяет номер параметра , который явлется местом сохранения данных для параметра

        protected bool simple = true;           // определяет является параметр простым или нет
        protected string _name = string.Empty;  // текстовое название параметра

        protected ReaderWriterLockSlim slim;    // синхронизатор

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public TParameter(Guid p_identifier)
        {
            _value = float.NaN;
            identifier = p_identifier;

            p_number = -1;
            s_number = -1;

            slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        /// <param name="unigueName">Уникальное имя класса</param>
        protected TParameter(Guid p_identifier, String unigueName)
        {
            _value = float.NaN;
            identifier = p_identifier;

            p_number = -1;
            s_number = -1;

            AttributeName = unigueName;
            slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        /// <param name="unigueName">Уникальное имя класса</param>
        /// <param name="name">Текстовое название параметра</param>
        protected TParameter(Guid p_identifier, String unigueName, String name)
        {
            _value = float.NaN;
            identifier = p_identifier;

            p_number = -1;
            s_number = -1;

            _name = name;
            AttributeName = unigueName;
            
            slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        }

        /// <summary>
        /// Возвращяет текущее значение параметра
        /// </summary>
        public float Value
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return _value;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return float.NaN;
            }

            protected set
            {
                if (slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        _value = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет параметр простой или нет
        /// </summary>
        public Boolean IsSimple
        {
            get
            {
                return simple;
            }
        }

        /// <summary>
        /// Определяет идентификатор технологического параметра
        /// </summary>
        public Guid Identifier
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return identifier;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return Guid.Empty;
            }

            set
            {
                if (slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        identifier = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет номер параметра который является источником данных для технологического параметра
        /// </summary>
        public int PNumber
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return p_number;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return -1;
            }

            set
            {
                if (slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        p_number = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет номер параметра , который явлется местом сохранения данных для параметра
        /// </summary>
        public int SNumber
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        if (simple)
                        {
                            return p_number;
                        }
                        else
                            return s_number;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return -1;
            }

            set
            {
                if (slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        s_number = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет текстовое название параметра
        /// </summary>
        public String Name
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return _name;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return string.Empty;
            }

            set
            {
                if (slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        _name = value;
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
        public virtual void Calculate(float[] slice)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    if (p_number > -1 && p_number < slice.Length)
                    {
                        _value = slice[p_number];
                    }
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Вычислить значение параметра
        /// </summary>
        /// <param name="n_value">Присваиваемое значение параметру</param>
        public virtual void Calculate(float n_value)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    _value = n_value;
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        // --------------------------- сохранение параметра ---------------------------

        /// <summary>
        /// Имя корневого узла
        /// </summary>
        public const string RootName = "TParameter";
        
        /// <summary>
        /// Имя узла в котором сохраняется текущее значение параметра
        /// </summary>
        protected const string ValueName = "Value";

        /// <summary>
        /// Имя узла, в котором сохраняется идентификатор параметра
        /// </summary>
        protected const string IdentifierName = "Identifier";

        /// <summary>
        /// Имя узла в котором сохраняется номер параметра, который является 
        /// источником данных для данного технологического параметра
        /// </summary>
        protected const string PNumberName = "PNumber";

        /// <summary>
        /// Имя узла в котором сохраняется номер параметра, который является 
        /// источником сохранения данных для данного технологического параметра
        /// </summary>
        protected const string SNumberName = "SNumber";

        /// <summary>
        /// Имя узла в котором хранится уникальное имя класса
        /// </summary>
        protected string AttributeName = "TParameter";

        /// <summary>
        /// Возвращяет уникальное имя класса.
        /// Исползуется при загрузке параметра.
        /// </summary>
        protected internal string UnigueClassName
        {
            get 
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return AttributeName;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return string.Empty;
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        AttributeName = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Сохранить параметр в Xml узел
        /// </summary>
        /// <param name="document">XML документ, куда будет добавлен данный XmlNode</param>
        /// <returns>Узел в котором сохранен параметр</returns>
        public virtual XmlNode Save(XmlDocument document)
        {
            if (slim.TryEnterReadLock(100))
            {
                try
                {
                    XmlNode RootNode = document.CreateElement(RootName);
                    XmlNode ValueNode = document.CreateElement(ValueName);

                    XmlNode IdentifierNode = document.CreateElement(IdentifierName);
                    XmlNode PNumberNode = document.CreateElement(PNumberName);

                    XmlNode SNumberNode = document.CreateElement(SNumberName);

                    ValueNode.InnerText = _value.ToString();
                    IdentifierNode.InnerText = identifier.ToString();

                    PNumberNode.InnerText = p_number.ToString();
                    SNumberNode.InnerText = s_number.ToString();
                                        
                    RootNode.AppendChild(IdentifierNode);
                    RootNode.AppendChild(PNumberNode);

                    RootNode.AppendChild(SNumberNode);
                    RootNode.AppendChild(ValueNode);

                    XmlAttribute attribute = document.CreateAttribute(AttributeName);
                    RootNode.Attributes.Append(attribute);

                    return RootNode;
                }
                finally
                {
                    slim.ExitReadLock();
                }
            }

            return null;
        }

        /// <summary>
        /// Загрузить параметр из Xml узла
        /// </summary>
        /// <param name="Node">Xml узел в котором сохранен параметр</param>
        public virtual void Load(XmlNode Node)
        {
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
                                    case ValueName:

                                        try
                                        {
                                            _value = float.Parse(Child.InnerText);
                                        }
                                        catch { }

                                        break;

                                    case IdentifierName:

                                        try
                                        {
                                            identifier = new Guid(Child.InnerText);
                                        }
                                        catch
                                        {
                                            identifier = Guid.Empty;
                                        }
                                        break;

                                    case PNumberName:

                                        try
                                        {
                                            p_number = int.Parse(Child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case SNumberName:

                                        try
                                        {
                                            s_number = int.Parse(Child.InnerText);
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