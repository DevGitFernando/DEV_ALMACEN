using System;
using System.Collections.Generic;
using System.Text;
using System.Data; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Usuarios_y_Permisos; 

namespace DllFarmaciaSoft.Devoluciones
{
    public class clsDevoluciones
    {
        #region Declaracion de variables 
        clsConexionSQL cnn;
        clsDatosConexion datosCnn;
        clsLeer exec;
        clsLeer leer;        
        clsLotes pListaLotes;
        clsGrabarError Error;
        List<clsProducto> pListaProductos; // = new List<clsProducto>();
        clsSKU SKU; 

        string sInicio = "  Set DateFormat YMD  ";

        string sMensaje = "";
        string sIdEmpresa = "";
        string sIdEstado = "";
        string sIdFarmacia = "";
        string sMsjSinPermiso = "";
        string sNombreOperacion = "";

        // string sFolioCompra = "";
        string sFolioDevolucion = "";
        string sFolioVale = "";

        TipoDevolucion tpDevolucion = TipoDevolucion.Ninguna;
        string sReferencia = "";
        string sFolioMovtoInv = "";
        DateTime dFechaSistema = DateTime.Now;
        DateTime dFechaRegistro = DateTime.Now;
        string sIdPersonal = "";
        string sObservaciones = "";

        //double dTasaIva = 0;
        double dSubTotal = 0;
        double dIva = 0;
        double dTotal = 0;

        string sIdTipoMovtoInv = "CC";
        string sTipoES = "S";
        string sStoreDEV = " ";
        bool bEsRemision = false;
        bool bIncidenciasDetectadas = false;
        DataSet dtsIndicendias = null;
        DataSet dtsMotivosDev = new DataSet();
        clsOperacionesSupervizadasHuellas opPermisosEspeciales;
        string sNombrePosicion = "";
        string sMovtoSREU = "";
        string sMovtoEREU = "";
        string sFolioREUSalida = "";
        string sFolioREUEntrada = "";

        clsDatosCliente DatosCliente;        
        #endregion Declaracion de variables

        #region Constructor y Destructor de la Clase 
        public clsDevoluciones()
        {
            pListaProductos = new List<clsProducto>();
        }

        public clsDevoluciones(string IdEmpresa, string IdEstado, string IdFarmacia, clsDatosConexion DatosDeCnn)
        {
            this.sIdEmpresa = IdEmpresa;
            this.sIdEstado = IdEstado;
            this.sIdFarmacia = IdFarmacia;
            this.datosCnn = DatosDeCnn;

            pListaProductos = new List<clsProducto>();
            cnn = new clsConexionSQL(datosCnn);
            exec = new clsLeer(ref cnn);
            leer = new clsLeer(ref cnn);
            
            Error = new clsGrabarError(DtGeneral.DatosApp, "clsDevoluciones");
            ////opPermisosEspeciales = new clsOperacionesSupervizadasHuellas(ref cnn, IdEmpresa, IdEstado, IdFarmacia, sIdPersonal);

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, "clsDevoluciones", "");
        }
        #endregion Constructor y Destructor de la Clase

        #region Propiedades publicas 
        public string IdEmpresa
        {
            get { return sIdEmpresa; }
        }

        public string IdEstado
        {
            get { return sIdEstado; }
        }

        public string IdFarmacia
        {
            get { return sIdFarmacia; }
        }

        public string Folio
        {
            get { return sFolioDevolucion; }
            set { sFolioDevolucion = value; }
        }

        public string Vale
        {
            get { return sFolioVale; }
            set { sFolioVale = value; }
        }

        public string MsjSinPermiso
        {
            get { return sMsjSinPermiso; }
            set { sMsjSinPermiso = value; }
        }

        public string NombreOperacion
        {
            get { return sNombreOperacion; }
            set { sNombreOperacion = value; }
        }

        //public string FolioCompra
        //{
        //    get { return sFolioCompra; }
        //    set { sFolioCompra = value; }
        //}

