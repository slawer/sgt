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
    public partial class TechTunerForm : Form
    {
        SgtApplication _app = null;

        public TechTunerForm()
        {
            InitializeComponent();

            _app = SgtApplication.CreateInstance();
            if (_app != null)
            {
                init_grid();
            }
            else
            {
                MessageBox.Show(this, "Во время загрузки возникла ошибка", 
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        /// <summary>
        /// Инициализировать таблицу констант
        /// </summary>
        protected void init_grid()
        {
            DataGridViewRow locking_weight_hook = new DataGridViewRow();
            DataGridViewRow locking_value_rotor_speed = new DataGridViewRow();

            DataGridViewRow locking_value_load = new DataGridViewRow();
            DataGridViewRow locking_pressure = new DataGridViewRow();

            DataGridViewRow locking_speed_talbloka = new DataGridViewRow();
            DataGridViewRow interval_pzr = new DataGridViewRow();

            DataGridViewRow drilling_interval = new DataGridViewRow();
            DataGridViewRow size_bottom_hole_zone = new DataGridViewRow();

            DataGridViewRow averaging_time_drilling = new DataGridViewRow();
            DataGridViewRow averaging_mechanical_speed = new DataGridViewRow();

            DataGridViewRow size_layout_top_column = new DataGridViewRow();
            DataGridViewRow size_layout_bottom_column = new DataGridViewRow();

            DataGridViewRow averaging_period = new DataGridViewRow();
           
            dataGridView.Rows.Add(locking_weight_hook);
            dataGridView.Rows.Add(locking_value_rotor_speed);
            
            dataGridView.Rows.Add(locking_value_load);
            dataGridView.Rows.Add(locking_pressure);
            
            dataGridView.Rows.Add(locking_speed_talbloka);
            dataGridView.Rows.Add(interval_pzr);
            
            dataGridView.Rows.Add(drilling_interval);
            dataGridView.Rows.Add(size_bottom_hole_zone);
            
            dataGridView.Rows.Add(averaging_time_drilling);
            dataGridView.Rows.Add(averaging_mechanical_speed);

            dataGridView.Rows.Add(size_layout_top_column);
            dataGridView.Rows.Add(size_layout_bottom_column);

            dataGridView.Rows.Add(averaging_period);

            dataGridView.Rows[0].Cells[0].Value = "Блокировочное значение веса на крюке (Тс)";
            dataGridView.Rows[1].Cells[0].Value = "Блокировочное значене оборотов ротора (Об/Мин)";

            dataGridView.Rows[2].Cells[0].Value = "Блокировочное значение нагрузки (Тс)";
            dataGridView.Rows[3].Cells[0].Value = "Блокировочное значение давления на входе (кг/см2)";

            dataGridView.Rows[4].Cells[0].Value = "Блокировочное значение скорости тальблока (м/сек)";
            dataGridView.Rows[5].Cells[0].Value = "Интервал ПЗР (м)";

            dataGridView.Rows[6].Cells[0].Value = "Интервал бурения (м)";
            dataGridView.Rows[7].Cells[0].Value = "Размер призабойной зоны (м)";

            dataGridView.Rows[8].Cells[0].Value = "Шаг усреднения времени бурения 1м (м)";
            dataGridView.Rows[9].Cells[0].Value = "Шаг усреднения механической скорости(м)";

            dataGridView.Rows[10].Cells[0].Value = "Размер компоновки верха колонны (м)";
            dataGridView.Rows[11].Cells[0].Value = "Размер компоновки низа колонны (м)";

            dataGridView.Rows[12].Cells[0].Value = "Период усреднения скорости тальблока (мсек)";
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TechTunerForm_Load(object sender, EventArgs e)
        {
            // "Блокировочное значение веса на крюке";
            dataGridView.Rows[0].Cells[1].Value = string.Format("{0:F2}", _app.Technology.LockingWeightHook);

            // "Блокировочное значене оборотов ротора";
            dataGridView.Rows[1].Cells[1].Value = string.Format("{0:F2}", _app.Technology.LockingValueRotorSpeed);

            // "Блокировочное значение нагрузки";
            dataGridView.Rows[2].Cells[1].Value = string.Format("{0:F2}", _app.Technology.LockingValueLoad);

            // "Блокировочное значение давления на входе";
            dataGridView.Rows[3].Cells[1].Value = string.Format("{0:F2}", _app.Technology.LockingPressure);

            // "Блокировочное значение скорости тальблока";
            dataGridView.Rows[4].Cells[1].Value = string.Format("{0:F2}", _app.Technology.LockingSpeedTalblok);

            // "Интервал ПЗР";
            dataGridView.Rows[5].Cells[1].Value = string.Format("{0:F2}", _app.Technology.IntervalPzr);

            // "Интервал бурения";
            dataGridView.Rows[6].Cells[1].Value = string.Format("{0:F2}", _app.Technology.DrillingInterval);

            // "Размер призабойной зоны";
            dataGridView.Rows[7].Cells[1].Value = string.Format("{0:F2}", _app.Technology.SizeBottomHoleZone);

            // "Шаг усреднения времени бурения 1м";
            dataGridView.Rows[8].Cells[1].Value = string.Format("{0:F2}", _app.Technology.P0209.AveragingStep);

            // "Шаг усреднения механической скорости";
            dataGridView.Rows[9].Cells[1].Value = string.Format("{0:F2}", _app.Technology.P0208.AveragingStep);

            // "Размер компоновки верха колонны";
            dataGridView.Rows[10].Cells[1].Value = string.Format("{0:F2}", _app.Technology.SizeLayoutTopColumn);

            // "Размер компоновки низа колонны";
            dataGridView.Rows[11].Cells[1].Value = string.Format("{0:F2}", _app.Technology.SizeLayoutBottomColumn);

            // "Период усреднения скорости тальблока";
            dataGridView.Rows[12].Cells[1].Value = string.Format("{0:N2}", _app.Technology.P0103.AveragingPeriod);

            // --------------- размер свечи -------------------


            // ---------------- настраиваем методы и режимы ---------------------------

            switch (_app.Technology.TechnologicalRegimeWeightHook)
            {
                case TechnologicalRegimeWeightHook.Weight:

                    radioButtonPoVesy.Checked = true;
                    break;

                case TechnologicalRegimeWeightHook.Wedges:

                    radioButtonPoKlinam.Checked = true;
                    break;

                case TechnologicalRegimeWeightHook.WeightOrWedges:

                    radioButtonPoVesyIliKlinam.Checked = true;
                    break;

                default:
                    
                    radioButtonPoVesy.Checked = false;
                    radioButtonPoKlinam.Checked = false;

                    radioButtonPoVesyIliKlinam.Checked = false;
                    break;
            }

            switch (_app.Technology.TechnologicalRegimStudy)
            {
                case TechnologicalRegimStudy.RotorSpeed:

                    radioButtonOborotiRotora.Checked = true;
                    break;

                case TechnologicalRegimStudy.SpeedTalblok:

                    radioButtonSkorostTalbloka.Checked = true;
                    break;

                case TechnologicalRegimStudy.RotorSpeenOrSpeedTalblok:

                    radioButtonRotorIliTalblok.Checked = true;
                    break;

                default:

                    radioButtonOborotiRotora.Checked = false;
                    radioButtonSkorostTalbloka.Checked = false;

                    radioButtonRotorIliTalblok.Checked = false;
                    break;
            }

            switch (_app.Technology.TechnologicalRegimDrilling)
            {
                case TechnologicalRegimDrilling.Pressure:

                    radioButtonDavlenie.Checked = true;
                    break;

                case TechnologicalRegimDrilling.PressureAndLoad:

                    radioButtonDavlenieINagryska.Checked = true;
                    break;

                default:

                    radioButtonDavlenie.Checked = false;
                    radioButtonDavlenieINagryska.Checked = false;

                    break;
            }

            switch (_app.Technology.P0203.Algorithm)
            {
                case CalculateCandleAlgorithm.LengthOfCandles:

                    radioButton1.Checked = true;
                    radioButton2.Checked = false;

                    break;

                case CalculateCandleAlgorithm.LenghtOfCandleCount:

                    radioButton1.Checked = false;
                    radioButton2.Checked = true;
                    break;

                case CalculateCandleAlgorithm.Default:

                    radioButton1.Checked = false;
                    radioButton2.Checked = false;

                    break;

                default:
                    break;
            }

            if (_app.GuiMode == GuiMode.User)
            {
                gonfig_parameters_btn.Enabled = false;
                button3.Enabled = false;
            }
        }

        /// <summary>
        /// определяем коэффициента пересчета для ходов насоса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pump_movies_btn_Click(object sender, EventArgs e)
        {
            PumpMovesForm frm = new PumpMovesForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                _app.Technology.P0109.ScaleFactorPump1 = SgtApplication.ParseSingle(frm.textBoxAnalogPervii.Text);
                _app.Technology.P0109.ScaleFactorPump2 = SgtApplication.ParseSingle(frm.textBoxAnalogVtoroi.Text);

                _app.Technology.P0112.ScaleFactorPump1 = SgtApplication.ParseSingle(frm.textBoxAsyPervii.Text);
                _app.Technology.P0112.ScaleFactorPump2 = SgtApplication.ParseSingle(frm.textBoxAsyVtoroi.Text);
            }
        }

        /// <summary>
        /// определяет настройки датчиков газа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gas_sensors_btn_Click(object sender, EventArgs e)
        {
            GasSensorsForm frm = new GasSensorsForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
            }
        }

        /// <summary>
        /// определяем вес колонны
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void weight_column_btn_Click(object sender, EventArgs e)
        {
            WeightColumnForm frm = new WeightColumnForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
            }
        }

        /// <summary>
        /// определяем вес на крюке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void weight_hook_btn_Click(object sender, EventArgs e)
        {
            WeightHookForm frm = new WeightHookForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
            }
        }

        /// <summary>
        /// настраиваем объем раствора в емкостях
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void volume_solution_btn_Click(object sender, EventArgs e)
        {
            VolumeSolutionTanksForm frm = new VolumeSolutionTanksForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
            }
        }

        /// <summary>
        /// определяем фиксированное значение расхода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void docked_rate_btn_Click(object sender, EventArgs e)
        {
            DockedRateForm frm = new DockedRateForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
            }
        }

        /// <summary>
        /// определяем фиксируемое значение суммарного объема
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void docked_volume_btn_Click(object sender, EventArgs e)
        {
            DockedVolumeForm frm = new DockedVolumeForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
            }
        }

        /// <summary>
        /// определяем сигнал тревоги газа 20
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            GasAlarmForm frm = new GasAlarmForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
            }
        }

        /// <summary>
        /// расход на входе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void flow_inlet_btn_Click(object sender, EventArgs e)
        {
            FlowInletForm frm = new FlowInletForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
            }
        }

        // --------------------------- реадктируем таблицу ---------------------

        protected float old_value = float.NaN;
        protected float new_value = float.NaN;

        /// <summary>
        /// начался процесс редактирования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            old_value = Convert.ToSingle(dataGridView[e.ColumnIndex, e.RowIndex].Value);
        }

        /// <summary>
        /// закончен процесс редактирования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView[e.ColumnIndex, e.RowIndex].Value = string.Format("{0:F2}", new_value);
            switch (e.RowIndex)
            {
                case 0: _app.Technology.LockingWeightHook = new_value; break;
                case 1: _app.Technology.LockingValueRotorSpeed = new_value; break;

                case 2: _app.Technology.LockingValueLoad = new_value; break;
                case 3: _app.Technology.LockingPressure = new_value; break;

                case 4: _app.Technology.LockingSpeedTalblok = new_value; break;
                case 5: _app.Technology.IntervalPzr = new_value; break;

                case 6: _app.Technology.DrillingInterval = new_value; break;
                case 7: _app.Technology.SizeBottomHoleZone = new_value; break;

                case 8: _app.Technology.P0209.AveragingStep = new_value; break;
                case 9: _app.Technology.P0208.AveragingStep = new_value; break;

                case 10: _app.Technology.SizeLayoutTopColumn = new_value; break;
                case 11: _app.Technology.SizeLayoutBottomColumn = new_value; break;

                case 12: _app.Technology.P0103.AveragingPeriod = (int)new_value; break;

                default:
                    break;
            }
        }

        /// <summary>
        /// разбираем введенное значение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
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

        // ------------------------------------------------------------------

        /// <summary>
        /// сохраняем некоторые настройки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TechTunerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // --------------- размер свечи -------------------

            /*float s_candle = SgtApplication.ParseSingle(textBoxSizeCandle.Text);
            if (float.IsNaN(s_candle) == false)
            {
                _app.Technology.P0203.SizeCandle = s_candle;
            }

            float s_lower = SgtApplication.ParseSingle(textBoxLowerSizeCandle.Text);
            if (float.IsNaN(s_lower) == false)
            {
                _app.Technology.P0203.LowerSizeCandle = s_lower;
            }

            float s_upper = SgtApplication.ParseSingle(textBoxUpperSizeCandle.Text);
            if (float.IsNaN(s_upper) == false)
            {
                _app.Technology.P0203.UpperSizeCandle = s_upper;
            }*/
        }

        /// <summary>
        /// метод расчета веса на крюке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonPoVesy_CheckedChanged(object sender, EventArgs e)
        {
            _app.Technology.TechnologicalRegimeWeightHook = TechnologicalRegimeWeightHook.Weight;
        }

        /// <summary>
        /// метод расчета веса на крюке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonPoKlinam_CheckedChanged(object sender, EventArgs e)
        {
            _app.Technology.TechnologicalRegimeWeightHook = TechnologicalRegimeWeightHook.Wedges;
        }

        /// <summary>
        /// метод расчета веса на крюке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonPoVesyIliKlinam_CheckedChanged(object sender, EventArgs e)
        {
            _app.Technology.TechnologicalRegimeWeightHook = TechnologicalRegimeWeightHook.WeightOrWedges;
        }

        /// <summary>
        /// метод расчета проработка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonOborotiRotora_CheckedChanged(object sender, EventArgs e)
        {
            _app.Technology.TechnologicalRegimStudy = TechnologicalRegimStudy.RotorSpeed;
        }

        /// <summary>
        /// метод расчета проработка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonSkorostTalbloka_CheckedChanged(object sender, EventArgs e)
        {
            _app.Technology.TechnologicalRegimStudy = TechnologicalRegimStudy.SpeedTalblok;
        }

        /// <summary>
        /// метод расчета проработка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonRotorIliTalblok_CheckedChanged(object sender, EventArgs e)
        {
            _app.Technology.TechnologicalRegimStudy = TechnologicalRegimStudy.RotorSpeenOrSpeedTalblok;
        }

        /// <summary>
        /// режим бурения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonDavlenie_CheckedChanged(object sender, EventArgs e)
        {
            _app.Technology.TechnologicalRegimDrilling = TechnologicalRegimDrilling.Pressure;
        }

        /// <summary>
        /// режим бурения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonDavlenieINagryska_CheckedChanged(object sender, EventArgs e)
        {
            _app.Technology.TechnologicalRegimDrilling = TechnologicalRegimDrilling.PressureAndLoad;
        }

        /// <summary>
        /// настраиваем технологические параметры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gonfig_parameters_btn_Click(object sender, EventArgs e)
        {
            TechParametersForm frm = new TechParametersForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
            }

            _app.Technology.ActualizedParameters();
        }

        /// <summary>
        /// настраиваем обороты ротора
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rotor_movie_btn_Click(object sender, EventArgs e)
        {
            SpeedRotorForm frm = new SpeedRotorForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GasAlarmForm50 frm = new GasAlarmForm50();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
            }
        }

        /// <summary>
        /// настраиваем крутящий момент ротора
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rotor_btn_Click(object sender, EventArgs e)
        {
            TorqueRotorFormP101 frm = new TorqueRotorFormP101();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
            }
        }

        /// <summary>
        /// настраиваем команды АСУ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            AsyCommandTunerForm frm = new AsyCommandTunerForm();
            frm.ShowDialog(this);
        }

        /// <summary>
        /// Настраиваем источник диаметра поршня насоса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            SourceDiameterPumpForm frm = new SourceDiameterPumpForm();
            frm.ShowDialog(this);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DrillingShaftForm frm = new DrillingShaftForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            _app.Technology.P0203.Algorithm = CalculateCandleAlgorithm.LengthOfCandles;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            _app.Technology.P0203.Algorithm = CalculateCandleAlgorithm.LenghtOfCandleCount;
        }
    }
}