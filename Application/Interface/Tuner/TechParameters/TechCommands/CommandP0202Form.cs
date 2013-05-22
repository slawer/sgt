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
    public partial class CommandP0202Form : Form
    {
        SgtApplication _app = null;
        
        public CommandP0202Form()
        {
            InitializeComponent();
            _app = SgtApplication.CreateInstance();
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandP0202Form_Load(object sender, EventArgs e)
        {
            textBoxDlina.Text = string.Format("{0:F2}", _app.Technology.P0202.Value);
        }

        /// <summary>
        /// проверяем введенное значение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accept_Click(object sender, EventArgs e)
        {
            float cur_val = SgtApplication.ParseSingle(textBoxDlina.Text);
            if (float.IsNaN(cur_val))
            {
                MessageBox.Show(this, "Значение Изменение потока на выходе указано не корректно",
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                textBoxDlina.Select();
                textBoxDlina.SelectAll();

                DialogResult = System.Windows.Forms.DialogResult.None;
            }
            else
            {
                _app.Technology.P0202.Calculate(cur_val);
                _app.Technology.P0202.ModeProccess = P0202.TModeProcess.mpSetUser;
            }
        }
    }
}