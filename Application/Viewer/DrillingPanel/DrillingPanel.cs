using System;
using System.Xml;
using System.Drawing;
using System.Threading;

using Buffering;
using NumericTable;
using GraphicComponent;

namespace SGT
{
    /// <summary>
    /// реализует отображение и настройку панели Буровая площадка
    /// </summary>
    public class DrillingPanel : VPanel
    {
        protected NumericTable.Panel panel;         // реализует цифровую панель
        protected GraphicManager manager = null;    // управляет отрисовкой параметров

        protected Graphic graphic_glybina;          // отображаемый график Глубина скважины
        protected Graphic graphic_mehskorost;       // отображаемый график Мех.скорость проходки
        protected Graphic graphic_vesnakruke;       // отображаемый график вес на крюке
        protected Graphic graphic_davlenienaman;    // отображаемый график давления на манифольде
        protected Graphic graphic_rashodnavhode;    // отображаемый график расход на входе

        protected PanelItem[] items = null;         // параметры отображаемые на панели Буровая площадка

        protected VPanelParameter svp;              // Обороты СВП"
        protected VPanelParameter m_svp;            // Момент СВП"

        protected VPanelParameter kmb;              // Усиление в ключе КМБ
        protected VPanelParameter rotor;            // Момент на роторе        

        protected VPanelParameter mom1;             // Момент на ключе №1
        protected VPanelParameter mom2;             // Момент на ключе №2

        protected Boolean loaded = false;           // загружены настройки или нет
        protected long initialized = 0;             // инициализирована панель или нет

        protected float splitterDistance = 324;       // геометрия панели

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="app">Контекст в котором работает панель</param>
        public DrillingPanel(SgtApplication app)
            : base("Буровая площадка", VPanelType.DrillingFloor)
        {
            _app = app;

            svp = new VPanelParameter();
            m_svp = new VPanelParameter();

            kmb = new VPanelParameter();
            rotor = new VPanelParameter();

            mom1 = new VPanelParameter();
            mom2 = new VPanelParameter();

            app.Technology.onComplete += new EventHandler(Technology_onComplete);
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="show">Осуществлять вывод данных на панель или нет</param>
        public DrillingPanel(Boolean show)
            : base("Буровая площадка", VPanelType.DrillingFloor)
        {
            n_show = show;
        }

        /// <summary>
        /// Обороты СВП"
        /// </summary>
        public VPanelParameter Svp
        {
            get 
            { 
                return svp; 
            }
        }

        /// <summary>
        /// Момент СВП"
        /// </summary>
        public VPanelParameter MSvp
        {
            get
            {
                return m_svp;
            }
        }

        /// <summary>
        /// Усиление в ключе КМБ
        /// </summary>
        public VPanelParameter KMB
        {
            get
            {
                return kmb;
            }
        }

        /// <summary>
        /// Момент на роторе
        /// </summary>
        public VPanelParameter Rotor
        {
            get
            {
                return rotor;
            }
        }

        /// <summary>
        /// Момент на ключе №1
        /// </summary>
        public VPanelParameter Mom1
        {
            get
            {
                return mom1;
            }
        }

        /// <summary>
        /// Момент на ключе №2
        /// </summary>
        public VPanelParameter Mom2
        {
            get
            {
                return mom2;
            }
        }

        /// <summary>
        /// График Глубина скважины
        /// </summary>
        public Graphic Glybina
        {
            get
            {
                return graphic_glybina;
            }
        }

        /// <summary>
        /// график Мех.скорость проходки
        /// </summary>
        public Graphic Mehskorost
        {
            get
            {
                return graphic_mehskorost;
            }
        }
        /// <summary>
        /// график вес на крюке
        /// </summary>
        public Graphic Vesnakruke
        {
            get
            {
                return graphic_vesnakruke;
            }
        }

        /// <summary>
        /// график давления на манифольде
        /// </summary>
        public Graphic Davlenienaman
        {
            get
            {
                return graphic_davlenienaman;
            }
        }

        /// <summary>
        /// график расход на входе
        /// </summary>
        public Graphic Rashodnavhode
        {
            get
            {
                return graphic_rashodnavhode;
            }
        }

        /// <summary>
        /// Положение разделителя на панели
        /// </summary>
        public float SplitterDistance
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

                return 324;
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

        // ------------------------ вспомогательные функции ------------------------

        /// <summary>
        /// Выполнить инициализацию элементов цифровой панели
        /// </summary>
        protected void InitializeNumericPanelItems()
        {
            items = new PanelItem[19];
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = new PanelItem();

                items[i].Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 12.0f, FontStyle.Regular);
                panel.InsertItem(items[i]);
            }

