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
    public partial class DrillingShaftTubeForm : Form
    {
        public DrillingShaftTubeForm()
        {
            InitializeComponent();
        }

        private void textBoxLenght_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// проверяем введенные данные
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accept_Click(object sender, EventArgs e)
        {
            float n_val = SgtApplication.ParseSingle(textBoxLenght.Text);
            if (float.IsNaN(n_val) && n_val > -1)
            {
                textBoxLenght.Focus();
                textBoxLenght.SelectAll();

                MessageBox.Show(this, "Длина трубки указана не корректно", "Сообщение",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                DialogResult = DialogResult.None;
                return;
            }

            float n_val1 = SgtApplication.ParseSingle(textBoxNumber.Text);
            if (float.IsNaN(n_val1) && n_val1 > -1)
            {
                textBoxNumber.Focus();
                textBoxNumber.SelectAll();

                MessageBox.Show(this, "Номер свечи указан не корректно", "Сообщение",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                DialogResult = DialogResult.None;
                return;
            }
        }
    }
}