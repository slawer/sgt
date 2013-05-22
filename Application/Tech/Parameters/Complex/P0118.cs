using System;
using System.Xml;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Сумма ходов насоса
    /// </summary>
    public class P0118 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P0118(Guid p_identifier)
            : base(p_identifier, "P0118", "Сумма ходов насоса")
        {
            simple = false;
        }

        /// <summary>
        /// Вычисляет текущее значение Параметр Сумма ходов насоса
        /// </summary>
        /// <param name="v1">Параметр Ходы Насоса 1</param>
        /// <param name="v2">Параметр Ходы Насоса 2</param>
        public void Calculate(P0116 v1, P0117 v2)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    float val1 = 0, val2 = 0;
                    bool b = false;

                    if (!float.IsNaN(v1.Value))
                    {
                        val1 = v1.Value;
                        b = true;
                    }
                    if (!float.IsNaN(v2.Value))
                    {
                        val2 = v2.Value;
                        b = true;
                    }

                    if (b)
                        _value = val1 + val2;
                    else
                        _value = float.NaN;
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }
    }
}