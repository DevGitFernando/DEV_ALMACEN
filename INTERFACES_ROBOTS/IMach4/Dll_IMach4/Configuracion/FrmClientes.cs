using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;

namespace Dll_IMach4.Configuracion
{
    public partial class FrmClientes : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas Consultas;
        clsAyudas Ayuda;

        public FrmClientes()
        {
            InitializeComponent();

            cnn.SetConnectionString();
            Consultas = new clsConsultas(General.DatosConexion, IMach4.Modulo, this.Name, IMach4.Version);
            Ayuda = new clsAyudas(General.DatosConexion, IMach4.Modulo, this.Name, IMach4.Version);
        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
            tmIMach.Enabled = true;
            tmIMach.Start();         
        }

        #region Buscar Cliente 
        private void txtIdCliente_Validating(object sender, CancelEventArgs e)
        {
            leer = new clsLeer(ref cnn);

            if (txtIdCliente.Text.Trim() == "" || txtIdCliente.Text.Trim() == "*")
            {
                txtIdCliente.Text = "*";
                //txtIdCliente.Enabled = false;
                txtIdCliente.ReadOnly = true;
                IniciarToolbar(true, true, false, false);
            }
            else
            {
                leer.DataSetClase = Consultas.Clientes(txtIdCliente.Text.Trim(), "txtIdCliente_Validating"); 
                if (leer.Leer()) 
                { 
                    CargarDatosCliente(); 
                } 
                else 
                {
                    btnNuevo_Click(null, null); 
                }
            }
        }

        private void CargarDatosCliente()
        {
            //Se hace de esta manera para la ayuda.
            txtIdCliente.Text = leer.Campo("IdCliente");

            txtIdEstado.Text = leer.Campo("IdEstado");
            lblEstado.Text = leer.Campo("Estado"); 

            txtIdFarmacia.Text = leer.Campo("IdFarmacia"); 
            lblFarmacia.Text = leer.Campo("Farmacia"); 

            txtIdCliente.Enabled = false; 
            txtIdFarmacia.Enabled = false; 
        } 
        #endregion Buscar Cliente 

        #region Buscar Estado
        private void txtIdEstado_Validating(object sender, CancelEventArgs e)
        {
            leer = new clsLeer(ref cnn);

            if (txtIdEstado.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Estados(txtIdEstado.Text, "txtIdEstado_Validating");
                if (!leer.Leer())
                {
                    btnNuevo_Click(null, null);
                }
                else
                {
                    CargarDatosEstado();
                }
            }
        }

        private void txtIdEstado_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtIdEstado_TextChanged(object sender, EventArgs e)
        {
            lblEstado.Text = "";
            txtIdFarmacia.Text = "";
            lblFarmacia.Text = ""; 
        }

        private void CargarDatosEstado()
        {
            //Se hace de esta manera para la ayuda.
            txtIdEstado.Text = leer.Campo("IdEstado");
            lblEstado.Text = leer.Campo("Estado");

            txtIdEstado.Enabled = false;
        }

        #endregion Buscar Estado

        #region Buscar Farmacia 
        private void txtIdFarmacia_Validating(object sender, CancelEventArgs e)
        {
            leer = new clsLeer(ref cnn);

            if (txtIdFarmacia.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Farmacias(txtIdEstado.Text, txtIdFarmacia.Text.Trim(), "txtIdCliente_Validating");
                if (!leer.Leer())
                {
                    txtIdFarmacia.Text = "";
                    e.Cancel = true; 
                }
                else
                {
                    CargarDatosFarmacia();
                }
            }
        }

        private void txtIdFarmacia_TextChanged(object sender, EventArgs e)
        {
            lblFarmacia.Text = ""; 
        }

        private void CargarDatosFarmacia()
        {
            //Se hace de esta manera para la ayuda.
            txtIdFarmacia.Text = leer.Campo("IdFarmacia");
            lblFarmacia.Text = leer.Campo("Farmacia");

            txtIdFarmacia.Enabled = false;
        }

        private void txtIdFarmacia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Farmacias("txtId_KeyDown", txtIdEstado.Text);

                if (leer.Leer())
                {
                    CargarDatosFarmacia();
                }
            }
        }
        #endregion Buscar Farmacia 

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(true);
            txtIdCliente.ReadOnly = false;
            txtIdCliente.Focus();
            IniciarToolbar(true, false, false, false);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "";
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion

            if (ValidaDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    sSql = String.Format("Exec spp_Mtto_IMach_CFGC_Clientes '{0}', '{1}', '{2}', '{3}' ",
                            txtIdCliente.Text.Trim(), txtIdEstado.Text, txtIdFarmacia.Text.Trim(), iOpcion);

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
                        General.msjError("Ocurrió un error al guardar la informacion. Verifique que la Farmacia no este asignada a otro Cliente");
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

            if (txtIdCliente.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese el IdCliente por favor");
                txtIdCliente.Focus();                
            }

            if ( bRegresa && lblFarmacia.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese la Farmacia por favor");
                txtIdFarmacia.Focus();                
            }
            
            return bRegresa;
        }
        #endregion Funciones 

        private void tmIMach_Tick(object sender, EventArgs e)
        {
            tmIMach.Stop();
            tmIMach.Enabled = false;
            if (!IMach4.Mach4Instalado)
            {
                General.msjAviso("La Unidad conectada no puede ser configurada para Mach4."); 
                this.Close(); 
            }
        }

    }
}
