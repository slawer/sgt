using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Объём ДУ8 Датчик
    /// </summary>
    public class P09_7 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P09_7(Guid p_identifier)
            : base(p_identifier, "P09_7", "Объём ДУ8 Датчик")
        {
        }

        /// <summary>
        /// Вычислить значение технологического параметра Объём ДУ8 Датчик
        /// </summary>
        /// <param name="slice">Срез данных</param>
        public override void Calculate(float[] slice)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    if (p_number > -1 && p_number < slice.Length)
                    {
                        float v = slice[p_number];
                        if (!float.IsNaN(v))
                        {
                            _value = v;
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