#region USING
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Data.Odbc;
using System.Windows.Forms;
using System.Threading;
using System.ServiceModel;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;

using DllFarmaciaSoft;
using Dll_IFacturacion.Configuracion;
#endregion USING

namespace Dll_IFacturacion.XSA
{
    public partial class FrmObservaciones : FrmBaseExt
    {
        private bool bInformacionValida = false; 
        public bool FacturacionDeRemisiones = false; 

        public string sIdCliente = "";
        public string sIdCliente_Nombre = "";
        public string sIdEstablecimiento = "";
        public string sIdEstablecimiento_Receptor = "";

        public string sReferencia = "";
        public string sReferencia_02 = "";
        public string sReferencia_03 = "";
        public string sReferencia_04 = "";
        public string sReferencia_05 = "";

        public string sObservaciones_01 = "";
        public string sObservaciones_02 = "";
        public string sObservaciones_03 = ""; 

        public int iTipoDocumento = 0;
        public int iTipoInsumo = 0; 
        public string sRubroFinanciamiento = "";
        public string sFuenteFinanciamiento = "";
        public bool bEsNotaDeCredito = false;


        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas Consultas;
        clsAyudas Ayudas;


        DataSet dtsInformacionRemision = new DataSet();
        clsLeer leerInformacion;
        bool bCierreAutomatico = false;
        private bool bEsRemisionConcentrada = false;
        Size tamForm;
        Size tamForm_PAC;
        Size TamNotaDeCredito;

        public FrmObservaciones():this(new DataSet(), false, false) 
        { 
        }

        public FrmObservaciones(DataSet InformacionRemision, bool CierreAutomatico, bool EsRemisionConcentrada )
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            leerInformacion = new clsLeer();

            leerInformacion.DataSetClase = InformacionRemision;
            bCierreAutomatico = CierreAutomatico;
            bEsRemisionConcentrada = EsRemisionConcentrada;

            Consultas = new clsConsultas(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);
            Ayudas = new clsAyudas(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);  

            //////tamForm = new Size(585, 440);
            //////tamForm_PAC = new Size(585, 585);
            

            //////if (DtIFacturacion.PAC_Informacion.PAC == PACs_Timbrado.Tralix)
            //////{
            //////    this.Size = tamForm; 
            //////    FrameIdentificadorDeDocumento.Top += 20;
            //////    FrameIdentificadorDeDocumento.Visible = false; 
            //////}
            //////else 
            //////{
            //////    this.Size = tamForm_PAC; 
            //////}
        }

        #region Propiedades 
        public bool InformacionValida
        {
            get { return bInformacionValida; }
        }
        #endregion Propiedades 

