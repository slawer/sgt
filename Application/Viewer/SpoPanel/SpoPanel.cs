using System;
using System.Xml;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

using Buffering;
using NumericTable;
using GraphicComponent;

namespace SGT
{
    /// <summary>
    /// Реализует панель СПО
    /// </summary>
    public class SpoPanel : VPanel
    {
        private SetLabelValue setter = null;         // выполняет отображение данных

        protected GraphicManager manager1 = null;    // управляет отрисовкой параметров группы 1
        protected GraphicManager manager2 = null;    // управляет отрисовкой параметров группы 2

        // ----- первая группа графиков -----

        protected Graphic graphic_talblok;          // отображаемый график положение талевого блока
        protected Graphic graphic_glybina;          // отображаемый график глубина инструмента
        protected Graphic graphic_skorostSpo;       // отображаемый график скорость СПО

        // ----- вторая группа графиков -----

        protected Graphic graphic_gasnavihode;      // отображаемый график газ на выходе
        protected Graphic graphic_gasnaplosadke;    // отображаемый график газ на площадке
        protected Graphic graphic_gaspodrotorom;    // отображаемый график газ под ротором
        protected Graphic graphic_gaspriemnaemk;    // отображаемый график газ приемная емкость
        protected Graphic graphic_gasvibrosit;      // отображаемый график газ вибросит

        // ----- текстовый вывод -----

        protected Label talblok;              // Положение талевого блока
        protected Label ves;                  // Вес на крюке

        protected Label glybina;              // Глубина инструмента
        protected Label skorost;              // Скорость СПО

        protected Label nadZaboem;            // Над забоем
        protected Label gasNavihode;          // Газ на выходе

        protected Label gasNaplosandke;       // Газ на площадке
        protected Label gasPodrotorom;        // Газ под ротором

        protected Label gasPriemna;           // Газ приемная емкость
        protected Label gasVibrosit;          // Газ вибросит

        protected Boolean loaded = false;     // загруженны настройки или нет
        protected long initialized = 0;       // инициализирована панель или нет

        protected float splitterDistance = 271;       // геометрия панели

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="app">Контекст в котором работает панель</param>
        public SpoPanel(SgtApplication app)
            : base("Панель СПО", VPanelType.PanelSpo)
        {
            _app = app;
            setter = new SetLabelValue(setLabelValue);
            
            app.Technology.onComplete += new EventHandler(Technology_onComplete);
        }

        /// <summary>
        /// Положение талевого блока
        /// </summary>
        public Graphic GraphicTalblok
        {
            get
            {
                return graphic_talblok;
            }
        }

        /// <summary>
        /// Глубина инструмента
        /// </summary>
        public Graphic GraphicGlybina
        {
            get 
            {
                return graphic_glybina;
            }
        }

        /// <summary>
        /// скорость СПО
        /// </summary>
        public Graphic GraphicSkorostSpo
        {
            get 
            {
                return graphic_skorostSpo;
            }

        }

        /// <summary>
        /// Газ на выходе
        /// </summary>
        public Graphic GraphicGasnavihode
        {
            get 
            {
                return graphic_gasnavihode;
            }

        }

        /// <summary>
        /// Газ на площадке
        /// </summary>
        public Graphic GraphicGasnaplosadke
        {
            get 
            {
                return graphic_gasnaplosadke;
            }

        }

        /// <summary>
        /// Газ под ротором
        /// </summary>
        public Graphic GraphicGaspodrotorom
        {
            get 
            {
                return graphic_gaspodrotorom;
            }

        }

        /// <summary>
        /// Газ приемная емкость
        /// </summary>
        public Graphic GraphicGaspriemnaemk
        {
            get 
            {
                return graphic_gaspriemnaemk;
            }

        }

