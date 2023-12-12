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
using DllProveedores.ConfirmarPedidos;

namespace DllProveedores.Clases
{
    class clsConfirmarCodigosEAN
    {
        #region Declaracion de Variables

        private enum ColsEAN
        {
            IdClaveSSA = 0, CodigoEAN = 1, Descripcion = 2, Cantidad = 3
        }

        DataTable dtTablaEAN = new DataTable("CodigosEAN");
        clsConexionSQL ConexionLocal;
        clsLeerWeb leer = new clsLeerWeb(General.Url, GnProveedores.DatosDelCliente);
        clsGrabarError manError;
        FrmConfirmarCodigosEAN CapturaCodigos;
        string sCantidadSurtible = "";

        private basGenerales Fg = new basGenerales();
        #endregion Declaracion de Variables

        #region Constructores
        public clsConfirmarCodigosEAN()
        {
            CrearTabla();
        }
        #endregion Constructores

        #region Crear Tabla
        private void CrearTabla()
        {
            //Se agregan las columnas a la tabla CodigosEAN
            dtTablaEAN.Columns.Add("IdClaveSSA", Type.GetType("System.String"));
            dtTablaEAN.Columns.Add("CodigoEAN", Type.GetType("System.String"));
            dtTablaEAN.Columns.Add("DescripcionProducto", Type.GetType("System.String"));
            dtTablaEAN.Columns.Add("Cantidad", Type.GetType("System.String"));
        }
        #endregion Crear Tabla 

        #region Propiedades Publicas
        public DataSet ListaDeErrores
        {
            get
            {
                return leer.ListaDeErrores();
            }
        }

        public string CantidadSurtible
        {
            get
            {
                return sCantidadSurtible;
            }
        }
        #endregion Propiedades Publicas

        #region Funciones y Procedimientos Publicos

        public void AgregarRenglon(string IdClaveSSA, string CodigoEAN, string DescripcionProducto, string Cantidad)
        {
            object[] Codigos = { IdClaveSSA, CodigoEAN, DescripcionProducto, Cantidad };
            dtTablaEAN.Rows.Add(Codigos);
        }

        public void MostrarPantalla(string sIdEmpresa, string sIdEstado, string sIdClaveSSA, string sDescripcion, int iCantidadRequerida, string sPedido )
        {
            DataSet dtsCodigos = new DataSet("dtsCodigosEAN");
            DataTable dtCodigosSal = new DataTable();
            DataTable dtCodigosNuevos = dtTablaEAN.Clone();

            dtCodigosSal = ObtenerCodigosSal(sIdEmpresa, sIdEstado, sIdClaveSSA, sPedido);
            dtsCodigos.Tables.Add(dtCodigosSal);

            // Se muestra la pantalla.
            CapturaCodigos = new FrmConfirmarCodigosEAN();
            CapturaCodigos.MostrarDetalle(sIdClaveSSA, sDescripcion, iCantidadRequerida, dtsCodigos);

            //Se actualizan los codigos.
            dtsCodigos.Tables.Remove(dtCodigosSal);
            
            dtCodigosNuevos = CapturaCodigos.ObtenerTablaEAN();
            if (dtCodigosNuevos != null)
            {
                RemoverEAN(sIdClaveSSA);
                ActualizarTablaEAN(dtCodigosNuevos, sIdClaveSSA);                
            }
            sCantidadSurtible = ObtenerCantidadSurtible(sIdClaveSSA);
        }

        private DataTable ObtenerCodigosSal(string sIdEmpresa, string sIdEstado, string IdClaveSSA, string sPedido)
        {
            int iCodigosEan_Sal = 0;
            DataTable dtCodigosSal = dtTablaEAN.Clone();
            string sFiltro = string.Format("IdClaveSSA = '{0}' ", IdClaveSSA);

            foreach (DataRow dtRow in dtTablaEAN.Select(sFiltro))
            {
                iCodigosEan_Sal++;
                dtCodigosSal.Rows.Add(dtRow.ItemArray);
            }

            if (iCodigosEan_Sal == 0) // Si la sal no tiene Codigos EAN asignados, se cargan los que solicito el comprador.
            {
                dtCodigosSal = ObtenerCodigosSal_Solicitados(sIdEmpresa, sIdEstado, IdClaveSSA, sPedido);
            }
            return dtCodigosSal;
        }

        private DataTable ObtenerCodigosSal_Solicitados(string sIdEmpresa, string sIdEstado, string IdClaveSSA, string sPedido)
        {
            DataTable dtCodigosSal = dtTablaEAN.Clone();

            string sSql = String.Format("Select IdClaveSSA, CodigoEAN, Descripcion as DescripcionProducto , Cantidad_Confirmada  " +
                " From vw_COM_OCEN_Pedidos_Proveedor_Claves_CodigosEAN(NoLock) " +
                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdProveedor = '{2}' And Folio = '{3}' And IdClaveSSA = '{4}' ",
                sIdEmpresa, sIdEstado, GnProveedores.IdProveedor, sPedido, IdClaveSSA);

            if (!leer.Exec(sSql))
            {
                // manError.GrabarError(leer, "");
                General.msjError("Ocurrió un error los datos del Pedido");
            }
            else
            {
                if (leer.Leer())
                {
                    dtCodigosSal = leer.DataTableClase;
                }
                else
                {
                    General.msjUser("El Pedido ingresado no contiene detalles. Verifique");
                }
            }

            return dtCodigosSal;
        }

        private void ActualizarTablaEAN(DataTable dtCodigosNuevos, string IdClaveSSA)
        {
            string sFiltro = string.Format("IdClaveSSA = '{0}' ", IdClaveSSA);

            if (dtCodigosNuevos != null)
            {
                foreach (DataRow dtRow in dtCodigosNuevos.Select(sFiltro))
                {
                    dtTablaEAN.Rows.Add(dtRow.ItemArray);
                }
            }
        }

        public void RemoverEAN(string IdClaveSSA)
        {
            string sFiltro = string.Format("IdClaveSSA = '{0}' ", IdClaveSSA);

            foreach (DataRow dtRow in dtTablaEAN.Select(sFiltro))
            {
                dtTablaEAN.Rows.Remove(dtRow);
            }
        }

        private string ObtenerCantidadSurtible(string IdClaveSSA)
        {
            int iCantidadConfirmada = 0;
            string sFiltro = string.Format("IdClaveSSA = '{0}' ", IdClaveSSA);
            string sCantidadConfirmada = "";

            //if (dtCodigosNuevos != null)
            {
                foreach (DataRow dtRow in dtTablaEAN.Select(sFiltro))
                {
                    iCantidadConfirmada = iCantidadConfirmada + Convert.ToInt32(dtRow[(int)ColsEAN.Cantidad].ToString());
                }
            }

            sCantidadConfirmada = iCantidadConfirmada.ToString();

            return sCantidadConfirmada;
        }

        public DataTable ObtenerTablaEAN()
        {
            return dtTablaEAN;
        }
        
        #endregion Funciones y Procedimientos Publicos

    }

}
