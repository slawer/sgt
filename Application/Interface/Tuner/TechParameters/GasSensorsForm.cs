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
    public partial class GasSensorsForm : Form
    {
        SgtApplication _app = null;

        public GasSensorsForm()
        {
            InitializeComponent();

            _app = SgtApplication.CreateInstance();

            for (int i = 0; i < 10; i++)
            {
                dataGridView1.Rows.Add();
            }

            dataGridView1.Rows[0].Cells[0].Value = "Датчик 1";
            dataGridView1.Rows[1].Cells[0].Value = "Датчик 2";

            dataGridView1.Rows[2].Cells[0].Value = "Датчик 3";
            dataGridView1.Rows[3].Cells[0].Value = "Датчик 4";

            dataGridView1.Rows[4].Cells[0].Value = "Датчик 5";
            dataGridView1.Rows[5].Cells[0].Value = "Датчик 6";

            dataGridView1.Rows[6].Cells[0].Value = "Датчик 7";
            dataGridView1.Rows[7].Cells[0].Value = "Датчик 8";

            dataGridView1.Rows[8].Cells[0].Value = "Датчик 9";
            dataGridView1.Rows[9].Cells[0].Value = "Датчик 10";
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GasSensorsForm_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows[0].Cells[1].Value = _app.Technology.P0006.Lower; // "Газ Д1 (CH4) Датчик";
            dataGridView1.Rows[0].Cells[2].Value = _app.Technology.P0006.Upper; // "Газ Д1 (CH4) Датчик";

            dataGridView1.Rows[1].Cells[1].Value = _app.Technology.P06_1.Lower; // "Газ Д2 (CH4) Датчик";
            dataGridView1.Rows[1].Cells[2].Value = _app.Technology.P06_1.Upper; // "Газ Д2 (CH4) Датчик";

            dataGridView1.Rows[2].Cells[1].Value = _app.Technology.P06_2.Lower; // "Газ Д3 (CH4) Датчик";
            dataGridView1.Rows[2].Cells[2].Value = _app.Technology.P06_2.Upper; // "Газ Д3 (CH4) Датчик";

            dataGridView1.Rows[3].Cells[1].Value = _app.Technology.P06_3.Lower; // "Газ Д4 (CH4) Датчик";
            dataGridView1.Rows[3].Cells[2].Value = _app.Technology.P06_3.Upper; // "Газ Д4 (CH4) Датчик";

            dataGridView1.Rows[4].Cells[1].Value = _app.Technology.P06_4.Lower; // "Газ Д4 (CH4) Датчик";
            dataGridView1.Rows[4].Cells[2].Value = _app.Technology.P06_4.Upper; // "Газ Д4 (CH4) Датчик";

            dataGridView1.Rows[5].Cells[1].Value = _app.Technology.P06_5.Lower; // "Газ Д5 (H2S) Датчик";
            dataGridView1.Rows[5].Cells[2].Value = _app.Technology.P06_5.Upper; // "Газ Д5 (H2S) Датчик";

            dataGridView1.Rows[6].Cells[1].Value = _app.Technology.P06_6.Lower; // "Газ Д6 (H2S) Датчик";
            dataGridView1.Rows[6].Cells[2].Value = _app.Technology.P06_6.Upper; // "Газ Д6 (H2S) Датчик";

            dataGridView1.Rows[7].Cells[1].Value = _app.Technology.P06_7.Lower; // "Газ Д7 (H2S) Датчик";
            dataGridView1.Rows[7].Cells[2].Value = _app.Technology.P06_7.Upper; // "Газ Д7 (H2S) Датчик";

            dataGridView1.Rows[8].Cells[1].Value = _app.Technology.P06_8.Lower; // "Газ Д8 (H2S) Датчик";
            dataGridView1.Rows[8].Cells[2].Value = _app.Technology.P06_8.Upper; // "Газ Д8 (H2S) Датчик";

            dataGridView1.Rows[9].Cells[1].Value = _app.Technology.P06_9.Lower; // "Газ Д9 (CH4) Датчик";
            dataGridView1.Rows[9].Cells[2].Value = _app.Technology.P06_9.Upper; // "Газ Д9 (CH4) Датчик";
        }

        /// <summary>
        /// проверяем данные на корректность
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accept_Click(object sender, EventArgs e)
        {
            try
            {
                _app.Technology.P0006.Lower = SgtApplication.ParseSingle(dataGridView1.Rows[0].Cells[1].Value.ToString());
                _app.Technology.P0006.Upper = SgtApplication.ParseSingle(dataGridView1.Rows[0].Cells[2].Value.ToString());

                _app.Technology.P06_1.Lower = SgtApplication.ParseSingle(dataGridView1.Rows[1].Cells[1].Value.ToString());
                _app.Technology.P06_1.Upper = SgtApplication.ParseSingle(dataGridView1.Rows[1].Cells[2].Value.ToString());

                _app.Technology.P06_2.Lower = SgtApplication.ParseSingle(dataGridView1.Rows[2].Cells[1].Value.ToString());
                _app.Technology.P06_2.Upper = SgtApplication.ParseSingle(dataGridView1.Rows[2].Cells[2].Value.ToString());

                _app.Technology.P06_3.Lower = SgtApplication.ParseSingle(dataGridView1.Rows[3].Cells[1].Value.ToString());
                _app.Technology.P06_3.Upper = SgtApplication.ParseSingle(dataGridView1.Rows[3].Cells[2].Value.ToString());

                _app.Technology.P06_4.Lower = SgtApplication.ParseSingle(dataGridView1.Rows[4].Cells[1].Value.ToString());
                _app.Technology.P06_4.Upper = SgtApplication.ParseSingle(dataGridView1.Rows[4].Cells[2].Value.ToString());

                _app.Technology.P06_5.Lower = SgtApplication.ParseSingle(dataGridView1.Rows[5].Cells[1].Value.ToString());
                _app.Technology.P06_5.Upper = SgtApplication.ParseSingle(dataGridView1.Rows[5].Cells[2].Value.ToString());

                _app.Technology.P06_6.Lower = SgtApplication.ParseSingle(dataGridView1.Rows[6].Cells[1].Value.ToString());
                _app.Technology.P06_6.Upper = SgtApplication.ParseSingle(dataGridView1.Rows[6].Cells[2].Value.ToString());

                _app.Technology.P06_7.Lower = SgtApplication.ParseSingle(dataGridView1.Rows[7].Cells[1].Value.ToString());
                _app.Technology.P06_7.Upper = SgtApplication.ParseSingle(dataGridView1.Rows[7].Cells[2].Value.ToString());

                _app.Technology.P06_8.Lower = SgtApplication.ParseSingle(dataGridView1.Rows[8].Cells[1].Value.ToString());
                _app.Technology.P06_8.Upper = SgtApplication.ParseSingle(dataGridView1.Rows[8].Cells[2].Value.ToString());

                _app.Technology.P06_9.Lower = SgtApplication.ParseSingle(dataGridView1.Rows[9].Cells[1].Value.ToString());
                _app.Technology.P06_9.Upper = SgtApplication.ParseSingle(dataGridView1.Rows[9].Cells[2].Value.ToString());
            }
            catch
            {
                MessageBox.Show(this, "Не удалось сохранить данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        // -------------------------------------------

        protected float old_value = float.NaN;
        protected float new_value = float.NaN;

        /// <summary>
        /// начали редактировать ячейку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            old_value = Convert.ToSingle(dataGridView1[e.ColumnIndex, e.RowIndex].Value);
        }

        /// <summary>
        /// завершаем редактирование ячейки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1[e.ColumnIndex, e.RowIndex].Value = new_value;
        }

        /// <summary>
        /// разбираем введенные данные
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            new_value = old_value;
            if (Type.GetTypeCode(e.Value.GetType()) == TypeCode.String)
            {
                try
                {
                    float n_val = SgtApplication.ParseSingle(e.Value.ToString());
                    if (!float.IsNaN(n_val))
                    {
                        e.ParsingApplied = true;
                        new_value = n_val;
                    }
                    else
                    {
                        MessageBox.Show(this, "Данное число не корректно", "Сообщение",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                catch { }
            }
        }
    }
}