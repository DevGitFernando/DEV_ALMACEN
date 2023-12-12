using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

namespace DllFarmaciaSoft.IntercambioCartaCanje
{
    public class clsDescargarCartaCanje
    {

        ////////DataSet dtsOrdenCompra;
        //////OrdenesWeb.Timeout = 300000; 
        ////////// OrdenesWeb.Url = "http://intermed.homeip.net/wsComprasTest/wsOficinaCentral.asmx";  
        //////dtsOrdenCompra = OrdenesWeb.InformacionOrdenCompra(sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtOrden.Text.Trim(), 8));
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        string sUrl = ""; 
        string sIdEmpresa = ""; 
        string sIdEstado = ""; 
        string sIdFarmacia = "";
        string sOrigen = "";
        string sFolioCartaCanje = "";


        basGenerales Fg = new basGenerales();

        bool bExistenDatos = false;
        bool bCartaCanjeDescargada = false;
        bool bCartaCanjeGuardada = true;
        DataSet dtsDatosCartaCanje = new DataSet();
        clsLeer leerCartaCanje = new clsLeer();

        string sDatos_001_Enc = "Encabezado";
        string sDatos_002_Det = "Detalles";
        // string sDatos_003_Lotes = "Lotes";

        string sVehiculosCJ = "VehiculosCJ";
        string sEncabezadoCJ = "EncabezadoCJ";
        string sDetalleCJ = "DetalleCJ";
        string sCartaCanjeCJ = "CartaCanjeCJ";

        #region Constructores y Destructores de Clase 
        public clsDescargarCartaCanje(clsDatosConexion DatosConexion, string Url, string Empresa, string Estado, string Almacen, string Folio )
        {
            ConexionLocal = new clsConexionSQL(DatosConexion);
            leer = new clsLeer(ref ConexionLocal);

            this.sUrl = Url;
            this.sIdEmpresa = Fg.PonCeros(Empresa, 3);
            this.sIdEstado = Fg.PonCeros(Estado, 2);
            this.sOrigen = Fg.PonCeros(sOrigen, 4);
            this.sIdFarmacia = Fg.PonCeros(Almacen, 4);
            this.sFolioCartaCanje = Fg.PonCeros(Folio, 8);           
        }
        #endregion Constructores y Destructores de Clase

        #region Propiedades Publicas 
        public string Empresa
        {
            get { return sIdEmpresa; }
        }

        public string Estado
        {
            get { return sIdEstado; }
        }

        public string Almacen
        {
            get { return sIdFarmacia; }
        }

        public string Folio
        {
            get { return sFolioCartaCanje; }
        }

        public bool ExistenDatos
        {
            get { return bExistenDatos; }
        }
        public bool CartaCanjeDescargada
        {
            get { return bCartaCanjeDescargada; }
        }

        public bool CartaCanjeGuardada
        {
            get { return bCartaCanjeGuardada; }
        }

        ////public DataSet Encabezado 
        ////{
        ////    get { return GetDatos(sDatos_001_Enc); }
        ////}

        ////public DataSet Detalles 
        ////{
        ////    get { return GetDatos(sDatos_002_Det); }
        ////}

        ////public DataSet Lotes
        ////{
        ////    get { return GetDatos(sDatos_002_Det); }
        ////}

        ////public DataSet VehiculosCJ
        ////{
        ////    get { return GetDatos(sVehiculosCJ); }
        ////}

        ////public DataSet EncabezadoCJ
        ////{
        ////    get { return GetDatos(sEncabezadoCJ); }
        ////}

        ////public DataSet DetalleCJ
        ////{
        ////    get { return GetDatos(sDetalleCJ); }
        ////}

        ////public DataSet CartaCanjeCJ
        ////{
        ////    get { return GetDatos(sCartaCanjeCJ); }
        ////}
        

        #endregion Propiedades Publicas

