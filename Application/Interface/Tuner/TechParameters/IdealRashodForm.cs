using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SGT
{
    public partial class IdealRashodForm : Form
    {
        protected P0109 p109 = null;
        protected P0112 p112 = null;

        protected SgtApplication _app = null;

        public IdealRashodForm(TParameter parameter)
        {
            InitializeComponent();
            if (parameter is P0109)
            {
                p109 = parameter as P0109;
            }
            else
                p112 = parameter as P0112;

            _app = SgtApplication.CreateInstance();

            listView1.ListViewItemSorter = new ListComparer();
        }

        /// <summary>
        /// Реализует сортировку
        /// </summary>
        protected class ListComparer : IComparer
        {
            /// <summary>
            /// Инициализирует новый экземпляр класса
            /// </summary>
            public ListComparer()
            {
            }

            /// <summary>
            /// Реализует сравнение двух объектов
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public int Compare(ListViewItem x, ListViewItem y)
            {
                try
                {
                    if (x != null && y != null)
                    {
                        float x_item = SgtApplication.ParseSingle(x.SubItems[1].ToString());
                        float y_item = SgtApplication.ParseSingle(y.SubItems[1].ToString());

                        if (!float.IsNaN(x_item) && !float.IsNaN(y_item))
                        {
                            if (x_item > y_item) return 1;
                            if (x_item < y_item) return -1;
                        }
                    }
                }
                catch { }
                return 0;
            }

            /// <summary>
            /// Реализует сравнение двух объектов
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public int Compare(Object x, Object y)
            {
                try
                {
                    if (x != null && y != null)
                    {
                        ListViewItem x_j = x as ListViewItem;
                        ListViewItem y_j = y as ListViewItem;

                        if (x_j != null && y_j != null)
                        {
                            float x_item = SgtApplication.ParseSingle(x_j.SubItems[2].Text);
                            float y_item = SgtApplication.ParseSingle(y_j.SubItems[2].Text);

                            if (!float.IsNaN(x_item) && !float.IsNaN(y_item))
                            {
                                if (x_item > y_item) return 1;
                                if (x_item < y_item) return -1;
                            }
                        }
                    }
                }
                catch { }
                return 0;
            }
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IdealRashodForm_Load(object sender, EventArgs e)
        {
            if (p109 != null && p112 == null)
            {
                P0109.IdealFlowPair[] pairs = p109.Pairs;
                if (pairs != null)
                {
                    foreach (P0109.IdealFlowPair pair in pairs)
                    {
                        if (pair != null)
                        {
                            ListViewItem item = new ListViewItem((listView1.Items.Count + 1).ToString());

                            ListViewItem.ListViewSubItem rashod = new ListViewItem.ListViewSubItem(item, string.Format("{0:F3}", pair.Flow));
                            ListViewItem.ListViewSubItem diameter = new ListViewItem.ListViewSubItem(item, string.Format("{0:F3}", pair.Diameter));

                            item.SubItems.Add(rashod);
                            item.SubItems.Add(diameter);

                            item.Tag = pair;
                            listView1.Items.Add(item);
                        }
                    }
                }
            }
            else
                if (p112 != null && p109 == null)
                {
                    P0112.IdealFlowPair[] pairs = p112.Pairs;
                    if (pairs != null)
                    {
                        foreach (P0112.IdealFlowPair pair in pairs)
                        {
                            if (pair != null)
                            {
                                ListViewItem item = new ListViewItem((listView1.Items.Count + 1).ToString());

                                ListViewItem.ListViewSubItem rashod = new ListViewItem.ListViewSubItem(item, string.Format("{0:F3}", pair.Flow));
                                ListViewItem.ListViewSubItem diameter = new ListViewItem.ListViewSubItem(item, string.Format("{0:F3}", pair.Diameter));

                                item.SubItems.Add(rashod);
                                item.SubItems.Add(diameter);

                                item.Tag = pair;
                                listView1.Items.Add(item);
                            }
                        }
                    }
                }
        }

        /// <summary>
        /// добавляем пару
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addNew_Click(object sender, EventArgs e)
        {
            if (p109 != null && p112 == null)
            {
                IdealPairForm frm = new IdealPairForm();
                frm.Text = "Добавление";

                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    float flow = SgtApplication.ParseSingle(frm.textBoxFlow.Text);
                    float diam = SgtApplication.ParseSingle(frm.textBoxDiametr.Text);

                    P0109.IdealFlowPair pair = new P0109.IdealFlowPair(diam, flow);
                    _app.Technology.P0109.InsertPair(pair);

                    ListViewItem item = new ListViewItem((listView1.Items.Count + 1).ToString());

                    ListViewItem.ListViewSubItem rashod = new ListViewItem.ListViewSubItem(item, string.Format("{0:F3}", pair.Flow));
                    ListViewItem.ListViewSubItem diameter = new ListViewItem.ListViewSubItem(item, string.Format("{0:F3}", pair.Diameter));

                    item.SubItems.Add(rashod);
                    item.SubItems.Add(diameter);

                    item.Tag = pair;
                    listView1.Items.Add(item);
                }
            }
            else
                if (p112 != null && p109 == null)
                {
                    IdealPairForm frm = new IdealPairForm();
                    frm.Text = "Добавление";

                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        float flow = SgtApplication.ParseSingle(frm.textBoxFlow.Text);
                        float diam = SgtApplication.ParseSingle(frm.textBoxDiametr.Text);

                        P0112.IdealFlowPair pair = new P0112.IdealFlowPair(flow, diam);
                        _app.Technology.P0112.InsertPair(pair);

                        ListViewItem item = new ListViewItem((listView1.Items.Count + 1).ToString());

                        ListViewItem.ListViewSubItem rashod = new ListViewItem.ListViewSubItem(item, string.Format("{0:F3}", pair.Flow));
                        ListViewItem.ListViewSubItem diameter = new ListViewItem.ListViewSubItem(item, string.Format("{0:F3}", pair.Diameter));

                        item.SubItems.Add(rashod);
                        item.SubItems.Add(diameter);

                        item.Tag = pair;
                        listView1.Items.Add(item);
                    }
                }
        }

        /// <summary>
        /// удаляем пару
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeCurrent_Click(object sender, EventArgs e)
        {
            if (p109 != null && p112 == null)
            {
                if (listView1.SelectedItems != null && listView1.SelectedItems.Count > 0)
                {
                    ListViewItem item = listView1.SelectedItems[0];
                    if (item != null)
                    {
                        P0109.IdealFlowPair selected_pair = item.Tag as P0109.IdealFlowPair;
                        if (selected_pair != null)
                        {
                            p109.RemovePair(selected_pair);
                            listView1.Items.Remove(item);

                            for (int i = 0; i < listView1.Items.Count; i++)
                            {
                                listView1.Items[i].Text = (i + 1).ToString();
                            }
                        }
                    }
                }                
            }
            else
                if (p112 != null && p109 == null)
                {
                    if (listView1.SelectedItems != null && listView1.SelectedItems.Count > 0)
                    {
                        ListViewItem item = listView1.SelectedItems[0];
                        if (item != null)
                        {
                            P0112.IdealFlowPair selected_pair = item.Tag as P0112.IdealFlowPair;
                            if (selected_pair != null)
                            {
                                p112.RemovePair(selected_pair);
                                listView1.Items.Remove(item);

                                for (int i = 0; i < listView1.Items.Count; i++)
                                {
                                    listView1.Items[i].Text = (i + 1).ToString();
                                }
                            }
                        }
                    }
                }
        }

        /// <summary>
        /// редактируем пару
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editCurrent_Click(object sender, EventArgs e)
        {
            if (p109 != null && p112 == null)
            {
                if (listView1.SelectedItems != null && listView1.SelectedItems.Count > 0)
                {
                    P0109.IdealFlowPair sel = listView1.SelectedItems[0].Tag as P0109.IdealFlowPair;
                    if (sel != null)
                    {
                        IdealPairForm frm = new IdealPairForm();

                        frm.Text = "Редактирование";
                        
                        frm.textBoxFlow.Text = sel.Flow.ToString();
                        frm.textBoxDiametr.Text = sel.Diameter.ToString();

                        if (frm.ShowDialog(this) == DialogResult.OK)
                        {
                            sel.Flow = SgtApplication.ParseSingle(frm.textBoxFlow.Text);
                            sel.Diameter = SgtApplication.ParseSingle(frm.textBoxDiametr.Text);

                            listView1.SelectedItems[0].SubItems[1].Text = sel.Flow.ToString();
                            listView1.SelectedItems[0].SubItems[2].Text = sel.Diameter.ToString();
                        }
                    }
                }
            }
            else
                if (p112 != null && p109 == null)
                {
                    if (listView1.SelectedItems != null && listView1.SelectedItems.Count > 0)
                    {
                        P0112.IdealFlowPair sel = listView1.SelectedItems[0].Tag as P0112.IdealFlowPair;
                        if (sel != null)
                        {
                            IdealPairForm frm = new IdealPairForm();

                            frm.Text = "Редактирование";

                            frm.textBoxFlow.Text = sel.Flow.ToString();
                            frm.textBoxDiametr.Text = sel.Diameter.ToString();

                            if (frm.ShowDialog(this) == DialogResult.OK)
                            {
                                sel.Flow = SgtApplication.ParseSingle(frm.textBoxFlow.Text);
                                sel.Diameter = SgtApplication.ParseSingle(frm.textBoxDiametr.Text);

                                listView1.SelectedItems[0].SubItems[1].Text = sel.Flow.ToString();
                                listView1.SelectedItems[0].SubItems[2].Text = sel.Diameter.ToString();
                            }
                        }
                    }
                }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IdealRashodForm_Shown(object sender, EventArgs e)
        {
            listView1.Sort();
        }
    }
}