        public TipoDevolucion Tipo
        {
            get { return tpDevolucion; }
            set 
            {
                if (value == TipoDevolucion.Compras)
                {
                    sIdTipoMovtoInv = "CC";
                    sTipoES = "S";
                    sStoreDEV = " spp_DEV_AfectarCompras ";
                }

                if (value == TipoDevolucion.Venta)
                {
                    sIdTipoMovtoInv = "ED";
                    sTipoES = "E";
                    sStoreDEV = " spp_DEV_AfectarVentas ";
                    sNombrePosicion = "VTA_DEVOLUCION";
                    sMovtoSREU = "SRDV";
                    sMovtoEREU = "ERDV";
                }

                if (value == TipoDevolucion.PedidosVenta )
                {
                    sIdTipoMovtoInv = "DEPD";
                    sTipoES = "S";
                    sStoreDEV = " spp_DEV_AfectarPedidosDis ";
                }

                if (value == TipoDevolucion.PedidosConsignacion)
                {
                    sIdTipoMovtoInv = "DPDC";
                    sTipoES = "S";
                    sStoreDEV = " spp_DEV_AfectarPedidosDis ";
                }


                if (value == TipoDevolucion.EntradasDeConsignacion)
                {
                    sIdTipoMovtoInv = "DEPC";
                    sTipoES = "S";
                    sStoreDEV = " spp_DEV_Afectar_Entradas_Consignacion ";
                }

                if (value == TipoDevolucion.OrdenCompra)
                {
                    sIdTipoMovtoInv = "DOC";
                    sTipoES = "S";
                    sStoreDEV = " spp_DEV_Afectar_Ordenes_Compras ";
                }

                if (value == TipoDevolucion.Dev_Proveedor)
                {
                    sIdTipoMovtoInv = "CSDP";
                    sTipoES = "E";
                    sStoreDEV = " spp_DEV_Afectar_DevolucionesAProveedor ";
                }

                if (value == TipoDevolucion.VentasSocioComercial)
                {
                    sIdTipoMovtoInv = "EDSC";
                    sTipoES = "E";
                    sStoreDEV = " spp_DEV_AfectarVentas_SociosComerciales ";
                    sNombrePosicion = "VTA_DEVOLUCION";
                    sMovtoSREU = "SRDSC";
                    sMovtoEREU = "ERDSC";
                }

                tpDevolucion = value; 
            }
        }

        public string Referencia
        {
            get { return sReferencia; }
            set { sReferencia = value; }
        }

        public string FolioInventario
        {
            get { return sFolioMovtoInv; }
            set { sFolioMovtoInv = value; }
        }

        public DateTime FechaOperacionDeSistema
        {
            get { return dFechaSistema; }
            set { dFechaSistema = value; }
        }

        public DateTime FechaRegistro
        {
            get { return dFechaRegistro; }
            set { dFechaRegistro = value; }
        }

        public string IdPersonal
        {
            get { return sIdPersonal; }
            set { sIdPersonal = value; }
        }

        public string Observaciones
        {
            get { return sObservaciones; }
            set { sObservaciones = value; }
        }

        //public double TasaIva
        //{
        //    get { return dTasaIva; }
        //    set { dTasaIva = value; }
        //}

        public double SubTotal
        {
            get { return dSubTotal; }
            set { dSubTotal = value; }
        }

        public double Iva
        {
            get { return dIva; }
            set { dIva = value; }
        }

        public double Total
        {
            get { return dTotal; }
            set { dTotal = value; }
        }

        public clsLotes Lotes
        {
            get { return pListaLotes; }
            set { pListaLotes = value; }
        }

        public DataSet MotivosDev
        {
            get { return dtsMotivosDev; }
            set { dtsMotivosDev = value; }
        }
        #endregion Propiedades publicas

        #region Funciones y Procedimientos Publicos 
        public void AddProducto(clsProducto Producto)
        {
            pListaProductos.Add(Producto);
        }

        public void AddProducto(string IdProducto, string CodigoEAN, int Unidad, double TasaIva, double Cantidad, double Valor)
        { 
            pListaProductos.Add(new clsProducto(IdProducto, CodigoEAN, Unidad, TasaIva, Cantidad, Valor));
        }

