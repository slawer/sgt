using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Крутящий момент ротора Аналоговый
    /// </summary>
    public class P0002 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P0002(Guid p_identifier)
            : base(p_identifier, "P0002", "Крутящий момент ротора Аналоговый")
        {
        }
    }
}