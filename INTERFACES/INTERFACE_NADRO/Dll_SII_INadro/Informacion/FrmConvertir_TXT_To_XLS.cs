using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.ProviderBase;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.IO;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

namespace Dll_SII_INadro.Informacion
{
    public partial class FrmConvertir_TXT_To_XLS : FrmBaseExt 
    {

        SC_SolutionsSystem.ExportarDatos.FormatoExcel formatoExcel = FormatoExcel.XLS;

        OpenFileDialog openIni = new OpenFileDialog();
        OpenFileDialog openTxt = new OpenFileDialog();

        clsListView lst;
        clsListView lDatos; 
        clsLeerTxtCsv leer;

        string sFileEsquema = "";
        string sEsquema = "";

        string sFileTxt = "";
        string sFileTxt_Aux = "";

        string sFileExcel = "";

        DataTable dtsDatos;
        DataSet dtsInformacion;

        public bool ArchivoCargado = false;

        public FrmConvertir_TXT_To_XLS()
        {
            InitializeComponent();
            lst = new clsListView(lstEsquema);
            lDatos = new clsListView(lstVwInformacion); 
        }

        #region Propiedades 
        public DataSet Informacion
        {
            get { return dtsInformacion; }
        }
        #endregion Propiedades

        #region Form
        private void FrmConvertir_TXT_To_XLS_Load(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }
        #endregion Form

        #region Archivos de proceso 
        private void btnEsquema_Click(object sender, EventArgs e)
        {
            openIni.Title = "Archivos de Configuracion";
            openIni.Filter = "Archivos Config (*.ini)| *.ini";
            openIni.InitialDirectory = Application.StartupPath;
            openIni.AddExtension = true; 

            if (openIni.ShowDialog() == DialogResult.OK)
            {
                FileInfo docto = new FileInfo(openIni.FileName);
                sFileEsquema = openIni.FileName;
                lblDocumentoEsquema.Text = sFileEsquema; 
                LeerEsquema();
                
                btnEsquema.Enabled = false; 
                btnEjecutar.Enabled = true; 
            }
        }

        private void btnArchivoConvertir_Click(object sender, EventArgs e)
        {
            openTxt.Title = "Archivos de Texto";
            openTxt.Filter = "Archivos de texto (*.txt)| *.txt";
            openTxt.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openTxt.AddExtension = true;

            if (openTxt.ShowDialog() == DialogResult.OK)
            {
                FileInfo docto = new FileInfo(openTxt.FileName);
                sFileTxt = openTxt.FileName;
                lblDocumentoConvertir.Text = sFileTxt;


                sFileExcel = docto.Name.Replace(docto.Extension, "." + formatoExcel.ToString().ToLower()  );
                sFileExcel = docto.DirectoryName + @"\" + sFileExcel;

                sFileTxt_Aux = docto.Name.Replace(docto.Extension, "");
                sFileTxt_Aux = sFileTxt_Aux + "_tmp" + docto.Extension;
                sFileTxt_Aux = docto.DirectoryName + @"\" + sFileTxt_Aux;

                btnArchivoConvertir.Enabled = false;
                btnEjecutar.Enabled = true; 
            }
        }
        #endregion Archivos de proceso

