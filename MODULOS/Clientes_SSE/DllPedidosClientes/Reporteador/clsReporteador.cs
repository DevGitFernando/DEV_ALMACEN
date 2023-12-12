using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SC_SolutionsSystem; 
using SC_SolutionsSystem.Data; 
using SC_SolutionsSystem.Reportes;

namespace DllPedidosClientes.Reporteador
{
    public class clsReporteador
    {
        #region Declaracion de Variables 
        FrmReporteador Reporteador; 
        clsImprimir myRpt;
        // clsDatosCliente datosCliente;

        string sUrl = "";
        bool bImpresionViaWeb = false; 

        string sEstado = ""; 
        string sFarmacia = "";
        DataSet dtsInformacionReporteWeb = new DataSet();
        DataSet dtsInformacionCliente = new DataSet();
        bool bEsRegional = false; 

        #endregion Declaracion de Variables

        #region Constructores  
        public clsReporteador(clsImprimir Reporte, string Url, string Estado, string Farmacia, DataSet InformacionReporteWeb, DataSet InformacionCliente, bool EsRegional)
        {
            myRpt = Reporte; 
            sUrl = Url;
            sEstado = Estado;
            sFarmacia = Farmacia;
            dtsInformacionReporteWeb = InformacionReporteWeb;
            dtsInformacionCliente = InformacionCliente;
            bEsRegional = EsRegional; 
        }
        #endregion Constructores

        #region Propiedades 
        public clsImprimir Reporte
        {
            get { return myRpt; }
            set { myRpt = value; }
        }

        public string Url
        {
            get { return sUrl; }
            set { sUrl = value; }
        }

        public bool ImpresionViaWeb
        {
            get { return true; }
            set { bImpresionViaWeb = true; }
        }
        #endregion Propiedades

        #region Funciones y Metodos Publicos 
        public bool GenerarReporte()
        {
            bool bRegresa = false;

            Reporteador = new FrmReporteador(myRpt, sUrl, sEstado, sFarmacia, dtsInformacionReporteWeb, dtsInformacionCliente, bEsRegional); 
            bRegresa = Reporteador.GenerarReporte(sUrl, bImpresionViaWeb);

            if (bRegresa) 
            {
                if (!myRpt.ExistenDatosDeReporte) 
                {
                    General.msjUser("No existe información disponible para generar el reporte solicitado."); 
                }
                else 
                {
                    myRpt.ShowDialog();
                }
                ////if (myRpt.EnviarAImpresora)
                ////{
                ////    myRpt.ImprimirReporte();
                ////}
                ////else
                ////{
                ////    // myRpt.CargarReporte(true);
                ////    myRpt.ShowDialog();
                ////}
            }

            return bRegresa; 
        }

        public bool GenerarReporteRemoto()
        {
            bool bRegresa = false;

            ////Reporteador = new FrmReporteador(myRpt, datosCliente, datosConexionUnidad);

            ////bRegresa = Reporteador.GenerarReporte(sUrl, bImpresionViaWeb);

            ////if (bRegresa)
            ////{
            ////    if (myRpt.EnviarAImpresora)
            ////    {
            ////        myRpt.ImprimirReporte();
            ////    }
            ////}

            return bRegresa;
        }
        #endregion Funciones y Metodos Publicos

    }
}
