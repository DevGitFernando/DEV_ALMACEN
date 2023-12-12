using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.IO;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Inventario;
using DllTransferenciaSoft;
using DllTransferenciaSoft.Zip;

using Dll_SII_INadro;

namespace Dll_SII_INadro.GenerarArchivos
{
    public class GenerarCatalogo
    {
        #region Declaracion de Variables
        string sPrefijo = "CAT_ND___";

        basGenerales Fg = new basGenerales();
        string sClaveCliente = "";
        DataSet dtsPedido;
        DateTime dtMarcaTiempo = DateTime.Now;
        string sMarcaTiempo = "";
        int iEsDeSurtimiento = 0;

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerInf;
        clsLeer leerExec;
        clsLeer leerCat;
        clsLeer leerDet;

        DataSet dtsResultado; 

        clsGrabarError Error;
        string sRutaDestino = "";
        string sFileName = "";
        #endregion Declaracion de Variables 

        #region Constructor de Clase  
        public GenerarCatalogo()
        {

            dtMarcaTiempo = General.FechaSistema; 
            sMarcaTiempo = string.Format("", dtMarcaTiempo.Year, dtMarcaTiempo.Month, dtMarcaTiempo.Day);

            sRutaDestino = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); 
            sRutaDestino += @"\DOCUMENTOS_NADRO\CATALOGO\"; 
            
            if (!Directory.Exists(sRutaDestino))
            {
                Directory.CreateDirectory(sRutaDestino);
            }

            Error = new clsGrabarError(General.DatosConexion, GnDll_SII_INadro.DatosApp, "Dll_SII_INadro.GenerarArchivos.GenerarCatalogo"); 
        }

        ~GenerarCatalogo()
        { 
        }
        #endregion Constructor de Clase
        
        #region Funciones y Procedimientos Publicos
        public void MsjFinalizado()
        {
            General.msjUser("Generación de catálogos concluida satisfactoriamente.");
        }

        public bool Generar()
        {
            bool bRegresa = false;

            dtsResultado = new DataSet(); 
            leer = new clsLeer(ref cnn);
            leerCat = new clsLeer(ref cnn);
            leerDet = new clsLeer(ref cnn);
            leerExec = new clsLeer(ref cnn);
            leerInf = new clsLeer(ref cnn); 

            bRegresa = GenerarDocumento(); 

            return bRegresa; 
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private bool GenerarDocumento()
        {
            bool bRegresa = false;
            string sTabla = ""; 

            if (!ExistenTablasEnvio())
            {
                General.msjAviso("No se encontró información de catálogos."); 
            }
            else
            {
                bRegresa = true; 
                while (leerCat.Leer())
                {
                    sTabla = leerCat.Campo("NombreTabla"); 
                    bRegresa = ObtenerInformacion(sTabla); 
                    if (!bRegresa)
                    {
                        bRegresa = false; 
                        break;
                    }
                }

                if (!bRegresa)
                {
                    General.msjError("Ocurrio un error al Generar el Catálogo.");
                }
            }

            if (bRegresa)
            {
                if (GenerarArchivo())
                {
                    bRegresa = Empacar(); 
                }                
            }

            return bRegresa;
        }

        private bool ExistenTablasEnvio()
        {
            bool bRegresa = true;

            string sSql = string.Format("Select IdEnvio, NombreTabla, IdOrden, Status, Actualizado " +
                " From INT_ND_CFG_EnvioInformacion (NoLock) " +
                " Where Status = 'A' " +
                " Order By IdOrden, NombreTabla "); 

            // leerCat = new clsLeer(ref cnn);
            if (!leerCat.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leerCat, "ExistenTablasEnvio()");
            }

            return bRegresa;
        }

        private bool ExisteTabla_A_Procesar(string Tabla)
        {
            bool bRegresa = false;
            string sSql = string.Format("Select * From Sysobjects (NoLock) Where Name = '{0}' and xType = 'U' ", Tabla);

            bRegresa = leer.Exec(sSql);
            if (!bRegresa)
            {
                sSql = string.Format("El objeto tabla [[ {0} ]] no existe", Tabla); 
                Error.GrabarError(sSql, "ExisteTabla_A_Procesar");
            }

            return bRegresa;
        }

        private bool ObtenerInformacion(string Tabla)
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_INT_ND_CFG_ObtenerDatos @Tabla = '{0}', @Comillas_Separador = 1 ", Tabla);

            if (ExisteTabla_A_Procesar(Tabla))
            {
                if (!leerExec.Exec(Tabla, sSql)) 
                {
                    Error.GrabarError(leerExec, "ObtenerInformacion()"); 
                }
                else 
                {
                    bRegresa = true;
                    dtsResultado.Tables.Add(leerExec.DataTableClase.Copy()); 
                }
            }

