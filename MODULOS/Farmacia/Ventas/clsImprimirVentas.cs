using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos;
using DllFarmaciaSoft.Reporteador;

namespace Farmacia.Ventas
{
    public enum TipoReporteVenta
    {
        Ninguno = 0,
        Contado = 1,
        Credito = 2
    }

    public class clsImprimirVentas
    {
        clsDatosConexion datosCnn;
        clsDatosCliente datosCliente;
        wsFarmacia.wsCnnCliente conexionWeb;
        clsConexionSQL cnn;

        clsGrabarError Error = new clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, "ClsImprimirTransferencias");

        string sUrl = "";

        string sEmpresa = "";
        string sEstado = "";
        string sFarmacia = "";
        string sRutaReportes = "";
        TipoReporteVenta tpReporte = TipoReporteVenta.Ninguno;
        double dImporte = 0;
        bool bImprimirDirecto = true;
        bool bImpresionDetallada = false;
        bool bMostrarPrecios = false;
        bool bMostrarCantidadConLetra = false;

        string sMd5 = "";       
        string sNombreReporte = "";
        int iNumeroDeCopias = 1;

        string sRutaDestino_Archivos = "";
        bool bGenerarArchivos = false;
        clsLeer leer;

        public clsImprimirVentas(clsDatosConexion DatosCnn, clsDatosCliente DatosCliente, 
            string Empresa, string Estado, string Farmacia, string Url, string RutaReportes, TipoReporteVenta TipoReporte)
        {
            this.datosCnn = DatosCnn;
            this.datosCliente = DatosCliente;
            this.sEmpresa = Empresa;
            this.sEstado = Estado;
            this.sFarmacia = Farmacia;
            this.sRutaReportes = RutaReportes;
            this.tpReporte = TipoReporte;
            this.sUrl = Url;
            this.bImpresionDetallada = false; 

            conexionWeb = new Farmacia.wsFarmacia.wsCnnCliente();
            conexionWeb.Url = sUrl;

            cnn = new clsConexionSQL(datosCnn);

            leer = new clsLeer(ref cnn);
        }

        #region Propiedades 
        public double Importe 
        {
            get { return dImporte; }
            set { dImporte = value; }
        }

        public TipoReporteVenta TipoDeReporte
        {
            get { return tpReporte; }
            set { tpReporte = value; }
        }

        public bool MostrarVistaPrevia
        {
            get { return bImprimirDirecto; }
            set { bImprimirDirecto = !value; }
        }

        public bool MostrarPrecios
        {
            get { return bMostrarPrecios; }
            set { bMostrarPrecios = value; }
        }

        public bool MostrarImpresionDetalle
        {
            get { return bImpresionDetallada; }
            set { bImpresionDetallada = value;  }
        }

        public bool MostrarCantidadConLetra
        {
            get { return bMostrarCantidadConLetra; }
            set { bMostrarCantidadConLetra = value; }
        }

        public bool Imprimir(string Folio)
        {
            return Imprimir(Folio, dImporte);
        }

        public bool Imprimir(string Folio, string FolioFinal)
        {
            return Imprimir(Folio, FolioFinal, dImporte);
        }

        public bool Imprimir(string Folio, double Importe)
        {
            return Imprimir(Folio, Folio, Importe);
        }

        public int NumeroDeCopias 
        {
            get { return iNumeroDeCopias; } 
            set 
            { 
                iNumeroDeCopias = value <= 0 ? 1 : value; 
            } 
        }
        #endregion Propiedades
        
        #region Impresion Excel
        private bool ObtenerNombre()
        {
            bool bRegresa = false;
            sNombreReporte = "";

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Title = "Guardar información en formato Excel";
            saveFile.Filter = "Archivo de Excel | *.xls";

            if (saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                sNombreReporte = saveFile.FileName;
                bRegresa = true;
            }

            return bRegresa;
        } 

