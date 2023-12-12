using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.SQL;
using SC_SolutionsSystem.SistemaOperativo; 

namespace DllTransferenciaSoft.IntegrarBD
{
    public class clsFTP_BaseDeDatos
    {
        enum Iconos 
        {
            Raiz = 0, Directorio = 1, DirectorioAbierto = 2, Archivo = 3 
        }

        #region Declaracion de variables 
        string sRuta_FTP = "";
        string sRuta_Integracion_BD = "";
        TreeNode twNodo = new TreeNode("Raiz");
        DataSet dtsArchivos = new DataSet();
        int iFiles = 0;
        int iFile_Base = 10; 
        int iFiles_Depurar = 10;
        int iDias_Integracion = 15; 
        basGenerales Fg = new basGenerales(); 

        StreamWriter lista; 
        string sLog_FTP = General.UnidadSO + @":\Lista_FTP.txt";


        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer; 
        #endregion Declaracion de variables

        #region Constructores y Destructor de Clase 
        public clsFTP_BaseDeDatos(string FTP):this(FTP, "") 
        {
        }

        public clsFTP_BaseDeDatos(string FTP, string Integracion)
        {
            //////FTP = Fg.Right(FTP, 1) != @"\" ? FTP + @"\" : FTP;
            //////Integracion = Fg.Right(Integracion, 1) != @"\" ? Integracion + @"\" : Integracion;

            sRuta_FTP = FTP + @"\" + Transferencia.DirectorioFTP;
            if (!Directory.Exists(sRuta_FTP))
            {
                sRuta_FTP = FTP;
            }

            //// inicializar la conexion a bd 
            leer = new clsLeer(ref cnn);


            sRuta_Integracion_BD = Integracion;
            dtsArchivos = new DataSet("ListaDeArchivos"); 

            lista = new StreamWriter(sLog_FTP, true);
            lista.WriteLine("--------------------------------------------------- ");
            lista.WriteLine("--------------------------------------------------- "); 
            lista.WriteLine(string.Format("----------------------------------- {0}     ", DateTime.Now.ToString()));
        }

        ~clsFTP_BaseDeDatos()
        {
            try
            {
                lista.Close(); 
            }
            catch { }
        } 
        #endregion Constructores y Destructor de Clase

        #region Propiedades Publicas 
        public int Depurar_NumArchivos
        {
            get { return iFiles_Depurar; }
            set 
            {
                iFiles_Depurar = value;
                iFiles_Depurar = iFiles_Depurar <= 0 ? 1 : iFiles_Depurar; 
            }
        }

        public int Dias_Integracion
        {
            get { return iDias_Integracion; }
            set
            {
                iDias_Integracion = value;
                iDias_Integracion = iDias_Integracion <= 0 ? 1 : iDias_Integracion;
            }
        }

        ////public TreeNode []Arbol
        ////{
        ////    get { return twNodo.Nodes; }
        ////} 
        #endregion Propiedades Publicas

        #region Funciones y Procedimientos Publicos
        public bool Procesar()
        {
            bool bRegresa = false;

            ListarDirectorios(sRuta_FTP, true);
            lista.Close(); 

            return bRegresa; 
        }