        #region Form 
        private void FrmObservaciones_Load(object sender, EventArgs e)
        {
            if (bEsNotaDeCredito)
            {
                TamNotaDeCredito = new Size(590, 644);
                this.Size = TamNotaDeCredito;
            }

            CargarInformacionInicial(true);
        }
        #endregion Form

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            CargarInformacionInicial(false); 
        }

        private void CargarInformacionInicial( bool ValidarCierreAutomatico )
        {
            if(sIdCliente_Nombre != "")
            {
                groupBox_02_EstablecimientoReceptor.Text += string.Format(" : {0}", sIdCliente_Nombre); 
            } 

            txtClave_01_Emisor.Text = sIdEstablecimiento;
            txtClave_02_Receptor.Text = sIdEstablecimiento_Receptor; 

            txtObservaciones_01.Text = sObservaciones_01;
            txtObservaciones_02.Text = sObservaciones_02;
            txtObservaciones_03.Text = sObservaciones_03;
            txtReferencia.Text = sReferencia;
            txtReferencia_02.Text = sReferencia_02;
            txtReferencia_03.Text = sReferencia_03;
            txtReferencia_04.Text = sReferencia_04;
            txtReferencia_05.Text = sReferencia_05;


            //rdoVenta.Checked = false; 
            //if (iTipoDocumento == 1 || iTipoDocumento == 3)
            //{
            //    rdoVenta.Checked = true;
            //}

            rdoVenta.Checked = (iTipoDocumento == 1 || iTipoDocumento == 3 || iTipoDocumento == 4 || iTipoDocumento == 5) ? true : false;
            rdoAdministracion.Checked = (iTipoDocumento == 2 || iTipoDocumento == 6) ? true : false;
            rdoMaterialDeCuracion.Checked = iTipoInsumo == 1 ? true : false;
            rdoMedicamento.Checked = iTipoInsumo == 2 ? true : false;
            rdoInsumo_Ambos.Checked = iTipoInsumo == 0 ? true : false;
            rdoInsumo_Ambos.Enabled = false; 

            txtIdFuenteFinanciamiento.Text = sRubroFinanciamiento;
            txtSegmentoFinanciamiento.Text = sFuenteFinanciamiento;

            if (sRubroFinanciamiento != "")
            {
                txtIdFuenteFinanciamiento_Validating(null, null);
                if (sFuenteFinanciamiento != "")
                {
                    txtSegmentoFinanciamiento_Validating(null, null);
                }
            }

            if(txtClave_01_Emisor.Text.Trim() != "")
            {
                txtClave_01_Emisor_Validating(null, null);
            }

            if(txtClave_02_Receptor.Text.Trim() != "")
            {
                txtClave_02_Receptor_Validating(null, null); 
            }


            leerInformacion.RegistroActual = 1; 
            if (leerInformacion.Leer())
            {
                txtObservaciones_01.Text = sObservaciones_01 == "" ? leerInformacion.Campo("InformacionDeContrato") : sObservaciones_01;
                txtObservaciones_02.Text = sObservaciones_02 == "" ? leerInformacion.Campo("InformacionAdicional") : sObservaciones_02;
                txtObservaciones_03.Text = sObservaciones_03 == "" ? leerInformacion.Campo("FarmaciasRemision") : sObservaciones_03;

                txtReferencia.Text = leerInformacion.Campo("Referencia"); 
                //txtReferencia.Text = txtReferencia.Text == "" ? sReferencia : txtReferencia.Text;
                txtReferencia_02.Text = sReferencia_02 == "" ? leerInformacion.Campo("Referencia_02") : sReferencia_02;
                txtReferencia_03.Text = sReferencia_03 == "" ? leerInformacion.Campo("Referencia_03") : sReferencia_03;
                txtReferencia_04.Text = sReferencia_04 == "" ? leerInformacion.Campo("Referencia_04") : sReferencia_04;
                txtReferencia_05.Text = sReferencia_05 == "" ? leerInformacion.Campo("Referencia_05") : sReferencia_05;


                if (!bEsRemisionConcentrada)
                {
                    txtReferencia.Text = "RM" + leerInformacion.Campo("FolioRemision");
                    txtReferencia_02.Text = leerInformacion.Campo("ReferenciaDePedido");

                    txtReferencia_04.Text = leerInformacion.Campo("Referencia_Remisiones");
                    txtReferencia_05.Text = leerInformacion.Campo("Referencia_Facturas");
                }

                // Informacion requerida por SSH 20190805.1545  Jesús Díaz 
                txtReferencia_03.Text = leerInformacion.Campo("ReferenciaDeAccion");


                txtObservaciones_01.ReadOnly = leerInformacion.Campo("InformacionDeContrato") == "" ? false : true;
                txtObservaciones_02.ReadOnly = leerInformacion.Campo("InformacionAdicional") == "" ? false : true;
                txtObservaciones_03.ReadOnly = leerInformacion.Campo("FarmaciasRemision") == "" ? false : true;

                txtReferencia.ReadOnly = txtReferencia.Text == "" ? false : true;
                txtReferencia_02.ReadOnly = txtReferencia_02.Text == "" ? false : true;
                txtReferencia_03.ReadOnly = txtReferencia_03.Text == "" ? false : true;
                txtReferencia_04.ReadOnly = txtReferencia_04.Text == "" ? false : true;
                txtReferencia_05.ReadOnly = txtReferencia_05.Text == "" ? false : true;



                txtIdFuenteFinanciamiento.Text = leerInformacion.Campo("IdFuenteFinanciamiento");
                lblRubro.Text = leerInformacion.Campo("FuenteFinanciamiento");
                txtIdFuenteFinanciamiento.ReadOnly = txtIdFuenteFinanciamiento.Text == "" ? false : true; 


                txtSegmentoFinanciamiento.Text = leerInformacion.Campo("IdFinanciamiento");
                lblConcepto.Text = leerInformacion.Campo("Financiamiento");
                txtSegmentoFinanciamiento.ReadOnly = txtSegmentoFinanciamiento.Text == "" ? false : true;


                iTipoDocumento = leerInformacion.CampoInt("TipoDeRemision");
                iTipoInsumo = leerInformacion.CampoInt("TipoInsumo");

                //rdoVenta.Checked = iTipoDocumento == 1 ? true : false;
                rdoVenta.Checked = (iTipoDocumento == 1 || iTipoDocumento == 3 || iTipoDocumento == 4 || iTipoDocumento == 5) ? true : false;
                rdoAdministracion.Checked = (iTipoDocumento == 2 || iTipoDocumento == 6) ? true : false;
                rdoMaterialDeCuracion.Checked = iTipoInsumo == 1 ? true : false;
                rdoMedicamento.Checked = iTipoInsumo == 2 ? true : false;

                if (iTipoInsumo == 0)
                {
                    rdoInsumo_Ambos.Checked = true; 
                    rdoMaterialDeCuracion.Checked = false;
                    rdoMedicamento.Checked = false;
                }


                rdoVenta.Enabled = iTipoDocumento == 0 ? true : false;
                rdoAdministracion.Enabled = iTipoDocumento == 0 ? true : false;

                rdoInsumo_Ambos.Enabled = false;
                rdoMaterialDeCuracion.Enabled = false; // iTipoInsumo == 0 ? false : false;
                rdoMedicamento.Enabled = false; // iTipoInsumo == 0 ? false : false;

            }

            if (ValidarCierreAutomatico)
            {
                if (bCierreAutomatico)
                {
                    GuardarInformacion(); 
                }
            }
        }
        
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                GuardarInformacion();
            }
        }
        #endregion Botones 

        #region Validacion de datos 
        private bool validarDatos()
        {
            bool bRegresa = true;
            bInformacionValida = false; 

            if (bRegresa && DtIFacturacion.Factura_Requiere_FuenteDeFinanciamiento)
            {
                if ( txtIdFuenteFinanciamiento.Text.Trim() == "" || txtSegmentoFinanciamiento.Text.Trim() =="")
                {
                    bRegresa = false;
                    General.msjError("No ha especificado la información de Fuente de Financiamiento, verique.");
                }
            }

            if (bRegresa && DtIFacturacion.Factura_Requiere_TipoDocto)
            {
                if (!rdoVenta.Checked && !rdoAdministracion.Checked)
                {
                    bRegresa = false;
                    General.msjError("No ha especificado el Tipo de Documento ( Venta ó Administración), verique.");
                }
            }

            if (bRegresa && DtIFacturacion.Factura_Requiere_TipoInsumo)
            {
                if (!FacturacionDeRemisiones)
                {
                    if (!rdoMedicamento.Checked && !rdoMaterialDeCuracion.Checked)
                    {
                        bRegresa = false;
                        General.msjError("No ha especificado el Tipo de Documento ( Venta ó Administración), verique.");
                    }
                }
            }


            /// Validar todos los campos obligatorios 
            bInformacionValida = bRegresa;

            return bRegresa; 
        }
        private void GuardarInformacion()
        {
            sIdEstablecimiento = txtClave_01_Emisor.Text;
            sIdEstablecimiento_Receptor = txtClave_02_Receptor.Text;

            sObservaciones_01 = txtObservaciones_01.Text;
            sObservaciones_02 = txtObservaciones_02.Text;
            sObservaciones_03 = txtObservaciones_03.Text;
            sReferencia = txtReferencia.Text;
            sReferencia_02 = txtReferencia_02.Text;
            sReferencia_03 = txtReferencia_03.Text;
            sReferencia_04 = txtReferencia_04.Text;
            sReferencia_05 = txtReferencia_05.Text;


            iTipoDocumento = 0;
            if (rdoVenta.Checked)
            {
                iTipoDocumento = 1;
            }

            if (rdoAdministracion.Checked)
            {
                iTipoDocumento = 2;
            }

            iTipoInsumo = 0;
            if (rdoMaterialDeCuracion.Checked)
            {
                iTipoInsumo = 1;
            }

            if (rdoMedicamento.Checked)
            {
                iTipoInsumo = 2;
            }

            sRubroFinanciamiento = txtIdFuenteFinanciamiento.Text;
            sFuenteFinanciamiento = txtSegmentoFinanciamiento.Text;

            if (bCierreAutomatico)
            {
                this.Close();
            }
            else
            {
                this.Hide();
            }
        }
        #endregion Validacion de datos 

        #region Fuentes de Financiamiento 
        private void txtIdFuenteFinanciamiento_TextChanged(object sender, EventArgs e)
        {
            lblRubro.Text = "";
            txtSegmentoFinanciamiento.Text = "";
            lblConcepto.Text = "";
            lblNombreCliente.Text = "";
            lblNombreSubCliente.Text = "";
            lblNumeroDeContrato.Text = "";
            lblNumeroDeLicitacion.Text = "";
            lblTipoDeFuente.Text = "";
            lblEsDiferencial.Text = ""; 
        }

        private void txtIdFuenteFinanciamiento_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdFuenteFinanciamiento.Text.Trim() != "" && !txtIdFuenteFinanciamiento.ReadOnly)
            {
                leer.DataSetClase = Consultas.FuentesDeFinanciamiento_Encabezado(txtIdFuenteFinanciamiento.Text.Trim(), "txtIdFuenteFinanciamiento_Validating"); 
                if (!leer.Leer())
                {
                    txtIdFuenteFinanciamiento.Text = "";
                    lblRubro.Text = "";
                    txtIdFuenteFinanciamiento.Focus();   
                }
                else
                {
                    CargarDatosRubro();
                }
            }
        }

        private void txtIdFuenteFinanciamiento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.FuentesDeFinanciamiento_Encabezado("txtIdFuenteFinanciamiento_KeyDown"); 
                if (leer.Leer())
                {
                    CargarDatosRubro();
                }
            }
        }

        private void CargarDatosRubro()
        {
            txtIdFuenteFinanciamiento.Text = leer.Campo("IdFuenteFinanciamiento");
            lblRubro.Text = leer.Campo("Estado");

            lblNombreCliente.Text = leer.Campo("Cliente");
            lblNombreSubCliente.Text = leer.Campo("SubCliente");
            lblNumeroDeContrato.Text = leer.Campo("NumeroDeContrato");
            lblNumeroDeLicitacion.Text = leer.Campo("NumeroDeLicitacion");

            lblTipoDeFuente.Text = leer.Campo("EsParaExcedente_Descripcion");
            lblEsDiferencial.Text = leer.Campo("EsDiferencial_Descripcion");


            if (leer.Campo("Status") == "C")
            {
                General.msjUser("El Rubro seleccionado se encuentra cancelado. Verifique");
                txtIdFuenteFinanciamiento.Text = "";
                lblRubro.Text = "";
            }
        }

        private void txtSegmentoFinanciamiento_TextChanged(object sender, EventArgs e)
        {
            lblConcepto.Text = ""; 
        }

        private void txtSegmentoFinanciamiento_Validating(object sender, CancelEventArgs e)
        {
            if (txtSegmentoFinanciamiento.Text.Trim() != "" && !txtSegmentoFinanciamiento.ReadOnly)
            {
                leer.DataSetClase = Consultas.FuentesDeFinanciamiento_Detalle(txtIdFuenteFinanciamiento.Text.Trim(), txtSegmentoFinanciamiento.Text.Trim(), "txtIdFuenteFinanciamiento_Validating"); 
                if (!leer.Leer())
                {
                    txtSegmentoFinanciamiento.Text = "";
                    lblConcepto.Text = "";
                    txtSegmentoFinanciamiento.Focus();
                }
                else
                {
                    CargarDatosConcepto();
                }
            }
        }

        private void txtSegmentoFinanciamiento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.FuentesDeFinanciamiento_Detalle("txtPrograma_KeyDown", txtIdFuenteFinanciamiento.Text.Trim());
                if (leer.Leer())
                {
                    CargarDatosConcepto();
                }
            }       
        }

        private void CargarDatosConcepto()
        {
            txtSegmentoFinanciamiento.Text = leer.Campo("IdFinanciamiento");
            lblConcepto.Text = leer.Campo("Financiamiento");

            if (leer.Campo("Status") == "C")
            {
                General.msjUser("El Financiamiento seleccionado se encuentra cancelado. Verifique");
                txtSegmentoFinanciamiento.Text = "";
                lblConcepto.Text = "";
            }
        }
        #endregion Fuentes de Financiamiento

        #region Establecimientos 
        private void txtClave_01_Emisor_TextChanged( object sender, EventArgs e )
        {
            lblEstablecimiento_01_Emisor.Text = ""; 
            lblDomicilio_01_Emisor.Text = "";
        }

        private void txtClave_01_Emisor_Validating( object sender, CancelEventArgs e )
        {
            if(txtClave_01_Emisor.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.CFDI_Establecimientos_Emisor(
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtClave_01_Emisor.Text, "txtClave_01_Emisor_Validating");
                if(!leer.Leer())
                {
                }
                else
                {
                    CargarInfo_Establecimiento();
                }
            }
        }

        private void txtClave_01_Emisor_KeyDown( object sender, KeyEventArgs e )
        {
            if(e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.CFDI_Establecimientos_Emisor(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, "txtClave_01_Emisor_KeyDown");
                if(leer.Leer())
                {
                    CargarInfo_Establecimiento();
                }
            }
        }

        private void CargarInfo_Establecimiento()
        {
            string sDomicilio = ""; 

            txtClave_01_Emisor.Text = leer.Campo("IdEstablecimiento");
            lblEstablecimiento_01_Emisor.Text = leer.Campo("NombreEstablecimiento");

            //txtId.Text = leer.Campo("IdEstablecimiento");
            //txtNombreEstablecimiento.Text = leer.Campo("NombreEstablecimiento");
            //txtD_Pais.Text = leer.Campo("Pais");
            //txtD_Estado.Text = leer.Campo("Estado");
            //txtD_Municipio.Text = leer.Campo("Municipio");
            //txtD_Localidad.Text = leer.Campo("Localidad");
            //txtD_Colonia.Text = leer.Campo("Colonia");
            //txtD_Calle.Text = leer.Campo("Calle");
            //txtD_NoExterior.Text = leer.Campo("NoExterior");
            //txtD_NoInterior.Text = leer.Campo("NoInterior");
            //txtD_Referencia.Text = leer.Campo("Referencia");
            //txtD_CodigoPostal.Text = leer.Campo("CodigoPostal");

            sDomicilio = string.Format("{0}, NO. {1}", leer.Campo("Calle"), leer.Campo("NoExterior"));
            if(leer.Campo("NoInterior") == "")
            {
                sDomicilio += string.Format(", "); 
            }
            else 
            {
                sDomicilio += string.Format(", " + "INT. {0}", leer.Campo("NoInterior"));
            }
            sDomicilio += "\n";

            sDomicilio += string.Format("{0}, \n", leer.Campo("Colonia"));
            sDomicilio += string.Format("C.P. {0}, \n", leer.Campo("CodigoPostal"));
            sDomicilio += string.Format("{0}, \n", leer.Campo("Municipio"));
            sDomicilio += string.Format("{0}, \n", leer.Campo("Estado"));
            sDomicilio += string.Format("{0}, \n", leer.Campo("Pais"));

            lblDomicilio_01_Emisor.Text = sDomicilio.ToUpper(); 
        }

        private void txtClave_02_Receptor_TextChanged( object sender, EventArgs e )
        {
            lblEstablecimiento_02_Receptor.Text = "";
            lblDomicilio_02_Receptor.Text = "";
        }

        private void txtClave_02_Receptor_Validating( object sender, CancelEventArgs e )
        {
            if(txtClave_02_Receptor.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.CFDI_Clientes_Establecimientos(
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sIdCliente, txtClave_02_Receptor.Text, "txtClave_02_Receptor");
                if(!leer.Leer())
                { 
                }
                else 
                {
                    CargarInfo_EstablecimientoReceptor();
                }
            }
        }

        private void txtClave_02_Receptor_KeyDown( object sender, KeyEventArgs e )
        {
            if(e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.CFDI_Clientes_Establecimientos(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, 
                sIdCliente, "txtId_KeyDown");
                if(leer.Leer())
                {
                    CargarInfo_EstablecimientoReceptor();
                }
            }
        }

        private void CargarInfo_EstablecimientoReceptor()
        {
            string sDomicilio = "";

            txtClave_02_Receptor.Text = leer.Campo("IdEstablecimiento");
            lblEstablecimiento_02_Receptor.Text = leer.Campo("NombreEstablecimiento");

            //txtId.Text = leer.Campo("IdEstablecimiento");
            //txtNombreEstablecimiento.Text = leer.Campo("NombreEstablecimiento");
            //txtD_Pais.Text = leer.Campo("Pais");
            //txtD_Estado.Text = leer.Campo("Estado");
            //txtD_Municipio.Text = leer.Campo("Municipio");
            //txtD_Localidad.Text = leer.Campo("Localidad");
            //txtD_Colonia.Text = leer.Campo("Colonia");
            //txtD_Calle.Text = leer.Campo("Calle");
            //txtD_NoExterior.Text = leer.Campo("NoExterior");
            //txtD_NoInterior.Text = leer.Campo("NoInterior");
            //txtD_Referencia.Text = leer.Campo("Referencia");
            //txtD_CodigoPostal.Text = leer.Campo("CodigoPostal");

            sDomicilio = string.Format("{0}, NO. {1}", leer.Campo("Calle"), leer.Campo("NoExterior"));
            if(leer.Campo("NoInterior") == "")
            {
                sDomicilio += string.Format(", ");
            }
            else
            {
                sDomicilio += string.Format(", " + "INT. {0}", leer.Campo("NoInterior"));
            }
            sDomicilio += "\n";

            sDomicilio += string.Format("{0}, \n", leer.Campo("Colonia"));
            sDomicilio += string.Format("C.P. {0}, \n", leer.Campo("CodigoPostal"));
            sDomicilio += string.Format("{0}, \n", leer.Campo("Municipio"));
            sDomicilio += string.Format("{0}, \n", leer.Campo("Estado"));
            sDomicilio += string.Format("{0}, \n", leer.Campo("Pais"));
            
            lblDomicilio_02_Receptor.Text = sDomicilio.ToUpper();
        }
        #endregion Establecimientos 
    }
}
