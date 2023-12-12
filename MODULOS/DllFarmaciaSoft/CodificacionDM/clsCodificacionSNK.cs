using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

namespace DllFarmaciaSoft
{
    public class ItemCodificacion
    {
        basGenerales Fg = new basGenerales(); 
        string sUUID = "";

        public string IdEmpresa_Local = "";
        public string IdEstado_Local = "";
        public string IdFarmacia_Local = "";


        public string IdEmpresa = "";
        public string Empresa = "";
        public string IdEstado = "";
        public string Estado = "";
        public string IdFarmacia = "";
        public string Farmacia = "";
        public string Codificadora = "";
        public int Multiplo = 0;
        public string IdProducto = "";
        public string CodigoEAN = "";
        public string DescripcionComercial = "";
        public string ClaveSSA = "";
        public string DescripcionClave = "";
        public string IdLaboratorio = "";
        public string Laboratorio = "";
        public string IdSubFarmacia = "";
        public string SubFarmacia = "";
        public bool EsConsignacion = false;
        public string ClaveLote = "";
        private string sCaducidad = "";
        private string sCaducidad_Formato = "";
        public string FechaEmpaque = "";
        public string GlobalTrade = "";
        public string NumeroDeItems = "";
        private string sClaveLote_SubFarmacia = "";
        private string sSeparadorDig = "|";

        public string Año = "";
        public string Mes = "";
        public string Dia = "";
        public string Hora = "";
        public string Minuto = "";
        public string Segundo = "";
        public string Consecutivo = "";
        public string IdProveedor = "";
        public string Proveedor = "";
        public string NumeroDeFactura = "";


        bool bUUID_Valido = false; 
        bool bExiste_UUID = false;
        bool bExistenciaUbicacion = true;
        string sResultado = "";
        Color colorResultado = Color.Black;

        #region Constructor de Clase 
        public ItemCodificacion():this("") 
        { 
        }

        public ItemCodificacion(string UUID)
        {
            sUUID = UUID; 
        }
        #endregion Constructor de Clase

        #region Propiedades
        public string UUID
        {
            get { return sUUID; }
        }

        public bool Existe_UUID
        {
            get { return bExiste_UUID; }
            set { bExiste_UUID = value; }
        }

        public bool UUID_Valido
        {
            get { return bUUID_Valido; }
            set { bUUID_Valido = value; }
        }

        public bool ExistenciaUbicacion
        {
            get { return bExistenciaUbicacion; }
            set { bExistenciaUbicacion = value; }
        }

        public string Resultado
        {
            get 
            {
                sResultado = "UUID no registrado";
                if (bExiste_UUID)
                {
                    sResultado = "UUID registrado";

                    if (!bUUID_Valido)
                    {
                        sResultado = "UUID invalido";
                    }

                    if (!bExistenciaUbicacion)
                    {
                        sResultado = "Existencia insuficiente en ubicación.";
                    }

                    if (GnFarmacia.ImplementaReaderDM)
                    {
                        sResultado = "UUID decodificado";
                    }
                }

                return sResultado;
            }
        }

        public Color ColorResultado
        {
            get
            {
                colorResultado = Color.Red;
                if (bExiste_UUID)
                {
                    colorResultado = Color.Black;
                    if (!bUUID_Valido)
                    {
                        colorResultado = Color.Red; 
                    }

                    if (GnFarmacia.ImplementaReaderDM)
                    {
                        colorResultado = Color.Black;
                    }
                }

                return colorResultado;
            }
        }
        #endregion Propiedades 

        #region Propiedades extendidas
        public string Caducidad
        {
            get { return sCaducidad; }
            set 
            {
                sCaducidad = value;
                sCaducidad_Formato = Fg.PonCeros(value, 4);
                sCaducidad_Formato = string.Format("AÑO {0} - MES {1}", Fg.Mid(sCaducidad_Formato, 1, 2), Fg.Mid(sCaducidad_Formato, 3, 2));
            }
        }

        public string Caducidad_Formato
        {
            get { return sCaducidad_Formato; }
        }

