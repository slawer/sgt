using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SGT
{
    public partial class SourceDiameterPumpForm : Form
    {
        SgtApplication _app = null;

        public SourceDiameterPumpForm()
        {
            InitializeComponent();

            _app = SgtApplication.CreateInstance();
            seter = new setter(seterF);
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SourceDiameterPumpForm_Load(object sender, EventArgs e)
        {
            if (_app != null)
            {
                comboBoxFirstDiameter.Text = string.Format("{0:F3}", _app.Technology.P0014.Value);
                comboBoxSecondDiameter.Text = string.Format("{0:F3}", _app.Technology.P14_1.Value);                

                if (_app.Technology.P0014.Source == P0014Source.Own || 
                    _app.Technology.P0014.Source == P0014Source.Default)
                {
                    radioButtonFirstOwn.Checked = true;
                }
                else
                    radioButtonFirstExternal.Checked = true;

                if (_app.Technology.P14_1.Source == P0014Source.Default ||
                    _app.Technology.P14_1.Source == P0014Source.Own)
                {
                    radioButtonSecondOnw.Checked = true;
                }
                else
                    radioButtonSecondExternal.Checked = true;

                switch (_app.Technology.P0116.Source)
                {
                    case P0116.SourceMoving.Asy:

                        foreach (P0112.IdealFlowPair pair in _app.Technology.P0112.Pairs)
                        {
                            comboBoxFirstDiameter.Items.Add(string.Format("{0:F3}", pair.Diameter));
                            comboBoxSecondDiameter.Items.Add(string.Format("{0:F3}", pair.Diameter));
                        }
                        break;

                    case P0116.SourceMoving.Analog:

                        foreach (P0109.IdealFlowPair pair in _app.Technology.P0109.Pairs)
                        {
                            comboBoxFirstDiameter.Items.Add(string.Format("{0:F3}", pair.Diameter));
                            comboBoxSecondDiameter.Items.Add(string.Format("{0:F3}", pair.Diameter));
                        }

                        break;

                    case P0116.SourceMoving.Default:

                        foreach (P0109.IdealFlowPair pair in _app.Technology.P0109.Pairs)
                        {
                            comboBoxFirstDiameter.Items.Add(string.Format("{0:F3}", pair.Diameter));
                            comboBoxSecondDiameter.Items.Add(string.Format("{0:F3}", pair.Diameter));
                        }

                        break;

                    default:
                        break;
                }

                _app.Technology.onComplete += new EventHandler(Technology_onComplete);
            }
        }

        /// <summary>
        /// выгружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SourceDiameterPumpForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _app.Technology.onComplete -= Technology_onComplete;
        }

        /// <summary>
        /// проверяем и присваиваем
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accept_Click(object sender, EventArgs e)
        {
            try
            {
                float f_dia = SgtApplication.ParseSingle(comboBoxFirstDiameter.Text);
                if (float.IsNaN(f_dia))
                {
                    MessageBox.Show(this, "Указано не верное значение диаметра поршня 1", "Сообщение",
                         MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    comboBoxFirstDiameter.Focus();
                    comboBoxFirstDiameter.SelectAll();

                    DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }

                float s_dia = SgtApplication.ParseSingle(comboBoxSecondDiameter.Text);
                if (float.IsNaN(s_dia))
                {
                    MessageBox.Show(this, "Указано не верное значение диаметра поршня 2", "Сообщение",
                         MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    comboBoxSecondDiameter.Focus();
                    comboBoxSecondDiameter.SelectAll();

                    DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }

                if (radioButtonFirstExternal.Checked)
                {
                    _app.Technology.P0014.Source = P0014Source.External;
                }
                else
                {
                    _app.Technology.P0014.Source = P0014Source.Own;
                    f_dia = SgtApplication.ParseSingle(comboBoxFirstDiameter.Text);

                    if (float.IsNaN(f_dia))
                    {
                        MessageBox.Show(this, "Указано не верное значение диаметра поршня 1", "Сообщение",
                             MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        comboBoxFirstDiameter.Focus();
                        comboBoxFirstDiameter.SelectAll();

                        DialogResult = System.Windows.Forms.DialogResult.None;
                        return;
                    }

                    _app.Technology.P0014.Calculate(f_dia);
                }

                if (radioButtonSecondExternal.Checked)
                {
                    _app.Technology.P14_1.Source = P0014Source.External;
                }
                else
                {
                    _app.Technology.P14_1.Source = P0014Source.Own;
                    s_dia = SgtApplication.ParseSingle(comboBoxSecondDiameter.Text);

                    if (float.IsNaN(s_dia))
                    {
                        MessageBox.Show(this, "Указано не верное значение диаметра поршня 2", "Сообщение",
                             MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        comboBoxSecondDiameter.Focus();
                        comboBoxSecondDiameter.SelectAll();

                        DialogResult = System.Windows.Forms.DialogResult.None;
                        return;
                    }

                    _app.Technology.P14_1.Calculate(s_dia);
                }
            }
            catch { }
        }

        delegate void setter(float value, ComboBox box);
        setter seter;

        long may_1 = 0;
        long may_2 = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="box"></param>
        void seterF(float value, ComboBox box)
        {
            try
            {
                box.Text = string.Format("{0:F3}", value);
            }
            catch { }
        }


        /// <summary>
        /// технология  завершила свою работу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Technology_onComplete(object sender, EventArgs e)
        {
            if (seter != null)
            {
                if (Interlocked.Read(ref may_1) == 1)
                {
                    Invoke(seter, _app.Technology.P0014.Value, comboBoxFirstDiameter);
                }

                if (Interlocked.Read(ref may_2) == 1)
                {
                    Invoke(seter, _app.Technology.P14_1.Value, comboBoxSecondDiameter);
                }
            }
        }

        private void radioButtonFirstOwn_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonFirstOwn.Checked)
            {
                Interlocked.Exchange(ref may_1, 0);
            }
        }

        private void radioButtonFirstExternal_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonFirstExternal.Checked)
            {
                Interlocked.Exchange(ref may_1, 1);
            }
        }

        private void radioButtonSecondOnw_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSecondOnw.Checked)
            {
                Interlocked.Exchange(ref may_2, 0);
            }
        }

        private void radioButtonSecondExternal_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSecondExternal.Checked)
            {
                Interlocked.Exchange(ref may_2, 1);
            }
        }
    }
}