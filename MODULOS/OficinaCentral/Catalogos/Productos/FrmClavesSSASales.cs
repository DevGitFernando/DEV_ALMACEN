using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Reportes;      

using DllFarmaciaSoft;
using OficinaCentral;

namespace OficinaCentral.Catalogos
{
    public partial class FrmClavesSSASales : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsAyudas Ayuda = new clsAyudas();
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;

        clsDatosCliente DatosCliente;
        wsOficinaCentral.wsCnnOficinaCentral conexionWeb;

        //Para Auditoria
        clsAuditoria auditoria;

        public FrmClavesSSASales()
        {
            InitializeComponent();
            ConexionLocal.SetConnectionString();

            myLeer = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.Modulo, GnOficinaCentral.Version, this.Name);

            DatosCliente = new clsDatosCliente(GnOficinaCentral.DatosApp, this.Name, "");
            conexionWeb = new OficinaCentral.wsOficinaCentral.wsCnnOficinaCentral();
            conexionWeb.Url = General.Url;

            auditoria = new clsAuditoria(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal,
                                        DtGeneral.IdSesion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
        }

        private void FrmClavesSSASales_Load(object sender, EventArgs e)
        {
            CargarGruposTerapeuticos();
            CargarTiposCatalogo();
            CargaTipoDeClave();
            btnNuevo_Click(null, null);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            lblCancelado.Visible = false;
            LlenaPresentaciones();
            cboTipoClave.SelectedIndex = 0;
            txtContenido.Text = "";
            txtId.Focus();
        }

        #region Buscar Clave SSA Sales

        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            myLeer = new clsLeer(ref ConexionLocal);

            string sCadena = "";

