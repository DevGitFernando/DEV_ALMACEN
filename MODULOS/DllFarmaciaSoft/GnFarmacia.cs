using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Data;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Diagnostics;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Usuarios_y_Permisos;

using DllFarmaciaSoft;

//using Dll_IMach4;

using FarPoint.Win.Spread;

namespace DllFarmaciaSoft
{
    public static class GnFarmacia
    {
        #region Declaracion de variables
        //private static string sModulo = "Farmacia";
        //private static string sVersion = "";

        private static int iMesesCaducaMedicamento = 1;
        private static int iDiasARevisarpedidosCedis;
        private static string sDiasARevisarpedidosCedis = "";
        private static string sIdCaja = "01";

        private static clsDatosApp dpDatosApp = new clsDatosApp("Farmacia", "");
        private static clsParametrosPtoVta paramParametros;

        private static string sRutaReportes = "";
        private static DateTime dtpFechaOperacionSistema = DateTime.Now;
        private static double dTipoDeCambio = -1;

        private static bool bMostrarPreciosCostos = false;
        private static string sModuloTransferencia = "Servicio Cliente.exe";
        private static string sRutaServicio = Application.StartupPath + @"\\" + sModuloTransferencia;

        public static clsGrabarError Error = new clsGrabarError();
        private static bool bEsServidorLocal = false;
        private static bool bExisteMAC_Servidor = false;
        private static bool bExisteServicio = File.Exists(sRutaServicio);
        private static bool bEsEquipoDeDesarrollo = File.Exists(General.UnidadSO + @":\\Dev.xml");
        private static Color colorProductosIMach = Color.Yellow;
        private static bool bValidarSesionUsuario = false;
        private static bool bUnidadConServidorDedicado = false;

        public static string[] ListaFormas = { "FrmMain", "FrmNavegador" };

        private static bool bEsModulo_Operativo = false;
        private static string sEsModulo_Operativo = ""; 
        #endregion Declaracion de variables

        static GnFarmacia()
        {
            ////clsAbrirForma.AssemblyActual("Farmacia");
            ////dpDatosApp = clsAbrirForma.DatosApp;

            AssemblyName x = System.Reflection.Assembly.GetExecutingAssembly().GetName();
            dpDatosApp = new clsDatosApp(x.Name, x.Version.ToString());
        }

        #region Especiales
        public static void CargarModulo( string NombreModulo )
        {
            clsAbrirForma.AssemblyActual(NombreModulo);
            dpDatosApp = clsAbrirForma.DatosApp;
        }
        #endregion Especiales

        #region Propieades Dll
        public static clsDatosApp DatosApp
        {
            get { return dpDatosApp; }
            set { dpDatosApp = value; }
        }
        public static string Modulo
        {
            get { return dpDatosApp.Modulo; }
        }
        public static string Version
        {
            get { return dpDatosApp.Version; }
        }
        #endregion Propieades Dll

        #region Propiedades
        /// <summary>
        /// Indica los meses de caducidad a partir de la fecha actual que 
        /// puede tener un lote de medicamento para ser dado de alta.
        /// 2009-01-01 + 12 == > 2010-01-01
        /// </summary>
        public static int MesesCaducaMedicamento
        {
            get { return iMesesCaducaMedicamento; }
            set { iMesesCaducaMedicamento = value; }
        }

        public static string NumCaja
        {
            get { return sIdCaja; }
            set { sIdCaja = value; }
        }

        public static string RutaReportes
        {
            get
            {
                if(sRutaReportes == "")
                {
                    ////if(pParametros == null)
                    ////{
                    ////    pParametros = new clsParametrosPtoVta(General.DatosConexion, GnFarmacia.DatosApp, "", "", "");
                    ////}

                    sRutaReportes = pParametros.GetValor("RutaReportes");
                }
                return sRutaReportes;
            }
            set { sRutaReportes = value; }
        }

        public static double TipoDeCambioDollar
        {
            get
            {
                if(dTipoDeCambio < 0)
                    ObtenerTipoDeCambio();

                return dTipoDeCambio;
            }
            set { dTipoDeCambio = value; }
        }

        public static bool MostrarPrecios_y_Costos
        {
            get { return bMostrarPreciosCostos; }
            set { bMostrarPreciosCostos = value; }
        }

        public static DateTime FechaOperacionSistema
        {
            get
            {
                DateTime dt = General.FechaSistema;
                try
                {
                    dt = Convert.ToDateTime(pParametros.GetValor("FechaOperacionSistema"));
                }
                catch
                {
                }
                return dt;
            }
        }

        public static clsParametrosPtoVta Parametros
        {
            get { return pParametros; }
            set { pParametros = value; }
        }

        private static clsParametrosPtoVta pParametros
        {
            get 
            {
                if(paramParametros == null)
                {
                    paramParametros = new clsParametrosPtoVta(General.DatosConexion, GnFarmacia.DatosApp, "", "", "");
                }

                return paramParametros; 
            }
            set 
            {
                if(paramParametros == null)
                {
                    paramParametros = new clsParametrosPtoVta(General.DatosConexion, GnFarmacia.DatosApp, "", "", "");
                }

                paramParametros = value; 
            }
        }

        public static bool ValidarSesionUsuario
        {
            get { return bValidarSesionUsuario; }
            set { bValidarSesionUsuario = value; }
        }

        public static int DiasARevisarpedidosCedis
        {
            //get { return iDiasARevisarpedidosCedis; }

            get
            {
                if(sDiasARevisarpedidosCedis == "")
                {
                    sDiasARevisarpedidosCedis = pParametros.GetValor("DiasARevisarpedidosCedis");
                    try
                    {
                        iDiasARevisarpedidosCedis = Convert.ToInt32(sDiasARevisarpedidosCedis);
                    }
                    catch { }
                }
                return iDiasARevisarpedidosCedis;
            }
        }

        #endregion Propiedades

        #region Pedidos de Producto
        private static string sGeneraPedidoAutomatico = "";
        private static string sGenerePedidoEspecial = "";
        private static bool bGeneraPedidoAutomatico = false;
        private static bool bGeneraPeridoEspecial = false;
        private static string sImplementaValidacionCiega = "";
        private static bool bImplementaValidacionCiega = false;


        private static string sPedidos_ModificarInformacionAdicional = "";
        private static bool bPedidos_ModificarInformacionAdicional = false;

        private static string sPedidos_ModificarFoliosSurtidos = "";
        private static bool bPedidos_ModificarFoliosSurtidos = false;

        private static bool bMostrar_SKU_Surtidos = false;

        public static bool Mostrar_SKU_Surtidos
        {
            get { return bMostrar_SKU_Surtidos; }
            set { bMostrar_SKU_Surtidos = value; }
        }

        public static bool GeneraPedidosAutomaticos
        {
            get
            {
                if(sGeneraPedidoAutomatico == "")
                {
                    sGeneraPedidoAutomatico = pParametros.GetValor("GeneraPedidosAutomaticos");
                    try
                    {
                        bGeneraPedidoAutomatico = Convert.ToBoolean(sGeneraPedidoAutomatico);
                    }
                    catch { }
                }
                return bGeneraPedidoAutomatico;
            }
        }

        public static bool GeneraPedidosEspeciales
        {
            get
            {
                if(sGenerePedidoEspecial == "")
                {
                    sGenerePedidoEspecial = pParametros.GetValor("GeneraPedidosEspeciales");
                    try
                    {
                        bGeneraPeridoEspecial = Convert.ToBoolean(sGenerePedidoEspecial);
                    }
                    catch { }
                }
                return bGeneraPeridoEspecial;
            }
        }

        public static bool ImplementaValidacionCiega
        {
            get
            {
                if(sImplementaValidacionCiega == "")
                {
                    sImplementaValidacionCiega = pParametros.GetValor("ImplementaValidacionCiega");
                    try
                    {
                        bImplementaValidacionCiega = Convert.ToBoolean(sImplementaValidacionCiega);
                    }
                    catch { }
                }
                return bImplementaValidacionCiega;
            }
        }

        public static bool Pedidos_ModificarInformacionAdicional
        {
            get
            {
                if(sPedidos_ModificarInformacionAdicional == "")
                {
                    bPedidos_ModificarInformacionAdicional = pParametros.GetValorBool("Pedidos_ModificarInformacionAdicional");
                    sPedidos_ModificarInformacionAdicional = bPedidos_ModificarInformacionAdicional.ToString();
                }
                return bPedidos_ModificarInformacionAdicional;
            }
        }

        public static bool Pedidos_ModificarFoliosSurtidos
        {
            get
            {
                if (sPedidos_ModificarFoliosSurtidos == "")
                {
                    bPedidos_ModificarFoliosSurtidos = pParametros.GetValorBool("Pedidos_ModificarFoliosSurtidos");
                    sPedidos_ModificarFoliosSurtidos = bPedidos_ModificarFoliosSurtidos.ToString();
                }
                return bPedidos_ModificarFoliosSurtidos;
            }
        }

        

        #endregion Pedidos de Producto

        #region Impresion
        private static string sImpresionDetalladaTicket = "";
        private static bool bImpresionDetalladaTicket = false;

        public static bool ImpresionDetalladaTicket
        {
            get
            {
                if(sImpresionDetalladaTicket == "")
                {
                    sImpresionDetalladaTicket = pParametros.GetValor("ImpresionDetalladaTicket");
                    try
                    {
                        bImpresionDetalladaTicket = Convert.ToBoolean(sImpresionDetalladaTicket);
                    }
                    catch { }
                }
                return bImpresionDetalladaTicket;
            }
        }
        #endregion Impresion

        #region SEGURO POPULAR
        static string sIdClienteSeguroPopular = "";
        static bool bValidarInfoSeguroPopular = false;
        static bool bValidarBeneficioSeguroPopular = false;

        public static string SeguroPopular
        {
            get
            {
                //if (sIdClienteSeguroPopular == "")
                //{
                //    sIdClienteSeguroPopular = pParametros.GetValor("ClienteSeguroPopular");
                //    bValidarInfoSeguroPopular = Convert.ToBoolean(pParametros.GetValor("ValidarInfoClienteSeguroPopular"));
                //}
                ObtenerDatosSeguroPopular();
                return sIdClienteSeguroPopular;
            }
        }

        public static bool ValidarInformacionSeguroPopular
        {
            get
            {
                ObtenerDatosSeguroPopular();
                return bValidarInfoSeguroPopular;
            }
        }

        public static bool ValidarBeneficioSeguroPopular
        {
            get
            {
                ObtenerDatosSeguroPopular();
                return bValidarBeneficioSeguroPopular;
            }
        }

        private static void ObtenerDatosSeguroPopular()
        {
            if(sIdClienteSeguroPopular == "")
            {
                sIdClienteSeguroPopular = pParametros.GetValor("ClienteSeguroPopular");
                bValidarInfoSeguroPopular = pParametros.GetValorBool("ValidarInfoClienteSeguroPopular");
                bValidarBeneficioSeguroPopular = pParametros.GetValorBool("ValidarBeneficioClienteSeguroPopular");
            }
        }

        #endregion SEGURO POPULAR

        #region Alta de Beneficiarios 
        static string sUsuarioTienePermisoCapturaBeneficiariosNuevos = "REGISTRO_DE_BENEFICIARIOS";
        static string sPermitirCapturaBeneficiariosNuevos = "";
        static bool bPermitirCapturaBeneficiariosNuevos = false;
        static bool bUsuarioTienePermisoCapturaBeneficiariosNuevos = false;

        public static bool ValidarCapturaBeneficiariosNuevos( bool ValorDefault )
        {
            bool bRegresa = ValorDefault;

            if(DtGeneral.EsAlmacen)
            {
                if(sPermitirCapturaBeneficiariosNuevos == "")
                {
                    bUsuarioTienePermisoCapturaBeneficiariosNuevos = DtGeneral.PermisosEspeciales.TienePermiso(sUsuarioTienePermisoCapturaBeneficiariosNuevos);
                    sPermitirCapturaBeneficiariosNuevos = bUsuarioTienePermisoCapturaBeneficiariosNuevos.ToString();
                }

                bRegresa = bUsuarioTienePermisoCapturaBeneficiariosNuevos;
            }

            return bRegresa;
        }

        #endregion Alta de Beneficiarios

        #region SubClienteSSP_Estado
        static string sIdSubCliente = "";

        public static string SubCliente
        {
            get
            {
                ObtenerDatosSubClienteSSP();
                return sIdSubCliente;
            }
        }

        private static void ObtenerDatosSubClienteSSP()
        {
            if(sIdSubCliente == "")
            {
                sIdSubCliente = pParametros.GetValor("IdSubCliente");
            }
        }
        #endregion SubClienteSSP_Estado

        #region Cliente - SubCliente Default Operacion 
        static string sIdCliente_DefaultOperacion = "";
        static string sIdSubCliente_DefaultOperacion = "";

        public static string Cliente_DefaultOperacion
        {
            get
            {
                if(sIdCliente_DefaultOperacion == "")
                {
                    sIdCliente_DefaultOperacion = pParametros.GetValor("Cliente_DefaultOperacion");

                    if(sIdCliente_DefaultOperacion != "")
                    {
                        sIdCliente_DefaultOperacion = General.Fg.PonCeros(sIdCliente_DefaultOperacion, 4);
                    }
                }
                return sIdCliente_DefaultOperacion;
            }
        }

        public static string SubCliente_DefaultOperacion
        {
            get
            {
                if(sIdSubCliente_DefaultOperacion == "")
                {
                    sIdSubCliente_DefaultOperacion = pParametros.GetValor("Cliente_SubClienteDefaultOperacion");

                    if(sIdSubCliente_DefaultOperacion != "")
                    {
                        sIdSubCliente_DefaultOperacion = General.Fg.PonCeros(sIdSubCliente_DefaultOperacion, 4);
                    }
                }
                return sIdSubCliente_DefaultOperacion;
            }
        }
        #endregion Cliente - SubCliente Default Operacion

