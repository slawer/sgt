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
    public partial class CommandP0207Form : Form
    {
        SgtApplication _app = null;

        public CommandP0207Form()
        {
            InitializeComponent();

            _app = SgtApplication.CreateInstance();
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FeedForm_Load(object sender, EventArgs e)
        {
            textBoxPodasa.Text = string.Format("{0:F2}", _app.Technology.P0207.StartingPoint);
        }

        private void set_new_btn_Click(object sender, EventArgs e)
        {
            float cur_val = SgtApplication.ParseSingle(textBoxPodasa.Text);
            if (float.IsNaN(cur_val))
            {
                MessageBox.Show(this, "Значение Изменение потока на выходе указано не корректно",
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                textBoxPodasa.Select();
                textBoxPodasa.SelectAll();

                DialogResult = System.Windows.Forms.DialogResult.None;
            }
            else
            {
                _app.Technology.P0207.ResetStartingPoint(cur_val);
            }
        }
    }
}