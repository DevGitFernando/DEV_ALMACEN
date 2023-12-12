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

using DllFarmaciaSoft;

namespace OficinaCentral.Catalogos
{
    public partial class FrmSubProgramas_Farmacias : FrmBaseExt
    {
        clsConexionSQL con = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsAyudas Ayuda = new clsAyudas();
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;

        public FrmSubProgramas_Farmacias()
        {
            InitializeComponent();
            con.SetConnectionString();

            leer = new clsLeer(ref con);
            Consultas = new clsConsultas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.Modulo, GnOficinaCentral.Version, this.Name);

        }

        #region Funciones

        private bool LlenaEstados()
        {
            bool bRegresa = false;
            leer = new clsLeer(ref con);

            leer.DataSetClase = Consultas.ComboEstados("LlenaEstados");
            if (leer.Leer())
            {
                bRegresa = true;
                LlenaComboEstados();
            }
            else
            {
                this.Close();
            }
            return bRegresa;
        }

        private void LlenaComboEstados()
        {
            //Se hace de esta manera para la ayuda.
            cboEdo.Add("0", "<< Seleccione >>");
            cboEdo.Add(leer.DataSetClase, true);
            cboEdo.SelectedIndex = 0;

            cboFar.Clear();
            cboFar.Add("0", "<< Seleccione >>");
            cboFar.SelectedIndex = 0;
        }

        private bool LlenaFarmacias()
        {
            string sIdEdo = "";
            bool bRegresa = false;
            leer = new clsLeer(ref con);
            sIdEdo = cboEdo.Data;
            leer.DataSetClase = Consultas.ComboFarmacias(sIdEdo, "LlenaFarmacias");
            if (leer.Leer())
            {
                bRegresa = true;
                LlenaComboFarmacias();
            }
            
            return bRegresa;
        }

        private void LlenaComboFarmacias()
        {
            //Se hace de esta manera para la ayuda.
            cboFar.Clear();
            cboFar.Add("0", "<< Seleccione >>");
            cboFar.Add(leer.DataSetClase.Tables[0].Select("IdEstado = '" + cboEdo.Data + "'"), true, "IdFarmacia", "NombreFarmacia");
            cboFar.SelectedIndex = 0;
        }

        #endregion Funciones

        private void FrmSubProgramas_Farmacias_Load(object sender, EventArgs e)
        {
            LlenaEstados();
            btnNuevo_Click(null, null);
        }

        #region Botones

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            lblCancelado.Visible = false;
            cboEdo.Focus();
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "", sIdEstado = "", sIdFarmacia = "";
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion

            if (ValidaDatos())
            {
                if (con.Abrir())
                {
                    con.IniciarTransaccion();

                    sIdEstado = cboEdo.Data; //Se obtiene el numero del estado el cual esta oculto.
                    sIdFarmacia = cboFar.Data; //Se obtiene el numero de la farmacia el cual esta oculto.
                    sSql = String.Format("EXEC spp_Mtto_CatSubProgramas_Farmacias '{0}', '{1}', '{2}', '{3}', {4} ",
                            sIdEstado, sIdFarmacia, txtPro.Text, txtSubPro.Text, iOpcion);

                    if (leer.Exec(sSql))
                    {
                        if (leer.Leer())
                            sMensaje = String.Format("{0}", leer.Campo("Mensaje"));

                        con.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        con.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click_1");
                        General.msjError("Ocurrió un error al guardar la información.");
                        //btnNuevo_Click(null, null);

                    }

                    con.Cerrar();
                }
                else
                {
                    General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
                }

            }
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "", sIdEstado = "", sIdFarmacia = "";
            int iOpcion = 2; //La opcion 1 indica que es una insercion/actualizacion 
            string message = "¿ Desea eliminar el SubPrograma Por Farmacia seleccionado ?";

