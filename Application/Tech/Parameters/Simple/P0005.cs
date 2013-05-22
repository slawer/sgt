using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Высота талевого блока Датчик
    /// </summary>
    public class P0005 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P0005(Guid p_identifier)
            : base(p_identifier, "P0005", "Высота талевого блока Датчик")
        {
        }
    }
}