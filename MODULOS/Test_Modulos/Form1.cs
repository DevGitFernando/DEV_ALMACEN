using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.FuncionesGenerales;

namespace Test_Modulos
{
    public partial class Form1 : Form
    {
        string sCodigo = "";
        byte[] bBuffer = null;
        int[] iSeparadores = null;


      

        basGenerales Fg = new basGenerales();

        int iCaracter = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private byte[] HexStringToByteArray(string s)
        {
            string sError = "";
            string sValor = ""; 

            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 1];
            for (int i = 0; i < s.Length; i += 1)
            {
                try
                {
                    sValor = s.Substring(i, 1);
                    buffer[i / 1] = (byte)Convert.ToByte(sValor, 16);
                }
                catch (Exception ex)
                {
                    sError = ex.Message; 
                }
            }
            return buffer;
        }

        private string ByteArrayToHexString(byte[] data)
        {
            string sError = ""; 

            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
            {
                try 
                {
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
                }
                catch (Exception ex)
                {
                    sError = ex.Message;
                }
            }
            return sb.ToString().ToUpper();
        }

        private void txtCodigo_Validating(object sender, CancelEventArgs e)
        {
            //if (txtCodigo.Text.Trim() != "")
            //{
            //    get
            //    bBuffer = Encoding.ASCII.GetBytes(txtCodigo.Text.Trim());
            //    sCodigo = Encoding.ASCII.GetString(bBuffer).ToLower();

            //    //bBuffer = HexStringToByteArray(txtCodigo.Text.Trim());
            //    //sCodigo = ByteArrayToHexString(bBuffer); 


            //}
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            


        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            Keys f;
            f = e.KeyCode;

            iCaracter += 1; 

            if (f == Keys.ControlKey)
            {
                txt3.Text += iCaracter.ToString() + "&&";
                txt2.Text += "<GS>";
            }

            //txt2.Text = txt2.Text + f + "(" + iCaracter + ")";
            //bBuffer2 = f;
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            char f;
            f = e.KeyChar;

            txt2.Text = txt2.Text + f.ToString();
            //txt2.Text = txt2.Text +  Fg.Asc(f.ToString()).ToString() + "(" + f.ToString() + ")";
            //bBuffer2 = f;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            iCaracter = 0;
            txt2.Text = "";
            txt3.Text = "";
            txtCodigo.Text = "";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }
    }
}
