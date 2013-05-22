using System;
using System.Threading;

namespace Technology
{
    /// <summary>
    /// Реализует генератор среза данных для технологии
    /// </summary>
    public class Generator
    {        
        protected Timer timer;                  // генерирует события по которому осуществлять генерацию среза
        protected Mutex t_mutex;                // синхронизирует события таймера

        protected Random rnd;                   // генератор случайных чисел
        protected float[] data;                 // срез данных

        /// <summary>
        /// Возникает когда сгенерирован срез данных
        /// </summary>
        public event GenerationComplete complete;

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public Generator()
        {
            t_mutex = new Mutex();
            rnd = new Random(100000);

            timer = new Timer(callback, null, Timeout.Infinite, 100);

            data = new float[256];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = 0.0f;
            }
        }

        /// <summary>
        /// Запустить генератор срезов данных
        /// </summary>
        /// <param name="period">Частота генерации срезов данных</param>
        public void Start(int period)
        {
            timer.Change(0, period);
        }

        /// <summary>
        /// Генерирует срез данных
        /// </summary>
        /// <param name="state">Не используется.</param>
        protected void callback(object state)
        {
            bool blocked = false;
            try
            {
                if (t_mutex.WaitOne(0))
                {
                    blocked = true;
                    for (int i = 0; i < data.Length; i++)
                    {
                        data[i] = (float)(rnd.NextDouble() * rnd.Next(10000));
                    }

                    if (complete != null)
                    {
                        complete(this, new GenerationEventArgs(data));
                    }
                }
            }
            finally
            {
                if (blocked) t_mutex.ReleaseMutex();
            }
        }
    }

    /// <summary>
    /// Реализует передачу параметров генератора среза данных
    /// </summary>
    public class GenerationEventArgs : EventArgs
    {
        private float[] slice;          // срез данных

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="data">Передаваемый срез данных</param>
        public GenerationEventArgs(float[] data)
        {
            if (data != null && data.Length > 0)
            {
                slice = new float[data.Length];
                data.CopyTo(slice, 0);
            }
        }

        /// <summary>
        /// Сгенерированный срез данных
        /// </summary>
        public float[] Slice
        {
            get
            {
                return slice;
            }
        }
    }

    public delegate void GenerationComplete(object sender, GenerationEventArgs e);
}