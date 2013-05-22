using System;
using System.Xml;
using System.Drawing;
using System.Threading;

namespace SGT
{
    /// <summary>
    /// Базовый класс для всех панелей, отображающих данные
    /// </summary>
    public class VPanel
    {        
        protected string PanelName;                 // название панели
        protected readonly VPanelType PanelType;    // тип панели

        protected Boolean n_show;                   // отображать данные на панели или нет
        protected ReaderWriterLockSlim slim;        // синхронизатор

        protected SgtApplication _app = null;       // контекс работы

        /// <summary>
        /// инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="panelName">Название панели</param>
        public VPanel(string panelName, VPanelType panelType)
        {
            PanelName = panelName;
            PanelType = panelType;

            n_show = false;
            slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);            
        }

        /// <summary>
        /// Возвращяет название панели
        /// </summary>
        public string VPanelName
        {
            get
            {
                return PanelName;
            }

            set
            {
                PanelName = value;
            }
        }

        /// <summary>
        /// Возвращяет тип панели
        /// </summary>
        public VPanelType VPanelType
        {
            get
            {
                return PanelType;
            }
        }

        /// <summary>
        /// Определяет отображать данные на панели или нет
        /// </summary>
        public Boolean NShow
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return n_show;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return true;
            }

            set
            {
                if (slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        n_show = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Обновить панель
        /// </summary>
        public virtual void Update()
        {
        }

        /// <summary>
        /// Обновить панель
        /// </summary>
        public virtual void UpdateWithRedraw()
        {
        }

        /// <summary>
        /// Актуализировать панель
        /// </summary>
        public virtual void Actualize()
        {
        }

        /// <summary>
        /// Получить копию объекта
        /// </summary>
        /// <returns></returns>
        public virtual VPanel Clone()
        {
            return null;
        }

        /// <summary>
        /// Загрузить данные панели из клона
        /// </summary>
        /// <param name="panel">Панель клон</param>
        public virtual void LoadFromClone(VPanel panel)
        {
        }

        // ------------------ сохранение , загрузка ------------------

        /// <summary>
        /// Сохранить настройки панели
        /// </summary>
        /// <param name="doc">Xml документ в который осуществляется сохранение настроек панели</param>
        /// <returns>Сохраненые настройки панели</returns>
        public virtual XmlNode Save(XmlDocument doc)
        {
            return null;
        }

        /// <summary>
        /// Загрузить настройки панели
        /// </summary>
        /// <param name="Root">Корневой узел в котром находятся настройки панели</param>
        public virtual void Load(XmlNode Root)
        {
        }
    }

    /// <summary>
    /// Реализует отображаемый параметр на панели
    /// </summary>
    public class VPanelParameter
    {
        protected int p_number = -1;                    // номер отображаемого параметра в срезе данных
        protected Guid identifier;                      // идентификатор параметра

        protected Font font;                            // шрифт которым отрисовывать елемент
        protected Color color;                          // цвет которым отрисовывать шрифт

        protected ReaderWriterLockSlim slim = null;     // синхронизатор

        protected object tag;                           // пользовательский объек ассоциированный с данным экземпляром класса

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public VPanelParameter()
        {
            color = Color.Black;
            font = new System.Drawing.Font(FontFamily.GenericSansSerif, 12.0f, FontStyle.Regular);

            identifier = Guid.Empty;
            slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        }

        /// <summary>
        /// Номер параметра в срезе данных
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
        /// Идентификатор параметра в срезе данных
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
        /// Определяет шрифт, которым отрисовывать елемент
        /// </summary>
        public Font Font
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return font;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return null;
            }

            set
            {
                if (slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        font = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет цвет, которым отрисовывать елемент
        /// </summary>
        public Color Color
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return color;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return SystemColors.Desktop;
            }

            set
            {
                if (slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        color = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Пользовательский объек ассоциированный с данным экземпляром класса
        /// </summary>
        public Object Tag
        {
            get
            {
                return tag;
            }

            set
            {
                tag = value;
            }
        }

        // ---------------------- сохранить/загрузить ----------------------

        internal const string rootName = "VPanelParameter";

        /// <summary>
        /// Имя узла в котором сохраняется номер отображаемого параметра в срезе данных
        /// </summary>
        protected const string p_numberName = "p_number";

        /// <summary>
        /// Имя узла в котором сохраняется идентификатор параметра
        /// </summary>
        protected const string identifierName = "identifier";

        /// <summary>
        /// Имя узла в котором сохраняется шрифт которым отрисовывать елемент
        /// </summary>
        protected const string fontName = "font";

        /// <summary>
        /// Имя узла в котором сохраняется цвет которым отрисовывать шрифт
        /// </summary>
        protected const string colorName = "color";

        /// <summary>
        /// Сохранить параметр панели
        /// </summary>
        /// <param name="doc">Документ в который осуществляется сохранение параметра</param>
        /// <returns>Сохраненый параметр</returns>
        public XmlNode Save(XmlDocument doc)
        {
            if (slim.TryEnterReadLock(100))
            {
                try
                {
                    XmlNode root = doc.CreateElement(rootName);
                    XmlNode p_numberNode = doc.CreateElement(rootName);

                    XmlNode identifierNode = doc.CreateElement(identifierName);
                    
                    XmlNode fontNode = doc.CreateElement(fontName);
                    XmlNode colorNode = doc.CreateElement(colorName);

                    p_numberNode.InnerText = p_number.ToString();
                    identifierNode.InnerText = identifier.ToString();

                    FontConverter converter = new FontConverter();
                    fontNode.InnerText = converter.ConvertToString(font);

                    colorNode.InnerText = color.ToArgb().ToString();

                    root.AppendChild(p_numberNode);
                    root.AppendChild(identifierNode);

                    root.AppendChild(fontNode);
                    root.AppendChild(colorNode);

                    return root;
                }
                finally
                {
                    slim.ExitReadLock();
                }
            }
            return null;
        }

        /// <summary>
        /// Сохранить параметр панели
        /// </summary>
        /// <param name="doc">Документ в который осуществляется сохранение параметра</param>
        /// <param name="nameRoot">Имя узла</param>
        /// <returns>Сохраненый параметр</returns>
        public XmlNode Save(XmlDocument doc, String nameRoot)
        {
            if (slim.TryEnterReadLock(100))
            {
                try
                {
                    XmlNode root = doc.CreateElement(nameRoot);
                    XmlNode p_numberNode = doc.CreateElement(rootName);

                    XmlNode identifierNode = doc.CreateElement(identifierName);

                    XmlNode fontNode = doc.CreateElement(fontName);
                    XmlNode colorNode = doc.CreateElement(colorName);

                    p_numberNode.InnerText = p_number.ToString();
                    identifierNode.InnerText = identifier.ToString();

                    FontConverter converter = new FontConverter();
                    fontNode.InnerText = converter.ConvertToString(font);

                    colorNode.InnerText = color.ToArgb().ToString();

                    root.AppendChild(p_numberNode);
                    root.AppendChild(identifierNode);

                    root.AppendChild(fontNode);
                    root.AppendChild(colorNode);

                    return root;
                }
                finally
                {
                    slim.ExitReadLock();
                }
            }
            return null;
        }

        /// <summary>
        /// Загрузить 
        /// </summary>
        /// <param name="root">Корневой узел в котором содержится параметр</param>
        public void Load(XmlNode root)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    if (root != null)
                    {
                        if (root.HasChildNodes && root.Name == rootName)
                        {
                            foreach (XmlNode child in root.ChildNodes)
                            {
                                switch (child.Name)
                                {
                                    case p_numberName:

                                        try
                                        {
                                            p_number = int.Parse(child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case identifierName:

                                        try
                                        {
                                            identifier = new Guid(child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case fontName:

                                        try
                                        {
                                            FontConverter converter = new FontConverter();
                                            font = (Font)converter.ConvertFromString(child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case colorName:

                                        try
                                        {
                                            color = Color.FromArgb(int.Parse(child.InnerText));
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

        /// <summary>
        /// Загрузить 
        /// </summary>
        /// <param name="root">Корневой узел в котором содержится параметр</param>
        /// <param name="nameRoot">Имя узла</param>
        public void Load(XmlNode root, String nameRoot)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    if (root != null)
                    {
                        if (root.HasChildNodes && root.Name == nameRoot)
                        {
                            foreach (XmlNode child in root.ChildNodes)
                            {
                                switch (child.Name)
                                {
                                    case p_numberName:

                                        try
                                        {
                                            p_number = int.Parse(child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case identifierName:

                                        try
                                        {
                                            identifier = new Guid(child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case fontName:

                                        try
                                        {
                                            FontConverter converter = new FontConverter();
                                            font = (Font)converter.ConvertFromString(child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case colorName:

                                        try
                                        {
                                            color = Color.FromArgb(int.Parse(child.InnerText));
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
    /// Реализует график панели
    /// </summary>
    public class VPanelGraphic
    {
        protected Font font;                    // шрифт которым отрисовывать описание графика
        protected Color color;                  // цвет которым отрисовывать график

        protected float _min;                   // минимальное значение диапазона
        protected float _max;                   // максимальное значение диапазона

        protected string desc;                  // текстовое описание графика
        protected string units;                 // текстовое описание единиц измерения отображаемого параметра        

        protected int gra_width;                // ширина линии графика
        protected Parameter parameter;          // параметр ассоциированный с графиком

        protected ReaderWriterLockSlim slim;    // синхронизатор
        protected VPanelParameter v_parameter;  // базовая информация о параметре

        /// <summary>
        /// инициализирует новый экземпляр класса
        /// </summary>
        public VPanelGraphic()
        {
            v_parameter = new VPanelParameter();
            slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

            units = "[]";
            desc = "параметр";

            color = SystemColors.Control;//Color.Black;

            _min = 0;
            _max = 65535;
        }

        /// <summary>
        /// Номер параметра в срезе данных
        /// </summary>
        public int PNumber
        {
            get
            {
                return v_parameter.PNumber;
            }
        }

        /// <summary>
        /// Идентификатор параметра в срезе данных
        /// </summary>
        public Guid Identifier
        {
            get
            {
                return v_parameter.Identifier;
            }

            set
            {
                v_parameter.Identifier = value;
            }
        }

        /// <summary>
        /// Пользовательский объек ассоциированный с данным экземпляром класса
        /// </summary>
        public Object Tag
        {
            get
            {
                return v_parameter.Tag;
            }

            set
            {
                v_parameter.Tag = value;
            }
        }

        /// <summary>
        /// Определяет параметр ассоциированный с графиком
        /// </summary>
        public Parameter Parameter
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return parameter;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return null;
            }

            set
            {
                if (slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        parameter = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// шрифт которым отрисовывать описание графика
        /// </summary>
        public Font Font
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return font;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return null;
            }

            set
            {
                if (slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        font = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// цвет которым отрисовывать график
        /// </summary>
        public Color Color
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return color;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return Color.Beige;
            }

            set
            {
                if (slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        color = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }

        }

        /// <summary>
        /// минимальное значение диапазона
        /// </summary>
        public float Min
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return _min;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return float.MinValue;
            }

            set
            {
                if (slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        _min = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }
        
        /// <summary>
        /// максимальное значение диапазона
        /// </summary>
        public float Max
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return _max;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return float.MaxValue;
            }

            set
            {
                if (slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        _max = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// текстовое описание графика
        /// </summary>
        public string Description
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return desc;
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
                        desc = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// текстовое описание единиц измерения отображаемого параметра
        /// </summary>
        public string Units
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return units;
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
                        units = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Ширина линии графика
        /// </summary>
        public int Width
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        if (gra_width < 1)
                        {
                            return 1;
                        }
                        else
                            return gra_width;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return 1;
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        gra_width = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        // --------------- сохранение/загрузка ---------------

        public const string GraphicName = "graphic";

        /// <summary>
        /// Сохранить график
        /// </summary>
        /// <param name="doc">Документ в который осуществляется сохранение</param>
        /// <returns>Сохраненый график</returns>
        public XmlNode Save(XmlDocument doc)
        {
            if (slim.TryEnterReadLock(100))
            {
                try
                {
                    XmlNode root = doc.CreateElement(GraphicName);

                    XmlNode colorNode = doc.CreateElement("color");

                    XmlNode rangeNodeMin = doc.CreateElement("range_min");
                    XmlNode rangeNodeMax = doc.CreateElement("range_max");

                    XmlNode descNode = doc.CreateElement("desc");
                    XmlNode unitsNode = doc.CreateElement("units");
                    XmlNode widthNode = doc.CreateElement("width");

                    colorNode.InnerText = color.ToArgb().ToString();

                    rangeNodeMin.InnerText = _min.ToString();
                    rangeNodeMax.InnerText = _max.ToString();

                    descNode.InnerText = desc;
                    unitsNode.InnerText = units;
                    widthNode.InnerText = gra_width.ToString();

                    root.AppendChild(colorNode);

                    root.AppendChild(rangeNodeMin);
                    root.AppendChild(rangeNodeMax);

                    root.AppendChild(descNode);
                    root.AppendChild(unitsNode);
                    root.AppendChild(widthNode);

                    XmlNode p_Node = v_parameter.Save(doc);
                    if (p_Node != null)
                    {
                        root.AppendChild(p_Node);
                    }

                    return root;
                }
                finally
                {
                    slim.ExitReadLock();
                }
            }
            return null;
        }

        /// <summary>
        /// Загрузить графики
        /// </summary>
        /// <param name="root">Узел в котором сохранены графики</param>
        public void Load(XmlNode root)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    if (root != null && root.HasChildNodes)
                    {
                        foreach (XmlNode child in root.ChildNodes)
                        {
                            switch (child.Name)
                            {
                                case "color":

                                    try
                                    {
                                        color = Color.FromArgb(int.Parse(child.InnerText));
                                    }
                                    catch { }
                                    break;

                                case "range_min":

                                    try
                                    {
                                        _min = float.Parse(child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case "range_max":

                                    try
                                    {
                                        _max = float.Parse(child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case "desc":

                                    try
                                    {
                                        desc = child.InnerText;
                                    }
                                    catch { }
                                    break;

                                case "units":

                                    try
                                    {
                                        units = child.InnerText;
                                    }
                                    catch { }
                                    break;

                                case "width":

                                    try
                                    {
                                        gra_width = int.Parse(child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case VPanelParameter.rootName:

                                    try
                                    {
                                        v_parameter.Load(child);
                                    }
                                    catch { }
                                    break;

                                default:
                                    break;
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
    /// Перечисляет типы панелей
    /// </summary>
    public enum VPanelType
    {
        /// <summary>
        /// Буровая площадка
        /// </summary>
        DrillingFloor,

        /// <summary>
        /// АСУ Буровой
        /// </summary>
        AutomatedDrilling,

        /// <summary>
        /// Параметры бурового раствора
        /// </summary>
        SolutionPanel,

        /// <summary>
        /// Панель СПО
        /// </summary>
        PanelSpo,

        /// <summary>
        /// Цифровая панель
        /// </summary>
        NumericPanel,

        /// <summary>
        /// Общая панель
        /// </summary>
        FullPanel,

        /// <summary>
        /// Панель не определена
        /// </summary>
        Default
    }
}