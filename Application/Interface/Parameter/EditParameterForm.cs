using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using WCF;

namespace SGT
{
    public partial class EditParameterForm : Form
    {
        private Parameter edited = null;                // редактируемый параметр
        private PDescription channel = null;            // канал

        public EditParameterForm(Parameter parameter)
        {
            InitializeComponent();

            if (parameter != null)
            {
                edited = parameter;
                channel = edited.Channel;
            }
            else
            {
                MessageBox.Show("jnjnjnj");
                this.Close();
            }
        }

        private void EditParameterForm_Load(object sender, EventArgs e)
        {
            if (edited != null)
            {
                textBoxParameterName.Text = edited.Name;
                textBoxParameterDesc.Text = edited.Description;

                if (edited.Units == string.Empty)
                {
                    comboBoxParameterUnits.SelectedItem = "Единицы измерения не определены";
                }
                else
                {
                    comboBoxParameterUnits.Text = edited.Units;
                    comboBoxParameterUnits.SelectedItem = edited.Units;
                }

                textBoxMin.Text = edited.Range.Min.ToString();
                textBoxMax.Text = edited.Range.Max.ToString();

                textBoxAlarmValue.Text = edited.Alarm.ToString();

                checkBoxControlAlarm.Checked = edited.IsControlAlarm;

                checkBoxControlMaximum.Checked = edited.IsControlMaximum;
                checkBoxIsControlMinimum.Checked = edited.IsControlMinimum;

                checkBoxIsSaveToDB.Checked = edited.SaveToDB;
                textBoxPorogToDB.Text = edited.ThresholdToBD.ToString();

                numericUpDownNumberDecimal.Value = edited.NumberOfDecimalPoints;
                numericUpDownIntervalToSaveToDB.Value = edited.IntervalToSaveToDB;
                
                if (edited.Channel != null)
                {
                    textBoxParameterChannelName.Text = edited.Channel.Description;
                }
            }
        }

        /// <summary>
        /// Выбрать канал для параметра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectChannel_Click(object sender, EventArgs e)
        {
            DevManParametersForm frm = new DevManParametersForm(false);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                channel = frm.SelectedParameter;
                textBoxParameterChannelName.Text = channel.Description;
            }
        }

        /// <summary>
        /// Сохранить результат
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accept_Click(object sender, EventArgs e)
        {
            edited.Channel = channel;
            
            edited.Name = textBoxParameterName.Text;
            edited.Description = textBoxParameterDesc.Text;
            
            if (comboBoxParameterUnits.SelectedItem != null)
            {
                edited.Units = comboBoxParameterUnits.SelectedItem.ToString();
                if (edited.Units == "Единицы измерения не определены")
                {
                    edited.Units = string.Empty;
                }
            }

            edited.Range.Min = GetValue(textBoxMin);
            edited.Range.Max = GetValue(textBoxMax);

            edited.Alarm = GetValue(textBoxAlarmValue);

            edited.IsControlAlarm = checkBoxControlAlarm.Checked;

            edited.IsControlMaximum = checkBoxControlMaximum.Checked;
            edited.IsControlMinimum = checkBoxIsControlMinimum.Checked;

            edited.SaveToDB = checkBoxIsSaveToDB.Checked;
            edited.ThresholdToBD = GetValue(textBoxPorogToDB);

            edited.NumberOfDecimalPoints = (int)numericUpDownNumberDecimal.Value;
            edited.IntervalToSaveToDB = (int)numericUpDownIntervalToSaveToDB.Value;
        }

        /// <summary>
        /// получить значение и TextBox
        /// </summary>
        /// <param name="box">TextBox из которого извлекать значение</param>
        /// <returns>Значение извлеченное из TextBox</returns>
        private float GetValue(TextBox box)
        {
            try
            {
                float koef;
                bool result = false;
                
                string number = box.Text;

                result = float.TryParse(number, out koef);
                if (result == false)
                {
                    number = number.Replace(".", ",");
                    result = float.TryParse(number, out koef);
                    if (!result)
                    {
                        number = box.Text.Replace(",", ".");
                        result = float.TryParse(number, out koef);

                        if (!result)
                        {
                            MessageBox.Show(this, "Введено не корректное число", "Предупреждение",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                            DialogResult = System.Windows.Forms.DialogResult.None;
                            return float.NaN;
                        }

                        return koef;
                    }
                    else
                        return koef;
                }
                else
                    return koef;
            }
            catch
            {
                MessageBox.Show(this, "Введено не корректное число", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                DialogResult = System.Windows.Forms.DialogResult.None;
            }

            return float.NaN;
        }

        private void numericUpDownIntervalToSaveToDB_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownIntervalToSaveToDB.Value < 500)
            {
                numericUpDownIntervalToSaveToDB.Value = 500;
            }
        }
    }
}