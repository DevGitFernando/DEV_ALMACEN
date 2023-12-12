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
    public class RecetaElectronica
    {
        clsDatosConexion datosDeConexion; 
        clsConexionSQL cnn;
        clsLeer leer;
        clsGrabarError Error;

        string sFolioRegistro = ""; 
        string sMensajesError = ""; 

        public RecetaElectronica(clsDatosConexion DatosConexion)
        {
            datosDeConexion = DatosConexion;
            cnn = new clsConexionSQL(datosDeConexion);
            leer = new clsLeer(ref cnn);

            Error = new clsGrabarError(datosDeConexion, Dll_SII_IMediaccess.GnDll_SII_IMediaccess.DatosApp, "RecetaElectronica"); 
        }

        public ResponsePublicacionReM Guardar(string Folio, string IdFarmacia, string Paciente, string Medico, string Especialidad,
            int Copago, string Plan, string Fecha, string Eligibilidad, string ICD1, string ICD2, string ICD3, string ICD4,
            string Datos_Receta)
        {
            ResponsePublicacionReM respuesta = new ResponsePublicacionReM();
            ////Producto[] Productos = null;
            ////string sEtiqueda_Receta = "<Datos_Receta>".ToUpper();
            ////string sEtiqueda_Receta_Fin = "</Datos_Receta>".ToUpper();
            ////string sEtiqueda_Producto = "<Producto>".ToUpper();
            ////string sEtiqueda_Producto_Fin = "</Producto>".ToUpper();
            ////string sEtiqueda_ID = "<Id>".ToUpper();
            ////string sEtiqueda_ID_Fin = "</Id>".ToUpper();
            ////string sEtiqueda_Cantidad = "<Cantidad>".ToUpper();
            ////string sEtiqueda_Cantidad_Fin = "</Cantidad>".ToUpper();

            try
            {
                ProductoReceta[] productosReceta = ProductoReceta.GetListaProductos(Datos_Receta);

                ////ArrayList listaProductos = new ArrayList();
                ////ProductoReceta producto = null;
                ////string[] stringListaProductos = null;
                ////string sProductos = Datos_Receta.ToUpper().Replace(" ", ""); 
                ////int iProducto = 0;
                ////string sRegistro = "";
                ////string[] item;

                ////sProductos = sProductos.Replace(sEtiqueda_Receta, "");
                ////sProductos = sProductos.Replace(sEtiqueda_Receta_Fin, "");
                ////sProductos = sProductos.Replace(sEtiqueda_Producto, "");
                ////sProductos = sProductos.Replace(sEtiqueda_Producto_Fin, "^");
                ////stringListaProductos = sProductos.Split('^');

                ////foreach (string sRenglon in stringListaProductos)
                ////{
                ////    if (sRenglon.Trim() != "")
                ////    {
                ////        producto = new ProductoReceta();
                ////        sRegistro = sRenglon.Replace(sEtiqueda_ID, "");
                ////        sRegistro = sRegistro.Replace(sEtiqueda_ID_Fin, "^");

                ////        sRegistro = sRegistro.Replace(sEtiqueda_Cantidad, "");
                ////        sRegistro = sRegistro.Replace(sEtiqueda_Cantidad_Fin, "^");
                ////        item = sRegistro.Split('^');

                ////        producto.Id = Convert.ToInt32("0" + item[0]);
                ////        producto.Cantidad = Convert.ToInt32("0" + item[1]);

                ////        listaProductos.Add(producto);
                ////    }
                ////}

                ////productosReceta = new ProductoReceta[listaProductos.Count];
                ////foreach (ProductoReceta p in listaProductos)
                ////{
                ////    productosReceta[iProducto] = p;
                ////    iProducto++;
                ////}

                respuesta = Guardar(Folio, IdFarmacia, Paciente, Medico, Especialidad, Copago, Plan, Fecha, Eligibilidad, ICD1, ICD2, ICD3, ICD4, productosReceta);
            }
            catch 
            {
            }

            return respuesta; 
        }

        public ResponsePublicacionReM Guardar(string Folio, string IdFarmacia, string Paciente, string Medico, string Especialidad,
            int Copago, string Plan, string Fecha, string Eligibilidad, string ICD1, string ICD2, string ICD3, string ICD4, 
            ProductoReceta []Datos_Receta)
        {
            ResponsePublicacionReM respuesta = new ResponsePublicacionReM();

            if (!ValidarDatosDeEntrada(Folio, IdFarmacia, Paciente, Medico, Especialidad, Copago, Plan, Fecha, Eligibilidad, ICD1, ICD2, ICD3, ICD4, Datos_Receta))
            {
                respuesta.Estatus = 1;
                respuesta.Error = sMensajesError; 
            }
            else
            {
                if (!ValidarFolioReceta(Folio))
                {
                    respuesta.Estatus = 2;
                    respuesta.Error = sMensajesError;
                }
                else
                {
                    if (!ValidarEligibilidad(Eligibilidad))
                    {
                        respuesta.Estatus = 3;
                        respuesta.Error = sMensajesError;
                    }
                    else
                    {
                        if (!Guardar_001_Informacion(Folio, IdFarmacia, Paciente, Medico, Especialidad, Copago, Plan, Fecha, Eligibilidad, ICD1, ICD2, ICD3, ICD4, Datos_Receta))
                        {
                            respuesta.Estatus = 4;
                            respuesta.Error = sMensajesError;
                        }
                        else
                        {
                            respuesta.Estatus = 0;
                            respuesta.Error = "Información guardada satisfactoriamente.";
                        }
                    }
                }
            }

            return respuesta; 
        }

        private bool ValidarFolioReceta(string Folio)
        {
            bool respuesta = true;
            string sSql = "";
            sMensajesError = "";

            sSql = string.Format("select top 1 Folio_MA From INT_MA__RecetasElectronicas_001_Encabezado (NoLock) Where Folio_MA = '{0}' ", Folio);  
            if (!leer.Exec(sSql))
            {
                respuesta = false;
                Error.GrabarError(leer, "ValidarFolioReceta");
                sMensajesError = string.Format("Error al validar el Folio de receta {0}.", Folio);
            }
            else
            {
                if (leer.Leer())
                {
                    respuesta = false;
                    sMensajesError = string.Format("Folio de receta {0} previamente registrado.", Folio);
                }
            }

            return respuesta;
        }

        private bool ValidarEligibilidad(string Eligibilidad) 
        {
            bool respuesta = true;
            string sSql = ""; 
            sMensajesError = "";

            sSql = string.Format("select top 1 Elegibilidad From INT_MA__RecetasElectronicas_001_Encabezado (NoLock) Where Elegibilidad = '{0}' ", Eligibilidad);  
            if (!leer.Exec(sSql))
            {
                respuesta = false;
                Error.GrabarError(leer, "ValidarEligibilidad");
                sMensajesError = string.Format("Error al validar la Elegibilidad {0}.", Eligibilidad);
            }
            else
            {
                if (leer.Leer())
                {
                    respuesta = false;
                    sMensajesError = string.Format("Elegibilidad {0} previamente registrada.", Eligibilidad);
                }
            }

            return respuesta;
        }

        private bool ValidarDatosDeEntrada(string Folio, string IdFarmacia, string Paciente, string Medico, string Especialidad,
            int Copago, string Plan, string Fecha, string Eligibilidad, string ICD1, string ICD2, string ICD3, string ICD4,
            ProductoReceta[] Datos_Receta)
        {
            bool respuesta = true;
            sMensajesError = "";

            if (Folio == null || Folio.Trim() == "" || Folio.Trim() == "0")
            {
                respuesta = false;
                sMensajesError += string.Format("Folio nulo ó en 0\n");
            }

            if (IdFarmacia == null || IdFarmacia.Trim() == "")
            {
                respuesta = false;
                sMensajesError += string.Format("IdFarmacia nulo ó vacio\n");
            }

            if (Paciente == null || Paciente.Trim() == "")
            {
                respuesta = false;
                sMensajesError += string.Format("Paciente nulo ó vacio\n");
            }

            if (Medico == null || Medico.Trim() == "")
            {
                respuesta = false;
                sMensajesError += string.Format("Medico nulo ó vacio\n");
            }

            if (Especialidad == null || Especialidad.Trim() == "")
            {
                respuesta = false;
                sMensajesError += string.Format("Especialidad nula ó vacia\n");
            }

            if (Copago == null)
            {
                respuesta = false;
                sMensajesError += string.Format("Copago nulo0\n");
            }

            if (Plan == null || Plan.Trim() == "")
            {
                respuesta = false;
                sMensajesError += string.Format("Plan nulo ó vacio\n");
            }

            if (Fecha == null || Fecha.Trim() == "")
            {
                respuesta = false;
                sMensajesError += string.Format("Fecha nula ó vacia\n");
            }

            if (Eligibilidad == null || Eligibilidad.Trim() == "")
            {
                respuesta = false;
                sMensajesError += string.Format("Eligibilidad nula ó vacia\n");
            }

            if (ICD1 == null || ICD1.Trim() == "")
            {
                respuesta = false;
                sMensajesError += string.Format("Diagnóstico CIE-10 nulo ó vacio\n");
            }

            if (Datos_Receta == null)
            {
                respuesta = false;
                sMensajesError += string.Format("No se detectaron productos requeridos para la receta.\n");
            }

            return respuesta; 
        }

        private bool Guardar_001_Informacion(string Folio, string IdFarmacia, string Paciente, string Medico, string Especialidad,
            int Copago, string Plan, string Fecha, string Eligibilidad, string ICD1, string ICD2, string ICD3, string ICD4,
            ProductoReceta[] Datos_Receta)
        {
            bool respuesta = true;
            string sSql = "";
            sMensajesError = "";

            if (!cnn.Abrir())
            {
                respuesta = false;
                sMensajesError = "Error al guardar la información";
            }
            else
            {
                cnn.IniciarTransaccion();

                respuesta = Guardar_002_Encabezado(Folio, IdFarmacia, Paciente, Medico, Especialidad, Copago, Plan, Fecha, Eligibilidad, ICD1, ICD2, ICD3, ICD4, Datos_Receta);

                if (!respuesta)
                {
                    Error.GrabarError(leer, "Guardar_001_Informacion");
                    cnn.DeshacerTransaccion(); 
                }
                else
                {
                    cnn.CompletarTransaccion(); 
                }
            }

            return respuesta;
        }

        private bool Guardar_002_Encabezado(string Folio, string IdFarmacia, string Paciente, string Medico, string Especialidad,
            int Copago, string Plan, string Fecha, string Eligibilidad, string ICD1, string ICD2, string ICD3, string ICD4,
            ProductoReceta[] Datos_Receta)
        {
            bool respuesta = true;
            string sSql = "";
            sMensajesError = "";

            sSql = string.Format("Exec spp_INT_MA__RecetasElectronicas_001_Encabezado " + 
                " @Folio_MA = '{0}', @IdFarmacia = '{1}', @NombrePaciente = '{2}', @NombreMedico = '{3}', @Especialidad = '{4}', @Copago = '{5}', " + 
                " @PlanBeneficiario = '{6}', @FechaEmision = '{7}', @Elegibilidad = '{8}', " +
                " @CIE_01 = '{9}', @CIE_02 = '{10}', @CIE_03 = '{11}', @CIE_04 = '{12}', @EsRecetaManual = '{13}' ",
                Folio, IdFarmacia, Paciente, Medico, Especialidad, Copago, Plan, Fecha, Eligibilidad, ICD1, ICD2, ICD3, ICD4, 0); 

            if (!leer.Exec(sSql))
            {
                respuesta = false;
                sMensajesError = "Error al guardar la información de receta"; 
            }
            else
            {
                if (!leer.Leer())
                {
                    respuesta = false;
                    sMensajesError = "Folio no generado"; 
                }
                else
                {
                    sFolioRegistro = leer.Campo("Folio");
                }
            }

            if (respuesta)
            {
                respuesta = Guardar_003_Detalles(Folio, Datos_Receta); 
            }

            return respuesta; 
        }

        private bool Guardar_003_Detalles(string Folio, ProductoReceta[] Datos_Receta)
        {
            bool respuesta = true;
            string sSql = "";
            int iPartida = 0; 
            sMensajesError = "";


            foreach (ProductoReceta p in Datos_Receta)
            {
                iPartida++; 
                sSql = string.Format("Exec spp_INT_MA__RecetasElectronicas_002_Productos " + 
                    " @Folio_MA = '{0}', @Partida = '{1}', @CodigoEAN = '{2}', @CantidadSolicitada = '{3}' ",
                    Folio, iPartida, p.Id, p.Cantidad); 
                if (!leer.Exec(sSql))
                {
                    respuesta = false;
                    sMensajesError = "Error al guardar la información de detalle de receta";
                    break; 
                }
            }

            return respuesta;
        }
    }
}
