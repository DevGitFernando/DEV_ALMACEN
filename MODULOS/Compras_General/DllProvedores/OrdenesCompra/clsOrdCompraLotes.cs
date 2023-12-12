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

using DllProveedores;
using DllProveedores.Lotes;

namespace DllProveedores.OrdenesCompra
{    

    public class clsOrdCompraLotes
    {
        #region Declaracion de Variables 
        DataTable dtTablaEnc = new DataTable("EncabezadoPedido");
        DataTable dtTablaDet_Lotes = new DataTable("DetallesLotes");
        
        clsConexionSQL ConexionLocal;
        clsLeer leer;// = new clsLeerWeb(General.Url, GnProveedores.DatosDelCliente);
        clsLeer leerExec;// = new clsLeerWeb(General.Url, GnProveedores.DatosDelCliente);
        clsGrabarError manError;
        string Name = "clsOrdCompraLotes";
        //Error = new clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);

        private clsDatosCliente datosCliente = new clsDatosCliente("", "", "", "");
        private clsDatosConexion pDatosCnn;
        private basGenerales Fg = new basGenerales();
        #endregion Declaracion de Variables

        #region Constructor 
        public clsOrdCompraLotes()
        {
            manError = new clsGrabarError();
            CrearTablas();
        }

        public clsOrdCompraLotes(DataSet dtsInformacionWeb, clsDatosConexion DatosConexion)
        {
            //manError = new clsGrabarError(DatosConexion, GnProveedores.DatosApp, this.Name);
            manError = new clsGrabarError(DatosConexion, new clsDatosApp("orden", ""), this.Name);
            manError.MostrarErrorAlGrabar = false;

            try
            {
                ConexionLocal = new clsConexionSQL(DatosConexion);
                leer = new clsLeer(ref ConexionLocal);
                leerExec = new clsLeer(ref ConexionLocal);                
                
                //this.pDatosCnn = new clsDatosConexion(dtsInformacionWeb.Tables["Conexion"]);
                dtTablaEnc = dtsInformacionWeb.Tables["EncabezadoPedido"];
                dtTablaDet_Lotes = dtsInformacionWeb.Tables["DetallesLotes"];
                
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
            dtTablaEnc.Columns.Add("Proveedor", Type.GetType("System.String"));
            dtTablaEnc.Columns.Add("Folio", Type.GetType("System.String"));
            dtTablaEnc.Columns.Add("Pedido", Type.GetType("System.String"));            
            dtTablaEnc.Columns.Add("ObservacionesProv", Type.GetType("System.String"));


            // Se agregan las columnas a la tabla de los detalles "DetallesPedido"
            dtTablaDet_Lotes.Columns.Add("NombreSP", Type.GetType("System.String"));
            dtTablaDet_Lotes.Columns.Add("Empresa", Type.GetType("System.String"));
            dtTablaDet_Lotes.Columns.Add("Estado", Type.GetType("System.String"));
            dtTablaDet_Lotes.Columns.Add("Farmacia", Type.GetType("System.String"));
            dtTablaDet_Lotes.Columns.Add("Folio", Type.GetType("System.String"));
            dtTablaDet_Lotes.Columns.Add("IdClaveSSA", Type.GetType("System.String"));
            dtTablaDet_Lotes.Columns.Add("CodigoEAN", Type.GetType("System.String"));            
            dtTablaDet_Lotes.Columns.Add("ClaveLote", Type.GetType("System.String"));            
            dtTablaDet_Lotes.Columns.Add("Cantidad", Type.GetType("System.String"));
            dtTablaDet_Lotes.Columns.Add("FechaCaducidad", Type.GetType("System.DateTime"));            
        }
        #endregion Crear Tablas 

        #region Interfaz WebService
        public DataSet ObtenerInformacionWeb()
        {
            DataSet dtsInformacionWeb = new DataSet("DtsInformacionPedidosWeb");

            try
            {
                dtsInformacionWeb.Tables.Add(dtTablaEnc);
                dtsInformacionWeb.Tables.Add(dtTablaDet_Lotes);                
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

        public void AgregarRenglonEncabezado(string NombreSP, string Empresa, string Estado, string Farmacia, string Proveedor, string Folio, string Pedido, string ObservacionesProveedor)
        {
            object[] Encabezado = { NombreSP, Empresa, Estado, Farmacia, Proveedor, Folio, Pedido, ObservacionesProveedor };
            dtTablaEnc.Rows.Add(Encabezado);
        }
        public void AgregarRenglonDetalles(string NombreSP, string Empresa, string Estado, string Farmacia, string Folio, string IdClaveSSA, string CodigoEAN, string ClaveLote, string Cantidad, DateTime FechaCad)
        {
            object[] Detalles = { NombreSP, Empresa, Estado, Farmacia, Folio, IdClaveSSA, CodigoEAN, ClaveLote, Cantidad, FechaCad };
            dtTablaDet_Lotes.Rows.Add(Detalles);            
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
                    ConexionLocal.DeshacerTransaccion();
                    manError.GrabarError(leerExec, "GuardarPedido");

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
                    "'" + leer.Campo("Pedido") + "', " +
                    "'" + leer.Campo("Proveedor") + "', " + 
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
            string sSql = "";

            leer.DataTableClase = dtTablaDet_Lotes;
            while (leer.Leer())
            {
                sSql = "Set Dateformat YMD Exec " + leer.Campo("NombreSP") + " "+ 
                    "'" + leer.Campo("Empresa") + "', " +
                    "'" + leer.Campo("Estado") + "', " +
                    "'" + leer.Campo("Farmacia") + "', " +
                    "'" + leer.Campo("Folio") + "', " +
                    "'" + leer.Campo("IdClaveSSA") + "', " +
                    "'" + leer.Campo("CodigoEAN") + "', " +
                    "'" + leer.Campo("ClaveLote") + "', " +
                    "'" + leer.Campo("Cantidad") + "', " +
                    "'" + General.FechaYMD(leer.CampoFecha("FechaCaducidad")) + "'";
                

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
