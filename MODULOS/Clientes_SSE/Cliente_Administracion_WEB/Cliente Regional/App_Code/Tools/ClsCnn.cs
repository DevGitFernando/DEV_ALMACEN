using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;

//SC Solutions
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

namespace Cliente_Regional.Code
{
    /// <summary>
    /// Descripción breve de ClsCnn
    /// </summary>
    public class ClsCnn
    {
        #region Declaración de variables
        //Conexión
        private clsDatosConexion datosCnn = new clsDatosConexion();
        private clsGrabarError Error;
        private readonly clsConexionSQL cnn;
        private clsLeer leer;
        #endregion Declaración de variables

        public ClsCnn()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //

            //cargar conexión y referenciar a la clase leer
            General.ArchivoIni = DtGeneral.ArchivoIniConexion;
            General.DatosConexion = GetConexion(General.ArchivoIni);

            cnn = new clsConexionSQL(General.DatosConexion);
            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(datosCnn, DtGeneral.DatosApp, "ClsCnn.cs");
        }

        /// <summary>
        /// Obtencion de datos de necesarios para establecer la conexion
        /// </summary>
        /// <param name="ArchivoConexion">Nombre del archivo del conexion</param>
        /// <returns></returns>
        private clsDatosConexion GetConexion(string ArchivoConexion)
        {
            clsCriptografo crypto = new clsCriptografo();
            basSeguridad fg = new basSeguridad(ArchivoConexion);

            datosCnn.Servidor = fg.Servidor;
            datosCnn.BaseDeDatos = fg.BaseDeDatos;
            datosCnn.Usuario = fg.Usuario;
            datosCnn.Password = fg.Password;
            datosCnn.TipoDBMS = fg.TipoDBMS;
            datosCnn.Puerto = fg.Puerto;
            datosCnn.NormalizarDatos();
            return datosCnn;
        }

        /// <summary>
        /// Función para ejecutar una consulta en la base datosS
        /// </summary>
        /// <param name="sSql">Query a ser ejecutada</param>
        /// <param name="sNombreTabla">Nombre que se le pondra a la primera tabla que sea regresada por el servidor</param>
        /// <param name="sFuncion">Nombre del método que esta invocando la función</param>
        /// <returns>Retorna un DataSet con el resultado de la consulta</returns>
        public DataSet ExecQuery(string sSql, string sNombreTabla, string sFuncion)
        {
            DataSet dtsReturn = new DataSet();
            dtsReturn = null;
            //sSql = sSql.Replace("'", "");
            sSql = sSql.Replace("-", "");
            if (leer.Exec(sSql))
            {
                if (leer.Leer())
                {
                    leer.RenombrarTabla(1, sNombreTabla);
                    dtsReturn = leer.DataSetClase;
                }
            }
            else
            {
                Error.GrabarError(leer, sFuncion);
            }
            return dtsReturn;
        }
    }
}