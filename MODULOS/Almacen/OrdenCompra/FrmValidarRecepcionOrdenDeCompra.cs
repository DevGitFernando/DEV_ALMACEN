using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Conexiones;
using DllFarmaciaSoft.wsFarmaciaSoftGn;

namespace Almacen.OrdenCompra
{
    internal partial class FrmValidarRecepcionOrdenDeCompra : FrmBaseExt
    {
        #region Declaracion de variables
        //basGenerales Fg = new basGenerales();
        // DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoIniManager Ini; // = new clsIniManager();
        DllFarmaciaSoft.wsFarmaciaSoftGn.wsConexion conexionWeb = null; 

        Thread thrReporte; 

        // bool bConexionWeb = false;
        // bool bConectando = true;
        public bool bExisteFileConfig = true;
        public bool bConexionEstablecida = false;

        clsImprimir myRpt;
        clsDatosCliente datosCliente;
        clsConexionClienteUnidad datosConexionUnidad;
        string sUrl = ""; 
        bool bImpresionWeb = false;
        
        bool bNoSeEncontraronRecepciones = false;
        bool bCanceladoPorError = false;
        public bool bCanceladoPorUsuario = false; 
        bool bReporteRemoto = false;
        bool bMostrarInterface = true;


        string sIdEmpresa = "";
        string sIdEstado = "";
        string sIdFarmacia = "";
        string sFolio = "";
        string sMensaje_Error = "";
        string sMensaje_Error_Usuario = "";
        string sMensaje_Error_Usuario_Default = "Se encontraron registros de ingreso de la Orden de Compra, no es posible realizar la cancelación.";
        clsLeer Resultado;
        bool bBorrar = false;

        #endregion 

        public FrmValidarRecepcionOrdenDeCompra()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            datosCliente = new clsDatosCliente(GnCompras.DatosApp, this.Name, ""); 

            Resultado = new clsLeer(); 
        }

        #region Funciones y Procedimientos Privados 
        private void FrmValidarRecepcionOrdenDeCompra_Load(object sender, EventArgs e)
        {
            ////////this.Height = 95;
            ////////this.Width = 260;
            ////////FrameProceso.Left = 6;
            ////////FrameProceso.Top = 20;
            ////////FrameProceso.Height = 52;
            ////////FrameProceso.Width = 244;
            ////////this.Height = 105;

            if(bBorrar)
            {
                this.Text = "Eliminando recepción(es) de Orden de Compra";
            }

            tmIniciarProceso.Interval = 1500;
            tmIniciarProceso.Enabled = true;
            tmIniciarProceso.Start(); 
        }

        private void tmIniciarProceso_Tick(object sender, EventArgs e)
        {
            tmIniciarProceso.Stop();
            tmIniciarProceso.Enabled = false;

            thrReporte = new Thread(this.GenerarReporte_Thread);
            thrReporte.Name = "ValidandoRecepciones_OC";
            thrReporte.Start();
        }

        private void tmRevisarGeneracion_Tick(object sender, EventArgs e)
        {
            //tmRevisarGeneracion
            try
            {
                bCanceladoPorError = true; 
                thrReporte.Abort();
            }
            catch
            {
            }
            finally 
            {
                bNoSeEncontraronRecepciones = false;
                this.Hide(); 
            }
        } 
        #endregion Funciones y Procedimientos Privados 

        public bool ValidarRecepcion(string Url, string Empresa, string Estado, string IdFarmacia, string Folio)
        {
            return this.ValidarRecepcion(Url, Empresa, Estado, IdFarmacia, Folio, sMensaje_Error_Usuario_Default); 
        }

        public bool ValidarRecepcion(string Url, string Empresa, string Estado, string IdFarmacia, string Folio, string Mensaje_Error_Usuario)
        {
            sUrl = Url;
            sIdEmpresa = Empresa;
            sIdEstado = Estado;
            sIdFarmacia = IdFarmacia;
            sFolio = Folio;
            sMensaje_Error_Usuario = Mensaje_Error_Usuario;
            this.ShowDialog();

            if (bCanceladoPorUsuario)
            {
                bNoSeEncontraronRecepciones = false;
                General.msjAviso("Validación cancelada por el usuario.");
            }
            else
            {
                if (!bNoSeEncontraronRecepciones)
                {
                    General.msjError(sMensaje_Error); 
                }
            }

            return bNoSeEncontraronRecepciones; 
        }


        public bool EliminarRegistros(string Url, string Empresa, string Estado, string IdFarmacia, string Folio)
        {
            return this.EliminarRegistros(Url, Empresa, Estado, IdFarmacia, Folio, sMensaje_Error_Usuario_Default);
        }

