using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

// Implementacion de hilos 
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.wsFarmacia;
using Dll_IRE_SIGHO.Clases;
using Dll_IRE_SIGHO.Informacion;

namespace Dll_IRE_SIGHO.Informacion
{
    public partial class FrmRecetaEspecifica : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosConexion DatosDeConexion;
        clsLeer myLeer;
        clsLeer leer;
        clsLeer leerXML;
        clsLeerWebExt leerWeb;

        wsCnnCliente conexionWeb;
        Thread _workerThread;
                
        clsDatosCliente DatosCliente;
        clsListView lst;

        string sRutaPlantilla = "";
        clsExportarExcelPlantilla xpExcel;
        clsLeer leerExportarExcel;

        DataSet dtsEstados = new DataSet();
        DataSet dtsJurisdiccion = new DataSet();
        DataSet dtsFarmacias = new DataSet();
        DataSet dtsInformacion;
        clsConsultas Consultas;
        clsAyudas ayuda;
        ClsReplicacioneRecetaElectronica replicacion;
        clsREST Rest;
        ws_IRE_SIGHO.wsIRE_SIGHO IRE_SIGHO;
        //ClsReplicacioneRecetaElectronica Receta;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sJurisdiccion = DtGeneral.Jurisdiccion;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sFechaSistema = General.FechaYMD(General.FechaSistema, "-");
        public string sFolio = "";

        string sUrl = "";
        string sHost = "";
        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;

        public FrmRecetaEspecifica()
        {
            InitializeComponent();
            DatosCliente = new clsDatosCliente(GnDll_SII_RE_SIGHO.DatosApp, this.Name, "");
            conexionWeb = new wsCnnCliente();

            CheckForIllegalCrossThreadCalls = false;

            myLeer = new clsLeer(ref cnn);
            leer = new clsLeer(ref cnn);
            leerExportarExcel = new clsLeer(ref cnn);
            leerXML = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnDll_SII_RE_SIGHO.DatosApp, this.Name);

            Consultas = new clsConsultas(General.DatosConexion, GnDll_SII_RE_SIGHO.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, GnDll_SII_RE_SIGHO.DatosApp, this.Name);
            //Receta = new RecetaElectronica(cnn.DatosConexion);
            IRE_SIGHO = new ws_IRE_SIGHO.wsIRE_SIGHO();
            
        }

        private void FrmDesplazamientosEdosLaboratorios_Load(object sender, EventArgs e)
        {
            replicacion = new ClsReplicacioneRecetaElectronica();
            llenarCombos();

            btnNuevo_Click(null, null);
        }


        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            cboClues.SelectedIndex = 0;
        }

        //private void ObtenerInformacion()
        //{
        //    btnNuevo.Enabled = false;
        //    btnEjecutar.Enabled = false;
            
        //    tmEjecuciones.Enabled = true;
        //    tmEjecuciones.Start();

        //    Cursor.Current = Cursors.WaitCursor;
        //    System.Threading.Thread.Sleep(1000);

        //    _workerThread = new Thread(this.Obetener);
        //    _workerThread.Name = "Obetener";
        //    _workerThread.Start();
        //}

        private bool Obtener()
        {
            bool bRegresa = false;
            string sMensaje = "";
            bEjecutando = true;
            dtsInformacion = new DataSet();
            this.Cursor = Cursors.WaitCursor;
            string urls = string.Format("http://{0}/{1}/{2}", DtGeneral.DatosDeServicioWeb.Servidor, DtGeneral.DatosDeServicioWeb.WebService, "wsIRE_SIGHO.asmx"); 


            HabilitarControles(false);


            try
            {

                IRE_SIGHO.Url = urls;
                sFolio = IRE_SIGHO.GetRecetaElectronica(cboClues.Data, sFolio);
                //bRegresa = replicacion.ObtenerRecetasElectronica_Especifica(cboClues.Data, sFolio);

                bRegresa = sFolio != "";

                if (!bRegresa)
                {
                    sMensaje = "No se encontró información con el folio de receta solicitado, verifique.";
                    General.msjAviso(sMensaje);
                }
                else
                {
                    sMensaje = "Información de receta descargada correctamente.";
                    General.msjUser(sMensaje);
                }
            }
            catch {  }
            {
                
            }

            HabilitarControles(true);

            bEjecutando = false;
            this.Cursor = Cursors.Default;

            return bRegresa; 
        }

        private void HabilitarControles(bool bValor)
        {
            btnNuevo.Enabled = bValor;
            btnEjecutar.Enabled = bValor;
            
            FrameDatos.Enabled = bValor; 
        }
        #endregion Funciones

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            bool bResultado = false; 

            sFolio = txtReceta.Text;
            if (validarDatos())
            {
                bResultado = Obtener();
            }

            if (bResultado)
            {
                this.Hide();
            }
        }
        #endregion Botones

        #region Validaciones
        private bool validarDatos()
        {
            bool bRegresa = true;


            if (bRegresa && cboClues.Data == "0")
            {
                bRegresa = false;
                General.msjUser("No ha Capturado el CLUES, verifique.");
                cboClues.Focus();
            }

            if (bRegresa && txtReceta.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha Capturado el folio de receta, verifique.");
                txtReceta.Focus();
            }
            
            return bRegresa;
        }

        private void llenarCombos()
        {
            cboClues.Clear();
            cboClues.Add(Consultas.UnidadesMedicasEstado_SIGHO(DtGeneral.EstadoConectado, "llenarCombos()"), true, "Clues", "CluesNombreuMedica");

            try
            {
                cboClues.SelectedIndex = 0;
            }
            catch 
            {
                cboClues.Clear();
                cboClues.Add("-1", "<< Sin configuración >>");
                cboClues.SelectedIndex = 0;
            }

        }
        #endregion Validaciones
    }
}
