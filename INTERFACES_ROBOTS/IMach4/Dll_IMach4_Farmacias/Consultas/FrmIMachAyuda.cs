using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.IO; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

namespace Dll_IMach4
{
    #region FrmIMachAyuda
    /// <summary>
    /// Forma para mostrar los catalogos de ayuda que se emplean en un mÛdulo.
    /// </summary>    
    public partial class FrmIMachAyuda : FrmBase
    {
        //private basGenerales Fg = new basGenerales();
        public DataSet dtsAyuda = new DataSet();
        private DataSet dtsCriterios = new DataSet();
        public string strResultado = "";
        public bool bFormatearCamposNumericos = false;
        private int intColumnas = 0;
        private bool bMuestraPantalla = true;

        //private clsConexionSQL cnn;
        //private clsDatosConexion datosCnn;
        private clsLeer leer;
        public bool bAccesarA_BD_Local = false;
        // private string sQueryConsulta = "";

        public FrmIMachAyuda()
        {
            InitializeComponent();
        }

        //public FrmAyuda(clsDatosConexion DatosConexion, string CadenaEjecutar )
        //{
        //    InitializeComponent();

        //    this.datosCnn = DatosConexion;
        //    cnn = new clsConexionSQL(datosCnn);
        //    leer = new clsLeer(ref cnn);
        //    bAccesarA_BD = true;
        //    // sQueryConsulta = " Select top 1 * From ( " + CadenaEjecutar + " ) R Order by " + OrderBy;
        //    sQueryConsulta = CadenaEjecutar;
        //    Error = new SC_SolutionsSystem.Errores.clsGrabarError("SC_SolutionsSystem", Application.ProductVersion, this.Name);


        //    if (!leer.Exec(sQueryConsulta))
        //    {
        //        Error.GrabarError(leer, "FrmAyuda");
        //    }
        //    else
        //    {
        //        dtsAyuda = leer.DataSetClase;
        //    }
        //}

        public void MostrarPantalla()
        {
            if (bMuestraPantalla)
            {
                this.ShowDialog();
            }
        }

        private void pfEjecutar()
        {
            // Iniciar los objetos para el filtrado de la informaciÛn
            string strValor = "";
            int j = 0;
            ListViewItem itmX = null;
            DataSet dts = new DataSet();
            DataRow[] myRow;
            string sBuscar = txtBuscar.Text.ToString();
            string sOrderBy = "" + cboCriterios.SelectedItem.ToString();
            //sBuscar = sBuscar.Replace(" ", " ");

            string strSql = "[" + cboCriterios.SelectedItem.ToString() + "]" + " like '%" + sBuscar + "%'";

            try
            {
                // Limpiar el ListView para agregar el resultado de la busqueda
                lwAyuda.Items.Clear();

                lblRegistros.Text = "Registros :   0";  
                myRow = dtsAyuda.Tables[0].Select(strSql);
                lblRegistros.Text = "Registros :   " + myRow.Length.ToString();  

                foreach (DataRow myRw in myRow)
                {
                    foreach (DataColumn myDc in dtsAyuda.Tables[0].Columns)
                    {
                        strValor = myRw[myDc.ColumnName].ToString();
                        if (j == 0)
                        {
                            itmX = lwAyuda.Items.Add(strValor);
                            j = 1;
                        }
                        else
                        {
                            itmX.SubItems.Add("" + strValor);
                        }
                    }
                    j = 0;
                }
            }
            catch (Exception ex) 
            {
                // MessageBox.Show(ex.Message); 
            }


            // Modificar el tamaÒo de las columnas, segun el contenido de los renglones
            AjustarColumnas();

            BuscarProductosIMach(); 
            txtBuscar.Text = "";
            txtBuscar.Focus();

            try
            {
                lwAyuda.Focus();
                lwAyuda.Items[0].Selected = true;
            }
            catch { }
            //lwAyuda.Focus();
        }

