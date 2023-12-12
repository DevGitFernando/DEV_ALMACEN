using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

using DllFarmaciaSoft;

namespace Farmacia.Inventario.Reubicaciones
{
    public partial class FrmRptReubicaciones_Monitor : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsListView lst;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        DateTime dtCuenta = DateTime.Now;
        int iMinutosActualizacion = 5;
        string sTituloActualizacion = ""; 

        public FrmRptReubicaciones_Monitor()
        {
            InitializeComponent();
            leer = new clsLeer(ref cnn);
            lst = new clsListView(lstVwInformacion);

            int iAnchoPantalla = (int)((double)Screen.PrimaryScreen.WorkingArea.Size.Width * 0.98);
            int iAltoPantalla = (int)((double)Screen.PrimaryScreen.WorkingArea.Size.Height * 0.85);
            this.Width = iAnchoPantalla;
            this.Height = iAltoPantalla;


            ////FrameResultado.Width = (int)((double)iAnchoPantalla * 0.98);
            ////FrameResultado.Height = (int)((double)iAltoPantalla * 0.88);

            sTituloActualizacion = lblTiempoActualizacion.Text;
            dtCuenta = new DateTime(dtCuenta.Year, dtCuenta.Month, dtCuenta.Day, 0, iMinutosActualizacion, 0); 

            this.Location = new Point(100, 100);
        }

        private void FrmRptReubicaciones_Monitor_Load(object sender, EventArgs e)
        {
            CargarInformacion();

            tmActualizarInformacion.Interval = (1000 * 60) * iMinutosActualizacion;

            ConfigurarCuentaRegresiva();
        }

        private void tmRefrescar_Tick(object sender, EventArgs e)
        {
            ////////tmRefrescar.Enabled = false;

            ////////CargarInformacion();

            ////////tmRefrescar.Enabled = true;
            ////////tmRefrescar.Start();
        }

        private void ConfigurarCuentaRegresiva()
        {
            dtCuenta = new DateTime(dtCuenta.Year, dtCuenta.Month, dtCuenta.Day, 0, iMinutosActualizacion, 0);
            lblTiempoActualizacion.Text = string.Format("{0}  {1}", sTituloActualizacion, dtCuenta.ToString("HH:mm:ss"));
        }

        private void CargarInformacion()
        {
            string sSql = string.Format("Exec spp_Rpt_Ctrl_Reubicaciones_Monitor {0}, {1}, {2}", sEmpresa, sEstado, sFarmacia);

            tmCuentaRegresiva.Stop();
            tmCuentaRegresiva.Enabled = false;

            tmActualizarInformacion.Stop();
            tmActualizarInformacion.Enabled = false;


            lst.LimpiarItems();
            if (!leer.Exec(sSql))
            {
                General.msjAviso("Ocurrió un error al obtener los datos.");
                Error.GrabarError(leer, "CargarInformacion()");
                General.msjError("Ocurrió un error al verificar el inventario a integrar.");
            }
            else
            {
                if (leer.Leer())
                {
                    lst.CargarDatos(leer.DataSetClase, true, true);
                }
            }

            ConfigurarCuentaRegresiva();
            tmCuentaRegresiva.Enabled = true;
            tmCuentaRegresiva.Start();

            tmActualizarInformacion.Enabled = true;
            tmActualizarInformacion.Start(); 
        }

        private void FrmRptReubicaciones_Monitor_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    CargarInformacion();
                    break;

                default:
                    break;
            }
        }

        private void tmCuentaRegresiva_Tick(object sender, EventArgs e)
        {
            dtCuenta = dtCuenta.AddSeconds(-1);
            lblTiempoActualizacion.Text = string.Format("{0}  {1}", sTituloActualizacion, dtCuenta.ToString("HH:mm:ss")); 
        }

        private void tmActualizarInformacion_Tick(object sender, EventArgs e)
        {
            CargarInformacion(); 
        }
    }
}