        public bool Guardar()
        {
            bool bRegresa = false;
            bIncidenciasDetectadas = false;
            dtsIndicendias = null;

            SKU = new clsSKU();

            if (ValidarProductos())
            {
                if (!cnn.Abrir())
                {
                    Error.LogError(cnn.MensajeError); 
                    General.msjErrorAlAbrirConexion(); 
                }
                else 
                {
                    cnn.IniciarTransaccion();

                    if (GrabarMovtoInv())
                    {
                        if (GuardaMotivosDev())
                        {
                            if (GrabarDevolucion())
                            {
                                bRegresa = AfectarExistencia();
                                if (bRegresa && tpDevolucion == TipoDevolucion.Venta)
                                {
                                    bRegresa = ActualizarRemision();
                                }

                                if (bRegresa && GnFarmacia.ManejaUbicaciones)
                                {
                                    bRegresa = ReubicacionDevoluciones();
                                }
                            }
                        }
                    }

                    if (!bRegresa)
                    {
                        Error.GrabarError(exec, "Guardar()"); 
                        cnn.DeshacerTransaccion();
                        General.msjError("Ocurrió un error al grabar la Devolución.");
                    }
                    else
                    {
                        cnn.CompletarTransaccion();
                        General.msjUser(sMensaje);

                        if(GnFarmacia.ManejaUbicaciones && GnFarmacia.ManejaUbicacionesEstandar)
                        {
                            General.msjUser("Se generaron los Folios satisfactoriamente.");
                        }
                    }  

                    cnn.Cerrar();

                    ////if (bIncidenciasDetectadas)
                    ////{
                    ////    MostrarIncidencias(); 
                    ////}
                }
            }

            return bRegresa;
        }

        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private void MostrarIncidencias()
        {
            FrmDevoluciones_VerificarCantidades f = new FrmDevoluciones_VerificarCantidades(dtsIndicendias);
            f.MostrarProductosConIncidencias(); 
        }

        private bool ValidarProductos()
        {
            bool bRegresa = true;

            if (pListaProductos.Count <= 0)
            {
                bRegresa = false;
                General.msjUser("No existen productos para realizar la devolución.");
            }

            if (bRegresa && DtGeneral.ConfirmacionConHuellas)
            {
                ////bRegresa = opPermisosEspeciales.VerificarPermisos(sNombreOperacion, sMsjSinPermiso);
                bRegresa = DtGeneral.PermisosEspeciales_Biometricos.VerificarPermisos(sNombreOperacion, sMsjSinPermiso);
            }

            return bRegresa;
        }

        #region Devoluciones 
        private bool ActualizarInformacionOrigen()
        {
            bool bRegresa = true;
            string sSql = sInicio + string.Format(" Exec  {0}  '{1}', '{2}', '{3}', '{4}', '{5}' ",
                sStoreDEV, sIdEmpresa, sIdEstado, sIdFarmacia, sReferencia, sFolioDevolucion );

            if (!exec.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(exec, "ActualizarInformacionOrigen()");
            }

            return bRegresa;
        }

        private bool ActualizarRemision()
        {
            string sFolioRemision = "", sFechaSistema = "";
            bool bRegresa = true;
            string sSql = sInicio + string.Format(" Exec spp_Mtto_Remisiones \n" + 
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioVenta = '{3}', @FolioRemision = '{4}', @FechaSistema = '{5}' \n",
                sIdEmpresa, sIdEstado, sIdFarmacia, sReferencia, sFolioRemision, sFechaSistema);

            if (EsRemision())
            {
                if (!exec.Exec(sSql))
                {
                    bRegresa = false;
                    Error.GrabarError(exec, "ActualizarRemision()");
                }
            }

            return bRegresa;
        }

        private bool EsRemision()
        {
            bool bRegresa = false;
            string sSql = string.Format(" Select * From RemisionesEnc (NoLock) " + 
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioVenta = '{3}' ",
                sIdEmpresa, sIdEstado, sIdFarmacia, sReferencia);

            if (!exec.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(exec, "EsRemision()");
            }
            else
            {
                bRegresa = exec.Leer(); 
            }


            bEsRemision = bRegresa; 
            return bRegresa; 
        }

        private bool ValidarCantidades_Devolucion()
        {
            bool bRegresa = true; 
            clsLeer leerValidarCantidades = new clsLeer();
            string sSql = sInicio + string.Format(" Exec spp_DEV_ValidarDevolucion  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioDevolucion = '{3}' ",
                            sIdEmpresa, sIdEstado, sIdFarmacia, sFolioDevolucion);

            if (!exec.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(exec, "ValidarCantidades_Devolucion()");
            }
            else
            {
                exec.Leer();
                exec.RenombrarTabla(1, "Validacion");
                exec.RenombrarTabla(2, "EANs");
                exec.RenombrarTabla(3, "Lotes");
                exec.RenombrarTabla(4, "Caducidades");

                bIncidenciasDetectadas = exec.CampoBool("ExisteError");
                bRegresa = !bIncidenciasDetectadas;

                dtsIndicendias = exec.DataSetClase;
                dtsIndicendias.Tables.Remove("Validacion");

            }

            return bRegresa; 
        }