        public string ClaveLote_SubFarmacia
        {
            get 
            {
                string sRegresa = EsConsignacion ? "*" + ClaveLote : ClaveLote;

                return sRegresa;
            }
        }
        #endregion Propiedades extendidas

        #region Funciones y Procedimientos Publicos
        public string Get_UUID()
        {
            string sRegresa = "";
            //basGenerales Fg = new basGenerales(); 

            ////item_QR.IdEmpresa = DtGeneral.EmpresaConectada;
            ////item_QR.IdEstado = DtGeneral.EstadoConectado;
            ////item_QR.IdFarmacia = DtGeneral.FarmaciaConectada;
            ////item_QR.Codificadora = "00";
            ////item_QR.CodigoEAN = txtCodigo.Text;
            ////item_QR.Multiplo = 1;
            ////item_QR.IdSubFarmacia = cboLotes.ItemActual.GetItem("IdSubFarmacia");
            ////item_QR.ClaveLote = cboLotes.Data;

            ////item_QR.Caducidad = "0000"; // Fg.Mid(sLista, 1, 4);
            ////item_QR.Año = Fg.PonCeros(dtMarcaDeTiempo.Year, 4);
            ////item_QR.Mes = Fg.PonCeros(dtMarcaDeTiempo.Month, 2);
            ////item_QR.Dia = Fg.PonCeros(dtMarcaDeTiempo.Day, 2);
            ////item_QR.Hora = Fg.PonCeros(dtMarcaDeTiempo.Hour, 2);
            ////item_QR.Minuto = Fg.PonCeros(dtMarcaDeTiempo.Minute, 2);
            ////item_QR.Segundo = Fg.PonCeros(dtMarcaDeTiempo.Second, 2);
            ////item_QR.Consecutivo = Fg.PonCeros(dtMarcaDeTiempo.Millisecond, 2);
            ////item_QR.IdProveedor = "0000";
            ////item_QR.NumeroDeFactura = "0000";

            //clsDataMatrix dm = new clsDataMatrix();
            //dm.CodificarImagen("00209001101007503006916038001*01142127*161015102712051001O100", @"D:\Img_DM_OC.jpeg");


            sRegresa += string.Format("{0}{1}{2}{3}{4}{5}*", 
                Fg.PonCeros(IdEmpresa, 3), Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdFarmacia, 4), Fg.PonCeros(Codificadora, 2),
                Fg.PonCeros(CodigoEAN, 15), Fg.PonCeros(Multiplo, 3));  
                    
            sRegresa += string.Format("{0}{1}*", Fg.PonCeros(IdSubFarmacia,2), ClaveLote );

            sRegresa += string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", 
                Fg.PonCeros(Caducidad, 4), Fg.PonCeros(Año, 2), Fg.PonCeros(Mes, 2), Fg.PonCeros(Dia, 2), 
                Fg.PonCeros(Hora, 2), Fg.PonCeros(Minuto, 2), Fg.PonCeros(Segundo, 2), Fg.PonCeros(Consecutivo, 2));
            sRegresa += string.Format("{0}{1}", Fg.PonCeros(IdProveedor, 4), Fg.PonCeros(NumeroDeFactura, 20) );