            if (ValidaDatos())
            {
                if (lblCancelado.Visible == false)
                {
                    if (General.msjCancelar(message) == DialogResult.Yes)
                    {
                        if (con.Abrir())
                        {
                            con.IniciarTransaccion();

                            sIdEstado = cboEdo.Data; //Se obtiene el numero del estado el cual esta oculto.
                            sIdFarmacia = cboFar.Data; //Se obtiene el numero de la farmacia el cual esta oculto.
                            sSql = String.Format("Exec spp_Mtto_CatSubProgramas_Farmacias '{0}', '{1}', '{2}', '{3}', {4} ",
                                    sIdEstado, sIdFarmacia, txtPro.Text.Trim(), txtSubPro.Text.Trim(), iOpcion);

                            if (leer.Exec(sSql))
                            {
                                if (leer.Leer())
                                    sMensaje = String.Format("{0}", leer.Campo("Mensaje"));

                                con.CompletarTransaccion();
                                General.msjUser(sMensaje); //Este mensaje lo genera el SP
                                btnNuevo_Click(null, null);
                            }
                            else
                            {
                                con.DeshacerTransaccion();
                                Error.GrabarError(leer, "btnCancelar_Click_1");
                                General.msjError("Ocurrió un error al guardar la información.");
                                //btnNuevo_Click(null, null);

                            }

                            con.Cerrar();
                        }
                        else
                        {
                            General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
                        }
                    }
                }
                else
                {
                    General.msjError("El SubPrograma Por Farmacia ya esta cancelado, no se puede cancelar.");
                }
            }
        }       

        #endregion Botones

        #region Validaciones

        private void cboEdo_Validating(object sender, CancelEventArgs e)
        {
            LlenaFarmacias();
        }

        private void txtPro_Validating(object sender, CancelEventArgs e)
        {
            string sDato = "";

            if (txtPro.Text.Trim() == "")
            {
                // General.msjError("Capture el Programa por favor...");
                // txtPro.Focus();
            }
            else
            {
                sDato = string.Format("SELECT * FROM CatProgramas WHERE IdPrograma='{0}' ", Fg.PonCeros(txtPro.Text, 3));

                if (!leer.Exec(sDato))
                {
                    Error.GrabarError(leer, "txtPro_Validating");
                    General.msjError("Error al consultar el Programa");
                }
                else
                {
                    if (leer.Leer())
                    {                        
                        if (leer.Campo("Status") == "C")
                        {
                            General.msjError("El Programa esta Cancelado");
                            txtPro.Text = "";
                            txtPro.Focus();
                        }
                        else
                        {
                            txtPro.Text = leer.Campo("IdPrograma");
                            lblDes.Text = leer.Campo("Descripcion");
                        }
                    }
                    else
                    {
                        General.msjError("El Programa no existe");
                        txtPro.Text = "";
                        txtPro.Focus();
                    }
                }
            }
        }

        private void txtSubPro_Validating(object sender, CancelEventArgs e)
        {
            string sDato = "";

            if (txtPro.Text.Trim() == "")
            {
                //General.msjError("Capture el SubPrograma por favor...");
                //txtPro.Focus();
            }
            else
            {
                sDato = string.Format("SELECT * FROM CatSubProgramas WHERE IdSubPrograma = '{0}' ", Fg.PonCeros(txtSubPro.Text, 2));

                if (!leer.Exec(sDato))
                {
                    Error.GrabarError(leer, "txtSubPro_Validating");
                    General.msjError("Error al consultar el SubPrograma");
                }
                else
                {
                    if (leer.Leer())
                    {
                        if (leer.Campo("Status") == "C")
                        {
                            General.msjError("El SubPrograma esta Cancelado");
                            txtSubPro.Text = "";
                            txtSubPro.Focus();
                        }
                        else
                        {
                            txtSubPro.Text = leer.Campo("IdSubPrograma");
                            lblSubDes.Text = leer.Campo("Descripcion");
                        }
                    }
                    else
                    {
                        General.msjError("El SubPrograma no existe");
                        txtSubPro.Text = "";
                        txtSubPro.Focus();
                    }
                }
            }
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;
            //int i = 0;

            if (cboEdo.Data == "0")
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado el Estado, verifique.");
                cboEdo.Focus();
            }

            if (bRegresa && cboFar.Data == "0")
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado la Farmacia, verifique.");
                cboFar.Focus();
            }

            if (bRegresa && txtPro.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el programa, verifique.");
                txtPro.Focus();
            }

            if (bRegresa && txtSubPro.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el sub-programa, verifique.");
                txtSubPro.Focus();
            }

            return bRegresa;
        }

        #endregion Validaciones
        


    }
}
