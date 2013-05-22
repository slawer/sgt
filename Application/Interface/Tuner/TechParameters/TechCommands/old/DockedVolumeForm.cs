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
    public partial class DockedVolumeForm : Form
    {
        SgtApplication _app = null;

        public DockedVolumeForm()
        {
            InitializeComponent();
            _app = SgtApplication.CreateInstance();
        }

        /// <summary>
        /// Загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DockedVolumeForm_Load(object sender, EventArgs e)
        {
            textBoxFix.Text = string.Format("{0:F2}", _app.Technology.P0106.StartingPoint);
        }

        /// <summary>
        /// сохраняем значение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accept_Click(object sender, EventArgs e)
        {
            float val = SgtApplication.ParseSingle(textBoxFix.Text);
            if (float.IsNaN(val))
            {
                MessageBox.Show(this, "значение указано не корректно",
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                DialogResult = System.Windows.Forms.DialogResult.None;

                textBoxFix.Select();
                textBoxFix.SelectAll();

                return;
            }

            _app.Technology.P0106.StartingPoint = val;
        }
    }
}