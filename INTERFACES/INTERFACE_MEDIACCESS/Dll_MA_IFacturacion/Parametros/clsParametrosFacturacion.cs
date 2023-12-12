using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;

namespace Dll_MA_IFacturacion
{
    public class clsParametrosFacturacion
    {
        clsConexionSQL cnn;
        clsLeer leer;
        clsGrabarError Error;

        string sArbol = "";
        // string sParam = "";
        string sIdEstado = "";
        string sIdFarmacia = "";
        DataSet dtsParametros;

        public clsParametrosFacturacion(clsDatosConexion DatosConexion, clsDatosApp DatosApp, string IdEstado, string IdFarmacia, string Arbol)
        {
            this.cnn = new clsConexionSQL(DatosConexion);
            this.leer = new clsLeer(ref cnn);
            this.Error = new clsGrabarError(DatosApp, "clsParametrosFacturacion");

            this.sArbol = Arbol;
            this.sIdEstado = IdEstado;
            this.sIdFarmacia = IdFarmacia;
            PreparaDtsParametros();
        }

        #region Funciones y Procedimientos publicos 
        public bool CargarParametros()
        {
            return CargarParametros(true); 
        }

        public bool CargarParametros(bool RegistrarParametros)
        {
            bool bRegresa = true;

            if (RegistrarParametros)
            {
                GenerarParametros();
            }

            string sSql = string.Format("Select ArbolModulo, NombreParametro, Valor, Descripcion, EsDeSistema, EsEditable " +
                " From Net_CFG_Parametros_Facturacion (NoLock) " + 
                " Where IdEstado = '{0}' and IdFarmacia = '{1}' ", sIdEstado, sIdFarmacia);
            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "CargarParametros");
            }
            else
            {
                dtsParametros = leer.DataSetClase;
                ////while (leer.Leer())
                ////{
                ////}
            }

