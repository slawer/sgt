using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Уровень ДУ12 Датчик
    /// </summary>
    public class P7_11 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P7_11(Guid p_identifier)
            : base(p_identifier, "P7_11", "Уровень ДУ12 Датчик")
        {
        }
    }
}