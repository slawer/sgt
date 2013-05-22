using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Уровень ДУ11 Датчик
    /// </summary>
    public class P7_10 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P7_10(Guid p_identifier)
            : base(p_identifier, "P7_10", "Уровень ДУ11 Датчик")
        {
        }
    }
}