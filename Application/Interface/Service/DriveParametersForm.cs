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
    public partial class DriveParametersForm : Form
    {
        SgtApplication _app = null;

        public DriveParametersForm()
        {
            InitializeComponent();

            _app = SgtApplication.CreateInstance();
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DriveParametersForm_Load(object sender, EventArgs e)
        {
            Parameter[] parameters = _app.Commutator.Parameters;
            if (parameters != null)
            {
                foreach (Parameter parameter in parameters)
                {
                    InsertParameterToList(parameter);
                }
            }
        }

        /// <summary>
        /// добавить параметр в список
        /// </summary>
        /// <param name="parameter">Добавляемый параметр</param>
        private void InsertParameterToList(Parameter parameter)
        {
            int number = listViewParameters.Items.Count + 1;

            ListViewItem item = new ListViewItem(number.ToString());
            ListViewItem.ListViewSubItem des = new ListViewItem.ListViewSubItem(item, parameter.Name);

            item.Tag = parameter;
            item.SubItems.Add(des);

            listViewParameters.Items.Add(item);
        }

        private void listViewParameters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewParameters.SelectedItems != null && listViewParameters.SelectedItems.Count > 0)
            {
                if (listViewParameters.SelectedItems[0].Tag != null)
                {
                    Parameter parameter = listViewParameters.SelectedItems[0].Tag as Parameter;
                    if (parameter != null)
                    {
                        textBoxAlarmValue.Text = parameter.Alarm.ToString();

                        textBoxMax.Text = parameter.Range.Max.ToString();
                        textBoxMin.Text = parameter.Range.Min.ToString();

                        checkBoxControlAlarm.Checked = parameter.IsControlAlarm;
                        checkBoxControlMaximum.Checked = parameter.IsControlMaximum;
                        checkBoxIsControlMinimum.Checked = parameter.IsControlMinimum;

                        checkBoxIsSaveToDB.Checked = parameter.SaveToDB;

                        numericUpDownNumberDecimal.Value = parameter.NumberOfDecimalPoints;
                        numericUpDownIntervalToSaveToDB.Value = parameter.IntervalToSaveToDB;

                        textBoxPorogToDB.Text = string.Format("{0:F2}", parameter.ThresholdToBD);

                        if (parameter.Units == string.Empty)
                        {
                            comboBoxParameterUnits.SelectedItem = "Единицы измерения не определены";
                        }
                        else
                        {
                            comboBoxParameterUnits.Text = parameter.Units;
                            comboBoxParameterUnits.SelectedItem = parameter.Units;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// текст чило или нет
        /// </summary>
        /// <param name="value">проверяемый текст</param>
        /// <returns></returns>
        protected bool CheckText(string value)
        {
            try
            {
                float val = SgtApplication.ParseSingle(value);
                return !float.IsNaN(val);
            }
            catch { }
            return false;
        }

        /// <summary>
        /// сохраняем настройки параметра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSaveToSelect_Click(object sender, EventArgs e)
        {
            if (listViewParameters.SelectedItems != null && listViewParameters.SelectedItems.Count > 0)
            {
                if (listViewParameters.SelectedItems[0].Tag != null)
                {
                    Parameter parameter = listViewParameters.SelectedItems[0].Tag as Parameter;
                    if (parameter != null)
                    {
                        if (!CheckText(textBoxAlarmValue.Text))
                        {
                            MessageBox.Show(this, "В поле Аварийного параметра введено не корректное число",
                                "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                            textBoxAlarmValue.Focus();
                            textBoxAlarmValue.SelectAll();
                            
                            return;                            
                        }

                        if (!CheckText(textBoxMax.Text))
                        {
                            MessageBox.Show(this, "В поле Максимальное значение параметра введено не корректное число",
                                "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                            textBoxMax.Focus();
                            textBoxMax.SelectAll();

                            return;
                        }

                        if (!CheckText(textBoxMin.Text))
                        {
                            MessageBox.Show(this, "В поле Минимальное значение параметра введено не корректное число",
                                "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                            textBoxMin.Focus();
                            textBoxMin.SelectAll();

                            return;
                        }

                        parameter.Range.Min = SgtApplication.ParseSingle(textBoxMin.Text);
                        parameter.Range.Max = SgtApplication.ParseSingle(textBoxMax.Text);

                        parameter.Alarm = SgtApplication.ParseSingle(textBoxAlarmValue.Text);

                        parameter.IsControlAlarm = checkBoxControlAlarm.Checked;
                        parameter.IsControlMaximum = checkBoxControlMaximum.Checked;
                        parameter.IsControlMinimum = checkBoxIsControlMinimum.Checked;

                        parameter.Units = comboBoxParameterUnits.Text;
                        parameter.NumberOfDecimalPoints = (int)numericUpDownNumberDecimal.Value;

                        _app.Technology.ActualizedParameters();

                        _app.SpoPanel.Actualize();
                        _app.SolutionPanel.Actualize();

                        _app.DrillingPanel.Actualize();

                        VPanel[] panels = _app.OptPanels;
                        if (panels != null)
                        {
                            foreach (VPanel panel in panels)
                            {
                                panel.UpdateWithRedraw();
                            }
                        }
                    }
                }
            }
        }
    }
}