using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using WCF;
using WCF.WCF_Client;

namespace SGT
{
    public partial class AsyCommandTunerForm : Form
    {
        SgtApplication _app = null;

        //int dr = -1;
        //int re = -1;

        Guid id_dr, id_re;

        public AsyCommandTunerForm()
        {
            InitializeComponent();
            _app = SgtApplication.CreateInstance();
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AsyCommandTunerForm_Load(object sender, EventArgs e)
        {
            /*Parameter drilling = _app.GetParameter(_app.Technology.DrillerConsole);
            Parameter resetVes = _app.GetParameter(_app.Technology.DrillerConsoleWeightColumn);*/

            Parameter drilling = _app.GetParameter(_app.Technology.IdentifierDrillerConsole);
            Parameter resetVes = _app.GetParameter(_app.Technology.IdentifierDrillerConsoleWeightColumn);

            if (drilling != null)
            {
                textBoxPult.Text = drilling.Name;
            }

            if (resetVes != null)
            {
                textBoxResetVes.Text = resetVes.Name;
            }

            //dr = _app.Technology.DrillerConsole;
            //re = _app.Technology.DrillerConsoleWeightColumn;
            id_dr = _app.Technology.IdentifierDrillerConsole;
            id_re = _app.Technology.IdentifierDrillerConsoleWeightColumn;
        }

        /// <summary>
        /// настраиваем команду пульт бурильщика
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPult_Click(object sender, EventArgs e)
        {
            SelectParameterForm frm = new SelectParameterForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                Parameter channel = frm.SelectedParameter;
                if (channel != null)
                {
                    if (channel.Channel != null)
                    {
                        id_dr = channel.Identifier;
                        //dr = channel.Channel.Number;
                        
                        textBoxPult.Text = channel.Name;
                    }
                }
            }
        }

        /// <summary>
        /// настраиваем сброс объема
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonResetVes_Click(object sender, EventArgs e)
        {
            SelectParameterForm frm = new SelectParameterForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                Parameter channel = frm.SelectedParameter;
                if (channel != null)
                {
                    if (channel.Channel != null)
                    {
                        id_re = channel.Identifier;
                        //re = channel.Channel.Number;

                        textBoxResetVes.Text = channel.Name;
                    }
                }
            }
        }

        /// <summary>
        /// проверяем и присваиваем
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accept_Click(object sender, EventArgs e)
        {
            /*_app.Technology.DrillerConsole = dr;
            _app.Technology.DrillerConsoleWeightColumn = re;*/

            _app.Technology.IdentifierDrillerConsole = id_dr;
            _app.Technology.IdentifierDrillerConsoleWeightColumn = id_re;
        }
    }
}