using System;
using System.Threading;

namespace SGT
{
    /// <summary>
    /// Реализует технологию СГТ
    /// </summary>
    public partial class Technology
    {
        protected float locking_weight_hook;                // блокировочное значение веса на крюке
        protected float locking_value_rotor_speed;          // блокировочное значене оборотов ротора

        protected float locking_value_load;                 // блокировочное значение нагрузки
        protected float locking_pressure;                   // блокировочное значение давления на входе

        protected float locking_speed_talbloka;             // блокировочное значение скорости тальблока

        protected float interval_pzr;                       // интервал ПЗР
        protected float drilling_interval;                  // интервал бурения
        protected float size_bottom_hole_zone;              // размер призабойной зоны

        protected float size_layout_top_column;             // размер компоновки верха колонны
        protected float size_layout_bottom_column;          // размер компоновки низа колонны

        protected TechnologicalRegimDrilling r_drilling;    // метод расчета режима бурения
        protected TechnologicalRegimStudy r_study;          // метод расчета проработка

        protected TechnologicalRegimeWeightHook r_weight;   // Определяет возможные методы расчета состояния Вес на крюке

        protected DateTime currentTime;                     // текущее технологическое время
        protected ReaderWriterLockSlim c_slim;              // синхронизует доступ к технологическим настройкам

        /// <summary>
        /// Возникает когда завершено вычисление технологических параметров
        /// </summary>
        public event EventHandler onComplete;

        /// <summary>
        /// Возникает когда необходимо отправить команду Тальблок
        /// </summary>
        public event EventHandler onTalblock;

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public Technology()
        {
            c_slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
            cal_slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

            p1 = new SGT.P0001(Guid.Empty);
            p2 = new SGT.P0002(Guid.Empty);

            p3 = new SGT.P0003(Guid.Empty);
            p4 = new SGT.P0004(Guid.Empty);

            p5 = new SGT.P0005(Guid.Empty);

            p6 = new SGT.P0006(Guid.Empty);
            p6_1 = new SGT.P06_1(Guid.Empty);

            p6_2 = new SGT.P06_2(Guid.Empty);
            p6_3 = new SGT.P06_3(Guid.Empty);

            p6_4 = new SGT.P06_4(Guid.Empty);
            p6_5 = new SGT.P06_5(Guid.Empty);

            p6_6 = new SGT.P06_6(Guid.Empty);
            p6_7 = new SGT.P06_7(Guid.Empty);

            p6_8 = new SGT.P06_8(Guid.Empty);
            p6_9 = new SGT.P06_9(Guid.Empty);

            p7 = new SGT.P0007(Guid.Empty);
            p7_1 = new SGT.P07_1(Guid.Empty);

            p7_2 = new SGT.P07_2(Guid.Empty);
            p7_3 = new SGT.P07_3(Guid.Empty);

            p7_4 = new SGT.P07_4(Guid.Empty);
            p7_5 = new SGT.P07_5(Guid.Empty);

            p7_6 = new SGT.P07_6(Guid.Empty);
            p7_7 = new SGT.P07_7(Guid.Empty);

            p7_8 = new SGT.P07_8(Guid.Empty);
            p7_9 = new SGT.P07_9(Guid.Empty);

            p7_10 = new SGT.P7_10(Guid.Empty);
            p7_11 = new SGT.P7_11(Guid.Empty);

            p7_12 = new SGT.P7_12(Guid.Empty);
            p7_13 = new SGT.P7_13(Guid.Empty);

            p8 = new SGT.P0008(Guid.Empty);
            p8_1 = new SGT.P08_1(Guid.Empty);

            p9 = new SGT.P0009(Guid.Empty);
            p9_1 = new SGT.P09_1(Guid.Empty);

            p9_2 = new SGT.P09_2(Guid.Empty);
            p9_3 = new SGT.P09_3(Guid.Empty);

            p9_4 = new SGT.P09_4(Guid.Empty);
            p9_5 = new SGT.P09_5(Guid.Empty);

            p9_6 = new SGT.P09_6(Guid.Empty);
            p9_7 = new SGT.P09_7(Guid.Empty);

            p9_8 = new SGT.P09_8(Guid.Empty);
            p9_9 = new SGT.P09_9(Guid.Empty);

            p9_10 = new SGT.P9_10(Guid.Empty);
            p9_11 = new SGT.P9_11(Guid.Empty);

            p9_12 = new SGT.P9_12(Guid.Empty);
            p9_13 = new SGT.P9_13(Guid.Empty);

            p10 = new P0010(Guid.Empty);

            p11 = new P0011(Guid.Empty);
            p11_1 = new P11_1(Guid.Empty);

            p12 = new P0012(Guid.Empty);
            p13 = new P0013(Guid.Empty);

            p14 = new P0014(Guid.Empty);
            p14_1 = new P14_1(Guid.Empty);

            p15 = new P0015(Guid.Empty);
            p16 = new P0016(Guid.Empty);

            p17 = new P0017(Guid.Empty);
            p18 = new P0018(Guid.Empty);

            p101 = new P0101(Guid.Empty);
            p102 = new P0102(Guid.Empty);

            p103 = new P0103(Guid.Empty);
            p104 = new P0104(Guid.Empty);

            p105 = new P0105(Guid.Empty);
            p106 = new P0106(Guid.Empty);

            p107 = new P0107(Guid.Empty);
            p108 = new P0108(Guid.Empty);

            p109 = new P0109(Guid.Empty);
            p110 = new P0110(Guid.Empty);

            p112 = new P0112(Guid.Empty);
            p113 = new P0113(Guid.Empty);

            p114 = new P0114(Guid.Empty);
            p116 = new P0116(Guid.Empty);

            p117 = new P0117(Guid.Empty);
            p118 = new P0118(Guid.Empty);

            p200 = new P0200(Guid.Empty);
            p201 = new P0201(Guid.Empty);

            p202 = new P0202(Guid.Empty);
            p203 = new P0203(Guid.Empty);

            p204 = new P0204(Guid.Empty);
            p205 = new P0205(Guid.Empty);

            p206 = new P0206(Guid.Empty);
            p207 = new P0207(Guid.Empty);

            p208 = new P0208(Guid.Empty);
            p209 = new P0209(Guid.Empty);

            p210 = new P0210(Guid.Empty);
            p211 = new P0211(Guid.Empty);

            p212 = new P0212(Guid.Empty);
            p213 = new P0213(Guid.Empty);

            currentTime = DateTime.MinValue;

            driller_console = -1;
            driller_console_weight_column = -1;
        }

