using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

namespace DllProveedores.Clases
{    

    public class clsConfirmarPedidoCredito
    {
        #region Declaracion de Variables 
        DataTable dtTablaEnc = new DataTable("EncabezadoPedido");
        DataTable dtTablaDet = new DataTable("DetallesPedido");
        DataTable dtTablaEAN = new DataTable("CodigosEANPedido");
        clsConexionSQL ConexionLocal;
        clsLeer leer;// = new clsLeerWeb(General.Url, GnProveedores.DatosDelCliente);
        clsLeer leerExec;// = new clsLeerWeb(General.Url, GnProveedores.DatosDelCliente);
        clsGrabarError manError;
        string Name = "clsConfirmarPedidoCredito";
        //Error = new clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);

        private clsDatosCliente datosCliente = new clsDatosCliente("", "", "", "");
        private clsDatosConexion pDatosCnn;
        private basGenerales Fg = new basGenerales();
        #endregion Declaracion de Variables

        #region Constructor 
        public clsConfirmarPedidoCredito()
        {
            manError = new clsGrabarError();
            CrearTablas();
        }

        public clsConfirmarPedidoCredito(DataSet dtsInformacionWeb, clsDatosConexion DatosConexion)
        {
            // manError = new clsGrabarError(DatosConexion, GnProveedores.DatosApp, this.Name);
            manError = new clsGrabarError(DatosConexion, new clsDatosApp("PrePedido", ""), this.Name);
            manError.MostrarErrorAlGrabar = false;

            try
            {
                ConexionLocal = new clsConexionSQL(DatosConexion);
                leer = new clsLeer(ref ConexionLocal);
                leerExec = new clsLeer(ref ConexionLocal);                
                
                //this.pDatosCnn = new clsDatosConexion(dtsInformacionWeb.Tables["Conexion"]);
                dtTablaEnc = dtsInformacionWeb.Tables["EncabezadoPedido"];
                dtTablaDet = dtsInformacionWeb.Tables["DetallesPedido"];
                dtTablaEAN = dtsInformacionWeb.Tables["CodigosEANPedido"];
            }
            catch (Exception ex)
            {
                manError.LogError("[ Error al guardar la informacion.... " + ex.Message + "] ");
            }
        }
        #endregion Constructor 

        #region Crear Tablas
        private void CrearTablas()
        {
            // Se agregan las columnas a la tabla del encabezado "EncabezadoPedido"
            dtTablaEnc.Columns.Add("NombreSP", Type.GetType("System.String"));
            dtTablaEnc.Columns.Add("Empresa", Type.GetType("System.String"));
            dtTablaEnc.Columns.Add("Estado", Type.GetType("System.String"));
            dtTablaEnc.Columns.Add("Farmacia", Type.GetType("System.String"));
            dtTablaEnc.Columns.Add("Folio", Type.GetType("System.String"));
            dtTablaEnc.Columns.Add("FechaPromesaEntrega", Type.GetType("System.String"));
            dtTablaEnc.Columns.Add("ObservacionesProv", Type.GetType("System.String"));
            dtTablaEnc.Columns.Add("Opcion", Type.GetType("System.String"));


            // Se agregan las columnas a la tabla de los detalles "DetallesPedido"
            dtTablaDet.Columns.Add("NombreSP", Type.GetType("System.String"));
            dtTablaDet.Columns.Add("Empresa", Type.GetType("System.String"));
            dtTablaDet.Columns.Add("Estado", Type.GetType("System.String"));
            dtTablaDet.Columns.Add("Farmacia", Type.GetType("System.String"));
            dtTablaDet.Columns.Add("Folio", Type.GetType("System.String"));
            dtTablaDet.Columns.Add("IdClaveSSA", Type.GetType("System.String"));
            dtTablaDet.Columns.Add("ClaveSSA", Type.GetType("System.String"));
            dtTablaDet.Columns.Add("CantidadConfirmada", Type.GetType("System.String"));

            // Se agregan las columnas a la tabla de los detalles "DetallesPedido"
            dtTablaEAN.Columns.Add("NombreSP", Type.GetType("System.String"));
            dtTablaEAN.Columns.Add("Empresa", Type.GetType("System.String"));
            dtTablaEAN.Columns.Add("Estado", Type.GetType("System.String"));
            dtTablaEAN.Columns.Add("Farmacia", Type.GetType("System.String"));
            dtTablaEAN.Columns.Add("Folio", Type.GetType("System.String"));
            dtTablaEAN.Columns.Add("IdClaveSSA", Type.GetType("System.String"));
            dtTablaEAN.Columns.Add("CodigoEAN", Type.GetType("System.String"));
            dtTablaEAN.Columns.Add("CantidadConfirmada", Type.GetType("System.String"));
        }
        #endregion Crear Tablas 

        #region Interfaz WebService
        public DataSet ObtenerInformacionWeb()
        {
            DataSet dtsInformacionWeb = new DataSet("DtsInformacionPedidosWeb");

            try
            {
                dtsInformacionWeb.Tables.Add(dtTablaEnc);
                dtsInformacionWeb.Tables.Add(dtTablaDet);
                dtsInformacionWeb.Tables.Add(dtTablaEAN);
            }
            catch
            {
                dtsInformacionWeb = new DataSet("DtsInformacionReporteWeb");
            }

            return dtsInformacionWeb;
        }