        #region PUBLICO GENERAL
        static string sIdClientePublicoGral = "0000";
        static string sIdSubClientePublicoGral = "0000";
        static string sIdProgramaPublicoGral = "0000";
        static string sIdSubProgramaPublicoGral = "0000";

        public static void CargarDatosPublicoGeneral()
        {
            sIdClientePublicoGral = pParametros.GetValor("CtePubGeneral");
            sIdSubClientePublicoGral = pParametros.GetValor("CteSubPubGeneral");
            sIdProgramaPublicoGral = pParametros.GetValor("ProgPubGeneral");
            sIdSubProgramaPublicoGral = pParametros.GetValor("ProgSubPubGeneral");
        }

        public static string PublicoGral
        {
            get { return sIdClientePublicoGral; }
        }

        public static string PublicoGralSubCliente
        {
            get { return sIdSubClientePublicoGral; }
        }

        public static string PublicoGralPrograma
        {
            get { return sIdProgramaPublicoGral; }
        }

        public static string PublicoGralSubPrograma
        {
            get { return sIdSubProgramaPublicoGral; }
        }
        #endregion PUBLICO GENERAL

        #region CLIENTE SS
        private static string sIdClaveCteSS_Edo = "";
        private static string sNombreClaveCteSS_Edo = "";

        public static string ClienteEdo
        {
            get
            {
                if(sIdClaveCteSS_Edo == "")
                {
                    sIdClaveCteSS_Edo = pParametros.GetValor("ClaveCteSS_Edo");
                    GetNombreClienteSS_Edo();
                }
                return sIdClaveCteSS_Edo;
            }
        }

        public static string NombreClienteEdo
        {
            get { return sNombreClaveCteSS_Edo; }
        }

        private static void GetNombreClienteSS_Edo()
        {
            clsConexionSQL myCnn = new clsConexionSQL(General.DatosConexion);
            clsLeer myLeer = new clsLeer(ref myCnn);

            if(myLeer.Exec(string.Format("Select Top 1 * From vw_Clientes (NoLock) Where IdCliente = '{0}' ", sIdClaveCteSS_Edo)))
            {
                if(myLeer.Leer())
                    sNombreClaveCteSS_Edo = myLeer.Campo("NombreCliente");
            }
        }
        #endregion CLIENTE SS

        #region Lotes
        private static string sMostrarLotesSinExistencia = "";
        private static bool bMostrarLotesSinExistencia = false;

        public static bool MostrarLotesSinExistencia
        {
            get
            {
                if(sMostrarLotesSinExistencia == "")
                {
                    bMostrarLotesSinExistencia = pParametros.GetValorBool("MostrarLotesSinExistencia");
                    sMostrarLotesSinExistencia = bMostrarLotesSinExistencia.ToString();
                }
                return bMostrarLotesSinExistencia;
            }

            set
            {
                bMostrarLotesSinExistencia = value;
                sMostrarLotesSinExistencia = bMostrarLotesSinExistencia.ToString();
            }
        }
        #endregion Lotes

        #region Claves para inventario aleatorio 
        private static int iClaves_INV_Aleatorio__Default = 3;
        private static int iClaves_INV_Aleatorio__Dispensador = 3;
        private static int iClaves_INV_Aleatorio__Encargado = 3;
        private static int iClaves_INV_Aleatorio__CierrePeriodo = 3;
        private static bool bClavesINV_Aleatorio__Asignadas = false;


        private static int iProductos_INV_Aleatorio__Default = 5;
        private static int iProductos_INV_Aleatorio__Dispensador = 5;
        private static int iProductos_INV_Aleatorio__Encargado = 10;


        public static int Claves_Inventario_Aleatorio__Dispensador
        {
            get
            {
                if(!bClavesINV_Aleatorio__Asignadas)
                {
                    Get_Claves_Inventario_Aleatorio();
                }

                return iClaves_INV_Aleatorio__Dispensador;
            }
        }

        public static int Claves_Inventario_Aleatorio__Encargado
        {
            get
            {
                if(!bClavesINV_Aleatorio__Asignadas)
                {
                    Get_Claves_Inventario_Aleatorio();
                }

                return iClaves_INV_Aleatorio__Encargado;
            }
        }

        public static int Claves_Inventario_Aleatorio__CierrePeriodo
        {
            get
            {
                if(!bClavesINV_Aleatorio__Asignadas)
                {
                    Get_Claves_Inventario_Aleatorio();
                }

                return iClaves_INV_Aleatorio__CierrePeriodo;
            }
        }

        public static int Productos_Inventario_Aleatorio__Dispensador
        {
            get
            {
                if(!bClavesINV_Aleatorio__Asignadas)
                {
                    Get_Claves_Inventario_Aleatorio();
                }

                return iProductos_INV_Aleatorio__Dispensador;
            }
        }

        public static int Productos_Inventario_Aleatorio__Encargado
        {
            get
            {
                if(!bClavesINV_Aleatorio__Asignadas)
                {
                    Get_Claves_Inventario_Aleatorio();
                }

                return iProductos_INV_Aleatorio__Encargado;
            }
        }

        private static void Get_Claves_Inventario_Aleatorio()
        {
            bClavesINV_Aleatorio__Asignadas = true;
            iClaves_INV_Aleatorio__Dispensador = pParametros.GetValorInt("ClavesINV_AleatorioDispensador");
            iClaves_INV_Aleatorio__Encargado = pParametros.GetValorInt("ClavesINV_AleatorioEncargado");
            iClaves_INV_Aleatorio__CierrePeriodo = pParametros.GetValorInt("ClavesINV_AleatorioCierrePeriodo");

            iProductos_INV_Aleatorio__Dispensador = pParametros.GetValorInt("ProductosINV_AleatorioDispensador");
            iProductos_INV_Aleatorio__Encargado = pParametros.GetValorInt("ProductosINV_AleatorioEncargado");


            if(iClaves_INV_Aleatorio__Dispensador == 0)
            {
                iClaves_INV_Aleatorio__Dispensador = iClaves_INV_Aleatorio__Default;
            }

            if(iClaves_INV_Aleatorio__Encargado == 0)
            {
                iClaves_INV_Aleatorio__Encargado = iClaves_INV_Aleatorio__Default;
            }

            if(iClaves_INV_Aleatorio__CierrePeriodo == 0)
            {
                iClaves_INV_Aleatorio__CierrePeriodo = iClaves_INV_Aleatorio__Default;
            }


            ////////////////////////////////////////////////////////////////////////////////////////////////// 
            if(iProductos_INV_Aleatorio__Dispensador == 0)
            {
                iProductos_INV_Aleatorio__Dispensador = iProductos_INV_Aleatorio__Default;
            }

            if(iProductos_INV_Aleatorio__Encargado == 0)
            {
                iProductos_INV_Aleatorio__Encargado = iProductos_INV_Aleatorio__Default;
            }



            //iClaves_INV_Aleatorio__Dispensador = iClaves_INV_Aleatorio__Dispensador == 0 ? iClaves_INV_Aleatorio__Default : iClaves_INV_Aleatorio__Dispensador;
            //iClaves_INV_Aleatorio__Encargado = iClaves_INV_Aleatorio__Encargado == 0 ? iClaves_INV_Aleatorio__Default : iClaves_INV_Aleatorio__Encargado;
            //iClaves_INV_Aleatorio__CierrePeriodo = iClaves_INV_Aleatorio__CierrePeriodo == 0 ? iClaves_INV_Aleatorio__Default : iClaves_INV_Aleatorio__CierrePeriodo; 
        }
        #endregion Claves para inventario aleatorio 

        #region Procesos al realizar Cortes Parciales y Cortes Diarios 
        private static bool bGeneraInventariosAleatoriosAutomaticos = false;
        private static bool bGeneraReporteDispensacionPersonal = false;
        private static bool bGeneraReporteDispensacionPersonal_Vales = false;
        private static bool bProcesosCortes_Asignados = false;
        private static bool bGeneraInventariosAleatoriosAutomaticos_Productos = false;

        public static bool GeneraInventariosAleatoriosAutomaticos
        {
            get
            {
                if(!bProcesosCortes_Asignados)
                {
                    Get_Procesos_Cortes();
                }

                return bGeneraInventariosAleatoriosAutomaticos;
            }
        }

        public static bool GeneraInventariosAleatoriosAutomaticos_Productos
        {
            get
            {
                if(!bProcesosCortes_Asignados)
                {
                    Get_Procesos_Cortes();
                }

                return bGeneraInventariosAleatoriosAutomaticos_Productos;
            }
        }

        public static bool GeneraReporteDispensacionPersonal
        {
            get
            {
                if(!bProcesosCortes_Asignados)
                {
                    Get_Procesos_Cortes();
                }

                return bGeneraReporteDispensacionPersonal;
            }
        }

        public static bool GeneraReporteDispensacionValesPersonal
        {
            get
            {
                if(!bProcesosCortes_Asignados)
                {
                    Get_Procesos_Cortes();
                }

                return bGeneraReporteDispensacionPersonal_Vales;
            }
        }

        private static void Get_Procesos_Cortes()
        {
            bProcesosCortes_Asignados = true;
            bGeneraInventariosAleatoriosAutomaticos = pParametros.GetValorBool("Corte_InventariosAleatoriosAutomaticos");
            bGeneraInventariosAleatoriosAutomaticos_Productos = pParametros.GetValorBool("Corte_InventariosAleatoriosAutomaticos_Productos");
            bGeneraReporteDispensacionPersonal = pParametros.GetValorBool("Corte_ImpresionReporteDispensacion");
            bGeneraReporteDispensacionPersonal_Vales = pParametros.GetValorBool("Corte_ImpresionReporteDispensacion");
        }

        #endregion Procesos al realizar Cortes Parciales y Cortes Diarios

        #region MANEJO DE UBICACIONES
        private static bool bPermiteManejoUbicaciones = false;
        private static bool bPermiteManejoUbicacionesAsignado = false;

        private static bool bPermiteManejoUbicacionesEstandar = false;
        private static bool bPermiteManejoUbicacionesEstandarAsignado = false;

        private static bool bEsUnidadUnidosis = false;
        private static bool bEsUnidadUnidosisAsignado = false;

        public static bool ManejaUbicaciones
        {
            get { return bPermiteManejoUbicaciones; }
            set
            {
                if(!bPermiteManejoUbicacionesAsignado)
                {
                    bPermiteManejoUbicacionesAsignado = true;
                    bPermiteManejoUbicaciones = value;
                }
            }
        }

        public static bool EsUnidadUnidosis
        {
            get { return bEsUnidadUnidosis; }
            set
            {
                if(!bEsUnidadUnidosisAsignado)
                {
                    bEsUnidadUnidosisAsignado = true;
                    bEsUnidadUnidosis = value;
                }
            }
        }

        public static bool ManejaUbicacionesEstandar
        {
            get
            {
                if(!bPermiteManejoUbicacionesEstandarAsignado)
                {
                    bPermiteManejoUbicacionesEstandar = pParametros.GetValorBool("ManejaUbicacionesEstandar");
                    bPermiteManejoUbicacionesEstandarAsignado = true;
                }
                return bPermiteManejoUbicacionesEstandar;
            }
            set
            {
                if(!bPermiteManejoUbicacionesEstandarAsignado)
                {
                    bPermiteManejoUbicacionesEstandarAsignado = true;
                    bPermiteManejoUbicacionesEstandar = value;
                }
            }
        }
        #endregion MANEJO DE UBICACIONES 

        #region MANEJO DE CAJAS PARA DISTRIBUCION DE INSUMOS 
        private static bool bPermiteManejoCajas = false;
        private static bool bPermiteManejoCajasAsignado = false;

        public static bool ManejaCajasDeDistribucion
        {
            get
            {
                if(bPermiteManejoCajasAsignado == false)
                {
                    bPermiteManejoCajasAsignado = true;
                    bPermiteManejoCajas = pParametros.GetValorBool("ManejaCajasParaDistribucion");
                }
                return bPermiteManejoCajas;
            }
            set { bPermiteManejoCajas = value; }

        }
        #endregion MANEJO DE CAJAS PARA DISTRIBUCION DE INSUMOS

        #region Claves SSA
        private static string sCapturaDeClaves = "";
        private static bool bCapturaDeClaves = false;

        public static bool CapturaDeClavesSolicitadasHabilitada
        {
            get
            {
                if(sCapturaDeClaves == "")
                {
                    bCapturaDeClaves = pParametros.GetValorBool("CapturaClavesSolicitadas");
                    sCapturaDeClaves = bCapturaDeClaves.ToString();
                }
                return bCapturaDeClaves;
            }

            set
            {
                bCapturaDeClaves = value;
                sCapturaDeClaves = bCapturaDeClaves.ToString();
            }
        }
        #endregion Claves SSA

        #region Programacion de consumos Claves SSA 
        private static string sValidarConsumoClaves_Programacion = "";
        private static bool bValidarConsumoClaves_Programacion = false;

        private static string sValidarConsumoClaves_ProgramaAtencion = "";
        private static bool bValidarConsumoClaves_ProgramaAtencion = false;

        public static bool ValidarConsumoClaves_Programacion
        {
            get
            {
                if(sValidarConsumoClaves_Programacion == "")
                {
                    bValidarConsumoClaves_Programacion = pParametros.GetValorBool("ValidarConsumoClaves_Programacion");
                    sValidarConsumoClaves_Programacion = bValidarConsumoClaves_Programacion.ToString();
                }
                return bValidarConsumoClaves_Programacion;
            }

            set
            {
                bValidarConsumoClaves_Programacion = value;
                sValidarConsumoClaves_Programacion = bValidarConsumoClaves_Programacion.ToString();
            }
        }

