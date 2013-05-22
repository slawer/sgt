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
    public partial class SolutionPanelForm : Form
    {
        SgtApplication _app = null;

        public SolutionPanelForm()
        {
            InitializeComponent();
            _app = SgtApplication.CreateInstance();
        }

        /// <summary>
        /// плотность приемная емкость
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
                    textBoxplPriemna.Text = sel_par.Name;
                    _app.SolutionPanel.PlPriemna.Identifier = sel_par.Identifier;
                }
            }
        }

        /// <summary>
        /// плотность блока очистки
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
                    textBoxplBlock.Text = sel_par.Name;
                    _app.SolutionPanel.PlBlocka.Identifier = sel_par.Identifier;
                }
            }
        }

        /// <summary>
        /// плотность емкости отс2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            SelectParameterForm frm = new SelectParameterForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                Parameter sel_par = frm.SelectedParameter;
                if (sel_par != null)
                {
                    textBoxOts2.Text = sel_par.Name;
                    _app.SolutionPanel.PlEmkOts2.Identifier = sel_par.Identifier;
                }
            }
        }

        /// <summary>
        /// температура на выходе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            SelectParameterForm frm = new SelectParameterForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                Parameter sel_par = frm.SelectedParameter;
                if (sel_par != null)
                {
                    textBoxTemVihod.Text = sel_par.Name;
                    _app.SolutionPanel.TempVihod.Identifier = sel_par.Identifier;
                }
            }
        }

        /// <summary>
        /// температура на входе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            SelectParameterForm frm = new SelectParameterForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                Parameter sel_par = frm.SelectedParameter;
                if (sel_par != null)
                {
                    textBoxTempVhod.Text = sel_par.Name;
                    _app.SolutionPanel.TemVhod.Identifier = sel_par.Identifier;
                }
            }
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SolutionPanelForm_Load(object sender, EventArgs e)
        {
            /*Parameter _plPriemna = _app.GetParameter(_app.SolutionPanel.PlPriemna.PNumber);
            Parameter _plBlocka = _app.GetParameter(_app.SolutionPanel.PlBlocka.PNumber);

            Parameter _plEmkOts2 = _app.GetParameter(_app.SolutionPanel.PlEmkOts2.PNumber);
            Parameter _tempVihod = _app.GetParameter(_app.SolutionPanel.TempVihod.PNumber);

            Parameter _temVhod = _app.GetParameter(_app.SolutionPanel.TemVhod.PNumber);*/

            Parameter _plPriemna = _app.GetParameter(_app.SolutionPanel.PlPriemna.Identifier);
            Parameter _plBlocka = _app.GetParameter(_app.SolutionPanel.PlBlocka.Identifier);

            Parameter _plEmkOts2 = _app.GetParameter(_app.SolutionPanel.PlEmkOts2.Identifier);
            Parameter _tempVihod = _app.GetParameter(_app.SolutionPanel.TempVihod.Identifier);

            Parameter _temVhod = _app.GetParameter(_app.SolutionPanel.TemVhod.Identifier);

            if (_plPriemna != null)
            {
                textBoxplPriemna.Text = _plPriemna.Name;
            }

            if (_plBlocka != null)
            {
                textBoxplBlock.Text = _plBlocka.Name;
            }

            if (_plEmkOts2 != null)
            {
                textBoxOts2.Text = _plEmkOts2.Name;
            }

            if (_tempVihod != null)
            {
                textBoxTemVihod.Text = _tempVihod.Name;
            }

            if (_temVhod != null)
            {
                textBoxTempVhod.Text = _temVhod.Name;
            }

            buttonGlubinaColor.BackColor = _app.SolutionPanel.GraphicplPriemna.Color;
            lb_GlubinaMin.Text = _app.SolutionPanel.GraphicplPriemna.Range.Min.ToString();
            lb_GlubinaMax.Text = _app.SolutionPanel.GraphicplPriemna.Range.Max.ToString();
            
            buttonMehSkorostColor.BackColor = _app.SolutionPanel.GraphicplBlokaOchi.Color;
            lb_MehSkorostMin.Text = _app.SolutionPanel.GraphicplBlokaOchi.Range.Min.ToString();
            lb_MehSkorostMax.Text = _app.SolutionPanel.GraphicplBlokaOchi.Range.Max.ToString();
            
            buttonVesColor.BackColor = _app.SolutionPanel.GraphicplEmnsek2.Color;
            lb_VesMin.Text = _app.SolutionPanel.GraphicplEmnsek2.Range.Min.ToString();
            lb_VesMax.Text = _app.SolutionPanel.GraphicplEmnsek2.Range.Max.ToString();

            button8.BackColor = _app.SolutionPanel.GraphicsummObem.Color;
            textBox11.Text = _app.SolutionPanel.GraphicsummObem.Range.Min.ToString();
            textBox10.Text = _app.SolutionPanel.GraphicsummObem.Range.Max.ToString();

            button7.BackColor = _app.SolutionPanel.Graphichodi1.Color;
            textBox9.Text = _app.SolutionPanel.Graphichodi1.Range.Min.ToString();
            textBox8.Text = _app.SolutionPanel.Graphichodi1.Range.Max.ToString();

            button6.BackColor = _app.SolutionPanel.Graphichodi2.Color;
            textBox7.Text = _app.SolutionPanel.Graphichodi2.Range.Min.ToString();
            textBox6.Text = _app.SolutionPanel.Graphichodi2.Range.Max.ToString();

            buttonPressureColor.BackColor = _app.SolutionPanel.Graphicpotok.Color;
            lb_pressureMin.Text = _app.SolutionPanel.Graphicpotok.Range.Min.ToString();
            lb_pressureMax.Text = _app.SolutionPanel.Graphicpotok.Range.Max.ToString();
        }

        /// <summary>
        /// цвет приемная емкость
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGlubinaColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                _app.SolutionPanel.GraphicplPriemna.Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        /// <summary>
        /// цвет блока очистки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMehSkorostColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                _app.SolutionPanel.GraphicplBlokaOchi.Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        /// <summary>
        /// емкость , отс2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonVesColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                _app.SolutionPanel.GraphicplEmnsek2.Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        /// <summary>
        /// суммарный объем
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                _app.SolutionPanel.GraphicsummObem.Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        /// <summary>
        /// ходы насоса 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                _app.SolutionPanel.Graphichodi1.Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        /// <summary>
        /// ходы насоса 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                _app.SolutionPanel.Graphichodi2.Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        /// <summary>
        /// поток на выходе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPressureColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                _app.SolutionPanel.Graphicpotok.Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        /// <summary>
        /// плотность приемная мин
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
                    _app.SolutionPanel.GraphicplPriemna.Range.Min = n_val;
                }
            }
        }

        /// <summary>
        /// плотность приемная макс
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
                    _app.SolutionPanel.GraphicplPriemna.Range.Max = n_val;
                }
            }
        }

        /// <summary>
        /// блок очистки мин
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
                    _app.SolutionPanel.GraphicplBlokaOchi.Range.Min = n_val;
                }
            }
        }

        /// <summary>
        /// блок очистки макс
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
                    _app.SolutionPanel.GraphicplBlokaOchi.Range.Max = n_val;
                }
            }
        }

        /// <summary>
        /// отс2 мин
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
                    _app.SolutionPanel.GraphicplEmnsek2.Range.Min = n_val;
                }
            }
        }

        /// <summary>
        /// отс2 макс
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
                    _app.SolutionPanel.GraphicplEmnsek2.Range.Max = n_val;
                }
            }
        }

        /// <summary>
        /// суммарный объем мин
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    _app.SolutionPanel.GraphicsummObem.Range.Min = n_val;
                }
            }
        }

        /// <summary>
        /// суммарный объем макс
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    _app.SolutionPanel.GraphicsummObem.Range.Max = n_val;
                }
            }
        }

        /// <summary>
        /// ходы насоса 1 мин
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    _app.SolutionPanel.Graphichodi1.Range.Min = n_val;
                }
            }
        }

        /// <summary>
        /// ходы насоса 2 макс
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    _app.SolutionPanel.Graphichodi1.Range.Max = n_val;
                }
            }
        }

        /// <summary>
        /// ходы насоса 2 мин
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    _app.SolutionPanel.Graphichodi2.Range.Min = n_val;
                }
            }
        }

        /// <summary>
        /// ходы насоса 2 макс
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    _app.SolutionPanel.Graphichodi2.Range.Max = n_val;
                }
            }
        }

        /// <summary>
        /// поток на выходе мин
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
                    _app.SolutionPanel.Graphicpotok.Range.Min = n_val;
                }
            }
        }

        /// <summary>
        /// поток на выходе макс
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
                    _app.SolutionPanel.Graphicpotok.Range.Max = n_val;
                }
            }
        }
    }
}