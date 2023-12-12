using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

// Implementacion de hilos 
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FTP;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.wsOficinaCentral; 

namespace DllFarmaciaSoft.GetInformacionManual
{
    public partial class FrmDescargarImagenesDeProductos : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeerWebExt leerWeb;
        clsLeer leer;
        clsGrid grid;
        clsDatosCliente datosCliente;
        clsConsultas query;
        DataTable dtTablas;
        
        string sURL = "";
        Thread thDescargar; 

        public FrmDescargarImagenesDeProductos(string URL)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; 

            sURL = URL;
            leer = new clsLeer(ref cnn);
            leerWeb = new clsLeerWebExt(sURL, DtGeneral.CfgIniOficinaCentral, new clsDatosCliente());


            lblProceso.BorderStyle = BorderStyle.None; 
            lblProceso.BackColor = General.BackColorBarraMenu;
            lblProceso.Visible = false; 
        }

        private void FrmDescargarImagenesDeProductos_Load(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void LimpiarPantalla()
        {
            lblProceso.Visible = false; 
            Fg.IniciaControles();

            txtDesde.Focus(); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void BloquearControles(bool Bloquear)
        {
            btnNuevo.Enabled = Bloquear;
            btnObtenerImagenes.Enabled = Bloquear;

            txtDesde.Enabled = Bloquear;
            txtHasta.Enabled = Bloquear;

            chkDescargarTodo.Enabled = Bloquear; 
        }

        private void btnObtenerImagenes_Click(object sender, EventArgs e)
        {
            BloquearControles(false);
            this.Refresh();
            Application.DoEvents(); 

            thDescargar = new Thread(DescargarImagenes);
            thDescargar.Name = "Descargar Imagenes De Productos";
            thDescargar.Start(); 
        }

        private void DescargarImagenes()
        {
            int iRegistros = 0; 
            bool bRegresa = false; 
            string sSql = string.Format(
                "Select  IdProducto, CodigoEAN, Consecutivo, NombreImagen, Imagen, FechaRegistro, IdPersonal, Status, Actualizado \n " +
                "From CatProductos_Imagenes (NoLock) \n " +
                "Where IdProducto between '{0}' and '{1}' ", Fg.PonCeros(txtDesde.Text, 8), Fg.PonCeros(txtHasta.Text, 8));


            if (chkDescargarTodo.Checked)
            {
                sSql = string.Format(
                "Select  IdProducto, CodigoEAN, Consecutivo, NombreImagen, Imagen, FechaRegistro, IdPersonal, Status, Actualizado \n " +
                "From CatProductos_Imagenes (NoLock) \n "); 
            }

            lblProceso.Visible = true;
            lblProceso.Text = "Descargando imagenes ...";
            Application.DoEvents(); 

            if (!leerWeb.Exec("Imagenes", sSql))
            {
                lblProceso.Text = "Ocurrió un error al descargar las imagenes."; 
                Error.GrabarError(leerWeb, "btnObtenerImagenes_Click()"); 
            }
            else
            {
                if (leerWeb.Registros > 0)
                {
                    lblProceso.Text = string.Format("Integrando imagen {0:0} de {1:0} ", iRegistros, leerWeb.Registros);
                }

                while (leerWeb.Leer())
                {
                    sSql = string.Format(
                        "Exec spp_Mtto_CatProductos_Imagenes " +
                        " @IdProducto = '{0}', @CodigoEAN = '{1}', @Consecutivo = '{2}', @NombreImagen = '{3}', @Imagen = '{4}', @IdPersonal = '{5}', @Status = '{6}' ",
                            leerWeb.Campo("IdProducto"), leerWeb.Campo("CodigoEAN"), leerWeb.CampoInt("Consecutivo"),
                            leerWeb.Campo("NombreImagen"), leerWeb.Campo("Imagen"), leerWeb.Campo("IdPersonal"), leerWeb.Campo("Status")  
                        );

                    bRegresa = leer.Exec(sSql);

                    iRegistros++;
                    lblProceso.Text = string.Format("Integrando imagen {0:0} de {1:0} ", iRegistros, leerWeb.Registros);

                    Application.DoEvents();
                    Thread.Sleep(5);
                }
            }

            BloquearControles(true);
        }

        private void lblProceso_EnabledChanged(object sender, EventArgs e)
        {
            string sValor = "";
        }
    }
}
