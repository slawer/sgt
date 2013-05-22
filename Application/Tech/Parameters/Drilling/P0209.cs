using System;
using System.Xml;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Время бурения 1м проходки
    /// </summary>
    public class P0209 : TParameter
    {
        /// <summary>
        /// Значение не определно
        /// </summary>
        public static int Техпроцесс_Default = 0;

        /// <summary>
        /// Константа Над забоем / Бурение / Бурение
        /// </summary>
        public static int НадЗабоем_Бурение_Бурение = 0 + 128 + 6;

        // ----------------------------------------------------------------------

        protected float current_node_drilling;      // глубина забоя в m, от которого рассчитывается Время бурения 1м проходки
        protected DateTime current_node_time;       // время, соответствующее Текущему узлу

        protected float target_node;                // глубина забоя в m, до которого рассчитывается Время бурения 1м проходки
        protected int previous_state;               // предыдущее состояние процесса

        protected float averaging_step;             // шаг усреднения в метрах, используемую для расчёта Время бурения 1м проходки

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор параметра</param>
        public P0209(Guid p_identifier)
            : base(p_identifier, "P0209", "Время бурения 1м проходки")
        {
            simple = false;

            current_node_drilling = float.NaN;
            current_node_time = DateTime.MinValue;

            target_node = 0;
            previous_state = Техпроцесс_Default;

            averaging_step = 1;
        }

        /// <summary>
        /// Определяет шаг усреднения в метрах
        /// </summary>
        public float AveragingStep
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return averaging_step;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return float.NaN;
            }

            set
            {
                if (slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        averaging_step = value;
                        current_node_drilling = float.NaN;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Вычисляет целевую точку по текущей
        /// </summary>
        protected void setGoalPoint()
        {
            int _g = ((int)(current_node_drilling / averaging_step)) + 1;
            target_node = _g * averaging_step;
        }

        /// <summary>
        /// Вычисляет текущее значение параметра Время бурения 1м проходки
        /// </summary>
        /// <param name="Забой">Глубина забоя в текущий момент</param>
        /// <param name="Состояние_процесса">Cостояние процесса бурения в текущий момент</param>
        /// <param name="history">История процесса</param>
        public void Calculate(P0205 v1, P0206 v2, DateTime currentTime)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    if (float.IsNaN(v1.Value) || float.IsNaN(v2.Value))
                    {
                        _value = 0;
                        current_node_drilling = float.NaN;
                    }
                    else
                    {
                        int _state = (int)Math.Round(v2.Value);
                        if (_state == НадЗабоем_Бурение_Бурение)
                        {
                            if (float.IsNaN(current_node_drilling))
                            {
                                current_node_drilling = v1.Value;
                                setGoalPoint();

                                _value = 0;
                                current_node_time = currentTime;
                            }
                            else
                            {
                                if (target_node < v1.Value)
                                {
                                    DateTime _T = currentTime;
                                    TimeSpan _dT = _T -current_node_time;

                                    double lT = _dT.Ticks;// / TimeSpan.TicksPerMillisecond;

                                    _value = (float)((lT / TimeSpan.TicksPerMinute) / (v1.Value - current_node_drilling));

                                    current_node_drilling = v1.Value;
                                    setGoalPoint();

                                    current_node_time = _T;
                                }
                            }
                        }
                        else
                        {
                            _value = 0;
                            current_node_drilling = float.NaN;
                        }
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
        /// Имя узла в котором глубина забоя в m, от которого рассчитывается скорость
        /// </summary>
        private const string CurrentNodeDrillingName = "current_node_drilling";

        /// <summary>
        /// Имя узла в котором Время, соответствующее Текущему узлу
        /// </summary>
        private const string CurrentNodeTimeName = "current_node_time";

        /// <summary>
        /// Имя узла в котором Хранит глубину забоя в m, до которого рассчитывается скорость
        /// </summary>
        private const string TargetNodeName = "target_node";

        /// <summary>
        /// Имя узла в котором Предыдущее состояние процесса
        /// </summary>
        private const string PreviousStateName = "previous_state";

        /// <summary>
        /// Имя узла в котором шаг усреднения в метрах, используемую для расчёта скорости
        /// </summary>
        private const string AveragingStepName = "averaging_step";

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
                        XmlNode CurrentNodeDrillingNode = document.CreateElement(CurrentNodeDrillingName);
                        XmlNode CurrentNodeTimeNode = document.CreateElement(CurrentNodeTimeName);

                        XmlNode TargetNodeNode = document.CreateElement(TargetNodeName);
                        XmlNode PreviousStateNode = document.CreateElement(PreviousStateName);

                        XmlNode AveragingStepNode = document.CreateElement(AveragingStepName);

                        CurrentNodeDrillingNode.InnerText = current_node_drilling.ToString();
                        CurrentNodeTimeNode.InnerText = current_node_time.ToString();

                        TargetNodeNode.InnerText = target_node.ToString();
                        PreviousStateNode.InnerText = previous_state.ToString();

                        AveragingStepNode.InnerText = averaging_step.ToString();

                        root.AppendChild(CurrentNodeDrillingNode);
                        root.AppendChild(CurrentNodeTimeNode);

                        root.AppendChild(TargetNodeNode);
                        root.AppendChild(PreviousStateNode);

                        root.AppendChild(AveragingStepNode);

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
                                    case CurrentNodeDrillingName:

                                        try
                                        {
                                            current_node_drilling = float.Parse(Child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case CurrentNodeTimeName:

                                        try
                                        {
                                            current_node_time = DateTime.Parse(Child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case TargetNodeName:

                                        try
                                        {
                                            target_node = float.Parse(Child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case PreviousStateName:

                                        try
                                        {
                                            previous_state = int.Parse(Child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case AveragingStepName:

                                        try
                                        {
                                            averaging_step = float.Parse(Child.InnerText);
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