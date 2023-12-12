using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using System.Diagnostics;

// Implementacion de hilos 
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Criptografia;
using SC_SolutionsSystem.FTP;
using SC_SolutionsSystem.SistemaOperativo;
using SC_SolutionsSystem.SQL;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales; 

using DllFarmaciaSoft;

using DllTransferenciaSoft;
using DllTransferenciaSoft.IntegrarInformacion;
using DllTransferenciaSoft.ObtenerInformacion;
using DllTransferenciaSoft.Zip;

namespace DllTransferenciaSoft.ExportarBD 
{
    internal partial class FrmExportarBD : FrmBaseExt
    {
        private enum Cols 
        { 
            NombreTabla = 0, Tabla = 1, Procesado = 2 
        }; 

        clsSQL svrSql;      
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerCat;
        clsLeer leerMigrado;
        TipoTiempo eTiempoRespaldos = TipoTiempo.Horas;
        // clsGrid myGrid;
        clsListView lst; 

        string sRutaRespaldos = General.UnidadSO + @":\\RespaldosBD\"; 
        string sPrefijoRespaldo = "";
        string sOrigen = "";
        string sDestino = "";
        string sFileDestino = "";
        string sRespaldoBD = " Net_CFGC_Respaldos";

        // bool bRespaldoConfigurado = false;

        public bool SeGeneroRespaldo = false;
        public string sRutaExportarInformacionBD = ""; 
        private bool bMostrarInterface = false;
        Thread thrProceso;

        ////public FrmExportarBD()
        ////{
        ////    Inicializar(); 
        ////}

        public FrmExportarBD(bool MostrarDetalles)
        {
            this.bMostrarInterface = MostrarDetalles;
            Inicializar();
        }

        public void Inicializar()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            leer = new clsLeer(ref cnn);
            leerCat = new clsLeer(ref cnn);
            leerMigrado = new clsLeer(ref cnn);

            lst = new clsListView(lstwDetalles);

            if (!bMostrarInterface)
            {
                this.Text = "Generar respaldo de base de datos"; 
                lstwDetalles.Visible = false; 

                this.Width = 557;
                this.Height = 210;

                FrameBD.Top = 20;
                FrameBD.Left = 32;

                grpCatalogosProcesar.Text = "Proceso"; 
                grpCatalogosProcesar.Width = 529;
                grpCatalogosProcesar.Height = 145;

                btnNuevo.Enabled = false;
                // btnProcesarTablas.Enabled = false;
                // threadIniciarProceso(); 
            }
            else
            {
                this.Width = 557; 
                this.Height = 365;

                FrameBD.Top = 93;
                FrameBD.Left = 32;  

                grpCatalogosProcesar.Width = 529; 
                grpCatalogosProcesar.Height = 298;

                lstwDetalles.Width = 508;
                lstwDetalles.Height = 270; 
            }
        }


        private void FrmServicioSinConexion_Load(object sender, EventArgs e)
        {
            ConfigRespaldosBD();
            CargarTablasEnvio();
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FrameBD.Visible = false;
            btnProcesarTablas.Enabled = true; 
            lst.LimpiarItems();
            lst.CargarDatos(leerCat.DataSetClase); 
        }

        private void btnProcesarTablas_Click(object sender, EventArgs e)
        {
            if (GenerarRespaldo())
            {
                threadIniciarProceso(); 
            }
        }  
        #endregion Botones

        #region Funciones y Procedimientos Privados  
        private void threadIniciarProceso()
        {
            btnNuevo.Enabled = false;
            btnProcesarTablas.Enabled = false;
            FrameBD.Visible = true;
            lblBD.Text = "Procesando Base de Datos";

            this.Refresh();
            Thread.Sleep(500); 

            thrProceso = new Thread(this.IniciarProceso);
            thrProceso.Name = "Procesando información";
            thrProceso.Start(); 
        }

        private void IniciarProceso()
        {
            if (GenerarRespaldoCompleto())
            {
                SeGeneroRespaldo = true;
                if (bMostrarInterface)
                {
                    btnProcesarTablas.Enabled = !ProcesarTablas();
                }
            }

            if (!bMostrarInterface)
            {
                this.Close(); 
            }
            else
            {
                btnNuevo.Enabled = true;
            }
        }

        private bool ProcesarTablas()
        {
            bool bRegresa = true; 

            if (cnn.Abrir())
            {
                cnn.IniciarTransaccion();

                bRegresa = MarcarTablas(); 

                if (!bRegresa)
                {
                    Error.GrabarError(leer, "MarcarTablas()"); 
                    cnn.DeshacerTransaccion();
                    General.msjError("Ocurrió un error al procesar la información"); 
                }
                else
                {
                    cnn.CompletarTransaccion();
                    General.msjUser("Información procesada satisfactoriamente."); 
                }
                cnn.Cerrar();
            }
            else
            {
                General.msjAviso(General.MsjErrorAbrirConexion); 
            }

            return bRegresa; 
        }

        private bool MarcarTablas()
        {
            bool bRegresa = true;
            string sValor = ""; //  lstwTablasMigrar.Items[i].Text; 
            string sSql = ""; 

            for (int i = 0; i <= lstwDetalles.Items.Count - 1; i++)
            {
                ActualizarStatusBD(i, "Actualizando"); 
                sValor = lstwDetalles.Items[i].Text; 
                sSql = string.Format("Update {0} Set Actualizado = 1 Where Actualizado in ( 0, 2 ) ", sValor);

                ////if (!leer.Exec(sSql))
                ////{
                ////    bRegresa = false;
                ////    ActualizarStatusBD(i, "Error al actualizar");
                ////    break; 
                ////}
                ////else 
                {
                    ActualizarStatusBD(i, "Procesado");
                }
            }

            return bRegresa; 
        }