        public bool ImprimirExcel(string Folio, string FolioFinal, double Importe)
        {
            clsConexionSQL cnn = new clsConexionSQL(datosCnn);
            clsLeer leer = new clsLeer(ref cnn); 

            bool bRegresa = true;
            string sSql = ""; 
            string sWhere = "";

            sWhere = string.Format(
                "   Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' " +  
                "       and Folio Between '{3}' and '{4}' " , sEmpresa, sEstado, sFarmacia, Folio, FolioFinal ); 


            if (tpReporte == TipoReporteVenta.Contado)
            {
                ////myRpt.Add("CantidadLetra", "( " + General.LetraMoneda(dImporte) + " )");
                ////myRpt.NombreReporte = "PtoVta_TicketPublicoGral.rpt";
                ////if (bImpresionDetallada)
                ////{
                ////    myRpt.NombreReporte = "PtoVta_TicketPublicoGral_Detallado.rpt";
                ////}
            }
            else if (tpReporte == TipoReporteVenta.Credito)
            {
                sSql = "Select \n " +
                " Empresa, Estado, Municipio, Colonia, Domicilio, \n " +
                " IdFarmacia, Farmacia, Folio, FechaSistema, FechaRegistro, IdPersonal, NombrePersonal, \n " +
                " IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, \n " +
                " IdPrograma, Programa, IdSubPrograma, SubPrograma, \n " +
                " IdBeneficiario, Beneficiario, FolioReferencia, \n " +
                " NumReceta, FechaReceta, IdClaveSSA_Sal, ClaveSSA, DescripcionClave, DescripcionCortaClave, \n " +
                " IdProducto, CodigoEAN, DescripcionCorta, Presentacion, ContenidoPaquete, \n" + 
                " TasaIva, ClaveLote, CantidadLote, CantidadCajasLote, FechaCad \n "; 

                // myRpt.NombreReporte = "PtoVta_TicketCredito.rpt";
                // if (bImpresionDetallada)
                {
                    if (bMostrarPrecios)
                    {
                        sSql += "  , Costo, Importe ";
                        // sSql += "From vw_Impresion_Ventas_Credito_Lotes (NoLock)  "; 
                        // myRpt.NombreReporte = "PtoVta_TicketCredito_Detallado.rpt"; 
                    }
                    sSql += "From vw_Impresion_Ventas_Credito_Lotes (NoLock)  "; 
                } 
            } 

            sSql += sWhere;

            if (ObtenerNombre())
            {
                if (!leer.Exec(sSql))
                {
                }
                else
                {
                    if (leer.Leer())
                    {
                        FrmExportarExcel f = new FrmExportarExcel(leer.DataSetClase, sNombreReporte);
                        if (f.Exportar())
                        {
                            f.Dispose();
                            f = null;
                            if (General.msjConfirmar("Desea abrir el documento generado") == DialogResult.Yes)
                            {
                                General.AbrirDocumento(sNombreReporte);
                            }
                        }
                    }
                }
            }

            return bRegresa ;
        }
        #endregion Impresion Excel 

        #region Impresion
        public bool Imprimir(string Folio, string FolioFinal, double Importe)
        {
            return Imprimir(Folio, FolioFinal, Importe, false);
        }

        public bool Imprimir(string Folio, string FolioFinal, double Importe, bool Desglozado)
        {
            bool bRegresa = true;

            int iDesglozado = Desglozado ? 1 : 0;

            string sSegmento, sNumeroDeSegmento, sNombreArchivo;

            string sSql = string.Format(" Exec spp_Rpt_Impresion_Ticket_Credito_Lotes_Custom_GetSegmentos @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio = '{3}', @TipoProceso_Desglozado = {4}",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, Folio, iDesglozado);


            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Imprimir()");
                General.msjError("Ocurrió un error al obtener la lista de folios.");
            }
            else
            {
                for (int i = 1; leer.Leer(); i++)
                {
                    sSegmento = leer.Campo("Segmentos");
                    sNumeroDeSegmento = leer.Campo("NumeroDeSegmento");
                    sNombreArchivo = leer.Campo("NombreArchivo");

                    bRegresa = Imprimir(Folio, FolioFinal, Importe, iDesglozado, i, sSegmento, sNumeroDeSegmento, sNombreArchivo);

                }

            }


