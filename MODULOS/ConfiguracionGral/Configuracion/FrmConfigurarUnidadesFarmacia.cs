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

using DllFarmaciaSoft;

namespace Configuracion
{
    public partial class FrmConfigurarUnidadesFarmacia : FrmBaseExt
    {
        // clsDatosConexion DatosDeConexion; 
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        // clsConexionSQL cnnUnidad;  
        clsLeer leer;
        clsLeer leerLocal; 
        clsLeerWebExt leerWeb;
        clsConsultas Consultas;
        clsAyudas Ayuda;
        // clsGrid Grid;
        clsGrid gridUnidades; 

        string sSqlFarmacias = "";
        // string sUrl;
        // string sHost = ""; 
        // string sUrl_RutaReportes = ""; 

        clsDatosCliente DatosCliente;

        // int iBusquedasEnEjecucion = 0;
        int iEsEmpresaConsignacion = Convert.ToInt32(DtGeneral.EsEmpresaDeConsignacion);
        DataSet dtsEstados = new DataSet();
        // string sIdPublicoGeneral =  "0001";

        // bool bEjecutando = false;
        // bool bSeEncontroInformacion = false;
        // bool bSeEjecuto = false;

        Color colorEjecutando = Color.DarkSeaGreen;
        Color colorEjecucionExito = Color.White;
        Color colorEjecucionError = Color.BurlyWood;

        public FrmConfigurarUnidadesFarmacia()
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "");

            CheckForIllegalCrossThreadCalls = false;
            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmConfigurarUnidadesFarmacia");
            leerWeb = new clsLeerWebExt(ref cnn, General.Url, General.ArchivoIni, DatosCliente);


            ////cnn = new clsConexionSQL();
            ////cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            ////cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            ////cnn.DatosConexion.Usuario= General.DatosConexion.Usuario;
            ////cnn.DatosConexion.Password = General.DatosConexion.Password;
            ////cnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            ////cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite; 

            leer = new clsLeer(ref cnn);
            leerLocal = new clsLeer(ref cnn); 
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            gridUnidades = new clsGrid(ref grdFarmacias, this);
            gridUnidades.EstiloGrid(eModoGrid.ModoRow);
            gridUnidades.AjustarAnchoColumnasAutomatico = true;

