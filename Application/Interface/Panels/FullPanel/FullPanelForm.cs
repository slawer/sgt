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
    public partial class FullPanelForm : Form
    {
        private SgtApplication _app = null;        
        private bufferArea area = null;

        private FullPanel fullPanel = null;

        public FullPanelForm(FullPanel _panel)
        {
            InitializeComponent();

            area = new bufferArea();
            _app = SgtApplication.CreateInstance();

            if (_panel == null)
            {
            }
            else
            {
                fullPanel = _panel;
                fullPanel.UpdateNumeric();

                CopyTo(fullPanel.GPanel_1.Graphic_1, area.GraphicsGroup_1[0]);
                CopyTo(fullPanel.GPanel_1.Graphic_2, area.GraphicsGroup_1[1]);
                CopyTo(fullPanel.GPanel_1.Graphic_3, area.GraphicsGroup_1[2]);
                CopyTo(fullPanel.GPanel_1.Graphic_4, area.GraphicsGroup_1[3]);
                CopyTo(fullPanel.GPanel_1.Graphic_5, area.GraphicsGroup_1[4]);

                CopyTo(fullPanel.GPanel_2.Graphic_1, area.GraphicsGroup_2[0]);
                CopyTo(fullPanel.GPanel_2.Graphic_2, area.GraphicsGroup_2[1]);
                CopyTo(fullPanel.GPanel_2.Graphic_3, area.GraphicsGroup_2[2]);
                CopyTo(fullPanel.GPanel_2.Graphic_4, area.GraphicsGroup_2[3]);
                CopyTo(fullPanel.GPanel_2.Graphic_5, area.GraphicsGroup_2[4]);

                CopyTo(fullPanel.GPanel_3.Graphic_1, area.GraphicsGroup_3[0]);
                CopyTo(fullPanel.GPanel_3.Graphic_2, area.GraphicsGroup_3[1]);
                CopyTo(fullPanel.GPanel_3.Graphic_3, area.GraphicsGroup_3[2]);
                CopyTo(fullPanel.GPanel_3.Graphic_4, area.GraphicsGroup_3[3]);
                CopyTo(fullPanel.GPanel_3.Graphic_5, area.GraphicsGroup_3[4]);

                area.Items.Clear();
                if (fullPanel.Items != null && fullPanel.Items.Count > 0)
                {                    
                    foreach (VPanelParameter item in fullPanel.Items)
                    {
                        if (item != null)
                        {
                            VPanelParameter n_item = new VPanelParameter();

                            n_item.Color = item.Color;
                            n_item.Font = item.Font;

                            n_item.Identifier = item.Identifier;
                            n_item.PNumber = item.PNumber;

                            n_item.Tag = item.Tag;

                            area.Items.Add(n_item);
                        }
                    }
                }

                checkBox1.Checked = area.Show_gr1 = fullPanel.Show_gr1;
                checkBox2.Checked = area.Show_gr2 = fullPanel.Show_gr2;
                checkBox3.Checked = area.Show_gr3 = fullPanel.Show_gr3;

                numericUpDown1.Value = area.GraphicsGroup_1[0].Width;
                numericUpDown2.Value = area.GraphicsGroup_1[1].Width;
                numericUpDown4.Value = area.GraphicsGroup_1[2].Width;
                numericUpDown3.Value = area.GraphicsGroup_1[3].Width;
                numericUpDown5.Value = area.GraphicsGroup_1[4].Width;

                numericUpDown15.Value = area.GraphicsGroup_2[0].Width;
                numericUpDown14.Value = area.GraphicsGroup_2[1].Width;
                numericUpDown13.Value = area.GraphicsGroup_2[2].Width;
                numericUpDown12.Value = area.GraphicsGroup_2[3].Width;
                numericUpDown11.Value = area.GraphicsGroup_2[4].Width;

                numericUpDown10.Value = area.GraphicsGroup_3[0].Width;
                numericUpDown9.Value = area.GraphicsGroup_3[1].Width;
                numericUpDown8.Value = area.GraphicsGroup_3[2].Width;
                numericUpDown7.Value = area.GraphicsGroup_3[3].Width;
                numericUpDown6.Value = area.GraphicsGroup_3[4].Width;

                textBox31.Text = fullPanel.VPanelName;
            }
        }

        /// <summary>
        /// Возвращяет общую панель при создании новой панели
        /// </summary>
        public FullPanel FullPanel
        {
            get
            {
                return fullPanel;
            }
        }

        /// <summary>
        /// Отображаем данные на форме
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FullPanelForm_Shown(object sender, EventArgs e)
        {
            if (area.Items != null)
            {
                foreach (VPanelParameter parameter in area.Items)
                {
                    InsertNumericParameter(parameter);
                }
            }

            if (area.GraphicsGroup_1 != null)
            {
                InitializeFirstGroup();
            }

            if (area.GraphicsGroup_2 != null)
            {
                InitializeSecondGroup();
            }

            if (area.GraphicsGroup_3 != null)
            {
                InitializeThirdGroup();
            }

            if (fullPanel != null)
            {
                InitFullGraphic(listViewGraphics);
                InitFullGraphic(listView2);
                InitFullGraphic(listView3);
            }
        }


        protected void InitFullGraphic(ListView list)
        {
            foreach (ListViewItem item in list.Items)
            {
                VPanelGraphic sel_par = item.Tag as VPanelGraphic;
                if (sel_par != null)
                {
                    if (sel_par.Parameter != null)
                    {
                        item.SubItems[1].Text = sel_par.Parameter.Name;
                    }
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
                Parameter par = _app.GetParameter(parameter.Identifier);

                if (par != null)
                {
                    ListViewItem.ListViewSubItem name = new ListViewItem.ListViewSubItem(item, par.Name);
                    ListViewItem.ListViewSubItem fnt = new ListViewItem.ListViewSubItem(item, string.Format("{0};{1} pt", parameter.Font.Name, parameter.Font.SizeInPoints));

                    item.Tag = parameter;
                    
                    item.SubItems.Add(name);
                    item.SubItems.Add(fnt);

                    listView1.Items.Add(item);
                }
                else
                {
                    MessageBox.Show(this, "Данный параметр не может быть добавлен на панель", "Сообщение", 
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    return;
                }

            }
            catch { }
        }

        /// <summary>
        /// настроить первую группу графиков
        /// </summary>
        protected void InitializeFirstGroup()
        {
            ListViewItem item_1 = new ListViewItem("График 1");
            ListViewItem.ListViewSubItem desc_1 = new ListViewItem.ListViewSubItem(item_1, string.Empty);

            item_1.SubItems.Add(desc_1);
            item_1.Tag = area.GraphicsGroup_1[0];

            ListViewItem item_2 = new ListViewItem("График 2");
            ListViewItem.ListViewSubItem desc_2 = new ListViewItem.ListViewSubItem(item_2, string.Empty);

            item_2.SubItems.Add(desc_2);
            item_2.Tag = area.GraphicsGroup_1[1];

            ListViewItem item_3 = new ListViewItem("График 3");
            ListViewItem.ListViewSubItem desc_3 = new ListViewItem.ListViewSubItem(item_3, string.Empty);

            item_3.SubItems.Add(desc_3);
            item_3.Tag = area.GraphicsGroup_1[2];

            ListViewItem item_4 = new ListViewItem("График 4");
            ListViewItem.ListViewSubItem desc_4 = new ListViewItem.ListViewSubItem(item_4, string.Empty);

            item_4.SubItems.Add(desc_4);
            item_4.Tag = area.GraphicsGroup_1[3];

            ListViewItem item_5 = new ListViewItem("График 5");
            ListViewItem.ListViewSubItem desc_5 = new ListViewItem.ListViewSubItem(item_5, string.Empty);

            item_5.SubItems.Add(desc_5);
            item_5.Tag = area.GraphicsGroup_1[4];

            listViewGraphics.Items.Add(item_5);
            listViewGraphics.Items.Add(item_4);
            listViewGraphics.Items.Add(item_3);
            listViewGraphics.Items.Add(item_2);
            listViewGraphics.Items.Add(item_1);

            lb_GlubinaMin.Text = area.GraphicsGroup_1[0].Min.ToString();
            lb_GlubinaMax.Text = area.GraphicsGroup_1[0].Max.ToString();

            lb_MehSkorostMin.Text = area.GraphicsGroup_1[1].Min.ToString();
            lb_MehSkorostMax.Text = area.GraphicsGroup_1[1].Max.ToString();

            lb_VesMin.Text = area.GraphicsGroup_1[2].Min.ToString();
            lb_VesMax.Text = area.GraphicsGroup_1[2].Max.ToString();

            lb_pressureMin.Text = area.GraphicsGroup_1[3].Min.ToString();
            lb_pressureMax.Text = area.GraphicsGroup_1[3].Max.ToString();

            lb_RashodMin.Text = area.GraphicsGroup_1[4].Min.ToString();
            lb_RashodMax.Text = area.GraphicsGroup_1[4].Max.ToString();

            buttonGlubinaColor.BackColor = area.GraphicsGroup_1[0].Color;
            buttonMehSkorostColor.BackColor = area.GraphicsGroup_1[1].Color;

            buttonVesColor.BackColor = area.GraphicsGroup_1[2].Color;
            buttonPressureColor.BackColor = area.GraphicsGroup_1[3].Color;

            buttonRashodColor.BackColor = area.GraphicsGroup_1[4].Color;
            checkBox1.Checked = area.Show_gr1;
        }

        /// <summary>
        /// настроить вторую группу графиков
        /// </summary>
        protected void InitializeSecondGroup()
        {
            ListViewItem item_1 = new ListViewItem("График 1");
            ListViewItem.ListViewSubItem desc_1 = new ListViewItem.ListViewSubItem(item_1, string.Empty);

            item_1.SubItems.Add(desc_1);
            item_1.Tag = area.GraphicsGroup_2[0];

            ListViewItem item_2 = new ListViewItem("График 2");
            ListViewItem.ListViewSubItem desc_2 = new ListViewItem.ListViewSubItem(item_2, string.Empty);

            item_2.SubItems.Add(desc_2);
            item_2.Tag = area.GraphicsGroup_2[1];

            ListViewItem item_3 = new ListViewItem("График 3");
            ListViewItem.ListViewSubItem desc_3 = new ListViewItem.ListViewSubItem(item_3, string.Empty);

            item_3.SubItems.Add(desc_3);
            item_3.Tag = area.GraphicsGroup_2[2];

            ListViewItem item_4 = new ListViewItem("График 4");
            ListViewItem.ListViewSubItem desc_4 = new ListViewItem.ListViewSubItem(item_4, string.Empty);

            item_4.SubItems.Add(desc_4);
            item_4.Tag = area.GraphicsGroup_2[3];

            ListViewItem item_5 = new ListViewItem("График 5");
            ListViewItem.ListViewSubItem desc_5 = new ListViewItem.ListViewSubItem(item_5, string.Empty);

            item_5.SubItems.Add(desc_5);
            item_5.Tag = area.GraphicsGroup_2[4];

            listView2.Items.Add(item_5);
            listView2.Items.Add(item_4);
            listView2.Items.Add(item_3);
            listView2.Items.Add(item_2);
            listView2.Items.Add(item_1);

            textBox10.Text = area.GraphicsGroup_2[0].Min.ToString();
            textBox9.Text = area.GraphicsGroup_2[0].Max.ToString();

            textBox8.Text = area.GraphicsGroup_2[1].Min.ToString();
            textBox7.Text = area.GraphicsGroup_2[1].Max.ToString();

            textBox6.Text = area.GraphicsGroup_2[2].Min.ToString();
            textBox5.Text = area.GraphicsGroup_2[2].Max.ToString();

            textBox4.Text = area.GraphicsGroup_2[3].Min.ToString();
            textBox3.Text = area.GraphicsGroup_2[3].Max.ToString();

            textBox2.Text = area.GraphicsGroup_2[4].Min.ToString();
            textBox1.Text = area.GraphicsGroup_2[4].Max.ToString();

            button7.BackColor = area.GraphicsGroup_2[0].Color;
            button6.BackColor = area.GraphicsGroup_2[1].Color;

            button5.BackColor = area.GraphicsGroup_2[2].Color;
            button3.BackColor = area.GraphicsGroup_2[3].Color;

            button1.BackColor = area.GraphicsGroup_2[4].Color;
        }

        /// <summary>
        /// настроить третью группу графиков
        /// </summary>
        protected void InitializeThirdGroup()
        {
            ListViewItem item_1 = new ListViewItem("График 1");
            ListViewItem.ListViewSubItem desc_1 = new ListViewItem.ListViewSubItem(item_1, string.Empty);

            item_1.SubItems.Add(desc_1);
            item_1.Tag = area.GraphicsGroup_3[0];

            ListViewItem item_2 = new ListViewItem("График 2");
            ListViewItem.ListViewSubItem desc_2 = new ListViewItem.ListViewSubItem(item_2, string.Empty);

            item_2.SubItems.Add(desc_2);
            item_2.Tag = area.GraphicsGroup_3[1];

            ListViewItem item_3 = new ListViewItem("График 3");
            ListViewItem.ListViewSubItem desc_3 = new ListViewItem.ListViewSubItem(item_3, string.Empty);

            item_3.SubItems.Add(desc_3);
            item_3.Tag = area.GraphicsGroup_3[2];

            ListViewItem item_4 = new ListViewItem("График 4");
            ListViewItem.ListViewSubItem desc_4 = new ListViewItem.ListViewSubItem(item_4, string.Empty);

            item_4.SubItems.Add(desc_4);
            item_4.Tag = area.GraphicsGroup_3[3];

            ListViewItem item_5 = new ListViewItem("График 5");
            ListViewItem.ListViewSubItem desc_5 = new ListViewItem.ListViewSubItem(item_5, string.Empty);

            item_5.SubItems.Add(desc_5);
            item_5.Tag = area.GraphicsGroup_3[4];

            listView3.Items.Add(item_5);
            listView3.Items.Add(item_4);
            listView3.Items.Add(item_3);
            listView3.Items.Add(item_2);
            listView3.Items.Add(item_1);

            textBox20.Text = area.GraphicsGroup_3[0].Min.ToString();
            textBox19.Text = area.GraphicsGroup_3[0].Max.ToString();

            textBox18.Text = area.GraphicsGroup_3[1].Min.ToString();
            textBox17.Text = area.GraphicsGroup_3[1].Max.ToString();

            textBox16.Text = area.GraphicsGroup_3[2].Min.ToString();
            textBox15.Text = area.GraphicsGroup_3[2].Max.ToString();

            textBox14.Text = area.GraphicsGroup_3[3].Min.ToString();
            textBox13.Text = area.GraphicsGroup_3[3].Max.ToString();

            textBox12.Text = area.GraphicsGroup_3[4].Min.ToString();
            textBox11.Text = area.GraphicsGroup_3[4].Max.ToString();

            button12.BackColor = area.GraphicsGroup_3[0].Color;
            button11.BackColor = area.GraphicsGroup_3[1].Color;

            button10.BackColor = area.GraphicsGroup_3[2].Color;
            button9.BackColor = area.GraphicsGroup_3[3].Color;

            button8.BackColor = area.GraphicsGroup_3[4].Color;
        }

        /// <summary>
        /// 
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
                        vp_parameter.Identifier = sel_parameter.Identifier;

                        area.Items.Add(vp_parameter);
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
        /// 
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
                                sel_parameter.Identifier = sel_par.Identifier;

                                listView1.SelectedItems[0].SubItems[1].Text = sel_par.Name;
                                listView1.SelectedItems[0].SubItems[2].Text = string.Format("{0};{1} pt", sel_parameter.Font.Name, sel_parameter.Font.SizeInPoints);
                            }
                        }
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// удалить параметр
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
                        area.Items.Remove(sel_parameter);
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

        /// <summary>
        /// определяем шрифт
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
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

                                listView1.SelectedItems[0].SubItems[2].Text = string.Format("{0};{1} pt", parameter.Font.Name, parameter.Font.SizeInPoints);
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void buttonGlubinaColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                area.GraphicsGroup_1[0].Color = colorDialog.Color;
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
                    area.GraphicsGroup_1[0].Min = n_val;
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
                    area.GraphicsGroup_1[0].Max = n_val;
                }
            }
        }

        private void buttonMehSkorostColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                area.GraphicsGroup_1[1].Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
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
                    area.GraphicsGroup_1[1].Min = n_val;
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
                    area.GraphicsGroup_1[1].Max = n_val;
                }
            }
        }

        private void buttonVesColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                area.GraphicsGroup_1[2].Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
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
                    area.GraphicsGroup_1[2].Min = n_val;
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
                    area.GraphicsGroup_1[2].Max = n_val;
                }
            }
        }

        private void buttonPressureColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                area.GraphicsGroup_1[3].Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
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
                    area.GraphicsGroup_1[3].Min = n_val;
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
                    area.GraphicsGroup_1[3].Max = n_val;
                }
            }
        }

        private void buttonRashodColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                area.GraphicsGroup_1[4].Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
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
                    area.GraphicsGroup_1[4].Min = n_val;
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
                    area.GraphicsGroup_1[4].Max = n_val;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                area.GraphicsGroup_2[0].Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    area.GraphicsGroup_2[0].Min = n_val;
                }
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    area.GraphicsGroup_2[0].Max = n_val;
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                area.GraphicsGroup_2[1].Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    area.GraphicsGroup_2[1].Min = n_val;
                }
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    area.GraphicsGroup_2[1].Max = n_val;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                area.GraphicsGroup_2[2].Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    area.GraphicsGroup_2[2].Min = n_val;
                }
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    area.GraphicsGroup_2[2].Max = n_val;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                area.GraphicsGroup_2[3].Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    area.GraphicsGroup_2[3].Min = n_val;
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    area.GraphicsGroup_2[3].Max = n_val;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                area.GraphicsGroup_2[4].Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    area.GraphicsGroup_2[4].Min = n_val;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    area.GraphicsGroup_2[4].Max = n_val;
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                area.GraphicsGroup_3[0].Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    area.GraphicsGroup_3[0].Min = n_val;
                }
            }
        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    area.GraphicsGroup_3[0].Max = n_val;
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                area.GraphicsGroup_3[1].Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    area.GraphicsGroup_3[1].Min = n_val;
                }
            }
        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    area.GraphicsGroup_3[1].Max = n_val;
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                area.GraphicsGroup_3[2].Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    area.GraphicsGroup_3[2].Min = n_val;
                }
            }
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    area.GraphicsGroup_3[2].Max = n_val;
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                area.GraphicsGroup_3[3].Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    area.GraphicsGroup_3[3].Min = n_val;
                }
            }
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    area.GraphicsGroup_3[3].Max = n_val;
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                area.GraphicsGroup_3[4].Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    area.GraphicsGroup_3[4].Min = n_val;
                }
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    area.GraphicsGroup_3[4].Max = n_val;
                }
            }
        }

        private void настроитьГрафикToolStripMenuItem_Click(object sender, EventArgs e)
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

                            sel_gr.Units = sel_par.Units;
                            listViewGraphics.SelectedItems[0].SubItems[1].Text = sel_par.Name;
                        }
                    }
                }
            }
        }

        private void очиститьГрафикToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
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
            catch
            {
            }
        }

        private void настроитьГрафикToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems != null && listView2.SelectedItems.Count > 0)
            {
                VPanelGraphic sel_gr = listView2.SelectedItems[0].Tag as VPanelGraphic;
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

                            sel_gr.Units = sel_par.Units;
                            listView2.SelectedItems[0].SubItems[1].Text = sel_par.Name;
                        }
                    }
                }
            }
        }

        private void очиститьГрафикToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems != null && listView2.SelectedItems.Count > 0)
            {
                VPanelGraphic sel_gr = listView2.SelectedItems[0].Tag as VPanelGraphic;
                if (sel_gr != null)
                {
                    sel_gr.Identifier = Guid.Empty;
                    sel_gr.Description = string.Empty;

                    sel_gr.Units = string.Empty;
                }

                listView2.SelectedItems[0].SubItems[1].Text = string.Empty;
            }
        }

        private void настроитьГрафикToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (listView3.SelectedItems != null && listView3.SelectedItems.Count > 0)
            {
                VPanelGraphic sel_gr = listView3.SelectedItems[0].Tag as VPanelGraphic;
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

                            sel_gr.Units = sel_par.Units;
                            listView3.SelectedItems[0].SubItems[1].Text = sel_par.Name;
                        }
                    }
                }
            }
        }

        private void очиститьГрафикToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (listView3.SelectedItems != null && listView3.SelectedItems.Count > 0)
            {
                VPanelGraphic sel_gr = listView3.SelectedItems[0].Tag as VPanelGraphic;
                if (sel_gr != null)
                {
                    sel_gr.Identifier = Guid.Empty;
                    sel_gr.Description = string.Empty;

                    sel_gr.Units = string.Empty;
                }

                listView3.SelectedItems[0].SubItems[1].Text = string.Empty;
            }
        }

        /// <summary>
        /// сохранем и принимаем
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accept_Click(object sender, EventArgs e)
        {
            if (fullPanel == null)
            {
                fullPanel = new FullPanel();
            }

            if (fullPanel != null)
            {
                CopyTo(area.GraphicsGroup_1[0], fullPanel.GPanel_1.Graphic_1);
                CopyTo(area.GraphicsGroup_1[1], fullPanel.GPanel_1.Graphic_2);
                CopyTo(area.GraphicsGroup_1[2], fullPanel.GPanel_1.Graphic_3);
                CopyTo(area.GraphicsGroup_1[3], fullPanel.GPanel_1.Graphic_4);
                CopyTo(area.GraphicsGroup_1[4], fullPanel.GPanel_1.Graphic_5);

                CopyTo(area.GraphicsGroup_2[0], fullPanel.GPanel_2.Graphic_1);
                CopyTo(area.GraphicsGroup_2[1], fullPanel.GPanel_2.Graphic_2);
                CopyTo(area.GraphicsGroup_2[2], fullPanel.GPanel_2.Graphic_3);
                CopyTo(area.GraphicsGroup_2[3], fullPanel.GPanel_2.Graphic_4);
                CopyTo(area.GraphicsGroup_2[4], fullPanel.GPanel_2.Graphic_5);

                CopyTo(area.GraphicsGroup_3[0], fullPanel.GPanel_3.Graphic_1);
                CopyTo(area.GraphicsGroup_3[1], fullPanel.GPanel_3.Graphic_2);
                CopyTo(area.GraphicsGroup_3[2], fullPanel.GPanel_3.Graphic_3);
                CopyTo(area.GraphicsGroup_3[3], fullPanel.GPanel_3.Graphic_4);
                CopyTo(area.GraphicsGroup_3[4], fullPanel.GPanel_3.Graphic_5);

                if (area.Items != null && area.Items.Count > 0)
                {
                    fullPanel.Items.Clear();
                    foreach (VPanelParameter item in area.Items)
                    {
                        if (item != null)
                        {
                            VPanelParameter n_item = new VPanelParameter();

                            n_item.Color = item.Color;
                            n_item.Font = item.Font;

                            n_item.Identifier = item.Identifier;
                            n_item.PNumber = item.PNumber;

                            n_item.Tag = item.Tag;

                            fullPanel.Items.Add(n_item);
                        }
                    }
                }

                fullPanel.VPanelName = textBox31.Text;

                fullPanel.Show_gr1 = area.Show_gr1 = checkBox1.Checked;
                fullPanel.Show_gr2 = area.Show_gr2 = checkBox2.Checked;
                fullPanel.Show_gr3 = area.Show_gr3 = checkBox3.Checked;
            }
        }

        /// <summary>
        /// Скопировать данные
        /// </summary>
        /// <param name="source">Источник</param>
        /// <param name="destination">Приемник</param>
        protected void CopyTo(VPanelGraphic source, VPanelGraphic destination)
        {
            if (source != null && destination != null)
            {
                destination.Color = source.Color;
                destination.Description = source.Description;

                destination.Font = source.Font;
                destination.Identifier = source.Identifier;

                destination.Max = source.Max;
                destination.Min = source.Min;

                destination.Parameter = source.Parameter;
                destination.Tag = source.Tag;

                destination.Units = source.Units;
                destination.Width = source.Width;
            }
        }

        /// <summary>
        /// изменили значение первого графика из первой группы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numeric = sender as NumericUpDown;
            if (numeric != null)
            {
                area.GraphicsGroup_1[0].Width = (int)numeric.Value;
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numeric = sender as NumericUpDown;
            if (numeric != null)
            {
                area.GraphicsGroup_1[1].Width = (int)numeric.Value;
            }
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numeric = sender as NumericUpDown;
            if (numeric != null)
            {
                area.GraphicsGroup_1[2].Width = (int)numeric.Value;
            }
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numeric = sender as NumericUpDown;
            if (numeric != null)
            {
                area.GraphicsGroup_1[3].Width = (int)numeric.Value;
            }
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numeric = sender as NumericUpDown;
            if (numeric != null)
            {
                area.GraphicsGroup_1[4].Width = (int)numeric.Value;
            }
        }

        private void numericUpDown15_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numeric = sender as NumericUpDown;
            if (numeric != null)
            {
                area.GraphicsGroup_2[0].Width = (int)numeric.Value;
            }
        }

        private void numericUpDown14_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numeric = sender as NumericUpDown;
            if (numeric != null)
            {
                area.GraphicsGroup_2[1].Width = (int)numeric.Value;
            }
        }

        private void numericUpDown13_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numeric = sender as NumericUpDown;
            if (numeric != null)
            {
                area.GraphicsGroup_2[2].Width = (int)numeric.Value;
            }
        }

        private void numericUpDown12_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numeric = sender as NumericUpDown;
            if (numeric != null)
            {
                area.GraphicsGroup_2[3].Width = (int)numeric.Value;
            }
        }

        private void numericUpDown11_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numeric = sender as NumericUpDown;
            if (numeric != null)
            {
                area.GraphicsGroup_2[4].Width = (int)numeric.Value;
            }
        }

        private void numericUpDown10_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numeric = sender as NumericUpDown;
            if (numeric != null)
            {
                area.GraphicsGroup_3[0].Width = (int)numeric.Value;
            }
        }

        private void numericUpDown9_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numeric = sender as NumericUpDown;
            if (numeric != null)
            {
                area.GraphicsGroup_3[1].Width = (int)numeric.Value;
            }
        }

        private void numericUpDown8_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numeric = sender as NumericUpDown;
            if (numeric != null)
            {
                area.GraphicsGroup_3[2].Width = (int)numeric.Value;
            }
        }

        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numeric = sender as NumericUpDown;
            if (numeric != null)
            {
                area.GraphicsGroup_3[3].Width = (int)numeric.Value;
            }
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numeric = sender as NumericUpDown;
            if (numeric != null)
            {
                area.GraphicsGroup_3[4].Width = (int)numeric.Value;
            }
        }
    }

    /// <summary>
    /// реализует буррирование данных для работы
    /// </summary>
    class bufferArea
    {
        protected List<VPanelParameter> items;              // элементы цифровой панели

        protected List<VPanelGraphic> graphicsGroup_1;      // графическое поле 1
        protected List<VPanelGraphic> graphicsGroup_2;      // графическое поле 2

        protected List<VPanelGraphic> graphicsGroup_3;      // графическое поле 3
        protected List<VPanelGraphic> graphicsGroup_4;      // графическое поле 4

        protected bool show_gr1 = false;                    // отображать группу или нет
        protected bool show_gr2 = false;                    // отображать группу или нет

        protected bool show_gr3 = false;                    // отображать группу или нет
        protected bool show_gr4 = false;                    // отображать группу или нет


        /// <summary>
        /// инициализирует новый экземпляр класса
        /// </summary>
        public bufferArea()
        {
            items = new List<VPanelParameter>();

            graphicsGroup_1 = new List<VPanelGraphic>();
            for (int i = 0; i < 5; i++)
            {
                graphicsGroup_1.Add(new VPanelGraphic());
            }

            graphicsGroup_2 = new List<VPanelGraphic>();
            for (int i = 0; i < 5; i++)
            {
                graphicsGroup_2.Add(new VPanelGraphic());
            }

            graphicsGroup_3 = new List<VPanelGraphic>();
            for (int i = 0; i < 5; i++)
            {
                graphicsGroup_3.Add(new VPanelGraphic());
            }

            graphicsGroup_4 = new List<VPanelGraphic>();
            for (int i = 0; i < 5; i++)
            {
                graphicsGroup_4.Add(new VPanelGraphic());
            }
        }

        /// <summary>
        /// Элементы цифровой панели
        /// </summary>
        public List<VPanelParameter> Items
        {
            get { return items; }
        }

        /// <summary>
        /// графическое поле 1
        /// </summary>
        public List<VPanelGraphic> GraphicsGroup_1
        {
            get { return graphicsGroup_1; }
        }

        /// <summary>
        /// графическое поле 2
        /// </summary>
        public List<VPanelGraphic> GraphicsGroup_2
        {
            get { return graphicsGroup_2; }
        }

        /// <summary>
        /// графическое поле 3
        /// </summary>
        public List<VPanelGraphic> GraphicsGroup_3
        {
            get { return graphicsGroup_3; }
        }

        /// <summary>
        /// графическое поле 4
        /// </summary>
        public List<VPanelGraphic> GraphicsGroup_4
        {
            get { return graphicsGroup_4; }
        }

        /// <summary>
        /// отображать группу или нет
        /// </summary>
        public bool Show_gr1
        {
            get { return show_gr1; }
            set { show_gr1 = value; }
        }

        /// <summary>
        /// отображать группу или нет
        /// </summary>
        public bool Show_gr2
        {
            get { return show_gr2; }
            set { show_gr2 = value; }
        }

        /// <summary>
        /// отображать группу или нет
        /// </summary>
        public bool Show_gr3
        {
            get { return show_gr3; }
            set { show_gr3 = value; }
        }

        /// <summary>
        /// отображать группу или нет
        /// </summary>
        public bool Show_gr4
        {
            get { return show_gr4; }
            set { show_gr4 = value; }
        }
    }
}