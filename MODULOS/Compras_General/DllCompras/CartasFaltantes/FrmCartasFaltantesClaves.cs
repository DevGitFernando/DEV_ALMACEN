using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;


using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft; 

namespace DllCompras.CartasFaltantes
{
    public partial class FrmCartasFaltantesClaves : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid grid;

        clsConsultas Consultas;
        clsAyudas ayuda;
        ToolTip tip = new ToolTip(); 

        string sFolio = "";
        string sMensaje = "";
        string sValorGrid = "";
        string sDocumento = "";
        string sNombreDocto = "";
        string sArchivo = "";
        string sNombreArchivo = "";

        #region Documentos
        OpenFileDialog file = new OpenFileDialog();
        FolderBrowserDialog Folder = new FolderBrowserDialog();        
        #endregion Documentos

        private enum Cols
        {
            Ninguna = 0,
            ClaveSSA = 1, Descripcion = 2
        }

        public FrmCartasFaltantesClaves()
        {
            InitializeComponent();
            leer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DllCompras.GnCompras.DatosApp, this.Name);

            Consultas = new clsConsultas(General.DatosConexion, GnCompras.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, GnCompras.DatosApp, this.Name);

            grid = new clsGrid(ref grdClaves, this);
            grid.EstiloGrid(eModoGrid.ModoRow);

