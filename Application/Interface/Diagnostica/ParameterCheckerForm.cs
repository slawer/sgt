using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SGT
{
    public partial class ParameterCheckerForm : Form
    {
        private SgtApplication _app;

        public ParameterCheckerForm()
        {
            InitializeComponent();

            _app = SgtApplication.CreateInstance();
            SetExStyles();
        }

        [DllImport("User32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int uMsg, IntPtr wParam, IntPtr lParam);

        const int LVM_FIRST = 0x1000;

        int LVM_SETEXTENDEDLISTVIEWSTYLE = (LVM_FIRST + 54);
        int LVM_GETEXTENDEDLISTVIEWSTYLE = (LVM_FIRST + 55);
        int LVS_EX_DOUBLEBUFFER = 0x00010000;

        public void SetExStyles()
        {
            IntPtr styles = SendMessage(listViewParameters.Handle, (int)LVM_GETEXTENDEDLISTVIEWSTYLE,
                IntPtr.Zero, IntPtr.Zero);

            styles = (IntPtr)(styles.ToInt32() | LVS_EX_DOUBLEBUFFER);
            SendMessage(listViewParameters.Handle, (int)LVM_SETEXTENDEDLISTVIEWSTYLE, IntPtr.Zero, styles);
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParameterCheckerForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (_app != null)
                {
                    Parameter[] parameters = _app.Commutator.Parameters;
                    if (parameters != null)
                    {
                        foreach (Parameter parameter in parameters)
                        {
                            if (parameter.Description != "-----" && parameter.Name != "Параметр не определен")
                            {
                                InsertToList(parameter);
                            }
                        }
                    }
                }

                timer1_Tick(timer1, EventArgs.Empty);
                timer1.Start();
            }
            catch { }
        }

        /// <summary>
        /// добавить параметр в список
        /// </summary>
        /// <param name="parameter"></param>
        private void InsertToList(Parameter parameter)
        {
            int count = listViewParameters.Items.Count + 1;

            ListViewItem item = new ListViewItem(count.ToString());

            ListViewItem.ListViewSubItem des = new ListViewItem.ListViewSubItem(item, parameter.Name);
            ListViewItem.ListViewSubItem status = new ListViewItem.ListViewSubItem(item, "-----");

            item.Tag = parameter;

            item.SubItems.Add(des);
            item.SubItems.Add(status);

            listViewParameters.Items.Add(item);
        }

        private Mutex cMutex = new Mutex();

        /// <summary>
        /// проверяем текущее состояние параметров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                //listViewParameters.BeginUpdate();
                foreach (ListViewItem item in listViewParameters.Items)
                {
                    if (item.Tag != null)
                    {
                        Parameter parameter = item.Tag as Parameter;
                        if (parameter != null)
                        {
                            if (parameter.IsValidValue)
                            {
                                if (parameter.IsControlAlarm)
                                {
                                    if (parameter.CalculatedValue >= parameter.Alarm)
                                    {
                                        if (item.BackColor != Color.Salmon)
                                        {
                                            item.BackColor = Color.Salmon;
                                        }

                                        if (parameter.IsControlMaximum &&
                                            parameter.CalculatedValue >= parameter.Range.Max)
                                        {
                                            if (item.SubItems[2].Text != "Аварийное/Максимальное")
                                            {
                                                item.SubItems[2].Text = "Аварийное/Максимальное";
                                            }
                                        }
                                        else
                                            if (item.SubItems[2].Text != "Аварийное")
                                            {
                                                item.SubItems[2].Text = "Аварийное";
                                            }

                                        continue;
                                    }
                                }

                                if (parameter.IsControlMaximum)
                                {
                                    if (parameter.CalculatedValue >= parameter.Range.Max)
                                    {
                                        if (item.BackColor != Color.DarkOrange || item.SubItems[2].Text != "Максимальное")
                                        {
                                            item.BackColor = Color.DarkOrange;
                                            item.SubItems[2].Text = "Максимальное";
                                        }

                                        continue;
                                    }
                                }

                                if (parameter.IsControlMinimum)
                                {
                                    if (parameter.CalculatedValue <= parameter.Range.Min)
                                    {
                                        if (item.BackColor != Color.Orchid || item.SubItems[2].Text != "Минимальное")
                                        {
                                            item.BackColor = Color.Orchid;
                                            item.SubItems[2].Text = "Минимальное";
                                        }

                                        continue;
                                    }
                                }

                                if (item.SubItems[2].Text != "Включен")
                                {
                                    item.SubItems[2].Text = "Включен";
                                    item.BackColor = listViewParameters.BackColor;
                                }
                            }
                            else
                            {
                                if (item.BackColor != Color.Salmon || item.SubItems[2].Text != "Отключен")
                                {
                                    item.BackColor = Color.Goldenrod;
                                    item.SubItems[2].Text = "Отключен";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteToLog(this, new ErrorArgs(ex.Message, ErrorType.Unknown));
            }
            finally
            {
                //listViewParameters.EndUpdate();
            }
        }
    }
}