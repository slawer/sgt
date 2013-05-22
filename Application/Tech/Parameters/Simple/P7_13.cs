using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Уровень ДУ14 Датчик
    /// </summary>
    public class P7_13 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P7_13(Guid p_identifier)
            : base(p_identifier, "P7_13", "Уровень ДУ14 Датчик")
        {
        }
    }
}