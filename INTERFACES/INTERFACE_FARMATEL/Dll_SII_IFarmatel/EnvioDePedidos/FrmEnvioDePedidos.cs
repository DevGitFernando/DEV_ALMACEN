using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.SQL;
using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using Dll_SII_IFarmatel; 

namespace Dll_SII_IFarmatel.EnvioDePedidos
{
    public partial class FrmEnvioDePedidos : FrmBaseExt
    {
        #region Enums 
        enum Cols 
        {
            Ninguna = 0, 
            FechaRegistro, TipoAtencion, Folio, Beneficiario, Domicilio, Enviar, Status 
        }
        #endregion Enums
        
        #region Declaracion de Variables
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsDatosCliente DatosCliente;

        clsGrid grid;
        Thread thEnvio;
        bool bEnvioHabilitado = false;
        bool bEnviandoPedidos = false; 

        #endregion Declaracion de Variables

        public FrmEnvioDePedidos()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            AjustarAnchoPantalla(); 

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");

            cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario = General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.DatosConexion.Puerto = General.DatosConexion.Puerto;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;

            leer = new clsLeer(ref cnn);
            grid = new clsGrid(ref grdServiciosADomicilio, this);
            grid.EstiloDeGrid = eModoGrid.ModoRow;
            grid.SetOrder((int)Cols.Beneficiario, (int)Cols.Beneficiario, true);
            grid.AjustarAnchoColumnasAutomatico = true; 

            btnEnviarPedidos.Enabled = bEnvioHabilitado;

        }

        private void AjustarAnchoPantalla()
        {
            int iAnchoPantalla = (int)((double)Screen.PrimaryScreen.WorkingArea.Size.Width * 0.95);
            int iAltoPantalla = (int)((double)Screen.PrimaryScreen.WorkingArea.Size.Height * 0.70);


            this.Width = iAnchoPantalla;
            this.Height = iAltoPantalla;
        }

        private void FrmEnvioDePedidos_Load(object sender, EventArgs e)
        {
            GnDll_SII_IFarmatel.Interface_Pedidos.GetConfiguracion();
            GnDll_SII_IFarmatel.Interface_Pedidos.GetToken();
            bEnvioHabilitado = GnDll_SII_IFarmatel.Interface_Pedidos.EnvioHabilitado; 

            btnEnviarPedidos.Enabled = bEnvioHabilitado; 

            LimpiarPantalla();
            CargarPedidos(); 
        }

        private void FrmEnvioDePedidos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                TeclasRapidas(e);
            }

            switch (e.KeyCode)
            {
                default:
                    break;
            }
        }

        private void TeclasRapidas(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    GnDll_SII_IFarmatel.Interface_Pedidos.Informacion(); 
                    break;

                default:
                    break;
            }
        }


        #region Botones 
        private void LimpiarPantalla()
        {
            Fg.IniciaControles();
            grid.Limpiar(false);
            grdServiciosADomicilio.Enabled = false; 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void btnMostrarServiciosADomicilio_Click(object sender, EventArgs e)
        {
            CargarPedidos(); 
        }

        private void btnEnviarPedidos_Click(object sender, EventArgs e)
        {
            bEnviandoPedidos = true;
            BloqueControles(true);
            Application.DoEvents();

            timerEnviandoPedidos.Enabled = true;
            timerEnviandoPedidos.Interval = 500;
            timerEnviandoPedidos.Start(); 

            thEnvio = new Thread(EnviarPedidos);
            thEnvio.Name = "EnvioDePedidos";
            thEnvio.Start(); 
        }
        #endregion Botones

        #region Funciones y Procedimientos Privados
        private void CargarPedidos()
        {
            grid.Limpiar(false);
            chkMarcarDesmarcarTodo.Checked = false; 
            if (GnDll_SII_IFarmatel.Interface_Pedidos.GetPedidosParaEnvio())
            {
                if (GnDll_SII_IFarmatel.Interface_Pedidos.ServiciosADomicilio.Leer())
                {
                    grid.LlenarGrid(GnDll_SII_IFarmatel.Interface_Pedidos.ServiciosADomicilio.DataSetClase);
                }
            }
            grdServiciosADomicilio.Enabled = grid.Rows > 0;
        }

        private void BloqueControles(bool Bloquear)
        {
            btnNuevo.Enabled = !Bloquear;
            btnEnviarPedidos.Enabled = !Bloquear;
            btnMostrarServiciosADomicilio.Enabled = !Bloquear;

            grid.BloqueaColumna(Bloquear, (int)Cols.Enviar); 
        }

        private void EnviarPedidos()
        {
            string sFolio = "";
            bool bRegresa = false;
            string sTitulo = "";

            for (int i = 1; i <= grid.Rows; i++)
            {
                if (grid.GetValueBool(i, (int)Cols.Enviar)) 
                {
                    sFolio = grid.GetValue(i, (int)Cols.Folio);
                    sTitulo = "Enviando pedido.";
                    grid.SetValue(i, (int)Cols.Status, sTitulo);

                    bRegresa = GnDll_SII_IFarmatel.Interface_Pedidos.EnviarPedido(sFolio);

                    sTitulo = bRegresa ? "Pedido enviado" : GnDll_SII_IFarmatel.Interface_Pedidos.MensajeError;  //"Error al enviar el pedido";
                    grid.SetValue(i, (int)Cols.Status, sTitulo);
                }
            }

            bEnviandoPedidos = false; 
            //BloqueControles(false);
            //CargarPedidos(); 
        }

        private void chkMarcarDesmarcarTodo_CheckedChanged(object sender, EventArgs e)
        {
            grid.SetValue((int)Cols.Enviar, chkMarcarDesmarcarTodo.Checked);
        }
        #endregion Funciones y Procedimientos Privados

        private void timerEnviandoPedidos_Tick(object sender, EventArgs e)
        {
            if (!bEnviandoPedidos)
            {
                timerEnviandoPedidos.Stop(); 
                timerEnviandoPedidos.Enabled = false;

                Application.DoEvents();                
                BloqueControles(false);
            }
        }
    }
}
