using System;
using System.Xml;
using System.Drawing;
using System.Threading;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Buffering;
using NumericTable;
using GraphicComponent;

namespace SGT
{
    /// <summary>
    /// реализует хранение данных графической панели в общей панели отображения данных
    /// </summary>
    public class GraphicPanel
    {
        protected VPanelGraphic graphic_1;          // первый график
        protected VPanelGraphic graphic_2;          // второй график
        protected VPanelGraphic graphic_3;          // третий график
        protected VPanelGraphic graphic_4;          // четвертый график
        protected VPanelGraphic graphic_5;          // пятый график

        protected GraphicManager manager;           // управляющий отрисовкой графиков

        TimeSpan span = new TimeSpan(0, 3, 0);
        int count = 5;
        int orientation = 0;


        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public GraphicPanel()
        {
            graphic_1 = new VPanelGraphic();
            graphic_2 = new VPanelGraphic();
            graphic_3 = new VPanelGraphic();
            graphic_4 = new VPanelGraphic();
            graphic_5 = new VPanelGraphic();
        }

        /// <summary>
        /// Управляющий отрисовкой графиков
        /// </summary>
        public GraphicManager GManager
        {
            get { return manager; }
            set { manager = value; }
        }

        /// <summary>
        /// Отображаемый график 1
        /// </summary>
        public VPanelGraphic Graphic_1
        {
            get
            {
                return graphic_1;
            }
        }

        /// <summary>
        /// Отображаемый график 2
        /// </summary>
        public VPanelGraphic Graphic_2
        {
            get
            {
                return graphic_2;
            }
        }

        /// <summary>
        /// Отображаемый график 3
        /// </summary>
        public VPanelGraphic Graphic_3
        {
            get
            {
                return graphic_3;
            }
        }

        /// <summary>
        /// Отображаемый график 4
        /// </summary>
        public VPanelGraphic Graphic_4
        {
            get
            {
                return graphic_4;
            }
        }

        /// <summary>
        /// Отображаемый график 5
        /// </summary>
        public VPanelGraphic Graphic_5
        {
            get
            {
                return graphic_5;
            }
        }

        /// <summary>
        /// Выполнить инициализацию панели
        /// </summary>
        public void InitializePanel()
        {
            Graphic gr_1 = manager.InstanceGraphic();
            Graphic gr_2 = manager.InstanceGraphic();
            Graphic gr_3 = manager.InstanceGraphic();
            Graphic gr_4 = manager.InstanceGraphic();
            Graphic gr_5 = manager.InstanceGraphic();

            gr_1.Color = graphic_1.Color;
            gr_2.Color = graphic_2.Color;
            gr_3.Color = graphic_3.Color;
            gr_4.Color = graphic_4.Color;
            gr_5.Color = graphic_5.Color;

            gr_1.Description = graphic_1.Description;
            gr_2.Description = graphic_2.Description;
            gr_3.Description = graphic_3.Description;
            gr_4.Description = graphic_4.Description;
            gr_5.Description = graphic_5.Description;

            gr_1.Units = graphic_1.Units;
            gr_2.Units = graphic_2.Units;
            gr_3.Units = graphic_3.Units;
            gr_4.Units = graphic_4.Units;
            gr_5.Units = graphic_5.Units;

            gr_1.Width = graphic_1.Width;
            gr_2.Width = graphic_2.Width;
            gr_3.Width = graphic_3.Width;
            gr_4.Width = graphic_4.Width;
            gr_5.Width = graphic_5.Width;

            gr_1.Range.Min = graphic_1.Min;
            gr_1.Range.Max = graphic_1.Max;

            gr_2.Range.Min = graphic_2.Min;
            gr_2.Range.Max = graphic_2.Max;

            gr_3.Range.Min = graphic_3.Min;
            gr_3.Range.Max = graphic_3.Max;

            gr_4.Range.Min = graphic_4.Min;
            gr_4.Range.Max = graphic_4.Max;

            gr_5.Range.Min = graphic_5.Min;
            gr_5.Range.Max = graphic_5.Max;

            graphic_1.Tag = gr_1;
            graphic_2.Tag = gr_2;
            graphic_3.Tag = gr_3;
            graphic_4.Tag = gr_4;
            graphic_5.Tag = gr_5;

            if (manager != null)
            {
                manager.IntervalInCell = span;
                manager.GrinCount = count;
                manager.Orientation = (Orientation)orientation;

                manager.StartTime = DateTime.Now;
            }
        }

