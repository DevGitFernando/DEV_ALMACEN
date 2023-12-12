using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using DllTransferenciaSoft; 

namespace Test_Modulos
{
    public partial class FrmIntegrarInformacion : FrmBaseExt
    {
        string sDirectorio = Application.StartupPath + @"\Integracion_SQL\";
        string sFileIntegracion = ""; 

        public FrmIntegrarInformacion()
        {
            InitializeComponent();

            scComboBoxExt1.Clear();
            scComboBoxExt1.Add(Encoding.Default.EncodingName, Encoding.Default.EncodingName, Encoding.Default);
            scComboBoxExt1.Add(Encoding.ASCII.EncodingName, Encoding.ASCII.EncodingName, Encoding.ASCII);
            scComboBoxExt1.Add(Encoding.UTF7.EncodingName, Encoding.UTF7.EncodingName, Encoding.UTF7);
            scComboBoxExt1.Add(Encoding.UTF8.EncodingName, Encoding.UTF8.EncodingName, Encoding.UTF8);
            scComboBoxExt1.SelectedIndex = 0;

            if (!Directory.Exists(sDirectorio))
            {
                Directory.CreateDirectory(sDirectorio); 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            wsCnnOficinaCentral con = new wsCnnOficinaCentral();
            byte[] archivo = Fg.ConvertirArchivoEnBytes(@"C:\Temp\000000-02-20160810-1247___20160810.SII");

            con.ReplicacionInformacion("Test.SII", archivo); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool bRegresa = false; 
            Encoding encode = (Encoding)scComboBoxExt1.ItemActual.Item;
            sFileIntegracion = scTextBoxExt1.Text; 


            clsDatosConexion datosCnn = new clsDatosConexion();
            datosCnn.Servidor = txtServidor.Text;
            datosCnn.BaseDeDatos = txtBaseDeDatos.Text;
            datosCnn.Usuario = txtUsuario.Text;
            datosCnn.Password = txtPassword.Text;
            datosCnn.Puerto = txtPuerto.Text;
            

            DllTransferenciaSoft.IntegrarInformacion.clsCliente cliente = new DllTransferenciaSoft.IntegrarInformacion.clsCliente(datosCnn, encode);


            sFileIntegracion = sFileIntegracion.Replace("SII", "").Replace(".", "");
            sFileIntegracion = sFileIntegracion.Replace(sDirectorio, ""); 

            cliente.EsIntegracionManual = true;
            cliente.EsIntegracionWeb = true;
            cliente.RutaIntegracionManual = sDirectorio;
            cliente.ArchivoIntegracionManual = sFileIntegracion;
            bRegresa = cliente.Integrar();

        }
    }
}
