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
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.wsFarmacia;
using Dll_IRE_INTERMED.Clases;

namespace Dll_IRE_INTERMED.Informacion
{
    public partial class FrmRecetaEspecifica : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosConexion DatosDeConexion;
        clsLeer myLeer;
        clsLeer leer;
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
        clsConsultas Consultas;
        clsAyudas ayuda;
        ClsReplicacioneRecetaElectronica replicacion;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sJurisdiccion = DtGeneral.Jurisdiccion;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sFechaSistema = General.FechaYMD(General.FechaSistema, "-");
        string sFolio = "";
        string sCLUES = ""; 

        string sUrl = "";
        string sHost = "";
        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;

        public FrmRecetaEspecifica() : this("") 
        {
        }

        public FrmRecetaEspecifica(string CLUES)
        {
            InitializeComponent();

            sCLUES = CLUES; 
            DatosCliente = new clsDatosCliente(GnDll_SII_RE_INTERMED.DatosApp, this.Name, "");
            conexionWeb = new wsCnnCliente();

            CheckForIllegalCrossThreadCalls = false;

            myLeer = new clsLeer(ref cnn);
            leer = new clsLeer(ref cnn);
            leerExportarExcel = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnDll_SII_RE_INTERMED.DatosApp, this.Name);

            Consultas = new clsConsultas(General.DatosConexion, GnDll_SII_RE_INTERMED.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, GnDll_SII_RE_INTERMED.DatosApp, this.Name);
            
        }

        private void FrmDesplazamientosEdosLaboratorios_Load(object sender, EventArgs e)
        {
            replicacion = new ClsReplicacioneRecetaElectronica();
            llenarCombos();

            LimpiaPantalla(); 
        }

        #region Propiedades 
        public string Folio
        {
            get { return sFolio; }
        }
        #endregion Propiedades 

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);

            pgBar.Top = txtReceta.Top + 100; 

            cboEstados.SelectedIndex = 0;
            cboEstados.Data = sCLUES;
        }

        private void ObtenerInformacion()
        {
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;

            pgBar.Top = txtReceta.Top;
            Application.DoEvents();
            //Thread.Sleep(1000); 

            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();

            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(1000);

            _workerThread = new Thread(this.Obtener);
            _workerThread.Name = "Obtener";
            _workerThread.Start();
        }

        private void Obtener()
        {
            string sMensaje = "";
            bEjecutando = true;
            bool bRegresa = false;
            
            this.Cursor = Cursors.WaitCursor;
            //Thread.Sleep(1000);

            HabilitarControles(false);

            sFolio = ""; 
            bRegresa = replicacion.ObtenerRecetasElectronica_Especifica(cboEstados.Data.Trim(), txtReceta.Text);

            if (!replicacion.bRecetaEncontrada)
            {
                sMensaje = "No se encontró la receta.";
            }
            else
            {
                sMensaje = "Receta descargada.";
                sFolio = txtReceta.Text; 
            }

            pgBar.Top = txtReceta.Top + 100;
            Application.DoEvents();
            Thread.Sleep(1000);

            General.msjAviso(sMensaje);

            HabilitarControles(true);

            bEjecutando = false;
            this.Cursor = Cursors.Default;

            if(replicacion.bRecetaEncontrada && sCLUES != "")
            {
                this.Hide();
            }
        }

        private void HabilitarControles(bool bValor)
        {
            btnNuevo.Enabled = bValor;
            btnEjecutar.Enabled = bValor;

            //FrameDatos.Enabled = bValor; 

            cboEstados.Enabled = bValor;
            txtReceta.Enabled = bValor;

            //pgBar.Top = bValor ? txtReceta.Top : txtReceta.Top + 100;
        }
        #endregion Funciones



        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                ObtenerInformacion();
            }
        }
        #endregion Botones

        #region Validaciones
        private bool validarDatos()
        {
            bool bRegresa = true;


            if (bRegresa && cboEstados.Data == "0")
            {
                bRegresa = false;
                General.msjUser("No ha Capturado el CLUES, verifique.");
                cboEstados.Focus();
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
            cboEstados.Clear();
            cboEstados.Add("0", "<<Seleccione>>");
            cboEstados.Add(Consultas.UnidadesMedicasEstado(DtGeneral.EstadoConectado, "llenarCombos()"), true, "Clues", "CluesNombreuMedica");
            cboEstados.SelectedIndex = 0;

        }
        #endregion Validaciones       
    }
}
