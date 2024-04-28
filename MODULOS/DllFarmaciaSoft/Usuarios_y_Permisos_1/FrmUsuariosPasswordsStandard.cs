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
using SC_SolutionsSystem.Errores;

using SC_SolutionsSystem.ExportarDatos;
using DllFarmaciaSoft.ExportarExcel;


namespace DllFarmaciaSoft.Usuarios_y_Permisos
{
    public partial class FrmUsuariosPasswordsStandard : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerUsuarios;
        clsCriptografo crypto = new clsCriptografo();

        clsConsultas query; // = new clsConsultas(General.DatosConexion, "Configuracion", "FrmPersonal", Application.ProductVersion, true);
        clsAyudas Ayuda;

        DataSet dtsFarmacias;
        clsGenerarExcel excel; 

        public string IdEstado = "";
        public string IdFarmacia = "";
        public string Estado = "";
        public string Farmacia = "";
        public string IdPersonal = "";
        int iNumUsuarios = 0;
        private bool bEsDesarrollo = General.EsEquipoDeDesarrollo; 

        public FrmUsuariosPasswordsStandard()
        {
            InitializeComponent();
            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, "Configuracion", this.Name, Application.ProductVersion, true);
            Ayuda = new clsAyudas(General.DatosConexion, "Configuracion", this.Name, Application.ProductVersion);
            Error = new clsGrabarError(General.DatosConexion, General.DatosApp, this.Name); 
        }

        private void FrmUsuariosPasswordsStandard_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null); 
            ObtenerEstados();
            ObtenerFarmacias();


            if (!bEsDesarrollo)
            {
                btnGenerarPassword.Visible = false;
                btnExportarExcel.Visible = false;
                toolStripSeparator_03.Visible = false; 
            }
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            btnEjecutar.Enabled = true; 
            btnGenerarPassword.Enabled = false;
            btnExportarExcel.Enabled = false;
            rdoSinPassword.Checked = true;
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            string sSQL = "";
            string sPassWord = rdoSinPassword.Checked ? " And Len(Password) = 0 " : "";

            sSQL = string.Format("Select \n"+
                "\tIdEstado, Estado, IdFarmacia, Farmacia, IdPersonal, NombreCompleto, LoginUser, Status \n" +
                "From vw_Personal U (NoLock) \n" +
                "Where IdEstado = '{0}' and Status = 'A' {1} \n" +
                "Order by IdEstado, IdFarmacia, IdPersonal \n",
                cboEstados.Data, sPassWord);

            lblTotalUsuarios.Text = "0";
            lblAvance.Text = "0 / 0";
            leerUsuarios = new clsLeer();
            iNumUsuarios = 0; 

            if (cboFarmacias.SelectedIndex != 0)
            {
                sSQL = string.Format("Select \n" + 
                    "\tIdEstado, Estado, IdFarmacia, Farmacia, IdPersonal, NombreCompleto, LoginUser, Status \n" +
                    "From vw_Personal U (NoLock) \n" +
                    "Where IdEstado = '{0}' and IdFarmacia = '{1}' and Status = 'A' {2} \n" +
                    "Order by IdEstado, IdFarmacia, IdPersonal \n",
                    cboEstados.Data, cboFarmacias.Data, sPassWord); 
            }

            if (!leer.Exec(sSQL))
            {
                Error.GrabarError(leer, "btnEjecutar_Click");
                General.msjError("Ocurrió un error al obtener la lista de Usuarios"); 
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjAviso("No se encontrarón Usuarios para el Estado-Farmacia."); 
                }
                else
                {
                    cboEstados.Enabled = false;
                    cboFarmacias.Enabled = false; 

                    btnEjecutar.Enabled = false;
                    btnGenerarPassword.Enabled = true;
                    btnExportarExcel.Enabled = true;
                    iNumUsuarios = leer.Registros; 
                    lblTotalUsuarios.Text = leer.Registros.ToString();
                    lblAvance.Text = string.Format("0 / {0}", lblTotalUsuarios.Text);

                    leerUsuarios.DataSetClase = leer.DataSetClase; 
                }
            }
        }

        private void btnGenerarPassword_Click(object sender, EventArgs e)
        {
            bool bExito = true; 
            string sSql = "";
            string sCadena = "";
            string sPass = "";
            string sIdEstado = "";
            string sIdFarmacia = "";
            string sIdPersonal = "";
            string sLogin = "";
            int iUsers = 0;
            string sFechaExec = "";
            DateTime dtPass = DateTime.Now; 

            if (!cnn.Abrir())
            {
                General.msjErrorAlAbrirConexion(); 
            }
            else 
            {
                cnn.IniciarTransaccion();
                sFechaExec = General.FechaYMD(dtPass, "");
                sFechaExec += General.Hora(dtPass, ""); 

                while (leerUsuarios.Leer())
                {
                    dtPass = DateTime.Now; 
                    sFechaExec = General.FechaYMD(dtPass, "");
                    sFechaExec += General.Hora(dtPass, ""); 

                    sIdEstado = leerUsuarios.Campo("IdEstado");
                    sIdFarmacia = leerUsuarios.Campo("IdFarmacia");
                    sIdPersonal = leerUsuarios.Campo("IdPersonal");
                    sLogin = leerUsuarios.Campo("LoginUser");
                    ////sLogin = Fg.Mid(sLogin, 1, sLogin.Length - 1);

                    sCadena = sIdEstado + sIdFarmacia + sIdPersonal;
                    sCadena += Fg.Mid(clsMD5.GenerarMD5(sCadena + sFechaExec), 1, 8).ToUpper(); 
                    sPass = crypto.PasswordEncriptar(sCadena);
                    sFechaExec = ""; 

                    sSql = string.Format("Exec spp_Net_Usuarios_Mtto \n" + 
                        "\t@IdEstado = '{0}', @IdSucursal = '{1}', @IdPersonal = '{2}', @LoginUser = '{3}', @Password = '{4}', @iTipo = '{5}' ", 
                        sIdEstado, sIdFarmacia, sIdPersonal, sLogin, sPass, 1);


                    lblFarmacia.Text = leerUsuarios.Campo("IdFarmacia") + " -- " + leerUsuarios.Campo("Farmacia");
                    lblNombreUsuario.Text = leerUsuarios.Campo("NombreCompleto"); 

                    if (!leer.Exec(sSql))
                    {
                        bExito = false;
                        break; 
                    }

                    ///// Avance   
                    iUsers++;
                    lblAvance.Text = string.Format("{0} / {1}", iUsers, iNumUsuarios);
                    this.Refresh(); 
                }

                if (!bExito)
                {
                    Error.GrabarError(leer.Error, "btnGenerarPassword_Click");
                    cnn.DeshacerTransaccion();
                    General.msjError("Ocurrió un error al actualizar los Passwords."); 
                }
                else
                {
                    cnn.CompletarTransaccion();
                    General.msjUser("Passwords actualizados satisfactoriamente.");
                    btnEjecutar.Enabled = true;
                    btnGenerarPassword.Enabled = false;
                    btnExportarExcel.Enabled = false;

                    lblFarmacia.Text = "";
                    lblNombreUsuario.Text = ""; 
                }

                cnn.Cerrar(); 
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            string sPassword = ""; 
            string sSQL = string.Format("Select U.IdEstado, U.Estado, U.IdFarmacia, U.Farmacia, U.IdPersonal, U.NombreCompleto, U.LoginUser, P.Password " +
                " From vw_Personal U (NoLock) " +
                " Inner Join Net_Usuarios P (NoLock) On ( U.IdEstado = P.IdEstado and U.IdFarmacia = P.IdSucursal and U.IdPersonal = P.IdPersonal ) " + 
                " Where U.IdEstado = '{0}' and U.Status = 'A' Order by U.IdEstado, U.IdFarmacia, U.IdPersonal ",
                cboEstados.Data);
            int iLargo = 0;
            int iUsers = 0; 

            lblTotalUsuarios.Text = "0";
            lblAvance.Text = "0 / 0";
            leerUsuarios = new clsLeer();
            iNumUsuarios = 0;

            if(cboFarmacias.SelectedIndex != 0)
            {
                sSQL = string.Format("Select U.IdEstado, U.Estado, U.IdFarmacia, U.Farmacia, U.IdPersonal, U.NombreCompleto, U.LoginUser, P.Password " +
                    " From vw_Personal U (NoLock) " +
                    " Inner Join Net_Usuarios P (NoLock) On ( U.IdEstado = P.IdEstado and U.IdFarmacia = P.IdSucursal and U.IdPersonal = P.IdPersonal ) " +
                    " Where U.IdEstado = '{0}' and U.IdFarmacia = '{1}' and U.Status = 'A' Order by U.IdEstado, U.IdFarmacia, U.IdPersonal ",
                    cboEstados.Data, cboFarmacias.Data);
            }
            else
            {
                if(txtFarmaciaInicial.Text.Trim() != "" && txtFarmaciaFinal.Text.Trim() != "")
                {
                    sSQL = string.Format(
                        "Select U.IdEstado, U.Estado, U.IdFarmacia, U.Farmacia, U.IdPersonal, U.NombreCompleto, U.LoginUser, P.Password \n" +
                        "From vw_Personal U (NoLock) \n" +
                        "Inner Join Net_Usuarios P (NoLock) On ( U.IdEstado = P.IdEstado and U.IdFarmacia = P.IdSucursal and U.IdPersonal = P.IdPersonal ) \n" +
                        "Where U.IdEstado = '{0}' and ( U.IdFarmacia Between '{1}' and '{2}' ) and U.Status = 'A' \n" +
                        "Order by U.IdEstado, U.IdFarmacia, U.IdPersonal \n", cboEstados.Data, Fg.PonCeros(txtFarmaciaInicial.Text.Trim(), 4), Fg.PonCeros(txtFarmaciaFinal.Text.Trim(), 4)); 
                }
                else
                {
                    if(txtFarmaciaInicial.Text.Trim() != "")
                    {
                        sSQL = string.Format(
                            "Select U.IdEstado, U.Estado, U.IdFarmacia, U.Farmacia, U.IdPersonal, U.NombreCompleto, U.LoginUser, P.Password \n" +
                            "From vw_Personal U (NoLock) \n" +
                            "Inner Join Net_Usuarios P (NoLock) On ( U.IdEstado = P.IdEstado and U.IdFarmacia = P.IdSucursal and U.IdPersonal = P.IdPersonal ) \n" +
                            "Where U.IdEstado = '{0}' and U.IdFarmacia >= '{1}' and U.Status = 'A' \n" +
                            "Order by U.IdEstado, U.IdFarmacia, U.IdPersonal \n", cboEstados.Data, Fg.PonCeros(txtFarmaciaInicial.Text.Trim(), 4));
                    }
                    else
                    {
                        sSQL = string.Format(
                            "Select U.IdEstado, U.Estado, U.IdFarmacia, U.Farmacia, U.IdPersonal, U.NombreCompleto, U.LoginUser, P.Password \n" +
                            "From vw_Personal U (NoLock) \n" +
                            "Inner Join Net_Usuarios P (NoLock) On ( U.IdEstado = P.IdEstado and U.IdFarmacia = P.IdSucursal and U.IdPersonal = P.IdPersonal ) \n" +
                            "Where U.IdEstado = '{0}' and U.IdFarmacia <= '{1}' and U.Status = 'A' \n" +
                            "Order by U.IdEstado, U.IdFarmacia, U.IdPersonal \n", cboEstados.Data, Fg.PonCeros(txtFarmaciaFinal.Text.Trim(), 4));
                    }
                }
            }

            if (!leer.Exec(sSQL))
            {
                Error.GrabarError(leer, "btnEjecutar_Click");
                General.msjError("Ocurrió un error al obtener la lista de Usuarios");
            }
            else
            {
                if (leer.Leer())
                {
                    ////leerUsuarios.DataSetClase = leer.DataSetClase;

                    iNumUsuarios = leer.Registros; 
                    lblTotalUsuarios.Text = leer.Registros.ToString();
                    lblAvance.Text = string.Format("0 / {0}", lblTotalUsuarios.Text);

                    leer.RegistroActual = 1; 
                    while (leer.Leer())
                    {
                        iUsers++;
                        iLargo = leer.Campo("IdEstado").Length + leer.Campo("IdFarmacia").Length + leer.Campo("IdPersonal").Length;

                        try
                        {
                            sPassword = crypto.PasswordDesencriptar(leer.Campo("Password")).Substring(10);
                        }
                        catch 
                        {
                            sPassword = ""; 
                        }
                        
                        leer.GuardarDatos(iUsers, "Password", sPassword);

                        ///// Avance   
                        lblAvance.Text = string.Format("{0} / {1}", iUsers, iNumUsuarios);
                    }

                    leer.RegistroActual = 1;

                    //excel = new clsExportarExcel();
                    //excel.InicioRenglonEncabezado = 1;
                    //excel.InicioColumnaEncabezado = 2;
                    //excel.FormatoDeExcel = FormatoExcel.XLS; 
                    //excel.Exportar(leer.DataTableClase, System.IO.Path.Combine(Application.StartupPath, "Usuarios"));
                    //excel.AbrirDocumentoGenerado();

                    string sNombreHoja = "Usuarios";
                    string sNombreDocumento = "";
                    int iRow = 2;
                    int iCol = 2;

                    excel = new clsGenerarExcel();
                    excel.RutaArchivo = @"C:\\Excel";
                    excel.NombreArchivo = sNombreDocumento;
                    excel.AgregarMarcaDeTiempo = true;

                    if(excel.PrepararPlantilla(sNombreDocumento))
                    {
                        excel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                        //excel.LlenarDetalleHorizontal(sNombreHoja, iRenglon, iColBase, leer.DataSetClase); 
                        excel.InsertarTabla(sNombreHoja, iRow, iCol, leer.DataSetClase);

                        //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                        excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);

                        excel.CerraArchivo();

                        excel.AbrirDocumentoGenerado(true);
                    }


                }
            }
        } 
        #endregion Botones

        private void btnGuardar_Click(object sender, EventArgs e)
        {   
            GuardarInformacion(1);
        }

        private void GuardarInformacion(int Tipo)
        {
            if (cnn.Abrir())
            {
                cnn.IniciarTransaccion();

                string sCadena = ""; //  IdEstado + IdFarmacia + txtIdPersonal.Text + txtPassword.Text.ToUpper();
                string sPass = crypto.PasswordEncriptar(sCadena);
                
                string sSql = string.Format("Exec spp_Net_Usuarios_Mtto '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ", IdEstado, IdFarmacia, "txtIdPersonal.Text", "txtLogin.Text", sPass, Tipo.ToString());

                if (!leer.Exec(sSql))
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(leer, "GuardarInformacion()");
                    General.msjError("Ocurrió un error al grabar el usuario.");
                }
                else
                {
                    cnn.CompletarTransaccion();
                    General.msjUser("Información guardada satisfactoriamente.");
                    btnNuevo_Click(null, null);
                }

                cnn.Cerrar();
            }
            else
                General.msjAviso("No se pudo conectar con el servidor, intente de nuevo.");
        }

        #region Funciones y Procedimientos Privados 
        private void ObtenerEstados()
        {
            cboEstados.Clear();
            cboEstados.Add();

            try
            {
                cboEstados.Add(query.EstadosConFarmacias("CargarEstados()"), true, "IdEstado", "NombreEstado");
            }
            catch { }

            cboEstados.SelectedIndex = 0; 
        }

        private void ObtenerFarmacias()
        {
            dtsFarmacias = new DataSet(); 
            cboFarmacias.Clear();
            cboFarmacias.Add();

            try
            {
                dtsFarmacias = query.Farmacias("CargarFarmacias()");
                // cboFarmacias.Add(query.Farmacias(""), true, IdEstado, Estado);
            }
            catch { }

            cboFarmacias.SelectedIndex = 0; 
        }
        #endregion Funciones y Procedimientos Privados

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboFarmacias.Clear();
            cboFarmacias.Add();

            if (cboEstados.SelectedIndex != 0)
            {
                // dtsFarmacias = query.Farmacias("CargarFarmacias()");
                cboFarmacias.Filtro = string.Format("IdEstado = '{0}'", cboEstados.Data);
                cboFarmacias.Add(dtsFarmacias, true, "IdFarmacia", "NombreFarmacia");
            }
            cboFarmacias.SelectedIndex = 0; 
        }
    }
}
