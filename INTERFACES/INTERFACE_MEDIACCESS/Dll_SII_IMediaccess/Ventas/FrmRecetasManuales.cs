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
using DllFarmaciaSoft.Devoluciones;
using DllFarmaciaSoft.LimitesConsumoClaves;
using DllFarmaciaSoft.Usuarios_y_Permisos;

using Farmacia.Procesos;
using Farmacia.Vales;
using Farmacia.Ventas;

using Dll_SII_IMediaccess;
using Dll_SII_IMediaccess.Validaciones_MA; 

namespace Dll_SII_IMediaccess.Ventas
{
    public partial class FrmRecetasManuales : FrmBaseExt 
    {
        private enum Cols
        {
            Ninguna = 0,
            CodEAN, Codigo, ClaveSSA, Descripcion, TasaIva, Cantidad
        }

        clsDatosCliente DatosCliente;
        clsConexionSQL con = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid myGrid;


        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        DllFarmaciaSoft.clsAyudas Ayuda; // = new clsAyudas();
        DataSet dtsCargarDatos = new DataSet();
        DllFarmaciaSoft.clsConsultas Consultas;

        public clsValidar_Elegibilidad localElegibilidad;
        public bool RecetaManualGuardada = false; 
        private bool bExterna = false; 

        clsCodigoEAN EAN = new clsCodigoEAN();
        string sCodigoEAN_Seleccionado = "";
        FrmRevisarCodigosEAN RevCodigosEAN = new FrmRevisarCodigosEAN();
        Cols ColActiva = Cols.Ninguna;
        string sValorGrid = "";

        public FrmRecetasManuales()
        {
            InitializeComponent();
            RecetaManualGuardada = true;
            con.SetConnectionString();
            leer = new clsLeer(ref con);

            Ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Consultas = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, false);
            Error = new clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            grdProductos.EditModeReplace = true;
            myGrid.BackColorColsBlk = Color.White;

            this.Text = "Modificación de recetas manuales";
        }

