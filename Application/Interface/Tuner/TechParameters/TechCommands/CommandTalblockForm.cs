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
    public partial class CommandTalblockForm : Form
    {
        protected SgtApplication _app;

        public CommandTalblockForm()
        {
            InitializeComponent();
            _app = SgtApplication.CreateInstance();
        }

        private void accept_Click(object sender, EventArgs e)
        {
            float val = SgtApplication.ParseSingle(textBox1.Text);
            if (float.IsNaN(val) == false)
            {
                Parameter parameter = _app.GetParameter(_app.Technology.P0005.Identifier);
                if (parameter != null)
                {
                    float signal = parameter.Transformation.GetSignal(val);
                    _app.DoTalblock(signal);
                }
            }
            else
            {
                MessageBox.Show(this, "Введено не корректное значение тальблока",
                    "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                DialogResult = DialogResult.None;
            }
        }
    }
}