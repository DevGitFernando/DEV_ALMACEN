using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FP;
using SC_SolutionsSystem.FP.Huellas;

namespace DllFarmaciaSoft.Usuarios_y_Permisos
{
    public class clsOperacionesSupervizadasHuellas
    {
        #region Declaracion de Variables
        clsConexionSQL cnn;
        clsLeer leer;
        clsDatosConexion datosCnn;
        clsDatosConexion DatosDeConexion;
        clsConsultas query;

        wsFarmacia.wsCnnCliente validarHuella = null;
        string sUrlChecador = "";
        private clsConexionSQL CnnHuellas = new clsConexionSQL(General.DatosConexion);
        clsLeer leerChecador;

        string sIdEmpresa = "";
        string sIdEstado = "";
        string sIdFarmacia = "";
        string sIdPersonal = "";
        string sReferenciaHuella = "";
        //string sNombreOperacion = "";
        string sHost = "";
        bool bConectado = false;

        clsGrabarError Error;

        DataSet dtsPermisos = new DataSet();
        DataSet dtsPermisos_Unidades = new DataSet();
        Dictionary<string, bool> pPermisos = new Dictionary<string, bool>();
        #endregion Declaracion de Variables

        public clsOperacionesSupervizadasHuellas():this(General.DatosConexion, DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal) 
        { 
        }

        public clsOperacionesSupervizadasHuellas(clsDatosConexion DatosConexion, string Empresa, string Estado, string Farmacia, string IdPersonal)
        {
            sIdEmpresa = Empresa;
            sIdEstado = Estado;
            sIdFarmacia = Farmacia;
            sIdPersonal = IdPersonal;

            datosCnn = DatosConexion;
            cnn = new clsConexionSQL(DatosConexion); 
            leer = new clsLeer(ref cnn);

            query = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, "clsOperacionesSupervizadasHuellas", true);
            Error = new clsGrabarError(datosCnn, DtGeneral.DatosApp, "clsOperacionesSupervizadasHuellas");
            validarHuella = new DllFarmaciaSoft.wsFarmacia.wsCnnCliente();

            if (DtGeneral.ConfirmacionConHuellas)
            {
                if (!bConectado)
                {
                    if (Obtener_Url_Firma())
                    {
                        if (validarDatosDeConexionHuellas())
                        {
                            ConexionChecador();
                        }
                    }
                }
            }
        }

        ////////public clsOperacionesSupervizadasHuellas(ref clsConexionSQL Conexion, string sEmpresa, string Estado, string Farmacia, string IdPersonal)
        ////////{
        ////////    sIdEmpresa = sEmpresa;
        ////////    sIdEstado = Estado;
        ////////    sIdFarmacia = Farmacia;
        ////////    sIdPersonal = IdPersonal;

        ////////    datosCnn = Conexion.DatosConexion;
        ////////    leer = new clsLeer(ref Conexion);
        ////////    cnn = Conexion;

        ////////    query = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, "clsOperacionesSupervizadasHuellas", true);
        ////////    Error = new clsGrabarError(datosCnn, DtGeneral.DatosApp, "clsOperacionesSupervizadasHuellas");
        ////////    validarHuella = new DllFarmaciaSoft.wsFarmacia.wsCnnCliente();

        ////////    if (DtGeneral.ConfirmacionConHuellas)
        ////////    {
        ////////        if (Obtener_Url_Firma())
        ////////        {
        ////////            if (validarDatosDeConexionHuellas())
        ////////            {
        ////////                ConexionChecador();
        ////////            }
        ////////        }
        ////////    }
        ////////}

        #region Propiedades
        public bool Conectado
        {
            get { return bConectado; }
            set { bConectado = value; }
        }

        public string ReferenciaHuella
        {
            get { return sReferenciaHuella; }
            set { sReferenciaHuella = value; }
        }


        #endregion Propiedades 
        #region Funciones y Procedimientos Publicos

