using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Comun;

using DllFarmaciaSoft;

namespace OficinaCentral.Catalogos
{
    public partial class FrmFarmacias : FrmBaseExt 
    {
        clsConexionSQL con = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        // clsLeer leer2;
        
        
        clsAyudas Ayuda = new clsAyudas();
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;

        DataSet dtsEstados = new DataSet();
        // DataSet dtsFarmacias = new DataSet();
        DataSet dtsRegiones = new DataSet();
        DataSet dtsSubRegiones = new DataSet();
        DataSet dtsJurisdicciones = new DataSet();

        string sRegion = "0";

        public FrmFarmacias()
        {
            InitializeComponent();
            con.SetConnectionString();
            leer = new clsLeer(ref con);
            Consultas = new clsConsultas(General.DatosConexion, "OficinaCentral", this.Name, Application.ProductVersion);
            Ayuda = new clsAyudas(General.DatosConexion, "OficinaCentral", this.Name, Application.ProductVersion);

            btnFarmaciasRelacionadas.Visible = DtGeneral.EsEquipoDeDesarrollo;
            btnFarmaciasRelacionadas.Enabled = DtGeneral.EsEquipoDeDesarrollo;

            btnAlmacenesPermisosEspeciales.Visible = DtGeneral.EsEquipoDeDesarrollo;
            btnAlmacenesPermisosEspeciales.Enabled = DtGeneral.EsEquipoDeDesarrollo;

            btnCFDIPermisosEspeciales.Visible = DtGeneral.EsEquipoDeDesarrollo;
            btnCFDIPermisosEspeciales.Enabled = DtGeneral.EsEquipoDeDesarrollo;

            CargarTipoUnidades(); 
            CargarNivelesDeAtencion(); 
        }

        private void FrmFarmacias_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Funciones

        #region Obtener Datos 
        private void CargarEstados()
        {
            cboEdo.Clear();
            cboEdo.Add("0", "<< Seleccione >>");
            cboEdo.Add(Consultas.ComboEstados("CargarEstados()"), true, "IdEstado", "EstadoNombre");
            cboEdo.SelectedIndex = 0;
        }

        private void CargarRegiones()
        {
            cboRegion.Clear();
            cboRegion.Add("0", "<< Seleccione >>");
            cboRegion.Add(Consultas.ComboRegiones("CargarRegiones()"), true, "IdRegion", "Descripcion");
            cboRegion.SelectedIndex = 0;
        }

        private void CargarSubRegiones()
        {
            cboSubRegion.Clear();
            cboSubRegion.Add("0", "<< Seleccione >>");
            cboSubRegion.SelectedIndex = 0;
            dtsSubRegiones  = Consultas.ComboSubRegiones("CargarSubRegiones()");
        }

        private void CargarJurisdicciones()
        {
            cboJurisdicciones.Clear();
            cboJurisdicciones.Add("0", "<< Seleccione >>");
            cboJurisdicciones.SelectedIndex = 0;
            dtsJurisdicciones = Consultas.Jurisdicciones("CargarJurisdicciones()");
        }

        private void CargarTipoUnidades()
        {
            cboTipoUnidad.Clear();
            cboTipoUnidad.Add("0", "<< Seleccione >>");
            cboTipoUnidad.Add(Consultas.TipoUnidades("", "CargarTipoUnidades()"), true, "IdTipoUnidad", "NombreTipoUnidad");
            cboTipoUnidad.SelectedIndex = 0;
        }

        private void CargarNivelesDeAtencion()
        {
            cboNivelesDeAtencion.Clear();
            cboNivelesDeAtencion.Add("0", "<< Seleccione >>");
            cboNivelesDeAtencion.Add(Consultas.NivelesDeAtencion("", "CargarNivelesDeAtencion()"), true, "IdNivelDeAtencion", "NivelDeAtencion");
            cboNivelesDeAtencion.SelectedIndex = 0;
        }
        #endregion Obtener Datos

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (bRegresa && cboEdo.Data == "0")
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado el Estado, verifique.");
                cboEdo.Focus();
            }

