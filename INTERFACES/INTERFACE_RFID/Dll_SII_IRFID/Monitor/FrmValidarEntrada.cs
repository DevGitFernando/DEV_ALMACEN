using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.Errores;

using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllTransferenciaSoft;
using DllTransferenciaSoft.IntegrarInformacion; 

using Impinj.OctaneSdk;
using Dll_SII_IRFID.Demonio; 

namespace Dll_SII_IRFID.Monitor
{
    public partial class FrmValidarEntrada : FrmBaseExt
    {
        clsMonitorRFID monitor_RFID;  
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente datosCliente;
        clsLeer leer;
        clsConsultas Consulta;
        clsListView lst;
        clsGrid Grid;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sFolioMovimiento = "";

        string[] sListaTipoDeAntenas = { "4" };
        DataSet dtsTAGS_Revision = new DataSet(); 

        public FrmValidarEntrada()
        {
            InitializeComponent();

            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmValidarEntrada");

            leer = new clsLeer(ref cnn);
            Consulta = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name);

            ////Grid = new clsGrid(ref grdProductos, this);
            ////Grid.EstiloDeGrid = eModoGrid.ModoRow;

            lst = new clsListView(listvwResultados); 

            monitor_RFID = new clsMonitorRFID(sListaTipoDeAntenas, listvwResultados);
            monitor_RFID.ValidarTAGS_Leidos__vs_Listado = true;

            CargarComboFarmacias(); 
        }

        #region Form 
        private void FrmValidarEntrada_Load(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void FrmValidarEntrada_FormClosing(object sender, FormClosingEventArgs e)
        {
            monitor_RFID.Stop(2, true);
            IniciarToolBar(true, false, true, false);
        }
        #endregion Form

        #region Botones 
        public void LimpiarPantalla()
        {
            IniciarToolBar(); 
            
            Fg.IniciaControles();
            lst.LimpiarItems(); 

            dtpFechaRegistro.Enabled = false;
            cboFarmacias.Focus(); 

        }

        private void IniciarToolBar()
        {
            IniciarToolBar(true, false, false, false); 
        }

        private void IniciarToolBar(bool Nuevo, bool Guardar, bool Escanear, bool DetenerEscaneo)
        {
            btnNuevo.Enabled = Nuevo;
            btnGuardar.Enabled = Guardar;
            btnIniciarEscaneo.Enabled = Escanear;
            btnIntegrarPaquetesDeDatos.Enabled = !Escanear; 
            btnDetenerEscaneo.Enabled = DetenerEscaneo;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        private void btnIntegrarPaquetesDeDatos_Click(object sender, EventArgs e)
        {
            FrmIntegrarPaquetesDeDatos f = new FrmIntegrarPaquetesDeDatos();
            f.ShowDialog(this); 
        }

        private void btnIniciarEscaneo_Click(object sender, EventArgs e)
        {
            lst.LimpiarItems(); 
            IniciarToolBar(false, false, false, true); 
            monitor_RFID.TAGS_Revision = dtsTAGS_Revision;
            monitor_RFID.Start(); 
        }

        private void btnDetenerEscaneo_Click(object sender, EventArgs e)
        {
            monitor_RFID.Stop(2, true);
            IniciarToolBar(true, false, true, false);

            if (monitor_RFID.Resultado_ValidacionTAGS())
            {
                IniciarToolBar(true, true, false, false);
                General.msjUser("Lectura de tags correcta."); 
            }
            else
            {
                General.msjAviso("Se detecto un error el número de tags a validar.");
            }

        }
        #endregion Botones

        private void CargarComboFarmacias()
        {
            cboFarmacias.Clear();
            cboFarmacias.Add();

            cboFarmacias.Filtro = " EsAlmacen = 1 ";
            cboFarmacias.Add(Consulta.Farmacias(sEstado, "CargarComboFarmacias()"), true, "IdFarmacia", "NombreFarmacia");

            cboFarmacias.SelectedIndex = 0;
        }

        #region Informacion
        private void txtFolioMovto_Validating(object sender, CancelEventArgs e)
        {
            if (txtFolioMovto.Text.Trim() == "" || txtFolioMovto.Text.Trim() == "*")
            {
                //// No hacer nada 
            }
            else
            {
                leer.DataSetClase = Consulta.RFID_Movimientos(sEmpresa, sEstado, cboFarmacias.Data, txtFolioMovto.Text.Trim(), "txtFolioMovto_Validating");
                if (!leer.Leer())
                {
                    //General.msjUser(); 
                    txtFolioMovto.Text = "";
                    txtFolioMovto.Focus();
                }
                else
                {
                    IniciarToolBar(true, false, true, false);
                    Get_001_Encabezado();
                }
            }
        }

        private bool Get_001_Encabezado()
        {
            bool bRegresa = true;
            string sSql = "";

            cboFarmacias.Enabled = false; 
            txtFolioMovto.Enabled = false;
            txtFolioMovto.Text = leer.Campo("Folio");
            dtpFechaRegistro.Value = leer.CampoFecha("FechaRegistro");


            if (bRegresa)
            {
                bRegresa = Get_002_Detalles();
            }

            return bRegresa;
        }

        private bool Get_002_Detalles()
        {
            bool bRegresa = false;
            string sSql = "";
            leer.DataSetClase = Consulta.RFID_MovimientosDetalles_ListadoTAGS(sEmpresa, sEstado, cboFarmacias.Data, txtFolioMovto.Text.Trim(), "Get_002_Detalles()");

            if (leer.Leer())
            {
                dtsTAGS_Revision = leer.DataSetClase; 
                ////Grid.LlenarGrid(leer.DataSetClase);
            }

            return bRegresa;
        }
        #endregion Informacion
    }
}