                /// <summary>
        /// MÈtodo para Normalizar(Quitar acentos y otros simbolos) los datos de la Ayuda. 
        /// </summary>
        private void AdministrarDataSet()
        {
            string sXml = "";
            string sEnc = "<?xml version=" + Fg.Comillas() + "1.0" + Fg.Comillas() + " standalone=" + Fg.Comillas() + "yes" + Fg.Comillas() + "?>"; 

            string consignos = "·‡‰ÈËÎÌÏÔÛÚˆ˙˘uÒ¡¿ƒ…»ÀÕÃœ”“÷⁄Ÿ‹—Á«";
            string sinsignos = "aaaeeeiiiooouuunAAAEEEIIIOOOUUUNcC";

            string sRuta = General.UnidadSO + @":\DatosSII.xml";
            StreamWriter datos = new StreamWriter(sRuta);
            DataSet dtsTipeado = new DataSet(dtsAyuda.DataSetName); 

            // Para manejo de Consultas internas 
            leer = new clsLeer(); 
            // leer.DataSetClase = dtsAyuda;
            sXml = dtsAyuda.GetXml(); 

            for (int i = 0; i <= consignos.Length - 1; i++)
            {
                sXml = sXml.Replace(consignos[i], sinsignos[i]); 
            }

            datos.WriteLine(sEnc); 
            datos.WriteLine(sXml); 
            datos.Close(); 

            dtsTipeado.ReadXml(sRuta);
            leer.DataSetClase = dtsTipeado; 
            dtsAyuda = leer.DataSetClase; 

            // Borrar el archivo 
            File.Exists(sRuta);

        }

        private void BuscarProductosIMach()
        { 
        }

        public void pfConfiguraListView()
        {
            pfConfiguraListView(1);
        }
        
