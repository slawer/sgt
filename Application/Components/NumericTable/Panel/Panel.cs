using System;
using System.Xml;
using System.Drawing;
using System.Threading;
using System.Collections.Generic;

namespace NumericTable
{
    /// <summary>
    /// Реализует панель цифрового табло
    /// </summary>
    public partial class Panel
    {
        /// <summary>
        /// Коэффициент для верхней границы рамки
        /// </summary>
        protected const float k_top = 0.05f;

        /// <summary>
        /// Коэффициент для правой границы рамки
        /// </summary>
        protected const float k_right = 0.02f;

        /// <summary>
        /// Коэфициен для нижней границы рамки
        /// </summary>
        protected const float k_bottom = 0.02f;

        /// <summary>
        /// Коэффициент для левой границы рамки
        /// </summary>
        protected const float k_left = 0.02f;

        // ---------------------- данные класса ---------------------------

        protected NumericTable _table = null;           // графический элемент на котором осуществляется 
                                                        // отрисовка панели

        protected Mutex d_mutex;                        // синхронизирует отрисовку
        protected GraphicDrawter drawter = null;        // осуществлят буферизацию области отрисовки

        protected List<PanelItem> items;                // параметры для отрисовки
        protected ReaderWriterLockSlim i_slim;          // синхронизирует доступ к списку параметров
        
        protected BoundingRectangle rect;               // область в которую отрисовывать параметры

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="table"></param>
        public Panel(NumericTable table)
        {
            if (table != null)
            {
                _table = table;

                _table.Resize += new EventHandler(_table_Resize);
                _table.Paint += new System.Windows.Forms.PaintEventHandler(_table_Paint);

                items = new List<PanelItem>();
                i_slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

                rect = new BoundingRectangle();

                d_mutex = new Mutex();
                drawter = new GraphicDrawter(_table.CreateGraphics(), _table.ClientRectangle);
            }
        }

        /// <summary>
        /// Отрисовать панель
        /// </summary>
        public void Redraw()
        {
            bool blocked = false;
            try
            {
                if (d_mutex.WaitOne(65))
                {
                    blocked = true;

                    drawter.Clear(_table.BackColor);

                    DrawItems();

                    drawter.Present();
                }
            }
            catch { }
            finally
            {
                if (blocked) d_mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Изменился размер панели
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _table_Resize(object sender, EventArgs e)
        {
            bool blocked = false;
            try
            {
                if (d_mutex.WaitOne(50))
                {
                    blocked = true;
                    if (!_table.Size.IsEmpty)
                    {
                        Point n_location = new Point();

                        n_location.X = Math.Abs((int)(_table.Width * k_left));
                        n_location.Y = Math.Abs((int)(_table.Height * k_top));

                        Size n_size = new Size();

                        n_size.Width = Math.Abs((int)(_table.Width - (_table.Width * k_left) - (_table.Width * k_right)));
                        n_size.Height = Math.Abs((int)(_table.Height - (_table.Height * k_top) - (_table.Height * k_bottom)));

                        rect = new BoundingRectangle(n_location, n_size);
                        drawter = new GraphicDrawter(_table.CreateGraphics(), _table.ClientRectangle);
                    }
                }
            }
            catch { }
            finally
            {
                if (blocked) d_mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Необходимо перерисовать панель
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _table_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            bool blocked = false;
            try
            {
                if (d_mutex.WaitOne(75))
                {
                    blocked = true;

                    drawter.Clear(_table.BackColor);

                    DrawItems();                    
                    
                    drawter.Present();
                }
            }
            catch { }
            finally
            {
                if (blocked) d_mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Отрисовать параметры
        /// </summary>
        private void DrawItems()
        {
            try
            {
                int remaining = rect.Height;
                foreach (PanelItem item in items)
                {
                    SizeF size_val = drawter.Graphics.MeasureString(item.Value, item.Font);
                    item.Description = item.Description.Replace("(Единицы измерения не определены)", string.Empty);
                    
                    SizeF size_desc = drawter.Graphics.MeasureString(item.Description, item.Font);

                    float t_height = (size_val.Height > size_desc.Height) ? size_val.Height : size_desc.Height;
                    RectangleF t_rect = new RectangleF(rect.X, (rect.Height - remaining) + rect.Y, rect.Width, t_height);

                    if (remaining > t_rect.Height + 5)
                    {
                        using (SolidBrush brush = new SolidBrush(item.Color))
                        {
                            StringFormat str_format = new StringFormat();
                            str_format.Alignment = StringAlignment.Far;

                            PointF pt = new PointF();

                            pt.X = rect.X;
                            pt.Y = rect.Y;

                            string i_value = item.Value;
                            if (i_value == "ОТКЛ")
                            {
                                using (SolidBrush rbrush = new SolidBrush(Color.Salmon))
                                {
                                    drawter.Graphics.DrawString(item.Description, item.Font, rbrush, t_rect);

                                    float ost_size = size_desc.Width - t_rect.Width;
                                    if (ost_size >= size_val.Width)
                                    {
                                        drawter.Graphics.DrawString(i_value, item.Font, rbrush, t_rect, str_format);
                                    }
                                }
                            }
                            else
                            {
                                drawter.Graphics.DrawString(item.Description, item.Font, brush, t_rect);

                                float ost_size = t_rect.Width - size_desc.Width;
                                if (ost_size >= size_val.Width)
                                {
                                    drawter.Graphics.DrawString(i_value, item.Font, brush, t_rect, str_format);
                                }
                            }

                            using (Pen pen = new Pen(Color.DarkGray))
                            {
                                pen.DashPattern = new float[] { 5.0F, 1.0F, 3.0F, 2.0F };
                                drawter.Graphics.DrawLine(pen, (int)rect.X, (int)(t_rect.Y + t_rect.Height + 3),
                                    (int)(rect.X + rect.Width), (int)(t_rect.Y + t_rect.Height + 3));
                            }
                        }
                    }
                    else
                        break;

                    remaining = remaining - (int)t_height - 5;
                }
            }
            catch { }
        }

        /// <summary>
        /// Сохранить настройки элементов панели
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public XmlNode Save(XmlDocument doc)
        {
            if (i_slim.TryEnterWriteLock(500))
            {
                try
                {
                    if (doc != null)
                    {
                        XmlNode root = doc.CreateElement("numericPanel");
                        foreach (PanelItem item in items)
                        {
                            XmlNode itemNode = item.Save(doc);
                            if (itemNode != null)
                            {
                                root.AppendChild(itemNode);
                            }
                        }

                        return root;
                    }
                }
                finally
                {
                    i_slim.ExitWriteLock();
                }
            }
            return null;
        }

        /// <summary>
        /// Загрузить настройки панели
        /// </summary>
        /// <param name="root"></param>
        public void Load(XmlNode root)
        {
            if (i_slim.TryEnterWriteLock(500))
            {
                try
                {
                    int index = 0;
                    if (root != null && root.Name == "numericPanel")
                    {
                        if (root.HasChildNodes)
                        {
                            foreach (XmlNode child in root.ChildNodes)
                            {
                                if (child != null)
                                {
                                    switch (child.Name)
                                    {
                                        case "panelItem":

                                            try
                                            {
                                                if (index > -1 && index < items.Count)
                                                {
                                                    items[index].Load(child);
                                                    index = index + 1;
                                                }
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
                }
                finally
                {
                    i_slim.ExitWriteLock();
                }
            }
        }
    }
}