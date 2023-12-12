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
using System.Reflection;

namespace DllFarmaciaSoft.OrdenesDeCompra
{
    public partial class FrmAddArchivos : FrmBaseExt
    {
        ArrayList documentos;
        ArrayList documentosTmp;
        clsListView lst;
        DataSet dtsDocumentos = new DataSet();
        clsLeer doctos = new clsLeer();

        OpenFileDialog open = new OpenFileDialog();
        bool bDescargar = false;
        bool bSeGuardoInformacion = false;

        string sRutaTemporal = Application.StartupPath + @"\Temporales\";

        public FrmAddArchivos(string Titulo, ArrayList Documentos, bool Descargar, DataSet DocumentosDescarga) : this(false, Titulo, Documentos, Descargar, DocumentosDescarga)
        {
        }

        public FrmAddArchivos(bool PermiteAgregar, string Titulo, ArrayList Documentos, bool Descargar, DataSet DocumentosDescarga)
        {
            InitializeComponent();

            DtGeneral.CrearDirectorio(sRutaTemporal);

            documentos = Documentos;
            lst = new clsListView(ltsDocumentos);

            open.AddExtension = true;
            open.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
            open.Title = Titulo;
            open.Multiselect = true;

            //Descargar = false; 
            dtsDocumentos = DocumentosDescarga;
            bDescargar = Descargar;


            //PermiteAgregar = true;
            //Descargar = true;

            //btnAdd.Visibility = !Descargar ? bVisible : bNoVisible; 
            //btnDel.Visibility = !Descargar ? bVisible : bNoVisible;

            btnAdd.Visible = PermiteAgregar; // ? bVisible : bNoVisible;
            btnDel.Visible = PermiteAgregar; //  ? bVisible : bNoVisible;


            btnDescargar.Visible = false; //  ? bVisible : bNoVisible;
            btnVistaPrevia.Visible = false; //  ? bVisible : bNoVisible;
            //ribbonPageGroup_MenuAuxiliar.Visible = PermiteAgregar;

            this.Text = Titulo;
        }

        public bool SeGuardoInformacion
        {
            get { return bSeGuardoInformacion; }
        }
        public ArrayList ListaDocumentos
        {
            get { return documentos; }
        }

        private void FrmAddArchivos_Load(object sender, EventArgs e)
        {
            if (bDescargar)
            {
                doctos.DataSetClase = dtsDocumentos;

                while (doctos.Leer())
                {
                    lst.AddRow();
                    lst.SetValue(lst.Registros, 1, doctos.Campo("NombreDocto"));
                }
            }
            else
            {
                foreach (cslArchivo file in documentos)
                {
                    lst.AddRow();
                    lst.SetValue(lst.Registros, 1, file.PathFile);
                }

                ActualizarLista();
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (lst.EliminarRenglonSeleccionado())
            {
                ActualizarLista();
            }
        }

        private void btnVistaPrevia_ItemClick(object sender, EventArgs e)
        {
            string sItemFile = lst.GetValue(lst.RenglonActivo, 1);
            string sFile_Salida = sRutaTemporal + lst.GetValue(lst.RenglonActivo, 1);

            doctos.DataRowsClase = dtsDocumentos.Tables[0].Select(string.Format(" NombreDocto = '{0}' ", sItemFile));
            while (doctos.Leer())
            {
                Fg.ConvertirStringB64EnArchivo(doctos.Campo("NombreDocto"), sRutaTemporal, doctos.Campo("ContenidoDocto"), true);
            }

            if (!File.Exists(sFile_Salida))
            {
                General.msjError("No fue posible visualizar el archivo seleccionado.");
            }
            else
            {
                General.AbrirDocumento(sFile_Salida);
                //DtGeneral.EliminarArchivo(sFile_Salida); 
            }

        }

        private void btnDescargar_ItemClick(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            string sRuta = "";

            if (folder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                sRuta = folder.SelectedPath;
                doctos.DataSetClase = dtsDocumentos;
                while (doctos.Leer())
                {
                    Fg.ConvertirStringB64EnArchivo(doctos.Campo("NombreDocto"), sRuta, doctos.Campo("ContenidoDocto"), true);
                    //lst.SetValue(lst.Registros, 1, doctos.Campo("NombreArchivo"));
                }

                General.msjUser("Documentos descargados");
                General.AbrirDirectorio(sRuta);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string sFile in open.FileNames)
                {
                    lst.AddRow();
                    lst.SetValue(lst.Registros, 1, sFile);
                }
            }

            ActualizarLista();
        }

        private void FrmAddArchivos_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void ActualizarLista()
        {
            documentos = new ArrayList();
            for (int i = 1; i <= lst.Registros; i++)
            {
                cslArchivo file = new cslArchivo(lst.GetValue(i, 1));
                documentos.Add(file);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bSeGuardoInformacion = lst.Registros > 0;
            this.Hide();
        }
    }
}
