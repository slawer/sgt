using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace SGT
{
    public partial class CommandP0212Form : Form
    {
        SgtApplication _app = null;
        
        public CommandP0212Form()
        {
            InitializeComponent();
            _app = SgtApplication.CreateInstance();
        }

        private void CommandP0212Form_Load(object sender, EventArgs e)
        {
            TimeSpan span = new TimeSpan(_app.Technology.P0212.ExactTimeCirculation);

            textBoxHours.Text = span.Hours.ToString();
            textBoxMinutes.Text = span.Minutes.ToString();
        }

        private void accept_Click(object sender, EventArgs e)
        {
            float hours = SgtApplication.ParseSingle(textBoxHours.Text);
            float minutes = SgtApplication.ParseSingle(textBoxMinutes.Text);

            if (float.IsNaN(hours) == false && float.IsNaN(minutes) == false)
            {
                TimeSpan span = new TimeSpan((int)hours, (int)minutes, 0);
                _app.Technology.P0212.Reset(span.Ticks);
            }
            else
            {
                if (float.IsNaN(hours))
                {
                    textBoxHours.Select();
                    textBoxHours.SelectAll();

                    MessageBox.Show(this, "Значение Часы указано не корректно",
                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                if (float.IsNaN(minutes))
                {
                    textBoxHours.Select();
                    textBoxHours.SelectAll();

                    MessageBox.Show(this, "Значение Минуты указано не корректно",
                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                DialogResult = System.Windows.Forms.DialogResult.None;
            }
        }
    }
}