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
    public partial class FrmSubRegiones : FrmBaseExt
    {
        clsConexionSQL con = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsAyudas Ayuda = new clsAyudas();
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;
         
        public FrmSubRegiones()
        {
            InitializeComponent();
            con.SetConnectionString();

            leer = new clsLeer(ref con);
            Consultas = new clsConsultas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.Modulo, GnOficinaCentral.Version, this.Name);

        }

        private void FrmSubRegiones_Load(object sender, EventArgs e)
        {
            LlenaRegiones();
            btnNuevo_Click(null, null);
        }

        #region Funciones
        private bool LlenaRegiones()
        {
            bool bRegresa = false;
            leer = new clsLeer(ref con);

            leer.DataSetClase = Consultas.ComboRegiones("LlenaRegiones");
            if (leer.Leer())
            {
                bRegresa = true;
                LlenaComboRegiones();
            }
            
            return bRegresa;
        }

        private void LlenaComboRegiones()
        {
            //Se hace de esta manera para la ayuda.
            cboReg.Add("0", "<< Seleccione >>");
            cboReg.Add(leer.DataSetClase, true);
            cboReg.SelectedIndex = 0;            
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtIdSubReg.Text == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese la SubRegión por favor");
                txtIdSubReg.Focus();
            }

            if (bRegresa && txtDescripcion.Text == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese la Descripción por favor");
                txtDescripcion.Focus();
            }

            return bRegresa;
        }
        #endregion Funciones

        #region Botones
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int iOpcion = 0;
            string sIdRegion = "";

            if (ValidaDatos())
            {
               
                    if (con.Abrir())
                    {
                        iOpcion = 1;
                        con.IniciarTransaccion();

                        sIdRegion = cboReg.Data;
                        string sSql = string.Format(" Exec spp_Mtto_CatSubRegiones '{0}', '{1}', '{2}', '{3}'", sIdRegion, txtIdSubReg.Text, txtDescripcion.Text, iOpcion);
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

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            lblCancelado.Visible = false;
            cboReg.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            int iOpcion = 0;
            string message = "¿ Desea eliminar la SubRegión seleccionada ?";
            //string caption = "Intermed", 
            string sIdRegion = "";

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
                            sIdRegion = cboReg.Data;
                            string sSql = string.Format(" Exec spp_Mtto_CatSubRegiones '{0}', '{1}', '{2}','{3}'", sIdRegion, txtIdSubReg.Text, txtDescripcion.Text, iOpcion);
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
                    General.msjError("La SubRegion ya esta cancelada, no se puede cancelar.");
                }
            }
        }
        #endregion Botones        

        private void txtIdSubReg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                string sIdRegion = "";

                sIdRegion = cboReg.Data; //Se obtiene el numero de la Region el cual esta oculto.
                leer.DataSetClase = Ayuda.SubRegiones("txtIdSubReg_KeyDown", sIdRegion);

                if (leer.Leer())
                {
                    txtIdSubReg.Text = leer.Campo("IdSubRegion");
                    txtDescripcion.Text = leer.Campo("Descripcion");

                    if (leer.Campo("Status") == "C")
                    {
                        lblCancelado.Visible = true;
                        txtIdSubReg.Enabled = false;
                        txtDescripcion.Enabled = false;
                        cboReg.Enabled = false;
                    }
                    else
                    {
                        txtIdSubReg.Enabled = false;
                        txtDescripcion.Focus();
                    }
                }
            }
        }

        private void txtIdSubReg_Validating(object sender, CancelEventArgs e)
        {
            string sDato = "", sIdRegion = "";

            if (txtIdSubReg.Text.Trim() == "")
            {
                txtIdSubReg.Text = "*";
                txtIdSubReg.Enabled = false;
            }
            else
            {
                sIdRegion = cboReg.Data;
                sDato = string.Format("SELECT * FROM CatSubRegiones (nolock) WHERE IdSubRegion = '{0}' AND IdRegion = '{1}'", Fg.PonCeros(txtIdSubReg.Text, 2),Fg.PonCeros(sIdRegion,2));

                if (!leer.Exec(sDato))
                {
                    Error.GrabarError(leer, "txtIdSubReg_Validating");
                    General.msjError("Error al buscar SubRegion");
                }
                else
                {
                    if (leer.Leer())
                    {
                        txtIdSubReg.Text = leer.Campo("IdSubRegion");
                        txtDescripcion.Text = leer.Campo("Descripcion");

                        if (leer.Campo("Status") == "C")
                        {
                            lblCancelado.Visible = true;
                            txtIdSubReg.Enabled = false;
                            txtDescripcion.Enabled = false;
                            cboReg.Enabled = false;
                        }
                    }
                    else
                    {
                        General.msjError("La SubRegion no existe");
                        txtIdSubReg.Text = "";
                        txtIdSubReg.Focus();
                    }
                }
            }
        }
    }
}
