using System;
using System.Xml;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Скорость тальблока
    /// </summary>
    public class P0103 : TParameter
    {
        protected int averaging_period;             // период усреднения 
        protected DateTime time_saving;             // Время сохранения параметра _Высота_талевого_блока

        protected float height_traveling_block;     // Предыдущее значение тальблока или Nan, если в истории не вычислялся ранее

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P0103(Guid p_identifier)
            : base(p_identifier, "P0103", "Скорость тальблока")
        {
            simple = false;
            averaging_period = 1000;

            time_saving = DateTime.MinValue;
            height_traveling_block = float.NaN;
        }

        /// <summary>
        /// Определяет период усреднения скорости тальблока
        /// </summary>
        public int AveragingPeriod
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return averaging_period;
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
                        averaging_period = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Вычисляет значение параметра Скорость тальблока
        /// </summary>
        /// <param name="Текущее_положение">Текущее_положение тальблока</param>
        /// <param name="CurrTechTime">Текущее время процесса</param>
        public void Calculate(P0005 currentPosition, DateTime CurrTechTime)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    DateTime t1 = CurrTechTime; // текущее время технологии

                    float p = height_traveling_block; // значение тальблока в указанный момент или Nan, если в истории не сохранился
                    if (float.IsNaN(p))
                    {
                        time_saving = t1;
                        height_traveling_block = currentPosition.Value;                        
                        
                        _value = float.NaN;
                    }

                    TimeSpan tPS = TimeSpan.FromTicks(averaging_period * TimeSpan.TicksPerMillisecond);
                    DateTime t2 = time_saving + tPS; // время, отстоящее от предыдущего момента на интервал

                    if (t1 < t2)
                    {
                        return;
                    }
                    else
                    {
                        TimeSpan dT = t1 - time_saving;
                        double lT = dT.Ticks;

                        height_traveling_block = currentPosition.Value;
                        time_saving = t1;

                        _value = (float)(((p - currentPosition.Value)  / lT) * TimeSpan.TicksPerSecond);
                    }
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        // --------------------------- сохранение параметра ---------------------------

        /// <summary>
        /// Имя узла в котором сохраняется период усреднения 
        /// </summary>
        protected const string AveragingPeriodName = "averaging_period";
        
        /// <summary>
        /// Имя узла в котором сохраняется Время сохранения параметра _Высота_талевого_блока
        /// </summary>
        protected const string TimeSavingName = "time_saving";

        /// <summary>
        /// Имя узла в котором сохраняется Предыдущее значение тальблока или Nan, если в истории не вычислялся ранее
        /// </summary>
        protected const string HeightTravelingBlockName = "height_traveling_block";

        /// <summary>
        /// Сохранить параметр в Xml узел
        /// </summary>
        /// <param name="document">XML документ, куда будет добавлен данный XmlNode</param>
        /// <returns>Узел в котором сохранен параметр</returns>
        public override XmlNode Save(XmlDocument document)
        {
            XmlNode root = base.Save(document);
            if (root != null)
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        XmlNode AveragingPeriodNode = document.CreateElement(AveragingPeriodName);
                        XmlNode TimeSavingNode = document.CreateElement(TimeSavingName);

                        XmlNode HeightTravelingBlockNode = document.CreateElement(HeightTravelingBlockName);

                        AveragingPeriodNode.InnerText = averaging_period.ToString();
                        TimeSavingNode.InnerText = time_saving.ToString();

                        HeightTravelingBlockNode.InnerText = height_traveling_block.ToString();

                        root.AppendChild(AveragingPeriodNode);
                        root.AppendChild(TimeSavingNode);

                        root.AppendChild(HeightTravelingBlockNode);                        

                        return root;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Загрузить параметр из Xml узла
        /// </summary>
        /// <param name="Node">Xml узел в котором сохранен параметр</param>        
        public override void Load(XmlNode Node)
        {
            base.Load(Node);
            if (slim.TryEnterWriteLock(500))
            {
                try
                {
                    if (Node != null && Node.HasChildNodes)
                    {
                        if (Node.Name == RootName)
                        {
                            foreach (XmlNode Child in Node.ChildNodes)
                            {
                                switch (Child.Name)
                                {
                                    case AveragingPeriodName:

                                        try
                                        {
                                            averaging_period = int.Parse(Child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case TimeSavingName:

                                        try
                                        {
                                            time_saving = DateTime.Parse(Child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case HeightTravelingBlockName:

                                        try
                                        {
                                            height_traveling_block = float.Parse(Child.InnerText);
                                        }
                                        catch { }
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

        // ----------------------------------------------------------------------------
    }
}