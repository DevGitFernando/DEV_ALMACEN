using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using DllFarmaciaSoft;

namespace OficinaCentral.CfgPrecios
{
    public partial class FrmPreciosClavesSSA_Programas : FrmBaseExt
    {
        private enum Cols
        {
            IdPrograma = 1, Programa = 2, IdSubPrograma = 3, Subprograma = 4, ClaveSSAAux = 5, Descripcion = 6, Precio = 7, Status = 8
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas query;
        clsAyudas ayuda;

        clsGrid myGrid;
        int iVacio = 0;
        string sFormato = "#,###,###,##0.###0";
        string sIdEstado, sIdCliente, sIdSubCliente, sIdClaveSSA;

        public FrmPreciosClavesSSA_Programas()
        {
            InitializeComponent();
            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name, false);
            ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name);

            myGrid = new clsGrid(ref grpProductos, this);
            myGrid.BackColorColsBlk = Color.White;
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            myGrid.AjustarAnchoColumnasAutomatico = true; 

            grpProductos.EditModeReplace = true;
            myGrid.SetOrder(true);
        }

        private void FrmPreciosClavesSSA_Programas_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();
            myGrid.Limpiar();
            txtClaveSSA.Text = sIdClaveSSA;
            CargarClaveSSA();
            CargarLista_Asignada_Precio();
        }

        #region Eventos

        private void txtPrograma_TextChanged(object sender, EventArgs e)
        {
            lblPrograma.Text = "";
            txtSubPrograma.Text = "";
            lblSubPrograma.Text = "";
        }

        private void txtSubPrograma_TextChanged(object sender, EventArgs e)
        {
            lblSubPrograma.Text = "";
        }

        private void txtPrograma_Validating(object sender, CancelEventArgs e)
        {
            if (txtPrograma.Text.Trim() != "")
            {
                leer.DataSetClase = query.Farmacia_Clientes_Programas("", sIdEstado, "", sIdCliente, sIdSubCliente, txtPrograma.Text.Trim(), "txtPrograma_Validating");
                if (leer.Leer())
                {
                    CargaDatosPrograma();
                }
                else
                {
                    txtPrograma.Text = "";
                    lblPrograma.Text = "";
                    txtSubPrograma.Text = "";
                    lblSubPrograma.Text = "";
                }
            }
        }

        private void txtSubPrograma_Validating(object sender, CancelEventArgs e)
        {
            if (txtSubPrograma.Text.Trim() != "")
            {
                leer.DataSetClase = query.Farmacia_Clientes_Programas("", sIdEstado, "", sIdCliente, sIdSubCliente, txtPrograma.Text.Trim(), txtSubPrograma.Text, "txtSubPrograma_Validating");
                if (leer.Leer())
                {
                    CargaDatosSubPrograma();
                }
                else
                {
                    txtSubPrograma.Text = "";
                    lblSubPrograma.Text = "";
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar(1);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Guardar(2);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
            CargarLista_Asignada_Precio();
        }

        private void grpProductos_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int iRow = myGrid.ActiveRow;
            txtPrograma.Text = myGrid.GetValue(iRow, (int)Cols.IdPrograma);
            txtSubPrograma.Text = myGrid.GetValue(iRow, (int)Cols.IdSubPrograma);
             txtPrograma_Validating(this, null);
            txtSubPrograma_Validating(this, null);
            txtPrecio.Text = myGrid.GetValueDou(iRow, (int)Cols.Precio).ToString(sFormato);

            txtPrecio.Focus();
        }

        private void txtPrograma_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.Farmacia_Clientes_Programas("", false, sIdEstado, "", sIdCliente, sIdSubCliente, "txtPrograma_KeyDown");
                if (leer.Leer())
                {
                    txtPrograma.Text = leer.Campo("IdPrograma");
                    lblPrograma.Text = leer.Campo("Programa");
                    txtSubPrograma.Focus();
                }
            }
        }

        private void txtSubPrograma_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (txtPrograma.Text.Trim() != "")
                {
                    leer.DataSetClase = ayuda.Farmacia_Clientes_Programas("", false, sIdEstado, "", sIdCliente, sIdSubCliente, txtPrograma.Text, "txtSubPrograma_KeyDown");
                    if (leer.Leer())
                    {
                        txtSubPrograma.Text = leer.Campo("IdSubPrograma");
                        lblSubPrograma.Text = leer.Campo("SubPrograma");
                    }
                }
            }
        }

        #endregion Eventos

        #region Funciones y procedimientos

        public void show(string IdEstado, string IdCliente, string IdSubCliente, string IdClaveSSA)
        {
            sIdEstado = IdEstado;
            sIdCliente = IdCliente;
            sIdSubCliente = IdSubCliente;
            sIdClaveSSA = IdClaveSSA;
            this.ShowDialog();
        }

        private void LimpiarPantalla()
        {
            txtPrograma.Enabled = true;
            txtPrograma.Text = "";
            txtSubPrograma.Enabled = true;
            txtSubPrograma.Text = "";

            txtPrecio.Text = iVacio.ToString(sFormato);
            myGrid.Limpiar();
        }

        private void CargarClaveSSA()
        {
            leer.DataSetClase = query.ClavesSSA_Asignadas_A_Clientes(sIdCliente, sIdSubCliente, txtClaveSSA.Text, "txtClaveSSA_Validating");
            if (!leer.Leer())
            {
                General.msjUser("Clave de Producto no encontrada, verifique.");
            }
            else
            {
                txtClaveSSA.Text = leer.Campo("IdClaveSSA_Sal");
                lblClaveSSA.Text = leer.Campo("ClaveSSA");
                lblDescripcion.Text = leer.Campo("DescripcionSal");
                lblTipoInsumo.Text = leer.Campo("TipoDeClaveDescripcion");
                lblPrecio.Text = leer.CampoDouble("Precio").ToString(sFormato);
                lblContenidoPaquete.Text = leer.Campo("ContenidoPaquete");
                lblPrecioUnitario.Text = leer.CampoDouble("PrecioUnitario").ToString(sFormato);
                lblFactor.Text = leer.CampoInt("Factor").ToString();

                double dPrecio = Convert.ToDouble("0" + lblPrecio.Text.Replace(",", ""));
                double dPrecioCaja = Convert.ToDouble("0" + lblContenidoPaquete.Text) * dPrecio;

                //lblContenidoPaquete.Text = "0";
                lblPrecioUnitario.Text = dPrecioCaja.ToString(sFormato);
            }
        }

        private bool validaDatos()
        {
            bool bRegresa = true;

            SendKeys.Send("{TAB}");


            if (txtPrograma.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("El programa no puede estar vacio, verifique.");
                txtPrograma.Focus();
            }

            if (txtSubPrograma.Text.Trim() == "" && bRegresa)
            {
                bRegresa = false;
                General.msjUser("El Sub-programa no puede estar vacio, verifique.");
                txtSubPrograma.Focus();
            }

            return bRegresa;
        }

        private void Guardar(int Opcion)
        {
            string sSql = "";

            if (validaDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    sSql = string.Format(" Exec spp_CFG_AsignarPrecios_ClavesSSA_Por_Programa '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', {8}, '{9}', {10}",
                            sIdEstado, sIdCliente, sIdSubCliente, txtPrograma.Text, txtSubPrograma.Text, txtClaveSSA.Text, txtPrecio.NumericText, DtGeneral.EstadoConectado,
                            DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal, Opcion);
                    if (!leer.Exec(sSql))
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al guardar la información.");
                    }
                    else
                    {
                        cnn.CompletarTransaccion();
                        General.msjUser("Información guardada satisfactoriamente.");
                        txtPrograma.Text = "";
                        LimpiarPantalla();
                        CargarLista_Asignada_Precio();
                    }

                    cnn.Cerrar();
                }
                else
                {
                    General.msjAviso("No fue posible establecer conexión con el servidor, intente de nuevo.");
                }
            }
        }

        private void CargaDatosPrograma()
        {
            //Se hace de esta manera para la ayuda. 
            txtPrograma.Enabled = false;
            txtPrograma.Text = leer.Campo("IdPrograma");
            lblPrograma.Text = leer.Campo("Programa");
        }

        private void CargaDatosSubPrograma()
        {
            //Se hace de esta manera para la ayuda. 
            txtSubPrograma.Enabled = false;
            txtSubPrograma.Text = leer.Campo("IdSubPrograma");
            lblSubPrograma.Text = leer.Campo("SubPrograma");

            CargarLista_Asignada_Precio();
        }

        private void CargarLista_Asignada_Precio()
        {

            txtPrecio.Text = iVacio.ToString(sFormato);

            myGrid.Limpiar();
            // if ( query.ExistenDatos ) 
            System.Threading.Thread.Sleep(200);
            this.Refresh();

            myGrid.LlenarGrid(query.ClientesClavesSSA_Asignadas_Precios_Programa(sIdEstado, sIdCliente, sIdSubCliente, sIdClaveSSA, "CargarClavesSSA_Asignadas_Precio()"));

            leer.DataSetClase =  query.ClientesClavesSSA_Asignadas_Precios_Programa(sIdEstado, sIdCliente, sIdSubCliente, sIdClaveSSA, txtPrograma.Text, txtSubPrograma.Text, "CargarClavesSSA_Asignadas_Precio()");

            if (leer.Leer())
            {
                txtPrecio.Text = leer.CampoDouble("Precio").ToString(sFormato);
            }
        }

        #endregion Funciones y procedimientos
    }
}
