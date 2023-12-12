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
using SC_SolutionsSystem.Errores;

using DllPedidosClientes;

namespace DllPedidosClientes.Reportes
{
    public partial class FrmListadoBeneficiarios : FrmBaseExt
    {
        clsConexionSQL ConexionLocal; // = new clsConexionSQL(General.DatosConexion); 
        clsDatosConexion DatosDeConexionRemota;
        clsLeer myLeer;
        clsLeer myLlenaDatos;
        DataSet dtsCargarDatos = new DataSet();        
        clsGrid Grid;        

        clsDatosCliente DatosCliente;
        wsCnnClienteAdmin.wsCnnClientesAdmin conexionWeb; // = new Farmacia.wsFarmacia.wsCnnCliente();

        // bool bContinua = true;
        bool bSeEncontroInformacion = false;

        // string sMensaje = "";        
        string sFormato = "#,###,##0.###0";        

        private enum Cols
        {
            Ninguna = 0,
            Beneficiario = 1, FolioRef = 2, Nombre = 3, Total = 4, Check = 5
        }

        public FrmListadoBeneficiarios(clsDatosConexion DatosDeConexion)
        {
            InitializeComponent();

            DatosDeConexionRemota = DatosDeConexion;
            ConexionLocal = new clsConexionSQL(DatosDeConexionRemota);
            ConexionLocal.SetConnectionString();

            DatosCliente = new clsDatosCliente(DtGeneralPedidos.DatosApp, this.Name, "");
            conexionWeb = new wsCnnClienteAdmin.wsCnnClientesAdmin();
            conexionWeb.Url = General.Url;

            myLeer = new clsLeer(ref ConexionLocal);
            myLlenaDatos = new clsLeer(ref ConexionLocal);                       

            Grid = new clsGrid(ref grdBeneficiarios, this);           
            grdBeneficiarios.EditModeReplace = true;

            Error = new clsGrabarError(DtGeneralPedidos.DatosApp, this.Name);
        }

        private void FrmListadoBeneficiarios_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(this, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //Fg.IniciaControles(this, true);
            grdBeneficiarios.Enabled = true;//Se habilita a pie ya que el IniciaControles no lo hace.
            bSeEncontroInformacion = false;
            IniciaControles(false, true, false);
            rdoRptConcentrado.Checked = true;
            rdoRptConcentrado.Focus();
            chkTodos.Checked = false;
            //Grid.SetValue((int)Cols.Check, false);
            //Grid.Limpiar(true);
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (GenerarRptBeneficiarios())
            {
                IniciaControles(true, false, true);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirInformacion();
        }
        #endregion Botones

        #region Funciones

        private void IniciaControles(bool bNuevo, bool bEjecutar, bool bImprimir)
        {
            btnNuevo.Enabled = bNuevo;
            btnEjecutar.Enabled = bEjecutar;
            btnImprimir.Enabled = bImprimir;
            btnNuevo.Visible = bNuevo;
        }

        public void MostrarListaBeneficiarios(clsDatosConexion DatosDeConexion)
        {
            string sTabla = "";
            DatosDeConexionRemota = DatosDeConexion;
            ConexionLocal = new clsConexionSQL(DatosDeConexionRemota);
            ConexionLocal.SetConnectionString();

            myLeer = new clsLeer(ref ConexionLocal);

            if (DtGeneralPedidos.ClienteConectado == TipoDeClienteExterno.Administracion_Regional)
            {
                sTabla = "RptAdmonDispensacion_Secretaria";
            }

            if (DtGeneralPedidos.ClienteConectado == TipoDeClienteExterno.Administracion_Unidad)
            {
                sTabla = "RptAdmonDispensacion_Unidad";
            }

            string sSql = string.Format(" Select IdBeneficiario, FolioReferencia, Beneficiario, Sum(ImporteEAN) As Total " + 
	                                    " From {0} (Nolock) " +
	                                    " Group By IdBeneficiario, Beneficiario, FolioReferencia " +
	                                    " Order By Beneficiario ", sTabla );

            Grid.Limpiar(false);

            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "MostrarListaBeneficiarios");
            }
            else
            {
                if (myLeer.Leer())
                {
                    Grid.LlenarGrid(myLeer.DataSetClase);
                    lblTotal.Text = Grid.TotalizarColumnaDou((int)Cols.Total).ToString(sFormato); 
                }
            }           

            this.ShowDialog();
            
        }

