using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Conexiones;

namespace Facturacion.GenerarRemisiones
{
    class ClsRemision_Ventas
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        DllFarmaciaSoft.wsFarmaciaSoftGn.wsConexion conexionWeb = null;
        DllFarmaciaSoft.wsFarmaciaSoftGn.wsConexion conexionWebRegional = null;
        clsLeer leer, leerRegional;
        clsConexionSQL ConexionRegional;
        clsDatosConexion DatosDeConexion;
        //clsLeer leerDet;
        //clsLeer leerDet_Lotes;

        string sUrl_Regional = "";
        public string sMensaje = "";

        #region Declaracion Variables       

        clsLeer LeerInformacion = new clsLeer();
    

        string Empresa = "", Estado = "", Farmacia = "", Folio = "", sUrl = "";

        clsGrabarError Error;
        bool bError = false;

        clsConexionClienteUnidad conecionCte;
        #endregion Declaracion Variables

        public ClsRemision_Ventas(string Url, string Empresa, string Estado, string IdFarmacia, string Folio)
        {
            leer = new clsLeer(ref ConexionLocal);
            Error = new clsGrabarError();

            this.sUrl = Url;
            this.Empresa = Empresa;
            this.Estado = Estado;
            this.Farmacia = IdFarmacia;
            this.Folio = Folio;

            conecionCte = new clsConexionClienteUnidad();

            conecionCte.Empresa = Empresa;
            conecionCte.Estado = Estado;
            conecionCte.Farmacia = Farmacia;
            

            conecionCte.ArchivoConexionCentral = DtGeneral.CfgIniOficinaCentral;
            conecionCte.ArchivoConexionUnidad = DtGeneral.CfgIniPuntoDeVenta;
        }


        public bool TieneError
        {
            get { return bError; }
        }

        public bool InformacionOrdenCompra()
        {
            bool bRegresa = false;
            string sSql = "";

            sSql = string.Format(" Exec spp_FACT_ObtenerInformacionVenta   @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioVenta = '{3}' ",
                                 Empresa, Estado, Farmacia, Folio);

            conexionWeb = new DllFarmaciaSoft.wsFarmaciaSoftGn.wsConexion();
            conexionWeb.Url = sUrl;
            conexionWeb.Timeout = 500000;

            conecionCte.Sentencia = sSql;

            leer.DataSetClase = conexionWeb.ExecuteExt(conecionCte.dtsInformacion, DtGeneral.CfgIniPuntoDeVenta, sSql);

            if (leer.SeEncontraronErrores())
            {
                LeerInformacion.DataSetClase = leer.ListaDeErrores();
                bError = true;
            }
            else
            {
                LeerInformacion.DataSetClase = leer.DataSetClase.Copy();

                //DataSet dts = new DataSet();
                //dts.Tables.Add(dtsRetorno.Tables["OrdenesDeComprasEnc"].Copy());
                //leerDet.DataSetClase = dts;

                bRegresa = leer.Leer();

                //if (!bLocal && leerDet.CampoBool("PermiteDescarga"))
                //{
                //    bRegresa = SiguienteStatus("", true);
                //}

                //bRegresa = true;
            }

            return bRegresa;
        }

        public bool RegistrarEnRegional()
        {
            bool bRegresa = true;
            clsLeer LeerTrabajo = new clsLeer();

            string sSql = "";
            sSql = "Set DateFormat YMD \n";

            bRegresa = ObtenerUrlRegional();

            if (bRegresa)
            {
                ConexionLocal.IniciarTransaccion();
                ConexionRegional.IniciarTransaccion();

                for (int iTablas = 1; LeerInformacion.Tablas >= iTablas && bRegresa; iTablas++)
                {
                    LeerTrabajo.DataTableClase = LeerInformacion.Tabla(iTablas);
                    sSql = "Set DateFormat YMD \n";

                    while(LeerTrabajo.Leer() && bRegresa)
                    {
                        //sSql = "Set DateFormat YMD " + LeerTrabajo.Campo("Resultado");

                        sSql += string.Format("{0}\n", LeerTrabajo.Campo("Resultado"));
                    }


                    bRegresa = AplicarRegional(sSql);
                    if(bRegresa)
                    {
                        bRegresa = AplicarFacturacion(sSql);
                    }
                }

                if (bRegresa)
                {
                    ConexionLocal.CompletarTransaccion();
                    ConexionRegional.CompletarTransaccion();
                }
                else
                {
                    ConexionLocal.DeshacerTransaccion();
                    ConexionRegional.DeshacerTransaccion();
                }

            }

            return bRegresa;
        }

        private bool ObtenerUrlRegional()
        {
            bool bRegresa = false;
            string sSql = ""; //  "Select distinct IdEstado, Estado, EdoStatus From vw_Farmacias (NoLock) Where EdoStatus = 'A' Order By IdEstado ";

            sSql = string.Format("Select * From vw_Regionales_Urls U(NoLock) Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '0001' ", 
                    Empresa, Estado);

            try
            {
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "ObtenerUrlRegional()");
                    General.msjError("Ocurrió un error al obtener la Url del Regional.");
                }
                else
                {
                    if (leer.Leer())
                    {
                        sUrl_Regional = leer.Campo("UrlFarmacia");

                        conexionWeb.Url = sUrl_Regional;
                        DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneral.CfgIniOficinaCentral));

                        ConexionRegional = new clsConexionSQL(DatosDeConexion);

                        leerRegional = new clsLeer(ref ConexionRegional);

                        bRegresa = true;
                    }
                }
            }
            catch(Exception ex)
            {
                ex = null;
            }

            return bRegresa;
        }

        private bool AplicarRegional(string sSql)
        {
            bool bRegresa = false;

            if (!leerRegional.Exec(sSql))
            {
                LeerInformacion.DataSetClase = leerRegional.ListaDeErrores();
                bError = true;
                sMensaje = "Ocurrio un error al registrarlo en el regional.";
            }
            else
            {
                bRegresa = true;
            }

            return bRegresa;
        }

        private bool AplicarFacturacion(string sSql)
        {
            bool bRegresa = false;

            if (!leer.Exec(sSql))
            {
                LeerInformacion.DataSetClase = leer.ListaDeErrores();
                bError = true;
                sMensaje = "Ocurrio un error al registrarlo.";
            }
            else
            {
                bRegresa = true;
            }

            return bRegresa;
        }

    }
}
