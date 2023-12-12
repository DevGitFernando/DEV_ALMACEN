using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.FP;
using SC_SolutionsSystem.FP.Huellas;
using SC_SolutionsSystem.Data;

using DllFarmaciaSoft;


namespace Farmacia.Vales
{
    public partial class FrmHuellasVales : FrmBaseExt
    {
        //public string sIdPersonaFirma = "";
        public bool bTieneHuella = false;
        bool bEsPersonal = true;
        string sUrlChecador = "";
        string sHost = "";
        
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsConexionSQL CnnLocal = new clsConexionSQL(General.DatosConexion);
        clsDatosConexion DatosDeConexion;
       
        clsLeer leer;
        clsLeer leerChecador;
        //clsLeer leerHuellas;
        clsConsultas Consultas;

        public string sGUID = ""; 
        DllFarmaciaSoft.wsFarmacia.wsCnnCliente validarHuella = null;

        public FrmHuellasVales()
        {
            InitializeComponent();

            leer = new clsLeer(ref CnnLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            //FP_General.Conexion = General.DatosConexion;
            validarHuella = new DllFarmaciaSoft.wsFarmacia.wsCnnCliente();
            validarHuella.Url = General.Url;
            leerChecador = new clsLeer(ref cnn);

            bEsPersonal = true; 
        }

        public FrmHuellasVales(bool EsPersonaFarmacia)
        {
            InitializeComponent();
            leer = new clsLeer(ref CnnLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            FP_General.Conexion = General.DatosConexion;
            bEsPersonal = EsPersonaFarmacia;

            validarHuella = new DllFarmaciaSoft.wsFarmacia.wsCnnCliente();
            validarHuella.Url = General.Url;
            leerChecador = new clsLeer(ref cnn);  
        }

        private void FrmHuellasVales_Load(object sender, EventArgs e)
        {
            btnGuardar.Enabled = false;
            chkEsPersonalFarmacia.Enabled = false;
            chkEsPersonalFarmacia.Checked = bEsPersonal;
            if (!bEsPersonal)
            {
                SendKeys.Send("{TAB}");
            }

            if (Obtener_Url_Firma())
            {
                if (validarDatosDeConexion())
                {
                    ConexionChecador();
                }
            }

            LlenarParentesco();
        }

        #region Eventos

        private void btnHuellas_Click(object sender, EventArgs e)
        {
            LeerHuella_Usuario();
        }

        private bool LeerHuella_Usuario()
        {
            bool bRegresa = false;
            sGUID = Guid.NewGuid().ToString();
            FP_General.TablaHuellas = "FP_Huellas_Vales";
            FP_General.StoreRegistroHuellas = "spp_Registrar_Huellas_Vales";
            FP_General.Referencia_Huella = sGUID;
            FP_General.Dedo = Dedos.MD_Indice;

            clsLeerHuella f = new clsLeerHuella();
            f.Show();

            if (FP_General.HuellaRegistrada)
            {
                bTieneHuella = true;
                bRegresa = true;
            }
            return bRegresa;
        }

        //private void CargarHuellasRegistradas()
        //{
        //    string sSql = string.Format("Exec spp_Get_Huellas_Personal_Vales '{0}' ", CalcularReferencia());

        //    //lst.LimpiarItems();
        //    if (leer.Exec(sSql))
        //    {
        //        //lst.CargarDatos(leer.DataSetClase, false, false);
        //    }
        //}

        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            leer = new clsLeer(ref CnnLocal);

            if (txtId.Text.Trim() == "")
            {
                //IniciaToolBar(true, true, false, false);
                txtId.Enabled = false;
                txtId.Text = "*";
                btnGuardar.Enabled = true;
            }
            else
            {
                leer.DataSetClase = Consultas.FarmaciaValesHuellas(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtId.Text.Trim(), "txtId_Validating");
                if (leer.Leer())
                {
                    CargaDatos();
                    ChecarHuella();
                }
                else
                {
                    btnNuevo_Click(null, null);
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "";
            int iOpcion = 1;

            if (ValidaDatos())
            {
                if (cnn.Abrir())
                {
                    FP_General.Abrir();
                    FP_General.IniciarTransaccion();
                    cnn.IniciarTransaccion();

                    if (LeerHuella_Usuario())
                    {
                        sSql = String.Format("Exec spp_ValesPersona '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', {8}, '{9}' ",
                                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtId.Text.Trim(), sGUID, txtNombre.Text.Trim(),
                                txtPaterno.Text.Trim(), txtMaterno.Text.Trim(), cboParentesco.Data, Convert.ToInt32(chkEsPersonalFarmacia.Checked), iOpcion);

                        if (leer.Exec(sSql))
                        {
                            if (leer.Leer())
                            {
                                sMensaje = leer.Campo("Mensaje");
                                txtId.Text = leer.Campo("Clave");
                            }

                            cnn.CompletarTransaccion();
                            FP_General.CompletarTransaccion();
                            btnGuardar.Enabled = false;

                            if (!bEsPersonal)
                            {
                                this.Hide();
                            }
                            else
                            {
                                General.msjUser(sMensaje);
                                btnNuevo_Click(null, null);
                            }
                        }
                        else
                        {
                            cnn.DeshacerTransaccion();
                            FP_General.DeshacerTransaccion();
                            Error.GrabarError(leer, "btnGuardar_Click");
                            General.msjError("Ocurrió un error al guardar la información.");
                        }
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        FP_General.DeshacerTransaccion();
                    }

                    cnn.Cerrar();
                    FP_General.Cerrar();
                }
                else
                {
                    Error.LogError(cnn.MensajeError);
                    General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo.");
                }
            } 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtId.Text = "";
            txtId.Enabled = true;
            txtNombre.Text = "";
            txtPaterno.Text = "";
            txtMaterno.Text = "";
            txtId.Focus();
        }

        private void FrmHuellasVales_KeyDown(object sender, KeyEventArgs e)
        {
            //sIdPersonaFirma = sGUID;

            if (e.KeyCode == Keys.F12)
            {
                this.Hide();
            }
        }

        #endregion Eventos


        #region Eventos y Funciones
        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtNombre.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el nombre, verifique.");
                txtNombre.Focus();
            }

            if (bRegresa && txtPaterno.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el apellido paterno, verifique.");
                txtPaterno.Focus();
            }

            if (bRegresa && txtMaterno.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el apellido materno, verifique.");
                txtMaterno.Focus();
            }

            if (bRegresa && cboParentesco.SelectedIndex == 0)
            {
                bRegresa = false; 
                General.msjUser("No ha seleccionado un Parentesco válido, verifique.");
                cboParentesco.Focus(); 
            }

            //if (bRegresa)
            //{
            //    if (!bEsPersonal)
            //    {
            //        LeerHuella_Usuario();
            //        bRegresa = bTieneHuella; 
            //    }
            //}

            return bRegresa;
        }

        private void CargaDatos()
        {
            txtId.Enabled = false;
            txtNombre.Enabled = false;
            txtPaterno.Enabled = false;
            txtMaterno.Enabled = false;

            txtId.Text = leer.Campo("IdPersonaFirma");
            txtNombre.Text = leer.Campo("Nombre");
            txtPaterno.Text = leer.Campo("ApPaterno");
            txtMaterno.Text = leer.Campo("ApMaterno");
            chkEsPersonalFarmacia.Checked = leer.CampoBool("EsPersonalFarmacia");
        }

        private void ChecarHuella()
        {
            string sSql = string.Format("Select * From vw_FP_Huellas_Vales Where ReferenciaHuella = '{0}'", CalcularReferencia());

            if (leer.Exec(sSql))
            {
                //if (leer.Leer())
                //{
                //    btnHuellas.Enabled = false;
                //}
                //else
                //{
                //    btnHuellas.Enabled = true;
                //}
            }
            else
            {
                Error.GrabarError(leer, "btnGuardar_Click");
                General.msjError("Ocurrió un error al guardar la información.");
            }
        }

        private string CalcularReferencia()
        {
            return DtGeneral.EstadoConectado + DtGeneral.FarmaciaConectada + txtId.Text;
        }

        private bool Obtener_Url_Firma()
        {
            bool bRegresa = true;

            leer.DataSetClase = Consultas.Url_PersonalFirma("Obtener_Url_Firma");

            if (leer.Leer())
            {
                sUrlChecador = leer.Campo("UrlFarmacia");
                sHost = leer.Campo("Servidor");
            }
            else
            {
                bRegresa = false;
            }

            return bRegresa;
        }

        private bool validarDatosDeConexion()
        {
            bool bRegresa = false;

            try
            {
                //leerWeb = new clsLeerWebExt(sUrlChecador, DtGeneral.CfgIniChecadorPersonal, DatosCliente);
                validarHuella.Url = sUrlChecador;
                DatosDeConexion = new clsDatosConexion(validarHuella.ConexionEx(DtGeneral.CfgIniValidarHuellas));
                //DatosDeConexion = new clsDatosConexion(AbrirConexionEx(DtGeneral.CfgIniChecadorPersonal));
                //DatosDeConexion.Servidor = sHost;
                bRegresa = true;
            }
            catch (Exception ex1)
            {
                Error.GrabarError(leer.DatosConexion, ex1, "validarDatosDeConexion()");
                General.msjAviso("No fue posible establecer conexión con la Unidad, intente de nuevo.");
            }

            return bRegresa;
        }

        private void ConexionChecador()
        {
            cnn = new clsConexionSQL(DatosDeConexion);
            leerChecador = new clsLeer(ref cnn);
            FP_General.Conexion = DatosDeConexion;
        }

        private void LlenarParentesco()
        {
            leer = new clsLeer(ref CnnLocal);

            cboParentesco.Clear();
            cboParentesco.Add("0", "<< Seleccione >>");

            leer.DataSetClase = Consultas.Parentescos("", "LlenarPuestos()");
            if (leer.Leer())
            {
                cboParentesco.Add(leer.DataSetClase, true);
            }
            cboParentesco.SelectedIndex = 0;
        }

        #endregion Eventos y Funciones
    }
}