            return bRegresa;

        }

        private bool Imprimir(string Folio, string FolioFinal, double Importe, int Desglozado, int Iteracion, string Segmentos, string NumeroDeSegmento, string NombreArchivo)
        {

            bool bRegresa = true;
            string sPrefijo = "";
            string sNameRpt = "PtoVta_TicketCredito";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);

            datosCliente.Funcion = "Imprimir()";
            dImporte = Importe;


            //////if (GnFarmacia.Vta_Impresion_Personalizada_Ticket)
            //{
            //    if (tpReporte == TipoReporteVenta.Contado)
            //    {
            //        sNameRpt = GnFarmacia.Vta_Plantilla_Personalizada_Ticket_PublicoGeneral;
            //    }
            //    else
            //    {
            //        sNameRpt = GnFarmacia.Vta_Plantilla_Personalizada_Ticket;
            //    }
            //}


            if (tpReporte == TipoReporteVenta.Contado)
            {
                sNameRpt = GnFarmacia.Vta_Plantilla_Personalizada_Ticket_PublicoGeneral;
                if (bImpresionDetallada)
                {
                    sNameRpt = "PtoVta_TicketPublicoGral_Detallado.rpt";
                }
            }

            if (tpReporte == TipoReporteVenta.Credito)
            {
                if (!bImpresionDetallada)
                {
                    sNameRpt = GnFarmacia.Vta_Plantilla_Personalizada_Ticket;
                }
                else
                {
                    if (bMostrarPrecios)
                    {
                        sNameRpt = "PtoVta_TicketCredito_Detallado_Precios.rpt";
                        sNameRpt = GnFarmacia.Vta_Plantilla_Personalizada_Ticket_Detallado_Precios;
                    }
                    else
                    {
                        sNameRpt = "PtoVta_TicketCredito_Detallado.rpt";
                        sNameRpt = GnFarmacia.Vta_Plantilla_Personalizada_Ticket_Detallado;
                    }
                }
            }


            ////if (GnFarmacia.Vta_Impresion_Personalizada_Ticket || GnFarmacia.Vta_Impresion_Personalizada_Ticket_Detalle)
            ////{
            ////    sPrefijo = "@"; 
            ////}

            // byte[] btReporte = null;

            //// Jesus Diaz 2K110612-1456 
            //clsReporteador Reporteador = new clsReporteador(myRpt, datosCliente); 

            string sFile = "";
            //sFile = string.Format("VENTA_{0}_{1}", DtGeneral.FarmaciaConectada, Folio);
            sFile = Path.Combine(sRutaDestino_Archivos, NombreArchivo + ".pdf");

            myRpt.RutaReporte = sRutaReportes;
            myRpt.NombreReporte = sNameRpt;
            myRpt.TituloReporte = "Impresión de ticket de venta";

            myRpt.Add(sPrefijo + "IdEmpresa", sEmpresa);
            myRpt.Add(sPrefijo + "IdEstado", sEstado);
            myRpt.Add(sPrefijo + "IdFarmacia", sFarmacia);
            myRpt.Add(sPrefijo + "Folio", General.Fg.PonCeros(Folio, 8));


            if (bMostrarCantidadConLetra)
            {
                myRpt.Add("CantidadLetra", "( " + General.LetraMoneda(dImporte) + " )");
            }

            myRpt.Add(sPrefijo + "TipoProceso_Desglozado", Desglozado);
            myRpt.Add(sPrefijo + "Iteracion", Iteracion);
            myRpt.Add(sPrefijo + "Segmentos", Segmentos);
            myRpt.Add(sPrefijo + "NumeroDeSegmento", NumeroDeSegmento);

            //if (tpReporte == TipoReporteVenta.Contado)
            //{
            //    myRpt.NombreReporte = "PtoVta_TicketPublicoGral.rpt";
            //    myRpt.NombreReporte = sNameRpt + ".rpt";
            //} 

            myRpt.Impresora = DtGeneral.XmlEdoConfig.GetValues("ImpresoraTickets");
            myRpt.NumeroDeCopias = iNumeroDeCopias;
            myRpt.EnviarAImpresora = bImprimirDirecto;


            clsReporteador Reporteador = new clsReporteador(myRpt, datosCliente);
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


            //bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, datosCliente);



            ////Reporteador.ImpresionViaWeb = General.ImpresionViaWeb;
            ////Reporteador.Url = sUrl;
            ////bRegresa = Reporteador.GenerarReporte(); 

            ////if (General.ImpresionViaWeb)
            ////{
            ////    DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
            ////    DataSet datosC = datosCliente.DatosCliente();

            ////    btReporte = conexionWeb.Reporte(InfoWeb, datosC);
            ////    bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);
            ////}
            ////else
            ////{
            ////    myRpt.CargarReporte(true);
            ////    bRegresa = !myRpt.ErrorAlGenerar; 
            ////}

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }

            return bRegresa;
        }

        public bool ImprimirVale(string Folio)
        {
            return ImprimirVale(Folio, ""); 
        }

        public bool ImprimirVale( string Folio, string GUID )
        {
            bool bRegresa = false;

            if(GnFarmacia.Vales_TipoDeImpresion == Vales_TipoDeImpresion.General)
            {
                bRegresa = ImprimirVale_01_General(Folio, GUID, 1, "", 1);
            }

            if(GnFarmacia.Vales_TipoDeImpresion == Vales_TipoDeImpresion.Clave || GnFarmacia.Vales_TipoDeImpresion == Vales_TipoDeImpresion.Clave_Pieza)
            {
                bRegresa = GenerarVale_Clave(Folio, GUID);
            }

            return bRegresa;
        }

        private bool GenerarVale_Clave( string Folio, string GUID )
        {
            bool bRegresa = false;
            int iIteracion = 0;
            int iIteracion_Piezas = 0;
            string sClaveSSA = "";
            int iCantidad = 0;

            string sSql = string.Format(
                "Select C.ClaveSSA, sum(V.Cantidad) as Cantidad \n" +
                "From Vales_EmisionDet V (NoLock) \n" +
                "Inner Join CatClavesSSA_Sales C \n (NoLock) On ( V.IdClaveSSA_Sal = C.IdClaveSSA_Sal ) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioVale = '{3}' \n" + 
                "Group by C.ClaveSSA \n " + 
                "Order by C.ClaveSSA",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, General.Fg.PonCeros(Folio, 8));

            clsConexionSQL cnn = new clsConexionSQL(datosCnn);
            clsLeer leer = new clsLeer(ref cnn);

            if(!leer.Exec(sSql))
            {
                General.msjError("Ocurrió un error al imprimir el vale.");
            }
            else
            {
                if(leer.Registros > 0)
                {
                    while(leer.Leer())
                    {
                        sClaveSSA = leer.Campo("ClaveSSA");
                        iCantidad = Convert.ToInt32(leer.CampoDouble("Cantidad"));
                        iIteracion++;

                        if(GnFarmacia.Vales_TipoDeImpresion == Vales_TipoDeImpresion.Clave)
                        {
                            bRegresa = ImprimirVale_01_General(Folio, GUID, iIteracion, sClaveSSA, iCantidad);
                        }

                        if(GnFarmacia.Vales_TipoDeImpresion == Vales_TipoDeImpresion.Clave_Pieza)
                        {
                            for(int i = 1; i <= iCantidad; i++)
                            {
                                iIteracion_Piezas++;
                                bRegresa = ImprimirVale_01_General(Folio, GUID, iIteracion_Piezas, sClaveSSA, 1);
                            }
                        }
                    }
                }
            }

            return bRegresa;
        }

        public bool ImprimirVale_01_General(string Folio, string GUID, int Iteracion, string ClaveSSA, int Cantidad)
        {
            bool bRegresa = true;
            dImporte = Importe;
            
            datosCliente.Funcion = "ImprimirVale()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);

            myRpt.RutaReporte = sRutaReportes;
            myRpt.TituloReporte = "Impresión de vale";

            myRpt.Add("IdEmpresa", sEmpresa);
            myRpt.Add("IdEstado", sEstado);
            myRpt.Add("IdFarmacia", sFarmacia);
            myRpt.Add("Folio", General.Fg.PonCeros(Folio, 8));

            //// Jesus Diaz 
            //   Cambio generado por PUEBLA
            myRpt.NombreReporte = "PtoVta_Vales_Generacion_" + DtGeneral.EstadoConectado;
            myRpt.NombreReporte = GnFarmacia.Vales_Plantilla_Personalizada;


            //// Jesús Díaz 20190715.1155 
            if(GnFarmacia.Vales_TipoDeImpresion == Vales_TipoDeImpresion.Clave || GnFarmacia.Vales_TipoDeImpresion == Vales_TipoDeImpresion.Clave_Pieza )
            {
                myRpt.NombreReporte = GnFarmacia.Vales_Plantilla_Personalizada_Desglozada;
                myRpt.Add("Iteracion", Iteracion); 
                myRpt.Add("ClaveSSA", ClaveSSA);
                myRpt.Add("Cantidad", Cantidad);
            }

            myRpt.Impresora = DtGeneral.XmlEdoConfig.GetValues("ImpresoraTickets"); 
            myRpt.NumeroDeCopias = iNumeroDeCopias;
            myRpt.EnviarAImpresora = bImprimirDirecto;
            ////myRpt.CargarReporte(true, true);
            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, datosCliente);

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte del Vale.");
            }

            return bRegresa;
        }

        public bool ImprimirValeReembolso(string Folio)
        {
            bool bRegresa = true;
            dImporte = Importe;

            datosCliente.Funcion = "ImprimirValeReembolso()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;

            myRpt.RutaReporte = sRutaReportes;

            myRpt.Add("IdEmpresa", sEmpresa);
            myRpt.Add("IdEstado", sEstado);
            myRpt.Add("IdFarmacia", sFarmacia);
            myRpt.Add("Folio", General.Fg.PonCeros(Folio, 8));
            // myRpt.NombreReporte = "PtoVta_Vales_Generacion";

            //// Jesus Diaz 
            //   Cambio generado por PUEBLA
            myRpt.NombreReporte = "PtoVta_Vales_Generacion_Reembolso_" + DtGeneral.EstadoConectado;

            myRpt.Impresora = DtGeneral.XmlEdoConfig.GetValues("ImpresoraTickets"); 
            myRpt.EnviarAImpresora = bImprimirDirecto;
            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, datosCliente);

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte del Vale.");
            }

            return bRegresa;
        }

        private string GeneraMD5(string FolioVale)
        {
            sMd5 = DtGeneral.EmpresaConectada + DtGeneral.EstadoConectado + DtGeneral.FarmaciaConectada + FolioVale;
            sMd5 = GenerarMD5(sMd5);
            return sMd5;
        }

        private static string GenerarMD5(string Cadena)
        {
            string sMD5 = "";
            byte[] bytesCadena, bytesRegresa;
            MD5CryptoServiceProvider MD5Crypto = new MD5CryptoServiceProvider();

            bytesCadena = System.Text.Encoding.UTF8.GetBytes(Cadena);
            bytesRegresa = MD5Crypto.ComputeHash(bytesCadena);

            sMD5 = BitConverter.ToString(bytesRegresa);
            sMD5 = sMD5.Replace("-", "").ToLower();

            return sMD5;
        }

        #endregion Impresion 

    }
}