        private bool GrabarDevolucion()
        {
            bool bRegresa = true;
            string sSql = "";

            ////sFolioDevolucion = SKU.Foliador;

            sSql = sInicio + string.Format(" Exec spp_Mtto_DevolucionesEnc \n" + 
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioDevolucion = '{3}', @TipoDeDevolucion = '{4}', @Referencia = '{5}', \n"   +
                "\t@FolioMovtoInv = '{6}', @FechaSistema = '{7}', @IdPersonal = '{8}', @Observaciones = '{9}', @SubTotal = '{10}', @Iva = '{11}', @Total = '{12}' \n",
                sIdEmpresa, sIdEstado, sIdFarmacia, sFolioDevolucion, (int)tpDevolucion, sReferencia, sFolioMovtoInv,
                General.FechaYMD(dFechaSistema, "-"), sIdPersonal, sObservaciones.Trim(),
                General.GetFormatoNumerico_Double(dSubTotal),
                General.GetFormatoNumerico_Double(dIva),
                General.GetFormatoNumerico_Double(dTotal), 1);

            if (!exec.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(exec, "GrabarDevolucion_Det()");
            }
            else
            {
                exec.Leer();
                sFolioDevolucion = exec.Campo("Folio");
                sMensaje = exec.Campo("Mensaje");

                bRegresa = GrabarDevolucion_Det();

                ////if (GrabarDevolucion_Det())
                ////{
                ////    bRegresa = ValidarCantidades_Devolucion();
                ////} 

            }

            return bRegresa;
        }

        private bool GrabarDevolucion_Det()
        {
            bool bRegresa = false;
            string sSql = "";

            foreach (clsProducto P in pListaProductos)
            {
                if (P.IdProducto != "")
                {
                    if (P.Cantidad > 0)
                    {
                        sSql = sInicio + string.Format("Exec spp_Mtto_DevolucionesDet " + 
                            " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioDevolucion = '{3}', " + 
                            " @IdProducto = '{4}', @CodigoEAN = '{5}', @Unidad = '{6}', @Cant_Devuelta = '{7}', " + 
                            " @PrecioCosto_Unitario = '{8}', @TasaIva = '{9}', @ImpteIva = '{10}'  ",
                            sIdEmpresa, sIdEstado, sIdFarmacia, sFolioDevolucion, P.IdProducto, P.CodigoEAN, P.Unidad,
                            P.Cantidad, P.Valor, P.TasaIva, P.ImporteIva);
                        if (!exec.Exec(sSql))
                        {
                            bRegresa = false;
                            Error.GrabarError(exec, "GrabarMovtoInv_Det()");
                            break;
                        }
                        else
                        {
                            bRegresa = GrabarDevolucion_DetLotes(P.IdProducto, P.CodigoEAN);
                            if (!bRegresa)
                            {
                                break;
                            }
                        }
                    }
                }
            }

            // Afectar la tabla de Ventas ó Compras según sea el caso 
            if (bRegresa)
            {
                bRegresa = ActualizarInformacionOrigen();
            }

            return bRegresa;
        }

        private bool GrabarDevolucion_DetLotes(string IdProducto, string CodigoEAN)
        {
            bool bRegresa = false;
            string sSql = "";

            clsLotes[] ListaLotes = Lotes.Lotes(IdProducto, CodigoEAN);

            foreach (clsLotes L in ListaLotes)
            {
                if ( L.Cantidad > 0 )
                { 
                    sSql = sInicio + string.Format("Exec spp_Mtto_DevolucionesDet_Lotes " +
                    " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', @FolioDevolucion = '{4}', " + 
                    " @IdProducto = '{5}', @CodigoEAN = '{6}', @ClaveLote = '{7}', @Cant_Devuelta = '{8}', @SKU = '{9}' ",
                        sIdEmpresa, sIdEstado, sIdFarmacia, L.IdSubFarmacia, sFolioDevolucion, IdProducto, CodigoEAN, L.ClaveLote, L.Cantidad, L.SKU);
                    if (!exec.Exec(sSql))
                    {
                        bRegresa = false;
                        Error.GrabarError(exec, "GrabarDevolucion_DetLotes()");
                        break;
                    }
                    else
                    {
                        bRegresa = true;
                        if (GnFarmacia.ManejaUbicaciones)
                        {
                            bRegresa = GrabarDevolucion_DetLotes_Ubicaciones(L);
                            if(!bRegresa)
                            {
                                break;  
                            }
                        }
                    }
                }
            }

            return bRegresa;
        }

