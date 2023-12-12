using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;

namespace Dll_IGPI.Configuracion
{
    public partial class FrmTerminales : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas Consultas;
        clsAyudas Ayudas;

        public FrmTerminales()
        {
            InitializeComponent();

            cnn.SetConnectionString();
            Consultas = new clsConsultas(General.DatosConexion, IGPI.Modulo, this.Name, IGPI.Version);
            Ayudas = new clsAyudas(General.DatosConexion, IGPI.Modulo, this.Name, IGPI.Version);
        }

        private void FrmTerminales_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
            tmIMach.Enabled = true;
            tmIMach.Start();         
        }

        #region Buscar Terminal
        private void txtIdTerminal_Validating(object sender, CancelEventArgs e)
        {
            leer = new clsLeer(ref cnn);

            if (txtIdTerminal.Text.Trim() == "")
            {
                txtIdTerminal.Text = "*";
                txtIdTerminal.Enabled = false;
                IniciarToolbar(true, true, false, false);
            }
            else
            {
                leer.DataSetClase = Consultas.Terminales(txtIdTerminal.Text.Trim(), "txtIdTerminal_Validating");
                if (leer.Leer())
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
            txtIdTerminal.Text = leer.Campo("IdTerminal");
            txtNombre.Text = leer.Campo("Nombre");
            txtMac.Text = leer.Campo("MAC_Address");
            chkEsInterface.Checked = leer.CampoBool("EsDeSistema");
            txtIdTerminal.Enabled = false;
            txtNombre.Enabled = false;
            txtMac.Enabled = false;
        }

        private void txtIdTerminal_KeyDown(object sender, KeyEventArgs e)
        {
            leer = new clsLeer(ref cnn);
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.Terminales("txtId_KeyDown");
                if (leer.Leer())
                {
                    CargaDatos();
                }
            }
        }

        #endregion Buscar Terminal

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(true);
            IniciarToolbar(true, false, false, false);
            txtIdTerminal.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "";
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion
            int iEsServidor = chkEsInterface.Checked ? 1 : 0;

            if (ValidaDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    sSql = String.Format("Exec spp_Mtto_IGPI_CFGC_Terminales  @IdTerminal = '{0}', @Nombre = '{1}', @MAC_Address = '{2}', @iEsInterface = '{3}', @iOpcion = '{4}' ",
                            txtIdTerminal.Text.Trim(), txtNombre.Text.Trim(), txtMac.Text.Trim(), iEsServidor, iOpcion);

                    if (leer.Exec(sSql))
                    {
                        if (leer.Leer())
                            sMensaje = String.Format("{0}", leer.Campo("Mensaje"));

                        cnn.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al guardar la informacion. Verifique que la MAC no este asignada a otra Terminal");
                    }

                    cnn.Cerrar();
                }
                else
                {
                    General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
                }

            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones

        #region Funciones
        private void IniciarToolbar(bool bNuevo, bool bGuardar, bool bCancelar, bool bImprimir)
        {
            btnNuevo.Enabled = bNuevo;
            btnGuardar.Enabled = bGuardar;
            btnCancelar.Enabled = bCancelar;
            btnImprimir.Enabled = bImprimir;
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtIdTerminal.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese la Terminal por favor");
                txtIdTerminal.Focus();                
            }

            if (bRegresa && txtNombre.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese el Nombre por favor");
                txtNombre.Focus();                
            }

            if (bRegresa && txtMac.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese el Nombre por favor");
                txtMac.Focus();                
            }

            return bRegresa;
        }
        #endregion Funciones 

        private void tmIMach_Tick(object sender, EventArgs e)
        {
            tmIMach.Stop();
            tmIMach.Enabled = false;
            if (!IGPI.GPI_Instalado)
            {
                General.msjAviso("La Unidad conectada no puede ser configurada para GPI.");
                this.Close();
            }
        }

        private void btnGetMac_Click(object sender, EventArgs e)
        {
            txtNombre.Text = General.NombreEquipo; 
            txtMac.Text = General.MacAddress; 
        }
    }
}
