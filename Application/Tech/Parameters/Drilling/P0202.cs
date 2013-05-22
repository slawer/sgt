using System;
using System.Xml;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Длина инструмента
    /// </summary>
    public class P0202 : TParameter
    {
        protected WeightStatus weight_status;   // текущее состояние веса на крюке
        protected TModeProcess mode_proccess;   // режим вычисления технологических параметров

        protected float last_talblock;          // начение тальблока в предыдущий цикл расчётов при состоянии процесса "над забоем"

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор параметра</param>
        public P0202(Guid p_identifier)
            : base(p_identifier, "P0202", "Длина инструмента")
        {
            simple = false;

            _value = 0;
            last_talblock = 0;

            mode_proccess = TModeProcess.mpBase;
            weight_status = WeightStatus.wsClear;
        }

        /// <summary>
        /// Определяет режим вычисления технологических параметров
        /// </summary>
        public TModeProcess ModeProccess
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return mode_proccess;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return TModeProcess.Default;
            }

            set
            {
                if (slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        mode_proccess = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Вычисляет текущее значение параметра Длина инструмента
        /// </summary>
        /// <param name="v1">Положение тальблока в текущий момент</param>
        /// <param name="v2">Вес на крюке в текущий момент</param>
        /// <param name="v3">Положение клиньев в текущий момент</param>
        /// <param name="v4">Положение инструмента (долота) в текущий момент</param>
        /// <param name="v5">Глубина забоя в текущий момент</param>
        /// <param name="v6">Количество опущенных свеч</param>
        /// <param name="currentTime">Текущее технологическое время</param>
        /// <param name="locking_weight_hook">Блокировочное значение веса на крюке</param>
        /// <param name="size_layout_bottom_column">Размер компоновки низа колонны</param>
        /// <param name="r_weight">Метод расчета веса на крюке</param>
        public void Calculate(P0005 v1, P0102 v2, P0012 v3, P0204 v4, P0205 v5, P0203 v6,
            DateTime currentTime, float locking_weight_hook, float size_layout_bottom_column, 
            float size_layout_top_column, TechnologicalRegimeWeightHook r_weight)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    switch (mode_proccess)
                    {
                        case TModeProcess.mpBase:

                            _value = current_length_instrument(v1, v3, v2, locking_weight_hook, r_weight);
                            break;

                        case TModeProcess.mpSetUser:

                            mode_proccess = TModeProcess.mpBase;
                            if (WeightHookForWeightOrWedges(v2, v3, locking_weight_hook, r_weight)
                                != TProcResult.True || float.IsNaN(v1.Value))
                            {
                                v4.Calculate(_value);
                            }

                            if (_value <= (size_layout_bottom_column + size_layout_top_column))
                            {
                                v6.CorrectLenghtInstrument(0.0f);
                                v6.Calculate(0.0f);
                            }
                            else
                            {
                                float _tmp = _value - (size_layout_bottom_column + size_layout_top_column);
                                v6.CorrectLenghtInstrument(_tmp);
                            }
                            break;

                        case TModeProcess.mpCMDzaboi:

                            mode_proccess = TModeProcess.mpBase;

                            if (WeightHookForWeightOrWedges(v2, v3, locking_weight_hook, r_weight)
                                != TProcResult.True || float.IsNaN(v1.Value))
                            {
                                _value = current_length_instrument(v1, v3, v2, locking_weight_hook, r_weight);
                            }
                            else
                            {
                                last_talblock = v1.Value;
                                weight_status = WeightStatus.wsWeight;

                                _value = v5.Value + v1.Value;

                                if (_value <= (size_layout_bottom_column + size_layout_top_column))
                                {
                                    v6.CorrectLenghtInstrument(0.0f);
                                    v6.Calculate(0.0f);
                                }
                                else
                                {
                                    float _tmp = _value - (size_layout_bottom_column + size_layout_top_column);
                                    v6.CorrectLenghtInstrument(_tmp);
                                }
                            }
                            break;

                        case TModeProcess.mpCMDmodifyDepth:

                            if (WeightHookForWeightOrWedges(v2, v3, locking_weight_hook, r_weight)
                                != TProcResult.True || float.IsNaN(v1.Value))
                            {
                                weight_status = WeightStatus.wsClear;
                                if (v5.Value < _value)
                                {
                                    _value = v5.Value;
                                    if (_value <= (size_layout_bottom_column + size_layout_top_column))
                                    {
                                        v6.CorrectLenghtInstrument(0.0f);
                                        v6.Calculate(0.0f);
                                    }
                                    else
                                    {
                                        float _tmp = _value - (size_layout_bottom_column + size_layout_top_column);
                                        v6.CorrectLenghtInstrument(_tmp);
                                    }
                                }

                                v4.Calculate(_value);
                            }
                            else
                            {
                                last_talblock = v1.Value;
                                weight_status = WeightStatus.wsWeight;

                                if (v5.Value < (_value - v1.Value))
                                {
                                    _value = v5.Value + v1.Value;
                                    if (_value <= (size_layout_bottom_column + size_layout_top_column))
                                    {
                                        v6.CorrectLenghtInstrument(0.0f);
                                        v6.Calculate(0.0f);
                                    }
                                    else
                                    {
                                        float _tmp = _value - (size_layout_bottom_column + size_layout_top_column);
                                        v6.CorrectLenghtInstrument(_tmp);
                                    }
                                }
                            }

                            mode_proccess = TModeProcess.mpBase;
                            break;

                        default:
                            break;
                    }
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        // -------------- вспомогательные функции --------------

                /// <summary>
        /// Вычисляет текущее значение параметра Длина инструмента
        /// </summary>
        /// <param name="v1">Положение тальблока в текущий момент</param>
        /// <param name="клинья">Положение клиньев в текущий момент</param>
        /// <param name="вес_колонны">Вес на крюке в текущий момент</param>
        /// <param name="locking_weight_hook">Блокировочное значение веса на крюке.</param>
        /// <param name="r_weight">Метод расчета веса на крюке</param>
        /// <returns>Вычисленное значение параметра</returns>
        private float current_length_instrument(P0005 v1, P0012 v2, P0102 v3,
            float locking_weight_hook, TechnologicalRegimeWeightHook r_weight)
        {
            if (float.IsNaN(v1.Value))
            {
                return _value;
            }
            else
            {
                switch (weight_status)
                {
                    case WeightStatus.wsClear:

                        if (WeightHookForWeightOrWedges(v3, v2, locking_weight_hook, r_weight) == TProcResult.True)
                        {
                            _value = _value + v1.Value - last_talblock;
                            
                            last_talblock = v1.Value;
                            weight_status = WeightStatus.wsWeight;
                        }
                        return _value;

                    case WeightStatus.wsWeight:

                        if (WeightHookForWeightOrWedges(v3, v2, locking_weight_hook, r_weight) == TProcResult.True)
                        {
                            last_talblock = v1.Value;
                        }
                        else
                        {
                            weight_status = WeightStatus.wsClear;
                        }

                        return _value;

                    case WeightStatus.Default:

                        weight_status = WeightStatus.wsClear;
                        break;

                    default:
                        break;
                }

                return _value;
            }
        }

        /// <summary>
        /// Присвоить значение параметру
        /// </summary>
        /// <param name="nvalue">присваиваемое значение</param>
        public void setValue(float nvalue)
        {
            try
            {
                Value = nvalue;
            }
            catch { }
        }

        // -----------------------------------------------------

        /// <summary>
        /// Возможные состояния технологического процесса, задающие 
        /// алгорит вычисления некоторых технологических параметров.
        /// </summary>
        public enum TModeProcess
        {
            /// <summary>
            /// Основное состяние. Длина инструмента вычисляется путём анализа тальблока и веса
            /// </summary>
            mpBase,

            /// <summary>
            /// Длина инструмента задана пользователем
            /// </summary>
            mpSetUser,

            /// <summary>
            /// Длина инструмента устанавливается по команде пользователя постановка на забой
            /// </summary>
            mpCMDzaboi,

            /// <summary>
            /// Длина инструмента устанавливается по команде пользователя Корректировка глубины забоя
            /// </summary>
            mpCMDmodifyDepth,

            /// <summary>
            /// По умолчанию. состояние не определено.
            /// </summary>
            Default
        }

        // --------------------------- сохранение параметра ---------------------------

        /// <summary>
        /// Имя узла в котором сохраняется источник значения для параметра
        /// </summary>
        private const string TModeProcessName = "mode_proccess";

        /// <summary>
        /// Имя узла в котором сохраняется текущее состояние веса на крюке
        /// </summary>
        private const string WeightStatusName = "weight_status";

        /// <summary>
        /// Имя узла в котором сохраняется начение тальблока в предыдущий цикл расчётов при состоянии процесса "над забоем"
        /// </summary>
        private const string last_talblockName = "last_talbloc";

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
                        XmlNode TModeProcessNode = document.CreateElement(TModeProcessName);
                        XmlNode WeightStatusNode = document.CreateElement(WeightStatusName);

                        XmlNode last_talblockNode = document.CreateElement(last_talblockName);

                        TModeProcessNode.InnerText = mode_proccess.ToString();
                        WeightStatusNode.InnerText = weight_status.ToString();

                        last_talblockNode.InnerText = last_talblock.ToString();

                        root.AppendChild(TModeProcessNode);
                        root.AppendChild(WeightStatusNode);

                        root.AppendChild(last_talblockNode);
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
                                    case TModeProcessName:

                                        try
                                        {
                                            mode_proccess = (TModeProcess)Enum.Parse(typeof(TModeProcess), Child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case WeightStatusName:

                                        try
                                        {
                                            weight_status = (WeightStatus)Enum.Parse(typeof(WeightStatus), Child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case last_talblockName:

                                        try
                                        {
                                            last_talblock = float.Parse(Child.InnerText);
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

        // ----------------------------------------------------------------------------
    }
}