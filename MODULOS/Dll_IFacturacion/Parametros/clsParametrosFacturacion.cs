using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;

namespace Dll_IFacturacion
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
        string sListaDeParametros_a_Ejecutar = "";

        public clsParametrosFacturacion(clsDatosConexion DatosConexion, clsDatosApp DatosApp, string IdEstado, string IdFarmacia, string Arbol)
        {
            this.cnn = new clsConexionSQL(DatosConexion);
            this.leer = new clsLeer(ref cnn);
            this.Error = new clsGrabarError(DatosApp, "clsParametrosFacturacion");

            sListaDeParametros_a_Ejecutar = "";
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

                ////// Procesar en una sola ejecución todos los parámetros
                RegistrarListaDeParametros();
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
        public bool RegistrarListaDeParametros()
        {
            bool bRegresa = true;

            if (!leer.Exec(sListaDeParametros_a_Ejecutar))
            {
                Error.GrabarError(leer, "GrabarParametro");
                bRegresa = false;
            }

            return bRegresa;
        }

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
            string sSql = string.Format(" Exec spp_Mtto_Net_CFG_Parametros_Facturacion \n" +
                "\t@IdEstado = '{0}', @IdFarmacia = '{1}', @ArbolModulo = '{2}', @NombreParametro = '{3}', @Valor = '{4}', \n" +
                "\t@Descripcion = '{5}', @EsDeSistema = '{6}', @EsEditable = '{7}', @Actualizar = '{8}'",
                sIdEstado, sIdFarmacia, ArbolModulo, NombreParametro, Valor, Descripcion, EsDeSistema.ToString(), EsEditable.ToString(), Actualizar.ToString());

            ////if (!leer.Exec(sSql))
            ////{
            ////    Error.GrabarError(leer, "GrabarParametro");
            ////    bRegresa = false;
            ////}

            sListaDeParametros_a_Ejecutar += sSql + "\n ";


            return bRegresa;
        }

        private void GenerarParametros()
        {
            Informacion();
            RutaDeReportes();
            EsquemaDeFacturacion();
            DocumentoAdministracion();
            AnexarLotes_y_Caducidades_EnRemision();
            ReportesPersonalizados();
        }

        #region Parametros 
        private void Informacion()
        {
            GrabarParametro(sArbol, "BaseDeDatosOperacion", @"", "Determina la Base de datos Regional (Operación) de la cual se alimenta el Módulo de Facturación.", false, true);
        }

        private void RutaDeReportes()
        {
            GrabarParametro(sArbol, "RutaReportes", @"", "Determina ruta donde se encuentran los reportes del Módulo de Facturación.", false, true);
        }

        private void EsquemaDeFacturacion()
        {
            //GrabarParametro(sArbol, "MostrarPreciosBaseEnFacturacion_Descuentos", @"FALSE", "Determina si se utilizan los precios base de los productos para la generación de XML.", false, true);


            GrabarParametro(sArbol, "EsquemaDeFacturacion", @"0", "Determina el esquema de facturación a utilizar." +
                @"\t\n0 ==> Sin espeficicar \t\n1 ==> Libre \t\n2 ==> Por montos", false, true);


            GrabarParametro(sArbol, "ImplementaMascaras", @"FALSE", "Determina si se utiliza la información de Mascaras para la generación de Facturas.", false, true);
            GrabarParametro(sArbol, "ForzarImplementacionDeMascaras", @"FALSE", "Determina si la implementación de Mascaras es obligatorio.", false, true);

            GrabarParametro(sArbol, "ImplementaClaveSSA_Base__Identificador", @"TRUE", "Determina si implementa la Clave SSA Base como identificador en las facturas electrónicas.", false, true);

            GrabarParametro(sArbol, "ImplementaInformacionPredeterminada", @"TRUE", "Determina si se precarga información para facturación centralizada.", false, true);


            GrabarParametro(sArbol, "Factura_UsoCDFI", @"G03", "Determina la Clave de Uso de CFDI para los CFDI de Ingreso-Facturas.", false, true);  
            GrabarParametro(sArbol, "Factura_FormaDePago", @"99", "Determina la Forma de pago predeterminada para facturación centralizada.", false, true);
            GrabarParametro(sArbol, "Factura_PlazoDiasVenceFactura", @"30", "Determina el Plazo de vencimiento en días del documento emitido para facturación centralizada.", false, true);
            GrabarParametro(sArbol, "Factura_CondicionesDePago", @"CRÉDITO", "Determina las Condiciones de pago predeterminada para facturación centralizada.", false, true);
            GrabarParametro(sArbol, "Factura_MetodoDePago", @"PPD", "Determina el Método de pago predeterminada para facturación centralizada.", false, true);
            GrabarParametro(sArbol, "Factura_MetodoDePagoReferencia", @"****", "Determina la Referencia de pago predeterminada para facturación centralizada.", false, true); 
            GrabarParametro(sArbol, "Factura_SAT_ClaveProducto__Servicio", @"", "Determina la Clave Clave de Producto y/o Servicio predeterminado para el Servicio de Administración.", false, true);
            GrabarParametro(sArbol, "Factura_SAT_UnidadDeMedida__Servicio", @"", "Determina la Unidad de Medida predeterminada para el Servicio de Administración.", false, true);


            GrabarParametro(sArbol, "NotaDeCredito_UsoCDFI", @"G02", "Determina la Clave de Uso de CFDI para los CFDI de Egreso-Notas de Credito.", false, true);
            GrabarParametro(sArbol, "NotaDeCredito_TipoDeRelacionCFDI", @"01", "Determina el Tipo de Relación con los CFDIs involucrados.", false, true);            
            GrabarParametro(sArbol, "NotaDeCredito_MetodoDePago", @"PUE", "Determina el Método de pago predeterminada..", false, true);
            GrabarParametro(sArbol, "NotaDeCredito_FormaDePago", @"99", "Determina la Forma de pago predeterminada.", false, true);
            GrabarParametro(sArbol, "NotaDeCredito_SAT_ClaveProducto", @"84111506", "Determina la Clave de Producto y/o Servicio predeterminado.", false, true);
            GrabarParametro(sArbol, "NotaDeCredito_SAT_UnidadDeMedida", @"ACT", "Determina la Unidad de Medida predeterminada.", false, true);



            GrabarParametro(sArbol, "Pago_UsoCDFI", @"P01", "Determina la Clave de Uso de CFDI para los Complementos de Pago.", false, true);
            GrabarParametro(sArbol, "Pago_FormaDePago", @"99", "Determina la Forma de pago predeterminada para los Complementos de Pago.", false, true);
            GrabarParametro(sArbol, "Pago_Moneda", @"MXN", "Determina la Moneda de pago predeterminada para los Complementos de Pago.", false, true);



            #region Remisiones 
            GrabarParametro(sArbol, "Factura_Requiere_FF", @"FALSE", "Especifica si la facturación manual requiere Fuente de Financiamiento.", false, true);
            GrabarParametro(sArbol, "Factura_Requiere_TipoDocto", @"FALSE", "Especifica si la facturación manual requiere el Tipo de documento (Producto ó Servicio).", false, true);
            GrabarParametro(sArbol, "Factura_Requiere_TipoInsumo", @"FALSE", "Especifica si la facturación manual requiere el Tipo de insumo (Medicamento ó Material de curación ó Ambos).", false, true);

            GrabarParametro(sArbol, "Factura_Obs_01_Default", @"FALSE", "Especifica si la Observación 01 muestra un valor por default.", false, true);
            GrabarParametro(sArbol, "Factura_Obs_02_Default", @"FALSE", "Especifica si la Observación 02 muestra un valor por default.", false, true);
            GrabarParametro(sArbol, "Factura_Obs_03_Default", @"FALSE", "Especifica si la Observación 03 muestra un valor por default.", false, true);

            GrabarParametro(sArbol, "Factura_Ref_01_Default", @"FALSE", "Especifica si la Referencia 01 muestra un valor por default.", false, true);
            GrabarParametro(sArbol, "Factura_Ref_02_Default", @"FALSE", "Especifica si la Referencia 02 muestra un valor por default.", false, true);
            GrabarParametro(sArbol, "Factura_Ref_03_Default", @"FALSE", "Especifica si la Referencia 03 muestra un valor por default.", false, true);
            GrabarParametro(sArbol, "Factura_Ref_04_Default", @"FALSE", "Especifica si la Referencia 04 muestra un valor por default.", false, true);
            GrabarParametro(sArbol, "Factura_Ref_05_Default", @"FALSE", "Especifica si la Referencia 05 muestra un valor por default.", false, true);



            GrabarParametro(sArbol, "Factura_Remision___CuotaServicio_General", @"0.00", "Especifica la Cuota del Servicio de Dispensación.", false, true);
            GrabarParametro(sArbol, "Factura_Remision___CantidadServicio_General", @"1", "Especifica la Cantidad del Servicio de Dispensacion.", false, true);

            GrabarParametro(sArbol, "Factura_Remision___Servicio_Unitario", @"FALSE", "Especifica si el Servicio de cobra de forma Unitaria ( Cantidad = 1, PrecioUnitario = suma de Precio x Cantidad).", false, true);

            GrabarParametro(sArbol, "Factura_Remision___DescripcionServicio_General", @"SERVICIO DE DISPENSACIÓN DE MEDICAMENTOS Y MATERIAL DE CURACIÓN EN FARMACIA SUBROGADA",
                "Especifica la Descripción a mostrar para las Facturas concentradas de Servicio general (Medicamento y Material de curación) .", false, true);


            GrabarParametro(sArbol, "Factura_Remision___DescripcionServicio_Medicamento", @"SERVICIO DE DISPENSACIÓN DE MEDICAMENTOS EN FARMACIA SUBROGADA", 
                "Especifica la Descripción a mostrar para las Facturas concentradas de Medicamentos.", false, true);
            GrabarParametro(sArbol, "Factura_Remision___DescripcionServicio_MC", @"SERVICIO DE DISPENSACIÓN DE MATERIAL DE CURACIÓN E INSUMOS EN FARMACIA SUBROGADA",
                "Especifica la Descripción a mostrar para las Facturas concentradas de Material de curación e insumos.", false, true);
            #endregion Remisiones 

            GrabarParametro(sArbol, "Establecimiento_Nombre", @"", "Especifica el nombre del establecimiento que emite el CFDI Factura.", false, true);
            GrabarParametro(sArbol, "Establecimiento_Domicilio", @"", "Especifica el domicilio del establecimiento que emite el CFDI Factura.", false, true);

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

        /*
        private static string sFormatoImpresion_ComplementoDePagos = "";
        private static string sFormatoImpresion_NotasDeCredito = "";
        private static string sFormatoImpresion_Traslados = ""; 
         */

        #endregion Parametros
        #endregion Funciones y Procedimientos privados

    }
}

