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
using SC_SolutionsSystem.FTP; 

using DllFarmaciaSoft;
using Dll_SII_IMediaccess; 

namespace Dll_SII_IMediaccess.ExportarInformacion
{
    public class clsEnviarCatalogos
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrabarError Error; 

        clsFTP FTP = null;
        bool bFTP_Configurado = false;

        string sUrl_FTP = "";
        string sUsuario_FTP = "";
        string sPassword_FTP = "";
        bool bModoActivoDeTransferencia = false;

        string sMarcaDeTiempo = ""; 
        string sRuta = Application.StartupPath + @"\CATALOGOS_MEDIACCESS\";
        string sFile_Productos = "CatalogoProductos";
        string sFile_Productos_Presentacion = "CatalogoProductosConPresentacion";
        string sFile_Productos_Presentacion_MSAI = "PSP MSAI";
        string sFile_Productos_ListaDePrecios = "CatalogoProductosListaDePrecios";

        string sFile_Productos__Base = "CatalogoProductos.txt";
        string sFile_Productos_Presentacion__Base = "CatalogoProductosConPresentacion.txt";
        string sFile_Productos_Presentacion_MSAI__Base = "PSP MSAI.txt";
        string sFile_Productos_ListaDePrecios__Base = "CatalogoProductosListaDePrecios";

        public clsEnviarCatalogos()
        {
            clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, GnDll_SII_IMediaccess.DatosApp, "clsEnviarCatalogos"); 

            GetConexionFTP(0);

            if (!Directory.Exists(sRuta))
            {
                Directory.CreateDirectory(sRuta); 
            }

            sMarcaDeTiempo = MarcaDeTiempo(); 
            ////sFile_Productos += "__"+ General.FechaYMD(DateTime.Now, "") + ".txt";
            ////sFile_Productos_Presentacion += "__" + General.FechaYMD(DateTime.Now, "") + ".txt";
            ////sFile_Productos_Presentacion_MSAI += "__" + General.FechaYMD(DateTime.Now, "") + ".txt";


