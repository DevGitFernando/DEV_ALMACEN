using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

using DllFarmaciaSoft;

namespace DllTransferenciaSoft.IntegrarBD
{
    public partial class FrmMttoFTP : FrmBaseExt 
    {
        
        clsFTP_BaseDeDatos ftp;
        clsListView lst; 

        public FrmMttoFTP()
        {
            InitializeComponent();
            lst = new clsListView(lstArchivos);
            // lst.PermitirAjusteDeColumnas = false; 
        }

        private void FrmMttoFTP_Load(object sender, EventArgs e)
        {
            ftp = new clsFTP_BaseDeDatos(@"C:\inetpub\ftproot\");

            ftp.ListarFTP(twFTP); 
        }

        private void twFTP_AfterSelect(object sender, TreeViewEventArgs e)
        {
            lst.LimpiarItems();
            string sNodo = ""; 
            string sRuta = ""; 
            string []sCols = { "Id", "Archivo", "Fecha", "Size" };

            try
            {
                sNodo = twFTP.SelectedNode.Tag.ToString();  
            }
            catch { }

            if (Fg.Mid(sNodo, 1, 1) == "1") 
            {
                sRuta = Fg.Mid(twFTP.SelectedNode.Tag.ToString(), 2);
                
                clsLeer leer = new clsLeer();
                leer.DataSetClase = ftp.Archivos(sRuta);
                leer.FiltrarColumnas(1, sCols); 

                lst.CargarDatos(leer.DataSetClase);
            }
        }
    }
}
