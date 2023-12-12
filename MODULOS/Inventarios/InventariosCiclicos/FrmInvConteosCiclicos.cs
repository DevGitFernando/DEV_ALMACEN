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
using DllFarmaciaSoft.ExportarExcel;

using ClosedXML.Excel;

namespace Inventarios.InventariosCiclicos
{
    public partial class FrmInvConteosCiclicos : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid Grid;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sIdPersonal = DtGeneral.IdPersonal;

        Color cColor_Validado = Color.GreenYellow;
        Color cColor_ValidadoPrevio = Color.Yellow;
        Color cColor_NoValidado = Color.White;

        bool bEsModuloValido = false;
        clsAyudas Ayuda;
        clsConsultas Consultas;


        private enum Cols
        {
            Ninguna = 0, ClaveSSA, DescSal, IdProducto, CodigoEAN, Descripcion, Presentacion, Lote, IdPasillo, IdEstante, IdEntrepaño, ExistenciaLogica, Cantidad, Observaciones, Validado
        }

        clsDatosCliente DatosCliente;

        public FrmInvConteosCiclicos()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "");

            Grid = new clsGrid(ref grdDetalles, this);
            Grid.BackColorColsBlk = Color.White;
            grdDetalles.EditModeReplace = true;

            Error = new clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);

            
        }

        private void FrmInvConteosCiclicos_Load(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            //VerificarConteosCiclicos();
            if(validarInformacion())
            {
                GenerarFolioConteo();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar(false);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir();
        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);


            Grid.Limpiar(false);
            dtpFechaRegistro.Enabled = false;
            lblCancelado.Visible = false;

            IniciaBotones(false, false, true, false);

            bEsModuloValido = DtGeneral.EsAlmacen;

            CargarFolioCiclico();

            nmNumClaves.Focus();
        }

        private void IniciaBotones(bool Ejecutar, bool Guardar, bool Imprimir, bool Cerrar)
        {
            btnEjecutar.Enabled = Ejecutar;
            btnGuardar.Enabled = Guardar;
            btnExportarExcel.Enabled = Imprimir;
            btnCerrarFolio.Enabled = Cerrar;
        }

        private void CargarFolioCiclico()
        {
            string sSql = "";
            string sFolioCiclico = "";

            FrmFoliosOrdenesCompras f = new FrmFoliosOrdenesCompras();

            sSql = string.Format(" Exec spp_Mtto_Inv_Ciclicos @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdPersonal = '{3}' ", 
                sEmpresa, sEstado, sFarmacia, DtGeneral.IdPersonal );

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarFolioCiclico()");
                General.msjError("Ocurrio un error al verificar el Folio ciclico.");
            }
            else
            {
                if (leer.Leer())
                {
                    sFolioCiclico = leer.Campo("FolioCiclico");
                    txtFolioCiclico.Text = sFolioCiclico;
                    txtFolioCiclico.Enabled = false;
                    f.MostrarPantalla(sFolioCiclico);

                    txtFolioConteo.Text = f.FolioConteo;
                    //BuscarFolioConteo(Fg.PonCeros(txtFolioConteo.Text, 8));
                    txtFolioConteo_Validating(this, null);
                }
            }
        }

        #endregion Funciones

        #region Buscar_Folio_Conteo_Ciclico
        private void BuscarFolioConteo(string Folio)
        {
            string sSql = "";

            sSql = string.Format("Select * \n" +
                "From vw_Inv_ConteosCiclicosEnc E (NoLock) \n" +
                "Where E.IdEmpresa = '{0}' and E.IdEstado = '{1}' and E.IdFarmacia = '{2}' and E.FolioCiclico = '{3}' And E.FolioConteo = '{4}' \n",
                sEmpresa, sEstado, sFarmacia, txtFolioCiclico.Text, Folio );

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "BuscarFolio()");
                General.msjError("Ocurrio un error al consultar el folio de conteo.");
            }
            else
            {
                if (leer.Leer())
                {
                    CargaDatosFolio();
                }
                else
                {
                    General.msjAviso("Folio no encontrado.");
                    txtFolioConteo.Text = "";
                    txtFolioConteo.Focus();
                }
            }

        }

        private void CargaDatosFolio()
        {
            IniciaBotones(false, true, true, true);
            txtFolioConteo.Text = leer.Campo("FolioConteo");
            txtFolioConteo.Enabled = false;
            dtpFechaRegistro.Value = leer.CampoFecha("FechaRegistro");
            nmNumClaves.Text = leer.CampoInt("NumClaves").ToString();
            nmNumClaves.Enabled = false;

            txtIdPersonal.Enabled = false;
            txtIdPersonal.Text = leer.Campo("IdPersonal_Conteo");
            lblPersonal.Text = leer.Campo("Personal_Conteo");

            /// Aqui los datos de Tipo de Ubicación y Tipo de Insumo 
            rdo_Insumo_Todo.Checked = leer.CampoInt("TipoDeInsumos") == 0;
            rdo_Insumo_MaterialDeCuracion.Checked = leer.CampoInt("TipoDeInsumos") == 1;
            rdo_Insumo_Medicamento.Checked = leer.CampoInt("TipoDeInsumos") == 2;
            rdo_Insumo_Todo.Enabled = false;
            rdo_Insumo_MaterialDeCuracion.Enabled = false;
            rdo_Insumo_Medicamento.Enabled = false;


            rdo_U_Todo.Checked = leer.CampoInt("TipoDeUbicaciones") == 0;
            rdo_U_Picking.Checked = leer.CampoInt("TipoDeUbicaciones") == 1;
            rdo_U_Almacenaje.Checked = leer.CampoInt("TipoDeUbicaciones") == 2;
            rdo_U_Todo.Enabled = false;
            rdo_U_Picking.Enabled = false;
            rdo_U_Almacenaje.Enabled = false;



            if(leer.Campo("Status") == "T")
            {
                lblCancelado.Text = "CERRADO";
                lblCancelado.Visible = true;
                IniciaBotones(false, false, false, false);
            }

            if (leer.Campo("Status") == "C")
            {
                lblCancelado.Visible = true;
                IniciaBotones(false, false, true, false);
            }

            BuscarClavesConteo(leer.Campo("FolioConteo"));
        }

        private void BuscarClavesConteo(string Folio)
        {
            string sSql = "";

            sSql = string.Format( 
                "Select \n" +
                "\tS.ClaveSSA, DescripcionSal, D.IdProducto, D.CodigoEAN, S.Descripcion, S.Presentacion, ClaveLote, \n" +
                "\tD.IdPasillo, D.IdEstante, D.IdEntrepaño, ExistenciaLogica, CantidadContada, Observaciones, Validado \n" +
                "From Inv_Ciclicos_Conteos_Det D (NoLock) \n" +
                "Inner Join vw_Productos_CodigoEAN S (NoLock) On ( D.CodigoEAN = S.CodigoEAN ) \n" +
                "Where D.IdEmpresa = '{0}' and D.IdEstado = '{1}' and D.IdFarmacia = '{2}' and D.FolioCiclico = '{3}' And D.FolioConteo = '{4}' \n" +
                "Order By \n" +
                "\tS.DescripcionSal, D.IdPasillo, D.IdEstante, D.IdEntrepaño, D.IdProducto, D.CodigoEAN, ClaveLote \n",
            sEmpresa, sEstado, sFarmacia, txtFolioCiclico.Text, Folio);

            Grid.Limpiar(false);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "BuscarClavesConteo()");
                General.msjError("Ocurrio un error al consultar las claves a contar.");
            }
            else
            {
                if (leer.Leer())
                {
                    Grid.LlenarGrid(leer.DataSetClase, false, false);

                    for(int i = 1; Grid.Rows >= i; i++)
                    {
                        MarcarValidado(i, true);
                    }

                }
            }
        }
        #endregion Buscar_Folio_Conteo_Ciclico

        #region Generacion_Folio_Conteo
        private bool validarInformacion()
        {
            bool bRegresa = true;

            if(txtIdPersonal.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjError("No ha capturado el Personal a quien se le asignara el Folio de Conteo, verifique.");
                txtIdPersonal.Focus();
            }

            return bRegresa; 
        }
        private bool GenerarFolioConteo()
        {
            bool bRegresa = true;
            string sSql = "";
            int iTipoDeUbicaciones = 0;
            int iTipoDeInsumo = 0;

            int iOpcion = 1;

            sSql = string.Format(" Exec spp_Mtto_Inv_Ciclicos_Conteos \n" + 
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioCiclico = '{3}', @Claves = {4}, \n" +
                "\t@IdPersonal = '{5}', @IdPersonal_Conteo = '{6}', " +
                "\t@TipoDeInsumos = '{7}', @TipoDeUbicaciones = '{8}',\n" + 
                "\t@Opcion = {9} ",
                sEmpresa, sEstado, sFarmacia, txtFolioCiclico.Text, nmNumClaves.Value, sIdPersonal, txtIdPersonal.Text,
                iTipoDeInsumo, iTipoDeUbicaciones, 
                iOpcion);

            if(!cnn.Abrir())
            {
                General.msjErrorAlAbrirConexion();
            }
            else 
            {
                cnn.IniciarTransaccion();

                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(leer, "GenerarFolioConteo()");
                    General.msjError("Ocurrio un error al generar el folio de conteo.");
                }
                else
                {
                    if(!leer.Leer())
                    {
                        cnn.DeshacerTransaccion();
                        General.msjAviso("No se encontro información para generar el Folio de Conteo");
                    }
                    else 
                    {
                        if(!leer.CampoBool("FolioGenerado"))
                        {
                            cnn.DeshacerTransaccion();
                            General.msjAviso("No se encontro información para generar el Folio de Conteo");
                        }
                        else
                        {
                            cnn.CompletarTransaccion();

                            General.msjUser(leer.Campo("Mensaje"));

                            if(leer.Campo("Folio") != "")
                            {
                                BuscarFolioConteo(leer.Campo("Folio"));
                            }
                        }
                    }
                }
                cnn.Cerrar();
            }

            return bRegresa;
        }

        private bool CerrarFolioConteo()
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format(" Exec spp_MTTO_Inv_Ciclicos_Conteos____Cerrar \n" + 
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioCiclico = '{3}', @FolioConteo = '{4}', @IdPersonal = '{5}' \n",
                sEmpresa, sEstado, sFarmacia, txtFolioCiclico.Text, txtFolioConteo.Text.Trim(), sIdPersonal);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "GenerarFolioConteo()");
                General.msjError("Ocurrio un error al Cerrar el folio de conteo.");
            }
            //else
            //{
            //    if (leer.Leer())
            //    {
            //        General.msjUser(leer.Campo("Mensaje"));
            //    }
            //}

            return bRegresa;
        }
        #endregion Generacion_Folio_Conteo

        #region Guarda_Claves_A_Contar
        private bool GuardaClavesConteo()
        {
            bool bRegresa = true;
            string sSql = "";
            string sClaveSSA = "";
            bool bValidado = false;

            for (int i = 1; i <= Grid.Rows; i++)
            {
                sClaveSSA = Grid.GetValue(i, (int)Cols.ClaveSSA);
                bValidado = Grid.GetValueBool(i, (int)Cols.Validado);

                if (bValidado)
                {

                    sSql = string.Format(" Exec spp_Mtto_Inv_Ciclicos_Conteos_Det @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', " +
                                         "@FolioCiclico = '{3}', @FolioConteo = '{4}', @ClaveSSA = '{5}', @IdProducto = '{6}', @CodigoEAN = '{7}', " +
                                         "@ClaveLote = '{8}', @IdPasillo = {9}, @IdEstante = {10}, @IdEntrepaño = {11}, " +
                                         "@ExistenciaLogica = {12}, @CantidadContada = {13}, @Observaciones = '{14}', @Validado = '{15}'",
                                          sEmpresa, sEstado, sFarmacia, txtFolioCiclico.Text, txtFolioConteo.Text, Grid.GetValue(i, (int)Cols.ClaveSSA), Grid.GetValue(i, (int)Cols.IdProducto), Grid.GetValue(i, (int)Cols.CodigoEAN),
                                          Grid.GetValue(i, (int)Cols.Lote), Grid.GetValue(i, (int)Cols.IdPasillo), Grid.GetValue(i, (int)Cols.IdEstante), Grid.GetValue(i, (int)Cols.IdEntrepaño),
                                          Grid.GetValueInt(i, (int)Cols.ExistenciaLogica), Grid.GetValueInt(i, (int)Cols.Cantidad), Grid.GetValue(i, (int)Cols.Observaciones), 1);

                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }

            }

            return bRegresa;
        }

        private void Guardar(bool CerrarFolio)
        {            
            bool bContinua = true;

            if (ValidaDatos())
            {
                if(!cnn.Abrir())
                {
                    General.msjErrorAlAbrirConexion();
                }
                else 
                {
                    
                    cnn.IniciarTransaccion();

                    bContinua = GuardaClavesConteo();

                    if (bContinua && CerrarFolio)
                    {
                        bContinua = CerrarFolioConteo();
                    }

                    if (!bContinua)
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "Guardar()");
                        General.msjError("Ocurrió un error al grabar la información");
                    }
                    else
                    {
                        cnn.CompletarTransaccion();
                        leer.Leer();
                        General.msjUser("La Información se Grabo satisfactoriamente.");
                        btnNuevo_Click(null, null);
                    }

                    cnn.Cerrar();
                }
            }
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;
            
            if (txtFolioCiclico.Text == "")
            {
                bRegresa = false;
                General.msjUser("La clave del folio es incorrecta. Verifique !!");
                txtFolioCiclico.Focus();
            }

            //if (bRegresa && Grid.Rows == 0)
            //{
            //    bRegresa = false;
            //    General.msjUser("No se encuentran claves a grabar. Verifique !! ");
            //}

            return bRegresa;
        }
        #endregion Guarda_Claves_A_Contar

        #region Imprimir
        private void Imprimir()
        {
            bool bRegresa = false;
            
            string sFolio = "", sFecha = "";

            sFolio = Fg.PonCeros(txtFolioCiclico.Text, 8);

            DatosCliente.Funcion = "btnImprimir_Click()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                
            myRpt.RutaReporte = DtGeneral.RutaReportes;

            myRpt.NombreReporte = "INV_Conteos_Ciclicos_Claves_Diario.rpt";
                
            myRpt.Add("IdEmpresa", sEmpresa);
            myRpt.Add("IdEstado", sEstado);
            myRpt.Add("IdFarmacia", sFarmacia);
            myRpt.Add("Folio", sFolio);
            myRpt.Add("FechaRegistro", sFecha);

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            if (bRegresa)
            {
                btnNuevo_Click(null, null);
            }
            else
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }
        #endregion Imprimir

        private void txtFolioConteo_Validating(object sender, CancelEventArgs e)
        {
            if (txtFolioConteo.Text.Trim() == "" || txtFolioConteo.Text.Trim() == "*")
            {
                IniciaBotones(true, false, false, false);
                txtFolioConteo.Text = "*";
                txtFolioConteo.Enabled = false;
                nmNumClaves.Enabled = true;
                nmNumClaves.Focus();
            }
            else
            {
                BuscarFolioConteo(Fg.PonCeros(txtFolioConteo.Text, 8));
            }
        }

        private void MarcarValidado()
        {
            int iRenglon = Grid.ActiveRow;

            Grid.SetValue(iRenglon, Cols.Validado, 1);

            MarcarValidado(iRenglon, false);
        }

        private void MarcarValidado(int Renglon, bool CargaInicial)
        {
            bool bEsValidado = false;
            Color cValidado = CargaInicial ? cColor_ValidadoPrevio : cColor_Validado;
            Color color = cColor_NoValidado;

            bEsValidado = Grid.GetValueBool(Renglon, Cols.Validado);

            color = bEsValidado ? cValidado : cColor_NoValidado;


            if (CargaInicial)
            {
                Grid.ColorRenglon(Renglon, color);
            }
            else
            {
                Grid.ColorCelda(Renglon, (int)Cols.Cantidad, color);
                Grid.ColorCelda(Renglon, (int)Cols.Observaciones, color);
            }

        }

        private void grdDetalles_EditModeOff(object sender, EventArgs e)
        {
            try
            {
                Cols iCol = (Cols)Grid.ActiveCol;
                switch (iCol)
                {
                    case Cols.Cantidad:
                    case Cols.Observaciones:
                        MarcarValidado();
                        break;
                }
            }
            catch (Exception ex)
            {
                //General.msjError("01 "  + ex.Message); 
            }
        }

        private void btnCerrarFolio_Click(object sender, EventArgs e)
        {
            Guardar(true);
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            if (GenerarInformacionExcel())
            {
                ExportarExcel();
            }
        }

        private bool GenerarInformacionExcel()
        {
            string sSql = "";
            bool bRegresa = false;

            string sFolioCiclico = txtFolioCiclico.Text.Trim();
            string sFolioConteo = txtFolioConteo.Text.Trim();

            sFolioConteo = sFolioConteo == "*" ? "" : sFolioConteo;


            sSql = string.Format("Exec spp_Mtto_Inv_Ciclicos____Listado \n" +
                "@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', " +
                "@FolioCiclico = '{3}', @FolioConteo = '{4}' " ,

                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                sFolioCiclico, sFolioConteo
                );

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "GenerarInformacionExcel()");
                General.msjError("Ocurrió un error al obtener la información de inventario.");
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjUser("No se encontró información.");
                }
                else
                {
                    bRegresa = true;
                }
            }

            return bRegresa;
        }

        private void ExportarExcel()
        { 
            string sAño = "", sNombre = "Reporte de inventario ciclico", sNombreHoja = "Hoja1";

            int iRow = 2, iColBase = 2, iColsEncabezado = 0, iRenglon = 0;

            // bloqueo principal 
            Cursor.Current = Cursors.Default;

            clsGenerarExcel generarExcel = new clsGenerarExcel();

            leer.RegistroActual = 1;


            iColsEncabezado = iRow + leer.Columnas.Length - 1;
            iColsEncabezado = iRow + 8;

            generarExcel.RutaArchivo = @"C:\\Excel";
            generarExcel.NombreArchivo = sNombre;
            generarExcel.AgregarMarcaDeTiempo = true;

            if (generarExcel.PrepararPlantilla(sNombre))
            {
                sNombreHoja = "Hoja1";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, 4, iColBase, iColsEncabezado, 16, DtGeneral.FarmaciaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, sNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, 6, iColBase, iColsEncabezado, 14, string.Format("Fecha de generación: {0} ", General.FechaSistema), XLAlignmentHorizontalValues.Left);


                iRenglon = 8;
                generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);
                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(iColBase, iColBase).Width = 30;

                generarExcel.CerraArchivo();

                generarExcel.AbrirDocumentoGenerado(true);
            }
        }

        private void FrmInvConteosCiclicos_Shown(object sender, EventArgs e)
        {
            if (!bEsModuloValido)
            {
                General.msjAviso("El opción solicitada no esta habilitada para este módulo.");
                this.Close();
            }
        }

        #region Personal Relacionado 
        private void txtIdPersonal_TextChanged( object sender, EventArgs e )
        {
            lblPersonal.Text = "";
        }

        private void txtIdPersonal_KeyDown( object sender, KeyEventArgs e )
        {
            if(e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.PersonalCEDIS(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, 
                    Puestos_CEDIS.InventariosCiclicos, "txtIdPersonal_KeyDown");
                if(leer.Leer())
                {
                    CargarDatosDePersonalRelacionado();
                }
            }
        }
        private void txtIdPersonal_Validating( object sender, CancelEventArgs e )
        {
            if(txtIdPersonal.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.PersonalCEDIS(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtIdPersonal.Text, 
                    Puestos_CEDIS.InventariosCiclicos, "txtIdPersonal_Validating");
                if(leer.Leer())
                {
                    CargarDatosDePersonalRelacionado();
                }
            }
        }

        private void CargarDatosDePersonalRelacionado()
        {
            txtIdPersonal.Text = leer.Campo("IdPersonal_Relacionado");
            lblPersonal.Text = leer.Campo("Personal");
        }
        #endregion Personal Relacionado 
    }
}
