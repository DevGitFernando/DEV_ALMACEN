using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;

namespace DllFarmaciaSoft
{
    public class clsParametrosPtoVta
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

        public clsParametrosPtoVta(clsDatosConexion DatosConexion, clsDatosApp DatosApp, string IdEstado, string IdFarmacia, string Arbol)
        {
            this.cnn = new clsConexionSQL(DatosConexion);
            this.leer = new clsLeer(ref cnn);
            this.Error = new clsGrabarError(DatosApp, "clsParametrosPtoVta");

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

        public bool CargarParametros(bool EsPuntoDeVenta)
        {
            return CargarParametros(EsPuntoDeVenta, true);
        }

        public bool CargarParametros(bool EsPuntoDeVenta, bool RegistrarParametros)
        {
            bool bRegresa = true;
            string sSql = ""; 

            if (RegistrarParametros)
            {
                if (EsPuntoDeVenta)
                {
                    GenerarParametros_PuntoDeVenta();
                }
                else
                {
                    GenerarParametros_Almacen();
                }

                ////// Procesar en una sola ejecución todos los parámetros
                RegistrarListaDeParametros(); 
            }


            sSql = string.Format("Select ArbolModulo, NombreParametro, Valor, Descripcion, EsDeSistema, EsEditable " +
                " From Net_CFGC_Parametros (NoLock) Where IdEstado = '{0}' and IdFarmacia = '{1}' ", sIdEstado, sIdFarmacia);
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

            if(!leer.Exec(sListaDeParametros_a_Ejecutar))
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
            string sSql = ""; 

            sSql = string.Format(" Exec spp_Mtto_Net_CFGC_Parametros " + 
                 "  @IdEstado = '{0}', @IdFarmacia = '{1}', @ArbolModulo = '{2}', @NombreParametro = '{3}', @Valor = '{4}', @Descripcion = '{5}', " + 
                 " @EsDeSistema = '{6}', @EsEditable = '{7}', @Actualizar = '{8}'  ", 
                 sIdEstado, sIdFarmacia, ArbolModulo, NombreParametro, Valor, Descripcion, EsDeSistema.ToString(), EsEditable.ToString(), Actualizar.ToString());

            ////if (!leer.Exec(sSql))
            ////{
            ////    Error.GrabarError(leer, "GrabarParametro");
            ////    bRegresa = false;
            ////}

            sListaDeParametros_a_Ejecutar += sSql + "\n ";  

            return bRegresa; 
        }

        private void GenerarParametros_PuntoDeVenta()
        {
            ClienteSeguroPopular();
            SubClienteEstado();
            Cliente_SubClienteDefaultOperacion(); 

            FechaOperacionSistema();
            EfectivoEnCajaPermitido();
            ClientePublicoGeneral();
            RutaDeReportes();
            ClaveCliente_SS_Estado();
            PedidosAutomaticos_Y_Especiales();
            ImpresionDetalladaTickets();
            FechaCriterioValidacion();
            MostrarLotesSinExistencia();
            MostrarCapturaDeClavesRequeridas();
            PeriodoCierreDeTicketsPermitido();
            DispensarSoloCuadroBasico();
            DiasArevisarpedidosCedis();
            ServidorAlmacenRegional();
            ClaveDispensacionRecetasForaneas();
            ExportaInformacion();

            EmiteVales();
            FechaRecetas();

            PermitirDispensacionVenta_ConExistenciaConsignacion();
            SubFarmaciaTraspasosEstados();
            ServidorDedicado();
            InformacionAdicional();
            ClavesInventariosAleatorios();
            Procesos_Al_CorteParcial_y_CorteDiario();
            PermitirAjustesInventario_Con_ExistenciaEnTransito();
            ValidarClavesEnPerfil();
            ReferenciaAuxiliarBeneficiario();
            PadronDeBeneficiarios();
            EmisionVales_ValidarExistencia();
            ValidarFoliosDeRecetaUnicos();
            Vales_Maneja_FirmaElectronica();
            EntradaConsinga_MostrarSubFarmaciaEmulaVenta();
            Modificar_Detalles_OrdenesDeCompra();
            ForzarCapturaEnMultiplosDeCajas();

            ConfirmacionConHuellas();
            ImplementaCodificacion(); 

            OperacionConMaquila();

            Vta_ImpresionPersonalizada_Ticket();
            ValidarBeneficariosAlertas();
            ValidarClavesEnCuadroBasico();

            CamaHabitacion();


            ImplementaInterfaceExpedienteElectronico();
            ImplementaDigitalizacionDeRecetas();

            Controlados_DispensacionPermitida();

            Sesion();

            HabilitarTransferenciasControlado();

            ImplementaImpresionDesglosada_VtaTS();
            Dispensacion_ImplementaExpiracion();


            if (DtGeneral.ModuloMA_EnEjecucion == TipoModulo_MA.Farmacia || DtGeneral.ModuloMA_EnEjecucion == TipoModulo_MA.Almacen)
            {
                Transferencias_Interestatales__Farmacias(true);
            }
            else
            {
                //////// Se habilitan las Transferencias Interestatales para todas las unidades 
                Transferencias_Interestatales__Farmacias(true);
            }

        }

        private void GenerarParametros_Almacen()
        {
            ClienteSeguroPopular();
            SubClienteEstado();
            Cliente_SubClienteDefaultOperacion(); 

            FechaOperacionSistema();
            ClientePublicoGeneral();
            RutaDeReportes();
            ClaveCliente_SS_Estado();
            PedidosAutomaticos_Y_Especiales();
            ImpresionDetalladaTickets();
            FechaCriterioValidacion();
            MostrarLotesSinExistencia();
            MostrarCapturaDeClavesRequeridas();
            PeriodoCierreDeTicketsPermitido();
            DispensarSoloCuadroBasico();
            DiasArevisarpedidosCedis();
            ServidorAlmacenRegional();
            ExportaInformacion();
            

            ManejoDeUbicaciones();
            FechaRecetas(); 

            PermitirDispensacionVenta_ConExistenciaConsignacion();
            SubFarmaciaTraspasosEstados(); 
            ServidorDedicado();
            InformacionAdicional();
            ClavesInventariosAleatorios();
            Procesos_Al_CorteParcial_y_CorteDiario();
            ClaveDispensacionUnidadesNoAdministradas();
            PermitirAjustesInventario_Con_ExistenciaEnTransito();
            ValidarClavesEnPerfil();
            ReferenciaAuxiliarBeneficiario();
            PadronDeBeneficiarios();
            ValidarUbicacionesEnCapturaDeSurtido();
            SurtidoDePedidos();
            Modificar_Detalles_OrdenesDeCompra();
            ForzarCapturaEnMultiplosDeCajas();

            ConfirmacionConHuellas();
            ImplementaCodificacion(); 

            OperacionConMaquila();

            Vta_ImpresionPersonalizada_Ticket();
            ValidarClavesEnCuadroBasico();

            Controlados_DispensacionPermitida();
            Sesion();

            ImplementaImpresionDesglosada_VtaTS();
            Dispensacion_ImplementaExpiracion();


            InventariosCiclicos();

            if (DtGeneral.ModuloMA_EnEjecucion == TipoModulo_MA.Farmacia || DtGeneral.ModuloMA_EnEjecucion == TipoModulo_MA.Almacen)
            {
                Transferencias_Interestatales__Farmacias(true);
            }
            else
            {
                //////// Se habilitan las Transferencias Interestatales para todas las unidades 
                Transferencias_Interestatales__Farmacias(true);
            }
        }


        #region Parametros 
        private void PedidosAutomaticos_Y_Especiales()
        {
            GrabarParametro(sArbol, "GeneraPedidosAutomaticos", @"FALSE", "Determina si la Unidad genera Pedidos Automaticos de Producto.", false, true);
            GrabarParametro(sArbol, "GeneraPedidosEspeciales", @"FALSE", "Determina si la Unidad genera Pedidos Pedidos Especiales de Producto.", false, true);
        }

        private void Cliente_SubClienteDefaultOperacion()
        {
            GrabarParametro(sArbol, "Cliente_DefaultOperacion", @"", "Determina la Clave del Cliente default a mostrar en la captura de Dispensación.", false, true);
            GrabarParametro(sArbol, "Cliente_SubClienteDefaultOperacion", @"", "Determina la Clave del SubCliente default a mostrar en la captura de Dispensación.", false, true);
        }

        private void ClienteSeguroPopular()
        {
            //GrabarParametro(sArbol, "FechaOperacionSistema", @"2009-07-01", "Determina la Fecha de Operación del Sistema.", true);
            GrabarParametro(sArbol, "ClienteSeguroPopular", @"0002", "Determina la Clave del Cliente que se implementa como Cliente Seguro Popular.", true, false);
            GrabarParametro(sArbol, "ValidarInfoClienteSeguroPopular", @"TRUE", "Determina la Clave del Cliente que se implementa como Cliente Seguro Popular.", true, false);
            GrabarParametro(sArbol, "ValidarBeneficioClienteSeguroPopular", @"TRUE", "Determina si se Validará el Beneficio de Seguro Popular.", false, false); 
        }

        private void SubClienteEstado()
        {
            GrabarParametro(sArbol, "IdSubCliente", @"0000", "Determina la Clave del Sub Cliente del Seguro Popular del Estado.", true, false);
        }

        private void FechaOperacionSistema()
        {
            //GrabarParametro(sArbol, "FechaOperacionSistema", @"2009-07-01", "Determina la Fecha de Operación del Sistema.", true);
            GrabarParametro(sArbol, "FechaOperacionSistema", @"", "Determina la Fecha de Operación del Sistema.", true, false);
        }

        private void EfectivoEnCajaPermitido()
        {
            GrabarParametro(sArbol, "EfectivoEnCaja", @"4000", "Determina la cantidad máxima de efectivo permitido en caja.", false, false); 
        } 

        private void RutaDeReportes()
        {
            //GrabarParametro(sArbol, "RutaReportes", @"C:\Reportes\", "Determina ruta donde se encuentran los reportes del Punto de Venta", false);
            //GrabarParametro(sArbol, "RutaReportes", @"D:\PROYECTO SC-SOFT\SISTEMA_INTERMED\REPORTES\", "Determina ruta donde se encuentran los reportes del Punto de Venta", false);
            GrabarParametro(sArbol, "RutaReportes", @"", "Determina ruta donde se encuentran los reportes del Punto de Venta.", false, true);
        } 

        private void ClientePublicoGeneral()
        {
            GrabarParametro(sArbol, "CtePubGeneral", "0001", "Determina el Id del Cliente definido como Publico General.", true, false);
            GrabarParametro(sArbol, "CteSubPubGeneral", "0001", "Determina el Id del Sub Cliente definido como Publico General.", true, false);
            GrabarParametro(sArbol, "ProgPubGeneral", "0001", "Determina el Id del Programa definido como Publico General.", true, false);
            GrabarParametro(sArbol, "ProgSubPubGeneral", "0001", "Determina el Id del Sub Programa definido como Publico General.", true, false);
        }        

        private void ClaveCliente_SS_Estado()
        {
            GrabarParametro(sArbol, "ClaveCteSS_Edo", "", "Determina el Id del Cliente SS definido para el Estado.", true, false);
        }

        private void ImpresionDetalladaTickets()
        {
            GrabarParametro(sArbol, "ImpresionDetalladaTicket", @"FALSE", "Determina si la Unidad puede generar impresiones detalladas de los tickets de venta.", false, true);
        }

        private void FechaCriterioValidacion()
        {
            GrabarParametro(sArbol, "FechaCriterioValidacion", @"1", "Determina si la Unidad genera los reportes de validación en base a (1)Fecha de Registro ó (2)Fecha de Receta.", true, false);
        }

        private void MostrarLotesSinExistencia()
        {
            // Por default se muestran todos los Lotes 
            GrabarParametro(sArbol, "MostrarLotesSinExistencia", @"True", "Determina si la Unidad muestra los Lotes con existencia en cero(0).", true, false);
        }

        private void MostrarCapturaDeClavesRequeridas()
        {
            // Por default se muestra la Opcion  
            GrabarParametro(sArbol, "CapturaClavesSolicitadas", @"TRUE", "Determina si la Unidad captura la lista de Claves y Cantidades requeridas.", true, false);
        }

        private void PeriodoCierreDeTicketsPermitido()
        {
            // Por default se muestran todos los Lotes 
            GrabarParametro(sArbol, "CierreDeTicketsDiasAdicionalesPermitido", @"30", 
                "Determina el número de DIAS adicional permitido para el cierre de tickets de un periodo.", false, true);
        }
        ////private void DiasValidosPermitidos()
        ////{
        ////    // Por default se muestran todos los Lotes 
        ////    GrabarParametro(sArbol, "DiasValidosPermitidos", @"30",
        ////        "Determina el número de Dias permitidos para el acceso al sistema.", false, true);
        ////}

        private void DispensarSoloCuadroBasico()
        {
            // Por default se muestran todos los Lotes 
            GrabarParametro(sArbol, "DispensarSoloCuadroBasico", @"False", "Determina si la unidad solo puede Dispensar las Claves de Cuadro Basico.", false, true);
        }

        private void DiasArevisarpedidosCedis()
        {
            // Por default se muestran todos los Lotes 
            GrabarParametro(sArbol, "DiasARevisarpedidosCedis", @"180", "Determina los días que se tomaran en cuenta para el cálculo de cantidad sugerida.", false, true);
        }

        private void ServidorAlmacenRegional()
        {
            GrabarParametro(sArbol, "ServidorAlmacenRegional", @"00-0000", "Determina la Url del servidor del almacen Regional.", false, false);
            GrabarParametro(sArbol, "ServidorAlmacenCEDIS", @"00-0000", "Determina la Url del servidor del almacen CEDIS.", false, false);

            GrabarParametro(sArbol, "UnidadComprasCentrales", string.Format(@"{0}-{1}-0001", DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado), "Determina el Identificar de la Unidad asociada para Compras.", false, false);
        }

        private void ServidorAlmacenCEDIS()
        {
            //GrabarParametro(sArbol, "ServidorAlmacenCEDIS", @"00-0000", "Determina la Url del servidor del almacen CEDIS.", false, false);
        }

        private void ClaveDispensacionRecetasForaneas()
        {
            GrabarParametro(sArbol, "ClaveDispensacionRecetasForaneas", @"06", "Especifica el Tipo de Dispensación para Recetas Foraneas.", true, false);
        }

        private void ClaveDispensacionUnidadesNoAdministradas()
        {
            GrabarParametro(sArbol, "ClaveDispensacionUnidadesNoAdministradas", @"99", "Especifica el Tipo de Dispensación para Unidades No Administradas.", true, false);
        } 

        private void ExportaInformacion()
        {
            GrabarParametro(sArbol, "ExportaInformacion", @"TRUE", "Determina si la Unidad tiene permitido el Migrar toda su información para ser enviada a un Punto de Integración.", true, false);
        }

        /// <summary>
        /// Emisión de vales 
        /// </summary>
        private void EmiteVales()
        {
            string sParametros_Vales = "";


            sParametros_Vales = string.Format("1 ==> Normal, ticket por el vale completo.\n");
            sParametros_Vales += string.Format("2 ==> Clave, ticket por cada Clave contenida en el vale.\n");
            sParametros_Vales += string.Format("3 ==> Clave-Piezas, ticket por cada Clave por el número de piezas.");


            GrabarParametro(sArbol, "EmiteVales", @"FALSE", "Determina si la Unidad emite vales para ser cambiados en alguna farmacia con Convenio.", false, false);
            GrabarParametro(sArbol, "EmiteValesCompletos", @"FALSE", "Determina si la Unidad emite vales en GENERAL para ser cambiados en alguna farmacia con Convenio.", false, false);

            GrabarParametro(sArbol, "EmiteValesAutomaticoAlDispensar", @"TRUE", "Determina si la Unidad emite vales de forma autómatica al terminar la dispensación.", false, false);

            GrabarParametro(sArbol, "EmiteValesManuales", @"TRUE", "Determina si la Unidad emite vales de forma manual (sin captura del No Surtido) para ser cambiados en alguna farmacia con Convenio.", false, false); 

            //// Se maneja encriptado por razones de seguridad 
            GrabarParametro(sArbol, "EmiteValesExcepcion", @"", "Determina si existe una excepción en la validación de emisión de vales", false, false);


            GrabarParametro(sArbol, "TipoDispensacionVale", @"07", "Determina el Tipo de dispensación para la captura de Recetas con Vale asociado.", true, false);
            GrabarParametro(sArbol, "TipoDispensacionRecetaValeForaneo", @"98", "Determina el Tipo de dispensación para la captura de Recetas con Vale Foraneo(Vale emitido en otras unidades).", true, false);            
            
            GrabarParametro(sArbol, "FirmarVales", @"FALSE", "Determina si es requerida la firma-huella de quien recibe el vale.", false, true);
            GrabarParametro(sArbol, "Maneja_Vales_ServicioADomicilio", @"FALSE", "Determina si la unidad maneja Servicio a Domicilio para surtido de Vales No Canjeados.", false, true);
            GrabarParametro(sArbol, "ServicioADomicilio_Inicio", General.FechaYMD(General.FechaSistema.AddDays(1)), "Determina si la fecha miníma para generar Servicio a Domicilio.", false, true);

            GrabarParametro(sArbol, "EmisionValesCopias", @"1", "Determina el número default de copias de vales.", false, true);
            GrabarParametro(sArbol, "EmisionVales_Impresion_Directa", @"FALSE", "Determina si se envia directo a la impresora configurada.", false, true);


            GrabarParametro(sArbol, "EmisionVales_ContenidoPaqueteLicitado", @"TRUE", "Determina si se considera el Contenido Paquete Licitado para la emisión de vales ó se generan en piezas.", false, true);


            GrabarParametro(sArbol, "EmisionVales_Impresion_Personalizada", @"FALSE", "Determina si la Unidad utiliza impresión personalizada de vales.", false, true);
            GrabarParametro(sArbol, "EmisionVales_Plantilla_Personalizada_Ticket", string.Format(@"PtoVta_Vales_Generacion_{0}", DtGeneral.EstadoConectado), 
                                    "Determina si la Unidad-Almacen utiliza impresión personalizada de ticket de vale.", false, true);
            GrabarParametro(sArbol, "EmisionVales_Plantilla_Personalizada_Ticket_Desglozado", string.Format(@"PtoVta_Vales_Generacion_{0}_Desglozado", DtGeneral.EstadoConectado),
                                    "Determina si la Unidad-Almacen utiliza impresión personalizada de ticket de vale.", false, true);
            GrabarParametro(sArbol, "EmisionVales_TipoDeImpresion", @"1", sParametros_Vales, false, true);

        } 

        /// <summary>
        /// Manejo de Ubicaciones de Productos en el Almacén.
        /// 2K111012.1133 
        /// </summary>
        
        /// <summary>
        /// Confirmación Con Huellas 
        /// </summary>
        private void ConfirmacionConHuellas()
        {
            GrabarParametro(sArbol, "ConfirmacionConHuellas", @"FALSE", "Determina si se requiere firma con huella digital.", false, true);
        }

        private void ManejoDeUbicaciones() 
        {
            GrabarParametro(sArbol, "ManejaUbicaciones", @"TRUE", "Determina si la Unidad-Almacen maneja Localización de Productos(Ubicaciones).", false, false);
            GrabarParametro(sArbol, "ManejaCajasParaDistribucion", @"FALSE", "Determina si la Unidad-Almacen maneja cajas para distribución de Productos.", false, false);
            GrabarParametro(sArbol, "ManejaUbicacionesEstandar", @"TRUE", "Determina si el Almacen maneja ubicaciones estandar.", false, false);
        }

        /// <summary>
        /// Control de la Fecha receta permitida para captura.
        /// </summary>
        private void FechaRecetas() 
        {
            ////GrabarParametro(sArbol, "PermitirRecetasFechaAñosAnteriores", @"TRUE", 
            ////    "Determina si la Unidad permite la captura de Recetas con fecha de años anteriores", false, true); 

            ////GrabarParametro(sArbol, "MesesRecetasFechaAñosAnteriores", @"1", 
            ////    "Determina el número de meses hacia atras que se permitira la captura de recetas de años anteriores sólo en el mes de ENERO.", false, true);

            GrabarParametro(sArbol, "FechaRecetaMesesAnteriores", @"1", 
                "Determina el número de meses hacia atras que se permitira la captura de recetas.", false, true);

            GrabarParametro(sArbol, "FechaReceta_ADT_DiasHaciaAtras", @"30",
                "Determina el número de días hacia atras que se puede modificar la fecha de receta.", false, true);

            GrabarParametro(sArbol, "FechaReceta_ADT_DiasHaciaAdelante", @"10",
                "Determina el número de días hacia adelante que se puede modificar la fecha de receta.", false, true);  
        }

        private void PermitirDispensacionVenta_ConExistenciaConsignacion()
        {
            GrabarParametro(sArbol, "DispensarVentaConExistenciaConsignacion", @"FALSE", "Determina si la Unidad-Almacen puede Dispensar productos de Venta si hay existencia de Consignación.", false, true);
        }

        private void SubFarmaciaTraspasosEstados()
        {
            GrabarParametro(sArbol, "TraspasosEstados_TipoDeInventario", @"1", "Determina el tipo de inventario para traspasos entre estados filiales. (0 ==> Todo, 1 ==> Venta, 2 ==> Consigna)", true, false);

            GrabarParametro(sArbol, "SubFarmaciaTraspasosEstados", @"001", "Determina la SubFarmaciá ó SubAlmacen de Venta (Inventario propio) para traspasos entre estados filiales.", true, false);
            GrabarParametro(sArbol, "SubFarmaciaEntradaTraspasosEstados", @"001", "Determina la SubFarmaciá ó SubAlmacen de Venta (Inventario propio) para entrada de traspasos entre estados filiales.", true, false);
        }

        private void ServidorDedicado()
        {
            GrabarParametro(sArbol, "EsServidorDedicado", @"FALSE", "Determina si el servidor de la unidad es un servidor dedicado.", true, false);
        }

        private void InformacionAdicional()
        {
            GrabarParametro(sArbol, "Diagnostico", @"0001",
                "Determina la Clave de Diagnostico default para la captura de recetas.", false, true);

            GrabarParametro(sArbol, "DiagnosticoCaracteres", @"4",
                "Determina el largo de la Clave de Diagnostico para la captura de recetas.", false, true);

            GrabarParametro(sArbol, "Beneficio", @"0000",
                "Determina la Clave de Beneficio default para la captura de recetas.", false, true);
        }

        private void AlmacenGeneraTransferenciasManuales()
        {
            GrabarParametro(sArbol, "EsServidorDedicado", @"FALSE", "Determina si el Almacén solo genera transferencias de salida asociada a pedidos de unidades.", true, false);
        }

        private void InventariosCiclicos()
        {
            GrabarParametro(sArbol, "INV_Ciclicos__DiasPeriodo", @"60", "Determina el número de días que abarca el ciclo inventario.", false, false);
            GrabarParametro(sArbol, "INV_Ciclicos__IniciarCicloDefault", @"FALSE", "Determina si se inicia ciclo de inventario de forma automática.", false, false);
        }

        private void ClavesInventariosAleatorios()
        {
            GrabarParametro(sArbol, "ClavesINV_AleatorioDispensador", @"3", "Determina el número de Claves que se considerarán en el inventario aleatorio del Dispensador", false, false);
            GrabarParametro(sArbol, "ClavesINV_AleatorioEncargado", @"3", "Determina el número de Claves que se considerarán en el inventario aleatorio del Encargado", false, false);
            GrabarParametro(sArbol, "ClavesINV_AleatorioCierrePeriodo", @"3", "Determina el número de Claves que se considerarán en el inventario aleatorio al Cierre de Periodo", false, false);

            GrabarParametro(sArbol, "ProductosINV_AleatorioDispensador", @"5", "Determina el número de Productos que se considerarán en el inventario aleatorio del Dispensador", false, false);
            GrabarParametro(sArbol, "ProductosINV_AleatorioEncargado", @"10", "Determina el número de Productos que se considerarán en el inventario aleatorio del Encargado", false, false);

        }

        private void Procesos_Al_CorteParcial_y_CorteDiario()
        {
            GrabarParametro(sArbol, "Corte_InventariosAleatoriosAutomaticos", @"FALSE", "Determina si la unidad genera inventarios aleatorios de forma automática.", false, false);
            GrabarParametro(sArbol, "Corte_ImpresionReporteDispensacion", @"FALSE", "Determina si la unidad genera el reportes de dispensación al generar cortes parciales y cortes diarios", false, false);
            GrabarParametro(sArbol, "Corte_ImpresionReporteDispensacion_Vales", @"FALSE", "Determina si la unidad genera el reportes de generación de vales al generar cortes parciales y cortes diarios", false, false);

            GrabarParametro(sArbol, "Corte_InventariosAleatoriosAutomaticos_Productos", @"FALSE", "Determina si la unidad genera inventarios aleatorios por productos de forma automática.", false, false);        
        }

        private void ValidarClavesEnCuadroBasico()
        {
            GrabarParametro(sArbol, "ValidarClavesEnCuadroBasico", @"TRUE", "Determina si el Almacén valida las Claves SSA al momento de Dispensar(Venta), evitar dispensar claves fuera de cuadro básico.", true, true);

            GrabarParametro(sArbol, "ValidarConsumoClaves_Programacion", @"FALSE", "Determina si se bloquea la dispensación de Claves de acuerdo a la programación predeterminada.", true, true);
            GrabarParametro(sArbol, "ValidarConsumoClaves_ProgramaAtencion", @"FALSE", "Determina si se bloquea la dispensación de Claves de acuerdo a la programación predeterminada según el Programa-SubPrograma.", true, true);
        }
        
        private void CamaHabitacion()
        {
            GrabarParametro(sArbol, "CapturarNumeroDeCama", @"FALSE", "Determina si la unidad captura el número de cama.", true, true);
            GrabarParametro(sArbol, "CapturarNumeroDeHabitacion", @"FALSE", "Determina si la unidad captura el número de habitación.", true, true);
        }

        private void PermitirAjustesInventario_Con_ExistenciaEnTransito()
        {
            GrabarParametro(sArbol, "AjusteInv_ExistenciaEnTransito", @"FALSE", "Determina si la unidad puede realizar ajustes de inventario si cuenta con transferencias en transito.", false, true);

            //// Bloquear VENTAS y TRANSFERENCIAS si existen Transferencias sin aplicar dentro del parámetro configurado 
            GrabarParametro(sArbol, "Transferencias_Dias_ConfirmacionTransitos", @"15", "Determina el número de días máximo para confirmar transferencias en transito.", false, true);
        }

        private void HabilitarTransferenciasControlado()
        {
            GrabarParametro(sArbol, "HabilitarTransferenciasControlado", @"TRUE", "Determina si la unidad puede puede enviar controlado a otra farmacia.", false, true);
        }

        private void ValidarClavesEnPerfil() 
        {
            GrabarParametro(sArbol, "ValidarClavesEnPerfil", @"TRUE", "Determina si la unidad valida ó no en sus perfiles las claves que dispensa.", false, true);
        }

        private void ReferenciaAuxiliarBeneficiario()
        {
            GrabarParametro(sArbol, "ReferenciaAuxiliarBeneficiario", @"FALSE", "Determina si la unidad agrega una referencia padre en la captura de beneficiarios.", false, true);
        }

        private void PadronDeBeneficiarios()
        {
            GrabarParametro(sArbol, "PadronRerefenciaLongitud", @"10", "Determina longitud miníma de la Referencia para busquedas en el padrón de beneficiarios.", false, true);
            GrabarParametro(sArbol, "PadronFormatearRerefencia", @"FALSE", "Determina si la Referencia de debe formatear a una longitud determinada.", false, true);
            GrabarParametro(sArbol, "PadronApellidoPaternoLongitud", @"3", "Determina longitud miníma del Apellido Paterno para busquedas en el padrón de beneficiarios.", false, true);
            GrabarParametro(sArbol, "PadronNombreLongitud", @"4", "Determina longitud miníma del Nombre para busquedas en el padrón de beneficiarios.", false, true);
        }

        private void EmisionVales_ValidarExistencia()
        {
            GrabarParametro(sArbol, "EmisionVales_ValidarExistencia", @"TRUE", "Determina si se válida la existencia de la Clave solicitada para emisión de vales.", false, true);
        }

        private void ValidarFoliosDeRecetaUnicos()
        {
            GrabarParametro(sArbol, "ValidarFoliosDeRecetaUnicos", @"FALSE", "Determina si la unidad permitirá la captura de Folios de receta repetidos.", false, true);
        }

        private void Vales_Maneja_FirmaElectronica()
        {
            GrabarParametro(sArbol, "Vales_Maneja_FE", @"FALSE", "Determina si la Unidad Implementa firma electronica para la Emision de Vales.", false, true);
        }

        private void ValidarUbicacionesEnCapturaDeSurtido()
        {
            GrabarParametro(sArbol, "ValidarUbicacionesEnCapturaDeSurtido", @"FALSE", "Determina si el Almacén válida las el tipo de las ubicaciones disponibles en la captura de Surtido.", false, true);
        }

        private void SurtidoDePedidos()
        {
            GrabarParametro(sArbol, "ImplementaValidacionCiega", @"TRUE", "Determina si el Almacén implementa la VALIDACIÓN CIEGA en el Surtido de pedidos.", false, true);
            GrabarParametro(sArbol, "Pedidos_ModificarInformacionAdicional", @"FALSE", "Determina si esta habilitada la modificación del Beneficiario y Número de Receta.", false, true);
            GrabarParametro(sArbol, "Pedidos_ModificarFoliosSurtidos", @"FALSE", "Determina si esta habilitada la modificación del check folios a procesar.", false, true);
        }

        private void EntradaConsinga_MostrarSubFarmaciaEmulaVenta()
        {
            GrabarParametro(sArbol, "ENT_CNSGN_MostrarSubFarmaciasEmulaVenta", @"TRUE", "Determina si en la Opción de Entradas por Consignación se muestran las SubFarmacias que emulan SubFarmacia de Venta.", false, true);
        }

        private void Modificar_Detalles_OrdenesDeCompra()
        {
            GrabarParametro(sArbol, "COM_ModificarPreciosOrdenesDeCompra", @"FALSE", "Determina si el Almacén puede modificar los precios de los productos segun Orden de Compra versus Factura recibida.", true, false);
            GrabarParametro(sArbol, "COM_ModificarProductosOrdenesDeCompra", @"FALSE", "Determina si el Almacén puede agregar/modificar los productos recibidos segun Orden de Compra versus Factura recibida.", true, false);
        }

        private void ForzarCapturaEnMultiplosDeCajas()
        {            
            GrabarParametro(sArbol, "ForzarCapturaEnMultiplosDeCajas", @"TRUE", "Determina si la unidad realiza Entradas y Salidas en multiplos según el contenido paquete del producto (Cajas completas).", false, true);
            GrabarParametro(sArbol, "ForzarCapturaEnMultiplosDeCajas_ProgramaSubPrograma", @"FALSE", "Determina si la unidad dispensa en multiplos según el contenido paquete del producto (Cajas completas) dependiendo del Programa-SubPrograma atendido.", false, true);
            GrabarParametro(sArbol, "ForzarCapturaEnMultiplosDeCajas_ClaveSSA_Especifica", @"FALSE", "Determina si la unidad dispensa en multiplos según el contenido paquete del producto (Cajas completas) dependiendo de la Clave SSA.", false, true);

            GrabarParametro(sArbol, "ForzarCapturaEnMultiplosHabilitarValidaciones", @"TRUE", "Determina si la unidad debe habilitar las validaciones correspondientes para el manejo de Multiplos de Caja.", false, true);
        }

        private void OperacionConMaquila()
        {
            ///GrabarParametro(sArbol, "ManejaOperacionMaquila", @"FALSE", "Determina si la Unidad-Almacen maneja una Operación Externa.", false, true);

            GrabarParametro(sArbol, "INT_OPM_ManejaOperacionMaquila", @"FALSE", "Determina si la Unidad-Almacen maneja una Operación Externa.", false, true);
            GrabarParametro(sArbol, "INT_OPM_EstadoOperacionMaquila", sIdEstado, "Determina el Id Estado con Operación Externa.", false, true);
        }

        private void Vta_ImpresionPersonalizada_Ticket()
        {
            GrabarParametro(sArbol, "Vta_Impresion_Personalizada_Ticket", @"FALSE", "Determina si la Unidad-Almacen utiliza impresión personalizada de ticket.", false, true);
            GrabarParametro(sArbol, "Vta_Impresion_Personalizada_Ticket_Detallado", @"FALSE", "Determina si la Unidad-Almacen utiliza impresión personalizada de ticket a detalle.", false, true);
            GrabarParametro(sArbol, "Vta_Impresion_Copias", @"1", "Determina el número default de copias de tickets.", false, true);
            GrabarParametro(sArbol, "Vta_Impresion_Directa", @"FALSE", "Determina si se envia directo a la impresora configurada.", false, true);



            GrabarParametro(sArbol, "Vta_Plantilla_Personalizada_Ticket", "PtoVta_TicketCredito", "Determina el nombre de la plantilla para la impresión personalizada de ticket.", false, true);
            GrabarParametro(sArbol, "Vta_Plantilla_Personalizada_Ticket_Detallado", "PtoVta_TicketCredito_Detallado", "Determina el nombre de la plantilla para la impresión personalizada de ticket a detalle.", false, true);
            GrabarParametro(sArbol, "Vta_Plantilla_Personalizada_Ticket_Detallado_Precios", "PtoVta_TicketCredito_Detallado_Precios", "Determina el nombre de la plantilla para la impresión personalizada de ticket a detalle con precios.", false, true);
            GrabarParametro(sArbol, "Vta_Plantilla_Personalizada_Ticket_PublicoGeneral", "PtoVta_TicketPublicoGral", "Determina el nombre de la plantilla para la impresión personalizada de ticket para publico general.", false, true);
            GrabarParametro(sArbol, "Vta_Plantilla_Personalizada_Surtido_De_Pedidos", "PtoVta_Cedis_SurtidoPedidos_Det", "Determina el nombre de la plantilla para la impresión personalizada de surtido de pedidos.", false, true);

            GrabarParametro(sArbol, "Vta_Plantilla_Personalizada_Surtido_De_Pedidos_Caratula", "PtoVta_Cedis_SurtidoPedidos_Caratula", "Determina el nombre de la plantilla para la impresión personalizada de caratula de surtido de pedidos.", false, true);

        } 

        private void ImplementaCodificacion()
        {
            GrabarParametro(sArbol, "ImplementaCodificacion", @"FALSE", "Determina si la Unidad-Almacen implementa la codificación de productos(Código DataMatrix).", false, true);
            GrabarParametro(sArbol, "ImplementaReaderDM", @"FALSE", "Determina si la Unidad-Almacen implementa el lector DataMatrix).", false, true);
        }

        private void ValidarBeneficariosAlertas()
        {
            GrabarParametro(sArbol, "ValidarBeneficariosAlertas", @"FALSE", "Determina si la Unidad verifica si el Beneficiario cuenta con alertas emitidas.", false, true);
        }

        private void ImplementaInterfaceExpedienteElectronico()
        {
            GrabarParametro(sArbol, "ImplementaInterfaceExpedienteElectronico", @"FALSE", "Determina si la Unidad implementa interface con expediente electrónico (Recetas).", false, true);
        }

        private void ImplementaDigitalizacionDeRecetas()
        {
            GrabarParametro(sArbol, "ImplementaDigitalizacionDeRecetas", @"FALSE", "Determina si la Unidad implementa la Digitilazación de Recetas.", false, true);
            GrabarParametro(sArbol, "ImplementaDigitalizacionDeRecetasDepurar", @"TRUE", "Determina si se depura de forma automatica el directorio de Digitilazación de Recetas.", false, true);
        }

        private void Controlados_DispensacionPermitida()
        {
            bool bPermiso = DtGeneral.EsAlmacen;
            string sPermiso = @"" + bPermiso.ToString().ToUpper();

            GrabarParametro(sArbol, "Controlados_DispensacionPermitida", sPermiso, "Determina si la Unidad dispensa controlados en colectivos ó recetas.", false, true);
            GrabarParametro(sArbol, "Controlados_ManejoPermitido", sPermiso, "Determina si la Unidad maneja controlados de forma general.", false, true);
        }


        private void Sesion()
        {
            GrabarParametro(sArbol, "Sesion_MinRefresco", "2", "Determina el tiempo de refresco de la sesión.", false, true);
            GrabarParametro(sArbol, "Sesion_MinDesconexion", "7", "Determina el tiempo de desconexión de la sesión.", false, true);
            GrabarParametro(sArbol, "Sesion_DiasInactivo", "15", "Determina los dias el tiempo de desconexión de la sesión.", false, true);            
            GrabarParametro(sArbol, "Sesion_MultiplesConexiones", "FALSE", "Determina si permite multiples conexiones en el mismo equipo.", false, true);
        }



        
        private void ImplementaImpresionDesglosada_VtaTS()
        {
            GrabarParametro(sArbol, "ImplementaImpresionDesglosada_VtaTS", @"FALSE", "Determina si la Unidad implementa impresión desglosada de ventas y transferencias.", false, true);
        }

        /// <summary>
        /// Información requerida por Servicios de Salud de Hidalgo 
        /// </summary>
        private void CapturaInformacionAEI()
        {
            ////GrabarParametro(sArbol, "CapturaInformacionAEI", @"FALSE", "Determina si la Unidad captura información AEI (H´s).", false, true);
        }

        private void Dispensacion_ImplementaExpiracion()
        {
            bool bActivarParametro = ("21" == DtGeneral.EstadoConectado) && !DtGeneral.EsAlmacen; 

            GrabarParametro(sArbol, "Dispensacion_ImplementaExpiracion", string.Format(@"{0}", bActivarParametro ? "TRUE" : "FALSE"), 
                "Determina si la Unidad implementa bloqueo de dispensación a una fecha determinada.", false, true);

            GrabarParametro(sArbol, "Dispensacion_FechaExpiracion_Venta", string.Format(@"{0}", bActivarParametro ? "2020-01-01" : "2023-01-01"), 
                "Determina la fecha de bloqueo de la dispensación de Venta.", false, true);


            GrabarParametro(sArbol, "Dispensacion_FechaExpiracion_Consigna", string.Format(@"{0}", bActivarParametro ? "2020-01-01" : "2023-01-01"),
                "Determina la fecha de bloqueo de la dispensación de Consigna.", false, true);
        }
        #endregion Parametros  

        #region Parametros MA 
        private void Transferencias_Interestatales__Farmacias()
        {
            Transferencias_Interestatales__Farmacias(false);
        }

        private void Transferencias_Interestatales__Farmacias(bool ValorDefault)
        {
            GrabarParametro(sArbol, "Transferencias_Interestatales__Farmacias", ValorDefault.ToString().ToUpper(), "Determina si la Unidad esta habilitada para generar-recibir Transferencias de otras Operaciones.", false, true);
        }
        #endregion Parametros MA

        #endregion Funciones y Procedimientos privados

    }
}
