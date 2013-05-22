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
    /// Реализует общую панель отображения данных
    /// </summary>
    public class FullPanel : VPanel
    {
        protected NumericTable.Panel n_panel;       // числовая панель
        protected List<VPanelParameter> items;      // параметры отображаемые на цифровой панели

        protected GraphicPanel g_panel_1;           // графическая панель 1
        protected GraphicPanel g_panel_2;           // графическая панель 2
        protected GraphicPanel g_panel_3;           // графическая панель 3

        protected bool show_gr1 = false;            // отображать группу или нет
        protected bool show_gr2 = false;            // отображать группу или нет

        protected bool show_gr3 = false;            // отображать группу или нет
        protected bool show_gr4 = false;            // отображать группу или нет

        protected float scale_gr_1 = 0.5f;          // соотношение первой панели
        protected float scale_gr_2 = 0.5f;          // соотношение второй панели
        protected float scale_gr_3 = 0.5f;          // соотношение третьей панели

        // -------------------------------------------------------

        protected Boolean loaded = false;           // загружены настройки или нет
        protected long initialized = 0;             // инициализирована панель или нет

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public FullPanel()
            : base("Общая панель", VPanelType.FullPanel)
        {
            g_panel_1 = new GraphicPanel();
            g_panel_2 = new GraphicPanel();
            g_panel_3 = new GraphicPanel();

            items = new List<VPanelParameter>();

            _app = SgtApplication.CreateInstance();
            _app.Technology.onComplete += new EventHandler(Technology_onComplete);
        }

        /// <summary>
        /// Выполнить инициализацию панели
        /// </summary>
        /// <param name="_panel">Цифровая панель</param>
        /// <param name="_manager">Управляющий графическим компонентом</param>
        public void init(NumericTable.Panel _panel, GraphicManager _manager_1, 
            GraphicManager _manager_2, GraphicManager _manager_3)
        {
            if (Interlocked.Read(ref initialized) == 0)
            {
                n_panel = _panel;

                g_panel_1.GManager = _manager_1;
                g_panel_2.GManager = _manager_2;
                g_panel_3.GManager = _manager_3;

                DateTime now = DateTime.Now;

                g_panel_1.GManager.StartTime = now;
                g_panel_2.GManager.StartTime = now;
                g_panel_3.GManager.StartTime = now;

                g_panel_1.GManager.Orientation = Orientation.Vertical;
                g_panel_2.GManager.Orientation = Orientation.Vertical;
                g_panel_3.GManager.Orientation = Orientation.Vertical;

                InitializeGraphicPanel();
                InitializeNumericPanelItems();

                g_panel_1.GManager.OnDataNeed += new EventHandler(manager_OnDataNeed_1);
                g_panel_2.GManager.OnDataNeed += new EventHandler(manager_OnDataNeed_2);
                g_panel_3.GManager.OnDataNeed += new EventHandler(manager_OnDataNeed_3);

                g_panel_1.GManager.Update();
                g_panel_2.GManager.Update();
                g_panel_3.GManager.Update();

                g_panel_1.GManager.UpdatePeriod = 1000;
                g_panel_1.GManager.Mode = DrawMode.Activ;

                g_panel_2.GManager.UpdatePeriod = 1000;
                g_panel_2.GManager.Mode = DrawMode.Activ;

                g_panel_3.GManager.UpdatePeriod = 1000;
                g_panel_3.GManager.Mode = DrawMode.Activ;

                Interlocked.Exchange(ref initialized, 1);
            }
        }

        /// <summary>
        /// Выполнить инициализацию элементов графических панелей
        /// </summary>
        protected void InitializeGraphicPanel()
        {
            try
            {
                g_panel_1.InitializePanel();
                g_panel_2.InitializePanel();
                g_panel_3.InitializePanel();
            }
            catch { }
        }

        /// <summary>
        /// Выполнить инициализацию элементов цифровой панели
        /// </summary>
        protected void InitializeNumericPanelItems()
        {
            if (items != null)
            {
                foreach (VPanelParameter item in items)
                {
                    if (item != null)
                    {
                        Parameter p_item = _app.GetParameter(item.Identifier);
                        if (p_item != null)
                        {
                            PanelItem panel_item = new PanelItem();

                            string total = Regex.Replace(p_item.Name, @"(?<=\[).+(?=\])", string.Empty);
                            panel_item.Description = total.Replace("[]", String.Empty);

                            //panel_item.Description = p_item.Name;

                            if (p_item.Units != string.Empty && p_item.Units != "Единицы измерения не определены")
                            {
                                panel_item.Description += "(" + p_item.Units + ")";
                            }

                            if (item.Font != null)
                            {
                                panel_item.Font = new System.Drawing.Font(item.Font, item.Font.Style);
                            }
                            else
                                panel_item.Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 12.0f, FontStyle.Regular);

                            panel_item.Color = item.Color;

                            item.Tag = panel_item;
                            n_panel.InsertItem(panel_item);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Элементы, отображаемые на цифровой панели
        /// </summary>
        public List<VPanelParameter> Items
        {
            get
            {
                return items;
            }
        }

        /// <summary>
        /// Графическая панель 1
        /// </summary>
        public GraphicPanel GPanel_1
        {
            get { return g_panel_1; }
        }

        /// <summary>
        /// Графическая панель 2
        /// </summary>
        public GraphicPanel GPanel_2
        {
            get { return g_panel_2; }
        }

        /// <summary>
        /// Графическая панель 3
        /// </summary>
        public GraphicPanel GPanel_3
        {
            get { return g_panel_3; }
        }

        /// <summary>
        /// отображать группу или нет
        /// </summary>
        public bool Show_gr1
        {
            get { return show_gr1; }
            set { show_gr1 = value; }
        }

        /// <summary>
        /// отображать группу или нет
        /// </summary>
        public bool Show_gr2
        {
            get { return show_gr2; }
            set { show_gr2 = value; }
        }

        /// <summary>
        /// отображать группу или нет
        /// </summary>
        public bool Show_gr3
        {
            get { return show_gr3; }
            set { show_gr3 = value; }
        }

        /// <summary>
        /// Соотношение первой панели
        /// </summary>
        public float Scale_gr_1
        {
            get { return scale_gr_1; }
            set { scale_gr_1 = value; }
        }

        /// <summary>
        /// Соотношение второй панели
        /// </summary>
        public float Scale_gr_2
        {
            get { return scale_gr_2; }
            set { scale_gr_2 = value; }
        }

        /// <summary>
        /// Соотношение третьей панели
        /// </summary>
        public float Scale_gr_3
        {
            get { return scale_gr_3; }
            set { scale_gr_3 = value; }
        }

        /// <summary>
        /// Обновить панель
        /// </summary>
        public override void Update()
        {
            try
            {
                base.Update();
                n_panel.ClearItems();

                GPanel_1.UpdatePanel();
                GPanel_2.UpdatePanel();
                GPanel_3.UpdatePanel();

                InitializeNumericPanelItems();
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteToLog(this, new ErrorArgs(string.Format("message: {0} stack: {1}", ex.Message, ex.StackTrace)));
            }
        }

        /// <summary>
        /// Обновить панель
        /// </summary>
        public override void UpdateWithRedraw()
        {
            try
            {
                base.Update();
                n_panel.ClearItems();

                GPanel_1.UpdatePanelWithRedraw();
                GPanel_2.UpdatePanelWithRedraw();
                GPanel_3.UpdatePanelWithRedraw();

                InitializeNumericPanelItems();
                n_panel.Redraw();
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteToLog(this, new ErrorArgs(string.Format("message: {0} stack: {1}", ex.Message, ex.StackTrace)));
            }
        }



        /// <summary>
        /// Синхронизировать числовые параметры
        /// </summary>
        public void UpdateNumeric()
        {
            try
            {
                foreach (VPanelParameter item in items)
                {
                    if (item != null && item.Tag != null)
                    {
                        PanelItem p_item = item.Tag as PanelItem;
                        if (p_item != null)
                        {
                            item.Font = p_item.Font;
                            item.Color = p_item.Color;
                        }
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Актуализировать параметры цифровой панели
        /// </summary>
        public override void Actualize()
        {
            try
            {
                GPanel_1.Actualize(_app);
                GPanel_2.Actualize(_app);
                GPanel_3.Actualize(_app);

                if (items != null)
                {
                    foreach (VPanelParameter item in items)
                    {
                        if (item != null)
                        {
                            Parameter p_item = _app.GetParameter(item.Identifier);
                            if (p_item != null)
                            {
                                PanelItem panel_item = item.Tag as PanelItem;
                                if (panel_item != null)
                                {
                                    string total = Regex.Replace(p_item.Name, @"(?<=\[).+(?=\])", string.Empty);
                                    panel_item.Description = total.Replace("[]", String.Empty);

                                    if (p_item.Units != string.Empty)
                                    {
                                        panel_item.Description += "(" + p_item.Units + ")";
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        // ------------------------- обработчики событий -------------------------

        /// <summary>
        /// Технология завершила обработку данных
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void Technology_onComplete(object sender, EventArgs e)
        {
            try
            {
                if (NShow)
                {
                    if (Interlocked.Read(ref initialized) == 1)
                    {
                        if (items != null)
                        {
                            foreach (VPanelParameter item in items)
                            {
                                if (item != null && item.Tag != null)
                                {
                                    Parameter parameter = _app.GetParameter(item.Identifier);
                                    if (parameter != null)
                                    {
                                        PanelItem panel_item = item.Tag as PanelItem;
                                        if (panel_item != null)
                                        {
                                            panel_item.Value = parameter.FormattedCaclulatedValue;
                                        }
                                    }
                                }
                            }
                        }
                        n_panel.Redraw();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteToLog(this, new ErrorArgs(ex.Message, ErrorType.NotFatal));
            }
        }

        /// <summary>
        /// Необходимо передать данные для отрисовки графическому компоненту
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Параметры события</param>
        private void manager_OnDataNeed_1(object sender, EventArgs e)
        {
            try
            {
                if (NShow)
                {
                    if (_app != null && Interlocked.Read(ref initialized) == 1)
                    {
                        if (Show_gr1)
                        {
                            Graphic gr_1 = GPanel_1.Graphic_1.Tag as Graphic;
                            Graphic gr_2 = GPanel_1.Graphic_2.Tag as Graphic;
                            Graphic gr_3 = GPanel_1.Graphic_3.Tag as Graphic;
                            Graphic gr_4 = GPanel_1.Graphic_4.Tag as Graphic;
                            Graphic gr_5 = GPanel_1.Graphic_5.Tag as Graphic;

                            if (gr_1 != null) gr_1.Clear();
                            if (gr_2 != null) gr_2.Clear();
                            if (gr_3 != null) gr_3.Clear();
                            if (gr_4 != null) gr_4.Clear();
                            if (gr_5 != null) gr_5.Clear();
                                                                
                            Parameter p1, p2, p3, p4, p5;
                            int np1 = -1, np2 = -1, np3 = -1, np4 = -1, np5 = -1;

                            p1 = GPanel_1.Graphic_1.Parameter;
                            p2 = GPanel_1.Graphic_2.Parameter;
                            p3 = GPanel_1.Graphic_3.Parameter;
                            p4 = GPanel_1.Graphic_4.Parameter;
                            p5 = GPanel_1.Graphic_5.Parameter;

                            if (p1 != null && p1.Channel != null) np1 = p1.Channel.Number;
                            if (p2 != null && p2.Channel != null) np2 = p2.Channel.Number;

                            if (p3 != null && p3.Channel != null) np3 = p3.Channel.Number;
                            if (p4 != null && p4.Channel != null) np4 = p4.Channel.Number;

                            if (p5 != null && p5.Channel != null) np5 = p5.Channel.Number;

                            Slice[] slices = _app.Commutator.GetDataFromBuffer(GPanel_1.GManager.StartTime, GPanel_1.GManager.FinishTime, 
                                np1, np2, np3, np4, np5);

                            if (slices != null)
                            {
                                DateTime _minT = _app.Commutator.MinTimeParameter();
                                foreach (Slice slice in slices)
                                {
                                    if (slice.slice != null)
                                    {
                                        if (gr_1 != null)
                                        {
                                            gr_1.Insert(slice._date, slice[0]);
                                            gr_1.Tmin = _minT;
                                        }

                                        if (gr_2 != null)
                                        {
                                            gr_2.Insert(slice._date, slice[1]);
                                            gr_2.Tmin = _minT;
                                        }

                                        if (gr_3 != null)
                                        {
                                            gr_3.Insert(slice._date, slice[2]);
                                            gr_3.Tmin = _minT;
                                        }

                                        if (gr_4 != null)
                                        {
                                            gr_4.Insert(slice._date, slice[3]);
                                            gr_4.Tmin = _minT;
                                        }

                                        if (gr_5 != null)
                                        {
                                            gr_5.Insert(slice._date, slice[4]);
                                            gr_5.Tmin = _minT;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteToLog(this, new ErrorArgs(ex.Message, ErrorType.NotFatal));
            }
        }

        /// <summary>
        /// Необходимо передать данные для отрисовки графическому компоненту
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Параметры события</param>
        private void manager_OnDataNeed_2(object sender, EventArgs e)
        {
            try
            {
                if (NShow)
                {
                    if (_app != null && Interlocked.Read(ref initialized) == 1)
                    {
                        if (Show_gr2)
                        {
                            Graphic gr_1 = GPanel_2.Graphic_1.Tag as Graphic;
                            Graphic gr_2 = GPanel_2.Graphic_2.Tag as Graphic;
                            Graphic gr_3 = GPanel_2.Graphic_3.Tag as Graphic;
                            Graphic gr_4 = GPanel_2.Graphic_4.Tag as Graphic;
                            Graphic gr_5 = GPanel_2.Graphic_5.Tag as Graphic;

                            if (gr_1 != null) gr_1.Clear();
                            if (gr_2 != null) gr_2.Clear();
                            if (gr_3 != null) gr_3.Clear();
                            if (gr_4 != null) gr_4.Clear();
                            if (gr_5 != null) gr_5.Clear();

                            Parameter p1, p2, p3, p4, p5;
                            int np1 = -1, np2 = -1, np3 = -1, np4 = -1, np5 = -1;

                            p1 = GPanel_2.Graphic_1.Parameter;
                            p2 = GPanel_2.Graphic_2.Parameter;
                            p3 = GPanel_2.Graphic_3.Parameter;
                            p4 = GPanel_2.Graphic_4.Parameter;
                            p5 = GPanel_2.Graphic_5.Parameter;

                            if (p1 != null && p1.Channel != null) np1 = p1.Channel.Number;
                            if (p2 != null && p2.Channel != null) np2 = p2.Channel.Number;

                            if (p3 != null && p3.Channel != null) np3 = p3.Channel.Number;
                            if (p4 != null && p4.Channel != null) np4 = p4.Channel.Number;

                            if (p5 != null && p5.Channel != null) np5 = p5.Channel.Number;

                            Slice[] slices = _app.Commutator.GetDataFromBuffer(GPanel_2.GManager.StartTime, GPanel_2.GManager.FinishTime,
                                np1, np2, np3, np4, np5);

                            if (slices != null)
                            {
                                DateTime _minT = _app.Commutator.MinTimeParameter();
                                foreach (Slice slice in slices)
                                {
                                    if (slice.slice != null)
                                    {
                                        if (gr_1 != null)
                                        {
                                            gr_1.Insert(slice._date, slice[0]);
                                            gr_1.Tmin = _minT;
                                        }

                                        if (gr_2 != null)
                                        {
                                            gr_2.Insert(slice._date, slice[1]);
                                            gr_2.Tmin = _minT;
                                        }

                                        if (gr_3 != null)
                                        {
                                            gr_3.Insert(slice._date, slice[2]);
                                            gr_3.Tmin = _minT;
                                        }

                                        if (gr_4 != null)
                                        {
                                            gr_4.Insert(slice._date, slice[3]);
                                            gr_4.Tmin = _minT;
                                        }

                                        if (gr_5 != null)
                                        {
                                            gr_5.Insert(slice._date, slice[4]);
                                            gr_5.Tmin = _minT;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteToLog(this, new ErrorArgs(ex.Message, ErrorType.NotFatal));
            }
        }

        /// <summary>
        /// Необходимо передать данные для отрисовки графическому компоненту
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Параметры события</param>
        private void manager_OnDataNeed_3(object sender, EventArgs e)
        {
            try
            {
                if (NShow)
                {
                    if (_app != null && Interlocked.Read(ref initialized) == 1)
                    {
                        if (Show_gr3)
                        {
                            Graphic gr_1 = GPanel_3.Graphic_1.Tag as Graphic;
                            Graphic gr_2 = GPanel_3.Graphic_2.Tag as Graphic;
                            Graphic gr_3 = GPanel_3.Graphic_3.Tag as Graphic;
                            Graphic gr_4 = GPanel_3.Graphic_4.Tag as Graphic;
                            Graphic gr_5 = GPanel_3.Graphic_5.Tag as Graphic;

                            if (gr_1 != null) gr_1.Clear();
                            if (gr_2 != null) gr_2.Clear();
                            if (gr_3 != null) gr_3.Clear();
                            if (gr_4 != null) gr_4.Clear();
                            if (gr_5 != null) gr_5.Clear();

                            Parameter p1, p2, p3, p4, p5;
                            int np1 = -1, np2 = -1, np3 = -1, np4 = -1, np5 = -1;

                            p1 = GPanel_3.Graphic_1.Parameter;
                            p2 = GPanel_3.Graphic_2.Parameter;
                            p3 = GPanel_3.Graphic_3.Parameter;
                            p4 = GPanel_3.Graphic_4.Parameter;
                            p5 = GPanel_3.Graphic_5.Parameter;

                            if (p1 != null && p1.Channel != null) np1 = p1.Channel.Number;
                            if (p2 != null && p2.Channel != null) np2 = p2.Channel.Number;

                            if (p3 != null && p3.Channel != null) np3 = p3.Channel.Number;
                            if (p4 != null && p4.Channel != null) np4 = p4.Channel.Number;

                            if (p5 != null && p5.Channel != null) np5 = p5.Channel.Number;

                            Slice[] slices = _app.Commutator.GetDataFromBuffer(GPanel_3.GManager.StartTime, GPanel_3.GManager.FinishTime,
                                np1, np2, np3, np4, np5);

                            if (slices != null)
                            {
                                DateTime _minT = _app.Commutator.MinTimeParameter();
                                foreach (Slice slice in slices)
                                {
                                    if (slice.slice != null)
                                    {
                                        if (gr_1 != null)
                                        {
                                            gr_1.Insert(slice._date, slice[0]);
                                            gr_1.Tmin = _minT;
                                        }

                                        if (gr_2 != null)
                                        {
                                            gr_2.Insert(slice._date, slice[1]);
                                            gr_2.Tmin = _minT;
                                        }

                                        if (gr_3 != null)
                                        {
                                            gr_3.Insert(slice._date, slice[2]);
                                            gr_3.Tmin = _minT;
                                        }

                                        if (gr_4 != null)
                                        {
                                            gr_4.Insert(slice._date, slice[3]);
                                            gr_4.Tmin = _minT;
                                        }

                                        if (gr_5 != null)
                                        {
                                            gr_5.Insert(slice._date, slice[4]);
                                            gr_5.Tmin = _minT;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteToLog(this, new ErrorArgs(ex.Message, ErrorType.NotFatal));
            }
        }

        // --------------------- сохраняемся/загружаемся ---------------------

        /// <summary>
        /// Имя узла в котором сохраняется корневой узел панели
        /// </summary>
        public const string rootName = "fullPanel";

        /// <summary>
        /// имя узла в котором сохраняются цифровые параметры
        /// </summary>
        protected const string numbersRoot = "numbers";

        /// <summary>
        /// имя узла в котором сохраняются графические параметры
        /// </summary>
        protected const string graphicsRoot = "graphics";

        /// <summary>
        /// Сохранить настройки панели
        /// </summary>
        /// <param name="doc">Xml документ в который осуществляется сохранение настроек панели</param>
        /// <returns>Сохраненые настройки панели</returns>
        public override XmlNode Save(XmlDocument doc)
        {
            try
            {
                foreach (VPanelParameter item in items)
                {
                    if (item != null && item.Tag != null)
                    {
                        PanelItem p_item = item.Tag as PanelItem;
                        if (p_item != null)
                        {
                            item.Font = p_item.Font;
                            item.Color = p_item.Color;
                        }
                    }
                }

                if (doc != null)
                {
                    XmlNode root = doc.CreateElement(rootName);
                    XmlNode panel_name = doc.CreateElement("panel_name");

                    XmlNode show_gr1Node = doc.CreateElement("show_gr1");
                    XmlNode show_gr2Node = doc.CreateElement("show_gr2");

                    XmlNode show_gr3Node = doc.CreateElement("show_gr3");
                    XmlNode show_gr4Node = doc.CreateElement("show_gr4");

                    XmlNode scale_gr_1Node = doc.CreateElement("scale_gr_1");
                    XmlNode scale_gr_2Node = doc.CreateElement("scale_gr_2");
                    XmlNode scale_gr_3Node = doc.CreateElement("scale_gr_3");

                    panel_name.InnerText = PanelName;

                    show_gr1Node.InnerText = show_gr1.ToString();
                    show_gr2Node.InnerText = show_gr2.ToString();

                    show_gr3Node.InnerText = show_gr3.ToString();
                    show_gr4Node.InnerText = show_gr4.ToString();

                    scale_gr_1Node.InnerText = scale_gr_1.ToString();
                    scale_gr_2Node.InnerText = scale_gr_2.ToString();
                    scale_gr_3Node.InnerText = scale_gr_3.ToString();

                    root.AppendChild(panel_name);

                    root.AppendChild(show_gr1Node);
                    root.AppendChild(show_gr2Node);

                    root.AppendChild(show_gr3Node);
                    root.AppendChild(show_gr4Node);

                    root.AppendChild(scale_gr_1Node);
                    root.AppendChild(scale_gr_2Node);
                    root.AppendChild(scale_gr_3Node);

                    if (items != null)
                    {
                        XmlNode numberParametersNode = doc.CreateElement(numbersRoot);
                        foreach (VPanelParameter item in items)
                        {
                            if (item != null)
                            {
                                XmlNode itemNode = item.Save(doc);
                                if (itemNode != null)
                                {
                                    numberParametersNode.AppendChild(itemNode);
                                }
                            }
                        }

                        root.AppendChild(numberParametersNode);
                    }

                    int index = 1;
                    GraphicPanel[] panels = { g_panel_1, g_panel_2, g_panel_3 };

                    foreach (GraphicPanel panel in panels)
                    {
                        if (panel != null)
                        {
                            XmlNode panelNode = panel.Save(doc, string.Format("GPanel{0}", index));
                            if (panelNode != null)
                            {
                                index = index + 1;
                                root.AppendChild(panelNode);
                            }
                        }
                    }

                    return root;
                }
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Загрузить настройки панели
        /// </summary>
        /// <param name="root">Корневой узел панели</param>
        public override void Load(XmlNode root)
        {
            try
            {
                if (root != null)
                {
                    if (root.Name == rootName && root.HasChildNodes)
                    {
                        foreach (XmlNode child in root.ChildNodes)
                        {
                            switch (child.Name)
                            {
                                case "panel_name":

                                    try
                                    {
                                        PanelName = child.InnerText;
                                    }
                                    catch { }
                                    break;

                                case "show_gr1":

                                    try
                                    {
                                        show_gr1 = bool.Parse(child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case "show_gr2":

                                    try
                                    {
                                        show_gr2 = bool.Parse(child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case "show_gr3":

                                    try
                                    {
                                        show_gr3 = bool.Parse(child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case "show_gr4":

                                    try
                                    {
                                        show_gr4 = bool.Parse(child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case "scale_gr_1":

                                    try
                                    {
                                        scale_gr_1 = float.Parse(child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case "scale_gr_2":

                                    try
                                    {
                                        scale_gr_2 = float.Parse(child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case "scale_gr_3":

                                    try
                                    {
                                        scale_gr_3 = float.Parse(child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case numbersRoot:

                                    try
                                    {
                                        LoadNumeric(child);
                                    }
                                    catch { }
                                    break;

                                case "GPanel1":

                                    try
                                    {
                                        g_panel_1.Load(child);
                                    }
                                    catch { }
                                    break;

                                case "GPanel2":

                                    try
                                    {
                                        g_panel_2.Load(child);
                                    }
                                    catch { }
                                    break;

                                case "GPanel3":

                                    try
                                    {
                                        g_panel_3.Load(child);
                                    }
                                    catch { }
                                    break;

                                case "GPanel4":

                                    try
                                    {                                        
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
            catch { }
        }

        /// <summary>
        /// Загрузить числовые параметры
        /// </summary>
        /// <param name="root">Узел в котором числовые параметры</param>
        protected void LoadNumeric(XmlNode root)
        {
            if (root != null && root.HasChildNodes)
            {
                foreach (XmlNode child in root.ChildNodes)
                {
                    switch (child.Name)
                    {
                        case VPanelParameter.rootName:

                            try
                            {
                                VPanelParameter parameter = new VPanelParameter();
                                parameter.Load(child);

                                items.Add(parameter);
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