        public static bool ValidarConsumoClaves_ProgramaAtencion
        {
            get
            {
                if(sValidarConsumoClaves_ProgramaAtencion == "")
                {
                    bValidarConsumoClaves_ProgramaAtencion = pParametros.GetValorBool("ValidarConsumoClaves_ProgramaAtencion");
                    sValidarConsumoClaves_ProgramaAtencion = bValidarConsumoClaves_ProgramaAtencion.ToString();
                }
                return bValidarConsumoClaves_ProgramaAtencion;
            }

            set
            {
                bValidarConsumoClaves_ProgramaAtencion = value;
                sValidarConsumoClaves_ProgramaAtencion = bValidarConsumoClaves_ProgramaAtencion.ToString();
            }
        }
        #endregion Programacion de consumos Claves SSA


        #region Validar selecion de ClaveSSA


        public static bool ValidarSeleccionClaveSSA( ref string sClaveSSA )
        {
            string sClaveSSA_temp = "";
            DllFarmaciaSoft.Consultas.FrmRevisarClaveSSA RevClaveSSA = new DllFarmaciaSoft.Consultas.FrmRevisarClaveSSA();
            bool bRegresa = true;

            sClaveSSA_temp = RevClaveSSA.VerificarCodigosEAN(sClaveSSA);
            bRegresa = RevClaveSSA.bClaveSeleccionada;

            if(bRegresa)
            {
                sClaveSSA = sClaveSSA_temp;
            }

            return bRegresa;
        }


        #endregion Validar selecion de ClaveSSA

        #region Validar selecion de Codigos EAN
        public static bool ValidarSeleccionCodigoEAN( string Codigo, ref string CodigoEAN_Seleccionado )
        {
            bool EsEAN_Unico = false;
            return ValidarSeleccionCodigoEAN(Codigo, ref CodigoEAN_Seleccionado, false, ref EsEAN_Unico);
        }

        public static bool ValidarSeleccionCodigoEAN( string Codigo, ref string CodigoEAN_Seleccionado, ref bool EsEAN_Unico )
        {
            return ValidarSeleccionCodigoEAN(Codigo, ref CodigoEAN_Seleccionado, false, ref EsEAN_Unico);
        }

        public static bool ValidarSeleccionCodigoEAN( string Codigo, ref string CodigoEAN_Seleccionado, bool EsPublicoGeneral, ref bool EsEAN_Unico )
        {
            DllFarmaciaSoft.Lotes.FrmRevisarCodigosEAN RevCodigosEAN = new DllFarmaciaSoft.Lotes.FrmRevisarCodigosEAN();
            bool bRegresa = true;

            CodigoEAN_Seleccionado = Codigo;
            CodigoEAN_Seleccionado = RevCodigosEAN.VerificarCodigosEAN(Codigo, EsPublicoGeneral);
            bRegresa = RevCodigosEAN.CodigoSeleccionado;
            EsEAN_Unico = RevCodigosEAN.EsEAN_Unico;

            return bRegresa;
        }
        #endregion Validar selecion de Codigos EAN

        #region Cierres de Tickets por Periodo
        private static string sDiasAdicionalesCierreTickets = "";
        private static int iDiasAdicionalesCierreTickets = 0;

        public static int DiasAdicionalesCierreTickets
        {
            get
            {
                if(sDiasAdicionalesCierreTickets == "")
                {
                    iDiasAdicionalesCierreTickets = pParametros.GetValorInt("CierreDeTicketsDiasAdicionalesPermitido");
                    sDiasAdicionalesCierreTickets = iDiasAdicionalesCierreTickets.ToString();
                }
                return iDiasAdicionalesCierreTickets;
            }

            set
            {
                iDiasAdicionalesCierreTickets = value;
                sDiasAdicionalesCierreTickets = iDiasAdicionalesCierreTickets.ToString();
            }
        }
        #endregion Cierres de Tickets por Periodo

        #region Dispensacion solo de Claves de Cuadro Basico
        private static string sDispensarSoloCuadroBasico = "";
        private static bool bDispensarSoloCuadroBasico = false;

        public static bool DispensarSoloCuadroBasico
        {
            get
            {
                if(sDispensarSoloCuadroBasico == "")
                {
                    bDispensarSoloCuadroBasico = pParametros.GetValorBool("DispensarSoloCuadroBasico");
                    sDispensarSoloCuadroBasico = bDispensarSoloCuadroBasico.ToString();
                }
                return bDispensarSoloCuadroBasico;
            }

            set
            {
                bDispensarSoloCuadroBasico = value;
                sDispensarSoloCuadroBasico = bDispensarSoloCuadroBasico.ToString();
            }
        }
        #endregion Dispensacion solo de Claves de Cuadro Basico

        #region Emision de Vales
        private static bool bEmiteVales = false;
        private static string sEmiteVales = "";

        private static bool bEmiteValesManuales = false;
        private static string sEmiteValesManuales = "";

        private static bool bEmiteValesCompletos = false;
        private static string sEmiteValesCompletos = "";

        private static bool bEmiteValesAutomaticosAlDispensar = false;
        private static string sEmiteValesAutomaticosAlDispensar = "";

        private static bool bEmiteValesExcepcion = false;
        private static string sEmiteValesExcepcion = "";

        private static bool bSeFirmanVales = false;
        private static string sSeFirmanVales = "";

        private static bool bManeja_FE_Vales = false;
        private static string sManeja_FE_Vales = "";

        private static bool bManeja_Vales_ServicioDomicilio = false;
        private static string sManeja_Vales_ServicioDomicilio = "";
        private static string sManeja_Vales_ServicioDomicilio_Inicio = "";

        private static bool bImplementaImpresionDesglosada_VtaTS = false;
        private static string sImplementaImpresionDesglosada_VtaTS = "";

        private static int iNumeroDeCopiasVales = 0;
        private static string sNumeroDeCopiasVales = "";

        private static bool bEmiteVales_ContenidoPaqueteLicitado = false;
        private static string sEmiteVales_ContenidoPaqueteLicitado = "";


        private static string sVale_Impresion_Personalizada = "";
        private static bool bVale_Impresion_Personalizada = false;
        private static string sVale_Plantilla_Personalizada = "";
        private static string sVale_Plantilla_Personalizada_Desglozado = "";
        private static string sVale_TipoDeImpresion = "";


        public static void GenerarExcepcionesDeVales()
        {
            if(DtGeneral.EsEquipoDeDesarrollo)
            {
                Vales.FrmValesExcepcion frm = new Vales.FrmValesExcepcion();
                frm.ShowDialog();
            }
        }

        public static string GetExcepcionVales( string Cadena )
        {
            string sRegresa = Cadena.Replace("-", "");
            clsCriptografo crypto = new clsCriptografo();

            sRegresa = crypto.Encrypt(sRegresa, true);

            return sRegresa;
        }

        public static string GetExcepcionValesDecode( string Cadena )
        {
            string sRegresa = Cadena;
            clsCriptografo crypto = new clsCriptografo();

            sRegresa = crypto.Decrypt(sRegresa, true);

            return sRegresa;
        }

        public static bool EmisionDeVales
        {
            get
            {
                if(sEmiteVales == "")
                {
                    bEmiteVales = pParametros.GetValorBool("EmiteVales");
                    sEmiteVales = bEmiteVales.ToString();
                }
                return bEmiteVales;
            }

            set
            {
                bEmiteVales = value;
                if(GnFarmacia.EsUnidadUnidosis)
                {
                    bEmiteVales = false;
                }
                sEmiteVales = bEmiteVales.ToString();
            }
        }

        public static bool EmisionDeValesCompletos
        {
            get
            {
                if(sEmiteVales == "")
                {
                    bEmiteValesCompletos = pParametros.GetValorBool("EmiteValesCompletos");
                    sEmiteValesCompletos = bEmiteValesCompletos.ToString();
                }
                return bEmiteValesCompletos;
            }

            set
            {
                bEmiteValesCompletos = value;
                if(GnFarmacia.EsUnidadUnidosis)
                {
                    bEmiteValesCompletos = false;
                }
                sEmiteValesCompletos = bEmiteValesCompletos.ToString();
            }
        }

        public static bool EmisionDeValesManuales
        {
            get
            {
                if(sEmiteValesManuales == "")
                {
                    bEmiteValesManuales = pParametros.GetValorBool("EmiteValesManuales");
                    sEmiteValesManuales = bEmiteValesManuales.ToString();
                }
                return bEmiteValesManuales;
            }

            set
            {
                bEmiteValesManuales = value;
                if(GnFarmacia.EsUnidadUnidosis)
                {
                    bEmiteValesManuales = false;
                }
                sEmiteValesManuales = bEmiteValesManuales.ToString();
            }
        }

        public static bool EmisionDeValesAutomaticosAlDispensar
        {
            get
            {
                if(sEmiteValesAutomaticosAlDispensar == "")
                {
                    bEmiteValesAutomaticosAlDispensar = pParametros.GetValorBool("EmiteValesAutomaticoAlDispensar");
                    sEmiteValesAutomaticosAlDispensar = bEmiteValesAutomaticosAlDispensar.ToString();
                }
                return bEmiteValesAutomaticosAlDispensar;
            }

            set
            {
                bEmiteValesAutomaticosAlDispensar = value;
                if(GnFarmacia.EsUnidadUnidosis)
                {
                    bEmiteValesAutomaticosAlDispensar = false;
                }
                sEmiteValesAutomaticosAlDispensar = bEmiteValesAutomaticosAlDispensar.ToString();
            }
        }

        public static bool EmiteValesExcepcion
        {
            get
            {
                if(sEmiteValesExcepcion == "")
                {
                    string sCadena_a_Validar = DtGeneral.EstadoConectado + DtGeneral.FarmaciaConectada;
                    sCadena_a_Validar = General.FechaYMD(General.FechaSistema, "");

                    //bEmiteValesCompletos = pParametros.GetValorBool("EmiteValesExcepcion");
                    sEmiteValesExcepcion = pParametros.GetValor("EmiteValesExcepcion");
                    sEmiteValesExcepcion = GetExcepcionValesDecode(sEmiteValesExcepcion);


                    if(General.Fg.Left(sEmiteValesExcepcion, 4) == DtGeneral.EstadoConectado + DtGeneral.FarmaciaConectada)
                    {
                        sEmiteValesExcepcion = General.Fg.Right(sEmiteValesExcepcion, 6);
                        if(Convert.ToInt32(sCadena_a_Validar) <= Convert.ToInt32(sEmiteValesExcepcion))
                        {
                            bEmiteValesExcepcion = true;
                        }
                    }

                    sEmiteValesExcepcion = bEmiteValesExcepcion.ToString();
                }
                return bEmiteValesExcepcion;
            }
            set { bEmiteValesExcepcion = value; }
        }

        public static bool FirmaVales
        {
            get
            {
                if(sSeFirmanVales == "")
                {
                    bSeFirmanVales = pParametros.GetValorBool("FirmarVales");
                    sSeFirmanVales = bSeFirmanVales.ToString();
                }
                return bSeFirmanVales;
            }

            set
            {
                bSeFirmanVales = value;
                if(GnFarmacia.EsUnidadUnidosis)
                {
                    bSeFirmanVales = false;
                }
                sSeFirmanVales = bSeFirmanVales.ToString();
            }
        }

        public static bool Maneja_ValesElectronicos
        {
            get
            {
                if(sManeja_FE_Vales == "")
                {
                    bManeja_FE_Vales = pParametros.GetValorBool("Vales_Maneja_FE");
                    sManeja_FE_Vales = bManeja_FE_Vales.ToString();
                }
                return bManeja_FE_Vales;
            }

            set
            {
                bManeja_FE_Vales = value;
                if(GnFarmacia.EsUnidadUnidosis)
                {
                    bManeja_FE_Vales = false;
                }
                sManeja_FE_Vales = bManeja_FE_Vales.ToString();
            }
        }

        public static int NumeroDeCopiasVales
        {
            get
            {
                if(sNumeroDeCopiasVales == "")
                {
                    iNumeroDeCopiasVales = pParametros.GetValorInt("EmisionValesCopias");
                    iNumeroDeCopiasVales = iNumeroDeCopiasVales == 0 ? 1 : iNumeroDeCopiasVales;

                    sNumeroDeCopiasVales = iNumeroDeCopiasVales.ToString();
                }
                return iNumeroDeCopiasVales;
            }

            set
            {
                iNumeroDeCopiasVales = value;
                if(GnFarmacia.EsUnidadUnidosis)
                {
                    iNumeroDeCopiasVales = 0;
                }
                sNumeroDeCopiasVales = iNumeroDeCopiasVales.ToString();
            }
        }

        public static bool EmiteVales_ContenidoPaqueteLicitado
        {
            get
            {
                if(sEmiteVales_ContenidoPaqueteLicitado == "")
                {
                    bEmiteVales_ContenidoPaqueteLicitado = pParametros.GetValorBool("EmisionVales_ContenidoPaqueteLicitado");
                    sEmiteVales_ContenidoPaqueteLicitado = bEmiteVales_ContenidoPaqueteLicitado.ToString();
                }
                return bEmiteVales_ContenidoPaqueteLicitado;
            }

            set
            {
                bEmiteVales_ContenidoPaqueteLicitado = value;
                if(GnFarmacia.EsUnidadUnidosis)
                {
                    bEmiteVales_ContenidoPaqueteLicitado = false;
                }
                sEmiteVales_ContenidoPaqueteLicitado = bEmiteVales_ContenidoPaqueteLicitado.ToString();
            }
        }

        //private static string sVale_Impresion_Personalizada = "";
        //private static bool bVale_Impresion_Personalizada = false;
        //private static string sVale_Plantilla_Personalizada = "";
        //private static string sVale_TipoDeImpresion = "";



