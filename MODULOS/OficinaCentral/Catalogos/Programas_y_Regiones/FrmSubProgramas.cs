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
    public partial class FrmSubProgramas : FrmBaseExt
    {
        clsConexionSQL con = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsAyudas Ayuda = new clsAyudas();
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;

        public FrmSubProgramas()
        {
            InitializeComponent();
            con.SetConnectionString();

            leer = new clsLeer(ref con);
            Consultas = new clsConsultas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.Modulo, GnOficinaCentral.Version, this.Name);

        }

        #region Funciones
        private bool LlenaProgramas()
        {
            bool bRegresa = false;
            leer = new clsLeer(ref con);

            leer.DataSetClase = Consultas.ComboProgramas("LlenaProgramas");
            if (leer.Leer())
            {
                bRegresa = true;
                LlenaComboProgramas();
            }
            
            return bRegresa;
        }

        private void LlenaComboProgramas()
        {
            //Se hace de esta manera para la ayuda.
            cboPro.Add("0", "<< Seleccione >>");
            cboPro.Add(leer.DataSetClase, true);
            cboPro.SelectedIndex = 0;
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtIdSubPro.Text == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese el SubPrograma por favor");
                txtIdSubPro.Focus();
            }

            if (bRegresa &&  txtDescripcion.Text == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese la Descripción por favor");
                txtDescripcion.Focus();
            }

            return bRegresa;
        }

        #endregion Funciones

        #region Validaciones
        private void FrmSubProgramas_Load(object sender, EventArgs e)
        {
            LlenaProgramas();
        }

        private void txtIdSubPro_Validating(object sender, CancelEventArgs e)
        {
            string sDato = "", sIdProg = "";

            if (txtIdSubPro.Text.Trim() == "")
            {
                txtIdSubPro.Text = "*";
                txtIdSubPro.Enabled = false;
            }
            else
            {
                sIdProg = cboPro.Data;
                sDato = string.Format("SELECT * FROM CatSubProgramas (nolock) WHERE IdSubPrograma = '{0}' AND IdPrograma = '{1}'", Fg.PonCeros(txtIdSubPro.Text, 4), Fg.PonCeros(sIdProg, 4));

                if (!leer.Exec(sDato))
                {
                    Error.GrabarError(leer, "txtIdSubPro_Validating");
                    General.msjError("Error al buscar el SubPrograma");
                }
                else
                {
                    if (leer.Leer())
                    {
                        txtIdSubPro.Text = leer.Campo("IdSubPrograma");
                        txtDescripcion.Text = leer.Campo("Descripcion");

                        if (leer.Campo("Status") == "C")
                        {
                            lblCancelado.Visible = true;
                            txtIdSubPro.Enabled = false;
                            txtDescripcion.Enabled = false;
                            cboPro.Enabled = false;
                        }
                    }
                    else
                    {
                        General.msjError("El SubPrograma no existe");
                        txtIdSubPro.Text = "";
                        txtIdSubPro.Focus();
                    }
                }
            }
        }

        #endregion Validaciones

        #region Botones

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            lblCancelado.Visible = false;
            cboPro.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int iOpcion = 0;
            string sIdPrograma = "";

            if (ValidaDatos())
            {                
                if (con.Abrir())
                {
                    iOpcion = 1;
                    con.IniciarTransaccion();

                    sIdPrograma = cboPro.Data;
                    string sSql = string.Format(" Exec spp_Mtto_CatSubProgramas '{0}', '{1}', '{2}', '{3}'", sIdPrograma, txtIdSubPro.Text, txtDescripcion.Text, iOpcion);
                    if (!leer.Exec(sSql))
                    {
                        con.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al grabar la información");
                    }
                    else
                    {
                        con.CompletarTransaccion();
                        leer.Leer();
                        General.msjUser("Información guardada satisfactoriamente." + leer.Campo(1));
                        btnNuevo_Click(null, null);
                    }

                    con.Cerrar();
                }
                else
                {
                    General.msjError("Sin conexion al servidor.");
                }                
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            int iOpcion = 0;
            string message = "¿ Desea eliminar el SubPrograma seleccionado ?";
            //string caption = "Intermed", 
            string sIdPrograma = "";

            if (ValidaDatos())
            {
                if (lblCancelado.Visible == false)
                {
                    if (General.msjCancelar(message) == DialogResult.Yes)
                    {
                        if (con.Abrir())
                        {
                            iOpcion = 2;
                            con.IniciarTransaccion();
                            sIdPrograma = cboPro.Data;
                            string sSql = string.Format(" Exec spp_Mtto_CatSubProgramas '{0}', '{1}', '{2}','{3}'", sIdPrograma, txtIdSubPro.Text, txtDescripcion.Text, iOpcion);
                            if (!leer.Exec(sSql))
                            {
                                con.DeshacerTransaccion();
                                General.msjError("Ocurrió un error al actualizar la información");
                            }
                            else
                            {
                                con.CompletarTransaccion();
                                leer.Leer();
                                General.msjUser("Información actualizada satisfactoriamente." + leer.Campo(1));
                                btnNuevo_Click(null, null);
                            }

                            con.Cerrar();
                        }
                        else
                        {
                            General.msjError("Sin conexion al servidor.");
                        }
                    }
                }
                else
                {
                    General.msjError("El SubPrograma ya esta cancelado, no se puede cancelar.");
                }
            }
        }

        #endregion Botones

        

        private void txtIdSubPro_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                string sIdPrograma = "";

                sIdPrograma = cboPro.Data; //Se obtiene el numero del Programa el cual esta oculto.
                leer.DataSetClase = Ayuda.SubProgramas("txtIdSubPro_KeyDown", sIdPrograma);

                if (leer.Leer())
                {
                    txtIdSubPro.Text = leer.Campo("IdSubPrograma");
                    txtDescripcion.Text = leer.Campo("Descripcion");

                    if (leer.Campo("Status") == "C")
                    {
                        lblCancelado.Visible = true;
                        txtIdSubPro.Enabled = false;
                        txtDescripcion.Enabled = false;
                        cboPro.Enabled = false;
                    }
                    else
                    {
                        txtIdSubPro.Enabled = false;
                        txtDescripcion.Focus();
                    }
                }
            }
        }

        


    }
}
