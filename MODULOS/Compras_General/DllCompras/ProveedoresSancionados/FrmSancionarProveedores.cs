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
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.Errores;

using DllFarmaciaSoft;
using DllCompras;


namespace DllCompras.ProveedoresSancionados
{
    public partial class FrmSancionarProveedores : FrmBaseExt 
    {
        private enum Cols
        {
            CodigoEAN = 1, IdClaveSSA = 2, Status = 3, Descripcion = 4, Precio = 5, 
            Descuento = 6, TasaIva = 7, Iva = 8, PrecioUnitario = 9, FechaRegistro = 10, FechaVigencia = 11
        }

        private clsConexionSQL myCnn = new clsConexionSQL(General.DatosConexion);
        private clsGrabarError myError = new clsGrabarError();
        private clsAyudas myAyuda;
        private clsConsultas myQuery;
        private clsLeer myLeer;
        private DateTime dtFechaServer = DateTime.Now;
        private DateTime dtFechaMin = DateTime.Now;        

        clsDatosCliente datosCliente;

        public FrmSancionarProveedores()
        {
            InitializeComponent();

            datosCliente = new clsDatosCliente(GnCompras.DatosApp, this.Name, ""); 

            myCnn = new clsConexionSQL(General.DatosConexion);
            myLeer = new clsLeer(ref myCnn);
            myAyuda = new clsAyudas(General.DatosConexion, GnCompras.Modulo, this.Name, GnCompras.Version);
            myQuery = new clsConsultas(General.DatosConexion, "DllCompras", this.Name, Application.ProductVersion);
        }

        private void FrmListaPreciosClaveSSA_Load(object sender, EventArgs e)
        {
            //Limpiar();
            btnNuevo_Click(null, null);
        }

        #region Buscar Proveedor

        private void txtIdProveedor_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdProveedor.Text != "")
            {
                myLeer.DataSetClase = myQuery.Proveedores(Fg.PonCeros(txtIdProveedor.Text, 4), "txtIdProveedor_Validating");

                if (myLeer.Leer())
                {
                    CargarDatos();
                    ProveedorSancionado();
                }
                else
                {
                    btnNuevo_Click(null, null);
                }
                
            }
            else
            {
                btnNuevo_Click(null, null);
            }
        }

        private void txtIdProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = myAyuda.Proveedores("txtIdProveedor_KeyDown()");

                if (myLeer.Leer())
                {
                    CargarDatos();
                }
            }
        }

        private void CargarDatos()
        {
            txtIdProveedor.Text = myLeer.Campo("IdProveedor");
            lblNombreProveedor.Text = myLeer.Campo("Nombre");
            txtIdProveedor.Enabled = false;
            InicializaToolBar(true, true, false);
        }


        #endregion Buscar Proveedor

        #region Teclas Rápidas
        private void TeclasRapidas(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.N:
                    if (btnNuevo.Enabled)
                    {
                        btnNuevo_Click(null, null);
                    }
                    break;
                case Keys.G:
                    if (btnGuardar.Enabled)
                    {
                        btnGuardar_Click(null, null);
                    }
                    break;
                case Keys.C:
                    if (btnCancelar.Enabled)
                    {
                        btnCancelar_Click(null, null);
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Botones

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            InicializaToolBar(true, false, false);
            txtIdProveedor.Focus();   
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                if (myCnn.Abrir())
                {
                    myCnn.IniciarTransaccion();

                    string sSql = string.Format("Set Dateformat YMD Exec spp_Mtto_COM_OCEN_Proveedores_Sancionados '{0}', '{1}', '{2}', '{3}', '{4}' ",
                        txtIdProveedor.Text.Trim(), DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal,
                        txtObservaciones.Text.Trim());

                    if (!myLeer.Exec(sSql))
                    {
                        myCnn.DeshacerTransaccion();
                        myError.GrabarError(General.MsjErrorAbrirConexion, "GuardarInformacion()");
                        General.msjError("Ocurrió un error al guardar la información del Producto.");
                    }
                    else
                    {
                        myCnn.CompletarTransaccion();

                        if (myLeer.Leer())
                        {
                            General.msjUser(myLeer.Campo("Mensaje"));
                            btnNuevo_Click(null, null);
                        }
                    }

                    myCnn.Cerrar();
                }
                else
                {
                    General.msjAviso(General.MsjErrorAbrirConexion);
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

        #region Funciones y Procedimientos Privados 
        private void InicializaToolBar(bool bNuevo, bool bGuardar, bool bCancelar )
        {
            btnNuevo.Enabled = bNuevo;
            btnGuardar.Enabled = bGuardar;
            btnCancelar.Enabled = bCancelar;
        }

        private bool validarDatos()
        {
            bool bRegresa = true;

            if (txtIdProveedor.Text == "")
            {
                General.msjAviso("No ha capturado un Proveedor válido, verfique.");
                txtIdProveedor.Focus();
                bRegresa = false;
                return bRegresa;
            } 

            if (txtObservaciones.Text == "")
            {
                General.msjAviso("No ha capturado un Motivo válido, verifique.");
                txtObservaciones.Focus();
                bRegresa = false;
                return bRegresa;
            }

            return bRegresa;
        }

        private bool ProveedorSancionado()
        {
            bool bRegresa = true;

            myQuery.MostrarMsjSiLeerVacio = false;
            myLeer.DataSetClase = myQuery.Proveedores_Sancionados(txtIdProveedor.Text.Trim(), "ProveedorSancionado");
            myQuery.MostrarMsjSiLeerVacio = true;

            if (!myLeer.Leer())
            {
                bRegresa = false;
            }
            else
            {
                General.msjUser("El proveedor ingresado ya se encuentra sancionado. Verifique");
                txtObservaciones.Text = myLeer.Campo("Motivo");
                //txtObservaciones.Enabled = false;
                InicializaToolBar(true, false, false);
                //btnNuevo_Click(null, null);
            }
            
            return bRegresa;
        }

        #endregion Funciones y Procedimientos Privados        
        
    }
}
