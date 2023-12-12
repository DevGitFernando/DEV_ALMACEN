using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Data;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Procesos;
using DllFarmaciaSoft.ExportarExcel;

namespace Almacen.Pedidos.Validacion
{
    public partial class FrmPedidosValidacionCiega : FrmBaseExt
    {
        private enum Cols
        {
            Ninguna = 0,
            CodEAN = 1, Codigo = 2, Descripcion = 3, TasaIva = 4, Cantidad = 5
        }

        Cols ColActiva = Cols.Ninguna;
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer LeerRPT;

        string sFolioSurtido = "";
        string sCodigoEAN_Seleccionado = "";

        clsGrid myGrid;

        DllFarmaciaSoft.clsAyudas Ayuda; // = new clsAyudas();
        DataSet dtsCargarDatos = new DataSet();
        DllFarmaciaSoft.clsConsultas Consultas;

        clsCodigoEAN EAN = new clsCodigoEAN();

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        bool bCuadrado = false;

        ClsValidarLotes Lotes; // = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);


        public FrmPedidosValidacionCiega()
        {
            InitializeComponent();

            Ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Consultas = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, false);

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            grdProductos.EditModeReplace = true;
            myGrid.BackColorColsBlk = Color.White;

            leer = new clsLeer(ref cnn);
            LeerRPT = new clsLeer(ref cnn);
        }

        private void FrmPedidosValidacionCiega_Load(object sender, EventArgs e)
        {
            limpiar();
        }