        private bool BeneficiariosProcesar()
        {
            bool bReturn = false, bCheck = false;
            string sBeneficiario = "";

            string sSql = string.Format("Delete From Cte_BeneficiariosProcesar ");

            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "BeneficiariosProcesar()");
                General.msjError("Ocurrió un error al borrar tabla.");
                bReturn = false;
            }
            else
            {
                for (int i = 1; i <= Grid.Rows; i++)
                {
                    bCheck = Grid.GetValueBool(i, (int)Cols.Check);

                    if (bCheck)
                    {
                        bReturn = true;
                        sBeneficiario = Grid.GetValue(i, (int)Cols.Beneficiario);

                        if (sBeneficiario != "")
                        {
                            string sQuery = string.Format(" Insert Into Cte_BeneficiariosProcesar " +
                                                          " Select '{0}' ", sBeneficiario);

                            if (!myLeer.Exec(sQuery))
                            {
                                Error.GrabarError(myLeer, "BeneficiariosProcesar()");
                                General.msjError("Ocurrió un error al Insertar Beneficiarios a Procesar.");
                                bReturn = false;
                                break;
                            }
                        }
                    }
                }
            }

            return bReturn;
        }

        private bool GenerarRptBeneficiarios()
        {
            string sTabla = "";
            bool bReturn = true;

            if (DtGeneralPedidos.ClienteConectado == TipoDeClienteExterno.Administracion_Regional)
            {
                sTabla = "RptAdmonDispensacion_Secretaria";
            }

            if (DtGeneralPedidos.ClienteConectado == TipoDeClienteExterno.Administracion_Unidad)
            {
                sTabla = "RptAdmonDispensacion_Unidad";
            }

            if (BeneficiariosProcesar())
            {
                string sSql = string.Format(" Exec spp_Rpt_Admon_Cte_Beneficiarios '{0}', '{1}', '{2}' ", 
                                            sTabla, DtGeneralPedidos.EncabezadoPrincipal, DtGeneralPedidos.EncabezadoSecundario);
                sSql += "\n " + string.Format("Select top 1 * From Rpt_Admon_Cte_Beneficiarios (NoLock) ");

                if (!myLeer.Exec(sSql))
                {
                    Error.GrabarError(myLeer, "GenerarRptBeneficiarios()");
                    General.msjError("Ocurrió un error al generar la Información.");
                    bReturn = false;
                }
                else
                {
                    if (myLeer.Leer())
                    {
                        bSeEncontroInformacion = true;
                    }
                }
            }
            else
            {
                bReturn = false;
            }

            return bReturn;
        }

        #endregion Funciones       

        #region Eventos

        private void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            Grid.SetValue((int)Cols.Check, chkTodos.Checked);
        }

        #endregion Eventos

        #region Impresion

        private bool validarImpresion()
        {
            bool bRegresa = true;

            if (!bSeEncontroInformacion)
            {
                bRegresa = false;
                General.msjUser("No existe información para generar el reporte, verifique.");
            }            

            return bRegresa;
        }

        private void ImprimirInformacion()
        {
            bool bRegresa = false;

            if (validarImpresion())
            {
                // El reporte se localiza fisicamente en el Servidor Regional ó Central.               

                DatosCliente.Funcion = "Imprimir()";
                clsImprimir myRpt = new clsImprimir(DatosDeConexionRemota);
                byte[] btReporte = null;

                //// Linea Para Prueba
                // DtGeneralPedidos.RutaReportes = @"D:\PROYECTO SC-SOFT\SISTEMA_INTERMED\REPORTES";

                myRpt.RutaReporte = DtGeneralPedidos.RutaReportes; 
                if (rdoRptConcentrado.Checked)
                {
                    myRpt.NombreReporte = "Cte_RegUni_BeneficiarioConcentrado";
                }

                if (rdoRptDetallado.Checked)
                {
                    myRpt.NombreReporte = "Cte_RegUni_BeneficiariosDetallado";
                }

                //if (General.ImpresionViaWeb)
                {
                    // Pendiente de Modificar 
                    DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                    DataSet datosC = DatosCliente.DatosCliente();

                    conexionWeb.Url = General.Url;
                    conexionWeb.Timeout = 300000;

                    btReporte = conexionWeb.Reporte(InfoWeb, datosC);
                    bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);

                    //////DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                    //////DataSet datosC = DatosCliente.DatosCliente();
                    //////bRegresa = DtGeneralPedidos.GenerarReporte(General.Url, myRpt, sEstado, sFarmacia, InfoWeb, datosC); 


                    //}
                    //else
                    //{
                    //// Lineas para pruebas locales ///////
                    //myRpt.CargarReporte(true);
                    //bRegresa = !myRpt.ErrorAlGenerar;
                    ////////////////////////////////////////
                }

                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
                
            }
        }

        #endregion Impresion
    }
}
