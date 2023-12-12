#region USING
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
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
using System.Threading;
using System.ServiceModel;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;

using DllFarmaciaSoft;
using Dll_IFacturacion.Configuracion;
#endregion USING

namespace Dll_IFacturacion.XSA
{
    internal class clsFormaMetodoPago
    {
        #region Declaracion de Variables 
        DataSet dtsFormaPago = new DataSet();
        DataSet dtsMetodoPago = new DataSet();
        clsConsultas consulta;

        string sIdFormaDePago = "";
        string sFormaDePago = "";
        string sObservacionesPago;         
        string sIdMetodoDePago = "";
        DataSet dtsMetodosDePago;
        string sMetodoDePago = "";
        string sListaMetodoDePago = "";
        string sListaCuentaDePago = ""; 
        bool bTieneFechaVencimiento = false;
        DateTime dtFechaVencimiento = DateTime.Now; 
        string sObservaciones = "";
        double dImporteACobrar = 0;
        bool bInformacionGuardada = false;
        bool bImportePagadoCompleto = false;
        eTipoDeFacturacion tpModuloFacturacion = eTipoDeFacturacion.Ninguna;
        eVersionCFDI tpVersionCFDI = eVersionCFDI.Ninguna;
        string sVersionCFDI = ""; 
        #endregion Declaracion de Variables

        #region Constructores y Destructor de Clase 
        public clsFormaMetodoPago()
            : this(eTipoDeFacturacion.Ninguna, eVersionCFDI.Version__3_2) 
        {
        }

        public clsFormaMetodoPago(eTipoDeFacturacion ModuloFacturacion, eVersionCFDI VersionCFDI)
        {
            consulta = new clsConsultas(General.DatosConexion, DtIFacturacion.DatosApp, "clsFormaMetodoPago");

            tpModuloFacturacion = ModuloFacturacion;
            tpVersionCFDI = VersionCFDI;

            sVersionCFDI = DtIFacturacion.VersionCFDI_ToString(tpVersionCFDI); 

            CargarFormas_Metodos_DePago(); 
        }
        #endregion Constructores y Destructor de Clase

        #region Propiedades 
        public string IdFormaDePago
        {
            get { return sIdFormaDePago; }
        }

        public string FormaDePago
        {
            get 
            { 
                if ( sFormaDePago == "" )
                {
                    sFormaDePago = DtIFacturacion.XMLFormaPago_Default;
                }
                return sFormaDePago; 
            }
        }

        public string ObservacionesPago
        {
            get 
            { 
                return sObservacionesPago; 
            }
        }

        public string IdMetodoPago 
        {
            get { return sIdMetodoDePago; } 
        }

        public string MetodoPago
        {
            get 
            {
                if (sMetodoDePago == "")
                {
                    sMetodoDePago = DtIFacturacion.XMLMetodoPago_Default;
                }
                return sMetodoDePago; 
            }
        }

        public string ListadMetodoPago_Tralix
        {
            get
            {
                return sListaMetodoDePago;
            }
        }

        public string ListaCuentaDePago_Tralix
        {
            get { return sListaCuentaDePago; }
        }

        public DataSet ListaDeMetodosDePago
        {
            get { return dtsFormaPago; }
            set { dtsFormaPago = value; }
        }

        public double ImporteACobrar
        {
            get { return dImporteACobrar; }
            set { dImporteACobrar = value; }
        }

        public bool ImportePagadoCompleto
        {
            get
            {
                bool bRegresa = !bInformacionGuardada ? false : bImportePagadoCompleto;
                return bRegresa;
            }
            set { ; }
        }

        public bool TieneFechaVencimiento
        {
            get { return bTieneFechaVencimiento; }
        }

        public DateTime FechaVencimiento
        {
            get { return dtFechaVencimiento; }
        }

        public string Observaciones
        {
            get { return sObservaciones; }
        }

        public eTipoDeFacturacion TipoDeFacturacion
        {
            get { return tpModuloFacturacion; }
            set { tpModuloFacturacion = value; }
        }
        #endregion Propiedades

        #region Funciones y Procedimientos Publicos
        public void Show()
        {
            Show(false); 
        }

