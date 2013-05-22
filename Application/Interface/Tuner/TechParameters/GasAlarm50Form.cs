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
    public partial class GasAlarmForm50 : Form
    {
        SgtApplication _app = null;
        public GasAlarmForm50()
        {
            InitializeComponent();
            _app = SgtApplication.CreateInstance();
        }

        /// <summary>
        /// Загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GasAlarmForm_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = _app.Technology.P0108.Gas_Sensor_1;
            checkBox2.Checked = _app.Technology.P0108.Gas_Sensor_2;

            checkBox3.Checked = _app.Technology.P0108.Gas_Sensor_3;
            checkBox4.Checked = _app.Technology.P0108.Gas_Sensor_4;

            checkBox5.Checked = _app.Technology.P0108.Gas_Sensor_5;

            checkBox6.Checked = _app.Technology.P0108.Gas_Sensor_6;
            checkBox7.Checked = _app.Technology.P0108.Gas_Sensor_7;

            checkBox8.Checked = _app.Technology.P0108.Gas_Sensor_8;
            checkBox9.Checked = _app.Technology.P0108.Gas_Sensor_9;

            checkBox10.Checked = _app.Technology.P0108.Gas_Sensor_10;
        }


        /// <summary>
        /// сохраняем
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accept_Click(object sender, EventArgs e)
        {
            _app.Technology.P0108.Gas_Sensor_1 = checkBox1.Checked;
            _app.Technology.P0108.Gas_Sensor_2 = checkBox2.Checked;

            _app.Technology.P0108.Gas_Sensor_3 = checkBox3.Checked;
            _app.Technology.P0108.Gas_Sensor_4 = checkBox4.Checked;

            _app.Technology.P0108.Gas_Sensor_5 = checkBox5.Checked;
            _app.Technology.P0108.Gas_Sensor_6 = checkBox6.Checked;

            _app.Technology.P0108.Gas_Sensor_7 = checkBox7.Checked;
            _app.Technology.P0108.Gas_Sensor_8 = checkBox8.Checked;

            _app.Technology.P0108.Gas_Sensor_9 = checkBox9.Checked;
            _app.Technology.P0108.Gas_Sensor_10 = checkBox10.Checked;
        }
    }
}