        /// <summary>
        /// Инициализировать графическую панель
        /// </summary>
        public void UpdatePanel()
        {
            Graphic gr_1 = graphic_1.Tag as Graphic;
            Graphic gr_2 = graphic_2.Tag as Graphic;
            Graphic gr_3 = graphic_3.Tag as Graphic;
            Graphic gr_4 = graphic_4.Tag as Graphic;
            Graphic gr_5 = graphic_5.Tag as Graphic;

            gr_1.Color = graphic_1.Color;
            gr_2.Color = graphic_2.Color;
            gr_3.Color = graphic_3.Color;
            gr_4.Color = graphic_4.Color;
            gr_5.Color = graphic_5.Color;

            gr_1.Description = graphic_1.Description;
            gr_2.Description = graphic_2.Description;
            gr_3.Description = graphic_3.Description;
            gr_4.Description = graphic_4.Description;
            gr_5.Description = graphic_5.Description;

            gr_1.Units = graphic_1.Units;
            gr_2.Units = graphic_2.Units;
            gr_3.Units = graphic_3.Units;
            gr_4.Units = graphic_4.Units;
            gr_5.Units = graphic_5.Units;

            gr_1.Width = graphic_1.Width;
            gr_2.Width = graphic_2.Width;
            gr_3.Width = graphic_3.Width;
            gr_4.Width = graphic_4.Width;
            gr_5.Width = graphic_5.Width;

            gr_1.Range.Min = graphic_1.Min;
            gr_1.Range.Max = graphic_1.Max;

            gr_2.Range.Min = graphic_2.Min;
            gr_2.Range.Max = graphic_2.Max;

            gr_3.Range.Min = graphic_3.Min;
            gr_3.Range.Max = graphic_3.Max;

            gr_4.Range.Min = graphic_4.Min;
            gr_4.Range.Max = graphic_4.Max;

            gr_5.Range.Min = graphic_5.Min;
            gr_5.Range.Max = graphic_5.Max;

            graphic_1.Tag = gr_1;
            graphic_2.Tag = gr_2;
            graphic_3.Tag = gr_3;
            graphic_4.Tag = gr_4;
            graphic_5.Tag = gr_5;
        }

        /// <summary>
        /// Инициализировать графическую панель
        /// </summary>
        public void UpdatePanelWithRedraw()
        {
            Parameter pr_1 = graphic_1.Parameter;
            Parameter pr_2 = graphic_2.Parameter;
            Parameter pr_3 = graphic_3.Parameter;
            Parameter pr_4 = graphic_4.Parameter;
            Parameter pr_5 = graphic_5.Parameter;

            if (pr_1 != null) graphic_1.Units = pr_1.Units;
            if (pr_2 != null) graphic_2.Units = pr_2.Units;
            if (pr_3 != null) graphic_3.Units = pr_3.Units;
            if (pr_4 != null) graphic_4.Units = pr_4.Units;
            if (pr_5 != null) graphic_5.Units = pr_5.Units;

            Graphic gr_1 = graphic_1.Tag as Graphic;
            Graphic gr_2 = graphic_2.Tag as Graphic;
            Graphic gr_3 = graphic_3.Tag as Graphic;
            Graphic gr_4 = graphic_4.Tag as Graphic;
            Graphic gr_5 = graphic_5.Tag as Graphic;

            gr_1.Color = graphic_1.Color;
            gr_2.Color = graphic_2.Color;
            gr_3.Color = graphic_3.Color;
            gr_4.Color = graphic_4.Color;
            gr_5.Color = graphic_5.Color;

            gr_1.Description = graphic_1.Description;
            gr_2.Description = graphic_2.Description;
            gr_3.Description = graphic_3.Description;
            gr_4.Description = graphic_4.Description;
            gr_5.Description = graphic_5.Description;

            gr_1.Units = graphic_1.Units;
            gr_2.Units = graphic_2.Units;
            gr_3.Units = graphic_3.Units;
            gr_4.Units = graphic_4.Units;
            gr_5.Units = graphic_5.Units;

            gr_1.Width = graphic_1.Width;
            gr_2.Width = graphic_2.Width;
            gr_3.Width = graphic_3.Width;
            gr_4.Width = graphic_4.Width;
            gr_5.Width = graphic_5.Width;

            gr_1.Range.Min = graphic_1.Min;
            gr_1.Range.Max = graphic_1.Max;

            gr_2.Range.Min = graphic_2.Min;
            gr_2.Range.Max = graphic_2.Max;

            gr_3.Range.Min = graphic_3.Min;
            gr_3.Range.Max = graphic_3.Max;

            gr_4.Range.Min = graphic_4.Min;
            gr_4.Range.Max = graphic_4.Max;

            gr_5.Range.Min = graphic_5.Min;
            gr_5.Range.Max = graphic_5.Max;

            graphic_1.Tag = gr_1;
            graphic_2.Tag = gr_2;
            graphic_3.Tag = gr_3;
            graphic_4.Tag = gr_4;
            graphic_5.Tag = gr_5;
        }

