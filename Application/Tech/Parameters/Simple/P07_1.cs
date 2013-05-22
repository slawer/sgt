using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Уровень ДУ2 Датчик
    /// </summary>
    public class P07_1 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P07_1(Guid p_identifier)
            : base(p_identifier, "P07_1", "Уровень ДУ2 Датчик")
        {
        }
    }
}