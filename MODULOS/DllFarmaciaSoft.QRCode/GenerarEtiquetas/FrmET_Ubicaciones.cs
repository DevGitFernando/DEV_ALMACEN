using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem.QRCode.Codec;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.QRCode;

using SC_SolutionsSystem.QRCode;
using SC_SolutionsSystem.SistemaOperativo; 


namespace DllFarmaciaSoft.QRCode.GenerarEtiquetas
{
    public partial class FrmET_Ubicaciones : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsAyudas ayuda;
        clsLeer leer;
        //clsLeer leerInformacion;
        //clsLeer leerInformacion_Aux;
        // clsListView lst;
        clsGrid grid;

        QR_Reporte Impresion; // = new QR_Reporte(); 
        DataSet dtsPosicion = new DataSet();
        DataSet dtsInformacion = new DataSet();
        DataSet dtsReeimpresion = new DataSet();
        string sFolioGenerado = "";

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        QR_Reader reader;

        bool bPosicion = false;
        bool bInformacion = false;

        bool bExisteImpresoraEtiquetas = DtGeneral.Impresoras.ExisteImpresora("etiquetas");
        bool bExisteLector = DtGeneral.Camaras.ExisteCamara("QReader");

        public FrmET_Ubicaciones()
        {
            QR_General.InicializarSDK(); 

            InitializeComponent();

            CargarFormatosEtiquetas(); 
            chkMostrarImpresionEnPantalla.BackColor = General.BackColorBarraMenu;
            leer = new clsLeer(ref cnn);
            btnNuevo_Click(this, null);
        }

        #region Eventos 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtPasillo.Text = "";
            txtPasillo.Enabled = true;
            txtEstante.Text = "";
            txtEstante.Enabled = false;
            txtEntrepaño.Text = "";
            txtEntrepaño.Enabled = false;
            lblEntrepaño.Text = "";
            lblEstante.Text = "";
            lblPasillo.Text = "";
            chkMostrarImpresionEnPantalla.Checked = false;
            IniciarToolBar(true, false, true);
            txtPasillo.Focus();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (ValidaDatos())
            {
                Impresion = new QR_Reporte();
                Impresion.Reporte = "ET_Ubicacion";
                Impresion.Reporte = cboFormatosEtiquetas.Data; 
                QR_General.IdPasillo = Convert.ToInt32(txtPasillo.Text);
                QR_General.IdEstante = Convert.ToInt32(txtEstante.Text);
                QR_General.IdEntrepaño = Convert.ToInt32(txtEntrepaño.Text);
                QR_General.GenerarUbicacion();
                DatosPosicion();
                IniciarToolBar(false, true, false);
                cboFormatosEtiquetas.Enabled = false; 

                Impresion.AgregarEtiqueta(QR_General.Encoder.Imagen);
                Impresion.AgregarDetalles(dtsPosicion);

                Impresion.GenerarXml();
                QR_General.GuardarXml(Impresion.Datos);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Impresion.EnviarAImpresora = !chkMostrarImpresionEnPantalla.Checked;
            Impresion.Imprimir();

            General.msjUser("Etiqueta generada satisfactoriamente.");
            btnNuevo_Click(null, null); 
        }

        private void btnReader_Click(object sender, EventArgs e)
        {
            reader = new QR_Reader();
            // reader.Camara = "Chicony USB 2.0 Camera";
            reader.Camara = DtGeneral.Camaras.GetCamara("QReader");
            reader.Show();

            if (reader.DatosLeidos && reader.Resultado.BarcodeFormat == ZXing.BarcodeFormat.QR_CODE)
            {
                string[] a = reader.DatosDecodificados.Split('|');

                if (a[1] == DtGeneral.EstadoConectado)
                {
                    General.msjAviso("Etiqueta de otro estado.");
                }
                else if (a[2] == DtGeneral.FarmaciaConectada)
                {
                    General.msjAviso("Etiqueta de otra unidad.");
                }
                else
                {
                    txtPasillo.Text = a[2];
                    txtEstante.Text = a[3];
                    txtEntrepaño.Text = a[4];
                }
            }
        }

        private void txtPasillo_Validating(object sender, CancelEventArgs e)
        {
            if (txtPasillo.Text != "")
            {
                txtPasillo.Enabled = false;
                string sSql = string.Format("Select IdPasillo, DescripcionPasillo, Status From CatPasillos " +
                                "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And IdPasillo = '{3}'",
                                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtPasillo.Text);

                if (leer.Exec(sSql))
                {
                    if (leer.Leer())
                    {
                        if (leer.Campo("Status") == "C")
                        {
                            General.msjAviso("Rack cancelado. Favor de verificar.");
                            txtPasillo.Text = "";
                            txtPasillo.Enabled = true;
                        }
                        else
                        {
                            lblPasillo.Text = leer.Campo("DescripcionPasillo");
                            txtPasillo.Text = leer.Campo("IdPasillo");
                            txtEstante.Enabled = true;
                            txtEstante.Focus();
                        }

                    }
                    else
                    {
                        General.msjAviso("Rack no existe. Favor de verificar.");
                        txtPasillo.Text = "";
                        txtPasillo.Enabled = true;
                        txtPasillo.Focus();
                    }
                }
                else
                {
                    Error.GrabarError(leer, "txtPasillo_Validating");
                }
            }
            
        }

