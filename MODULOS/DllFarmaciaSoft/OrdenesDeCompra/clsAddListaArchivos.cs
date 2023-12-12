
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;

namespace DllFarmaciaSoft.OrdenesDeCompra
{
    public class cslArchivo
    {

        string sMD5 = "";
        string sNombreArchivo = "";
        string sPathFile = "";
        string sContenidoArchivo = "";

        public cslArchivo(string Path)
        {
            FileInfo f = new FileInfo(Path);
            sNombreArchivo = f.Name;
            sPathFile = Path;
            sContenidoArchivo = General.Fg.ConvertirArchivoEnStringB64(Path);

            sMD5 = clsMD5.GenerarMD5_Archivo(Path);
        }

        public string MD5
        {
            get { return sMD5; }
        }

        public string NombreArchivo
        {
            get { return sNombreArchivo; }
        }

        public string PathFile
        {
            get { return sPathFile; }
        }

        public string ContenidoArchivo
        {
            get { return sContenidoArchivo; }
        }
    }

    public class clsAddListaArchivos
    {
        ArrayList documentos;
        ArrayList documentosTmp;
        basGenerales Fg;
        bool bDescargarDoctos = false;
        DataSet dtsDocumentos = new DataSet();
        string sTitulo = "Agregar documentos";
        string sListaDeArchivos = "";

        bool bSeGuardoInformacion = false;

        public clsAddListaArchivos()
        {
            documentos = new ArrayList();
            documentosTmp = new ArrayList();
            Fg = new basGenerales();
        }

        public bool SeGuardoInformacion
        {
            get { return bSeGuardoInformacion; }
        }

        public string Titulo
        {
            get { return sTitulo; }
            set { sTitulo = value; }
        }

        public ArrayList Documentos
        {
            get { return documentos; }
        }

        public string ListaDeDocumentos
        {
            get { return sListaDeArchivos; }
        }

        public DataSet DocumentosGuardados
        {
            set { dtsDocumentos = value; }
        }

        public bool DescargarDoctos
        {
            get { return bDescargarDoctos; }
            set { bDescargarDoctos = value; }
        }

        public void Show()
        {
            Show(false);
        }

        public void Show(bool PermiteAnexar)
        {
            bSeGuardoInformacion = false;

            FrmAddArchivos f = new FrmAddArchivos(PermiteAnexar, sTitulo, documentos, bDescargarDoctos, dtsDocumentos);
            f.ShowDialog();

            documentos = new ArrayList();
            documentosTmp = f.ListaDocumentos;
            bSeGuardoInformacion = f.SeGuardoInformacion;

            foreach (cslArchivo file in documentosTmp)
            {
                documentos.Add(file);
                sListaDeArchivos += string.Format(@"{0};", file.PathFile);
            }
        }
    }
}