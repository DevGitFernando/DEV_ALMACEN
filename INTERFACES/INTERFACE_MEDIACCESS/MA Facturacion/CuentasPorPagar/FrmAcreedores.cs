using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.Errores;

using DllFarmaciaSoft;

namespace MA_Facturacion.CuentasPorPagar
{
    public partial class FrmAcreedores : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas Consultas;
        clsAyudas Ayudas;

        string sFolio = "";
        string sMensaje = "";

        public FrmAcreedores()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayudas = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Error = new clsGrabarError(General.DatosConexion, General.DatosApp, this.Name); 
        }

        private void FrmAcreedores_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializaPantalla();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bContinua = false;

            if (validaDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    bContinua = GuardarInformacion(1);

                    if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                    {
                        txtId.Text = sFolio;
                        cnn.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP                        
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        //Error.GrabarError(leer, "btnGuardar_Click");
                        cnn.DeshacerTransaccion();
                        General.msjError("Ocurrió un error al guardar la Información.");

                    }

                    cnn.Cerrar();
                }
                else
                {
                    General.msjAviso(General.MsjErrorAbrirConexion);
                }

            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            bool bContinua = false;

            if (validaDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    bContinua = GuardarInformacion(2);

                    if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                    {
                        txtId.Text = sFolio;
                        cnn.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP                        
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        //Error.GrabarError(leer, "btnGuardar_Click");
                        cnn.DeshacerTransaccion();
                        General.msjError("Ocurrió un error al guardar la Información.");

                    }

                    cnn.Cerrar();
                }
                else
                {
                    General.msjAviso(General.MsjErrorAbrirConexion);
                }

            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones

        #region Funciones
        private void InicializaToolBar(bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;           
        }

        private void InicializaPantalla()
        {           
            Fg.IniciaControles(this, true);
            lblCancelado.Visible = false;
            InicializaToolBar(false, false, false);
            txtId.Focus();
        }

        private void CargarDatos()
        {
            txtId.Text = leer.Campo("IdAcreedor");
            txtId.Enabled = false;
            txtRazonSocial.Text = leer.Campo("Nombre");
            txtRFC.Text = leer.Campo("RFC");
            txtD_Pais.Text = leer.Campo("Pais");
            txtD_Estado.Text = leer.Campo("Estado");
            txtD_Municipio.Text = leer.Campo("Municipio");
            txtD_Localidad.Text = leer.Campo("Localidad");
            txtD_Colonia.Text = leer.Campo("Colonia");
            txtD_Calle.Text = leer.Campo("Calle");
            txtD_NoExterior.Text = leer.Campo("NoExterior");
            txtD_NoInterior.Text = leer.Campo("NoInterior");
            txtD_Referencia.Text = leer.Campo("Referencia");
            txtD_CodigoPostal.Text = leer.Campo("CodigoPostal");

            if (leer.Campo("Status") == "C")
            {
                lblCancelado.Visible = true;
                txtId.Enabled = false;
            }
            
        }
        #endregion Funciones

        #region Guardar
        private bool GuardarInformacion(int Tipo)
        {
            bool bRegresa = false;
            string sSql = string.Format("");

            sSql = string.Format(" Exec spp_Mtto_FACT_CatAcreedores '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', " +
                " '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}'  ", DtGeneral.EstadoConectado,
                txtId.Text.Trim(), txtRazonSocial.Text.Trim(), txtRFC.Text.Trim(), txtD_Pais.Text.Trim(), txtD_Estado.Text.Trim(), txtD_Municipio.Text.Trim(),
                txtD_Localidad.Text.Trim(), txtD_Colonia.Text.Trim(), txtD_Calle.Text.Trim(),
                txtD_NoExterior.Text.Trim(), txtD_NoInterior.Text.Trim(), txtD_CodigoPostal.Text.Trim(), txtD_Referencia.Text.Trim(), Tipo);

            
            if (!leer.Exec(sSql))
            {
            }
            else
            {
                if (leer.Leer())
                {                    
                    bRegresa = true;
                    sFolio = leer.Campo("Folio");
                    sMensaje = leer.Campo("Mensaje");                    
                }
            }

            return bRegresa;
        }

        private bool validaDatos()
        {
            bool bRegresa = true;

            if (bRegresa && txtRazonSocial.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("El Nombre no debe ser vacio, verifique.");
                txtRazonSocial.Focus();
            }

            if (bRegresa && txtRFC.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("El RFC no debe ser vacio, verifique.");
                txtRFC.Focus();
            }

            if (bRegresa)
            {
                bRegresa = validaDatosDomicilio();
            }

            return bRegresa;
        }

        private bool validaDatosDomicilio()
        {
            bool bRegresa = true;

            if (txtD_Pais.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("El Pais no debe ser vacio, verifique.");
                txtD_Pais.Focus();
            }

            if (bRegresa && txtD_Estado.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("El Estado no debe ser vacio, verifique.");
                txtD_Estado.Focus();
            }

            if (bRegresa && txtD_Municipio.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("El Municipio no debe ser vacio, verifique.");
                txtD_Municipio.Focus();
            }

            if (bRegresa && txtD_Localidad.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("La Localidad no debe ser vacio, verifique.");
                txtD_Localidad.Focus();
            }

            if (bRegresa && txtD_Colonia.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("La Colonia no debe ser vacio, verifique.");
                txtD_Colonia.Focus();
            }

            if (bRegresa && txtD_Calle.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("La Calle no debe ser vacio, verifique.");
                txtD_Calle.Focus();
            }

            if (bRegresa && txtD_NoExterior.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("El número exterior no debe ser vacio, verifique.");
                txtD_NoExterior.Focus();
            }

            if (bRegresa && txtD_CodigoPostal.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("El Codigo postal no debe ser vacio, verifique.");
                txtD_CodigoPostal.Focus();
            }

            return bRegresa;
        }
        #endregion Guardar

        #region Eventos
        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            if (txtId.Text.Trim() == "" )
            {
                txtId.Text = "*";
                txtId.Enabled = false;
                InicializaToolBar(true, false, false);
            }
            else
            {
                leer.DataSetClase = Consultas.Acreedores_Facturacion(DtGeneral.EstadoConectado, txtId.Text, "txtId_Validating");
                if (leer.Leer())
                {
                    CargarDatos();
                    InicializaToolBar(true, true, false);
                }
            }
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.Acreedores_Facturacion(DtGeneral.EstadoConectado, "txtId_KeyDown");

                if (leer.Leer())
                {
                    CargarDatos();
                    InicializaToolBar(true, true, false);
                }
            }
        }
        #endregion Eventos
    }
}
