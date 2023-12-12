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

namespace DllInformacionFarmacia
{
    public class clsGetInformacion
    {
        private clsDatosConexion datosCnn;
        private clsConexionSQL cnn;
        private clsLeer leer;
        private basGenerales Fg = new basGenerales();

        private string sIdEstado = "";
        private string sIdFarmacia = ""; 

        private string sHost = "";
        private string sNombreHost = ""; 


        public clsGetInformacion(clsDatosConexion DatosDeConexion, string IdEstado, string IdFarmacia, string Host, string HostName)
        {
            datosCnn = DatosDeConexion;
            this.sIdEstado = Fg.PonCeros(IdEstado, 2);
            this.sIdFarmacia = Fg.PonCeros(IdFarmacia, 4); 
            this.sHost = Host;
            this.sNombreHost = HostName;

            cnn = new clsConexionSQL(datosCnn);
            leer = new clsLeer(ref cnn);
        } 

        #region Funciones y Procedimientos Publicos 
        public DataSet CuadroBasico() 
        {
            DataSet dts = new DataSet();
            string sSql = string.Format("Exec spp_AIEF_CuadroBasico  '{0}', '{1}' ", sIdEstado, sIdFarmacia);

            dts = EjecutarConsulta(1, "CuadroBasico", sSql); 

            return dts; 
        }

        public DataSet Existencia(string ClaveSSA) 
        {
            return Existencia(ClaveSSA, false); 
        } 

        public DataSet Existencia(string ClaveSSA, bool MostrarGrupo)
        {
            DataSet dts = new DataSet();
            string sSql = string.Format("Exec spp_AIEF_Existencia_ClaveSSA  '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ",
                sIdEstado, sIdFarmacia, ClaveSSA, MostrarGrupo, sHost, sNombreHost); 

            dts = EjecutarConsulta(2, "Existencias", sSql); 

            return dts;
        }
        #endregion Funciones y Procedimientos Publicos 

        #region Funciones y Procedimientos Privados 
        public void RegistrarLog(int Opcion, int IdAccesoExterno, string Sentencia)
        {
            string sSql = Sentencia.Replace("'", "" + Fg.Comillas() + "");
            string sSentencia = Sentencia.Replace("'", "" + Fg.Comillas() + "");

            sSql = string.Format("Exec spp_AIEF_RegistroLog '{0}', '{1}', '{2}', '{3}', '{4}' ", IdAccesoExterno, Opcion, sSentencia, sHost, sNombreHost);
            leer.Exec(sSql);
        }

        private DataSet EjecutarConsulta(int Opcion, string NombreTabla, string Sentencia)
        {
            DataSet dts = new DataSet();

            RegistrarLog(Opcion, 1, Sentencia); 
            if (!leer.Exec(NombreTabla, Sentencia)) 
            {
                dts = leer.ListaDeErrores(); 
            }
            else
            {
                leer.RenombrarDataSet("Dts_" + NombreTabla); 
                dts = leer.DataSetClase; 
            }

            return dts;
        }
        #endregion Funciones y Procedimientos Privados
    }
}
