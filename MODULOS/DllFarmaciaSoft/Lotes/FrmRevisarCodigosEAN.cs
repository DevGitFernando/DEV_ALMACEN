using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_ControlsCS;
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

namespace DllFarmaciaSoft.Lotes
{
    public partial class FrmRevisarCodigosEAN : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas query;
        clsListView list;

        string sCodigoEAN_Validado = "";
        public bool CodigoSeleccionado = false;
        private bool bCodigoUnico = false; 

        public FrmRevisarCodigosEAN()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name); 

            list = new clsListView(listCodigoEAN); 
        }

        private void FrmRevisarCodigosEAN_Load(object sender, EventArgs e)
        {

        }

        #region Propiedades 
        public bool EsEAN_Unico
        {
            get { return bCodigoUnico; }
        }
        #endregion Propiedades
        
        #region Botones
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CodigoSeleccionado = false;
            this.Hide();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            sCodigoEAN_Validado = list.GetValue(1); //listCodigoEAN.FocusedItem.SubItems[0].Text.ToString();

            if (sCodigoEAN_Validado == "")
            {
                General.msjUser("No ha seleccionado un Codigo EAN valido."); 
            }
            else
            {
                CodigoSeleccionado = true;
                this.Hide();
            }
        }
        #endregion Botones

        #region Lista de Codigos EAN
        private void listCodigoEAN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                btnAceptar_Click(null, null);
            }
        }

        private void listCodigoEAN_DoubleClick(object sender, EventArgs e)
        {
            sCodigoEAN_Validado = list.GetValue(1); //listCodigoEAN.FocusedItem.SubItems[0].Text.ToString();

            if (sCodigoEAN_Validado == "")
            {
                General.msjUser("No ha seleccionado un Codigo EAN valido.");
            }
            else
            {
                CodigoSeleccionado = true;
                this.Hide();
            }

            ////CodigoSeleccionado = true; 
            ////sCodigoEAN_Validado = list.GetValue(1); //listCodigoEAN.FocusedItem.SubItems[0].Text.ToString();
            ////this.Hide();
        }
        #endregion Lista de Codigos EAN

        #region Funciones y Procedimientos Publicos
        public string VerificarCodigosEAN(string CodigoEAN, bool EsPublicoGeneral)
        {
            sCodigoEAN_Validado = CodigoEAN; 

            if (RevisarEANs(EsPublicoGeneral))
            {
                if (!bCodigoUnico)
                {
                    sCodigoEAN_Validado = "";
                    this.ShowDialog();
                }
                else
                {
                    CodigoSeleccionado = true;
                    this.Hide(); 
                }
            }

            return sCodigoEAN_Validado;
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private bool RevisarEANs(bool bEsPublicoGeneral)
        {
            bool bMostrarPantalla = true; 

            string sEsSectorSalud = "";
            string sSql = "";
            int iEsPublicoGeneral = bEsPublicoGeneral ? 1 : 0; 

            if (bEsPublicoGeneral)
            {
                sEsSectorSalud = " and V.EsSectorSalud = 0 ";
            }

            sSql = string.Format(" Select 'Codigo EAN' = V.CodigoEAN, cast(V.Existencia - ( V. ) as int) as Existencia, \n" +
                "    V.IdProducto as Codigo, V.Descripcion  \n" +
                " From vw_ProductosExistenEnEstadoFarmacia V (NoLock) \n" +
                " Where V.IdEstado = '{0}' and V.IdFarmacia = '{1}' {2} \n" +
                "    and V.IdProducto In ( Select IdProducto \n" +
                "                      From  CatProductos_CodigosRelacionados (NoLock) \n" +
                "                      Where v.CodigoEAN = '{3}' or v.CodigoEAN_Interno = '{4}' ) \n" +
                " Order by V.Existencia Desc  \n", 
                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sEsSectorSalud,
                sCodigoEAN_Validado, Fg.PonCeros(sCodigoEAN_Validado, 13));


            sSql = string.Format("Exec spp_ProductosExistenEnEstadoFarmacia  " + 
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @CodigoEAN = '{3}', @EsPublicoGeneral = '{4}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sCodigoEAN_Validado, iEsPublicoGeneral ); 

            if (!leer.Exec(sSql)) 
            {
                bMostrarPantalla = false; 
                Error.GrabarError(leer, "RevisarEANs");
                General.msjError("Ocurrió un error al validar los CodigosEAN del Producto."); 
            }
            else
            {
                if (!leer.Leer())
                {
                    bMostrarPantalla = false; 
                    General.msjUser("Producto no encontrado ó no esta Asignado a la Farmacia."); 
                }
                else
                {
                    if (leer.Registros == 1)
                    {
                        bCodigoUnico = true; 
                        sCodigoEAN_Validado = leer.Campo("Codigo EAN");
                        list.FocusItem(1); 
                    } 
                    else 
                    { 
                        bMostrarPantalla = true; 
                        list.CargarDatos(leer.DataSetClase, true, true);
                        list.AjustarColumnas();
                        list.FocusItem(1); 
                    } 
                }
            }

            return bMostrarPantalla; 
        }
        #endregion Funciones y Procedimientos Privados

        private void listCodigoEAN_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
