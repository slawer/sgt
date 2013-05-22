using System;
using System.Net.NetworkInformation;

using Tcp;

namespace SGT
{
    /// <summary>
    /// Реализует коммутатор, который отвечает за 
    /// соединение, прием и передачу данных от devMan
    /// </summary>
    public partial class Commutator
    {
        protected devTcpManager client;                 // работает по старому TCP каналу обмена данными

        /// <summary>
        /// Оправить данные серверу данных по старому каналу обмена данными
        /// </summary>
        /// <param name="data"></param>
        public void SendTcp(string data)
        {
            try
            {
                client.Client.Send(System.Text.Encoding.ASCII.GetBytes(data));
            }
            catch { }
        }
    }
}