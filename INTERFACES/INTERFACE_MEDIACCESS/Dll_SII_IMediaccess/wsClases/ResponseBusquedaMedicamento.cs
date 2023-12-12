using System;
using System.Collections.Generic;
using System.Text;

namespace Dll_SII_IMediaccess.wsClases
{
    public class ResponseBusquedaMedicamento
    {
        Producto[] listaDeProductos;
        ProductoReceta[] listaDeProductosReceta;

        int iEstatus = 0;
        string sError = "";

        public ResponseBusquedaMedicamento()
        {
        }

        #region Propiedades Publicas
        public Producto[] ListaDeProductos
        {
            get { return listaDeProductos; }
            set { listaDeProductos = value; }
        }

        public ProductoReceta[] ListaDeProductosRecetas
        {
            get { return listaDeProductosReceta; }
            set { listaDeProductosReceta = value; }
        }

        public int Estatus
        {
            get { return iEstatus; }
            set { iEstatus = value; }
        }

        public string Error
        {
            get { return sError; }
            set { sError = value; }
        }
        #endregion Propiedades Publicas 

        #region Funciones y Procedimientos Publicos 
        public string GetString()
        {
            string sRegresa = "";

            sRegresa += string.Format("<{0}>{1}</{0}>", "Estatus", iEstatus);
            sRegresa += string.Format("<{0}>{1}</{0}>", "Error", sError);

            if (listaDeProductos != null)
            {
                if (listaDeProductos.Length > 0)
                {
                    sRegresa += string.Format("<{0}>", "Productos");
                    foreach (Producto p in listaDeProductos)
                    {
                        sRegresa += p.GetString();
                    }
                    sRegresa += string.Format("</{0}>", "Productos");
                }
            }

            return sRegresa;
        }

        public string GetString_Receta()
        {
            string sRegresa = "";

            sRegresa += string.Format("<{0}>{1}</{0}>", "Estatus", iEstatus);
            sRegresa += string.Format("<{0}>{1}</{0}>", "Error", sError);

            if (listaDeProductosReceta != null)
            {
                if (listaDeProductosReceta.Length > 0)
                {
                    sRegresa += string.Format("<{0}>", "Productos");
                    foreach (ProductoReceta p in listaDeProductosReceta)
                    {
                        sRegresa += p.GetString();
                    }
                    sRegresa += string.Format("</{0}>", "Productos");
                }
            }

            return sRegresa;
        }
        #endregion Funciones y Procedimientos Publicos 
    }
}