        private static void Vales_ObtenerConfiguracionImpresion()
        {
            if(sVale_Impresion_Personalizada == "")
            {
                bVale_Impresion_Personalizada = pParametros.GetValorBool("EmisionVales_Impresion_Personalizada");
                sVale_Impresion_Personalizada = bVale_Impresion_Personalizada.ToString();

                sVale_Plantilla_Personalizada = pParametros.GetValor("EmisionVales_Plantilla_Personalizada_Ticket");
                sVale_Plantilla_Personalizada_Desglozado = pParametros.GetValor("EmisionVales_Plantilla_Personalizada_Ticket_Desglozado");

                sVale_TipoDeImpresion = pParametros.GetValor("EmisionVales_TipoDeImpresion");
            }
        }

        public static bool Vales_Impresion_Personalizada
        {
            get
            {
                Vales_ObtenerConfiguracionImpresion();
                return bVale_Impresion_Personalizada;
            }
            set { bVale_Impresion_Personalizada = value; }
        }

        public static string Vales_Plantilla_Personalizada
        {
            get
            {
                Vales_ObtenerConfiguracionImpresion();
                return sVale_Plantilla_Personalizada;
            }
            set { sVale_Plantilla_Personalizada = value; }
        }

        public static string Vales_Plantilla_Personalizada_Desglozada
        {
            get
            {
                Vales_ObtenerConfiguracionImpresion();
                return sVale_Plantilla_Personalizada_Desglozado;
            }
            set { sVale_Plantilla_Personalizada_Desglozado = value; }
        }

        public static bool ImplementaImpresionDesglosada_VtaTS
        {
            get
            {
                if(sImplementaImpresionDesglosada_VtaTS == "")
                {
                    bImplementaImpresionDesglosada_VtaTS = pParametros.GetValorBool("ImplementaImpresionDesglosada_VtaTS");
                    sImplementaImpresionDesglosada_VtaTS = bImplementaImpresionDesglosada_VtaTS.ToString();
                }
                return bImplementaImpresionDesglosada_VtaTS;
            }
        }


        public static Vales_TipoDeImpresion Vales_TipoDeImpresion
        {
            get
            {
                Vales_ObtenerConfiguracionImpresion();
                return (Vales_TipoDeImpresion)Convert.ToInt32(sVale_TipoDeImpresion);
            }
            set { sVale_TipoDeImpresion = ((int)value).ToString(); }
        }


        public static bool Maneja_ValesServicioDomicilio
        {
            get
            {
                if(sManeja_Vales_ServicioDomicilio == "")
                {
                    bManeja_Vales_ServicioDomicilio = pParametros.GetValorBool("Maneja_Vales_ServicioADomicilio");
                    sManeja_Vales_ServicioDomicilio = bManeja_Vales_ServicioDomicilio.ToString();
                }
                return bManeja_Vales_ServicioDomicilio;
            }

            set
            {
                bManeja_Vales_ServicioDomicilio = value;
                if(GnFarmacia.EsUnidadUnidosis)
                {
                    bManeja_Vales_ServicioDomicilio = false;
                }
                sManeja_Vales_ServicioDomicilio = bManeja_Vales_ServicioDomicilio.ToString();
            }
        }

        public static string Maneja_ValesServicioDomicilio_Inicio
        {
            get
            {
                DateTime d = DateTime.Now;
                DateTimePicker dt = new DateTimePicker();
                dt.Value = dt.Value.AddDays(2);
                if(sManeja_Vales_ServicioDomicilio_Inicio == "")
                {
                    int iAño = 0;
                    int iMes = 0;
                    int iDia = 0;

                    sManeja_Vales_ServicioDomicilio_Inicio = pParametros.GetValor("ServicioADomicilio_Inicio");
                    sManeja_Vales_ServicioDomicilio_Inicio = sManeja_Vales_ServicioDomicilio_Inicio.Replace("-", "").Trim();
                    sManeja_Vales_ServicioDomicilio_Inicio = General.Fg.PonCeros(sManeja_Vales_ServicioDomicilio_Inicio, 8);

                    try
                    {
                        iAño = Convert.ToInt32(General.Fg.Mid(sManeja_Vales_ServicioDomicilio_Inicio, 1, 4));
                        iMes = Convert.ToInt32(General.Fg.Mid(sManeja_Vales_ServicioDomicilio_Inicio, 5, 2));
                        iDia = Convert.ToInt32(General.Fg.Mid(sManeja_Vales_ServicioDomicilio_Inicio, 7, 2));

                        d = new DateTime(iAño, iMes, iDia);
                        dt.Value = d;
                    }
                    catch
                    {
                    }

                    sManeja_Vales_ServicioDomicilio_Inicio = General.FechaYMD(dt.Value);
                }


                return sManeja_Vales_ServicioDomicilio_Inicio;
            }

            set
            {
                sManeja_Vales_ServicioDomicilio_Inicio = value;
            }
        }

        //sManeja_Vales_ServicioDomicilio_Inicio 
        #endregion Emision de Vales

        #region Tipo de dispensacion Receta Foranea 
        private static string sClaveDispensacionRecetasForaneas = "";
        public static string ClaveDispensacionRecetasForaneas
        {
            get
            {
                if(sClaveDispensacionRecetasForaneas == "")
                {
                    sClaveDispensacionRecetasForaneas = pParametros.GetValor("ClaveDispensacionRecetasForaneas");
                }
                return sClaveDispensacionRecetasForaneas;
            }

            set
            {
                sClaveDispensacionRecetasForaneas = value;
            }
        }
        #endregion Tipo de dispensacion Receta Foranea

        #region Tipo de dispensacion Unidades No Administradas 
        private static string sClaveDispensacionUndadesNoAdministradas = "";

        public static string ClaveDispensacionUnidadesNoAdministradas
        {
            get
            {
                if(sClaveDispensacionUndadesNoAdministradas == "")
                {
                    sClaveDispensacionUndadesNoAdministradas = pParametros.GetValor("ClaveDispensacionUnidadesNoAdministradas");
                }
                return sClaveDispensacionUndadesNoAdministradas;
            }

            set
            {
                sClaveDispensacionUndadesNoAdministradas = value;
            }
        }
        #endregion Tipo de dispensacion Unidades No Administradas

        #region Fecha receta de años-meses anteriores
        private static string sPermiteRecetasAñosAnteriores = "";
        private static bool bPermiteRecetasAñosAnteriores = true;
        private static int iMesesRecetasAñosAnteriores = 0;
        private static int iMesesAnteriores_FechaReceta = 0;

        private static int iDiasHaciaAtras_FechaReceta = 0;
        private static int iDiasHaciaAdelante_FechaReceta = 0;

        public static int DiasHaciaAtras_FechaReceta
        {
            get
            {
                FechaRecetasAñosAnteriores();
                return iDiasHaciaAtras_FechaReceta;
            }

            set { iDiasHaciaAtras_FechaReceta = value; }
        }

        public static int DiasHaciaAdelante_FechaReceta
        {
            get
            {
                FechaRecetasAñosAnteriores();
                return iDiasHaciaAdelante_FechaReceta;
            }

            set { iDiasHaciaAdelante_FechaReceta = value; }
        }


        public static int MesesAtras_FechaRecetas
        {
            get
            {
                FechaRecetasAñosAnteriores();
                return iMesesAnteriores_FechaReceta;
            }

            set { iMesesAnteriores_FechaReceta = value; }
        }

        private static void FechaRecetasAñosAnteriores()
        {
            if(sPermiteRecetasAñosAnteriores == "")
            {
                bPermiteRecetasAñosAnteriores = pParametros.GetValorBool("PermitirRecetasFechaAñosAnteriores");
                iMesesRecetasAñosAnteriores = pParametros.GetValorInt("MesesRecetasFechaAñosAnteriores");

                iMesesAnteriores_FechaReceta = pParametros.GetValorInt("FechaRecetaMesesAnteriores");


                iDiasHaciaAtras_FechaReceta = pParametros.GetValorInt("FechaReceta_ADT_DiasHaciaAtras");
                iDiasHaciaAdelante_FechaReceta = pParametros.GetValorInt("FechaReceta_ADT_DiasHaciaAdelante");

            }
        }
        #endregion Fecha receta de años-meses anteriores

        #region Dispensacion de Venta con existencia de Consignacion 
        private static string sPermiteVenta_ConExistenciaConsignacion = "";
        private static bool bPermitirDispensacionVenta_SiExisteConsignacion = true;

        public static bool PermitirDispensacionVenta_ConExistenciaConsiganacion
        {
            get
            {
                if(sPermiteVenta_ConExistenciaConsignacion == "")
                {
                    bPermitirDispensacionVenta_SiExisteConsignacion = pParametros.GetValorBool("DispensarVentaConExistenciaConsignacion");
                    sPermiteVenta_ConExistenciaConsignacion = Convert.ToString(bPermitirDispensacionVenta_SiExisteConsignacion);
                }
                return bPermitirDispensacionVenta_SiExisteConsignacion;
            }
        }
        #endregion Dispensacion de Venta con existencia de Consignacion

        #region SubFarmacia__SubAlmacen de Inventario Propio (Venta) 
        private static string sIdSubFarmaciaVenta_Traspasos_Estados = "";
        private static string sIdSubFarmaciaVenta_Entrada_Traspasos_Estados = "";

        private static string stpInventario___TS_InterEstatal = "";
        private static TiposDeInventario tpInventario___TS_InterEstatal = TiposDeInventario.Venta;

        public static TiposDeInventario TipoInventario___TS_InterEstatal
        {
            get
            {
                if(stpInventario___TS_InterEstatal == "")
                {
                    try
                    {
                        tpInventario___TS_InterEstatal = (TiposDeInventario)pParametros.GetValorInt("TraspasosEstados_TipoDeInventario");
                    }
                    catch { tpInventario___TS_InterEstatal = TiposDeInventario.Venta; }

                    stpInventario___TS_InterEstatal = tpInventario___TS_InterEstatal.ToString();
                }
                return tpInventario___TS_InterEstatal;
            }
        }

        public static string SubFarmaciaVenta_Traspasos_Estados
        {
            get
            {
                if(sIdSubFarmaciaVenta_Traspasos_Estados == "")
                {
                    try
                    {
                        sIdSubFarmaciaVenta_Traspasos_Estados = pParametros.GetValor("SubFarmaciaTraspasosEstados");
                    }
                    catch { sIdSubFarmaciaVenta_Traspasos_Estados = "001"; }
                }
                return sIdSubFarmaciaVenta_Traspasos_Estados;
            }
        }

        public static string SubFarmaciaVenta_Entrada_Traspasos_Estados
        {
            get
            {
                if(sIdSubFarmaciaVenta_Entrada_Traspasos_Estados == "")
                {
                    try
                    {
                        sIdSubFarmaciaVenta_Entrada_Traspasos_Estados = pParametros.GetValor("SubFarmaciaEntradaTraspasosEstados");
                    }
                    catch { sIdSubFarmaciaVenta_Entrada_Traspasos_Estados = "001"; }
                }
                return sIdSubFarmaciaVenta_Entrada_Traspasos_Estados;
            }
        }
        #endregion SubFarmacia__SubAlmacen de Inventario Propio (Venta)

        #region Ajustes de Inventario 
        private static string sPermitirAjustesInventario_Con_ExistenciaEnTransito = "";
        private static bool bPermitirAjustesInventario_Con_ExistenciaEnTransito = false;

        public static bool PermitirAjustesInventario_Con_ExistenciaEnTransito
        {
            get
            {
                if(sPermitirAjustesInventario_Con_ExistenciaEnTransito == "")
                {
                    try
                    {
                        bPermitirAjustesInventario_Con_ExistenciaEnTransito = pParametros.GetValorBool("AjusteInv_ExistenciaEnTransito");
                    }
                    catch { }
                }

                sPermitirAjustesInventario_Con_ExistenciaEnTransito = bPermitirAjustesInventario_Con_ExistenciaEnTransito.ToString();
                return bPermitirAjustesInventario_Con_ExistenciaEnTransito;
            }
        }
        #endregion Ajustes de Inventario

        #region Transferencias de controlado
        private static string sHabilitarTransferenciasControlado = "";
        private static bool bHabilitarTransferenciasControlado = false;

        public static bool HabilitarTransferenciasControlado
        {
            get
            {
                if(sHabilitarTransferenciasControlado == "")
                {
                    try
                    {
                        bHabilitarTransferenciasControlado = pParametros.GetValorBool("HabilitarTransferenciasControlado");
                    }
                    catch { }
                }

                sHabilitarTransferenciasControlado = bHabilitarTransferenciasControlado.ToString();
                return bHabilitarTransferenciasControlado;
            }
        }
        #endregion Transferencias de controlado

        #region Tipo de dispensacion Receta Vales
        private static string sClaveDispensacionRecetasVales = "";
        private static string sClaveDispensacionRecetasValesForaneos = "";

        public static string ClaveDispensacionRecetasVales
        {
            get
            {
                if(sClaveDispensacionRecetasVales == "")
                {
                    sClaveDispensacionRecetasVales = pParametros.GetValor("TipoDispensacionVale");
                }
                return sClaveDispensacionRecetasVales;
            }

            set
            {
                sClaveDispensacionRecetasVales = value;
            }
        }

        public static string ClaveDispensacionRecetasValesForaneos
        {
            get
            {
                if(sClaveDispensacionRecetasValesForaneos == "")
                {
                    sClaveDispensacionRecetasValesForaneos = pParametros.GetValor("TipoDispensacionRecetaValeForaneo");
                }
                return sClaveDispensacionRecetasValesForaneos;
            }

            set
            {
                sClaveDispensacionRecetasValesForaneos = value;
            }
        }
        #endregion Tipo de dispensacion Receta Vales 

        #region Capturar Cama Habitación
        private static bool bCapturarNumeroDeCama = false;
        private static bool bCapturarNumeroDeHabitacion = false;
        public static bool CapturarNumeroDeCama
        {
            get
            {
                bCapturarNumeroDeCama = pParametros.GetValorBool("CapturarNumeroDeCama");
                return bCapturarNumeroDeCama;
            }
            set { bCapturarNumeroDeCama = value; }
        }

