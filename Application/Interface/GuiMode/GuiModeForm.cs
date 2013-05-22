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
    public partial class GuiModeForm : Form
    {
        public GuiModeForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// показывать пароль или нет
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxViewPassword_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox check = sender as CheckBox;
            if (check != null)
            {
                switch (check.Checked)
                {
                    case true:

                        textBoxPassword.UseSystemPasswordChar = false;
                        break;

                    case false:

                        textBoxPassword.UseSystemPasswordChar = true;
                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// проверяем
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accept_Click(object sender, EventArgs e)
        {
            SgtApplication _app = SgtApplication.CreateInstance();
            if (_app != null)
            {                    
                if (textBoxPassword.Text == _app.DB_Manager.Password)
                {
                    MessageBox.Show(this, "Система переведена в режим настройки.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    _app.GuiMode = GuiMode.Techolog;
                }
                else
                {
                    MessageBox.Show(this, "Система переведена в режим пользователя.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //MessageBox.Show(this, "Введен неверный пароль. Система переведена в режим пользователя.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    _app.GuiMode = GuiMode.User;
                    //DialogResult = DialogResult.None;
                }
            }
        }
    }
}