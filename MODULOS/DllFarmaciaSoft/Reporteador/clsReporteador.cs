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
    public class clsReporteador
    {
        #region Declaracion de Variables 
        FrmReporteador Reporteador; 
        clsImprimir myRpt;
        clsDatosCliente datosCliente;
        clsConexionClienteUnidad datosConexionUnidad; 

        string sUrl = "";
        bool bImpresionViaWeb = false;
        bool bMostrarInterface = true; 
        string sFileNameExport = "";
        FormatosExportacion fExportacion = FormatosExportacion.NoFormat;

        bool bCanceladoPorUsuario = false; 
        bool bMostrarMensaje_ReporteSinDatos = true; 
        #endregion Declaracion de Variables 
        
        #region Constructores  
        public clsReporteador(clsImprimir Reporte, clsDatosCliente DatosTerminal)
        {
            myRpt = Reporte;
            datosCliente = DatosTerminal; 
        }

        public clsReporteador(clsImprimir Reporte, clsDatosCliente DatosTerminal, clsConexionClienteUnidad ConexionUnidad)
        {
            myRpt = Reporte;
            datosCliente = DatosTerminal;
            datosConexionUnidad = ConexionUnidad; 
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
            get { return bImpresionViaWeb; }
            set { bImpresionViaWeb = value; }
        }

        public bool MostrarInterface
        {
            get { return bMostrarInterface; }
            set { bMostrarInterface = value;  }
        }

        public bool MostrarMensaje_ReporteSinDatos
        {
            get { return bMostrarMensaje_ReporteSinDatos; }
            set { bMostrarMensaje_ReporteSinDatos = value; }
        }
        
        public bool CanceladoPorUsuario
        {
        get { return bCanceladoPorUsuario; } 
        }

        #endregion Propiedades

        #region Funciones y Metodos Publicos 
        private bool GeneraReporteInterno(int Tipo, bool Exportar, bool ExportarSilencioso)
        {
            bool bRegresa = false;

            if (Tipo == 1)
            {
                Reporteador = new FrmReporteador(myRpt, datosCliente); 
            }
            else
            {
                Reporteador = new FrmReporteador(myRpt, datosCliente, datosConexionUnidad); 
            }

            if (Exportar) 
            {
                Reporteador.Text = "Exportando documento";
            }

            Reporteador.MostrarInterface = bMostrarInterface; 
            bRegresa = Reporteador.GenerarReporte(sUrl, bImpresionViaWeb);
            bCanceladoPorUsuario = Reporteador.bCanceladoPorUsuario; 

            if (bRegresa)
            {
                if (!myRpt.ExistenDatosDeReporte)
                {
                    if (bMostrarMensaje_ReporteSinDatos)
                    {
                        General.msjUser("No existe información disponible para generar el reporte solicitado.");
                    } 
                }
                else
                {
                    if (Exportar)
                    {
                        if (ExportarSilencioso)
                        {
                            myRpt.ExportarReporteSilencioso(sFileNameExport, fExportacion);
                        }
                        else
                        {
                            myRpt.ExportarReporte(sFileNameExport, fExportacion);
                        }
                    }
                    else
                    {
                        if (myRpt.EnviarAImpresora)
                        {
                            myRpt.ImprimirReporte_Impresora();
                        }
                        else
                        {
                            myRpt.ShowDialog(); 
                        }
                    }
                }
            }

            ////if (!bRegresa)
            ////{
            ////    bRegresa = Reporteador.bCanceladoPorUsuario; 
            ////} 

            return bRegresa; 
        }

        public bool GenerarReporte()
        {
            bool bRegresa = false; 
            bRegresa = GeneraReporteInterno(1, false, false); 
            return bRegresa; 
        }

        public bool GenerarReporteRemoto()
        {
            bool bRegresa = false;
            bRegresa = GeneraReporteInterno(2, false, false); 
            return bRegresa;
        }

        public bool ExportarReporte(string Nombre, FormatosExportacion Formato)
        {
            bool bRegresa = false; 

            bRegresa = ExportarReporte(Nombre, Formato, false);

            return bRegresa;
        }

        public bool ExportarReporte(string Nombre, FormatosExportacion Formato, bool Silencioso)
        {
            bool bRegresa = false; 

            sFileNameExport = Nombre;
            fExportacion = Formato;
            bRegresa = GeneraReporteInterno(1, true, Silencioso);

            return bRegresa;
        }

        public bool ExportarReporteRemoto(string Nombre, FormatosExportacion Formato)
        {
            bool bRegresa = false; 
            
            sFileNameExport = Nombre;
            fExportacion = Formato; 
            bRegresa = GeneraReporteInterno(2, true, false);

            return bRegresa;
        }
        #endregion Funciones y Metodos Publicos

    }
}