        public bool EliminarRegistros(string Url, string Empresa, string Estado, string IdFarmacia, string Folio, string Mensaje_Error_Usuario)
        {
            sUrl = Url;
            sIdEmpresa = Empresa;
            sIdEstado = Estado;
            sIdFarmacia = IdFarmacia;
            sFolio = Folio;
            sMensaje_Error_Usuario = Mensaje_Error_Usuario;
            bBorrar = true;

            this.ShowDialog();

            if (bCanceladoPorUsuario)
            {
                bNoSeEncontraronRecepciones = false;
                General.msjAviso("Validación cancelada por el usuario.");
            }
            else
            {
                if (!bNoSeEncontraronRecepciones)
                {
                    General.msjError(sMensaje_Error);
                }
            }

            return bNoSeEncontraronRecepciones;
        }

        public void GenerarReporte_Thread()
        {
            bool bRegresa = true;
            byte[] btReporte = null;

            string sSql = string.Format(" Select FolioOrdenCompraReferencia, Folio, convert(varchar(10),FechaRegistro,120) As FechaRegistro " +
                    " From vw_OrdenesDeComprasEnc (Nolock) " +
                    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' " +
                    " And FolioOrdenCompraReferencia = '{3}' \n " ,
                    Fg.PonCeros(sIdEmpresa, 3), Fg.PonCeros(sIdEstado, 2), Fg.PonCeros(sIdFarmacia, 4), Fg.PonCeros(sFolio, 8));


            if (bBorrar)
            {
                sSql = string.Format(
                    "Delete COM_OCEN_OrdenesCompra_CodigosEAN_Det Where IdEmpresa = '{0}' And IdEstado = '{1}' And FolioOrden = '{2}' " +
                    "Delete COM_OCEN_OrdenesCompra_Claves_Enc Where IdEmpresa = '{0}' And IdEstado = '{1}' And FolioOrden = '{2}'",
                     Fg.PonCeros(sIdEmpresa, 3), Fg.PonCeros(sIdEstado, 2), Fg.PonCeros(sFolio, 8));
            }

            try 
            {
                bNoSeEncontraronRecepciones = false; 
                tmRevisarGeneracion.Enabled = true;
                tmRevisarGeneracion.Interval = ((500) * (60)) * 1;
                tmRevisarGeneracion.Start();

                conexionWeb = new DllFarmaciaSoft.wsFarmaciaSoftGn.wsConexion();
                conexionWeb.Url = sUrl;
                conexionWeb.Timeout = 5000000;

                //// Resultado.DataSetClase = conexionWeb.ExecuteExt(datosCliente.DatosCliente(), DtGeneral.CfgIniPuntoDeVenta, sSql);                 
                clsLeerWebExt myWeb = new clsLeerWebExt(sUrl, DtGeneral.CfgIniPuntoDeVenta, datosCliente);
                if (!myWeb.Exec(sSql)) 
                {
                    sMensaje_Error = "No fue posible verificar si la Orden de Compra cuenta con registros de ingreso en el CEDIS.";  
                }
                else 
                {
                    if (!myWeb.Leer())
                    {
                        bNoSeEncontraronRecepciones = true;
                    }
                    else
                    {
                        bNoSeEncontraronRecepciones = false;
                        sMensaje_Error = sMensaje_Error_Usuario; 
                    }
                }

                tmRevisarGeneracion.Stop();
                tmRevisarGeneracion.Enabled = false;

            }
            catch (Exception ex)
            {
                bNoSeEncontraronRecepciones = false;
                sMensaje_Error = "No fue posible verificar si la Orden de Compra cuenta con registros de ingreso en el CEDIS.";  
                Error.LogError("GenerarReporte_Thread .... " + ex.Message);
            }

            if (!bCanceladoPorError)
            {
                ////bNoSeEncontraronRecepciones = bRegresa; 
                this.Hide();
            }
        }

        private void FrmValidarRecepcionOrdenDeCompra_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
            }
            catch { } 
        }

        private void btnDetener_Click(object sender, EventArgs e)
        {
            tmRevisarGeneracion.Stop();
            tmRevisarGeneracion.Enabled = false;

            bCanceladoPorError = false;
            bCanceladoPorUsuario = true;

            CancelarReporte();
            CancelarReporte();

            this.Hide();
        }

        private void CancelarReporte()
        {
            //tmRevisarGeneracion
            try
            {
                try
                {
                    conexionWeb = null;
                    thrReporte.Abort();
                    conexionWeb = null;
                }
                catch (Exception ex)
                {
                    ex.Source = ex.Source; 
                }

                thrReporte.Abort();                 
            }
            catch (Exception ex)
            {
                ex.Source = ex.Source; 
            }  
        }
    }
}