        private bool GrabarDevolucion_DetLotes_Ubicaciones(clsLotes Lote)
        {
            bool bRegresa = false;
            string sSql = "";

            clsLotesUbicacionesItem[] Ubicaciones = Lote.Ubicaciones(Lote.SKU, Lote.Codigo, Lote.CodigoEAN, Lote.ClaveLote, Lote.IdSubFarmacia);

            foreach (clsLotesUbicacionesItem L in Ubicaciones)
            {
                if (L.Cantidad > 0)
                {
                    sSql = string.Format("Exec spp_Mtto_DevolucionesDetLotes_Ubicaciones " + 
                        " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', @FolioDevolucion = '{4}', " + 
                        " @IdProducto = '{5}', @CodigoEAN = '{6}', @ClaveLote = '{7}', @IdPasillo = '{8}', @IdEstante = '{9}', @IdEntrepaño = '{10}', @Cant_Devuelta = '{11}', @SKU = '{12}' ",
                        sIdEmpresa, sIdEstado, sIdFarmacia, L.IdSubFarmacia, sFolioDevolucion, L.IdProducto, L.CodigoEAN, L.ClaveLote,
                        L.Pasillo, L.Estante, L.Entrepano, L.Cantidad, L.SKU);

                    bRegresa = exec.Exec(sSql);
                    if (!bRegresa)
                    {
                        Error.GrabarError(exec, "GrabarDevolucion_DetLotes_Ubicaciones()");
                        break;
                    }                    
                }
            }

            return bRegresa;

        }

        private bool CancelarVale()
        {
            bool bRegresa = true;
            //////string sSql = "", sFolioVenta = "", sFechaCanje = "";
            //////int iOpcion = 2;

            //////if (sFolioVale != "")
            //////{ 
            //////    ////sSql = string.Format(" Exec spp_Mtto_Ventas_Vales '{0}', '{1}', '{2}', '{3}', '', '', '', '{4}' ",
            //////    ////    sIdEmpresa, sIdEstado, sIdFarmacia, sFolioVale, iOpcion);

            //////    sSql = string.Format(" Exec spp_Mtto_Vales_EmisionEnc '{0}', '{1}', '{2}', '{3}', '', '', '', '{4}' ",
            //////        sIdEmpresa, sIdEstado, sIdFarmacia, sFolioVale, iOpcion);

            //////    if (!exec.Exec(sSql))
            //////    {
            //////        bRegresa = false;
            //////    }
            //////}
            
            return bRegresa;
        }
        #endregion Devoluciones

        #region Inventarios
        private bool AfectarExistencia()
        {
            bool bRegresa = true;
            AfectarInventario Inv = AfectarInventario.Aplicar;
            AfectarCostoPromedio Costo = AfectarCostoPromedio.NoAfectar;

            string sSql = sInicio + string.Format("Exec spp_INV_AplicarDesaplicarExistencia \n" + 
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovto = '{3}', @Aplica = '{4}', @AfectarCostos = '{5}' ",
                sIdEmpresa, sIdEstado, sIdFarmacia, sFolioMovtoInv, (int)Inv, (int)Costo);

            if (!exec.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(exec, "AfectarExistencia()");
            }

            if (DtGeneral.ConfirmacionConHuellas && bRegresa)
            {
                bRegresa = opPermisosEspeciales.GrabarPropietarioDeHuella(sFolioMovtoInv);
            }

            return bRegresa;
        }