            if (bRegresa && txtIdFar.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Número de Farmacia inválido, verifique.");
                txtIdFar.Focus();
            }

            if (bRegresa && txtNomFar.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Nombre de Farmacia inválido, verifique.");
                txtNomFar.Focus();
            }

            if (bRegresa && txtEmail.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha proporcionado el Correo Electronico de la Farmacia, verifique."); 
                txtEmail.Focus(); 
            }

            if (bRegresa && cboTipoUnidad.Data == "0")
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado el Tipo de Unidad, verifique.");
                cboTipoUnidad.Focus();
            }

            if(bRegresa && cboNivelesDeAtencion.Data == "0")
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado el Nivel de Atención, verifique.");
                cboNivelesDeAtencion.Focus();
            }

            if (bRegresa && cboRegion.Data == "0")
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado la Región, verifique.");
                cboRegion.Focus();
            }

            if (bRegresa && cboSubRegion.Data == "0")
            {
                bRegresa = false;
                General.msjUser("No seleccionado la Sub-Región, verifique.");
                cboSubRegion.Focus();
            }

            if ( bRegresa && cboJurisdicciones.SelectedIndex == 0 )
            {
                bRegresa =false;
                General.msjUser("No ha seleccionado la Jurisdicción, verifique.");
                cboJurisdicciones.Focus();
            }

            return bRegresa;
        }
        #endregion Funciones

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            txtAlm.Enabled = false;
            lblCancelado.Visible = false;

            Consultas.MostrarMsjSiLeerVacio = false;
            CargarEstados();
            CargarRegiones();
            CargarSubRegiones();
            CargarJurisdicciones();
            Consultas.MostrarMsjSiLeerVacio = true;
            cboTipoUnidad.SelectedIndex = 0;
            cboEdo.Focus();
            sRegion = "0";

            Ayuda.MostrarMsjSiLeerVacio = true;

        }
        #endregion Botones

        private void txtIdFar_Validating(object sender, CancelEventArgs e)
        {
            // string sDato = "", sEdo = ""; // sReg = "", sSubReg = "";

            if (txtIdFar.Text.Trim() == "" || txtIdFar.Text.Trim() == "*")
            {
                if (cboEdo.SelectedIndex != 0)
                {
                    txtIdFar.Text = "*";
                    txtIdFar.Enabled = false;
                }
            }
            else
            {
                //sEdo = cboEdo.Data;
                //sDato = string.Format("SELECT * FROM vw_Farmacias (NoLock) WHERE IdFarmacia = '{0}' AND IdEstado = '{1}' ", Fg.PonCeros(txtIdFar.Text, 4), sEdo);

                //if (!leer.Exec(sDato))
                //{
                //    General.Error.GrabarError(leer.Error, con.DatosConexion, "OficinaCentral", "1", this.Name, "", leer.QueryEjecutado);
                //    General.msjError("Error al consultar la Farmacia");
                //}
                //else
                {
                    leer.DataSetClase = Consultas.Farmacias(cboEdo.Data, txtIdFar.Text, "txtIdFar_Validating");
                    if (leer.Leer())
                    {
                        CargarDatosFarmacia();
                    }
                    else
                    {
                        General.msjUser("Farmacia no encontrada, verifique.");
                        txtIdFar.Text = "";
                        txtNomFar.Text = "";
                        txtIdFar.Focus();
                    }
                }
            }
        }

        private void CargarDatosFarmacia()
        {
            txtIdFar.Enabled = false;
            cboEdo.Enabled = false;
            txtIdFar.Text = leer.Campo("IdFarmacia");
            txtNomFar.Text = leer.Campo("Farmacia");
            txtEmail.Text = leer.Campo("eMail"); 

            chkEsDeConsignacion.Checked = leer.CampoBool("EsDeConsignacion");
            chkEsUnidosis.Checked = leer.CampoBool("EsUnidosis");


            cbManCon.Checked = leer.CampoBool("ManejaControlados");
            chkVtaPublico.Checked = leer.CampoBool("ManejaVtaPubGral");
            cbAlm.Checked = leer.CampoBool("EsAlmacen");
            cbFront.Checked = leer.CampoBool("EsFrontera");
            txtAlm.Text = leer.Campo("IdAlmacen");
            chkHabilitadaParaTransferencias.Checked = leer.CampoBool("Transferencia_RecepcionHabilitada");


            txtCLUES.Text = leer.Campo("CLUES");
            txtNombrePropio.Text = leer.Campo("NombrePropio_UMedica");

            cboTipoUnidad.Data = leer.Campo("IdTipoUnidad");
            cboNivelesDeAtencion.Data = leer.Campo("IdNivelDeAtencion");

            cboRegion.Data = leer.Campo("IdRegion");
            sRegion = cboRegion.Data;
            cboSubRegion.Data = leer.Campo("IdSubRegion");
            cboJurisdicciones.Data = leer.Campo("IdJurisdiccion");

            txtMun.Text = leer.Campo("IdMunicipio");
            lblMun.Text = leer.Campo("Municipio");
            txtCol.Text = leer.Campo("IdColonia");
            lblCol.Text = leer.Campo("Colonia");

            txtDom.Text = leer.Campo("Domicilio");
            txtCP.Text = leer.Campo("CodigoPostal");
            txtTel.Text = leer.Campo("Telefonos");

            if (leer.Campo("Status") == "C")
            {
                lblCancelado.Visible = true;
                txtNomFar.Enabled = false;
                General.msjUser("La Farmacia actualmente se encuentra cancelada.");
            }
        }

        private void txtIdFar_KeyDown(object sender, KeyEventArgs e)
        {
            string sEdo = "";

            if (e.KeyCode == Keys.F1)
            {
                sEdo = cboEdo.Data;
                leer.DataSetClase = Ayuda.Farmacias("txtIdFar_KeyDown", sEdo);
                if (leer.Leer())
                {
                    CargarDatosFarmacia();
                }
            }
        }

        private void cbAlm_CheckedChanged(object sender, EventArgs e)
        {
            bool bDato = cbAlm.Checked;
            txtAlm.Enabled = bDato;
        }

        private void cbFront_CheckedChanged(object sender, EventArgs e)
        {
            //if (cbFront.Checked)
            //{
            //    cbAlm.Visible = false;
            //}
            //else
            //{
            //    cbAlm.Visible = true;
            //}
        }

        private void txtAlm_Validating(object sender, CancelEventArgs e)
        {
            //if (txtAlm.Text.Trim() == "")
            //{
            //    General.msjError("Favor de capturar el Almacen");
            //    txtAlm.Focus();
            //}
            //else
            //{
            //   // LlenaRegiones();
            //}
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int iEsConsig = 0, iAlm = 0, iFront = 0, iManCon = 0, iManVtaPub = 0, iOpcion = 0, iUni = 0, iTransferencia = 0;
            string sSql = "", sMensaje = "";

            if (ValidaDatos())
            {
                if (chkEsDeConsignacion.Checked)
                    iEsConsig = 1;

                if ( cbAlm.Checked )
                    iAlm = 1;

                if ( cbFront.Checked )
                    iFront = 1;

                if ( cbManCon.Checked )
                    iManCon = 1;

                if (chkVtaPublico.Checked)
                    iManVtaPub = 1;

                iUni =  chkEsUnidosis.Checked ? 1:0;
                iTransferencia = chkHabilitadaParaTransferencias.Checked ? 1 : 0;


                if (!con.Abrir())
                {
                    General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
                }
                else 
                {
                    iOpcion = 1;
                    con.IniciarTransaccion();

                    sSql = String.Format("EXEC spp_Mtto_CatFarmacias \n" + // '{0}', '{1}', '{2}', {3}, '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}' ",
                            " \t@IdEstado = '{0}', @IdFarmacia = '{1}', @Descripcion = '{2}', @EsConsignacion = '{3}', @ManVtaPubGral = '{4}', \n" +
                            " \t@ManejaCon = '{5}', @IdJurisdiccion = '{6}', @IdRegion = '{7}', @IdSubRegion = '{8}', @EsAlm = '{9}', @IdAlm = '{10}', \n" +
                            " \t@EsFront = '{11}', @IdMun = '{12}', @IdCol = '{13}', @Dom = '{14}', @CP = '{15}', @Tel = '{16}', @eMail = '{17}', \n" +
                            " \t@IdTipoUnidad = '{18}', @iOpcion = '{19}', @EsUnidosis = '{20}', @CLUES = '{21}', @NombrePropio_UMedica = '{22}', \n" +
                            " \t@Transferencia_RecepcionHabilitada = '{23}', @IdNivelDeAtencion = '{24}' \n",  
                            cboEdo.Data, txtIdFar.Text, txtNomFar.Text, iEsConsig, iManVtaPub, iManCon, 
                            cboJurisdicciones.Data, cboRegion.Data, cboSubRegion.Data, iAlm, txtAlm.Text, iFront,
                            txtMun.Text, txtCol.Text, txtDom.Text, txtCP.Text, txtTel.Text, txtEmail.Text, cboTipoUnidad.Data, iOpcion, iUni, 
                            txtCLUES.Text.Trim(), txtNombrePropio.Text.Trim(), iTransferencia, cboNivelesDeAtencion.Data 
                            ); 
                    if (leer.Exec(sSql))
                    {
                        if (leer.Leer())
                        {
                            sMensaje = leer.Campo("Mensaje");
                        }

                        con.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        con.DeshacerTransaccion();
                        General.Error.GrabarError(leer.Error, con.DatosConexion, "OficinaCentral", "1", this.Name, "", leer.QueryEjecutado);
                        General.msjError("Ocurrió un error al guardar la información.");
                        
                    }

                    con.Cerrar();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            int iEsConsig = 0, iAlm = 0, iFront = 0, iManCon = 0, iManVtaPub = 0, iOpcion = 0, iUni = 0;
            string sSql = "", sMensaje = "";

            if (ValidaDatos())
            {
                if (chkEsDeConsignacion.Checked)
                    iEsConsig = 1;

                if (cbAlm.Checked)
                    iAlm = 1;

                if (cbFront.Checked)
                    iFront = 1;

                if (cbManCon.Checked)
                    iManCon = 1;

                if (chkVtaPublico.Checked)
                    iManVtaPub = 0;

                iUni = chkEsUnidosis.Checked ? 1 : 0;

                if(!con.Abrir())
                {
                    General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
                }
                else 
                {
                    iOpcion = 2;
                    con.IniciarTransaccion();

                    sSql = String.Format("EXEC spp_Mtto_CatFarmacias \n" + // '{0}', '{1}', '{2}', {3}, '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}' ",
                            " \t@IdEstado = '{0}', @IdFarmacia = '{1}', @Descripcion = '{2}', @EsConsignacion = '{3}', @ManVtaPubGral = '{4}', \n" +
                            " \t@ManejaCon = '{5}', @IdJurisdiccion = '{6}', @IdRegion = '{7}', @IdSubRegion = '{8}', @EsAlm = '{9}', @IdAlm = '{10}', \n" +
                            " \t@EsFront = '{11}', @IdMun = '{12}', @IdCol = '{13}', @Dom = '{14}', @CP = '{15}', @Tel = '{16}', @eMail = '{17}', \n" +
                            " \t@IdTipoUnidad = '{18}', @iOpcion = '{19}', @EsUnidosis = '{20}', @CLUES = '{21}', @NombrePropio_UMedica = '{22}' \n",
                            cboEdo.Data, txtIdFar.Text, txtNomFar.Text, iEsConsig, iManVtaPub, iManCon,
                            cboJurisdicciones.Data, cboRegion.Data, cboSubRegion.Data, iAlm, txtAlm.Text, iFront,
                            txtMun.Text, txtCol.Text, txtDom.Text, txtCP.Text, txtTel.Text, txtEmail.Text, cboTipoUnidad.Data, iOpcion, iUni,
                            txtCLUES.Text.Trim(), txtNombrePropio.Text.Trim());
                    if (leer.Exec(sSql))
                    {
                        if(leer.Leer())
                        {
                            sMensaje = String.Format("{0}", leer.Campo("Mensaje"));
                        }

                        con.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        con.DeshacerTransaccion();
                        General.Error.GrabarError(leer.Error, con.DatosConexion, "OficinaCentral", "1", this.Name, "", leer.QueryEjecutado);
                        General.msjError("Ocurrió un error al Eliminar la información.");
                        //btnNuevo_Click(null, null);

                    }

                    con.Cerrar();
                }
            }
        }

        private void btnFarmaciasRelacionadas_Click(object sender, EventArgs e)
        {
            FrmFarmaciasRelacionadas f = new FrmFarmaciasRelacionadas();
            f.ShowInTaskbar = false;
            f.ShowDialog(this); 
        }

        private void btnAlmacenesPermisosEspeciales_Click( object sender, EventArgs e )
        {
            FrmAlmacensPermisosEspeciales f = new FrmAlmacensPermisosEspeciales();
            f.ShowInTaskbar = false;
            f.ShowDialog(this);
        }

        private void btnCFDIPermisosEspeciales_Click( object sender, EventArgs e )
        {
            FrmCFDI_PermisosEspeciales f = new FrmCFDI_PermisosEspeciales();
            f.ShowInTaskbar = false;
            f.ShowDialog(this);
        }

        private void cbFront_Validating(object sender, CancelEventArgs e)
        {            
            //if (txtIdFar.Text.Trim() == "*")
            //{
            //    LlenaRegiones();
            //}            
        }

        private void cboEdo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEdo.SelectedIndex != 0)
            {
                cboJurisdicciones.Clear();
                cboJurisdicciones.Add("0", "<< Seleccione >>");
                try
                {
                    cboJurisdicciones.Add(dtsJurisdicciones.Tables[0].Select(string.Format("IdEstado = '{0}'", cboEdo.Data)), true, "IdJurisdiccion", "NombreJurisdiccion");
                }
                catch { }
                cboJurisdicciones.SelectedIndex = 0;
            }
        }

        private void cboRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboRegion.SelectedIndex != 0)
            {
                cboSubRegion.Clear();
                cboSubRegion.Add("0", "<< Seleccione >>");
                try
                {
                    cboSubRegion.Add(dtsSubRegiones.Tables[0].Select(string.Format("IdRegion = '{0}'", cboRegion.Data)), true, "IdSubRegion", "Descripcion");
                }
                catch { }
                cboSubRegion.SelectedIndex = 0;
            }
        }

        private void txtMun_Validating(object sender, CancelEventArgs e)
        {
            if (txtMun.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Municipios(cboEdo.Data, txtMun.Text, "txtMun_Validating");
                if (leer.Leer())
                {
                    txtMun.Text = leer.Campo("IdMunicipio");
                    lblMun.Text = leer.Campo("Descripcion");
                }
                else
                {
                    txtMun.Text = "";
                    lblMun.Text = "";
                    e.Cancel = true;
                }
            }
            else
            {
                lblMun.Text = "";
            }
        }

        private void txtCol_Validating(object sender, CancelEventArgs e)
        {
            if (txtCol.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Colonias(cboEdo.Data, txtMun.Text, txtCol.Text, "txtMun_Validating");
                if (leer.Leer())
                {
                    txtCol.Text = leer.Campo("IdColonia");
                    lblCol.Text = leer.Campo("Descripcion");
                }
                else
                {
                    txtCol.Text = "";
                    lblCol.Text = "";
                    e.Cancel = true;
                }
            }
            else
            {
                lblCol.Text = ""; 
            }
        }

        private void txtMun_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Municipios("txtMun_KeyDown", cboEdo.Data);
                if (leer.Leer())
                {
                    txtMun.Text = leer.Campo("IdMunicipio");
                    lblMun.Text = leer.Campo("Descripcion");
                }
            }
        }

        private void txtCol_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Colonias("txtCol_KeyDown", cboEdo.Data, txtMun.Text);
                if (leer.Leer())
                {
                    txtCol.Text = leer.Campo("IdColonia");
                    lblCol.Text = leer.Campo("Descripcion");
                }
            }
        }


    }
}