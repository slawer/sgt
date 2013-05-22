using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Над забоем в метрах
    /// </summary>
    public class P0211 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор параметра</param>
        public P0211(Guid p_identifier)
            : base(p_identifier, "P0211", "Над забоем в метрах")
        {
            simple = false;
        }

        /// <summary>
        /// Вычисляет текущее значение параметра Над забоем
        /// </summary>
        /// <param name="Забой">Глубина забоя в текущий момент</param>
        /// <param name="Инструмент">Положение инструмента в текущий момент</param>
        public void Calculate(P0205 v1, P0204 v2)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    if (float.IsNaN(v1.Value) || float.IsNaN(v2.Value))
                    {
                        _value = float.NaN;
                    }
                    else
                        _value = v1.Value - v2.Value;
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }
    }
}