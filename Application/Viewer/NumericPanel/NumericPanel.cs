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
    /// Реализует цифровую панель. (Цифровая панель и одна графическая)
    /// </summary>
    public class NumericPanel : VPanel
    {
        protected NumericTable.Panel panel;         // реализует цифровую панель
        protected List<VPanelParameter> items;      // параметры отображаемые на цифровой панели

        protected GraphicManager manager = null;    // управляет отрисовкой параметров

        // ------------- графики цифровой панели -------------

        protected VPanelGraphic graphic_1;          // первый график
        protected VPanelGraphic graphic_2;          // второй график
        protected VPanelGraphic graphic_3;          // третий график
        protected VPanelGraphic graphic_4;          // четвертый график
        protected VPanelGraphic graphic_5;          // пятый график

        // -------------------------------------------------------

        protected Boolean loaded = false;           // загружены настройки или нет
        protected long initialized = 0;             // инициализирована панель или нет

        protected int splitterDistance = 325;       // геометрия окна

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="app">Контекст в котором работает панель</param>
        public NumericPanel(SgtApplication app)
            : base("Цифровая панель", VPanelType.NumericPanel)
        {
            _app = app;
            items = new List<VPanelParameter>();

            graphic_1 = new VPanelGraphic();
            graphic_2 = new VPanelGraphic();
            graphic_3 = new VPanelGraphic();
            graphic_4 = new VPanelGraphic();
            graphic_5 = new VPanelGraphic();

            _app.Technology.onComplete += new EventHandler(Technology_onComplete);
        }

        /// <summary>
        /// Положение разделителя на панели
        /// </summary>
        public int SplitterDistance
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return splitterDistance;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return 271;
            }

            set
            {
                if (slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        splitterDistance = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Выполнить инициализацию панели
        /// </summary>
        /// <param name="_panel">Цифровая панель</param>
        /// <param name="_manager">Управляющий графическим компонентом</param>
        public void init(NumericTable.Panel _panel, GraphicManager _manager)
        {
            if (Interlocked.Read(ref initialized) == 0)
            {
                panel = _panel;
                manager = _manager;

                InitializeGraphicPanel();
                InitializeNumericPanelItems();

                manager.Update();

                manager.UpdatePeriod = 1000;
                manager.Mode = GraphicComponent.DrawMode.Activ;

                Interlocked.Exchange(ref initialized, 1);
            }
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

                            if (p_item.Units != string.Empty)
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
                            panel.InsertItem(panel_item);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Инициализировать графическую панель
        /// </summary>
        protected void InitializeGraphicPanel()
        {
            manager.StartTime = DateTime.Now;
            manager.Orientation = GraphicComponent.Orientation.Vertical;

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

            manager.OnDataNeed += new EventHandler(manager_OnDataNeed);
        }

        /// <summary>
        /// Инициализировать графическую панель
        /// </summary>
        protected void UpdateGraphicPanel()
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
        /// Список отображаемых параметров на панели
        /// </summary>
        public List<VPanelParameter> Items
        {
            get
            {
                return items;
            }
        }

        /// <summary>
        /// отображаемый график 1
        /// </summary>
        public VPanelGraphic Graphic_1
        {
            get
            {
                return graphic_1;
            }
        }

        /// <summary>
        /// отображаемый график 2
        /// </summary>
        public VPanelGraphic Graphic_2
        {
            get
            {
                return graphic_2;
            }
        }

        /// <summary>
        /// отображаемый график 3
        /// </summary>
        public VPanelGraphic Graphic_3
        {
            get
            {
                return graphic_3;
            }
        }

        /// <summary>
        /// отображаемый график 4
        /// </summary>
        public VPanelGraphic Graphic_4
        {
            get
            {
                return graphic_4;
            }
        }

        /// <summary>
        /// отображаемый график 5
        /// </summary>
        public VPanelGraphic Graphic_5
        {
            get
            {
                return graphic_5;
            }
        }

        // ---------------------------------

        /// <summary>
        /// Обновить панель
        /// </summary>
        public override void Update()
        {
            try
            {
                base.Update();
                panel.ClearItems();

                UpdateGraphicPanel();
                InitializeNumericPanelItems();
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteToLog(this, new ErrorArgs(string.Format("message: {0} stack: {1}", ex.Message, ex.StackTrace)));
            }
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
                        panel.Redraw();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteToLog(this, new ErrorArgs(ex.Message, ErrorType.NotFatal));
            }
        }

        /// <summary>
        /// Актуализировать параметры цифровой панели
        /// </summary>
        public override void Actualize()
        {
            try
            {
                graphic_1.Parameter = _app.GetParameter(graphic_1.Identifier);
                graphic_2.Parameter = _app.GetParameter(graphic_2.Identifier);
                graphic_3.Parameter = _app.GetParameter(graphic_3.Identifier);
                graphic_4.Parameter = _app.GetParameter(graphic_4.Identifier);
                graphic_5.Parameter = _app.GetParameter(graphic_5.Identifier);

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

        /// <summary>
        /// Необходимо передать данные для отрисовки графическому компоненту
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Параметры события</param>
        private void manager_OnDataNeed(object sender, EventArgs e)
        {
            try
            {
                if (NShow)
                {
                    if (_app != null && Interlocked.Read(ref initialized) == 1)
                    {
                        Slice[] slices = _app.Commutator.GetDataFromBuffer(manager.StartTime, manager.FinishTime);
                        if (slices != null)
                        {
                            Graphic gr_1 = graphic_1.Tag as Graphic;
                            Graphic gr_2 = graphic_2.Tag as Graphic;
                            Graphic gr_3 = graphic_3.Tag as Graphic;
                            Graphic gr_4 = graphic_4.Tag as Graphic;
                            Graphic gr_5 = graphic_5.Tag as Graphic;

                            gr_1.Clear();
                            gr_2.Clear();
                            gr_3.Clear();
                            gr_4.Clear();
                            gr_5.Clear();

                            foreach (Slice slice in slices)
                            {
                                if (slice.slice != null)
                                {
                                    int sliceLen = slice.slice.Length;

                                    if (gr_1 != null)
                                    {
                                        Parameter pr_1 = graphic_1.Parameter; //_app.GetParameter(graphic_1.Identifier);
                                        if (pr_1 != null)
                                        {
                                            if (pr_1.Channel != null)
                                            {
                                                if (pr_1.Channel.Number > -1 && pr_1.Channel.Number < sliceLen)
                                                {
                                                    gr_1.Insert(slice._date, slice[pr_1.Channel.Number]);
                                                }
                                            }
                                        }
                                    }

                                    if (gr_2 != null)
                                    {
                                        Parameter pr_2 = graphic_2.Parameter; //_app.GetParameter(graphic_2.Identifier);
                                        if (pr_2 != null)
                                        {
                                            if (pr_2.Channel != null)
                                            {
                                                if (pr_2.Channel.Number > -1 && pr_2.Channel.Number < sliceLen)
                                                {
                                                    gr_2.Insert(slice._date, slice[pr_2.Channel.Number]);
                                                }
                                            }
                                        }
                                    }

                                    if (gr_3 != null)
                                    {
                                        Parameter pr_3 = graphic_3.Parameter;// _app.GetParameter(graphic_3.Identifier);
                                        if (pr_3 != null)
                                        {
                                            if (pr_3.Channel != null)
                                            {
                                                if (pr_3.Channel.Number > -3 && pr_3.Channel.Number < sliceLen)
                                                {
                                                    gr_3.Insert(slice._date, slice[pr_3.Channel.Number]);
                                                }
                                            }
                                        }
                                    }

                                    if (gr_4 != null)
                                    {
                                        Parameter pr_4 = graphic_4.Parameter; //_app.GetParameter(graphic_4.Identifier);
                                        if (pr_4 != null)
                                        {
                                            if (pr_4.Channel != null)
                                            {
                                                if (pr_4.Channel.Number > -4 && pr_4.Channel.Number < sliceLen)
                                                {
                                                    gr_4.Insert(slice._date, slice[pr_4.Channel.Number]);
                                                }
                                            }
                                        }
                                    }

                                    if (gr_5 != null)
                                    {
                                        Parameter pr_5 = graphic_5.Parameter; //_app.GetParameter(graphic_5.Identifier);
                                        if (pr_5 != null)
                                        {
                                            if (pr_5.Channel != null)
                                            {
                                                if (pr_5.Channel.Number > -5 && pr_5.Channel.Number < sliceLen)
                                                {
                                                    gr_5.Insert(slice._date, slice[pr_5.Channel.Number]);
                                                }
                                            }
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
        public const string rootName = "numericPanel";

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
                    XmlNode splitNode = doc.CreateElement("split");

                    panel_name.InnerText = PanelName;
                    splitNode.InnerText = splitterDistance.ToString();

                    root.AppendChild(panel_name);
                    root.AppendChild(splitNode);

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

                    VPanelGraphic[] graphics = { graphic_1, graphic_2, graphic_3, graphic_4, graphic_5 };
                    if (graphics != null)
                    {
                        XmlNode graphicsNode = doc.CreateElement(graphicsRoot);
                        foreach (VPanelGraphic graphic in graphics)
                        {
                            if (graphic != null)
                            {
                                XmlNode graphicNode = graphic.Save(doc);
                                if (graphicNode != null)
                                {
                                    graphicsNode.AppendChild(graphicNode);
                                }
                            }
                        }

                        root.AppendChild(graphicsNode);
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
        /// <param name="Root">Корневой узел в котром находятся настройки панели</param>
        public override void Load(XmlNode Root)
        {
            try
            {
                if (Root != null)
                {
                    if (Root.Name == rootName && Root.HasChildNodes)
                    {
                        foreach (XmlNode child in Root.ChildNodes)
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

                                case "split":

                                    try
                                    {
                                        splitterDistance = int.Parse(child.InnerText);
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

                                case graphicsRoot:

                                    try
                                    {
                                        LoadGraphics(child);
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

        /// <summary>
        /// Загрузка графиков
        /// </summary>
        /// <param name="root">Узел в котором сохраняются графики</param>
        protected void LoadGraphics(XmlNode root)
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

                        default:
                            break;
                    }
                }
            }
        }
    }
}