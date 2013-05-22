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
    public partial class PumpMovesForm : Form
    {
        SgtApplication _app = null;

        public PumpMovesForm()
        {
            InitializeComponent();

            _app = SgtApplication.CreateInstance();
            if (_app == null)
            {
                MessageBox.Show(this, "Не удалось инициализировать настройки", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                Close();
            }
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PumpMovesForm_Load(object sender, EventArgs e)
        {
            textBoxAsyPervii.Text = string.Format("{0:F2}", _app.Technology.P0112.ScaleFactorPump1);
            textBoxAsyVtoroi.Text = string.Format("{0:F2}", _app.Technology.P0112.ScaleFactorPump2);

            textBoxAnalogPervii.Text = string.Format("{0:F2}", _app.Technology.P0109.ScaleFactorPump1);
            textBoxAnalogVtoroi.Text = string.Format("{0:F2}", _app.Technology.P0109.ScaleFactorPump2);

            switch (_app.Technology.P0116.Source)
            {
                case P0116.SourceMoving.Analog:

                    radioButtonAnalog.Checked = true;
                    break;

                case P0116.SourceMoving.Asy:

                    radioButtonASY.Checked = true;
                    break;

                default:

                    radioButtonASY.Checked = false;
                    radioButtonAnalog.Checked = false;

                    break;
            }
        }

        /// <summary>
        /// проверяем введенные данные
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accept_Click(object sender, EventArgs e)
        {
            float v = SgtApplication.ParseSingle(textBoxAsyPervii.Text);
            if (float.IsNaN(v))
            {
                MessageBox.Show(this, "Значение коэффициента указано не корректно",
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                textBoxAsyPervii.Select();
                textBoxAsyPervii.SelectAll();

                DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }

            v = SgtApplication.ParseSingle(textBoxAsyVtoroi.Text);
            if (float.IsNaN(v))
            {
                MessageBox.Show(this, "Значение коэффициента указано не корректно",
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                textBoxAsyVtoroi.Select();
                textBoxAsyVtoroi.SelectAll();

                DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }

            v = SgtApplication.ParseSingle(textBoxAnalogPervii.Text);
            if (float.IsNaN(v))
            {
                MessageBox.Show(this, "Значение коэффициента указано не корректно",
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                textBoxAnalogPervii.Select();
                textBoxAnalogPervii.SelectAll();

                DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }

            v = SgtApplication.ParseSingle(textBoxAnalogVtoroi.Text);
            if (float.IsNaN(v))
            {
                MessageBox.Show(this, "Значение коэффициента указано не корректно",
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                textBoxAnalogVtoroi.Select();
                textBoxAnalogVtoroi.SelectAll();

                DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }

            if (radioButtonASY.Checked)
            {
                _app.Technology.P0113.Source = P0113.SourceFlow.Asy;

                _app.Technology.P0116.Source = P0116.SourceMoving.Asy;
                _app.Technology.P0117.Source = P0117.SourceMoving.Asy;
            }
            else
                if (radioButtonAnalog.Checked)
                {
                    _app.Technology.P0113.Source = P0113.SourceFlow.Analog;

                    _app.Technology.P0116.Source = P0116.SourceMoving.Analog;
                    _app.Technology.P0117.Source = P0117.SourceMoving.Analog;
                }
        }

        /// <summary>
        /// опрелеяем идеальный расход по ходам насоса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            IdealRashodForm frm = new IdealRashodForm(_app.Technology.P0112);
            frm.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IdealRashodForm frm = new IdealRashodForm(_app.Technology.P0109);
            frm.ShowDialog(this);
        }
    }
}