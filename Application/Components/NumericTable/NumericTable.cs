using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace NumericTable
{
    public partial class NumericTable : UserControl
    {
        protected Panel panel = null;       // реализует панель

        public NumericTable()
        {
            InitializeComponent();

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            panel = new Panel(this);
        }

        /// <summary>
        /// Возвращяет панель для числового табло
        /// </summary>
        public Panel Panel
        {
            get
            {
                return panel;
            }
        }

        private void настройкаЕлементовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsNumericForm frm = new OptionsNumericForm(this);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
            }
        }
    }
}