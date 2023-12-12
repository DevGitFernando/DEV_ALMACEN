using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Data;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Diagnostics;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Usuarios_y_Permisos;

namespace Dll_SII_IMediaccess.wsClases
{
    public class ProductoReceta
    {
        string iId = "0";
        int iCantidad = 0;

        #region Propiedades Publicas 
        public string Id
        {
            get { return iId; }
            set { iId = value; }
        }

        public int Cantidad
        {
            get { return iCantidad; }
            set { iCantidad = value; }
        }
        #endregion Propiedades Publicas

        #region Funciones y Procedimentos Publicos 
        public string GetString()
        {
            string sRegresa = "";

            sRegresa += string.Format("<{0}>", "Producto");

            sRegresa += string.Format("<{0}>{1}</{0}>", "Id", iId);
            sRegresa += string.Format("<{0}>{1}</{0}>", "Faltante", iCantidad);

            sRegresa += string.Format("</Producto>");

            return sRegresa;
        }

        public string GetStringValidaDisponibilidadExistencia()
        {
            string sRegresa = string.Format("{0}*{1}|", iId, iCantidad);
            return sRegresa;
        }

        public static ProductoReceta[] GetListaProductos(string Datos_Receta)
        {
            Producto[] Productos = null;
            ProductoReceta[] productosReceta = null;
            string sEtiqueda_Receta = "<Datos_Receta>".ToUpper();
            string sEtiqueda_Receta_Fin = "</Datos_Receta>".ToUpper();
            string sEtiqueda_Producto = "<Producto>".ToUpper();
            string sEtiqueda_Producto_Fin = "</Producto>".ToUpper();
            string sEtiqueda_ID = "<Id>".ToUpper();
            string sEtiqueda_ID_Fin = "</Id>".ToUpper();
            string sEtiqueda_Cantidad = "<Cantidad>".ToUpper();
            string sEtiqueda_Cantidad_Fin = "</Cantidad>".ToUpper();

            try
            {
                ArrayList listaProductos = new ArrayList(); 
                ProductoReceta producto = null;
                string[] stringListaProductos = null;
                string sProductos = Datos_Receta.ToUpper().Replace(" ", "");
                int iProducto = 0;
                string sRegistro = "";
                string[] item;

                sProductos = sProductos.Replace(sEtiqueda_Receta, "");
                sProductos = sProductos.Replace(sEtiqueda_Receta_Fin, "");
                sProductos = sProductos.Replace(sEtiqueda_Producto, "");
                sProductos = sProductos.Replace(sEtiqueda_Producto_Fin, "^");
                stringListaProductos = sProductos.Split('^');

                foreach (string sRenglon in stringListaProductos)
                {
                    if (sRenglon.Trim() != "")
                    {
                        producto = new ProductoReceta();
                        sRegistro = sRenglon.Replace(sEtiqueda_ID, "");
                        sRegistro = sRegistro.Replace(sEtiqueda_ID_Fin, "^");

                        sRegistro = sRegistro.Replace(sEtiqueda_Cantidad, "");
                        sRegistro = sRegistro.Replace(sEtiqueda_Cantidad_Fin, "^");
                        item = sRegistro.Split('^');

                        producto.Id = item[0];
                        producto.Cantidad = Convert.ToInt32("0" + item[1]);

                        listaProductos.Add(producto);
                    }
                }

                productosReceta = new ProductoReceta[listaProductos.Count];
                foreach (ProductoReceta p in listaProductos)
                {
                    productosReceta[iProducto] = p;
                    iProducto++;
                }
            }
            catch 
            {
            }

            return productosReceta;
        }
        #endregion Funciones y Procedimentos Publicos
    }
}
