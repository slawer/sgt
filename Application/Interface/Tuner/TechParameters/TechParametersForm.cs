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
    public partial class TechParametersForm : Form
    {
        SgtApplication _app = null;

        public TechParametersForm()
        {
            InitializeComponent();
            _app = SgtApplication.CreateInstance();
        }


        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TechParametersForm_Load(object sender, EventArgs e)
        {
            InitializeTable();
        }

        /// <summary>
        /// Настроить таблицу
        /// </summary>
        protected void InitializeTable()
        {
            for (int i = 0; i < 56; i++)
            {
                if (i > -1 && i < _app.Technology.Parameters.Length)
                {
                    TParameter parameter = _app.Technology.Parameters[i];
                    if (parameter != null)
                    {
                        ListViewItem item = new ListViewItem(parameter.Name);
                        //Parameter par = _app.GetParameter(parameter.PNumber);
                        Parameter par = _app.GetParameter(parameter.Identifier);

                        if (par != null)
                        {
                            ListViewItem.ListViewSubItem sub = new ListViewItem.ListViewSubItem(item, par.Name);
                            item.SubItems.Add(sub);
                        }
                        else
                        {
                            ListViewItem.ListViewSubItem sub = new ListViewItem.ListViewSubItem(item, "не определен");
                            item.SubItems.Add(sub);
                        }

                        item.Tag = parameter;
                        listView1.Items.Add(item);
                    }
                }
            }
        }

        /// <summary>
        /// настраиваем технологический параметр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tune_btn_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems != null && listView1.SelectedItems.Count > 0)
            {
                TParameter sel_par = listView1.SelectedItems[0].Tag as TParameter;
                if (sel_par != null)
                {
                    SelectParameterForm frm = new SelectParameterForm();
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        Parameter selected = frm.SelectedParameter;
                        if (selected != null)
                        {
                            if (selected.Channel != null)
                            {
                                sel_par.Identifier = selected.Identifier;
                                //sel_par.PNumber = selected.Channel.Number;                                

                                listView1.SelectedItems[0].SubItems[1].Text = selected.Name;
                            }
                            else
                                MessageBox.Show(this, "Выбранный параметр не настроен.", "Предупреждение", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// сбрасываем настройки технологического параметра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clear_btn_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems != null && listView1.SelectedItems.Count > 0)
            {
                TParameter sel_par = listView1.SelectedItems[0].Tag as TParameter;
                if (sel_par != null)
                {
                    //sel_par.PNumber = -1;
                    sel_par.Identifier = Guid.Empty;

                    sel_par.Calculate(float.NaN);

                    listView1.SelectedItems[0].SubItems[1].Text = "не определен";
                }
            }
        }
    }
}