            tip.SetToolTip(btnAsignarDocto, "Asocia el documento especificado al registro de Carta de faltantes.");
            tip.SetToolTip(btnDescargar, "Descarga el documento de Carta de faltantes asociado al registro actual."); 
        }

        private void FrmCartasFaltantesClaves_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bContinua = false;

            EliminarRenglonesVacios();
           
            if (ValidaDatos())
            {
                if (!cnn.Abrir())
                {
                    General.msjAviso(General.MsjErrorAbrirConexion);
                }
                else
                {
                    cnn.IniciarTransaccion();

                    bContinua = GuardaEncabezado();

                    ////if (bContinua)
                    ////{
                    ////    bContinua = GuardaDetalles();
                    ////}

                    if (bContinua) // Si no Ocurrió ningun error se llevan a cabo las transacciones.
                    {
                        txtFolio.Text = sFolio;
                        cnn.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP                        
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        Error.GrabarError(leer, "btnGuardar_Click");
                        cnn.DeshacerTransaccion();
                        General.msjError("Ocurrió un error al guardar la información.");

                    }

                    cnn.Cerrar();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            dtpFechaRegistro.Enabled = false;
            IniciaToolBar(false, false, false, false, false);
            sDocumento = "";
            sNombreDocto = "";
            sArchivo = "";
            sNombreArchivo = "";
            grid.Limpiar(true);
            grid.BloqueaGrid(false);
            txtFolio.Focus();

        }

        private void IniciaToolBar(bool Guardar, bool Cancelar, bool Imprimir, bool Asignar, bool Descargar)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = false;
            btnAsignarDocto.Enabled = Asignar;
            btnDescargar.Enabled = Descargar;
        }
        #endregion Funciones

        #region Eventos
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            string sSql = "";

            if (txtFolio.Text.Trim() == "")
            {
                txtFolio.Text = "*";
                txtFolio.Enabled = false;
                IniciaToolBar(true, false, false, true, false);
            }
            else
            {
                sSql = string.Format(" Select * From vw_COM_CartasFaltantes (Nolock) Where Folio = '{0}' ", Fg.PonCeros(txtFolio.Text, 8));

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "txtFolio_Validating");
                    General.msjError("Ocurrió un error al Cargar los datos de la Carta de Faltantes");
                }
                else
                {
                    if (leer.Leer())
                    {
                        CargaEncabezado();
                        CargaDetalles();
                        IniciaToolBar(false, false, true, false, true);
                    }
                    else
                    {
                        General.msjAviso("No se encontró el folio de la Carta de Faltantes");
                        txtFolio.Focus();
                    }
                }                
            }
        }

        private void CargaEncabezado()
        {
            txtFolio.Text = leer.Campo("Folio");
            txtFolio.Enabled = false;
            txtProveedor.Text = leer.Campo("IdProveedor");
            txtProveedor.Enabled = false;
            lblProveedor.Text = leer.Campo("Proveedor");
            txtObservaciones.Text = leer.Campo("Observaciones");
            txtObservaciones.Enabled = false;
            dtpFechaRegistro.Value = leer.CampoFecha("FechaRegistro");
            sArchivo = leer.Campo("Documento");
            sNombreArchivo = leer.Campo("NombreDocto");
        }

        private void CargaDetalles()
        {
            string sSql = "";

            sSql = string.Format(" Select ClaveSSA, Descripcion From vw_COM_CartasFaltantes_Detalles (Nolock) Where Folio = '{0}' ", Fg.PonCeros(txtFolio.Text, 8));

            grid.Limpiar(false);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargaDetalles");
                General.msjError("Ocurrió un error al Cargar los detalles");
            }
            else
            {
                if (leer.Leer())
                {
                    grid.LlenarGrid(leer.DataSetClase);
                    grid.BloqueaGrid(true);
                }
            }  
        }
        #endregion Eventos

        #region Eventos Grid
        private void grdClaves_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if ((grid.ActiveRow == grid.Rows) && e.AdvanceNext)
            {
                if (grid.GetValue(grid.ActiveRow, 1) != "" && grid.GetValue(grid.ActiveRow, 2) != "")
                {
                    grid.Rows = grid.Rows + 1;
                    grid.ActiveRow = grid.Rows;
                    grid.SetActiveCell(grid.Rows, 1);
                }
            }
        }

        private void grdClaves_EditModeOn(object sender, EventArgs e)
        {
            sValorGrid = grid.GetValue(grid.ActiveRow, (int)Cols.ClaveSSA);
        }

        private void grdClaves_EditModeOff(object sender, EventArgs e)
        {
            switch (grid.ActiveCol)
            {
                case (int)Cols.ClaveSSA:
                    {
                        ObtenerDatosSal();
                    }

                    break;
            }
        }

        private void grdClaves_KeyDown(object sender, KeyEventArgs e)
        {
            if (grid.ActiveCol == (int)Cols.ClaveSSA)
            {
                if (e.KeyCode == Keys.F1)
                {
                    leer.DataSetClase = ayuda.ClavesSSA_Sales("grdClaves_KeyDown");
                    if (leer.Leer())
                    {
                        grid.SetValue(grid.ActiveRow, (int)Cols.ClaveSSA, leer.Campo("ClaveSSA"));                        
                        ObtenerDatosSal();
                    }
                }

                if (e.KeyCode == Keys.Delete)
                {
                    grid.DeleteRow(grid.ActiveRow);

                    if (grid.Rows == 0)
                        grid.Limpiar(true);
                }
            }
        }

        private void ObtenerDatosSal()
        {
            string sCodigo = "";         

            sCodigo = grid.GetValue(grid.ActiveRow, (int)Cols.ClaveSSA);

            if (sCodigo.Trim() == "")
            {
                General.msjUser("Clave no encontrada ó no esta Asignada a la Farmacia.");
                grid.LimpiarRenglon(grid.ActiveRow);
            }
            else
            {
                leer.DataSetClase = Consultas.ClavesSSA_Sales(sCodigo, true, "ObtenerDatosSal()");
                if (!leer.Leer())
                {                    
                    grid.LimpiarRenglon(grid.ActiveRow);
                }
                else
                {
                    CargaDatosSal();
                }                
            }

        }

        private void CargaDatosSal()
        {
            int iRowActivo = grid.ActiveRow;
           
            if (!grid.BuscaRepetido(leer.Campo("ClaveSSA"), iRowActivo, 1))
            {
                grid.SetValue(iRowActivo, (int)Cols.ClaveSSA, leer.Campo("ClaveSSA"));
                grid.SetValue(iRowActivo, (int)Cols.Descripcion, leer.Campo("Descripcion"));
                grid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.ClaveSSA);
                grid.AddRow();
                grid.SetActiveCell(iRowActivo + 1, (int)Cols.ClaveSSA);
            }
            else
            {
                General.msjUser("Esta Clave ya se encuentra capturada en otro renglon.");
                grid.SetValue(grid.ActiveRow, (int)Cols.ClaveSSA, "");
                limpiarColumnas();
                grid.SetActiveCell(grid.ActiveRow, 1);
            }            
        }

        private void limpiarColumnas()
        {
            for (int i = 2; i <= grid.Columns; i++) //Columnas. Nota: Inicia a partir de la 2da.
            {
                grid.SetValue(grid.ActiveRow, i, "");
            }
        }

        private void EliminarRenglonesVacios()
        {
            for (int i = 1; i <= grid.Rows; i++) //Renglones.
            {
                if (grid.GetValue(i, 2).Trim() == "") 
                    grid.DeleteRow(i);
            }

            if (grid.Rows == 0) // Si No existen renglones, se inserta 1.
                grid.AddRow();
        }
        #endregion Eventos Grid

        #region Eventos_Proveedor
        private void txtProveedor_Validating(object sender, CancelEventArgs e)
        {
            if (txtProveedor.Text.Trim() != "")
            {
                leer = new clsLeer(ref cnn);
                leer.DataSetClase = Consultas.Proveedores(txtProveedor.Text, "txtProveedor_Validating");
                if (leer.Leer())
                {
                    txtProveedor.Text = leer.Campo("IdProveedor");
                    lblProveedor.Text = leer.Campo("Nombre");
                }
            }
        }

        private void txtProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.Proveedores("txtProveedor_KeyDown");
                if (leer.Leer())
                {
                    txtProveedor.Text = leer.Campo("IdProveedor");
                    lblProveedor.Text = leer.Campo("Nombre");
                }
            }
        }
        #endregion Eventos_Proveedor

        #region GuardarInformacion
        private bool GuardaEncabezado() 
        {
            bool bRegresa = false;
            string sSql = "";
            int iOpcion = 0;

            iOpcion = 1;

            sSql = string.Format(" Exec spp_Mtto_COM_CartasFaltantes @Folio = '{0}', @IdProveedor = '{1}', @Observaciones = '{2}', @Documento = '{3}', @NombreDocto = '{4}', @iOpcion = {5} ",
                                txtFolio.Text, txtProveedor.Text, txtObservaciones.Text, sDocumento, sNombreDocto, iOpcion);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                if (leer.Leer())
                {
                    bRegresa = true; 
                    sFolio = leer.Campo("Folio");
                    sMensaje = leer.Campo("Mensaje");
                }
            }

            if (bRegresa)
            {
                bRegresa = GuardaDetalles(); 
            }

            return bRegresa;
        }

        private bool GuardaDetalles()
        {
            bool bRegresa = false;
            string sSql = "", sClaveSSA = "";

            for (int i = 1; i <= grid.Rows; i++)
            {
                bRegresa = true; 
                sClaveSSA = grid.GetValue(i, (int)Cols.ClaveSSA);

                if (sClaveSSA.Trim() != "")
                {
                    sSql = string.Format(" Exec spp_Mtto_COM_CartasFaltantes_Detalles '{0}', '{1}' ", sFolio, sClaveSSA);

                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }

            return bRegresa;
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtFolio.Text.Trim() == "")
            {
                General.msjAviso("Folio inválido, verifique.");
                txtFolio.Focus();
            }

            if (bRegresa && txtProveedor.Text.Trim() == "")
            {
                General.msjAviso("No ha capturado el Proveedor, verifique..");
                txtProveedor.Focus();
                bRegresa = false;
            }

            if (bRegresa && txtObservaciones.Text.Trim() == "")
            {
                General.msjAviso("No ha capturado las Observaciones, verifique..");
                txtObservaciones.Focus();
                bRegresa = false;
            }

            if (bRegresa && sDocumento.Trim() == "")
            {
                General.msjAviso("No ha asignado el Documento, verifique..");
                btnAsignarDocto.Focus();
                bRegresa = false;
            }

            if (bRegresa)
            {
                bRegresa = validarCapturaClaves();
            }

            return bRegresa;
        }

        private bool validarCapturaClaves()
        {
            bool bRegresa = true;

            if (grid.Rows == 0)
            {
                bRegresa = false;
            }
            else
            {
                if (grid.GetValue(1, (int)Cols.ClaveSSA) == "")
                {
                    bRegresa = false;
                }                
            }

            if (!bRegresa)
            {
                General.msjUser("Debe capturar al menos una Clave para generar la Carta de Faltantes, verifique.");
            }

            return bRegresa;

        }
        #endregion GuardarInformacion        

        #region Botones_Documentos
        private void btnAsignarDocto_Click(object sender, EventArgs e)
        {
            sDocumento = "";
            sNombreDocto = "";
            lblRutaDocto.Text = "";

            file = new OpenFileDialog(); 
            file.Multiselect = false;            
            file.Title = "Seleccione el archivo a cargar";
            file.InitialDirectory = Environment.SpecialFolder.Desktop.ToString(); 

            if (file.ShowDialog() == DialogResult.OK)
            {
                //sDocumento = Fg.ConvertirArchivoEnStringB64(file.FileName);
                FileInfo docto = new FileInfo(file.FileName);
                // sDocumento = Codificar(file.FileName); 
                sDocumento =  Fg.ConvertirArchivoEnStringB64(file.FileName); 
                sNombreDocto = docto.Name;
                lblRutaDocto.Text = file.FileName;
            }
        }

        private void btnDescargar_Click(object sender, EventArgs e)
        {
            Folder = new FolderBrowserDialog();
            Folder.RootFolder = Environment.SpecialFolder.Desktop;
            Folder.ShowNewFolderButton = true;
            Folder.Description = "Direcrtorio donde se descargara el documento de Carta de Faltante";  


            if (Folder.ShowDialog() == DialogResult.OK)
            {
                //if (Decodificar(sNombreArchivo, Folder.SelectedPath, sArchivo))
                if ( Fg.ConvertirStringB64EnArchivo(sNombreArchivo, Folder.SelectedPath, sArchivo, true) )
                {
                    General.msjUser("Documento descargado satisfactoriamente.");
                    lblRutaDocto.Text = Folder.SelectedPath + sNombreArchivo; 
                    General.AbrirDocumento(Path.Combine(Folder.SelectedPath, sNombreArchivo)); 
                } 
            }           
        }
        #endregion Botones_Documentos        

        private void grdClaves_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

        }
    }
}