        public static bool CapturarNumeroDeHabitacion
        {
            get
            {
                bCapturarNumeroDeHabitacion = pParametros.GetValorBool("CapturarNumeroDeHabitacion");
                return bCapturarNumeroDeHabitacion;
            }
            set { bCapturarNumeroDeHabitacion = value; }
        }
        #endregion Capturar Cama Habitación

        #region Diagnostico y Beneficio de Receta
        private static string sClaveDiagnostico = "";
        private static string sClaveBeneficio = "";

        private static string sClaveDiagnosticoCaracteres = "";
        private static int iClaveDiagnosticoCaracteres = 4;

        public static string ClaveDiagnostico
        {
            get
            {
                if(sClaveDiagnostico == "")
                {
                    sClaveDiagnostico = pParametros.GetValor("Diagnostico");
                }
                return sClaveDiagnostico;
            }

            set
            {
                sClaveDiagnostico = value;
            }
        }

        public static int ClaveDiagnosticoCaracteres
        {
            get
            {
                if(sClaveDiagnosticoCaracteres == "")
                {
                    iClaveDiagnosticoCaracteres = pParametros.GetValorInt("DiagnosticoCaracteres");
                }

                sClaveDiagnosticoCaracteres = iClaveDiagnosticoCaracteres.ToString();
                return iClaveDiagnosticoCaracteres;
            }

            set
            {
                iClaveDiagnosticoCaracteres = value;
            }
        }

        public static string ClaveBeneficio
        {
            get
            {
                if(sClaveBeneficio == "")
                {
                    sClaveBeneficio = pParametros.GetValor("Beneficio");
                }
                return sClaveBeneficio;
            }

            set
            {
                sClaveBeneficio = value;
            }
        }
        #endregion Diagnostico y Beneficio de Receta

        #region Obtener_Url_Almacen_Regional
        private static string sComprasCentrales = "";

        private static string sAlmacenRegional = "";
        private static string sHostAlmacenRegional = "";
        private static string sIdFarmaciaAlmacenRegional = "";

        public static string UnidadComprasCentrales
        {
            get
            {
                string[] sParametros = null;

                try
                {
                    if(sComprasCentrales == "")
                    {
                        sComprasCentrales = pParametros.GetValor("UnidadComprasCentrales");
                        sParametros = sComprasCentrales.Split('-');

                        sComprasCentrales = "";
                        if(sParametros.Length < 3)
                        {
                            sComprasCentrales = "";
                        }
                        else
                        {
                            sComprasCentrales = sParametros[2];
                        }
                    }
                }
                catch
                {
                    sComprasCentrales = "";
                }
                return sComprasCentrales;
            }

            set
            {
                sComprasCentrales = value;
            }
        }

        public static string UrlAlmacenRegional
        {
            get
            {
                if(sAlmacenRegional == "")
                {
                    sAlmacenRegional = pParametros.GetValor("ServidorAlmacenRegional");
                    CargarUrlAlmacenRegional(sAlmacenRegional);
                }
                return sAlmacenRegional;
            }

            set
            {
                sAlmacenRegional = value;
                //sDispensarSoloCuadroBasico = bDispensarSoloCuadroBasico.ToString();
            }
        }

        public static string HostAlmacenRegional
        {
            get { return sHostAlmacenRegional; }
        }

        public static string IdFarmaciaAlmacenRegional
        {
            get { return sIdFarmaciaAlmacenRegional; }
        }

        private static void CargarUrlAlmacenRegional( string AlmacenRegional )
        {
            clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            clsLeer leer = new clsLeer(ref cnn);

            string sSql = string.Format(" Select Distinct U.IdFarmacia, (U.IdFarmacia + ' - ' + U.Farmacia) as Farmacia, U.UrlFarmacia, C.Servidor, C.WebService, C.PaginaWeb  " +
                            " From vw_Farmacias_Urls U (NoLock) " +
                            " Inner Join CFGS_ConfigurarConexiones C (NoLock) On ( U.IdEstado = C.IdEstado and U.IdFarmacia = C.IdFarmacia ) " +
                            " Where U.IdEstado = '{0}' and ( U.IdFarmacia = '{1}' ) " +
                            "   and U.FarmaciaStatus = 'A' and U.StatusRelacion = 'A' ",
                            AlmacenRegional.Substring(0, 2), AlmacenRegional.Substring(3, 4));

            if(!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarUrlAlmacenRegional()");
                General.msjError("Ocurrió un error al obtener Url Almacen Regional.");
            }
            else
            {
                if(leer.Leer())
                {
                    sAlmacenRegional = leer.Campo("UrlFarmacia");
                    sHostAlmacenRegional = leer.Campo("Servidor");
                    sIdFarmaciaAlmacenRegional = leer.Campo("IdFarmacia");

                    DtGeneral.DatosDeServicioWebCEDIS.Servidor = leer.Campo("Servidor");
                    DtGeneral.DatosDeServicioWebCEDIS.WebService = leer.Campo("WebService");
                    DtGeneral.DatosDeServicioWebCEDIS.PaginaASMX = leer.Campo("PaginaWeb");
                }
            }

        }
        #endregion Obtener_Url_Almacen_Regional 

        #region Obtener_Url_CEDIS
        private static string sAlmacenCEDIS = "";
        private static string sHostAlmacenCEDIS = "";
        private static string sIdFarmaciaAlmacenCEDIS = "";

        public static string UrlAlmacenCEDIS
        {
            get
            {
                if(sAlmacenCEDIS == "")
                {
                    sAlmacenCEDIS = pParametros.GetValor("ServidorAlmacenCEDIS");
                    CargarUrlAlmacenCEDIS(sAlmacenCEDIS);
                }
                return sAlmacenCEDIS;
            }

            set
            {
                sAlmacenCEDIS = value;
                //sDispensarSoloCuadroBasico = bDispensarSoloCuadroBasico.ToString();
            }
        }

        public static string HostAlmacenCEDIS
        {
            get { return sHostAlmacenCEDIS; }
        }

        public static string IdFarmaciaAlmacenCEDIS
        {
            get { return sIdFarmaciaAlmacenCEDIS; }
        }

        private static void CargarUrlAlmacenCEDIS( string AlmacenCEDIS )
        {
            clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            clsLeer leer = new clsLeer(ref cnn);
            basGenerales Fg = new basGenerales();
            string sIdEstado = "";
            string sIdFarmacia = "";

            // AlmacenCEDIS = AlmacenCEDIS != "" ? AlmacenCEDIS : "000000";
            AlmacenCEDIS = Fg.PonCeros(AlmacenCEDIS, 6);
            sIdEstado = Fg.Left(AlmacenCEDIS, 2);
            sIdFarmacia = Fg.Mid(AlmacenCEDIS, 3);


            string sSql = string.Format(" Select Distinct U.IdFarmacia, (U.IdFarmacia + ' - ' + U.Farmacia) as Farmacia, U.UrlFarmacia, C.Servidor  " +
                            " From vw_Farmacias_Urls U (NoLock) " +
                            " Inner Join CFGS_ConfigurarConexiones C (NoLock) On ( U.IdEstado = C.IdEstado and U.IdFarmacia = C.IdFarmacia ) " +
                            " Where U.IdEstado = '{0}' and ( U.IdFarmacia = '{1}' ) " +
                            "   and U.FarmaciaStatus = 'A' and U.StatusRelacion = 'A' ",
                            sIdEstado, sIdFarmacia);

            if(!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarUrlAlmacenCEDIS()");
                General.msjError("Ocurrió un error al obtener Url Almacen CEDIS.");
            }
            else
            {
                if(leer.Leer())
                {
                    sAlmacenCEDIS = leer.Campo("UrlFarmacia");
                    sHostAlmacenCEDIS = leer.Campo("Servidor");
                    sIdFarmaciaAlmacenCEDIS = leer.Campo("IdFarmacia");
                }
            }
        }
        #endregion Obtener_Url_CEDIS

        #region Exportar Informacion
        private static string sExportaInformacion = "";
        private static bool bExportaInformacion = false;

        public static bool ExportaInformacion
        {
            get
            {
                if(sExportaInformacion == "")
                {
                    bExportaInformacion = pParametros.GetValorBool("ExportaInformacion");
                    sExportaInformacion = bExportaInformacion.ToString();
                }
                return bExportaInformacion;
            }
            set { bExportaInformacion = value; }
        }
        #endregion Exportar Informacion

        #region Informacion de beneficiarios 
        private static string sReferenciaAuxiliarBeneficiario = "";
        private static bool bReferenciaAuxiliarBeneficiario = false;

        public static bool ReferenciaAuxiliarBeneficiario
        {
            get
            {
                if(sReferenciaAuxiliarBeneficiario == "")
                {
                    bReferenciaAuxiliarBeneficiario = pParametros.GetValorBool("ReferenciaAuxiliarBeneficiario");
                    sReferenciaAuxiliarBeneficiario = bReferenciaAuxiliarBeneficiario.ToString();
                }
                return bReferenciaAuxiliarBeneficiario;
            }
            set { bReferenciaAuxiliarBeneficiario = value; }
        }
        #endregion Informacion de beneficiarios

        #region Informacion de ubicaciones para surtido de Ventas/Transferencias 
        private static string sValidarUbicacionesEnCapturaDeSurtido = "";
        private static bool bValidarUbicacionesEnCapturaDeSurtido = false;

        public static bool ValidarUbicacionesEnCapturaDeSurtido
        {
            get
            {
                if(sValidarUbicacionesEnCapturaDeSurtido == "")
                {
                    bValidarUbicacionesEnCapturaDeSurtido = pParametros.GetValorBool("ValidarUbicacionesEnCapturaDeSurtido");
                    sValidarUbicacionesEnCapturaDeSurtido = bValidarUbicacionesEnCapturaDeSurtido.ToString();
                }
                return bValidarUbicacionesEnCapturaDeSurtido;
            }
            set { bValidarUbicacionesEnCapturaDeSurtido = value; }
        }
        #endregion Informacion de ubicaciones para surtido de Ventas/Transferencias

        #region Validar folios de receta unicos
        private static string sValidarFoliosDeRecetaUnicos = "";
        private static bool bValidarFoliosDeRecetaUnicos = false;

        public static bool ValidarFoliosDeRecetaUnicos
        {
            get
            {
                if(sValidarFoliosDeRecetaUnicos == "")
                {
                    bValidarFoliosDeRecetaUnicos = pParametros.GetValorBool("ValidarFoliosDeRecetaUnicos");
                    sValidarFoliosDeRecetaUnicos = bValidarFoliosDeRecetaUnicos.ToString();
                }
                return bValidarFoliosDeRecetaUnicos;
            }
            set { bValidarFoliosDeRecetaUnicos = value; }
        }
        #endregion Validar folios de receta unicos

        #region Huellas
        private static string sConfirmacionConHuellas = "";
        private static bool bConfirmacionConHuellas = false;

        public static bool ConfirmacionConHuellas
        {
            get
            {
                if(sConfirmacionConHuellas == "")
                {
                    bConfirmacionConHuellas = pParametros.GetValorBool("ConfirmacionConHuellas");
                    sConfirmacionConHuellas = bConfirmacionConHuellas.ToString();
                }
                return bConfirmacionConHuellas;
            }
            set { bConfirmacionConHuellas = value; }
        }
        #endregion Huellas

        #region Busqueda padron de beneficiarios
        private static string sInfBusquedaPadron = "";
        private static int iLongitudRerefencia = 10;
        private static bool bFormatearReferenciaPadron = false;
        private static int iLongitudApellidoPaterno = 3;
        private static int iLongitudNombre = 4;

        private static void InformacionBusquedaPadron()
        {
            if(sInfBusquedaPadron == "")
            {
                sInfBusquedaPadron = "true";
                iLongitudRerefencia = pParametros.GetValorInt("PadronRerefenciaLongitud");
                bFormatearReferenciaPadron = pParametros.GetValorBool("PadronFormatearRerefencia");
                iLongitudApellidoPaterno = pParametros.GetValorInt("PadronApellidoPaternoLongitud");
                iLongitudNombre = pParametros.GetValorInt("PadronNombreLongitud");
            }
        }

        public static int PadronLongitudRerefencia
        {
            get
            {
                InformacionBusquedaPadron();
                return iLongitudRerefencia;
            }
        }

        public static bool PadronFormatearRerefencia
        {
            get
            {
                InformacionBusquedaPadron();
                return bFormatearReferenciaPadron;
            }
        }

        public static int PadronLongitudApellidoPaterno
        {
            get
            {
                InformacionBusquedaPadron();
                return iLongitudApellidoPaterno;
            }
        }

        public static int PadronLongitudNombre
        {
            get
            {
                InformacionBusquedaPadron();
                return iLongitudNombre;
            }
        }
        #endregion Busqueda padron de beneficiarios

        #region Entradas por Consignacion - SubFarmacias Consiganacion emulando Venta 
        private static string sMostrarSF_ConsignaEmulaVenta = "";
        private static bool bsMostrarSF_ConsignaEmulaVenta = false;

        public static bool MostrarSubFarmaciaEmulaVenta_EntradasPorConsignacion
        {
            get
            {
                if(sMostrarSF_ConsignaEmulaVenta == "")
                {
                    bsMostrarSF_ConsignaEmulaVenta = pParametros.GetValorBool("ENT_CNSGN_MostrarSubFarmaciasEmulaVenta");
                    sMostrarSF_ConsignaEmulaVenta = bsMostrarSF_ConsignaEmulaVenta.ToString();
                }
                return bsMostrarSF_ConsignaEmulaVenta;
            }
            set { bsMostrarSF_ConsignaEmulaVenta = value; }
        }
        #endregion Entradas por Consignacion - SubFarmacias Consiganacion emulando Venta

