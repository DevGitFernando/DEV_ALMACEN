using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;

namespace DllAcceso_FES
{
    public class clsGetInformacion
    {
        private clsDatosConexion datosCnn;
        private clsConexionSQL cnn;
        private clsLeer leer;
        private basGenerales Fg = new basGenerales(); 
        private string sHost = "";
        private string sNombreHost = ""; 


        public clsGetInformacion(clsDatosConexion DatosDeConexion, string Host, string HostName)
        {
            datosCnn = DatosDeConexion;
            this.sHost = Host;
            this.sNombreHost = HostName; 

            cnn = new clsConexionSQL(datosCnn);
            leer = new clsLeer(ref cnn); 
        }

        public void RegistrarLog(int IdAccesoExterno, string Sentencia)
        {
            string sSql = Sentencia.Replace("'", "" + Fg.Comillas() + "");
            sSql = string.Format("Exec spp_AIE_RegistroLog '{0}', '{1}', '{2}', '{3}' ", IdAccesoExterno, sSql, sHost, sNombreHost); 
            leer.Exec(sSql); 
        } 

        public DataSet Lista_De_Claves_Licitadas(int IdAccesoExterno)
        {
            DataSet dts = new DataSet();
            string sSql = string.Format("Exec spp_AIE_Lista_ClavesLicitadas '{0}' ", Fg.PonCeros(IdAccesoExterno, 4));

            RegistrarLog(IdAccesoExterno, sSql); 
            if (!leer.Exec(sSql))
            {
                dts = leer.ListaDeErrores();
                // dts.Tables[0].TableName = "Errores"; 
            }
            else
            {
                leer.RenombrarTabla(1, "Claves_Licitadas"); 
                dts = leer.DataSetClase; 
            }

            return dts; 
        }

        public DataSet Consumos(int IdAccesoExterno, string FechaInicial, string FechaFinal)
        {
            DataSet dts = new DataSet();
            string sSql = string.Format("Exec spp_AIE_Consumos '{0}', '{1}', '{2}' ",
                Fg.PonCeros(IdAccesoExterno, 4), FechaInicial, FechaFinal);
 
            RegistrarLog(IdAccesoExterno, sSql); 
            if (!leer.Exec(sSql))
            {
                dts = leer.ListaDeErrores();
                // dts.Tables[0].TableName = "Errores"; 
            }
            else
            {
                leer.RenombrarTabla(1, "Consumo_Claves_Licitadas");
                dts = leer.DataSetClase;
            }

            return dts;
        }
    }
}
