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

namespace DllFarmaciaAuditor.Registros
{
    public partial class FrmModCaducidades : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        DllFarmaciaSoft.clsConsultas Consultas;
        DllFarmaciaSoft.clsAyudas Ayuda;
        clsLeer myLeer;
        clsLeer myLeerLotes;
        clsGrid myGrid;

        string sFolio = "", sMensaje = "";
        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        DateTime dFechaCaducidad_Anterior;

        #region Permisos Especiales 
        string sPermisoCaducidades = "MODIFICAR_CADUCIDADES";
        bool bPermisoCaducidades = false;

        string sPermisoCaducidades_Aux = "ADT_MODIFICAR_CADUCIDADES";
        bool bPermisoCaducidades_Aux = false;
        #endregion Permisos Especiales

        private enum Cols
        {
            Ninguna = 0,
            Codigo = 1, CodigoEAN = 2, IdSubFarmacia = 3, SubFarmacia = 4, ClaveLote = 5, MesesCaducar = 6, FechaEntrada = 7,
            FechaCaducidad = 8, Status = 9, Existencia = 10, FechaCaducidadMaxima = 11
        }

        public FrmModCaducidades()
        {
            InitializeComponent();
            myLeer = new clsLeer(ref ConexionLocal);
            myLeerLotes = new clsLeer(ref ConexionLocal);
            Consultas = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, General.DatosApp, this.Name, true);
            Ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, General.DatosApp, this.Name, true);

            grdLotes.EditModeReplace = true;
            myGrid = new clsGrid(ref grdLotes, this);
            myGrid.EstiloGrid(eModoGrid.SoloLectura);
            myGrid.AjustarAnchoColumnasAutomatico = true;

            SolicitarPermisosUsuario(); 
        }

        private void FrmModCaducidades_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Permisos de Usuario
        /// <summary>
        /// Obtiene los privilegios para el usuario conectado 
        /// </summary>
        private void SolicitarPermisosUsuario()
        {
            bPermisoCaducidades = DtGeneral.PermisosEspeciales.TienePermiso(sPermisoCaducidades);
            bPermisoCaducidades_Aux = DtGeneral.PermisosEspeciales.TienePermiso(sPermisoCaducidades_Aux); 
        }
        #endregion Permisos de Usuario

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true, FrameInformacion);
            Fg.IniciaControles(this, true, FrameDatosLotes);
            myGrid.Limpiar(false);

            lblCorregido.Text = "";

            cboSubFarmacias.Clear();
            cboSubFarmacias.Add("0", "<< Seleccione >>");
            cboSubFarmacias.SelectedIndex = 0;

            IniciarToolBar(true, false, false, false);
            CargaSubFarmacias();
            txtIdProducto.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidaDatos())
            {
                if (!ConexionLocal.Abrir())
                {
                    Error.LogError(ConexionLocal.MensajeError);
                    General.msjErrorAlAbrirConexion(); 
                }
                else 
                {
                    ConexionLocal.IniciarTransaccion();

                    if (GrabarLote())
                    {
                        ConexionLocal.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        ConexionLocal.DeshacerTransaccion();
                        Error.GrabarError(myLeer, "btnGuardar_Click");
                        General.msjError("Error al guardar la Información.");
                        IniciarToolBar(true, true, false, false);
                    }

                    ConexionLocal.Cerrar();
                } 
            }
        }

        private bool GrabarLote()
        {
            bool bRegresa = false;
            string sSql = "";
            string sFechaCaducidad = General.FechaYMD(dtpFechaCaducidad.Value); // Fg.FechaYMD(dtpFechaCaducidad.Value.ToShortDateString(), "-");
            string sFechaCaducidad_Anterior = General.FechaYMD(dFechaCaducidad_Anterior); // Fg.FechaYMD(dFechaCaducidad_Anterior.ToShortDateString(), "-");

            sSql = String.Format("Set DateFormat YMD Exec spp_Mtto_Adt_FarmaciaProductos_CodigoEAN_Lotes " +
                                "'{0}', '{1}', '{2}', " +
                                "'{3}', '{4}', '{5}', " +
                                "'{6}', '{7}', '{8}', '{9}' ",
                            sEmpresa, sEstado, sFarmacia, cboSubFarmacias.Data, txtIdProducto.Text.Trim(), lblCodigoEAN.Text.Trim(),
                            txtClaveLote.Text.Trim(), sFechaCaducidad, sFechaCaducidad_Anterior, DtGeneral.IdPersonal);
            if (myLeer.Exec(sSql))
            {
                if (myLeer.Leer())
                {
                    sFolio = myLeer.Campo("Clave");
                    sMensaje = myLeer.Campo("Mensaje");
                    bRegresa = true;
                }
            }

            return bRegresa;
        }
        #endregion Botones

        #region Buscar Producto 
       
        private void txtIdProducto_Validating(object sender, CancelEventArgs e)
        {
            string sIdProducto = Fg.PonCeros(txtIdProducto.Text.Trim(), 8);
            string sCodigoEAN_Seleccionado = ""; 

            if (txtIdProducto.Text.Trim() != "")
            {
                if (!GnFarmacia.ValidarSeleccionCodigoEAN(sIdProducto, ref sCodigoEAN_Seleccionado))
                {
                    btnNuevo_Click(null, null);
                }
                else 
                {
                    myLeer.DataSetClase = Consultas.ProductosFarmacia(sEmpresa, sEstado, sFarmacia, sCodigoEAN_Seleccionado, "txtId_Validating");
                    if (myLeer.Leer()) 
                    {
                        CargaDatos();
                    }
                    else
                    {
                        btnNuevo_Click(null, null);
                    }
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
                Fg.BloqueaControles(this, false, FrameDatosLotes);
                General.msjUser("Producto Cancelado. No es posible realizar alguna modificación.");
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

            string sSql = string.Format("Set DateFormat YMD \nSelect L.IdProducto, L.CodigoEAN, L.IdSubFarmacia, F.Descripcion As SubFarmacia, L.ClaveLote, \n" +
                   "\tdatediff(mm, getdate(), L.FechaCaducidad) as MesesCad, \n" +
                   "\tConvert( varchar(10), L.FechaRegistro, 120) as FechaReg, Convert(varchar(10), L.FechaCaducidad, 120 ) as FechaCad, \n" +
                   "\t(Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, cast(L.Existencia as Int) as Existencia, \n" +
                   "\tConvert( varchar(10), ( DateAdd( month, 2, L.FechaCaducidad) ), 120 ) as FechaCadMaxima \n" +
                   "From FarmaciaProductos_CodigoEAN_Lotes L (NoLock) \n" +
                   "Inner join CatFarmacias_SubFarmacias F (NoLock) On ( L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and L.IdSubFarmacia = F.IdSubFarmacia ) \n" +
                   "Where L.IdEstado =  '{0}' and L.IdFarmacia = '{1}' and L.IdProducto = '{2}' and L.CodigoEAN = '{3}' \n", sEstado, sFarmacia, sCodigo, sCodEAN);

            myLeerLotes.DataSetClase = Consultas.LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, false, "CargarLotesCodigoEAN()");
            if (!myLeerLotes.Exec(sSql))
            {
                Error.GrabarError(myLeerLotes, "Buscar_Lotes()");
                General.msjError("Error al consultar Lotes de producto."); 
            }
            else 
            {
                if (myLeerLotes.Leer())
                {
                    myGrid.LlenarGrid(myLeerLotes.DataSetClase);
                }
                else
                {
                    bRegresa = false;
                    General.msjUser("Producto ingresado no tiene Lotes registrados. Favor de verificar.");
                    btnNuevo_Click(null, null);
                }
            }

            return bRegresa;
        }
        #endregion Buscar Producto

        #region Buscar Lote
        private void txtClaveLote_Validating(object sender, CancelEventArgs e)
        {
            DateTime dFechaCaducidadMaxima, dFechaCaducidadMinima;
            int iRenglon = 0;

            if (txtClaveLote.Text.Trim() != "")
            {
                iRenglon = myGrid.BuscarRenglonEnGrid(txtClaveLote.Text.Trim(), (int)Cols.ClaveLote);

                if (iRenglon != 0)
                {
                    if (!BuscarDatosLote())
                    {
                        txtClaveLote.Text = ""; 
                        txtClaveLote.Focus(); 
                    }
                    else 
                    {
                        //Se verifica que el Lote no haya sido modificado anteriormente.
                        if (!Lote_Modificado_Anteriormente())
                        {
                            dFechaCaducidadMaxima = myGrid.GetValueFecha(iRenglon, (int)Cols.FechaCaducidadMaxima);
                            dFechaCaducidadMinima = myGrid.GetValueFecha(iRenglon, (int)Cols.FechaCaducidad);
                            dFechaCaducidad_Anterior = myGrid.GetValueFecha(iRenglon, (int)Cols.FechaCaducidad);

                            //Se asigna la Fecha de Caducidad Maxima y Minima del Lote seleccionado.
                            dtpFechaCaducidad.MaxDate = dFechaCaducidadMaxima;
                            dtpFechaCaducidad.MinDate = dFechaCaducidadMinima;

                            //// Jesús Díaz 2K111225.1630   // Dejar libre el cambio de caducidad 
                            if (bPermisoCaducidades || bPermisoCaducidades_Aux) 
                            {
                                DateTimePicker dtActual = new DateTimePicker();
                                dtpFechaCaducidad.MinDate = dtActual.MinDate;
                                dtpFechaCaducidad.MaxDate = dtActual.MaxDate;
                            }

                            IniciarToolBar(true, true, false, false);
                        }
                    }
                }
                else
                {
                    General.msjUser("LOTE ingresado no existe para este Producto. Favor de verificar.");
                    txtClaveLote.Focus();
                }

            }
        }

        private bool BuscarDatosLote()
        {
            bool bRegresa = false;
            string sSql = string.Format(" Select IdSubFarmacia, ClaveLote " +
                   " From FarmaciaProductos_CodigoEAN_Lotes (NoLock) " + 
                   " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and IdSubFarmacia = '{3}' and ClaveLote = '{4}' ",
                   DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, cboSubFarmacias.Data, txtClaveLote.Text );

            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "BuscarDatosLote()");
                General.msjError("Error al consultar Lote."); 
            }
            else 
            {
                // bRegresa = myLeer.Leer(); 
                bRegresa = true;
                if (!myLeer.Leer())
                {
                    bRegresa = false; 
                    General.msjUser(" No existe LOTE en la F. Financiamiento seleccionada. Favor de verificar."); 
                }
            }

            return bRegresa;
        }

        private bool Lote_Modificado_Anteriormente()
        {
            bool bRegresa = false;
            myLeer = new clsLeer(ref ConexionLocal);

            Consultas.MostrarMsjSiLeerVacio = false;
            myLeer.DataSetClase = Consultas.Adt_FarmaciaProductos_CodigoEAN_Lotes(sEmpresa, sEstado, sFarmacia, cboSubFarmacias.Data, txtIdProducto.Text.Trim(), lblCodigoEAN.Text.Trim(), txtClaveLote.Text.Trim(), "txtFolio_Validating");
            if (myLeer.Leer())
            {
                lblCorregido.Visible = true;
                lblCorregido.Text = "CORREGIDO";
                //Si el usuario es administrador, puede hacer cambios a la compra.
                //if (!DtGeneral.EsAdministrador )
                //{
                //    if (!(bPermisoCaducidades || bPermisoCaducidades_Aux))
                //    {
                //        bRegresa = true;
                //        txtClaveLote.Text = "";
                //        txtClaveLote.Focus();
                //        General.msjUser("Este Lote ya ha sido corregido, verifique");
                //    }
                //}
            }

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
            DateTimePicker dtActual = new DateTimePicker(); 

            //dtpFechaCaducidad.MinDate = DateTime.Parse("01/01/1753");
            //dtpFechaCaducidad.MaxDate = DateTime.Parse("31/12/9998");

            dtpFechaCaducidad.MinDate = dtActual.MinDate;
            dtpFechaCaducidad.MaxDate = dtActual.MaxDate; 
            
            dtpFechaCaducidad.Value = General.FechaSistema;
            IniciarToolBar(true, false, false, false);
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;
            int iRenglon = 0;

            if (cboSubFarmacias.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("Seleccione F. Financiamiento. Favor de verificar.");
                cboSubFarmacias.Focus();
            }

            if (bRegresa && txtClaveLote.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("LOTE no valido. Favor de verificar.");
                txtClaveLote.Focus();
            }

            //iRenglon = myGrid.BuscarRenglonEnGrid(txtClaveLote.Text.Trim(), (int)Cols.ClaveLote);
            string sValorBuscar = cboSubFarmacias.Data  + txtClaveLote.Text.Trim();
            int[] Columnas = { (int)Cols.IdSubFarmacia, (int)Cols.ClaveLote };


            //myGrid.BuscarRepetidosColumnas

            //if( myGrid.GetValue(iRenglon, (int)Cols.IdSubFarmacia) != cboSubFarmacias.Data) 
            if (myGrid.BuscarRepetidosColumnas(sValorBuscar, Columnas) == 0)
            {
                bRegresa = false;
                General.msjUser("LOTE no pertenece a F. Financiamiento. Favor de verificar.");
                cboSubFarmacias.Focus();
            }
            
            return bRegresa;
        }

        private void CargaSubFarmacias()
        {
            cboSubFarmacias.Clear();
            cboSubFarmacias.Add("0", "<< Seleccione >>");

            myLeer.DataSetClase = Consultas.SubFarmacias(sEstado, sFarmacia, "", "CargaSubFarmacias()");

            if (myLeer.Leer())
            {
                cboSubFarmacias.Add(myLeer.DataSetClase, true);
            }

            cboSubFarmacias.SelectedIndex = 0;
        }

        #endregion Funciones

    }
}
