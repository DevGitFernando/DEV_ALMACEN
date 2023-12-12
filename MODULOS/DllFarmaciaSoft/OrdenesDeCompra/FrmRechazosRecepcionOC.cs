using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;


namespace DllFarmaciaSoft.OrdenesDeCompra
{
    public partial class FrmRechazosRecepcionOC : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leer2;
               
        clsAyudas Ayuda;
        clsConsultas Consultas;

        DataSet dtsOrdenEnc = new DataSet();
        
        clsGrid Grid;

        string sEmpresa = DtGeneral.EmpresaConectada; 
        string sEstado = DtGeneral.EstadoConectado;
        string sOrigen = GnFarmacia.UnidadComprasCentrales;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sIdPersonal = DtGeneral.IdPersonal;

        string sFolioRechazo = "";

        wsFarmacia.wsCnnCliente conexionWeb;
        wsFarmacia.wsCnnCliente RechazosWeb;
        clsLeerWebExt leerWeb;
        clsDatosConexion DatosDeConexion;
        clsDatosCliente DatosCliente;

        string sUrlServidorCentral = "";
        bool bServidorCentral_EnLinea = false;

        private enum Cols
        {
            Ninguna = 0, IdRechazo = 1, Rechazo = 2, Marcar = 3
        }

        public FrmRechazosRecepcionOC()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            leer2 = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);

            conexionWeb = new wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            RechazosWeb = new wsFarmacia.wsCnnCliente();

            DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "");            
            leerWeb = new clsLeerWebExt(General.Url, DtGeneral.CfgIniOficinaCentral, DatosCliente);
            
            Grid = new clsGrid(ref grdRechazos, this);
            Grid.BackColorColsBlk = Color.White;
            grdRechazos.EditModeReplace = true;
            Grid.AjustarAnchoColumnasAutomatico = true; 

            Error = new clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);
            
        }

        private void FrmRechazosRecepcionOC_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bContinua = true;

            if (ValidaDatos())
            {
                if (cnn.Abrir())
                {

                    cnn.IniciarTransaccion();

                    bContinua = GuardaEncabezado();

                    if (!bContinua)
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "GrabarInformacion");
                        General.msjError("Ocurrio un error al guardar la información.");
                    }
                    else
                    {
                        cnn.CompletarTransaccion();
                        General.msjUser("La informacion del Folio : " + sFolioRechazo + " se guardo satisfactoriamente.");
                        ImprimirFolio();
                        Insertar_FolioRechazoWeb();
                        LimpiaPantalla();
                    }

                    cnn.Cerrar();
                }
                else
                {
                    General.msjAviso("No fue posible establecer conexión con el servidor. Intente de nuevo.");
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirFolio();
        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            dtpFechaRegistro.Enabled = false;
            dtpFechaResurtido.Enabled = false;
            IniciaBotones(false, false, false);
            rdoNotaCredito.Enabled = true;
            rdoNotaCredito.Checked = false;
            rdoResurtido.Enabled = true;
            rdoResurtido.Checked = false;
            Cargar_Motivos_Rechazos();
            txtFolio.Focus();
        }        

        private bool Cargar_Motivos_Rechazos()
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format(" Select IdRechazo, Descripcion, 0 as Marcar From COM_Cat_Rechazos Order By IdRechazo ");

            Grid.Limpiar(false);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "");
                General.msjError("Ocurrio un error al obtener los datos de los motivos de rechazo.");
            }
            else
            {
                if (leer.Leer())
                {
                    Grid.LlenarGrid(leer.DataSetClase, false, false);
                }
                else
                {
                    bRegresa = false;
                    General.msjUser("No se encontro información de los motivos de Rechazo.");
                }
            }

            return bRegresa;
        }

        private void IniciaBotones(bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
        }
        #endregion Funciones

        #region Evento_Orden_Compra
        private void txtOrden_Validating(object sender, CancelEventArgs e)
        {
            if (txtOrden.Text.Trim() != "")
            {
                txtOrden.Text = Fg.PonCeros(txtOrden.Text, 8);
                leer.DataSetClase = Consultas.OrdenesCompras_Ingresadas(sEmpresa, sEstado, sOrigen, sFarmacia, txtOrden.Text.Trim(), "txtOrden_Validating");

                if (leer.Leer())
                {
                    txtOrden.Text = leer.Campo("FolioOrdenCompraReferencia");
                    txtOrden.Enabled = false;
                    lblIdProv.Text = leer.Campo("IdProveedor");
                    lblProveedor.Text = leer.Campo("Proveedor");
                }
                else
                {
                    IniciaBotones(false, false, false);
                }
                
            }
        }
        #endregion Evento_Orden_Compra

        #region Evento_Folio_Rechazo
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            string sSql = "";

            if (txtFolio.Text.Trim() == "")
            {
                txtFolio.Text = "*";
                txtFolio.Enabled = false;
                IniciaBotones(true, false, false);
            }
            else
            {
                sSql = string.Format(" Select * From COM_Adt_Rechazos_Enc Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioRechazo = '{3}' ",
                                      sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text, 8));

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "txtFolio_Validating");
                    General.msjError(" Ocurrio un error al consultar el folio de rechazo. ");
                }
                else
                {
                    if (leer.Leer())
                    {
                        IniciaBotones(false, false, true);
                        txtFolio.Text = leer.Campo("FolioRechazo");
                        txtFolio.Enabled = false;
                        txtOrden.Text = leer.Campo("FolioOrden");
                        txtRecibeRechazo.Text = leer.Campo("NombreRecibeRechazo");
                        txtRecibeRechazo.Enabled = false;
                        txtObservaciones.Text = leer.Campo("Observaciones");
                        txtObservaciones.Enabled = false;
                        dtpFechaRegistro.Value = leer.CampoFecha("FechaRegistro");
                        dtpFechaResurtido.Value = leer.CampoFecha("FechaResurtido");                        

                        if (leer.CampoInt("TipoProceso") == 1)
                        {
                            rdoResurtido.Checked = true;
                        }

                        if (leer.CampoInt("TipoProceso") == 2)
                        {
                            rdoNotaCredito.Checked = true;
                        }

                        rdoResurtido.Enabled = false;
                        rdoNotaCredito.Enabled = false;
                        dtpFechaResurtido.Enabled = false;

                        txtOrden_Validating(null, null);

                        CargaDet_Rechazo();
                    }
                    else
                    {
                        General.msjUser(" No se encontro información del folio capturado. ");
                    }
                }
            }
        }

        private void CargaDet_Rechazo()
        {
            string sSql = "";

            sSql = string.Format(" Select IdRechazo, Rechazo, 1 as Marcar From vw_COM_Adt_Rechazos_Det Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
                                      sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text, 8));

            Grid.Limpiar(false);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargaDet_Rechazo");
                General.msjError(" Ocurrio un error al consultar el detalle de rechazo. ");
            }
            else
            {
                if (leer.Leer())
                {
                    Grid.LlenarGrid(leer.DataSetClase, false, false);
                }
            }
        }
        #endregion Evento_Folio_Rechazo

        #region Guardar_Informacion
        private bool GuardaEncabezado()
        {
            bool bRegresa = true;
            string sSql = "";
            int iOpcion = 1, iTipoProceso = 0;

            if (rdoResurtido.Checked)
            {
                iTipoProceso = 1;
            }

            if (rdoNotaCredito.Checked)
            {
                iTipoProceso = 2;
            }

            sSql = string.Format(" Exec spp_Mtto_COM_Adt_Rechazos_Enc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', {8}, '{9}', {10} ",
                                 sEmpresa, sEstado, sFarmacia, txtFolio.Text, txtOrden.Text, sIdPersonal, txtRecibeRechazo.Text, txtObservaciones.Text, 
                                 iTipoProceso, General.FechaYMD(dtpFechaResurtido.Value, "-"), iOpcion);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                if (leer.Leer())
                {
                    sFolioRechazo = leer.Campo("Folio");

                    bRegresa = GuardaDet();
                }
            }

            return bRegresa;
        }

        private bool GuardaDet()
        {
            bool bRegresa = true;
            string sSql = "", sIdRechazo = "";
            bool bCheck = false;

            for (int i = 1; i <= Grid.Rows; i++)
            {
                bCheck = false;

                bCheck = Grid.GetValueBool(i, (int)Cols.Marcar);
                sIdRechazo = Grid.GetValue(i, (int)Cols.IdRechazo);

                if (bCheck)
                {
                    sSql = string.Format(" Exec spp_Mtto_COM_Adt_Rechazos_Det '{0}', '{1}', '{2}', '{3}', '{4}' ",
                                 sEmpresa, sEstado, sFarmacia, sFolioRechazo, sIdRechazo);

                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }

            return bRegresa;
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtFolio.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("Clave de folio incorrecta. Verifique.");
                txtFolio.Focus();
            }

            if (bRegresa && txtOrden.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("Clave de orden incorrecta. Verifique.");
                txtOrden.Focus();
            }

            if (bRegresa && txtRecibeRechazo.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("No ha capturado quién recibe el rechazo. Verifique.");
                txtRecibeRechazo.Focus();
            }

            if (bRegresa && txtObservaciones.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("No ha capturado las observaciones. Verifique.");
                txtObservaciones.Focus();
            }

            if (bRegresa && !rdoResurtido.Checked && !rdoNotaCredito.Checked)
            {
                bRegresa = false;
                General.msjAviso("No ha seleccionado el tipo de proceso a seguir. Verifique.");
            }

            if (bRegresa && !ValidaMarca())
            {
                bRegresa = false;
                General.msjAviso("Favor de marcar al menos un motivo de rechazo. Verifique.");
                
            }

            return bRegresa;
        }

        private bool ValidaMarca()
        {
            bool bRegresa = true, bCheck = false;

            for (int i = 1; i <= Grid.Rows; i++)
            {
                bCheck = false;

                bCheck = Grid.GetValueBool(i, (int)Cols.Marcar);

                if (bCheck)
                {
                    break;
                }
            }

            if (!bCheck)
            {
                bRegresa = false;
            }

            return bRegresa;
        }
        #endregion Guardar_Informacion

        #region Eventos_TipoProceso
        private void rdoResurtido_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoResurtido.Checked)
            {
                dtpFechaResurtido.Enabled = true;
            }
        }

        private void rdoNotaCredito_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoNotaCredito.Checked)
            {
                dtpFechaResurtido.Enabled = false;
            }
        }
        #endregion Eventos_TipoProceso        
        
        #region Guardar_Informacion_Web
        private bool GetUrl_ServidorCentral()
        {
            try
            {
                sUrlServidorCentral = DtGeneral.UrlServidorCentral;
                RechazosWeb.Url = sUrlServidorCentral;
                bServidorCentral_EnLinea = true;
            }
            catch
            {
                bServidorCentral_EnLinea = false;
            }

            return bServidorCentral_EnLinea;
        }

        private bool validarDatosDeConexion()
        {
            bool bRegresa = false;

            try
            {
                leerWeb = new clsLeerWebExt(sUrlServidorCentral, DtGeneral.CfgIniOficinaCentral, DatosCliente);

                conexionWeb.Url = sUrlServidorCentral;
                DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneral.CfgIniOficinaCentral));

                //DatosDeConexion.Servidor = sHost;
                bRegresa = true;
            }
            catch (Exception ex1)
            {
                Error.GrabarError(leer.DatosConexion, ex1, "validarDatosDeConexion()");
                General.msjAviso("No fue posible establecer conexión con la Unidad, intente de nuevo.");                
            }

            return bRegresa;
        }

        private DataSet Execute_Informacion(string Cadena)
        {
            DataSet dts = new DataSet();                   

            if (validarDatosDeConexion())
            {
                //clsConexionSQL cnnRemota = new clsConexionSQL(DatosDeConexion);
                //cnnRemota.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;
                //cnnRemota.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;

                clsLeer leerDatos = new clsLeer(ref cnn);

                leerDatos.DataSetClase = conexionWeb.ExecuteExt(dts, DtGeneral.CfgIniOficinaCentral, Cadena);                
                
                //leerDatos.Exec(Cadena);
                dts = leerDatos.DataSetClase;
            }

            return dts;
        }

        private string ObtenerFolioRechazo()
        {
            string sFiltro = "", sSql = "", sExecute = "";
            clsLeer Enc = new clsLeer();
            clsLeer Det = new clsLeer();

            //sFolioRechazo = txtFolio.Text;

            sFiltro = string.Format(" Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioRechazo = '{3}' ",
                                     sEmpresa, sEstado, sFarmacia, sFolioRechazo);

            sSql = string.Format(" Exec spp_CFG_ObtenerDatos '{0}', [ {1} ], '0' \n", "COM_Adt_Rechazos_Enc", sFiltro);

            sSql += string.Format(" Exec spp_CFG_ObtenerDatos '{0}', [ {1} ], '0' ", "COM_Adt_Rechazos_Det", sFiltro);

            if (!leer2.Exec(sSql))
            {
                Error.GrabarError(leer2, "ObtenerFolioRechazo");
                General.msjError("Ocurrio un error al obtener los datos del folio de rechazo.");
            }
            else
            {
                if (leer2.Leer())
                {
                    Enc.DataTableClase = leer2.Tabla(1).Copy();
                    Det.DataTableClase = leer2.Tabla(2).Copy();

                    if (Enc.Leer())
                    {
                        sExecute = " Set DateFormat YMD \n " + Enc.Campo("Resultado") + "\n";
                    }

                    while (Det.Leer())
                    {
                        sExecute += Det.Campo("Resultado") + "\n";
                    }                    
                }
            }

            return sExecute;
        }

        private void Insertar_FolioRechazoWeb()
        {
            clsLeer execute = new clsLeer(ref cnn);
            string sExecute = "";

            if (GetUrl_ServidorCentral())
            {
                sExecute = ObtenerFolioRechazo();

                if (sExecute.Trim() != "")
                {
                    execute.DataSetClase = Execute_Informacion(sExecute);

                    if (execute.SeEncontraronErrores())
                    {
                        Error.GrabarError(execute, sExecute, "Insertar_FolioRechazo", "");
                        General.msjError("No se pudo realizar la inserccion del folio de rechazo en el servidor central.");
                    }
                }
            }

        }
        #endregion Guardar_Informacion_Web

        private void ImprimirFolio()
        {
            bool bRegresa = false;

            
            DatosCliente.Funcion = "ImprimirFolio()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;

            myRpt.RutaReporte = GnFarmacia.RutaReportes;
            myRpt.NombreReporte = "COM_RechazosRecepcion.rpt";

            myRpt.Add("IdEmpresa", sEmpresa);
            myRpt.Add("IdEstado", sEstado);
            myRpt.Add("IdFarmacia", sFarmacia);
            myRpt.Add("Folio", txtFolio.Text);

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            if (bRegresa)
            {
                btnNuevo_Click(null, null);
            }
            else
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
            
        }
                
    }
}
