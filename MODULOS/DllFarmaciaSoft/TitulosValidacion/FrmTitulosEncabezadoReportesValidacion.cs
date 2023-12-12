using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;

namespace DllFarmaciaSoft.TitulosValidacion
{
    internal partial class FrmTitulosEncabezadoReportesValidacion : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);        
        clsLeer leer;
        
        clsConsultas Consultas;
        clsAyudas Ayuda;

        string sMensaje = "";
        string sIdEstado = "";
        string sIdCliente = "";
        string sIdSubCliente = "";
        string sIdPrograma = "";
        string sIdSubPrograma = "";
        int iTipo = 0;
        bool bSeModificoInformacion = false;


        public FrmTitulosEncabezadoReportesValidacion()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);            
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            CargarEstados(); 
        }

        private void FrmTitulosEncabezadoReportesValidacion_Load(object sender, EventArgs e)
        {            
            btnNuevo_Click(null, null);            
        }

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true); 
        }

        #region Funciones
        private void CargarEstados()
        {
            cboEstados.Clear();
            cboEstados.Add();

            leer.DataSetClase = Consultas.EstadosConFarmacias("CargarEstados");

            if (leer.Leer())
            {
                cboEstados.Add(leer.DataSetClase);
            }

            cboEstados.SelectedIndex = 0;
        }
        #endregion Funciones 
        #endregion Funciones

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bContinua = false; 

            if (ValidaDatos())
            {
                if (!cnn.Abrir())
                {
                    General.msjErrorAlAbrirConexion();
                }
                else 
                {
                    cnn.IniciarTransaccion();

                    bContinua = GuardaInformacion(1);

                    if (bContinua)
                    {
                        cnn.CompletarTransaccion();
                        General.msjUser("Información guardada satisfactoriamente."); //Este mensaje lo genera el SP                      
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al guardar la Información.");         

                    }

                    cnn.Cerrar();
                }
            }
        }
        #endregion Botones

        #region Eventos 
        private void txtIdTitulo_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdTitulo.Text != "")
            {
                leer.DataSetClase = Consultas.Validacion_Titulos_EncabezadoReportes(cboEstados.Data, txtIdTitulo.Text.Trim(), "txtIdTitulo_Validating");                 
                if (leer.Leer())
                {
                    CargarDatosTitulo();
                }
                else
                {
                    txtTituloReporte.Text = ""; 
                    txtIdTitulo.Focus();
                }
            }
        }

        private void txtIdTitulo_TextChanged(object sender, EventArgs e)
        {
            txtTituloReporte.Text = ""; 
        }

        private void txtIdTitulo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Validacion_Titulos_EncabezadoReportes(cboEstados.Data, "txtCte_KeyDown");                
                if (leer.Leer())
                {
                    CargarDatosTitulo();
                }
            }
        }

        private void CargarDatosTitulo()
        {
            txtIdTitulo.Enabled = false;
            txtIdTitulo.Text = leer.Campo("IdTitulo"); 
            txtTituloReporte.Text = leer.Campo("TituloEncabezadoReporte");
            chkActivo.Checked = leer.Campo("Status").ToUpper() == "A" ? true : false; 
        }
        #endregion Eventos 

        #region Guardar-Informacion
        private bool GuardaInformacion(int Opcion)
        {
            bool bRegresa = false; 

            string sSql = string.Format(" Exec spp_Mtto_CFG_EX_Validacion_Titulos_Reportes  " +
                " @IdEstado = '{0}', @IdTitulo = '{1}', @TituloEncabezadoReporte = '{2}', @iActivo = '{3}' ", 
                sIdEstado, txtIdTitulo.Text, txtTituloReporte.Text, Opcion);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "GuardaInformacion()"); 
            }
            else 
            {
                if (leer.Leer())
                {
                    bRegresa = true; 
                    sMensaje = String.Format("{0}", leer.Campo("Mensaje"));
                }
            }

            return bRegresa;
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;           
           

            if ( bRegresa && txtIdTitulo.Text.Trim() == "")
            {
                General.msjUser("No ha capturado una Clave de titulo, verifique.");
                txtIdTitulo.Focus();
                bRegresa = false;                
            }
            
            return bRegresa;
        }
        #endregion Guardar-Informacion

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEstados.SelectedIndex != 0)
            {
                cboEstados.Enabled = false; 
            }
        }
    }
}