        #region Funciones y Procedimientos Publicos 
        public bool Descargar()
        {
            FrmDescargarCartaCanje f = new FrmDescargarCartaCanje(sUrl, sIdEmpresa, sIdEstado, sIdFarmacia, sFolioCartaCanje);

            bExistenDatos = false;
            bCartaCanjeGuardada = false; 
            bCartaCanjeDescargada = false;
            leerCartaCanje = new clsLeer(); 
            dtsDatosCartaCanje = new DataSet(); 


            if (f.Descargar())
            {
                bCartaCanjeDescargada = true;
                leerCartaCanje.DataSetClase = f.dtsDatosCartaCanje; 
                dtsDatosCartaCanje = f.dtsDatosCartaCanje;

                leerCartaCanje.RenombrarTabla("RutasDistribucionEnc", sDatos_001_Enc);
                bExistenDatos = leerCartaCanje.Registros > 0;


                //leerCartaCanje.RenombrarTabla("RutasDistribucionDet_CartasCanje", sDatos_002_Det);
                // leerCartaCanje.RenombrarTabla("OrdenesDeComprasDet", sDatos_003_Lotes);

                ////leerCartaCanje.RenombrarTabla("Vehiculos_SQL", sVehiculosCJ);
                ////leerCartaCanje.RenombrarTabla("RutasDistribucionEnc_SQL", sEncabezadoCJ);
                ////leerCartaCanje.RenombrarTabla("RutasDistribucionDet_SQL", sDetalleCJ);
                ////leerCartaCanje.RenombrarTabla("RutasDistribucionDet_CartasCanje_SQL", sCartaCanjeCJ);
            }

            if (bCartaCanjeDescargada)
            {
                if(bExistenDatos)
                {
                    bCartaCanjeGuardada= InsertaCartaCanje();
                }


                ////if (!RevisarStatusOrdenDeCompra())
                ////{
                ////    bCartaCanjeDescargada = false; 
                ////}
                ////else
                ////{
                ////    if (!RevisarInformacionDestino())
                ////    {
                ////        bCartaCanjeDescargada = false;
                ////    }
                ////    else 
                ////    {

                ////    }
                ////}
            }
           
            return bCartaCanjeDescargada; 

        } 
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private bool RevisarStatusOrdenDeCompra()
        {
            bool bRegresa = true;

            string sStatus = "";
            bool bPermiteDescargas = false;
            string sMsj = ""; 
            ////////clsLeer datos = new clsLeer();
            ////////datos.DataSetClase = this.Encabezado;

            ////////if (!datos.Leer())
            ////////{
            ////////    General.msjUser("No se encontro el Folio de Orden de Compra solicitado, verifique.");
            ////////}
            ////////else
            ////////{
            ////////    sStatus = datos.Campo("Status").ToUpper();
            ////////    bPermiteDescargas = datos.CampoBool("PermiteDescarga");

            ////////    if (sStatus == "OC")
            ////////    {
            ////////        if (!bPermiteDescargas)
            ////////        {
            ////////            General.msjUser("La Orden de Compra solicitada no permite descargas.\n Verifique con el área compras.");
            ////////        }
            ////////        else
            ////////        {
            ////////            bRegresa = true;
            ////////        }
            ////////    }
            ////////    else
            ////////    {
            ////////        if (sStatus == "C")
            ////////        {
            ////////            sMsj = "La Orden de Compra solicitada esta cancelada.";
            ////////        }

            ////////        if (sStatus == "A")
            ////////        {
            ////////            sMsj = "La Orden de Compra solicitada no ha sido colocada.";
            ////////        }

            ////////        General.msjUser(sMsj);
            ////////    }
            ////////}

            return bRegresa; 
        }

        private bool RevisarInformacionDestino()
        {
            bool bRegresa = true;
            string sIdUnidad = sIdEstado + sIdFarmacia;
            string sIdUnidad_OrdCom = "";
            string sMsj = "";
            
            ////clsLeer datos = new clsLeer(); 
            ////datos.DataSetClase = this.Encabezado; 

            ////if (!datos.Leer())
            ////{
            ////    General.msjUser("No se encontro el Folio de Orden de Compra solicitado, verifique.");
            ////}
            ////else
            ////{
            ////    bRegresa = true; 
            ////    sIdUnidad_OrdCom = datos.Campo("EstadoEntrega") + datos.Campo("EntregarEn");

            ////    if (sIdUnidad != sIdUnidad_OrdCom)
            ////    {
            ////        bRegresa = false; 
            ////        sMsj = string.Format(
            ////            "El folio [ {0} ] de Orden de Compra no pertenece a esta Unidad, \n" +
            ////            "pertenece a {1}, {2} -- {3}  ",
            ////            sFolioCartaCanje, datos.Campo("NomEstadoEntrega"), 
            ////            datos.Campo("EntregarEn"), datos.Campo("FarmaciaEntregarEn")); 
            ////        General.msjAviso(sMsj);
            ////    }
            ////}

            return bRegresa; 
        }

        private DataSet GetDatos(string Tabla)
        {
            DataSet dts = new DataSet();

            if (bCartaCanjeDescargada)
            {
                dts.Tables.Add(leerCartaCanje.Tabla(Tabla).Copy());
            }

            return dts; 
        }
        #endregion Funciones y Procedimientos Privados 

        #region InserccionOrdenCompra
        private bool InsertaCartaCanje()
        {
            bool bContinua = false;
            if (ConexionLocal.Abrir())
            {

                ConexionLocal.IniciarTransaccion();

                bContinua = Guardar_Informacion__CartaCanje(); 

                if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                {
                    ConexionLocal.CompletarTransaccion();
                }
                else
                {
                    ConexionLocal.DeshacerTransaccion();
                    General.Error.GrabarError(leer, "Guardar_Informacion__CartaCanje()");
                }

                ConexionLocal.Cerrar();
            }
            else
            {
                ////Error.LogError(ConexionLocal.MensajeError);
                ////General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo.");
            }

            return bContinua;
        }

        private bool Guardar_Informacion__CartaCanje()
        {
            bool bRegresa = true;
            string sSql = "";
            string sSql_Concentrado = "";

            clsLeer leerLocal = new clsLeer();
            clsLeer leerEnc = new clsLeer();
            leerEnc.DataSetClase = dtsDatosCartaCanje;


            if(leerEnc.Leer())
            {
                leerEnc.RegistroActual = 1;
                sSql_Concentrado = "";
                for(int i = 2; i <= leerCartaCanje.Tablas; i++)
                {
                    leerLocal.DataTableClase = leerCartaCanje.Tabla(i); 
                    while(leerLocal.Leer())
                    {
                        sSql = leerLocal.Campo("Resultado");

                        sSql_Concentrado += string.Format("{0} \n", sSql);

                        ////if(!leer.Exec(sSql))
                        ////{
                        ////    bRegresa = false;
                        ////}
                    }

                    sSql_Concentrado += "\n\n";
                }

                bRegresa = leer.Exec(sSql_Concentrado);
            }
            else
            {
                bRegresa = false;
            }


            return bRegresa;
        }
        #endregion InserccionOrdenCompra
    }
}
