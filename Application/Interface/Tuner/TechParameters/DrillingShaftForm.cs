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
    public partial class DrillingShaftForm : Form
    {
        SgtApplication _app = null;

        public DrillingShaftForm()
        {
            InitializeComponent();

            _app = SgtApplication.CreateInstance();
            if (_app != null)
            {
            }
            else
            {
                MessageBox.Show(this, "Во время загрузки возникла ошибка",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrillingShaftForm_Load(object sender, EventArgs e)
        {
            textBoxSizeCandle.Text = string.Format("{0:F2}", _app.Technology.P0203.SizeCandle);

            textBoxLowerSizeCandle.Text = string.Format("{0:F2}", _app.Technology.P0203.LowerSizeCandle);
            textBoxUpperSizeCandle.Text = string.Format("{0:F2}", _app.Technology.P0203.UpperSizeCandle);

            textBoxDop.Text = string.Format("{0:F2}", _app.Technology.P0203.Deviation);

            try
            {
                int number = 1;
                foreach (tubeInfo info in _app.Technology.P0203.Tubes)
                {
                    if (info != null)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        dataGridView1.Rows.Add(row);

                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = number++;

                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = info.Lenght;
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Value = info.Number;
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3].Value = info.Comment;
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[4].Value = info.Total;                        
                        
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Tag = info;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Редактируем трубку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows != null)
                {
                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        DataGridViewRow selected = dataGridView1.SelectedRows[0];
                        if (selected != null && selected.Tag != null)
                        {
                            tubeInfo sel_tube = selected.Tag as tubeInfo;
                            if (sel_tube != null)
                            {
                                int sel_index = selected.Index;
                                if (sel_index > -1 && sel_index < dataGridView1.Rows.Count)
                                {
                                    if (sel_index == 0)
                                    {
                                        // -------- редактируем выбранную трубку

                                        DrillingShaftTubeForm frm = new DrillingShaftTubeForm();

                                        frm.textBoxLenght.Text = selected.Cells[1].Value.ToString();
                                        frm.textBoxNumber.Text = selected.Cells[2].Value.ToString();

                                        frm.textBoxComment.Text = selected.Cells[3].Value.ToString();

                                        if (frm.ShowDialog(this) == DialogResult.OK)
                                        {
                                            selected.Cells[1].Value = frm.textBoxLenght.Text;
                                            selected.Cells[2].Value = frm.textBoxNumber.Text;
                                            selected.Cells[3].Value = frm.textBoxComment.Text;
                                            selected.Cells[4].Value = frm.textBoxLenght.Text;

                                            calc_data_grid();
                                        }
                                    }
                                    else
                                    {
                                        int f_index = sel_index - 1;
                                        DataGridViewRow f_row = dataGridView1.Rows[f_index];

                                        if (f_row != null)
                                        {
                                            float len = SgtApplication.ParseSingle(f_row.Cells[1].Value.ToString());
                                            if (len <= 0)
                                            {
                                                MessageBox.Show(this, "Текущую трубку нельзя редактировать, потому что не определенна предыдущая трубка", "Сообщение",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                            }
                                            else
                                            {
                                                // -------- редактируем выбранную трубку

                                                DrillingShaftTubeForm frm = new DrillingShaftTubeForm();

                                                frm.textBoxLenght.Text = selected.Cells[1].Value.ToString();

                                                if (sel_tube.Lenght <= 0)
                                                {
                                                    frm.textBoxNumber.Text = f_row.Cells[2].Value.ToString();
                                                }
                                                else
                                                    frm.textBoxNumber.Text = selected.Cells[2].Value.ToString();

                                                frm.textBoxComment.Text = selected.Cells[3].Value.ToString();

                                                if (frm.ShowDialog(this) == DialogResult.OK)
                                                {
                                                    selected.Cells[1].Value = frm.textBoxLenght.Text;
                                                    selected.Cells[2].Value = frm.textBoxNumber.Text;
                                                    selected.Cells[3].Value = frm.textBoxComment.Text;

                                                    calc_data_grid();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void accept_Click(object sender, EventArgs e)
        {
            try
            {
                float s_candle = SgtApplication.ParseSingle(textBoxSizeCandle.Text);
                if (float.IsNaN(s_candle) == false)
                {
                    if (s_candle >= 0)
                    {
                        _app.Technology.P0203.SizeCandle = s_candle;
                    }
                    else
                    {
                        MessageBox.Show(this, "Средняя длина свечи не может быть меньше нуля", "Сообщение", 
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        textBoxSizeCandle.Focus();
                        textBoxSizeCandle.SelectAll();

                        DialogResult = System.Windows.Forms.DialogResult.None;
                        return;
                    }
                }
                else
                {
                    MessageBox.Show(this, "Указано не корректное значение средней длины свечи", "Сообщение",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    textBoxSizeCandle.Focus();
                    textBoxSizeCandle.SelectAll();

                    DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }

                float s_lower = SgtApplication.ParseSingle(textBoxLowerSizeCandle.Text);
                if (float.IsNaN(s_lower) == false)
                {
                    if (s_lower >= 0)
                    {
                        _app.Technology.P0203.LowerSizeCandle = s_lower;
                    }
                    else
                    {
                        MessageBox.Show(this, "Нижняя граница длины свечи не может быть меньше нуля", "Сообщение",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        textBoxLowerSizeCandle.Focus();
                        textBoxLowerSizeCandle.SelectAll();

                        DialogResult = System.Windows.Forms.DialogResult.None;
                        return;
                    }
                }
                else
                {
                    MessageBox.Show(this, "Указано не корректное значение нижней границы длины свечи", "Сообщение",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    textBoxLowerSizeCandle.Focus();
                    textBoxLowerSizeCandle.SelectAll();

                    DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }

                float s_upper = SgtApplication.ParseSingle(textBoxUpperSizeCandle.Text);
                if (float.IsNaN(s_upper) == false)
                {
                    if (s_upper >= 0)
                    {
                        _app.Technology.P0203.UpperSizeCandle = s_upper;
                    }
                    else
                    {
                        MessageBox.Show(this, "Верхняя граница длины свечи не может быть меньше нуля", "Сообщение",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        textBoxUpperSizeCandle.Focus();
                        textBoxUpperSizeCandle.SelectAll();

                        DialogResult = System.Windows.Forms.DialogResult.None;
                        return;
                    }
                }
                else
                {
                    MessageBox.Show(this, "Указано не корректное значение верхней границы длины свечи", "Сообщение",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    textBoxUpperSizeCandle.Focus();
                    textBoxUpperSizeCandle.SelectAll();

                    DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }

                float dep = SgtApplication.ParseSingle(textBoxDop.Text);
                if (float.IsNaN(dep) == false)
                {
                    if (dep >= 0)
                    {
                        _app.Technology.P0203.Deviation = dep;
                    }
                    else
                    {
                        MessageBox.Show(this, "Значение допустимого отклонения не может быть меньше нуля", "Сообщение",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        textBoxDop.Focus();
                        textBoxDop.SelectAll();

                        DialogResult = System.Windows.Forms.DialogResult.None;
                        return;
                    }
                }
                else
                {
                    MessageBox.Show(this, "Указано не корректное значение допустимого отклонения", "Сообщение",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    textBoxDop.Focus();
                    textBoxDop.SelectAll();

                    DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }

                if (check_rows())
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row != null)
                        {
                            if (row.Tag != null)
                            {
                                tubeInfo r_info = row.Tag as tubeInfo;
                                if (r_info != null)
                                {
                                    r_info.Lenght = float.Parse(row.Cells[1].Value.ToString());
                                    r_info.Number = int.Parse(row.Cells[2].Value.ToString());

                                    r_info.Comment = row.Cells[3].Value.ToString();
                                }
                            }
                        }
                    }
                }
                else
                {
                    DialogResult = DialogResult.None;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1_DoubleClick(dataGridView1, EventArgs.Empty);
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 32)
            {
                dataGridView1_DoubleClick(dataGridView1, EventArgs.Empty);
            }
        }

        /// <summary>
        /// пересчитать таблицу
        /// </summary>
        protected void calc_data_grid()
        {
            float total = 0.0f;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row != null)
                {
                    float lenght = SgtApplication.ParseSingle(row.Cells[1].Value.ToString());//float.Parse(row.Cells[1].Value.ToString());
                    if (float.IsNaN(lenght) == false && lenght > 0)
                    {
                        float tlt = SgtApplication.ParseSingle(row.Cells[1].Value.ToString());// float.Parse(row.Cells[1].Value.ToString());
                        if (float.IsNaN(tlt) == false)
                        {
                            total = total + tlt;
                        }

                        row.Cells[4].Value = total;

                    }
                    else
                        break;
                }
            }
        }

        /// <summary>
        /// jxboftv nf,kbwe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows != null)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow selected = dataGridView1.SelectedRows[0];
                    if (selected != null)
                    {
                        int number = selected.Index;
                        if (number > -1)
                        {
                            for (int index = number; index < dataGridView1.Rows.Count; index++)
                            {
                                DataGridViewRow row = dataGridView1.Rows[index];
                                if (row != null)
                                {
                                    row.Cells[1].Value = 0.0f;
                                    row.Cells[2].Value = 0.0f;
                                    row.Cells[3].Value = string.Empty;
                                    row.Cells[4].Value = 0.0f;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected bool check_rows()
        {
            try
            {
                int current = 0;
                for (int index = 0; index < dataGridView1.Rows.Count; index++)
                {
                    int len = int.Parse(dataGridView1.Rows[index].Cells[1].Value.ToString());

                    if (len > 0)
                    {
                        if (index == 0)
                        {
                            current = int.Parse(dataGridView1.Rows[index].Cells[2].Value.ToString());
                        }
                        else
                        {
                            int b_cru = int.Parse(dataGridView1.Rows[index].Cells[2].Value.ToString());
                            if (b_cru < current)
                            {
                                MessageBox.Show(this, "Не верно указан номер трубки", "Сообщение",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                                dataGridView1.Rows[index - 1].Selected = true;
                                dataGridView1.FirstDisplayedScrollingRowIndex = index - 1;

                                return false;
                            }
                            else
                                current = b_cru;
                        }
                    }
                    else
                        break;
                }
            }
            catch { }
            return true;
        }
    }
}