            return bRegresa;
        }

        public string GetValor(string Parametro)
        {
            return GetValor(sArbol, Parametro);
        }

        public bool GetValorBool(string Parametro)
        {
            bool bRegresa = false;
            string sValor = GetValor(sArbol, Parametro);

            if (sValor != "")
            {
                try
                {
                    bRegresa = Convert.ToBoolean(sValor);
                }
                catch { }
            }

            return bRegresa; 
        }

        public int GetValorInt(string Parametro)
        {
            int iRegresa = 0;
            string sValor = GetValor(sArbol, Parametro);

            if (sValor != "")
            {
                try
                {
                    iRegresa = Convert.ToInt32(sValor);
                }
                catch { }
            }

            return iRegresa;
        }

        public string GetValor(string Arbol, string Parametro)
        {
            string sRegresa = "";
            string sDatos = string.Format("ArbolModulo = '{0}' and NombreParametro = '{1}' ", Arbol, Parametro);

            try
            {
                DataRow[] dt = dtsParametros.Tables[0].Select(sDatos);
                if (dt.Length > 0)
                {
                    sRegresa = Convert.ToString(dt[0]["Valor"].ToString());
                }

                //leer.DataSetClase = dtsParametros;
                //while (leer.Leer())
                //{
                //}
            }
            catch { }
            return sRegresa;
        }
        #endregion Funciones y Procedimientos publicos
        
        #region Funciones y Procedimientos
        private Type GetType(TypeCode TipoDato)
        {
            return Type.GetType("System." + TipoDato.ToString());
        }

        private void PreparaDtsParametros()
        {
            dtsParametros = new DataSet();
            DataTable dtParam = new DataTable("Parametros");

            dtParam.Columns.Add("ArbolModulo", GetType(TypeCode.String));
            dtParam.Columns.Add("NombreParametro", GetType(TypeCode.DateTime));
            dtParam.Columns.Add("Valor", GetType(TypeCode.DateTime));

            dtParam.Columns.Add("Descripcion", GetType(TypeCode.String));
            dtParam.Columns.Add("EsDeSistema", GetType(TypeCode.Int32));
            dtsParametros.Tables.Add(dtParam);
        }
        #endregion Funciones y Procedimientos 

        #region Funciones y Procedimientos privados 
        ////public bool GrabarParametro(string ArbolModulo, string NombreParametro, string Valor)
        ////{
        ////    return GrabarParametro(ArbolModulo, NombreParametro, Valor, "", false, false, 1);
        ////} 

        public bool GrabarParametro(string ArbolModulo, string NombreParametro, string Valor, string Descripcion, bool EsDeSistema, bool EsEditable)
        {
            return GrabarParametro(ArbolModulo, NombreParametro, Valor, Descripcion, EsDeSistema, EsEditable, 0);
        }

        public bool GrabarParametro(string ArbolModulo, string NombreParametro, string Valor, string Descripcion, bool EsDeSistema, bool EsEditable, int Actualizar )
        {
            bool bRegresa = true;
            string sSql = string.Format(" Exec spp_Mtto_Net_CFG_Parametros_Facturacion '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}'  ",
                sIdEstado, sIdFarmacia, ArbolModulo, NombreParametro, Valor, Descripcion, EsDeSistema.ToString(), EsEditable.ToString(), Actualizar.ToString());
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "GrabarParametro");
                bRegresa = false;
            }
            return bRegresa;
        }

        private void GenerarParametros()
        {
            RutaDeReportes();
            EsquemaDeFacturacion();
            DocumentoAdministracion();
            AnexarLotes_y_Caducidades_EnRemision();
            ReportesPersonalizados();
        }

        #region Parametros 
        private void RutaDeReportes()
        {
            GrabarParametro(sArbol, "RutaReportes", @"", "Determina ruta donde se encuentran los reportes del Módulo de Facturación.", false, true);
        }

        private void EsquemaDeFacturacion()
        {
            GrabarParametro(sArbol, "EsquemaDeFacturacion", @"0", "Determina el esquema de facturación a utilizar." +
                @"\t\n0 ==> Sin espeficicar \t\n1 ==> Libre \t\n2 ==> Por montos", false, true);


            GrabarParametro(sArbol, "ImplementaInformacionPredeterminada", @"FALSE", "Determina si se precarga información para facturación centralizada.", false, true);
            GrabarParametro(sArbol, "MostrarPreciosBaseEnFacturacion_Descuentos", @"FALSE", "Determina si se utilizan los precios base de los productos para la generación de XML.", false, true);
            GrabarParametro(sArbol, "FormaDePago", @"01", "Determina la Forma de pago predeterminada para facturación centralizada.", false, true);
            GrabarParametro(sArbol, "PlazoDiasVenceFactura", @"15", "Determina el Plazo de vencimiento en días del documento emitido para facturación centralizada.", false, true);

            GrabarParametro(sArbol, "MetodoDePago", @"03", "Determina el Método de pago predeterminada para facturación centralizada.", false, true);
            GrabarParametro(sArbol, "MetodoDePagoReferencia", @"", "Determina la Referencia de pago predeterminada para facturación centralizada.", false, true);


            GrabarParametro(sArbol, "Factura_UsoCDFI", @"G03", "Determina la Clave de Uso de CFDI para los CFDI de Ingreso-Facturas.", false, true);
            GrabarParametro(sArbol, "Factura_FormaDePago", @"99", "Determina la Forma de pago predeterminada para facturación centralizada.", false, true);
            GrabarParametro(sArbol, "Factura_PlazoDiasVenceFactura", @"30", "Determina el Plazo de vencimiento en días del documento emitido para facturación centralizada.", false, true);
            GrabarParametro(sArbol, "Factura_CondicionesDePago", @"CRÉDITO", "Determina las Condiciones de pago predeterminada para facturación centralizada.", false, true);
            GrabarParametro(sArbol, "Factura_MetodoDePago", @"PPD", "Determina el Método de pago predeterminada para facturación centralizada.", false, true);
            GrabarParametro(sArbol, "Factura_MetodoDePagoReferencia", @"****", "Determina la Referencia de pago predeterminada para facturación centralizada.", false, true);
            GrabarParametro(sArbol, "Factura_SAT_ClaveProducto__Servicio", @"93151507", "Determina la Clave Clave de Producto y/o Servicio predeterminado para el Servicio de Administración.", false, true);
            GrabarParametro(sArbol, "Factura_SAT_UnidadDeMedida__Servicio", @"ACT", "Determina la Unidad de Medida predeterminada para el Servicio de Administración.", false, true);


            GrabarParametro(sArbol, "NotaDeCredito_UsoCDFI", @"G02", "Determina la Clave de Uso de CFDI para los CFDI de Egreso-Notas de Credito.", false, true);
            GrabarParametro(sArbol, "NotaDeCredito_TipoDeRelacionCFDI", @"01", "Determina el Tipo de Relación con los CFDIs involucrados.", false, true);
            GrabarParametro(sArbol, "NotaDeCredito_MetodoDePago", @"PUE", "Determina el Método de pago predeterminada..", false, true);
            GrabarParametro(sArbol, "NotaDeCredito_FormaDePago", @"99", "Determina la Forma de pago predeterminada.", false, true);
            GrabarParametro(sArbol, "NotaDeCredito_SAT_ClaveProducto", @"84111506", "Determina la Clave de Producto y/o Servicio predeterminado.", false, true);
            GrabarParametro(sArbol, "NotaDeCredito_SAT_UnidadDeMedida", @"ACT", "Determina la Unidad de Medida predeterminada.", false, true);



            GrabarParametro(sArbol, "Pago_UsoCDFI", @"P01", "Determina la Clave de Uso de CFDI para los Complementos de Pago.", false, true);
            GrabarParametro(sArbol, "Pago_FormaDePago", @"99", "Determina la Forma de pago predeterminada para los Complementos de Pago.", false, true);
            GrabarParametro(sArbol, "Pago_Moneda", @"MXN", "Determina la Moneda de pago predeterminada para los Complementos de Pago.", false, true);



        }

        private void DocumentoAdministracion()
        {
            GrabarParametro(sArbol, "DocumentoAdministracion", @"1", "Determina el esquema para la generación del documento de cobro de administración." +
                @"\t\n1 ==> Detallado \t\n2 ==> Concentrado", false, true);
        }

        private void AnexarLotes_y_Caducidades_EnRemision()
        {
            GrabarParametro(sArbol, "AnexarLotes_y_Caducidades_EnRemision", @"FALSE", "Determina si se anexan los Lotes y Caducidades en la factura electrónica.", false, true);
        }

        private void UtilizarUltimasObservaciones_FacturacionManual()
        {
            GrabarParametro(sArbol, "UtilizarUltimasObservaciones_FacturacionManual", @"FALSE", "Determina si se reutilizan las ultimas Observaciones capturadas en la facturación manual y de carga de archivos.", false, true);
        }

        private void UnidadDeMedida_Predeterminada()
        {
            GrabarParametro(sArbol, "UnidadDeMedida", @"001", "Determina la Unidad de Medida predeterminada para la emisión de facturas.", false, true);
        }

        private void ReportesPersonalizados()
        {
            GrabarParametro(sArbol, "Vta_Impresion_Personalizada_Ticket", "FACT_REMISIONES", "Determina si el estado utiliza impresión personalizada de remisiones.", false, true);

            GrabarParametro(sArbol, "FormatoImpresion_Facturas", "CFDI_Factura", "Determina si el nombre de formato de impresión para las Facturas emitidas.", false, true);
            GrabarParametro(sArbol, "FormatoImpresion_ComplementoDePagos", "CFDI_ComplementoDePagos", "Determina si el nombre de formato de impresión para los Complementos de pago emitidos.", false, true);
            GrabarParametro(sArbol, "FormatoImpresion_NotasDeCredito", "CFDI_NotasDeCredito", "Determina si el nombre de formato de impresión para las Notas de Crédito emitidas.", false, true);
            GrabarParametro(sArbol, "FormatoImpresion_Traslados", "CFDI_Traslados", "Determina si el nombre de formato de impresión para los Traslados emitidos.", false, true);
            GrabarParametro(sArbol, "FormatoImpresion_Anticipo", "CFDI_Anticipos", "Determina si el nombre de formato de impresión para los Anticipos recibidos.", false, true);


            GrabarParametro(sArbol, "FormatoImpresion_Remisiones", "FACT_REMISIONES", "Determina si el nombre de formato de impresión para las remisiones emitidas.", false, true);
            GrabarParametro(sArbol, "FormatoImpresion_FacturasRemisiones", "FACT_FACTURA_REMISIONES", "Determina si el nombre de formato de impresión para las Facturas y Remisiones asociadas.", false, true);
        }

        #endregion Parametros  
        #endregion Funciones y Procedimientos privados

    }
}

