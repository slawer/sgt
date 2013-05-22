using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace SGT
{
    public partial class CurrentWorkForm : Form
    {
        SgtApplication _app = null;

        public CurrentWorkForm()
        {
            InitializeComponent();
            _app = SgtApplication.CreateInstance();
        }

        private void CurrentWorkForm_Load(object sender, EventArgs e)
        {
            Work work = _app.CurrentWork;
            if (work != null)
            {
                textBox1.Text = work.Field;
                textBox6.Text = work.Bush;

                textBox5.Text = work.Well;
                textBox4.Text = work.Description;

                textBox3.Text = work.StartTime.ToString("dddd dd MMMM yyyy  Время: HH:mm:ss    ", CultureInfo.CurrentCulture);
                textBox2.Text = work.StartingDepth.ToString();

                Session session = work.Current;
                if (session != null)
                {
                    textBox8.Text = session.Number.ToString();
                    textBox7.Text = session.Description;
                }
            }
        }
    }
}