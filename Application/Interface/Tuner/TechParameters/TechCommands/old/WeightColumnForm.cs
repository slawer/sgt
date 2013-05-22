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
    public partial class WeightColumnForm : Form
    {
        SgtApplication _app = null;

        public WeightColumnForm()
        {
            InitializeComponent();

            _app = SgtApplication.CreateInstance();
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WeightColumnForm_Load(object sender, EventArgs e)
        {
            textBoxVesKol.Text = _app.Technology.P0200.Value.ToString();
        }

        /// <summary>
        /// проверяем и сохраняем
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accept_Click(object sender, EventArgs e)
        {
            float val = SgtApplication.ParseSingle(textBoxVesKol.Text);
            if (float.IsNaN(val) == true)
            {
                MessageBox.Show(this, "значение веса колоны указано не корректно",
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                DialogResult = System.Windows.Forms.DialogResult.None;

                textBoxVesKol.Select();
                textBoxVesKol.SelectAll();

                return;
            }

            _app.Technology.P0200.Calculate(val);
        }
    }
}