            items[0].Description = "Над Забоем(м)";
            items[1].Description = "Мех.скорость проходки(м/час)";
            items[2].Description = "ДМК(мин/м)";
            items[3].Description = "Подача(м)";
            items[4].Description = "Номер свечи";
            items[5].Description = "Положение тальблока(м)";
            items[6].Description = "Вес на крюке(т)";
            items[7].Description = "Давление на манифольде(кг/см2)";
            items[8].Description = "Нагрузка на долото(т)";
            items[9].Description = "Обороты ротора(об/мин)";
            items[10].Description = "Момент на ключе №1(кГм)";
            items[11].Description = "Момент на ключе №2(кГм)";
            items[12].Description = "Момент на роторе(кНм)";
            items[13].Description = "Обороты СВП(кНм)";
            items[14].Description = "Расход на входе(л/сек)";
            items[15].Description = "Приток бурового раствора(м.куб)";
            items[16].Description = "Длина инструмента(м)";
            items[17].Description = "Момент СВП(кНм)";
            items[18].Description = "Усилие в ключе КМБ(Тс)";

            // -------------------------------------------------------

            if (n_panelNode != null)
            {
                panel.Load(n_panelNode);
                n_panelNode = null;
            }
        }

        /// <summary>
        /// Инициализировать графическую панель
        /// </summary>
        protected void InitializeGraphicPanel()
        {
            manager.StartTime = DateTime.Now;

            manager.OnData += new OnDataEventHander(manager_OnData);
            manager.OnDataNeed += new EventHandler(manager_OnDataNeed);

            manager.Orientation = GraphicComponent.Orientation.Vertical;

            graphic_glybina = manager.InstanceGraphic();          // отображаемый график Глубина скважины
            if (loaded == false)
            {
                graphic_glybina.Description = "Забой";

                graphic_glybina.Range.Min = 0;
                graphic_glybina.Range.Max = 6500;

                graphic_glybina.Units = "м";

                graphic_glybina.Color = Color.Red;
            }
            else
            {
                if (gr_glybina != null)
                {
                    graphic_glybina.Description = gr_glybina.Description;

                    graphic_glybina.Range.Min = gr_glybina.Range.Min;
                    graphic_glybina.Range.Max = gr_glybina.Range.Max;

                    graphic_glybina.Units = gr_glybina.Units;
                    graphic_glybina.Color = gr_glybina.Color;

                    gr_glybina.Font.Dispose();
                    gr_glybina.Brush.Dispose();

                    gr_glybina = null;
                }
            }

            graphic_mehskorost = manager.InstanceGraphic();       // отображаемый график Мех.скорость проходки
            if (loaded == false)
            {
                graphic_mehskorost.Range.Min = 0;
                graphic_mehskorost.Range.Max = 200;

                graphic_mehskorost.Units = "м/ч";
                graphic_mehskorost.Description = "СК.Мех";

                graphic_mehskorost.Color = Color.Green;
            }
            else
                if (gr_mehskorost != null)
                {
                    graphic_mehskorost.Description = gr_mehskorost.Description;

                    graphic_mehskorost.Range.Min = gr_mehskorost.Range.Min;
                    graphic_mehskorost.Range.Max = gr_mehskorost.Range.Max;

                    graphic_mehskorost.Units = gr_mehskorost.Units;
                    graphic_mehskorost.Color = gr_mehskorost.Color;

                    gr_mehskorost.Font.Dispose();
                    gr_mehskorost.Brush.Dispose();

                    gr_mehskorost = null;
                }

            graphic_vesnakruke = manager.InstanceGraphic();       // отображаемый график вес на крюке
            if (loaded == false)
            {
                graphic_vesnakruke.Range.Min = 0;
                graphic_vesnakruke.Range.Max = 450;

                graphic_vesnakruke.Units = "тс";
                graphic_vesnakruke.Description = "Вес";

                graphic_vesnakruke.Color = Color.Blue;
            }
            else
                if (gr_vesnakruke != null)
                {
                    graphic_vesnakruke.Description = gr_vesnakruke.Description;

                    graphic_vesnakruke.Range.Min = gr_vesnakruke.Range.Min;
                    graphic_vesnakruke.Range.Max = gr_vesnakruke.Range.Max;

                    graphic_vesnakruke.Units = gr_vesnakruke.Units;
                    graphic_vesnakruke.Color = gr_vesnakruke.Color;

                    gr_vesnakruke.Font.Dispose();
                    gr_vesnakruke.Brush.Dispose();

                    gr_vesnakruke = null;
                }
            
            graphic_davlenienaman = manager.InstanceGraphic();    // отображаемый график давления на манифольде
            if (loaded == false)
            {
                graphic_davlenienaman.Range.Min = 0;
                graphic_davlenienaman.Range.Max = 400;

                graphic_davlenienaman.Units = "кг/см2";
                graphic_davlenienaman.Description = "Давление";

                graphic_davlenienaman.Color = Color.Brown;
            }
            else
                if (gr_davlenienaman != null)
                {
                    graphic_davlenienaman.Description = gr_davlenienaman.Description;

                    graphic_davlenienaman.Range.Min = gr_davlenienaman.Range.Min;
                    graphic_davlenienaman.Range.Max = gr_davlenienaman.Range.Max;

                    graphic_davlenienaman.Units = gr_davlenienaman.Units;
                    graphic_davlenienaman.Color = gr_davlenienaman.Color;

                    gr_davlenienaman.Font.Dispose();
                    gr_davlenienaman.Brush.Dispose();

                    gr_davlenienaman = null;
                }

            graphic_rashodnavhode = manager.InstanceGraphic();    // отображаемый график расход на входе
            if (loaded == false)
            {
                graphic_rashodnavhode.Range.Min = 0;
                graphic_rashodnavhode.Range.Max = 100;

                graphic_rashodnavhode.Units = "л/с";
                graphic_rashodnavhode.Description = "Расход";

                graphic_rashodnavhode.Color = Color.DarkOrange;
            }
            else
                if (gr_rashodnavhode != null)
                {
                    graphic_rashodnavhode.Description = gr_rashodnavhode.Description;

                    graphic_rashodnavhode.Range.Min = gr_rashodnavhode.Range.Min;
                    graphic_rashodnavhode.Range.Max = gr_rashodnavhode.Range.Max;

                    graphic_rashodnavhode.Units = gr_rashodnavhode.Units;
                    graphic_rashodnavhode.Color = gr_rashodnavhode.Color;

                    gr_rashodnavhode.Font.Dispose();
                    gr_rashodnavhode.Brush.Dispose();

                    gr_rashodnavhode = null;
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
            if (NShow)
            {
                if (Interlocked.Read(ref initialized) == 1)
                {
                    /*Parameter p_svp = _app.GetParameter(svp.PNumber);
                    Parameter p_m_svp = _app.GetParameter(m_svp.PNumber);

                    Parameter p_kmb = _app.GetParameter(kmb.PNumber);
                    Parameter p_rotor = _app.GetParameter(rotor.PNumber);

                    Parameter p_mom1 = _app.GetParameter(mom1.PNumber);
                    Parameter p_mom2 = _app.GetParameter(mom2.PNumber);*/

                    Parameter p_svp = _app.GetParameter(svp.Identifier);
                    Parameter p_m_svp = _app.GetParameter(m_svp.Identifier);

                    Parameter p_kmb = _app.GetParameter(kmb.Identifier);
                    Parameter p_rotor = _app.GetParameter(rotor.Identifier);

                    Parameter p_mom1 = _app.GetParameter(mom1.Identifier);
                    Parameter p_mom2 = _app.GetParameter(mom2.Identifier);

                    items[0].Value = string.Format("{0:F3}", _app.Technology.P0211.Value);
                    items[1].Value = string.Format("{0:F3}", _app.Technology.P0208.Value);
                    items[2].Value = string.Format("{0:F3}", _app.Technology.P0209.Value);
                    items[3].Value = string.Format("{0:F3}", _app.Technology.P0207.Value);
                    items[4].Value = string.Format("{0:F3}", _app.Technology.P0203.Value);
                    items[5].Value = string.Format("{0:F3}", _app.Technology.P0005.Value);
                    items[6].Value = string.Format("{0:F3}", _app.Technology.P0102.Value);
                    items[7].Value = string.Format("{0:F3}", _app.Technology.P0004.Value);
                    items[8].Value = string.Format("{0:F3}", _app.Technology.P0201.Value);
                    items[9].Value = string.Format("{0:F3}", _app.Technology.P0110.Value);

                    if (p_mom1 != null)
                    {
                        /*items[10].Color = Color.Black;
                        if (p_mom1.IsControlAlarm)
                        {
                            if (p_mom1.CalculatedValue >= p_mom1.Alarm)
                            {
                                items[10].Color = Color.Red;
                            }
                        }*/
                        items[10].Value = p_mom1.FormattedCaclulatedValue;
                    }

                    if (p_mom2 != null)
                    {
                        /*items[11].Color = Color.Black;
                        if (p_mom2.IsControlAlarm)
                        {
                            if (p_mom2.CalculatedValue >= p_mom2.Alarm)
                            {
                                items[11].Color = Color.Red;
                            }
                        }*/
                        items[11].Value = p_mom2.FormattedCaclulatedValue;
                    }

                    if (p_rotor != null)
                    {
                        /*items[12].Color = Color.Black;
                        if (p_rotor.IsControlAlarm)
                        {
                            if (p_rotor.CalculatedValue >= p_rotor.Alarm)
                            {
                                items[12].Color = Color.Red;
                            }
                        }*/
                        items[12].Value = p_rotor.FormattedCaclulatedValue;
                    }

                    if (p_svp != null)
                    {
                        /*items[13].Color = Color.Black;
                        if (p_svp.IsControlAlarm)
                        {
                            if (p_svp.CalculatedValue >= p_svp.Alarm)
                            {
                                items[13].Color = Color.Yellow;
                            }
                            
                        }*/
                        items[13].Value = p_svp.FormattedCaclulatedValue;
                    }

                    items[14].Value = string.Format("{0:F3}", _app.Technology.P0114.Value);
                    items[15].Value = string.Format("{0:F3}", _app.Technology.P0106.Value);

                    items[16].Value = string.Format("{0:F3}", _app.Technology.P0202.Value);

                    if (p_m_svp != null)
                    {
                        /*items[17].Color = Color.Black;
                        if (p_m_svp.IsControlAlarm)
                        {
                            if (p_m_svp.CalculatedValue >= p_m_svp.Alarm)
                            {
                                items[17].Color = Color.Yellow;
                            }
                        }*/
                        items[17].Value = p_m_svp.FormattedCaclulatedValue;
                    }

                    if (p_kmb != null)
                    {
                        /*items[18].Color = Color.Black;
                        if (p_kmb.IsControlAlarm)
                        {
                            if (p_kmb.CalculatedValue >= p_kmb.Alarm)
                            {
                                items[18].Color = Color.Yellow;
                            }
                        }*/
                        items[18].Value = p_kmb.FormattedCaclulatedValue;
                    }

                    panel.Redraw();
                }
            }
        }

        /// <summary>
        /// передать данные
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void manager_OnData(object sender, GraphicEventArgs e)
        {
            try
            {
                if (NShow)
                {
                    if (_app != null)
                    {
                        Slice[] slices = _app.Commutator.GetDataFromBuffer(e.StartTime, e.FinishTime);
                        if (slices != null)
                        {
                            graphic_glybina.Clear();
                            graphic_mehskorost.Clear();

                            graphic_vesnakruke.Clear();
                            graphic_davlenienaman.Clear();

                            graphic_rashodnavhode.Clear();

                            foreach (Slice slice in slices)
                            {
                                if (slice.slice != null)
                                {
                                    int sliceLen = slice.slice.Length;
                                    if (_app.Technology.P0205.SNumber > -1 && _app.Technology.P0205.SNumber < sliceLen)
                                    {
                                        graphic_glybina.Insert(slice._date, slice[_app.Technology.P0205.SNumber]);
                                    }

                                    if (_app.Technology.P0208.SNumber > -1 && _app.Technology.P0208.SNumber < sliceLen)
                                    {
                                        graphic_mehskorost.Insert(slice._date, slice[_app.Technology.P0208.SNumber]);
                                    }

                                    if (_app.Technology.P0102.SNumber > -1 && _app.Technology.P0102.SNumber < sliceLen)
                                    {
                                        graphic_vesnakruke.Insert(slice._date, slice[_app.Technology.P0102.SNumber]);
                                    }

                                    if (_app.Technology.P0004.SNumber > -1 && _app.Technology.P0004.SNumber < sliceLen)
                                    {
                                        graphic_davlenienaman.Insert(slice._date, slice[_app.Technology.P0004.SNumber]);
                                    }

                                    if (_app.Technology.P0114.SNumber > -1 && _app.Technology.P0114.SNumber < sliceLen)
                                    {
                                        graphic_rashodnavhode.Insert(slice._date, slice[_app.Technology.P0114.SNumber]);
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
                    if (_app != null)
                    {
                        Slice[] slices = _app.Commutator.GetDataFromBuffer(manager.StartTime, manager.FinishTime);
                        if (slices != null)
                        {
                            graphic_glybina.Clear();
                            graphic_mehskorost.Clear();

                            graphic_vesnakruke.Clear();
                            graphic_davlenienaman.Clear();

                            graphic_rashodnavhode.Clear();

                            foreach (Slice slice in slices)
                            {
                                if (slice.slice != null)
                                {
                                    int sliceLen = slice.slice.Length;
                                    if (_app.Technology.P0205.SNumber > -1 && _app.Technology.P0205.SNumber < sliceLen)
                                    {
                                        graphic_glybina.Insert(slice._date, slice[_app.Technology.P0205.SNumber]);
                                    }

                                    if (_app.Technology.P0208.SNumber > -1 && _app.Technology.P0208.SNumber < sliceLen)
                                    {
                                        graphic_mehskorost.Insert(slice._date, slice[_app.Technology.P0208.SNumber]);
                                    }

                                    if (_app.Technology.P0102.SNumber > -1 && _app.Technology.P0102.SNumber < sliceLen)
                                    {
                                        graphic_vesnakruke.Insert(slice._date, slice[_app.Technology.P0102.SNumber]);
                                    }

                                    if (_app.Technology.P0004.SNumber > -1 && _app.Technology.P0004.SNumber < sliceLen)
                                    {
                                        graphic_davlenienaman.Insert(slice._date, slice[_app.Technology.P0004.SNumber]);
                                    }

                                    if (_app.Technology.P0114.SNumber > -1 && _app.Technology.P0114.SNumber < sliceLen)
                                    {
                                        graphic_rashodnavhode.Insert(slice._date, slice[_app.Technology.P0114.SNumber]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        // ------------------ сохранение , загрузка ------------------

        /// <summary>
        /// Сохранить настройки панели
        /// </summary>
        /// <param name="doc">Xml документ в который осуществляется сохранение настроек панели</param>
        /// <returns>Сохраненые настройки панели</returns>
        public override XmlNode Save(XmlDocument doc)
        {
            if (slim.TryEnterWriteLock(500))
            {
                try
                {
                    XmlNode root = doc.CreateElement("DrillingPanel");

                    XmlNode graphic_glybinaNode = graphic_glybina.SerializeToXml(doc, "graphic_glybina");
                    XmlNode graphic_mehskorostNode = graphic_mehskorost.SerializeToXml(doc, "graphic_mehskorost");
                    XmlNode graphic_vesnakrukeNode = graphic_vesnakruke.SerializeToXml(doc, "graphic_vesnakruke");
                    XmlNode graphic_davlenienamanNode = graphic_davlenienaman.SerializeToXml(doc, "graphic_davlenienaman");
                    XmlNode graphic_rashodnavhodeNode = graphic_rashodnavhode.SerializeToXml(doc, "graphic_rashodnavhode");

                    XmlNode svpNode = doc.CreateElement("svp");
                    XmlNode m_svpNode = doc.CreateElement("m_svp");

                    XmlNode kmbNode = doc.CreateElement("kmb");
                    XmlNode rotorNode = doc.CreateElement("rotor");

                    XmlNode mom1Node = doc.CreateElement("mom1");
                    XmlNode mom2Node = doc.CreateElement("mom2");

                    XmlNode splitNode = doc.CreateElement("split");

                    /*svpNode.InnerText = svp.PNumber.ToString();
                    m_svpNode.InnerText = m_svp.PNumber.ToString();

                    kmbNode.InnerText = kmb.PNumber.ToString();
                    rotorNode.InnerText = rotor.PNumber.ToString();

                    mom1Node.InnerText = mom1.PNumber.ToString();
                    mom2Node.InnerText = mom2.PNumber.ToString();*/

                    svpNode.InnerText = svp.Identifier.ToString();
                    m_svpNode.InnerText = m_svp.Identifier.ToString();

                    kmbNode.InnerText = kmb.Identifier.ToString();
                    rotorNode.InnerText = rotor.Identifier.ToString();

                    mom1Node.InnerText = mom1.Identifier.ToString();
                    mom2Node.InnerText = mom2.Identifier.ToString();

                    splitNode.InnerText = splitterDistance.ToString();

                    root.AppendChild(graphic_glybinaNode);
                    root.AppendChild(graphic_mehskorostNode);
                    root.AppendChild(graphic_vesnakrukeNode);
                    root.AppendChild(graphic_davlenienamanNode);
                    root.AppendChild(graphic_rashodnavhodeNode);
                    root.AppendChild(svpNode);
                    root.AppendChild(m_svpNode);
                    root.AppendChild(kmbNode);
                    root.AppendChild(rotorNode);
                    root.AppendChild(mom1Node);
                    root.AppendChild(mom2Node);
                    root.AppendChild(splitNode);

                    XmlNode n_opts = panel.Save(doc);
                    if (n_opts != null)
                    {
                        root.AppendChild(n_opts);
                    }

                    return root;
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
            
            return null;
        }

        private Graphic gr_glybina = null;
        private Graphic gr_mehskorost = null;
        private Graphic gr_vesnakruke = null;
        private Graphic gr_davlenienaman = null;
        private Graphic gr_rashodnavhode = null;

        XmlNode n_panelNode = null;

        /// <summary>
        /// Загрузить настройки панели
        /// </summary>
        /// <param name="Root">Корневой узел в котром находятся настройки панели</param>
        public override void Load(XmlNode Root)
        {
            if (slim.TryEnterWriteLock(500))
            {
                try
                {
                    if (Root != null && Root.Name == "DrillingPanel")
                    {
                        if (Root.HasChildNodes)
                        {
                            loaded = true;
                            foreach (XmlNode child in Root.ChildNodes)
                            {
                                switch (child.Name)
                                {
                                    case "numericPanel":

                                        try
                                        {
                                            n_panelNode = child.Clone();
                                        }
                                        catch { }
                                        break;

                                    case "graphic_glybina":

                                        try
                                        {
                                            gr_glybina = new Graphic();
                                            gr_glybina.DeSerializeToXml(child);
                                        }
                                        catch { }
                                        break;

                                    case "graphic_mehskorost":

                                        try
                                        {
                                            gr_mehskorost = new Graphic();
                                            gr_mehskorost.DeSerializeToXml(child);
                                        }
                                        catch { }
                                        break;

                                    case "graphic_vesnakruke":

                                        try
                                        {
                                            gr_vesnakruke = new Graphic();
                                            gr_vesnakruke.DeSerializeToXml(child);
                                        }
                                        catch { }
                                        break;

                                    case "graphic_davlenienaman":

                                        try
                                        {
                                            gr_davlenienaman = new Graphic();
                                            gr_davlenienaman.DeSerializeToXml(child);
                                        }
                                        catch { }
                                        break;

                                    case "graphic_rashodnavhode":

                                        try
                                        {
                                            gr_rashodnavhode = new Graphic();
                                            gr_rashodnavhode.DeSerializeToXml(child);
                                        }
                                        catch { }
                                        break;

                                    case "svp":

                                        try
                                        {
                                            svp.Identifier = new Guid(child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case "m_svp":

                                        try
                                        {
                                            m_svp.Identifier = new Guid(child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case "kmb":

                                        try
                                        {
                                            kmb.Identifier = new Guid(child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case "rotor":

                                        try
                                        {
                                            rotor.Identifier = new Guid(child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case "mom1":

                                        try
                                        {
                                            mom1.Identifier = new Guid(child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case "mom2":

                                        try
                                        {
                                            mom2.Identifier = new Guid(child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case "split":

                                        try
                                        {
                                            splitterDistance = float.Parse(child.InnerText);
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