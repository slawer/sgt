using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Technology
{
    public partial class Form1 : Form
    {
        protected Tech tech;
        protected Generator generator = null;

        public Form1()
        {            
            InitializeComponent();

            tech = new Tech();

            generator = new Generator();
            generator.complete += new GenerationComplete(generator_complete);
        }

        /// <summary>
        /// запустить работу генератора
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            generator.Start(100);
        }

        /// <summary>
        /// Сгенерирован срез данных
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        void generator_complete(object sender, GenerationEventArgs e)
        {
            tech.Calculate(e.Slice);
        }
    }
}