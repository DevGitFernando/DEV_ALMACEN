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
using SC_SolutionsSystem.SQL;

using DllFarmaciaSoft; 
using DllTransferenciaSoft.wsCliente; 

namespace DllTransferenciaSoft.Servicio
{
    public partial class FrmConfigurarConexionSvrLnk : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid grid;
        clsConsultas query; 

        Color colorEjecutando = Color.DarkSeaGreen;
        Color colorEjecucionExito = Color.White;
        Color colorEjecucionError = Color.BurlyWood;

        int iBusquedasEnEjecucion = 0;

        clsCriptografo crypto = new clsCriptografo();
        clsServerLinked svrlnk;

        private enum Cols
        {
            Ninguna = 0,
            IdFarmacia = 1, Farmacia = 2, UrlFarmacia = 3, SvrLnk = 4, Host = 5, NombreBD = 6, Usuario = 7, Password = 8
        }

        public FrmConfigurarConexionSvrLnk()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, Transferencia.DatosApp, this.Name);

            svrlnk = new clsServerLinked(General.DatosConexion);

            grid = new clsGrid(ref grdExistencia, this);
            //grid.EstiloGrid(eModoGrid.SeleccionSimple);
            
            //lblConsultando.BackColor = colorEjecutando;
            //lblFinExito.BackColor = colorEjecucionExito;
            //lblFinError.BackColor = colorEjecucionError;
        }

        private void FrmSvrRemoto_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        #region Botones  
        private void LimpiarPantalla()
        {
            grid.Limpiar();
            CargarEstados(); 
            cboEstados.SelectedIndex = 0;
            btnGuardar.Enabled = false;
            ////if (Transferencia.ServicioEnEjecucion == TipoServicio.OficinaCentral)
            ////{
            ////    cboEstados.Focus();  
            ////}
            ////else
            ////{
            ////    cboEstados.Data = DtGeneral.EstadoConectado;
            ////    cboEstados.Enabled = false; 
            ////} 
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
                if ( leer.Leer() )
                    cboEstados.Add(leer.DataSetClase, true, "IdEstado", "Estado");
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            IniciarServicios(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bContinua = true;
            if (cnn.Abrir())
            {
                cnn.IniciarTransaccion();

                bContinua = GuardaInformacion();

                if (!bContinua)
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(leer, "btnGuardar_Click");
                    //General.msjError(sError);
                }
                else
                {
                    cnn.CompletarTransaccion();
                    //General.msjUser(sExito);
                    btnNuevo_Click(null, null);
                }
                cnn.Cerrar();
            }
        }

        private bool GuardaInformacion()
        {
            bool bRegresa = true;
            string sSql = "";
            string sFarmacia = "", sSvrLnk = "", sHost = "", sNombreBD = "", sUsuario = "", sPass = "", sPassword = "";

            for (int i = 1; i <= grid.Rows; i++)
            {
                sFarmacia = grid.GetValue(i, (int)Cols.IdFarmacia);
                sSvrLnk = grid.GetValue(i, (int)Cols.SvrLnk);
                sHost = grid.GetValue(i, (int)Cols.Host);
                sNombreBD = grid.GetValue(i, (int)Cols.NombreBD);
                sUsuario = grid.GetValue(i, (int)Cols.Usuario);
                sPass = grid.GetValue(i, (int)Cols.Password);
                sPassword = crypto.PasswordEncriptar(sPass);

                if (sFarmacia != "")
                {
                    sSql = string.Format(" Exec spp_Mtto_CFGS_ConfigurarConexiones_SvrLnk '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}' ",
                                        cboEstados.Data, sFarmacia, sSvrLnk, sHost, sNombreBD, sUsuario, sPassword);

                    if (sHost != "")
                    {
                        svrlnk.RegistrarServidor(sHost, sSvrLnk, sUsuario, sPass);
                    }

                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }

            return bRegresa;
        }
        #endregion Botones

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iRenglon = 0;
            string sPass = "";
            if (cboEstados.SelectedIndex != 0)
            {

                string sSql = string.Format(" Select F.IdFarmacia, F.Farmacia, F.UrlFarmacia, " +
                                            " IsNull(C.SvrLnk, 'svr'+F.IdEstado+F.IdFarmacia) As Servidor, " +
                                            " C.Host, C.NombreBD, C.Usuario, C.Password " +
                                            " From vw_Farmacias_Urls F (NoLock) " +
                                            " Left Join CFGS_ConfigurarConexiones_SvrLnk C (Nolock) " +
                                                " On( F.IdEstado = C.IdEstado And F.IdFarmacia = C.IdFarmacia ) " +
                                            " Where F.IdEstado = '{0}' ", cboEstados.Data);

                grid.Limpiar();
                if (!leer.Exec(sSql))
                {
                    General.msjError("Ocurrió un error al obtener la lista de Farmacias.");
                }
                else
                {
                    iRenglon = 1;
                    while (leer.Leer())
                    {
                        grid.Rows = grid.Rows + 1;
                        grid.SetValue(iRenglon, (int)Cols.IdFarmacia, leer.Campo("IdFarmacia"));
                        grid.SetValue(iRenglon, (int)Cols.Farmacia, leer.Campo("Farmacia"));
                        grid.SetValue(iRenglon, (int)Cols.UrlFarmacia, leer.Campo("UrlFarmacia"));
                        grid.SetValue(iRenglon, (int)Cols.SvrLnk, leer.Campo("Servidor"));
                        grid.SetValue(iRenglon, (int)Cols.Host, leer.Campo("Host"));
                        grid.SetValue(iRenglon, (int)Cols.NombreBD, leer.Campo("NombreBD"));
                        grid.SetValue(iRenglon, (int)Cols.Usuario, leer.Campo("Usuario"));
                        sPass = crypto.PasswordDesencriptar(leer.Campo("Password"));
                        grid.SetValue(iRenglon, (int)Cols.Password, sPass);
                        //grid.LlenarGrid(leer.DataSetClase);
                        iRenglon++;
                    }
                }
            }

        }

        private void IniciarServicios()
        {
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            cboEstados.Enabled = false;
            btnGuardar.Enabled = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();

            grid.ColorRenglon(colorEjecucionExito); 
            ////grid.SetValue(5, ""); 

            for (int i = 1; i <= grid.Rows; i++)
            {
                Thread _workerThread = new Thread(this.IniciarServicioFarmacia);
                _workerThread.Name = grid.GetValue(i, 2);
                _workerThread.Start(i);
            }
        }

        private void IniciarServicioFarmacia(object Renglon)
        {
            int iRow = (int)Renglon;
            // int iValor = -1; 
            string sIdFarmacia = grid.GetValue(iRow, 1);
            string sUrl = grid.GetValue(iRow, 3);
            string sValor = "-- " + DtGeneral.EstadoConectado + "-" + sIdFarmacia;
            
            bool bExito = false;
            // string sResultado = "Conectando";           

            DataSet dtsConexiones = null;
            clsDatosConexion DatosConexion = new clsDatosConexion();           

            //grid.ColorRenglon(iRow, colorEjecutando);
            //grid.SetValue(iRow, 4, "0");
            iBusquedasEnEjecucion++;


            //if (grid.GetValueBool(iRow, 4))
            {
                //grid.SetValue(iRow, 5, sResultado);
                DllTransferenciaSoft.wsCliente.wsCnnCliente cliente = new DllTransferenciaSoft.wsCliente.wsCnnCliente();
                grid.ColorRenglon(iRow, colorEjecutando);

                try
                {
                    cliente.Url = sUrl;
                    bExito = cliente.TestConection();
                    dtsConexiones = cliente.ConexionEx(DtGeneral.CfgIniPuntoDeVenta);
                    
                    // iValor = cliente.IniciarServicio("");
                }
                catch { }

                if (bExito)
                {
                    // sResultado = "Exitó";
                    grid.ColorRenglon(iRow, colorEjecucionExito);                    
                    DatosConexion = new clsDatosConexion(dtsConexiones);
                    grid.SetValue(iRow, (int)Cols.Host, DatosConexion.Servidor);
                    grid.SetValue(iRow, (int)Cols.NombreBD, DatosConexion.BaseDeDatos);
                    grid.SetValue(iRow, (int)Cols.Usuario, DatosConexion.Usuario);                    
                    grid.SetValue(iRow, (int)Cols.Password, DatosConexion.Password);
                }
                else
                {
                    // sResultado = "Falló"; 
                    grid.ColorRenglon(iRow, colorEjecucionError); 
                }

                //grid.SetValue(iRow, 5, sResultado);                
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
                cboEstados.Enabled = true;
                btnGuardar.Enabled = true;
            }
        }

        private void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            //grid.SetValue(4, chkTodos.Checked);
            // myGrid.SetValue((int)Cols.Costo, 0);
        }        
    }
}