        private bool GrabarMovtoInv()
        {
            bool bRegresa = true;

            string sSql = sInicio + string.Format("Exec spp_Mtto_MovtoInv_Enc \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovtoInv = '{3}', @IdTipoMovto_Inv = '{4}', @TipoES = '{5}', @Referencia = '{6}', \n" +
                "\t@IdPersonal = '{7}', @Observaciones = '{8}', @SubTotal = '{9}', @Iva = '{10}', @Total = '{11}', @iOpcion = '{12}', @SKU = '{13}' \n",
                sIdEmpresa, sIdEstado, sIdFarmacia, "*", sIdTipoMovtoInv, sTipoES, "",
                sIdPersonal, sObservaciones .Trim(),
                General.GetFormatoNumerico_Double(dSubTotal),
                General.GetFormatoNumerico_Double(dIva),
                General.GetFormatoNumerico_Double(dTotal), 1, SKU.SKU);

            if (!exec.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(exec, "GrabarMovtoInv()");
            }
            else
            {
                exec.Leer();
                sFolioMovtoInv = exec.Campo("Folio");

                SKU.FolioMovimiento = exec.Campo("Folio");
                SKU.TipoDeMovimiento = sIdTipoMovtoInv;
                SKU.Foliador = exec.Campo("Foliador");


                bRegresa = GrabarMovtoInv_Det();
            }

            return bRegresa;
        }

