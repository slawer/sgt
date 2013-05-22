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
    public partial class EditNumericPanelForm : Form
    {
        SgtApplication _app = null;
        NumericPanel n_panel = null;
                
        List<VPanelParameter> copy_items;
        String copy_panelName = string.Empty;

        public EditNumericPanelForm(NumericPanel edited)
        {
            InitializeComponent();
                        
            _app = SgtApplication.CreateInstance();
            if (edited != null)
            {
                n_panel = edited;

                copy_panelName = n_panel.VPanelName;
                copy_items = new List<VPanelParameter>();

                foreach (VPanelParameter par in n_panel.Items)
                {
                    copy_items.Add(par);
                }

                copy_gr1 = new VPanelGraphic();
                copy_gr2 = new VPanelGraphic();
                copy_gr3 = new VPanelGraphic();
                copy_gr4 = new VPanelGraphic();
                copy_gr5 = new VPanelGraphic();

                copy_graphic(n_panel.Graphic_1, copy_gr1);
                copy_graphic(n_panel.Graphic_2, copy_gr2);
                copy_graphic(n_panel.Graphic_3, copy_gr3);
                copy_graphic(n_panel.Graphic_4, copy_gr4);
                copy_graphic(n_panel.Graphic_5, copy_gr5);
            }
            else
                n_panel = new NumericPanel(_app);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gr_source"></param>
        /// <param name="gr_destiny"></param>
        protected void copy_graphic(VPanelGraphic gr_source, VPanelGraphic gr_destiny)
        {
            gr_destiny.Color = gr_source.Color;
            gr_destiny.Description = gr_source.Description;

            gr_destiny.Identifier = gr_source.Identifier;
            gr_destiny.Max = gr_source.Max;

            gr_destiny.Min = gr_source.Min;
            gr_destiny.Parameter = gr_source.Parameter;

            gr_destiny.Tag = gr_source.Tag;
            gr_destiny.Units = gr_source.Units;
        }

        /// <summary>
        /// Цифровая панель
        /// </summary>
        public NumericPanel NumericPanel
        {
            get
            {
                return n_panel;
            }
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumericPanelForm_Load(object sender, EventArgs e)
        {
            if (n_panel != null)
            {
                textBoxPanelName.Text = n_panel.VPanelName;
            }

            ListViewItem item_1 = new ListViewItem("График 1");
            ListViewItem.ListViewSubItem desc_1;

            Parameter gr_par1 = _app.GetParameter(n_panel.Graphic_1.Identifier);
            if (gr_par1 != null)
            {
                desc_1 = new ListViewItem.ListViewSubItem(item_1, gr_par1.Name);
            }
            else
                desc_1 = new ListViewItem.ListViewSubItem(item_1, string.Empty);
                        
            item_1.SubItems.Add(desc_1);
            item_1.Tag = n_panel.Graphic_1;

            ListViewItem item_2 = new ListViewItem("График 2");
            ListViewItem.ListViewSubItem desc_2;

            Parameter gr_par2 = _app.GetParameter(n_panel.Graphic_2.Identifier);
            if (gr_par2 != null)
            {
                desc_2 = new ListViewItem.ListViewSubItem(item_2, gr_par2.Name);
            }
            else
                desc_2 = new ListViewItem.ListViewSubItem(item_2, string.Empty);

            item_2.SubItems.Add(desc_2);
            item_2.Tag = n_panel.Graphic_2;

            ListViewItem item_3 = new ListViewItem("График 3");
            ListViewItem.ListViewSubItem desc_3;

            Parameter gr_par3 = _app.GetParameter(n_panel.Graphic_3.Identifier);
            if (gr_par3 != null)
            {
                desc_3 = new ListViewItem.ListViewSubItem(item_3, gr_par3.Name);
            }
            else
                desc_3 = new ListViewItem.ListViewSubItem(item_3, string.Empty);

            item_3.SubItems.Add(desc_3);
            item_3.Tag = n_panel.Graphic_3;

            ListViewItem item_4 = new ListViewItem("График 4");
            ListViewItem.ListViewSubItem desc_4;

            Parameter gr_par4 = _app.GetParameter(n_panel.Graphic_4.Identifier);
            if (gr_par4 != null)
            {
                desc_4 = new ListViewItem.ListViewSubItem(item_4, gr_par4.Name);
            }
            else
                desc_4 = new ListViewItem.ListViewSubItem(item_4, string.Empty);

            item_4.SubItems.Add(desc_4);
            item_4.Tag = n_panel.Graphic_4;

            ListViewItem item_5 = new ListViewItem("График 5");
            ListViewItem.ListViewSubItem desc_5;

            Parameter gr_par5 = _app.GetParameter(n_panel.Graphic_5.Identifier);
            if (gr_par5 != null)
            {
                desc_5 = new ListViewItem.ListViewSubItem(item_5, gr_par5.Name);
            }
            else
                desc_5 = new ListViewItem.ListViewSubItem(item_5, string.Empty);

            item_5.SubItems.Add(desc_5);
            item_5.Tag = n_panel.Graphic_5;

            listViewGraphics.Items.Add(item_1);
            listViewGraphics.Items.Add(item_2);
            listViewGraphics.Items.Add(item_3);
            listViewGraphics.Items.Add(item_4);
            listViewGraphics.Items.Add(item_5);

            lb_GlubinaMin.Text = n_panel.Graphic_1.Min.ToString();
            lb_GlubinaMax.Text = n_panel.Graphic_1.Max.ToString();

            lb_MehSkorostMin.Text = n_panel.Graphic_2.Min.ToString();
            lb_MehSkorostMax.Text = n_panel.Graphic_2.Max.ToString();

            lb_VesMin.Text = n_panel.Graphic_3.Min.ToString();
            lb_VesMax.Text = n_panel.Graphic_3.Max.ToString();

            lb_pressureMin.Text = n_panel.Graphic_4.Min.ToString();
            lb_pressureMax.Text = n_panel.Graphic_4.Max.ToString();

            lb_RashodMin.Text = n_panel.Graphic_5.Min.ToString();
            lb_RashodMax.Text = n_panel.Graphic_5.Max.ToString();

            buttonGlubinaColor.BackColor = n_panel.Graphic_1.Color;
            buttonMehSkorostColor.BackColor = n_panel.Graphic_2.Color;

            buttonVesColor.BackColor = n_panel.Graphic_3.Color;
            buttonPressureColor.BackColor = n_panel.Graphic_4.Color;

            buttonRashodColor.BackColor = n_panel.Graphic_5.Color;

            if (n_panel.Items != null)
            {
                foreach (VPanelParameter item in n_panel.Items)
                {
                    InsertNumericParameter(item);
                }
            }            
        }

        /// <summary>
        /// Добавить цифровой параметр в список
        /// </summary>
        /// <param name="parameter">Добавляемый параметр</param>
        protected void InsertNumericParameter(VPanelParameter parameter)
        {
            try
            {
                ListViewItem item = new ListViewItem((listView1.Items.Count + 1).ToString());

                //Parameter par = _app.GetParameter(parameter.PNumber);
                Parameter par = _app.GetParameter(parameter.Identifier);

                if (par != null)
                {
                    ListViewItem.ListViewSubItem name = new ListViewItem.ListViewSubItem(item, par.Name);

                    item.Tag = parameter;
                    item.SubItems.Add(name);

                    listView1.Items.Add(item);
                }
                else
                {
                    /*MessageBox.Show(this, "Данный параметр не может быть добавлен на панель", "Сообщение", 
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    return;*/
                }

            }
            catch { }
        }

        /// <summary>
        /// добавляем новый цифровой параметр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNewParameter_Click(object sender, EventArgs e)
        {
            try
            {
                SelectParameterForm frm = new SelectParameterForm();
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    Parameter sel_parameter = frm.SelectedParameter;
                    if (sel_parameter != null && sel_parameter.Channel != null)
                    {
                        VPanelParameter vp_parameter = new VPanelParameter();
                        //vp_parameter.PNumber = sel_parameter.Channel.Number;
                        vp_parameter.Identifier = sel_parameter.Identifier;
                        n_panel.Items.Add(vp_parameter);

                        InsertNumericParameter(vp_parameter);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// изменяем параметр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditParameter_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems != null && listView1.SelectedItems.Count > 0)
                {
                    VPanelParameter sel_parameter = listView1.SelectedItems[0].Tag as VPanelParameter;
                    if (sel_parameter != null)
                    {
                        SelectParameterForm frm = new SelectParameterForm();
                        if (frm.ShowDialog(this) == DialogResult.OK)
                        {
                            Parameter sel_par = frm.SelectedParameter;
                            if (sel_par != null && sel_par.Channel != null)
                            {
                                //sel_parameter.PNumber = sel_par.Channel.Number;
                                sel_parameter.Identifier = sel_par.Identifier;

                                listView1.SelectedItems[0].SubItems[1].Text = sel_par.Name;
                            }
                        }
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// удаляем параметр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems != null && listView1.SelectedItems.Count > 0)
                {
                    VPanelParameter sel_parameter = listView1.SelectedItems[0].Tag as VPanelParameter;
                    if (sel_parameter != null)
                    {
                        n_panel.Items.Remove(sel_parameter);
                        listView1.Items.Remove(listView1.SelectedItems[0]);

                        int number = 1;
                        foreach (ListViewItem item in listView1.Items)
                        {
                            item.Text = number.ToString();
                            number = number + 1;
                        }
                    }
                }
            }
            catch { }
        }

        private void accept_Click(object sender, EventArgs e)
        {
            if (n_panel != null)
            {
                n_panel.VPanelName = textBoxPanelName.Text;
                //n_panel = edited;
            }
        }

        /// <summary>
        /// настраиваем график
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (listViewGraphics.SelectedItems != null && listViewGraphics.SelectedItems.Count > 0)
            {
                VPanelGraphic sel_gr = listViewGraphics.SelectedItems[0].Tag as VPanelGraphic;
                if (sel_gr != null)
                {
                    SelectParameterForm frm = new SelectParameterForm();
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        Parameter sel_par = frm.SelectedParameter;
                        if (sel_par != null)
                        {
                            sel_gr.Identifier = sel_par.Identifier;
                            sel_gr.Description = sel_par.Description;
                            
                            //sel_gr.Min = sel_par.Range.Min;
                            //sel_gr.Max = sel_par.Range.Max;

                            sel_gr.Units = sel_par.Units;

                            listViewGraphics.SelectedItems[0].SubItems[1].Text = sel_par.Name;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// очищаем график
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            if (listViewGraphics.SelectedItems != null && listViewGraphics.SelectedItems.Count > 0)
            {
                VPanelGraphic sel_gr = listViewGraphics.SelectedItems[0].Tag as VPanelGraphic;
                if (sel_gr != null)
                {
                    sel_gr.Identifier = Guid.Empty;
                    sel_gr.Description = string.Empty;

                    sel_gr.Units = string.Empty;
                }

                listViewGraphics.SelectedItems[0].SubItems[1].Text = string.Empty;
            }
        }

        /// <summary>
        /// настраиваем цвет первого графика
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGlubinaColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                n_panel.Graphic_1.Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        private void buttonMehSkorostColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                n_panel.Graphic_2.Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        private void buttonVesColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                n_panel.Graphic_3.Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        private void buttonPressureColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                n_panel.Graphic_4.Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        private void buttonRashodColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                n_panel.Graphic_5.Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        private void lb_GlubinaMin_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    n_panel.Graphic_1.Min = n_val;
                }
            }
        }

        private void lb_GlubinaMax_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    n_panel.Graphic_1.Max = n_val;
                }
            }
        }

        private void lb_MehSkorostMin_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    n_panel.Graphic_2.Min = n_val;
                }
            }
        }

        private void lb_MehSkorostMax_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    n_panel.Graphic_2.Max = n_val;
                }
            }
        }

        private void lb_VesMin_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    n_panel.Graphic_3.Min = n_val;
                }
            }
        }

        private void lb_VesMax_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    n_panel.Graphic_3.Max = n_val;
                }
            }
        }

        private void lb_pressureMin_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    n_panel.Graphic_4.Min = n_val;
                }
            }
        }

        private void lb_pressureMax_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    n_panel.Graphic_4.Max = n_val;
                }
            }
        }

        private void lb_RashodMin_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    n_panel.Graphic_1.Min = n_val;
                }
            }
        }

        private void lb_RashodMax_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    n_panel.Graphic_5.Max = n_val;
                }
            }
        }

        VPanelGraphic copy_gr1;
        VPanelGraphic copy_gr2;
        VPanelGraphic copy_gr3;
        VPanelGraphic copy_gr4;
        VPanelGraphic copy_gr5;

        /// <summary>
        /// откатываем назад параметры панели
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancel_Click(object sender, EventArgs e)
        {
            try
            {
                n_panel.VPanelName = copy_panelName;

                n_panel.Items.Clear();
                foreach (VPanelParameter par in copy_items)
                {
                    n_panel.Items.Add(par);
                }

                copy_graphic(copy_gr1, n_panel.Graphic_1);
                copy_graphic(copy_gr2, n_panel.Graphic_2);
                copy_graphic(copy_gr3, n_panel.Graphic_3);
                copy_graphic(copy_gr4, n_panel.Graphic_4);
                copy_graphic(copy_gr5, n_panel.Graphic_5);
            }
            catch { }
        }

        private void buttonSelectFontParameter_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems != null && listView1.SelectedItems.Count > 0)
                {
                    ListViewItem selected = listView1.SelectedItems[0];
                    if (selected != null)
                    {
                        VPanelParameter parameter = selected.Tag as VPanelParameter;
                        if (parameter != null)
                        {
                            fontDialog.Font = parameter.Font;
                            fontDialog.Color = parameter.Color;

                            if (fontDialog.ShowDialog(this) == DialogResult.OK)
                            {
                                parameter.Font = fontDialog.Font;
                                parameter.Color = fontDialog.Color;
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems != null && listView1.SelectedItems.Count > 0)
            {
                ListViewItem sel = listView1.SelectedItems[0];
                if (sel != null)
                {
                    VPanelParameter par = sel.Tag as VPanelParameter;
                    if (par != null)
                    {
                        textBox1.Text = string.Format("{0};{1} pt", par.Font.Name, par.Font.SizeInPoints);
                    }
                }
            }
        }
    }
}