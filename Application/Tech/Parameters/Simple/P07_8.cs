using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Уровень ДУ9 Датчик
    /// </summary>
    public class P07_8 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P07_8(Guid p_identifier)
            : base(p_identifier, "P07_8", "Уровень ДУ9 Датчик")
        {
        }
    }
}