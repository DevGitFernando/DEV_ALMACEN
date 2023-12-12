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

namespace OficinaCentral.FarmaciasConvenioVales
{
    public partial class FrmFarmaciasConvenio : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsAyudas Ayuda;
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;

        clsDatosCliente DatosCliente;
        wsOficinaCentral.wsCnnOficinaCentral conexionWeb;

        //Para Auditoria
        clsAuditoria auditoria;

        public FrmFarmaciasConvenio()
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

            CargarEstados();
        }

        private void FrmLaboratorios_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            lblCancelado.Visible = false;
            IniciaToolBar(false, false, false);
            cboEstados.Focus();
        }


        #region Buscar Farmacia Convenio 

        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            string sCadena = "";

            myLeer = new clsLeer(ref ConexionLocal);

            if (cboEstados.SelectedIndex != 0)
            {
                if (txtId.Text.Trim() == "")
                {
                    cboEstados.Enabled = false;
                    txtId.Enabled = false;
                    txtId.Text = "*";
                    IniciaToolBar(true, false, false);
                }
                else
                {
                    myLeer.DataSetClase = Consultas.FarmaciasConvenio(cboEstados.Data, txtId.Text.Trim(), "txtId_Validating");

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
        }

        private void CargaDatos()
        {
            //Se hace de esta manera para la ayuda.
            cboEstados.Data = myLeer.Campo("IdEstado");
            cboEstados.Enabled = false;
            txtId.Text = myLeer.Campo("IdFarmaciaConvenio");
            txtId.Enabled = false;
            txtDescripcion.Text = myLeer.Campo("Nombre");
            txtDomicilio.Text = myLeer.Campo("Direccion");
            IniciaToolBar(true, true, false);

            if (myLeer.Campo("Status") == "C")
            {
                lblCancelado.Visible = true;
                IniciaToolBar(true, false, false);
                txtId.Enabled = false;
                txtDescripcion.Enabled = false;
                txtDomicilio.Enabled = false;
            }

        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            string sCadena = "";

            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayuda.FarmaciasConvenio(cboEstados.Data, "txtId_KeyDown");

                sCadena = Ayuda.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (myLeer.Leer())
                {
                    CargaDatos();
                }
            }

        }

        #endregion Buscar Farmacia Convenio

        #region Guardar/Actualizar Farmacia Convenio 

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "", sCadena = "";
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion

            if (ValidaDatos())
            {
                if (ConexionLocal.Abrir())
                {
                    ConexionLocal.IniciarTransaccion();

                    sSql = String.Format("Exec spp_Mtto_CatFarmacias_ConvenioVales '{0}', '{1}', '{2}', '{3}', '{4}' ",
                            cboEstados.Data, txtId.Text.Trim(), txtDescripcion.Text.Trim(), txtDomicilio.Text.Trim(), iOpcion );

                    sCadena = sSql.Replace("'", "\"");
                    auditoria.GuardarAud_MovtosUni("*", sCadena);

                    if (myLeer.Exec(sSql))
                    {
                        if (myLeer.Leer())
                        {
                            sMensaje = String.Format("{0}", myLeer.Campo("Mensaje"));
                        }

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
                else
                {
                    General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
                }                    

            }

        }

        #endregion Guardar/Actualizar Farmacia Convenio

        #region Eliminar Farmacia Convenio 

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "", sCadena = "";
            int iOpcion = 2; //La opcion 2 indica que es una cancelacion
            string message = "¿ Desea eliminar la Farmacia Convenio seleccionada ?";

            if (General.msjCancelar(message) == DialogResult.Yes)
            {
                if (ConexionLocal.Abrir())
                {
                    ConexionLocal.IniciarTransaccion();

                    sSql = String.Format("Exec spp_Mtto_CatFarmacias_ConvenioVales '{0}', '{1}', '{2}', '{3}', '{4}' ",
                            cboEstados.Data, txtId.Text.Trim(), txtDescripcion.Text.Trim(), txtDomicilio.Text.Trim(), iOpcion);

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
                        Error.GrabarError(myLeer, "btnCancelar_Click");
                        General.msjError("Ocurrió un error al eliminar la Farmacia Convenio.");
                    }

                    ConexionLocal.Cerrar();
                }
                else
                {
                    General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
                }

            }
        }

        #endregion Eliminar Farmacia Convenio

        #region Validaciones de Controles

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (cboEstados.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("Seleccione el Estado por favor");
                cboEstados.Focus();

            }

            if (bRegresa && txtId.Text == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese la Clave Farmacia por favor");
                txtId.Focus();
                
            }

            if (bRegresa && txtDescripcion.Text == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese la Descripción por favor");
                txtDescripcion.Focus();
            }

            if (bRegresa && txtDomicilio.Text == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese el Domicilio por favor");
                txtDomicilio.Focus();
            }
            
            return bRegresa;
        }

        #endregion Validaciones de Controles

        #region Imprimir
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //DatosCliente.Funcion = "btnImprimir_Click()";
            //clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            //byte[] btReporte = null;

            //myRpt.RutaReporte = GnOficinaCentral.RutaReportes;
            //myRpt.NombreReporte = "Central_Listado_Laboratorios";

            //DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
            //DataSet datosC = DatosCliente.DatosCliente();

            //btReporte = conexionWeb.Reporte(InfoWeb, datosC);

            //if (!myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true))
            //{
            //    General.msjError("Ocurrió un error al cargar el reporte.");
            //}

            //auditoria.GuardarAud_MovtosUni("*", myRpt.NombreReporte);
        }
        #endregion Imprimir

        #region Funciones 
        private void IniciaToolBar(bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
        }

        private void CargarEstados()
        {
            // string sSql = " Select IdEstado, Nombre as Estado From CatEstados(NoLock) Where Status = 'A' Order by Nombre ";
            
            cboEstados.Clear();
            cboEstados.Add();

            cboEstados.Add(Consultas.EstadosConFarmacias("CargarEstados()"), true, "IdEstado", "NombreEstado");

            cboEstados.SelectedIndex = 0;
        } 
        #endregion Funciones 



    } //Llaves de la clase
}
