﻿using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Объём ДУ4 Датчик
    /// </summary>
    public class P09_3 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P09_3(Guid p_identifier)
            : base(p_identifier, "P09_3", "Объём ДУ4 Датчик")
        {
        }

        /// <summary>
        /// Вычислить значение технологического параметра Объём ДУ4 Датчик
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