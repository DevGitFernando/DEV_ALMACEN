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

    public class clsConfirmarPedidoContado
    {
        #region Declaracion de Variables 
        DataTable dtTablaEnc = new DataTable("EncabezadoPedido");
        DataTable dtTablaDet = new DataTable("DetallesPedido");
        clsConexionSQL ConexionLocal;
        clsLeer leer;// = new clsLeerWeb(General.Url, GnProveedores.DatosDelCliente);
        clsLeer leerExec;// = new clsLeerWeb(General.Url, GnProveedores.DatosDelCliente);
        clsGrabarError manError;
        //Error = new clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);

        private clsDatosCliente datosCliente = new clsDatosCliente("", "", "", "");
        private clsDatosConexion pDatosCnn;
        private basGenerales Fg = new basGenerales();
        #endregion Declaracion de Variables

        #region Constructor 
        public clsConfirmarPedidoContado()
        {
            CrearTablas();
        }

        public clsConfirmarPedidoContado(DataSet dtsInformacionWeb, clsDatosConexion DatosConexion)
        {
            clsGrabarError manError = new clsGrabarError();

            try
            {
                ConexionLocal = new clsConexionSQL(DatosConexion);
                leer = new clsLeer(ref ConexionLocal);
                leerExec = new clsLeer(ref ConexionLocal);                
                
                //this.pDatosCnn = new clsDatosConexion(dtsInformacionWeb.Tables["Conexion"]);
                dtTablaEnc = dtsInformacionWeb.Tables["EncabezadoPedido"];
                dtTablaDet = dtsInformacionWeb.Tables["DetallesPedido"];
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
            dtTablaEnc.Columns.Add("Folio", Type.GetType("System.String"));
            dtTablaEnc.Columns.Add("FechaPromesaEntrega", Type.GetType("System.String"));
            dtTablaEnc.Columns.Add("ObservacionesProv", Type.GetType("System.String"));


            // Se agregan las columnas a la tabla de los detalles "DetallesPedido"
            dtTablaDet.Columns.Add("NombreSP", Type.GetType("System.String"));
            dtTablaDet.Columns.Add("Empresa", Type.GetType("System.String"));
            dtTablaDet.Columns.Add("Estado", Type.GetType("System.String"));
            dtTablaDet.Columns.Add("Folio", Type.GetType("System.String"));
            dtTablaDet.Columns.Add("Producto", Type.GetType("System.String"));
            dtTablaDet.Columns.Add("CodigoEAN", Type.GetType("System.String"));
            dtTablaDet.Columns.Add("CantidadConfirmada", Type.GetType("System.String"));
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

        public void AgregarRenglonEncabezado(string NombreSP, string Empresa, string Estado, string Folio, string FechaPromesaEntrega, string ObservacionesProveedor)
        {
            object[] Encabezado = { NombreSP, Empresa, Estado, Folio, FechaPromesaEntrega, ObservacionesProveedor };
            dtTablaEnc.Rows.Add(Encabezado);
        }
        public void AgregarRenglonDetalles(string NombreSP, string Empresa, string Estado, string Folio, string Producto, string CodigoEAN, string CantidadConfirmada)
        {
            object[] Detalles = { NombreSP, Empresa, Estado, Folio, Producto, CodigoEAN, CantidadConfirmada };
            dtTablaDet.Rows.Add(Detalles);            
        }

        public bool GuardarPedido()
        {
            bool bContinua = false;

            if (ConexionLocal.Abrir())
            {
                ConexionLocal.IniciarTransaccion();

                if (GuardarEncabezado())
                {
                    bContinua = GuardarDetalle();
                }

                if (bContinua)
                {
                    ConexionLocal.CompletarTransaccion();
                }
                else
                {
                    manError.GrabarError(leer, "GuardarPedido");
                    ConexionLocal.DeshacerTransaccion(); 
                }
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
                    "'" + leer.Campo("Folio") + "', " + 
                    "'" + leer.Campo("FechaPromesaEntrega") + "', " + 
                    "'" + leer.Campo("ObservacionesProv") + "'";

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
            string sSql = "Set Dateformat YMD Exec ";

            leer.DataTableClase = dtTablaDet;
            while (leer.Leer())
            {
                sSql = sSql + leer.Campo("NombreSP") + " "+ 
                    "'" + leer.Campo("Empresa") + "', " +
                    "'" + leer.Campo("Estado") + "', " +
                    "'" + leer.Campo("Folio") + "', " +
                    "'" + leer.Campo("Producto") + "', " +
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