        /// <summary>
        /// Возвращяет текущее технологическое время
        /// </summary>
        public DateTime TechnologyTime
        {
            get
            {
                if (c_slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return currentTime;
                    }
                    finally
                    {
                        c_slim.ExitReadLock();
                    }
                }

                return DateTime.MinValue;
            }

            protected set
            {
                if (c_slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        currentTime = value;
                    }
                    finally
                    {
                        c_slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Метод расчета режима бурения
        /// </summary>
        public TechnologicalRegimDrilling TechnologicalRegimDrilling
        {
            get
            {
                if (c_slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return r_drilling;
                    }
                    finally
                    {
                        c_slim.ExitReadLock();
                    }
                }

                return SGT.TechnologicalRegimDrilling.Default;
            }

            set
            {
                if (c_slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        r_drilling = value;
                    }
                    finally
                    {
                        c_slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Метод расчета проработка
        /// </summary>
        public TechnologicalRegimStudy TechnologicalRegimStudy
        {
            get
            {
                if (c_slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return r_study;
                    }
                    finally
                    {
                        c_slim.ExitReadLock();
                    }
                }

                return SGT.TechnologicalRegimStudy.Default;
            }

            set
            {
                if (c_slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        r_study = value;
                    }
                    finally
                    {
                        c_slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет возможные методы расчета состояния Вес на крюке
        /// </summary>
        public TechnologicalRegimeWeightHook TechnologicalRegimeWeightHook
        {
            get
            {
                if (c_slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return r_weight;
                    }
                    finally
                    {
                        c_slim.ExitReadLock();
                    }
                }

                return SGT.TechnologicalRegimeWeightHook.Default;
            }

            set
            {
                if (c_slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        r_weight = value;
                    }
                    finally
                    {
                        c_slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Блокировочное значение веса на крюке
        /// </summary>
        public float LockingWeightHook
        {
            get
            {
                if (c_slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return locking_weight_hook;
                    }
                    finally
                    {
                        c_slim.ExitReadLock();
                    }
                }

                return float.NaN;
            }

            set
            {
                if (c_slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        locking_weight_hook = value;
                    }
                    finally
                    {
                        c_slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Блокировочное значение скорости тальблока
        /// </summary>
        public float LockingSpeedTalblok
        {
            get
            {
                if (c_slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return locking_speed_talbloka;
                    }
                    finally
                    {
                        c_slim.ExitReadLock();
                    }
                }

                return float.NaN;
            }

            set
            {
                if (c_slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        locking_speed_talbloka = value;
                    }
                    finally
                    {
                        c_slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Интервал ПЗР
        /// </summary>
        public float IntervalPzr
        {
            get
            {
                if (c_slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return interval_pzr;
                    }
                    finally
                    {
                        c_slim.ExitReadLock();
                    }
                }

                return float.NaN;
            }

            set
            {
                if (c_slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        interval_pzr = value;
                    }
                    finally
                    {
                        c_slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Интервал бурения
        /// </summary>
        public float DrillingInterval
        {
            get
            {
                if (c_slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return drilling_interval;
                    }
                    finally
                    {
                        c_slim.ExitReadLock();
                    }
                }

                return float.NaN;
            }

            set
            {
                if (c_slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        drilling_interval = value;
                    }
                    finally
                    {
                        c_slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Размер призабойной зоны
        /// </summary>
        public float SizeBottomHoleZone
        {
            get
            {
                if (c_slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return size_bottom_hole_zone;
                    }
                    finally
                    {
                        c_slim.ExitReadLock();
                    }
                }

                return float.NaN;
            }

            set
            {
                if (c_slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        size_bottom_hole_zone = value;
                    }
                    finally
                    {
                        c_slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Блокировочное значение давления на входе
        /// </summary>
        public float LockingPressure
        {
            get
            {
                if (c_slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return locking_pressure;
                    }
                    finally
                    {
                        c_slim.ExitReadLock();
                    }
                }

                return float.NaN;
            }

            set
            {
                if (c_slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        locking_pressure = value;
                    }
                    finally
                    {
                        c_slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Размер компоновки низа колонны
        /// </summary>
        public float SizeLayoutBottomColumn
        {
            get
            {
                if (c_slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return size_layout_bottom_column;
                    }
                    finally
                    {
                        c_slim.ExitReadLock();
                    }
                }

                return float.NaN;
            }

            set
            {
                if (c_slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        size_layout_bottom_column = value;
                    }
                    finally
                    {
                        c_slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Размер компоновки низа колонны
        /// </summary>
        public float SizeLayoutTopColumn
        {
            get
            {
                if (c_slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return size_layout_top_column;
                    }
                    finally
                    {
                        c_slim.ExitReadLock();
                    }
                }

                return float.NaN;
            }

            set
            {
                if (c_slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        size_layout_top_column = value;
                    }
                    finally
                    {
                        c_slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Блокировочное значене оборотов ротора
        /// </summary>
        public float LockingValueRotorSpeed
        {
            get
            {
                if (c_slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return locking_value_rotor_speed;
                    }
                    finally
                    {
                        c_slim.ExitReadLock();
                    }
                }

                return float.NaN;
            }

            set
            {
                if (c_slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        locking_value_rotor_speed = value;
                    }
                    finally
                    {
                        c_slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Блокировочное значение нагрузки
        /// </summary>
        public float LockingValueLoad
        {
            get
            {
                if (c_slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return locking_value_load;
                    }
                    finally
                    {
                        c_slim.ExitReadLock();
                    }
                }

                return float.NaN;
            }

            set
            {
                if (c_slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        locking_value_load = value;
                    }
                    finally
                    {
                        c_slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Номер параметра в списке , который содержит команду пульта бурильщика
        /// </summary>
        public int DrillerConsole
        {
            get
            {
                if (c_slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return driller_console;
                    }
                    finally
                    {
                        c_slim.ExitReadLock();
                    }
                }

                return -1;
            }

            set
            {
                if (c_slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        driller_console = value;
                    }
                    finally
                    {
                        c_slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Идентификатор параметра в списке , который содержит команду пульта бурильщика
        /// </summary>
        public Guid IdentifierDrillerConsole
        {
            get
            {
                if (c_slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return id_driller_console;
                    }
                    finally
                    {
                        c_slim.ExitReadLock();
                    }
                }

                return Guid.Empty;
            }

            set
            {
                if (c_slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        id_driller_console = value;
                    }
                    finally
                    {
                        c_slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Номер параметра в списке , который содержит команду пульта бурильщика на сброс веса колоны
        /// </summary>
        public int DrillerConsoleWeightColumn
        {
            get
            {
                if (c_slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return driller_console_weight_column;
                    }
                    finally
                    {
                        c_slim.ExitReadLock();
                    }
                }

                return -1;
            }

            set
            {
                if (c_slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        driller_console_weight_column = value;
                    }
                    finally
                    {
                        c_slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Идентификатор параметра в списке , который содержит команду пульта бурильщика на сброс веса колоны
        /// </summary>
        public Guid IdentifierDrillerConsoleWeightColumn
        {
            get
            {
                if (c_slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return id_driller_console_weight_column;
                    }
                    finally
                    {
                        c_slim.ExitReadLock();
                    }
                }

                return Guid.Empty;
            }

            set
            {
                if (c_slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        id_driller_console_weight_column = value;
                    }
                    finally
                    {
                        c_slim.ExitWriteLock();
                    }
                }
            }
        }

        // --------------- технологический этап и режим работы ---------------

        private string tech_stage = string.Empty;
        private string tech_regime = string.Empty;

        private string tech_hook = string.Empty;

        /// <summary>
        /// Возвращяет текстовое описание текущего технологического этапа работы
        /// </summary>
        public string TechnologicalStage
        {
            get
            {
                if (c_slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return tech_stage;
                    }
                    finally
                    {
                        c_slim.ExitReadLock();
                    }
                }

                return string.Empty;
            }

            protected set
            {
                if (c_slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        tech_stage = value;
                    }
                    finally
                    {
                        c_slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Возвращяет текстовое описание текущего технологического режима работы
        /// </summary>
        public string TechnologicalRegime
        {
            get
            {
                if (c_slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return tech_regime;
                    }
                    finally
                    {
                        c_slim.ExitReadLock();
                    }
                }

                return string.Empty;
            }

            protected set
            {
                if (c_slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        tech_regime = value;
                    }
                    finally
                    {
                        c_slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Возвращяет текстовое описание крюка
        /// </summary>
        public string TechnologicalHook
        {
            get
            {
                if (c_slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return tech_hook;
                    }
                    finally
                    {
                        c_slim.ExitReadLock();
                    }
                }

                return string.Empty;
            }

            protected set
            {
                if (c_slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        tech_hook = value;
                    }
                    finally
                    {
                        c_slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Осуществляет сохранение технологических данных
        /// </summary>
        /// <param name="slice">Срез данных в который осуществляется сохранение вычисленных технологических параметров</param>
        public void SaverTechnologyData(float[] slice)
        {
            if (slice != null)
            {
                TParameter[] parameters = Parameters;
                if (parameters != null)
                {
                    foreach (TParameter parameter in parameters)
                    {
                        if (parameter != null)
                        {
                            if (!parameter.IsSimple)
                            {
                                if (parameter.SNumber > -1 && parameter.SNumber < slice.Length)
                                {
                                    slice[parameter.SNumber] = parameter.Value;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Определяет результат работы функции
    /// </summary>
    public enum TProcResult
    {
        /// <summary>
        /// Результат выполениния функции положительный.
        /// </summary>
        False,

        /// <summary>
        /// Результат выполнения функции отрицательный.
        /// </summary>
        True,

        /// <summary>
        /// Результат работы функции не определен.
        /// </summary>
        Default
    }

    /// <summary>
    /// Определяет метод расчета режима бурения
    /// </summary>
    public enum TechnologicalRegimDrilling
    {
        /// <summary>
        /// Давление
        /// </summary>
        Pressure,

        /// <summary>
        /// Давление и нагрузка
        /// </summary>
        PressureAndLoad,

        /// <summary>
        /// Метод не определен
        /// </summary>
        Default
    }

    /// <summary>
    /// Определяет метод расчета проработка
    /// </summary>
    public enum TechnologicalRegimStudy
    {
        /// <summary>
        /// Расчитывать по оборотам ротора
        /// </summary>
        RotorSpeed,

        /// <summary>
        /// Расчитывать по скорости тальблока
        /// </summary>
        SpeedTalblok,

        /// <summary>
        /// Расчитывать по оборотам ротора или скорости тальблока
        /// </summary>
        RotorSpeenOrSpeedTalblok,

        /// <summary>
        /// Метод расчета не определен
        /// </summary>
        Default
    }

    /// <summary>
    /// Определяет возможные методы расчета состояния Вес на крюке
    /// </summary>
    public enum TechnologicalRegimeWeightHook
    {
        /// <summary>
        /// Расчитывать по весу
        /// </summary>
        Weight,

        /// <summary>
        /// Расчитывать по клиньям
        /// </summary>
        Wedges,

        /// <summary>
        /// Расчитывать по весу или клиньям
        /// </summary>
        WeightOrWedges,


        /// <summary>
        /// Метод не определен.
        /// </summary>
        Default
    }

    /// <summary>
    /// Состояние параметра вес
    /// </summary>
    public enum WeightStatus
    {
        /// <summary>
        /// Пустой крюк
        /// </summary>
        wsClear,

        /// <summary>
        /// Над забоем
        /// </summary>
        wsWeight,

        /// <summary>
        /// Состояние "неопределено"
        /// </summary>
        Default
    }
}