using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

namespace DllFarmaciaSoft.OrdenesDeCompra
{
    public class clsGetOrdenDeCompra
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerDet;
        clsLeer leerDet_Lotes;

        #region Declaracion Variables       

        DataSet dtsRetorno = new DataSet();
        DataSet dtsOrdenCompra = new DataSet();

        string Empresa = "", Estado = "", Destino = "", Folio = "";
        string Origen = "";

        clsGrabarError Error;
        #endregion Declaracion Variables

        #region Constructor

        public clsGetOrdenDeCompra(clsDatosConexion DatosConexion, string Empresa, string Estado, string Origen, string Destino, string Folio)
        {
            ConexionLocal = new clsConexionSQL(DatosConexion);
            leer = new clsLeer(ref ConexionLocal);
            leerDet = new clsLeer(ref ConexionLocal);
            leerDet_Lotes = new clsLeer(ref ConexionLocal);
            Error = new clsGrabarError();
 

            this.Empresa = Empresa;
            this.Estado = Estado;
            this.Origen = Origen;
            this.Destino = Destino;
            this.Folio = Folio;
        }

        #endregion Constructor      

        #region Funciones  
        public DataSet InformacionOrdenCompra()
        {
            return InformacionOrdenCompra(true);
        }

        public DataSet InformacionOrdenCompra(bool bLocal)
        //public DataSet InformacionOrdenCompra()
        {
            dtsRetorno = new DataSet();
            if (EncabezadoOrdenCompra())
            {
                if (DetallesOrdenCompra())
                {
                    GenerarInsertsOC();
                }
            }
            return dtsRetorno;
        }

