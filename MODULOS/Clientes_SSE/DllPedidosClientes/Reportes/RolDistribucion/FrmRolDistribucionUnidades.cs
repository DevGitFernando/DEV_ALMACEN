using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

// Implementacion de hilos 
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllPedidosClientes;

namespace DllPedidosClientes.Reportes
{
    public partial class FrmRolDistribucionUnidades : FrmBaseExt 
    {
        public FrmRolDistribucionUnidades()
        {
            InitializeComponent();
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            webView.Visible = false; 
        }

        private void bntSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMenor800_Click(object sender, EventArgs e)
        {
            string sUrl = "";
            sUrl = "http://" + DtGeneralPedidos.DatosDeServicioWeb.Servidor;
            sUrl += "/" + DtGeneralPedidos.DatosDeServicioWeb.WebService;
            sUrl += "/" + "Documentos/Menores_800_Familias.pdf";

            webView.Visible = true;
            webView.Navigate(sUrl); 
        }

        private void btnVistaPrevia_Click(object sender, EventArgs e)
        {
            string sUrl = "";
            sUrl = "http://" + DtGeneralPedidos.DatosDeServicioWeb.Servidor;
            sUrl += "/" + DtGeneralPedidos.DatosDeServicioWeb.WebService;
            sUrl += "/" + "Documentos/Rol.pdf"; 

            //// Uri uri = new Uri("ftp://" + _hostName + directoryPath);
            //webView.Url = new Uri("http://pl-consultas.dyndns-ip.com/wsCliente/Archivos/Rol.pdf"); 

            webView.Visible = true;
            webView.Navigate(sUrl); 
            ////// webView.Navigate("http://pl-consultas.dyndns-ip.com/wsCliente/Archivos/Rol.pdf");

            //////sUrl = "http://pl-consultas.dyndns-ip.com/wsCliente/Archivos/Rol.pdf"; 

            //////webView.Navigate("http://pl-consultas.dyndns-ip.com/wsCliente/Archivos/01.xlsx");


            ////// webView.Url = new Uri(@"C:\Users\JesusDiaz\Desktop\Recibir\Rol__X.pdf");
            //////webView.Navigate(@"C:\Users\JesusDiaz\Desktop\Recibir\Rol__X.pdf"); 

            //////webView.Url = new Uri("http://pl-consultas.dyndns-ip.com/wsCliente/Archivos/02.txt");
            //////webView.Navigate("http://pl-consultas.dyndns-ip.com/wsCliente/Archivos/02.txt");

            //////webView.Url = new Uri("http://pl-consultas.dyndns-ip.com/wsCliente/Archivos/03.sql"); 
            //////webView.Navigate("http://pl-consultas.dyndns-ip.com/wsCliente/Archivos/03.sql");
        }
        #endregion Botones 


    }
}