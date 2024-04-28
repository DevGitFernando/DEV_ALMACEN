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
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using Farmacia.Ventas;

using DllFarmaciaSoft;
using SC_SolutionsSystem.OfficeOpenXml.FormulaParsing.LexicalAnalysis;

namespace Farmacia.Turnos
{
    public partial class FrmTurnos : FrmBaseExt 
    {
        enum Cols
        {
            Folio = 1, Fuente = 2, Licitacion = 3, Orden = 4, FolioPresup = 5
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente datosCliente;
        clsLeer leer;
        clsConsultas query;
        
        string sIdFarmacia = "", sIdEstado = "", sIdEmpresa = "";      

        public FrmTurnos()
        {
            InitializeComponent();

            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmTurnos");

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name);

            sIdEmpresa = DtGeneral.EmpresaConectada;
            sIdEstado = DtGeneral.EstadoConectado;
            sIdFarmacia = DtGeneral.FarmaciaConectada;                             
                        
        }        

        private void FrmListaDeSurtidosPedido_Load(object sender, EventArgs e)
        {
            Fg.IniciaControles();
                        
            InicializarPantalla(); 
            
        }      
        
        #region Funciones 
        private void CargarTipoConsultas()
        {
            cboTipoConsultas.Clear();
            cboTipoConsultas.Add("0", "<< Seleccione >>");
            cboTipoConsultas.Add(query.Turnos_Consultas(sIdEmpresa, sIdEstado, "CargarTipoConsultas()"), true, "IdTipo", "Consulta");
            cboTipoConsultas.SelectedIndex = 0;
        }        
        #endregion Funciones 

        #region Botones Menu 
        private void InicializarPantalla()
        {
            CargarTipoConsultas();
            txtIdPaciente.Text = "";
            txtEdad.Text = "";
            txtNombre.Text = "";
            txtApPaterno.Text = "";
            txtApMaterno.Text = "";
            cboTipoConsultas.Focus();
        }
        private void btnNuevo_Click( object sender, EventArgs e )
        {
            InicializarPantalla();
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "", sFechaTurno = "";
            int iTurno = 0;

            if (ValidaDatos())
            {
                if (!cnn.Abrir())
                {
                    Error.LogError(cnn.MensajeError);
                    General.msjErrorAlAbrirConexion();
                }
                else
                {
                    cnn.IniciarTransaccion();

                    sSql = String.Format("Exec spp_Mtto_Ctrl_Turnos '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}' ",
                            sIdEmpresa, sIdEstado, sIdFarmacia, cboTipoConsultas.Data, "0", txtIdPaciente.Text, txtNombre.Text.Trim(),
                            txtApPaterno.Text.Trim(), txtApMaterno.Text.Trim(), txtEdad.Text);

                    if (leer.Exec(sSql)) 
                    {
                        if (leer.Leer())
                        {
                            sMensaje = String.Format("{0}", leer.Campo("Mensaje"));
                            iTurno = leer.CampoInt("Turno");
                            sFechaTurno = String.Format("{0}", leer.Campo("FechaTurno"));
                        }

                        cnn.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        ImprimirTurno(iTurno, sFechaTurno);
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Error al generar el turno.");             

                    }

                    cnn.Cerrar();
                }
            }
        }
        #endregion Botones Menu 

        #region GenerarTurno
        private bool ValidaDatos()
        {
            bool bRegresa = true;
            
            if(cboTipoConsultas.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("Seleccione Tipo de Consulta. Favor de verificar.");
                cboTipoConsultas.Focus();
            }

            if (bRegresa && txtIdPaciente.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Capture Id Paciente. Favor de verificar.");
                txtIdPaciente.Focus();
            }

            if (bRegresa && txtEdad.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Capture Edad. Favor de verificar.");
                txtEdad.Focus();
            }

            if (bRegresa && txtNombre.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Capture Nombre. Favor de verificar.");
                txtNombre.Focus();
            }
            
            if (bRegresa && txtApPaterno.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Capture Apellido Paterno. Favor de verificar.");
                txtApPaterno.Focus();
            }

            if (bRegresa && txtApMaterno.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Capture Apellido Materno. Favor de verificar.");
                txtApMaterno.Focus();
            }

            return bRegresa;
        }
        #endregion GenerarTurno

        #region ImprimirTurno
        private void ImprimirTurno(int iTurno, string FechaTurno)
        {
            bool bContinua = false;

            ////DateTime date1 = new DateTime();
            ////string sFecha = "";

            ////date1 = DateTime.Now;
            ////sFecha = date1.ToString("yyyy-MM-dd");

            datosCliente.Funcion = "ImprimirTurno()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;

            myRpt.RutaReporte = GnFarmacia.RutaReportes;
            myRpt.NombreReporte = "Genera_Turnos.rpt";
            myRpt.TituloReporte = "Impresión de Turnos.";

            myRpt.Add("IdEmpresa", sIdEmpresa);
            myRpt.Add("IdEstado", sIdEstado);
            myRpt.Add("IdFarmacia", sIdFarmacia);
            myRpt.Add("IdTipo", cboTipoConsultas.Data);
            myRpt.Add("IdTurno", iTurno);
            myRpt.Add("FechaTurno", FechaTurno);

            bContinua = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, datosCliente);

            if (bContinua)
            {
                InicializarPantalla();
                
            }
        }
        #endregion ImprimirTurno
    }
}
