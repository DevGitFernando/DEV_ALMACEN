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

namespace Almacen.ControlDistribucion
{
    public partial class FrmRegistro_RutaDeDistribucionDetalles : FrmBaseExt
    {
        private enum Cols
        {
            Ninguno = 0,
            Ruta = 1, 
            Folio, Fecha, Referencia, Piezas, Modificado, Agregar
        }
        bool bSinInformacion = true;
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer Leer;
        public clsLeer LeerAux;

        int iTipo = 0;
        int iDiasRevision = 0;
        int iTipoDeProceso = 0;
        string sIdRuta = "";
        string sIdPrioridad = ""; 
        public clsGrid myGrid;


        public FrmRegistro_RutaDeDistribucionDetalles( int Tipo, int TipoDeProceso, int DiasRevision, string IdRuta, string IdPrioridad )
        {
            InitializeComponent();
            Leer = new clsLeer(ref cnn);
            LeerAux = new clsLeer(ref cnn);

            myGrid = new clsGrid(ref grdDetalle, this);
            myGrid.AjustarAnchoColumnasAutomatico = true;
            myGrid.EstiloDeGrid = eModoGrid.ModoRow;
            myGrid.SetOrder(true);

            iTipo = Tipo;
            iDiasRevision = DiasRevision;
            iTipoDeProceso = TipoDeProceso;
            sIdRuta = IdRuta;
            sIdPrioridad = IdPrioridad; 
        }

        private void FrmRegistro_RutaDeDistribucionDetalles_Load(object sender, EventArgs e)
        {
            CargarDatos();

            if (bSinInformacion)
            {
                this.Close();
            }

        }


        private void CargarDatos()
        {
            string sMsjEnc = "Asignación de Traspasos en Ruta.";
            string sMsjNoEncontrado = "No existen Traspasos pendientes para envio en ruta.";
            string sMsjError = "Error al consultar listado de Traspasos.";

            if (iTipo == 1)
            {
                sMsjEnc = "Asignación de Dispersiones en Ruta.";
                sMsjNoEncontrado = "No existen Dispersiones pendientes para envio en ruta.";
                sMsjError = "Error al consultar listado de Dispersiones.";
                myGrid.PonerEncabezado((int)Cols.Folio, "Folio Venta");
                myGrid.PonerEncabezado((int)Cols.Referencia, "Beneficiario");
            }

            myGrid.Limpiar(false);
            this.Text = sMsjEnc;
            string sSql = string.Format("Exec spp_Rpt_RutasDistribucionDetalles  " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Tipo = '{3}', @DiasRevision = '{4}', @OrigenDatos = '{5}', @IdRuta = '{6}', @Prioridad = '{7}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, iTipo, iDiasRevision, iTipoDeProceso, sIdRuta, sIdPrioridad);

            if (!Leer.Exec(sSql))
            {
                General.msjError(sMsjError);
                Error.GrabarError(Leer, "CargarDatos");
            }
            else 
            {
                if (!Leer.Leer())
                {
                    General.msjAviso(sMsjNoEncontrado);
                }
                else
                {
                    myGrid.LlenarGrid(Leer.DataSetClase);
                    bSinInformacion = false;
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void grdTransferencias_EditModeOff(object sender, EventArgs e)
        {
            myGrid.SetValue(myGrid.ActiveRow, (int)Cols.Modificado, true);
        }

        private void chkMarcarDesmarcar_CheckedChanged( object sender, EventArgs e )
        {
            myGrid.SetValue(Cols.Modificado, true);
            myGrid.SetValue(Cols.Agregar, chkMarcarDesmarcar.Checked);
        }
    }
}