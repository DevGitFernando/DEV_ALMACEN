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

namespace DllFarmaciaSoft.LimitesConsumoClaves
{
    public partial class FrmDescargarProgramacionConsumos : FrmBaseExt
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

        public FrmDescargarProgramacionConsumos()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; 

            leer = new clsLeer(ref cnn);
            ////leerWeb = new clsLeerWebExt(sURL, DtGeneral.CfgIniOficinaCentral, new clsDatosCliente());


            lblProceso.BorderStyle = BorderStyle.None; 
            lblProceso.BackColor = General.BackColorBarraMenu;
            lblProceso.Visible = false; 
        }

        private void FrmDescargarProgramacionConsumos_Load(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void LimpiarPantalla()
        {
            lblProceso.Visible = false; 
            Fg.IniciaControles();

            rdoSvrCentral.Checked = true; 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void BloquearControles(bool Bloquear)
        {
            btnNuevo.Enabled = Bloquear;
            btnObtenerImagenes.Enabled = Bloquear;

            rdoSvrCentral.Enabled = Bloquear;
            rdoSvrRegional.Enabled = Bloquear;
            nmAño.Enabled = Bloquear;
            nmMes.Enabled = Bloquear;
        }

        private void btnObtenerImagenes_Click(object sender, EventArgs e)
        {
            BloquearControles(false);
            this.Refresh();
            Application.DoEvents();

            thDescargar = new Thread(DescargarProgramacion);
            thDescargar.Name = "Descargar Programacion de Consumos";
            thDescargar.Start(); 
        }

        private void DescargarProgramacion()
        {
            string sUrlConexion = "";
            int iRegistros = 0; 
            bool bRegresa = false;
            string sSql = "";
            clsLeer leerDescarga = new clsLeer(); 
            DataSet dtsDescarga = new DataSet();
            DataTable dtTablasDescarga = new DataTable(); 


            if (DtGeneral.ModuloEnEjecucion == TipoModulo.Farmacia || DtGeneral.ModuloEnEjecucion == TipoModulo.Almacen
                || DtGeneral.ModuloEnEjecucion == TipoModulo.FarmaciaUnidosis || DtGeneral.ModuloEnEjecucion == TipoModulo.AlmacenUnidosis)
            {
                sUrlConexion = DtGeneral.UrlServidorCentral;
                if (rdoSvrRegional.Checked)
                {
                    sUrlConexion = DtGeneral.UrlServidorRegional;
                }
            }
            else 
            {
                ////sUrlConexion = DtGeneral.UrlServidorCentral_Regional;
                sUrlConexion = General.Url;
            }


            lblProceso.Visible = true;
            lblProceso.Text = string.Format("Descargando información de {0} - {1} ", nmAño.Value, nmMes.Value);
            Application.DoEvents();

            sSql = string.Format("Exec spp_GET_CFG_CB_CuadroBasico_Claves_Programacion " +
                " @IdEstado = '{0}', @IdFarmacia = '{1}', @Año = '{2}', @Mes = '{3}' ",
                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, nmAño.Value, nmMes.Value);

            leerWeb = new clsLeerWebExt(sUrlConexion, DtGeneral.CfgIniOficinaCentral, new clsDatosCliente());
            if (!leerWeb.Exec("Programacion", sSql))
            {
                lblProceso.Text = "Ocurrió un error al descargar las imagenes."; 
                Error.GrabarError(leerWeb, "btnObtenerImagenes_Click()"); 
            }
            else
            {
                dtsDescarga = new DataSet();
                if (leerWeb.Tablas == 2)
                {
                    leerWeb.RenombrarTabla(1, "01_Programacion");
                    leerWeb.RenombrarTabla(2, "02_Excepciones");

                    dtTablasDescarga.Merge(leerWeb.Tabla(1), true);
                    dtTablasDescarga.Merge(leerWeb.Tabla(2), true);

                    leerDescarga.DataTableClase = dtTablasDescarga; 
                }


                if (leerDescarga.Registros > 0)
                {
                    lblProceso.Text = string.Format("Integrando registro {0:0} de {1:0} ", iRegistros, leerDescarga.Registros);
                }

                bRegresa = true;
                while (leerDescarga.Leer())
                {
                    sSql = leerDescarga.Campo("Resultado");

                    if (!leer.Exec(sSql))
                    {
                        Error.GrabarError(leer, "DescargarProgramacion"); 
                        bRegresa = false;
                        break; 
                    }

                    iRegistros++;
                    lblProceso.Text = string.Format("Integrando registro {0:0} de {1:0} ", iRegistros, leerDescarga.Registros);

                    Application.DoEvents();
                    Thread.Sleep(5);
                }

                if (!bRegresa)
                {
                    lblProceso.Text += " error en el proceso ";
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
