using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Уровень ДУ3 Датчик
    /// </summary>
    public class P07_2 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P07_2(Guid p_identifier)
            : base(p_identifier, "P07_2", "Уровень ДУ3 Датчик")
        {
        }
    }
}