        #endregion Interfaz WebService

        #region Propiedades Publicas 
        public DataSet ListaDeErrores
        {
            get
            {
                return leerExec.ListaDeErrores();
            }
        }
        #endregion Propiedades Publicas

        #region Funciones y Procedimientos Publicos

        public void AgregarRenglonEncabezado(string NombreSP, string Empresa, string Estado, string Farmacia, string Folio, string FechaPromesaEntrega, string ObservacionesProveedor, string Opcion)
        {
            object[] Encabezado = { NombreSP, Empresa, Estado, Farmacia, Folio, FechaPromesaEntrega, ObservacionesProveedor, Opcion };
            dtTablaEnc.Rows.Add(Encabezado);
        }
        public void AgregarRenglonDetalles(string NombreSP, string Empresa, string Estado, string Farmacia, string Folio, string IdClaveSSA, string ClaveSSA, string CantidadConfirmada)
        {
            object[] Detalles = { NombreSP, Empresa, Estado, Farmacia, Folio, IdClaveSSA, ClaveSSA, CantidadConfirmada };
            dtTablaDet.Rows.Add(Detalles);            
        }
        public void AgregarRenglonCodigosEAN(string NombreSP, string Empresa, string Estado, string Farmacia, string Folio, string IdClaveSSA, string CodigoEAN, string CantidadConfirmada)
        {
            object[] CodigosEAN = { NombreSP, Empresa, Estado, Farmacia, Folio, IdClaveSSA, CodigoEAN, CantidadConfirmada };
            dtTablaEAN.Rows.Add(CodigosEAN);
        }

        public bool GuardarPedido()
        {
            bool bContinua = false;

            if (ConexionLocal.Abrir())
            {
                ConexionLocal.IniciarTransaccion();

                if (GuardarEncabezado())
                {
                    if (GuardarDetalle())
                    {
                        bContinua = GuardarCodigosEAN();
                    }
                }

                if (bContinua)
                {
                    ConexionLocal.CompletarTransaccion();
                }
                else
                {
                    manError.GrabarError(leerExec, "GuardarPedido"); 
                    ConexionLocal.DeshacerTransaccion(); 

                }

                ConexionLocal.Cerrar();
            }
            else
            {
                bContinua = false;
            }

            return bContinua;
        }
        private bool GuardarEncabezado()
        {
            bool bRegresa = true;
            string sSql = "Set Dateformat YMD Exec ";

            leer.DataTableClase = dtTablaEnc;
            if( leer.Leer())
            {
                sSql = sSql + leer.Campo("NombreSP") + " " +
                    "'" + leer.Campo("Empresa") + "', " + 
                    "'" + leer.Campo("Estado") + "', " +
                    "'" + leer.Campo("Farmacia") + "', " + 
                    "'" + leer.Campo("Folio") + "', " + 
                    "'" + leer.Campo("FechaPromesaEntrega") + "', " + 
                    "'" + leer.Campo("ObservacionesProv") + "', " +
                    "'" + leer.Campo("Opcion") + "'";

                if (!leerExec.Exec(sSql))
                {
                    bRegresa = false;
                }

            }
            return bRegresa;
        }

        private bool GuardarDetalle()
        {
            bool bRegresa = true;
            string sSql = "";

            leer.DataTableClase = dtTablaDet;
            while (leer.Leer())
            {
                sSql = "Set Dateformat YMD Exec " + leer.Campo("NombreSP") + " "+ 
                    "'" + leer.Campo("Empresa") + "', " +
                    "'" + leer.Campo("Estado") + "', " +
                    "'" + leer.Campo("Farmacia") + "', " + 
                    "'" + leer.Campo("Folio") + "', " +
                    "'" + leer.Campo("IdClaveSSA") + "', " +
                    "'" + leer.Campo("CantidadConfirmada") + "'";

                if (!leerExec.Exec(sSql))
                {
                    bRegresa = false;
                    break;
                }

            }
            return bRegresa;
        }

        private bool GuardarCodigosEAN()
        {
            bool bRegresa = true;
            string sSql = "";

            leer.DataTableClase = dtTablaEAN;
            while (leer.Leer())
            {
                sSql = "Set Dateformat YMD Exec " + leer.Campo("NombreSP") + " " +
                    "'" + leer.Campo("Empresa") + "', " +
                    "'" + leer.Campo("Estado") + "', " +
                    "'" + leer.Campo("Farmacia") + "', " +                      
                    "'" + leer.Campo("Folio") + "', " +
                    "'" + leer.Campo("IdClaveSSA") + "', " +
                    "'" + leer.Campo("CodigoEAN") + "', " +
                    "'" + leer.Campo("CantidadConfirmada") + "'";

                if (!leerExec.Exec(sSql))
                {
                    bRegresa = false;
                    break;
                }

            }
            return bRegresa;
        }
        #endregion Funciones y Procedimientos Publicos 
    }
}
