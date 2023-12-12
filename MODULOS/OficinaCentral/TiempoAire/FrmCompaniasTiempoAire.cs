using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Comun;

using DllFarmaciaSoft;

namespace OficinaCentral.TiempoAire
{
    public partial class FrmCompaniasTiempoAire : FrmBaseExt
    {
        private enum Cols
        {
            Id = 1, Descripcion = 2, Monto = 3, Status = 4
        }

        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsLeer myLlenaDatos;
        clsAyudas Ayuda;
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;
        clsGrid myGridMontos;

        public FrmCompaniasTiempoAire()
        {
            InitializeComponent();
            ConexionLocal.SetConnectionString();

            myLeer = new clsLeer(ref ConexionLocal);
            myLlenaDatos = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.Modulo, GnOficinaCentral.Version, this.Name);
        }

        private void FrmCompaniasTiempoAire_Load(object sender, EventArgs e)
        {
            grdMontos.EditModeReplace = false;
            myGridMontos = new clsGrid(ref grdMontos, this);
            myGridMontos.EstiloGrid(eModoGrid.ModoRow);

            btnNuevo_Click(null, null);
        }

        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;

            btnImprimir.Enabled = false;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            lblCancelado.Visible = false;
            IniciarToolBar(false, false, false);
            myGridMontos.Limpiar(true);
            txtId.Focus();
        }


        #region Buscar Compañia

        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            myLeer = new clsLeer(ref ConexionLocal);
            
            if (txtId.Text.Trim() == "")
            {
                IniciarToolBar(true, false, false);
                txtId.Enabled = false;
                txtId.Text = "*";
            }
            else
            {
                IniciarToolBar(true, true, false);
                myLeer.DataSetClase = Consultas.CompaniasTA(txtId.Text.Trim(), "txtId_Validating");
                if (myLeer.Leer())
                    CargaDatos();
                else
                    btnNuevo_Click(null, null);
            }
        }

        private void CargaDatos()
        {
            //Se hace de esta manera para la ayuda.
            txtId.Text = myLeer.Campo("IdCompania");
            txtDescripcion.Text = myLeer.Campo("Descripcion");
            txtId.Enabled = false;

            CargaMontos();
            if (myLeer.Campo("Status") == "C")
            {
                IniciarToolBar(true, false, false);
                lblCancelado.Visible = true;
                txtId.Enabled = false;
                txtDescripcion.Enabled = false;
            }
 
        }

        private void CargaMontos()
        {
            myLlenaDatos = new clsLeer(ref ConexionLocal);
            myLlenaDatos.DataSetClase = Consultas.CompaniasTA_Montos(txtId.Text.Trim(), "CargaMontos");
            {
                myGridMontos.Limpiar(true);
                if (myLlenaDatos.Leer())
                {
                    myGridMontos.LlenarGrid(myLlenaDatos.DataSetClase, false, false);
                }
            }

        }

        #endregion Buscar Compañia

        #region Guardar/Actualizar Compañia

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bContinua = false;
            string sSql = "", sMensaje = "", sIdCompania = "";
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion

            if (ValidaDatos())
            {
                if (ConexionLocal.Abrir())
                {
                    ConexionLocal.IniciarTransaccion();

                    sSql = String.Format("Exec spp_Mtto_CatCompaniasTiempoAire '{0}', '{1}', {2} ",
                            txtId.Text.Trim(), txtDescripcion.Text.Trim(), iOpcion );

                    if (myLeer.Exec(sSql))
                    {
                        if (myLeer.Leer())
                        {
                            sMensaje = String.Format("{0}", myLeer.Campo("Mensaje"));
                            sIdCompania = String.Format("{0}", myLeer.Campo("Clave"));

                            bContinua = GuardarMontos(sIdCompania);
                        }
                    }

                    if (bContinua)
                    {
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

        private bool GuardarMontos(string sIdCompania)
        {
            bool bRegresa = true, bCheck = false;
            int i = 0, iRenglones = 0, iMonto = 0, iStatus = 0;
            string sIdMonto = "", sDescripcion = "", sSql = ""; //sMensaje = "", 

            iRenglones = myGridMontos.Rows;
            for (i = 1; i <= iRenglones; i++)
            {
                bCheck = myGridMontos.GetValueBool(i, (int)Cols.Status);
                if (bCheck)
                    iStatus = 1;
                else
                    iStatus = 0;

                iMonto = myGridMontos.GetValueInt(i, (int)Cols.Monto);
                sIdMonto = myGridMontos.GetValue(i, (int)Cols.Id);
                sDescripcion = myGridMontos.GetValue(i, (int)Cols.Descripcion);

                if (sIdMonto == "")
                {
                    sIdMonto = "*";
                    iStatus = 1; //Si es nuevo se pone activo aun cuando el check este apagado.
                }

                sSql = String.Format("Exec spp_Mtto_CatCompaniasTA_Montos '{0}', '{1}', '{2}', '{3}', '{4}' ",
                        sIdCompania, sIdMonto, sDescripcion, iMonto, iStatus);

                if (iMonto > 0)
                {
                    if (!myLeer.Exec(sSql))
                    {
                        bRegresa = false;
                        Error.GrabarError(myLeer, "GuardarMontos()");
                        break;
                    }
                }

            }

            return bRegresa;
        }

        #endregion Guardar/Actualizar Compañia

        #region Eliminar Compañia

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "";
            int iOpcion = 2; //La opcion 2 indica que es una cancelacion
            string message = "¿ Desea eliminar la Compañia seleccionada ?";

            if (General.msjCancelar(message) == DialogResult.Yes)
            {
                if (ConexionLocal.Abrir())
                {
                    ConexionLocal.IniciarTransaccion();

                    sSql = String.Format("Exec spp_Mtto_CatCompaniasTiempoAire '{0}', '', {1} ",
                            txtId.Text.Trim(), iOpcion);

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
                        General.msjError("Ocurrió un error al eliminar la Compañia.");
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

        #endregion Eliminar Compañia

        #region Validaciones de Controles

        private bool ValidaDatos()
        {
            bool bRegresa = true;
            
            if (txtId.Text == "")
            {
                bRegresa = false; 
                General.msjUser("Ingrese la Clave Compañia por favor");
                txtId.Focus();                               
            }

            if (bRegresa && txtDescripcion.Text == "")
            {
                bRegresa = false; 
                General.msjUser("Ingrese la Descripción por favor");
                txtDescripcion.Focus();
            }

            if(bRegresa)
                bRegresa = validaMontos();

            return bRegresa;
        }

        #endregion Validaciones de Controles

        #region Eventos

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayuda.CompaniasTA("txtId_KeyDown");

                if (myLeer.Leer())
                {
                    CargaDatos();
                }
            }

        }

        #endregion Eventos

        #region Grid

        private void grdMontos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            int iValor = 0;

            if ((myGridMontos.ActiveRow == myGridMontos.Rows) && e.AdvanceNext)
            {
                iValor = myGridMontos.GetValueInt(myGridMontos.ActiveRow, (int)Cols.Monto);
                if (iValor > 0)
                {
                    myGridMontos.Rows = myGridMontos.Rows + 1;
                    myGridMontos.ActiveRow = myGridMontos.Rows;
                    myGridMontos.SetActiveCell(myGridMontos.Rows, (int)Cols.Descripcion);
                }
            }
        }

        private bool validaMontos()
        {
            bool bRegresa = true;

            if (myGridMontos.Rows == 0)
            {
                bRegresa = false;
            }
            else
            {
                if (myGridMontos.GetValue(1, (int)Cols.Descripcion) == "")
                {
                    bRegresa = false;
                }
                else
                {
                    for (int i = 1; i <= myGridMontos.Rows; i++)
                    {
                        if (myGridMontos.GetValueInt(i, (int)Cols.Monto) == 0)
                        {
                            bRegresa = false;
                            break;
                        }
                    }                    
                }
            }

            if (!bRegresa)
                General.msjUser("Debe capturar al menos un monto \n y/o capturar su descripcion, verifique.");

            return bRegresa;

        }

        #endregion Grid

    } //Llaves de la clase
}
