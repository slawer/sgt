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
    /// Реализует панель Параметры бурового раствора
    /// </summary>
    public class SolutionPanel : VPanel
    {
        protected GraphicManager manager1 = null;   // управляет отрисовкой графиков первой группы
        protected GraphicManager manager2 = null;   // управляет отрисовкой графиков второй группы

        // ------------ текстовый вывод ------------

        protected TextBox _p1;              // Водяная емкость
        protected TextBox _p2;              // Доливная емкость
        protected TextBox _p3;              // Приемная емкость
        protected TextBox _p4;              // Блока очистки
        protected TextBox _p5;              // Промежуточная
        protected TextBox _p6;              // Емкость приготовления отсек1
        protected TextBox _p7;              // Емкость приготовления отсек2
        protected TextBox _p8;              // Приток/Потери бурового раствора
        protected TextBox _p9;              // Суммарные объем в емкостях
        protected TextBox _p10;             // Плотность приемная емкость
        protected TextBox _p11;             // Плотность блока очистки
        protected TextBox _p12;             // Плотность емкости приготовления отсек2
        protected TextBox _p13;             // Температура на выходе
        protected TextBox _p14;             // Температура на входе
        protected TextBox _p15;             // Ходы насоса 1
        protected TextBox _p16;             // Ходы насоса 2
        protected TextBox _p17;             // Поток на выходе
        protected TextBox _p18;             // Изменение потока на выходе

        // ------------ графический вывод ------------

        protected Graphic graphic_plPriemna;        // отображаемый график плотность приемная емкость
        protected Graphic graphic_plBlokaOchi;      // отображаемый график плотность блока очистки
        protected Graphic graphic_plEmnsek2;        // отображаемый график плотность емкости приготовления отсек2
        
        protected Graphic graphic_summObem;         // отображаемый график суммарный объем в емкостях
        protected Graphic graphic_hodi1;            // отображаемый график ходы насоса 1
        protected Graphic graphic_hodi2;            // отображаемый график ходы насоса 2
        protected Graphic graphic_potok;            // отображаемый график поток на выходе

        protected VPanelParameter plPriemna;        // источник данных для Плотность приемная емкость
        protected VPanelParameter plBlocka;         // источник данных для Плотность блока очистки

        protected VPanelParameter plEmkOts2;        // источник данных для Плотность емкости приготовления отсек2
        protected VPanelParameter tempVihod;        // источник данных для Температура на выходе

        protected VPanelParameter temVhod;          // источник данных для Температура на входе
        protected Boolean loaded = false;           // загружены настройки или нет

        protected long initialized = 0;             // инициализирована панель или нет
        protected float splitterDistance = 321;     // геометрия панели

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="app">Контекст в котором работает панель</param>
        public SolutionPanel(SgtApplication app)
            : base("Параметры бурового раствора", VPanelType.SolutionPanel)
        {
            _app = app;
            setter = new SetterValue(setterValue);

            plPriemna = new VPanelParameter();
            plBlocka = new VPanelParameter();

            plEmkOts2 = new VPanelParameter();
            tempVihod = new VPanelParameter();

            temVhod = new VPanelParameter();

            app.Technology.onComplete += new EventHandler(Technology_onComplete);
        }

        /// <summary>
        /// Плотность приемная емкость
        /// </summary>
        public VPanelParameter PlPriemna
        {
            get
            {
                return plPriemna;
            }
        }

        /// <summary>
        /// Плотность блока очистки
        /// </summary>
        public VPanelParameter PlBlocka
        {
            get
            {
                return plBlocka;
            }
        }

        /// <summary>
        /// Плотность емкости приготовления отсек2
        /// </summary>
        public VPanelParameter PlEmkOts2
        {
            get
            {
                return plEmkOts2;
            }
        }

        /// <summary>
        /// Температура на выходе
        /// </summary>
        public VPanelParameter TempVihod
        {
            get
            {
                return tempVihod;
            }
        }

        /// <summary>
        /// Температура на входе
        /// </summary>
        public VPanelParameter TemVhod
        {
            get
            {
                return temVhod;
            }
        }

        /// <summary>
        /// плотность приемная емкость
        /// </summary>
        public Graphic GraphicplPriemna
        {
            get
            {
                return graphic_plPriemna;
            }
        }

        /// <summary>
        /// плотность блока очистки
        /// </summary>
        public Graphic GraphicplBlokaOchi
        {
            get
            {
                return graphic_plBlokaOchi;
            }
        }

        /// <summary>
        /// плотность емкости приготовления отсек2
        /// </summary>
        public Graphic GraphicplEmnsek2
        {
            get
            {
                return graphic_plEmnsek2;
            }
        }

        /// <summary>
        /// суммарный объем в емкостях
        /// </summary>
        public Graphic GraphicsummObem
        {
            get
            {
                return graphic_summObem;
            }
        }

        /// <summary>
        /// ходы насоса 1
        /// </summary>
        public Graphic Graphichodi1
        {
            get
            {
                return graphic_hodi1;
            }
        }

        /// <summary>
        /// ходы насоса 2
        /// </summary>
        public Graphic Graphichodi2
        {
            get
            {
                return graphic_hodi2;
            }
        }

        /// <summary>
        /// поток на выходе
        /// </summary>
        public Graphic Graphicpotok
        {
            get
            {
                return graphic_potok;
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

                return 321;
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
        /// <param name="_manager1">Управляющий графическим компонентом отображающим первую группу графиков</param>
        /// <param name="_manager2">Управляющий графическим компонентом отображающим вторую группу графиков</param>
        /// <param name="p1">Водяная емкость</param>
        /// <param name="p2">Доливная емкость</param>
        /// <param name="p3">Приемная емкость</param>
        /// <param name="p4">Блока очистки</param>
        /// <param name="p5">Промежуточная</param>
        /// <param name="p6">Емкость приготовления отсек1</param>
        /// <param name="p7">Емкость приготовления отсек2</param>
        /// <param name="p8">Приток/Потери бурового раствора</param>
        /// <param name="p9">Суммарные объем в емкостях</param>
        /// <param name="p10">Плотность приемная емкость</param>
        /// <param name="p11">Плотность блока очистки</param>
        /// <param name="p12">Плотность емкости приготовления отсек2</param>
        /// <param name="p13">Температура на выходе</param>
        /// <param name="p14">Температура на входе</param>
        /// <param name="p15">Ходы насоса 1</param>
        /// <param name="p16">Ходы насоса 2</param>
        /// <param name="p17">Поток на выходе</param>
        /// <param name="p18">Изменение потока на выходе</param>
        public void init(GraphicManager _manager1, GraphicManager _manager2, TextBox p1, TextBox p2, TextBox p3, TextBox p4, TextBox p5,
            TextBox p6, TextBox p7, TextBox p8, TextBox p9, TextBox p10, TextBox p11, TextBox p12, TextBox p13,
            TextBox p14, TextBox p15, TextBox p16, TextBox p17, TextBox p18)
        {
            if (Interlocked.Read(ref initialized) == 0)
            {
                manager1 = _manager1;
                manager2 = _manager2;

                _p1 = p1;
                _p2 = p2;
                _p3 = p3;
                _p4 = p4;
                _p5 = p5;
                _p6 = p6;
                _p7 = p7;
                _p8 = p8;
                _p9 = p9;
                _p10 = p10;
                _p11 = p11;
                _p12 = p12;
                _p13 = p13;
                _p14 = p14;
                _p15 = p15;
                _p16 = p16;
                _p17 = p17;
                _p18 = p18;

                InitializeFGraphicPanel();
                InitializeSGraphicPanel();

                Interlocked.Exchange(ref initialized, 1);
            }
        }

        protected SetterValue setter = null;            // осуществляет вывод данных в текстовое поле

        /// <summary>
        /// Осуществляет вывод данных в текстовое поле
        /// </summary>
        /// <param name="box">Текстовое поле в которое необходимо вывести данные</param>
        /// <param name="value">Значение, которое необходимо вывести в текстовое поле</param>
        protected void setterValue(TextBox box, Single value)
        {
            if (float.IsNaN(value) == false)
            {
                box.Text = string.Format("{0:F3}", value);
                box.BackColor = SystemColors.Control;
            }
            else
            {
                box.BackColor = Color.Salmon;
                box.Text = "ОТКЛ";
            }
        }

        /// <summary>
        /// Интерфейс функции , выполняющей вывод данных в текстовое поле
        /// </summary>
        /// <param name="box">Текстовое поле в которое необходимо вывести данные</param>
        /// <param name="value">Значение, которое необходимо вывести в текстовое поле</param>
        protected delegate void SetterValue(TextBox box, Single value);

        // ------------------------ вспомогательные функции ------------------------

        /// <summary>
        /// Инициализировать графическую панель
        /// </summary>
        protected void InitializeFGraphicPanel()
        {
            graphic_plPriemna = manager1.InstanceGraphic();        // график плотность приемная емкость
            if (loaded == false)
            {
                graphic_plPriemna.Units = "г/см3";
                graphic_plPriemna.Description = "Плот1";

                graphic_plPriemna.Range.Min = 0.8f;
                graphic_plPriemna.Range.Max = 2.2f;

                graphic_plPriemna.Color = Color.Red;
            }
            else
                if (gr_plPriemna != null)
                {
                    graphic_plPriemna.Units = "г/см3";
                    graphic_plPriemna.Description = "Плот1";

                    graphic_plPriemna.Range.Min = gr_plPriemna.Range.Min;
                    graphic_plPriemna.Range.Max = gr_plPriemna.Range.Max;

                    graphic_plPriemna.Color = gr_plPriemna.Color;

                    gr_plPriemna.Font.Dispose();
                    gr_plPriemna.Brush.Dispose();

                    gr_plPriemna = null;
                }
            
            graphic_plBlokaOchi = manager1.InstanceGraphic();      // график плотность блока очистки
            if (loaded == false)
            {
                graphic_plBlokaOchi.Units = "г/см3";
                graphic_plBlokaOchi.Description = "Плот2";

                graphic_plBlokaOchi.Range.Min = 0.8f;
                graphic_plBlokaOchi.Range.Max = 2.2f;

                graphic_plBlokaOchi.Color = Color.Green;
            }
            else
                if (gr_plBlokaOchi != null)
                {
                    graphic_plBlokaOchi.Units = "г/см3";
                    graphic_plBlokaOchi.Description = "Плот2";

                    graphic_plBlokaOchi.Range.Min = gr_plBlokaOchi.Range.Min;
                    graphic_plBlokaOchi.Range.Max = gr_plBlokaOchi.Range.Max;

                    graphic_plBlokaOchi.Color = gr_plBlokaOchi.Color;

                    gr_plBlokaOchi.Font.Dispose();
                    gr_plBlokaOchi.Brush.Dispose();

                    gr_plBlokaOchi = null;
                }

            graphic_plEmnsek2 = manager1.InstanceGraphic();        // график плотность емкости приготовления отсек2
            if (loaded == false)
            {
                graphic_plEmnsek2.Units = "г/см3";
                graphic_plEmnsek2.Description = "Плот3";

                graphic_plEmnsek2.Range.Min = 0.8f;
                graphic_plEmnsek2.Range.Max = 2.2f;

                graphic_plEmnsek2.Color = Color.Blue;
            }
            else
                if (gr_plEmnsek2 != null)
                {
                    graphic_plEmnsek2.Units = "г/см3";
                    graphic_plEmnsek2.Description = "Плот3";

                    graphic_plEmnsek2.Range.Min = gr_plEmnsek2.Range.Min;
                    graphic_plEmnsek2.Range.Max = gr_plEmnsek2.Range.Max;

                    graphic_plEmnsek2.Color = gr_plEmnsek2.Color;

                    gr_plEmnsek2.Font.Dispose();
                    gr_plEmnsek2.Brush.Dispose();

                    gr_plEmnsek2 = null;
                }

            manager1.StartTime = DateTime.Now;
            manager1.Orientation = GraphicComponent.Orientation.Vertical;

            manager1.Update();
            manager1.UpdatePeriod = 1000;
            manager1.Mode = GraphicComponent.DrawMode.Activ;

            manager1.OnDataNeed += new EventHandler(manager1_OnData);
        }

        /// <summary>
        /// выполнить инициализацию второй группы графиков
        /// </summary>
        protected void InitializeSGraphicPanel()
        {
            graphic_summObem = manager2.InstanceGraphic();         // график суммарный объем в емкостях
            if (loaded == false)
            {
                graphic_summObem.Units = "м.куб";
                graphic_summObem.Description = "Об.Сумм";

                graphic_summObem.Range.Min = 0;
                graphic_summObem.Range.Max = 550;

                graphic_summObem.Color = Color.Red;
            }
            else
                if (gr_summObem != null)
                {
                    graphic_summObem.Units = gr_summObem.Units;
                    graphic_summObem.Description = gr_summObem.Description;

                    graphic_summObem.Range.Min = gr_summObem.Range.Min;
                    graphic_summObem.Range.Max = gr_summObem.Range.Max;

                    graphic_summObem.Color = gr_summObem.Color;

                    gr_summObem.Font.Dispose();
                    gr_summObem.Brush.Dispose();

                    gr_summObem = null;
                }

            graphic_hodi1 = manager2.InstanceGraphic();            // график ходы насоса 1
            if (loaded == false)
            {
                graphic_hodi1.Units = "ход/мин";
                graphic_hodi1.Description = "Ходы 1";

                graphic_hodi1.Range.Min = 0;
                graphic_hodi1.Range.Max = 130;

                graphic_hodi1.Color = Color.Green;
            }
            else
                if (gr_hodi1 != null)
                {
                    graphic_hodi1.Units = gr_hodi1.Units;
                    graphic_hodi1.Description = gr_hodi1.Description;

                    graphic_hodi1.Range.Min = gr_hodi1.Range.Min;
                    graphic_hodi1.Range.Max = gr_hodi1.Range.Max;

                    graphic_hodi1.Color = gr_hodi1.Color;

                    gr_hodi1.Font.Dispose();
                    gr_hodi1.Brush.Dispose();

                    gr_hodi1 = null;
                }

            graphic_hodi2 = manager2.InstanceGraphic();            // график ходы насоса 2
            if (loaded == false)
            {
                graphic_hodi2.Units = "ход/мин";
                graphic_hodi2.Description = "Ходы 2";

                graphic_hodi2.Range.Min = 0;
                graphic_hodi2.Range.Max = 130;

                graphic_hodi2.Color = Color.Blue;
            }
            else
                if (gr_hodi2 != null)
                {
                    graphic_hodi2.Units = gr_hodi2.Units;
                    graphic_hodi2.Description = gr_hodi2.Description;

                    graphic_hodi2.Range.Min = gr_hodi2.Range.Min;
                    graphic_hodi2.Range.Max = gr_hodi2.Range.Max;

                    graphic_hodi2.Color = gr_hodi2.Color;

                    gr_hodi2.Font.Dispose();
                    gr_hodi2.Brush.Dispose();

                    gr_hodi2 = null;
                }

            graphic_potok = manager2.InstanceGraphic();            // график поток на выходе
            if (loaded == false)
            {
                graphic_potok.Units = "%";
                graphic_potok.Description = "Поток";

                graphic_potok.Range.Min = 0;
                graphic_potok.Range.Max = 100;

                graphic_potok.Color = Color.Brown;
            }
            else
                if (gr_potok != null)
                {
                    graphic_potok.Units = gr_potok.Units;
                    graphic_potok.Description = gr_potok.Description;

                    graphic_potok.Range.Min = gr_potok.Range.Min;
                    graphic_potok.Range.Max = gr_potok.Range.Max;

                    graphic_potok.Color = gr_potok.Color;

                    gr_potok.Font.Dispose();
                    gr_potok.Brush.Dispose();

                    gr_potok = null;
                }

            manager2.StartTime = DateTime.Now;
            manager2.Orientation = GraphicComponent.Orientation.Vertical;

            manager2.Update();
            manager2.UpdatePeriod = 1000;
            manager2.Mode = GraphicComponent.DrawMode.Activ;

            manager2.OnDataNeed += new EventHandler(manager2_OnData);
        }

        // ------------------------- обработчики событий -------------------------

        protected IAsyncResult Iasync_1 = null;
        protected IAsyncResult Iasync_2 = null;
        protected IAsyncResult Iasync_3 = null;
        protected IAsyncResult Iasync_4 = null;
        protected IAsyncResult Iasync_5 = null;
        protected IAsyncResult Iasync_6 = null;
        protected IAsyncResult Iasync_7 = null;
        protected IAsyncResult Iasync_8 = null;
        protected IAsyncResult Iasync_9 = null;
        protected IAsyncResult Iasync_10 = null;
        protected IAsyncResult Iasync_11 = null;
        protected IAsyncResult Iasync_12 = null;
        protected IAsyncResult Iasync_13 = null;
        protected IAsyncResult Iasync_14 = null;
        protected IAsyncResult Iasync_15 = null;
        protected IAsyncResult Iasync_16 = null;
        protected IAsyncResult Iasync_17 = null;
        protected IAsyncResult Iasync_18 = null;

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
                    /*Parameter _plPriemna = _app.GetParameter(plPriemna.PNumber);
                    Parameter _plBlocka = _app.GetParameter(plBlocka.PNumber);

                    Parameter _plEmkOts2 = _app.GetParameter(plEmkOts2.PNumber);
                    Parameter _tempVihod = _app.GetParameter(tempVihod.PNumber);

                    Parameter _temVhod = _app.GetParameter(temVhod.PNumber);*/

                    Parameter _plPriemna = _app.GetParameter(plPriemna.Identifier);
                    Parameter _plBlocka = _app.GetParameter(plBlocka.Identifier);

                    Parameter _plEmkOts2 = _app.GetParameter(plEmkOts2.Identifier);
                    Parameter _tempVihod = _app.GetParameter(tempVihod.Identifier);

                    Parameter _temVhod = _app.GetParameter(temVhod.Identifier);

                    if (Iasync_1 == null)
                    {
                        Iasync_1 = _p1.BeginInvoke(setter, _p1, _app.Technology.P0009.Value);
                    }
                    else
                    {
                        if (Iasync_1.IsCompleted)
                        {
                            Iasync_1 = _p1.BeginInvoke(setter, _p1, _app.Technology.P0009.Value);
                        }
                    }

                    if (Iasync_2 == null)
                    {
                        Iasync_2 = _p2.BeginInvoke(setter, _p2, _app.Technology.P09_1.Value);
                    }
                    else
                    {
                        if (Iasync_2.IsCompleted)
                        {
                            Iasync_2 = _p2.BeginInvoke(setter, _p2, _app.Technology.P09_1.Value);
                        }
                    }

                    if (Iasync_3 == null)
                    {
                        Iasync_3 = _p3.BeginInvoke(setter, _p3, _app.Technology.P09_2.Value);
                    }
                    else
                        if (Iasync_3.IsCompleted)
                        {
                            Iasync_3 = _p3.BeginInvoke(setter, _p3, _app.Technology.P09_2.Value);
                        }

                    if (Iasync_4 == null || Iasync_4.IsCompleted)
                    {
                        Iasync_4 = _p4.BeginInvoke(setter, _p4, _app.Technology.P09_3.Value);
                    }

                    if (Iasync_5 == null || Iasync_5.IsCompleted)
                    {
                        Iasync_5 = _p5.BeginInvoke(setter, _p5, _app.Technology.P09_6.Value);
                    }

                    if (Iasync_6 == null || Iasync_6.IsCompleted)
                    {
                        Iasync_6 = _p6.BeginInvoke(setter, _p6, _app.Technology.P09_5.Value);
                    }

                    if (Iasync_7 == null || Iasync_7.IsCompleted)
                    {
                        Iasync_7 = _p7.BeginInvoke(setter, _p7, _app.Technology.P09_4.Value);
                    }

                    if (Iasync_8 == null || Iasync_8.IsCompleted)
                    {
                        Iasync_8 = _p8.BeginInvoke(setter, _p8, _app.Technology.P0106.Value);
                    }

                    if (Iasync_9 == null || Iasync_9.IsCompleted)
                    {
                        Iasync_9 = _p9.BeginInvoke(setter, _p9, _app.Technology.P0104.Value);
                    }

                    if (_plPriemna != null)
                    {
                        if (Iasync_10 == null || Iasync_10.IsCompleted)
                        {
                            Iasync_10 = _p10.BeginInvoke(setter, _p10, _plPriemna.CalculatedValue);
                        }
                    }

                    if (_plBlocka != null)
                    {
                        if (Iasync_11 == null || Iasync_11.IsCompleted)
                        {
                            Iasync_11 = _p11.BeginInvoke(setter, _p11, _plBlocka.CalculatedValue);
                        }
                    }

                    if (_plEmkOts2 != null)
                    {
                        if (Iasync_12 == null || Iasync_12.IsCompleted)
                        {
                            Iasync_12 = _p12.BeginInvoke(setter, _p12, _plEmkOts2.CalculatedValue);
                        }
                    }

                    if (_tempVihod != null)
                    {
                        if (Iasync_13 == null || Iasync_13.IsCompleted)
                        {
                            Iasync_13 = _p13.BeginInvoke(setter, _p13, _tempVihod.CalculatedValue);
                        }
                    }

                    if (_temVhod != null)
                    {
                        if (Iasync_14 == null || Iasync_14.IsCompleted)
                        {
                            Iasync_14 = _p14.BeginInvoke(setter, _p14, _temVhod.CalculatedValue);
                        }
                    }

                    if (Iasync_15 == null || Iasync_15.IsCompleted)
                    {
                        Iasync_15 = _p15.BeginInvoke(setter, _p15, _app.Technology.P0116.Value);
                    }

                    if (Iasync_16 == null || Iasync_16.IsCompleted)
                    {
                        Iasync_16 = _p16.BeginInvoke(setter, _p16, _app.Technology.P0117.Value);
                    }

                    if (Iasync_17 == null || Iasync_17.IsCompleted)
                    {
                        Iasync_17 = _p17.BeginInvoke(setter, _p17, _app.Technology.P0003.Value);
                    }

                    if (Iasync_18 == null || Iasync_18.IsCompleted)
                    {
                        Iasync_18 = _p18.BeginInvoke(setter, _p18, _app.Technology.P0105.Value);
                    }
                }
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
                            /*Parameter _plPriemna = _app.GetParameter(plPriemna.PNumber);
                            Parameter _plBlocka = _app.GetParameter(plBlocka.PNumber);

                            Parameter _plEmkOts2 = _app.GetParameter(plEmkOts2.PNumber);*/

                            Parameter _plPriemna = _app.GetParameter(plPriemna.Identifier);
                            Parameter _plBlocka = _app.GetParameter(plBlocka.Identifier);

                            Parameter _plEmkOts2 = _app.GetParameter(plEmkOts2.Identifier);

                            graphic_plPriemna.Clear();        // график плотность приемная емкость
                            graphic_plBlokaOchi.Clear();      // график плотность блока очистки
                            graphic_plEmnsek2.Clear();        // график плотность емкости приготовления отсек2

                            foreach (Slice slice in slices)
                            {
                                if (slice.slice != null)
                                {
                                    int sliceLen = slice.slice.Length;

                                    if (_plPriemna != null && _plPriemna.Channel != null)
                                    {
                                        if (_plPriemna.Channel.Number > -1 && _plPriemna.Channel.Number < sliceLen)
                                        {
                                            graphic_plPriemna.Insert(slice._date, slice[_plPriemna.Channel.Number]);
                                        }
                                    }

                                    if (_plBlocka != null && _plBlocka.Channel != null)
                                    {
                                        if (_plBlocka.Channel.Number > -1 && _plBlocka.Channel.Number < sliceLen)
                                        {
                                            graphic_plBlokaOchi.Insert(slice._date, slice[_plBlocka.Channel.Number]);
                                        }
                                    }

                                    if (_plEmkOts2 != null && _plEmkOts2.Channel != null)
                                    {
                                        if (_plEmkOts2.Channel.Number > -1 && _plEmkOts2.Channel.Number < sliceLen)
                                        {
                                            graphic_plEmnsek2.Insert(slice._date, slice[_plEmkOts2.Channel.Number]);
                                        }
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
                            graphic_summObem.Clear();
                            graphic_hodi1.Clear();
                            graphic_hodi2.Clear();
                            graphic_potok.Clear();

                            foreach (Slice slice in slices)
                            {
                                if (slice.slice != null)
                                {
                                    int sliceLen = slice.slice.Length;
                                    if (_app.Technology.P0104.SNumber > -1 && _app.Technology.P0104.SNumber < sliceLen)
                                    {
                                        graphic_summObem.Insert(slice._date, slice[_app.Technology.P0104.SNumber]);
                                    }

                                    if (_app.Technology.P0116.SNumber > -1 && _app.Technology.P0116.SNumber < sliceLen)
                                    {
                                        graphic_hodi1.Insert(slice._date, slice[_app.Technology.P0116.SNumber]);
                                    }

                                    if (_app.Technology.P0117.SNumber > -1 && _app.Technology.P0117.SNumber < sliceLen)
                                    {
                                        graphic_hodi2.Insert(slice._date, slice[_app.Technology.P0117.SNumber]);
                                    }

                                    if (_app.Technology.P0003.SNumber > -1 && _app.Technology.P0003.SNumber < sliceLen)
                                    {
                                        graphic_potok.Insert(slice._date, slice[_app.Technology.P0003.SNumber]);
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
                    XmlNode root = doc.CreateElement("SolutionPanel");

                    XmlNode graphic_plPriemnaNode = graphic_plPriemna.SerializeToXml(doc, "graphic_plPriemna");
                    XmlNode graphic_plBlokaOchiNode = graphic_plBlokaOchi.SerializeToXml(doc, "graphic_plBlokaOchi");
                    XmlNode graphic_plEmnsek2Node = graphic_plEmnsek2.SerializeToXml(doc, "graphic_plEmnsek2");
        
                    XmlNode graphic_summObemNode = graphic_summObem.SerializeToXml(doc, "graphic_summObem");
                    XmlNode graphic_hodi1Node = graphic_hodi1.SerializeToXml(doc, "graphic_hodi1");
                    XmlNode graphic_hodi2Node = graphic_hodi2.SerializeToXml(doc, "graphic_hodi2");
                    XmlNode graphic_potokNode = graphic_potok.SerializeToXml(doc, "graphic_potok");

                    XmlNode plPriemnaNode = doc.CreateElement("plPriemna");
                    XmlNode plBlockaNode = doc.CreateElement("plBlocka");

                    XmlNode plEmkOts2Node = doc.CreateElement("plEmkOts2");
                    XmlNode tempVihodNode = doc.CreateElement("tempVihod");

                    XmlNode temVhodNode = doc.CreateElement("temVhod");
                    XmlNode splitNode = doc.CreateElement("split");

                    /*plPriemnaNode.InnerText = plPriemna.PNumber.ToString();
                    plBlockaNode.InnerText = plBlocka.PNumber.ToString();

                    plEmkOts2Node.InnerText = plEmkOts2.PNumber.ToString();
                    tempVihodNode.InnerText = tempVihod.PNumber.ToString();

                    temVhodNode.InnerText = temVhod.PNumber.ToString();*/

                    plPriemnaNode.InnerText = plPriemna.Identifier.ToString();
                    plBlockaNode.InnerText = plBlocka.Identifier.ToString();

                    plEmkOts2Node.InnerText = plEmkOts2.Identifier.ToString();
                    tempVihodNode.InnerText = tempVihod.Identifier.ToString();

                    temVhodNode.InnerText = temVhod.Identifier.ToString();
                    splitNode.InnerText = splitterDistance.ToString();

                    root.AppendChild(graphic_plPriemnaNode);
                    root.AppendChild(graphic_plBlokaOchiNode);
                    root.AppendChild(graphic_plEmnsek2Node);
                    root.AppendChild(graphic_summObemNode);
                    root.AppendChild(graphic_hodi1Node);
                    root.AppendChild(graphic_hodi2Node);
                    root.AppendChild(graphic_potokNode);
                    root.AppendChild(plPriemnaNode);
                    root.AppendChild(plBlockaNode);
                    root.AppendChild(plEmkOts2Node);
                    root.AppendChild(tempVihodNode);
                    root.AppendChild(temVhodNode);
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

        protected Graphic gr_plPriemna;        // отображаемый график плотность приемная емкость
        protected Graphic gr_plBlokaOchi;      // отображаемый график плотность блока очистки
        protected Graphic gr_plEmnsek2;        // отображаемый график плотность емкости приготовления отсек2

        protected Graphic gr_summObem;         // отображаемый график суммарный объем в емкостях
        protected Graphic gr_hodi1;            // отображаемый график ходы насоса 1
        protected Graphic gr_hodi2;            // отображаемый график ходы насоса 2
        protected Graphic gr_potok;            // отображаемый график поток на выходе

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
                    if (Root != null && Root.Name == "SolutionPanel")
                    {
                        if (Root.HasChildNodes)
                        {
                            loaded = true;
                            foreach (XmlNode child in Root.ChildNodes)
                            {
                                switch (child.Name)
                                {
                                    case "graphic_plPriemna":

                                        try
                                        {
                                            gr_plPriemna = new Graphic();
                                            gr_plPriemna.DeSerializeToXml(child);
                                        }
                                        catch { }
                                        break;

                                    case "graphic_plBlokaOchi":

                                        try
                                        {
                                            gr_plBlokaOchi = new Graphic();
                                            gr_plBlokaOchi.DeSerializeToXml(child);
                                        }
                                        catch { }
                                        break;

                                    case "graphic_plEmnsek2":

                                        try
                                        {
                                            gr_plEmnsek2 = new Graphic();
                                            gr_plEmnsek2.DeSerializeToXml(child);
                                        }
                                        catch { }
                                        break;

                                    case "graphic_summObem":

                                        try
                                        {
                                            gr_summObem = new Graphic();
                                            gr_summObem.DeSerializeToXml(child);
                                        }
                                        catch { }
                                        break;

                                    case "graphic_hodi1":

                                        try
                                        {
                                            gr_hodi1 = new Graphic();
                                            gr_hodi1.DeSerializeToXml(child);
                                        }
                                        catch { }
                                        break;

                                    case "graphic_hodi2":

                                        try
                                        {
                                            gr_hodi2 = new Graphic();
                                            gr_hodi2.DeSerializeToXml(child);
                                        }
                                        catch { }
                                        break;

                                    case "graphic_potok":

                                        try
                                        {
                                            gr_potok = new Graphic();
                                            gr_potok.DeSerializeToXml(child);
                                        }
                                        catch { }
                                        break;

                                    case "plPriemna":

                                        try
                                        {
                                            plPriemna.Identifier = new Guid(child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case "plBlocka":

                                        try
                                        {
                                            plBlocka.Identifier = new Guid(child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case "plEmkOts2":

                                        try
                                        {
                                            plEmkOts2.Identifier = new Guid(child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case "tempVihod":

                                        try
                                        {
                                            tempVihod.Identifier = new Guid(child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case "temVhod":

                                        try
                                        {
                                            temVhod.Identifier = new Guid(child.InnerText);
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