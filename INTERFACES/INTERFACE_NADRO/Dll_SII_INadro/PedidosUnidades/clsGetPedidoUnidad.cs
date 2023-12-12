using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

namespace Dll_SII_INadro.PedidosUnidades
{
    public class clsGetPedidoUnidad
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerDet;
        clsLeer leerDet_Lotes;

        #region Declaracion Variables       

        DataSet dtsRetorno = new DataSet();
        DataSet dtsOrdenCompra = new DataSet();

        string Empresa = "";
        string Estado = "";
        string Origen = "";
        string FolioPedido = ""; 
        string Destino = "";
        string Folio = "";


        string sEncabezado = "INT_ND_PedidosEnc";
        string sDetalle = "INT_ND_PedidosDet"; 

        #endregion Declaracion Variables

        #region Constructor 
        public clsGetPedidoUnidad(clsDatosConexion DatosConexion, string Empresa, string Estado, string Destino, string Folio)
        {
            ConexionLocal = new clsConexionSQL(DatosConexion);
            leer = new clsLeer(ref ConexionLocal);
            leerDet = new clsLeer(ref ConexionLocal);
            leerDet_Lotes = new clsLeer(ref ConexionLocal);   

            this.Empresa = Empresa;
            this.Estado = Estado;
            this.Destino = Destino;
            this.Folio = Folio;
        } 
        #endregion Constructor      

        #region Propiedades 
        public string Encabezado
        {
            get { return sEncabezado; }
        }

        public string Detalle
        {
            get { return sDetalle; }
        }
        #endregion Propiedades

        #region Funciones
        public DataSet InformacionPedido()
        {
            if (EncabezadoPedido())
            {
                if (DetallesPedido()) 
                {
                    GenerarInsertsPedido();
                }
            }
            return dtsRetorno;
        }

        private bool EncabezadoPedido()
        {
            bool bRegresa = false; 
            string sSql = ""; 

            sSql = string.Format(" Select IdEmpresa, IdEstado, IdFarmacia, Farmacia, Folio, TipoDeFarmacias, " +
                " IdProveedor, Proveedor, FechaRegistro, FechaPromesaEntrega as FechaRequeridaEntrega, Status, CodigoCliente, NombreCliente, " + 
                " ReferenciaFolioPedido, " + 
                " IdFarmaciaPedido, FarmaciaPedido " +
                " From vw_INT_ND_PedidosUnidades ( nolock ) " + 
                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmaciaPedido = '{2}' And ReferenciaFolioPedido = '{3}' and Status = 'V' ",
                Empresa, Estado, Destino, Folio);

            if (!leer.Exec(sEncabezado, sSql)) 
            {
                dtsRetorno = leer.ListaDeErrores();
            }
            else
            {
                if (leer.Leer())
                {
                    Origen = leer.Campo("IdFarmacia");
                    FolioPedido = leer.Campo("Folio");
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

        private bool DetallesPedido()
        {
            bool bRegresa = false; 
            string sSql = ""; 

            sSql =
                string.Format(" Select CodigoEAN, IdProducto, Descripcion, TasaIva, " +
                    " CantidadCajas, Cantidad as Piezas, 0 As Cantidad, PrecioUnitario As Costo, 0 As Importe, 0 As ImporteIva, 0 As ImporteTotal " +
                    " From vw_INT_ND_PedidosUnidades ( Nolock ) " +
                    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And ReferenciaFolioPedido = '{2}' " +
                    " Order By Descripcion ", Empresa, Estado, Folio);

            if (!leerDet.Exec(sDetalle, sSql))
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

        private bool GenerarInsertsPedido()
        {
            bool bRegresa = false;
            string sSql = "", sTablaEnc = "", sTablaDet = "";

            sTablaEnc = "INT_ND_Pedidos_Enviados";
            sTablaDet = "INT_ND_Pedidos_Enviados_Det"; 

            sSql = string.Format(
                " Exec spp_CFG_ObtenerDatos @Tabla = '{0}', @Criterio = [ Where IdEmpresa = '{2}' And IdEstado = '{3}' And IdFarmacia = '{4}' And FolioPedido = '{5}' ] \n" +
                " Exec spp_CFG_ObtenerDatos @Tabla = '{1}', @Criterio = [ Where IdEmpresa = '{2}' And IdEstado = '{3}' " +
                " And IdFarmaciaPedido = '{6}' And FolioPedido = '{5}' ], @CriterioAux = [ and ReferenciaPedido = '{7}' ] ",
                sTablaEnc, sTablaDet, Empresa, Estado, Origen, FolioPedido, Destino, Folio);

            if (!leerDet.Exec(sSql)) 
            {
                dtsRetorno = leerDet.ListaDeErrores();
            }
            else
            {
                //dtsRetorno.Tables.Add(leerDet.DataTableClase.Copy());
                leerDet.RenombrarTabla(1, sTablaEnc);
                leerDet.RenombrarTabla(2, sTablaDet);

                dtsRetorno.Tables.Add(leerDet.Tabla(1).Copy());
                dtsRetorno.Tables.Add(leerDet.Tabla(2).Copy()); 

                bRegresa = true; 
            }

            return bRegresa;
        }
        #endregion Funciones
         
    }
}
