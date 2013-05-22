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
    public partial class SpoPanelForm : Form
    {
        SgtApplication _app = null;

        public SpoPanelForm()
        {
            InitializeComponent();
            _app = SgtApplication.CreateInstance();
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpoPanelForm_Load(object sender, EventArgs e)
        {
            button8.BackColor = _app.SpoPanel.GraphicTalblok.Color;
            textBox11.Text = _app.SpoPanel.GraphicTalblok.Range.Min.ToString();
            textBox10.Text = _app.SpoPanel.GraphicTalblok.Range.Max.ToString();

            button7.BackColor = _app.SpoPanel.GraphicGlybina.Color;
            textBox9.Text = _app.SpoPanel.GraphicGlybina.Range.Min.ToString();
            textBox8.Text = _app.SpoPanel.GraphicGlybina.Range.Max.ToString();

            button6.BackColor = _app.SpoPanel.GraphicSkorostSpo.Color;
            textBox7.Text = _app.SpoPanel.GraphicSkorostSpo.Range.Min.ToString();
            textBox6.Text = _app.SpoPanel.GraphicSkorostSpo.Range.Max.ToString();

            buttonGlubinaColor.BackColor = _app.SpoPanel.GraphicGasnavihode.Color;
            lb_GlubinaMin.Text = _app.SpoPanel.GraphicGasnavihode.Range.Min.ToString();
            lb_GlubinaMax.Text = _app.SpoPanel.GraphicGasnavihode.Range.Max.ToString();

            buttonMehSkorostColor.BackColor = _app.SpoPanel.GraphicGasnaplosadke.Color;
            lb_MehSkorostMin.Text = _app.SpoPanel.GraphicGasnaplosadke.Range.Min.ToString();
            lb_MehSkorostMax.Text = _app.SpoPanel.GraphicGasnaplosadke.Range.Max.ToString();

            buttonVesColor.BackColor = _app.SpoPanel.GraphicGaspodrotorom.Color;
            lb_VesMin.Text = _app.SpoPanel.GraphicGaspodrotorom.Range.Min.ToString();
            lb_VesMax.Text = _app.SpoPanel.GraphicGaspodrotorom.Range.Max.ToString();

            button2.BackColor = _app.SpoPanel.GraphicGaspriemnaemk.Color;
            textBox4.Text = _app.SpoPanel.GraphicGaspriemnaemk.Range.Min.ToString();
            textBox3.Text = _app.SpoPanel.GraphicGaspriemnaemk.Range.Max.ToString();

            button1.BackColor = _app.SpoPanel.GraphicGasvibrosit.Color;
            textBox2.Text = _app.SpoPanel.GraphicGasvibrosit.Range.Min.ToString();
            textBox1.Text = _app.SpoPanel.GraphicGasvibrosit.Range.Max.ToString();
        }

        /// <summary>
        /// цвет Положение тальблока
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                _app.SpoPanel.GraphicTalblok.Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        /// <summary>
        /// минимум Положение тальблока
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
                    _app.SpoPanel.GraphicTalblok.Range.Min = n_val;
                }
            }
        }

        /// <summary>
        /// макс Положение тальблока
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
                    _app.SpoPanel.GraphicTalblok.Range.Max = n_val;
                }
            }
        }

        /// <summary>
        /// цвет глубина инструмента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                _app.SpoPanel.GraphicGlybina.Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        /// <summary>
        /// мин глубина инструмента
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
                    _app.SpoPanel.GraphicGlybina.Range.Min = n_val;
                }
            }
        }

        /// <summary>
        /// макс глубина инструмента
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
                    _app.SpoPanel.GraphicGlybina.Range.Max = n_val;
                }
            }
        }

        /// <summary>
        /// цвет Скорость СПО
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                _app.SpoPanel.GraphicSkorostSpo.Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        /// <summary>
        /// мин Скорость СПО
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
                    _app.SpoPanel.GraphicSkorostSpo.Range.Min = n_val;
                }
            }
        }

        /// <summary>
        /// макс Скорость СПО
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
                    _app.SpoPanel.GraphicSkorostSpo.Range.Max = n_val;
                }
            }
        }

        /// <summary>
        /// цвет Газ на выходе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGlubinaColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                _app.SpoPanel.GraphicGasnavihode.Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        /// <summary>
        /// мин Газ на выходе
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
                    _app.SpoPanel.GraphicGasnavihode.Range.Min = n_val;
                }
            }
        }

        /// <summary>
        /// макс Газ на выходе
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
                    _app.SpoPanel.GraphicGasnavihode.Range.Max = n_val;
                }
            }
        }

        /// <summary>
        /// цвет Газ на площадке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMehSkorostColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                _app.SpoPanel.GraphicGasnaplosadke.Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        /// <summary>
        /// мин Газ на площадке
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
                    _app.SpoPanel.GraphicGasnaplosadke.Range.Min = n_val;
                }
            }
        }

        /// <summary>
        /// макс Газ на площадке
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
                    _app.SpoPanel.GraphicGasnaplosadke.Range.Max = n_val;
                }
            }
        }

        /// <summary>
        /// цвет газ под ротором
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonVesColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                _app.SpoPanel.GraphicGaspodrotorom.Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        /// <summary>
        /// мин газ под ротором
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
                    _app.SpoPanel.GraphicGaspodrotorom.Range.Min = n_val;
                }
            }
        }

        /// <summary>
        /// макс газ под ротором
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
                    _app.SpoPanel.GraphicGaspodrotorom.Range.Max = n_val;
                }
            }
        }

        /// <summary>
        /// цвет газ приемная емкость
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                _app.SpoPanel.GraphicGaspriemnaemk.Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        /// <summary>
        /// мин газ приемная емкость
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    _app.SpoPanel.GraphicGaspriemnaemk.Range.Min = n_val;
                }
            }
        }

        /// <summary>
        /// макс газ приемная емкость
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    _app.SpoPanel.GraphicGaspriemnaemk.Range.Max = n_val;
                }
            }
        }

        /// <summary>
        /// цвет газ вибросит
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                _app.SpoPanel.GraphicGasvibrosit.Color = colorDialog.Color;
                Button cb = sender as Button;

                if (cb != null)
                {
                    cb.BackColor = colorDialog.Color;
                }
            }
        }

        /// <summary>
        /// мин газ вибросит
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    _app.SpoPanel.GraphicGasvibrosit.Range.Min = n_val;
                }
            }
        }

        /// <summary>
        /// макс газ вибросит
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                float n_val = SgtApplication.ParseSingle(box.Text);
                if (float.IsNaN(n_val) == false)
                {
                    _app.SpoPanel.GraphicGasvibrosit.Range.Max = n_val;
                }
            }
        }
    }
}