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
    public class clsGetCartaCanje
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerDet;
        clsLeer leerDet_Lotes;
        basGenerales Fg = new basGenerales();


        #region Declaracion Variables       

        DataSet dtsRetorno = new DataSet();
        DataSet dtsOrdenCompra = new DataSet();

        string Empresa = "", Estado = "", Almancen = ""; 
        string Tipo = "", Folio = "";
        string Origen = "";

        clsGrabarError Error;
        #endregion Declaracion Variables

        #region Constructor

        public clsGetCartaCanje(clsDatosConexion DatosConexion, string Empresa, string Estado, string Almancen, string Folio)
        {
            ConexionLocal = new clsConexionSQL(DatosConexion);
            leer = new clsLeer(ref ConexionLocal);
            leerDet = new clsLeer(ref ConexionLocal);
            leerDet_Lotes = new clsLeer(ref ConexionLocal);
            Error = new clsGrabarError();
 

            this.Empresa = Fg.PonCeros(Empresa, 3);
            this.Estado = Fg.PonCeros(Estado, 2);
            this.Almancen = Fg.PonCeros(Almancen, 4);
            this.Folio = Fg.PonCeros(Folio, 8);
        }
        #endregion Constructor  

        #region Funciones  
        public DataSet InformacionCartaCanje()
        {
            return InformacionCartaCanje(true);
        }

        public DataSet InformacionCartaCanje( bool bLocal)
        //public DataSet InformacionOrdenCompra()
        {
            dtsRetorno = new DataSet();
            if (EncabezadoCartaCanje())
            {
                if (DetallesCartaCanje())
                {
                   // GenerarInsertsCartaCanje();
                }
            }
            return dtsRetorno;
        }

        private bool EncabezadoCartaCanje()
        {
            bool bRegresa = false;
            string sSql = ""; 

            sSql = string.Format("Select * \n" +
                " From RutasDistribucionDet_CartasCanje ( nolock ) " +
                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioCarta = '{3}' ",
                Empresa, Estado, Almancen, Folio);

            ////"Select * \n" +
            ////"From RutasDistribucionDet_CartasCanje (NoLock) \n" +
            ////"Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioCarta = '{3}' \n",

            if(!leer.Exec("RutasDistribucionEnc", sSql)) 
            {
                dtsRetorno = leer.ListaDeErrores();
            }
            else
            {
                if (leer.Leer())
                {
                    dtsRetorno.Tables.Add(leer.DataTableClase.Copy());
                    bRegresa = true;
                }
                else
                {
                    bRegresa = false;
                }
            }
            return bRegresa;
        }

        private bool DetallesCartaCanje()
        {
            bool bRegresa = false;
            string sSql = "";

            sSql= string.Format("Select * \n" +
                    "From RutasDistribucionDet_CartasCanje ( Nolock ) " +
                    "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' and FolioCarta = '{3}' " +
                    "", Empresa, Estado, Almancen, Folio);

            if (!leerDet.Exec("RutasDistribucionDet_CartasCanje", sSql))
            {
                dtsRetorno = leerDet.ListaDeErrores();
            }
            else 
            {
                dtsRetorno.Tables.Add(leerDet.DataTableClase.Copy());
                bRegresa = true;
            }
            return bRegresa;
        }

        private bool GenerarInsertsCartaCanje()
        {
            bool bRegresa = true;
            return bRegresa;
        }
        #endregion Funciones
         
    }
}
