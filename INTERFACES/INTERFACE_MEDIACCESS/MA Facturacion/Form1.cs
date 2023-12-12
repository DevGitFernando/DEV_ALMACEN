using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;

namespace MA_Facturacion
{
    public partial class Form1 : FrmBaseExt 
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ////General.msjAviso(button1.Parent.Name); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Fg.IniciaControles(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Fg.IniciaTabControls(); 
            //scTabControlExt1.DrawFrame(this.CreateGraphics()); 

            //scTabControlExt1.BackColor = this.BackColor; 
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //scTabControlExt1.MostrarBorde = !scTabControlExt1.MostrarBorde; 
        }
    }
}
