using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Обороты ротора АСУ
    /// </summary>
    public class P0017 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P0017(Guid p_identifier)
            : base(p_identifier, "P0017", "Обороты ротора АСУ")
        {
        }
    }
}