using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;

using System.Threading;

namespace DllCompras.Planeacion
{
    public partial class FrmObtenerConsumos : FrmBaseExt
    {
        enum Cols
        {
            Ninguno = 0,
            IdEstado = 1,
            Fecha = 2,
            Consumos = 3,
            Actualizar = 4
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsConsultas Consultas;
        clsLeer leer;
        clsGrid grid;
        Thread thPCM;
        clsDatosCliente datosCliente;

        string sFecha = "";
        int iRow = 0;

        public FrmObtenerConsumos()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            grid = new clsGrid(ref grdConsumos, this);

            datosCliente = new clsDatosCliente(GnInventarios.DatosApp, this.Name, "FrmObtenerConsumos");
        }

        private void FrmObtenerConsumos_Load(object sender, EventArgs e)
        {
            CargarComboEstados();
            txtMeses.Text = 6.ToString();
        }


        private void CargarComboEstados()
        {
            cboEstados.Clear();
            cboEstados.Add();

            cboEstados.Add(Consultas.EstadosConFarmacias("CargarComboEstados()"), true, "IdEstado", "NombreEstado");

            cboEstados.SelectedIndex = 0;
            cboEstados.Data = DtGeneral.EstadoConectado;
            cboEstados.Enabled = GnFarmacia.Transferencias_Interestatales__Farmacias;
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            CargarConsumos();
        }

        private void CargarConsumos()
        {
            string sSql = string.Format(" Exec spp_OcenPln_ResumenEdo @IdEstado = '{0}', @NumMeses = '{1}'", 
                    cboEstados.Data, txtMeses.Text);


            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarFarmaciasGrid()");
                General.msjError("Ocurrió un error al obtener la lista de farmacias.");
            }
            else
            {
                grid.Limpiar(false);
                grid.LlenarGrid(leer.DataSetClase);
            }
        }

        private void grdConsumos_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

        }

        private void grdConsumos_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            Cols columna = (Cols)e.Column + 1;
            iRow = e.Row + 1;
            sFecha = grid.GetValue(iRow, (int)Cols.Fecha);

            switch (columna)
            {


                case Cols.Actualizar:
                    ObtenerInformacion(sFecha);
                    break;

                default:
                    break;
            }

        }


        private void ObtenerInformacion(string sFecha)
        {
                thPCM = new Thread(this.ThObtenerPCM);
                thPCM.Name = "ObtenerFaltantes_Juris";
                thPCM.Start();

        }


        private void ThObtenerPCM()
        {
            bool bOcurrioError = false;
            string sFecha = "";

            grid.BloqueaGrid(true);

            for (int i = 1; i <= grid.Rows; i++)
            {
                Obtener(sFecha);

                if (!bOcurrioError)
                {
                    //if (bExisteInformacionJurisdiccion)
                    //{
                    //    myGrid.SetValue(i, (int)Cols.Status, "Terminado");
                    //}
                }
            }

            grid.BloqueaGrid(false);
        }

        private void Obtener(string sFecha)
        {
            string sUrlServidorRegional = "";

            string sSql = string.Format(" Select * From vw_Regionales_Urls Where StatusUrl = 'A' And StatusRelacion = 'A' And FarmaciaStatus = 'A' And IdEstado = '{0}' ",
               cboEstados.Data );

            if (leer.Exec(sSql))
            {
                while (leer.Leer())
                {
                    sUrlServidorRegional = leer.Campo("UrlFarmacia");

                    Obtener_PCM(sUrlServidorRegional, sFecha, iRow);
                }
            }
        }

        private bool Obtener_PCM(string sUrlServidorRegional, string sFecha, int iRenglon)
        {
            bool bRegresa = false;
            string sSql = "";
            int iNumeroMesesExistencia = 1;
            string sTabla = string.Format("tmpPCM_{0}", General.MacAddress);
            DateTime dtpFecha = General.FechaSistema;

            DataSet dtsPCM = new DataSet();
            clsLeerWebExt leerWeb = new clsLeerWebExt(sUrlServidorRegional, DtGeneral.CfgIniOficinaCentral, datosCliente);


            sSql = string.Format("Exec spp_ObtenerInformacion_OcenPln_ResumenPorMes @IdEstado = '{0}', @Año = {1}, @Mes = {2} \n \n ",
                            cboEstados.Data, sFecha.PadLeft(4), sFecha.PadRight(2));



            if (!leerWeb.Exec(sSql))
            {
                Error.GrabarError(General.DatosConexion, leerWeb.Error, "ObtenerConsumos");
                grid.ColorRenglon(iRenglon, Color.Red);
            }
            else
            {
                if (leerWeb.Leer())
                {
                    bRegresa = true;
                    dtsPCM = leerWeb.DataSetClase;

                    sSql = string.Format("If Exists ( Select Name From sysobjects (noLock) Where Name = '{0}' and xType = 'U') Drop Table {0} ", sTabla);
                    leerWeb.Exec(sSql);
                }
            }

            //if (bRegresa)
            //{
            //    if (Registrar_PCM(dtsPCM, IdJurisdiccion))
            //    {
            //        if (VerificarPCM(IdJurisdiccion, iRenglon))
            //        {
            //            System.Threading.Thread.Sleep(1000);
            //        }
            //    }
            //}

            return bRegresa;
        }


    }
}
