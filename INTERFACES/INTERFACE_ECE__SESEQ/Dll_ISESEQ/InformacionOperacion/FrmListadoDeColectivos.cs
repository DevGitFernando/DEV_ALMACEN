using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Windows.Forms;

using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Data.Odbc;

using Microsoft.VisualBasic;

using System.Text;
using System.IO;
using System.Configuration;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;


using DllFarmaciaSoft; 
using DllFarmaciaSoft.ExportarExcel;


namespace Dll_ISESEQ.InformacionOperacion
{
    public partial class FrmListadoDeColectivos : FrmBaseExt 
    {

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        ////clsErrorManager error; 

        clsListView lst; 
        public FrmListadoDeColectivos()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; 

            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(GnDll_SII_SESEQ.DatosApp, this.Name);

            lst = new clsListView(lstResultados);
        }

        private void FrmListadoDeColectivos_Load( object sender, EventArgs e )
        {
            IniciarPantalla();

            GetListadoDeColectivos(); 
        }

        #region Botones 
        private void IniciarPantalla()
        {
            Fg.IniciaControles(true);
            lst.LimpiarItems(); 

            pgBar.Visible = false; 
            lblMensaje.Text = "Preparado para envio de información";
        }
        private void btnNuevo_Click( object sender, EventArgs e )
        {
            IniciarPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            GetListadoDeColectivos();
        }

        private void btnEnviarInformacion_Click( object sender, EventArgs e )
        {
            string sFolioSESEQ = lst.LeerItem().Campo("Folio interno"); 
            string sFolio = lst.LeerItem().Campo("Folio de colectivo");

            if (sFolio == "")
            {
                General.msjAviso("No ha seleccionado un folio de colectivo válido, verifique.");
            }
            else
            {
                FrmEntregaDeColectivos f = new FrmEntregaDeColectivos(sFolioSESEQ, sFolio);
                f.ShowInTaskbar = false;

                f.ShowDialog(this); 
                if (f.EntregaValidada)
                {
                    General.msjAviso(string.Format("Entrega del colectivo {0} autorizada.", sFolio));
                    GetListadoDeColectivos(); 
                }
            }
        }
        #endregion Botones 

        #region Funciones y Procedimientos Privados 
        private void GetListadoDeColectivos()
        {
            string sSql = string.Format(
                "Exec spp_INT_SESEQ__RecetasElectronicas_0031_Obtener_ColectivosAtendidos \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioReceta = '{3}' ", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtFolioRecetaColectivo.Text.Trim()
                );

            lst.LimpiarItems();
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "GetListadoDeColectivos");
                General.msjError("Ocurrió un error al obtener la información solicitada.");
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjAviso("No se encontro información de Colectivos.");
                }

                lst.CargarDatos(leer.DataSetClase, true, true);
            }
        }
        #endregion Funciones y Procedimientos Privados 
    }
}