            return sRegresa; 

        }
        #endregion Funciones y Procedimientos Publicos
    }

    public static class CodificacionSNK
    {

        #region Declaracion de variables
        private static string sIdEmpresa = DtGeneral.EmpresaConectada;
        private static string sIdEstado = DtGeneral.EstadoConectado;
        private static string sIdFarmacia = DtGeneral.FarmaciaConectada;

        private static clsConexionSQL cnn;
        private static clsLeer leer;
        private static clsLeer leerGuardar;
        private static ItemCodificacion itemGuardado; 

        private static clsGrabarError Error;
        private static basGenerales Fg = new basGenerales();
        private static string sUltimoMensaje = ""; 
        #endregion Declaracion de variables

        #region Constructor y Destructor de Clase 
        static CodificacionSNK()
        {
            cnn = new clsConexionSQL(General.DatosConexion);
            leer = new clsLeer(ref cnn);
            leerGuardar = new clsLeer(ref cnn);
            itemGuardado = new ItemCodificacion(); 

            Error = new clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, "CodificacionSNK"); 
        }
        #endregion Constructor y Destructor de Clase

        #region Propiedades 
        public static ItemCodificacion UltimoItemGuardado
        {
            get { return itemGuardado; }
        }

        public static string UltimoMensaje
        {
            get { return sUltimoMensaje; }
        }
        #endregion Propiedades 

        #region Funciones y Procedimientos Publicos
        public static ItemCodificacion Decodificar_Segmentos(string UUID)
        {
            return Decodificar_Segmentos(UUID, false); 
        }

        public static ItemCodificacion Decodificar_Segmentos(string UUID, bool InformacionRapidaProducto)
        {
            int i;
            ItemCodificacion item = new ItemCodificacion(UUID);

            if (int.TryParse(UUID.Substring(0, 1), out i))
            {
                if (UUID.Contains("("))
                {
                    UUID = UUID.Replace("(", "*");
                    ////item = new ItemCodificacion(UUID);
                }


                item = Decodificar_SegmentosInter(UUID, InformacionRapidaProducto);
            }
            else
            {
                item = Decodificar_SegmentosDig(UUID, InformacionRapidaProducto);
            }


            return item;
        }

        private static ItemCodificacion Decodificar_SegmentosDig(string UUID, bool InformacionRapidaProducto)
        {
            ItemCodificacion item = new ItemCodificacion(UUID);
            string[] lista = null;

            try
            {
                UUID = UUID.Remove(0, 4);
                Decodificar_SegmentosDig(ref item, UUID);
            }
            catch { }

            //if (InformacionRapidaProducto)
            //{
            //    GetInformacionRapidaProducto(ref item);
            //    GetInformacionSubFarmacia(ref item);
            //    GetInformacionUUID(ref item, true);
            //}

            return item;
        }

        private static void Decodificar_SegmentosDig(ref ItemCodificacion Item, string UUID)
        {
            string sValor = "", sValorT = "", sClaveSSARegSan;
            bool bContinua = true;
            //int iValorChar = 0;
            

            while (UUID.Length > 0 && bContinua)
            {
                sValorT = "";
                sClaveSSARegSan = "";
                sValor = UUID.Trim().Substring(0, 2);

                //iValorChar = Encoding.ASCII.GetBytes(sValor.Substring(0, 1))[0];


                switch (sValor)
                {
                    case "01":
                        UUID = UUID.Remove(0, 2);
                        Item.GlobalTrade = UUID.Substring(0, 14);
                        UUID = UUID.Remove(0, 14);
                        break;

                    case "13":
                        UUID = UUID.Remove(0, 2);
                        Item.FechaEmpaque = UUID.Substring(0, 6);
                        UUID = UUID.Remove(0, 6);
                        break;

                    case "17":
                        UUID = UUID.Remove(0, 2);
                        Item.Caducidad = UUID.Substring(0, 6);
                        UUID = UUID.Remove(0, 6);
                        break;

                    case "10":
                        Item.ClaveLote = CadenaConSeparador(2, ref Item, ref UUID);
                        Item.ClaveLote = LimpiarCadena(Item.ClaveLote);
                        break;

                    case "30":
                        Item.NumeroDeItems = CadenaConSeparador(2, ref Item, ref UUID);
                        break;

                    case "91":
                        Item.DescripcionClave = CadenaConSeparador(2, ref Item, ref UUID);
                        break;

                    case "92":
                    case "93":
                        UUID = UUID.Remove(0, 2);
                        sValorT = UUID.Substring(0, 1);
                        while (UUID.Length > 0 && Encoding.ASCII.GetBytes(sValorT)[0] != 29)
                        {
                            UUID = UUID.Remove(0, 1);
                        }
                        break;

                    case "21":
                        UUID = UUID.Remove(0, 2);
                        Item.FechaEmpaque = UUID.Substring(0, 9);
                        UUID = UUID.Remove(0, 9);
                        break;

                    default:
                        sValor = UUID.Substring(0, 3);
                        if ("241" == sValor)
                        {
                            sClaveSSARegSan = CadenaConSeparador(3, ref Item, ref UUID);

                            while (sClaveSSARegSan.Length > 0 && ((sValorT != "_") && (sValorT != "/") && (sValorT != "?") && (sValorT != "-")))
                            {
                                sValorT = sClaveSSARegSan.Substring(0, 1);
                                if ((sValorT != "_") && (sValorT != "/") && (sValorT != "?") && (sValorT != "-"))
                                {
                                    Item.ClaveSSA += sValorT;
                                }
                                sClaveSSARegSan = sClaveSSARegSan.Remove(0, 1);
                            }
                        }
                        else
                        {
                            bContinua = false;
                        }
                        break;
                }
            }

        }


        private static string CadenaConSeparador(int NumCaracterLlave,ref ItemCodificacion Item, ref string UUID)
        {
            string sValorT = "", sRegresa = "";
            sValorT = UUID.Substring(0, 1);

            UUID = UUID.Remove(0, NumCaracterLlave);
            while (UUID.Length > 0 && Encoding.ASCII.GetBytes(sValorT)[0] != 29)
            {
                sValorT = UUID.Substring(0, 1);
                if (Encoding.ASCII.GetBytes(sValorT)[0] != 29)
                {
                    sRegresa += sValorT;
                }
                UUID = UUID.Remove(0, 1);
            }
            return sRegresa;
        }

        static string LimpiarCadena(string Valor)
        {
            // Replace invalid characters with empty strings.
            try
            {
                return Regex.Replace(Valor, @"[^0-9A-Za-z]", "", RegexOptions.None); 
            }
            // If we timeout when replacing invalid characters, 
            // we should return Empty.
            catch
            {
                return Valor;
            }
        }


        private static ItemCodificacion Decodificar_SegmentosInter(string UUID, bool InformacionRapidaProducto)
        {
            ItemCodificacion item = new ItemCodificacion(UUID);
            string[] lista = null;
            string sItem_01 = "";
            string sItem_02 = "";
            string sItem_03 = "";
            string sSeparador = ""; 

            try
            {
                sSeparador = Fg.Mid(UUID, 30, 1); 
                lista = UUID.Split(Convert.ToChar(sSeparador));
                Decodificar_Segmento_01(ref item, lista);
                Decodificar_Segmento_02(ref item, lista);
                Decodificar_Segmento_03(ref item, lista);

                ////sItem_01 = lista[0];
                ////sItem_02 = lista[1];
                ////sItem_03 = lista[2];

            }
            catch { }

            if (InformacionRapidaProducto)
            {
                GetInformacionRapidaProducto(ref item);
                GetInformacionSubFarmacia(ref item);
                GetInformacionUUID(ref item, true); 
            }

            return item;
        }

        public static ItemCodificacion Decodificar(string UUID)
        {
            ItemCodificacion item = Decodificar_Segmentos(UUID);

            GetInformacionEmpresa(ref item);
            GetInformacionEstadoFarmacia(ref item);
            GetInformacionProducto(ref item);
            GetInformacionSubFarmacia(ref item); 
            GetInformacionProveedor(ref item);
            GetInformacionUUID(ref item); 

            return item; 
        }

        public static bool Registrar_UUID(clsLeer Leer, clsLotes Lotes)
        {
            bool bRegresa = Leer.Conexion.TransaccionActiva;

            if (bRegresa)
            {
                bRegresa = local__Registrar_UUID(Leer, Lotes);
            }

            return bRegresa;
        }

        public static bool local__Registrar_UUID(clsLeer Leer, clsLotes Lotes)
        {
            bool bRegresa = true;
            clsLeer leer_UUIDS = new clsLeer();

            leer_UUIDS.DataSetClase = Lotes.UUID_List.DataSetClase;
            bRegresa = leer_UUIDS.Registros > 0;

            while (leer_UUIDS.Leer() && bRegresa)
            {
                if (!Registrar_UUID(Leer, leer_UUIDS.Campo("UUID")))
                {
                    bRegresa = false;
                }
            }

            return bRegresa;
        }

        public static bool Registrar_UUID(string UUID)
        {
            return Registrar_UUID(leerGuardar, UUID);
        }


        public static bool Registrar_UUID(clsLeer leerGuardar, string UUID)
        {
            bool bRegresa = false;
            string sSql = "";
            ItemCodificacion item = Decodificar_Segmentos(UUID);

            //// obtener la informacion del CodigoEAN 
            GetInformacionRapidaProducto(ref item);

            item.IdEmpresa_Local = DtGeneral.EmpresaConectada;
            item.IdEstado_Local = DtGeneral.EstadoConectado;
            item.IdFarmacia_Local = DtGeneral.FarmaciaConectada;

            sSql = string.Format("Exec spp_Mtto_FarmaciaProductos_UUID @UUID = '{0}', @IdEmpresa = '{1}', @IdEstado = '{2}', @IdFarmacia = '{3}', @IdSubFarmacia = '{4}', " +
                " @IdProducto = '{5}', @CodigoEAN = '{6}', @ClaveLote = '{7}', @IdCodificador = '{0}'  ", 
                item.UUID, item.IdEmpresa_Local, item.IdEstado_Local, item.IdFarmacia_Local, item.IdSubFarmacia,
                item.IdProducto, item.CodigoEAN, item.ClaveLote, (item.IdEstado + item.IdFarmacia)); 

            if (!leerGuardar.Exec(sSql)) 
            {
                sUltimoMensaje = "Ocurrió un error al registrar el UUID";
                Error.GrabarError(leerGuardar, "Registrar_UUID()"); 
            }
            else
            {
                leerGuardar.Leer();
                bRegresa = true;

                leerGuardar.CampoBool("Resultado");
                sUltimoMensaje = leerGuardar.Campo("Mensaje");

                item.Existe_UUID = true;
                item.UUID_Valido = true; 
                itemGuardado = item; 
            }

            return bRegresa; 
        }
        #endregion Funciones y Procedimientos Publicos

        #region Interface de guardado de informacion 
        /// <summary>
        /// Registra los UUIDS capturados.
        /// </summary>
        /// <param name="Folio">Folio de Movimiento del Sistema</param>
        /// <param name="Leer">Clase transaccional para el guardado de información</param>
        /// <param name="Lotes">Contenedor de UUIDS a registrar</param>
        /// <param name="Validar_UUID">Validar que el UUID este disponible para su uso</param>
        /// <param name="EsSalida">Determina si es un movimiento de Entrada ó Salida</param>
        /// <returns></returns>
        public static bool Guardar_UUIDS_Movimientos_De_Inventario(string Folio, clsLeer Leer, clsLotes Lotes, bool Validar_UUID, bool EsSalida)
        {
            bool bRegresa = Leer.Conexion.TransaccionActiva;

            if (bRegresa)
            {
                bRegresa = local__Guardar_UUIDS_Movimientos_De_Inventario(Folio, Leer, Lotes, Validar_UUID, EsSalida);
            }

            return bRegresa; 
        }

        private static bool local__Guardar_UUIDS_Movimientos_De_Inventario(string Folio, clsLeer Leer, clsLotes Lotes, bool Validar_UUID, bool EsSalida)
        {
            bool bRegresa = true;
            string sSql = "";
            clsLeer leer_UUIDS = new clsLeer();
            int iValidar = Validar_UUID ? 1 : 0;
            int iEsSalida = EsSalida ? 1 : 0;

            leer_UUIDS.DataSetClase = Lotes.UUID_List.DataSetClase;
            bRegresa = leer_UUIDS.Registros > 0; 

            while (leer_UUIDS.Leer())
            {
                sSql = string.Format("Exec spp_Mtto_MovtosInv_Detalles_UUID \n" +
                    "\t@UUID = '{0}', @IdEmpresa = '{1}', @IdEstado = '{2}', @IdFarmacia = '{3}', @FolioMovtoInv = '{4}', \n" +
                    "\t@ValidarUUID = '{5}', @TipoDeProceso = '{6}' ",
                    leer_UUIDS.Campo("UUID"), DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, Folio,
                    iValidar, iEsSalida);
                if (!Leer.Exec(sSql))
                {
                    bRegresa = false;
                    break;
                }
            }


            return bRegresa;
        }
        #endregion Interface de guardado de informacion 

        #region Funciones y Procedimientos Privados
        private static void Decodificar_Segmento_01(ref ItemCodificacion Item, string []Elementos)
        {
            string sLista = "";
            bool bExistenDatos = false; 

            try
            {
                // 00209000201007503001007663001
                sLista = Elementos[0];
                bExistenDatos = true; 
            }
            catch { }

            if (bExistenDatos)
            {
                Item.IdEmpresa = Fg.Mid(sLista, 1, 3);
                Item.IdEstado = Fg.Mid(sLista, 4, 2);
                Item.IdFarmacia = Fg.Mid(sLista, 6, 4);
                Item.Codificadora = Fg.Mid(sLista, 10, 2);
                Item.CodigoEAN = Fg.Mid(sLista, 12, 15);
                Item.Multiplo = Convert.ToInt32("0" + Fg.Mid(sLista, 27)); 
            }
        }

        private static void Decodificar_Segmento_02(ref ItemCodificacion Item, string[] Elementos)
        {
            string sLista = "";
            bool bExistenDatos = false;

            try
            {
                // 01LG150600
                sLista = Elementos[1];
                bExistenDatos = true; 
            }
            catch { }

            if (bExistenDatos)
            {
                Item.IdSubFarmacia = Fg.Mid(sLista, 1, 2);
                Item.ClaveLote = Fg.Mid(sLista, 3);  
            }
        }

        private static void Decodificar_Segmento_03(ref ItemCodificacion Item, string[] Elementos)
        {
            string sLista = "";
            bool bExistenDatos = false;

            try 
            {
                // 1612|15|10|27|12|05|10|01|0001|ABCDEFG 
                // 1612151027120510010001ABCDEFG
                sLista = Elementos[2]; 
                bExistenDatos = true;  
            }
            catch { }

            if (bExistenDatos)
            {
                Item.Caducidad = Fg.Mid(sLista, 1, 4);
                Item.Año = Fg.Mid(sLista, 5, 2);
                Item.Mes = Fg.Mid(sLista, 7, 2);
                Item.Dia = Fg.Mid(sLista, 9, 2);
                Item.Hora = Fg.Mid(sLista, 11, 2);
                Item.Minuto = Fg.Mid(sLista, 13, 2);
                Item.Segundo = Fg.Mid(sLista, 15, 2);
                Item.Consecutivo = Fg.Mid(sLista, 17, 2);
                Item.IdProveedor = Fg.Mid(sLista, 19, 1);
                Item.NumeroDeFactura = Fg.Mid(sLista, 20);
            }
        }

        private static void GetInformacionEmpresa(ref ItemCodificacion Item)
        {
            string sSql = string.Format("Select * From vw_Empresas (NoLock) Where IdEmpresa = '{0}' ", Item.IdEmpresa); 

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "InformacionEstadoFarmacia()");
            }
            else
            {
                if (leer.Leer())
                {
                    Item.Empresa = leer.Campo("Empresa");
                }
            }

        }

        private static void GetInformacionEstadoFarmacia(ref ItemCodificacion Item)
        {
            string sSql = string.Format("Select * From vw_Farmacias (NoLock) Where IdEstado = '{0}' and IdFarmacia = '{1}' ", 
                Item.IdEstado, Item.IdFarmacia); 

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "InformacionEstadoFarmacia()"); 
            }
            else
            {
                if (leer.Leer())
                {
                    Item.Estado = leer.Campo("Estado");
                    Item.Farmacia = leer.Campo("Farmacia");
                }
            }
        }

        private static void GetInformacionProducto(ref ItemCodificacion Item)
        {
            string sSql = string.Format("Select * From vw_Productos_CodigoEAN (NoLock) " +
                " Where right('00000000000000000000' + CodigoEAN, 20) = '{0}' ",
                Fg.PonCeros(Item.CodigoEAN, 20)); 

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "InformacionProducto()");
            }
            else
            {
                if (leer.Leer())
                {
                    Item.IdProducto = leer.Campo("IdProducto");
                    Item.CodigoEAN = leer.Campo("CodigoEAN");
                    Item.DescripcionComercial = leer.Campo("Descripcion");
                    Item.ClaveSSA = leer.Campo("ClaveSSA");
                    Item.DescripcionClave = leer.Campo("DescripcionClave");
                    Item.IdLaboratorio = leer.Campo("IdLaboratorio"); 
                    Item.Laboratorio = leer.Campo("Laboratorio"); 
                }
            }

        }

        public static void GetInformacionRapidaProducto(ref ItemCodificacion Item)
        {
            string sSql = string.Format("Select * From CatProductos_CodigosRelacionados (NoLock) " +
                " Where right('00000000000000000000' + CodigoEAN, 20) = '{0}' ",
                Fg.PonCeros(Item.CodigoEAN, 20));

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "InformacionProducto()");
            }
            else
            {
                if (leer.Leer())
                {
                    Item.IdProducto = leer.Campo("IdProducto");
                    Item.CodigoEAN = leer.Campo("CodigoEAN");
                }
            }

        }

        private static void GetInformacionSubFarmacia(ref ItemCodificacion Item)
        {
            string sSql = string.Format("Select  IdEstado, IdSubFarmacia, Descripcion as SubFarmacia, EsConsignacion, Status, Actualizado, EmulaVenta " + 
                " From CatEstados_SubFarmacias (NoLock) Where IdEstado = '{0}' and IdSubFarmacia = '{1}' ",
                Fg.PonCeros(Item.IdEstado, 2), Fg.PonCeros(Item.IdSubFarmacia, 2));

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "InformacionProveedor()");
            }
            else
            {
                if (leer.Leer())
                {
                    Item.SubFarmacia = leer.Campo("SubFarmacia");
                    Item.EsConsignacion = leer.CampoBool("EsConsignacion"); 
                }
            }

        }

        private static void GetInformacionProveedor(ref ItemCodificacion Item)
        {
            ////string sSql = string.Format("Select * From vw_Proveedores (NoLock) Where IdProveedor = '{0}' ",
            ////    Fg.PonCeros(Item.IdProveedor, 4) ); 

            ////if (!leer.Exec(sSql))
            ////{
            ////    Error.GrabarError(leer, "InformacionProveedor()");
            ////}
            ////else
            ////{
            ////    if (leer.Leer())
            ////    {
            ////        Item.Proveedor = leer.Campo("Nombre");
            ////    }
            ////}

            Item.Proveedor = Item.IdProveedor.ToUpper() == "O" ? "Orden de compra" : "Entrada por transferencia";

        }

        private static void GetInformacionUUID(ref ItemCodificacion Item)
        {
            GetInformacionUUID(ref Item, false); 
        }

        private static void GetInformacionUUID(ref ItemCodificacion Item, bool ValidarStatus)
        {
            string sSql = string.Format("Select * From FarmaciaProductos_UUID (NoLock) Where UUID = '{0}' ", Item.UUID );

            if (GnFarmacia.ImplementaReaderDM)
            {
                Item.Existe_UUID = true;
                Item.UUID_Valido = true;
            }
            else 
            {
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "GetInformacionUUID()");
                }
                else
                {
                    Item.Existe_UUID = leer.Leer();
                    Item.UUID_Valido = true;
                    if (ValidarStatus)
                    {
                        Item.UUID_Valido = leer.Campo("Status") == "A";
                    }
                }
            }

        }
        #endregion Funciones y Procedimientos Privados


    }
}
