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
    internal class clsDescargarPedido
    {

        ////////DataSet dtsOrdenCompra;
        //////OrdenesWeb.Timeout = 300000; 
        ////////// OrdenesWeb.Url = "http://intermed.homeip.net/wsComprasTest/wsOficinaCentral.asmx";  
        //////dtsOrdenCompra = OrdenesWeb.InformacionOrdenCompra(sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtOrden.Text.Trim(), 8));
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        string sUrl = ""; 
        string sIdEmpresa = ""; 
        string sIdEstado = ""; 
        string sIdFarmacia = "";
        string sFolioReferenciaPedido = "";


        basGenerales Fg = new basGenerales(); 

        bool bPedidoDescargado = false;
        bool bOrdenGuardada = true;
        DataSet dtsDatosOrden = new DataSet();
        clsLeer leerOrden = new clsLeer();

        string sDatos_001_Enc = "Encabezado";
        string sDatos_002_Det = "Detalles";
        // string sDatos_003_Lotes = "Lotes";
        string sEncabezadoOC = "EncabezadoOC";
        string sDetalleOC = "DetalleOC";

        #region Constructores y Destructores de Clase 
        public clsDescargarPedido(clsDatosConexion DatosConexion, string Url, string Empresa, string Estado, string Farmacia, string Folio)
        {
            ConexionLocal = new clsConexionSQL(DatosConexion);
            leer = new clsLeer(ref ConexionLocal);

            this.sUrl = Url;
            this.sIdEmpresa = Fg.PonCeros(Empresa, 3);
            this.sIdEstado = Fg.PonCeros(Estado, 2);
            this.sIdFarmacia = Fg.PonCeros(Farmacia, 4);
            this.sFolioReferenciaPedido = Folio;           
        }
        #endregion Constructores y Destructores de Clase

        #region Propiedades Publicas 
        public string Empresa
        {
            get { return sIdEmpresa; }
        }

        public string Estado
        {
            get { return sIdEstado; }
        }

        public string Farmacia
        {
            get { return sIdFarmacia; }
        }

        public string Folio
        {
            get { return sFolioReferenciaPedido; }
        }

        public bool OrdenDescargada
        {
            get { return bPedidoDescargado; }
        }

        public bool OrdenGuardada
        {
            get { return bOrdenGuardada; }
        }

        public DataSet Encabezado 
        {
            get { return GetDatos(sDatos_001_Enc); }
        }

        public DataSet Detalles 
        {
            get { return GetDatos(sDatos_002_Det); }
        }

        public DataSet Lotes
        {
            get { return GetDatos(sDatos_002_Det); }
        }

        public DataSet EncabezadoOC
        {
            get { return GetDatos(sEncabezadoOC); }
        }

        public DataSet DetalleOC
        {
            get { return GetDatos(sDetalleOC); }
        }
        #endregion Propiedades Publicas

        #region Funciones y Procedimientos Publicos 
        public bool Descargar()
        {
            FrmDescargarPedidoUnidad f = new FrmDescargarPedidoUnidad(sUrl, sIdEmpresa, sIdEstado, sIdFarmacia, sFolioReferenciaPedido);

            bPedidoDescargado = false;
            leerOrden = new clsLeer(); 
            dtsDatosOrden = new DataSet(); 


            if (f.Descargar())
            {
                bPedidoDescargado = true;
                leerOrden.DataSetClase = f.dtsDatosOrden; 
                dtsDatosOrden = f.dtsDatosOrden;

                leerOrden.RenombrarTabla("INT_ND_PedidosEnc", sDatos_001_Enc);
                leerOrden.RenombrarTabla("INT_ND_PedidosDet", sDatos_002_Det);
                // leerOrden.RenombrarTabla("OrdenesDeComprasDet", sDatos_003_Lotes);
                leerOrden.RenombrarTabla("INT_ND_Pedidos_Enviados", sEncabezadoOC);
                leerOrden.RenombrarTabla("INT_ND_Pedidos_Enviados_Det", sDetalleOC);
            }

            if (bPedidoDescargado)
            {
                if (!RevisarStatus_Pedido())
                {
                    bPedidoDescargado = false; 
                }
                else
                {
                    if (!RevisarInformacionDestino())
                    {
                        bPedidoDescargado = false;
                    }
                    else 
                    {
                        bPedidoDescargado = true;
                        bOrdenGuardada = InsertaOrdenPedido();
                    }
                }
            }
           
            return bPedidoDescargado; 

        } 
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private bool RevisarStatus_Pedido()
        {
            bool bRegresa = false;

            string sStatus = "";
            string sMsj = ""; 
            clsLeer datos = new clsLeer();
            datos.DataSetClase = this.Encabezado;

            if (!datos.Leer())
            {
                General.msjUser("No se encontro el Pedido solicitado, verifique.");
            }
            else
            {
                sStatus = datos.Campo("Status").ToUpper();
                bRegresa = true;

                if (!bRegresa)
                {
                    if (sStatus == "OC")
                    {
                        bRegresa = true;
                    }
                    else
                    {
                        if (sStatus == "C")
                        {
                            sMsj = "La Orden de Compra solicitada esta cancelada.";
                        }

                        if (sStatus == "A")
                        {
                            sMsj = "La Orden de Compra solicitada no ha sido colocada.";
                        }

                        General.msjUser(sMsj);
                    }
                }
            }

            return bRegresa; 
        }

        private bool RevisarInformacionDestino()
        {
            bool bRegresa = false;
            string sIdUnidad = sIdEstado + sIdFarmacia;
            string sIdUnidad_OrdCom = "";
            string sMsj = "";
            
            clsLeer datos = new clsLeer(); 
            datos.DataSetClase = this.Encabezado; 

            if (!datos.Leer())
            {
                General.msjUser("No se encontro el Pedido solicitado, verifique.");
            }
            else
            {
                bRegresa = true; 
                sIdUnidad_OrdCom = datos.Campo("IdEstado") + datos.Campo("IdFarmaciaPedido");

                if (sIdUnidad != sIdUnidad_OrdCom)
                {
                    bRegresa = false; 
                    sMsj = string.Format(
                        "El folio [ {0} ] de Pedido no pertenece a esta Unidad, \n" +
                        "pertenece a {1}, {2} -- {3}  ",
                        sFolioReferenciaPedido, datos.Campo("Estado"),
                        datos.Campo("IdEstado"), datos.Campo("IdFarmaciaPedido")); 
                    General.msjAviso(sMsj);
                }
            }

            return bRegresa; 
        }

        private DataSet GetDatos(string Tabla)
        {
            DataSet dts = new DataSet();

            if (bPedidoDescargado)
            {
                dts.Tables.Add(leerOrden.Tabla(Tabla).Copy());
            }

            return dts; 
        }
        #endregion Funciones y Procedimientos Privados 

        #region Insertar informacion de pedido descargado 
        private bool InsertaOrdenPedido()
        {
            bool bContinua = false;

            if (ConexionLocal.Abrir())
            {
                ConexionLocal.IniciarTransaccion();

                if (GuardaOCEnc())
                {
                    bContinua = GuardaOCDet();
                }

                if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                {
                    ConexionLocal.CompletarTransaccion();
                }
                else
                {
                    ConexionLocal.DeshacerTransaccion();
                    General.Error.GrabarError(leer, "InsertaOrdenPedido()");
                }

                ConexionLocal.Cerrar();
            }
            else
            {
                ////Error.LogError(ConexionLocal.MensajeError);
                ////General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo.");
            }

            return bContinua;
        }

        private bool GuardaOCEnc()
        {
            bool bRegresa = true;
            string sSql = "";
            clsLeer leerEnc = new clsLeer();
            leerEnc.DataSetClase = EncabezadoOC;

            if (leerEnc.Leer())
            {
                sSql = leerEnc.Campo("Resultado");

                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                }
            }
            else
            {
                bRegresa = false;
            }

            return bRegresa;
        }

        private bool GuardaOCDet()
        {
            bool bRegresa = true;
            string sSql = "";
            clsLeer leerDet = new clsLeer();
            leerDet.DataSetClase = DetalleOC;

            while (leerDet.Leer())
            {
                sSql = leerDet.Campo("Resultado");

                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    break;
                }
            }

            return bRegresa;
        }
        #endregion Insertar informacion de pedido descargado
    }
}
