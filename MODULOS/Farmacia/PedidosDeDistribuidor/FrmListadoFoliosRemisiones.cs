using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;

namespace Farmacia.PedidosDeDistribuidor
{
    public partial class FrmListadoFoliosRemisiones : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        clsDatosCliente DatosCliente;
        clsListView lst;

        clsConsultas Consultas;
        clsAyudas Ayudas;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        public FrmListadoFoliosRemisiones()
        {
            InitializeComponent();
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");                        
            leer = new clsLeer(ref cnn);

            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Ayudas = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);

            lst = new clsListView(lstFoliosRemisiones);
            lst.OrdenarColumnas = true;
            lst.PermitirAjusteDeColumnas = true;
        }

        private void FrmListadoFoliosRemisiones_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (ValidaDatos())
            {
                CargarFoliosRemisiones();
            }
        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            lst.Limpiar();
            rdoAdmon.Focus();
        }

        private void CargarFoliosRemisiones()
        {
            string sSql = "";
            int iEsAdministrado = 0;

            if (rdoAdmon.Checked)
            {
                iEsAdministrado = 1;
            }

            if (rdoNoAdmon.Checked)
            {
                iEsAdministrado = 0;
            }

           
            sSql = string.Format(" Select Folio, 'Referencia Documento' = ReferenciaPedido, 'Codigo Cliente' = CodigoCliente, " +
	                            " Cliente, Observaciones " +
	                            " From vw_RemisionesDistEnc (Nolock) " +
	                            " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' " +
	                            " and IdDistribuidor = '{3}' and EsAdministrado = {4}  " +
                                " and Convert(varchar(10), FechaDocumento, 120) = '{5}' ", sEmpresa, sEstado,
                                sFarmacia, txtIdDistribuidor.Text, iEsAdministrado,General.FechaYMD(dtpFechaDoc.Value, "-"));

            lst.Limpiar(); 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarFoliosRemisiones()");
                General.msjError("Ocurrió un error al obtener la lista de remisiones."); 
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjAviso("No se encontro información con los criterios especificados."); 
                }
                else 
                {
                    lst.CargarDatos(leer.DataSetClase, true, true);
                }
            }

        }

        private void CargaDatosDistribuidor()
        {
            //Se hace de esta manera para la ayuda. 

            if (leer.Campo("Status").ToUpper() == "A")
            {
                txtIdDistribuidor.Text = leer.Campo("IdDistribuidor");
                lblDistribuidor.Text = leer.Campo("NombreDistribuidor");
            }
            else
            {
                General.msjUser("El Distribuidor " + leer.Campo("NombreDistribuidor") + " actualmente se encuentra cancelado, verifique. ");
                txtIdDistribuidor.Text = "";
                lblDistribuidor.Text = "";
                txtIdDistribuidor.Focus();
            }
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtIdDistribuidor.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("Distribuidor Incorrecto, Verifique..");
                txtIdDistribuidor.Focus();
            }

            if (bRegresa && !rdoAdmon.Checked && !rdoNoAdmon.Checked)
            {
                bRegresa = false;
                General.msjAviso("Seleccione Tipo Unidad, Verifique..");
                rdoAdmon.Focus();
            }

            return bRegresa;
        }
        #endregion Funciones

        #region Eventos_Distribuidor
        private void txtIdDistribuidor_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdDistribuidor.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Distribuidores(txtIdDistribuidor.Text.Trim(), "txtIdDistribuidor_Validating");
                if (leer.Leer())
                {
                    CargaDatosDistribuidor();                    
                }
                else
                {
                    txtIdDistribuidor.Focus();
                }
            }
        }

        private void txtIdDistribuidor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.Distribuidores("txtIdDistribuidor_KeyDown");

                if (leer.Leer())
                {
                    CargaDatosDistribuidor();
                }
            }
        }

        private void txtIdDistribuidor_TextChanged(object sender, EventArgs e)
        {
            lblDistribuidor.Text = "";
        }
        #endregion Eventos_Distribuidor

        #region Eventos_ListView
        private void lstFoliosRemisiones_DoubleClick(object sender, EventArgs e)
        {
            string sFolio = "";

            sFolio = lst.GetValue(1);

            FrmRemisionesDistribuidor f = new FrmRemisionesDistribuidor();
            f.LevantaForma(sFolio);
        }
        #endregion Eventos_ListView
    }
}
