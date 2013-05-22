using System;

namespace Technology
{
    /// <summary>
    /// Реализует параметр Уровень ДУ1
    /// Параметры Уровень ДУххх образуют группу из 14 датчиков (реально используется 7)
    /// </summary>
    public class P7
    {
        protected float _value;              // текущее значение параметра

        /// <summary>
        /// Определяет текущее значение параметра
        /// </summary>
        public float Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
            }
        }

        /// <summary>
        /// Вычисляет текущее значение параметра Уровень ДУ1
        /// </summary>
        /// <param name="slice">Срез данных, на основе которого вычисляется значение параметра</param>
        /// <returns>Вычисленное значение параметра</returns>
        public static float Calculate(float[] slice)
        {
            return slice[PNumber.Уровень_ДУ1];
        }
    }
}