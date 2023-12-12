using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem.QRCode.Codec;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.QRCode;

using SC_SolutionsSystem.QRCode;
using SC_SolutionsSystem.SistemaOperativo; 

namespace DllFarmaciaSoft.QRCode.GenerarEtiquetas
{
    public partial class FrmET_Cajas : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerInformacion;
        clsAyudas ayuda;
        
        QR_Reporte Impresion; // = new QR_Reporte(); 
        DataSet dtsPosicion = new DataSet();
        DataSet dtsInformacion = new DataSet();
        DataSet dtsReeimpresion = new DataSet(); 
        string sFolioGenerado = "";

        QR_Reader reader; 

        bool bPosicion = false;
        bool bInformacion = false;

        bool bExisteImpresoraEtiquetas = DtGeneral.Impresoras.ExisteImpresora("etiquetas");
        // bool bExisteLector = false; // DtGeneral.Camaras.ExisteCamara("QReader");


        public FrmET_Cajas() 
        {
            QR_General.InicializarSDK();

            InitializeComponent();

            leer = new clsLeer(ref cnn);
            leerInformacion = new clsLeer(ref cnn); 
        } 

        private void FrmET_Cajas_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null); 
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles();

            //btnGenerarEtiqueta.Enabled = bExisteImpresoraEtiquetas;  
            btnImprimir.Enabled = bExisteImpresoraEtiquetas;
            btnImprimir.Enabled = false;
            //btnReader.Enabled = bExisteLector;  

            bPosicion = false;
            bInformacion = false; 
            dtsPosicion = new DataSet();
            dtsInformacion = new DataSet();

            txtIdCaja.Enabled = true;

            txtIdCaja.Focus();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (txtIdCaja.Text.Trim() == "*")
            {
                GetCajas();
            }

            GenerarEtiqueta();
        }
        #endregion Botones

        #region Funciones y procedimientos

        private void GenerarEtiqueta()
        {
            Impresion = new QR_Reporte();
            Impresion.Reporte = "ET_Caja";
            Impresion.EnviarAImpresora = false;
            
            GenerarDetalles(); 

            Impresion.GenerarXml(); 
            Impresion.Imprimir(true,false);
        }

        private void GenerarDetalles()
        {
           
            if (bInformacion)
            {
                leerInformacion.Leer(); 
                leerInformacion.RegistroActual = 1;

                Impresion.AgregarDetalles(leerInformacion.DataSetClase); 
            } 
        }

        private void GetCajas()
        {
            if (txtIdCaja.Text != "*")
            {
                txtIdCaja.Text = Fg.PonCeros(txtIdCaja.Text, 8);
            }
            string sSql = string.Format("Exec spp_Mtto_CajasDistribucion '{0}', '{1}', '{2}', '{3}', 'A'",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtIdCaja.Text);

            bInformacion = false;
            if (!leerInformacion.Exec("Informacion", sSql))
            {
                Error.GrabarError(leerInformacion, "GetCodigoEAN()");
            }
            else
            {
                if (leerInformacion.Leer())
                {
                    if (leerInformacion.Campo("Status") != "No Encontrado")
                    {
                        bInformacion = true;
                        dtsInformacion = leerInformacion.DataSetClase;
                        txtIdCaja.Text = leerInformacion.Campo("IdCaja");
                        lblStatus.Text = leerInformacion.Campo("Status");
                        btnImprimir.Enabled = true;
                        txtIdCaja.Enabled = false;
                    }
                    else
                    {
                        General.msjAviso("Clave Caja no encontrado verifique.");
                    }
                }

            }
        }
        #endregion  Funciones y procedimientos

        #region Eventos
        private void txtIdCaja_TextChanged(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            btnImprimir.Enabled = false;
        }

        private void txtIdCaja_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdCaja.Text.Trim() == "" || txtIdCaja.Text.Trim() == "*")
            {
                txtIdCaja.Text = "*";
                txtIdCaja.Enabled = false;
                btnImprimir.Enabled = true;
                lblStatus.Text = "Nuevo";
            }
            else
            {
                GetCajas();
            }
        }

        private void txtIdCaja_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtIdCaja.Text.Trim() == "")
                {
                    txtIdCaja.Enabled = false;
                }
                else
                {
                    GetCajas();
                }
            }
        }
        #endregion Eventos 
    }
}
