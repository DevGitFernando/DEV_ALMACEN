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
using SC_SolutionsSystem.FuncionesGrid;

using DllFarmaciaSoft;

namespace OficinaCentral.Catalogos
{
    public partial class FrmSubFarmacias : FrmBaseExt 
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer, myLeerDatos; 
        clsAyudas Ayuda;
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;
        clsGrid Grid;

        clsDatosCliente DatosCliente;
        wsOficinaCentral.wsCnnOficinaCentral conexionWeb;
        // string sMensaje = "";

        private enum Cols
        {
            Ninguna = 0,
            IdSubFarmacia = 1, Descripcion = 2, EsConsignacion = 3
        }

        public FrmSubFarmacias()
        {
            InitializeComponent();
            ConexionLocal.SetConnectionString();

            myLeer = new clsLeer(ref ConexionLocal);
            myLeerDatos = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.Modulo, GnOficinaCentral.Version, this.Name);

            DatosCliente = new clsDatosCliente(GnOficinaCentral.DatosApp, this.Name, "");
            conexionWeb = new OficinaCentral.wsOficinaCentral.wsCnnOficinaCentral();
            conexionWeb.Url = General.Url;

            Grid = new clsGrid(ref grdSubFarmacias, this);
            Grid.EstiloGrid(eModoGrid.Normal);
            Grid.Limpiar(false);
        }

        private void FrmSubFarmacias_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Buscar SubFarmacias

        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            myLeer = new clsLeer(ref ConexionLocal);

            if (txtIdEstado.Text.Trim() != "")            
            {
                myLeer.DataSetClase = Consultas.Estados(txtIdEstado.Text.Trim(), "txtId_Validating");
                if (myLeer.Leer())
                {
                    CargaDatos();
                }
                //else
                //{
                //    btnNuevo_Click(null, null);
                //}
            }
        }

        private void CargaDatos()
        {
            //Se hace de esta manera para la ayuda.
            txtIdEstado.Enabled = false;
            txtIdEstado.Text = myLeer.Campo("IdEstado");
            lblEstado.Text = myLeer.Campo("Nombre");
            IniciaToolBar(true, true, false, false);

            if (myLeer.Campo("Status") == "C")
            {
                IniciaToolBar(true, false, false, false);
                lblCancelado.Visible = true;
                txtIdEstado.Enabled = false;
                txtNumero.Enabled = false;
            }

            BuscarSubFarmacias();
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayuda.Estados("txtId_KeyDown");

                if (myLeer.Leer())
                {
                    CargaDatos();
                }
            }

        }

        #endregion Buscar SubFarmacias

        #region Botones

        #region Limpiar
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            IniciaToolBar(true, false, false, false);
            lblCancelado.Visible = false;
            Grid.Limpiar();
            txtNumero.Text = "0";
            txtIdEstado.Focus();
        }
        #endregion Limpiar

        #region Guardar/Actualizar SubFarmacias

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bContinua = false;

            EliminarRenglonesVacios();
            if (ValidaDatos())
            {
                if (ConexionLocal.Abrir())
                {
                    ConexionLocal.IniciarTransaccion();

                    if (GuardaEstadoSubFarmacias())
                    {
                        bContinua = GuardaSubFarmacias();
                    }

                    if (bContinua)
                    {
                        ConexionLocal.CompletarTransaccion();
                        General.msjUser("La información se guardó exitosamente"); //Este mensaje lo genera el SP
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        ConexionLocal.DeshacerTransaccion();
                        Error.GrabarError(myLeer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al guardar la información.");                        
                    }

                    ConexionLocal.Cerrar();
                }
                else
                {
                    General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
                }                    

            }

        }

        private bool GuardaEstadoSubFarmacias()
        {
            bool bRegresa = true;
            string sSql = "", sSubFarmacia = "", sNombre = "";
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion
            int iEsConsignacion = 0, iRenglones = Grid.Rows;

            for (int i = 1; i <= iRenglones; i++)
            {
                sSubFarmacia = Grid.GetValue(i, (int)Cols.IdSubFarmacia);
                sNombre = Grid.GetValue(i, (int)Cols.Descripcion);
                iEsConsignacion = Grid.GetValueInt(i, (int)Cols.EsConsignacion);

                if (sSubFarmacia.Trim() == "")
                {
                    sSubFarmacia = "*";
                }

                if (sNombre != "")
                {
                    sSql = String.Format("Exec spp_Mtto_CatEstados_SubFarmacias '{0}', '{1}', '{2}', '{3}', '{4}' ",
                                    txtIdEstado.Text.Trim(), sSubFarmacia, sNombre, iEsConsignacion, iOpcion);

                    if (!myLeer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }
            return bRegresa;
        }

        private bool GuardaSubFarmacias()
        {
            bool bRegresa = false;
            string sSql = "";
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion

            sSql = String.Format("Exec spp_Mtto_CatFarmacias_SubFarmacias '{0}', '{1}' ",
                            txtIdEstado.Text.Trim(), iOpcion);

            if (myLeer.Exec(sSql))
            {
                bRegresa = true;
            }            
            
            return bRegresa;
        }

        #endregion Guardar/Actualizar SubFarmacias

        #region Eliminar SubFarmacias

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //string sSql = "", sMensaje = "";
            //int iOpcion = 2; //La opcion 2 indica que es una cancelacion
            //string message = "¿ Desea eliminar las SubFarmacias del Estado seleccionado ?";
            
            //if (lblEstado.Text.Trim() != "") //Si no esta vacio, significa que si existe.
            //{
            //    if (General.msjCancelar(message) == DialogResult.Yes)
            //    {
            //        if (ConexionLocal.Abrir())
            //        {
            //            ConexionLocal.IniciarTransaccion();

            //            sSql = String.Format("Exec  '{0}', '', {1} ",
            //                    txtId.Text.Trim(), iOpcion);

            //            if (myLeer.Exec(sSql))
            //            {
            //                if (myLeer.Leer())
            //                    sMensaje = String.Format("{0}", myLeer.Campo("Mensaje"));

            //                ConexionLocal.CompletarTransaccion();
            //                General.msjUser(sMensaje); //Este mensaje lo genera el SP
            //                btnNuevo_Click(null, null);
            //            }
            //            else
            //            {
            //                ConexionLocal.DeshacerTransaccion();
            //                Error.GrabarError(myLeer, "btnCancelar_Click");
            //                General.msjError("Ocurrió un error al eliminar las SubFarmacias del Estado.");
            //                //btnNuevo_Click(null, null);
            //            }

            //            ConexionLocal.Cerrar();
            //        }
            //        else
            //        {
            //            General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
            //        }

            //    }
            //}   

        }

        #endregion Eliminar SubFarmacias

        #region Imprimir SubFarmacias
        private void btnImprimir_Click(object sender, EventArgs e)
        {
        }

        #endregion Imprimir SubFarmacias
        #endregion Botones

        #region Funciones
        private void IniciaToolBar(bool Nuevo, bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnNuevo.Enabled = Nuevo;
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
        }
        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (lblEstado.Text == "")
            {
                General.msjUser("Ingrese el Estado por favor");
                lblEstado.Focus();
                bRegresa = false;
            }

            if (bRegresa)
            {
                bRegresa = validarCapturaSubFarmacias();
            }
            
            return bRegresa;
        }

        private void txtNumero_Validating(object sender, CancelEventArgs e)
        {
            if (txtNumero.Text.Trim() == "" || txtNumero.Text.Trim() == "0")
            {
                txtNumero.Text = "0";
            }
            else
            {
                txtNumero.Text = Fg.PonCeros(txtNumero.Text.Trim(), 2);
            }

            int iRenglones = int.Parse(txtNumero.Text);
            Grid.Rows = iRenglones;
        }

        private void txtNumero_TextChanged(object sender, EventArgs e)
        {
            if (txtNumero.Enabled)
            {
                //Si esta habilitado significa que el estado no tiene SubFarmacias y por lo tanto se borra.
                Grid.Limpiar();
            }
        }
        #endregion Funciones

        #region Grid

        private void BuscarSubFarmacias()
        {
            string sSql = string.Format("Select IdSubFarmacia, Descripcion, EsConsignacion " + 
                " From CatEstados_SubFarmacias(NoLock)" + 
                " Where IdEstado = '{0}' ", txtIdEstado.Text.Trim());

            if (myLeerDatos.Exec(sSql))
            {
                if (myLeerDatos.Leer())
                {
                    IniciaToolBar(true, true, false, false);//Se bloquean los botones ya que una vez guardado no se puede modificar. Jesus Diaz 2011-01-17.
                    txtNumero.Enabled = false;
                    Grid.LlenarGrid(myLeerDatos.DataSetClase);
                    Grid.BloqueaGrid(true);
                    txtNumero.Text = Grid.Rows.ToString();
                    txtNumero.Text = Fg.PonCeros(txtNumero.Text.Trim(), 2);
                }
            }
            else
            {
                Error.GrabarError(myLeerDatos, "BuscarSubFarmacias()");
                General.msjError("Ocurrió un error al obtener las SubFarmacias del estado seleccionado.");
            }
        }

        private bool validarCapturaSubFarmacias()
        {
            bool bRegresa = true;

            if (Grid.Rows == 0)
            {
                bRegresa = false;
            }
            else
            {
                if (Grid.GetValue(1, (int)Cols.Descripcion) == "")
                {
                    bRegresa = false;
                }
            }

            if (!bRegresa)
            {
                General.msjUser("Debe capturar al menos una SubFarmacia para este estado, verifique.");
            }

            return bRegresa;

        }

        private void EliminarRenglonesVacios()
        {
            for (int i = 1; i <= Grid.Rows; i++) //Renglones.
            {
                if (Grid.GetValue(i, (int)Cols.Descripcion).Trim() == "") //Si la columna Descripcion esta vacia se elimina.
                {
                    Grid.DeleteRow(i);
                }
            }

            if (Grid.Rows == 0) // Si No existen renglones, se inserta 1.
                Grid.AddRow();
        }
        #endregion Grid        


    } //Llaves de la clase
}
