using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos;

namespace Farmacia.Pedidos
{
    public enum TipoReportePedido
    {
        Ninguno = 0,
        Contado = 1,
        Credito = 2
    }

    public class clsImprimirPedidos
    {
        clsDatosConexion datosCnn;
        clsDatosCliente datosCliente;
        wsFarmacia.wsCnnCliente conexionWeb;
        string sUrl = "";

        string sEmpresa = "";
        string sEstado = "";
        string sFarmacia = "";
        string sRutaReportes = "";
        TipoReportePedido tpReporte = TipoReportePedido.Ninguno;
        bool bImprimirDirecto = false; 

        public clsImprimirPedidos(clsDatosConexion DatosCnn, clsDatosCliente DatosCliente,
            string Empresa, string Estado, string Farmacia, string Url, string RutaReportes, TipoReportePedido TipoReporte)
        {
            this.datosCnn = DatosCnn;
            this.datosCliente = DatosCliente;
            this.sEmpresa = Empresa;
            this.sEstado = Estado;
            this.sFarmacia = Farmacia;
            this.sRutaReportes = RutaReportes;
            this.tpReporte = TipoReporte;
            this.sUrl = Url;

            conexionWeb = new Farmacia.wsFarmacia.wsCnnCliente();
            conexionWeb.Url = sUrl;
        }

        //public double Importe 
        //{
        //    get { return dImporte; }
        //    set { dImporte = value; }
        //}

        public TipoReportePedido TipoDeReporte
        {
            get { return tpReporte; }
            set { tpReporte = value; }
        }

        public bool MostrarVistaPrevia
        {
            get { return bImprimirDirecto; }
            set { bImprimirDirecto = !value; }
        } 

        //public bool Imprimir(string Folio)
        //{
        //    return Imprimir(Folio, dImporte);
        //}

        public bool Imprimir(string Folio, string TipoPedido)
        {
            return Imprimir(Folio, TipoPedido, TipoDePedido.Productos); 
        }

        public bool Imprimir(string Folio, string TipoPedido, TipoDePedido TipoDePedido )
        {
            bool bRegresa = true;
            //dImporte = Importe;

            datosCliente.Funcion = "Imprimir()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;

            myRpt.RutaReporte = sRutaReportes;

           
            myRpt.Add("IdEmpresa", sEmpresa);
            myRpt.Add("IdEstado", sEstado);
            myRpt.Add("IdFarmacia", sFarmacia);
            myRpt.Add("Folio", General.Fg.PonCeros(Folio, 6));
            myRpt.Add("IdTipoPedido", TipoPedido);


            if (TipoDePedido == TipoDePedido.Claves)
            {
                myRpt.NombreReporte = "PtoVta_COM_Pedido_Claves.rpt";
            }
            else
            {
                myRpt.NombreReporte = "PtoVta_COM_Pedido_Productos.rpt";
            }


            myRpt.EnviarAImpresora = bImprimirDirecto; 
            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, datosCliente); 

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }

            return bRegresa;
        }
    }
}
