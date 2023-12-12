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
    internal partial class FrmTitulosEncabezados : FrmBaseExt
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
        Form formaPadre = null;

        public FrmTitulosEncabezados(Form Padre, string IdEstado, int Tipo)
        {
            InitializeComponent();

            formaPadre = Padre;
            this.iTipo = Tipo; 
            this.sIdEstado = IdEstado; 

            leer = new clsLeer(ref cnn);            
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);
        }

        private void FrmTitulosEncabezados_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null); 
        }

        #region Funciones y Procedimientos Publicos 
        public bool SeModificoInformacion 
        {
            get { return bSeModificoInformacion; }
        }

        public void MostrarPantalla()
        {
            this.MostrarPantalla("", "", "", ""); 
        }

        public void MostrarPantalla(string IdCliente, string IdSubCliente, string IdPrograma, string IdSubPrograma)
        {
            sIdCliente = IdCliente;
            sIdSubCliente = IdSubCliente;
            sIdPrograma = IdPrograma;
            sIdSubPrograma = IdSubPrograma;     

            this.ShowDialog(); 
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);

            if (iTipo == 2)
            {
                CargarInformacion();
            }
        }
        #endregion Funciones

        #region Obtener Informacion 
        private void CargarInformacion()
        {
            string sSql = string.Format(
               "Select * " +
               "From vw_CFG_EX_Validacion_Titulos (NoLock) " +
               "Where IdEstado = '{0}' and IdCliente = '{1}' and IdSubCliente = '{2}' and IdPrograma = '{3}' and IdSubPrograma = '{4}' ", 
               sIdEstado, sIdCliente, sIdSubCliente, sIdPrograma, sIdSubPrograma);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarInformacion()");
                General.msjError("Ocurrió un error al obtener la información solicitada."); 
            }
            else
            {
                leer.Leer(); 
                txtCte.Enabled = false;
                txtSubCte.Enabled = false;
                txtPro.Enabled = false;
                txtSubPro.Enabled = false; 

                txtCte.Text = leer.Campo("IdCliente");
                lblCte.Text = leer.Campo("Cliente");
                txtTituloCliente.Text = leer.Campo("ClienteTitulo");

                txtSubCte.Text = leer.Campo("IdSubCliente");
                lblSubCte.Text = leer.Campo("SubCliente");
                txtTituloSubCliente.Text = leer.Campo("SubClienteTitulo");

                txtPro.Text = leer.Campo("IdPrograma");
                lblPro.Text = leer.Campo("Programa");
                txtTituloPrograma.Text = leer.Campo("ProgramaTitulo");

                txtSubPro.Text = leer.Campo("IdSubPrograma");
                lblSubPro.Text = leer.Campo("SubPrograma");
                txtTituloSubPrograma.Text = leer.Campo("SubProgramaTitulo");
                chkActivo.Checked = leer.CampoBool("Activo"); 
            }
        }
        #endregion Obtener Informacion

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
                        bSeModificoInformacion = true;
                        this.Hide(); 
                        //btnNuevo_Click(null, null);
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

        #region Eventos_Cliente
        private void txtCte_Validating(object sender, CancelEventArgs e)
        {
            if (txtCte.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Clientes(txtCte.Text, "txtCte_Validating");                   
                if (leer.Leer())
                {
                    CargarDatosCliente();
                }
                else
                {
                    lblCte.Text = "";
                    txtCte.Focus();
                }                
            }
        }

        private void txtCte_TextChanged(object sender, EventArgs e)
        {
            //txtCte.Text = "";
            lblCte.Text = "";
        }

        private void txtCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Clientes("txtCte_KeyDown");                 
                if (leer.Leer())
                {
                    CargarDatosCliente();
                }
            }
        }

        private void CargarDatosCliente()
        {
            txtCte.Text = leer.Campo("IdCliente");
            lblCte.Text = leer.Campo("Nombre");
        }
        #endregion Eventos_Cliente

        #region Eventos_Sub-Cliente
        private void txtSubCte_Validating(object sender, CancelEventArgs e)
        {
            if (txtSubCte.Text != "")
            {
                leer.DataSetClase = Consultas.SubClientes(txtCte.Text.Trim(), txtSubCte.Text.Trim(),"txtSubCte_Validating");
                
                if (leer.Leer())
                {
                    CargarDatosSubCliente();
                }
                else
                {
                    lblSubCte.Text = "";
                    txtSubCte.Focus();
                }
            }
        }

        private void txtSubCte_TextChanged(object sender, EventArgs e)
        {
            //txtSubCte.Text = "";
            lblSubCte.Text = "";
        }

        private void txtSubCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.SubClientes_Buscar("txtCte_KeyDown", txtCte.Text.Trim());
                
                if (leer.Leer())
                {
                    CargarDatosSubCliente();
                }
            }
        }

        private void CargarDatosSubCliente()
        {
            txtSubCte.Text = leer.Campo("IdSubCliente");
            lblSubCte.Text = leer.Campo("Nombre");
        }
        #endregion Eventos_Sub-Cliente

        #region Eventos_Programa
        private void txtPro_Validating(object sender, CancelEventArgs e)
        {
            if (txtPro.Text.Trim() != "")
            {
                {
                    leer.DataSetClase = Consultas.Programas(txtPro.Text, "txtPro_Validating");
                    
                    if (leer.Leer())
                    {
                        CargarDatosProgramas();
                    }
                    else
                    {
                        lblPro.Text = "";
                        txtPro.Focus();
                    }
                }
            }
        }

        private void txtPro_TextChanged(object sender, EventArgs e)
        {
            //txtPro.Text = "";
            lblPro.Text = "";
        }

        private void txtPro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Programas("txtPro_KeyDown");
                
                if (leer.Leer())
                {
                    CargarDatosProgramas();
                }
            }
        }

        private void CargarDatosProgramas()
        {
            txtPro.Text = leer.Campo("IdPrograma");
            lblPro.Text = leer.Campo("Descripcion");
        }
        #endregion Eventos_Programa

        #region Eventos_Sub-Programa
        private void txtSubPro_Validating(object sender, CancelEventArgs e)
        {
            if (txtSubPro.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.SubProgramas(txtPro.Text, txtSubPro.Text, "txtSubPro_Validating");
                
                if (leer.Leer())
                {
                    CargarDatosSubProgramas();
                }
                else
                {
                    lblSubPro.Text = "";
                    txtSubPro.Focus();
                }
            }
        }

        private void txtSubPro_TextChanged(object sender, EventArgs e)
        {
            //txtSubPro.Text = "";
            lblSubPro.Text = "";
        }

        private void txtSubPro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.SubProgramas("txtSubPro_KeyDown", txtPro.Text);                 
                if (leer.Leer())
                {
                    CargarDatosSubProgramas();
                }
            }
        }

        private void CargarDatosSubProgramas()
        {
            txtSubPro.Text = leer.Campo("IdSubPrograma");
            lblSubPro.Text = leer.Campo("Descripcion");
        }
        #endregion Eventos_Sub-Programa

        #region Guardar-Informacion
        private bool GuardaInformacion(int Opcion)
        {
            bool bRegresa = false;
            int iReferencia = 0;
            int iBeneficiario = 0;
            int iOpcion = chkActivo.Checked ? 1 : 0;

            string sSql = string.Format(" Exec spp_Mtto_CFG_EX_Validacion_Titulos  " + 
                " '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}' ",
                sIdEstado, 
                txtCte.Text, txtTituloCliente.Text, 
                txtSubCte.Text, txtTituloSubCliente.Text, 
                txtPro.Text, txtTituloPrograma.Text, 
                txtSubPro.Text, txtTituloSubPrograma.Text, 
                iOpcion); 

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
            
            if (txtCte.Text.Trim() == "")
            {
                General.msjUser("No ha capturado un Cliente válido, verifique.");
                txtCte.Focus();
                bRegresa = false;                
            }

            if ( bRegresa && txtSubCte.Text.Trim() == "")
            {
                General.msjUser("No ha capturado un Sub-Cliente válido, verifique.");
                txtSubCte.Focus();
                bRegresa = false;                
            }

            if (bRegresa && txtPro.Text.Trim() == "")
            {
                General.msjUser("No ha capturado un Programa válido, verifique.");
                txtPro.Focus();
                bRegresa = false;
            }

            if (bRegresa && txtSubPro.Text.Trim() == "")
            {
                General.msjUser("No ha capturado un Sub-Programa válido, verifique.");
                txtSubPro.Focus();
                bRegresa = false;
            } 
            
            return bRegresa;
        }
        #endregion Guardar-Informacion
    }
}