        public FrmRecetasManuales(clsValidar_Elegibilidad Elegibilidad)
        {
            InitializeComponent();

            localElegibilidad = Elegibilidad;
            bExterna = true;

            con.SetConnectionString();
            leer = new clsLeer(ref con);

            Ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Consultas = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, false);
            Error = new clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            grdProductos.EditModeReplace = true;
            myGrid.BackColorColsBlk = Color.White;
        }

        private void FrmRecetasManuales_Load(object sender, EventArgs e)
        {
            InicializarPantalla(); 
        }

        #region Botones 
        private void InicializarPantalla()
        {
            myGrid.Limpiar(true);
            Fg.IniciaControles(true);

            if (!RecetaManualGuardada)
            {
                RecetaManualGuardada = false;


                txtFolioElegibilidad.Enabled = false;
                txtFolioReceta.Enabled = false;

                txtFolioElegibilidad.Text = localElegibilidad.Elegibilidad;
                txtFolioReceta.Text = localElegibilidad.MA_FolioDeReceta;
                txtConsecutivo.Text = localElegibilidad.MA_FolioDeConsecutivo;

                txtConsecutivo.Text = txtConsecutivo.Text == "" ? "*":txtConsecutivo.Text;
                txtConsecutivo.Enabled = false;

                lblNombreBeneficiario.Text = localElegibilidad.NombreBeneficiario;
                lblFolioReferencia.Text = localElegibilidad.ReferenciaBeneficiario;
            }

            dtpFechaDeReceta.MaxDate = dtpFechaDeReceta.Value;
            dtpFechaDeReceta.MinDate = dtpFechaDeReceta.MaxDate.AddDays(-20); 
            dtpFechaDeReceta.Focus();
            dtpFechaDeReceta.Select();

            if (!bExterna)
            {
                txtEspecialidad.Focus(); 
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarPantalla(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                GuardarInformacion(); 
            }
        }

        private bool GuardarInformacion()
        {
            bool bRegresa = false;
            string sSql = "";
            int iPartida = 0;
            string sIdProducto = "";
            int iCantidad = 0; 

            //RecetaManualGuardada = false;
            if (!con.Abrir())
            {
                General.msjErrorAlAbrirConexion();
            }
            else
            {
                con.IniciarTransaccion();

                bRegresa = true; 
                if (RecetaManualGuardada)
                {
                    bRegresa = GuardarDetalladoAuditoria();
                }

                sSql = string.Format("Exec spp_INT_MA__RecetasElectronicas_001_Encabezado " +
                   " @Folio_MA = '{0}', @IdFarmacia = '{1}', @NombrePaciente = '{2}', @NombreMedico = '{3}', @Especialidad = '{4}', " +
                   " @Copago = '{5}', @PlanBeneficiario = '{6}', @FechaEmision = '{7}', @Elegibilidad = '{8}', " +
                   " @CIE_01 = '{9}', @CIE_02 = '{10}', @CIE_03 = '{11}', @CIE_04 = '{12}', " +
                   " @EsRecetaManual = '{13}', @Consecutivo = '{14}' ",
                   txtFolioReceta.Text, localElegibilidad.IdClinica, lblNombreBeneficiario.Text, 
                   ////txtNombreMedico.Text.Trim(), 
                   lblMedico.Text.Trim(), 
                   txtEspecialidad.Text.Trim(), 
                   localElegibilidad.Copago, txtPlanBeneficiario.Text.Trim(), General.FechaYMD(dtpFechaDeReceta.Value), localElegibilidad.Elegibilidad, 
                   txtIdDiagnostico_01.Text.Trim(), txtIdDiagnostico_02.Text.Trim(), txtIdDiagnostico_03.Text.Trim(), txtIdDiagnostico_04.Text.Trim(),
                   1, txtConsecutivo.Text.Trim()
                   );

                if (bRegresa)
                {
                    if (leer.Exec(sSql))
                    {
                        if (leer.Leer())
                        {
                            txtConsecutivo.Text = leer.Campo("Consecutivo");
                            localElegibilidad.MA_FolioDeConsecutivo = leer.Campo("Consecutivo");
                        }

                        for (int i = 1; i <= myGrid.Rows; i++)
                        {
                            iPartida++;
                            sIdProducto = myGrid.GetValue(i, (int)Cols.Codigo);
                            iCantidad = myGrid.GetValueInt(i, (int)Cols.Cantidad);

                            sSql = string.Format("Exec spp_INT_MA__RecetasElectronicas_002_Productos " +
                                " @Folio_MA = '{0}', @Partida = '{1}', @CodigoEAN = '{2}', @CantidadSolicitada = '{3}', @Consecutivo = '{4}'",
                                txtFolioReceta.Text, iPartida, sIdProducto, iCantidad, txtConsecutivo.Text.Trim());

                            if (!leer.Exec(sSql))
                            {
                                bRegresa = false;
                                break;
                            }
                        }
                    }
                    else
                     {
                         bRegresa = false;
                     }
                }


                if (!bRegresa)
                {
                    Error.GrabarError(leer, "");
                    con.DeshacerTransaccion();
                    General.msjError("Ocurrió un error al guardar la información de la receta manual.");
                }
                else
                {
                    con.CompletarTransaccion();
                    RecetaManualGuardada = true;

                    if (!bExterna)
                    {
                        General.msjUser("Información de receta manual actualizada satisfactoriamente.");
                    }
                    else
                    {
                        General.msjUser("Información de receta manual guardada satisfactoriamente.");
                    }
                }

                con.Cerrar(); 
            }

            if (bRegresa && bExterna)
            {
                this.Hide(); 
            }

            if (bRegresa && RecetaManualGuardada && !bExterna)
            {
                InicializarPantalla();
            }

            return bRegresa; 
        }
        #endregion Botones 

        #region Guardar informacion 
        private bool validarDatos()
        {
            bool bRegresa = true;

            if (bRegresa)
            {
                bRegresa = ValidarModificacionPrevia();
            }

            if (bRegresa && txtPlanBeneficiario.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el Plan del Beneficiario, verifique.");
                txtPlanBeneficiario.Focus();
            }

            if (bRegresa && txtNombreMedico.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el Nombre del Médico, verifique.");
                txtNombreMedico.Focus();
            }

            if (bRegresa && txtEspecialidad.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado la Especialidad, verifique.");
                txtEspecialidad.Focus();
            }

            if (bRegresa && txtIdDiagnostico_01.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Debe capturar al menos el primer Diagnóstico CIE10, verifique.");
                txtIdDiagnostico_01.Focus();
            }

            BorrarRenglonesVacios();

            if (bRegresa && myGrid.GetValue(1, (int)Cols.CodEAN) == "")
            {
                bRegresa = false;
                General.msjUser("Debe capturar al menos un producto, verifique.");
            }

            return bRegresa; 
        }

        private void BorrarRenglonesVacios()
        {
            for (int i = 1; i <= myGrid.Rows; i++)
            {
                if (myGrid.GetValue(i, (int)Cols.CodEAN) == "" || myGrid.GetValueInt(i, (int)Cols.Cantidad) == 0)
                {
                    myGrid.DeleteRow(i);
                }
            }

            if (myGrid.Rows == 0)
            {
                myGrid.Rows = 1;
            }
        }


        private bool GuardarDetalladoAuditoria()
        {
            bool bRegresa = false;

            string sSql = string.Format("Exec spp_INT_MA__ADT_RecetasElectronicas '{0}', '{1}', '{2}', '{3}'",
                txtFolioReceta.Text, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal);

            bRegresa = leer.Exec(sSql);

            return bRegresa;
        }

        private bool ValidarModificacionPrevia()
        {
            bool bRegresa = true;

            string sSql = string.Format("Select COUNT(*) As Registros From INT_MA__ADT_RecetasElectronicas_001_Encabezado Where Elegibilidad = '{0}'",
               txtFolioElegibilidad.Text);

            if (!DtGeneral.EsAdministrador)
            {
                bRegresa = false;
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "ValidarModificacionPrevia()");
                    General.msjError("Ocurrió un error al validar si existen modificaciones previas.");
                }
                else
                {
                    leer.Leer();
                    if (leer.CampoInt("Registros") != 0)
                    {
                        General.msjUser("Receta con modificación previa, verifique.");
                    }
                    else
                    {
                        bRegresa = true;
                    }
                }
            }

            return bRegresa;
        }


        #endregion Guardar informacion

        #region Diagnosticos 
        private void txtIdDiagnostico_01_Validating(object sender, CancelEventArgs e)
        {
            Validar_Diagnostico(txtIdDiagnostico_01, lblDiagnostico_01); 
        }

        private void txtIdDiagnostico_01_TextChanged(object sender, EventArgs e)
        {
            lblDiagnostico_01.Text = ""; 
        }

        private void txtIdDiagnostico_01_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                Ayuda_Diagnostico(txtIdDiagnostico_01, lblDiagnostico_01);
            }
        }

        private void txtIdDiagnostico_02_Validating(object sender, CancelEventArgs e)
        {
            Validar_Diagnostico(txtIdDiagnostico_02, lblDiagnostico_02);
        }

        private void txtIdDiagnostico_02_TextChanged(object sender, EventArgs e)
        {
            lblDiagnostico_02.Text = "";
        }

        private void txtIdDiagnostico_02_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                Ayuda_Diagnostico(txtIdDiagnostico_02, lblDiagnostico_02);
            }
        }

        private void txtIdDiagnostico_03_Validating(object sender, CancelEventArgs e)
        {
            Validar_Diagnostico(txtIdDiagnostico_03, lblDiagnostico_03);
        }

        private void txtIdDiagnostico_03_TextChanged(object sender, EventArgs e)
        {
            lblDiagnostico_03.Text = "";
        }

        private void txtIdDiagnostico_03_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                Ayuda_Diagnostico(txtIdDiagnostico_03, lblDiagnostico_03);
            }
        }

        private void txtIdDiagnostico_04_Validating(object sender, CancelEventArgs e)
        {
            Validar_Diagnostico(txtIdDiagnostico_04, lblDiagnostico_04);
        }

        private void txtIdDiagnostico_04_TextChanged(object sender, EventArgs e)
        {
            lblDiagnostico_04.Text = "";
        }

        private void txtIdDiagnostico_04_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                Ayuda_Diagnostico(txtIdDiagnostico_04, lblDiagnostico_04);
            }
        }

        private bool Validar_Diagnostico(TextBox Diagnostico, Label lblDiagnostico)
        {
            bool bRegresa = true; 

            if (Diagnostico.Text.Trim() != "")
            {
                if (Diagnostico.Text.Trim().Length < GnFarmacia.ClaveDiagnosticoCaracteres)
                {
                    bRegresa = false; 
                    General.msjUser(string.Format("Formato de Diagnóstico incorrecto, el diagnóstico debe ser de {0} caracteres, verifique.", GnFarmacia.ClaveDiagnosticoCaracteres));
                }
                else
                {
                    leer.DataSetClase = Consultas.DiagnosticosCIE10(Diagnostico.Text, "Diagnostico_Validating");
                    if (!leer.Leer())
                    {
                        General.msjUser("Clave de Diagnóstico no encontrada, verifique.");
                        bRegresa= false; 
                    }
                    else
                    {
                        Diagnostico.Text = leer.Campo("ClaveDiagnostico");
                        lblDiagnostico.Text = leer.Campo("Descripcion");
                    }
                }
            }

            return bRegresa; 
        }

        private bool Ayuda_Diagnostico(TextBox Diagnostico, Label lblDiagnostico)
        {
            bool bRegresa = false;

            leer.DataSetClase = Ayuda.DiagnosticosCIE10("txtIdDiagnostico_Validating");
            if (leer.Leer())
            {
                bRegresa = true;
                Diagnostico.Text = leer.Campo("ClaveDiagnostico");
                lblDiagnostico.Text = leer.Campo("Descripcion");
            }

            return bRegresa; 
        }
        #endregion Diagnosticos 

        #region Grid 
        private void grdProductos_EditModeOff(object sender, EventArgs e)
        {
            try
            {
                Cols iCol = (Cols)myGrid.ActiveCol;
                switch (iCol)
                {
                    case Cols.CodEAN:
                        ObtenerDatosProducto();
                        break;
                }
            }
            catch (Exception ex)
            {
                General.msjError("01 " + ex.Message);
            }
        }

        private void grdProductos_EditModeOn(object sender, EventArgs e)
        {
            try
            {
                sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);
            }
            catch (Exception ex)
            {
                ////General.msjError("02 " + ex.Message);
            }
        }

        private void grdProductos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            bool bAgregarRenglon = true;


            ////// Jesus.Diaz 2K151029.1410 
            if (bAgregarRenglon)
            {
                if (myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN) != "" && myGrid.GetValue(myGrid.ActiveRow, (int)Cols.Descripcion) != "")
                {
                    myGrid.Rows = myGrid.Rows + 1;
                    myGrid.ActiveRow = myGrid.Rows;
                    myGrid.SetActiveCell(myGrid.Rows, 1);
                    ObtenerDatosProducto();
                }
            }
        }

        private void grdProductos_KeyDown(object sender, KeyEventArgs e)
        {
            ColActiva = (Cols)myGrid.ActiveCol;
            int iRowActivo = myGrid.ActiveRow;

            switch (ColActiva)
            {
                case Cols.CodEAN:
                case Cols.Descripcion:
                case Cols.Cantidad:

                    if (e.KeyCode == Keys.F1)
                    {
                        sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);
                        leer.DataSetClase = Ayuda.ProductosFarmacia(sEmpresa, sEstado, sFarmacia, false, "grdProductos_KeyDown_1");
                        if (leer.Leer())
                        {
                            myGrid.SetValue(myGrid.ActiveRow, 1, leer.Campo("CodigoEAN"));
                            ObtenerDatosProducto();
                            //CargarDatosProducto();
                        }
                    }

                    if (e.KeyCode == Keys.Delete)
                    {
                        try
                        {
                            int iRow = myGrid.ActiveRow;
                            myGrid.DeleteRow(iRow);
                        }
                        catch { }

                        if (myGrid.Rows == 0)
                        {
                            myGrid.Limpiar(true);
                        }
                    }
                    break;
            }
        }

        private bool ValidarSeleccionCodigoEAN(string Codigo)
        {
            bool bRegresa = true;

            sCodigoEAN_Seleccionado = Codigo;

            sCodigoEAN_Seleccionado = RevCodigosEAN.VerificarCodigosEAN(Codigo, false);
            bRegresa = RevCodigosEAN.CodigoSeleccionado;


            return bRegresa;
        }

        private void ObtenerDatosProducto()
        {
            ObtenerDatosProducto(myGrid.ActiveRow, true);
        }

        private void ObtenerDatosProducto(int Renglon, bool BuscarInformacion)
        {
            string sCodigo = "", sSql = "";
            bool bCargarDatosProducto = true;
            string sMsj = "";

            sCodigo = myGrid.GetValue(Renglon, (int)Cols.CodEAN);
            if (EAN.EsValido(sCodigo) && sCodigo != "")
            {
                if (!GnFarmacia.ValidarSeleccionCodigoEAN(sCodigo, ref sCodigoEAN_Seleccionado))
                {
                    myGrid.LimpiarRenglon(Renglon);
                    myGrid.SetActiveCell(Renglon, (int)Cols.CodEAN);
                }
                else
                {
                    sCodigo = sCodigoEAN_Seleccionado;
                    sSql = string.Format("Exec Spp_ProductoVentasFarmacia " +
                        " @Tipo = '{0}', @IdCliente = '{1}', @IdSubCliente = '{2}', @IdCodigo = '{3}', @CodigoEAN = '{4}', " +
                        " @IdEstado = '{5}', @IdFarmacia = '{6}', @EsSectorSalud = '{7}', @EsClienteIMach = '{8}', @ClavesRecetaElectronica = [ {9} ] ,  " +
                        " @INT_OPM_ProcesoActivo = '{10}' ",
                        (int)TipoDeVenta.Credito, "0000", "0000",
                        Fg.PonCeros(sCodigo, 13), sCodigo.Trim(),
                        Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4), 1, 0, localElegibilidad.ListaClaves_Receta,
                        Convert.ToInt32(GnFarmacia.INT_OPM_ProcesoActivo));
                    if (!leer.Exec(sSql))
                    {
                        Error.GrabarError(leer, "ObtenerDatosProducto()");
                        General.msjError("Ocurrió un error al obtener la información del Producto.");
                    }
                    else
                    {
                        if (!leer.Leer())
                        {
                            General.msjUser("Producto no encontrado ó no esta Asignado a la Farmacia.");
                            myGrid.LimpiarRenglon(Renglon);
                        }
                        else
                        {
                            if (!leer.CampoBool("EsDeFarmacia"))
                            {
                                bCargarDatosProducto = false;
                                sMsj = "El Producto " + leer.Campo("Descripcion") + " no esta registrado en la Farmacia, verifique.";
                            }
                            //////else
                            //////{
                            //////    if (bDispensarSoloCuadroBasico)
                            //////    {
                            //////        if (!leer.CampoBool("DCB"))
                            //////        {
                            //////            bCargarDatosProducto = false;
                            //////            sMsj = "El Producto " + leer.Campo("Descripcion") + " no esta dentro del Cuadro Básico Autorizado, verifique.";
                            //////        }
                            //////    }
                            //////}

                            if (!bCargarDatosProducto)
                            {
                                General.msjUser(sMsj);
                                myGrid.LimpiarRenglon(Renglon);
                                myGrid.SetActiveCell(Renglon, (int)Cols.CodEAN);
                            }
                            else
                            {
                                CargaDatosProducto(Renglon, BuscarInformacion);
                            }
                        }
                    }
                }
            }
            else
            {
                //General.msjError(sMsjEanInvalido);
                myGrid.LimpiarRenglon(Renglon);
                myGrid.ActiveCelda(Renglon, (int)Cols.CodEAN);
                SendKeys.Send("");
            }
        }

        private bool validarProductoCtrlVales(string CodigoEAN)
        {
            bool bRegresa = true;
            bool bEsCero = false;
            // string sDato = "";

            return bRegresa;
        }

        private void CargaDatosProducto()
        {
            CargaDatosProducto(myGrid.ActiveRow, true);
        }

        private void CargaDatosProducto(int Renglon, bool BuscarInformacion)
        {
            int iRowActivo = Renglon; //// myGrid.ActiveRow;           
            int iColEAN = (int)Cols.CodEAN;
            bool bEsMach4 = false;
            string sCodEAN = leer.Campo("CodigoEAN");

            if (sValorGrid != sCodEAN)
            {
                if (validarProductoCtrlVales(sCodEAN))
                {
                    if (!myGrid.BuscaRepetido(sCodEAN, iRowActivo, iColEAN))
                    {
                        myGrid.SetValue(iRowActivo, iColEAN, sCodEAN);
                        myGrid.SetValue(iRowActivo, (int)Cols.Codigo, leer.Campo("IdProducto"));
                        myGrid.SetValue(iRowActivo, (int)Cols.Descripcion, leer.Campo("Descripcion"));
                        myGrid.SetValue(iRowActivo, (int)Cols.TasaIva, leer.Campo("PorcIva"));
                        myGrid.SetValue(iRowActivo, (int)Cols.Cantidad, 0);
                        myGrid.SetValue(iRowActivo, (int)Cols.ClaveSSA, leer.Campo("ClaveSSA"));


                        myGrid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.CodEAN);

                        ////////////// Marcar el Renglon como Codigo de Robot si la Farmacia conectada tiene Robot 
                        //////if (IMach4.EsClienteIMach4)
                        //////{
                        //////    if (bEsMach4)
                        //////    {
                        //////        GnFarmacia.ValidarCodigoIMach4(myGrid, bEsMach4, iRowActivo);
                        //////        IMachPtoVta.Show(leer.Campo("IdProducto"), sCodEAN);
                        //////    }
                        //////}

                        Application.DoEvents(); //// Asegurar que se refresque la pantalla 
                        this.Refresh();
                        ////CargarLotesCodigoEAN(BuscarInformacion);


                        // myGrid.SetActiveCell(myGrid.iRowActivo, 1);
                        myGrid.SetActiveCell(iRowActivo, (int)Cols.Cantidad);
                    }
                    else
                    {
                        General.msjUser("El artículo ya se encuentra capturado en otro renglón.");
                        myGrid.SetValue(myGrid.ActiveRow, 1, "");
                        myGrid.SetActiveCell(myGrid.ActiveRow, 1);
                        myGrid.EnviarARepetido();
                    }
                }
                else
                {
                    // Asegurar que no cambie el CodigoEAN
                    myGrid.SetValue(iRowActivo, iColEAN, sCodEAN);
                }
            }

            grdProductos.EditMode = false;
        }

        private void limpiarColumnas()
        {
            for (int i = 2; i <= myGrid.Columns; i++) //Columnas. Nota: Inicia a partir de la 2da.
            {
                myGrid.SetValue(myGrid.ActiveRow, i, "");
            }
        }

        private void EliminarRenglonesVacios()
        {
            for (int i = 1; i <= myGrid.Rows; i++) //Renglones.
            {
                if (myGrid.GetValue(i, 2).Trim() == "") //Si la columna oculta IdProducto esta vacia se elimina.
                {
                    myGrid.DeleteRow(i);
                }
            }

            if (myGrid.Rows == 0) // Si No existen renglones, se inserta 1.
            {
                myGrid.AddRow();
            }
        }
        #endregion Grid

        private void txtFolioElegibilidad_Validating(object sender, CancelEventArgs e)
        {
            //Fg.IniciaControles(this, true, FrameInformacion);
            bool bContinua = false;
            Application.DoEvents();
            System.Threading.Thread.Sleep(100);

            txtFolioElegibilidad.Enabled = false;
            //IniciarToolBar(false, false);

            Validar_Elegibilidad();
            localElegibilidad.Validar_FolioDeReceta(txtFolioElegibilidad.Text, txtFolioReceta.Text, localElegibilidad.MA_FolioDeConsecutivo);

            if (localElegibilidad.MA_EsRecetaManual)
            {
                bContinua = CargarDetalles();
            }

            if (localElegibilidad.RecetaSurtida)
            {
                General.msjAviso("La Receta actual ya fue surtida, no permite modificaciones.");
                bContinua = false;
            }
            else
            {
                if (!localElegibilidad.MA_EsRecetaManual)
                {
                    General.msjAviso("La Receta actual no es receta manual.");
                    bContinua = false;
                }
            }


            if (bContinua)
            {
                txtFolioReceta.Enabled = false;
                txtFolioElegibilidad.Enabled = false;
                //IniciarToolBar(false, true);
                txtPlanBeneficiario.Focus();
            }
            else
            {
                txtFolioElegibilidad.Enabled = true;
                //IniciarToolBar(true, false);
                btnNuevo_Click(this, null);
            }

            //lblResultadoValidacion.ForeColor = validarElegibilidad.Color__Elegilidad_Valida_ParaSurtido;
            //lblResultadoValidacion.Text = validarElegibilidad.Mensaje__Elegilidad_Valida_ParaSurtido; 
        }

        private void Validar_Elegibilidad()
        {
            localElegibilidad = new clsValidar_Elegibilidad();
            localElegibilidad.Validar_Elegibilidad(GnDll_SII_IMediaccess.IdFarmacia_MA, txtFolioElegibilidad.Text.Trim());

            txtFolioElegibilidad.Text = localElegibilidad.Elegibilidad;
            txtFolioReceta.Text = localElegibilidad.MA_FolioDeReceta_Asociado;
            lblFolioReferencia.Text = localElegibilidad.ReferenciaBeneficiario;
        }

        private bool CargarDetalles()
        {
            bool bRegresa = true;

            string sSql = string.Format("Select * From vw_INT_MA__RecetasElectronicas Where Elegibilidad = '{0}'", txtFolioElegibilidad.Text);

            if (leer.Exec(sSql))
            {
                if (leer.Leer())
                {
                    txtPlanBeneficiario.Text = leer.Campo("PlanBeneficiario");
                    lblNombreBeneficiario.Text = leer.Campo("NombrePaciente");
                   
                    txtNombreMedico.Text = leer.Campo("NombreMedico");
                    txtEspecialidad.Text = leer.Campo("Especialidad");
                    txtIdDiagnostico_01.Text = leer.Campo("CIE_01");
                    txtIdDiagnostico_02.Text = leer.Campo("CIE_02");
                    txtIdDiagnostico_03.Text = leer.Campo("CIE_03");
                    txtIdDiagnostico_04.Text = leer.Campo("CIE_04");

                    lblDiagnostico_01.Text = leer.Campo("NombreCIE_01");
                    lblDiagnostico_02.Text = leer.Campo("NombreCIE_02");
                    lblDiagnostico_03.Text = leer.Campo("NombreCIE_03");
                    lblDiagnostico_04.Text = leer.Campo("NombreCIE_04");

                    leer.RegistroActual = 0;

                    myGrid.Limpiar(false);

                    for (int i = 1; leer.Leer(); i++)
                    {
                        myGrid.Rows += 1; 
                        myGrid.SetValue(i, (int)Cols.CodEAN, leer.Campo("CodigoEAN"));
                        myGrid.SetValue(i, (int)Cols.Codigo, leer.Campo("IdProducto"));
                        myGrid.SetValue(i, (int)Cols.ClaveSSA, leer.Campo("ClaveSSA"));
                        myGrid.SetValue(i, (int)Cols.Descripcion, leer.Campo("DescripcionCortaClave"));
                        myGrid.SetValue(i, (int)Cols.TasaIva, leer.CampoInt("TasaIva"));
                        myGrid.SetValue(i, (int)Cols.Cantidad, leer.CampoInt("Cantidad"));
                    }
                }
                else
                {
                    bRegresa = false;
                    General.msjError("No se encontro información de la receta manual.");
                }
            }
            else
            {
                bRegresa = false;
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al buscar la información de la receta manual.");
            }

            return bRegresa;
        }

        private void btnRegistrarMedicos_Click(object sender, EventArgs e)
        {
            Farmacia.Catalogos.FrmMedicos f = new Farmacia.Catalogos.FrmMedicos();
            f.ShowDialog();
        }

        private void txtNombreMedico_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Medicos(sEstado, sFarmacia, "txtNombreMedico_KeyDown");
                if (leer.Leer())
                {
                    if (leer.Campo("Status") == "C")
                    {
                        General.msjAviso(" El Médico : " + leer.Campo("IdMedico") + " -- " + leer.Campo("NombreCompleto") + ". Esta cancelado. ");
                        txtNombreMedico.Text = "";
                        lblMedico.Text = "";
                        txtNombreMedico.Focus();
                    }
                    else
                    {
                        txtNombreMedico.Text = leer.Campo("IdMedico");
                        lblMedico.Text = leer.Campo("NombreCompleto");
                    }
                }
            }
        }

        private void txtNombreMedico_Validating(object sender, CancelEventArgs e)
        {
            if (txtNombreMedico.Text.Trim() == "")
            {
                txtNombreMedico.Text = "";
                lblMedico.Text = "";
            }
            else
            {
                leer.DataSetClase = Consultas.Medicos(sEstado, sFarmacia, txtNombreMedico.Text, "txtNombreMedico_Validating");
                if (leer.Leer())
                {
                    if (leer.Campo("Status") == "C")
                    {
                        General.msjAviso(" El Médico : " + leer.Campo("IdMedico") + " -- " + leer.Campo("NombreCompleto") + ". Esta cancelado. ");
                        txtNombreMedico.Text = "";
                        lblMedico.Text = "";
                        txtNombreMedico.Focus();
                    }
                    else
                    {
                        txtNombreMedico.Text = leer.Campo("IdMedico");
                        lblMedico.Text = leer.Campo("NombreCompleto");
                    }
                }
                else
                {
                    General.msjUser("Clave de Médico no encontrada, verifique.");
                    txtNombreMedico.Text = "";
                    lblMedico.Text = "";
                    txtNombreMedico.Focus();
                }
            }
        }

        private void txtNombreMedico_TextChanged(object sender, EventArgs e)
        {
            lblMedico.Text = "";
        }
    }
}
