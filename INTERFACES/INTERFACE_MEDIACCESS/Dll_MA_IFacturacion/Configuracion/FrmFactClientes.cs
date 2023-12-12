using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

// Implementacion de hilos 
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.ExportarDatos; 
using DllFarmaciaSoft;

using Dll_MA_IFacturacion; 

namespace Dll_MA_IFacturacion.Configuracion
{
    public partial class FrmFactClientes : FrmBaseExt 
    {
        private enum Cols_Detalle
        {
            IdEmpresa = 0, IdEstado = 1, IdSucursal = 2,
            Año = 3, Aprobacion = 4, Serie = 5, FolioInicio = 6, FolioFinal = 7, FolioUltimo = 8
        }

        private enum Cols
        {
            Año = 1, Aprobacion = 2, Serie = 3, FolioInicio = 4, FolioFinal = 5, FolioUltimo = 6 
        } 

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas query; 
        //clsListView list;

        string sIdCliente = "";
        string sNombreCliente = ""; 
        string sMensaje = "";
        bool bExisteRFC = false;

        bool bLlamadaFacturacion = false; 
        bool bClienteNuevo = false; 

        public FrmFactClientes()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, DtIFacturacion.DatosApp, this.Name); 
            Error = new clsGrabarError(General.DatosConexion, DtIFacturacion.DatosApp, this.Name); 
        }

        #region Propiedades 
        public string Cliente
        {
            get { return sIdCliente; }
        }

        public string ClienteNombre 
        {
            get { return sNombreCliente; }
        }

        public bool ClienteNuevo
        {
            get { return bClienteNuevo; } 
        }
        #endregion Propiedades

        #region Form
        private void FrmFactClientes_Load(object sender, EventArgs e)
        {
            InicializaPantalla();

            if (bLlamadaFacturacion)
            {
                txtId.Text = sIdCliente;
                txtId_Validating(null, null); 
            }

        } 
        #endregion Form 

        #region Botones 
        private void InicializaToolBar(bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;

            if (bLlamadaFacturacion) 
            {
                btnCancelar.Enabled = false;
                btnImprimir.Enabled = false;
            }
        }

        private void InicializaPantalla()
        {
            bExisteRFC = false;  
            Fg.IniciaControles();
            InicializaToolBar(false, false, false);
            txtId.Focus(); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializaPantalla(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validaDatos())
            {
                if (GuardarInformacion(1))
                {
                    if (bLlamadaFacturacion)
                    {
                        this.Close();
                    }
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

        #region Funciones y Procedimientos Publicos 
        public bool MostrarClientes(string IdCliente)
        {
            bLlamadaFacturacion = true;
            sIdCliente = IdCliente; 
            this.ShowDialog(); 

            return bClienteNuevo; 
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados
        private bool GuardarInformacion(int Tipo)
        {
            bool bRegresa = false;
            sMensaje = "Ocurrió un error al guardar la configuración de factura electrónica."; 

            if (!cnn.Abrir())
            {
                General.msjErrorAlAbrirConexion(); 
            } 
            else
            {
                cnn.IniciarTransaccion();

                //// Proceso de configuracion 
                bRegresa = GuardarCliente(Tipo); 

                if (!bRegresa)
                {
                    Error.GrabarError(leer, "GuardarInformacion");
                    cnn.DeshacerTransaccion();
                    General.msjError(sMensaje); 
                }
                else
                {
                    bRegresa = true; 
                    bClienteNuevo = true; 
                    txtId.Text = sIdCliente;
                    sNombreCliente = txtRazonSocial.Text.Trim(); 
                    cnn.CompletarTransaccion();
                    General.msjUser(sMensaje);

                    if (!bLlamadaFacturacion)
                    {
                        InicializaPantalla();
                    }
                }

                cnn.Cerrar(); 
            }

            return bRegresa; 
        }

        private bool GuardarCliente(int Tipo)
        {
            bool bRegresa = false; 
            string sSql = string.Format("");

            sSql = string.Format("Exec spp_Mtto_FACT_CFD_Clientes @IdCliente = '{0}', @Nombre = '{1}', @RFC = '{2}', " +
                " @Pais = '{3}', @Estado = '{4}', @Municipio = '{5}', @Localidad = '{6}', @Colonia = '{7}', " +
                " @Calle = '{8}', @NoExterior = '{9}', @NoInterior = '{10}', @CodigoPostal = '{11}', @Referencia = '{12}', @Opcion = '{13}'  ",
                txtId.Text.Trim(), txtRazonSocial.Text.Trim(), txtRFC.Text.Trim(), txtD_Pais.Text.Trim(), txtD_Estado.Text.Trim(), txtD_Municipio.Text.Trim(),
                txtD_Localidad.Text.Trim(), txtD_Colonia.Text.Trim(), txtD_Calle.Text.Trim(),
                txtD_NoExterior.Text.Trim(), txtD_NoInterior.Text.Trim(), txtD_CodigoPostal.Text.Trim(), txtD_Referencia.Text.Trim(), Tipo);

            bExisteRFC = false; 
            if (!leer.Exec(sSql))
            {
            }
            else
            {
                if (leer.Leer())
                {
                    sIdCliente = leer.Campo("Folio");
                    if (leer.CampoInt("Codigo") == 0)
                    {
                        bRegresa = true;
                        sMensaje = leer.Campo("Mensaje"); 
                    }
                    else
                    {
                        bExisteRFC = true;
                        sMensaje = leer.Campo("Mensaje"); 
                    }
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
                General.msjUser("La Razón social no debe ser vacia, verifique."); 
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
                if (!isRFC(txtRFC.Text))
                {
                    bRegresa = false;
                    General.msjError("Formato de RFC inválido, vefirique."); 
                }
            }

            if (bExisteRFC)
            { 
            }

            if (bRegresa)
            {
                bRegresa = validaDatosDomicilio(); 
            }

            return bRegresa;
        }

        public static bool isRFC(string tsInputRFC)
        {
            bool bRegresa = false; 
            string 
            lsPatron = @"^[A-ZÑ&] {3,4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9] [A-Z,0-9][0-9A]$";
            lsPatron = "[A-Z,Ñ,&]{3,4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9]?[A-Z,0-9]?[0-9,A-Z]?"; 

            Regex loRE = new Regex(lsPatron); 
            bRegresa = loRE.IsMatch(tsInputRFC); 

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

        private bool CargarDatos()
        {
            bool bRegresa = false;
            clsLeer datosLeer = new clsLeer(ref cnn);
            string sSql = string.Format("Select * From FACT_CFD_Clientes (NoLock) Where IdCliente = '{0}' ", Fg.PonCeros(txtId.Text, 6)); 
            
            
            if (!datosLeer.Exec(sSql))
            {
                Error.GrabarError(datosLeer, "");
                General.msjError("Ocurrió un error al obtener la información del Cliente."); 
            }
            else
            {
                if (!datosLeer.Leer())
                {
                    General.msjUser("Clave de Cliente no encontrada, verifique.");
                }
                else 
                {
                    bRegresa = true;
                    InicializaToolBar(true, true, false); 
                    txtId.Enabled = false;
                    txtRazonSocial.Enabled = false;
                    txtRFC.Enabled = false; 

                    txtId.Text = datosLeer.Campo("IdCliente");
                    txtRazonSocial.Text = datosLeer.Campo("Nombre");
                    txtRFC.Text = datosLeer.Campo("RFC");
                    txtD_Pais.Text = datosLeer.Campo("Pais");
                    txtD_Estado.Text = datosLeer.Campo("Estado");
                    txtD_Municipio.Text = datosLeer.Campo("Municipio");
                    txtD_Localidad.Text = datosLeer.Campo("Localidad");
                    txtD_Colonia.Text = datosLeer.Campo("Colonia");
                    txtD_Calle.Text = datosLeer.Campo("Calle");
                    txtD_NoExterior.Text = datosLeer.Campo("NoExterior");
                    txtD_NoInterior.Text = datosLeer.Campo("NoInterior");
                    txtD_Referencia.Text = datosLeer.Campo("Referencia");
                    txtD_CodigoPostal.Text = datosLeer.Campo("CodigoPostal");
                }
            }

            return bRegresa; 
        }
        #endregion Funciones y Procedimientos Privados

        #region Eventos 
        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            if (txtId.Text.Trim() == "" || txtId.Text.Trim() == "*")
            {
                txtId.Text = "*";
                txtId.Enabled = false;
                InicializaToolBar(true, false, false); 
            }
            else
            {
                if ( !CargarDatos()) 
                {
                    e.Cancel = true; 
                }
            }
        }
        #endregion Eventos  

    }
}
