using System;

namespace Technology
{
    /// <summary>
    /// Реализует параметр Газ Д1
    /// Параметры Газ Дххх образуют группу из 5 датчиков
    /// </summary>
    public class P6
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
        /// Вычисляет текущее значение параметра Газ Д1
        /// </summary>
        /// <param name="slice">Срез данных, на основе которого вычисляется значение параметра</param>
        /// <returns>Вычисленное значение параметра</returns>
        public static float Calculate(float[] slice)
        {
            return slice[PNumber.Газ_Д1];
        }
    }
}