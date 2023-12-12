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
    public partial class FrmDistribuidores : FrmBaseExt
    {
        clsConexionSQL con = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsAyudas Ayuda = new clsAyudas();
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;

        public FrmDistribuidores()
        {
            InitializeComponent();

            leer = new clsLeer(ref con);
            Consultas = new clsConsultas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name, false);
            Ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name, false);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name);
        }

        private void FrmDistribuidores_Load(object sender, EventArgs e)
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
            GuardaInformacion(1);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            GuardaInformacion(2);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            lblCancelado.Visible = false;
            txtId.Focus();
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;           

            if (txtDescripcion.Text == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el Nombre del Distribuidor, verifique.");
                txtDescripcion.Focus();
            }           

            return bRegresa;
        }

        private void GuardaInformacion(int iOpcion)
        {
            string sMensaje = "";

            if (ValidaDatos())
            {
                if (con.Abrir())
                {                    
                    con.IniciarTransaccion();

                    string sSql = string.Format(" Exec spp_Mtto_CatDistribuidores '{0}', '{1}', '{2}' ", txtId.Text, txtDescripcion.Text, iOpcion);

                    if (!leer.Exec(sSql))
                    {
                        Error.GrabarError(leer, "GuardaInformacion"); 
                        con.DeshacerTransaccion();
                        General.msjError("Ocurrió un error al grabar la información");
                    }
                    else
                    {                        
                        con.CompletarTransaccion();
                        leer.Leer();
                        sMensaje = leer.Campo("Mensaje");
                        General.msjUser(sMensaje);
                        btnNuevo_Click(null, null);
                    }

                    con.Cerrar();
                }
                else
                {
                    General.msjAviso(General.MsjErrorAbrirConexion); 
                }

            }
        }
        #endregion Funciones

        #region Eventos
        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            // string sDato = "";
            if (txtId.Text.Trim() == "")
            {
                txtId.Text = "*";
                txtId.Enabled = false;
            }
            else
            {
                leer.DataSetClase = Consultas.Distribuidores(txtId.Text, "txtId_Validating()"); 
                if (leer.Leer()) 
                {
                    txtId.Text = leer.Campo("IdDistribuidor");
                    txtDescripcion.Text = leer.Campo("NombreDistribuidor");
                    txtId.Enabled = false;

                    if (leer.Campo("Status") == "C")
                    {
                        lblCancelado.Visible = true;
                        txtDescripcion.Enabled = false;                            
                    }
                }
                else
                {
                    General.msjUser("Clave de Distribuidor no encontrada, verifique.");
                    txtId.Text = "";
                    txtId.Focus();
                }
            }
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Distribuidores("txtId_KeyDown");

                if (leer.Leer())
                {
                    txtId.Text = leer.Campo("IdDistribuidor");
                    txtDescripcion.Text = leer.Campo("NombreDistribuidor");
                    txtId.Enabled = false;

                    if (leer.Campo("Status") == "C")
                    {
                        lblCancelado.Visible = true;
                        txtDescripcion.Enabled = false;                        
                    }
                }
            }
        }
        #endregion Eventos

        
    }
}