        private void txtEstante_Validating(object sender, CancelEventArgs e)
        {
            if (txtEstante.Text != "")
            {
                txtEstante.Enabled = false;
                string sSql = string.Format("Select IdEstante, DescripcionEstante, Status From CatPasillos_Estantes " +
                                "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And IdPasillo = '{3}' And IdEstante = '{4}'",
                                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtPasillo.Text, txtEstante.Text);

                if (leer.Exec(sSql))
                {
                    if (leer.Leer())
                    {
                        if (leer.Campo("Status") == "C")
                        {
                            General.msjAviso("Nivel cancelado. Favor de verificar.");
                            txtEstante.Text = "";
                            txtEstante.Enabled = true;
                        }
                        else
                        {
                            lblEstante.Text = leer.Campo("DescripcionEstante");
                            txtEstante.Text = leer.Campo("IdEstante");
                            txtEntrepaño.Enabled = true;
                            txtEntrepaño.Focus();
                        }

                    }
                    else
                    {
                        General.msjAviso("Nivel no existe. Favor de verificar.");
                        txtEstante.Text = "";
                        txtEstante.Enabled = true;
                        txtPasillo.Focus();
                    }
                }
                else
                {
                    Error.GrabarError(leer, "txtEstante_Validating");
                }
            }
        }

        private void txtEntrepaño_Validating(object sender, CancelEventArgs e)
        {
            if (txtEntrepaño.Text != "")
            {
                txtEntrepaño.Enabled = false;
                string sSql = string.Format("Select IdEntrepaño, DescripcionEntrepaño, Status From CatPasillos_Estantes_Entrepaños " +
                                "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And IdPasillo = '{3}' And IdEstante = '{4}' And IdEntrepaño = '{5}'",
                                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtPasillo.Text, txtEstante.Text, txtEntrepaño.Text);

                if (leer.Exec(sSql))
                {
                    if (leer.Leer())
                    {
                        if (leer.Campo("Status") == "C")
                        {
                            General.msjAviso("Posición cancelada. Favor de verificar.");
                            txtEntrepaño.Text = "";
                            txtEntrepaño.Enabled = true;
                        }
                        else
                        {
                            lblEntrepaño.Text = leer.Campo("DescripcionEntrepaño");
                            txtEntrepaño.Text = leer.Campo("IdEntrepaño");

                        }

                    }
                    else
                    {
                        General.msjAviso("Posición no existe. Favor de verificar.");
                        txtEntrepaño.Text = "";
                        txtEntrepaño.Enabled = true;
                        txtEntrepaño.Focus();
                    }
                }
                else
                {
                    Error.GrabarError(leer, "txtEntrepaño_Validating");
                }
            }
        }

        #endregion Eventos

        #region Funciones y procedimientos 
        private void CargarFormatosEtiquetas()
        {
            cboFormatosEtiquetas.Clear();
            cboFormatosEtiquetas.Add("ET_Ubicacion", "Vertical", "");
            cboFormatosEtiquetas.Add("ET_Ubicacion_Formato__001", "Horizontal", "");
        }

        private void IniciarToolBar(bool Ejecutar, bool Imprimir, bool Reader)
        {
            btnEjecutar.Enabled = Ejecutar;
            btnImprimir.Enabled = Imprimir;
            btnReader.Enabled = Reader;
        }

        private bool DatosPosicion()
        {
            bool bRegresa = false;
            string sSql = string.Format(
                "Select " + 
                "   N.IdEmpresa, '{6}' as Empresa, N.IdEstado, '{7}' as Estado, N.IdFarmacia, '{8}' as Farmacia , cast(N.IdPasillo as varchar) as IdPasillo, DescripcionPasillo, " + 
                "   cast(N.IdEstante as varchar) as IdEstante, DescripcionEstante, " + 
                "   cast(N.IdEntrepaño as varchar) as IdEntrepaño, DescripcionEntrepaño " +
                "From CatPasillos P (NoLock) " +
                "Inner Join CatPasillos_Estantes S (NoLock) " +
                "	On (S.IdEmpresa = P.IdEmpresa And S.IdEstado = P.IdEstado And S.IdFarmacia = P.IdFarmacia And S.IdPasillo = P.IdPasillo) " +
                "Inner Join CatPasillos_Estantes_Entrepaños N (NoLock) " +
                "	On (S.IdEmpresa = N.IdEmpresa And S.IdEstado = N.IdEstado And S.IdFarmacia = N.IdFarmacia And S.IdPasillo = N.IdPasillo And S.IdEstante = N.IdEstante)" +
                "Where N.IdEmpresa = '{0}' and N.IdEstado = '{1}' and N.IdFarmacia = '{2}' " +
                "and N.IdPasillo = '{3}' and N.IdEstante = '{4}' and N.IdEntrepaño = '{5}' ",
                sEmpresa, sEstado, sFarmacia, txtPasillo.Text, txtEstante.Text,  txtEntrepaño.Text, 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            if (!leer.Exec("Posicion", sSql))
            {
                Error.GrabarError(leer, "DatosPosicion()");
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjAviso("Posición no encontrada.");
                }
                else
                {
                    bRegresa = true;
                    dtsPosicion = leer.DataSetClase;
                }
            }

            bPosicion = bRegresa;
            return bRegresa;
        }

        private bool ValidaDatos()
        {
            bool bDevuelbe = true;

            if (txtPasillo.Text == "")
            {
                bDevuelbe = false;
                General.msjAviso("Capturar Rack. Favor de verificar.");
            }
            else if (txtEstante.Text == "")
            {
                bDevuelbe = false;
                General.msjAviso("Capturar Nivel. Favor de verificar.");
            }
            else if (txtEntrepaño.Text == "")
            {
                bDevuelbe = false;
                General.msjAviso("Capturar Posición. Favor de verificar.");
            }

            return bDevuelbe;
        }

        #endregion Funciones y procedimientos
    }
}
