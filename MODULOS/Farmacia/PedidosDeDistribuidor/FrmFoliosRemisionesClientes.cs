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
    public partial class FrmFoliosRemisionesClientes : FrmBaseExt
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

        public FrmFoliosRemisionesClientes()
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

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirFoliosRemisiones();
        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            btnImprimir.Enabled = false;
            lst.Limpiar();
            txtIdDistribuidor.Focus();
        }

        private void CargarFoliosRemisiones()
        {
            string sSql = "", sStatus = "", sFiltroCliente = "", sFiltroRefDocto = "";            

            if (rdoTerminadas.Checked)
            {
                sStatus = "T";
            }

            if (rdoSinTerminar.Checked)
            {
                sStatus = "A";
            }

            if (txtCliente.Text.Trim() != "")
            {
                sFiltroCliente = string.Format(" and CodigoCliente = '{0}' ", txtCliente.Text );
            }

            if (txtReferenciaDocto.Text.Trim() != "")
            {
                sFiltroRefDocto = string.Format(" and ReferenciaPedido like '%{0}%' ", txtReferenciaDocto.Text);
            }
                 

            sSql = string.Format(" Select Folio, 'Referencia Documento' = ReferenciaPedido, 'Fecha Documento' = FechaDocumento, " +
                                " Observaciones " +
	                            " From vw_RemisionesDistEnc (Nolock) " +
	                            " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' " +
                                " and IdDistribuidor = '{3}' and Status = '{4}'  " +
                                " and Convert(varchar(10), FechaDocumento, 120) = '{5}'  {6}  {7} ", sEmpresa, sEstado,
                                sFarmacia, txtIdDistribuidor.Text, sStatus, General.FechaYMD(dtpFechaDoc.Value, "-"), sFiltroCliente, sFiltroRefDocto);

            lst.Limpiar();

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarFoliosRemisiones()");
            }
            else
            {
                if (leer.Leer())
                {
                    lst.CargarDatos(leer.DataSetClase, true, true);
                    btnImprimir.Enabled = true;
                }
                else
                {
                    General.msjAviso("No se encontro Información bajo los criterios especificados....");
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

            //if ( bRegresa && txtCliente.Text.Trim() == "")
            //{
            //    bRegresa = false;
            //    General.msjAviso("Cliente Incorrecto, Verifique..");
            //    txtCliente.Focus();
            //}

            if (bRegresa && !rdoTerminadas.Checked && !rdoSinTerminar.Checked)
            {
                bRegresa = false;
                General.msjAviso("Seleccione el Status, Verifique..");
                rdoTerminadas.Focus();
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
            
        }

        private void btnTerminarFolio_Click(object sender, EventArgs e)
        {
            string sFolio = "";

            sFolio = lst.GetValue(1);

            FrmRemisionesDistribuidor f = new FrmRemisionesDistribuidor();
            f.LevantaForma(sFolio);
        }
        #endregion Eventos_ListView

        #region EventosCliente
        private void txtCliente_Validating(object sender, CancelEventArgs e)
        {
            
            if (txtCliente.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Distribuidor_Clientes(sEstado, txtIdDistribuidor.Text.Trim(), txtCliente.Text.Trim(), "txtCliente_Validating");
                if (leer.Leer())
                {
                    CargaDatosCliente();                    
                }
                else
                {
                    txtCliente.Focus();
                }
            }
        }

        private void txtCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.Distribuidor_Clientes(sEstado, txtIdDistribuidor.Text.Trim(), "txtCliente_KeyDown");

                if (leer.Leer())
                {
                    CargaDatosCliente();
                }
            }
        }

        private void CargaDatosCliente()
        {
            if (leer.Campo("Status").ToUpper() == "A")
            {
                string sNombre = "";
                sNombre = string.Format("{0} -- {1}", leer.Campo("NombreCliente"), leer.Campo("Farmacia"));

                txtCliente.Text = leer.Campo("CodigoCliente");
                lblCliente.Text = sNombre;
            }
            else
            {
                General.msjUser("El Distribuidor " + leer.Campo("NombreCliente") + " actualmente se encuentra cancelado, verifique. ");
                txtCliente.Text = "";
                lblCliente.Text = "";
                txtCliente.Focus();
            }
        }
        #endregion EventosCliente

        #region Impresion
        private void ImprimirFoliosRemisiones()
        {
            bool bRegresa = false;
            string sStatus = "", sFolio = "";            

            if (rdoTerminadas.Checked)
            {
                sStatus = "T";
            }
            if (rdoSinTerminar.Checked)
            {
                sStatus = "A";
            }

            sFolio = lst.GetValue(1);           

            DatosCliente.Funcion = "ImprimirFoliosRemisiones()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;

            myRpt.RutaReporte = DtGeneral.RutaReportes;

            if (rdoTodos.Checked)
            {
                myRpt.NombreReporte = "PtoVta_ListadoRemisionesDistribuidor.rpt";

                myRpt.Add("IdEmpresa", sEmpresa);
                myRpt.Add("IdEstado", sEstado);
                myRpt.Add("IdFarmacia", sFarmacia);
                myRpt.Add("IdDistribuidor", txtIdDistribuidor.Text);
                myRpt.Add("CodigoCliente", txtCliente.Text);
                myRpt.Add("Status", sStatus);
                myRpt.Add("FechaDocumento", General.FechaYMD(dtpFechaDoc.Value, "-"));
               
            }

            if (rdoIndividual.Checked)
            {
                myRpt.NombreReporte = "PtoVta_RemisionesDistribuidor.rpt";

                myRpt.Add("IdEmpresa", sEmpresa);
                myRpt.Add("IdEstado", sEstado);
                myRpt.Add("IdFarmacia", sFarmacia);
                myRpt.Add("Folio", sFolio);
            }

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }
        #endregion Impresion     
                
    }
}