        public void pfConfiguraListView(int ColInicialCombo)
        {
            DataRow rw;
            // DataColumn rc;
            ListViewItem itmX = null;
            object NewColListView = null;
            string strValor = "";
            int iAncho = 0, j = 0, intColInicial = 0;

            // Asegurar que se seleccione un elemeto de la lista
            if (ColInicialCombo <= 0)
                ColInicialCombo = 1;

            this.FontHeight = 15;
            if (dtsAyuda == null)
            {
                bMuestraPantalla = false;
                MessageBox.Show("No se ha encontrado informaciÛn para estos criterios.", "Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            // Configurar el listview
            if (bMuestraPantalla)
            {
                lblRegistros.Text = "Registros :   0"; 
                if (dtsAyuda.Tables.Count > 0)
                {
                    AdministrarDataSet();
                    lblRegistros.Text = "Registros :   " + leer.Registros.ToString();  
 
                    if (dtsAyuda.Tables[0].Rows.Count >= 0)
                    {
                        //Inicia el ListView
                        lwAyuda.Columns.Clear();
                        lwAyuda.View = System.Windows.Forms.View.Details;

                        rw = dtsAyuda.Tables[0].Rows[0];

                        // Total de columnas, obtener la columna CLAVE
                        intColumnas = dtsAyuda.Tables[0].Columns.Count - 1;

                        // Limpiar el listado de criterios
                        cboCriterios.Items.Clear();

                        // Agregar las columnas al ListView
                        foreach (DataColumn rc in dtsAyuda.Tables[0].Columns)
                        {
                            if (intColInicial == 0)
                            {
                                iAncho = (Fg.Val((rw[rc.ColumnName].ToString().Trim().Length).ToString()) * 10) + Fg.Val(Fg.Str(rc.ColumnName.Length));
                                intColInicial = 1;
                            }
                            /*
                            else
                            {
                                iAncho = (Fg.Val((rw[rc.ColumnName].ToString().Trim().Length).ToString()) * 10) + Fg.Val(Fg.Str(rc.ColumnName.Length));
                            }
                            */

                            NewColListView = lwAyuda.Columns.Add((rc.ColumnName).Trim(), iAncho);
                            //NewColListView = lwAyuda.Columns.Add((rc.ColumnName).Trim());
                            NewColListView = (rc.ColumnName).Trim();
                            cboCriterios.Items.Add(NewColListView);
                        }

                        bool bEsNumerico = false;
                        int iPosCol = 0;

                        // Mostrar los renglones solo para los catalogos pequeÒos 
                        if (!bAccesarA_BD_Local)
                        {
                            foreach (DataRow myRw in dtsAyuda.Tables[0].Rows)
                            {
                                iPosCol = 0;
                                foreach (DataColumn myDc in dtsAyuda.Tables[0].Columns)
                                {
                                    strValor = myRw[myDc.ColumnName].ToString();
                                    bEsNumerico = false;

                                    // Formatear $100, 000.00 los campos Decimal
                                    if (bFormatearCamposNumericos &&
                                        myDc.DataType == System.Type.GetType("System.Decimal"))
                                    {
                                        bEsNumerico = true;
                                        strValor = String.Format("{0:C}", myRw[myDc.ColumnName]);
                                    }

                                    if (myDc.DataType == System.Type.GetType("System.Double"))
                                        bEsNumerico = true;

                                    if (j == 0)
                                    {
                                        itmX = lwAyuda.Items.Add(strValor);
                                        j = 1;
                                    }
                                    else
                                    {
                                        itmX.SubItems.Add("" + strValor);
                                    }

                                    if (bEsNumerico)
                                        lwAyuda.Columns[iPosCol].TextAlign = HorizontalAlignment.Right;

                                    iPosCol++;
                                }
                                j = 0;
                            }
                        }


                        // Modificar el tamaÒo de las columnas, segun el contenido de los renglones
                        AjustarColumnas();

                        // Seleccionar el item 0
                        cboCriterios.Text = cboCriterios.Items[0].ToString();
                        try
                        {
                            cboCriterios.Text = cboCriterios.Items[ColInicialCombo - 1].ToString();
                        }
                        catch { }
                        txtBuscar.Focus();
                    }
                }

                BuscarProductosIMach(); 
                txtBuscar.Text = "";
                txtBuscar.Focus();

                try
                {
                    lwAyuda.Focus();
                    lwAyuda.Items[0].Selected = true;
                }
                catch { } 
            }
        }

        private void AjustarColumnas()
        {
            // Modificar el tamaÒo de las columnas, segun el contenido de los renglones
            lwAyuda.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            
            int iAnchoCol = lwAyuda.Columns[0].Width;
            double iIncremento = Fg.ValD(iAnchoCol.ToString()) * 0.25;
            int dAncho = iAnchoCol + Fg.Val(iIncremento.ToString());

            lwAyuda.Columns[0].Width = dAncho; // 175

            // Establecer el Ancho de las columnas 
            for (int x = 1; x <= intColumnas; x++)
            {
                lwAyuda.AutoResizeColumn(x, ColumnHeaderAutoResizeStyle.HeaderSize);

                iAnchoCol = lwAyuda.Columns[x].Width;
                iIncremento = Fg.ValD(iAnchoCol.ToString()) * 0.25;
                dAncho = iAnchoCol + Fg.Val(iIncremento.ToString());
                lwAyuda.Columns[x].Width = dAncho;
            } 
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                strResultado = lwAyuda.FocusedItem.SubItems[intColumnas].Text.ToString();
            }
            catch
            {
                strResultado = "";
            }

            this.Hide();
        }

        private void lwAyuda_KeyPress(object sender, KeyPressEventArgs e)
        {
            //object obj = new object();
            //if (e.KeyChar.ToString() ==  Keys.Return.ToString() )
            //    btnAceptar_Click(obj, null);
        }

        private void lwAyuda_KeyDown(object sender, KeyEventArgs e)
        {
            object obj = new object();
            if (e.KeyCode == Keys.Return)
                btnAceptar_Click(obj, null);
        }

        private void lwAyuda_DoubleClick(object sender, EventArgs e)
        {
            strResultado = lwAyuda.FocusedItem.SubItems[intColumnas].Text.ToString();
            this.Hide();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (validaEjecutar())
            {
                pfEjecutar();
            }
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (validaEjecutar())
                {
                    pfEjecutar();
                }
            }

            if (e.KeyCode == Keys.Escape)
                this.Hide();
        }

        private bool validaEjecutar()
        {
            bool bRegresa = true;

            if (bAccesarA_BD_Local)
            {
                if (txtBuscar.Text.Trim() == "")
                {
                    bRegresa = false;
                    General.msjError("No se ha especificado el parametro de busqueda, verifique.");
                }
            }

            return bRegresa;
        }

        private void FrmAyuda_KeyDown(object sender, KeyEventArgs e)
        {
            switch ((int)e.KeyValue)
            {
                case (int)Keys.Escape:
                    {
                        strResultado = "";
                        this.Close();
                    }

                    break;
            }
        }

        private void lwAyuda_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lwAyuda.ListViewItemSorter = new ListViewOrder(ref lwAyuda, e.Column);
        }

        private void FrmAyuda_Load(object sender, EventArgs e)
        {
            //txtBuscar.Focus(); 
        }

        private void FrmAyuda_Activated(object sender, EventArgs e)
        {
            txtBuscar.Focus();      
        }
    }
    #endregion FrmIMachAyuda 
}
