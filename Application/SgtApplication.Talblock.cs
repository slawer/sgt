using System;
using System.Threading;

namespace SGT
{
    public partial class SgtApplication
    {
        protected int d_number = 1;              // номер устройства которому отсылать команду тальблок
        protected int k_talblock = 1000;         // коэффициент тальблока

        protected ReaderWriterLockSlim c_slim;

        /// <summary>
        /// Определяет коэффициент пересчета значения тальблока
        /// </summary>
        public int KoefTalblock
        {
            get
            {
                if (c_slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return k_talblock;
                    }
                    finally
                    {
                        c_slim.ExitReadLock();
                    }
                }

                return 1000;
            }

            set
            {
                if (c_slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        k_talblock = value;
                    }
                    finally
                    {
                        c_slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет номер устройства, которому отправлять команду Тальблок
        /// </summary>
        public int DeviceNumber
        {
            get
            {
                if (c_slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return d_number;
                    }
                    finally
                    {
                        c_slim.ExitReadLock();
                    }
                }

                return -1;
            }

            set
            {
                if (c_slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        d_number = value;
                    }
                    finally
                    {
                        c_slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Получить команду Тальблок
        /// </summary>
        /// <returns>Пакет в нотации DSN Тальблок, для отправки серверу данных</returns>
        protected string GetTalblockCommand(float talblock)
        {
            try
            {

                if (DeviceNumber > -1 && DeviceNumber < 32)
                {
                    if (KoefTalblock > 0 && KoefTalblock < 10000)
                    {
                        int talblock_value = (int)(talblock * k_talblock);

                        string total = "@JOB#000#";

                        total += string.Format("{0:X2}", d_number);
                        total += "0B022004C140";

                        total += string.Format("{0:X4}", talblock_value);

                        return (total + "00$");
                    }
                }
            }
            catch { }
            return string.Empty;
        }

        /// <summary>
        /// Выполнить команду Тальблок
        /// </summary>
        /// <param name="talblock">Значени параметра тальблок</param>
        public void DoTalblock(float tal)
        {
            try
            {
                string talblock = GetTalblockCommand(tal);
                if (talblock != string.Empty)
                {
                    commutator.SendTcp(talblock);
                }
            }
            catch { }
        }
    }
}