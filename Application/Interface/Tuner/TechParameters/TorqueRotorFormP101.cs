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
    public partial class TorqueRotorFormP101 : Form
    {
        protected SgtApplication _app = null;

        public TorqueRotorFormP101()
        {
            InitializeComponent();
            _app = SgtApplication.CreateInstance();
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TorqueRotorFormP101_Load(object sender, EventArgs e)
        {
            switch (_app.Technology.P0101.Source)
            {
                case P0101.SourceType.Analog:

                    radioButtonAnalog.Checked = true;
                    break;

                case P0101.SourceType.Asy:

                    radioButtonASY.Checked = true;
                    break;

                default:

                    radioButtonASY.Checked = false;
                    radioButtonAnalog.Checked = false;

                    break;
            }
        }

        /// <summary>
        /// сохраняем результат
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accept_Click(object sender, EventArgs e)
        {
            if (radioButtonAnalog.Checked)
            {
                _app.Technology.P0101.Source = P0101.SourceType.Analog;
            }
            else
                if (radioButtonASY.Checked)
                {
                    _app.Technology.P0101.Source = P0101.SourceType.Asy;
                }
                else
                    _app.Technology.P0101.Source = P0101.SourceType.Default;
        }
    }
}