        public bool Depurar()
        {
            bool bRegresa = false;

            ListarDirectorios(sRuta_FTP, false);
            lista.Close(); 

            return bRegresa; 
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados Interface 
        public DataSet Archivos(string Directorio)
        {
            DataSet dtsRetorno = new DataSet();
            clsLeer leer = new clsLeer(); 

            try
            {
                Directorio = Directorio.Trim().Replace(@"\\", @"\"); 
                leer.DataRowsClase = dtsArchivos.Tables[0].Select(string.Format(" Directorio = '{0}' ", Directorio), " Fecha Desc ");
                dtsRetorno = leer.DataSetClase; 
            }
            catch { } 

            return dtsRetorno; 
        }

        public void ListarFTP(TreeView Arbol)
        {
            PreparaListaArchivos();
            iFiles = 0; 
            DirectoryInfo d = new DirectoryInfo(sRuta_FTP);

            Arbol.Nodes.Clear();
            Arbol.BeginUpdate();

            // twNodo = new TreeNode(d.Name);
            twNodo = Arbol.Nodes.Add(d.Name);
            twNodo.ImageIndex = (int)Iconos.Raiz; 
            twNodo.SelectedImageIndex = (int)Iconos.Raiz;
            twNodo.Tag = "0 "; 

            ListarFTP(sRuta_FTP, twNodo);

            Arbol.EndUpdate(); 
        }

        private void ListarFTP(string Ruta, TreeNode Nodo)
        {
            string[] lstDirectorios = Directory.GetDirectories(Ruta);

            foreach (string sDirectorio in lstDirectorios)
            {
                DirectoryInfo d = new DirectoryInfo(sDirectorio);
                TreeNode myNode = Nodo.Nodes.Add(d.Name);
                
                myNode.ImageIndex = (int)Iconos.Directorio;
                myNode.SelectedImageIndex = (int)Iconos.Directorio;

                ListarFTP(sDirectorio, myNode);

                if (myNode.Nodes.Count > 0) 
                {
                    myNode.Tag = "1 " + sDirectorio; 
                } 
            }

            ListarArchivos(Ruta, Nodo, "SII");
            ListarArchivos(Ruta, Nodo, "BAK"); 
        }

        private void ListarArchivos(string Ruta, TreeNode Nodo, string Extension)
        {
            string[] sFiles = Directory.GetFiles(Ruta, string.Format("*.{0}", Extension));
            DataTable ListaDeArchivos = ListaArchivos();
            string sRutaFile = "";
            double iSize = 0; 

            foreach (string sFile in sFiles)
            {
                try
                {
                    iFiles++; 
                    FileInfo f = new FileInfo(sFile);
                    sRutaFile = f.Directory.FullName.Replace(@"\\", @"\");
                    // sRutaFile = f.Directory.FullName.Replace(@"\", "|"); 

                    iSize = (((int)f.Length / 1024.00) / 1024.00); 
                    object[] obj = { iFiles, sRutaFile, f.Name, iSize.ToString("##,###,##0.#0"), f.CreationTime };
                    // ListaDeArchivos.Rows.Add(obj);
                    dtsArchivos.Tables[0].Rows.Add(obj); 

                    TreeNode myNode = Nodo.Nodes.Add(f.Name); 

                    myNode.ImageIndex = (int)Iconos.Archivo; 
                    myNode.SelectedImageIndex = (int)Iconos.Archivo;
                    myNode.Tag = "0"; 

                    ////File.Copy(f.FullName, Path.Combine(sRuta_Integracion_BD, f.Name)); 
                    ////lista.WriteLine(string.Format("Copiando {0}", f.FullName)); 
                }
                catch (Exception ex)
                {
                    ex.Source = ex.Source; 
                } 
            } 
        }
        #endregion Funciones y Procedimientos Privados Interface 

        #region Funciones y Procedimientos Privados
        private void ListarDirectorios(string Ruta, bool CopiarIntegracion)
        {
            try
            {
                lista.WriteLine(Ruta);
                string[] lstDirectorios = Directory.GetDirectories(Ruta);

                foreach (string sDirectorio in lstDirectorios)
                {
                    ListarDirectorios(sDirectorio, CopiarIntegracion);
                    lista.WriteLine(""); 
                }

                if (CopiarIntegracion)
                {
                    CopiarArchivos(Ruta);
                }
                else
                {
                    DepurarArchivos(Ruta);
                }
            }
            catch { } 
        }

        private void DepurarArchivos(string Ruta)
        {
            DepurarArchivos(Ruta, "SII");
            DepurarArchivos(Ruta, "BAK"); 
        }

        private void DepurarArchivos(string Ruta, string Extension)
        {
            string[] sFiles = Directory.GetFiles(Ruta, string.Format("*.{0}", Extension));
            DataTable ListaDeArchivos = ListaArchivos();

            foreach (string sFile in sFiles)
            {
                try
                {
                    FileInfo f = new FileInfo(sFile);
                    string sFecha = string.Format("{0}{1}{2}{3}{4}", 
                        Fg.PonCeros(f.LastWriteTime.Year, 4), 
                        Fg.PonCeros(f.LastWriteTime.Month, 2), 
                        Fg.PonCeros(f.LastWriteTime.Day, 2),
                        Fg.PonCeros(f.LastWriteTime.Hour, 2), 
                        Fg.PonCeros(f.LastWriteTime.Minute, 2));
                    
                    object[] obj = { f.FullName, f.LastWriteTime, sFecha };
                    ListaDeArchivos.Rows.Add(obj);
                }
                catch { }
            }

            DepurarArchivo(ListaDeArchivos);
        }

        private bool DepurarArchivo(DataTable ListaDeArchivos)
        {
            bool bRegresa = false;
            clsLeer lstArchivos = new clsLeer();
            clsLeer archivoCopiar = new clsLeer();
            int iFiles = 0; 

            lstArchivos.DataTableClase = ListaDeArchivos;
            archivoCopiar.DataRowsClase = lstArchivos.DataTableClase.Select(" 1 = 1 ", " Fecha Desc ");

            while (archivoCopiar.Leer())
            {
                try
                {
                    iFiles++;

                    if (iFiles > iFiles_Depurar)
                    {
                        FileInfo f = new FileInfo(archivoCopiar.Campo("Archivo"));
                        File.Delete(f.FullName);
                        lista.WriteLine(string.Format("Eliminando {0}", f.FullName));
                        //lista.WriteLine("");
                    }
                }
                catch { }
            }

            return bRegresa;
        }

        private void CopiarArchivos(string Ruta)
        {
            CopiarArchivos(Ruta, "SII");
            CopiarArchivos(Ruta, "BAK"); 
        }

        private void CopiarArchivos(string Ruta, string Extension)
        {
            string[] sFiles = Directory.GetFiles(Ruta, string.Format("*.{0}", Extension));
            DataTable ListaDeArchivos = ListaArchivos();
            double iSize = 0;

            foreach (string sFile in sFiles)
            {
                try
                {
                    FileInfo f = new FileInfo(sFile);
                    string sFecha = string.Format("{0}{1}{2}{3}{4}",
                        Fg.PonCeros(f.CreationTime.Year, 4),
                        Fg.PonCeros(f.CreationTime.Month, 2),
                        Fg.PonCeros(f.CreationTime.Day, 2),
                        Fg.PonCeros(f.CreationTime.Hour, 2),
                        Fg.PonCeros(f.CreationTime.Minute, 2));

                    sFecha = f.Name.Replace(f.Extension, "");
                    sFecha = Fg.Left(Fg.Right(sFecha, 15), 8);
                    iSize = (((int)f.Length / 1024.00) / 1024.00);

                    object[] obj = { f.FullName, f.CreationTime, sFecha, iSize };
                    ListaDeArchivos.Rows.Add(obj); 

                    ////File.Copy(f.FullName, Path.Combine(sRuta_Integracion_BD, f.Name)); 
                    ////lista.WriteLine(string.Format("Copiando {0}", f.FullName)); 
                }
                catch { }
            }

            CopiarArchivo(ListaDeArchivos); 
        }

        private double GetSizePromedio(DataTable ListaDeArchivos)
        {
            double dRegresa = 0;
            double dPromedio = 0;
            double dArchivos = 0; 
            clsLeer lstArchivos = new clsLeer();
            clsLeer archivoCopiar = new clsLeer();


            lstArchivos.DataTableClase = ListaDeArchivos;  
            while (lstArchivos.Leer())
            {
                dArchivos++;
                dPromedio += lstArchivos.CampoDouble("Tamaño"); 
            }
            dRegresa = (dPromedio / dArchivos); 


            return dRegresa;
        }

        private string FormatearNombreBD(string NombreBaseDeDatos)
        {
            string sRegresa = NombreBaseDeDatos;
            string consignos = "áàäéèëíìïóòöúùuñÁÀÄÉÈËÍÌÏÓÒÖÚÙÜÑçÇ";
            string sinsignos = "aaaeeeiiiooouuunAAAEEEIIIOOOUUUNcC";

            sRegresa = sRegresa.Replace("-", "_");
            sRegresa = sRegresa.Replace(" ", "_");

            for (int i = 0; i <= consignos.Length - 1; i++)
            {
                sRegresa = sRegresa.Replace(consignos[i], sinsignos[i]);
            }

            NombreBaseDeDatos = NombreBaseDeDatos.Replace("-", "_");
            return sRegresa;
        }

        private bool RevisarBD_Integrada(string BaseDeDatos)
        {
            bool bRegresa = false;
            string sSql = ""; 
            string sBD = FormatearNombreBD(BaseDeDatos);

            sSql = string.Format("Select * From CFG_RegistroIntegracionBD (NoLock) Where NombreBD like '%{0}%' ", sBD);
            //lista.WriteLine(string.Format("RevisarBD_Integrada : {0}", sSql));

            if (!leer.Exec(sSql))
            {
                //lista.WriteLine(string.Format("Error RevisarBD_Integrada : {0}", leer.MensajeError));
            }
            else 
            {
                bRegresa = leer.Leer();
            }

            return bRegresa;
        }

        private bool CopiarArchivo(DataTable ListaDeArchivos)
        {
            bool bRegresa = false;
            clsLeer lstArchivos = new clsLeer();
            clsLeer archivoCopiar = new clsLeer();
            DateTime date = DateTime.Now.AddDays(-1 * iDias_Integracion);
            string sFiltro = "";
            double dPromedio = GetSizePromedio(ListaDeArchivos); 


            string sFecha = string.Format("{0}{1}{2}",
                Fg.PonCeros(date.Year, 4),
                Fg.PonCeros(date.Month, 2),
                Fg.PonCeros(date.Day, 2));

            sFiltro = string.Format(" 1 = 1 and FechaCreacion >= {0} and Tamaño >= {1} ", sFecha, dPromedio);
            lstArchivos.DataTableClase = ListaDeArchivos;
            archivoCopiar.DataRowsClase = lstArchivos.DataTableClase.Select(sFiltro, " Tamaño Desc, Fecha Desc ");

            lista.WriteLine(string.Format("Filtro para copiado {0}", sFiltro));
            if (archivoCopiar.Registros == 0) lista.WriteLine("No se encontraron archivos para copiar.");

            while (archivoCopiar.Leer())
            {
                try
                {
                    FileInfo f = new FileInfo(archivoCopiar.Campo("Archivo"));                    
                    if ( !RevisarBD_Integrada(f.Name.Replace(f.Extension, "")))
                    {
                        try
                        {
                            if (File.Exists(f.FullName))
                            {
                                File.Copy(f.FullName, Path.Combine(sRuta_Integracion_BD, f.Name));
                                lista.WriteLine(string.Format("Copiando {0}", f.FullName));
                            }
                        }
                        catch (Exception ex)
                        { 
                            lista.WriteLine(string.Format("Error: {0}", ex.Message));
                        }
                    }

                    //lista.WriteLine(""); 
                }
                catch { } 
                break;
            }

            return bRegresa; 
        }

        private DataTable ListaArchivos() 
        {
            DataTable dtRetorno = new DataTable("Archivos");

            dtRetorno.Columns.Add("Archivo", System.Type.GetType("System.String"));
            dtRetorno.Columns.Add("Fecha", System.Type.GetType("System.DateTime"));
            dtRetorno.Columns.Add("FechaCreacion", System.Type.GetType("System.String"));
            dtRetorno.Columns.Add("Tamaño", System.Type.GetType("System.Double")); 

            return dtRetorno.Copy(); 
        }

        private void PreparaListaArchivos()
        {
            dtsArchivos = new DataSet("ListaDeArchivos"); 
            DataTable dtRetorno = new DataTable("Archivos");

            dtRetorno.Columns.Add("Id", System.Type.GetType("System.Int32")); 
            dtRetorno.Columns.Add("Directorio", System.Type.GetType("System.String")); 
            dtRetorno.Columns.Add("Archivo", System.Type.GetType("System.String"));
            dtRetorno.Columns.Add("Size", System.Type.GetType("System.Double")); 
            dtRetorno.Columns.Add("Fecha", System.Type.GetType("System.DateTime")); 

            dtsArchivos.Tables.Add(dtRetorno); 
        } 
        #endregion Funciones y Procedimientos Privados
    }
}
