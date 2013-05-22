using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Глубина забоя
    /// </summary>
    public class P0205 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор параметра</param>
        public P0205(Guid p_identifier)
            : base(p_identifier, "P0205", "Глубина забоя")
        {
            simple = false;
            _value = 0;
        }

        /// <summary>
        /// Вычисляет текущее значение параметра Глубина забоя
        /// </summary>
        /// <param name="v1">Положение инструмента в текущий момент</param>
        /// <param name="v2">Длина инструмента</param>
        public void Calculate(P0204 v1, P0202 v2)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    float vNew = float.NaN;
                    if (float.IsNaN(_value)) _value = 0;

                    switch (v2.ModeProccess)
                    {
                        case P0202.TModeProcess.mpBase:

                            vNew = v1.Value;
                            if (!float.IsNaN(vNew))
                            {
                                if (vNew > _value)
                                {
                                    _value = vNew;
                                }
                            }
                            break;

                        case P0202.TModeProcess.mpSetUser:

                            /*vNew = v1.Value;
                            if (vNew > _value)
                            {
                                _value = vNew;
                            }                                
                            v2.ModeProccess = P0202.TModeProcess.mpBase;*/
                            break;

                        case P0202.TModeProcess.mpCMDzaboi:

                            //v2.ModeProccess = P0202.TModeProcess.mpBase;
                            break;

                        case P0202.TModeProcess.mpCMDmodifyDepth:

                            /*vNew = v1.Value;
                            v2.ModeProccess = P0202.TModeProcess.mpBase;                                

                            if (vNew > _value)
                            {
                                _value = vNew;
                            }*/
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