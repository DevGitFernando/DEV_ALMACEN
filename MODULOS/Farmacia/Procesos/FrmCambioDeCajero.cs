using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using DllFarmaciaSoft;
using SC_SolutionsSystem.FuncionesGenerales;

namespace Farmacia.Procesos
{
    public partial class FrmCambioDeCajero : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsConsultas Consultas;
        private clsCriptografo crypto = new clsCriptografo();
        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sFechaSistema = General.FechaYMD(GnFarmacia.FechaOperacionSistema, "-");

        string IdPersonal_Nuevo = "";
        string LoginUsuario_Nuevo = "";
        string NombrePersonal_Nuevo = "";
        string sPasswordDB = "";



        public FrmCambioDeCajero()
        {
            InitializeComponent();

            myLeer = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);
        }

        private void FrmCambioDeCajero_Load(object sender, EventArgs e)
        {
            Inicializa();
        }

        private void Inicializa()
        {
            txtIdPersonalSale.Text = DtGeneral.IdPersonal;
            lblPersonalSale.Text = DtGeneral.NombrePersonal;
            txtIdPersonalSale.Enabled = false;

            txtIdPersonalEntra.Text = "";
            lblPersonalEntra.Text = "";
            txtPassword.Text = "";
            txtIdPersonalEntra.Focus();
        }

        #region Valida Nuevo Cajero

        private void txtIdPersonalEntra_Validating(object sender, CancelEventArgs e)
        {
            sPasswordDB = "";
            if (txtIdPersonalEntra.Text.Trim() != "")
            {
                if (Fg.PonCeros(txtIdPersonalEntra.Text, 4) == txtIdPersonalSale.Text)
                {
                    General.msjUser("El personal que entra no puede ser igual al personal que sale");
                    txtIdPersonalEntra.Focus(); 
                }
                else
                {
                    //Se busca el nuevo cajero
                    if (!BuscaNuevoCajero())
                        Inicializa();
                }
            }
        }

        private bool BuscaNuevoCajero()
        {
            bool bRegresa = false;
            string sIdPersonal_Nvo = "", sPassword = txtPassword.Text.ToUpper();
            myLeer = new clsLeer(ref ConexionLocal);

            sIdPersonal_Nvo = Fg.PonCeros( txtIdPersonalEntra.Text.Trim(), 4 );
            myLeer.DataSetClase = Consultas.PersonalCorte(sEstado, sFarmacia, sIdPersonal_Nvo, sPassword, "BuscaNuevoCajero()");

            if (myLeer.Leer())
            {
                if (myLeer.Campo("Status").ToUpper() == "A")
                {
                    bRegresa = true;
                    AsignaDatosNuevoUsuarioConectado();
                }
                else
                {
                    General.msjUser("El Usuario especificado no existe ó se encuentra cancelado."); 
                } 
            }

            return bRegresa;

        }

        private void AsignaDatosNuevoUsuarioConectado()
        {
            //Se hace de esta manera para la ayuda.
            IdPersonal_Nuevo = myLeer.Campo("IdPersonal");
            LoginUsuario_Nuevo = myLeer.Campo("LoginUser");
            NombrePersonal_Nuevo = myLeer.Campo("NombreCompleto");
            sPasswordDB = myLeer.Campo("Password").ToString();

            txtIdPersonalEntra.Text = IdPersonal_Nuevo;
            lblPersonalEntra.Text = NombrePersonal_Nuevo;

            if ( ! ValidaStatusUsuario() )
                txtIdPersonalEntra.Focus();
        }

        private bool ValidaStatusUsuario()
        {
            bool bRegresa = true;
            string sIdPersonal_Nvo = "";
            myLeer = new clsLeer(ref ConexionLocal);

            sIdPersonal_Nvo = txtIdPersonalEntra.Text.Trim();       
            myLeer.DataSetClase = Consultas.CortesParciales_Status(sEmpresa, sEstado, sFarmacia, sFechaSistema, sIdPersonal_Nvo, false, "A", "ValidaUsuarioCorte");
            if (myLeer.Leer())
            {
                //Si lee significa que el usuario tiene status A, por lo tanto no puede efectuar el cambio de caja.
                bRegresa = false;
                General.msjUser("El usuario ingresado tiene sesion abierta por lo tanto no puede efectuar cambio de caja");
            }

            return bRegresa;

        }
        

        #endregion Valida Nuevo Cajero

        #region Guardar Informacion

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            bool bCorteRealizado = false;
            string sPassword = "";

            sPassword = crypto.PasswordEncriptar(sEstado + sFarmacia + IdPersonal_Nuevo + txtPassword.Text.ToUpper());

            if (sPassword != sPasswordDB)
            {
                General.msjUser("El Password es incorrecto");
                txtPassword.Focus();
            }
            else
            {
                FrmCorteParcial CorteParcial;
                CorteParcial = new FrmCorteParcial();

                CorteParcial.bOpcionExterna = true;
                CorteParcial.ShowDialog();
                bCorteRealizado = CorteParcial.bCorteRealizado;
                CorteParcial.Close();

                if (bCorteRealizado)
                {
                    DtGeneral.IdPersonal = IdPersonal_Nuevo;
                    DtGeneral.LoginUsuario = LoginUsuario_Nuevo;
                    DtGeneral.NombrePersonal = NombrePersonal_Nuevo;

                    General.msjUser("Cambio de Cajero realizado con exito");
                    this.Close();
                }
                else
                {
                    General.msjUser("Cambio de Cajero cancelado");
                }
            }
            
        }

        #endregion Guardar Informacion

        private void txtIdPersonalEntra_TextChanged(object sender, EventArgs e)
        {
            lblPersonalEntra.Text = "";
            txtPassword.Text = "";
            //sPasswordDB = "";
        }        
    }
}
