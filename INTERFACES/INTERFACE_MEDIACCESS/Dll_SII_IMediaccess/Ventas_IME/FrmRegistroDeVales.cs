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
using DllFarmaciaSoft;
using DllFarmaciaSoft.Ayudas;

using Farmacia.Procesos;
using Farmacia.Vales;
using Farmacia.Ventas;
using Farmacia.Catalogos;

using Dll_SII_IMediaccess;
using Dll_SII_IMediaccess.Validaciones_MA; 

namespace Dll_SII_IMediaccess.Ventas_IME
{
    public partial class FrmRegistroDeVales : FrmBaseExt 
    {
        private enum Cols
        {
            Ninguna = 0,
            ClaveSSA, Descripcion, Cantidad
        }

        clsDatosCliente DatosCliente;
        clsConexionSQL con = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid myGrid;
        FrmHelpBeneficiarios helpBeneficiarios;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sIdCliente = "";
        string sIdClienteNombre = "";
        string sIdSubCliente = "";
        string sIdSubClienteNombre = "";

        DllFarmaciaSoft.clsAyudas Ayuda; // = new clsAyudas();
        DataSet dtsCargarDatos = new DataSet();
        DllFarmaciaSoft.clsConsultas Consultas;

        clsValidar_Vale localVale;
        public bool bValeManualGuardado = false; 
        private bool bExterna = false; 

        clsCodigoEAN EAN = new clsCodigoEAN();
        string sCodigoEAN_Seleccionado = "";
        FrmRevisarCodigosEAN RevCodigosEAN = new FrmRevisarCodigosEAN();
        Cols ColActiva = Cols.Ninguna;
        string sValorGrid = "";

        public FrmRegistroDeVales()
        {
            InitializeComponent();
            bValeManualGuardado = true;
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

            ////this.Text = "Modificación de recetas manuales";
        }