            sFile_Productos += sMarcaDeTiempo; // "__" + General.FechaYMD(DateTime.Now, "") + ".txt";
            sFile_Productos_Presentacion += sMarcaDeTiempo; // "__" + General.FechaYMD(DateTime.Now, "") + ".txt";
            sFile_Productos_Presentacion_MSAI += sMarcaDeTiempo; // "__" + General.FechaYMD(DateTime.Now, "") + ".txt"; 
            sFile_Productos_ListaDePrecios += sMarcaDeTiempo;

        }

        #region Propiedades 
        public string Usuario
        {
            get { return sUsuario_FTP; }
            set { sUsuario_FTP = value; }
        }

        public string Password
        {
            get { return sPassword_FTP; }
            set { sPassword_FTP = value; }
        }

        public string Url
        {
            get { return sUrl_FTP; }
            set { sUrl_FTP  = value; }
        }
        #endregion Propiedades

        #region Funciones y Procedimientos Publicos 
        public void Enviar()
        {
            if (GnDll_SII_IMediaccess.Enviar_Catalogo_Productos)
            {
                System.Console.WriteLine("");
                System.Console.WriteLine("");
                generarProductos();


                System.Console.WriteLine("");
                System.Console.WriteLine("");
                generarProductosPresentacion();
            }

            if (GnDll_SII_IMediaccess.Enviar_Catalogo_Precios)
            {
                System.Console.WriteLine("");
                System.Console.WriteLine("");

                generarProductos_ListaDePrecios();
            }
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private string MarcaDeTiempo()
        {
            DateTime dateTime = DateTime.Now; 
            string sRegresa = "";
            string sFecha = General.FechaYMD(dateTime, "");
            string sHora = General.Hora(dateTime, "");

            sRegresa = string.Format("____{0}__{1}.txt", sFecha, sHora); 

            return sRegresa; 
        }

        private string LimpiarCadena(string Cadena)
        {
            string sRegresa = Cadena.ToUpper();
            string consignos = "áàäéèëíìïóòöúùuñÁÀÄÉÈËÍÌÏÓÒÖÚÙÜÑçÇ";
            string sinsignos = "aaaeeeiiiooouuunAAAEEEIIIOOOUUUNcC";

            for (int i = 0; i <= consignos.Length - 1; i++)
            {
                sRegresa = sRegresa.Replace(consignos[i], sinsignos[i]);
            }

            return sRegresa; 
        }

        private bool generarProductos()
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_INT_MA__CAT_Productos ");
            string sSalida = ""; 
            StreamWriter file;

            System.Console.WriteLine("Obteniendo información de Productos");
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "generarProductos()"); 
            }
            else
            {
                if (leer.Leer())
                {
                    leer.RegistroActual = 1;
                    file = new StreamWriter(Path.Combine(sRuta, sFile_Productos), false, Encoding.Default);
                    System.Console.WriteLine("Generando archivo de Productos");

                    while(leer.Leer())
                    {
                        sSalida = string.Format("{0}\t", leer.Campo("Referencia_Clinica_MA"));
                        sSalida += string.Format("{0}\t", leer.Campo("IdProducto"));
                        sSalida += string.Format("{0}\t", leer.Campo("CodigoEAN"));
                        sSalida += string.Format("{0}\t", leer.Campo("NombreComercial"));
                        sSalida += string.Format("{0}\t", leer.Campo("DescripcionClave"));
                        sSalida += string.Format("{0}\t", leer.Campo("Preferente"));

                        file.WriteLine(sSalida); 
                    }
                    file.Close(); 
                    file = null;

                    System.Console.WriteLine("Enviando archivo de Productos");
                    enviarArchivo(1, sFile_Productos, sFile_Productos__Base); 
                }
            }

            return bRegresa; 
        }

        private bool generarProductosPresentacion()
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_INT_MA__CAT_ProductosConPresentacion ");
            string sSalida = ""; 
            StreamWriter file;
            StreamWriter file_02;

            System.Console.WriteLine("Obteniendo archivo de Productos con Presentación");
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "generarProductosPresentacion()"); 
            }
            else
            {
                if (leer.Leer())
                {
                    leer.RegistroActual = 1;
                    file = new StreamWriter(Path.Combine(sRuta, sFile_Productos_Presentacion), false, Encoding.Default);
                    file_02 = new StreamWriter(Path.Combine(sRuta, sFile_Productos_Presentacion_MSAI), false, Encoding.Default);

                    System.Console.WriteLine("Generando archivo de Productos con Presentación");

                    while (leer.Leer())
                    {
                        sSalida = string.Format("{0}\t", leer.Campo("Referencia_Clinica_MA"));
                        sSalida += string.Format("{0}\t", leer.Campo("IdProductoPCP"));
                        sSalida += string.Format("{0}\t", leer.Campo("CodigoEAN"));
                        sSalida += string.Format("{0}\t", leer.Campo("NombreComercial"));
                        sSalida += string.Format("{0}\t", leer.Campo("DescripcionClave"));
                        sSalida += string.Format("{0}\t", leer.Campo("IdProducto"));

                        file.WriteLine(sSalida);

                        sSalida = string.Format("{0}\t", leer.Campo("Referencia_Clinica_MA"));
                        sSalida += string.Format("{0}\t", leer.Campo("IdProductoPCP"));
                        sSalida += string.Format("{0}\t", leer.Campo("CodigoEAN"));
                        sSalida += string.Format("{0}\t", leer.Campo("NombreComercial"));
                        sSalida += string.Format("{0}\t", leer.Campo("DescripcionClave"));
                        sSalida += string.Format("{0}\t", leer.Campo("Preferente"));

                        file_02.WriteLine(sSalida); 

                    }
                    file.Close();
                    file = null;

                    file_02.Close();
                    file_02 = null; 


                    System.Console.WriteLine("Enviando archivo de Productos con Presentación");
                    enviarArchivo(1, sFile_Productos_Presentacion, sFile_Productos_Presentacion__Base);

                    System.Console.WriteLine("Enviando archivo de Productos MSAI");
                    enviarArchivo(1, sFile_Productos_Presentacion_MSAI, sFile_Productos_Presentacion_MSAI__Base);

                }
            }

            return bRegresa; 
        }

        private bool generarProductos_ListaDePrecios()
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_INT_MA__CAT_Productos_ListaDePrecios ");
            string sSalida = "";
            StreamWriter file;
            StreamWriter file_02;

            System.Console.WriteLine("Obteniendo archivo de Productos Lista de Precios");
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "generarProductos_ListaDePrecios()");
            }
            else
            {
                if (leer.Leer())
                {
                    sFile_Productos_ListaDePrecios = string.Format("{0}", sFile_Productos_ListaDePrecios, leer.Campo("Referencia_Clinica_MA"), leer.Campo("FechaVigencia"));
                    sFile_Productos_ListaDePrecios__Base = string.Format("{0}{1}.txt", leer.Campo("Referencia_Clinica_MA"), leer.Campo("FechaVigencia"));
                    leer.RegistroActual = 1;

                    file = new StreamWriter(Path.Combine(sRuta, sFile_Productos_ListaDePrecios), false, Encoding.Default);

                    System.Console.WriteLine("Generando archivo de Productos Lista de Precios");
                    while (leer.Leer())
                    {
                        sSalida = string.Format("{0}|", leer.Campo("Referencia_Clinica_MA"));
                        sSalida += string.Format("{0}|", leer.Campo("CodigoEAN"));
                        sSalida += string.Format("{0}|", leer.Campo("IdProducto"));
                        sSalida += string.Format("{0}|", leer.Campo("DescripcionClave"));
                        sSalida += string.Format("{0}|", leer.Campo("NombreComercial"));
                        sSalida += string.Format("{0}|", leer.Campo("Laboratorio"));
                        sSalida += string.Format("{0}|", leer.Campo("Descuento"));
                        sSalida += string.Format("{0}|", leer.Campo("PrecioMaximoPublico"));
                        sSalida += string.Format("{0}|", leer.Campo("Controlado"));
                        sSalida += string.Format("{0}|", leer.CampoInt("DiaVigencia").ToString("#0"));
                        sSalida += string.Format("{0}|", leer.CampoInt("MesVigencia").ToString("#0"));
                        sSalida += string.Format("{0}|", leer.CampoInt("AñoVigencia").ToString("###0"));
                        sSalida += string.Format("{0}|", leer.Campo("KeyWord"));
                        sSalida += string.Format("{0}|", leer.Campo("GravaIVA"));
                        sSalida += string.Format("{0}|", leer.Campo("GrupoTerapeutico"));


                        sSalida = LimpiarCadena(sSalida); 
                        file.WriteLine(sSalida);

                    }
                    file.Close();
                    file = null;

                    System.Console.WriteLine("Enviando archivo de Productos Lista de Precios");
                    enviarArchivo(3, sFile_Productos_ListaDePrecios, sFile_Productos_ListaDePrecios__Base);

                }
            }

            return bRegresa;
        }

        private bool enviarArchivo(int TipoDeProceso, string Origen, string Destino)
        {
            bool bRegresa = false;

            GetConexionFTP(TipoDeProceso); 
            if (bFTP_Configurado)
            {
                bRegresa = enviarArchivo(Origen, Destino); 
            }

            return bRegresa;
        }

        private bool enviarArchivo(string Origen, string Destino)
        {
            bool bRegresa = false;

            try
            {
                if (FTP == null)
                {
                    FTP = new clsFTP(sUrl_FTP, sUsuario_FTP, sPassword_FTP, false, bModoActivoDeTransferencia);
                    //FTP = new clsFTP(Url_FTP, Usuario_FTP, Password_FTP, false, ModoDeTransferencia);
                }

                bRegresa = FTP.SubirArchivo(Path.Combine(sRuta, Origen), "", Destino); 
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(string.Format("Error al enviar el archivo {0} {1} ", Destino, ex.Message)); 
            }

            return bRegresa;
        }

        private void GetConexionFTP(int TipoProceso )
        {
            string sSql = string.Format("Select  URL_Produccion, Usuario, Password, ModoActivoDeTransferencia, Status " +
                "From INT_MA__CFG_Envio_FTP (NoLock) Where TipoEnvio = {0} and Status = 'A' ", TipoProceso);

            bFTP_Configurado = false; 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "GetConexionFTP()"); 
            }
            else
            {
                if (leer.Leer())
                {
                    bFTP_Configurado = true;
                    sUrl_FTP = leer.Campo("URL_Produccion");
                    sUsuario_FTP = leer.Campo("Usuario");
                    sPassword_FTP = leer.Campo("Password");
                    bModoActivoDeTransferencia = leer.CampoBool("ModoActivoDeTransferencia"); 

                    System.Console.WriteLine(string.Format("Url : {0}", sUrl_FTP));
                    System.Console.WriteLine(string.Format("Usuario : {0}", sUsuario_FTP)); 
                }
            }
        }
        #endregion Funciones y Procedimientos Privados
     }
}
