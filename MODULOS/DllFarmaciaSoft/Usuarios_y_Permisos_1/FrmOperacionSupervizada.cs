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

namespace DllFarmaciaSoft.Usuarios_y_Permisos
{
    public partial class FrmOperacionSupervizada : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer; 
        clsCriptografo crypto = new clsCriptografo();

        public bool Autorizado = false; 
        public string NombreOperacion = "";
        public string PersonalAutoriza = ""; 


        public FrmOperacionSupervizada()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name); 
        }

        private void FrmOperacionSupervizada_Load(object sender, EventArgs e)
        {
            lblDescripcion.Text = "";
            CargarUsuariosOperacion();
            cboPersonal.Data = PersonalAutoriza; 
        }

        private void FrmOperacionSupervizada_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    validarInformacion();
                    break; 

                case Keys.F12:
                    CancelarOperacion(); 
                    break; 

                default:
                    break; 
            } 
        }

        private void CancelarOperacion()
        {
            Autorizado = false;
            PersonalAutoriza = "";
            this.Close(); 
        }

        private bool validaDatos()
        {
            bool bRegresa = true;
            string sCadena = DtGeneral.EstadoConectado + DtGeneral.FarmaciaConectada + cboPersonal.Data + txtPassword.Text.ToUpper();
            string sPass = crypto.PasswordEncriptar(sCadena);
            string sPassUser = "";

            if (cboPersonal.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un Personal válido, verifique."); 
                cboPersonal.Focus(); 
            }

            if ( bRegresa && txtPassword.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha especificado el Password, verifique.");
                txtPassword.Focus();
            }
            else
            {
                sPassUser = ((DataRow)cboPersonal.ItemActual.Item)["Password"].ToString();
                if (sPassUser != sPass)
                {
                    bRegresa = false;
                    General.msjUser("La contraseña espeficiada es incorrecta, verifique.");
                    txtPassword.Focus(); 
                } 
            } 


            return bRegresa; 
        } 

        private void validarInformacion()
        {
            if (validaDatos())
            {
                Autorizado = true;
                PersonalAutoriza = cboPersonal.Data; 
                this.Close(); 
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            validarInformacion(); 
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CancelarOperacion(); 
        }

        private void CargarUsuariosOperacion()
        {
            string sSql = string.Format(" 	Select V.IdPersonal, V.NombreCompleto, U.Password, P.Status, O.Descripcion  " +  
	            " From vw_Personal V (NoLock) " + 
	            " Inner Join Net_Usuarios U (NoLock) On ( V.IdEstado = U.IdEstado and V.IdFarmacia = U.IdSucursal and V.IdPersonal = U.IdPersonal) " + 
	            " Inner Join Net_Permisos_Operaciones_Supervisadas P (NoLock) " + 
	            " 	On ( V.IdEstado = P.IdEstado and V.IdFarmacia = P.IdFarmacia and P.IdPersonal = V.IdPersonal )" + 
	            " Inner Join Net_Operaciones_Supervisadas O (NoLock)	On ( P.IdOperacion = O.IdOperacion ) " +
                " Where P.IdEstado = '{0}' and P.IdFarmacia = '{1}' and O.Nombre = '{2}' and P.Status = 'A' and O.Status = 'A' ", 
                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, NombreOperacion );

            cboPersonal.Clear();
            cboPersonal.Add(); 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener la lista de Personal autizado para la Operación solicitada."); 
            }
            else
            {
                if (!leer.Leer()) 
                {
                    General.msjUser("No se encontró Personal Autorizado para la Operación solicitada."); 
                }
                else
                {
                    lblDescripcion.Text = leer.Campo("Descripcion");
                    cboPersonal.Add(leer.DataSetClase, true, "IdPersonal", "NombreCompleto");
                }
            }
            cboPersonal.SelectedIndex = 0; 
        } 
    }
}
