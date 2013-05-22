using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace SGT
{
    public partial class WorksForm : Form
    {
        protected List<Work> works = null;          // рейсы
        protected SgtApplication _app = null;       // контекст работы программы

        public WorksForm()
        {
            InitializeComponent();
            
            _app = SgtApplication.CreateInstance();
            if (_app != null)
            {
                works = _app.Works;
            }
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorksForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (works != null)
                {
                    foreach (Work work in works)
                    {
                        if (work != null)
                        {
                            InsertWorkToTree(work);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Ошибка во время построения дерева проектов",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                treeViewWorks.Nodes.Clear();
            }
        }

        /// <summary>
        /// Добавить проект в дерево проектов
        /// </summary>
        /// <param name="project"></param>
        protected void InsertWorkToTree(Work work)
        {
            try
            {
                if (treeViewWorks.Nodes != null && treeViewWorks.Nodes.Count > 0)
                {
                    foreach (TreeNode node in treeViewWorks.Nodes)
                    {
                        if (node.Text == string.Format("Месторождение: {0}", work.Field))
                        {
                            InsertWorkToProjectNode(work, node);
                            return;
                        }
                    }

                    InsertFirstWorkToTree(work);
                }
                else
                {
                    InsertFirstWorkToTree(work);
                }
            }
            catch { }
        }

        /// <summary>
        /// Втавить проект в пустое дерево проектов
        /// </summary>
        /// <param name="project">Втавляемый в пустое дерево проектов проект</param>
        protected void InsertFirstWorkToTree(Work work)
        {
            try
            {
                TreeNode root = new TreeNode(string.Format("Месторождение: {0}", work.Field), 0, 0);

                TreeNode bushNode = new TreeNode(string.Format("Куст: {0}", work.Bush), 1, 1);
                TreeNode wellNode = new TreeNode(string.Format("Скважина: {0}", work.Well), 2, 2);

                TreeNode jobNode = new TreeNode(string.Format("Задание: {0}", work.Description), 3, 3);
                jobNode.Tag = work;

                wellNode.Nodes.Add(jobNode);
                bushNode.Nodes.Add(wellNode);

                root.Nodes.Add(bushNode);
                treeViewWorks.Nodes.Add(root);
            }
            catch { }
        }

        /// <summary>
        /// Вставить проект в существующее месторождение
        /// </summary>
        /// <param name="project">Вставляемый проект</param>
        /// <param name="node">Узел в который вставить проект</param>
        protected void InsertWorkToProjectNode(Work work, TreeNode node)
        {
            if (node != null)
            {
                if (node.Nodes != null && node.Nodes.Count > 0)
                {
                    foreach (TreeNode child in node.Nodes)  // просматириваем кусты дерева проектов
                    {
                        if (child != null)
                        {
                            if (child.Text == string.Format("Куст: {0}", work.Bush))
                            {
                                InsertWorkToBushNode(work, child);
                                return;
                            }
                        }
                    }

                    // ----- вставляем в дерево проектов новый куст ----

                    InsertNewBushNode(work, node);
                }
                else
                {
                    // ----- нету ни одного куста в проекте -----
                }
            }
        }

        /// <summary>
        /// Добавить новый куст в месторождение
        /// </summary>
        /// <param name="project">Втавляемый проект</param>
        /// <param name="node">Узел в который вставлять</param>
        protected void InsertNewBushNode(Work work, TreeNode node)
        {
            try
            {
                TreeNode bushNode = new TreeNode(string.Format("Куст: {0}", work.Bush), 1, 1);
                TreeNode wellNode = new TreeNode(string.Format("Скважина: {0}", work.Well), 2, 2);

                TreeNode jobNode = new TreeNode(string.Format("Задание: {0}", work.Description), 3, 3);
                jobNode.Tag = work;

                wellNode.Nodes.Add(jobNode);
                bushNode.Nodes.Add(wellNode);

                node.Nodes.Add(bushNode);
            }
            catch { }
        }

        /// <summary>
        /// Вставить в дерево проектов в существующий куст
        /// </summary>
        /// <param name="project">Вставляемый проект</param>
        /// <param name="node">Узел в который вставлять</param>
        protected void InsertWorkToBushNode(Work work, TreeNode node)
        {
            if (node != null)
            {
                if (node.Nodes != null && node.Nodes.Count > 0)
                {
                    foreach (TreeNode child in node.Nodes)  // просматириваем кусты дерева проектов
                    {
                        if (child != null)
                        {
                            if (child.Text == string.Format("Скважина: {0}", work.Well))
                            {
                                InsertNewJobNode(work, child);
                                return;
                            }
                        }
                    }

                    // ----- вставляем в дерево проектов новый куст ----

                    InsertNewWellNode(work, node);
                }
                else
                {
                    // ----- нету ни одного куста в проекте -----
                }
            }
        }

        /// <summary>
        /// Добавить новую скважину в куст
        /// </summary>
        /// <param name="project">Втавляемый проект</param>
        /// <param name="node">Узел в который вставлять</param>
        protected void InsertNewWellNode(Work work, TreeNode node)
        {
            try
            {
                TreeNode wellNode = new TreeNode(string.Format("Скважина: {0}", work.Well), 2, 2);
                TreeNode jobNode = new TreeNode(string.Format("Задание: {0}", work.Description), 3, 3);
                jobNode.Tag = work;

                wellNode.Nodes.Add(jobNode);
                node.Nodes.Add(wellNode);
            }
            catch { }
        }

        /// <summary>
        /// Добавить новую работу в скважину
        /// </summary>
        /// <param name="project"></param>
        /// <param name="node"></param>
        protected void InsertNewJobNode(Work work, TreeNode node)
        {
            try
            {
                TreeNode jobNode = new TreeNode(string.Format("Задание: {0}", work.Description), 3, 3);
                jobNode.Tag = work;

                node.Nodes.Add(jobNode);
            }
            catch { }
        }

        /// <summary>
        /// Отобразить информацию о выделенном елементе
        /// </summary>
        /// <param name="node">Выделенный элемент</param>
        protected void ShowSelectedNode(TreeNode node)
        {
            Work work = node.Tag as Work;
            if (work != null)
            {
                // --------------- выводим полную информацию о проекте ---------------

                string total_str = string.Format(" Заказчик: {1}{1}{8}{0} Исполнитель: {1}{1}{9}{0} Месторождение: {1}{1}{2}{0} Куст: {1}{1}{1}{3}{0} Скважина: {1}{1}{4}{0} " +
                    "Работа: {1}{1}{1}{5}{0} Дата создания работы: {1}{6}{0} Стартовая глубина: {1}{7} м{0}",
                    Constants.vbCrLf, Constants.vbTab, work.Field, work.Bush, work.Well, work.Description, work.StartTime, work.StartingDepth, work.Customer, 
                    work.Performer);

                textBox1.Text = total_str;
            }
            else
            {
                TreeNode parent = node.Parent;
                List<TreeNode> nodes = new List<TreeNode>();

                nodes.Add(node);
                while (parent != null)
                {
                    nodes.Add(parent);
                    parent = parent.Parent;
                }
                nodes.Reverse();
                TreeNode[] a_nodes = nodes.ToArray();

                if (a_nodes != null)
                {
                    string total = string.Empty;
                    foreach (TreeNode c_node in a_nodes)
                    {
                        total += string.Format("{0}{1}", c_node.Text, Constants.vbCrLf); 
                    }

                    textBox1.Text = total;
                }
            }
        }

        /// <summary>
        /// отобразить информацию о выбранном проекте
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewWorks_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeViewWorks.SelectedNode != null)
            {
                ShowSelectedNode(treeViewWorks.SelectedNode);
            }
        }

        /// <summary>
        /// добавляем новое задание
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InsertNewProject_Click(object sender, EventArgs e)
        {
            InsertNewWorkForm frm = new InsertNewWorkForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                Work work = new Work();

                work.Field = frm.SelectedField;
                work.Bush = frm.SelectedBush;

                work.Well = frm.SelectedWell;
                work.Description = frm.SelectedDescription;

                work.StartingDepth = frm.SelectedDept;

                _app.Works.Add(work);
                InsertWorkToTree(work);
            }
        }

        /// <summary>
        /// редактировать задание
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditProject_Click(object sender, EventArgs e)
        {
            if (treeViewWorks.SelectedNode != null)
            {
                Work work = treeViewWorks.SelectedNode.Tag as Work;
                if (work != null)
                {
                    InsertNewWorkForm frm = new InsertNewWorkForm();
                    frm.Text = "Редактирование задания";

                    frm.SelectedField = work.Field;
                    frm.SelectedBush = work.Bush;

                    frm.SelectedWell = work.Well;
                    frm.SelectedDescription = work.Description;

                    frm.SelectedDept = work.StartingDepth;

                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        work.Field = frm.SelectedField;
                        work.Bush = frm.SelectedBush;

                        work.Well = frm.SelectedWell;
                        work.Description = frm.SelectedDescription;

                        work.StartingDepth = frm.SelectedDept;

                        treeViewWorks.Nodes.Clear();
                        WorksForm_Load(this, EventArgs.Empty);
                    }
                }
                else
                    MessageBox.Show(this, "Выберите задание для редактирования.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show(this, "Выберите задание для редактирования.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected Work selected = null;

        /// <summary>
        /// Выбранная работа
        /// </summary>
        public Work SelectedWork
        {
            get
            {
                return selected;
            }
        }

        /// <summary>
        /// выбрать другое задание
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenProject_Click(object sender, EventArgs e)
        {
            if (treeViewWorks.SelectedNode != null)
            {
                Work work = treeViewWorks.SelectedNode.Tag as Work;
                if (work != null)
                {
                    selected = work;
                    DialogResult = DialogResult.OK;
                }
                else
                    MessageBox.Show(this, "Выберите задание для редактирования.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show(this, "Выберите задание для редактирования.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}