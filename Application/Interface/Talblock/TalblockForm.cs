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
    public partial class TalblockForm : Form
    {
        SgtApplication _app;

        public TalblockForm()
        {
            InitializeComponent();
            _app = SgtApplication.CreateInstance();
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TalblockForm_Load(object sender, EventArgs e)
        {
            numericUpDownDevNumber.Value = _app.DeviceNumber;
            numericUpDownKoef.Value = _app.KoefTalblock;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accept_Click(object sender, EventArgs e)
        {
            _app.DeviceNumber = (int)numericUpDownDevNumber.Value;
            _app.KoefTalblock = (int)numericUpDownKoef.Value;
        }
    }
}