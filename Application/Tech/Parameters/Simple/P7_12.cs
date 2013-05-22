using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Уровень ДУ13 Датчик
    /// </summary>
    public class P7_12 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P7_12(Guid p_identifier)
            : base(p_identifier, "P7_12", "Уровень ДУ13 Датчик")
        {
        }
    }
}