        public void CargarPedido(string FolioSurtido, int TipoCaptura)
        {
            sFolioSurtido = FolioSurtido;

            this.Text += sFolioSurtido;

            this.ShowDialog();
        }

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
                //General.msjError("01 "  + ex.Message); 
            }
        }

        private void ObtenerDatosProducto()
        {
            ObtenerDatosProducto(myGrid.ActiveRow, true);
        }

        private void ObtenerDatosProducto(int Renglon, bool BuscarInformacion)
        {
            ColActiva = (Cols)myGrid.ActiveCol;
            bool bEsEAN_Unico = true;

            switch (ColActiva)
            {
                case Cols.CodEAN:
                    string sValor = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);
                    if (sValor != "")
                    {
                        if (EAN.EsValido(sValor))
                        {
                            leer.DataSetClase = Consultas.ProductosEstado(sEmpresa, sEstado, sValor, "grdProductos_EditModeOff");
                            if (leer.Leer())
                            {
                                if (!GnFarmacia.ValidarSeleccionCodigoEAN(sValor, ref sValor, ref bEsEAN_Unico))
                                {
                                    myGrid.LimpiarRenglon(myGrid.ActiveRow);
                                    myGrid.SetActiveCell(myGrid.ActiveRow, (int)Cols.CodEAN);
                                }
                                //else
                                //{
                                if (!bEsEAN_Unico)
                                {
                                    leer.GuardarDatos(1, "CodigoEAN", sValor);
                                    //leer.DataSetClase = query.ProductosFarmacia(sEmpresa, sEstado, sFarmacia, sValor, "grdProductos_EditModeOff");
                                }

                                ////    CargarDatosProducto();
                                ////}
                                sValor = leer.Campo("CodigoEAN");
                                ObtenerInformacion(sValor);
                            }
                            else
                            {
                                myGrid.LimpiarRenglon(myGrid.ActiveRow);
                                myGrid.SetActiveCell(myGrid.ActiveRow, (int)Cols.CodEAN);
                            }
                        }
                        else
                        {
                            //General.msjError(sMsjEanInvalido);
                            myGrid.LimpiarRenglon(myGrid.ActiveRow);
                            myGrid.ActiveCelda(myGrid.ActiveRow, (int)Cols.CodEAN);
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

        private void ObtenerInformacion(string sValor)
        {
            bool bEsEAN_Unico = true;

            if (EAN.EsValido(sValor))
            {
                leer.DataSetClase = Consultas.ProductosEstado(sEmpresa, sEstado, sValor, "grdProductos_EditModeOff");
                if (leer.Leer())
                {
                    if (!GnFarmacia.ValidarSeleccionCodigoEAN(sValor, ref sValor, ref bEsEAN_Unico))
                    {
                        myGrid.LimpiarRenglon(myGrid.ActiveRow);
                        myGrid.SetActiveCell(myGrid.ActiveRow, (int)Cols.CodEAN);
                    }
                    else
                    {
                        if (!bEsEAN_Unico)
                        {
                            leer.GuardarDatos(1, "CodigoEAN", sValor);
                            //leer.DataSetClase = query.ProductosFarmacia(sEmpresa, sEstado, sFarmacia, sValor, "grdProductos_EditModeOff");
                        }

                        CargarDatosProducto();
                    }
                }
                else
                {
                    myGrid.LimpiarRenglon(myGrid.ActiveRow);
                    myGrid.SetActiveCell(myGrid.ActiveRow, (int)Cols.CodEAN);
                }
            }
            else
            {
                //General.msjError(sMsjEanInvalido);
                myGrid.LimpiarRenglon(myGrid.ActiveRow);
                myGrid.ActiveCelda(myGrid.ActiveRow, (int)Cols.CodEAN);
                SendKeys.Send("");
            }
        }

        private bool CargarDatosProducto()
        {
            return CargarDatosProducto(myGrid.ActiveRow);
        }

        private bool CargarDatosProducto(int Renglon)
        {
            bool bRegresa = true;
            int iRow = Renglon;
            int iColEAN = (int)Cols.CodEAN;
            bool bEsMach4 = false;
            string sCodEAN = leer.Campo("CodigoEAN");
            bool bAgregar = true;

            if (!bAgregar)
            {
                General.msjAviso("La Unidad destino no maneja controlados, no es posible agregar el producto.");
                myGrid.LimpiarRenglon(iRow);
                myGrid.SetActiveCell(iRow, iColEAN);
            }
            else
            {
                if (!myGrid.BuscaRepetido(sCodEAN, iRow, iColEAN))
                {
                    // No modificar la informacion capturada en el renglon si este ya existia
                    myGrid.SetValue(iRow, iColEAN, sCodEAN);
                    myGrid.SetValue(iRow, (int)Cols.Descripcion, leer.Campo("Descripcion"));
                    myGrid.SetValue(iRow, (int)Cols.TasaIva, leer.Campo("TasaIva"));

                    myGrid.SetValue(iRow, (int)Cols.Codigo, leer.Campo("IdProducto"));


                    //if (!bImplementaCodificacion)
                    {
                        mostrarOcultarLotes();
                    }
                }
                else
                {
                    General.msjUser("El producto ya fue capturado en otro renglon, verifique.");
                    myGrid.LimpiarRenglon(iRow);
                    myGrid.SetActiveCell(iRow, iColEAN);
                    myGrid.EnviarARepetido();
                }
            }

            return bRegresa;
        }

        private void mostrarOcultarLotes()
        {
            // Asegurar que el Grid tenga el Foco.
            if (this.ActiveControl.Name.ToUpper() == grdProductos.Name.ToUpper())
            {
                int iRow = myGrid.ActiveRow;

                if (myGrid.GetValue(iRow, (int)Cols.Codigo) != "")
                {
                    Lotes.Codigo = myGrid.GetValue(iRow, (int)Cols.Codigo);
                    Lotes.CodigoEAN = myGrid.GetValue(iRow, (int)Cols.CodEAN);
                    Lotes.Descripcion = myGrid.GetValue(iRow, (int)Cols.Descripcion);

                    Lotes.Show();
                    

                    myGrid.SetValue(iRow, (int)Cols.Cantidad, Lotes.Cantidad);
                }
                else
                {
                    myGrid.SetActiveCell(iRow, (int)Cols.CodEAN);
                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void limpiar()
        {
            Lotes = new ClsValidarLotes();
            myGrid.Limpiar(true);
        }

        private void grdProductos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if (myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN) != "")
            {
                myGrid.Rows = myGrid.Rows + 1;
                myGrid.ActiveRow = myGrid.Rows;
                myGrid.SetActiveCell(myGrid.Rows, 1);
            }
        }

        private void FrmPedidosValidacionCiega_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F7:
                    mostrarOcultarLotes();
                    break;

                default:
                    // base.OnKeyDown(e);
                    break;
            }
        }

        private void grdProductos_KeyDown(object sender, KeyEventArgs e)
        {
            ColActiva = (Cols)myGrid.ActiveCol;
            int iRowActivo = myGrid.ActiveRow;
            string sValorGrid = "";

            if (e.KeyCode == Keys.F1)
            {
                sValorGrid = myGrid.GetValue(myGrid.ActiveRow, Cols.CodEAN);
                sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);
                leer.DataSetClase = Ayuda.ProductosEstado(sEmpresa, sEstado, "grdProductos_KeyDown_1");
                if (leer.Leer())
                {
                    myGrid.SetValue(myGrid.ActiveRow, 1, leer.Campo("CodigoEAN"));
                    ObtenerDatosProducto();
                    //CargarDatosProducto();
                }
            }

            if (e.KeyCode == Keys.Delete)
            {
                removerLotes();
            }
        }

        private void removerLotes()
        {
            try
            {
                int iRow = myGrid.ActiveRow;
                Lotes.RemoveLotes(myGrid.GetValue(iRow, (int)Cols.Codigo), myGrid.GetValue(iRow, (int)Cols.CodEAN));
                myGrid.DeleteRow(iRow);
            }
            catch { }

            if (myGrid.Rows == 0)
            {
                myGrid.Limpiar(true);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bContinua = false;
            bool bBtnGuardar = btnGuardar.Enabled;

            if (ValidaDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();


                    if (Borrar())
                    {
                        if(Guardar())
                        {
                            bContinua = ReporteDeValidacion();
                        }
                    }

                    if (bCuadrado)
                    {
                        if (Confirmar_Envio_Validacion())
                        {
                            bContinua = Enviar_A_Validacion();
                        }
                    }

                    if (bContinua)
                    {
                        cnn.CompletarTransaccion();
                        //General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        //ImprimirInformacion();

                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al guardar la información.");
                    }
                    cnn.Cerrar();
                }
                else
                {
                    Error.LogError(cnn.MensajeError);
                    General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo.");
                }

                if(!bCuadrado)
                {
                    ExportarExcel();
                }
            }
        }

        private bool ValidaDatos()
        {
            bool bRegresa = false;

            if (myGrid.GetValueInt(1, (int)Cols.Cantidad) > 0)
            {
                bRegresa = true;
            }

            return bRegresa;
        }

        private bool Borrar()
        {
            bool bRegresa = true;

            string sSql = String.Format(" Delete Pedidos_Cedis_Validacion Where IdEmpresa = '{0}'And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioSurtido = '{3}'",
                                         sEmpresa, Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4), sFolioSurtido);
            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }

            return bRegresa;
        }


        private bool Confirmar_Envio_Validacion()
        {
            bool bRegresa = false;
            string message = "El folio de surtido se enviara de Validación a Documentación, ¿ Desea continuar ? ";

            bRegresa = General.msjConfirmar(message) == DialogResult.Yes;

            return bRegresa;
        }

        private bool Enviar_A_Validacion()
        {
            bool bRegresa = false;
            string message = "El folio de surtido se enviara de Validación a Distribución, ¿ Desea continuar ? ";

            ////if (General.msjConfirmar(message) == DialogResult.Yes)
            {
                string sSql = string.Format(
                    "Update Pedidos_Cedis_Enc_Surtido Set Status = 'D' " +
                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioSurtido = '{3}' ",
                sEmpresa, sEstado, sFarmacia, sFolioSurtido);

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "CargarFoliosSurtido()");
                    General.msjError("Ocurró un error al actualizar el status del Folio de Surtido.");
                    // this.Close();
                }
                else
                {
                    bRegresa = true;
                    General.msjAviso("El folio de surtido ha sido enviado a distribución exitosamente.");
                    this.Hide();
                }
            }

            return bRegresa;
        }

        private bool Guardar()
        {
            bool bRegresa = true;
            string sSql = "", sIdProducto = "", sCodigoEAN = "";  // , sClaveLote = "";


            for (int i = 1; i <= myGrid.Rows; i++)
            {
                sCodigoEAN = myGrid.GetValue(i, (int)Cols.CodEAN);
                sIdProducto = myGrid.GetValue(i, (int)Cols.Codigo);
                //ObtieneClaveLote(sIdProducto, sCodigoEAN, ref sClaveLote);

                ClsValidarLotes[] ListaLotes = Lotes.Lotes(sIdProducto, sCodigoEAN);

                foreach (ClsValidarLotes L in ListaLotes)
                {
                    if (sIdProducto != "" && L.Cantidad > 0)
                    {
                        sSql = String.Format(" Set DateFormat YMD " +
                                             "EXEC spp_Pedidos_Cedis_Validacion @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioSurtido = '{3}', " +
                                             "   @IdProducto = '{4}', @CodigoEAN = '{5}', @ClaveLote = '{6}', @Cantidad = '{7}'",
                                                sEmpresa, Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4), sFolioSurtido,
                                                sIdProducto, sCodigoEAN, L.ClaveLote, L.Cantidad);
                        if (!leer.Exec(sSql))
                        {
                            bRegresa = false;
                            break;
                        }
                    }
                }
            }
            return bRegresa;
        }

        private bool  ReporteDeValidacion()
        {
            bool bRegresa = true;

            string sSql = String.Format(" Set DateFormat YMD " +
                     "EXEC spp_RPT_Pedidos_Cedis_Validacion @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioSurtido = '{3}' ",
                        sEmpresa, Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4), sFolioSurtido);

            if (!LeerRPT.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                bCuadrado = LeerRPT.Registros > 0  ? false:true;
            }

            return bRegresa;
        }

        private bool ExportarExcel()
        {
            bool bRegresa = false;

            string sNombreHoja = "Hoja";
            string sConcepto = "";

            int iRen = 2, iCol = 2, iColEnc = iCol + LeerRPT.Columnas.Length - 1;

            string sEmpresaNom = DtGeneral.EmpresaConectadaNombre;
            string sEstadoNom = DtGeneral.EstadoConectadoNombre;
            string sFarmaciaNom = DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre + ", " + sEstadoNom;
            string sFechaImpresion = "Fecha de Impresión: " + General.FechaSistemaFecha.ToString();

            clsGenerarExcel excel = new clsGenerarExcel();

            if(excel.PrepararPlantilla())
            {
                excel.ArchivoExcel.Worksheets.Add(sNombreHoja);


                sConcepto = "Diferencias de la validación de la orden de surtimiento: " + sFolioSurtido;


                excel.EscribirCeldaEncabezado(sNombreHoja, iRen++, iCol, iColEnc, sEmpresaNom);
                excel.EscribirCeldaEncabezado(sNombreHoja, iRen++, iCol, iColEnc, sFarmaciaNom);
                excel.EscribirCeldaEncabezado(sNombreHoja, iRen++, iCol, iColEnc, sConcepto);
                iRen++;
                excel.InsertarTabla(sNombreHoja, iRen, 2, LeerRPT.DataSetClase);
                excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                excel.CerraArchivo();

                excel.AbrirDocumentoGenerado(true);
            }

            return bRegresa;
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            string sSql = string.Format("Select V.CodigoEAN, V.IdProducto, Descripcion, TasaIva, Sum(V.Cantidad) As Cantidad " +
                    "From Pedidos_Cedis_Validacion V(NoLock) " +
                    "Inner Join vw_Productos_CodigoEAN P (NoLock)On(V.IdProducto = P.Idproducto And V.CodigoEAN = P.CodigoEAN) " +
                    "Where IdEmpresa = '{0}' and IdEstado = '{1}' And IdFarmacia = '{2}' And FolioSurtido = '{3}' " +
                    "Group By V.CodigoEAN, V.IdProducto, Descripcion, TasaIva " +

                    "Select V.CodigoEAN, V.IdProducto, V.ClaveLote, V.Cantidad " +
                    "From Pedidos_Cedis_Validacion V(NoLock) " +
                    "Where IdEmpresa = '{0}' and IdEstado = '{1}' And IdFarmacia = '{2}' And FolioSurtido = '{3}'",
                    sEmpresa, sEstado, sFarmacia, sFolioSurtido);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "btnProcesar_Click()");
                General.msjError("No fue posible Obtener la Información, intente de nuevo.");
            }
            else
            {
                if (leer.Registros == 0)
                {
                    General.msjAviso("No existe Información previa.");
                }
                else
                {
                    myGrid.LlenarGrid(leer.DataSetClase, false, false);

                    leer.DataTableClase = leer.Tabla(2);

                    Lotes.AddLotes(leer.DataSetClase);
                }
            }
        }
    }
}