        #region CAPTURA POR CAJAS COMPLETAS SEGUN CONTENIDO PAQUETE DEL PRODUCTO 
        private static string sForzarCapturaEnMultiplosHabilitarValidaciones = "";
        private static bool bForzarCapturaEnMultiplosHabilitarValidaciones = false;

        private static string sForzarCapturaEnMultiplosDeCajas = "";
        private static bool bForzarCapturaEnMultiplosDeCajas = false;

        private static string sForzarCapturaEnMultiplosDeCajas_ProgramaSubPrograma = "";
        private static bool bForzarCapturaEnMultiplosDeCajas_ProgramaSubPrograma = false;

        private static string sForzarCapturaEnMultiplosDeCajas_ClaveSSA_Especifica = "";
        private static bool bForzarCapturaEnMultiplosDeCajas_ClaveSSA_Especifica = false;


        /// <summary>
        /// Especifica si se deben realizar las validaciones correspondientes para la Dispensación en Multiplos de Caja 
        /// </summary>
        public static bool ForzarCapturaEnMultiplosHabilitarValidaciones
        {
            get
            {
                if(sForzarCapturaEnMultiplosHabilitarValidaciones == "")
                {
                    if(pParametros != null)
                    {
                        bForzarCapturaEnMultiplosHabilitarValidaciones = pParametros.GetValorBool("ForzarCapturaEnMultiplosHabilitarValidaciones");
                    }
                    sForzarCapturaEnMultiplosHabilitarValidaciones = bForzarCapturaEnMultiplosHabilitarValidaciones.ToString();
                }
                return bForzarCapturaEnMultiplosHabilitarValidaciones;
            }
            set { bForzarCapturaEnMultiplosHabilitarValidaciones = value; }
        }

        /// <summary>
        /// Nivel 3, forza la dispensación en Multiplos de Cajas 
        /// </summary>
        public static bool ForzarCapturaEnMultiplosDeCajas
        {
            get
            {
                if(sForzarCapturaEnMultiplosDeCajas == "")
                {
                    if(pParametros != null)
                    {
                        bForzarCapturaEnMultiplosDeCajas = pParametros.GetValorBool("ForzarCapturaEnMultiplosDeCajas");
                    }
                    sForzarCapturaEnMultiplosDeCajas = bForzarCapturaEnMultiplosDeCajas.ToString();
                }
                return bForzarCapturaEnMultiplosDeCajas;
            }
            set { bForzarCapturaEnMultiplosDeCajas = value; }
        }

        /// <summary>
        /// Nivel 2, forza la validación si el Programa-SubPrograma tiene habilitada esta configuración 
        /// </summary>
        public static bool ForzarCapturaEnMultiplosDeCajas_ProgramaSubPrograma
        {
            get
            {
                if(sForzarCapturaEnMultiplosDeCajas_ProgramaSubPrograma == "")
                {
                    if(pParametros != null)
                    {
                        bForzarCapturaEnMultiplosDeCajas_ProgramaSubPrograma = pParametros.GetValorBool("ForzarCapturaEnMultiplosDeCajas_ProgramaSubPrograma");
                    }
                    sForzarCapturaEnMultiplosDeCajas_ProgramaSubPrograma = bForzarCapturaEnMultiplosDeCajas_ProgramaSubPrograma.ToString();
                }
                return bForzarCapturaEnMultiplosDeCajas_ProgramaSubPrograma;
            }
            set { bForzarCapturaEnMultiplosDeCajas_ProgramaSubPrograma = value; }
        }

        /// <summary>
        /// Nivel 1, forza la validación si la ClaveSSA tiene habilitada esta configuración 
        /// </summary>
        public static bool ForzarCapturaEnMultiplosDeCajas_ClaveSSA
        {
            get
            {
                if(sForzarCapturaEnMultiplosDeCajas_ClaveSSA_Especifica == "")
                {
                    if(pParametros != null)
                    {
                        bForzarCapturaEnMultiplosDeCajas_ClaveSSA_Especifica = pParametros.GetValorBool("ForzarCapturaEnMultiplosDeCajas_ClaveSSA_Especifica");
                    }
                    sForzarCapturaEnMultiplosDeCajas_ClaveSSA_Especifica = bForzarCapturaEnMultiplosDeCajas_ClaveSSA_Especifica.ToString();
                }
                return bForzarCapturaEnMultiplosDeCajas_ClaveSSA_Especifica;
            }
            set { bForzarCapturaEnMultiplosDeCajas_ClaveSSA_Especifica = value; }
        }
        #endregion CAPTURA POR CAJAS COMPLETAS SEGUN CONTENIDO PAQUETE DEL PRODUCTO

        #region Ordenes de Compra - Modificaciones
        private static string sModificarListaProductos_OC = "";
        private static bool bModificarListaProductos_OC = false;

        private static string sModificarPreciosProductos_OC = "";
        private static bool bModificarPreciosProductos_OC = false;

        public static bool OrdenesDeCompra__ModificarListaDeProductos
        {
            get
            {
                if(sModificarListaProductos_OC == "")
                {
                    bModificarListaProductos_OC = pParametros.GetValorBool("COM_ModificarProductosOrdenesDeCompra");
                    sModificarListaProductos_OC = bModificarListaProductos_OC.ToString();
                }
                return bModificarListaProductos_OC;
            }
            set { bModificarListaProductos_OC = value; }
        }

        public static bool OrdenesDeCompra__ModificarPrecios
        {
            get
            {
                bool bModificarLista_Productos = OrdenesDeCompra__ModificarListaDeProductos;
                if(sModificarPreciosProductos_OC == "")
                {
                    bModificarPreciosProductos_OC = pParametros.GetValorBool("COM_ModificarPreciosOrdenesDeCompra");
                    sModificarPreciosProductos_OC = bModificarPreciosProductos_OC.ToString();
                }

                return bModificarLista_Productos == false ? false : bModificarPreciosProductos_OC;
            }
            set { bModificarListaProductos_OC = value; }
        }
        #endregion Ordenes de Compra - Modificaciones 

        #region Operaciones Externas
        private static string sINT_OPM_ManejaOperacionMaquila = "";
        private static bool bINT_OPM_ManejaOperacionMaquila = false;

        private static string sINT_OPM_EstadoOperacionMaquila = "";
        //private static bool bINT_OPM_EstadoOperacionMaquila = false;

        private static bool bINT_OPM_EstadoOperacionMaquila_DatoConfigurado = false;


        private static void INT_OPM_Configurar()
        {
            bINT_OPM_ManejaOperacionMaquila = pParametros.GetValorBool("INT_OPM_ManejaOperacionMaquila");
            sINT_OPM_ManejaOperacionMaquila = bINT_OPM_ManejaOperacionMaquila.ToString();

            sINT_OPM_EstadoOperacionMaquila = pParametros.GetValor("INT_OPM_EstadoOperacionMaquila");
            bINT_OPM_EstadoOperacionMaquila_DatoConfigurado = true;

            ////bINT_OPM_EstadoOperacionMaquila_DatoConfigurado = sINT_OPM_EstadoOperacionMaquila == "";
        }

        public static bool INT_OPM_ProcesoActivo
        {
            get { return INT_OPM_EstadoOperacionMaquila_DatoConfigurado && bINT_OPM_ManejaOperacionMaquila; }
        }

        public static bool INT_OPM_EstadoOperacionMaquila_DatoConfigurado
        {
            get
            {
                if(sINT_OPM_ManejaOperacionMaquila == "" || sINT_OPM_EstadoOperacionMaquila == "")
                {
                    INT_OPM_Configurar();
                }
                return bINT_OPM_EstadoOperacionMaquila_DatoConfigurado;
            }
            set { bINT_OPM_EstadoOperacionMaquila_DatoConfigurado = value; }
        }

        public static bool INT_OPM_ManejaOperacionMaquila
        {
            get
            {
                if(sINT_OPM_ManejaOperacionMaquila == "")
                {
                    INT_OPM_Configurar();
                }
                return bINT_OPM_ManejaOperacionMaquila;
            }
            set { bINT_OPM_ManejaOperacionMaquila = value; }
        }

        public static string INT_OPM_EstadoOperacionMaquila
        {
            get
            {
                if(sINT_OPM_ManejaOperacionMaquila == "")
                {
                    INT_OPM_Configurar();
                }
                return sINT_OPM_EstadoOperacionMaquila;
            }
            set { sINT_OPM_EstadoOperacionMaquila = value; }
        }
        #endregion Operaciones Externas 

        #region Impresion Personalizada Ticket Venta 
        private static string sVta_Plantilla_Personalizada_Ticket = "";
        private static string sVta_Plantilla_Personalizada_Ticket_PublicoGeneral = "";

        private static string sVta_Plantilla_Personalizada_Ticket_Detallado = "";
        private static string sVta_Plantilla_Personalizada_Ticket_Detallado_Precios = "";

        private static string sVta_Plantilla_Personalizada_Surtido_De_Pedidos = "";
        private static string sVta_Plantilla_Personalizada_Surtido_De_Pedidos_Caratula = "";

        private static bool bVta_Impresion_Personalizada_Ticket = false;
        private static bool bVta_Impresion_Personalizada_Ticket_Detalle = false;

        private static int iNumeroDeCopiasTickets = 0;
        private static string sNumeroDeCopiasTickets = "";

        private static bool bVta_Impresion_Directa = false;
        private static string sVta_Impresion_Directa = "";

        public static bool Vta_Impresion_Directa
        {
            get
            {
                if(sVta_Impresion_Directa == "")
                {
                    Vta_Impresion_Personalizada_Configurar();
                }
                return bVta_Impresion_Directa;
            }
            set { bVta_Impresion_Directa = value; }
        }

        public static int NumeroDeCopiasTickets
        {
            get
            {
                if(sNumeroDeCopiasTickets == "")
                {
                    iNumeroDeCopiasTickets = pParametros.GetValorInt("Vta_Impresion_Copias");
                    iNumeroDeCopiasTickets = iNumeroDeCopiasTickets == 0 ? 1 : iNumeroDeCopiasTickets;

                    sNumeroDeCopiasTickets = iNumeroDeCopiasVales.ToString();
                }
                return iNumeroDeCopiasTickets;
            }

            set
            {
                iNumeroDeCopiasTickets = value;
                ////if (GnFarmacia.EsUnidadUnidosis)
                ////{
                ////    iNumeroDeCopiasTickets = 0;
                ////}
                sNumeroDeCopiasTickets = iNumeroDeCopiasTickets.ToString();
            }
        }

        private static void Vta_Impresion_Personalizada_Configurar()
        {
            bVta_Impresion_Personalizada_Ticket = pParametros.GetValorBool("Vta_Impresion_Personalizada_Ticket");
            bVta_Impresion_Personalizada_Ticket_Detalle = pParametros.GetValorBool("Vta_Impresion_Personalizada_Ticket_Detallado");
            bVta_Impresion_Directa = pParametros.GetValorBool("Vta_Impresion_Directa");
            sVta_Impresion_Directa = bVta_Impresion_Directa.ToString();

            sVta_Plantilla_Personalizada_Ticket = pParametros.GetValor("Vta_Plantilla_Personalizada_Ticket");
            sVta_Plantilla_Personalizada_Ticket_Detallado = pParametros.GetValor("Vta_Plantilla_Personalizada_Ticket_Detallado");
            sVta_Plantilla_Personalizada_Ticket_Detallado_Precios = pParametros.GetValor("Vta_Plantilla_Personalizada_Ticket_Detallado_Precios");
            sVta_Plantilla_Personalizada_Ticket_PublicoGeneral = pParametros.GetValor("Vta_Plantilla_Personalizada_Ticket_PublicoGeneral");

            sVta_Plantilla_Personalizada_Surtido_De_Pedidos = pParametros.GetValor("Vta_Plantilla_Personalizada_Surtido_De_Pedidos");

            sVta_Plantilla_Personalizada_Surtido_De_Pedidos_Caratula = pParametros.GetValor("Vta_Plantilla_Personalizada_Surtido_De_Pedidos_Caratula");

        }

        public static string Vta_Impresion_Personalizada_Surtido_De_Pedidos
        {
            get
            {
                if(sVta_Plantilla_Personalizada_Surtido_De_Pedidos == "")
                {
                    Vta_Impresion_Personalizada_Configurar();
                }
                return sVta_Plantilla_Personalizada_Surtido_De_Pedidos;
            }
            set { sVta_Plantilla_Personalizada_Surtido_De_Pedidos = value; }
        }

        public static string Vta_Impresion_Personalizada_Surtido_De_Pedidos_Caratula
        {
            get
            {
                if(sVta_Plantilla_Personalizada_Surtido_De_Pedidos_Caratula == "")
                {
                    Vta_Impresion_Personalizada_Configurar();
                }
                return sVta_Plantilla_Personalizada_Surtido_De_Pedidos_Caratula;
            }
            set { sVta_Plantilla_Personalizada_Surtido_De_Pedidos_Caratula = value; }
        }

        public static bool Vta_Impresion_Personalizada_Ticket
        {
            get
            {
                if(sVta_Plantilla_Personalizada_Ticket == "")
                {
                    Vta_Impresion_Personalizada_Configurar();
                }
                return bVta_Impresion_Personalizada_Ticket;
            }
            set { bVta_Impresion_Personalizada_Ticket = value; }
        }

        public static bool Vta_Impresion_Personalizada_Ticket_Detalle
        {
            get
            {
                if(sVta_Plantilla_Personalizada_Ticket == "")
                {
                    Vta_Impresion_Personalizada_Configurar();
                }
                return bVta_Impresion_Personalizada_Ticket_Detalle;
            }
            set { bVta_Impresion_Personalizada_Ticket_Detalle = value; }
        }

        public static string Vta_Plantilla_Personalizada_Ticket
        {
            get
            {
                if(sVta_Plantilla_Personalizada_Ticket == "")
                {
                    Vta_Impresion_Personalizada_Configurar();
                }
                return sVta_Plantilla_Personalizada_Ticket;
            }
            set { sVta_Plantilla_Personalizada_Ticket = value; }
        }

