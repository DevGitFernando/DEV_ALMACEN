using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

// Implementacion de hilos 
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos;
using DllFarmaciaSoft.Ayudas;
using DllFarmaciaSoft.ExportarExcel;

using ClosedXML.Excel;
using System.Security.Authentication.ExtendedProtection.Configuration;

namespace DllFarmaciaSoft.ReportesGerenciales
{
    public partial class FrmRptGN_ExistenciasValorizadas : FrmBaseExt
    {
        //clsDatosConexion datosDeConexion;
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion); 
        clsLeer leer;
        clsLeer leer2;
        clsConsultas consultas;
        clsAyudas ayuda;
        //clsGrid grid;
        clsListView lst; 

        string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\.xls";
        clsExportarExcelPlantilla xpExcel;
        clsGenerarExcel excel;
        clsLeer leerExportarExcel;

        FrmHelpBeneficiarios helpBeneficiarios;

        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb;
        //Thread _workerThread;

        //bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        //bool bSeEjecuto = false; 

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sIdPublicoGral = GnFarmacia.PublicoGral;

        public FrmRptGN_ExistenciasValorizadas()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmRptGN_ExistenciasValorizadas");
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite; 
            
            conexionWeb = new wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            leer = new clsLeer(ref cnn);
            leer2 = new clsLeer(ref cnn);
            leerExportarExcel = new clsLeer(ref cnn);
            consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            ayuda = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Error = new clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            lst = new clsListView(lstResultado); 
        }

        #region Form 
        private void FrmRptGN_ExistenciasValorizadas_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }
        #endregion Form

        #region Botones 
        private void LimpiarPantalla()
        {
            Fg.IniciaControles(this, true);
            IniciaToolBar(false, false);
            IniciaFrames(true);
            lst.LimpiarItems();
            txtCte.Focus();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (ValidaDatos())
            {
                ObtenerInformacion_Existencias();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            if (bSeEncontroInformacion)
            {
                GenerarExcel_XML();
            }
        }
        #endregion Botones 

        #region Funciones y Procedimientos Privados
        private void IniciaToolBar(bool Ejecutar, bool Exportar)
        {
            btnEjecutar.Enabled = Ejecutar;
            btnExportarExcel.Enabled = Exportar;
        }

        private void IniciaFrames(bool Valor)
        {
            //FrameParametros.Enabled = Valor;
            txtCte.Enabled = Valor;
            txtSubCte.Enabled = Valor;
        }

        private void ObtenerInformacion_Existencias()
        {
            string sSql = "", sWherePrograma = "", sWhereSubPrograma = "", sWhereBeneficiario = "";
            int iTipoReporte = 1;
            int iAplicarMascara = 0;
            
            
            IniciaToolBar(false, false);
            IniciaFrames(false);
            btnNuevo.Enabled = false;



            lst.LimpiarItems();
            bSeEncontroInformacion = false;


            sSql = string.Format("Exec spp_Rpt_GN___ValorizarExistencias \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdCliente = '{3}', @IdSubCliente = '{4}' \n", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                txtCte.Text.Trim(), txtSubCte.Text.Trim() );


            if(!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ObtenerInformacion_Existencias");
                General.msjError("Ocurrió un error al consultar las Existencias");
                IniciaToolBar(true, false);
            }
            else
            {
                if (leer.Leer())
                {
                    bSeEncontroInformacion = true;

                    leerExportarExcel.DataSetClase = leer.DataSetClase; 

                    lst.CargarDatos(leer.DataSetClase, true, true);
                    IniciaToolBar(true, true);
                }
                else
                {
                    General.msjAviso("No se encontró información bajo los criterios especificados...");
                    IniciaToolBar(true, false);
                }
            }

            IniciaFrames(true);
            btnNuevo.Enabled = true;
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtCte.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("No ha capturado el Cliente, verifique..");
                txtCte.Focus();
            }

            if (bRegresa && txtSubCte.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("No ha capturado el Sub-Cliente, verifique..");
                txtSubCte.Focus();
            }

            return bRegresa;
        }

        private void CargarDetalles_Salidas()
        {
            string sSql = "", sWherePrograma = "", sWhereSubPrograma = "", sWhereBeneficiario = "";
            int iTipoReporte = 1;
            int iAplicarMascara = 0;
            leerExportarExcel = new clsLeer(ref cnn);

        }
        #endregion Funciones y Procedimientos Privados

        #region Eventos_Cliente
        private void txtCte_Validating(object sender, CancelEventArgs e)
        {
            leer2 = new clsLeer(ref cnn);
            if (txtCte.Text.Trim() == "")
            {
                txtCte.Text = "";
                lblCte.Text = "";
                txtSubCte.Text = "";
                lblSubCte.Text = "";
                lblSubCte.Text = "";
                txtCte.Focus();
            }
            else
            {
                leer2.DataSetClase = consultas.Farmacia_Clientes(sIdPublicoGral, sEstado, sFarmacia, txtCte.Text, "txtCte_Validating");
                if (!leer2.Leer())
                {
                    txtCte.Focus();                   
                }
                else
                {
                    txtCte.Enabled = false;
                    txtCte.Text = leer2.Campo("IdCliente");
                    lblCte.Text = leer2.Campo("NombreCliente");
                }
            }
        }

        private void txtCte_KeyDown(object sender, KeyEventArgs e)
        {            
            if (e.KeyCode == Keys.F1)
            {
                leer = new clsLeer(ref cnn);

                leer.DataSetClase = ayuda.Farmacia_Clientes(sIdPublicoGral, false, sEstado, sFarmacia, "txtCte_KeyDown");
                if (leer.Leer())
                {
                    txtCte.Enabled = false;
                    txtCte.Text = leer.Campo("IdCliente");
                    lblCte.Text = leer.Campo("NombreCliente");
                }
                else
                {
                    txtCte.Focus();
                }
            }
        }

        private void txtCte_TextChanged(object sender, EventArgs e)
        {
            lblCte.Text = "";
            txtSubCte.Text = "";
            lblSubCte.Text = "";
        }        
        #endregion Eventos_Cliente

        #region Eventos_Sub-Cliente
        private void txtSubCte_Validating(object sender, CancelEventArgs e)
        {            
            if (txtSubCte.Text.Trim() == "")
            {
                txtSubCte.Text = "";
                lblSubCte.Text = "";
            }
            else
            {
                leer2 = new clsLeer(ref cnn);

                leer2.DataSetClase = consultas.Farmacia_Clientes(sIdPublicoGral, sEstado, sFarmacia, txtCte.Text, txtSubCte.Text, "txtSubCte_Validating");
                if (!leer2.Leer())
                {
                    txtSubCte.Focus();  
                }
                else
                {
                    txtSubCte.Enabled = false;
                    txtSubCte.Text = leer2.Campo("IdSubCliente");
                    lblSubCte.Text = leer2.Campo("NombreSubCliente");
                    IniciaToolBar(true, false);
                }
            }
        }

        private void txtSubCte_KeyDown(object sender, KeyEventArgs e)
        {            
            if (e.KeyCode == Keys.F1)
            {
                if (txtCte.Text.Trim() != "")
                {
                    leer = new clsLeer(ref cnn);

                    leer.DataSetClase = ayuda.Farmacia_Clientes(sIdPublicoGral, false, sEstado, sFarmacia, txtCte.Text, "txtSubCte_KeyDown");
                    if (leer.Leer())
                    {
                        txtSubCte.Enabled = false;
                        txtSubCte.Text = leer.Campo("IdSubCliente");
                        lblSubCte.Text = leer.Campo("NombreSubCliente");
                        IniciaToolBar(true, false);
                    }
                    else
                    {
                        txtSubCte.Focus();
                    }
                }
            }
        }

        private void txtSubCte_TextChanged(object sender, EventArgs e)
        {
            lblSubCte.Text = "";
        }
        #endregion Eventos_Sub-Cliente

        #region Exportar_A_Excel
        private void GenerarExcel_XML()
        {
            string sConcepto = "";
            string sHoja = "";
            clsLeer datos = new clsLeer(); 


            excel = new clsGenerarExcel();
            excel.AgregarMarcaDeTiempo = true;
            excel.NombreArchivo = "Existencias_Valorizadas";

            if(excel.PrepararPlantilla("Existencias_Valorizadas"))
            {
                datos.DataTableClase = leerExportarExcel.Tabla(1); 
                GenearExcel_XML_Detalles("Concentrado", "Valorizado de existencias por Código EAN - Lotes", datos);


                datos.DataTableClase = leerExportarExcel.Tabla(2);
                if(datos.Registros > 0)
                {
                    GenearExcel_XML_Detalles("Detallado", "Valorizado de existencias por Código EAN - Lotes - Ubicaciones", datos);
                }

                excel.CerraArchivo();

                excel.AbrirDocumentoGenerado(true);

            }
        }

        private void GenearExcel_XML_Detalles(string sNombreHoja, string Concepto, clsLeer Detalles)
        {
            int iRen = 2, iCol = 2, iColEnc = iCol + Detalles.Columnas.Length - 1;

            string sEmpresaNom = DtGeneral.EmpresaConectadaNombre;
            string sEstadoNom = DtGeneral.EstadoConectadoNombre;
            string sFarmaciaNom = sFarmacia + " -- " + DtGeneral.FarmaciaConectadaNombre + ", " + sEstadoNom;
            string sFechaImpresion = "Fecha de Impresión: " + General.FechaSistemaFecha.ToString();
            string sCliente = "Cliente: " + txtCte.Text + " -- " + lblCte.Text;
            string sSubCliente = "Sub-Cliente: " + txtSubCte.Text + " -- " + lblSubCte.Text;


            excel.ArchivoExcel.Worksheets.Add(sNombreHoja);

            excel.EscribirCeldaEncabezado(sNombreHoja, iRen++, iCol, iColEnc, sEmpresaNom);
            excel.EscribirCeldaEncabezado(sNombreHoja, iRen++, iCol, iColEnc, sFarmaciaNom);
            excel.EscribirCeldaEncabezado(sNombreHoja, iRen++, iCol, iColEnc, Concepto);
            excel.EscribirCeldaEncabezado(sNombreHoja, iRen++, iCol, iColEnc, sCliente, XLAlignmentHorizontalValues.Left);
            excel.EscribirCeldaEncabezado(sNombreHoja, iRen++, iCol, iColEnc, sSubCliente, XLAlignmentHorizontalValues.Left);
            iRen ++;
            excel.EscribirCeldaEncabezado(sNombreHoja, iRen++, iCol, iColEnc, sFechaImpresion, XLAlignmentHorizontalValues.Left);
            iRen++;
            excel.InsertarTabla(sNombreHoja, iRen, 2, Detalles.DataSetClase);
            excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);


        }
        #endregion Exportar_A_Excel   

    }
}
