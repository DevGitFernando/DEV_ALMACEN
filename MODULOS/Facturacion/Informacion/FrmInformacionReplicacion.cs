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
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.FuncionesGenerales;

using DllFarmaciaSoft;
using DllTransferenciaSoft.IntegrarInformacion;

namespace Facturacion.Informacion
{
    public partial class FrmInformacionReplicacion : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer, leer2, leerExcel, leerExcelFact;
        clsDatosCliente DatosCliente;
        clsAyudas Ayudas;
        clsConsultas Consultas;
        clsListView lst;
        clsExportarExcelPlantilla xpExcel;

        Thread _workerThread;

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sPersonal = DtGeneral.IdPersonal;

        clsAuditoria auditoria;

        private enum Cols
        {
            Ninguna = 0,
            Folio = 2, Factura = 3, Fecha = 4, TipoFactura = 5, Importe = 6, Status = 7, Insumo = 8
        }

        public FrmInformacionReplicacion()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();


            CheckForIllegalCrossThreadCalls = false;
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayudas = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "");
            leer = new clsLeer(ref cnn);
            leer2 = new clsLeer(ref cnn);
            leerExcel = new clsLeer(ref cnn);
            leerExcelFact = new clsLeer(ref cnn);

            Error = new clsGrabarError(DtGeneral.Modulo, DtGeneral.Version, this.Name);
            auditoria = new clsAuditoria(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal,
                                        DtGeneral.IdSesion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);

            lst = new clsListView(lstFacturas);
            lst.OrdenarColumnas = false;
        }

        private void FrmInformacionReplicacion_Load(object sender, EventArgs e)
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
            //Cargar_Folios_Facturas();
            IniciarProcesamiento();
        }

        private void IniciarProcesamiento()
        {
            bSeEncontroInformacion = false;
            btnNuevo.Enabled = false;
            IniciaToolBar(false, false);

            bSeEjecuto = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();


            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(1000);

            _workerThread = new Thread(this.ObtenerInformacion);
            _workerThread.Name = "ObteniendoDatos";
            _workerThread.Start();

            btnNuevo.Enabled = true;
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            ////FrmInformacionRemisionadoPendiente f = new FrmInformacionRemisionadoPendiente();
            ////f.ShowInTaskbar = false;
            ////f.Show(this); 
        }

        private void btnGetInformacion_Click(object sender, EventArgs e)
        {
            DllFarmaciaSoft.GetInformacionManual.FrmGruposDeInformacion f = new DllFarmaciaSoft.GetInformacionManual.FrmGruposDeInformacion();

            ////f.MdiParent = this; 
            f.ShowDialog(this);
        }

        private void btnIntegrarPaquetesDeDatos_Click(object sender, EventArgs e)
        {
            FrmIntegrarPaquetesDeDatos f = new FrmIntegrarPaquetesDeDatos();
            f.ShowDialog(this);
        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);

            IniciaToolBar(true, false);
            lst.Limpiar();
        }

        private void ObtenerInformacion()
        {
            string sSql = "", sWhereFecha = "";

            bEjecutando = true;
            this.Cursor = Cursors.WaitCursor;



            sSql = string.Format("Exec spp_FACT_INFO__StatusInformacion_Farmacias \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}' \n", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada
                );
            lst.Limpiar();

            if (!leer.Exec(sSql))
            {
                bSeEncontroInformacion = false;
                Error.GrabarError(leer, "ObtenerInformacion()");
                General.msjError("Ocurrió un error al obtener la información.");
            }
            else
            {
                if (leer.Leer())
                {
                    lst.CargarDatos(leer.DataSetClase, true, true);
                    IniciaToolBar(true, true);
                    bSeEncontroInformacion = true;
                }
                else
                {
                    bSeEncontroInformacion = false;
                    General.msjAviso("No se encontro información.");
                }
            }


            bEjecutando = false;
            this.Cursor = Cursors.Default;
        }

        private void IniciaToolBar(bool Ejecutar, bool Exportar)
        {
            btnEjecutar.Enabled = Ejecutar;
            btnExportar.Enabled = Exportar;
        }
        #endregion Funciones
        
    }
}
