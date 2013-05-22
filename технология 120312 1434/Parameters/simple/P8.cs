using System;

namespace Technology
{
    /// <summary>
    /// Реализует параметр Ходы насоса 1
    /// Параметры Ходы насоса ххх образуют группу из 2 датчиков
    /// </summary>
    public class P8
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
        /// Вычисляет текущее значение параметра Ходы насоса 1
        /// </summary>
        /// <param name="slice">Срез данных, на основе которого вычисляется значение параметра</param>
        /// <returns>Вычисленное значение параметра</returns>
        public static float Calculate(float[] slice)
        {
            return slice[PNumber.Ходы_насоса_1];
        }
    }
}