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

namespace OficinaCentral.Catalogos.ConfiguracionesAlmacen
{
    public partial class FrmUbicacionesEstandar : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsAyudas Ayuda;
        clsConsultas Consultas;

        public FrmUbicacionesEstandar()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.Modulo, GnOficinaCentral.Version, this.Name);
        }

        private void FrmUbicacionesEstandar_Load(object sender, EventArgs e)
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
            Guardar_Informacion(1);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Guardar_Informacion(2);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            IniciaBotones(false, false, false);
            lblCancelado.Visible = false;
            txtUbicacion.Focus();
        }

        private void IniciaBotones(bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
        }
        #endregion Funciones

        #region Eventos
        private void txtUbicacion_Validating(object sender, CancelEventArgs e)
        {
            
            if (txtUbicacion.Text.Trim() == "")
            {
                txtUbicacion.Focus();
            }
            else
            {
                Consultas.MostrarMsjSiLeerVacio = false;
                leer.DataSetClase = Consultas.UbicacionesEstandar(txtUbicacion.Text, "txtUbicacion_Validating");
                
                if (leer.Leer())
                {
                    CargaDatosUbicacion();
                }
                else
                {
                    IniciaBotones(true, false, false);
                }
                
            }
        }

        private void CargaDatosUbicacion()
        {
            IniciaBotones(true, true, true);

            txtUbicacion.Text = leer.Campo("NombrePosicion");
            txtDescripcion.Text = leer.Campo("Descripcion");

            if (leer.Campo("Status") == "C")
            {
                lblCancelado.Visible = true;
                IniciaBotones(true, false, true);
            }
        }

        private void txtUbicacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                
                leer.DataSetClase = Ayuda.Ubicaciones_Estandar("txtUbicacion_KeyDown");

                if (leer.Leer())
                {
                    CargaDatosUbicacion();
                }
                else
                {
                    IniciaBotones(true, false, false);
                }
            }
        }
        #endregion Eventos

        #region Validacion
        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtUbicacion.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Favor de capturar el nombre de Posición.");
                txtUbicacion.Focus();
            }

            if (bRegresa && txtDescripcion.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Favor de capturar la descripcion de la posición.");
                txtDescripcion.Focus();
            }

            return bRegresa;
        }
        #endregion Validacion

        #region Guardar_Cancelar_Informacion
        private void Guardar_Informacion(int Opcion)
        {
            string sSql = "";
            string sMsj = "Ocurrió un error al guardar la información";

            if (Opcion == 2)
            {
                sMsj = "Ocurrió un error al cancelar la información";
            }

            if (ValidaDatos())
            {
                if (cnn.Abrir())
                {
                    sSql = string.Format(" Exec spp_Mtto_OCEN_Cat_ALMN_Ubicaciones_Estandar '{0}', '{1}', {2} ", txtUbicacion.Text, txtDescripcion.Text, Opcion);

                    if (!leer.Exec(sSql))
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "Guardar_Informacion");
                        General.msjError(sMsj);
                    }
                    else
                    {
                        cnn.CompletarTransaccion();
                        leer.Leer();
                        General.msjUser(leer.Campo("Mensaje"));
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
        #endregion Guardar_Cancelar_Informacion
    }
}
