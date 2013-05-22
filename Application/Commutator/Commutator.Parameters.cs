using System;
using System.Threading;
using System.Net.NetworkInformation;

using Tcp;
using Buffering;

using WCF;
using WCF.WCF_Client;

namespace SGT
{
    /// <summary>
    /// Реализует коммутатор, который отвечает за 
    /// соединение, прием и передачу данных от devMan
    /// </summary>
    public partial class Commutator
    {
        protected static Parameter[] parameters;            // параметры от devMan
        protected static ReaderWriterLockSlim p_slim;       // синхронизатор параметров

        protected RSliceBuffer buffer = null;               // буфер, который хранит срезы данных пришедших от DevMan2;

        /// <summary>
        /// Извлечь данные из указанного диапазона времени
        /// </summary>
        /// <param name="startTime">Начало временного диапазона</param>
        /// <param name="finishTime">Конец временного диапазона</param>
        /// <returns>Массив срезов данных за указанный диапазон времени
        /// или же null если данных за указанный диапазон времени нет.
        /// </returns>
        public Slice[] GetDataFromBuffer(DateTime startTime, DateTime finishTime)
        {
            try
            {
                Slice[] slices = buffer.FindFromEnd(startTime, finishTime);
                if (slices != null)
                {
                    return slices;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteToLog(this, new ErrorArgs(ex.Message, ErrorType.NotFatal));
            }

            return null;
        }

        /// <summary>
        /// Извлечь данные из указанного диапазона времени
        /// </summary>
        /// <param name="startTime">Начало временного диапазона</param>
        /// <param name="finishTime">Конец временного диапазона</param>
        /// <returns>Массив срезов данных за указанный диапазон времени
        /// или же null если данных за указанный диапазон времени нет.
        /// </returns>
        public Slice[] GetDataFromBuffer(DateTime startTime, DateTime finishTime, int P1, int P2, int P3, int P4, int P5)
        {
            try
            {
                Slice[] slices = buffer.FindFromEnd(startTime, finishTime, P1, P2, P3, P4, P5);
                if (slices != null)
                {
                    return slices;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteToLog(this, new ErrorArgs(ex.Message, ErrorType.NotFatal));
            }

            return null;
        }

        /// <summary>
        /// получить минимальное время параметра
        /// </summary>
        /// <param name="number">Номер параметра</param>
        /// <returns>Найденное время параметра</returns>
        public DateTime MinTimeParameter()
        {
            try
            {
                return buffer.GetMinTime();
            }
            catch { }
            return DateTime.MinValue;
        }
    }
}