            if (txtId.Text.Trim() == "")
            {
                txtId.Enabled = false;
                txtId.Text = "*";
            }
            else
            {
                myLeer.DataSetClase = Consultas.ClavesSSA_Sales(txtId.Text.Trim(), false, "txtId_Validating");

                sCadena = Consultas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (myLeer.Leer())
                {
                    CargaDatos();
                }
                else
                {
                    btnNuevo_Click(null, null);
                }
            }
        }

        private void CargaDatos()
        {
            //Se hace de esta manera para la ayuda.
            txtId.Text = myLeer.Campo("IdClaveSSA_Sal");
            
            txtClaveSSA.Text = myLeer.Campo("ClaveSSA_Aux");
            txtClaveSSABase.Text = myLeer.Campo("ClaveSSA_Base");

            txtDescripcion.Text = myLeer.Campo("Descripcion");
            txtDescripcionCorta.Text = myLeer.Campo("DescripcionCortaClave"); 
            cboGrupoTerapeutico.Data = myLeer.Campo("IdGrupoTerapeutico");
            cboTipoCatalogo.Data = myLeer.Campo("TipoCatalogo");
            cboPresentaciones.Data = myLeer.Campo("IdPresentacion");
            cboTipoClave.Data = myLeer.Campo("TipoDeClave");
            txtContenido.Text = myLeer.Campo("ContenidoPaquete");

            chkMedicamento.Checked = myLeer.CampoBool("EsControlado");
            chkAntibiotico.Checked = myLeer.CampoBool("EsAntibiotico");

            txtId.Enabled = false;

            if (myLeer.Campo("Status") == "C")
            {
                lblCancelado.Visible = true;
                txtId.Enabled = false;
                txtClaveSSA.Enabled = false;
                txtDescripcion.Enabled = false;
                txtDescripcionCorta.Enabled = false;
                cboGrupoTerapeutico.Enabled = false;
                cboTipoCatalogo.Enabled = false;
                txtContenido.Enabled = false;
            }

        }

        #endregion Buscar Clave SSA Sales

        #region Guardar/Actualizar Clave SSA Sales

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "", sCadena = "";
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion

            int EsControlado = 0, EsAntibiotico = 0;

            if (chkMedicamento.Checked)
            {
                EsControlado = 1;
            }

            if (chkAntibiotico.Checked)
            {
                EsAntibiotico = 1;
            }

            if (ValidaDatos())
            {
                if(!ConexionLocal.Abrir())
                {
                    General.msjErrorAlAbrirConexion(); 
                }
                else 
                {
                    ConexionLocal.IniciarTransaccion();

                    sSql = string.Format("Exec spp_Mtto_CatClavesSSA_Sales \n" +
                        "\t@IdClaveSSA_Sal = '{0}', @ClaveSSA = '{1}', @Descripcion = '{2}', @DescripcionCortaClave = '{3}',\n" +
                        "\t@IdGrupoTerapeutico = '{4}', @TipoCatalogo = '{5}', @IdPresentacion = '{6}', @EsControlado = '{7}', @EsAntibiotico = '{8}',\n" +
                        "\t@ContenidoPaquete = '{9}', @ClaveSSA_Base = '{10}', @IdTipoProducto = '{11}', \n" +
                        "\t@IdEstado = '{12}', @IdFarmacia = '{13}', @IdPersonal = '{14}', @iOpcion = '{15}' ",
                        txtId.Text.Trim(), txtClaveSSA.Text, txtDescripcion.Text.Trim(), txtDescripcionCorta.Text.Trim(), 
                        cboGrupoTerapeutico.Data, cboTipoCatalogo.Data, cboPresentaciones.Data, EsControlado, EsAntibiotico,  
                        txtContenido.Text, txtClaveSSABase.Text, cboTipoClave.Data, 
                        DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal, iOpcion);

                    sCadena = sSql.Replace("'", "\"");
                    auditoria.GuardarAud_MovtosUni("*", sCadena);

                    if (myLeer.Exec(sSql))
                    {
                        if (myLeer.Leer())
                            sMensaje = String.Format("{0}", myLeer.Campo("Mensaje"));

                        ConexionLocal.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        ConexionLocal.DeshacerTransaccion();
                        Error.GrabarError(myLeer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al guardar la información.");
                        //btnNuevo_Click(null, null);

                    }

                    ConexionLocal.Cerrar();
                }
            }
        }

        #endregion Guardar/Actualizar Clave SSA Sales

        #region Eliminar Clave SSA Sales

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "", sCadena = "";
            int iOpcion = 2; //La opcion 2 indica que es una cancelacion
            string message = "¿ Desea eliminar la Clave Interna seleccionada ?";

            //Se verifica que no este cancelada.
            if(lblCancelado.Visible != false)
            {
                General.msjUser("Clave previamiente cancelada");
            }
            else 
            {
                txtId_Validating(txtId.Text, null);//Se manda llamar este evento para validar que exista la Sal.
                if (txtDescripcion.Text.Trim() != "") //Si no esta vacio, significa que si existe.
                {
                    if (General.msjCancelar(message) == DialogResult.Yes)
                    {
                        if(!ConexionLocal.Abrir())
                        {
                            General.msjErrorAlAbrirConexion();
                        }
                        else 
                        {
                            ConexionLocal.IniciarTransaccion();

                            sSql = String.Format("Exec spp_Mtto_CatClavesSSA_Sales '{0}', '', '', '', '', '', '', '', '', '', '', '{1}', '', {2} ",
                                    txtId.Text.Trim(), DtGeneral.IdPersonal, iOpcion);

                            sSql = string.Format("Exec spp_Mtto_CatClavesSSA_Sales \n" +
                                "\t@IdClaveSSA_Sal = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdPersonal = '{3}', @iOpcion = '{4}' ",
                                txtId.Text.Trim(), DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal, iOpcion);

                            sCadena = sSql.Replace("'", "\"");
                            auditoria.GuardarAud_MovtosUni("*", sCadena);

                            if (myLeer.Exec(sSql))
                            {
                                if(myLeer.Leer())
                                {
                                    sMensaje = string.Format("{0}", myLeer.Campo("Mensaje"));
                                }

                                ConexionLocal.CompletarTransaccion();
                                General.msjUser(sMensaje); //Este mensaje lo genera el SP
                                btnNuevo_Click(null, null);
                            }
                            else
                            {
                                ConexionLocal.DeshacerTransaccion();
                                Error.GrabarError(myLeer, "btnCancelar_Click");
                                General.msjError("Ocurrió un error al eliminar la Clave Sal");
                                //btnNuevo_Click(null, null);
                            }

                            ConexionLocal.Cerrar();
                        }
                    }
                }
            }
        }

        #endregion Eliminar Clave SSA Sales

        #region Validaciones de Controles

        private bool ValidaDatos()
        {
            bool bRegresa = true;
            // int i = 0;

            if ( txtId.Text.Trim() == "" ) 
            {
                bRegresa = false; 
                General.msjUser("Id de Clave inválido, verifique.");
                txtId.Focus();
            }

            if (bRegresa && txtClaveSSA.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado una Clave valida, verifique.");
                txtClaveSSA.Focus();
            }

            if (bRegresa && txtClaveSSABase.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado una Clave Base valida, verifique.");
                txtClaveSSABase.Focus();
            }

            if (bRegresa && txtDescripcion.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No capturado una Descripción para la Clave, verifique.");
                txtDescripcion.Focus();
            }

            if (bRegresa && txtDescripcionCorta.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No capturado una Descripción corta para la Clave, verifique.");
                txtDescripcionCorta.Focus();
            }

            if (bRegresa && cboGrupoTerapeutico.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un Grupo terapeutico, verifique.");
                cboGrupoTerapeutico.Focus(); 
            }

            if (bRegresa && cboTipoCatalogo.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un Tipo de catalogo, verifique.");
                cboTipoCatalogo.Focus();
            }

            if (bRegresa && cboPresentaciones.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado una Presentación valida, verifique.");
                cboPresentaciones.Focus();
            }

            if (bRegresa && cboTipoClave.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado el Tipo de Clave valida, Verifique....");
                cboTipoClave.Focus();
            }

            if (bRegresa && txtContenido.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el Contenido Paquete, verifique.");
                txtContenido.Focus();
            }

            return bRegresa;
        }

        #endregion Validaciones de Controles

        #region Eventos

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            string sCadena = "";

            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayuda.ClavesSSA_Sales("txtId_KeyDown");

                sCadena = Ayuda.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (myLeer.Leer())
                {
                    CargaDatos();
                }
            }

        }

        #endregion Eventos

        #region Imprimir
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            DatosCliente.Funcion = "btnImprimir_Click()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;
            bool bRegresa = false; 

            myRpt.RutaReporte = GnOficinaCentral.RutaReportes;
            myRpt.NombreReporte = "Central_Listado_ClavesSSA_Sales";

            bRegresa = DtGeneral.GenerarReporte(General.Url, true, myRpt, DatosCliente);

            ////DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
            ////DataSet datosC = DatosCliente.DatosCliente();

            ////conexionWeb.Timeout = 300000; 
            ////btReporte = conexionWeb.Reporte(InfoWeb, datosC);

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }
        #endregion Imprimir

        #region Funciones

        private void CargarGruposTerapeuticos()
        {
            cboGrupoTerapeutico.Clear();
            cboGrupoTerapeutico.Add("0", "<< Seleccione >>");
            myLeer.DataSetClase = Consultas.GruposTerapeuticos("CargarGruposTerapeuticos()");
            cboGrupoTerapeutico.Add(myLeer.DataSetClase, true, "IdGrupoTerapeutico", "Descripcion");
            cboGrupoTerapeutico.SelectedIndex = 0;
        }

        private void CargarTiposCatalogo()
        {
            cboTipoCatalogo.Clear();
            cboTipoCatalogo.Add("0", "<< Seleccione >>");
            myLeer.DataSetClase = Consultas.TiposCatalogoClaves("CargarTiposCatalogo()");
            cboTipoCatalogo.Add(myLeer.DataSetClase, true, "TipoCatalogo", "Descripcion");
            cboTipoCatalogo.SelectedIndex = 0;
        }

        private void LlenaPresentaciones()
        {
            cboPresentaciones.Clear();
            cboPresentaciones.Add("0", "<< Seleccione >>");
            myLeer.DataSetClase = Consultas.ComboPresentaciones("LlenaPresentaciones");
            if (myLeer.Leer())
            {
                cboPresentaciones.Add(myLeer.DataSetClase, true);
            }
            cboPresentaciones.SelectedIndex = 0;
        }

        private void CargaTipoDeClave()
        {
            cboTipoClave.Clear();
            cboTipoClave.Add("0", "<< Seleccione >>"); 
            cboTipoClave.Add("00", "OTROS"); 
            cboTipoClave.Add("01", "MATERIAL DE CURACION"); 
            cboTipoClave.Add("02", "MEDICAMENTO"); 
            cboTipoClave.SelectedIndex = 0;
        }
        #endregion Funciones

        private void btnPresentacion_Click(object sender, EventArgs e)
        {
            FrmPresentaciones f = new FrmPresentaciones();
            Fg.CentrarForma(f);
            f.ShowDialog();
            LlenaPresentaciones();
        }
    } //Llaves de la clase
}
