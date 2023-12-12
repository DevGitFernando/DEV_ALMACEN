using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;

using Dll_MA_IFacturacion;

using Dll_MA_IFacturacion.CFDI;
using Dll_MA_IFacturacion.CFDI.CFDFunctions;
using Dll_MA_IFacturacion.CFDI.EDOInvoice;
using Dll_MA_IFacturacion.CFDI.geCFD;
using Dll_MA_IFacturacion.CFDI.Timbrar;

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;

namespace Dll_MA_IFacturacion.CFDI
{
    public partial class FrmClienteEMails : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        clsGrid grid; 
        string sIdReferencia = ""; 
        string sRuta = "";
        string sNombreArchivo = "";

        EnvioDeCorreos tpTipoDeDestinatario = EnvioDeCorreos.Ninguno; 
        string sXML = "";
        string sPDF = "";

        CFDFunctions.cMain CFDFunct = new CFDFunctions.cMain(); 
        

        public FrmClienteEMails(EnvioDeCorreos TipoDeDestinatario, string IdReferencia, string Directorio, string NombreArchivo)
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);

            tpTipoDeDestinatario = TipoDeDestinatario; 
            sIdReferencia = IdReferencia;
            sRuta = Directorio;
            sNombreArchivo = NombreArchivo; 
            sXML = Path.Combine(sRuta, ".xml");
            sPDF = Path.Combine(sRuta, ".pdf");

            grid = new clsGrid(ref grdCorreos, this);
            grid.EstiloDeGrid = eModoGrid.ModoRow; 
        }

        #region Form 
        private void FrmClienteEMails_Load(object sender, EventArgs e)
        {
            bool bEnabled = false; 

            ////if (tpTipoDeDestinatario == EnvioDeCorreos.Clientes)
            {
                Cargar_Emails__Clientes();
                bEnabled = true; 
            }

            ////if (tpTipoDeDestinatario == EnvioDeCorreos.Empleados)
            ////{
            ////    Cargar_Emails__Empleados();
            ////    bEnabled = true; 
            ////}

            btnCustomerSendMail.Enabled = bEnabled; 
        }
        #endregion Form 

        #region Procedimientos 
        private void Cargar_Emails__Clientes()
        {
            string sSql = string.Format("Select TipoMail, EMail, 0 as Enviar " +
                "From vw_CFDI_Clientes_EMails (NoLock) " + 
                "Where IdCliente = '{0}' " +
                "Order By EMail ", Fg.PonCeros(sIdReferencia, 8));

            grid.Limpiar(false);
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Cargar_Emails()"); 
            }
            else
            {
                grid.LlenarGrid(leer.DataSetClase, false, false); 
            }
        }

        //////private void Cargar_Emails__Empleados()
        //////{
        //////    string sSql = string.Format("Select TipoMail, EMail, 0 as Enviar " +
        //////        "From vw_NM_CFDI_Empleados_EMails (NoLock) " +
        //////        "Where IdEmpleado = '{0}' " +
        //////        "Order By EMail ",  Fg.PonCeros(sIdReferencia, 8));

        //////    grid.Limpiar(false);
        //////    if (!leer.Exec(sSql))
        //////    {
        //////        Error.GrabarError(leer, "Cargar_Emails()");
        //////    }
        //////    else
        //////    {
        //////        grid.LlenarGrid(leer.DataSetClase, false, false);
        //////    }
        //////} 
        #endregion Procedimientos

        private bool validarCorreoAdicional()
        {
            bool bRegresa = true;

            if (!DtIFacturacion.EMail_Valido(txtCorreo.Text.Trim()))
            {
                bRegresa = false;
                General.msjUser("El formato del Correo adicional es incorrecto, verifique.");
            }

            return bRegresa;
        }

        private void btnCustomerSendMail_Click(object sender, EventArgs e)
        {
            string sMails = "";
            bool bRegresa = false; 

            for (int i = 1; i <= grid.Rows; i++)
            {
                if (grid.GetValueBool(i, 3))
                {
                    sMails += grid.GetValue(i, 2) + "; ";
                }
            }

            if (txtCorreo.Text.Trim() != "")
            {
                if (validarCorreoAdicional())
                {
                    sMails += txtCorreo.Text.Trim() + "; ";
                }
            }

            if (sMails != "")
            {
                sMails = sMails.Trim();
                sMails = Fg.Mid(sMails, 1, sMails.Length - 1);
                bRegresa = CFDFunct.enviarCorreoElectronico(sMails, DtIFacturacion.DatosEmail.MensajePredeterminado, sNombreArchivo, Path.Combine(sRuta, sNombreArchivo));

                if (bRegresa)
                {
                    this.Hide(); 
                }
            }
        }
    }
}
