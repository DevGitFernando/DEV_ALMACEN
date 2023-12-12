#region USING
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections;
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

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
#endregion USING

namespace Dll_IFacturacion.XSA
{
    public class xsaImprimirDocumento
    {
        clsDatosConexion datosDeConexion;
        clsConexionSQL cnn; 
        clsLeer leer; 
        clsGrabarError Error;

        basGenerales Fg = new basGenerales(); 
        //string sMensajeDeError = "";
        string sRutaCFDI_Documentos = DtIFacturacion.RutaCFDI_DocumentosImpresion;

        string sIdEmpresa = "";
        string sIdEstado = "";
        string sIdFarmacia = "";

        string sRFC = "";
        //string sKey = "";
        //string sSerie = "";
        //string sFolio = ""; 

        #region Constructor y Destructor de Clase 
        public xsaImprimirDocumento(string IdEmpresa, string IdEstado, string IdFarmacia, clsDatosConexion Conexion)
        {
            datosDeConexion = Conexion;
            cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = Conexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = Conexion.BaseDeDatos;
            cnn.DatosConexion.Usuario = Conexion.Usuario;
            cnn.DatosConexion.Password = Conexion.Password;
            cnn.DatosConexion.Puerto = Conexion.Puerto; 
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite; 
            leer = new clsLeer(ref cnn); 

            sIdEmpresa = IdEmpresa;
            sIdEstado = IdEstado;
            sIdFarmacia = IdFarmacia;
            sRFC = sIdEmpresa +  sIdEstado + sIdFarmacia ; 

            Error = new clsGrabarError(datosDeConexion, DtIFacturacion.DatosApp, "xsaImprimirDocumento");
            Error.NombreLogErorres = "xsa_CtlErrores";

            // GetConfiguracion(); 
        }
        #endregion Constructor y Destructor de Clase 

        #region Funciones y Procedimientos Publicos 
        public void Imprimir(string FolioDocumento)
        {
            string[] sFolio = FolioDocumento.Split('-'); 
            Imprimir(sFolio[0], sFolio[1], null); 
        }

        public void Imprimir(string FolioDocumento, Form InvocaVisor)
        {
            string[] sFolio = FolioDocumento.Split('-');
            Imprimir(sFolio[0], sFolio[1], InvocaVisor);
        }

        public void Imprimir(string Serie, string Folio)
        {
            Imprimir(Serie, Folio, null); 
        }

        public void Imprimir(string Serie, string Folio, Form InvocaVisor)
        {
            Serie = Serie.Trim();
            Folio = Convert.ToDouble("0" + Folio.Trim()).ToString(); 
            string sFile = "";
            string sDocumentoName = sRFC + "___" + Serie + "_" + Folio + "." + cfdFormatoDocumento.PDF.ToString();

            string sSql = string.Format("Select NombreDocumento, FormatoPDF as Formato " + 
                " From FACT_CFD_Documentos_Generados (NoLock) " + 
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Serie = '{3}' and Folio = '{4}' ", 
                sIdEmpresa, sIdEstado, sIdFarmacia, Serie, Folio);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Imprimir"); 
            }
            else
            {
                if (!leer.Leer())
                {
                }
                else 
                {
                    sFile = leer.Campo("Formato"); 
                    if ( Fg.ConvertirStringB64EnArchivo(sDocumentoName, sRutaCFDI_Documentos, sFile, true) )                    
                    {
                        FrmViewDocumento f = new FrmViewDocumento(sDocumentoName, Path.Combine(sRutaCFDI_Documentos, sDocumentoName)); 
                        if (InvocaVisor != null)
                        {
                            f.ShowDialog(InvocaVisor);
                        }
                        else
                        {
                            f.ShowDialog();
                        }
                    }
                }
            } 
        } 
        #endregion Funciones y Procedimientos Publicos
        
        #region Funciones y Procedimientos Private 
        ////private void GetConfiguracion()
        ////{
        ////    string sSql = string.Format("Select  " +
        ////        " IdEmpresa, Nombre, RFC, KeyLicencia, NombreProveedor, DireccionUrl, Telefonos, Status, Actualizado " +
        ////        " From FACT_CFD_Empresas (NoLock) " +
        ////        " Where IdEmpresa = '{0}' \n\n", sIdEmpresa); 
        ////}
        #endregion Funciones y Procedimientos Private 
    }
}
