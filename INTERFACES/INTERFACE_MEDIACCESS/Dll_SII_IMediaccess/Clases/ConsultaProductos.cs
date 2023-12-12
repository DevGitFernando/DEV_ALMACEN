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

using DllFarmaciaSoft;
using Dll_SII_IMediaccess.wsClases; 

namespace Dll_SII_IMediaccess.Clases
{
    public class ConsultaProductos
    {
        clsDatosConexion datosDeConexion; 
        clsConexionSQL cnn;
        clsLeer leer;
        clsGrabarError Error;
        basGenerales Fg = new basGenerales(); 

        string sFolioRegistro = ""; 
        string sMensajesError = "";

        public ConsultaProductos(clsDatosConexion DatosConexion)
        {
            datosDeConexion = DatosConexion;
            cnn = new clsConexionSQL(datosDeConexion);
            leer = new clsLeer(ref cnn);

            Error = new clsGrabarError(datosDeConexion, Dll_SII_IMediaccess.GnDll_SII_IMediaccess.DatosApp, "RecetaElectronica"); 
        }

        private string GetString_ValidarDisponibilidadExistencia(ProductoReceta[] listaProductos)
        {
            string sRegresa = "";

            foreach (ProductoReceta p in listaProductos)
            {
                sRegresa += p.GetStringValidaDisponibilidadExistencia(); 
            }

            sRegresa = sRegresa.Trim();
            sRegresa = Fg.Left(sRegresa, sRegresa.Length - 1); 

            return sRegresa; 
        }

        public ResponseBusquedaMedicamento ValidarDisponibilidadExistencia(string IdFarmacia, string Datos_Receta)
        {
            ResponseBusquedaMedicamento respuesta = new ResponseBusquedaMedicamento();
            ProductoReceta[] listaProductos = ProductoReceta.GetListaProductos(Datos_Receta);
            ProductoReceta producto = null;
            int iProducto = 0;
            string sRegresa = "";
            string sListaDeProductos = GetString_ValidarDisponibilidadExistencia(listaProductos);
            string sSql = ""; 


            sSql = string.Format("Exec spp_INT_MA__ValidarExistenciaDisponibleProductos @IdFarmacia = '{0}', @Productos = '{1}' ", 
                IdFarmacia, sListaDeProductos);


            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ValidarDisponibilidadExistencia");
                respuesta.Estatus = 1;
                respuesta.Error = "Error al obtener la información solicitada";
            }
            else
            {
                leer.Leer();
                {
                    respuesta.Estatus = leer.CampoInt("Estatus");
                    respuesta.Error = respuesta.Estatus == 0 ? "Todos los productos pueden ser surtidos por completo" : "Algunos productos no se pueden surtir por completo";
                    leer.RegistroActual = 1;

                    listaProductos = new ProductoReceta[leer.Registros];

                    while (leer.Leer())
                    {
                        producto = new ProductoReceta();
                        producto.Id = leer.Campo("IdProducto");
                        producto.Cantidad = leer.CampoInt("CantidadFaltante");

                        listaProductos[iProducto] = producto;
                        iProducto++;

                        /////sListaDeProductos += producto.GetString(); 
                    }

                    respuesta.ListaDeProductosRecetas = listaProductos;
                }
            }


            return respuesta;
        }

        public ResponseBusquedaMedicamento Consultar(int Id, int Tipo, string Busqueda, string IdFarmacia, int Ranking)
        {
            ResponseBusquedaMedicamento respuesta = new ResponseBusquedaMedicamento();
            Producto[] listaProductos = null;
            Producto producto = null;
            int iProducto = 0;
            string sRegresa = ""; 
            string sListaDeProductos = "";

            string sSql = string.Format("Exec spp_INT_MA__ConsultarProductos " + 
                " @Id_Producto = '{0}', @Tipo = '{1}', @Consulta = '{2}', @IdFarmacia = '{3}', @Ranking = '{4}' ",
                Id, Tipo, Busqueda, IdFarmacia, Ranking);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Consultar");
                respuesta.Estatus = 1;
                respuesta.Error = "Error al obtener la información solicitada";
            }
            else
            {
                if (!leer.Leer())
                {
                    respuesta.Estatus = 2;
                    respuesta.Error = "No se encontro información con los criteros solicitados";
                }
                else
                {
                    respuesta.Estatus = 0;
                    respuesta.Error = "Se encontro información con los criteros solicitados";
                    leer.RegistroActual = 1;

                    listaProductos = new Producto[leer.Registros];

                    while (leer.Leer())
                    {
                        producto = new Producto();
                        producto.Id = leer.Campo("IdProducto"); 
                        producto.Glosa = leer.Campo("NombreComercial");
                        producto.GlosaSubClase = leer.Campo("DescripcionGenerica");
                        producto.Existencias = leer.CampoInt("Existencia");
                        producto.Ranking = leer.CampoInt("Ranking");
                        producto.EAN = leer.Campo("CodigoEAN");

                        listaProductos[iProducto] = producto;
                        iProducto++;

                        /////sListaDeProductos += producto.GetString(); 
                    }

                    respuesta.ListaDeProductos = listaProductos; 
                }
            }

            return respuesta; 
        }
    }
}