        //public bool GrabarPropietarioDeHuella(string sFolio)
        //{
        //    return GrabarPropietarioDeHuella("MovtosInv_Enc", "IdPersonalHuella", "FolioMovtoInv", sFolio);
        //}

        //public bool GrabarPropietarioDeHuella(string sTabla, string sCampoAGrabar, string sCampoFolio, string sFolio)
        public bool GrabarPropietarioDeHuella(string sFolio)
        {
            return false; 
        }

        public bool GrabarPropietarioDeHuella(clsConexionSQL Conexion, string sFolio)
        {
            bool bRegresa = false;
            clsLeer leer_Grabar = new clsLeer(ref Conexion); 

            string sSql = string.Format("Update MovtosInv_Enc Set IdPersonalHuella = '{0}' " +
                            "Where IdEmpresa = '{1}' And IdEstado = '{2}' And IdFarmacia = '{3}' And FolioMovtoInv = '{4}' ",
                            sReferenciaHuella, sIdEmpresa, sIdEstado, sIdFarmacia, sFolio);

            if (leer_Grabar.Exec(sSql))
            {
                bRegresa = true;
            }

            return bRegresa;
        }

        public bool VerificarPermisos(string sNombreOperacion, string sMsjNoEncontrado)
        {
            bool bRegresa = false;
            if (!bConectado)
            {
                General.msjError("No fue posible establecer la conexión para la validación de huellas.");
            }
            else
            {
                clsVerificarHuella f = new clsVerificarHuella();
                f.MostrarMensaje = false;
                f.Titulo = "Validar permisos";
                f.Show();

                if (!FP_General.HuellaCapturada)
                {
                    General.msjAviso("No se detecto firma de huella.");
                }
                else
                {
                    if (!FP_General.ExisteHuella)
                    {
                        General.msjAviso("La huella leída no se encuentra registrada.");
                    }
                    else
                    {
                        sReferenciaHuella = FP_General.Referencia_Huella;

                        bRegresa = TienePermisoHuellas(sReferenciaHuella, sNombreOperacion);

                        if (!bRegresa)
                        {
                            bRegresa = TienePermisoPorFarmaciaHuellas(sIdEstado, sIdFarmacia, sReferenciaHuella, sNombreOperacion);
                        }

                        if (!bRegresa)
                        {
                            General.msjAviso(sMsjNoEncontrado);
                        }

                    }
                }
            }

            return bRegresa;
        }

        public bool CargarPermisos()
        {
            bool bRegresa = true;

            CargarPermisos_Huellas(); 
            CargarPermisos_FarmaciaHuellas(); 

            return bRegresa;
        }

