using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SGT
{
    public partial class CreateNewSessionForm : Form
    {
        SgtApplication _app = null;
        public CreateNewSessionForm()
        {
            InitializeComponent();
            _app = SgtApplication.CreateInstance();
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateNewSessionForm_Load(object sender, EventArgs e)
        {
            Work currentWork = _app.CurrentWork;
            if (currentWork != null)
            {
                Session curSession = currentWork.Current;
                if (curSession != null)
                {
                    int max = GetmaxNumber();
                    if (max != -1)
                    {
                        numericUpDown1.Minimum = curSession.Number + 1;
                        numericUpDown1.Maximum = numericUpDown1.Minimum + 100;

                        numericUpDown1.Value = curSession.Number + 1;
                    }
                }
                else
                {
                    int max = GetmaxNumber();
                    if (max != -1)
                    {
                        numericUpDown1.Minimum = max + 1;
                        numericUpDown1.Maximum = numericUpDown1.Minimum + 100;

                        numericUpDown1.Value = max + 1;
                    }
                }
            }
            else
            {
                MessageBox.Show(this, "Не созданно задание!" + Microsoft.VisualBasic.Constants.vbCrLf + "Осуществить начало рейса не представляется возможным.", 
                    "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }

        /// <summary>
        /// получить максимальное значений номера рейса
        /// </summary>
        /// <returns></returns>
        protected int GetmaxNumber()
        {
            try
            {
                Session[] sessions = _app.CurrentWork.Sessions;
                if (sessions != null)
                {
                    int max = 0;
                    foreach (Session session in sessions)
                    {
                        if (session.Number >= max)
                        {
                            max = session.Number;
                        }
                    }

                    return max;
                }
            }
            catch { }
            return -1;
        }

        /// <summary>
        /// ghjdthbnm dct b yfxfnm yjdsq htqc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accept_Click(object sender, EventArgs e)
        {
            Work currentWork = _app.CurrentWork;
            if (currentWork != null)
            {
                Session curSession = currentWork.Current;
                if (curSession != null)
                {
                    if ((int)numericUpDown1.Value <= curSession.Number)
                    {
                        MessageBox.Show(this, "Номер рейса указан не корректно", "Сообщение",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        DialogResult = DialogResult.None;
                        return;
                    }
                    else
                    {
                        newSession = new Session();

                        newSession.Description = textBox1.Text;
                        newSession.Number = (int)numericUpDown1.Value;
                    }
                }
                else
                {
                    newSession = new Session();

                    newSession.Description = textBox1.Text;
                    newSession.Number = (int)numericUpDown1.Value;
                }
            }
            else
                MessageBox.Show(this, "", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        Session newSession = null;
        public Session Session
        {
            get
            {
                return newSession;
            }
        }
    }
}