        #region Botones 
        private void LimpiarPantalla()
        {
            Fg.IniciaControles();
            lblTituloMigracion.Visible = false;
            lblTituloMigracion.Text = ""; 
            lDatos.Limpiar(); 

            lst.LimpiarItems();
            lst.AnchoColumna(1, 320);

            btnEjecutar.Enabled = false;
            btnGuardar.Enabled = false;
            btnExportarExcel.Enabled = false;

            btnEsquema.Enabled = true; 
            btnArchivoConvertir.Enabled = true; 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ArchivoCargado = true;
            this.Hide(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            ////if (PrepararArchivoTxt())
            {
                if (CargarArchivo())
                {
                    btnEjecutar.Enabled = false;
                    btnGuardar.Enabled = true;
                    btnExportarExcel.Enabled = true; 
                }
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            ////try
            ////{
            ////    FrmExportarExcel f = new FrmExportarExcel(leer.DataSetClase, sFileExcel);
            ////    if (f.Exportar())
            ////    {
            ////        f.Dispose();
            ////        f = null;

            ////        File.Delete(sFileTxt_Aux); 
            ////        if (General.msjConfirmar("Desea abrir el documento generado") == DialogResult.Yes)
            ////        {
            ////            General.AbrirDocumento(sFileExcel);
            ////        }
            ////    }
            ////}
            ////catch { } 
        }
        #endregion Botones

        #region Funciones y Procedimientos Privados 
        private void LeerEsquema()
        {
            StreamReader sr = new StreamReader(sFileEsquema);
            string sLinea = "";
            int iRow = 1;

            lst.LimpiarItems();
            lst.AnchoColumna(1, 320); 
            sEsquema = "";

            while (sr.Peek() >= 0)
            {
                sLinea = sr.ReadLine();
                sLinea = sLinea.Trim(); 

                if (sLinea != "")
                {
                    sEsquema += sLinea + ",";

                    lst.AddRow();
                    lst.SetValue(iRow, 1, sLinea);
                    iRow++;
                }
            }

            if (sEsquema != "")
            {
                sEsquema = Fg.Mid(sEsquema, 1, sEsquema.Length - 1);
            }

            sr.Close();

            Preparar_TablaDeDatos(); 
        }

        private bool PrepararArchivoTxt()
        {
            bool bRegresa = false;
            string sFileEntrada = ""; 

            try
            {
                StreamReader sr = new StreamReader(sFileTxt);
                sFileEntrada = sr.ReadToEnd(); 
                sr.Close();

                //sFileEntrada = sFileEntrada.Replace("|", ","); 

                StreamWriter sw = new StreamWriter(sFileTxt_Aux);
                ////sw.WriteLine(sEsquema);
                sw.Write(sFileEntrada); 

                sw.Close();

                bRegresa = true; 
            }
            catch { }

            return bRegresa; 
        }

        private bool CargarArchivo()
        {
            bool bRegresa = true;
            StreamReader sr = new StreamReader(sFileTxt, Encoding.Default, true);
            string sLinea = "";
            clsLeer leerDatos = new clsLeer();
            int iRegistros = 0;
            bool bColumnasValidadas = false;

            lblTituloMigracion.Visible = true;
            lblTituloMigracion.Text = string.Format("Registros : {0}", iRegistros);
            while (sr.Peek() >= 0)
            {
                sLinea = sr.ReadLine();
                if (!bColumnasValidadas)
                {
                    bColumnasValidadas = true; 
                    validarNumeroDeColumnas__Entrada_vs_Layout(sLinea); 
                }

                if (AgregarRenglon(sLinea))
                {
                    iRegistros++;
                    lblTituloMigracion.Text = string.Format("Registros : {0}", iRegistros);
                }
            }

            sr.Close();


            leerDatos.DataSetClase = dtsInformacion;
            lDatos.CargarDatos(leerDatos.DataSetClase, true, true);
            leer = new clsLeerTxtCsv(); 
            leer.DataSetClase = dtsInformacion;
            lblTituloMigracion.Visible = false;

            return bRegresa; 
        }

        private void validarNumeroDeColumnas__Entrada_vs_Layout(string Datos)
        {
            string sDatosLimpios = Datos.Replace("\0", " ");
            object[] datos = sDatosLimpios.Split('|');
            string[] columnas = sEsquema.Split(',');
            string sColName = "";
            int j = 0;
            int iCols = 0; 

            dtsDatos = dtsInformacion.Tables[0].Copy();
            lDatos.Limpiar();
            iCols = datos.Length - dtsDatos.Columns.Count;


            for (int i = 1; i <= iCols; i++)
            {
                sColName = string.Format("ColAux__{0}", i);
                dtsDatos.Columns.Add(sColName, Type.GetType("System.String"));
            }

            dtsInformacion = new DataSet("Datos");
            dtsInformacion.Tables.Add(dtsDatos.Copy());
            lDatos.CargarDatos(dtsInformacion, true, true); 

        }

        private bool AgregarRenglon(string Datos)
        {
            bool bRegresa = true;  
            string sDatosLimpios = Datos.Replace("\0", " ");
            object[] datos = sDatosLimpios.Split('|');
            string[] columnas = sEsquema.Split(',');
            int iLongitud = datos.Length;
            string sValor = ""; 


            for(int i = 0; i<= iLongitud-1;i++)
            {
                sValor = datos[i].ToString().Replace("\0", " ").Trim(); 
                ////sValor = string.Format("'{0}'", datos[i].ToString().Replace("\0", " ").Trim());               
                sValor = sValor.Trim();

                bRegresa = !(Fg.Asc(sValor) == 26);  
                sValor = string.Format("'{0}'", sValor); 

                datos[i] = (object)sValor;

                if (!bRegresa)
                {
                    break; 
                }
            }

            if (bRegresa)
            {
                dtsInformacion.Tables[0].Rows.Add(datos);
            }


            return bRegresa; 
        }

        private void Preparar_TablaDeDatos()
        {
            ListViewItem itmX = null;
            string sColName = "";
            int j = 0; 

            dtsDatos = new DataTable("Tabla");
            lDatos.Limpiar(); 

            for (int i = 1; i <= lst.Registros; i++)
            {
                sColName = lst.GetValue(i, 1); 
                dtsDatos.Columns.Add(sColName, Type.GetType("System.String"));
            }

            dtsInformacion = new DataSet("Datos");
            dtsInformacion.Tables.Add(dtsDatos.Copy()); 
            lDatos.CargarDatos(dtsInformacion, true, true);
            
            sFileExcel = ""; 
        }

        private bool CargarArchivo_Excel()
        {
            bool bRegresa = false; 

            leer = new clsLeerTxtCsv();
            if (leer.LeerArchivo(sFileTxt_Aux))
            {
                lDatos.Limpiar();
                lDatos.CargarDatos(leer.DataSetClase, true, true);

                bRegresa = true; 
            }

            return bRegresa; 
        }
        #endregion Funciones y Procedimientos Privados    

        private void btnSalir_Click(object sender, EventArgs e)
        {

        } 
    }
}
