using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.FuncionesGrid;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Usuarios_y_Permisos;


namespace OficinaCentral.Catalogos.Proveedores
{
    public partial class FrmProveedoresCertificacion : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerDoctos;
        clsAyudas Ayuda;
        clsConsultas Consultas;
        clsGrid Grid;

        #region Documentos
        OpenFileDialog file = new OpenFileDialog();
        FolderBrowserDialog Folder = new FolderBrowserDialog();

        string sDocto = "";
        string sNombreDocto = "";
        string sNomDocto = "";
        #endregion Documentos
              

        public FrmProveedoresCertificacion()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            leerDoctos = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.Modulo, GnOficinaCentral.Version, this.Name);

            Grid = new clsGrid(ref grdDocumentos, this);
            Grid.EstiloGrid(eModoGrid.ModoRow);
            grdDocumentos.EditModeReplace = true;
            Grid.BackColorColsBlk = Color.White;
            Grid.AjustarAnchoColumnasAutomatico = true;

            //DtGeneral.PermisosEspeciales_Biometricos.CargarPermisos();
            //DtGeneral.PermisosEspeciales_Biometricos.Conectado = true;

            Cargar_TipoDoctos();
        }

        private void FrmProveedoresCertificacion_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarDocumento();
        }

        private void btnCertificar_Click(object sender, EventArgs e)
        {
            Certificar_Proveedor();
        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            lblCancelado.Visible = false;
            IniciaBotones(false, false);
            Grid.Limpiar(true);
            cboDoctos.SelectedIndex = 0;
            txtProv.Focus();
        }

        private void Cargar_TipoDoctos()
        {
            string sSql = "";

            cboDoctos.Clear();
            cboDoctos.Add();

            sSql = string.Format(" Select * From CatProveedores_TipoDoctos Order by IdDocumento ");

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Cargar_TipoDoctos");
                General.msjError("Ocurrio un error al buscar los tipos de documentos.");
            }
            else
            {
                if (leer.Leer())
                {
                    cboDoctos.Add(leer.DataSetClase, true, "IdDocumento", "Descripcion");                    
                }
            }

            cboDoctos.SelectedIndex = 0;
        }

        private void IniciaBotones(bool Guardar, bool Certificar)
        {
            btnGuardar.Enabled = Guardar;
            btnCertificar.Enabled = Certificar;
           
        }
        #endregion Funciones

        #region Eventos_Proveedor
        private void txtProv_Validating(object sender, CancelEventArgs e)
        {            
            if (txtProv.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Proveedores(txtProv.Text.Trim(), "txtProv_Validating");
                    
                if (leer.Leer())
                {                    
                    CargaDatos();
                    CargarDocumentos();
                }                
            }
        }

        private void txtProv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Proveedores("txtId_KeyDown");
                            
                if (leer.Leer())
                {
                    CargaDatos();
                    CargarDocumentos();
                }
            }
        }

        private void CargaDatos()
        {
            IniciaBotones(true, true);

            txtProv.Text = leer.Campo("IdProveedor");
            lblProv.Text = leer.Campo("Nombre");

            txtProv.Enabled = false;

            if (leer.Campo("Status").ToUpper() == "C")
            {
                lblCancelado.Visible = true;
                IniciaBotones(false, false);
            }

        }

        private void CargarDocumentos()
        {
            string sSql = "";

            sSql = string.Format(" Select P.IdDocumento, D.Descripcion, P.NombreDocumento, P.Documento From CatProveedores_Certificacion_Doctos P " +
                                " Inner Join CatProveedores_TipoDoctos D On ( D.IdDocumento = P.IdDocumento ) " +
	                            " Where P.IdProveedor = '{0}' Order By P.IdDocumento ", Fg.PonCeros(txtProv.Text, 4));

            Grid.Limpiar(false);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrio un error al buscar los documentos del Proveedor.");
            }
            else
            {
                if (leer.Leer())
                {
                    leerDoctos.DataSetClase = leer.DataSetClase;
                    Grid.LlenarGrid(leer.DataSetClase, false, false);
                }
            }
        }
        #endregion Eventos_Proveedor

        #region Agregar_Documento
        private void btnAgregarDocto_Click(object sender, EventArgs e)
        {
            sDocto = "";
            sNombreDocto = "";
            lblDirectorio.Text = "";

            file = new OpenFileDialog();
            file.Multiselect = false;
            file.Title = "Seleccione el archivo a cargar";
            file.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();

            if (file.ShowDialog() == DialogResult.OK)
            {
                FileInfo docto = new FileInfo(file.FileName);
                 
                sDocto = Fg.ConvertirArchivoEnStringB64(file.FileName);
                sNombreDocto = docto.Name;
                lblDirectorio.Text = file.FileName;
            }
        }
        #endregion Agregar_Documento

        #region Guardar_Actualizar_Documento
        private bool GuardarDocumento()
        {
            bool bRegresa = true;
            string sSql = "";            

            if (ValidaDatos())
            {
                if (!cnn.Abrir())
                {
                    General.msjAviso(General.MsjErrorAbrirConexion);
                }
                else
                {
                    cnn.IniciarTransaccion();

                    sSql = string.Format(" Exec spp_Mtto_CatProveedores_Certificacion_Doctos '{0}', '{1}', '{2}', '{3}' ",
                                        Fg.PonCeros(txtProv.Text, 4), cboDoctos.Data, sNombreDocto, sDocto );

                    bRegresa = leer.Exec(sSql);
                    

                    if (bRegresa) // Si no Ocurrió ningun error se llevan a cabo las transacciones.
                    {
                        
                        cnn.CompletarTransaccion();
                        General.msjUser("La Información se guardo satisfactoriamente.");
                        LimpiaControles();
                        CargarDocumentos();
                    }
                    else
                    {
                        Error.GrabarError(leer, "GuardarDocumento");
                        cnn.DeshacerTransaccion();
                        General.msjError("Ocurrió un error al guardar la información.");

                    }

                    cnn.Cerrar();
                }
            }

            return bRegresa;
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtProv.Text.Trim() == "")
            {
                General.msjAviso("Clave de proveedor invalido, Verifique!!");
                txtProv.Focus();
            }

            if (bRegresa && cboDoctos.SelectedIndex == 0)
            {
                General.msjAviso("No ha seleccionado el tipo de documento, Verifique!!");
                cboDoctos.Focus();
                bRegresa = false;
            }

            if (bRegresa && lblDirectorio.Text.Trim() == "")
            {
                General.msjAviso("No ha cargado el documento a guardar, Verifique!!");
                btnAgregarDocto.Focus();
                bRegresa = false;
            }            

            return bRegresa;
        }

        private void LimpiaControles()
        {
            cboDoctos.SelectedIndex = 0;
            lblDirectorio.Text = "";
            sDocto = "";
            sNombreDocto = "";
        }
        #endregion Guardar_Actualizar_Documento

        #region Certificacion_de_Proveedores
        private void Certificar_Proveedor()
        {
            bool bCertifica = false;
            string sMsjNoEncontrado = "El usuario no tiene permiso para certificar a proveedores, verifique por favor.";

            if (rdoRpteLegal.Checked)
            {
                if (General.msjConfirmar("¿ Esta certificando como Representante Legal es correcto. ?") == DialogResult.Yes)
                {
                    bCertifica = DtGeneral.PermisosEspeciales_Biometricos.VerificarPermisos("CERTIFICA_RPTE_LEGAL", sMsjNoEncontrado);
                }
                
            }

            if (rdoRpteSanitario.Checked)
            {
                if (General.msjConfirmar("¿ Esta certificando como Representante Sanitario es correcto. ?") == DialogResult.Yes)
                {
                    bCertifica = DtGeneral.PermisosEspeciales_Biometricos.VerificarPermisos("CERTIFICA_RPTE_SANITARIO", sMsjNoEncontrado);
                }
            }

            if (bCertifica)
            {
                Guarda_Certificacion();
            }
            
        }

        private bool AplicaCertificacion()
        {
            bool bRegresa = true;
            string sSql = "";
            int iTipoRpte = 0;

            if (rdoRpteLegal.Checked)
            {
                iTipoRpte = 1;
            }

            if (rdoRpteSanitario.Checked)
            {
                iTipoRpte = 2;
            }

            sSql = string.Format(" Exec spp_Mtto_CatProveedores_Certificacion '{0}', '{1}', '{2}', '{3}' ",
                                Fg.PonCeros(txtProv.Text, 4), DtGeneral.PermisosEspeciales_Biometricos.ReferenciaHuella, DtGeneral.NombrePersonal, iTipoRpte );

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "AplicaCertificacion");
                General.msjError("Ocurrio un error al certificar el proveedor.");
            }
            else
            {
                if (leer.Leer())
                {
                    if (leer.CampoInt("Certifica") == 0)
                    {
                        bRegresa = false;
                        General.msjAviso("El proveedor no cuenta con los documentos completos. Verifique!!");
                    }
                }
            }

            return bRegresa;
        }

        private void Guarda_Certificacion()
        {
            bool bRegresa = true;            
            
            if (!cnn.Abrir())
            {
                General.msjAviso(General.MsjErrorAbrirConexion);
            }
            else
            {
                cnn.IniciarTransaccion();

                bRegresa = AplicaCertificacion();


                if (bRegresa) // Si no Ocurrió ningun error se llevan a cabo las transacciones.
                {
                    cnn.CompletarTransaccion();
                    General.msjUser("La certificación del proveedor se guardo satisfactoriamente.");
                    LimpiaControles();
                    CargarDocumentos();
                }
                else
                {                        
                    cnn.DeshacerTransaccion();                        
                }

                cnn.Cerrar();
            }
            
        }
        #endregion Certificacion_de_Proveedores
    }
}
