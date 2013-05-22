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
    public partial class SaveTechParametersForm : Form
    {
        SgtApplication _app = null;

        public SaveTechParametersForm()
        {
            InitializeComponent();
            _app = SgtApplication.CreateInstance();
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveTechParametersForm_Load(object sender, EventArgs e)
        {
            if (_app != null)
            {
                TParameter[] parameters = _app.Technology.Parameters;
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        InsertParameter(parameter);
                    }
                }
            }
        }

        /// <summary>
        /// добавить технологический параметр в список
        /// </summary>
        /// <param name="parameter"></param>
        protected void InsertParameter(TParameter parameter)
        {
            ListViewItem item = new ListViewItem(parameter.Name);
            ListViewItem.ListViewSubItem s_name = new ListViewItem.ListViewSubItem(item, string.Empty);

            if (parameter.SNumber != -1)
            {
                //Parameter par = _app.GetParameter(parameter.SNumber);
                Parameter par = _app.GetParameter(parameter.Identifier);
                if (par != null)
                {
                    s_name.Text = par.Name;
                }
            }

            if (s_name != null)
            {
                item.SubItems.Add(s_name);
            }

            item.Tag = parameter;
            listView1.Items.Add(item);
        }

        /// <summary>
        /// настроить технологический параметр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_tun_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems != null && listView1.SelectedItems.Count > 0)
            {
                TParameter s_tpar = listView1.SelectedItems[0].Tag as TParameter;
                if (s_tpar != null && !s_tpar.IsSimple)
                {
                    SelectParameterForm frm = new SelectParameterForm();
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        Parameter selected = frm.SelectedParameter;
                        if (selected != null)
                        {
                            if (selected.Channel != null)
                            {
                                //s_tpar.SNumber = selected.Channel.Number;
                                s_tpar.Identifier = selected.Identifier;

                                listView1.SelectedItems[0].SubItems[1].Text = selected.Name;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show(this, "Данный параметр не может быть настроен", "Предупреждение", 
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        /// <summary>
        /// очистить параметр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems != null && listView1.SelectedItems.Count > 0)
            {
                TParameter s_par = listView1.SelectedItems[0].Tag as TParameter;
                if (s_par != null)
                {
                    //s_par.SNumber = -1;
                    s_par.Identifier = Guid.Empty;

                    listView1.SelectedItems[0].SubItems[1].Text = string.Empty;
                }
            }
        }
    }
}