        private bool GrabarMovtoInv_Det()
        {
            bool bRegresa = false;
            string sSql = "";

            foreach(clsProducto P in pListaProductos)
            {
                if ( P.IdProducto != "")
                {
                    if (P.Cantidad > 0)
                    {
                        // Registrar el producto en las tablas de existencia 
                        sSql = sInicio + string.Format("Exec spp_Mtto_FarmaciaProductos @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdProducto = '{3}' \n" +
                                             "Exec spp_Mtto_FarmaciaProductos_CodigoEAN  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdProducto = '{3}', @CodigoEAN = '{4}' \n",
                                             sIdEmpresa, sIdEstado, sIdFarmacia, P.IdProducto, P.CodigoEAN);
                        if (!exec.Exec(sSql))
                        {
                            bRegresa = false;
                            Error.GrabarError(exec, "GrabarMovtoInv_Det()");
                            break;
                        }
                        else
                        {
                            sSql = string.Format("Exec spp_Mtto_MovtosInv_Det \n" + 
                                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovtoInv = '{3}', \n" +
                                "\t@IdProducto = '{4}', @CodigoEAN = '{5}', @UnidadDeSalida = '{6}', @TasaIva = '{7}', @Cantidad = '{8}', @Costo = '{9}', \n" +
                                "\t@Importe = '{10}', @Status = '{11}' ",
                                 sIdEmpresa, sIdEstado, sIdFarmacia, SKU.FolioMovimiento, P.IdProducto, P.CodigoEAN, P.Unidad,
                                P.TasaIva, P.Cantidad, P.Valor, P.SubTotal, 'A');
                            if (!exec.Exec(sSql))
                            {
                                bRegresa = false;
                                Error.GrabarError(exec, "GrabarMovtoInv_Det()");
                                break;
                            }
                            else
                            {
                                bRegresa = GrabarMovtoInv_DetLotes(P.IdProducto, P.CodigoEAN, P.Valor);
                                if (!bRegresa)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return bRegresa;
        }

        private bool GrabarMovtoInv_DetLotes(string IdProducto, string CodigoEAN, double Costo)
        {
            bool bRegresa = false;
            string sSql = "";
            

            clsLotes[] ListaLotes = Lotes.Lotes(IdProducto, CodigoEAN);

            foreach (clsLotes L in ListaLotes)
            {
                if (L.Cantidad > 0)
                {
                    // Registrar el producto en las tablas de existencia 
                    sSql = sInicio + string.Format("Exec spp_Mtto_FarmaciaProductos_CodigoEAN_Lotes \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', @IdProducto = '{4}', @CodigoEAN = '{5}', @ClaveLote = '{6}', \n" +
                        "\t@FechaCaduca = '{7}', @IdPersonal = '{8}', @SKU = '{9}' \n",
                        sIdEmpresa, sIdEstado, sIdFarmacia, L.IdSubFarmacia, IdProducto, CodigoEAN, L.ClaveLote, General.FechaYMD(L.FechaCaducidad, "-"), DtGeneral.IdPersonal, L.SKU);
                    if (!exec.Exec(sSql))
                    {
                        bRegresa = false;
                        Error.GrabarError(exec, "GrabarMovtoInv_DetLotes()");
                        break;
                    }
                    else
                    {
                        sSql = string.Format("Exec spp_Mtto_MovtosInv_DetLotes \n" +
                            "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', @FolioMovtoInv = '{4}', \n" +
                            "\t@IdProducto = '{5}', @CodigoEAN = '{6}', @ClaveLote = '{7}', @Cantidad = '{8}', @Costo = '{9}', @Importe = '{10}', @Status = '{11}', @SKU = '{12}' \n",
                            sIdEmpresa, sIdEstado, sIdFarmacia, L.IdSubFarmacia, SKU.FolioMovimiento, IdProducto, CodigoEAN, L.ClaveLote, L.Cantidad, Costo, L.Cantidad * Costo, 'A', L.SKU);
                        if (!exec.Exec(sSql))
                        {
                            bRegresa = false;
                            Error.GrabarError(exec, "GrabarMovtoInv_DetLotes()");
                            break;
                        }
                        else
                        {
                            bRegresa = true;
                            if (GnFarmacia.ManejaUbicaciones)
                            {
                                bRegresa = GrabarDetalleLotesUbicaciones(L);
                                if(!bRegresa)
                                {
                                    break; 
                                }
                            }
                        }
                    }
                }
            }

            return bRegresa;
        }

        private bool GrabarDetalleLotesUbicaciones(clsLotes Lote)
        {
            bool bRegresa = false;
            string sSql = "";

            clsLotesUbicacionesItem[] Ubicaciones = Lote.Ubicaciones(Lote.SKU, Lote.Codigo, Lote.CodigoEAN, Lote.ClaveLote, Lote.IdSubFarmacia);

            foreach (clsLotesUbicacionesItem L in Ubicaciones)
            {
                if (L.Cantidad > 0)
                {
                    // Registrar el producto en las tablas de existencia 
                    sSql = string.Format("Exec spp_Mtto_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones \n" +
                        "\n@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', \n" +
                        "\n@IdProducto = '{4}', @CodigoEAN = '{5}', @ClaveLote = '{6}', @IdPasillo = '{7}', @IdEstante = '{8}', @IdEntrepano = '{9}', @SKU = '{10}' \n",
                        DtGeneral.EmpresaConectada, sIdEstado, sIdFarmacia, L.IdSubFarmacia, L.IdProducto, L.CodigoEAN, L.ClaveLote,
                        L.Pasillo, L.Estante, L.Entrepano, L.SKU);

                    if (!exec.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    else
                    {
                        sSql = string.Format("Exec spp_Mtto_MovtosInv_DetLotes_Ubicaciones \n" +
                            "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', @FolioMovtoInv = '{4}', \n" +
                            "\t@IdProducto = '{5}', @CodigoEAN = '{6}', @ClaveLote = '{7}', @Cantidad = '{8}', \n" +
                            "\t@IdPasillo = '{9}', @IdEstante = '{10}', @IdEntrepano = '{11}', @Status = '{12}', @SKU = '{13}' \n",
                            DtGeneral.EmpresaConectada, sIdEstado, sIdFarmacia, L.IdSubFarmacia, SKU.FolioMovimiento,
                            L.IdProducto, L.CodigoEAN, L.ClaveLote, L.Cantidad, L.Pasillo, L.Estante, L.Entrepano, 'A', L.SKU);

                        bRegresa = exec.Exec(sSql);
                        if (!bRegresa)
                        {
                            break;
                        }
                    }
                }
            }

            return bRegresa;

        }
        #endregion Inventarios

        #region Motivos_Devoluciones
        private bool GuardaMotivosDev()
        {
            bool bRegresa = true;
            string sSql = "";
            string sTipoMov = "", sMotivo = "";
            int iMarca = 0;

            leer.DataSetClase = dtsMotivosDev;

            while (leer.Leer())
            {
                iMarca = 0;

                iMarca = leer.CampoInt("Marca");
                sTipoMov = leer.Campo("IdMovto");
                sMotivo = leer.Campo("IdMotivo");

                if (iMarca == 1)
                {
                    sSql = string.Format(" Exec spp_Mtto_MovtosInv_Adt_Devoluciones  " + 
                        " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovtoInv = '{3}', @IdTipoMovto_Inv = '{4}', @IdMotivo = '{5}' ",
                        DtGeneral.EmpresaConectada, sIdEstado, sIdFarmacia, SKU.FolioMovimiento, sTipoMov, sMotivo);

                    if (!exec.Exec(sSql))
                    {                        
                        bRegresa = false;
                        break;
                    }
                }
            }           

            return bRegresa;
        }
        #endregion Motivos_Devoluciones

        #region Reubicaciones_De_Devoluciones
        private bool ReubicacionDevoluciones() 
        {
            bool bRegresa = true;

            if (GnFarmacia.ManejaUbicacionesEstandar)
            {
                if (sNombrePosicion.Trim() != "")
                {
                    bRegresa = Guarda_Reubicacion(1);

                    if (bRegresa)
                    {
                        bRegresa = Aplica_DesAplica_Existencia(sFolioREUSalida);
                    }

                    if (bRegresa)
                    {
                        bRegresa = Guarda_Reubicacion(2);
                    }

                    if (bRegresa)
                    {
                        bRegresa = Aplica_DesAplica_Existencia(sFolioREUEntrada);
                    }
                }
            }

            return bRegresa;
        }

        private bool Guarda_Reubicacion(int Opcion)
        {
            bool bRegresa = true;
            string sSql = "", sTipoES = "", sFolio = "";
            string sIdMovto = sMovtoSREU;
            int iTipoMovto = 0;

            sTipoES = "S";

            if (Opcion == 2)
            {
                sIdMovto = sMovtoEREU;
                sTipoES = "E";
                iTipoMovto = 1;
            }

            sSql = string.Format("Exec spp_Mtto_Reubicacion_Movtos_Devoluciones \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovtoInv = '{3}', @IdPersonalRegistra = '{4}', \n" +
                "\t@IdTipoMovto_Inv = '{5}', @TipoES = '{6}', @NombrePosicion = '{7}', @TipoMovto = '{8}' \n",
                sIdEmpresa, sIdEstado, sIdFarmacia, sFolioMovtoInv, sIdPersonal, sIdMovto, sTipoES, sNombrePosicion, iTipoMovto );

            if (!exec.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                if (exec.Leer())
                {
                    sFolio = exec.Campo("Folio");

                    if (Opcion == 1)
                    {
                        sFolioREUSalida = exec.Campo("Folio");
                        //General.msjUser(" Se genero el Folio de Reubicación de salida : " + sFolio + " satisfactoriamente.");
                    }
                    else
                    {
                        sFolioREUEntrada = exec.Campo("Folio");
                        //General.msjUser(" Se genero el Folio de Reubicación de entrada : " + sFolio + " satisfactoriamente.");
                    }             
                                        
                }
            }

            return bRegresa;
        }

        private bool Aplica_DesAplica_Existencia(string Folio)
        {
            bool bRegresa = true;
            AfectarInventario Inv = AfectarInventario.Aplicar;
            AfectarCostoPromedio Costo = AfectarCostoPromedio.NoAfectar;

            string sSql = sInicio + string.Format("Exec spp_INV_AplicarDesaplicarExistencia \n" + 
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovto = '{3}', @Aplica = '{4}', @AfectarCostos = '{5}'",
                sIdEmpresa, sIdEstado, sIdFarmacia, Folio, (int)Inv, (int)Costo);

            if (!exec.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(exec, "AfectarExistencia()");
            }

            return bRegresa;
        }
        #endregion Reubicaciones_De_Devoluciones

        #region Impresion_ReUbicacion
        public void ImprimirReubicacion(int Opcion)
        {
            bool bRegresa = false;
            string sFolio = "";

            if (sFolioREUSalida.Trim() != "" && sFolioREUEntrada.Trim() != "")
            {
                sFolio = sFolioREUSalida;

                if (Opcion == 2)
                {
                    sFolio = sFolioREUEntrada;
                }

                DatosCliente.Funcion = "ImprimirReubicacion()";
                clsImprimir myRpt = new clsImprimir(datosCnn);

                myRpt.RutaReporte = GnFarmacia.RutaReportes;
                myRpt.NombreReporte = "PtoVta_InventarioInicial.rpt";

                myRpt.Add("IdEmpresa", sIdEmpresa);
                myRpt.Add("IdEstado", sIdEstado);
                myRpt.Add("IdFarmacia", sIdFarmacia);
                myRpt.Add("Folio", sFolio);

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);


                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }

        }
        #endregion Impresion_ReUbicacion
        #endregion Funciones y Procedimientos Privados

    }
}