        public static string Vta_Plantilla_Personalizada_Ticket_Detallado
        {
            get
            {
                if(sVta_Plantilla_Personalizada_Ticket_Detallado == "")
                {
                    Vta_Impresion_Personalizada_Configurar();
                }
                return sVta_Plantilla_Personalizada_Ticket_Detallado;
            }
            set { sVta_Plantilla_Personalizada_Ticket_Detallado = value; }
        }

        public static string Vta_Plantilla_Personalizada_Ticket_Detallado_Precios
        {
            get
            {
                if(sVta_Plantilla_Personalizada_Ticket_Detallado_Precios == "")
                {
                    Vta_Impresion_Personalizada_Configurar();
                }
                return sVta_Plantilla_Personalizada_Ticket_Detallado_Precios;
            }
            set { sVta_Plantilla_Personalizada_Ticket_Detallado_Precios = value; }
        }

        public static string Vta_Plantilla_Personalizada_Ticket_PublicoGeneral
        {
            get
            {
                if(sVta_Plantilla_Personalizada_Ticket_PublicoGeneral == "")
                {
                    Vta_Impresion_Personalizada_Configurar();
                }
                return sVta_Plantilla_Personalizada_Ticket_PublicoGeneral;
            }
            set { sVta_Plantilla_Personalizada_Ticket_PublicoGeneral = value; }
        }
        #endregion Impresion Personalizada Ticket Venta

        #region Codificacion de Productos 
        private static string sImplementaCodificacion_DM = "";
        private static bool bImplementaCodificacion_DM = false;

        public static bool ImplementaCodificacion_DM
        {
            get
            {
                if(sImplementaCodificacion_DM == "")
                {
                    bImplementaCodificacion_DM = pParametros.GetValorBool("ImplementaCodificacion");
                    sImplementaCodificacion_DM = bImplementaCodificacion_DM.ToString();
                }
                return bImplementaCodificacion_DM;
            }
            set { bImplementaCodificacion_DM = value; }
        }

        private static string sImplementaReaderDM = "";
        private static bool bImplementaReaderDM = false;

        public static bool ImplementaReaderDM
        {
            get
            {
                if(sImplementaReaderDM == "")
                {
                    bImplementaReaderDM = pParametros.GetValorBool("ImplementaReaderDM");
                    sImplementaReaderDM = bImplementaCodificacion_DM.ToString();
                }
                return bImplementaReaderDM;
            }
            set { bImplementaReaderDM = value; }
        }
        #endregion Codificacion de Productos 

        #region Beneficiarios con Alertas 
        private static string sValidarBeneficariosAlertas = "";
        private static bool bValidarBeneficariosAlertas = false;

        public static bool ValidarBeneficariosAlertas
        {
            get
            {
                if(sValidarBeneficariosAlertas == "")
                {
                    bValidarBeneficariosAlertas = pParametros.GetValorBool("ValidarBeneficariosAlertas");
                    sValidarBeneficariosAlertas = bValidarBeneficariosAlertas.ToString();
                }
                return bValidarBeneficariosAlertas;
            }
            set { bValidarBeneficariosAlertas = value; }
        }
        #endregion Beneficiarios con Alertas

        #region Interface con Expediente Electronico 
        private static string sImplementaInterfaceExpedienteElectronico = "";
        private static bool bImplementaInterfaceExpedienteElectronico = false;

        public static bool ImplementaInterfaceExpedienteElectronico
        {
            get
            {
                if(sImplementaInterfaceExpedienteElectronico == "")
                {
                    bImplementaInterfaceExpedienteElectronico = pParametros.GetValorBool("ImplementaInterfaceExpedienteElectronico");
                    sImplementaInterfaceExpedienteElectronico = bImplementaInterfaceExpedienteElectronico.ToString();
                }
                return bImplementaInterfaceExpedienteElectronico;
            }
            set { bImplementaInterfaceExpedienteElectronico = value; }
        }
        #endregion Interface con Expediente Electronico

        #region Interface para Digitalizacion de Recetas 
        private static string sImplementaDigitalizacion = "";
        private static bool bImplementaDigitalizacion = false;
        private static bool bImplementaDigitalizacionDepurar = false;

        public static bool ImplementaDigitalizacion
        {
            get
            {
                if(sImplementaDigitalizacion == "")
                {
                    bImplementaDigitalizacion = pParametros.GetValorBool("ImplementaDigitalizacionDeRecetas");
                    sImplementaDigitalizacion = bImplementaDigitalizacion.ToString();
                }
                return bImplementaDigitalizacion;
            }
            set { bImplementaDigitalizacion = value; }
        }

        public static bool ImplementaDigitalizacionDepurarDirectorio
        {
            get
            {
                if(sImplementaDigitalizacion == "")
                {
                    bImplementaDigitalizacion = pParametros.GetValorBool("ImplementaDigitalizacionDeRecetas");
                    sImplementaDigitalizacion = bImplementaDigitalizacion.ToString();

                    bImplementaDigitalizacionDepurar = pParametros.GetValorBool("ImplementaDigitalizacionDeRecetasDepurar");
                }
                return bImplementaDigitalizacionDepurar;
            }
            set { bImplementaDigitalizacionDepurar = value; }
        }
        #endregion Interface para Digitalizacion de Recetas

        #region Implementacion AEI ( H's ) 
        private static string sCapturaInformacionAEI = "";
        private static bool bCapturaInformacionAEI = false;

        public static bool ImplementaCapturaInformacionAEI
        {
            get
            {
                if(sCapturaInformacionAEI == "")
                {
                    bCapturaInformacionAEI = pParametros.GetValorBool("CapturaInformacionAEI");
                    sCapturaInformacionAEI = bCapturaInformacionAEI.ToString();
                }
                return bCapturaInformacionAEI;
            }
            set { bCapturaInformacionAEI = value; }
        }
        #endregion Implementacion AEI ( H's )

        #region Dispensación de controlados 
        private static string sDispensacionDeControlados = "";
        private static bool bDispensacionDeControlados = false;

        private static string sManejoDeControlados = "";
        private static bool bManejoDeControlados = false;

        public static bool DispensacionDeControladosPermitida
        {
            get
            {
                if(sDispensacionDeControlados == "")
                {
                    bDispensacionDeControlados = pParametros.GetValorBool("Controlados_DispensacionPermitida");
                    sDispensacionDeControlados = bDispensacionDeControlados.ToString();
                }
                return bDispensacionDeControlados;
            }
            set { bDispensacionDeControlados = value; }
        }

        public static bool ManejoDeControladosPermitido
        {
            get
            {
                if(sManejoDeControlados == "")
                {
                    bManejoDeControlados = pParametros.GetValorBool("Controlados_ManejoPermitido");
                    sManejoDeControlados = bManejoDeControlados.ToString();
                }
                return bManejoDeControlados;
            }
            set { bManejoDeControlados = value; }
        }
        #endregion Dispensación de controlados

        #region Transferencias Interestatales - Farmacias
        private static string sTransferencias_Interestatales__Farmacias = "";
        private static bool bTransferencias_Interestatales__Farmacias = false;


        private static string sTransferencias_Dias_ConfirmacionTransitos = "";
        private static int iTransferencias_Dias_ConfirmacionTransitos = 0;


        public static bool Transferencias_Interestatales__Farmacias
        {
            get
            {
                //////// Se habilitan las Transferencias Interestatales para todas las unidades 
                ////if (DtGeneral.ModuloMA_EnEjecucion == TipoModulo_MA.Farmacia || DtGeneral.ModuloMA_EnEjecucion == TipoModulo_MA.Almacen)
                {
                    if(sTransferencias_Interestatales__Farmacias == "")
                    {
                        bTransferencias_Interestatales__Farmacias = pParametros.GetValorBool("Transferencias_Interestatales__Farmacias");
                        sTransferencias_Interestatales__Farmacias = bTransferencias_Interestatales__Farmacias.ToString();
                    }
                }

                return bTransferencias_Interestatales__Farmacias;
            }

            set
            {
                //////// Se habilitan las Transferencias Interestatales para todas las unidades 
                ////if (DtGeneral.ModuloMA_EnEjecucion == TipoModulo_MA.Farmacia || DtGeneral.ModuloMA_EnEjecucion == TipoModulo_MA.Almacen)
                {
                    bTransferencias_Interestatales__Farmacias = value;
                    sTransferencias_Interestatales__Farmacias = bMostrarLotesSinExistencia.ToString();
                }
            }
        }

        public static int Transferencias_Dias_ConfirmacionTransitos
        {
            get
            {
                if(sTransferencias_Dias_ConfirmacionTransitos == "")
                {
                    iTransferencias_Dias_ConfirmacionTransitos = pParametros.GetValorInt("Transferencias_Dias_ConfirmacionTransitos");
                }

                validar__Transferencias_Dias_ConfirmacionTransitos();

                return iTransferencias_Dias_ConfirmacionTransitos;
            }

            set
            {
                iTransferencias_Dias_ConfirmacionTransitos = value;
                validar__Transferencias_Dias_ConfirmacionTransitos();
            }
        }

        private static void validar__Transferencias_Dias_ConfirmacionTransitos()
        {
            string s = "";

            if(iTransferencias_Dias_ConfirmacionTransitos < 7 || iTransferencias_Dias_ConfirmacionTransitos > 30)
            {
                iTransferencias_Dias_ConfirmacionTransitos = 7;
            }


            sTransferencias_Dias_ConfirmacionTransitos = iTransferencias_Dias_ConfirmacionTransitos.ToString();

        }
        #endregion Transferencias Interestatales - Farmacias

        #region Vigencia de Dispensacion 
        private static string sDispensacion_ImplementaExpiracion = "";
        //private static bool bDispensacionActiva = true;


        private static bool bDispensacion_ImplementaExpiracion = false;
        private static string sDispensacion_FechaExpiracion_Venta = "";
        private static string sDispensacion_FechaExpiracion_Consigna = "";
        private static bool bDispensacionActiva_Venta = true;
        private static bool bDispensacionActiva_Consigna = true;

        private static int iFechaSistema = 0;
        private static int iFechaExpiracion_Dispensacion_Venta = 0;
        private static int iFechaExpiracion_Dispensacion_Consigna = 0;

        private static string sDispensacion_Mensaje = "";

        public static bool DispensacionActiva_Verificar()
        {
            bool bRegresa = true;
            bool bDatos_Venta = DispensacionActiva_Venta;
            bool bDatos_Consigna = DispensacionActiva_Consigna;

            sDispensacion_Mensaje = ""; 
            if(bDispensacion_ImplementaExpiracion)
            {
                bRegresa = bDispensacionActiva_Venta && bDispensacionActiva_Consigna;

                if(!bRegresa)
                {
                    //General.msjAviso("La dispensación de Venta esta deshabilitada, sólo es posible dispensar Consignación.");
                    if(!bDispensacionActiva_Venta)
                    {
                        sDispensacion_Mensaje += string.Format("La dispensación de Venta está deshabilitada.\n"); 
                    }

                    if(!bDispensacionActiva_Consigna)
                    {
                        sDispensacion_Mensaje += string.Format("La dispensación de Consignación está deshabilitada.\n");
                    }

                    if(sDispensacion_Mensaje != "")
                    {
                        sDispensacion_Mensaje += "\n";
                    }
                }
            }

            return bRegresa; 
        }

        public static string DispensacionActiva_Mensaje()
        {
            string sRegresa = sDispensacion_Mensaje;

            return sRegresa;  
        }

        public static bool DispensacionActiva_Venta
        {
            get
            {
                DispensacionActiva_Validar();

                bDispensacionActiva_Venta = true;
                if(bDispensacion_ImplementaExpiracion)
                {
                    iFechaSistema = Convert.ToInt32("0" + General.FechaYMD(General.FechaSistemaObtener(), ""));
                    iFechaExpiracion_Dispensacion_Venta = Convert.ToInt32("0" + sDispensacion_FechaExpiracion_Venta);


                    if(iFechaSistema >= iFechaExpiracion_Dispensacion_Venta)
                    {
                        bDispensacionActiva_Venta = false;
                    }
                }

                return bDispensacionActiva_Venta;
            }
            set { bDispensacionActiva_Venta = value; }
        }

        public static bool DispensacionActiva_Consigna
        {
            get
            {
                DispensacionActiva_Validar();


                bDispensacionActiva_Consigna = true;
                if(bDispensacion_ImplementaExpiracion)
                {
                    iFechaSistema = Convert.ToInt32("0" + General.FechaYMD(General.FechaSistemaObtener(), ""));
                    iFechaExpiracion_Dispensacion_Consigna = Convert.ToInt32("0" + iFechaExpiracion_Dispensacion_Consigna);

                    if(iFechaSistema >= iFechaExpiracion_Dispensacion_Consigna)
                    {
                        bDispensacionActiva_Consigna = false;
                    }
                }

                return bDispensacionActiva_Consigna;
            }
            set { bDispensacionActiva_Consigna = value; }
        }

        private static void DispensacionActiva_Validar()
        {
            if(sDispensacion_ImplementaExpiracion == "")
            {
                bDispensacion_ImplementaExpiracion = pParametros.GetValorBool("Dispensacion_ImplementaExpiracion");
                sDispensacion_FechaExpiracion_Venta = pParametros.GetValor("Dispensacion_FechaExpiracion_Venta");
                sDispensacion_FechaExpiracion_Consigna = pParametros.GetValor("Dispensacion_FechaExpiracion_Consigna");
                

                //bDispensacionActiva = !bDispensacion_ImplementaExpiracion;

                try
                {
                    sDispensacion_FechaExpiracion_Venta = General.FechaYMD(Convert.ToDateTime(sDispensacion_FechaExpiracion_Venta), "");
                    iFechaExpiracion_Dispensacion_Venta = Convert.ToInt32("0" + sDispensacion_FechaExpiracion_Venta);
                }
                catch
                { }

                try
                {
                    sDispensacion_FechaExpiracion_Consigna = General.FechaYMD(Convert.ToDateTime(sDispensacion_FechaExpiracion_Consigna), "");
                    iFechaExpiracion_Dispensacion_Consigna = Convert.ToInt32("0" + sDispensacion_FechaExpiracion_Consigna);
                }
                catch
                { }

                sDispensacion_ImplementaExpiracion = bDispensacion_ImplementaExpiracion.ToString();
            }
        }
        #endregion Vigencia de Dispensacion 

