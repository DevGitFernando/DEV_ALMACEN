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

namespace OficinaCentral.Configuraciones
{
    public partial class FrmConfigurarUnidadesAlmacen : FrmBaseExt
    {
         
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerLocal; 
        clsLeerWebExt leerWeb;
        clsConsultas Consultas;
        clsAyudas Ayuda;
        clsGrid gridUnidades; 

        string sSqlFarmacias = "";        

        clsDatosCliente DatosCliente;

        int iEsEmpresaConsignacion = Convert.ToInt32(DtGeneral.EsEmpresaDeConsignacion);
        DataSet dtsEstados = new DataSet();
        
        Color colorEjecutando = Color.DarkSeaGreen;
        Color colorEjecucionExito = Color.White;
        Color colorEjecucionError = Color.BurlyWood;

        public FrmConfigurarUnidadesAlmacen()
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "");

            CheckForIllegalCrossThreadCalls = false;
            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmConfigurarUnidadesFarmacia");
            leerWeb = new clsLeerWebExt(ref cnn, General.Url, General.ArchivoIni, DatosCliente);
            
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
            CargarEstados();
            btnNuevo_Click(null, null);
        }

        #region Cargar Combos
        private void CargarEstados()
        {
            cboAlmacenes.Clear();
            cboAlmacenes.Add();
                        
            cboEstados.Clear();
            cboEstados.Add();

            leer.DataSetClase = Consultas.EstadosConFarmacias("CargarEstados");
            
            if (leer.Leer())
            {
                cboEstados.Add(leer.DataSetClase, true, "IdEstado", "Estado");
            }
            
            cboEstados.SelectedIndex = 0;
        }

        private void CargarAlmacenes()
        {
            string sSql = "";

            cboAlmacenes.Clear();
            cboAlmacenes.Add();

            sSql = string.Format(" Select IdFarmacia, (IdFarmacia + '--' + Farmacia) as Farmacia From vw_Farmacias " +
                                    " Where IdEstado = '{0}' and IdTipoUnidad = '006' and EsAlmacen = 1 and Status = 'A'  ", cboEstados.Data);

            if (!leer.Exec(sSql))
            {
                General.msjError("Ocurrió un error al obtener la lista de almacenes.");
                Error.GrabarError(leer, "CargarAlmacenes");
            }
            else
            {
                if (leer.Leer())
                {
                    cboAlmacenes.Add(leer.DataSetClase, true, "IdFarmacia", "Farmacia");
                }
            }          
            cboAlmacenes.SelectedIndex = 0;
        }

        private void CargarFarmacias()
        {
            sSqlFarmacias = string.Format(" Select F.IdFarmacia, ( F.IdFarmacia + '--' + F.Farmacia ) as Farmacia, C.EsAsignado as Asignar, C.EsAsignado as Valor " +
                                " From vw_Farmacias F  Inner Join CFG_ALMN_Unidades_Atendidas C On ( F.IdEstado = C.IdEstado AND F.IdFarmacia = C.IdFarmacia ) " +
                                " Where F.IdEstado = '{0}' and C.IdAlmacen = '{1}' and F.Status = 'A' AND F.IdTipoUnidad Not In ('005', '006') and C.EsAsignado = 1 " +
                                " UNION " +
                                " Select F.IdFarmacia, ( F.IdFarmacia + '--' + F.Farmacia ) as Farmacia, 0 as Asignar, 0 as Valor " +
                                " From vw_Farmacias F  Where F.IdEstado = '{0}' and F.Status = 'A' AND F.IdTipoUnidad Not In ('005', '006') " +
                                " AND Not Exists (Select IdFarmacia From CFG_ALMN_Unidades_Atendidas U  " +
                                " Where U.IdEstado = F.IdEstado AND U.IdFarmacia = F.IdFarmacia ) " +
                                " UNION " +
                                " Select F.IdFarmacia, ( F.IdFarmacia + '--' + F.Farmacia ) as Farmacia, C.EsAsignado as Asignar, C.EsAsignado as Valor " +
                                " From vw_Farmacias F  Inner Join CFG_ALMN_Unidades_Atendidas C On ( F.IdEstado = C.IdEstado AND F.IdFarmacia = C.IdFarmacia ) " +
                                " Where F.IdEstado = '{0}' and F.Status = 'A' AND F.IdTipoUnidad Not In ('005', '006') and C.EsAsignado = 0 " +
                                " AND NOT Exists (Select IdFarmacia From CFG_ALMN_Unidades_Atendidas U " + 
				                " Where U.IdEstado = C.IdEstado AND U.IdFarmacia = C.IdFarmacia AND U.EsAsignado = 1 )",
                                cboEstados.Data, cboAlmacenes.Data); 

            gridUnidades.Limpiar(); 
            

            if (!leer.Exec(sSqlFarmacias))
            {
                Error.GrabarError(leer, "CargarFarmacias()");
                General.msjError("Ocurrió un error al obtener la lista de farmacias.");
            }
            else
            {
                if (leer.Leer())
                {
                    gridUnidades.LlenarGrid(leer.DataSetClase);
                }                
            }
            
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
            cboEstados.SelectedIndex = 0; 

            Fg.IniciaControles(this, true); 
            gridUnidades.Limpiar();

            if (!DtGeneral.EsAdministrador)
            {
                 
            }
            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bRegresa = true;
            string sSql = "";
            string sFarmacia = "";
            int iValor = 0, iValorAux = 0;
            
            if (cnn.Abrir())
            {
                cnn.IniciarTransaccion();
                for (int i = 1; i <= gridUnidades.Rows; i++)
                {
                    sFarmacia = gridUnidades.GetValue(i, 1);
                    iValor = gridUnidades.GetValueInt(i, 3);
                    iValorAux = gridUnidades.GetValueInt(i, 4);                    

                    if (iValor != iValorAux) 
                    {
                        sSql = string.Format(" Exec spp_Mtto_CFG_ALMN_Unidades_Atendidas '{0}', '{1}', '{2}', '{3}' ",
                                            cboEstados.Data, cboAlmacenes.Data, sFarmacia, iValor); 
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
                    General.msjError("Ocurrió un error al guardar la información.");
                }
                cnn.Cerrar(); 
            }
            else
            {
                General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo."); 
            }
        }

        #endregion Botones
        
        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridUnidades.Limpiar(); 
            if (cboEstados.SelectedIndex != 0)
            {
                cboEstados.Enabled = false;
                CargarAlmacenes();
            } 
        }

        private void cboAlmacenes_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridUnidades.Limpiar();
            if (cboAlmacenes.SelectedIndex != 0)
            {
                cboAlmacenes.Enabled = false;
                CargarFarmacias();
            }
        }
    } 
}
