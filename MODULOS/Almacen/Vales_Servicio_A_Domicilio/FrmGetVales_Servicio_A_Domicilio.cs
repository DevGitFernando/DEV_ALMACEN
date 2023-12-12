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

using DllTransferenciaSoft.IntegrarInformacion;

namespace Almacen.Vales_Servicio_A_Domicilio
{
    public partial class FrmGetVales_Servicio_A_Domicilio : FrmBaseExt 
    {
        enum Cols
        {
            IdFarmacia = 1, Farmacia = 2, Url = 3, Procesar = 4, Status = 5
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente datosCliente;
        clsLeerWebExt leerWeb;
        clsLeer leer;
        clsAyudas ayuda;
        clsConsultas query;
        clsGrid grid;

        Color colorEjecutando = Color.DarkSeaGreen;
        Color colorEjecucionExito = Color.White;
        Color colorEjecucionError = Color.BurlyWood;

        int iBusquedasEnEjecucion = 0;
        //// int iTipoDePedidos = 1; 


        public FrmGetVales_Servicio_A_Domicilio()
        {
            InitializeComponent();

            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmGetVales_Servicio_A_Domicilio");

            leerWeb = new clsLeerWebExt(ref cnn, General.Url, General.ArchivoIni, datosCliente);
            leer = new clsLeer(ref cnn);
            ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            grid = new clsGrid(ref grdUnidades, this);
            // grid.EstiloGrid(eModoGrid.SeleccionSimple);
            ////grid.Ordenar(5);
            ////grid.Ordenar(6);
            ////grid.Ordenar(7);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name);

            lblConsultando.BackColor = colorEjecutando;
            lblFinExito.BackColor = colorEjecucionExito;
            lblFinError.BackColor = colorEjecucionError;

        }

        #region Botones 
        private void LimpiarPantalla()
        {
            Fg.IniciaControles();
            grid.Limpiar(false);

            CargarJurisdicciones(); 
            //// iTipoDePedidos = 1; 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            string sSql = "";

            grid.Limpiar(false); 
            if (cboJurisdicciones.Data == "0")
            {
                General.msjAviso("No ha seleccionado una jurisdicción válida, verifique.");
            }
            else 
            {
                sSql = string.Format(
                    " Select U.IdFarmacia, U.Farmacia, U.UrlFarmacia as Url, 0 as Recolectar, '' as Status \n " +
                    " From vw_Farmacias_Urls U (NoLock) \n" +
                    " Inner Join CatFarmacias F (NoLock) On ( U.IdEstado = F.IdEstado and U.IdFarmacia = F.IdFarmacia and F.Status = 'A' " + 
                    "   and F.IdTipoUnidad not In ( '005', '006' ) ) \n" +
                    " Where F.IdEstado = '{0}' and F.IdJurisdiccion = '{1}'", DtGeneral.EstadoConectado, cboJurisdicciones.Data);

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "btnEjecutar_Click");
                    General.msjError("Ocurrió un error al obtener la lista de Unidades.");
                }
                else
                {
                    if (leer.Leer())
                    {
                        grid.LlenarGrid(leer.DataSetClase);
                    }
                }
            }
        }

        private void btnActivarServicios_Click(object sender, EventArgs e)
        {
            IniciarProceso(); 
        }
        #endregion Botones

        #region FORM 
        private void FrmGetPedidosCEDIS_Load(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }
        #endregion FORM

        #region Cargar Informacion
        private void CargarJurisdicciones()
        {
            if (cboJurisdicciones.NumeroDeItems == 0)
            {
                cboJurisdicciones.Clear();
                cboJurisdicciones.Add("0","<< Seleccione >>");

                cboJurisdicciones.Add(query.Jurisdicciones(DtGeneral.EstadoConectado, "CargarJurisdicciones"), true, "IdJurisdiccion", "NombreJurisdiccion"); 
            } 

            cboJurisdicciones.SelectedIndex = 0; 
        } 
        #endregion Cargar Informacion

        #region Descargar Informacion 
        private void IniciarProceso()
        {            
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            btnActivarServicios.Enabled = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();

            for (int i = 1; i <= grid.Rows; i++)
            {
                grid.SetValue(i, (int)Cols.Status, " ");
                grid.ColorRenglon(i, colorEjecucionExito);

                if (grid.GetValueBool(i, (int)Cols.Procesar))
                {
                    Thread _workerThread = new Thread(this.Descargar_ServiciosADomicilio);
                    _workerThread.Name = grid.GetValue(i, (int)Cols.IdFarmacia) + grid.GetValue(i, (int)Cols.Farmacia);
                    _workerThread.Start(i); 
                }
            }
        }