        public void Show(bool CierreAutomatico)
        {
            FrmFormaMetodoPago f = new FrmFormaMetodoPago(tpVersionCFDI, dtsFormaPago, dtsMetodosDePago, dImporteACobrar);

            f.IdFormaDePago = sIdFormaDePago;
            f.IdMetodoDePago = sIdMetodoDePago;
            f.ObservacionesPago = sObservacionesPago;
            f.TieneFechaVencimiento = bTieneFechaVencimiento;
            f.FechaVencimiento = dtFechaVencimiento;
            f.Observaciones = sObservaciones;
            
            //f.ListadMetodosDePago = dtsMetodosDePago;
            f.ListadMetodosDePago = dtsFormaPago; 
            f.ModuloFacturacion = tpModuloFacturacion; 
            f.CierreAutomatico = CierreAutomatico; 

            f.ShowDialog();

            sIdFormaDePago = f.IdFormaDePago;
            sFormaDePago = f.FormaDePago;
            sIdMetodoDePago = f.IdMetodoDePago;
            sMetodoDePago = f.MetodoDePago;
            sListaMetodoDePago = f.ListaMetodoDePago;
            sListaCuentaDePago = f.ListaCuentaDePago; 

            ListaDeMetodosDePago = f.ListadMetodosDePago;
            bImportePagadoCompleto = f.ImportePagadoCompleto;
            sObservacionesPago = f.ObservacionesPago;
            bTieneFechaVencimiento = f.TieneFechaVencimiento;
            dtFechaVencimiento = f.FechaVencimiento;
            sObservaciones = f.Observaciones;
            bInformacionGuardada = f.InformacionGuardada; 
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados
        private void CargarFormas_Metodos_DePago()
        {
            bool bValidar = false; 

            ////if (tpVersionCFDI == eVersionCFDI.Version__3_2)
            ////{
            ////    dtsFormaPago = consulta.CFDI_FormasDePago(sVersionCFDI, "");

            ////    if (dtsMetodosDePago == null)
            ////    {
            ////        bValidar = true;
            ////        dtsMetodosDePago = consulta.CFDI_MetodosDePago_Listado("", sVersionCFDI, "CargarFormas_Metodos_DePago()");
            ////    }
            ////}

            ////if (tpVersionCFDI == eVersionCFDI.Version__3_3)
            {
                ////dtsFormaPago = consulta.CFDI_MetodosDePago_Listado("", sVersionCFDI, "CargarFormas_Metodos_DePago()");
                dtsFormaPago = consulta.CFDI_FormasDePago(sVersionCFDI, "");

                if (dtsMetodosDePago == null)
                {
                    ////dtsMetodosDePago = consulta.CFDI_FormasDePago(sVersionCFDI, "");
                    dtsMetodosDePago = consulta.CFDI_MetodosDePago_Listado("", sVersionCFDI, "CargarFormas_Metodos_DePago()");
                    bValidar = true;
                }
            }


            if (bValidar)
            {
                //dtsMetodosDePago = consulta.CFDI_MetodosDePago_Listado("", sVersionCFDI, "CargarFormas_Metodos_DePago()");

                if (DtIFacturacion.ImplementaInformacionPredeterminada)
                {
                    if (tpModuloFacturacion == eTipoDeFacturacion.Manual_Excel || tpModuloFacturacion == eTipoDeFacturacion.Manual)
                    {
                        clsLeer leerMetodosDePago = new clsLeer();
                        clsLeer leerDatos = new clsLeer(); 
                        //leerMetodosDePago.DataRowsClase = dtsMetodosDePago.Tables[0].Select(string.Format("IdFormaDePago = '{0}'", DtIFacturacion.MetodoDePago));

                        ////if (tpVersionCFDI == eVersionCFDI.Version__3_2)
                        {
                            leerDatos.DataSetClase = dtsFormaPago;
                            if (leerDatos.Leer())
                            {
                                try 
                                {
                                    leerMetodosDePago.DataRowsClase = dtsFormaPago.Tables[0].Select(string.Format(" IdFormaDePago = '{0}'", DtIFacturacion.FormaDePago));
                                }
                                catch { }
                            }
                        }

                        ////if (tpVersionCFDI == eVersionCFDI.Version__3_3)
                        ////{
                        ////    leerDatos.DataSetClase = dtsMetodosDePago;
                        ////    if (leerDatos.Leer())
                        ////    {
                        ////        try
                        ////        {
                        ////            leerMetodosDePago.DataRowsClase = dtsMetodosDePago.Tables[0].Select(string.Format(" IdFormaDePago = '{0}'", DtIFacturacion.MetodoDePago));
                        ////        }
                        ////        catch { }

                        ////        if (!leerMetodosDePago.Leer())
                        ////        {
                        ////            try
                        ////            {
                        ////                leerMetodosDePago.DataRowsClase = dtsMetodosDePago.Tables[0].Select(string.Format(" IdFormaDePago = '{0}'", DtIFacturacion.FormaDePago));
                        ////            }
                        ////            catch { }

                        ////        }
                        ////    }
                        ////}

                        if (leerMetodosDePago.Leer())
                        {
                            dtsFormaPago = leerMetodosDePago.DataSetClase;
                        }
                    }
                }
            }
        }
        #endregion Funciones y Procedimientos Privados
    }
}
