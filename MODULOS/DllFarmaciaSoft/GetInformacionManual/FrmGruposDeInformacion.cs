using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO; 
using System.Windows.Forms;

// Implementacion de hilos 
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FTP; 
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.wsOficinaCentral; 

namespace DllFarmaciaSoft.GetInformacionManual
{
    public partial class FrmGruposDeInformacion : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion); 
        clsLeerWebExt leerWeb;
        clsLeer leer; 
        clsGrid grid;
        clsDatosCliente datosCliente;
        clsConsultas query; 
        DataTable dtTablas;
        
        string sRutaIntegracion = "";
        string sRutaIntegracion_tmp = "";
        string sTablaIntegracion = "";
        bool bDireccionarServidorCentral = false;
        string sUrlCentral = ""; 

        Thread _workerThread; 
        DllFarmaciaSoft.wsOficinaCentral.wsCnnOficinaCentral rqCatalogos = new wsCnnOficinaCentral();

        enum Cols
        {
            Orden = 1, Catalogo = 2, Procesar = 3
        }

        public FrmGruposDeInformacion()
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent(); 

            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, ""); 
            // leerWeb = new clsLeerWebExt(DtGeneral.UrlServidorCentral, DtGeneral.CfgIniOficinaCentral, datosCliente); 

            leer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            sUrlCentral = query.Obtener_Url_ServidorCentral("FrmGruposDeInformacion"); 

            grid = new clsGrid(ref grdCatalogos, this);

            // Determinar el tipo de Modulo que lo solicita 
            if (DtGeneral.ModuloEnEjecucion == TipoModulo.Farmacia || DtGeneral.ModuloEnEjecucion == TipoModulo.Almacen || 
                DtGeneral.ModuloEnEjecucion == TipoModulo.FarmaciaUnidosis || DtGeneral.ModuloEnEjecucion == TipoModulo.AlmacenUnidosis)
            {
                sTablaIntegracion = " CFGC_ConfigurarIntegracion "; 
            }

            if (DtGeneral.ModuloEnEjecucion == TipoModulo.Regional)
            {
                sTablaIntegracion = " CFGS_ConfigurarIntegracion ";
            }

        }

        private void FrmGruposDeInformacion_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();
            ObtenerRutaIntegracion(); 
        }

        #region Botones 
        private void LimpiarPantalla()
        {
            FrmAvanze.Visible = false; 
            grid.Limpiar();

            chkTodas.Checked = false;
            rdoSvrCentral.Checked = false;
            toolStriplblResultado.Visible = false; 


            if ( DtGeneral.ModuloEnEjecucion == TipoModulo.Regional )
            {
                rdoSvrCentral.Enabled = false;
                rdoSvrRegional.Enabled = false; 
                rdoSvrCentral.Checked = true; 
            }

            if(DtGeneral.ModuloEnEjecucion == TipoModulo.Farmacia || DtGeneral.ModuloEnEjecucion == TipoModulo.Almacen ||
                DtGeneral.ModuloEnEjecucion == TipoModulo.FarmaciaUnidosis || DtGeneral.ModuloEnEjecucion == TipoModulo.AlmacenUnidosis)
            {
                if ( !DtGeneral.EsAdministrador ) 
                {
                    rdoSvrCentral.Enabled = false;
                    rdoSvrRegional.Enabled = false;
                    rdoSvrRegional.Checked = true;
                }

                if ( DtGeneral.EsServidorDeRedLocal ) 
                {
                    rdoSvrCentral.Enabled = true;
                    rdoSvrRegional.Enabled = true;
                    rdoSvrRegional.Checked = true;
                }
            } 

            if ( DtGeneral.EsEquipoDeDesarrollo )
            {
                rdoSvrCentral.Enabled = true;
                rdoSvrRegional.Enabled = true;
                rdoSvrCentral.Checked = true; 
            }

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            ////// Determinar el Origen de los datos 
            if(DtGeneral.ModuloEnEjecucion == TipoModulo.Farmacia || DtGeneral.ModuloEnEjecucion == TipoModulo.Almacen ||
                DtGeneral.ModuloEnEjecucion == TipoModulo.FarmaciaUnidosis || DtGeneral.ModuloEnEjecucion == TipoModulo.AlmacenUnidosis)
            {
                leerWeb = new clsLeerWebExt(DtGeneral.UrlServidorCentral, DtGeneral.CfgIniOficinaCentral, datosCliente);
                ////////leerWeb = new clsLeerWebExt("http://intermed.homeip.net/wsInt-OficinaCentral/wsOficinaCentral.asmx", DtGeneral.CfgIniOficinaCentral, datosCliente);

                if (rdoSvrRegional.Checked)
                {
                    leerWeb = new clsLeerWebExt(DtGeneral.UrlServidorRegional, DtGeneral.CfgIniOficinaCentral, datosCliente);
                }                
            }
            else
            {
                ////leerWeb = new clsLeerWebExt(DtGeneral.UrlServidorCentral_Regional, DtGeneral.CfgIniOficinaCentral, datosCliente);
                leerWeb = new clsLeerWebExt(General.Url, DtGeneral.CfgIniOficinaCentral, datosCliente);
            }

            ////if (rdoSvrRegional.Checked)
            ////{
            ////    leerWeb.UrlWebService = DtGeneral.UrlServidorRegional;
            ////}

            string sSql = "Select IdOrden, NombreTabla, 0 as Procesar " + 
                " From CFGS_EnvioCatalogos (NoLock) Where Status = 'A' Order By IdOrden ";

            dtTablas = new DataTable(); 
            if (!leerWeb.Exec(sSql))
            {
                General.msjError("Ocurrió un error al Solicitar la lista de Catalogos.");
                Error.GrabarError(leerWeb, "btnEjecutar_Click"); 
            }
            else
            {
                grid.Limpiar();
                if (leerWeb.Leer())
                {
                    grid.LlenarGrid(leerWeb.DataSetClase);
                    dtTablas = leerWeb.DataTableClase.Clone(); 
                }
            }
        }

        private void btnObtenerTransferencias_Click(object sender, EventArgs e)
        {
            ////byte[] btCatalogos = null;
            ////string sFile = ""; 

            ////DataSet dtsCatalogosSolicitados = ListaCatalogos();


            ////rqCatalogos.Url = DtGeneral.UrlServidorCentral;
            //////rqCatalogos.Url = "http://lapjesus/wsCompras/wsOficinaCentral.asmx"; 

            ////if (rdoSvrRegional.Checked)
            ////{
            ////    rqCatalogos.Url = DtGeneral.UrlServidorRegional;
            ////}

            ////try
            ////{
            ////    rqCatalogos.Timeout = (1000 * 60) * 2;
            ////    btCatalogos = rqCatalogos.Catalogos(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, "1", dtsCatalogosSolicitados);
            ////    // btCatalogos = catalogos.Catalogos("25", "0002", "1", dtsCatalogosSolicitados);
            ////    sFile = Application.StartupPath + @"\Test.SII"; 

            ////    Microsoft.VisualBasic.FileIO.FileSystem.WriteAllBytes(sFile, btCatalogos, false); 

            ////}
            ////catch (Exception ex1)
            ////{ 
            ////}

            if (General.msjConfirmar("Toda la información de los catálogos y configuraciones serán solicitadas, ¿ Desea continuar ?") == DialogResult.Yes)
            {
                toolStripBarraMenu.Enabled = false;
                FrameOrigenDatos.Enabled = false;
                //grdCatalogos.Visible = false; 
                grid.BloqueaColumna(true, (int)Cols.Procesar); 
                FrmAvanze.Visible = true;
                lblAvance.Visible = true;
                lblAvance.Text = "...";

                Thread.Sleep(500);
                _workerThread = new Thread(this.ReplicarInformacion);
                _workerThread.Name = "ReplicarInformacion";
                _workerThread.Start();
            }
        }

        private void btnDescargarImagenes_Click(object sender, EventArgs e)
        {
            string sUrlConexion = "";

            if (DtGeneral.ModuloEnEjecucion == TipoModulo.Farmacia || DtGeneral.ModuloEnEjecucion == TipoModulo.Almacen
                || DtGeneral.ModuloEnEjecucion == TipoModulo.FarmaciaUnidosis || DtGeneral.ModuloEnEjecucion == TipoModulo.AlmacenUnidosis)
            {
                sUrlConexion = DtGeneral.UrlServidorCentral;
                if (rdoSvrRegional.Checked)
                {
                    sUrlConexion = DtGeneral.UrlServidorRegional;
                }
            }
            else
            {
                ////sUrlConexion = DtGeneral.UrlServidorCentral_Regional;
                sUrlConexion = General.Url;
            }

            FrmDescargarImagenesDeProductos f = new FrmDescargarImagenesDeProductos(sUrlConexion);
            f.ShowDialog(); 
        }

        private DataSet ListaCatalogos()
        {
            // dtTablas
            DataSet dtsRetorno = new DataSet(); 
            dtTablas.Rows.Clear(); 

            for (int i = 1; i <= grid.Rows; i++)
            {
                if (grid.GetValueBool(i, (int)Cols.Procesar))
                {
                    object[] obj = { grid.GetValue(i, 1), grid.GetValue(i, (int)Cols.Catalogo), 0 };
                    dtTablas.Rows.Add(obj); 
                }
            }

            dtsRetorno.Tables.Add(dtTablas.Copy());
            return dtsRetorno; 
        } 
        #endregion Botones

        private void ObtenerRutaIntegracion()
        {
            string sSql = string.Format(" Select RutaArchivosRecibidos, RutaArchivosIntegrados, bFechaTerminacion, FechaTerminacion, Tiempo, TipoTiempo " +
                " From {0} (NoLock) ", sTablaIntegracion);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ObtenerRutaIntegracion()");
                General.msjError("Ocurrió un error al obtener la configuración inicial del proceso"); 
            }
            else
            {
                if (leer.Leer())
                {
                    sRutaIntegracion = leer.Campo("RutaArchivosRecibidos") + @"\\";
                    sRutaIntegracion_tmp = sRutaIntegracion + @"\\temp";
                    GenerarDirectorios();
                }
            }

            // return sSql; 
        }

        private void GenerarDirectorios()
        {
            if (!Directory.Exists(sRutaIntegracion))
            {
                Directory.CreateDirectory(sRutaIntegracion);
            }

            if (!Directory.Exists(sRutaIntegracion_tmp))
            {
                Directory.CreateDirectory(sRutaIntegracion_tmp);
            }
        }

        private void ReplicarInformacion()
        {            
            byte[] btCatalogos = null;
            string sFile = "";
            string sTipo = "";
            string sUrlConexion = "";
            bool bSoloEstadoEspecificado = !chkCatalogoGeneral.Checked;

            DataSet dtsCatalogosSolicitados = ListaCatalogos();
            DataSet dtsResultado = new DataSet();
            clsLeer leerResultado = new clsLeer(); 

            ////// DtGeneral.UrlServidorCentral = "http://intermed.homeip.net/wsInt-OficinaCentral/wsOficinaCentral.asmx";  
            ////// Determinar el Origen de los datos 
            if (DtGeneral.ModuloEnEjecucion == TipoModulo.Farmacia || DtGeneral.ModuloEnEjecucion == TipoModulo.Almacen
                || DtGeneral.ModuloEnEjecucion == TipoModulo.FarmaciaUnidosis || DtGeneral.ModuloEnEjecucion == TipoModulo.AlmacenUnidosis)
            {
                sUrlConexion = DtGeneral.UrlServidorCentral; 
                if (rdoSvrRegional.Checked)
                {
                    sUrlConexion = DtGeneral.UrlServidorRegional;
                }
            }
            else
            {
                ////sUrlConexion = DtGeneral.UrlServidorCentral_Regional;
                sUrlConexion = General.Url; 
            }

            sTipo = "2";
            if(DtGeneral.ModuloEnEjecucion == TipoModulo.Farmacia || DtGeneral.ModuloEnEjecucion == TipoModulo.Almacen
                || DtGeneral.ModuloEnEjecucion == TipoModulo.FarmaciaUnidosis || DtGeneral.ModuloEnEjecucion == TipoModulo.AlmacenUnidosis)
            {
                sTipo = "1";
            }
            //////leerWeb = new clsLeerWebExt(sUrlCentral, DtGeneral.CfgIniOficinaCentral, datosCliente); 


            try
            {
                /////sUrlConexion = "http://LapJesus:8181/wsOficinaCentral/wsOficinaCentral.asmx";
                rqCatalogos.Url = sUrlConexion;

                rqCatalogos.Timeout = (1000 * 60) * 30;
                ////btCatalogos = rqCatalogos.Catalogos(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sTipo, dtsCatalogosSolicitados);
                dtsResultado = rqCatalogos.InformacionCatalogos(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, bSoloEstadoEspecificado, sTipo, dtsCatalogosSolicitados);  
                leerResultado.DataSetClase = dtsResultado;

                if (leerResultado.ExisteTabla("DatosDescarga"))
                {
                    DescargarArchivo(leerResultado.DataSetClase);
                }
                else
                {
                    toolStriplblResultado.Text = "No se encontro información disponible para descargar."; 
                }
                
            }
            catch (Exception ex1)
            {
                ////General.msjError("Ocurrió un error al descargar los catalogos."); 
                toolStriplblResultado.Text = "Ocurrió un error al descargar la información."; 
                Error.GrabarError(ex1, "ReplicarInformacion"); 
            }

            toolStriplblResultado.Visible = true; 
            toolStripBarraMenu.Enabled = true;
            FrameOrigenDatos.Enabled = true;
            //grdCatalogos.Visible = true;
            grid.BloqueaColumna(false, (int)Cols.Procesar); 
            FrmAvanze.Visible = false;
        }

        private void FTP_OnProgressDownload( FtpProgressEventArgs sender )
        {
            //lblAvance.Text = string.Format("{0}", (int)sender.Porcentaje);

            lblAvance.Text = string.Format("Descargado : {0} de 100.00 %", sender.Porcentaje.ToString("##0.#0"));
        }

        private void FTP_OnProgressUpload( FtpProgressEventArgs sender )
        {
        }

        private void FTP_OnCompleteDownload( FtpProgressEventArgs sender )
        {
            lblAvance.Text = string.Format("Descargar completa {0}", sender.Porcentaje.ToString("##0.#0"));
        }

        private void FTP_OnCompleteUpload( FtpProgressEventArgs sender )
        {
        }

        private void FTP_OnError( FtpProgressEventArgs sender )
        {
            lblAvance.Text = sender.Error.Message;
        }

        private bool DescargarArchivo(DataSet Conexion)
        {
            bool bRegresa = false;
            clsCriptografo Cryp = new clsCriptografo();
            clsFTP FTP = new clsFTP(); 

            string sServidorFTP = "";
            string sUsuarioFTP = "";
            string sPassword_FTP = "";
            string sFile_FTP = ""; 
            string sUrlDescarga = sRutaIntegracion_tmp;

            ////lblAvance.Visible = true;
            ////Application.DoEvents();


            clsLeer leerFile = new clsLeer();
            leerFile.DataSetClase = Conexion;

            if (leerFile.Leer())
            {
                GenerarDirectorios();

                sServidorFTP = Cryp.Desencriptar(leerFile.Campo("Campo1"));
                sUsuarioFTP = Cryp.Desencriptar(leerFile.Campo("Campo2"));
                sPassword_FTP = Cryp.PasswordDesencriptar(leerFile.Campo("Campo3")).Substring(0);
                sFile_FTP = Cryp.Desencriptar(leerFile.Campo("Campo4"));

                FTP.Host = sServidorFTP;
                FTP.Usuario = sUsuarioFTP;
                FTP.Password = sPassword_FTP;


                FTP = new clsFTP(sServidorFTP, sUsuarioFTP, sPassword_FTP, false, true);
                FTP.PrepararConexion();


                FTP.OnProgressDownload += FTP_OnProgressDownload;
                FTP.OnError += FTP_OnError;
                FTP.OnCompleadDownload += FTP_OnCompleteDownload;

                //FTP.SubirArchivo(sDestino, sPath, sFileDestino); 
                bRegresa = FTP.BajarArchivo(sFile_FTP, Path.Combine(sRutaIntegracion_tmp, sFile_FTP));

                if (bRegresa)
                {
                    try
                    {
                        File.Copy(Path.Combine(sRutaIntegracion_tmp, sFile_FTP), Path.Combine(sRutaIntegracion, sFile_FTP));
                        bRegresa = true;

                        File.Delete(Path.Combine(sRutaIntegracion_tmp, sFile_FTP));

                        try
                        {
                            FTP.BorrarArchivo(sFile_FTP); 
                        }
                        catch 
                        { 
                        }
                    }
                    catch 
                    {
                        bRegresa = false; 
                    }
                }

                //General.msjUser("Descarga de información completada satisfactoriamente.");
            }

            if (bRegresa)
            {
                toolStriplblResultado.Text = "Información descargada satisfactoriamente.";
            }
            else
            {
                toolStriplblResultado.Text = "No fue posible descargar la información solicitada."; 
            }

            return bRegresa; 
        }


        private string NombreArchivo()
        {
            string sRegresa = "";
            string sMarcaTiempo = ""; 
            DateTime dtFechaProceso = General.FechaSistemaObtener();

            sMarcaTiempo = General.FechaYMD(General.FechaSistema).Replace("/", "");
            sMarcaTiempo = sMarcaTiempo.Replace("-", "");
            sMarcaTiempo += "-" + General.FechaSistemaHora.Substring(0, 5).Replace(":", "");

            sRegresa = sRutaIntegracion + @"\OC00SC-" + DtGeneral.EstadoConectado + "0001-05-" + sMarcaTiempo + ".SII";

            return sRegresa; 
        }

        private void chkTodas_CheckedChanged(object sender, EventArgs e)
        {
            grid.SetValue(3, chkTodas.Checked); 
        }

        private void FrmGruposDeInformacion_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F12:
                    bDireccionarServidorCentral = !bDireccionarServidorCentral;
                    FrameOrigenDatos.Text = !bDireccionarServidorCentral?"Origen de datos":"Origen de   datos"; 
                    break; 

                default:
                    break; 
            }
        }
    }
}
