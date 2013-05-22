using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Уровень ДУ8 Датчик
    /// </summary>
    public class P07_7 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P07_7(Guid p_identifier)
            : base(p_identifier, "P07_7", "Уровень ДУ8 Датчик")
        {
        }
    }
}