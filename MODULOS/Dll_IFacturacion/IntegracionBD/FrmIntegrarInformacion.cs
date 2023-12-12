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
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.ExportarDatos;
using DllFarmaciaSoft; 

namespace Dll_IFacturacion.IntegracionBD
{
    public partial class FrmIntegrarInformacion : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer, leerBD;
        clsGrid myGrid;
        clsDatosCliente DatosCliente;
        clsAyudas Ayudas;
        clsConsultas Consultas;

        Color colorEjecutando = Color.DarkSeaGreen;
        Color colorEjecucionExito = Color.White;
        Color colorEjecucionError = Color.BurlyWood;

        int iBusquedasEnEjecucion = 0;

        private enum Cols
        {
            Ninguna = 0,
            IdEnvio = 1, IdOrden = 2, NombreTabla = 3, Integrar = 4, Status = 5
            
        }

        public FrmIntegrarInformacion()
        {
            InitializeComponent();

            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayudas = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "");
            myLeer = new clsLeer(ref ConexionLocal);
            leerBD = new clsLeer(ref cnn);
            
            Error = new clsGrabarError(DtGeneral.Modulo, DtGeneral.Version, this.Name);

            myGrid = new clsGrid(ref grdTablas, this);
            myGrid.BackColorColsBlk = Color.White;
            myGrid.EstiloDeGrid = eModoGrid.ModoRow;
            //grdTablas.EditModeReplace = true;          
            
        }

        private void FrmIntegrarInformacion_Load(object sender, EventArgs e)
        {
            CargarBasesDeDatos();
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnIntegrar_Click(object sender, EventArgs e)
        {
            IniciarIntegracion();
        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            myGrid.Limpiar(true);

            lblIntegrando.BackColor = Color.DarkSeaGreen;
            lblFinExito.BackColor = Color.White;
            lblFinError.BackColor = Color.BurlyWood;


            cboBD.SelectedIndex = 0;
            cboBD.Enabled = true;
            cboBD.Focus();
        }

        private void CargarBasesDeDatos()
        {
            string sBdSistemas = " 'master', 'model', 'msdb', 'tempdb' ";
            string sSql = string.Format(" Select Name as DataName, Name " +
                " From sys.databases " +
                " where name not in ( {0} ) order by Name ", sBdSistemas);

            cboBD.Clear();
            cboBD.Add();

            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "CargarBasesDeDatos");
                General.msjError("Ocurrió un error al obtener las Base de Datos.");
            }
            else
            {
                cboBD.Add(myLeer.DataSetClase, true, "DataName", "Name");
            }

            cboBD.SelectedIndex = 0;
            
        }

        private void CargarTablasConfiguracion()
        {
            string sSql = string.Format(" Select IdEnvio, IdOrden, NombreTabla, 0 as Integrar, '' as Status " +
                " From CFGSC_EnvioFact_Estado " +
                " order by IdOrden ");
                      
            myGrid.Limpiar(false);

            if (!leerBD.Exec(sSql))
            {
                Error.GrabarError(leerBD, "CargarTablasConfiguracion");
                General.msjError("Ocurrió un error al obtener las tablas para integración.");
            }
            else
            {
                if (leerBD.Leer())
                {
                    myGrid.LlenarGrid(leerBD.DataSetClase);
                }
            }
        }
        #endregion Funciones

        #region Eventos
        private void cboBD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboBD.SelectedIndex != 0)
            {
                cboBD.Enabled = false;

                cnn = new clsConexionSQL();                
                cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
                cnn.DatosConexion.BaseDeDatos = cboBD.Data;
                cnn.DatosConexion.Usuario = General.DatosConexion.Usuario;
                cnn.DatosConexion.Password = General.DatosConexion.Password;
                cnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
                cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
                cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;

                leerBD = new clsLeer(ref cnn);

                CargarTablasConfiguracion();
            }
        }
        #endregion Eventos

        #region Integracion
        private void IniciarIntegracion()
        {
            btnNuevo.Enabled = false;
            btnIntegrar.Enabled = false;            
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();            
                        
            Thread _workerThread = new Thread(this.IntegrarTablas);
            _workerThread.Name = "Integrando Tablas";
            _workerThread.Start();          
            
        }

        private void IntegrarTablas()
        {            
            string sNombreTabla = "";

            string sSql = "";

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                myGrid.SetValue(i, (int)Cols.Status, " ");

                if (myGrid.GetValueBool(i, (int)Cols.Integrar))
                {
                    sNombreTabla = myGrid.GetValue(i, (int)Cols.NombreTabla);

                    sSql = string.Format("Declare @sSQL varchar(8000) \n");
                    sSql += string.Format("Exec spp_INF_SEND_Detalles '{0}', '{1}', '{2}', '{3}', @sSQL output, {4}, {5} ",
                                cboBD.Data, General.DatosConexion.BaseDeDatos, cboBD.Data, sNombreTabla, 1, 0);
                    sSql += string.Format("Exec(@sSQL) \n");


                    myGrid.ColorRenglon(i, colorEjecutando);
                    myGrid.SetValue(i, (int)Cols.Status, "Integrando");
                    iBusquedasEnEjecucion++;

                    if (!leerBD.Exec(sSql))
                    {
                        General.msjError("Ocurrió un error al integrar la tabla : " + sNombreTabla);
                        Error.GrabarError(leerBD, "IntegrarTablas()");
                        myGrid.SetValue(i, (int)Cols.Status, "Ejecución con Error");
                        myGrid.ColorRenglon(i, colorEjecucionError);
                        break;
                        //iBusquedasEnEjecucion = 0;
                    }
                    else
                    {
                        myGrid.SetValue(i, (int)Cols.Status, "Integración con Exito");
                        myGrid.ColorRenglon(i, colorEjecucionExito);
                    }
                }
                iBusquedasEnEjecucion--;
            }

            iBusquedasEnEjecucion = 0;
            
        }

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (iBusquedasEnEjecucion == 0)
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;
                
                btnIntegrar.Enabled = true;
                btnNuevo.Enabled = true;
            }
        }
        #endregion Integracion

        private void chkTodas_CheckedChanged(object sender, EventArgs e)
        {
            myGrid.SetValue((int)Cols.Integrar, chkTodas.Checked);
        }
                
    }
}