            gridUnidades.SetOrder(true); 
        }

        private void FrmConfigurarUnidadesFarmacia_Load(object sender, EventArgs e)
        {
            CargarEmpresas(); 
            btnNuevo_Click(null, null);
        }

        #region Cargar Combos 
        private void CargarEmpresas()
        {
            cboEmpresas.Clear();
            cboEmpresas.Add();

            cboEstados.Clear();
            cboEstados.Add();
            cboEstados.SelectedIndex = 0;

            string sSql = "Select Distinct IdEmpresa, NombreEmpresa From vw_EmpresasEstados (NoLock) Order By IdEmpresa ";
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarEmpresas()");
                General.msjError("Ocurrió un error al obtener la lista de Empresas.");
            }
            else
            {
                cboEmpresas.Add(leer.DataSetClase, true, "IdEmpresa", "NombreEmpresa");
                sSql = "Select distinct IdEstado, NombreEstado, IdEmpresa, StatusEdo From vw_EmpresasEstados (NoLock) Order By IdEmpresa ";
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "CargarEmpresas()");
                    General.msjError("Ocurrió un error al obtener la lista de Estados por Empresas.");
                }
                else
                {
                    dtsEstados = leer.DataSetClase;
                }

            }
            cboEmpresas.SelectedIndex = 0;
        }

        private void CargarEstados()
        {
            string sFiltro = string.Format(" IdEmpresa = '{0}' and StatusEdo = '{1}' ", cboEmpresas.Data, "A");
            cboEstados.Clear();
            cboEstados.Add();
            cboEstados.Add(dtsEstados.Tables[0].Select(sFiltro), true, "IdEstado", "NombreEstado");
            cboEstados.SelectedIndex = 0;
        }

        private void CargarFarmacias()
        {
            sSqlFarmacias = string.Format(" Select Distinct U.IdFarmacia, (U.IdFarmacia + ' - ' + U.Farmacia) as Farmacia, U.UrlFarmacia, 0 as Procesar, C.Servidor  " +
                            " From vw_Farmacias_Urls U (NoLock) " +
                            " Inner Join CFGS_ConfigurarConexiones C (NoLock) On ( U.IdEstado = C.IdEstado and U.IdFarmacia = C.IdFarmacia ) " +
                            " Where U.IdEmpresa = '{0}' and U.IdEstado = '{1}' and ( U.IdFarmacia <> '{2}' ) " +
                            "   and U.FarmaciaStatus = 'A' and U.StatusRelacion = 'A' ", 
                            cboEmpresas.Data, cboEstados.Data, DtGeneral.FarmaciaConectada); 

            // gridUnidades.Limpiar(); 
            //cboFarmacias.Clear();
            //cboFarmacias.Add("0", "<< Seleccione >>"); 

            sSqlFarmacias = string.Format("	Select " + 	
                " F.IdFarmacia, F.Farmacia, " +  
                " (case when IsNull(L.Status, 'C') = 'C' Then 0 Else 1 End ) as StatusRegistro, " +
                " (case when IsNull(L.Status, 'C') = 'C' Then 0 Else 1 End ) as StatusRegistroAuxiliar  " + 
	            " From vw_Farmacias F (NoLock)  " + 
	            " Left Join CFG_Svr_UnidadesRegistradas L (NoLock) On ( F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia ) " + 
	            " Where F.IdEstado = '{0}' and F.Status = 'A' and F.IdFarmacia <> '{1}' ", cboEstados.Data, DtGeneral.FarmaciaConectada);

            sSqlFarmacias = string.Format("	Select " +
                " F.IdFarmacia, F.Farmacia, " +
                " (case when IsNull(L.Status, 'C') = 'C' Then 0 Else 1 End ) as StatusRegistro, " +
                " (case when IsNull(L.Status, 'C') = 'C' Then 0 Else 1 End ) as StatusRegistroAuxiliar,  " +
                " L.EsRegional" +
                " From vw_Farmacias F (NoLock)  " +
                " Left Join CFG_Svr_UnidadesRegistradas L (NoLock) On ( F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia ) " +
                " Where F.IdEstado = '{0}' and F.Status = 'A' and F.IdFarmacia <> '' ", cboEstados.Data); 

            if (!leer.Exec(sSqlFarmacias))
            {
                Error.GrabarError(leer, "CargarFarmacias()");
                General.msjError("Ocurrió un error al obtener la lista de farmacias.");
            }
            else
            {
                //cboFarmacias.Add(leer.DataRowsClase, true, "IdFarmacia", "Farmacia");
                gridUnidades.LlenarGrid(leer.DataSetClase);

                ////grdResultados.Sheets.Clear(); 
                ////while (leer.Leer())
                ////{
                ////    FarPoint.Win.Spread.SheetView hoja = new FarPoint.Win.Spread.SheetView(leer.Campo("Farmacia"));
                ////    // hoja.
                ////    grdResultados.Sheets.Add(hoja); 
                ////}
            }
            //cboFarmacias.SelectedIndex = 0;
        }

        #endregion Cargar Combos  

        #region Llenar Combo Reporte
        private void LlenarCombo()
        {
        }
        #endregion Llenar Combo Reporte

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // bool bValor = true;
            // iBusquedasEnEjecucion = 0;

            cboEstados.Enabled = false;
            cboEstados.SelectedIndex = 0; 


            // Grid.Limpiar(); 
            Fg.IniciaControles(this, true); 
            gridUnidades.Limpiar();

            if (!DtGeneral.EsAdministrador)
            {
                cboEmpresas.Data = DtGeneral.EmpresaConectada;
                cboEstados.Data = DtGeneral.EstadoConectado;

                cboEmpresas.Enabled = false;
                cboEstados.Enabled = false; 
            }

            //txtCte.Focus(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bRegresa = true;
            string sSql = "";
            string sFarmacia = "";
            int iValor = 0, iValorAux = 0;
            int iEsRegional = 0, iEsRegionalAux = 0; 

            if (cnn.Abrir())
            {
                cnn.IniciarTransaccion();
                for (int i = 1; i <= gridUnidades.Rows; i++)
                {
                    sFarmacia = gridUnidades.GetValue(i, 1);
                    iValor = gridUnidades.GetValueInt(i, 3);
                    iValorAux = gridUnidades.GetValueInt(i, 4); 
                    iEsRegional = gridUnidades.GetValueInt(i, 5);
                    iEsRegionalAux = gridUnidades.GetValueInt(i, 6);

                    if ((iValor != iValorAux) || (iEsRegional != iEsRegionalAux)) 
                    {
                        sSql = string.Format(" Exec spp_Mtto_CFG_Svr_UnidadesRegistradas '{0}', '{1}', '{2}', '{3}', '{4}' ",
                            cboEmpresas.Data, cboEstados.Data, sFarmacia, iValor, iEsRegional); 
                        if (!leer.Exec(sSql)) 
                        {
                            bRegresa = false;
                            break; 
                        }
                    }
                }

                if (bRegresa)
                {
                    cnn.CompletarTransaccion();
                    General.msjUser("Información guardada satisfactoriamente.");
                    btnNuevo_Click(null, null); 
                }
                else
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(leer, "btnGuardar_Click");
                    General.msjError("Ocurrió un error al guardar la Información.");
                }
                cnn.Cerrar(); 
            }
            else
            {
                General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo."); 
            }
        }

        #endregion Botones

        private void cboEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboEstados.Clear();
            cboEstados.Add();

            if (cboEmpresas.SelectedIndex != 0)
            {
                cboEmpresas.Enabled = false;
                cboEstados.Enabled = true;
                CargarEstados();
            } 
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridUnidades.Limpiar(); 

            if (cboEstados.SelectedIndex != 0)
            {
                cboEstados.Enabled = false;
                CargarFarmacias();
            } 
        }
    } 
}
