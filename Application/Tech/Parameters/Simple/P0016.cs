using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Крутящий момент ротора АСУ
    /// </summary>
    public class P0016 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P0016(Guid p_identifier)
            : base(p_identifier, "P0016", "Крутящий момент ротора АСУ")
        {
        }
    }
}