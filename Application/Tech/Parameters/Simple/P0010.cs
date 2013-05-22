using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Расход на входе Датчик
    /// </summary>
    public class P0010 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P0010(Guid p_identifier)
            : base(p_identifier, "P0010", "Расход на входе Датчик")
        {
        }
    }
}