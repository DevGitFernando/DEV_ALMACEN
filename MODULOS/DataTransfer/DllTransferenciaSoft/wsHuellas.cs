using System;
using System.Collections.Generic;
using System.Text;

using System.Web;
using System.Web.Services;
using System.Data;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;

using DllFarmaciaSoft;
using DllFarmaciaSoft.wsFarmaciaSoftGn;

namespace DllTransferenciaSoft
{
    [WebService(Description = "Modulo Huellas", Namespace = "http://SC-Solutions/ServiciosWeb/")]
    public class wsHuellas: DllFarmaciaSoft.wsConexion
    {
        /// <summary>
        /// Proporciona funciones generales que se implementan en la clase
        /// </summary>
        basSeguridad funciones;
        string Solicitud = "Huellas";
        /// <summary>
        /// Obtiene los datos de conexion con el servidor de BD
        /// </summary>
        /// <returns>Regresa un Dataset con la información completa para la conexion con el servidor.</returns>
        [WebMethod(Description = ".")]
        private DataSet AbrirConexionEx(string ArchivoIni)
        {
            clsDatosConexion datosCnn = new clsDatosConexion();
            funciones = new basSeguridad(ArchivoIni);

            datosCnn.Servidor = funciones.Servidor;
            datosCnn.BaseDeDatos = funciones.BaseDeDatos;
            datosCnn.Usuario = funciones.Usuario;
            datosCnn.Password = funciones.Password;
            datosCnn.TipoDBMS = funciones.TipoDBMS;
            datosCnn.Puerto = funciones.Puerto;

            return datosCnn.DatosCnn();
        }

        [WebMethod(Description = "Probar conexión.")]
        public virtual string ProbarConexion(string ArchivoIni)
        {
            string sResultado = "Prueba";
            bool bRegresa = true;

            clsDatosConexion datosCnn = new clsDatosConexion();
            clsConexionSQL cnn = new clsConexionSQL();
            clsLeer leer = new clsLeer();
            wsTestConexion testConexion = new wsTestConexion(); 

            try
            {
                datosCnn = new clsDatosConexion(AbrirConexionEx(ArchivoIni));
                datosCnn.Servidor = testConexion.RevisarServidor(datosCnn.Servidor);
            }
            catch (Exception ex)
            {
                bRegresa = false;
                sResultado = "Error al obtener los datos de conexion. \n" + ex.Message;
            }

            if (bRegresa)
            {
                try
                {
                    cnn = new clsConexionSQL(datosCnn);
                }
                catch (Exception ex)
                {
                    bRegresa = false;
                    sResultado = "Error al crear la conexión. \n" + ex.Message;
                }
            }

            if (bRegresa)
            {
                leer = new clsLeer(ref cnn);
                try
                {
                    sResultado += cnn.DatosConexion.CadenaDeConexion;
                    sResultado += "\n\n\n\n ";
                    sResultado = "";

                    if (!leer.Exec("Select getdate() as Fecha"))
                    {
                        sResultado += leer.Error.Message;
                    }
                    else
                    {
                        //sResultado = "Fecha servidor :  " + leer.CampoFecha("Fecha").ToLongDateString().ToUpper();
                        sResultado += "Fecha servidor :  " + leer.CampoFecha("Fecha").ToString().ToUpper();                        
                    }
                }
                catch { }
            }

            return sResultado;

        }

        [WebMethod(Description = "Envio Informacion de Huellas")]
        public DataSet InformacionHuellas()
        {
            clsGrabarError manError = new clsGrabarError();
            string sTablaCfgHuellas = " CFGC_EnvioHuellas ", NomTabla = "";            
            bool bExecuto = true;
           
            DataSet dtsHuellas = new DataSet("Base_Resultado");           

            try
            {
                wsTestConexion testConexion = new wsTestConexion(); 
                clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(Solicitud));
                datosCnn.Servidor = testConexion.RevisarServidor(datosCnn.Servidor);

                clsConexionSQL cnn = new clsConexionSQL(datosCnn);
                clsLeer leerCat = new clsLeer(ref cnn);
                clsLeer leer = new clsLeer(ref cnn);

                string sSql = string.Format(" Select * From {0} (NoLock) " +
                                            " Where Status = 'A' Order By IdOrden, NombreTabla ", sTablaCfgHuellas);                

                
                if (!leerCat.Exec(sSql))
                {
                    dtsHuellas = leerCat.ListaDeErrores();
                }
                else
                {                    
                    while (leerCat.Leer())
                    {
                        leer = new clsLeer(ref cnn);
                        NomTabla = leerCat.Campo("NombreTabla");

                        string sQuery = string.Format(" Exec spp_CFG_ObtenerDatos '{0}' ", NomTabla);

                        if (!leer.Exec(NomTabla, sQuery))
                        {
                            bExecuto = false;
                            manError.GrabarError(leer, "InformacionHuellas");
                            break;
                        }
                        else
                        {
                            if (leer.Leer())
                            {
                                dtsHuellas.Tables.Add(leer.DataTableClase.Copy());
                            }
                        }
                    }

                    if (!bExecuto)                        
                    {
                        dtsHuellas = leer.ListaDeErrores();
                    }                    
                }                
            }
            catch (Exception ex1)
            {
                manError.LogError(ex1.Message);
            }
            return dtsHuellas;
        }
    }

    
}
