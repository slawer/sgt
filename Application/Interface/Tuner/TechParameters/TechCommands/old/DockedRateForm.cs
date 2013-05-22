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
    public partial class DockedRateForm : Form
    {
        SgtApplication _app = null;

        public DockedRateForm()
        {
            InitializeComponent();

            _app = SgtApplication.CreateInstance();
        }

        /// <summary>
        /// Загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DockedRateForm_Load(object sender, EventArgs e)
        {
            textBoxFixRas.Text = string.Format("{0:F2}", _app.Technology.P0105.StartingPoint);
        }

        /// <summary>
        /// проверяем
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accept_Click(object sender, EventArgs e)
        {
            float val = SgtApplication.ParseSingle(textBoxFixRas.Text);
            if (float.IsNaN(val))
            {
                MessageBox.Show(this, "значение указано не корректно",
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                DialogResult = System.Windows.Forms.DialogResult.None;

                textBoxFixRas.Select();
                textBoxFixRas.SelectAll();

                return;
            }

            _app.Technology.P0105.StartingPoint = val;

        }
    }
}