        /// <summary>
        /// Газ вибросит
        /// </summary>
        public Graphic GraphicGasvibrosit
        {
            get 
            {
                return graphic_gasvibrosit;
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
        /// <param name="_manager1">Графическая панель для первой группы графиков</param>
        /// <param name="_manager2">Графическая панель для второй группы графиков</param>
        /// <param name="talblok">Положение талевого блока</param>
        /// <param name="ves">Вес на крюке</param>
        /// <param name="glybina">Глубина инструмента</param>
        /// <param name="skorost">Скорость СПО</param>
        /// <param name="nadZaboem">Над забоем</param>
        /// <param name="gasNavihode">Газ на выходе</param>
        /// <param name="gasNaplosandke">Газ на площадке</param>
        /// <param name="gasPodrotorom">Газ под ротором</param>
        /// <param name="gasPriemna">Газ приемная емкость</param>
        /// <param name="gasVibrosit">Газ вибросит</param>
        public void init(GraphicManager _manager1, GraphicManager _manager2, Label _talblok, Label _ves,
            Label _glybina, Label _skorost, Label _nadZaboem, Label _gasNavihode, Label _gasNaplosandke,
            Label _gasPodrotorom, Label _gasPriemna, Label _gasVibrosit)
        {
            if (Interlocked.Read(ref initialized) == 0)
            {
                manager1 = _manager1;
                manager2 = _manager2;

                talblok = _talblok;
                ves = _ves;

                glybina = _glybina;
                skorost = _skorost;

                nadZaboem = _nadZaboem;
                gasNavihode = _gasNavihode;

                gasNaplosandke = _gasNaplosandke;
                gasPodrotorom = _gasPodrotorom;

                gasPriemna = _gasPriemna;
                gasVibrosit = _gasVibrosit;

                InitializeFGraphics();
                InitializeSGraphics();

                Interlocked.Exchange(ref initialized, 1);
            }
        }

        /// <summary>
        /// выполнить инициализацию первой группы графиков
        /// </summary>
        protected void InitializeFGraphics()
        {
            if (manager1 != null)
            {
                graphic_talblok = manager1.InstanceGraphic();

                if (loaded == false)
                {
                    graphic_talblok.Units = "м";
                    graphic_talblok.Description = "Т/блок";

                    graphic_talblok.Range.Min = 0;
                    graphic_talblok.Range.Max = 45;

                    graphic_talblok.Color = Color.Red;
                }
                else
                    if (gr_talblok != null)
                    {
                        graphic_talblok.Units = gr_talblok.Units;
                        graphic_talblok.Description = gr_talblok.Description;

                        graphic_talblok.Range.Min = gr_talblok.Range.Min;
                        graphic_talblok.Range.Max = gr_talblok.Range.Max;

                        graphic_talblok.Color = gr_talblok.Color;

                        gr_talblok.Font.Dispose();
                        gr_talblok.Brush.Dispose();

                        gr_talblok = null;
                    }

                graphic_glybina = manager1.InstanceGraphic();
                if (loaded == false)
                {
                    graphic_glybina.Units = "м";
                    graphic_glybina.Description = "Гл.Инст";

                    graphic_glybina.Range.Min = 0;
                    graphic_glybina.Range.Max = 6500;

                    graphic_glybina.Color = Color.Green;
                }
                else
                    if (gr_glybina != null)
                    {
                        graphic_glybina.Units = gr_glybina.Units;
                        graphic_glybina.Description = gr_glybina.Description;

                        graphic_glybina.Range.Min = gr_glybina.Range.Min;
                        graphic_glybina.Range.Max = gr_glybina.Range.Max;

                        graphic_glybina.Color = gr_glybina.Color;

                        gr_glybina.Font.Dispose();
                        gr_glybina.Brush.Dispose();

                        gr_glybina = null;
                    }

                graphic_skorostSpo = manager1.InstanceGraphic();
                if (loaded == false)
                {
                    graphic_skorostSpo.Units = "м/с";
                    graphic_skorostSpo.Description = "Ск.СПО";

                    graphic_skorostSpo.Range.Min = -2;
                    graphic_skorostSpo.Range.Max = 2;

                    graphic_skorostSpo.Color = Color.Blue;
                }
                else
                    if (gr_skorostSpo != null)
                    {
                        graphic_skorostSpo.Units = gr_skorostSpo.Units;
                        graphic_skorostSpo.Description = gr_skorostSpo.Description;

                        graphic_skorostSpo.Range.Min = gr_skorostSpo.Range.Min;
                        graphic_skorostSpo.Range.Max = gr_skorostSpo.Range.Max;

                        graphic_skorostSpo.Color = gr_skorostSpo.Color;

                        gr_skorostSpo.Font.Dispose();
                        gr_skorostSpo.Brush.Dispose();

                        gr_skorostSpo = null;
                    }

                manager1.StartTime = DateTime.Now;
                manager1.Orientation = GraphicComponent.Orientation.Vertical;

                manager1.Update();
                manager1.UpdatePeriod = 1000;
                manager1.Mode = GraphicComponent.DrawMode.Activ;

                manager1.OnDataNeed += new EventHandler(manager1_OnData);
            }
        }

        /// <summary>
        /// выполнить инициализацию второй группы графиков
        /// </summary>
        protected void InitializeSGraphics()
        {
            graphic_gasnavihode = manager2.InstanceGraphic();
            if (loaded == false)
            {
                graphic_gasnavihode.Units = "м";
                graphic_gasnavihode.Description = "Газ1";

                graphic_gasnavihode.Range.Min = 0;
                graphic_gasnavihode.Range.Max = 50;

                graphic_gasnavihode.Color = Color.Firebrick;
            }
            else
                if (gr_gasnavihode != null)
                {
                    graphic_gasnavihode.Units = gr_gasnavihode.Units;
                    graphic_gasnavihode.Description = gr_gasnavihode.Description;

                    graphic_gasnavihode.Range.Min = gr_gasnavihode.Range.Min;
                    graphic_gasnavihode.Range.Max = gr_gasnavihode.Range.Max;

                    graphic_gasnavihode.Color = gr_gasnavihode.Color;

                    gr_gasnavihode.Font.Dispose();
                    gr_gasnavihode.Brush.Dispose();

                    gr_gasnavihode = null;
                }

            graphic_gasnaplosadke = manager2.InstanceGraphic();
            if (loaded == false)
            {
                graphic_gasnaplosadke.Units = "м";
                graphic_gasnaplosadke.Description = "Газ2";

                graphic_gasnaplosadke.Range.Min = 0;
                graphic_gasnaplosadke.Range.Max = 50;

                graphic_gasnaplosadke.Color = Color.SeaGreen;
            }
            else
                if (gr_gasnaplosadke != null)
                {
                    graphic_gasnaplosadke.Units = gr_gasnaplosadke.Units;
                    graphic_gasnaplosadke.Description = gr_gasnaplosadke.Description;

                    graphic_gasnaplosadke.Range.Min = gr_gasnaplosadke.Range.Min;
                    graphic_gasnaplosadke.Range.Max = gr_gasnaplosadke.Range.Max;

                    graphic_gasnaplosadke.Color = gr_gasnaplosadke.Color;

                    gr_gasnaplosadke.Font.Dispose();
                    gr_gasnaplosadke.Brush.Dispose();

                    gr_gasnaplosadke = null;
                }

            graphic_gaspodrotorom = manager2.InstanceGraphic();
            if (loaded == false)
            {
                graphic_gaspodrotorom.Units = "м";
                graphic_gaspodrotorom.Description = "Газ3";

                graphic_gaspodrotorom.Range.Min = 0;
                graphic_gaspodrotorom.Range.Max = 50;

                graphic_gaspodrotorom.Color = Color.Blue;
            }
            else
                if (gr_gaspodrotorom != null)
                {
                    graphic_gaspodrotorom.Units = gr_gaspodrotorom.Units;
                    graphic_gaspodrotorom.Description = gr_gaspodrotorom.Description;

                    graphic_gaspodrotorom.Range.Min = gr_gaspodrotorom.Range.Min;
                    graphic_gaspodrotorom.Range.Max = gr_gaspodrotorom.Range.Max;

                    graphic_gaspodrotorom.Color = gr_gaspodrotorom.Color;

                    gr_gaspodrotorom.Font.Dispose();
                    gr_gaspodrotorom.Brush.Dispose();

                    gr_gaspodrotorom = null;
                }

            graphic_gaspriemnaemk = manager2.InstanceGraphic();
            if (loaded == false)
            {
                graphic_gaspriemnaemk.Units = "м";
                graphic_gaspriemnaemk.Description = "Газ4";

                graphic_gaspriemnaemk.Range.Min = 0;
                graphic_gaspriemnaemk.Range.Max = 50;

                graphic_gaspriemnaemk.Color = Color.SaddleBrown;
            }
            else
                if (gr_gaspriemnaemk != null)
                {
                    graphic_gaspriemnaemk.Units = gr_gaspriemnaemk.Units;
                    graphic_gaspriemnaemk.Description = gr_gaspriemnaemk.Description;

                    graphic_gaspriemnaemk.Range.Min = gr_gaspriemnaemk.Range.Min;
                    graphic_gaspriemnaemk.Range.Max = gr_gaspriemnaemk.Range.Max;

                    graphic_gaspriemnaemk.Color = gr_gaspriemnaemk.Color;

                    gr_gaspriemnaemk.Font.Dispose();
                    gr_gaspriemnaemk.Brush.Dispose();

                    gr_gaspriemnaemk = null;
                }

            graphic_gasvibrosit = manager2.InstanceGraphic();
            if (loaded == false)
            {
                graphic_gasvibrosit.Units = "м";
                graphic_gasvibrosit.Description = "Газ5";

                graphic_gasvibrosit.Range.Min = 0;
                graphic_gasvibrosit.Range.Max = 50;

                graphic_gasvibrosit.Color = Color.Black;
            }
            else
                if (gr_gasvibrosit != null)
                {
                    graphic_gasvibrosit.Units = gr_gasvibrosit.Units;
                    graphic_gasvibrosit.Description = gr_gasvibrosit.Description;

                    graphic_gasvibrosit.Range.Min = gr_gasvibrosit.Range.Min;
                    graphic_gasvibrosit.Range.Max = gr_gasvibrosit.Range.Max;

                    graphic_gasvibrosit.Color = gr_gasvibrosit.Color;

                    gr_gasvibrosit.Font.Dispose();
                    gr_gasvibrosit.Brush.Dispose();

                    gr_gasvibrosit = null;
                }

            manager2.StartTime = DateTime.Now;
            manager2.Orientation = GraphicComponent.Orientation.Vertical;

            manager2.Update();
            manager2.UpdatePeriod = 1000;
            manager2.Mode = GraphicComponent.DrawMode.Activ;

            manager2.OnDataNeed += new EventHandler(manager2_OnData);
        }

        // ------------------------- обработчики событий -------------------------

        /// <summary>
        /// Определяет интерфейс функции вывода данных
        /// </summary>
        /// <param name="label">Элемент управление на который выводить значение параметра</param>
        /// <param name="Value">Выводимое значение параметра</param>
        private delegate void SetLabelValue(Label label, Single Value);

        /// <summary>
        /// Вывести значение параметра
        /// </summary>
        /// <param name="label">Элемент управление на который выводить значение параметра</param>
        /// <param name="Value">Выводимое значение параметра</param>
        private void setLabelValue(Label label, Single Value)
        {
            if (float.IsNaN(Value) == false)
            {
                label.BackColor = SystemColors.Control;
                label.Text = string.Format("{0:F3}", Value);
            }
            else
            {
                label.Text = "ОТКЛ";
                label.BackColor = Color.Salmon;
            }
        }

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
                        talblok.BeginInvoke(setter, talblok, _app.Technology.P0005.Value);
                        ves.BeginInvoke(setter, ves, _app.Technology.P0102.Value);

                        glybina.BeginInvoke(setter, glybina, _app.Technology.P0204.Value);
                        skorost.BeginInvoke(setter, skorost, _app.Technology.P0210.Value);

                        nadZaboem.BeginInvoke(setter, nadZaboem, _app.Technology.P0211.Value);
                        gasNavihode.BeginInvoke(setter, gasNavihode, _app.Technology.P0006.Value);

                        gasNaplosandke.BeginInvoke(setter, gasNaplosandke, _app.Technology.P06_1.Value);
                        gasPodrotorom.BeginInvoke(setter, gasPodrotorom, _app.Technology.P06_2.Value);

                        gasPriemna.BeginInvoke(setter, gasPriemna, _app.Technology.P06_3.Value);
                        gasVibrosit.BeginInvoke(setter, gasVibrosit, _app.Technology.P06_4.Value);
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// передать данные
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void manager1_OnData(object sender, EventArgs e)
        {
            try
            {
                if (NShow)
                {
                    if (_app != null)
                    {
                        Slice[] slices = _app.Commutator.GetDataFromBuffer(manager1.StartTime, manager1.FinishTime);
                        if (slices != null)
                        {
                            graphic_talblok.Clear();
                            graphic_glybina.Clear();
                            graphic_skorostSpo.Clear();

                            foreach (Slice slice in slices)
                            {
                                if (slice.slice != null)
                                {
                                    int sliceLen = slice.slice.Length;
                                    if (_app.Technology.P0005.SNumber > -1 && _app.Technology.P0005.SNumber < sliceLen)
                                    {
                                        graphic_talblok.Insert(slice._date, slice[_app.Technology.P0005.SNumber]);
                                    }

                                    if (_app.Technology.P0204.SNumber > -1 && _app.Technology.P0204.SNumber < sliceLen)
                                    {
                                        graphic_glybina.Insert(slice._date, slice[_app.Technology.P0204.SNumber]);
                                    }

                                    if (_app.Technology.P0210.SNumber > -1 && _app.Technology.P0210.SNumber < sliceLen)
                                    {
                                        graphic_skorostSpo.Insert(slice._date, slice[_app.Technology.P0210.SNumber]);
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
        /// передать данные
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void manager2_OnData(object sender, EventArgs e)
        {
            try
            {
                if (NShow)
                {
                    if (_app != null)
                    {
                        Slice[] slices = _app.Commutator.GetDataFromBuffer(manager2.StartTime, manager2.FinishTime);
                        if (slices != null)
                        {
                            graphic_gasnavihode.Clear();
                            graphic_gasnaplosadke.Clear();
                            graphic_gaspodrotorom.Clear();
                            graphic_gaspriemnaemk.Clear();
                            graphic_gasvibrosit.Clear();

                            foreach (Slice slice in slices)
                            {
                                if (slice.slice != null)
                                {
                                    int sliceLen = slice.slice.Length;
                                    if (_app.Technology.P0006.SNumber > -1 && _app.Technology.P0006.SNumber < sliceLen)
                                    {
                                        graphic_gasnavihode.Insert(slice._date, slice[_app.Technology.P0006.SNumber]);
                                    }

                                    if (_app.Technology.P06_1.SNumber > -1 && _app.Technology.P06_1.SNumber < sliceLen)
                                    {
                                        graphic_gasnaplosadke.Insert(slice._date, slice[_app.Technology.P06_1.SNumber]);
                                    }

                                    if (_app.Technology.P06_2.SNumber > -1 && _app.Technology.P06_2.SNumber < sliceLen)
                                    {
                                        graphic_gaspodrotorom.Insert(slice._date, slice[_app.Technology.P06_2.SNumber]);
                                    }

                                    if (_app.Technology.P06_3.SNumber > -1 && _app.Technology.P06_3.SNumber < sliceLen)
                                    {
                                        graphic_gaspriemnaemk.Insert(slice._date, slice[_app.Technology.P06_3.SNumber]);
                                    }

                                    if (_app.Technology.P06_4.SNumber > -1 && _app.Technology.P06_4.SNumber < sliceLen)
                                    {
                                        graphic_gasvibrosit.Insert(slice._date, slice[_app.Technology.P06_4.SNumber]);
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
                    XmlNode root = doc.CreateElement("SpoPanel");

                    XmlNode graphic_talblokNode = graphic_talblok.SerializeToXml(doc, "graphic_talblok");
                    XmlNode graphic_glybinaNode = graphic_glybina.SerializeToXml(doc, "graphic_glybina");
                    XmlNode graphic_skorostSpoNode = graphic_skorostSpo.SerializeToXml(doc, "graphic_skorostSpo");

                    XmlNode graphic_gasnavihodeNode = graphic_gasnavihode.SerializeToXml(doc, "graphic_gasnavihode");
                    XmlNode graphic_gasnaplosadkeNode = graphic_gasnaplosadke.SerializeToXml(doc, "graphic_gasnaplosadke");
                    XmlNode graphic_gaspodrotoromNode = graphic_gaspodrotorom.SerializeToXml(doc, "graphic_gaspodrotorom");
                    XmlNode graphic_gaspriemnaemkNode = graphic_gaspriemnaemk.SerializeToXml(doc, "graphic_gaspriemnaemk");
                    XmlNode graphic_gasvibrositNode = graphic_gasvibrosit.SerializeToXml(doc, "graphic_gasvibrosit");

                    XmlNode splitNode = doc.CreateElement("split");
                    splitNode.InnerText = splitterDistance.ToString();

                    root.AppendChild(graphic_talblokNode);
                    root.AppendChild(graphic_glybinaNode);
                    root.AppendChild(graphic_skorostSpoNode);
                    root.AppendChild(graphic_gasnavihodeNode);
                    root.AppendChild(graphic_gasnaplosadkeNode);
                    root.AppendChild(graphic_gaspodrotoromNode);
                    root.AppendChild(graphic_gaspriemnaemkNode);
                    root.AppendChild(graphic_gasvibrositNode);
                    root.AppendChild(splitNode);

                    return root;
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }

            return null;
        }

        protected Graphic gr_talblok;          // отображаемый график положение талевого блока
        protected Graphic gr_glybina;          // отображаемый график глубина инструмента
        protected Graphic gr_skorostSpo;       // отображаемый график скорость СПО

        protected Graphic gr_gasnavihode;      // отображаемый график газ на выходе
        protected Graphic gr_gasnaplosadke;    // отображаемый график газ на площадке
        protected Graphic gr_gaspodrotorom;    // отображаемый график газ под ротором
        protected Graphic gr_gaspriemnaemk;    // отображаемый график газ приемная емкость
        protected Graphic gr_gasvibrosit;      // отображаемый график газ вибросит

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
                    if (Root != null && Root.Name == "SpoPanel")
                    {
                        if (Root.HasChildNodes)
                        {
                            loaded = true;
                            foreach (XmlNode child in Root.ChildNodes)
                            {
                                switch (child.Name)
                                {
                                    case "graphic_talblok":
                                        
                                        try
                                        {
                                            gr_talblok = new Graphic();
                                            gr_talblok.DeSerializeToXml(child);
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

                                    case "graphic_skorostSpo":
                                        
                                        try
                                        {
                                            gr_skorostSpo = new Graphic();
                                            gr_skorostSpo.DeSerializeToXml(child);
                                        }
                                        catch { }
                                        break;

                                    case "graphic_gasnavihode":
                                        
                                        try
                                        {
                                            gr_gasnavihode = new Graphic();
                                            gr_gasnavihode.DeSerializeToXml(child);
                                        }
                                        catch { }
                                        break;

                                    case "graphic_gasnaplosadke":
                                        
                                        try
                                        {
                                            gr_gasnaplosadke = new Graphic();
                                            gr_gasnaplosadke.DeSerializeToXml(child);
                                        }
                                        catch { }
                                        break;

                                    case "graphic_gaspodrotorom":
                                        
                                        try
                                        {
                                            gr_gaspodrotorom = new Graphic();
                                            gr_gaspodrotorom.DeSerializeToXml(child);
                                        }
                                        catch { }
                                        break;

                                    case "graphic_gaspriemnaemk":
                                        
                                        try
                                        {
                                            gr_gaspriemnaemk = new Graphic();
                                            gr_gaspriemnaemk.DeSerializeToXml(child);
                                        }
                                        catch { }
                                        break;

                                    case "graphic_gasvibrosit":
                                        
                                        try
                                        {
                                            gr_gasvibrosit = new Graphic();
                                            gr_gasvibrosit.DeSerializeToXml(child);
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