        private bool EncabezadoOrdenCompra()
        {
            bool bRegresa = false;

            string sSql = string.Format(" Select IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, " +
                " Folio, IdProveedor, Proveedor, EstadoEntrega, NomEstadoEntrega, EntregarEn, FarmaciaEntregarEn, IdPersonal, NombrePersonal, " +
                " FechaRegistro, FechaRequeridaEntrega, Status " +
                " From vw_OrdenesCompras_Claves_Enc ( nolock ) " +
                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And EntregarEn = '{2}' And Folio = '{3}' and Status = 'OC' ",
                Empresa, Estado, Destino, Folio);



            sSql = string.Format(" Select IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, " +
                " Folio, IdProveedor, Proveedor, EstadoEntrega, NomEstadoEntrega, EntregarEn, FarmaciaEntregarEn, IdPersonal, NombrePersonal, " +
                " FechaRegistro, FechaRequeridaEntrega, Status, 1 As PermiteDescarga " + //, dbo.fg_OrdenDeCompraPermiteDescarga('{0}', '{1}', '{2}', '{3}') As PermiteDescarga" +
                " From vw_OrdenesCompras_Claves_Enc ( nolock ) " +
                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And EntregarEn = '{2}' And Folio = '{3}' ",
                Empresa, Estado, Destino, Folio);
            if(Origen != "")
            {
                sSql = string.Format(" Select IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, " +
                    " Folio, IdProveedor, Proveedor, EstadoEntrega, NomEstadoEntrega, EntregarEn, FarmaciaEntregarEn, IdPersonal, NombrePersonal, " +
                    " FechaRegistro, FechaRequeridaEntrega, Status, 1 As PermiteDescarga " + //, dbo.fg_OrdenDeCompraPermiteDescarga('{0}', '{1}', '{2}', '{3}') As PermiteDescarga" +
                    " From vw_OrdenesCompras_Claves_Enc ( nolock ) " +
                    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And EntregarEn = '{3}' And Folio = '{4}' ",
                    Empresa, Estado, Origen, Destino, Folio);
            }


            if(!leer.Exec("OrdenesDeComprasEnc", sSql)) 
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

        private bool DetallesOrdenCompra()
        {
            bool bRegresa = false;

            string sSql = "";

            sSql= string.Format(
                "Select \n" + 
                "\tCodigoEAN, IdProducto, Descripcion, TasaIva, \n" +
                "\tCantidadCajas, Cantidad as Piezas, 0 As Cantidad, PrecioUnitario As Costo, 0 As Importe, 0 As ImporteIva, 0 As ImporteTotal \n" +
                "From vw_OrdenesCompras_CodigosEAN_Det ( Nolock ) \n" +
                "Where IdEmpresa = '{0}' And IdEstado = '{1}' And Folio = '{2}' \n" +
                "Order By Descripcion \n", Empresa, Estado, Folio); 

            if(Origen != "")
            {
                sSql =
                    string.Format(
                        "Select \n" +
                        "\tCodigoEAN, IdProducto, Descripcion, TasaIva, \n" +
                        "\tCantidadCajas, Cantidad as Piezas, 0 As Cantidad, PrecioUnitario As Costo, 0 As Importe, 0 As ImporteIva, 0 As ImporteTotal \n" +
                        "From vw_OrdenesCompras_CodigosEAN_Det ( Nolock ) \n" +
                        "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' and Folio = '{3}' \n" +
                        "Order By Descripcion \n", Empresa, Estado, Origen, Folio);
            }


            if (!leerDet.Exec("OrdenesDeComprasDet", sSql))
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

        private bool GenerarInsertsOC()
        {
            bool bRegresa = false;
            string sSql = "", sTablaEnc = "", sTablaDet = "";

            sTablaEnc = "COM_OCEN_OrdenesCompra_Claves_Enc";
            sTablaDet = "COM_OCEN_OrdenesCompra_CodigosEAN_Det";

            sSql = string.Format(" Exec spp_CFG_ObtenerDatos '{0}', [ Where IdEmpresa = '{2}' And IdEstado = '{3}' And EntregarEn = '{4}' And FolioOrden = '{5}' and Status = 'OC' ] " +
                                 " Exec spp_CFG_ObtenerDatos '{1}', [ Where IdEmpresa = '{2}' And IdEstado = '{3}' And FolioOrden = '{5}' ] ",
                                 sTablaEnc, sTablaDet, Empresa, Estado, Destino, Folio );

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

                DataSet dts = new DataSet();
                dts.Tables.Add(dtsRetorno.Tables["OrdenesDeComprasEnc"].Copy());
                leerDet.DataSetClase = dts;

                leerDet.Leer();
                //bool b = leerDet.CampoBool("PermiteDescarga");

                //if (!bLocal && leerDet.CampoBool("PermiteDescarga"))
                //{
                //    bRegresa = SiguienteStatus("", true);
                //}

                bRegresa = true; 
            }

            return bRegresa;
        }

        public bool SiguienteStatus(string IdStatus, bool EsAlmacen)
        {
            bool bRegresa = true;
            string sWhere = "PermiteDescarga = 1 And ModificaCompras = 0";

            if (!EsAlmacen)
            {
                sWhere = string.Format("IdStatus > '{0}'", IdStatus);
            }

            string sSql = string.Format("Select Top 1 * From Cat_StatusDeOrdenesDeCompras Where Status = 'A' And {0}", sWhere);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "SiguienteStatus()");
                General.msjError("Ocurrió un error al obtener el codigo del siguiente status.");
                bRegresa = false;
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjAviso("No existe un nivel para incrementar");
                }
                else
                {
                    if (!leer.CampoBool("ModificaCompras") && !EsAlmacen)
                    {
                        General.msjAviso(string.Format("El nivel {0} no lo puede ingresar el area de compras", leer.Campo("Nombre")));
                    }
                    else
                    {
                        bRegresa = AplicarSiguiente(Empresa, Estado, Destino, Folio, leer.Campo("IdStatus"), EsAlmacen);
                    }
                }
            }
            return bRegresa;
        }

        private bool AplicarSiguiente(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string IdSiguiente, bool EsAlmacen)
        {
            bool bRegresa = true;
            int iEsAlmacen = EsAlmacen ? 1 : 0;
            string sIdper = "0001";
            string sSql = string.Format("Exec spp_OrdenesCompra_Claves_Status '{0}', '{1}', '{2}', '{3}', '{4}','{5}', '{6}', {7}",
                            IdEmpresa, IdEstado, IdFarmacia, Folio, sIdper, IdSiguiente, "", iEsAlmacen);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "AplicarSiguiente()");
                General.msjError("Ocurrió un error al aumentar al siguiente status.");
            }
            return bRegresa;
        }  

        #endregion Funciones
         
    }
}
