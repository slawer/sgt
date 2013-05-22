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
    public partial class CommandP0205Form : Form
    {
        protected SgtApplication _app = null;

        public CommandP0205Form()
        {
            InitializeComponent();

            _app = SgtApplication.CreateInstance();
        }

        /// <summary>
        /// проверяем и сохраняем
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accept_Click(object sender, EventArgs e)
        {
            float n_val = SgtApplication.ParseSingle(textBox1.Text);
            if (float.IsNaN(n_val))
            {
                MessageBox.Show(this, "Глубина забоя указана не корректно",
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                textBox1.Select();
                textBox1.SelectAll();

                DialogResult = System.Windows.Forms.DialogResult.None;
            }
            else
            {
                _app.Technology.P0202.ModeProccess = P0202.TModeProcess.mpCMDmodifyDepth;
                _app.Technology.P0205.Calculate(n_val);
            }
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandP0205Form_Load(object sender, EventArgs e)
        {
            textBox1.Text = string.Format("{0:F2}", _app.Technology.P0205.Value);
        }
    }
}