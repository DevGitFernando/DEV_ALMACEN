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

namespace DllFarmaciaSoft.Lotes
{
    public partial class FrmMtto_Lotes : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        DllFarmaciaSoft.clsConsultas Consultas;
        DllFarmaciaSoft.clsAyudas Ayuda;
        clsLeer myLeer;
        clsLeer myLeerLotes;
        clsGrid myGrid;

        // string sFolio = "", sMensaje = "";
        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        // DateTime dFechaCaducidad_Anterior;

        private enum Cols
        {
            Ninguna = 0,
            Codigo = 1, CodigoEAN = 2, ClaveLote = 3, MesesCaducar = 4, FechaEntrada = 5,
            FechaCaducidad = 6, Existencia = 7, Status = 8, StatusAux = 9, StatusFinal = 10  
        }

        public FrmMtto_Lotes()
        {
            InitializeComponent();
            myLeer = new clsLeer(ref ConexionLocal);
            myLeerLotes = new clsLeer(ref ConexionLocal);
            Consultas = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, General.DatosApp, this.Name, true);
            Ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, General.DatosApp, this.Name, true);

            grdLotes.EditModeReplace = true;
            myGrid = new clsGrid(ref grdLotes, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
        }

        private void FrmModCaducidades_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }
        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true, FrameInformacion);
            myGrid.Limpiar(false);

            IniciarToolBar(true, false, false, false);
            txtIdProducto.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidaDatos())
            {
                if (ConexionLocal.Abrir())
                {
                    ConexionLocal.IniciarTransaccion();

                    if (GrabarLote())
                    {
                        ConexionLocal.CompletarTransaccion();
                        General.msjUser("Información guardada satisfactoriamente."); //Este mensaje lo genera el SP
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        ConexionLocal.DeshacerTransaccion();
                        Error.GrabarError(myLeer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al guardar la Información.");
                        IniciarToolBar(true, true, false, false);
                    }

                    ConexionLocal.Cerrar();
                }
                else
                {
                    Error.LogError(ConexionLocal.MensajeError);
                    General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo.");
                }
            }
        }

        private bool GrabarLote()
        {
            bool bRegresa = true;
            string sSql = "", sStatus = "";
            string sClaveLote = ""; 
            int iStatus = 0, iStatusAux = 0;

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                iStatus = myGrid.GetValueInt(i, (int)Cols.StatusAux);
                iStatusAux = myGrid.GetValueInt(i, (int)Cols.StatusFinal); 

                if ( iStatus != iStatusAux ) 
                {
                    sClaveLote = myGrid.GetValue(i, (int)Cols.ClaveLote); 
                    sStatus = iStatusAux == 0 ? "C" : "A"; 

                    sSql = string.Format("Set DateFormat YMD ");
                    sSql += string.Format( " Update F Set Status = '{6}', Actualizado = 0 " +
                            " From FarmaciaProductos_CodigoEAN_Lotes F (NoLock) " +
                            " Where F.IdEmpresa = '{0}' and F.IdEstado = '{1}' and F.IdFarmacia = '{2}' " +
                            " and F.IdProducto = '{3}' and F.CodigoEAN = '{4}' and F.ClaveLote = '{5}' ",
                            sEmpresa, sEstado, sFarmacia, txtIdProducto.Text, lblCodigoEAN.Text, sClaveLote, sStatus); 

                    if (!myLeer.Exec(sSql))
                    {
                        bRegresa = false;
                        break; 
                    }
                }
            }

            return bRegresa;
        }
        #endregion Botones

        #region Buscar Producto 
       
        private void txtIdProducto_Validating(object sender, CancelEventArgs e)
        {
            string sIdProducto = Fg.PonCeros(txtIdProducto.Text.Trim(), 8);
            if (txtIdProducto.Text.Trim() != "")
            {
                myLeer.DataSetClase = Consultas.ProductosFarmacia(sEmpresa, sEstado, sFarmacia, sIdProducto, "txtId_Validating");
                if (myLeer.Leer())
                {
                    CargaDatos();
                    IniciarToolBar(true, true, false, false);
                }
                else
                {
                    btnNuevo_Click(null, null);
                }
            }
        }

        private void CargaDatos()
        {
            txtIdProducto.Text = myLeer.Campo("IdProducto");
            lblProducto.Text = myLeer.Campo("Descripcion");
            lblCodigoEAN.Text = myLeer.Campo("CodigoEAN");
            lblPresentacion.Text = myLeer.Campo("Presentacion");
            lblContenido.Text = myLeer.Campo("ContenidoPaquete");
            lblClaveSSA.Text = myLeer.Campo("ClaveSSA");
            lblDescripcionSSA.Text = myLeer.Campo("DescripcionSal");

            txtIdProducto.Enabled = false;

            if (myLeer.Campo("StatusProducto") == "C")
            {
                IniciarToolBar(true, false, false, false);
                ////Fg.BloqueaControles(this, false, FrameDatosLotes);
                General.msjUser("Este Producto se encuentra Cancelado, por lo tanto no puede ser modificado");
            }

            Buscar_Lotes();            
        }

        private void txtIdProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayuda.ProductosFarmacia(sEmpresa, sEstado, sFarmacia, "txtId_KeyDown");

                if (myLeer.Leer())
                {
                    CargaDatos();
                }
            }
        }

        private bool Buscar_Lotes()
        {
            bool bRegresa = false;
            string sCodigo = txtIdProducto.Text.Trim();
            string sCodEAN = lblCodigoEAN.Text.Trim();

            string sSql = string.Format("Set DateFormat YMD Select L.IdProducto, L.CodigoEAN, L.ClaveLote, " +
                   " datediff(mm, getdate(), L.FechaCaducidad) as MesesCad, " +
                   " Convert( varchar(10), L.FechaRegistro, 120) as FechaReg, " + 
                   " Convert(varchar(8), L.FechaCaducidad, 120 ) as FechaCad, " +
                   " cast(L.Existencia as Int) as Existencia, " + 
                   " (Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, " + 
                   " cast((Case When L.Status = 'A' Then 1 Else 0 End) as bit ) as Status, " +
                   " cast((Case When L.Status = 'A' Then 1 Else 0 End) as bit ) as StatusAux  " + 
                   " From FarmaciaProductos_CodigoEAN_Lotes L (NoLock) " +
                   " Where IdEstado =  '{0}' and IdFarmacia = '{1}' and IdProducto = '{2}' and CodigoEAN = '{3}' ", sEstado, sFarmacia, sCodigo, sCodEAN);

            //myLeerLotes.DataSetClase = Consultas.LotesDeCodigo_CodigoEAN(sEstado, sFarmacia, sCodigo, sCodEAN, "CargarLotesCodigoEAN()");
            if (myLeerLotes.Exec(sSql))
            {
                if (myLeerLotes.Leer())
                {
                    myGrid.LlenarGrid(myLeerLotes.DataSetClase);
                }
                else
                {
                    bRegresa = false;
                    General.msjUser("El Producto ingresado no tiene registrado Lotes. Verifique.");
                    btnNuevo_Click(null, null);
                }
            }

            return bRegresa;
        }
        #endregion Buscar Producto

        #region Buscar Lote
        private void txtClaveLote_Validating(object sender, CancelEventArgs e)
        {
            ////DateTime dFechaCaducidadMaxima, dFechaCaducidadMinima;
            ////int iRenglon = 0;

            ////if (txtClaveLote.Text.Trim() != "")
            ////{
            ////    iRenglon = myGrid.BuscarRenglonEnGrid(txtClaveLote.Text.Trim(), (int)Cols.ClaveLote);

            ////    if (iRenglon != 0)
            ////    {
            ////        //Se verifica que el Lote no haya sido modificado anteriormente.
            ////        if (!Lote_Modificado_Anteriormente())
            ////        {
            ////            dFechaCaducidadMaxima = myGrid.GetValueFecha(iRenglon, (int)Cols.FechaCaducidadMaxima);
            ////            dFechaCaducidadMinima = myGrid.GetValueFecha(iRenglon, (int)Cols.FechaCaducidad);
            ////            dFechaCaducidad_Anterior = myGrid.GetValueFecha(iRenglon, (int)Cols.FechaCaducidad);

            ////            //Se asigna la Fecha de Caducidad Maxima y Minima del Lote seleccionado.
            ////            dtpFechaCaducidad.MaxDate = dFechaCaducidadMaxima;
            ////            dtpFechaCaducidad.MinDate = dFechaCaducidadMinima;

            ////            IniciarToolBar(true, true, false, false);
            ////        }
            ////    }
            ////    else
            ////    {
            ////        General.msjUser("La Clave de Lote ingresada no existe para este Producto. Verifique");
            ////        txtClaveLote.Focus();
            ////    }

            ////}
        }

        private bool Lote_Modificado_Anteriormente()
        {
            bool bRegresa = false;
            ////myLeer = new clsLeer(ref ConexionLocal);

            ////Consultas.MostrarMsjSiLeerVacio = false;
            ////myLeer.DataSetClase = Consultas.Adt_FarmaciaProductos_CodigoEAN_Lotes(sEmpresa, sEstado, sFarmacia, txtIdProducto.Text.Trim(), lblCodigoEAN.Text.Trim(), txtClaveLote.Text.Trim(), "txtFolio_Validating");
            ////if (myLeer.Leer())
            ////{
            ////    lblCorregido.Visible = true;

            ////    //Si el usuario es administrador, puede hacer cambios a la compra.
            ////    if (!DtGeneral.EsAdministrador)
            ////    {
            ////        bRegresa = true;
            ////        txtClaveLote.Text = "";
            ////        txtClaveLote.Focus();
            ////        General.msjUser("Este Lote ya ha sido corregido. Verifique");
            ////    }
            ////}

            return bRegresa;
        }
        #endregion Buscar Lote 

        #region Funciones
        private void IniciarToolBar(bool Nuevo, bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnNuevo.Enabled = Nuevo;
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
        }

        private void txtClaveLote_TextChanged(object sender, EventArgs e)
        {
            ////dtpFechaCaducidad.MinDate = DateTime.Parse("01/01/1753");
            ////dtpFechaCaducidad.MaxDate = DateTime.Parse("31/12/9998");
            ////dtpFechaCaducidad.Value = General.FechaSistema;
            ////IniciarToolBar(true, false, false, false);
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            ////if (txtClaveLote.Text.Trim() == "")
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("Clave de Lote inválida, verifique.");
            ////    txtClaveLote.Focus();
            ////}
            
            return bRegresa;
        }
        #endregion Funciones

    }
}