        public FrmRegistroDeVales(clsValidar_Vale Vale)
        {
            InitializeComponent();

            localVale = Vale;
            sIdCliente = localVale.IdCliente;
            sIdClienteNombre = localVale.ClienteNombre;
            sIdSubCliente = localVale.IdSubCliente;
            sIdSubClienteNombre = localVale.SubClienteNombre;

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

        private void FrmRegistroDeVales_Load(object sender, EventArgs e)
        {
            InicializarPantalla(); 
        }

        #region Botones 
        private void InicializarPantalla()
        {
            myGrid.Limpiar(true);
            Fg.IniciaControles(true);

            if (!bValeManualGuardado)
            {
                bValeManualGuardado = false;


                ////txtFolioElegibilidad.Enabled = false;
                lblSocioComercial.Text = localVale.NombreSocioComercial;
                lblSucursalSocioComercial.Text = localVale.NombreSucursalSocioComercial;
                txtFolioVale.Text = localVale.FolioVale; 
                txtFolioVale.Enabled = false;

                ////txtFolioElegibilidad.Text = localVale.Elegibilidad;
                ////txtFolioReceta.Text = localVale.MA_FolioDeReceta;

                ////lblNombreBeneficiario.Text = localVale.NombreBeneficiario;
                ////lblFolioReferencia.Text = localVale.ReferenciaBeneficiario;
            }



            dtpFechaDeReceta.MaxDate = dtpFechaDeReceta.Value;
            dtpFechaDeReceta.MinDate = dtpFechaDeReceta.MaxDate.AddDays(-20); 
            dtpFechaDeReceta.Focus();
            dtpFechaDeReceta.Select();

            if (!bExterna)
            {
                ////txtEspecialidad.Focus(); 
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
            string sClaveSSA = ""; 

            //RecetaManualGuardada = false;
            if (!con.Abrir())
            {
                General.msjErrorAlAbrirConexion();
            }
            else
            {
                con.IniciarTransaccion();

                bRegresa = true; 
                ////if (RecetaManualGuardada)
                ////{
                ////    bRegresa = GuardarDetalladoAuditoria();
                ////}

                sSql = string.Format("Exec spp_INT_IME__Vales_001_Encabezado " +
                   " @IdSocioComercial = '{0}', @IdSucursal = '{1}', @Folio_Vale = '{2}', @FechaEmision_Vale = '{3}', @EsValeManual = '{4}', " + 
                   " @IdEmpresa = '{5}', @IdEstado = '{6}', @IdFarmacia = '{7}', @IdPersonal = '{8}' ",
                   localVale.IdSocioComercial, localVale.IdSucursalSocioComercial, localVale.FolioVale, General.FechaYMD(dtpFechaDeReceta.Value), 1, 
                   sEmpresa, sEstado, sFarmacia, DtGeneral.IdPersonal );

                if (bRegresa)
                {
                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                    }
                    else 
                    {
                        ////bRegresa = true;
                        for (int i = 1; i <= myGrid.Rows; i++)
                        {
                            iPartida++;
                            sClaveSSA = myGrid.GetValue(i, (int)Cols.ClaveSSA); 
                            //sIdProducto = myGrid.GetValue(i, (int)Cols.Codigo);
                            iCantidad = myGrid.GetValueInt(i, (int)Cols.Cantidad);

                            sSql = string.Format("Exec spp_INT_IME__Vales__002_ClavesSSA " +
                                " @IdSocioComercial = '{0}', @IdSucursal = '{1}', @Folio_Vale = '{2}', @Partida = '{3}', " + 
                                " @ClaveSSA = '{4}', @CantidadSolicitada = '{5}', @CantidadSurtida = '{6}' ",
                                localVale.IdSocioComercial, localVale.IdSucursalSocioComercial, localVale.FolioVale,
                                iPartida, sClaveSSA, iCantidad, 0);

                            if (!leer.Exec(sSql))
                            {
                                bRegresa = false;
                                break;
                            }
                        }
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
                    bValeManualGuardado = true;

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

            if (bRegresa && bValeManualGuardado && !bExterna)
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

            ////if (bRegresa && txtPlanBeneficiario.Text.Trim() == "")
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("No ha capturado el Plan del Beneficiario, verifique.");
            ////    txtPlanBeneficiario.Focus();
            ////}

            ////if (bRegresa && txtNombreMedico.Text.Trim() == "")
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("No ha capturado el Nombre del Médico, verifique.");
            ////    txtNombreMedico.Focus();
            ////}

            ////if (bRegresa && txtEspecialidad.Text.Trim() == "")
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("No ha capturado la Especialidad, verifique.");
            ////    txtEspecialidad.Focus();
            ////}

            ////if (bRegresa && txtIdDiagnostico_01.Text.Trim() == "")
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("Debe capturar al menos el primer Diagnóstico CIE10, verifique.");
            ////    txtIdDiagnostico_01.Focus();
            ////}

            BorrarRenglonesVacios();

            if (bRegresa && myGrid.GetValue(1, (int)Cols.ClaveSSA) == "")
            {
                bRegresa = false;
                General.msjUser("Debe capturar al menos una Clave, verifique.");
            }

            return bRegresa; 
        }

        private void BorrarRenglonesVacios()
        {
            for (int i = 1; i <= myGrid.Rows; i++)
            {
                if (myGrid.GetValue(i, (int)Cols.ClaveSSA) == "" || myGrid.GetValueInt(i, (int)Cols.Cantidad) == 0)
                {
                    myGrid.DeleteRow(i);
                }
            }

            if (myGrid.Rows == 0)
            {
                myGrid.Rows = 1;
            }
        }


        //////private bool GuardarDetalladoAuditoria()
        //////{
        //////    bool bRegresa = false;

        //////    string sSql = string.Format("Exec spp_INT_MA__ADT_RecetasElectronicas '{0}', '{1}', '{2}', '{3}'",
        //////        txtFolioReceta.Text, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal);

        //////    bRegresa = leer.Exec(sSql);

        //////    return bRegresa;
        //////}

        private bool ValidarModificacionPrevia()
        {
            bool bRegresa = true;

            string sSql = "";  // string.Format("Select COUNT(*) As Registros From INT_MA__ADT_RecetasElectronicas_001_Encabezado Where Elegibilidad = '{0}'", txtFolioElegibilidad.Text);

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
        ////private void txtIdDiagnostico_01_Validating(object sender, CancelEventArgs e)
        ////{
        ////    Validar_Diagnostico(txtIdDiagnostico_01, lblDiagnostico_01); 
        ////}

        ////private void txtIdDiagnostico_01_TextChanged(object sender, EventArgs e)
        ////{
        ////    lblDiagnostico_01.Text = ""; 
        ////}

        ////private void txtIdDiagnostico_01_KeyDown(object sender, KeyEventArgs e)
        ////{
        ////    if (e.KeyCode == Keys.F1)
        ////    {
        ////        Ayuda_Diagnostico(txtIdDiagnostico_01, lblDiagnostico_01);
        ////    }
        ////}

        ////private void txtIdDiagnostico_02_Validating(object sender, CancelEventArgs e)
        ////{
        ////    Validar_Diagnostico(txtIdDiagnostico_02, lblDiagnostico_02);
        ////}

        ////private void txtIdDiagnostico_02_TextChanged(object sender, EventArgs e)
        ////{
        ////    lblDiagnostico_02.Text = "";
        ////}

        ////private void txtIdDiagnostico_02_KeyDown(object sender, KeyEventArgs e)
        ////{
        ////    if (e.KeyCode == Keys.F1)
        ////    {
        ////        Ayuda_Diagnostico(txtIdDiagnostico_02, lblDiagnostico_02);
        ////    }
        ////}

        ////private void txtIdDiagnostico_03_Validating(object sender, CancelEventArgs e)
        ////{
        ////    Validar_Diagnostico(txtIdDiagnostico_03, lblDiagnostico_03);
        ////}

        ////private void txtIdDiagnostico_03_TextChanged(object sender, EventArgs e)
        ////{
        ////    lblDiagnostico_03.Text = "";
        ////}

        ////private void txtIdDiagnostico_03_KeyDown(object sender, KeyEventArgs e)
        ////{
        ////    if (e.KeyCode == Keys.F1)
        ////    {
        ////        Ayuda_Diagnostico(txtIdDiagnostico_03, lblDiagnostico_03);
        ////    }
        ////}

        ////private void txtIdDiagnostico_04_Validating(object sender, CancelEventArgs e)
        ////{
        ////    Validar_Diagnostico(txtIdDiagnostico_04, lblDiagnostico_04);
        ////}

        ////private void txtIdDiagnostico_04_TextChanged(object sender, EventArgs e)
        ////{
        ////    lblDiagnostico_04.Text = "";
        ////}

        ////private void txtIdDiagnostico_04_KeyDown(object sender, KeyEventArgs e)
        ////{
        ////    if (e.KeyCode == Keys.F1)
        ////    {
        ////        Ayuda_Diagnostico(txtIdDiagnostico_04, lblDiagnostico_04);
        ////    }
        ////}

        ////private bool Validar_Diagnostico(TextBox Diagnostico, Label lblDiagnostico)
        ////{
        ////    bool bRegresa = true; 

        ////    if (Diagnostico.Text.Trim() != "")
        ////    {
        ////        if (Diagnostico.Text.Trim().Length < GnFarmacia.ClaveDiagnosticoCaracteres)
        ////        {
        ////            bRegresa = false; 
        ////            General.msjUser(string.Format("Formato de Diagnóstico incorrecto, el diagnóstico debe ser de {0} caracteres, verifique.", GnFarmacia.ClaveDiagnosticoCaracteres));
        ////        }
        ////        else
        ////        {
        ////            leer.DataSetClase = Consultas.DiagnosticosCIE10(Diagnostico.Text, "Diagnostico_Validating");
        ////            if (!leer.Leer())
        ////            {
        ////                General.msjUser("Clave de Diagnóstico no encontrada, verifique.");
        ////                bRegresa= false; 
        ////            }
        ////            else
        ////            {
        ////                Diagnostico.Text = leer.Campo("ClaveDiagnostico");
        ////                lblDiagnostico.Text = leer.Campo("Descripcion");
        ////            }
        ////        }
        ////    }

        ////    return bRegresa; 
        ////}

        ////private bool Ayuda_Diagnostico(TextBox Diagnostico, Label lblDiagnostico)
        ////{
        ////    bool bRegresa = false;

        ////    leer.DataSetClase = Ayuda.DiagnosticosCIE10("txtIdDiagnostico_Validating");
        ////    if (leer.Leer())
        ////    {
        ////        bRegresa = true;
        ////        Diagnostico.Text = leer.Campo("ClaveDiagnostico");
        ////        lblDiagnostico.Text = leer.Campo("Descripcion");
        ////    }

        ////    return bRegresa; 
        ////}
        #endregion Diagnosticos 

        #region Grid 
        private void grdProductos_EditModeOff(object sender, EventArgs e)
        {
            try
            {
                Cols iCol = (Cols)myGrid.ActiveCol;
                switch (iCol)
                {
                    case Cols.ClaveSSA:
                        ObtenerDatosProducto();
                        break;
                }
            }
            catch (Exception ex)
            {
                ////General.msjError("01 " + ex.Message);
            }
        }

        private void grdProductos_EditModeOn(object sender, EventArgs e)
        {
            try
            {
                sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.ClaveSSA);
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
                if (myGrid.GetValue(myGrid.ActiveRow, (int)Cols.ClaveSSA) != "" && myGrid.GetValue(myGrid.ActiveRow, (int)Cols.Descripcion) != "")
                {
                    myGrid.Rows = myGrid.Rows + 1;
                    myGrid.ActiveRow = myGrid.Rows;
                    myGrid.SetActiveCell(myGrid.Rows, (int)Cols.ClaveSSA);
                    //ObtenerDatosProducto();
                }
            }
        }

        private void grdProductos_KeyDown(object sender, KeyEventArgs e)
        {
            ColActiva = (Cols)myGrid.ActiveCol;
            int iRowActivo = myGrid.ActiveRow;

            if (ColActiva == Cols.ClaveSSA)
            {
                    if (e.KeyCode == Keys.F1)
                    {
                        sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.ClaveSSA);
                        leer.DataSetClase = Ayuda.ClavesSSA_Sales("grdProductos_KeyDown_1");
                        if (leer.Leer())
                        {
                            myGrid.SetValue(myGrid.ActiveRow, (int)Cols.ClaveSSA, leer.Campo("ClaveSSA"));
                            ObtenerDatosProducto();
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
            }
        }

        ////private bool ValidarSeleccionCodigoEAN(string Codigo)
        ////{
        ////    bool bRegresa = true;

        ////    sCodigoEAN_Seleccionado = Codigo;

        ////    sCodigoEAN_Seleccionado = RevCodigosEAN.VerificarCodigosEAN(Codigo, false);
        ////    bRegresa = RevCodigosEAN.CodigoSeleccionado;


        ////    return bRegresa;
        ////}

        private void ObtenerDatosProducto()
        {
            ObtenerDatosProducto(myGrid.ActiveRow, true);
        }

        private void ObtenerDatosProducto(int Renglon, bool BuscarInformacion)
        {
            string sCodigo = "", sSql = "";
            bool bCargarDatosProducto = true;
            string sMsj = "";

            sCodigo = myGrid.GetValue(Renglon, (int)Cols.ClaveSSA);
            if (sCodigo == "")
            {
                myGrid.LimpiarRenglon(Renglon);
                myGrid.SetActiveCell(Renglon, (int)Cols.ClaveSSA);
            }
            else
            {
                leer.DataSetClase = Consultas.ClavesSSA_Sales(sCodigo, true, "ObtenerDatosProducto");
                if (leer.Leer())
                {
                    myGrid.SetValue(Renglon, (int)Cols.ClaveSSA, leer.Campo("ClaveSSA"));
                    myGrid.SetValue(Renglon, (int)Cols.Descripcion, leer.Campo("Descripcion"));
                    myGrid.SetValue(Renglon, (int)Cols.Cantidad, 0);
                    myGrid.SetActiveCell(Renglon, (int)Cols.Cantidad);
                }
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
            ////CargaDatosProducto(myGrid.ActiveRow, true);
        }

        private void CargaDatosProducto(int Renglon, bool BuscarInformacion)
        {
            //////int iRowActivo = Renglon; //// myGrid.ActiveRow;           
            //////int iColEAN = (int)Cols.ClaveSSA;
            //////bool bEsMach4 = false;
            //////string sCodEAN = leer.Campo("CodigoEAN");

            //////if (sValorGrid != sCodEAN)
            //////{
            //////    if (validarProductoCtrlVales(sCodEAN))
            //////    {
            //////        if (!myGrid.BuscaRepetido(sCodEAN, iRowActivo, iColEAN))
            //////        {
            //////            myGrid.SetValue(iRowActivo, iColEAN, sCodEAN);
            //////            myGrid.SetValue(iRowActivo, (int)Cols.Codigo, leer.Campo("IdProducto"));
            //////            myGrid.SetValue(iRowActivo, (int)Cols.Descripcion, leer.Campo("Descripcion"));
            //////            myGrid.SetValue(iRowActivo, (int)Cols.TasaIva, leer.Campo("PorcIva"));
            //////            myGrid.SetValue(iRowActivo, (int)Cols.Cantidad, 0);
            //////            myGrid.SetValue(iRowActivo, (int)Cols.ClaveSSA, leer.Campo("ClaveSSA"));


            //////            myGrid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.CodEAN);

            //////            ////////////// Marcar el Renglon como Codigo de Robot si la Farmacia conectada tiene Robot 
            //////            //////if (IMach4.EsClienteIMach4)
            //////            //////{
            //////            //////    if (bEsMach4)
            //////            //////    {
            //////            //////        GnFarmacia.ValidarCodigoIMach4(myGrid, bEsMach4, iRowActivo);
            //////            //////        IMachPtoVta.Show(leer.Campo("IdProducto"), sCodEAN);
            //////            //////    }
            //////            //////}

            //////            Application.DoEvents(); //// Asegurar que se refresque la pantalla 
            //////            this.Refresh();
            //////            ////CargarLotesCodigoEAN(BuscarInformacion);


            //////            // myGrid.SetActiveCell(myGrid.iRowActivo, 1);
            //////            myGrid.SetActiveCell(iRowActivo, (int)Cols.Cantidad);
            //////        }
            //////        else
            //////        {
            //////            General.msjUser("El artículo ya se encuentra capturado en otro renglón.");
            //////            myGrid.SetValue(myGrid.ActiveRow, 1, "");
            //////            myGrid.SetActiveCell(myGrid.ActiveRow, 1);
            //////            myGrid.EnviarARepetido();
            //////        }
            //////    }
            //////    else
            //////    {
            //////        // Asegurar que no cambie el CodigoEAN
            //////        myGrid.SetValue(iRowActivo, iColEAN, sCodEAN);
            //////    }
            //////}

            //////grdProductos.EditMode = false;
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
            ////bool bContinua = false;
            ////Application.DoEvents();
            ////System.Threading.Thread.Sleep(100);

            ////txtFolioElegibilidad.Enabled = false;
            //////IniciarToolBar(false, false);

            ////Validar_Elegibilidad();
            ////localVale.Validar_FolioDeReceta(txtFolioElegibilidad.Text, txtFolioReceta.Text);

            ////if (localVale.MA_EsRecetaManual)
            ////{
            ////    bContinua = CargarDetalles();
            ////}

            ////if (localVale.RecetaSurtida)
            ////{
            ////    General.msjAviso("La Receta actual ya fue surtida, no permite modificaciones.");
            ////    bContinua = false;
            ////}
            ////else
            ////{
            ////    if (!localVale.MA_EsRecetaManual)
            ////    {
            ////        General.msjAviso("La Receta actual no es receta manual.");
            ////        bContinua = false;
            ////    }
            ////}


            ////if (bContinua)
            ////{
            ////    txtFolioReceta.Enabled = false;
            ////    txtFolioElegibilidad.Enabled = false;
            ////    //IniciarToolBar(false, true);
            ////    txtPlanBeneficiario.Focus();
            ////}
            ////else
            ////{
            ////    txtFolioElegibilidad.Enabled = true;
            ////    //IniciarToolBar(true, false);
            ////    btnNuevo_Click(this, null);
            ////}

            //////lblResultadoValidacion.ForeColor = validarElegibilidad.Color__Elegilidad_Valida_ParaSurtido;
            //////lblResultadoValidacion.Text = validarElegibilidad.Mensaje__Elegilidad_Valida_ParaSurtido; 
        }

        private void Validar_Elegibilidad()
        {
            ////localVale = new clsValidar_Elegibilidad();
            ////localVale.Validar_Elegibilidad(GnDll_SII_IMediaccess.IdFarmacia_MA, txtFolioElegibilidad.Text.Trim());

            ////txtFolioElegibilidad.Text = localVale.Elegibilidad;
            ////txtFolioReceta.Text = localVale.MA_FolioDeReceta_Asociado;
            ////lblFolioReferencia.Text = localVale.ReferenciaBeneficiario;
        }

        private bool CargarDetalles()
        {
            bool bRegresa = true;

            string sSql = string.Format("Select * From vw_INT_MA__RecetasElectronicas Where Elegibilidad = '{0}'", "");

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al buscar la información de la receta manual.");
            }
            else 
            {
                if (leer.Leer())
                {
                    ////txtPlanBeneficiario.Text = leer.Campo("PlanBeneficiario");
                    //lblNombreBeneficiario.Text = leer.Campo("NombrePaciente");
                   
                    ////txtNombreMedico.Text = leer.Campo("NombreMedico");
                    ////txtEspecialidad.Text = leer.Campo("Especialidad");
                    ////txtIdDiagnostico_01.Text = leer.Campo("CIE_01");
                    ////txtIdDiagnostico_02.Text = leer.Campo("CIE_02");
                    ////txtIdDiagnostico_03.Text = leer.Campo("CIE_03");
                    ////txtIdDiagnostico_04.Text = leer.Campo("CIE_04");

                    ////lblDiagnostico_01.Text = leer.Campo("NombreCIE_01");
                    ////lblDiagnostico_02.Text = leer.Campo("NombreCIE_02");
                    ////lblDiagnostico_03.Text = leer.Campo("NombreCIE_03");
                    ////lblDiagnostico_04.Text = leer.Campo("NombreCIE_04");

                    leer.RegistroActual = 0;

                    myGrid.Limpiar(false);

                    for (int i = 1; leer.Leer(); i++)
                    {
                        ////myGrid.Rows += 1; 
                        ////myGrid.SetValue(i, (int)Cols.CodEAN, leer.Campo("CodigoEAN"));
                        ////myGrid.SetValue(i, (int)Cols.Codigo, leer.Campo("IdProducto"));
                        ////myGrid.SetValue(i, (int)Cols.ClaveSSA, leer.Campo("ClaveSSA"));
                        ////myGrid.SetValue(i, (int)Cols.Descripcion, leer.Campo("DescripcionCortaClave"));
                        ////myGrid.SetValue(i, (int)Cols.TasaIva, leer.CampoInt("TasaIva"));
                        ////myGrid.SetValue(i, (int)Cols.Cantidad, leer.CampoInt("Cantidad"));
                    }
                }
                else
                {
                    bRegresa = false;
                    General.msjError("No se encontro información de la receta manual.");
                }
            }

            return bRegresa;
        }

        #region Informacion del Beneficiario 
        //private void btnRegistrarBeneficiarios_Click(object sender, EventArgs e)
        //{
        //    FrmBeneficiarios f = new FrmBeneficiarios();
        //    f.MostrarDetalle(sIdCliente, sIdClienteNombre, sIdSubCliente, sIdSubClienteNombre); 
        //}

        //private void txtIdBenificiario_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.F1)
        //    {
        //        helpBeneficiarios = new FrmHelpBeneficiarios();
        //        leer.DataSetClase = helpBeneficiarios.ShowHelp(sIdCliente, sIdSubCliente, false);
        //        if (leer.Leer())
        //        {
        //            CargarDatosBenefiario();
        //        }
        //    }
        //}

        //private void txtIdBenificiario_TextChanged(object sender, EventArgs e)
        //{
        //    lblNombreBeneficiario.Text = ""; 
        //}

        //private void txtIdBenificiario_Validating(object sender, CancelEventArgs e)
        //{
        //    if (txtIdBenificiario.Text.Trim() != "")
        //    {
        //        leer.DataSetClase = Consultas.Beneficiarios(sEstado, sFarmacia, sIdCliente, sIdSubCliente, txtIdBenificiario.Text, "txtIdBenificiario_Validating");
        //        if (leer.Leer())
        //        {
        //            CargarDatosBenefiario();
        //        }
        //        else
        //        {
        //            General.msjUser("Clave de Beneficiario no encontrada, verifique.");
        //            txtIdBenificiario.Text = ""; 
        //            txtIdBenificiario.Focus();
        //        }
        //    }
        //}

        //private void CargarDatosBenefiario()
        //{
        //    txtIdBenificiario.Text = leer.Campo("IdBeneficiario");
        //    lblNombreBeneficiario.Text = leer.Campo("NombreCompleto");
        //}
        #endregion Informacion del Beneficiario
    }
}
