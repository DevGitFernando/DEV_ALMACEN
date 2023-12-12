using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms; 

using SC_SolutionsSystem; 
using SC_SolutionsSystem.Data; 
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft.Conexiones; 

namespace DllFarmaciaSoft.Reporteador
{
    public class clsReporteadorExcel
    {
        #region Declaracion de Variables 
        FrmReporteadorExcel Reporteador; 
        // clsImprimir myRpt;
        clsDatosCliente datosCliente;
        // clsConexionClienteUnidad datosConexionUnidad;

        string sReporte = "";
        string sRutaReportes = "";  
        string sUrl = "";
        bool bImpresionViaWeb = false;
        // string sFileNameExport = "";
        // FormatosExportacion fExportacion = FormatosExportacion.NoFormat;  
        #endregion Declaracion de Variables 

        #region Constructores  
        public clsReporteadorExcel(string Reporte, string RutaReportes, clsDatosCliente DatosTerminal)
        {
            sReporte = Reporte;
            sRutaReportes = RutaReportes;
            datosCliente = DatosTerminal; 
        }
        #endregion Constructores

        #region Propiedades 
        public string Reporte
        {
            get { return sReporte; }
            set { sReporte = value; }
        }

        public string Url
        {
            get { return sUrl; }
            set { sUrl = value; }
        }

        public bool ImpresionViaWeb
        {
            get { return bImpresionViaWeb; }
            set { bImpresionViaWeb = value; }
        }
        #endregion Propiedades

        #region Funciones y Metodos Publicos 
        private bool GeneraReporteInterno( int Tipo )
        {
            bool bRegresa = false;

            if (Tipo == 1)
            {
                Reporteador = new FrmReporteadorExcel(sReporte, sRutaReportes, datosCliente); 
            }
            else
            {
                // Reporteador = new FrmReporteadorExcel(myRpt, datosCliente, datosConexionUnidad); 
            }

            bRegresa = Reporteador.GenerarReporte(sUrl, bImpresionViaWeb);  

            ////////if (bRegresa) 
            ////////{ 
            ////////    if (!myRpt.ExistenDatosDeReporte) 
            ////////    { 
            ////////        General.msjUser("No existe información disponible para generar el reporte solicitado."); 
            ////////    } 
            ////////    else 
            ////////    { 
            ////////        if (myRpt.EnviarAImpresora) 
            ////////        { 
            ////////            myRpt.ImprimirReporte(); 
            ////////        } 
            ////////        else 
            ////////        { 
            ////////            myRpt.ShowDialog(); 
            ////////        } 
            ////////    } 
            ////////} 

            ////////if (!bRegresa)
            ////////{
            ////////    bRegresa = Reporteador.bCanceladoPorUsuario; 
            ////////} 

            return bRegresa; 
        }

        public bool GenerarReporte()
        {
            bool bRegresa = false; 
            bRegresa = GeneraReporteInterno(1); 
            return bRegresa; 
        }       
        #endregion Funciones y Metodos Publicos

    }
}
