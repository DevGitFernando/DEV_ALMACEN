using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.ExportarExcel;

namespace Almacen.ControlDistribucion
{
    public partial class FrmRPTPedidosCedisExistencias : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer Leer, LeerExcel;
        clsExportarExcelPlantilla xpExcel;
        clsDatosCliente datosCliente;

        public FrmRPTPedidosCedisExistencias()
        {
            InitializeComponent();

            //cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            //cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite; 

            Leer = new clsLeer(ref cnn);
            LeerExcel = new clsLeer(ref cnn);
            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "ConsultaDeExistenciaFarmacias");
        }

        private void FrmRPTPedidosCedisExistencias_Load(object sender, EventArgs e)
        {
            Fg.IniciaControles();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles();
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            if (VerificarPedidos())
            {
                GenerarDatos();
            }
        }

        private bool VerificarPedidos()
        {
            bool bRegresa = true;

            string sSql = string.Format(
                "Exec spp_Mtto_Pedidos_Cedis_RevisarStatus \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @FechaInicial = '{2}', @FechaFinal = '{3}' \n", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, 
                General.FechaYMD(dtpFechaEntrega.Value), General.FechaYMD(dtpFechaEntrega_Final.Value) 
                );

            if (!Leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(Leer, "VerificarPedidos()");
                General.msjError("Ocurrió un error al leer la información de los pedidos.");
            }

            return bRegresa;
        }

        private void GenerarDatos()
        {
            clsGenerarExcel excel;
            string sNombreDocumento = "Existencia_Requerida_En_Picking";
            string sNombreHoja = "Cantidad_Picking";
            int iColBase = 2, iColsEncabezado = 1, iRenglon = 1;

            string sConcepto = "";


            string sSql = string.Format("Exec spp_RPT_Pedidos_Cedis_Existencias_Pickeo \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmaciaSurte = '{2}', @MesesCaducar = {3}, @FechaEntrega = '{4}', @FechaEntregaFinal = '{5}' ",
                            DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, Convert.ToInt32(nmMesesCaducidad.Value),
                            General.FechaYMD(dtpFechaEntrega.Value), General.FechaYMD(dtpFechaEntrega_Final.Value));

            if (!Leer.Exec(sSql))
            {
                Error.GrabarError(Leer, "GenerarDatos()");
                General.msjError("Ocurrió un error al leer la información de los pedidos.");
            }
            else 
            {
                if(!Leer.Leer())
                {
                    General.msjUser("No se encontro información para generar el reporte solicitado.");
                }
                else 
                {
                    excel = new clsGenerarExcel();
                    excel.RutaArchivo = @"C:\\Excel";
                    excel.NombreArchivo = sNombreDocumento;
                    excel.AgregarMarcaDeTiempo = true;

                    if (excel.PrepararPlantilla(sNombreDocumento))
                    {
                        excel.ArchivoExcel.Worksheets.Add(sNombreHoja);
                        //excel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                        //excel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, DtGeneral.EstadoConectadoNombre);
                        //excel.EscribirCeldaEncabezado(sNombreHoja, 4, iColBase, iColsEncabezado, 14, sConcepto);
                        //excel.EscribirCeldaEncabezado(sNombreHoja, 6, iColBase, iColsEncabezado, 12, string.Format("Fecha de Impresión: {0} ", General.FechaSistemaObtener()));

                        iRenglon = 2;
                        //excel.LlenarDetalleHorizontal(sNombreHoja, iRenglon, iColBase, leer.DataSetClase); 
                        excel.InsertarTabla(sNombreHoja, iRenglon, iColBase, Leer.DataSetClase);

                        //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                        excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                        excel.CerraArchivo();

                        excel.AbrirDocumentoGenerado(true);
                    }
                }
            }
        }
    }
}
