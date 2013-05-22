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
    public partial class CommandP0211Form : Form
    {
        SgtApplication _app = null;

        public CommandP0211Form()
        {
            InitializeComponent();

            _app = SgtApplication.CreateInstance();
        }

        /// <summary>
        /// Изменяем над забоем
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            _app.Technology.P0202.ModeProccess = P0202.TModeProcess.mpCMDzaboi;
        }
    }
}