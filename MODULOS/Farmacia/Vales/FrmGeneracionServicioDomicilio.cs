using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FP;
using SC_SolutionsSystem.QRCode.Codec;
using SC_SolutionsSystem.QRCode;

using DllFarmaciaSoft.QRCode;
using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos;
using DllFarmaciaSoft.Devoluciones;
using Farmacia.Procesos;
using Farmacia.Ventas;

using Dll_ValesFirmaElectronica;
using Dll_ValesFirmaElectronica.GeneracionDeFolios;

using DllFarmaciaSoft;

using Farmacia.Catalogos;
using Farmacia.Vales_Servicio_A_Domicilio; 

namespace Farmacia.Vales
{
    public partial class FrmGeneracionServicioDomicilio : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsAyudas Ayuda;
        clsConsultas Consultas;

        clsDatosCliente DatosCliente; 
        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        string sIdCliente = "";
        string sIdSubCliente = "";
        string sIdBeneficiario = "";
        string sFolioVale = ""; 
        string sFolioServicioDomicilio = "";
        bool bTieneDomicilio = false;
        string sMensaje = "";
        int iTipoSurtimiento = 0;
        bool bServicioConfirmado = false;

        public FrmGeneracionServicioDomicilio()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, false);
            Ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");

            Fg.IniciaControles();
            txtFolio.Enabled = false;
            dtpFechaRegistro.Enabled = false; 
        }

        #region Form 
        private void FrmGeneracionServicioDomicilio_Load(object sender, EventArgs e)
        {

        }

        private void FrmGeneracionServicioDomicilio_Shown(object sender, EventArgs e)
        {
            CargarInformacion_ServicioDomicilio(); 
        }

        #endregion Form

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles();
            CargarInformacion_ServicioDomicilio(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                Guardar_Informacion(); 
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir(false); 
        }

        private void btnDomicilioBeneficiario_Click(object sender, EventArgs e)
        {
            FrmBeneficiarios_Domicilios f = new FrmBeneficiarios_Domicilios();
            f.MostrarBeneficiarioDomicilio(sIdCliente, sIdSubCliente, sIdBeneficiario);
            CargarInformacion_DomicilioBeneficiario(); 
        }
        #endregion Botones 

        #region Funciones y Procedimientos Publicos
        public void MostrarRegistro_ServicioADomicilio(string IdCliente, string IdSubCliente, string IdBeneficiario, string FolioVale, string FolioServicioDomicilio)
        {
            sIdCliente = IdCliente;
            sIdSubCliente = IdSubCliente;
            sIdBeneficiario = IdBeneficiario;
            sFolioVale = FolioVale; 
            sFolioServicioDomicilio = FolioServicioDomicilio;

            txtFolio.Text = sFolioServicioDomicilio; 

            this.ShowDialog();
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private void CargarInformacion_ServicioDomicilio()
        {
            btnImprimir.Enabled = false;
            btnNuevo.Enabled = true;
            btnGuardar.Enabled = true;
            btnRegistrarSerDom.Enabled = false;

            leer.DataSetClase = Consultas.ValesServicioADomicilio(sEmpresa, sEstado, sFarmacia, sFolioVale, "CargarBeneficiarios()");
            if (leer.Leer())
            {
                sFolioServicioDomicilio = leer.Campo("FolioServicioDomicilio");
                txtFolio.Text = sFolioServicioDomicilio;
                btnImprimir.Enabled = true;
                btnNuevo.Enabled = false;
                btnGuardar.Enabled = false;
                btnRegistrarSerDom.Enabled = true;
                iTipoSurtimiento = leer.CampoInt("TipoSurtimiento");
                bServicioConfirmado = leer.CampoBool("ServicioConfirmado");
            }

            CargarInformacion_DomicilioBeneficiario();
        }

        private void CargarInformacion_DomicilioBeneficiario()
        {
            bTieneDomicilio = false;
            //lblDomicilioBeneficiario.Text = ""; 

            leer.DataSetClase = Consultas.Beneficiarios_Domicilio(sEstado, sFarmacia, sIdCliente, sIdSubCliente, sIdBeneficiario, "CargarBeneficiarios()");
            if (leer.Leer())
            {
                lblNombreBeneficiario.Text = leer.Campo("NombreCompleto");
                bTieneDomicilio = leer.CampoBool("TieneDomicilio");

                if (bTieneDomicilio)
                {
                    lblDomicilioBeneficiario.Text = "";
                    lblDomicilioBeneficiario.Text += "Dirección : ".ToUpper() + leer.Campo("Direccion") + " \n";
                    lblDomicilioBeneficiario.Text += "Colonia : ".ToUpper() + leer.Campo("Colonia_D") + " \n";
                    lblDomicilioBeneficiario.Text += "Código Postal : ".ToUpper() + leer.Campo("CodigoPostal") + " \n";
                    lblDomicilioBeneficiario.Text += "Telefonos : ".ToUpper() + leer.Campo("Telefonos") + " \n";
                    lblDomicilioBeneficiario.Text += "Referencia dirección : ".ToUpper() + leer.Campo("Referencia") + " \n";
                    lblDomicilioBeneficiario.Text += "Municipio : ".ToUpper() + leer.Campo("Municipio_D") + " \n";
                    lblDomicilioBeneficiario.Text += "Estado : ".ToUpper() + leer.Campo("Estado_D") + "\n"; 
                }
            }
        }
        #endregion Funciones y Procedimientos Privados

        #region Guardar Informacion 
        private bool validarDatos()
        {
            bool bRegresa = true;

            if (!bTieneDomicilio)
            {
                bRegresa = false;
                General.msjError("El beneficiario no tiene domicilio registrado, verifique.");
                btnDomicilioBeneficiario.Focus();
            }

            return bRegresa;
        }

        private bool Guardar_Informacion()
        {
            bool bRegresa = false;

            if (!cnn.Abrir())
            {
                General.msjErrorAlAbrirConexion(); 
            }
            else
            {
                cnn.IniciarTransaccion();

                bRegresa = Guardar_ServicioADomicilio();
                if (!bRegresa)
                {
                    Error.GrabarError(leer, "Guardar_Informacion()");
                    cnn.DeshacerTransaccion(); 
                    General.msjError("Ocurrió un error al generar el Servicio a Domicilio");
                }
                else
                {
                    cnn.CompletarTransaccion();
                    txtFolio.Text = sFolioServicioDomicilio; 
                    General.msjUser(sMensaje);
                    Imprimir(false);
                    this.Hide(); 
                }

                cnn.Cerrar(); 
            }

            return bRegresa;
        }

        private bool Guardar_ServicioADomicilio()
        {
            bool bRegresa = true;
            string sHora = ":00:00";
            string sSql = string.Format("Exec spp_Mtto_Vales_Servicio_A_Domicilio @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', " + 
                " @FolioServicioDomicilio = '{3}', @FolioVale = '{4}', @HoraVisita_Desde = '{5}', @HoraVisita_Hasta = '{6}', " + 
                " @IdPersonal = '{7}', @iOpcion = '{8}'", sEmpresa, sEstado, sFarmacia, sFolioServicioDomicilio, sFolioVale,
                General.Hora(dtpHora_Desde.Value), General.Hora(dtpHora_Hasta.Value),
                DtGeneral.IdPersonal, 1);

            sSql = string.Format("Exec spp_Mtto_Vales_Servicio_A_Domicilio @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', " +
                " @FolioServicioDomicilio = '{3}', @FolioVale = '{4}', @HoraVisita_Desde = '{5}', @HoraVisita_Hasta = '{6}', " +
                " @IdPersonal = '{7}', @iOpcion = '{8}'", sEmpresa, sEstado, sFarmacia, sFolioServicioDomicilio, sFolioVale,
                dtpHora_Desde.Value.Hour.ToString("##") + sHora, dtpHora_Hasta.Value.Hour.ToString("##") + sHora,
                DtGeneral.IdPersonal, 1); 

            if (!leer.Exec(sSql))
            {
                bRegresa = false; 
            }
            else
            {
                if (leer.Leer())
                {
                    bRegresa = true;
                    sFolioServicioDomicilio = leer.Campo("Clave");
                    sMensaje = leer.Campo("Mensaje");
                }
            }

            return bRegresa; 
        }
        #endregion Guardar Informacion

        #region Impresion 
        private void Imprimir(bool Confirmar)
        {
            bool bImprimir = true;
            bool bRegresa = false; 

            if (Confirmar)
            {
                if (General.msjConfirmar("¿ Desea imprimir el Servicio a Domicilio activo ?") == DialogResult.No)
                {
                    bImprimir = false;
                }
            }

            if (bImprimir)
            {
                DatosCliente.Funcion = "Imprimir()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.RutaReporte = GnFarmacia.RutaReportes;
                myRpt.NombreReporte = "PtoVta_Vales_ServicioDomicilio.rpt";
                myRpt.EnviarAImpresora = !chkMostrarImpresionEnPantalla.Checked; 

                myRpt.Add("IdEmpresa", sEmpresa);
                myRpt.Add("IdEstado", sEstado);
                myRpt.Add("IdFarmacia", sFarmacia);
                myRpt.Add("Folio", sFolioServicioDomicilio);

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

                if(!bRegresa && !DtGeneral.CanceladoPorUsuario)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }
        #endregion Impresion

        #region Boton_Registrar_Servicio_Domicilio
        private void btnRegistrarSerDom_Click(object sender, EventArgs e)
        {
            if (!bServicioConfirmado)
            {
                FrmValesTipoSurtimiento fr = new FrmValesTipoSurtimiento();
                fr.MostrarPantalla(Fg.PonCeros(txtFolio.Text, 8));
            }
            else
            {
                FrmRegistroVales_ServicioDomicilio fm = new FrmRegistroVales_ServicioDomicilio();
                fm.MostrarPantalla(Fg.PonCeros(txtFolio.Text, 8), iTipoSurtimiento);
            }
            
        }
        #endregion Boton_Registrar_Servicio_Domicilio
    }
}