            return bRegresa;
        }

        private bool GenerarArchivo()
        {
            bool bRegresa = false;
            int iReg = 0, iVueltas = 0; // , iRegistros = 0; 
            int iRegistros = 0; 
            int iPaquete = 0; 
            int iLargoFormatoNombre = 6;
            string sNombre = "";
            string sValor = "";
            string sFileAux = "";
            string sConcentrado = ""; 

            StreamWriter f = null;

            string sMarcaTiempo = "";  // General.FechaYMD(General.FechaSistema).Replace("/", "") + General.FechaSistemaHora.Substring(0, 5).Replace(":", "");              
            DateTime dtmMarcaDeTiempo = DateTime.Now;

            sMarcaTiempo = string.Format("{0}{1}{2}-{3}{4}{5}",
                Fg.PonCeros(dtmMarcaDeTiempo.Year, 4), Fg.PonCeros(dtmMarcaDeTiempo.Month, 2), Fg.PonCeros(dtmMarcaDeTiempo.Day, 2),
                Fg.PonCeros(dtmMarcaDeTiempo.Hour, 2), Fg.PonCeros(dtmMarcaDeTiempo.Minute, 2), Fg.PonCeros(dtmMarcaDeTiempo.Second, 2)
                );

            sNombre = string.Format("{0}{1}", sPrefijo, sMarcaTiempo);   
            sFileName = sRutaDestino + @"\" + sNombre;
            sFileAux = sFileName + ".sql";
            sFileAux = sRutaDestino + @"\" + sNombre + "__" + Fg.PonCeros(0, iLargoFormatoNombre) + "____" + "CONCENTRADO" + ".sql";

            try
            {
                //////File.Delete(sFileAux);
                foreach (DataTable tabla in dtsResultado.Tables)
                {
                    leerInf = new clsLeer();
                    leerInf.DataTableClase = tabla;

                    iReg = 0;
                    ////iVueltas = 0; 

                    while (leerInf.Leer())
                    {
                        if (iVueltas == 0)
                        {
                            iVueltas++;
                            iPaquete++;
                            sFileAux = sRutaDestino + @"\" + sNombre + "__" + Fg.PonCeros(iPaquete, iLargoFormatoNombre) + "____" + "CONCENTRADO" + ".sql";
                            ///////sFileAux = sRutaDestino + @"\" + sNombre + "__" + Fg.PonCeros(iPaquete, iLargoFormatoNombre) + "____" + tabla.TableName + ".sql";
                            ////File.Delete(sFileAux);
                            f = new StreamWriter(sFileAux, true);
                        }

                        sValor = leerInf.Campo(1);
                        ////f.WriteLine(sValor);
                        iReg++;
                        iRegistros++;

                        sConcentrado += string.Format("{0} \t\n", sValor);

                        ///// Agregar el separador de Registros 
                        if (iReg >= Transferencia.RegistrosSQL)
                        {
                            ////f.WriteLine(Transferencia.SQL);
                            ////f.WriteLine("");
                            iReg = 0;
                            iVueltas++;
                            sConcentrado += string.Format("{0} \t\n", Transferencia.SQL);
                        }

                        ////// Generar archivos de 200 Registros ==> 300-450 Kb
                        if (iVueltas >= 5)
                        {
                            //////// Cerrar el archivo con los Bloques Completos 
                            f.WriteLine(sConcentrado); 
                            f.WriteLine(Transferencia.SQL);
                            f.WriteLine("");
                            f.Close();
                            iVueltas = 0;
                            sConcentrado = "";
                        }
                    }

                    if (iVueltas != 0)
                    {
                        ////// Cerrar el archivo en caso de no completar los bloques de Registros 
                        sConcentrado += string.Format("{0} \t\n", Transferencia.SQL);
                        f.WriteLine(Transferencia.SQL);
                        f.WriteLine(sConcentrado);
                        f.Close();
                        iVueltas = 0;
                    }
                }

                ////f.Close();
                ////f = null;
                bRegresa = true;

                if (iVueltas != 0)
                {
                    sConcentrado += string.Format("{0} \t\n", Transferencia.SQL);
                    /////f = new StreamWriter(sFileAux, true);
                    f.WriteLine(sConcentrado);
                    f.Close();
                }
            }
            catch (Exception ex) 
            {
                sNombre = ex.Message;
            }

            ////try
            ////{
            ////    File.Delete(sFileAux); 
            ////}
            ////catch { } 

            iRegistros++; 
            return bRegresa; 
        }

        private bool Empacar()
        {
            clsGrabarError.LogFileError("Iniciando generando paquete de datos."); 

            bool bRegresa = false;
            //// int iTipo = (int)Destino;
            string sNombrePaquete = "Intermed." + Transferencia.ExtArchivosGenerados;


            string sFileZip = sFileName + @"." + Transferencia.ExtArchivosGenerados; 

            string[] sFiles = Directory.GetFiles(sRutaDestino, "*.sql");
            clsCriptografia Cripto = new clsCriptografia();
            ZipUtil zip = new ZipUtil();

            bRegresa = zip.Comprimir(sFiles, sFileZip, true);

            return bRegresa;
        }
        #endregion Funciones y Procedimientos Privados 
    }
}
