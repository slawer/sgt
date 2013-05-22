using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Вес колонны.
    /// </summary>
    public class P0200 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор параметра</param>
        public P0200(Guid p_identifier)
            : base(p_identifier, "P0200", "Вес колонны")
        {
            simple = false;
        }

        /// <summary>
        /// Сбросить точку отсчета
        /// </summary>
        public void ResetStartingPoint()
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    _value = float.NaN;
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Сбросить точку отсчета
        /// <param name="newValue">Значение новой точки отсчета</param>
        /// </summary>
        public void ResetStartingPoint(float newValue)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    _value = newValue;
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Вычисляет новое текущее значение параметра Вес колонны
        /// </summary>
        /// <param name="Вес">v1</param>
        public void Calculate(P0102 v1)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    if (!float.IsNaN(v1.Value))
                    {
                        if (float.IsNaN(_value))
                        {
                            _value = v1.Value;
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