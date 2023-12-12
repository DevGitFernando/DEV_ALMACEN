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
using Dll_IFacturacion; 

namespace Facturacion.GenerarRemisiones
{
    internal partial class FrmValidarRemisiones_FoliosInvalidos : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer; 
        clsDatosCliente DatosCliente;
        clsAyudas Ayudas;
        clsConsultas Consultas;
        clsListView lst;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sPersonal = DtGeneral.IdPersonal;

        bool bInvocadoDesdeGuardado = false; 
        bool bValidarRemisionConFoliosInvalidos = false;
        bool bSeQuitaronFoliosInvalidos = false;
        bool bSeRealizaronAjustes = false; 
        string sFolioRemision = "";
        DataSet dtsFoliosInvalidos = new DataSet(); 

        public FrmValidarRemisiones_FoliosInvalidos()
        {
            InitializeComponent();

            lst = new clsListView(lstwFolios);
            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, DtIFacturacion.DatosApp, this.Name); 
        }

        #region Form 
        private void FrmValidarRemisiones_FoliosInvalidos_Load(object sender, EventArgs e)
        {
            if (!bInvocadoDesdeGuardado)
            {
                btnGuardar.Enabled = false;
                btnGuardar.Visible = false;
                toolStripSeparator.Visible = false;
            }
            ////else
            ////{
            ////    btnQuitarFoliosInvalidos.Enabled = false;
            ////    btnQuitarFoliosInvalidos.Visible = false;
            ////    toolStripSeparator2.Visible = false; 
            ////}

            lst.CargarDatos(dtsFoliosInvalidos, true, true); 
        }
        #endregion Form

        #region Propiedades 
        public bool ValidarRemision
        {
            get { return bValidarRemisionConFoliosInvalidos; }
        }

        public bool SeRealizaronAjustes
        {
            get { return bSeRealizaronAjustes; }
        }

        public string FolioDeRemision
        {
            get { return sFolioRemision; }
            set { sFolioRemision = value; }
        }

        public DataSet FoliosInvalidos
        {
            get { return dtsFoliosInvalidos; }
            set { dtsFoliosInvalidos = value;  }
        } 
        #endregion Propiedades

        #region Funciones y Procedimientos Publicos
        public void MostrarFoliosInvalidos()
        {
            bInvocadoDesdeGuardado = true; 
            this.ShowDialog(); 
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private bool DevolverFoliosARepositorioRemisiones()
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_FACT_Remisiones_Validar__PolizaBeneficiario '{0}', '{1}', '{2}', '{3}', 0, 1 ",
                sEmpresa, sEstado, sFarmacia, sFolioRemision);

            if (!cnn.Abrir())
            {
                General.msjErrorAlAbrirConexion();
            }
            else
            {
                cnn.IniciarTransaccion();

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "DevolverFoliosARepositorioRemisiones()"); 
                    cnn.DeshacerTransaccion();
                    General.msjError("Ocurrión error al enviar los folios a la bandeja de Pendientes de remisionar."); 
                }
                else
                {
                    cnn.CompletarTransaccion();
                    bRegresa = true;

                    if (!bInvocadoDesdeGuardado)
                    {
                        General.msjUser("Folios enviados a la bandeja de Pendientes para remisionar satisfactoriamente."); 
                    }
                }

                cnn.Cerrar(); 
            }

            return bRegresa; 
        }
        #endregion Funciones y Procedimientos Privados 

        #region Botones
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sMsj = "La remisión contiene Folios de dispensación con Pólizas inválidas,\n\n¿ Desea validar la Remisión de cualquier forma ? ";

            if (General.msjConfirmar(sMsj) == System.Windows.Forms.DialogResult.Yes)
            {
                bValidarRemisionConFoliosInvalidos = true;
                bSeQuitaronFoliosInvalidos = false;
                bSeRealizaronAjustes = true; 
                this.Hide();
            }
        }

        private void btnQuitarFoliosInvalidos_Click(object sender, EventArgs e)
        {
            string sMsj = "Los folios de dispensación con Pólizas inválidas se enviarán de nuevo al repositorio para remisiones.\n\n¿ Desea continuar ? ";

            if (General.msjConfirmar(sMsj) == System.Windows.Forms.DialogResult.Yes)
            {
                if (DevolverFoliosARepositorioRemisiones())
                {
                    bValidarRemisionConFoliosInvalidos = true;
                    bSeQuitaronFoliosInvalidos = true;
                    bSeRealizaronAjustes = true;
                    this.Hide();
                }
            }
        }
        #endregion Botones 
    }
}
