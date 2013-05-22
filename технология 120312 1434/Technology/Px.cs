using System;

namespace Technology
{
    /// <summary>
    /// Реализует абстрактный параметр.
    /// Методы вычисляющие значение параметра должны быть статическими.
    /// Количество методов не ограничено.
    /// </summary>
    public class Px
    {
        protected static float _value;              // текущее значение параметра

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
        /// Вычисляет значение параметра используя только срез данных
        /// </summary>
        /// <param name="slice">Срез данных, на основе которого вычисляется значение параметра</param>
        /// <returns>Вычисленное значение параметра</returns>
        public static float Calculate(float[] slice)
        {
            return float.NaN;
        }

        /// <summary>
        /// Вычисляет значение параметра, используя срез данных и значение технологического параметра Px
        /// </summary>
        /// <param name="slice">Срез данных, на основе которого вычисляется значение параметра</param>
        /// <param name="p1">Технологический параметр Px, учавствующий в вычислении значения параметра</param>
        /// <returns>Вычисленное значение параметра</returns>
        public static float Calculate(float[] slice, Px p1)
        {
            return float.NaN;
        }

        /// <summary>
        /// Метод вычисляющий значение параметра на основе среза данных, 
        /// параметра Px и истории значений технологических параметров
        /// </summary>
        /// <param name="slice">Срез данных, на основе которого вычисляется значение параметра</param>
        /// <param name="p1">Технологический параметр Px, учавствующий в вычислении значения параметра</param>
        /// <param name="history">Абстрактный класс, позволяющий получить историю значений параметров</param>
        /// <returns>Вычисленное значение параметра</returns>
        public static float Calculate(float[] slice, Px p1, History history)
        {
            return float.NaN;
        }
    }
}