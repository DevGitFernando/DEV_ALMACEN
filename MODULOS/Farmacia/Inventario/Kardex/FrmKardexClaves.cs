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
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;

namespace Farmacia.Inventario
{
    public partial class FrmKardexClaves : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid myGrid;
        clsAyudas ayuda;
        clsConsultas query; 

        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb; // = new Farmacia.wsFarmacia.wsCnnCliente();

        // int iAnchoColDescripcion = 0;

        public FrmKardexClaves()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            query = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name); 

            myGrid = new clsGrid(ref grdMovtos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            myGrid.AjustarAnchoColumnasAutomatico = true;

            // cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new Farmacia.wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            //GnFarmacia.AjustaColumnasImportes(grdMovtos, 7, 8, 3 );
        }

        #region Botones 
        private void LimpiarPantalla()
        {
            myGrid.Limpiar(false);
            Fg.IniciaControles();

            btnEjecutar.Enabled = true;
            btnImprimir.Enabled = false; 

            dtpFechaInicial.MinDate = DtGeneral.FechaMinimaSistema;
            dtpFechaFinal.MinDate = DtGeneral.FechaMinimaSistema;

            dtpFechaInicial.MaxDate = dtpFechaInicial.Value;
            dtpFechaFinal.MaxDate = dtpFechaFinal.Value;

            rdoConcentrado.Checked = true; 
            txtCodigo.Focus();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (validarStatus_ClaveSSA())
            {
                GenerarKardex(); 
            }
        }

        private bool validarStatus_ClaveSSA()
        {
            bool bRegresa = true; 
            string sSql = string.Format("Select * " +
                " From vw_ProductosExistenEnEstadoFarmacia (NoLock) " +
                " Where IdEstado = '{0}' And IdFarmacia = '{1}' And  ClaveSSA = '{2}' and StatusDeProducto In ( 'I', 'S' ) ",
                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtCodigo.Text.Trim());

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "txtCodigo_Validating");
                General.msjError("Ocurrió un error al obtener la información de la Clave SSA.");
            }
            else
            {
                if (leer.Leer())
                {
                    bRegresa = false;
                    General.msjUser("No es posible generar el kardex por que la Clave SSA se encuentra bloqueda por inventario."); 
                }
            }

            return bRegresa; 
        }

        private void GenerarKardex()
        {
            //string sFechas = string.Format(" Convert(varchar(10), FechaSistema, 120) Between '{0}' and '{1}' ",
            //    General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));

            string []sColumnas = { "FechaRegistro", "Entradas", "Salidas", "Existencia" }; 
            string sSql = string.Format("Exec spp_Rpt_Kardex_Por_Clave '{0}', '{1}', '{2}', '{3}', '{4}','{5}' ", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, 
                txtCodigo.Text.Trim(), General.FechaYMD(dtpFechaInicial.Value),General.FechaYMD(dtpFechaFinal.Value));

            myGrid.Limpiar(false);
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "btnEjecutar_Click");
                General.msjError("Ocurrió un error al obtener la información de movimientos.");
            }
            else
            {
                if (leer.Leer())
                {
                    txtCodigo.Enabled = false;
                    btnImprimir.Enabled = true; 
                    leer.FiltrarColumnas(1, sColumnas);
                    myGrid.LlenarGrid(leer.DataSetClase);
                }
                else
                {
                    General.msjUser("No se encontro información para los criterios especificados.");
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            bool bRegresa = false; 
            //if (validarImpresion())
            {
                DatosCliente.Funcion = "btnImprimir_Click()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.RutaReporte = GnFarmacia.RutaReportes; 

                // if (rdoDetallado.Checked)
                {
                    myRpt.NombreReporte = "PtoVta_KardexClave";
                    myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
                    myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
                    myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
                    myRpt.Add("ClaveSSA", txtCodigo.Text);
                    myRpt.Add("MAC", General.NombreEquipo);
                    ////myRpt.Add("@FechaInicial", General.FechaYMD(dtpFechaInicial.Value));
                    ////myRpt.Add("@FechaFinal", General.FechaYMD(dtpFechaFinal.Value));
                    myRpt.Add("ImpresoPor", DtGeneral.IdPersonal + " - " + DtGeneral.NombrePersonal);
                }

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente); 

                if (!bRegresa) 
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }
        #endregion Botones

        private void FrmKardexDeProducto_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        private void txtCodigo_Validating(object sender, CancelEventArgs e) 
        {
            if (txtCodigo.Text.Trim() == "")
            {
                lblDescripcion.Text = "";
                myGrid.Limpiar();
                txtCodigo.Focus();
            }
            else 
            {

                leer.DataSetClase = query.ClavesSSA_Sales(txtCodigo.Text, true, "txtCodigo_Validating");
                if (!leer.Leer()) 
                {
                }
                else
                {
                    lblDescripcion.Text = leer.Campo("DescripcionClave"); 
                }
            } 
        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.ClavesSSA_Sales("txtCodigo_KeyDown"); 
                if (leer.Leer())
                {
                    txtCodigo.Text = leer.Campo("ClaveSSA");
                    lblDescripcion.Text = leer.Campo("DescripcionClave"); 
                }
            }
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            lblDescripcion.Text = ""; 
        }

        private void dtpFechaFinal_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFechaFinal.Value < dtpFechaInicial.Value)
            {
                dtpFechaFinal.Value = dtpFechaInicial.Value;
            }
        }

        private void dtpFechaInicial_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFechaFinal.Value < dtpFechaInicial.Value)
            {
                dtpFechaFinal.Value = dtpFechaInicial.Value;
            }
        }

        private void dtpFechaInicial_Validating(object sender, CancelEventArgs e)
        {
            if (dtpFechaFinal.Value < dtpFechaInicial.Value)
            {
                dtpFechaFinal.Value = dtpFechaInicial.Value;
            }
        }

        private void dtpFechaFinal_Validating(object sender, CancelEventArgs e)
        {
            if (dtpFechaFinal.Value < dtpFechaInicial.Value)
            {
                dtpFechaFinal.Value = dtpFechaInicial.Value;
            }
        } 
    }
}
