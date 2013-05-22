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
    public partial class VolumeSolutionTanksForm : Form
    {
        SgtApplication _app = null;

        public VolumeSolutionTanksForm()
        {
            InitializeComponent();

            _app = SgtApplication.CreateInstance();
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VolumeSolutionTanksForm_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = _app.Technology.P0104.Tank_1;
            checkBox2.Checked = _app.Technology.P0104.Tank_2;

            checkBox3.Checked = _app.Technology.P0104.Tank_3;
            checkBox4.Checked = _app.Technology.P0104.Tank_4;

            checkBox5.Checked = _app.Technology.P0104.Tank_5;
            checkBox6.Checked = _app.Technology.P0104.Tank_6;

            checkBox7.Checked = _app.Technology.P0104.Tank_7;
            checkBox8.Checked = _app.Technology.P0104.Tank_8;
            
            checkBox9.Checked = _app.Technology.P0104.Tank_9;
            checkBox10.Checked = _app.Technology.P0104.Tank_10;

            checkBox11.Checked = _app.Technology.P0104.Tank_11;
            checkBox12.Checked = _app.Technology.P0104.Tank_12;

            checkBox13.Checked = _app.Technology.P0104.Tank_13;
            checkBox14.Checked = _app.Technology.P0104.Tank_14;
        }

        /// <summary>
        /// сохраняем настройки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accept_Click(object sender, EventArgs e)
        {
            _app.Technology.P0104.Tank_1 = checkBox1.Checked;
            _app.Technology.P0104.Tank_2 = checkBox2.Checked;

            _app.Technology.P0104.Tank_3 = checkBox3.Checked;
            _app.Technology.P0104.Tank_4 = checkBox4.Checked;

            _app.Technology.P0104.Tank_5 = checkBox5.Checked;
            _app.Technology.P0104.Tank_6 = checkBox6.Checked;

            _app.Technology.P0104.Tank_7 = checkBox7.Checked;
            _app.Technology.P0104.Tank_8 = checkBox8.Checked;

            _app.Technology.P0104.Tank_9 = checkBox9.Checked;
            _app.Technology.P0104.Tank_10 = checkBox10.Checked;

            _app.Technology.P0104.Tank_11 = checkBox11.Checked;
            _app.Technology.P0104.Tank_12 = checkBox12.Checked;

            _app.Technology.P0104.Tank_13 = checkBox13.Checked;
            _app.Technology.P0104.Tank_14 = checkBox14.Checked;
        }
    }
}