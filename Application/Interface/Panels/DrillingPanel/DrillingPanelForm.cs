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
    public partial class DrillingPanelForm : Form
    {
        SgtApplication _app = null;

        public DrillingPanelForm()
        {
            InitializeComponent();

            _app = SgtApplication.CreateInstance();
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrillingPanelForm_Load(object sender, EventArgs e)
        {
            /*Parameter svp = _app.GetParameter(_app.DrillingPanel.Svp.PNumber);
            Parameter m_svp = _app.GetParameter(_app.DrillingPanel.MSvp.PNumber);

            Parameter kmb = _app.GetParameter(_app.DrillingPanel.KMB.PNumber);
            Parameter rotor = _app.GetParameter(_app.DrillingPanel.Rotor.PNumber);

            Parameter mom1 = _app.GetParameter(_app.DrillingPanel.Mom1.PNumber);
            Parameter mom2 = _app.GetParameter(_app.DrillingPanel.Mom2.PNumber);*/

            Parameter svp = _app.GetParameter(_app.DrillingPanel.Svp.Identifier);
            Parameter m_svp = _app.GetParameter(_app.DrillingPanel.MSvp.Identifier);

            Parameter kmb = _app.GetParameter(_app.DrillingPanel.KMB.Identifier);
            Parameter rotor = _app.GetParameter(_app.DrillingPanel.Rotor.Identifier);

            Parameter mom1 = _app.GetParameter(_app.DrillingPanel.Mom1.Identifier);
            Parameter mom2 = _app.GetParameter(_app.DrillingPanel.Mom2.Identifier);

            if (svp != null)
            {
                textBoxSvp.Text = svp.Name;
            }

            if (m_svp != null)
            {
                textBoxm_svp.Text = m_svp.Name;
            }

            if (kmb != null)
            {
                textBox1.Text = kmb.Name;
            }

            if (rotor != null)
            {
                textBoxRotor.Text = rotor.Name;
            }

            if (mom1 != null)
            {
                textBoxMom1.Text = mom1.Name;
            }

            if (mom2 != null)
            {
                textBoxMom2.Text = mom1.Name;
            }

            buttonGlubinaColor.BackColor = _app.DrillingPanel.Glybina.Color;

            lb_GlubinaMin.Text = _app.DrillingPanel.Glybina.Range.Min.ToString();
            lb_GlubinaMax.Text = _app.DrillingPanel.Glybina.Range.Max.ToString();

            buttonMehSkorostColor.BackColor = _app.DrillingPanel.Mehskorost.Color;

            lb_MehSkorostMin.Text = _app.DrillingPanel.Mehskorost.Range.Min.ToString();
            lb_MehSkorostMax.Text = _app.DrillingPanel.Mehskorost.Range.Max.ToString();

            buttonVesColor.BackColor = _app.DrillingPanel.Vesnakruke.Color;

            lb_VesMin.Text = _app.DrillingPanel.Vesnakruke.Range.Min.ToString();
            lb_VesMax.Text = _app.DrillingPanel.Vesnakruke.Range.Max.ToString();

            buttonPressureColor.BackColor = _app.DrillingPanel.Davlenienaman.Color;

            lb_pressureMin.Text = _app.DrillingPanel.Davlenienaman.Range.Min.ToString();
            lb_pressureMax.Text = _app.DrillingPanel.Davlenienaman.Range.Max.ToString();

            buttonRashodColor.BackColor = _app.DrillingPanel.Rashodnavhode.Color;

            lb_RashodMin.Text = _app.DrillingPanel.Rashodnavhode.Range.Min.ToString();
            lb_RashodMax.Text = _app.DrillingPanel.Rashodnavhode.Range.Max.ToString();
        }

        /// <summary>
        /// настраиваем СВП
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSvp_Click(object sender, EventArgs e)
        {
            SelectParameterForm frm = new SelectParameterForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                Parameter sel_par = frm.SelectedParameter;
                if (sel_par != null)
                {
                    _app.DrillingPanel.Svp.Identifier = sel_par.Identifier;
                    textBoxSvp.Text = sel_par.Name;
                }
            }
        }

        /// <summary>
        /// настраиваем момент на роторе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRotor_Click(object sender, EventArgs e)
        {
            SelectParameterForm frm = new SelectParameterForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                Parameter sel_par = frm.SelectedParameter;
                if (sel_par != null)
                {
                    textBoxRotor.Text = sel_par.Name;
                    _app.DrillingPanel.Rotor.Identifier = sel_par.Identifier;
                }
            }
        }

        /// <summary>
        /// настраиваем момент на ключе 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMom1_Click(object sender, EventArgs e)
        {
            SelectParameterForm frm = new SelectParameterForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                Parameter sel_par = frm.SelectedParameter;
                if (sel_par != null)
                {
                    textBoxMom1.Text = sel_par.Name;
                    _app.DrillingPanel.Mom1.Identifier = sel_par.Identifier;
                }
            }
        }

        /// <summary>
        /// настраиваем момент на ключе 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMom2_Click(object sender, EventArgs e)
        {
            SelectParameterForm frm = new SelectParameterForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                Parameter sel_par = frm.SelectedParameter;
                if (sel_par != null)
                {
                    textBoxMom2.Text = sel_par.Name;
                    _app.DrillingPanel.Mom2.Identifier = sel_par.Identifier;
                }
            }
        }

        /// <summary>
        /// цвет графика глубина скважины
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGlubinaColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                _app.DrillingPanel.Glybina.Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        /// <summary>
        /// изменили минимальное значение глуюины
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_GlubinaMin_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    _app.DrillingPanel.Glybina.Range.Min = n_val;
                }
            }
        }

        /// <summary>
        /// настраиваем максимальное значене глубины
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_GlubinaMax_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    _app.DrillingPanel.Glybina.Range.Max = n_val;
                }
            }
        }

        /// <summary>
        /// цвет графика мех скорость проходки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMehSkorostColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                _app.DrillingPanel.Mehskorost.Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        /// <summary>
        /// минимальное значение мех скорость проходки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_MehSkorostMin_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    _app.DrillingPanel.Mehskorost.Range.Min = n_val;
                }
            }
        }

        /// <summary>
        /// максимальное значение мех скорость проходки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_MehSkorostMax_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    _app.DrillingPanel.Mehskorost.Range.Max = n_val;
                }
            }
        }

        /// <summary>
        /// цвет вес на крюке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonVesColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                _app.DrillingPanel.Vesnakruke.Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        /// <summary>
        /// минимальное вес на крюке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_VesMin_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    _app.DrillingPanel.Vesnakruke.Range.Min = n_val;
                }
            }
        }

        /// <summary>
        /// максимальное вес на крюке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_VesMax_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    _app.DrillingPanel.Vesnakruke.Range.Max = n_val;
                }
            }
        }

        /// <summary>
        /// цвет давление на манифольде
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPressureColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                _app.DrillingPanel.Davlenienaman.Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        /// <summary>
        /// минимальное давление на манифольде
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_pressureMin_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    _app.DrillingPanel.Davlenienaman.Range.Min = n_val;
                }
            }
        }

        /// <summary>
        /// максимальное давление на манифольде
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_pressureMax_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    _app.DrillingPanel.Davlenienaman.Range.Max = n_val;
                }
            }
        }

        /// <summary>
        /// цвет расход
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRashodColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                _app.DrillingPanel.Rashodnavhode.Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        /// <summary>
        /// минимальное расход на входе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_RashodMin_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    _app.DrillingPanel.Rashodnavhode.Range.Min = n_val;
                }
            }
        }

        /// <summary>
        /// максимальное расход на входе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_RashodMax_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    _app.DrillingPanel.Rashodnavhode.Range.Max = n_val;
                }
            }
        }

        /// <summary>
        /// момент свп
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            SelectParameterForm frm = new SelectParameterForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                Parameter sel_par = frm.SelectedParameter;
                if (sel_par != null)
                {
                    textBoxm_svp.Text = sel_par.Name;
                    _app.DrillingPanel.MSvp.Identifier = sel_par.Identifier;
                }
            }
        }

        /// <summary>
        /// усиление в ключе КМБ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            SelectParameterForm frm = new SelectParameterForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                Parameter sel_par = frm.SelectedParameter;
                if (sel_par != null)
                {
                    textBox1.Text = sel_par.Name;
                    _app.DrillingPanel.KMB.Identifier = sel_par.Identifier;
                }
            }
        }
    }
}