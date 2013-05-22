using System;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SGT
{
    /// <summary>
    /// Точка входа в программу
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Идентификационный номер системного мьютекса, по которому определяется запещено или нет приложение
        /// </summary>
        private const string identifier = "0cb7ff53-14a7-40c8-866f-06f1a3510bac";

        private static Mutex mutex = null;              // определяет запуск приложения
        private static bool isNotRunning = false;       // содержит значение на основе которого определяет возможность запуска программы        

        private static SgtApplication app = null;       // основное приложение

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                AppDomain currentDomain = AppDomain.CurrentDomain;
                currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);

                mutex = new Mutex(true, identifier, out isNotRunning);
                if (isNotRunning)
                {
                    app = SgtApplication.CreateInstance();
                    if (app != null)
                    {
                        app.Load();
                        app.Commutator.DeSerialize();

                        app.Connect();

                        System.Windows.Forms.Application.ApplicationExit += new EventHandler(Application_ApplicationExit);

                        System.Windows.Forms.Application.EnableVisualStyles();
                        System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

                        System.Windows.Forms.Application.Run(new mainForm());
                    }
                }
                else
                    MessageBox.Show("Приложение уже запущено", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteToLog(null, new ErrorArgs(ex.Message, ErrorType.Unknown));
            }
        }

        /// <summary>
        /// Обработчик ошибки
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="args">Аргументы события</param>
        private static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            try
            {
                Exception e = args.ExceptionObject as Exception;
                if (e != null)
                {
                    ErrorHandler.WriteToLog(sender,
                        new ErrorArgs(string.Format("{0} stack {1} is terminating {2}", e.Message, e.StackTrace, args.IsTerminating), ErrorType.Fatal));
                }
                else
                {
                    ErrorHandler.WriteToLog(sender, new ErrorArgs("e == null", ErrorType.Fatal));

                }
            }
            catch { }
        }

        /// <summary>
        /// приложение завершает свою работу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            app.Save();
        }
    }
}