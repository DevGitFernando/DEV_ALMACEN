using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Procesos;

namespace Dll_SII_INadro.InventariosUnidades
{
    public partial class FrmInventariosOperacionMaquila : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leer2;
        clsGrid myGrid;

        DllFarmaciaSoft.clsConsultas query;
        DllFarmaciaSoft.clsAyudas ayuda;
        clsDatosCliente DatosCliente;
       
        clsCodigoEAN EAN = new clsCodigoEAN();

        private enum Cols
        {
            Ninguna = 0,
            EAN = 1, Descripcion = 2, Presentacion = 3, Existencia = 4, Cant = 5
        }

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sFolio = "";
        string sMensaje = "";
        string sValorGrid = "";
        Cols ColActiva = Cols.Ninguna;

        bool bBloqueado = false;
        bool bDiferencias = false;

        int iTpoImpresion = 0;

        public FrmInventariosOperacionMaquila()
        {
            InitializeComponent();

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            
            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            myGrid.BackColorColsBlk = Color.White;
            grdProductos.EditModeReplace = true;

            leer = new clsLeer(ref cnn);
            leer2 = new clsLeer(ref cnn);
            query = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, true);
            ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, true);

            Error = new clsGrabarError(General.DatosConexion, GnFarmacia.DatosApp, this.Name);

            myGrid.SetOrder(true); 
        }

        private void FrmInventariosOperacionMaquila_Load(object sender, EventArgs e)
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
            bool bContinua = false;
            bool bPermanecerEnPantalla = false;

            string sMensajeMostrar = ""; 
            string sMessage_ConteoConDiferencias = " Se encontraron diferencias al realizar el conteo.";
            string sMessage_ConteoSinDiferencias = " No se encontraron diferencias al realizar el conteo se dara por terminado.";
            string sMessage_ConteoTerminado = " Conteos de inventario Terminados.";
            string sFolio_Mensaje = "¿ Desea seguir capturando información del folio ? " ; 

            EliminarRenglonesVacios();
            if (ValidaDatos())
            {
                if (!cnn.Abrir())
                {
                    General.msjErrorAlAbriConexion(); 
                }
                else 
                {
                    cnn.IniciarTransaccion();

                    bContinua = GrabarEncabezado();

                    if (bContinua)
                    {
                        bContinua = VerificacionConteos();
                    }

                    if (!bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                    {
                        Error.GrabarError(leer, "btnGuardar_Click");
                        cnn.DeshacerTransaccion();
                        General.msjError("Ocurrió un error al guardar la información.");
                    }
                    else 
                    {
                        txtFolio.Text = sFolio;
                        cnn.CompletarTransaccion();

                        sFolio_Mensaje = string.Format("¿ Desea seguir capturando información del folio {0} ? ", sFolio); 
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        if (!chkConteo3.Checked)
                        {
                            if (bDiferencias)
                            {
                                sMensajeMostrar = sMessage_ConteoConDiferencias; 
                            }
                            else
                            {
                                sMensajeMostrar = sMessage_ConteoSinDiferencias; 
                            }
                        }
                        else
                        {
                            sMensajeMostrar = sMessage_ConteoTerminado; 
                        }


                        sMensajeMostrar = string.Format("{0}\n\n{1}", sMensajeMostrar, sFolio_Mensaje); 
                        if (General.msjConfirmar(sMensajeMostrar) == DialogResult.Yes)
                        {
                            txtFolio.Text = sFolio;
                            txtFolio_Validating(null, null);
                        }
                        else
                        {
                            btnNuevo_Click(this, null);
                        }
                    }

                    cnn.Cerrar();
                }
            }            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir();
        }

        private void btnImprimirReportesInternos_Click(object sender, EventArgs e)
        {
            Imprimir_Conteos_Inv(1);
            Imprimir_Inv_Fisico(1);
        }

        private void btnImprimirReportesCliente_Click(object sender, EventArgs e)
        {
            Imprimir_Conteos_Inv(2);
            Imprimir_Inv_Fisico(2);
        }
        #endregion Botones

        #region Evento_Folio
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            string sSql = "";

            if (txtFolio.Text.Trim() == "")
            {
                txtFolio.Text = "*";
                txtFolio.Enabled = false;
                myGrid.Limpiar(true);
                IniciarToolBar(true, false, false);                
            }
            else
            {
                sSql = string.Format(" Select * From INT_ND_INV_OperacionMaquilaEnc (Nolock) " +
	                                 " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
                                     sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text, 8));

                if (!leer2.Exec(sSql))
                {
                    Error.GrabarError(leer2, "txtFolio_Validating");
                    General.msjError("Ocurrio un error al consultar los datos del Folio");
                }
                else
                {
                    if (leer2.Leer())
                    {
                        CargarEncabezado();

                        txtEAN.Focus();
                    }
                    else
                    {
                        General.msjAviso("No existe información del folio capturado. Verifique !!");
                        txtFolio.Focus();
                    }
                }

            }
        }

        private void CargarEncabezado()
        {
            string sStatus = "";

            txtFolio.Text = leer2.Campo("Folio");
            txtFolio.Enabled = false;

            dtpFechaRegistro.Value = leer2.CampoFecha("FechaRegistro");
            txtObservaciones.Text = leer2.Campo("Observaciones");
            txtObservaciones.Enabled = false;
            sStatus = leer2.Campo("Status");
            IniciarToolBar(true, true, true);

            iTpoImpresion = 1;

            if (sStatus == "C")
            {
                IniciarToolBar(true, false, true);
                lblCancelado.Visible = true;
                lblCancelado.Text = "CANCELADO";
            }

            if (sStatus == "T")
            {
                IniciarToolBar(false, false, true);
                lblCancelado.Visible = true;
                lblCancelado.Text = "TERMINADO";
                CargarDetalle(2);
                myGrid.BloqueaColumna(true, (int)Cols.Cant);
                btnProductosNE.Enabled = true;
                iTpoImpresion = 2;
            }
            else
            {
                CargarDetalle(1);
            }

            HabilitarFrames();

        }

        private void CargarDetalle(int Opcion)
        {
            string sSql = "";
            string sCampo = "";
            DataSet dtsProductos = new DataSet();
            DataSet dtsFolio = new DataSet();           

            clsLeer leerFolio = new clsLeer();

            if (Opcion == 1)
            {
                sCampo = " 0 as Cantidad ";

                sSql = string.Format(" Select I.CodigoEAN, I.Descripcion, I.Presentacion, I.ExistenciaLogica, {4} " +
                                    " From vw_INT_ND_INV_OperacionMaquilaDet I (Nolock) " +
                                    " Where I.IdEmpresa = '{0}' and I.IdEstado = '{1}' and I.IdFarmacia = '{2}' and I.Folio = '{3}' " +
                                    " and I.ExistenciaLogica <> I.ExistenciaFinal " +
                                    " Order By I.Descripcion \n " + 
                                    " Select Top 1 EsConteo1, EsConteo2, EsConteo3 From INT_ND_INV_OperacionMaquilaDet (Nolock) " +
                                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
                                    sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text, 8), sCampo);
            }

            if (Opcion == 2)
            {
                sCampo = " I.ExistenciaFinal as Cantidad ";

                sSql = string.Format(" Select I.CodigoEAN, I.Descripcion, I.Presentacion, I.ExistenciaLogica, {4} " +
                                    " From vw_INT_ND_INV_OperacionMaquilaDet I (Nolock) " +
                                    " Where I.IdEmpresa = '{0}' and I.IdEstado = '{1}' and I.IdFarmacia = '{2}' and I.Folio = '{3}' " +
                                    " Order By I.Descripcion \n " + 
                                    " Select Top 1 EsConteo1, EsConteo2, EsConteo3 From INT_ND_INV_OperacionMaquilaDet (Nolock) " +
                                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
                                    sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text, 8), sCampo);
            }
            

            myGrid.Limpiar(false);

            if (!leer2.Exec(sSql))
            {
                Error.GrabarError(leer2, "CargarDetalle");
                General.msjError("Ocurrio un error al consultar los detalles del Folio");
            }
            else
            {
                if (leer2.Leer())
                {
                    dtsProductos.Tables.Add(leer2.Tabla(1).Copy());
                    dtsFolio.Tables.Add(leer2.Tabla(2).Copy());

                    myGrid.LlenarGrid(dtsProductos, false, false);

                    leerFolio.DataSetClase = dtsFolio;

                    leerFolio.Leer();                  

                    chkConteo1.Checked = leerFolio.CampoBool("EsConteo1"); 
                    chkConteo2.Checked = leerFolio.CampoBool("EsConteo2");
                    chkConteo3.Checked = leerFolio.CampoBool("EsConteo3");

                    FrameConteos.Enabled = false;

                    if (chkConteo1.Checked || chkConteo2.Checked || chkConteo3.Checked)
                    {
                        bBloqueado = true;
                    }

                    myGrid.BloqueaColumna(true, (int)Cols.EAN);
                }
            }
            
        }
        #endregion Evento_Folio

        #region Funciones
        private void LimpiaPantalla()
        {
            myGrid.Limpiar(false);
            Fg.IniciaControles(this, true);            
            sMensaje = "";
            TituloCapturaHabilitada(2); 

            dtpFechaRegistro.Enabled = false;
            FrameConteos.Enabled = false;

            chkActivar.Enabled = false; 
            ////FrameAgregar.Enabled = false;
            IniciarToolBar(false, false, false);
            btnProductosNE.Enabled = false;
            bBloqueado = false;
            bDiferencias = false;
            myGrid.BloqueaColumna(false, (int)Cols.Cant);
            iTpoImpresion = 0;
            txtEAN.Enabled = false;
            txtFolio.Focus();
        }

        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
            btnImprimirReportesCliente.Enabled = Imprimir;
            btnImprimirReportesInternos.Enabled = Imprimir; 
        }

        private void Desplagar_Listado_EAN()
        {
            string sSql = "";

            sSql = string.Format(" Select CodigoEAN, DescripcionProducto, Sum(Existencia) as Existencia, 0 as Conteo1, 0 as Conteo2, 0 as Conteo3 " +
		                        " From vw_ExistenciaPorCodigoEAN_Lotes (nolock) " +
                                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' " +
		                        " Group By CodigoEAN, DescripcionProducto ", sEmpresa, sEstado, sFarmacia);

            myGrid.Limpiar(false);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Desplagar_Listado_EAN");
                General.msjError("Ocurrio un error al buscar los Productos para el Inventario.");
            }
            else
            {
                if (leer.Leer())
                {
                    txtFolio.Text = "*";
                    txtFolio.Enabled = false;
                    IniciarToolBar(true, false, false);
                    myGrid.LlenarGrid(leer.DataSetClase, false, false);
                                        
                }
            }
        }
        #endregion Funciones

        #region Guardar
        private bool GrabarEncabezado()
        {
            bool bRegresa = true;
            int iConteos = 0;

            sFolio = "";

            string sSql = string.Format("Exec spp_Mtto_INT_ND_INV_OperacionMaquilaEnc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ",
                sEmpresa, sEstado, sFarmacia, txtFolio.Text.Trim(), DtGeneral.IdPersonal, txtObservaciones.Text );

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                if (leer.Leer())
                {
                    sFolio = leer.Campo("Folio");
                    sMensaje = leer.Campo("Mensaje");
                    iConteos = leer.CampoInt("Conteos");

                    if (iConteos == 1)
                    {
                        chkConteo1.Checked = true;
                    }

                    if (iConteos == 2)
                    {
                        chkConteo2.Checked = true;
                    }

                    if (iConteos == 3)
                    {
                        chkConteo3.Checked = true;
                    }

                    bRegresa = GrabarDetalle();
                }
            }

            return bRegresa;
        }

        private bool GrabarDetalle()
        {
            bool bRegresa = true, bSigue = true;
            string sSql = "";
            string sCodigoEAN = "";
            int iExistenciaLogica = 0;
            int iConteo = 0, iTipo = 0;

            if (chkConteo1.Checked)
            {
                iTipo = 1;
                //bSigue = false;
            }

            if (chkConteo2.Checked)
            {
                iTipo = 2;
                //bSigue = false;
            }

            if (chkConteo3.Checked)
            {
                iTipo = 3;                
            }

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                sCodigoEAN = myGrid.GetValue(i, (int)Cols.EAN);                
                iExistenciaLogica = myGrid.GetValueInt(i, (int)Cols.Existencia);
                iConteo = myGrid.GetValueInt(i, (int)Cols.Cant);

                if (sCodigoEAN != "")
                {
                    sSql = string.Format("Exec spp_Mtto_INT_ND_INV_OperacionMaquilaDet '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}'  ",
                        sEmpresa, sEstado, sFarmacia, sFolio, sCodigoEAN, iExistenciaLogica, iConteo, iTipo);
                   
                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }                    
                }
            }

            if (bRegresa)
            {
                if (iTipo == 1)
                {
                    bRegresa = Agregar_Productos_Unidad();
                }
            }

            if (bRegresa)
            {
                bRegresa = MarcarConteos(iTipo);
            }

            return bRegresa;
        }

        private bool Agregar_Productos_Unidad()
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format(" Exec spp_INT_ND_AgregarProductos_INV_OperacionMaquilaDet '{0}', '{1}', '{2}', '{3}' ",
                                sEmpresa, sEstado, sFarmacia, sFolio);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                //Error.GrabarError(leer, "Agregar_Productos_Unidad");                
            }

            return bRegresa;
        }

        private bool MarcarConteos(int Tipo)
        {
            bool bRegresa = true;
            string sSql = "", sCampo = "";

            if (Tipo == 1)
            {
                sCampo = "EsConteo1";
            }

            if (Tipo == 2)
            {
                sCampo = "EsConteo2";
            }

            if (Tipo == 3)
            {
                sCampo = "EsConteo3";
            }

            sSql = string.Format(" Update INT_ND_INV_OperacionMaquilaDet Set {4} = 1 " +
                                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
                                sEmpresa, sEstado, sFarmacia, sFolio, sCampo);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                //Error.GrabarError(leer, "MarcarConteos");
            }

            return bRegresa;
        }

        private bool VerificacionConteos()
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format(" Exec spp_INT_ND_Verificar_Conteos_Inventario '{0}', '{1}', '{2}', '{3}' ",
                                sEmpresa, sEstado, sFarmacia, sFolio );

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                if (leer.Leer())
                {
                    if (leer.CampoInt("Resultado") == 1)
                    {
                        bDiferencias = false;
                    }
                    else
                    {
                        bDiferencias = true;
                    }
                }
            }

            return bRegresa;
        }
        #endregion Guardar

        private void EliminarRenglonesVacios()
        {
            for (int i = 1; i <= myGrid.Rows; i++) //Renglones.
            {
                if (myGrid.GetValue(i, 2).Trim() == "") //Si la columna oculta IdProducto esta vacia se elimina.
                    myGrid.DeleteRow(i);
            }

            if (myGrid.Rows == 0) // Si No existen renglones, se inserta 1.
            {
                myGrid.AddRow();
            }
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtFolio.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Folio inválido, verifique.");
                txtFolio.Focus();
            }

            return bRegresa;
        }

        #region Eventos_Grid
        private void grdProductos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if (!bBloqueado)
            {
                if ((myGrid.ActiveRow == myGrid.Rows) && e.AdvanceNext)
                {
                    if (myGrid.GetValue(myGrid.ActiveRow, (int)Cols.EAN) != "" )
                    {
                        myGrid.Rows = myGrid.Rows + 1;
                        myGrid.ActiveRow = myGrid.Rows;
                        myGrid.SetActiveCell(myGrid.Rows, 1);
                        myGrid.BloqueaCelda(false, myGrid.ActiveRow, (int)Cols.EAN);
                    }
                }
            }
        }

        private void grdProductos_EditModeOff(object sender, EventArgs e)
        {
            ColActiva = (Cols)myGrid.ActiveCol;
            bool bEsEAN_Unico = true;

            switch (ColActiva)
            {
                case Cols.EAN:
                    string sValor = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.EAN);
                    if (sValor != "")
                    {
                        if (EAN.EsValido(sValor))
                        {
                            leer.DataSetClase = query.ProductosEstado(sEmpresa, sEstado, sValor, "grdProductos_EditModeOff");
                            if (leer.Leer())
                            {
                                if (!GnFarmacia.ValidarSeleccionCodigoEAN(sValor, ref sValor, ref bEsEAN_Unico))
                                {
                                    myGrid.LimpiarRenglon(myGrid.ActiveRow);
                                    myGrid.SetActiveCell(myGrid.ActiveRow, (int)Cols.EAN);
                                }
                                else
                                {
                                    if (!bEsEAN_Unico)
                                    {
                                        leer.GuardarDatos(1, "CodigoEAN", sValor);                                        
                                    }

                                    CargarDatosProducto();
                                }
                            }
                            else
                            {
                                myGrid.LimpiarRenglon(myGrid.ActiveRow);
                                myGrid.ActiveCelda(myGrid.ActiveRow, (int)Cols.EAN);
                            }
                        }
                        else
                        {
                            //General.msjError(sMsjEanInvalido);
                            myGrid.LimpiarRenglon(myGrid.ActiveRow);
                            myGrid.ActiveCelda(myGrid.ActiveRow, (int)Cols.EAN);
                            SendKeys.Send("");
                        }
                    }
                    else
                    {
                        myGrid.LimpiarRenglon(myGrid.ActiveRow);
                    }
                    break;
            }            
        }

        private void grdProductos_EditModeOn(object sender, EventArgs e)
        {
            sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.EAN);
        }

        private void grdProductos_KeyDown(object sender, KeyEventArgs e)
        {
            ColActiva = (Cols)myGrid.ActiveCol;
            switch (ColActiva)
            {
                case Cols.EAN:
                case Cols.Descripcion:       
                if (e.KeyCode == Keys.F1)
                {
                    sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.EAN);
                    leer.DataSetClase = ayuda.ProductosEstado(sEmpresa, sEstado, "grdProductos_KeyDown");
                    if (leer.Leer())
                    {
                        CargarDatosProducto();
                    }
                }

                if (e.KeyCode == Keys.Delete)
                {
                    if (myGrid.GetValue(myGrid.ActiveRow, (int)Cols.Descripcion) == "" )
                    {
                        removerProducto();
                    }
                }                        
                break;

                case Cols.Cant:
                if (txtEAN.Enabled)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        if (chkConteo1.Checked)
                        {
                            txtEAN.Focus();
                        }
                    }
                }
                break;
            }
        }

        private void removerProducto()
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

        private bool CargarDatosProducto()
        {
            bool bRegresa = true;
            int iRow = myGrid.ActiveRow;
            int iColEAN = (int)Cols.EAN;
            string sCodEAN = leer.Campo("CodigoEAN");

            if (sValorGrid != sCodEAN)
            {
                if (!myGrid.BuscaRepetido(sCodEAN, iRow, iColEAN))
                {
                    // No modificar la informacion capturada en el renglon si este ya existia
                    myGrid.SetValue(iRow, iColEAN, sCodEAN);
                    myGrid.SetValue(iRow, (int)Cols.Descripcion, leer.Campo("Descripcion"));
                    myGrid.SetValue(iRow, (int)Cols.Presentacion, leer.Campo("Presentacion"));
                    myGrid.BloqueaCelda(true, Color.WhiteSmoke, iRow, (int)Cols.EAN);

                    Obtener_Existencia_EAN(sCodEAN);
                    myGrid.SetActiveCell(iRow, (int)Cols.Cant);
                }
                else
                {
                    General.msjUser("El producto ya fue capturado en otro renglon, verifique.");
                    myGrid.LimpiarRenglon(iRow);
                    myGrid.SetActiveCell(iRow, iColEAN);
                    myGrid.EnviarARepetido();
                    EliminarRenglonesVacios();
                }
            }
            else
            {
                // Asegurar que no cambie el CodigoEAN
                myGrid.SetValue(iRow, iColEAN, sCodEAN);
            }

            return bRegresa;
        }

        private void Obtener_Existencia_EAN(string EAN)
        {
            string sSql = "";

            int iRow = myGrid.ActiveRow;

            sSql = string.Format(" Select CodigoEAN, cast(Sum(Existencia) as Int) as Existencia, 0 as Cantidad " +
                                " From vw_ExistenciaPorCodigoEAN_Lotes (nolock) " +
                                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and CodigoEAN = '{3}' " +
                                " Group By CodigoEAN ", sEmpresa, sEstado, sFarmacia, EAN);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Obtener_Existencia_EAN");
                General.msjError("Ocurrio un error al obtener la existencia del EAN.");
            }
            else
            {
                if (leer.Leer())
                {
                    myGrid.SetValue(iRow, (int)Cols.Existencia, leer.CampoInt("Existencia"));
                    myGrid.SetValue(iRow, (int)Cols.Cant, leer.CampoInt("Cantidad"));                    
                }
                else
                {
                    myGrid.SetValue(iRow, (int)Cols.Existencia, 0);
                    myGrid.SetValue(iRow, (int)Cols.Cant, 0);                    
                }
            }
        }
        #endregion Eventos_Grid

        private void TituloCapturaHabilitada(int Tipo)
        {
            if (Tipo == 1)
            {
                lblTituloCapturaHabilitada.Text = "Captura de productos habilitada"; 
            }

            if (Tipo == 2)
            {
                lblTituloCapturaHabilitada.Text = "Captura de productos deshabilitada";
            }
        }

        private void chkActivar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActivar.Checked)
            {
                bBloqueado = false;
                TituloCapturaHabilitada(1); 
                IniciarToolBar(true, false, true);
            }
            else
            {
                bBloqueado = true;
                TituloCapturaHabilitada(2); 
            }
        }

        private void HabilitarFrames()
        {
            if (chkConteo1.Checked)
            {
                chkActivar.Enabled = true; 
                ////FrameAgregar.Enabled = true;
                txtEAN.Enabled = true;
            }

            if (chkConteo2.Checked)
            {
                chkActivar.Enabled = true; 
                ////FrameAgregar.Enabled = true;
                txtEAN.Enabled = true;
            }

            if (chkConteo3.Checked)
            {
                chkActivar.Enabled = false;
                ////FrameAgregar.Enabled = false;
                txtEAN.Enabled = false;
            }
        }

        #region Impresion
        private void Imprimir()
        {
            bool bRegresa = false;
            
            DatosCliente.Funcion = "Imprimir()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            
            myRpt.RutaReporte = GnFarmacia.RutaReportes;

            if (iTpoImpresion == 1)
            {
                myRpt.NombreReporte = "INT_ND_Inv_Productos_Operacion_Maquila.rpt";
            }

            if (iTpoImpresion == 2)
            {
                myRpt.NombreReporte = "INT_ND_Inv_Operacion_Maquila_Conteo_Fisico.rpt";
            }

            myRpt.Add("@IdEmpresa", sEmpresa);
            myRpt.Add("@IdEstado", sEstado);
            myRpt.Add("@IdFarmacia", sFarmacia);
            myRpt.Add("@Folio", txtFolio.Text);            

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);
                     
            
            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
            
        }
        #endregion Impresion

        #region Eventos_CodigoEAN
        private void txtEAN_Validating(object sender, CancelEventArgs e)
        {
            string sCodEAN = "";
            int iRenglon = 0; 

            if (!txtEAN.Enabled)
            {
                txtEAN.Text = "";
                txtEAN.Focus();
            }
            else 
            {
                if (txtEAN.Text.Trim() != "")
                {
                    sCodEAN = txtEAN.Text; 
                    iRenglon = myGrid.BuscarRenglonEnGrid(sCodEAN, (int)Cols.EAN);
                    
                    if (iRenglon != 0) 
                    {
                        ////myGrid.EnviarARepetido();
                        myGrid.SetActiveCell(iRenglon, (int)Cols.EAN);
                        txtEAN.Text = "";
                    }
                    else
                    {
                        General.msjAviso("No se encontro el EAN capturado, verifique.");
                        txtEAN.Focus();
                    }
                }
            }
        }
        #endregion Eventos_CodigoEAN

        #region Impresion_Inv_Fisico
        private void FrmInventariosOperacionMaquila_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.F7:
                        //////Imprimir_Conteos_Inv(2);
                        //////Imprimir_Inv_Fisico(2);
                        break;

                    case Keys.F8:
                        //////Imprimir_Conteos_Inv(1);
                        //////Imprimir_Inv_Fisico(1);
                        break;

                    default:
                        break;
                }
            }
        }

        private void Imprimir_Inv_Fisico(int iTpoRpt)
        {
            bool bRegresa = false;

            if (lblCancelado.Text.Trim() == "TERMINADO")
            {
                DatosCliente.Funcion = "Imprimir()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);

                myRpt.RutaReporte = GnFarmacia.RutaReportes;

                if (iTpoRpt == 1)  // Reporte para la unidad
                {
                    myRpt.NombreReporte = "INT_ND_Inv_Fisico_Diferencias_Operacion_Maquila.rpt";
                }

                if (iTpoRpt == 2) // Reporte para el cliente operacion maquila
                {
                    myRpt.NombreReporte = "INT_ND_Inv_Fisico_Diferencias_Operacion_Maquila_Cliente.rpt";
                }

                myRpt.Add("@IdEmpresa", sEmpresa);
                myRpt.Add("@IdEstado", sEstado);
                myRpt.Add("@IdFarmacia", sFarmacia);
                myRpt.Add("@Folio", txtFolio.Text);

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);


                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }

        }

        private void Imprimir_Conteos_Inv(int iTpoRpt)
        {
            bool bRegresa = false;

            if (lblCancelado.Text.Trim() == "TERMINADO")
            {
                DatosCliente.Funcion = "Imprimir()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);

                myRpt.RutaReporte = GnFarmacia.RutaReportes;

                if (iTpoRpt == 1)  // Reporte para la unidad
                {
                    myRpt.NombreReporte = "INT_ND_Inv_Fisico_Conteos_Operacion_Maquila.rpt";
                }

                if (iTpoRpt == 2) // Reporte para el cliente operacion maquila
                {
                    myRpt.NombreReporte = "INT_ND_Inv_Fisico_Conteos_Operacion_Maquila_Cliente.rpt";
                }

                myRpt.Add("@IdEmpresa", sEmpresa);
                myRpt.Add("@IdEstado", sEstado);
                myRpt.Add("@IdFarmacia", sFarmacia);
                myRpt.Add("@Folio", txtFolio.Text);

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);


                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }

        }
        #endregion Impresion_Inv_Fisico

        #region Boton_Productos_No_Identificados
        private void btnProductosNE_Click(object sender, EventArgs e)
        {
            FrmInventarioFisicoNoIdentificado f = new FrmInventarioFisicoNoIdentificado();
            f.ShowDialog();
        }
        #endregion Boton_Productos_No_Identificados
    }
}
