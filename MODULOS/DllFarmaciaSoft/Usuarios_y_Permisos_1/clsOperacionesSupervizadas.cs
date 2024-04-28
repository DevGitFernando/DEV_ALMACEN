using System;
using System.Collections; 
using System.Collections.Generic;
using System.Data; 
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;

namespace DllFarmaciaSoft.Usuarios_y_Permisos
{
    public class clsOperacionesSupervizadas
    {
        #region Declaracion de Variables 
        clsConexionSQL cnn;
        clsLeer leer; 
        clsDatosConexion datosCnn; 

        string sIdEstado = "";
        string sIdFarmacia = ""; 
        string sIdPersonal = "";
        string sNombreOperacion = "";

        clsGrabarError Error;

        DataSet dtsPermisos = new DataSet(); 
        Dictionary<string, bool> pPermisos = new Dictionary<string, bool>(); 
        #endregion Declaracion de Variables

        #region Constructores y Destructor
        ////public clsOperacionesSupervizadas(clsDatosConexion DatosDeCnn, string Estado, string Farmacia)
        ////{
        ////    datosCnn = DatosDeCnn;
        ////    sIdEstado = Estado;
        ////    sIdFarmacia = Farmacia;

        ////    cnn = new clsConexionSQL(datosCnn);
        ////    leer = new clsLeer(ref cnn); 

        ////    Error = new clsGrabarError(datosCnn, DtGeneral.DatosApp, "clsOperacionesSupervizadas");  
        ////}

        public clsOperacionesSupervizadas( clsDatosConexion DatosDeCnn, string Estado, string Farmacia, string IdPersonal )
        {
            datosCnn = DatosDeCnn;
            sIdEstado = Estado;
            sIdFarmacia = Farmacia;
            sIdPersonal = IdPersonal; 

            cnn = new clsConexionSQL(datosCnn);
            leer = new clsLeer(ref cnn);

            Error = new clsGrabarError(datosCnn, DtGeneral.DatosApp, "clsOperacionesSupervizadas");
        }

        #endregion Constructores y Destructor

        #region Propiedades
        public string Personal
        {
            get { return sIdPersonal; }
            set { sIdPersonal = value; }
        }

        public string Operacion
        {
            get { return sNombreOperacion; }
            set { sNombreOperacion = value; }
        }
        #endregion Propiedades

        #region Funciones y Procedimientos Publicos 
        public bool CargarPermisos()
        {
            bool bRegresa = true;
            string sSql = string.Format(" 	Select V.IdPersonal, V.NombreCompleto, U.Password, P.Status, " +
                " Upper(O.Nombre) as Permiso, O.Descripcion  " +
                " From vw_Personal V (NoLock) " +
                " Inner Join Net_Usuarios U (NoLock) On ( V.IdEstado = U.IdEstado and V.IdFarmacia = U.IdSucursal and V.IdPersonal = U.IdPersonal) " +
                " Inner Join Net_Permisos_Operaciones_Supervisadas P (NoLock) " +
                " 	On ( V.IdEstado = P.IdEstado and V.IdFarmacia = P.IdFarmacia and P.IdPersonal = V.IdPersonal )" +
                " Inner Join Net_Operaciones_Supervisadas O (NoLock)	On ( P.IdOperacion = O.IdOperacion ) " +
                " Where P.IdEstado = '{0}' and P.IdFarmacia = '{1}' and V.IdPersonal = '{2}' and P.Status = 'A' and O.Status = 'A' ",
                sIdEstado, sIdFarmacia, Personal);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "TienePermiso");
                General.msjError("Ocurrió un error al obtener los permisos del usuario.");
            }
            else
            {
                dtsPermisos = leer.DataSetClase; 
            }

            return bRegresa; 
        }

        ////public bool TienePermiso()
        ////{
        ////    return TienePermiso(sIdPersonal, sNombreOperacion); 
        ////}

        public bool TienePermiso(string Operacion)
        {
            bool bRegresa = false;

            try
            {
                DataRow[] rows = dtsPermisos.Tables[0].Select(string.Format(" Permiso = '{0}' ", Operacion.ToUpper())); 
                bRegresa = rows.Length >= 1; 
            }
            catch { }

            if (DtGeneral.EsAdministrador)
            {
                bRegresa = true; 
            }

            return bRegresa; 
            //return TienePermiso(sIdPersonal, Operacion);
        }

        #endregion Funciones y Procedimientos Publicos

    }
}