        private void Descargar_ServiciosADomicilio(object Renglon)
        {
            int iRow = (int)Renglon;
            string sIdFarmacia = grid.GetValue(iRow, (int)Cols.IdFarmacia);
            string sUrl = grid.GetValue(iRow, (int)Cols.Url);
            string sValor = "-- " + DtGeneral.EstadoConectado + "-" + sIdFarmacia;
            string sSql = string.Format("Exec spp_Mtto_Vales_Servicio_A_Domicilio__ObtenerInformacion " + 
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @DiasRevision = '{3}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, sIdFarmacia, (int)nmDiasRevision.Value); 

            DataSet dtsDatos = new DataSet();
            clsLeer leerDatos = new clsLeer();

            try
            {
                iBusquedasEnEjecucion++;
                wsAlmacen.wsCnnCliente conexionWeb = new wsAlmacen.wsCnnCliente(); 
                conexionWeb.Timeout = 500000;
                conexionWeb.Url = sUrl;

                grid.ColorRenglon(iRow, colorEjecutando);
                grid.SetValue(iRow, (int)Cols.Status, "Procesando");  


                ////leerDatos.DataSetClase = conexionWeb.InformacionPedidos(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, sIdFarmacia, 1);
                leerDatos.DataSetClase = conexionWeb.ExecuteExt(datosCliente.DatosCliente(), DtGeneral.CfgIniPuntoDeVenta, sSql); 
                if (leerDatos.SeEncontraronErrores())
                {
                    grid.SetValue(iRow, (int)Cols.Status, "Error al descargar la lista de servicios a domicilio.");
                    grid.ColorRenglon(iRow, colorEjecucionError);
                }
                else 
                {
                    ////if (RegistrarPedido(iRow, leerDatos.DataSetClase))
                    ////{
                    ////}
                    Registrar_ServicioADomicilio(iRow, leerDatos.DataSetClase); 
                    grid.ColorRenglon(iRow, colorEjecucionExito); 
                }
            }
            catch 
            {
                grid.SetValue(iRow, (int)Cols.Status, "Error al descargar la lista de servicios a domicilio."); 
                grid.ColorRenglon(iRow, colorEjecucionError);
            }

            // Descontar Unidades del Procesamiento 
            iBusquedasEnEjecucion--; 
        } 
        #endregion Descargar Informacion 

        #region Registrar Informacion 
        private bool Registrar_ServicioADomicilio(int Renglon, DataSet Datos)
        {
            bool bRegresa = true;
            string sMsj = "Información descargada satisfactoriamente.";
            string sSql = "";
            clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            clsLeer leer = new clsLeer();
            clsLeer leerTablas = new clsLeer(); 
            clsLeer guardar = new clsLeer(ref cnn);


            leer.DataSetClase = Datos;
            leerTablas.DataSetClase = Datos; 
            if (leer.Registros == 0)
            {
                sMsj = "No se encontraron servicios a domicilio."; 
            }
            else 
            {
                if (!cnn.Abrir())
                {
                    sMsj = "Servicios a domicilio no descargados."; 
                }
                else
                {
                    cnn.IniciarTransaccion();

                    for (int i = 1; i <= leerTablas.Tablas; i++)
                    {
                        leer.DataTableClase = leerTablas.Tabla(i); 
                        while (leer.Leer())
                        {
                            sSql = leer.Campo("Resultado");
                            if (!guardar.Exec(sSql))
                            {
                                bRegresa = false;
                                break;
                            }
                        }
                    }

                    if (!bRegresa)
                    {
                        Error.GrabarError(guardar, "Registrar_ServicioADomicilio"); 
                        cnn.DeshacerTransaccion(); 
                    } 
                    else
                    {
                        cnn.CompletarTransaccion(); 
                    }

                    cnn.Cerrar();
                }
            }


            // Mostrar resultado de Proceso 
            grid.SetValue(Renglon, (int)Cols.Status, sMsj);

            return bRegresa; 
        }
        #endregion Registrar Informacion

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (iBusquedasEnEjecucion == 0)
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;

                btnActivarServicios.Enabled = true;
                btnEjecutar.Enabled = true;
                btnNuevo.Enabled = true;
            }
        }

        private void chkTodas_CheckedChanged(object sender, EventArgs e)
        {
            grid.SetValue((int)Cols.Procesar, chkTodas.Checked); 
        }

        private void btnIntegrarPaquetesDeDatos_Click(object sender, EventArgs e)
        {
            FrmIntegrarPaquetesDeDatos f = new FrmIntegrarPaquetesDeDatos();
            f.ShowDialog(this);
        }
    }
}
