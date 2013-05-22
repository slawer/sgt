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
    public partial class PanelsForm : Form
    {
        SgtApplication _app = null;

        public PanelsForm()
        {
            InitializeComponent();

            _app = SgtApplication.CreateInstance();
        }

        /// <summary>
        /// Добавить панель в список
        /// </summary>
        /// <param name="panel">Добавляемая панель</param>
        protected void InsertPanel(VPanel panel)
        {
            if (panel != null)
            {
                ListViewItem item = new ListViewItem((listViewPanels.Items.Count + 1).ToString());
                ListViewItem.ListViewSubItem name = new ListViewItem.ListViewSubItem(item, panel.VPanelName);

                item.SubItems.Add(name);

                item.Tag = panel;
                listViewPanels.Items.Add(item);
            }
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PanelsForm_Load(object sender, EventArgs e)
        {
            VPanel[] panels = _app.Panels;
            if (panels != null)
            {
                foreach (VPanel panel in panels)
                {
                    switch (panel.VPanelType)
                    {
                        case VPanelType.DrillingFloor:

                            if (_app.ShowDrilling)
                            {
                                InsertPanel(panel);
                            }

                            break;

                        case VPanelType.SolutionPanel:

                            if (_app.ShowSolution)
                            {
                                InsertPanel(panel);
                            }
                            break;

                        case VPanelType.PanelSpo:

                            if (_app.ShowSpo)
                            {
                                InsertPanel(panel);
                            }
                            break;
                    }
                    
                }
            }

            VPanel[] optPanels = _app.OptPanels;
            if (optPanels != null)
            {
                foreach (VPanel panel in optPanels)
                {
                    InsertPanel(panel);
                }
            }
        }

        /// <summary>
        /// редактируем панель
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (listViewPanels.SelectedItems != null && listViewPanels.SelectedItems.Count > 0)
            {
                ListViewItem selected = listViewPanels.SelectedItems[0];
                if (selected != null)
                {
                    VPanel s_panel = selected.Tag as VPanel;
                    if (s_panel != null)
                    {
                        switch (s_panel.VPanelType)
                        {
                            case VPanelType.DrillingFloor:

                                DrillingPanelForm d_frm = new DrillingPanelForm();
                                d_frm.ShowDialog(this);
                                
                                break;

                            case VPanelType.SolutionPanel:

                                SolutionPanelForm s_frm = new SolutionPanelForm();
                                s_frm.ShowDialog(this);
                                
                                break;

                            case VPanelType.PanelSpo:

                                SpoPanelForm sp_frm = new SpoPanelForm();
                                sp_frm.ShowDialog(this);

                                break;

                            case VPanelType.NumericPanel:

                                EditNumericPanelForm frm = new EditNumericPanelForm(s_panel as NumericPanel);
                                if (frm.ShowDialog(this) == DialogResult.OK)
                                {
                                    //s_panel.Actualize();
                                    selected.SubItems[1].Text = s_panel.VPanelName;
                                }

                                s_panel.Actualize();
                                break;

                            case VPanelType.FullPanel:

                                FullPanelForm full_frm = new FullPanelForm(s_panel as FullPanel);
                                if (full_frm.ShowDialog(this) == DialogResult.OK)
                                {
                                    selected.SubItems[1].Text = s_panel.VPanelName;
                                }
                                s_panel.Actualize();
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// добавить цифровую панель
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCreateNewNumericPanel_Click(object sender, EventArgs e)
        {
            NumericPanelForm frm = new NumericPanelForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                NumericPanel n_panel = frm.NumericPanel;
                if (n_panel != null)
                {
                    _app.InsertPanel(n_panel);
                    InsertPanel(n_panel);

                    n_panel.Actualize();
                }
            }
        }

        /// <summary>
        /// удаляем панель
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (listViewPanels.SelectedItems != null && listViewPanels.SelectedItems.Count > 0)
            {
                ListViewItem selected = listViewPanels.SelectedItems[0];
                if (selected != null)
                {
                    VPanel s_panel = selected.Tag as VPanel;
                    if (s_panel != null)
                    {
                        switch (s_panel.VPanelType)
                        {
                            case VPanelType.NumericPanel:

                                if (MessageBox.Show(this, "Вы действительно хотите удалить панель", "Сообщение",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    _app.RemovePanel(s_panel);
                                    listViewPanels.Items.Remove(selected);
                                }

                                break;

                            case VPanelType.FullPanel:

                                if (MessageBox.Show(this, "Вы действительно хотите удалить панель", "Сообщение",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    _app.RemovePanel(s_panel);
                                    listViewPanels.Items.Remove(selected);
                                }

                                break;

                            default:
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// добавить общую панель
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            FullPanelForm frm = new FullPanelForm(null);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                FullPanel n_panel = frm.FullPanel;
                if (n_panel != null)
                {
                    _app.InsertPanel(n_panel);
                    InsertPanel(n_panel);

                    n_panel.Actualize();
                }
            }
        }        
    }
}