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
    public partial class SpeedRotorForm : Form
    {
        protected SgtApplication _app = null;
        public SpeedRotorForm()
        {
            InitializeComponent();
            _app = SgtApplication.CreateInstance();
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpeedRotorForm_Load(object sender, EventArgs e)
        {
            switch (_app.Technology.P0110.Source)
            {
                case P0110.SourceRotor.Analog:

                    radioButtonAnalog.Checked = true;
                    break;

                case P0110.SourceRotor.Asy:

                    radioButtonAsy.Checked = true;
                    break;

                case P0110.SourceRotor.Svp:

                    radioButtonSvp.Checked = true;
                    break;

                default:

                    radioButtonAsy.Checked = false;
                    radioButtonAnalog.Checked = false;

                    break;
            }
            
        }

        /// <summary>
        /// сохраняем выбор
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accept_Click(object sender, EventArgs e)
        {
            if (radioButtonAnalog.Checked)
            {
                _app.Technology.P0110.Source = P0110.SourceRotor.Analog;
            }
            else
                if (radioButtonAsy.Checked)
                {
                    _app.Technology.P0110.Source = P0110.SourceRotor.Asy;
                }
                else if (radioButtonSvp.Checked)
                {
                    _app.Technology.P0110.Source = P0110.SourceRotor.Svp;
                }
                else
                    _app.Technology.P0110.Source = P0110.SourceRotor.Default;
        }
    }
}