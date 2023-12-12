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

using DllFarmaciaSoft; 
using DllTransferenciaSoft.wsCliente; 

namespace DllTransferenciaSoft.Auditoria
{
    public partial class FrmMovtosUnidades : FrmBaseExt 
    {
        private enum Cols
        {
            IdFarmacia = 1, Farmacia = 2, Url = 3, Procesar = 4, 
            RegistrosSvr = 5, FechaMenorSvr = 6, FechaMayorSvr = 7, 
            Registros = 8, FechaMenor = 9, FechaMayor = 10, 
            Diferencias = 11 
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid grid;
        clsConsultas query;
        clsLeerWebExt leerWeb;
        clsDatosCliente datosCliente; 


        Color colorEjecutando = Color.DarkSeaGreen;
        Color colorEjecucionExito = Color.White;
        Color colorEjecucionError = Color.BurlyWood;

        int iBusquedasEnEjecucion = 0;

        public FrmMovtosUnidades()
        {
            InitializeComponent();
            datosCliente = new clsDatosCliente(Transferencia.DatosApp, this.Name, "ObtenerInformacionUnidad");

            leerWeb = new clsLeerWebExt(General.Url, DtGeneral.CfgIniPuntoDeVenta, datosCliente); 
            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, Transferencia.DatosApp, this.Name); 

            grid = new clsGrid(ref grdExistencia, this);
            grid.EstiloGrid(eModoGrid.ModoRow);
            
            //lblConsultando.BackColor = colorEjecutando;
            //lblFinExito.BackColor = colorEjecucionExito;
            //lblFinError.BackColor = colorEjecucionError;
        }

        private void FrmMovtosUnidades_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        #region Botones  
        private void LimpiarPantalla()
        {
            grid.Limpiar();
            CargarEstados(); 
            cboEstados.SelectedIndex = 0;
            chkTodos.Checked = false; 

            if (Transferencia.ServicioEnEjecucion == TipoServicio.OficinaCentral)
            {
                cboEstados.Focus();  
            }
            else
            {
                cboEstados.Data = DtGeneral.EstadoConectado;
                cboEstados.Enabled = false; 
            } 
        }

