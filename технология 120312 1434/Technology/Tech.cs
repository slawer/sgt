using System;

namespace Technology
{
    /// <summary>
    /// Реализует технологию бурения
    /// </summary>
    public class Tech
    {
        protected P1 p1 = null;         // Вес на крюке
        protected P2 p2 = null;         // Крутящий момент ротора

        protected P3 p3 = null;         // Поток на выходе
        protected P4 p4 = null;         // Давление

        protected P5 p5 = null;         // Высота талевого блока
        protected P6 p6 = null;         // Газ Д1

        protected P7 p7 = null;         // Уровень ДУ1
        protected P8 p8 = null;         // Ходы насоса 1

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public Tech()
        {
            p1 = new P1();
            p2 = new P2();

            p3 = new P3();
            p4 = new P4();
            
            p5 = new P5();
            p6 = new P6();
            
            p7 = new P7();
            p8 = new P8();
        }

        /// <summary>
        /// Вычислить технологические параметры
        /// </summary>
        /// <param name="slice">Срез данных</param>
        public void Calculate(float[] slice)
        {
            p1.Value = P1.Calculate(slice);
            p2.Value = P2.Calculate(slice);

            p3.Value = P3.Calculate(slice);
            p4.Value = P4.Calculate(slice);
            
            p5.Value = P5.Calculate(slice);
            p6.Value = P6.Calculate(slice);
            
            p7.Value = P7.Calculate(slice);
            p8.Value = P8.Calculate(slice);
        }
    }
}