using System;
using System.Xml;
using System.Text;
using System.Drawing;
using System.Threading;

namespace NumericTable
{
    /// <summary>
    /// Реализует элемент на числовой панели
    /// </summary>
    public class PanelItem
    {
        protected string item_value;            // значение элемента
        protected string description;           // описание элемента

        protected Font font;                    // шрифт которым осуществлять отрисовку элемента
        protected Color color;                  // цвет которым отрисосывать элемент

        protected Guid identifier;              // идентификатор параметра
        protected ReaderWriterLockSlim slim;    // синхронизатор

        protected Color col_alarm;              // цвет аварийного значения

        protected float alarm = float.NaN;      // аварийное значение
        protected bool is_block_alarm = false;  // контролировать аварийное значение или нет
        
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public PanelItem()
        {
            color = Color.Black;
            identifier = Guid.Empty;

            col_alarm = Color.Yellow;

            font = new Font(FontFamily.GenericSansSerif, 11.0f, FontStyle.Regular);
            slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        }

        /// <summary>
        /// Определяет описание числового параметра
        /// </summary>
        public string Description
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return description;
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
                        description = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Возвращяет отформатированое описание параметра
        /// </summary>
        public String FormattedDescription
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return description;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Определяет значение параметра
        /// </summary>
        public string Value
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {

                        if (IsNumeric(item_value))
                        {
                            return item_value;
                        }
                        else
                        {
                            return "ОТКЛ";
                        }
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return "------";
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        item_value = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет шрифт которым отрисовывать параметр
        /// </summary>
        public Font Font
        {
            get
            {
                if (slim.TryEnterReadLock(300))
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
                if (slim.TryEnterWriteLock(500))
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
        /// Определяет цвет которым отрисовывать параметр
        /// </summary>
        public Color Color
        {
            get
            {
                if (slim.TryEnterReadLock(300))
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

                return default(Color);
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
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
        /// Определяет идентификатор отображаемого параметра
        /// </summary>
        public Guid Identifier
        {
            get
            {
                if (slim.TryEnterReadLock(300))
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
                if (slim.TryEnterWriteLock(500))
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
        /// Определить объект является числом или нет
        /// </summary>
        /// <param name="Expression">Проверяемый объект</param>
        /// <returns>Если объект является числом то true, в противном случае false</returns>
        private Boolean IsNumeric(Object Expression)
        {
            if (Expression == null || Expression is DateTime)
                return false;

            if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal ||
                Expression is Single || Expression is Double || Expression is Boolean)
                return true;

            try
            {
                if (Expression is string)
                {
                    Double dbl = Double.Parse(Expression as string);
                    if (double.IsInfinity(dbl) || double.IsNaN(dbl) ||
                        double.IsNegativeInfinity(dbl) || double.IsPositiveInfinity(dbl))
                    {
                        return false;
                    }
                }
                else
                {
                    Double dbl = Double.Parse(Expression.ToString());
                    if (double.IsInfinity(dbl) || double.IsNaN(dbl) ||
                        double.IsNegativeInfinity(dbl) || double.IsPositiveInfinity(dbl))
                    {
                        return false;
                    }
                }

                return true;
            }
            catch { }
            return false;
        }

        /// <summary>
        /// сохранить настройки элемента
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public XmlNode Save(XmlDocument doc)
        {
            if (slim.TryEnterWriteLock(400))
            {
                try
                {
                    XmlNode root = doc.CreateElement("panelItem");

                    XmlNode descriptionNode = doc.CreateElement("description");

                    XmlNode fontNode = doc.CreateElement("font");
                    XmlNode colorNode = doc.CreateElement("color");

                    XmlNode identifierNode = doc.CreateElement("identifier");
                    XmlNode col_alarmNode = doc.CreateElement("col_alarm");

                    XmlNode alarmNode = doc.CreateElement("alarm");
                    XmlNode is_block_alarmNode = doc.CreateElement("is_block_alarm");

                    descriptionNode.InnerText = description;

                    FontConverter f_cnv = new FontConverter();
                    fontNode.InnerText = f_cnv.ConvertToString(font);

                    colorNode.InnerText = color.ToArgb().ToString();

                    identifierNode.InnerText = identifier.ToString();
                    col_alarmNode.InnerText = col_alarm.ToArgb().ToString();

                    alarmNode.InnerText = alarm.ToString();
                    is_block_alarmNode.InnerText = is_block_alarm.ToString();

                    root.AppendChild(descriptionNode);

                    root.AppendChild(fontNode);
                    root.AppendChild(colorNode);

                    root.AppendChild(identifierNode);
                    root.AppendChild(col_alarmNode);

                    root.AppendChild(alarmNode);
                    root.AppendChild(is_block_alarmNode);

                    return root;
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }

            return null;
        }

        /// <summary>
        /// Загрузить настройки 
        /// </summary>
        /// <param name="root"></param>
        public void Load(XmlNode root)
        {
            if (slim.TryEnterWriteLock(500))
            {
                try
                {
                    if (root != null && root.Name == "panelItem")
                    {
                        if (root.HasChildNodes)
                        {
                            foreach (XmlNode child in root.ChildNodes)
                            {
                                switch (child.Name)
                                {
                                    case "description":

                                        try
                                        {
                                            description = child.InnerText;
                                        }
                                        catch { }
                                        break;

                                    case "font":

                                        try
                                        {
                                            FontConverter cnv = new FontConverter();
                                            font = (Font)cnv.ConvertFromString(child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case "color":

                                        try
                                        {
                                            color = Color.FromArgb(int.Parse(child.InnerText));
                                        }
                                        catch { }
                                        break;

                                    case "identifier":

                                        try
                                        {
                                            identifier = new Guid(child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case "col_alarm":

                                        try
                                        {
                                            col_alarm = Color.FromArgb(int.Parse(child.InnerText));
                                        }
                                        catch { }
                                        break;

                                    case "alarm":

                                        try
                                        {
                                            alarm = float.Parse(child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case "is_block_alarm":

                                        try
                                        {
                                            is_block_alarm = bool.Parse(child.InnerText);
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
}