        /// <summary>
        /// Актуализировать параметры цифровой панели
        /// </summary>
        public void Actualize(SgtApplication _app)
        {
            try
            {
                graphic_1.Parameter = _app.GetParameter(graphic_1.Identifier);
                graphic_2.Parameter = _app.GetParameter(graphic_2.Identifier);
                graphic_3.Parameter = _app.GetParameter(graphic_3.Identifier);
                graphic_4.Parameter = _app.GetParameter(graphic_4.Identifier);
                graphic_5.Parameter = _app.GetParameter(graphic_5.Identifier);
            }
            catch { }
        }

        /// <summary>
        /// Сохранить графическую панель
        /// </summary>
        /// <param name="doc">Документ в который сохраняются настройки</param>
        /// <returns></returns>
        public XmlNode Save(XmlDocument doc, String rootName)
        {
            try
            {
                XmlNode root = doc.CreateElement(rootName);
                VPanelGraphic[] graphics = { graphic_1, graphic_2, graphic_3, graphic_4, graphic_5 };

                if (graphics != null)
                {                    
                    foreach (VPanelGraphic graphic in graphics)
                    {
                        if (graphic != null)
                        {
                            XmlNode graphicNode = graphic.Save(doc);
                            if (graphicNode != null)
                            {
                                root.AppendChild(graphicNode);
                            }
                        }
                    }

                    if (manager != null)
                    {
                        XmlNode spanNode = doc.CreateElement("span");
                        XmlNode countNode = doc.CreateElement("count");
                        XmlNode orientationNode = doc.CreateElement("orientation");

                        spanNode.InnerText = manager.IntervalInCell.Ticks.ToString();
                        countNode.InnerText = manager.GrinCount.ToString();
                        orientationNode.InnerText = ((int)manager.Orientation).ToString();

                        root.AppendChild(spanNode);
                        root.AppendChild(countNode);
                        root.AppendChild(orientationNode);
                    }

                    return root;
                }


            }
            catch { }
            return null;
        }

        /// <summary>
        /// Загрузить настройки графиков
        /// </summary>
        /// <param name="root">Корневой узел с настройками</param>
        public void Load(XmlNode root)
        {
            try
            {
                if (root != null && root.HasChildNodes)
                {
                    int index = 0;
                    VPanelGraphic[] grs = { graphic_1, graphic_2, graphic_3, graphic_4, graphic_5 };

                    foreach (XmlNode child in root.ChildNodes)
                    {
                        switch (child.Name)
                        {
                            case VPanelGraphic.GraphicName:

                                try
                                {
                                    if (index > -1 && index < grs.Length)
                                    {
                                        grs[index++].Load(child);
                                    }
                                }
                                catch { }
                                break;

                            case "span":

                                try
                                {
                                    long ticks = long.Parse(child.InnerText);
                                    if (ticks > -1)
                                    {
                                        span = new TimeSpan(ticks);
                                    }
                                }
                                catch { }
                                break;

                            case "count":

                                try
                                {
                                    count = int.Parse(child.InnerText);
                                }
                                catch { }
                                break;

                            case "orientation":

                                try
                                {
                                    orientation = int.Parse(child.InnerText);
                                }
                                catch { }
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            catch { }
        }
    }
}