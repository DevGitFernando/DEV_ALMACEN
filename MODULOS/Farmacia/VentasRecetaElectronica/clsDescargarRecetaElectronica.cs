using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

namespace Farmacia.VentasRecetaElectronica
{
    internal class clsDescargarRecetaElectronica
    {

        ////////DataSet dtsOrdenCompra;
        //////OrdenesWeb.Timeout = 300000; 
        ////////// OrdenesWeb.Url = "http://intermed.homeip.net/wsComprasTest/wsOficinaCentral.asmx";  
        //////dtsOrdenCompra = OrdenesWeb.InformacionOrdenCompra(sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtOrden.Text.Trim(), 8));
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        string sUrl = "";
        string sIdEstado = "";
        string sCLUES = "";
        string sFolioRecetaElectronica = "";

        basGenerales Fg = new basGenerales(); 

        bool bRecetaDescargada = false;
        DataSet dtsDatosOrden = new DataSet();
        clsLeer leerOrden = new clsLeer();

        string sDatos_001_Enc = "REC_Recetas";
        string sDatos_002_Det = "REC_Recetas_ClavesSSA";
        string sDatos_003_DetDiagnosticos = "REC_Recetas_Diagnosticos";

        // string sDatos_003_Lotes = "Lotes";
        string sEncabezadoReceta = "REC_Recetas";
        string sDetalle_Claves = "REC_Recetas_ClavesSSA";
        string sDetalle_Diagnosticos = "REC_Recetas_Diagnosticos"; 
        

        #region Constructores y Destructores de Clase 
        public clsDescargarRecetaElectronica(clsDatosConexion DatosConexion, 
            string Url, string Empresa, string Estado, string CLUES, string Folio)
        {
            ConexionLocal = new clsConexionSQL(DatosConexion);
            leer = new clsLeer(ref ConexionLocal);
            this.sUrl = Url;
            this.sIdEstado = Fg.PonCeros(Estado, 2);
            this.sCLUES = CLUES;
            this.sFolioRecetaElectronica = Fg.PonCeros(Folio, 8);   
        }
        #endregion Constructores y Destructores de Clase

        #region Propiedades Publicas 
        public string Estado
        {
            get { return sIdEstado; }
        }

        public string CLUES
        {
            get { return sCLUES; }
        }

        public string Folio
        {
            get { return sFolioRecetaElectronica; }
        }

        public bool RecetaDescargada
        {
            get { return bRecetaDescargada; }
        }

        public DataSet Encabezado 
        {
            get { return GetDatos(sDatos_001_Enc); }
        }

        public DataSet Detalles 
        {
            get { return GetDatos(sDatos_002_Det); }
        }

        public DataSet Lotes
        {
            get { return GetDatos(sDatos_002_Det); }
        }

        public DataSet EncabezadoReceta
        {
            get { return GetDatos(sEncabezadoReceta); }
        }

        public DataSet DetalleReceta
        {
            get { return GetDatos(sDetalle_Claves); }
        }

        public DataSet DetalleRecetaDiagnosticos
        {
            get { return GetDatos(sDetalle_Diagnosticos); }
        }
        #endregion Propiedades Publicas

        #region Funciones y Procedimientos Publicos 
        public bool Descargar()
        {
            FrmDescargarRecetaElectronica f = new FrmDescargarRecetaElectronica(sUrl, sIdEstado, sCLUES, sFolioRecetaElectronica);


            bRecetaDescargada = false;
            leerOrden = new clsLeer(); 
            dtsDatosOrden = new DataSet(); 


            if (f.Descargar())
            {
                bRecetaDescargada = true;
                leerOrden.DataSetClase = f.dtsDatosOrden; 
                dtsDatosOrden = f.dtsDatosOrden;

                ////leerOrden.RenombrarTabla("OrdenesDeComprasEnc", sDatos_001_Enc);
                ////leerOrden.RenombrarTabla("OrdenesDeComprasDet", sDatos_002_Det);
                ////// leerOrden.RenombrarTabla("OrdenesDeComprasDet", sDatos_003_Lotes);
                ////leerOrden.RenombrarTabla("COM_OCEN_OrdenesCompra_Claves_Enc", sEncabezadoOC);
                ////leerOrden.RenombrarTabla("COM_OCEN_OrdenesCompra_CodigosEAN_Det", sDetalleOC);
            }

            if (bRecetaDescargada)
            {
                if (!RevisarStatus_RecetaElectronica())
                {
                    bRecetaDescargada = false; 
                }
                else
                {
                    bRecetaDescargada = true;
                    InsertaRecetaElectronica();
                }
            }

            return bRecetaDescargada; 

        } 
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private bool RevisarStatus_RecetaElectronica()
        {
            bool bRegresa = false;

            string sStatus = "";
            string sMsj = ""; 
            clsLeer datos = new clsLeer();
            datos.DataSetClase = this.Encabezado;

            if (!datos.Leer())
            {
                General.msjUser("No se encontro el Folio de Receta Electrónica solicitado, verifique.");
            }
            else
            {
                bRegresa = true; 

                ////sStatus = datos.Campo("Status").ToUpper();
                ////if (sStatus == "A")
                ////{
                ////    bRegresa = true; 
                ////}
                ////else 
                ////{
                ////    if (sStatus == "C")
                ////    {
                ////        sMsj = "La Orden de Compra solicitada esta cancelada.";
                ////    }

                ////    if (sStatus == "A")
                ////    {
                ////        sMsj = "La Orden de Compra solicitada no ha sido colocada.";
                ////    }

                ////    General.msjUser(sMsj); 
                ////} 
            }

            return bRegresa; 
        }

