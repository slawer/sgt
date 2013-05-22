using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NumericTable
{
    public partial class OptionsNumericForm : Form
    {
        private NumericTable n_table = null;
        public OptionsNumericForm(NumericTable nTable)
        {
            InitializeComponent();

            n_table = nTable;
        }

        /// <summary>
        /// Добавить элемент на форму для редактирования
        /// </summary>
        /// <param name="item">Добавляемый элемент</param>
        private void InsertElement(PanelItem item)
        {
            try
            {
                int number = listView1.Items.Count + 1;

                ListViewItem l_item = new ListViewItem(number.ToString());
                
                ListViewItem.ListViewSubItem n_item = new ListViewItem.ListViewSubItem(l_item, item.Description);
                ListViewItem.ListViewSubItem f_item = new ListViewItem.ListViewSubItem(l_item, string.Format("{0};{1} pt", item.Font.Name, item.Font.SizeInPoints));

                l_item.SubItems.Add(n_item);
                l_item.SubItems.Add(f_item);

                ItemOpt i_opt = new ItemOpt();
                
                i_opt.item = item;

                i_opt.i_color = item.Color;
                i_opt.i_font = new System.Drawing.Font(item.Font, item.Font.Style);

                l_item.Tag = i_opt;
                listView1.Items.Add(l_item);
            }
            catch { }
        }

        /// <summary>
        /// Отображаем елементы цифровой таблицы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionsNumericForm_Shown(object sender, EventArgs e)
        {
            if (n_table != null && n_table.Panel != null)
            {
                foreach (PanelItem item in n_table.Panel.Items)
                {
                    if (item != null)
                    {
                        InsertElement(item);
                    }
                }
            }
        }

        /// <summary>
        /// редактируем шрифт елемента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems != null && listView1.SelectedItems.Count > 0)
                {
                    ListViewItem selected = listView1.SelectedItems[0];
                    if (selected != null)
                    {
                        ItemOpt parameter = selected.Tag as ItemOpt;
                        if (parameter != null)
                        {
                            fontDialog.Font = parameter.i_font;
                            fontDialog.Color = parameter.i_color;

                            if (fontDialog.ShowDialog(this) == DialogResult.OK)
                            {
                                parameter.i_font = fontDialog.Font;
                                parameter.i_color = fontDialog.Color;

                                selected.SubItems[2].Text = string.Format("{0};{1} pt", parameter.i_font.Name,
                                    parameter.i_font.SizeInPoints);
                            }
                        }
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Проверяем и сохраняем
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accept_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem item in listView1.Items)
                {
                    if (item != null && item.Tag != null)
                    {
                        ItemOpt opt = item.Tag as ItemOpt;
                        if (opt != null)
                        {
                            opt.item.Font = opt.i_font;
                            opt.item.Color = opt.i_color;
                        }
                    }
                }

                n_table.Panel.Redraw();
            }
            catch { }
        }
    }

    class ItemOpt
    {
        public PanelItem item;

        public Color i_color;
        public Font i_font;
    }
}