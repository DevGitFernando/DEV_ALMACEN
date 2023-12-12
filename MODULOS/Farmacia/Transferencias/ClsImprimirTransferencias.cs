using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


using SC_SolutionsSystem;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos;
using DllFarmaciaSoft.Reporteador;

namespace Farmacia.Transferencias
{
    public enum TipoReporteTransferencia
    {
        Ninguno = 0,
        Detallado = 1,
        Ticket = 2
    }

    public class ClsImprimirTransferencias
    {
        string sFolioTransferencia = "";
        clsDatosCliente datosCliente;
        clsDatosConexion datosCnn;
        clsConexionSQL cnn;
        clsGrabarError Error = new clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, "ClsImprimirTransferencias");
        clsLeer leer;

        TipoReporteTransferencia TipoReporte = 0;

        string sRutaReportes = GnFarmacia.RutaReportes;
        string sRutaDestino_Archivos = "";
        bool bGenerarArchivos = false;

        string sIdCliente = "";
        string sIdSubCliente = "";

        #region Constructor 
        public ClsImprimirTransferencias(clsDatosConexion DatosCnn, clsDatosCliente DatosCliente, string RutaDestino_Archivos, bool GenerarArchivos, TipoReporteTransferencia TipoReporte)
        {
            this.datosCnn = DatosCnn;
            this.datosCliente = DatosCliente;
            //this.sFolioTransferencia = FolioTransferencia;
            this.sRutaDestino_Archivos = RutaDestino_Archivos;
            this.bGenerarArchivos = GenerarArchivos;
            this.TipoReporte = TipoReporte;

            cnn = new clsConexionSQL(datosCnn);

            leer = new clsLeer(ref cnn);
        }
        #endregion Constructor 

        #region Propiedades 
        public string IdCliente
        {
            get { return sIdCliente; }
            set { sIdCliente = value; } 
        }

        public string IdSubCliente
        {
            get { return sIdSubCliente; }
            set { sIdSubCliente = value; }
        }

        #endregion Propiedades 

        #region Funciones y Procedimientos Publicos 
        public bool Imprimir(string Folio, bool Desglozado)
        {
            bool bRegresa = true;

            int iDesglozado = Desglozado ? 1 : 0;

            string sSegmento, sNumeroDeSegmento, sNombreArchivo;

            string sSql = string.Format(" Exec spp_Rpt_Impresion_Transferencias_Custom_GetSegmentos @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio = '{3}', @TipoProceso_Desglozado = {4}",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, Folio, iDesglozado);


            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Imprimir()");
                General.msjError("Ocurrió un error al obtener la lista de folios.");
            }
            else
            {
                for(int i = 1; leer.Leer(); i++)
                {
                    sSegmento = leer.Campo("Segmentos");
                    sNumeroDeSegmento = leer.Campo("NumeroDeSegmento");
                    sNombreArchivo = leer.Campo("NombreArchivo");

                    bRegresa = Imprimir(Folio, iDesglozado, i, sSegmento, sNumeroDeSegmento, sNombreArchivo);

                }
                
            }


            return bRegresa;
        }
        #endregion Funciones y Procedimientos Publicos 

        #region Funciones y Procedimientos Privados 
        private bool Imprimir(string Folio, int Desglozado, int Iteracion, string Segmentos, string NumeroDeSegmento, string sNombreArchivo)
        {
            bool bRegresa = true;
            string sPrefijo = "";
            string sNameRpt = "PtoVta_Transferencias";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);


            switch (TipoReporte)
            {
                case TipoReporteTransferencia.Detallado:
                    sNameRpt = "PtoVta_Transferencias";
                    break;
                case TipoReporteTransferencia.Ticket:
                    sNameRpt = "PtoVta_TransferenciasTicket";
                    break;

            }      



            string sFile = "";
            //sFile = string.Format("VENTA_{0}_{1}", DtGeneral.FarmaciaConectada, Folio);
            sFile = Path.Combine(sRutaDestino_Archivos, sNombreArchivo + ".pdf");

            clsReporteador Reporteador;  // = new clsReporteador(Reporte, DatosTerminal);

            datosCliente.Funcion = "Imprimir()";


            myRpt.RutaReporte = sRutaReportes;
            myRpt.NombreReporte = sNameRpt;
            myRpt.TituloReporte = "Informe Traspaso.";

            myRpt.Add(sPrefijo + "IdEmpresa", DtGeneral.EmpresaConectada);
            myRpt.Add(sPrefijo + "IdEstado", DtGeneral.EstadoConectado);
            myRpt.Add(sPrefijo + "IdFarmacia", DtGeneral.FarmaciaConectada);


            myRpt.Add(sPrefijo + "Folio", Folio);

            myRpt.Add(sPrefijo + "TipoProceso_Desglozado", Desglozado);
            myRpt.Add(sPrefijo + "Iteracion", Iteracion);
            myRpt.Add(sPrefijo + "Segmentos", Segmentos);
            myRpt.Add(sPrefijo + "NumeroDeSegmento", NumeroDeSegmento);

            myRpt.Add(sPrefijo + "IdCliente", sIdCliente);
            myRpt.Add(sPrefijo + "IdSubCliente", sIdSubCliente);


            //myRpt.NumeroDeCopias = 1;
            //myRpt.EnviarAImpresora = false;

            Reporteador = new clsReporteador(myRpt, datosCliente);
            Reporteador.ImpresionViaWeb = General.ImpresionViaWeb;
            Reporteador.Url = General.Url;
            Reporteador.MostrarInterface = false;
            Reporteador.MostrarMensaje_ReporteSinDatos = false;

            if (bGenerarArchivos)
            {
                bRegresa = Reporteador.ExportarReporte(sFile, FormatosExportacion.PortableDocFormat, true);
            }
            else
            {
                bRegresa = Reporteador.GenerarReporte();
            }

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }

            return bRegresa;
        }
        #endregion Funciones y Procedimientos Privados 

    }
}