        private DataSet GetDatos(string Tabla)
        {
            DataSet dts = new DataSet();

            if (bRecetaDescargada)
            {
                dts.Tables.Add(leerOrden.Tabla(Tabla).Copy());
            }

            return dts; 
        }
        #endregion Funciones y Procedimientos Privados 

        #region InserccionOrdenCompra
        private bool InsertaRecetaElectronica()
        {
            bool bContinua = false;
            if (ConexionLocal.Abrir())
            {

                ConexionLocal.IniciarTransaccion();

                if (GuardaReceta_Encabezado())
                {
                    if (GuardaReceta_Claves())
                    {
                        bContinua = GuardaReceta_Diagnosticos();
                    }
                }

                if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                {
                    ConexionLocal.CompletarTransaccion();
                }
                else
                {
                    ConexionLocal.DeshacerTransaccion();
                    ////Error.GrabarError(leer, "GuardaOrdenCompra");                    
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

        private bool RegistrarInformacion(DataSet Informacion)
        {
            bool bRegresa = false;
            string sSql = "";
            clsLeer leerInformacion = new clsLeer();
            leerInformacion.DataSetClase = Informacion;


            while (leerInformacion.Leer())
            {
                bRegresa = true;
                sSql = leerInformacion.Campo("Resultado");

                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    break;
                }
            }

            //////if (leerEnc.Leer())
            //////{
            //////    sSql = leerEnc.Campo("Resultado");

            //////    if (!leer.Exec(sSql))
            //////    {
            //////        bRegresa = false;
            //////    }
            //////}
            //////else
            //////{
            //////    bRegresa = false;
            //////}

            return bRegresa;
        }


        private bool GuardaReceta_Encabezado()
        {
            bool bRegresa = true;
            bRegresa = RegistrarInformacion(EncabezadoReceta); 

            ////string sSql = "";
            ////clsLeer leerEnc = new clsLeer();
            ////leerEnc.DataSetClase = EncabezadoReceta;

            ////if (leerEnc.Leer())
            ////{
            ////    sSql = leerEnc.Campo("Resultado");

            ////    if (!leer.Exec(sSql))
            ////    {
            ////        bRegresa = false;
            ////    }
            ////}
            ////else
            ////{
            ////    bRegresa = false;
            ////}

            return bRegresa;
        }

        private bool GuardaReceta_Claves()
        {
            bool bRegresa = true;
            bRegresa = RegistrarInformacion(DetalleReceta); 

            ////string sSql = "";
            ////clsLeer leerDet = new clsLeer();
            ////leerDet.DataSetClase = DetalleReceta;

            ////while (leerDet.Leer())
            ////{
            ////    sSql = leerDet.Campo("Resultado");

            ////    if (!leer.Exec(sSql))
            ////    {
            ////        bRegresa = false;
            ////        break;
            ////    }
            ////}

            return bRegresa;
        }

        private bool GuardaReceta_Diagnosticos()
        {
            bool bRegresa = true;
            bRegresa = RegistrarInformacion(DetalleRecetaDiagnosticos); 

            ////string sSql = "";
            ////clsLeer leerDet = new clsLeer();
            ////leerDet.DataSetClase = DetalleRecetaDiagnosticos;

            ////while (leerDet.Leer())
            ////{
            ////    sSql = leerDet.Campo("Resultado");

            ////    if (!leer.Exec(sSql))
            ////    {
            ////        bRegresa = false;
            ////        break;
            ////    }
            ////}

            return bRegresa;
        }
        #endregion InserccionOrdenCompra
    }
}
