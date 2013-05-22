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
    public partial class FlowInletForm : Form
    {
        SgtApplication _app = null;

        public FlowInletForm()
        {
            InitializeComponent();
            _app = SgtApplication.CreateInstance();
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FlowInletForm_Load(object sender, EventArgs e)
        {
            if (_app.Technology.P0114.Source == P0114.SourceFlow.Pump)
            {
                radioButtonOreol.Checked = true;
            }
            else
                radioButtonHodam.Checked = true;
        }

        /// <summary>
        /// сохраняем
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accept_Click(object sender, EventArgs e)
        {
            if (radioButtonOreol.Checked)
            {
                _app.Technology.P0114.Source = P0114.SourceFlow.Pump;
            }
            else
                _app.Technology.P0114.Source = P0114.SourceFlow.Moving;
        }
    }
}