        private bool CargarPermisos_Huellas()
        {
            //// Se valida que tenga permisos de huella a nivel general

            bool bRegresa = false;
            string sSql = string.Format(" 	Select V.IdPersonal, O.Nombre as Permiso  " +
                " From CatPersonalHuellas V (NoLock) " +
                //" Inner Join Net_Usuarios U (NoLock) On ( V.IdEstado = U.IdEstado and V.IdFarmacia = U.IdSucursal and V.IdPersonal = U.IdPersonal) " +
                " Inner Join Net_Permisos_Operaciones_SupervisadasHuellas P (NoLock) " +
                //" 	On ( V.IdEstado = P.IdEstado and V.IdFarmacia = P.IdFarmacia and P.IdPersonal = V.IdPersonal )" +
                " 	On ( P.IdPersonal = V.IdPersonal ) " + 
                " Inner Join Net_Operaciones_SupervisadasHuellas O (NoLock)	On ( P.IdOperacion = O.IdOperacion ) " +
                " Where P.Status = 'A' and O.Status = 'A' ");

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "TienePermisoHuellas()");
                General.msjError("Ocurrió un error al obtener los permisos.");
            }
            else
            {
                dtsPermisos = leer.DataSetClase; 
            }

            return bRegresa;
        }

        private bool CargarPermisos_FarmaciaHuellas()
        {
            bool bRegresa = false;
            string sSql = string.Format("Select V.IdPersonal, O.Nombre as Permiso " + 
                "From CatPersonalHuellas V (NoLock) " +
                "Inner Join Net_Permisos_Operaciones_SupervisadasPorFarmaciaHuellas P (NoLock) On ( P.IdPersonal = V.IdPersonal )  " +
                "Inner Join Net_Operaciones_SupervisadasPorFarmaciaHuellas O (NoLock) On ( P.IdOperacion = O.IdOperacion )  " +
                "Where P.Status = 'A' and O.Status = 'A' And P.IdEstado = '{0}' And P.IdFarmacia = '{1}' ",
                sIdEstado, sIdFarmacia);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "TienePermisoPorFarmaciaHuellas()");
                General.msjError("Ocurrió un error al obtener los permisos del usuario.");
            }
            else
            {
                dtsPermisos_Unidades = leer.DataSetClase; 
            }

            return bRegresa;
        } 

        public bool TienePermisoHuellas(string Personal, string Operacion)
        {
            bool bRegresa = false;

            try
            {
                DataRow[] rows = dtsPermisos.Tables[0].Select(string.Format(" IdPersonal = '{0}' and Permiso = '{1}' ", Personal, Operacion.ToUpper()));
                bRegresa = rows.Length >= 1;

                //////// QUITAR 
                ////if (DtGeneral.EsAdministrador)
                ////{
                ////    bRegresa = true; 
                ////}
            }
            catch (Exception ex) 
            { 
            }

            return bRegresa; 
        }

        public bool TienePermisoPorFarmaciaHuellas(string IdEstado, string IdFarmacia, string Personal, string Operacion)
        {
            bool bRegresa = false;

            try
            {
                DataRow[] rows = dtsPermisos_Unidades.Tables[0].Select(
                    string.Format(" IdEstado = '{0}' and IdFarmacia = '{1}' and IdPersonal = '{2}' and Permiso = '{3}' ", 
                        IdEstado, IdFarmacia, Personal, Operacion.ToUpper())
                    );
                bRegresa = rows.Length >= 1;
            }
            catch (Exception ex)
            {
            }

            return bRegresa; 
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos privados
        private bool Obtener_Url_Firma()
        {
            bool bRegresa = true;

            leer.DataSetClase = query.Url_AutorizacionFirma("Obtener_Url_Firma");

            if (leer.Leer())
            {
                sUrlChecador = leer.Campo("UrlFarmacia");
                sHost = leer.Campo("Servidor");
            }
            else
            {
                bRegresa = false;
            }

            return bRegresa;
        }

        private bool validarDatosDeConexionHuellas()
        {
            bool bRegresa = false;

            try
            {
                //leerWeb = new clsLeerWebExt(sUrlChecador, DtGeneral.CfgIniChecadorPersonal, DatosCliente);
                validarHuella.Url = sUrlChecador;
                DatosDeConexion = new clsDatosConexion(validarHuella.ConexionEx(DtGeneral.CfgIniAutorizacionConHuellas));
                //DatosDeConexion = new clsDatosConexion(AbrirConexionEx(DtGeneral.CfgIniChecadorPersonal));
                DatosDeConexion.Servidor = sHost;
                bRegresa = true;
                bConectado = true;
            }
            catch (Exception ex1)
            {
                Error.GrabarError(leer.DatosConexion, ex1, "validarDatosDeConexionHuellas()"); 
                General.msjAviso("No fue posible establecer conexión con el sistema de validación de firma biométrica.");
            }

            return bRegresa;
        }

        private void ConexionChecador()
        {
            CnnHuellas = new clsConexionSQL(DatosDeConexion);
            leerChecador = new clsLeer(ref CnnHuellas);
            FP_General.Conexion = DatosDeConexion;
        }
        #endregion Funciones y Procedimientos privados
    }
}
