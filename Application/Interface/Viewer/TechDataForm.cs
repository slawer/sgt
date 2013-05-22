using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SGT
{
    public partial class TechDataForm : Form
    {
        private Mutex mutex = null;
        private SgtApplication _app = null;

        public TechDataForm()
        {
            InitializeComponent();

            mutex = new Mutex();
            _app = SgtApplication.CreateInstance();

        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TechDataForm_Load(object sender, EventArgs e)
        {
            TParameter[] parameters = _app.Technology.Parameters;
            if (parameters != null)
            {
                int rowIndex = 0;
                foreach (TParameter parameter in parameters)
                {
                    dataGridView10.Rows.Add();

                    DataGridViewCell channelNumber = dataGridView10.Rows[rowIndex].Cells[0];
                    DataGridViewCell channelDesc = dataGridView10.Rows[rowIndex].Cells[1];

                    DataGridViewCell fIteration = dataGridView10.Rows[rowIndex].Cells[2];

                    channelNumber.Value = (rowIndex + 1);
                    channelDesc.Value = parameter.Name;

                    dataGridView10.Rows[rowIndex].Tag = parameter;
                    rowIndex = rowIndex + 1;
                }

                _app.Technology.onComplete += new EventHandler(Technology_onComplete);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TechDataForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _app.Technology.onComplete -= Technology_onComplete;
        }

        /// <summary>
        /// отображаем данные
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Technology_onComplete(object sender, EventArgs e)
        {
            bool blocked = false;
            try
            {
                if (mutex.WaitOne(1000, false))
                {
                    blocked = true;
                    foreach (DataGridViewRow row in dataGridView10.Rows)
                    {
                        if (row.Index < dataGridView10.Rows.Count - 1)
                        {
                            TParameter parameter = row.Tag as TParameter;
                            if (parameter != null)
                            {
                                row.Cells[2].Value = string.Format("{0:F3}", parameter.Value);
                            }
                        }
                    }
                }
            }
            finally
            {
                if (blocked) mutex.ReleaseMutex();
            }            
        }
    }
}