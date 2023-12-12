using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos;
using DllFarmaciaSoft.Usuarios_y_Permisos;

using ZXing; 

namespace DllFarmaciaSoft
{
    public partial class FrmDecodificacionSNK : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        clsAyudas Ayuda;
        clsConsultas Consultas;

        clsDatosCliente DatosCliente;
        ItemCodificacion itemEncode = new ItemCodificacion(); 

        public FrmDecodificacionSNK()
        {
            InitializeComponent();

            cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario = General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            cnn.SetConnectionString();

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, ""); 


            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            DtGeneral.PermisosEspeciales_Biometricos.CargarPermisos();

            Error = new clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);

            ///// Ocultar la lectura del codigo 
            txtCodigo.PasswordChar = '*'; 

            lector = new ReaderCam(sCamara, BarcodeFormat.DATA_MATRIX, new Size(320, 240));
            lector.OnLectura += new EventHandler<ProcessArgs>(scanner_OnLectura);
            Application.DoEvents();
            System.Threading.Thread.Sleep(20);
            lector.IniciarCaptura(); 
        }

        #region Form 
        private void FrmDecodificacionSNK_Load(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void TeclasRapidas(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.N:
                    if (btnNuevo.Enabled)
                    {
                        btnNuevo_Click(null, null);
                    }
                    break;

                default:
                    break;
            }
        }

        private void FrmDecodificacionSNK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                TeclasRapidas(e);
            }
        }

        private void FrmDecodificacionSNK_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (lector != null)
            {
                lector.DetenerCaptura();
                lector.DetenerCaptura();
            }
        }
        #endregion Form

        #region SCANER WEB CAM
        bool bExisteLector = DtGeneral.Camaras.ExisteCamara("QReader");
        string sCamara = DtGeneral.Camaras.GetCamara("QReader");
        ZXing.ReaderCam lector;

        private void scanner_OnLectura(object sender, ProcessArgs e)
        {
            //////General.msjAviso(e.Resultado.Text); 
            /////itemEncode = CodificacionSNK.Decodificar(e.Resultado.Text);

            txtCodigo.Text = e.Resultado.Text; 
            Decodificar();

        }
        #endregion SCANER WEB CAM

        #region Botones
        private void LimpiarPantalla()
        {
            Fg.IniciaControles();
            lblResultado.Visible = false;
            lblResultado.BorderStyle = BorderStyle.None; 
            itemEncode = new ItemCodificacion();

            ////if (DtGeneral.EsEquipoDeDesarrollo)
            ////{
            ////    txtCodigo.Text = "00209000201007503001007663001|01LG150600|1612151027120510010001ABCDEFG";
            ////    if (!DtGeneral.EsAlmacen)
            ////    {
            ////        txtCodigo.Text = "00209001101007503006916038001|01142127|1610151027120510010001ABCDEFG"; 
            ////    }
            ////}

            txtCodigo.Focus(); 
        }

        private void Limpiar_Frame()       
        {
            lblResultado.Visible = false;
            Fg.IniciaControles(this, true, FrameInformacion);
            this.Refresh(); 
            Application.DoEvents(); 
            System.Threading.Thread.Sleep(1); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }
        #endregion Botones

        #region Captura de Codigo 
        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtCodigo.Text.Trim() != "")
                {
                    Decodificar();
                }
            }
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void txtCodigo_Validating(object sender, CancelEventArgs e)
        {
            if (txtCodigo.Text.Trim() != "")
            {
                Decodificar();
            }
        }
        #endregion Captura de Codigo

        #region Procedimientos y Funciones Publicos
        private void Decodificar() 
        {
            string sValor = txtCodigo.Text.Trim();
            Limpiar_Frame();

            //////txtCodigo.Text = "******************************"; 
            itemEncode = CodificacionSNK.Decodificar(sValor);

            lblUUID.Text = itemEncode.UUID; 
            lblEmpresa.Text = itemEncode.Empresa;
            lblEstado.Text = itemEncode.Estado;
            lblFarmacia.Text = itemEncode.Farmacia;
            lblCodificadora.Text = itemEncode.Codificadora;
            lblClaveSSA.Text = itemEncode.ClaveSSA;
            lblDescripcionClaveSSA.Text = itemEncode.DescripcionClave;
            lblCodigoEAN.Text = itemEncode.CodigoEAN;
            lblNombreComercial.Text = itemEncode.DescripcionComercial;
            lblSubFarmacia.Text = itemEncode.SubFarmacia;
            lblClaveLote.Text = itemEncode.ClaveLote;
            lblCaducidad.Text = itemEncode.Caducidad;
            lblCaducidad.Text = itemEncode.Caducidad_Formato;
            lblProveedor.Text = itemEncode.Proveedor;
            lblNumeroDeFactura.Text = itemEncode.NumeroDeFactura;

            lblResultado.Visible = true; 
            lblResultado.Text = itemEncode.Resultado;
            lblResultado.ForeColor = itemEncode.ColorResultado;

            txtCodigo.Text = ""; 
        }
        #endregion Procedimientos y Funciones Publicos 
    }
}
