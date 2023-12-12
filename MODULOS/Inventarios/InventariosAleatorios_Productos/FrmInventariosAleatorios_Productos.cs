using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;

namespace Inventarios.InventariosAleatorios
{
    public partial class FrmInventariosAleatorios_Productos : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        DllFarmaciaSoft.clsConsultas Consultas;
        DllFarmaciaSoft.clsAyudas Ayuda;
        clsLeer leer;
        clsLeer leerDatos;
        clsGrid Grid;

        clsDatosCliente DatosCliente;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        private enum Cols
        {
            Ninguna = 0,
            CodigoEAN = 1, Descripcion = 2, Existencia = 3
        }

        public FrmInventariosAleatorios_Productos()
        {
            InitializeComponent();

            DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "Inventarios Aleatorios");
            leer = new clsLeer(ref cnn);
            leerDatos = new clsLeer(ref cnn);
            Consultas = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, General.DatosApp, this.Name, true);
            Ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, General.DatosApp, this.Name, true);

            grdClaves.EditModeReplace = true;
            Grid = new clsGrid(ref grdClaves, this);
            Grid.EstiloGrid(eModoGrid.ModoRow);
            Grid.AjustarAnchoColumnasAutomatico = true; 
        }

        private void FrmInventariosAleatorios_Productos_Load(object sender, EventArgs e)
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

            if (RevisaCantidades())
            {
                if (!cnn.Abrir())
                {
                    Error.LogError(cnn.MensajeError);
                    General.msjErrorAlAbrirConexion(); 
                }
                else 
                {
                    cnn.IniciarTransaccion();

                    bContinua = GuardarInformacion();

                    if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                    {
                        cnn.CompletarTransaccion();
                        General.msjAviso("La información se guardó satisfactoriamente.");
                        Imprimir();
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        txtFolio.Text = "*";
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click");
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
            Imprimir();
        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            Grid.Limpiar(true);
            IniciaToolBar(false, false, false);
            rdoConteo01.Checked = false;
            rdoConteo02.Checked = false;
            rdoConteo03.Checked = false;
            dtpFechaRegistro.Enabled = false;
            lblCancelado.Visible = false;
            txtFolio.Focus();
        }

        private void IniciaToolBar(bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
        }

        private void CargaEncabezado()
        {
            txtFolio.Text = leer.Campo("Folio");
            txtFolio.Enabled = false;
            dtpFechaRegistro.Value = leer.CampoFecha("FechaRegistro");

            if (leer.Campo("Status") == "C")
            {
                lblCancelado.Visible = true;
            }

            CargaDetalles(); 
        }

        private void CargaDetalles()
        {
            string sSql = "";

            sSql = string.Format(" Select Distinct D.CodigoEAN, D.Descripcion, Cast(0 as Int) as Existencia, D.EsConteo_01, D.EsConteo_02, D.EsConteo_03 " +
                            " From vw_INV_Aleatorios_Productos_Det D (Nolock) " +	                        
                            " Where D.IdEmpresa = '{0}' and D.IdEstado = '{1}' and D.IdFarmacia = '{2}' and D.Folio = '{3}' " +
                            " and D.Conciliado = 0  and (D.EsConteo_01 = 0 OR D.EsConteo_02 = 0 OR D.EsConteo_03 = 0)  Order By D.Descripcion ",
                            sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text, 6));

            Grid.Limpiar(false);
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargaDetalles()");
                General.msjError("Ocurrió un error al obtener las Claves");
            }
            else
            {
                if (leer.Leer())
                {
                    IniciaToolBar(true, false, true);
                    Grid.LlenarGrid(leer.DataSetClase);
                    Grid.ColorColumna(1, Color.WhiteSmoke);
                    Grid.BloqueaColumna(true, 1);

                    rdoConteo01.Checked = leer.CampoBool("EsConteo_01");
                    rdoConteo02.Checked = leer.CampoBool("EsConteo_02");
                    rdoConteo03.Checked = leer.CampoBool("EsConteo_03");
                }
                else
                {
                    General.msjAviso("El folio se encuentra cerrado, no es posible agregar información.");
                }
            }
        }

        private bool RevisaCantidades()
        {
            bool bRegresa = true;
            int iExistencia = 0;

            for (int i = 1; i <= Grid.Rows; i++)
            {               
                iExistencia = Grid.GetValueInt(i, (int)Cols.Existencia);

                if (iExistencia == 0)
                {
                    if (General.msjConfirmar("Ingreso cantidades en Cero; ¿ Son correctas ?") == DialogResult.No)
                    {
                        bRegresa = false;
                    }                   
                    break;
                }
            }

            return bRegresa;
        }
        #endregion Funciones

        #region Eventos
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            bool bContinua = false;
            string sSql = "";

            if (txtFolio.Text.Trim() == "")
            {
                txtFolio.Focus();
            }
            else
            {
                sSql = string.Format(" Select * From INV_Aleatorios_Productos_Enc (Nolock) " +
                                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
                                sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text, 6));

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "txtFolio_Validating()");
                    General.msjError("Ocurrió un error al obtener la información del Folio de inventario.");
                }
                else
                {
                    if (leer.Leer())
                    {
                        CargaEncabezado();
                    }
                    else
                    {
                        General.msjAviso("Folio no encontrado.");
                        txtFolio.Focus();
                    }
                }
            }
        }
        #endregion Eventos

        #region Guardar
        private bool GuardarInformacion()
        {
            bool bRegresa = true;
            string sSql = "", sCodigoEAN = "";
            int iExistencia = 0;

            for (int i = 1; i <= Grid.Rows; i++)
            {

                sCodigoEAN = Grid.GetValue(i, (int)Cols.CodigoEAN);
                iExistencia = Grid.GetValueInt(i, (int)Cols.Existencia);

                sSql = String.Format(" Exec spp_Mtto_INV_Aleatorios_ProductosDet  " + 
                    " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio = '{3}', @CodigoEAN = '{4}', @Existencia = '{5}' ",
                    sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text, 6), sCodigoEAN, iExistencia);

                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    break;
                }                
            }
            
            return bRegresa;
        }
        #endregion Guardar

        #region Impresion
        private void Imprimir()
        {
            bool bRegresa = true;

            DatosCliente.Funcion = "Imprimir()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);

            myRpt.RutaReporte = DtGeneral.RutaReportes;
            myRpt.NombreReporte = "Rpt_INV_Aleatorios_Productos.rpt";

            myRpt.Add("IdEmpresa", sEmpresa);
            myRpt.Add("IdEstado", sEstado);
            myRpt.Add("IdFarmacia", sFarmacia);
            myRpt.Add("Folio", Fg.PonCeros(txtFolio.Text, 6)); 

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            if (!bRegresa && !DtGeneral.CanceladoPorUsuario)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }
        #endregion Impresion
    }
}
