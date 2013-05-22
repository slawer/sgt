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
    public partial class IdealPairForm : Form
    {
        public IdealPairForm()
        {
            InitializeComponent();
        }

        private void accept_Click(object sender, EventArgs e)
        {
            float val = SgtApplication.ParseSingle(textBoxFlow.Text);
            if (float.IsNaN(val))
            {
                MessageBox.Show(this, "Значение расхода указано не корректно",
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                DialogResult = System.Windows.Forms.DialogResult.None;

                textBoxFlow.Select();
                textBoxFlow.SelectAll();

                return;
            }

            val = SgtApplication.ParseSingle(textBoxDiametr.Text);
            if (float.IsNaN(val))
            {
                MessageBox.Show(this, "Значение расхода указано не корректно",
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                DialogResult = System.Windows.Forms.DialogResult.None;

                textBoxDiametr.Select();
                textBoxDiametr.SelectAll();

                return;
            }
        }
    }
}