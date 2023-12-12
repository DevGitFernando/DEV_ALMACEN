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
using SC_SolutionsSystem.FuncionesGenerales;

using DllFarmaciaSoft;
using DllTransferenciaSoft.ObtenerInformacion;
using DllFarmaciaSoft.Web;

namespace DllTransferenciaSoft.Configuraciones
{
    public partial class FrmConfigurarConexionOficinaCentral : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas query;
        clsAyudas Ayuda;

        clsCriptografo crypto = new clsCriptografo();

        string sSP = "", sTabla = ""; 

        string sError = "Ocurrió un error al guardar la información.";
        string sExito = "Información guardada satisfactoriamente.";

        Thread _workerThread;

        public FrmConfigurarConexionOficinaCentral()
        {
            InitializeComponent();
            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, "ConfigurarROC", this.Name, Application.ProductVersion);
            Ayuda = new clsAyudas(General.DatosConexion, "ConfigurarROC", this.Name, Application.ProductVersion);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(Transferencia.Modulo, Transferencia.Version, this.Name);

            CheckForIllegalCrossThreadCalls = false; 
        
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            btnCancelar.Enabled = true;
            Fg.IniciaControles(this, true);
            CargarConfiguracion();
            txtServidor.Focus();
        }

        private bool validarDatos()
        {
            bool bRegresa = true; 

            if (txtServidor.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha especificado el Servidor destino, verifique.");
                txtServidor.Focus();
            }

            if (txtWebService.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha especificado el Servicio Web destino, verifique.");
                txtWebService.Focus();
            }

            if (txtPagina.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha especificado la Pagina destino, verifique.");
                txtPagina.Focus();
            }

            return bRegresa;
        }

        private bool validarDatos_FTP()
        {
            bool bRegresa = true;

            if (txtServidorFTP.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha especificado el Servidor FTP destino, verifique.");
                txtServidor.Focus();
            }

            if (txtUsuarioFTP.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha especificado el usuario destino, verifique.");
                txtUsuarioFTP.Focus();
            }

            if (txtPassFTP.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha especificado el password de usuario, verifique.");
                txtPassFTP.Focus();
            }

            return bRegresa;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                Grabar("A");
            }
        }

        private void Grabar(string Status)
        {
            string sSql = ""; 
            int iSSL = chkSSL.Checked ? 1 : 0;
            int iCA = chkCA.Checked ? 1 : 0;
            string sCadena = txtPassFTP.Text;
            string sPass = crypto.PasswordEncriptar(sCadena);

            sSql = string.Format("Exec {0} ", sSP);
            sSql += string.Format(" @Servidor = '{0}', @WebService = '{1}', @PaginaWeb = '{2}', @SSL = '{3}', @IdEstado = '{4}', @IdFarmacia = '{5}', " +
                " @Status = '{6}', @ServidorFTP = '{7}', @UserFTP = '{8}', @PasswordFTP = '{9}', @ModoActivoDeTransferenciaFTP = '{10}' ",
                txtServidor.Text, txtWebService.Text, txtPagina.Text, iSSL, txtIdEstado.Text, txtIdFarmacia.Text, Status.ToUpper(),
                txtServidorFTP.Text, txtUsuarioFTP.Text, sPass, iCA); 

            if (Status == "C")
            {
                sSql = string.Format("Update {0} Set Status = 'C' ", sTabla);
            }

            if (cnn.Abrir())
            {
                cnn.IniciarTransaccion();
                if (!leer.Exec(sSql))
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(leer, "Grabar");
                    General.msjError(sError);
                }
                else
                {
                    cnn.CompletarTransaccion();
                    General.msjUser(sExito);
                    btnNuevo_Click(null, null);
                }
                cnn.Cerrar();
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            sError = "Ocurrió un error al cancelar la información.";
            sExito = "Información cancelada satisfactoriamente.";
            Grabar("C");
            btnNuevo_Click(null, null);
        }
        #endregion Botones

        private void btnTestConexion_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                _workerThread = new Thread(this.TestConexion);
                _workerThread.Name = "Probar conexión";
                _workerThread.Start();
            }
        }

        private void TestConexion()
        {
            string sDireccion = "";
            string sSSL = chkSSL.Checked ? "s" : ""; 

            toolStripBarraMenu.Enabled = false;
            FrameDatosConexion.Enabled = false;


            sDireccion = string.Format("http{0}://{1}/{2}/{3}.asmx", sSSL, txtServidor.Text.Trim(), txtWebService.Text.Trim(), txtPagina.Text.Trim());  

            FrmTestConexion frm = new FrmTestConexion(sDireccion);
            frm.ShowDialog();

            toolStripBarraMenu.Enabled = true;
            FrameDatosConexion.Enabled = true;
            _workerThread = null; 
        }

        private void CargarConfiguracion()
        {
            int iLargo = txtPassFTP.Text.Length;
            btnCancelar.Enabled = true;
            string sSql = string.Format(" Select C.*, IsNull(F.IdEstado, '') as IdEstado, IsNull(F.Estado, '') as Estado, " +
                " IsNull(F.IdFarmacia, '') as IdFarmacia, IsNull(F.Farmacia, '') as Farmacia " + 
                " From {0} C (NoLock) " + 
                " Left Join vw_Farmacias F (NoLock) On ( C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia ) ", sTabla );
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "FrmConfigurarConexionOficinaCentral_Load");
                General.msjUser("Ocurrió un error al obtener la información de configuración.");
            }
            else
            {
                if (leer.Leer())
                {
                    txtIdEstado.Text = leer.Campo("IdEstado");
                    lblEstado.Text = leer.Campo("Estado");
                    txtIdFarmacia.Text = leer.Campo("IdFarmacia");
                    lblFarmacia.Text = leer.Campo("Farmacia"); 

                    txtServidor.Text = leer.Campo("servidor");
                    txtWebService.Text = leer.Campo("webService");
                    txtPagina.Text = leer.Campo("paginaWeb");
                    chkSSL.Checked = leer.CampoBool("SSL");

                    txtServidorFTP.Text = leer.Campo("ServidorFTP");
                    txtUsuarioFTP.Text = leer.Campo("UserFTP");
                    chkCA.Checked = leer.CampoBool("ModoActivoDeTransferenciaFTP"); 

                    try
                    {
                        txtPassFTP.Text = crypto.PasswordDesencriptar(leer.Campo("PasswordFTP")).Substring(iLargo);
                    }
                    catch
                    {
                        txtPassFTP.Text = "";
                    }

                    //txtPassFTP.Text = leer.Campo("PasswordFTP");

                    if (leer.Campo("status").ToUpper() == "C")
                    {
                        btnCancelar.Enabled = false;
                        General.msjUser("Los datos de conexión actualmente se encuentran cancelados.");
                    }

                }
            }
        }

        private void FrmConfigurarConexionOficinaCentral_Load(object sender, EventArgs e)
        {
            if (Transferencia.ServicioEnEjecucion == TipoServicio.Cliente)
            {
                this.Text = "Configuración de obtención de información Oficina Central Regional";
                sSP = " spp_CFGC_ConfigurarConexion ";
                sTabla = " CFGC_ConfigurarConexion ";
            }

            if (Transferencia.ServicioEnEjecucion == TipoServicio.OficinaCentralRegional)
            {
                this.Text = "Configuración Conexión a Oficina Central";
                sSP = " spp_CFGSC_ConfigurarConexion ";
                sTabla = " CFGSC_ConfigurarConexion ";
            }

            btnNuevo_Click(null, null);
        }


        private bool CargarDatos(int Opcion)
        {
            bool bRegresa = true;

            if (Opcion == 1)
            {
                if (leer.Leer())
                {
                    txtIdEstado.Enabled = false;
                    txtIdEstado.Text = leer.Campo("IdEstado");
                    lblEstado.Text = leer.Campo("Nombre");
                }
                else
                {
                    bRegresa = false;
                    General.msjUser("Clave de estado no encontrada, verifique.");
                    txtIdEstado.Text = "";
                    lblEstado.Text = "";
                }
            }

            if (Opcion == 2)
            {
                if (leer.Leer())
                {
                    txtIdFarmacia.Enabled = false;
                    txtIdFarmacia.Text = leer.Campo("IdFarmacia");
                    lblFarmacia.Text = leer.Campo("Farmacia"); 
                }
                else
                {
                    bRegresa = false;
                    General.msjUser("La farmacia seleccionada no existes ó no pertenence al estado seleccionado, verifique.");
                    txtIdFarmacia.Text = "";
                    lblFarmacia.Text = "";
                }
            }

            return bRegresa;
        }

        private void txtIdEstado_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdEstado.Text.Trim() != "")
            {
                //string sSql = string.Format("Select * From CatEstados (NoLock) Where IdEstado = '{0}' ", Fg.PonCeros(txtIdEstado.Text, 2));
                leer.DataSetClase = query.Estados(txtIdEstado.Text, "txtIdEstado_Validating");
                e.Cancel = !CargarDatos(1);
            }
        }

        private void txtIdEstado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Estados("txtIdEstado_KeyDown");
                if (Ayuda.ExistenDatos)
                {
                    CargarDatos(1);
                }
            }
        }

        private void txtIdFarmacia_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdFarmacia.Text.Trim() != "")
            {
                //string sSql = string.Format("Select * From CatFarmacias (NoLock) Where IdEstado = '{0}' and IdFarmacia = '{1}' ", Fg.PonCeros(txtIdEstado.Text, 2), Fg.PonCeros(txtIdFarmacia.Text,4));
                leer.DataSetClase = query.Farmacias(txtIdEstado.Text, txtIdFarmacia.Text, "txtIdFarmacia_Validating");
                e.Cancel = !CargarDatos(2);
            }
        }

        private void txtIdFarmacia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Farmacias("txtIdFarmacia_KeyDown", txtIdEstado.Text);
                if (Ayuda.ExistenDatos)
                {
                    CargarDatos(2);
                }
            }
        }

        private void btnEnviarInformacion_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                if (General.msjConfirmar("Toda la información de los catálogos y configuraciones se enviaran a la unidad selecciona, ¿ Desea continuar ?") == DialogResult.Yes)
                {
                    toolStripBarraMenu.Enabled = false;
                    FrameInfUnidad.Enabled = false;
                    FrameDatosConexion.Enabled = false;

                    Thread.Sleep(500);
                    _workerThread = new Thread(this.ReplicarInformacion);
                    _workerThread.Name = "ReplicarInformacion";
                    _workerThread.Start();
                }
            }
        }

        private void ReplicarInformacion()
        {
            clsCliente oficina = new clsCliente("OficinaCentral", General.DatosConexion);

            oficina.GenerarArchivos();
            if (oficina.EnviarArchivos())
            {
                General.msjUser("Información enviada satisfactoriamente.");
            }
            else
            {
                General.msjAviso("La información se generó satisfactoriamente, pero no fue posible enviarla a la Unidad.");
            }

            toolStripBarraMenu.Enabled = true;
            FrameInfUnidad.Enabled = true;
            FrameDatosConexion.Enabled = true;
            _workerThread = null;
        }

        private void encriptarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtPassFTP.PasswordChar = '*';
            txtPassFTP.PasswordChar = '*'; 
        }

        private void desencriptarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtPassFTP.PasswordChar = '\0';
            txtPassFTP.PasswordChar = '\0'; 
        }

        private void btnTestFTP_Click(object sender, EventArgs e)
        {
            if (validarDatos_FTP())
            {
                _workerThread = new Thread(this.TestConexion_FTP);
                _workerThread.Name = "Probar conexión";
                _workerThread.Start();
            }
        }

        private void TestConexion_FTP()
        {
            toolStripBarraMenu.Enabled = false;
            FrameDatosConexion.Enabled = false;

            FrmTestConexion frm = new FrmTestConexion(txtServidorFTP.Text, txtUsuarioFTP.Text, txtPassFTP.Text, chkCA.Checked);
            frm.ShowDialog(); 

            toolStripBarraMenu.Enabled = true;
            FrameDatosConexion.Enabled = true;
            _workerThread = null;
        }

    }
}
