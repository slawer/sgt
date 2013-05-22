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
    public partial class CommandP0203Form : Form
    {
        SgtApplication _app = null;

        public CommandP0203Form()
        {
            InitializeComponent();
            _app = SgtApplication.CreateInstance();
        }

        private void CommandP0203Form_Load(object sender, EventArgs e)
        {
            textBoxNomer.Text = string.Format("{0:F2}", _app.Technology.P0203.Value);
        }

        private void accept_Click(object sender, EventArgs e)
        {
            float cur_val = SgtApplication.ParseSingle(textBoxNomer.Text);
            if (float.IsNaN(cur_val))
            {
                MessageBox.Show(this, "Значение Изменение потока на выходе указано не корректно",
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                textBoxNomer.Select();
                textBoxNomer.SelectAll();

                DialogResult = System.Windows.Forms.DialogResult.None;
            }
            else
            {
                _app.Technology.P0203.Calculate(cur_val);
            }
        }
    }
}