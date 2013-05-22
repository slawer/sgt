using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Уровень ДУ4 Датчик
    /// </summary>
    public class P07_3 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P07_3(Guid p_identifier)
            : base(p_identifier, "P07_3", "Уровень ДУ4 Датчик")
        {
        }
    }
}