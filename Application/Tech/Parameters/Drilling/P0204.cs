using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Положение инструмента
    /// </summary>
    public class P0204 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор параметра</param>
        public P0204(Guid p_identifier)
            : base(p_identifier, "P0204", "Положение инструмента")
        {
            simple = false;
            _value = 0;
        }

        /// <summary>
        /// Вычисляет текущее значение параметра Положение инструмента
        /// </summary>
        /// <param name="v1">Положение тальблока в текущий момент</param>
        /// <param name="v2">Вес на крюке в текущий момент</param>
        /// <param name="v3">Длина инструмента в текущий момент</param>
        /// <param name="v4">Клинья АСУ</param>
        /// <param name="v5">Глубина забоя</param>
        /// <param name="currentTime">Текущее время технологического процесса</param>
        /// <param name="locking_weight_hook">Блокировочное_значение_веса_на_крюке</param>
        /// <param name="r_weight">Метод расчета веса на крюке</param>
        public void Calculate(P0005 v1, P0102 v2, P0202 v3, P0012 v4, P0205 v5, DateTime currentTime, 
            float locking_weight_hook, TechnologicalRegimeWeightHook r_weight)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    if (float.IsNaN(_value)) _value = 0;
                    switch (v3.ModeProccess)
                    {
                        case P0202.TModeProcess.mpBase:

                            if ((WeightHookForWeightOrWedges(v2, v4, locking_weight_hook, r_weight) 
                                == TProcResult.True) && !float.IsNaN(v1.Value))
                            {
                                _value = v3.Value - v1.Value;
                            }
                            break;

                        case P0202.TModeProcess.mpSetUser:

                            /*if ((WeightHookForWeightOrWedges(v2, v4, locking_weight_hook, r_weight)
                                                            == TProcResult.True) && !float.IsNaN(v1.Value))
                            {
                                _value = v3.Value - v1.Value;
                            }
                            else
                            {
                                _value = v3.Value;
                            }*/
                            break;

                        case P0202.TModeProcess.mpCMDzaboi:

                            //_value = v2.Value;
                            break;

                        case P0202.TModeProcess.mpCMDmodifyDepth:

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
    }
}