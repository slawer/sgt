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
    public partial class WeightHookForm : Form
    {
        SgtApplication _app = null;

        public WeightHookForm()
        {
            InitializeComponent();

            _app = SgtApplication.CreateInstance();
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WeightHookForm_Load(object sender, EventArgs e)
        {
            switch (_app.Technology.P0102.Source)
            {
                case P0102.SensorType.Analog:

                    radioButtonVesYralmas.Checked = true;
                    break;

                case P0102.SensorType.Sensor:

                    radioButtonVesOreol.Checked = true;
                    break;

                default:

                    radioButtonVesOreol.Checked = false;
                    radioButtonVesYralmas.Checked = false;                    

                    break;
            }
        }

        /// <summary>
        /// сохраняем настройки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accept_Click(object sender, EventArgs e)
        {
            if (radioButtonVesYralmas.Checked)
            {
                _app.Technology.P0102.Source = P0102.SensorType.Analog;
            }
            else
                if (radioButtonVesOreol.Checked)
                {
                    _app.Technology.P0102.Source = P0102.SensorType.Sensor;
                }
        }
    }
}