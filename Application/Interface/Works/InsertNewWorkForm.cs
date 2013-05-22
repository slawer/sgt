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
    public partial class InsertNewWorkForm : Form
    {
        SgtApplication _app = null;
        public InsertNewWorkForm()
        {
            InitializeComponent();
            _app = SgtApplication.CreateInstance();
        }

        /// <summary>
        /// Месторождение
        /// </summary>
        public string SelectedField
        {
            get
            {
                return comboBoxField.Text;
            }

            set
            {
                comboBoxField.Text = value;
            }
        }

        /// <summary>
        /// Куст
        /// </summary>
        public string SelectedBush
        {
            get
            {
                return comboBoxBush.Text;
            }

            set
            {
                comboBoxBush.Text = value;
            }
        }

        /// <summary>
        /// Скважина
        /// </summary>
        public string SelectedWell
        {
            get
            {
                return comboBoxWell.Text;
            }

            set
            {
                comboBoxWell.Text = value;
            }
        }

        /// <summary>
        /// Описание задания
        /// </summary>
        public string SelectedDescription
        {
            get
            {
                return textBoxDescription.Text;
            }

            set
            {
                textBoxDescription.Text = value;
            }
        }

        /// <summary>
        /// Стартовая глубина
        /// </summary>
        public int SelectedDept
        {
            get
            {
                try
                {
                    return int.Parse(textBoxStartDept.Text);
                }
                catch { }
                return 0;
            }

            set
            {
                textBoxStartDept.Text = value.ToString();
            }
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InsertNewWorkForm_Load(object sender, EventArgs e)
        {
            List<Work> works = _app.Works;
            if (works != null)
            {
                foreach (Work work in works)
                {
                    if (work != null)
                    {
                        if (!comboBoxField.Items.Contains(work.Field)) comboBoxField.Items.Add(work.Field);
                        if (!comboBoxBush.Items.Contains(work.Bush)) comboBoxBush.Items.Add(work.Bush);

                        if (!comboBoxWell.Items.Contains(work.Well)) comboBoxWell.Items.Add(work.Well);
                    }
                }
            }

            textBoxStartDept.Text = "0";
        }

        /// <summary>
        /// проверяем данные на корректность
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accept_Click(object sender, EventArgs e)
        {
            float v = SgtApplication.ParseSingle(textBoxStartDept.Text);
            if (float.IsNaN(v) == true)
            {
                MessageBox.Show(this, "Указано не верно значение Стартовая глубина", "Предупреждение", 
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                DialogResult = System.Windows.Forms.DialogResult.None;
            }
        }
    }
}