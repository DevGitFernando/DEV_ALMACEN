using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

namespace DllFarmaciaSoft.Pedidos
{
    internal class clsGetPedidos 
    {
        #region Declaracion Variables
        
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerDet;
        clsLeer leerDet_Lotes;
        clsGrabarError Error; 

        string sIdEmpresa = "";
        string sIdEstado = "";
        string sIdFarmacia = ""; 

        #endregion Declaracion Variables


        #region Constructor y Destructor de Clase 
        public clsGetPedidos( clsDatosConexion DatosConexion, string Empresa, string Estado, string Farmacia )
        {
            ConexionLocal = new clsConexionSQL(DatosConexion);


            Error = new clsGrabarError(DatosConexion, DtGeneral.DatosApp, "clsGetPedidos");
            leer = new clsLeer(ref ConexionLocal);
            leerDet = new clsLeer(ref ConexionLocal);
            leerDet_Lotes = new clsLeer(ref ConexionLocal);   

            sIdEmpresa = Empresa;
            sIdEstado = Estado;
            sIdFarmacia = Farmacia; 
        } 
        #endregion Constructor y Destructor de Clase

        #region Funciones y Procedimientos Publicos  
        public DataSet PedidosCEDIS()
        {
            DataSet dtsRetorno = new DataSet();

            dtsRetorno = GetPedido("Pedidos_Cedis_Enc", "Pedidos_Cedis_Det"); 

            return dtsRetorno;
        }

        public DataSet PedidosDistribuidor()
        {
            DataSet dtsRetorno = new DataSet();

            dtsRetorno = GetPedido("PedidosOrdenDist_Enc", "PedidosOrdenDist_Det"); 

            return dtsRetorno;
        }

        private string ArmarQuery(string Encabezado, string Detalle, string Where, string Status, string Filtro)
        {
            string sSql = string.Format(
                            "Update E Set Status = '{2}' " +
                            "From {0} E (NoLock) " +
                            "   {1} and Status = '{3}' \n\n", Encabezado, Where, Status, Filtro);

            sSql += string.Format(
                            "Update D Set Status = '{2}' " +
                            "From {0} D (NoLock) " +
                "   {1} and Status = '{3}' \n\n", Detalle, Where, Status, Filtro); 

            return sSql; 
        }

        private DataSet GetPedido(string Encabezado, string Detalle)
        {
            DataSet dtsRetorno = new DataSet();
            Encabezado = Encabezado.Trim();
            Detalle = Detalle.Trim(); 

            string sWhere = 
                string.Format(
                    "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}'", 
                    sIdEmpresa, sIdEstado, sIdFarmacia);

            string sSql = ArmarQuery(Encabezado, Detalle, sWhere, "E", "A"); 

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "PedidosCEDIS()");
                dtsRetorno = leer.ListaDeErrores(); 
            }
            else
            {
                // sWhere; 

                sSql = string.Format(" Exec spp_CFG_ObtenerDatos '{0}', [ {1} and Status = 'E' ]  \n", Encabezado, sWhere );
                sSql += string.Format(" Exec spp_CFG_ObtenerDatos '{0}', [ {1} and Status = 'E' ]  \n", Detalle, sWhere);
                if (!leer.Exec(sSql)) 
                {
                    Error.GrabarError(leer, "GetPedido()"); 
                    dtsRetorno = leer.ListaDeErrores();
                }
                else
                {
                    leer.RenombrarTabla(1, Encabezado);
                    leer.RenombrarTabla(2, Detalle);  

                    dtsRetorno = leer.DataSetClase;

                    sSql = ArmarQuery(Encabezado, Detalle, sWhere, "R", "E");
                    if (!leer.Exec(sSql))
                    {
                        Error.GrabarError(leer, "GetPedido()");
                        dtsRetorno = leer.ListaDeErrores();
                    }
                }
            } 

            return dtsRetorno; 
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        #endregion Funciones y Procedimientos Privados 
    }
}
