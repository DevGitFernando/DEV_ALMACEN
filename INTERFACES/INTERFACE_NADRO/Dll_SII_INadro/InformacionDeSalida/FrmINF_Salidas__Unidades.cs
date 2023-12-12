using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.IO;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Inventario;
using Dll_SII_INadro;
using Dll_SII_INadro.GenerarArchivos;
using Dll_SII_INadro.Informacion; 

namespace Dll_SII_INadro.InformacionDeSalida
{
    public partial class FrmINF_Salidas__Unidades : FrmBaseExt 
    {
        enum Cols 
        { 
	        Cliente = 1, NombreCliente = 2, IdFarmacia = 3, Farmacia = 4, TipoDeUnidad = 5 
        } 

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsAyudas Ayuda;
        clsConsultas Consultas;

        clsListView lst;
        clsDatosCliente DatosCliente;

        string sCliente = "";
        string sFecha = "";
        TipoDeDocumento iTipoProceso = TipoDeDocumento.Ninguno;
        string sProceso = ""; 

        public FrmINF_Salidas__Unidades()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();

            DatosCliente = new clsDatosCliente(GnDll_SII_INadro.DatosApp, this.Name, "");

            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, General.Modulo, this.Name, General.Version);
            Ayuda = new clsAyudas(General.DatosConexion, General.Modulo, this.Name, General.Version);
        }

        #region Form
        private void FrmINF_Salidas__Unidades_Load(object sender, EventArgs e)
        {
            InicializarPantalla();
        }
        #endregion Form

        #region Botones
        private void InicializarPantalla()
        {
            bool bActivar = false;

            btnEjecutar.Enabled = true;
            btnProcesarDocumentos.Enabled = false; 
            rdoExistencias.Checked = true;

            rdoExistencias.Enabled = bActivar;
            rdoRecibos.Enabled = bActivar;
            rdoSurtidos.Enabled = bActivar;
            rdoTomaDeExistencias.Enabled = bActivar;
            rdoRemisiones.Enabled = bActivar;
            dtpFechaInicial.Enabled = bActivar;
            dtpFechaFinal.Enabled = bActivar; 

            ObtenerListadoDeClientes(); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            ObtenerListadoDeClientes();
        }

        private void btnProcesarDocumentos_Click(object sender, EventArgs e)
        {
            GenerarDocumentoPorFarmacia(); 
        }
        #endregion Botones

        #region Informacion
        private void ObtenerListadoDeClientes()
        {
            bool bActivar = false; 
            string sSql = string.Format(
                "Select  IdCliente, CodigoCliente, NombreCliente, IdEstado, Estado, IdFarmacia, Farmacia, IdTipoUnidad, TipoDeUnidad, EsDeSurtimiento, Status " + 
                "From vw_INT_ND_Clientes (NoLock) \n " +
                "Where IdEstado = '{0}' and IdFarmacia = '{1}' and EsDeSurtimiento = 0 \n ", 
                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            sCliente = ""; 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ObtenerListadoDeClientes()");
                General.msjError("Ocurrió un error al obtener el listado de clientes.");
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjUser(string.Format("La unidad {0} no se encuentra en la lista de unidades configuradas para utilizar esta opción.",DtGeneral.FarmaciaConectadaNombre));
                }
                else
                {
                    sCliente = leer.Campo("CodigoCliente");
                    bActivar = true; 
                }
            }

            btnEjecutar.Enabled = !bActivar; 
            btnProcesarDocumentos.Enabled = bActivar;

            rdoExistencias.Enabled = bActivar;
            rdoRecibos.Enabled = bActivar;
            rdoSurtidos.Enabled = bActivar;
            rdoTomaDeExistencias.Enabled = bActivar;
            rdoRemisiones.Enabled = bActivar;
            dtpFechaInicial.Enabled = bActivar;
            dtpFechaFinal.Enabled = bActivar; 

        }
        #endregion Informacion

        #region Menu 
        private void btnDocumentoGeneral_Click(object sender, EventArgs e)
        {
            GenerarDocumentoGeneral(); 
        }

        private void btnDocumentoPorFarmacia_Click(object sender, EventArgs e)
        {
            GenerarDocumentoPorFarmacia(); 
        }

        private void PrepararMenu(TipoDeDocumento Proceso)
        {
            bool bActivarFechas = true;

            btnEjecutar.Enabled = true;
            btnProcesarDocumentos.Enabled = true;
            iTipoProceso = Proceso; 

            switch (Proceso)
            {
                case TipoDeDocumento.Existencias:
                    //bActivarFechas = false; 
                    sProceso = " existencias ";
                    btnProcesarDocumentos.Text = "Generar existencia general"; 
                    break;

                case TipoDeDocumento.Surtidos:
                    sProceso = " surtidos ";
                    btnProcesarDocumentos.Text = "Generar surtidos general";
                    break;

                case TipoDeDocumento.Recibos:
                    sProceso = " recibos ";
                    btnProcesarDocumentos.Text = "Generar recibos general";
                    break;

                case TipoDeDocumento.Remisiones:
                    sProceso = " remisiones ";
                    btnProcesarDocumentos.Text = "Generar remisiones general"; 
                    break;

                case TipoDeDocumento.TomaDeExistencia:
                    sProceso = " toma de existencia ";
                    btnProcesarDocumentos.Text = "Generar toma de existencia general";
                    break; 

                ////case 6:
                ////    bActivarFechas = false;
                ////    sProceso = " catálogo de información ";
                ////    btnProcesarDocumentos.Text = "Generar catálogo";

                ////    break; 
            }

            btnProcesarDocumentos.ToolTipText = btnProcesarDocumentos.Text; 
            dtpFechaInicial.Enabled = bActivarFechas;
            dtpFechaFinal.Enabled = bActivarFechas; 

        }

        private void GenerarDocumentoGeneral()
        {
            //////bool bEjecutar = false;
            //////string sMsj = string.Format("¿ Desea ejecutar la generación de documentos de {0} para todas las unidades ?", sProceso);

            //////if (General.msjConfirmar(sMsj) == System.Windows.Forms.DialogResult.Yes)
            //////{
            //////    bEjecutar = true;
            //////} 

            //////if (bEjecutar)
            //////{
            //////    switch (iTipoProceso)
            //////    {
            //////        case 1:
            //////            Existencias ex = new Existencias();
            //////            ex.GenerarExistencias(dtpFechaInicial.Value, dtpFechaFinal.Value);
            //////            ex.MsjFinalizado(); 
            //////            break;

            //////        case 2:
            //////            Surtidos su = new Surtidos();
            //////            su.GenerarSurtidos(dtpFechaInicial.Value, dtpFechaFinal.Value);
            //////            su.MsjFinalizado();
            //////            break;

            //////        case 3:
            //////            Recibos re = new Recibos();
            //////            re.GenerarRecibos(dtpFechaInicial.Value, dtpFechaFinal.Value);
            //////            re.MsjFinalizado();
            //////            break;

            //////        case 4:
            //////            Remisiones rm = new Remisiones();
            //////            rm.GenerarRemisiones(dtpFechaInicial.Value, dtpFechaFinal.Value);
            //////            rm.MsjFinalizado(); 
            //////            break;

            //////        case 5:
            //////            TomaDeExistencias te = new TomaDeExistencias();
            //////            te.GenerarTomaDeExistencias(dtpFechaInicial.Value, dtpFechaFinal.Value);
            //////            te.MsjFinalizado();
            //////            break;

            //////        case 6:
            //////            GenerarCatalogo gn = new GenerarCatalogo();
            //////            if (gn.Generar())
            //////            {
            //////                gn.MsjFinalizado(); 
            //////            }
            //////            break;
            //////    }
            //////}
        }

        private void GenerarDocumentoPorFarmacia()
        {
            bool bEjecutar = false;
            string sMsj = string.Format("¿ Desea ejecutar la generación de documentos de {0} ?", sProceso);

            if (General.msjConfirmar(sMsj) == System.Windows.Forms.DialogResult.Yes)
            {
                bEjecutar = true;
            }

            if (bEjecutar)
            {
                switch (iTipoProceso)
                {
                    case TipoDeDocumento.Existencias:
                        Existencias ex = new Existencias();
                        ex.GenerarExistencias(DtGeneral.FarmaciaConectada, sCliente, dtpFechaInicial.Value, dtpFechaFinal.Value);
                        ex.MsjFinalizado();
                        break;

                    case TipoDeDocumento.Surtidos:
                        Surtidos su = new Surtidos();
                        su.GenerarSurtidos(DtGeneral.FarmaciaConectada, sCliente, dtpFechaInicial.Value, dtpFechaFinal.Value);
                        su.MsjFinalizado();
                        break;

                    case TipoDeDocumento.Recibos:
                        Recibos re = new Recibos();
                        re.GenerarRecibos(DtGeneral.FarmaciaConectada, sCliente, dtpFechaInicial.Value, dtpFechaFinal.Value);
                        re.MsjFinalizado();
                        break;

                    case TipoDeDocumento.Remisiones:
                        RemisionesUnidades rm = new RemisionesUnidades();
                        rm.GUID = Guid.NewGuid().ToString(); 
                        rm.GenerarRemisiones(DtGeneral.FarmaciaConectada, sCliente, dtpFechaInicial.Value, dtpFechaFinal.Value);
                        break;

                    case TipoDeDocumento.TomaDeExistencia:
                        TomaDeExistencias te = new TomaDeExistencias();
                        te.GenerarTomaDeExistencias(sCliente, dtpFechaInicial.Value, dtpFechaFinal.Value);
                        te.MsjFinalizado();
                        break;
                }
            }
        }

        private void rdoExistencias_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoExistencias.Checked) PrepararMenu(TipoDeDocumento.Existencias);
        }

        private void rdoSurtidos_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSurtidos.Checked) PrepararMenu(TipoDeDocumento.Surtidos);
        }

        private void rdoRecibos_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoRecibos.Checked) PrepararMenu(TipoDeDocumento.Recibos);
        }

        private void rdoRemisiones_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoRemisiones.Checked) PrepararMenu(TipoDeDocumento.Remisiones);
        }

        private void rdoTomaDeExistencias_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoTomaDeExistencias.Checked) PrepararMenu(TipoDeDocumento.TomaDeExistencia);
        }
        #endregion Menu

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            FrmINF_IntegrarCatalogos_Unidades f = new FrmINF_IntegrarCatalogos_Unidades();
            f.ShowDialog(); 
        }
    }
}