        #region Sesion
        private static int iSesion_MinRefresco = 0;
        private static int iSesion_MinDesconexion = 0 ;
        public static int iSesion_DiasInactivo = 0;
        private static bool bSesion_MultiplesConexiones = false;

        public static int Sesion_MinRefresco
        {
            get
            {
                validar__Sesion();

                return iSesion_MinRefresco;
            }
            set { iSesion_MinRefresco = value; }
        }

        public static int Sesion_MinDesconexion
        {
            get
            {
                validar__Sesion();

                return iSesion_MinDesconexion;
            }
            set { iSesion_MinDesconexion = value; }
        }

        public static int Sesion_DiasInactivo
        {
            get
            {
                validar__Sesion();

                return iSesion_DiasInactivo;
            }
            set { iSesion_DiasInactivo = value; }
        }

        public static bool Sesion_MultiplesConexiones
        {
            get
            {
                validar__Sesion();

                return bSesion_MultiplesConexiones;
            }
            set { bSesion_MultiplesConexiones = value; }
        }

        private static void validar__Sesion()
        {
            if (iSesion_MinRefresco == 0)
            {
                iSesion_MinRefresco = pParametros.GetValorInt("Sesion_MinRefresco");
                iSesion_MinDesconexion = pParametros.GetValorInt("Sesion_MinDesconexion");
                iSesion_DiasInactivo = pParametros.GetValorInt("Sesion_DiasInactivo");
                bSesion_MultiplesConexiones = pParametros.GetValorBool("Sesion_MultiplesConexiones");

                if (iSesion_MinRefresco < 1 || iSesion_MinRefresco > 4)
                {
                    iSesion_MinRefresco = 2;
                }

                if (iSesion_MinDesconexion < 5 || iSesion_MinDesconexion > 20)
                {
                    iSesion_MinDesconexion = 7;
                }

                if (iSesion_DiasInactivo < 15 || iSesion_DiasInactivo > 30)
                {
                    iSesion_DiasInactivo = 20;
                }

            }
        }


        #endregion Sesion

        #region Servidores
        public static bool UnidadConServidorDedicado
        {
            get
            {
                bUnidadConServidorDedicado = pParametros.GetValorBool("EsServidorDedicado");
                return bUnidadConServidorDedicado;
            }
            set { bUnidadConServidorDedicado = value; }
        }
        #endregion Servidores

        #region Funciones y Procedimientos IMach4
        #region IMach4
        public static Color ColorProductosIMach
        {
            get { return colorProductosIMach; }
            set { colorProductosIMach = value; }
        }

        public static void VerificarInterface()
        {
            ///// REVISAR 
            DllRobotDispensador.RobotDispensador.Robot.EmpresaConectada = DtGeneral.EmpresaConectada;
            DllRobotDispensador.RobotDispensador.Robot.EmpresaConectadaNombre = DtGeneral.EmpresaConectadaNombre;
            DllRobotDispensador.RobotDispensador.Robot.EstadoConectado = DtGeneral.EstadoConectado;
            DllRobotDispensador.RobotDispensador.Robot.EstadoConectadoNombre = DtGeneral.EstadoConectadoNombre;
            DllRobotDispensador.RobotDispensador.Robot.FarmaciaConectada = DtGeneral.FarmaciaConectada;
            DllRobotDispensador.RobotDispensador.Robot.FarmaciaConectadaNombre = DtGeneral.FarmaciaConectadaNombre;

            DllRobotDispensador.RobotDispensador.Robot.IdPersonal = DtGeneral.IdPersonal;
            DllRobotDispensador.RobotDispensador.Robot.NombrePersonal = DtGeneral.NombrePersonal;
            DllRobotDispensador.RobotDispensador.Robot.LoginUsuario = DtGeneral.LoginUsuario;
            DllRobotDispensador.RobotDispensador.Robot.PasswordUsuario = DtGeneral.PasswordUsuario;

            DllRobotDispensador.RobotDispensador.Robot.ObtenerConfiguracion(true); 
            DllRobotDispensador.RobotDispensador.Robot.ValidarPuntoDeVenta();
        }
        #endregion IMach4

        public static void ValidarCodigoIMach4(clsGrid Grid, bool EsIMach4, int Renglon)
        {
            if (EsIMach4)
            {
                Grid.ColorRenglon(Renglon, colorProductosIMach);
            }
        }
        #endregion Funciones y Procedimientos IMach4

        #region Funciones y Procedimientos Publico
        public static bool EsServidorDeRedLocal
        {
            get { return bEsServidorLocal; }
        }

        public static bool EsServidorLocal()
        {
            clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            clsLeer leer = new clsLeer(ref cnn);
            string sListaDeMAC_Address = General.ListaDeMacAddress("'");

            string sSql = string.Format(" Select * " +
                " From CFGC_Terminales Where replace(MAC_Address, '-', '') in ( {0} )  and EsServidor = 1 ", sListaDeMAC_Address);

            //clsGrabarError.LogFileError(General.NombreEquipo);
            //clsGrabarError.LogFileError(sListaDeMAC_Address);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "EsServidorLocal()");
                General.msjError("Ocurrió un error al validar el Equipo.");
            }
            else
            {
                bEsServidorLocal = leer.Leer();
            }

            return bEsServidorLocal;
        }

        public static bool ExisteMAC_Servidor()
        {
            clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            clsLeer leer = new clsLeer(ref cnn);
            string sListaDeMAC_Address = General.ListaDeMacAddress("'");
            string sSql = string.Format(" Select * " +
                " From CFGC_Terminales Where EsServidor = 1 " );

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ExisteMAC_Servidor()");
                General.msjError("Ocurrió un error al validar el Servidor de la Red.");
            }
            else
            {
                // bExisteMAC_Servidor = leer.Leer();
                while (leer.Leer())
                {
                    clsPing ping = new clsPing();
                    if (ping.Ping(General.DatosConexion.ServidorPing))
                    {
                        bExisteMAC_Servidor = true;
                        break;
                    }
                    else
                    {
                        ping = new clsPing(); 
                        if (ping.Ping(leer.Campo("Nombre"))) 
                        {
                            bExisteMAC_Servidor = true;
                            break;
                        }
                    }
                }
            }

            return bExisteMAC_Servidor;
        }


        public static bool EjecutarServicioCliente
        {
            get
            {
                return (!bEsEquipoDeDesarrollo && bExisteServicio && bEsServidorLocal);
            }
        }

        public static bool RevisarServicioDeInformacion()
        {
            bool bRegresa = false;

            if (!bEsEquipoDeDesarrollo)
            {
                if (bExisteServicio)
                {
                    if (bEsServidorLocal)
                    {
                        bExisteServicio = File.Exists(sRutaServicio);
                        if (!General.ProcesoEnEjecucionUnica(sModuloTransferencia))
                        {
                            Process svr = new Process();
                            svr.StartInfo.FileName = Application.StartupPath + @"\\" + sModuloTransferencia;
                            svr.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                            svr.Start();
                            System.Threading.Thread.Sleep(500);
                            bRegresa = true;
                        }
                    }
                }
            }
            return bRegresa;
        }

        public static bool ValidaProcesoFarmacia(string[] FormasValidas)
        {
            bool bRegresa = true;

            foreach (Form f in Application.OpenForms)
            {
                if (f.Name != "")
                {
                    bRegresa = false;
                    foreach (string sF in FormasValidas)
                    {
                        if (sF.ToUpper() == f.Name.ToUpper())
                        {
                            bRegresa = true;
                            break;
                        }
                    }
                }
            }

            return bRegresa;
        }

        public static bool UsuarioConSesionCerrada()
        {
            return UsuarioConSesionCerrada("", false, false);
        }

        public static bool UsuarioConSesionCerrada(bool CerrarModulo)
        {
            return UsuarioConSesionCerrada("El usuario actual ya cerró sesión en otra terminal, el módulo se cerrará.", true, CerrarModulo);
        }

        public static bool UsuarioConSesionCerrada(string Mensaje, bool MostrarMsj, bool CerrarModulo)
        {
            bool bRegresa = false;

            if(sEsModulo_Operativo == "")
            {
                TipoModulo x = DtGeneral.ModuloEnEjecucion;
                bEsModulo_Operativo = x == TipoModulo.Almacen || x == TipoModulo.AlmacenUnidosis || x == TipoModulo.Farmacia || x == TipoModulo.FarmaciaUnidosis;
                sEsModulo_Operativo = bEsModulo_Operativo.ToString();
            }

            if ( bValidarSesionUsuario && bEsModulo_Operativo )
            {

                clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
                clsLeer leer = new clsLeer(ref cnn);


                string sSql = string.Format(
                    "Select Top 1 Status \n" +
                    "From CtlCortesParciales (NoLock) \n" +
                    "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' \n" +
                    "\tAnd Convert( varchar(10), FechaSistema, 120 ) = '{3}' \n" +
                    "\tAnd IdPersonal = '{4}' and Status = 'C' \n" +
                    "Order by FechaCierre Desc \n",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                    General.FechaYMD(GnFarmacia.FechaOperacionSistema, "-"), DtGeneral.IdPersonal);

                if (!leer.Exec(sSql))
                {
                }
                else
                {
                    bRegresa = leer.Leer();
                }

                if (bRegresa)
                {
                    if (MostrarMsj)
                    {
                        General.msjUser(Mensaje);
                    }

                    //if (CerrarModulo)
                    //{
                    //    // Forzar el cierre de la aplicación 
                    //    Application.Exit(); 
                    //}
                }
            }

            return bRegresa;
        }
        //------------------
        public static bool DiasCiclicos()
        {
            return DiasCiclicos("", false, false);
        }

        public static bool DiasCiclicos(bool CerrarModulo)
        {
            return DiasCiclicos("El inventario ciclico no cumple con los parámetros establecidos, el módulo se cerrará.", true, CerrarModulo);
        }

        public static bool DiasCiclicos(string Mensaje, bool MostrarMsj, bool CerrarModulo)
        {
            bool bRegresa = false;

            if(sEsModulo_Operativo == "")
            {
                TipoModulo x = DtGeneral.ModuloEnEjecucion;
                bEsModulo_Operativo = x == TipoModulo.Almacen || x == TipoModulo.AlmacenUnidosis || x == TipoModulo.Farmacia || x == TipoModulo.FarmaciaUnidosis;
                sEsModulo_Operativo = bEsModulo_Operativo.ToString();
            }

            if (bEsModulo_Operativo)
            {

                clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
                clsLeer leer = new clsLeer(ref cnn);


                string sSql = string.Format(
                    "Exec spp_Mtto_Inv_Ciclicos_Verificar  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdPersonal = '{3}' ",
                                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal );

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "DiasCiclicos");
                }
                else
                {
                    bRegresa = leer.Leer();
                }

                if (bRegresa)
                {
                    if (MostrarMsj)
                    {
                        General.msjUser(Mensaje);
                    }

                    //if (CerrarModulo)
                    //{
                    //    // Forzar el cierre de la aplicación 
                    //    Application.Exit(); 
                    //}
                }
            }

            return bRegresa;
        }

        /// <summary>
        /// Oculta las columnas referentes a Costos, Precios e Importes del Grid indicado.
        /// Si el usuario Conectado es Administrador, se mostraran todas las columnas.
        /// Si el usuario Conectado tiene el Privilegio de ver los Precios y Costos se muestran todas las columnas.
        /// </summary>
        /// <param name="Grid">Grid al cual se desea oculpar las columnas de precio</param>
        /// <param name="CostoPrecio">Columna que representa el Costo ó Precio</param>
        /// <param name="Importe">Columa que representa el Importe</param>
        /// <param name="Descripcion">Columna que representa la Descripcion</param>
        public static void AjustaColumnasImportes(FpSpread Grid, int CostoPrecio, int Importe, int Descripcion)
        {
            int iAnchoColDescripcion = 0;

            // Determinar si se Muestran las Columas de Precios 
            if (!DtGeneral.EsAdministrador)
            {
                if (!GnFarmacia.MostrarPrecios_y_Costos)
                {
                    iAnchoColDescripcion = (int)Grid.Sheets[0].Columns[CostoPrecio - 1].Width;
                    iAnchoColDescripcion += (int)Grid.Sheets[0].Columns[Importe - 1].Width;

                    Grid.Sheets[0].Columns[CostoPrecio - 1].Visible = false;
                    Grid.Sheets[0].Columns[Importe - 1].Visible = false;

                    Grid.Sheets[0].Columns[Descripcion - 1].Width += (float)iAnchoColDescripcion;
                }
            }
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados
        private static void ObtenerTipoDeCambio()
        {
            dTipoDeCambio = 0;
            clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            clsLeer leer = new clsLeer(ref cnn);

            string sSql = "Select * From Net_CFGC_TipoCambio (NoLock) ";
            if (!leer.Exec(sSql))
            {
                General.msjError("Ocurrió un error al obtener el Tipo de Cambio.");
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjAviso("El Tipo de Cambio de Dollar no esta configurado.\nNo sera posible recibir pago en Dolares.\n\nReportarlo al Departamento de Sistemas.");
                }
                else
                {
                    dTipoDeCambio = leer.CampoDouble("TipoDeCambio");
                }
            }
        }
        #endregion Funciones y Procedimientos Privados

    }
}