        private void CargarEstados()
        {
            string sSql = " Select Distinct IdEstado, Estado From vw_Farmacias_Urls (NoLock) "; 
            cboEstados.Clear();
            cboEstados.Add();

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarEstados()");
                General.msjError("Ocurrió un error al Cargar la Lista de Estados."); 
            }
            else
            {
                if (leer.Leer())
                {
                    cboEstados.Add(leer.DataSetClase, true, "IdEstado", "Estado");
                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            IniciarConsulta(); 
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sSql = string.Format(" Set DateFormat YMD 	Select U.IdFarmacia, U.Farmacia, U.UrlFarmacia, 0 as Procesar, " + 
		         "      IsNull(M.NumMovimientos, -1) as RegistrosSvr,  " + 
		         "      IsNull(convert(varchar(16), M.FechaRegMenor, 120), '') as FechaRegMenorSvr,  " + 
		         "      IsNull(convert(varchar(16), M.FechaRegMayor, 120), '') as FechaRegMayorSvr,  " + 
		         "      0 as Registros, '' as FechaRegMenor, '' as FechaRegMayor     " + 
	             " From vw_Farmacias_Urls U (NoLock) " + 
                 " Left Join vw_Bitacora_Movimientos M (NoLock)  " + 
	             "	On ( U.IdEmpresa = M.IdEmpresa and U.IdEstado = M.IdEstado and U.IdFarmacia = M.IdFarmacia )  " + 
                 " Where U.StatusUrl = 'A' and U.IdEstado = '{0}' order by IdFarmacia ", cboEstados.Data); 
            
            grid.Limpiar();
            if (!leer.Exec(sSql))
            {
                General.msjError("Ocurrió un error al obtener la lista de Farmacias.");
            }
            else
            {
                grid.LlenarGrid(leer.DataSetClase); 
            }

        }

        private void IniciarConsulta()
        {
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();

            grid.ColorRenglon(colorEjecucionExito);
            grid.SetValue((int)Cols.Registros, "");
            grid.SetValue((int)Cols.FechaMenor, "");
            grid.SetValue((int)Cols.FechaMayor, "");
            grid.SetValue((int)Cols.Diferencias, "");


            for (int i = 1; i <= grid.Rows; i++)
            {
                if (grid.GetValueBool(i, (int)Cols.Procesar))
                {
                    Thread _workerThread = new Thread(this.ObtenerInformacionUnidad);
                    _workerThread.Name = grid.GetValue(i, (int)Cols.Farmacia);
                    _workerThread.Start(i);
                }
            }
        }

        private void ObtenerInformacionUnidad(object Renglon)
        {
            clsLeerWebExt leerLocal; 
            int iRow = (int)Renglon;
            // int iValor = -1; 
            string sIdFarmacia = grid.GetValue(iRow, (int)Cols.IdFarmacia);
            string sUrl = grid.GetValue(iRow, (int)Cols.Url);
            string sValor = "-- " + DtGeneral.EstadoConectado + "-" + sIdFarmacia;

            int iRegSvr = 0, iReg = 0; 
            bool bExito = false;
            // string sResultado = "Conectando";

            string sSql = string.Format(" Select count(*) as Registros, " +
                    " IsNull(convert(varchar(16), min(FechaRegistro), 120), '') as FechaRegMenor, " +
                    " IsNull(convert(varchar(16), max(FechaRegistro), 120), '') as FechaRegMayor " + 
	                " From MovtosInv_Enc (NoLock) " + 
	                " Where IdEstado = '{0}' and IdFarmacia = '{1}' ", cboEstados.Data, sIdFarmacia); 
            //grid.ColorRenglon(iRow, colorEjecutando);
            //grid.SetValue(iRow, 4, "0");
            iBusquedasEnEjecucion++;

            if (grid.GetValueBool(iRow, (int)Cols.Procesar))
            {
                grid.ColorRenglon(iRow, colorEjecutando); 
                leerLocal = new clsLeerWebExt(sUrl, DtGeneral.CfgIniPuntoDeVenta, datosCliente); 
                try
                {
                    if (!leerLocal.Exec(sSql))
                    {
                        bExito = false;
                    }
                    else 
                    {
                        bExito = true; 
                        leerLocal.Leer(); 
                        grid.SetValue(iRow, (int)Cols.Registros, leerLocal.CampoInt("Registros"));
                        grid.SetValue(iRow, (int)Cols.FechaMenor, leerLocal.Campo("FechaRegMenor"));
                        grid.SetValue(iRow, (int)Cols.FechaMayor, leerLocal.Campo("FechaRegMayor"));

                        iRegSvr = grid.GetValueInt(iRow, (int)Cols.RegistrosSvr);
                        iReg = grid.GetValueInt(iRow, (int)Cols.Registros); 
                        // RequisicionDeProducto = CantidadSolicitada > 0 ? true : false;
                        iRegSvr = iRegSvr <= 0 ? 0 : iRegSvr;

                        grid.SetValue(iRow, (int)Cols.Diferencias, (iReg - Math.Abs(iRegSvr))  ); 

                    }
                }
                catch { }

                if (bExito)
                {
                    // sResultado = "Exitó";
                    grid.ColorRenglon(iRow, colorEjecucionExito); 
                }
                else
                {
                    // sResultado = "Falló"; 
                    grid.ColorRenglon(iRow, colorEjecucionError); 
                }

                // grid.SetValue(iRow, 5, sResultado);
            }
            iBusquedasEnEjecucion--;
        }

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (iBusquedasEnEjecucion == 0)
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;

                btnEjecutar.Enabled = true;
                btnNuevo.Enabled = true;
            }
        }

        private void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            grid.SetValue(4, chkTodos.Checked);
            // myGrid.SetValue((int)Cols.Costo, 0);
        }
    }
}
