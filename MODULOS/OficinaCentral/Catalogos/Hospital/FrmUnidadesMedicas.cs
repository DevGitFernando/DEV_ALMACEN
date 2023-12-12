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

namespace OficinaCentral.Catalogos
{
    public partial class FrmUnidadesMedicas : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsAyudas Ayuda;
        DataSet dtsJurisdicciones;
        clsConsultas Consultas;

        clsDatosCliente DatosCliente;
        wsOficinaCentral.wsCnnOficinaCentral conexionWeb;

        //Para Auditoria
        clsAuditoria auditoria;

        public FrmUnidadesMedicas()
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
            ObtenerJurisdicciones();
        }

        private void FrmUnidadesMedicas_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            lblCancelado.Visible = false;
            IniciaToolBar(true, false, false, false);
            txtId.Focus();
        }


        #region Buscar Unidad

        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            string sCadena = "";

            myLeer = new clsLeer(ref ConexionLocal);

            if (txtId.Text.Trim() == "" || txtId.Text.Trim()== "*")
            {
                txtId.Enabled = false;
                txtId.Text = "*";
                IniciaToolBar(true, true, false, false);
            }
            else
            {
                myLeer.DataSetClase = Consultas.UnidadesMedicas(txtId.Text.Trim(), "txtId_Validating");

                sCadena = Consultas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (myLeer.Leer())
                    CargaDatos();
                else
                    btnNuevo_Click(null, null);
            }
        }

        private void CargaDatos()
        {
            //Se hace de esta manera para la ayuda.
            txtId.Text = myLeer.Campo("IdUMedica");
            cboEstados.Data = myLeer.Campo("IdEstado");
            cboJurisdicciones.Data = myLeer.Campo("IdJurisdiccion");
            txtCLUES.Text = myLeer.Campo("CLUES");
            txtDescripcion.Text = myLeer.Campo("NombreUMedica");
            txtId.Enabled = false;
            IniciaToolBar(true, true, true, false);

            if (myLeer.Campo("Status") == "C")
            {
                IniciaToolBar(true, true, false, false);
                lblCancelado.Visible = true;
                txtId.Enabled = false;
                cboEstados.Enabled = false;
                cboJurisdicciones.Enabled = false;
                txtCLUES.Enabled = false;
                txtDescripcion.Enabled = false;
            }
 
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            string sCadena = "";

            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayuda.UnidadesMedicas("txtId_KeyDown");

                sCadena = Ayuda.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (myLeer.Leer())
                {
                    CargaDatos();
                }
            }

        }
        #endregion Buscar Unidad

        #region Guardar/Actualizar Unidad

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "", sCadena = "";
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion

            if (ValidaDatos())
            {
                if (ConexionLocal.Abrir())
                {
                    ConexionLocal.IniciarTransaccion();

                    sSql = String.Format("Exec spp_Mtto_CatUnidadesMedicas '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ",
                            txtId.Text.Trim(), cboEstados.Data, cboJurisdicciones.Data, txtCLUES.Text.Trim(), txtDescripcion.Text.Trim(), iOpcion );

                    sCadena = sSql.Replace("'", "\"");
                    auditoria.GuardarAud_MovtosUni("*", sCadena);

                    if (myLeer.Exec(sSql))
                    {
                        if( myLeer.Leer() )
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
                else
                {
                    General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
                }                    

            }            

        }

        #endregion Guardar/Actualizar Unidad

        #region Eliminar Unidad

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "", sCadena = "";
            int iOpcion = 2; //La opcion 2 indica que es una cancelacion
            string message = "¿ Desea eliminar la Unidad Medica seleccionada ?";

            if (General.msjCancelar(message) == DialogResult.Yes)
            {
                if (ConexionLocal.Abrir())
                {
                    ConexionLocal.IniciarTransaccion();

                    sSql = String.Format("Exec spp_Mtto_CatUnidadesMedicas '{0}', '', '', '', '', '{1}' ",
                            txtId.Text.Trim(), iOpcion);

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
                        General.msjError("Ocurrió un error al eliminar la Unidad Medica.");
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

        #endregion Eliminar Unidad

        #region Validaciones de Controles

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtId.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese la Clave Unidad Medica por favor");
                txtId.Focus();                
            }

            if (bRegresa && cboEstados.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("Seleccione el Estado por favor");
                cboEstados.Focus();
            }

            if (bRegresa && cboJurisdicciones.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("Seleccione la Jurisdicción por favor");
                cboJurisdicciones.Focus();
            }

            if (bRegresa && txtCLUES.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese la CLUES por favor");
                txtCLUES.Focus();
            }

            if (bRegresa && txtDescripcion.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese la Descripción por favor");
                txtDescripcion.Focus();                
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
        private void IniciaToolBar(bool Nuevo, bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnNuevo.Enabled = Nuevo;
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            // btnImprimir.Enabled = Imprimir;            
        }
        private void CargarEstados()
        {
            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");
            cboEstados.Add(Consultas.EstadosConFarmacias("FrmExistenciaPorClaveSSA_EstadoFarmacias"), true, "IdEstado", "Estado");
            cboEstados.SelectedIndex = 0;

        }

        private void ObtenerJurisdicciones()
        {
            dtsJurisdicciones = new DataSet();

            cboJurisdicciones.Clear();
            cboJurisdicciones.Add("0", "<< Seleccione >>");
            cboJurisdicciones.SelectedIndex = 0;

            dtsJurisdicciones = Consultas.Jurisdicciones("ObtenerJurisdicciones()");
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboJurisdicciones.Clear();
            cboJurisdicciones.Add("0", "<< Seleccione >>");
            if (cboEstados.SelectedIndex != 0)
            {
                try
                {
                    cboJurisdicciones.Add(dtsJurisdicciones.Tables[0].Select(string.Format("IdEstado = '{0}'", cboEstados.Data)), true, "IdJurisdiccion", "Descripcion");
                }
                catch { }
            }
            cboJurisdicciones.SelectedIndex = 0;
        }

        #endregion Funciones 

        


    } //Llaves de la clase
}