        private void ActualizarStatusBD(int Renglon, string Mensaje)
        {
            // Thread.Sleep(500); 
            lstwDetalles.Items[Renglon].SubItems[(int)Cols.Procesado].Text = Mensaje;
            Thread.Sleep(100);  
        }

        private bool GenerarRespaldo()
        {
            bool bRegresa = false;

            FolderBrowserDialog f = new FolderBrowserDialog(); 
            f.Description = "Directorio donde se generará el respaldo.";
            // f.RootFolder = Application.StartupPath; 

            if (f.ShowDialog() == DialogResult.OK)
            {
                sRutaRespaldos = f.SelectedPath;
                sRutaExportarInformacionBD = sRutaRespaldos + @"\"; 
                bRegresa = true; // GenerarRespaldoCompleto();  
            }


            return bRegresa; 
        }

        private void EliminarTemporales_DatosCTES() 
        {
            clsLeer leerEliminar = new clsLeer(ref cnn); 

            bool bRegresa = false;
            string sSql = " Select Name as Tabla From Sysobjects (NoLock) Where ( Name like 'tmp%' or Name like 'Rpt_Admon%' ) ";

            if (leer.Exec(sSql))
            {
                while (leer.Leer())
                {
                    sSql = string.Format("Drop Table {0} ", leer.Campo("Tabla"));
                    leerEliminar.Exec(sSql); 
                }
            } 

            // Eliminar las tablas de los CTES 
            sSql = "Exec spp_Mtto_BorrarTablasRpt_Clientes \n "; 
            bRegresa = leer.Exec(sSql); 


        }

        private bool GenerarRespaldoCompleto()
        {
            bool bResultado = false;
            svrSql = new clsSQL(General.DatosConexion, sPrefijoRespaldo, sRutaRespaldos); 

            ////FrameBD.Visible = true;
            ////lblBD.Text = "Procesando Base de Datos"; 
            ////this.Refresh();
            ////Thread.Sleep(200); 

            lblBD.Text = "Eliminando temporales"; 
            EliminarTemporales_DatosCTES(); 

            lblBD.Text = "Reduciendo Log de Transacciones"; 
            svrSql.LogBd.ReducirLog();

            lblBD.Text = "Reduciendo Base de datos"; 
            svrSql.LogBd.ReducirBD();

            lblBD.Text = "Generando respaldo de Base de datos"; 
            if (svrSql.BackUp.Respaldar())
            {
                sFileDestino = svrSql.BackUp.NombreArchivo + ".SII";
                sOrigen = sRutaRespaldos + "\\" + svrSql.BackUp.NombreArchivo + ".bak";
                sDestino = sRutaRespaldos + "\\" + sFileDestino;

                ZipUtil zip = new ZipUtil();
                zip.Type = Transferencia.Modulo + "Install";
                zip.Type = Transferencia.Modulo + "BackUp";

                if (!bMostrarInterface)
                {
                    bResultado = true;
                }
                else 
                {
                    try
                    {
                        lblBD.Text = "Comprimiendo respaldo";
                        zip.Comprimir(sOrigen, sDestino, true);
                        File.Delete(sOrigen);
                        bResultado = true;
                    }
                    catch { }
                }
            }
            
            lblBD.Text = "Proceso terminado";
            this.Refresh(); 
            Thread.Sleep(1000);

            FrameBD.Visible = false;
            Thread.Sleep(100); 

            if (!bResultado)
            {
                General.msjAviso("No fue posible exportar la información, intente de nuevo por favor."); 
            }

            return bResultado;
        }

        private bool ConfigRespaldosBD()
        {
            bool bRegresa = false; 
            // bRespaldoConfigurado = false;

            string sSql = string.Format("Select * From {0} (NoLock) ", sRespaldoBD);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "ConfigRespaldosBD()");
            }
            else
            {
                if (leer.Leer())
                {
                    bRegresa = true;
                    // bRespaldoConfigurado = true;
                    sRutaRespaldos = leer.Campo("RutaDeRespaldos"); 
                    eTiempoRespaldos = (TipoTiempo)leer.CampoInt("TipoTiempo");
                    int iTiempo = leer.CampoInt("Tiempo");

                    // Crear el Directorio de Respaldos 
                    if (!Directory.Exists(sRutaRespaldos))
                    {
                        Directory.CreateDirectory(sRutaRespaldos);
                    }
                } 
            }

            return bRegresa;
        }
        
        private bool CargarTablasEnvio()
        {
            bool bResultado = true;
            string sTabla = " CFGC_EnvioDetalles ";
            string sSql = string.Format(" Select NombreTabla As Catalogo, NombreTabla As CatalogoAux, '' As Procesado From {0}(NoLock) Where Status = 'A' Order By IdOrden ", sTabla);

            lst.LimpiarItems(); 
            if (!leerCat.Exec(sSql))
            {
                bResultado = false;
                Error.GrabarError(leerCat, "ExistenTablasEnvio()");
            }
            else
            {
                leerCat.Leer(); 
                lst.CargarDatos(leerCat.DataSetClase); 
            }

            return bResultado;
        }
        #endregion Funciones y Procedimientos Privados

        private void lstwDetalles_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            ////if (e.ColumnIndex == (int)Cols.NombreTabla)
            ////{
            ////    e.Cancel = true;
            ////    e.NewWidth = 0; 
            ////}
        }
    }
}
