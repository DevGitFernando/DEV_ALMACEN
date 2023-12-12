using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data; 

namespace DtUtileriasBD
{
    public partial class FrmConexion : FrmBaseExt 
    {
        Thread thrConexion;
        clsDatosConexion datos;
        clsConexionSQL cnn;
        clsLeer leer; 

        public FrmConexion()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; 
        }

        private void FrmConexion_Load(object sender, EventArgs e)
        {
            txtServer.Text = General.DatosConexion.Servidor;
            txtUser.Text = General.DatosConexion.Usuario;
            txtPass.Text = General.DatosConexion.Password;

            btnConectar.Enabled = !GnSqlManager.ConexionEstablecida;
            btnDesconectar.Enabled = GnSqlManager.ConexionEstablecida;

            Bloquear(); 
        }

        private void Bloquear()
        {
            if (GnSqlManager.ConexionEstablecida)
            {
                txtServer.Enabled = false;
                txtUser.Enabled = false;
                txtPass.Enabled = false;
            }
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            if (validarConexion())
            {
                thrConexion = new Thread(this.Conectar);
                thrConexion.Name = "Conectando";
                thrConexion.Start();
            }

            //////if (validarConexion())
            //////{
            //////    General.DatosConexion.Servidor = txtServer.Text;
            //////    General.DatosConexion.Usuario = txtUser.Text;
            //////    General.DatosConexion.Password = txtPass.Text;
            //////    General.DatosConexion.ConexionDeConfianza = chkCnnConfianza.Checked; 
            //////    GnSqlManager.ConexionEstablecida = !GnSqlManager.ConexionEstablecida;

            //////    btnConectar.Enabled = !GnSqlManager.ConexionEstablecida;
            //////    btnDesconectar.Enabled = GnSqlManager.ConexionEstablecida;

            //////    Bloquear(); 
            //////}
        }

        private void Conectar()
        {
            btnCancelarConexion.Enabled = true;
            btnConectar.Enabled = false;

            if (ProbarConexion())
            {
                btnConectar.Enabled = false;
                btnDesconectar.Enabled = true;
                btnCancelarConexion.Enabled = false;


                General.DatosConexion.Servidor = txtServer.Text;
                General.DatosConexion.Usuario = txtUser.Text;
                General.DatosConexion.Password = txtPass.Text;
                General.DatosConexion.ConexionDeConfianza = chkCnnConfianza.Checked;



                datos = new clsDatosConexion();
                datos.Servidor = txtServer.Text;
                datos.BaseDeDatos = "master";
                datos.Usuario = txtUser.Text;
                datos.Password = txtPass.Text;
                datos.ConexionDeConfianza = chkCnnConfianza.Checked;
                General.DatosConexion = datos.Clone(); 


                GnSqlManager.ConexionEstablecida = !GnSqlManager.ConexionEstablecida;

                btnConectar.Enabled = !GnSqlManager.ConexionEstablecida;
                btnDesconectar.Enabled = GnSqlManager.ConexionEstablecida;

                Bloquear(); 
            }
            else
            {
                btnConectar.Enabled = true;
                btnCancelarConexion.Enabled = true;
            }
        }

        private void btnDesconectar_Click(object sender, EventArgs e)
        {
            try
            {
                General.DatosConexion.Servidor = "";
                General.DatosConexion.BaseDeDatos = "master";
                General.DatosConexion.Usuario = "";
                General.DatosConexion.Password = "";
                GnSqlManager.ConexionEstablecida = !GnSqlManager.ConexionEstablecida;

                btnConectar.Enabled = !GnSqlManager.ConexionEstablecida;
                btnDesconectar.Enabled = GnSqlManager.ConexionEstablecida;

                txtServer.Enabled = true;
                txtUser.Enabled = true;
                txtPass.Enabled = true;
            }
            catch (Exception ex)
            {
                General.msjError(ex.Message); 
            }
        }

        private bool validarConexion()
        {
            bool bRegresa = true;

            if (txtServer.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("No ha especificado el Servidor, verifique."); 
                txtServer.Focus(); 
            }

            if (bRegresa && !chkCnnConfianza.Checked)
            {
                if (bRegresa && txtUser.Text.Trim() == "")
                {
                    bRegresa = false;
                    General.msjAviso("No ha especificado el Usuario, verifique.");
                    txtUser.Focus();
                }

                if (bRegresa && txtPass.Text.Trim() == "")
                {
                    bRegresa = false;
                    General.msjAviso("No ha especificado el Password, verifique.");
                    txtPass.Focus();
                }
            }

            ////if (bRegresa)
            ////{
            ////    bRegresa = ProbarConexion();
            ////}

            return bRegresa; 
        }

        private bool ProbarConexion()
        {
            bool bRegresa = true;
            string sMsjError = ""; 
            datos = new clsDatosConexion();

            datos.Servidor = txtServer.Text;
            datos.BaseDeDatos = "master";
            datos.Usuario = txtUser.Text;
            datos.Password = txtPass.Text;
            datos.ConexionDeConfianza = chkCnnConfianza.Checked; 

            cnn = new clsConexionSQL(datos);
            leer = new clsLeer(ref cnn);

            if (!leer.Exec(" Select getdate() as Fecha "))
            {
                bRegresa = false;

                try
                {
                    sMsjError = leer.MensajeError;
                }
                catch { sMsjError = "Ocurrió un error al establecer conexión."; }

                General.msjError(sMsjError); 
            } 

            return bRegresa; 
        }

        private void btnPassword_Click(object sender, EventArgs e)
        {

        }

        private void btnGenPassword_Click(object sender, EventArgs e)
        {
            FrmGenerarPassword f = new FrmGenerarPassword();

            f.ShowDialog();
            txtPass.Text = f.Password; 
        }

        private void btnCancelarConexion_Click(object sender, EventArgs e)
        {
            try
            {
                thrConexion.Interrupt();
            }
            catch { }
            finally
            {
                thrConexion = null;

                try
                {
                    leer.Conexion.Cerrar();
                }
                catch { }

                leer = null;
                btnDesconectar_Click(null, null);

                btnCancelarConexion.Enabled = false;
                btnConectar.Enabled = true;
                btnDesconectar.Enabled = false; 
            }
        }
    }
}
