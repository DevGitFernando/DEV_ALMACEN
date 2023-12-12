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
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Productos;
using DllFarmaciaSoft.Usuarios_y_Permisos;
using OficinaCentral.Catalogos.Productos;

namespace OficinaCentral.Catalogos
{
    public partial class FrmProductos : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeerProducto;
        clsLeer myLeer;
        clsLeer leer;
        clsAyudas Ayuda = new clsAyudas();
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;
        clsGrid myGridEstados;
        clsGrid myGridCodigosEAN;

        DataSet dtsSegmentos;

        clsDatosCliente DatosCliente;
        wsOficinaCentral.wsCnnOficinaCentral conexionWeb;

        bool bInicioPantalla = true;

        bool bInformacionGuardada = false; 
        string sIdClaveSSA = "";
        string sClaveSSA = ""; 
        string sClaseSSA_Def = GnOficinaCentral.Parametros.GetValor("ClaseSSAGeneral");
        string sFamilia_Def = GnOficinaCentral.Parametros.GetValor("FamiliaGeneral");
        string sSubFamilia_Def = GnOficinaCentral.Parametros.GetValor("SubFamiliaGeneral");

        //clsOperacionesSupervizadas Permisos;
        string sPermisoClaveSSA = "MODIFICAR_CLAVESSA";
        string sPermisoContenidoPaquete = "MODIFICAR_CONTENIDO_PAQUETE";
        bool bPermisoClaveSSA = false;
        bool bPermisoContenidoPaquete = false;
        bool bEsModulo_MA = DtGeneral.ModuloMA_EnEjecucion == TipoModulo_MA.Regional; 

        //string sIdCliente = GnFarmacia.Parametros.GetValor("CtePubGeneral");  //"0001";

        clsAuditoria auditoria;

        private enum ColsEAN
        {
            Ninguno = 0, EAN = 1, Activo = 2, ContenidoUnitario = 3, ContenidoCorrugado = 4, CajasCama = 5, CajasTarima = 6, VerImagen = 7
        }

        public FrmProductos()
        {
            InitializeComponent();
            ConexionLocal.SetConnectionString();

            myLeerProducto = new clsLeer(ref ConexionLocal);
            myLeer = new clsLeer(ref ConexionLocal);
            leer = new clsLeer(ref ConexionLocal);

            Consultas = new clsConsultas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.Modulo, GnOficinaCentral.Version, this.Name);


            grdEstadosProductos.EditModeReplace = false;
            grdCodigosEAN.EditModeReplace = false;

            myGridEstados = new clsGrid(ref grdEstadosProductos, this);
            myGridCodigosEAN = new clsGrid(ref grdCodigosEAN, this);

            myGridEstados.EstiloGrid(eModoGrid.ModoRow);
            myGridCodigosEAN.EstiloGrid(eModoGrid.ModoRow);

            myGridEstados.AjustarAnchoColumnasAutomatico = true;
            myGridCodigosEAN.AjustarAnchoColumnasAutomatico = true;



            SolicitarPermisosUsuario();

            DatosCliente = new clsDatosCliente(GnOficinaCentral.DatosApp, this.Name, "");
            conexionWeb = new OficinaCentral.wsOficinaCentral.wsCnnOficinaCentral();
            conexionWeb.Url = General.Url;

            auditoria = new clsAuditoria(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal,
                                        DtGeneral.IdSesion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);


            btnKarrusel_Imagenes.Visible = DtGeneral.EsEquipoDeDesarrollo; 
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {


            Inicializa();

        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                ////case Keys.F5:
                ////    btnNuevo_Click(null, null);
                ////    break;

                ////case Keys.F6:
                ////    btnGuardar_Click(null, null);
                ////    break;

                ////case Keys.F8:
                ////    btnCancelar_Click(null, null);
                ////    break;

                ////case Keys.F10:
                ////    btnImprimir_Click(null, null);
                ////    break;

                default:
                    base.OnKeyDown(e);
                    break;
            }
        }

        #region Permisos de Usuario 
        /// <summary>
        /// Obtiene los privilegios para el usuario conectado 
        /// </summary>
        private void SolicitarPermisosUsuario()
        {
            ////// Valida si el usuario conectado tiene permiso sobre las opcione especiales 
            ////Permisos = new clsOperacionesSupervizadas(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);
            ////Permisos.Personal = DtGeneral.IdPersonal;
            ////bPermisoClaveSSA = Permisos.TienePermiso(sPermisoClaveSSA);
            ////bPermisoContenidoPaquete = Permisos.TienePermiso(sPermisoContenidoPaquete);

            bPermisoClaveSSA = DtGeneral.PermisosEspeciales.TienePermiso(sPermisoClaveSSA);
            bPermisoContenidoPaquete = DtGeneral.PermisosEspeciales.TienePermiso(sPermisoContenidoPaquete);

        }
        #endregion Permisos de Usuario

        #region Limpiar
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarPantalla();
        }

        private void InicializarPantalla() 
        {
            //Fg.IniciaControles(this, true);
            //Fg.IniciaControles();
            //Fg.BloqueaControles(this, true);

            sIdClaveSSA = "";
            sClaveSSA = "";
            bInformacionGuardada = false; 

            Fg.IniciaControles(this, true, FrameProducto);
            Fg.IniciaControles(this, true, FrameDatosPrecio); 
            lblCancelado.Visible = false;
            lblCancelado.Text = "CANCELADO";
            txtId.Enabled = true;

            //txtId.Text = "";
            //txtDescripcion.Text = "";
            //txtDescripcionCorta.Text = "";
            //txtClaveInternaSal.Text = "";
            //lblClaveSal.Text = "";
            //lblDescripcionSal.Text = "";

            toolTip.SetToolTip(lblDescripcionSal, "");

            ////Se posicionan los Combos en la primer opcion.            
            //cboClasificaciones.SelectedIndex = 0;
            //cboTipoProductos.SelectedIndex = 0;
            //cboFamilias.SelectedIndex = 0;
            //cboLaboratorios.SelectedIndex = 0;
            //cboPresentaciones.SelectedIndex = 0;

            ////Se limpia el combo SubFamilia para que solo muestre los de la Familia seleccionada.
            //cboSubFamilias.Clear();
            //cboSubFamilias.Add("0", "<< Seleccione >>");
            //cboSubFamilias.SelectedIndex = 0;            


            txtContenido.Text = "1";
            txtContenido.Enabled = false;

            //Se limpian los checkbox
            chkMedicamento.Checked = false;
            chkDescomponer.Checked = false;
            chkCodigoEAN.Checked = false;

            //Se limpian los Grids.
            myGridCodigosEAN.Limpiar(false);
            myGridCodigosEAN.AddRow();
            myGridCodigosEAN.BloqueaGrid(true);
            limpiaChecksGridEstados();


            // Establecer valores Default 
            cboClasificaciones.Data = sClaseSSA_Def;
            cboFamilias.Data = sFamilia_Def;
            cboSubFamilias.Data = sSubFamilia_Def;
            chkCodigoEAN.Checked = true;

            tabCatProductos.SelectTab(0);
            tabCatProductos.Focus();

            myGridCodigosEAN.SetValue((int)ColsEAN.ContenidoUnitario, 0);
            myGridCodigosEAN.SetValue((int)ColsEAN.ContenidoCorrugado, 0);
            myGridCodigosEAN.SetValue((int)ColsEAN.CajasCama, 0);
            myGridCodigosEAN.SetValue((int)ColsEAN.CajasTarima, 0);

            //if (DtGeneral.EsAdministrador)
            //{
            //    txtClaveInternaSal.Enabled = true; 
            //}
            //else
            //{
            //    // Falta agregar funcionalidad para activar la opcion de acuerdo a las Opereciones Supervizadas. 
            //    txtClaveInternaSal.Enabled = false;
            //}


            if (bInicioPantalla)
            {
                bInicioPantalla = false;
                SendKeys.Send("{TAB}");
            }


            if (DtGeneral.ModuloMA_EnEjecucion == TipoModulo_MA.Regional)
            { 
            }

            //bEsModulo_MA = !bEsModulo_MA; 
            txtMargenMininoUtilidad_01_Inferior.ReadOnly = !bEsModulo_MA;
            txtMargenMininoUtilidad_02_Superior.ReadOnly = !bEsModulo_MA;
            txtPorcentaje_CopagoColaborador.ReadOnly = !bEsModulo_MA; 

            txtId.Focus();
        }

        private void limpiaChecksGridEstados()
        {
            int i = 0, iRenglones = 0;

            iRenglones = myGridEstados.Rows;
            for (i = 1; i <= iRenglones; i++)
            {
                myGridEstados.SetValue(i, 3, 0);
            }

        } 
        #endregion Limpiar

        #region Inicializa 
        private void Inicializa()
        {
            // Cargar todos los combos y los Estados 
            Consultas.MostrarMsjSiLeerVacio = false;
            LlenaClasificaciones();
            LlenaTiposProducto();
            LlenaFamilias();
            CargarSegmentos();
            LlenaLaboratorios();
            LlenaPresentaciones();
            LlenaGridEstadosProductos();
            Consultas.MostrarMsjSiLeerVacio = true;

            InicializarPantalla();
        }

        private void LlenaClasificaciones()
        {
            cboClasificaciones.Clear();
            cboClasificaciones.Add("0", "<< Seleccione >>");
            myLeer.DataSetClase = Consultas.ComboClasificacionesSSA("LlenaClasificaciones");
            if (myLeer.Leer())
            {
                cboClasificaciones.Add(myLeer.DataSetClase, true);
            }
            cboClasificaciones.SelectedIndex = 0;
        }

        private void LlenaTiposProducto()
        {
            cboTipoProductos.Clear();
            cboTipoProductos.Add("0", "<< Seleccione >>");
            myLeer.DataSetClase = Consultas.ComboTiposDeProducto("LlenaTiposProducto");
            if (myLeer.Leer())
            {
                cboTipoProductos.Add(myLeer.DataSetClase, true);
            }
            cboTipoProductos.SelectedIndex = 0;
        }

        private void LlenaFamilias()
        {
            cboFamilias.Clear();
            cboFamilias.Add("0", "<< Seleccione >>");
            myLeer.DataSetClase = Consultas.ComboFamilias("LlenaFamilias");
            if (myLeer.Leer())
            {
                cboFamilias.Add(myLeer.DataSetClase, true);
            }

            // Cargar las sub-familias 
            LlenaSubFamilias();
            cboFamilias.SelectedIndex = 0;
        }

        private void LlenaSubFamilias()
        {
            string sIdFamilia = "";

            sIdFamilia = cboFamilias.Data;
            cboSubFamilias.Clear();
            cboSubFamilias.Add("0", "<< Seleccione >>");

            myLeer.DataSetClase = Consultas.ComboSubFamilias( sIdFamilia, "LlenaSubFamilias");
            if (myLeer.Leer())
            {                
                cboSubFamilias.Add(myLeer.DataSetClase, true);                
            }
            cboSubFamilias.SelectedIndex = 0;

        }

        private void LlenaLaboratorios()
        {
            //cboLaboratorios.Clear();
            //cboLaboratorios.Add("0", "<< Seleccione >>");
            //myLeer.DataSetClase = Consultas.ComboLaboratorios("LlenaLaboratorios");
            //if (myLeer.Leer())
            //{                
            //    cboLaboratorios.Add(myLeer.DataSetClase, true);
            //}
            //cboLaboratorios.SelectedIndex = 0;
        }

        private void LlenaPresentaciones()
        {
            cboPresentaciones.Clear();
            cboPresentaciones.Add("0", "<< Seleccione >>");
            myLeer.DataSetClase = Consultas.ComboPresentaciones("LlenaPresentaciones");
            if (myLeer.Leer())
            {                
                cboPresentaciones.Add(myLeer.DataSetClase, true);
            }
            cboPresentaciones.SelectedIndex = 0;
        }

        private void LlenaGridEstadosProductos()
        {
            //myLeer.DataSetClase = Consultas.ComboEstados("LlenaGridEstadosProductos");
            myGridEstados.Limpiar(true);
            if (myLeer.Exec("Select IdEstado, Nombre From CatEstados (NoLock) Where Status = 'A' Order By IdEstado") )
            {
                if (myLeer.Leer())
                    myGridEstados.LlenarGrid(myLeer.DataSetClase, false, false);
            }
        }

        private void CargarSegmentos()
        {
            dtsSegmentos = Consultas.Segmentos("CargarSegmentos()");
            cboSegmentos.Clear();
            cboSegmentos.Add("0", "<< Seleccione >>");
            cboSegmentos.SelectedIndex = 0;
        }

        private void CargarSegmentos(int i)
        {
            string sSql = string.Format(" IdFamilia = '{0}' and IdSubFamilia = '{1}' ", cboFamilias.Data, cboSubFamilias.Data);
            cboSegmentos.Clear();
            cboSegmentos.Add("0", "<< Seleccione >>");
            try
            {
                cboSegmentos.Add(dtsSegmentos.Tables[0].Select(sSql), true, "IdSegmento", "Descripcion");
            }
            catch { }
            cboSegmentos.SelectedIndex = 0;
        } 
        #endregion Inicializa

        #region Buscar Producto
        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            string sCadena = "";

            if (txtId.Text.Trim() == "" || txtId.Text.Trim() == "*")
            {
                txtId.Enabled = false;
                txtId.Text = "*";
            }
            else
            {
                myLeerProducto.DataSetClase = Consultas.Productos(txtId.Text.Trim(), "txtId_Validating");

                sCadena = Consultas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena); 
                if (myLeerProducto.Leer())
                {
                    CargaDatos();
                }
                else
                {
                    InicializarPantalla();
                }
            }
        }

        private void CargaDatos()
        {
            //Se hace de esta manera para la ayuda.
            bInformacionGuardada = true; 
            txtId.Enabled = false;
            txtClaveInternaSal.Enabled = false;     // Una vez asignada la sal no es posible cambiarla 


            txtId.Text = myLeerProducto.Campo("IdProducto");
            txtClaveInternaSal.Text = myLeerProducto.Campo("IdClaveSSA_Sal");
            lblClaveSal.Text = myLeerProducto.Campo("ClaveSSA");
            lblDescripcionSal.Text = myLeerProducto.Campo("DescripcionSal"); 

            sIdClaveSSA = txtClaveInternaSal.Text;
            sClaveSSA = myLeerProducto.Campo("ClaveSSA"); 
            // txtClaveInternaSal_Validating(txtClaveInternaSal.Text.Trim(), null); //Se cargan los datos de la Clave Interna Sal.


            cboTipoProductos.Data = myLeerProducto.Campo("IdTipoProducto");
            txtDescripcion.Text = myLeerProducto.Campo("Descripcion");
            txtDescripcionCorta.Text = myLeerProducto.Campo("DescripcionCorta");
            cboClasificaciones.Data = myLeerProducto.Campo("IdClasificacion");
            cboFamilias.Data = myLeerProducto.Campo("IdFamilia");
            LlenaSubFamilias(); //Se llenan las subfamilias que pertenecen a la Familia obtenida.
            cboSubFamilias.Data = myLeerProducto.Campo("IdSubFamilia");
            cboSegmentos.Data = myLeerProducto.Campo("IdSegmento");

            //cboLaboratorios.Data = myLeerProducto.Campo("IdLaboratorio");
            txtIdLaboratorio.Text = myLeerProducto.Campo("IdLaboratorio");
            lblLaboratorio.Text = myLeerProducto.Campo("Laboratorio");

            cboPresentaciones.Data = myLeerProducto.Campo("IdPresentacion");
            txtContenido.Text = myLeerProducto.CampoInt("ContenidoPaquete").ToString();

            chkMedicamento.Checked = myLeerProducto.CampoBool("EsControlado");
            chkCodigoEAN.Checked = myLeerProducto.CampoBool("ManejaCodigosEAN"); // No es modificable // 2K90906-1220 
            chkEsSectorSalud.Checked = myLeerProducto.CampoBool("EsSectorSalud"); 


            //if (myLeerProducto.CampoBool("Descomponer"))
            {
                chkDescomponer.Checked = true;
                chkDescomponer.Enabled = false;
                txtContenido.Enabled = false;
            }

            // El precio de Producto solo se puede capturar una vez en esta opcion,
            // para accesos posteriores es de Solo Lectura. 
            txtPrecioMaximo.Text = myLeerProducto.CampoDouble("PrecioMaxPublico").ToString();
            txtDescuento.Text = myLeerProducto.CampoDouble("DescuentoGral").ToString();
            txtUtilidad.Text = myLeerProducto.CampoDouble("UtilidadProducto").ToString();


            txtMargenMininoUtilidad_01_Inferior.Text = myLeerProducto.CampoDouble("PorcentajeMinimoUtilidad_Inferior").ToString();
            txtMargenMininoUtilidad_02_Superior.Text = myLeerProducto.CampoDouble("PorcentajeMinimoUtilidad_Superior").ToString();
            txtPorcentaje_CopagoColaborador.Text = myLeerProducto.CampoDouble("PorcentajeCopagoColaborador").ToString();


            ////txtPrecioMaximo.Enabled = false;
            ////txtDescuento.Enabled = false;
            ////txtUtilidad.Enabled = false;


            CargaCodigosEANProducto(); // Se llena el Grid Codigos EAN
            CargaEstadosProducto(); // Se llena el Grid de Productos_Estados

            chkCodigoEAN.Enabled = false; //Se bloquea ya que una vez guardado un producto no se puede cambiar.

            if (myLeerProducto.Campo("Status") == "C")
            {
                lblCancelado.Visible = true;
                lblCancelado.Text = "CANCELADO";
                Fg.BloqueaControles(this, false);
            }

            // Revisar lso permisos del Usuario 
            ActivarOpciones(); 
        }

        /// <summary>
        /// Activa ó Desactiva los controles de acuerdo a los permisos del Usuario Conectado. 
        /// </summary>
        private void ActivarOpciones()
        {
            // 2K90905-1442 
            if (DtGeneral.EsAdministrador)
            {
                txtClaveInternaSal.Enabled = true;
                txtContenido.Enabled = true;
            }
            else
            {
                // Falta agregar funcionalidad para activar la opcion de acuerdo a las Opereciones Supervizadas. 
                txtClaveInternaSal.Enabled = bPermisoClaveSSA; 
                txtContenido.Enabled = bPermisoContenidoPaquete; 
            }
        }


        private void CargaCodigosEANProducto()
        {
            myLeer.DataSetClase = Consultas.Productos_CodigosEAN(txtId.Text.Trim(), "txtId_Validating");
            {
                int iRenglones = 0, i = 0;

                myGridCodigosEAN.Limpiar(true);
                if (myLeer.Leer())
                {
                    myGridCodigosEAN.LlenarGrid(myLeer.DataSetClase, false, false);
                    myGridCodigosEAN.BloqueaGrid(false);

                    iRenglones = myGridCodigosEAN.Rows;

                    for (i = 1; i <= iRenglones; i++ )
                    {
                        //Se bloquean los codigos EAN leidos ya que no pueden ser modificados.
                        myGridCodigosEAN.BloqueaCelda(true, i, 1);
                    }
                    
                }
            }

        }

        private void CargaEstadosProducto()
        {
            myLeer.DataSetClase = Consultas.Productos_Estado(txtId.Text.Trim(), "txtId_Validating");
            {
                myGridEstados.Limpiar(true);
                if (myLeer.Leer())                
                    myGridEstados.LlenarGrid(myLeer.DataSetClase, false, false);                
                else
                {
                    //Significa que el producto no tiene ningun Estado aun, asi que se llena el Grid con todos los estados.
                    if (myLeer.Exec("Select * From CatEstados (NoLock) Order By IdEstado "))
                    {
                        myGridEstados.Limpiar(true);
                        if (myLeer.Leer())
                            myGridEstados.LlenarGrid(myLeer.DataSetClase, false, false);
                    }
 
                }
            }

        }

        #endregion Buscar Producto

        #region Guardar/Actualizar Producto 
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bContinua = false;
            string sSql = "", sMensaje = "", sIdProducto = "";
            int iMedicamento = 0, iDescomponer = 0, iCodigoEAN = 0, iEsSector = 0;
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion
            string sCadena = "";

            EliminaRenglonesVacios(); // Se Eliminan los Renglones Vacios.
            if (ValidaDatos())
            {
                if (chkMedicamento.Checked)
                    iMedicamento = 1;
                if (chkDescomponer.Checked)
                    iDescomponer = 1;
                if (chkCodigoEAN.Checked)
                    iCodigoEAN = 1;
                if (chkEsSectorSalud.Checked)
                    iEsSector = 1;

                if(!ConexionLocal.Abrir())
                {
                    General.msjErrorAlAbrirConexion();
                }
                else 
                {
                    ConexionLocal.IniciarTransaccion();

                    sSql = String.Format("Exec spp_Mtto_CatProductos \n" +
                        "\t@IdProducto = '{0}', @IdClaveSSA_Sal = '{1}', @Descripcion = '{2}', @DescripcionCorta = '{3}',\n" +
                        "\t@IdClasificacion = '{4}', @IdTipoProducto = '{5}', @EsMedicamentoControlado = '{6}', @EsSectorSalud = '{7}', @IdFamilia = '{8}',\n" +
                        "\t@IdSubFamilia = '{9}', @IdSegmento = '{10}', @IdLaboratorio = '{11}', @IdPresentacion = '{12}', @Descomponer = '{13}',\n" +
                        "\t@ContenidoPaquete = '{14}', @ManejaCodigoEAN = '{15}', @UtilidadProducto = '{16}', @PrecioMaxPublico = '{17}', @DescuentoGral = '{18}',\n" +
                        "\t@IdEstado = '{19}', @IdFarmacia = '{20}', @IdPersonal = '{21}', @iOpcion = '{22}',\n " +
                        "\t@PorcentajeMinimoUtilidad_Inferior = '{23}', @PorcentajeMinimoUtilidad_Superior = '{24}', @PorcentajeCopagoColaborador = '{25}' \n", 
                        txtId.Text.Trim(), txtClaveInternaSal.Text.Trim(), txtDescripcion.Text.Trim(), txtDescripcionCorta.Text,
                        cboClasificaciones.Data, cboTipoProductos.Data, iMedicamento, iEsSector,  
                        cboFamilias.Data, cboSubFamilias.Data, cboSegmentos.Data,  
                        Fg.PonCeros(txtIdLaboratorio.Text,4), cboPresentaciones.Data, iDescomponer,
                        txtContenido.Text, iCodigoEAN, 
                        General.GetFormatoNumerico_Double(txtUtilidad.NumericText), 
                        General.GetFormatoNumerico_Double(txtPrecioMaximo.NumericText), 
                        General.GetFormatoNumerico_Double(txtDescuento.NumericText),  
                        DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal, iOpcion,
                        General.GetFormatoNumerico_Double(txtMargenMininoUtilidad_01_Inferior.NumericText),
                        General.GetFormatoNumerico_Double(txtMargenMininoUtilidad_02_Superior.NumericText),
                        General.GetFormatoNumerico_Double(txtPorcentaje_CopagoColaborador.NumericText)                        
                        );

                    if (myLeerProducto.Exec(sSql))
                    {
                        if (myLeerProducto.Leer())
                        {
                            sIdProducto = String.Format("{0}", myLeerProducto.Campo("Clave"));
                            sMensaje = String.Format("{0}", myLeerProducto.Campo("Mensaje"));

                            if (guardarCodigosEANProducto(sIdProducto))
                            {
                                bContinua = guardarEstadosProducto(sIdProducto);
                            }
                        }

                        sCadena = sSql.Replace("'", "\""); 
                        auditoria.GuardarAud_MovtosUni("*", sCadena); 
                    }             

                    if (bContinua)
                    {
                        ConexionLocal.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        InicializarPantalla();
                    }
                    else
                    {
                        ConexionLocal.DeshacerTransaccion();
                        Error.GrabarError(myLeerProducto, "btnGuardar_Click"); 
                        General.msjError("Ocurrió un error al guardar la información."); 
                        //btnNuevo_Click(null, null);

                    }

                    ConexionLocal.Cerrar();
                }
            }
        }

        private bool guardarCodigosEANProducto(string sIdProducto)
        {
            bool bRegresa = true; // , bCheck = false;
            int i = 0, iStatus = 0; // iRenglones = 0
            string sSql = "", sCodigoEAN = ""; // , sMensaje = "";
            string sCadena = "";
            int iContUnitario = 0, iContCorrugado = 0, iCajasCama = 0, iCajasTarima = 0;

            if (chkCodigoEAN.Checked)
            {
                for (i = 1; i <= myGridCodigosEAN.Rows; i++)
                {
                    sCodigoEAN = myGridCodigosEAN.GetValue(i, 1).Trim();
                    iStatus = Convert.ToInt32(myGridCodigosEAN.GetValueBool(i, 2));

                    iContUnitario = myGridCodigosEAN.GetValueInt(i, (int)ColsEAN.ContenidoUnitario);
                    iContCorrugado = myGridCodigosEAN.GetValueInt(i, (int)ColsEAN.ContenidoCorrugado);
                    iCajasCama = myGridCodigosEAN.GetValueInt(i, (int)ColsEAN.CajasCama);
                    iCajasTarima = myGridCodigosEAN.GetValueInt(i, (int)ColsEAN.CajasTarima);

                    sSql = string.Format("Exec spp_Mtto_CatProductos_CodigosRelacionados \n" +
                        "\t@IdProducto = '{0}', @CodigoEAN = '{1}', @iStatus = '{2}', @ContenidoCorrugado = '{3}', \n" +
                        "\t@Cajas_Cama = '{4}', @Cajas_Tarima = '{5}', @ContenidoPiezasUnitario = '{6}' \n",
                        sIdProducto, sCodigoEAN, iStatus, iContCorrugado, iCajasCama, iCajasTarima, iContUnitario);

                    if (!myLeerProducto.Exec(sSql))
                    {
                        bRegresa = false;
                        Error.GrabarError(myLeerProducto, "guardarCodigosEANProducto()");
                        break;
                    }
                    
                    sCadena = sSql.Replace("'", "\"");
                    auditoria.GuardarAud_MovtosUni("*", sCadena);
                    
                }
            }
            else
            {
                // Grabar al menos el CodigoEAN-Interno si el producto no maneja lotes 
                sSql = String.Format("Exec spp_Mtto_CatProductos_CodigosRelacionados @IdProducto = '{0}', @CodigoEAN = '{1}', @iStatus = '{2}'  ", sIdProducto, "", 1);
                if (!myLeerProducto.Exec(sSql))
                {
                    bRegresa = false;
                    Error.GrabarError(myLeerProducto, "guardarCodigosEANProducto()");
                }

                sCadena = sSql.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);
            }

            return bRegresa;
        }

        private bool guardarEstadosProducto(string sIdProducto )
        {
            bool bRegresa = true; // , bCheck = false;
            int i = 0, iRenglones = 0, iStatus = 0;
            string sIdEstado = ""; // sMensaje = ""
            string sSql = "";
            string sCadena = "";

            sSql = string.Format("Update CatProductos_Estado Set Status = 'C', Actualizado = 0 Where IdProducto = '{0}' ", sIdProducto);

            sCadena = sSql.Replace("'", "\"");
            auditoria.GuardarAud_MovtosUni("*", sCadena);

            if (!myLeerProducto.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                iRenglones = myGridEstados.Rows;
                for (i = 1; i <= iRenglones; i++)
                {
                    iStatus = Convert.ToInt32(myGridEstados.GetValueBool(i, 3));
                    sIdEstado = myGridEstados.GetValue(i, 1).Trim();
                    sSql = String.Format("Exec spp_Mtto_CatProductos_Estado @IdEstado = '{0}', @IdProducto = '{1}', @iStatus = '{2}' ", sIdEstado, sIdProducto, iStatus);

                    if (iStatus == 1)
                    {
                        if (!myLeerProducto.Exec(sSql))
                        {
                            bRegresa = false;
                            Error.GrabarError(myLeerProducto, "guardarEstadosProducto");
                            break;
                        }                        
                    }

                    sCadena = sSql.Replace("'", "\"");
                    auditoria.GuardarAud_MovtosUni("*", sCadena);
                }
            }

            return bRegresa;
        }
   
        #endregion Guardar/Actualizar Producto

        #region Imprimir 
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            DatosCliente.Funcion = "btnImprimir_Click()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;
            bool bRegresa = false; 

            myRpt.RutaReporte = GnOficinaCentral.RutaReportes;
            myRpt.NombreReporte = "Central_Listado_Productos";

            bRegresa = DtGeneral.GenerarReporte(General.Url, true, myRpt, DatosCliente);

            ////DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
            ////DataSet datosC = DatosCliente.DatosCliente();

            ////conexionWeb.Timeout = 300000; 
            ////btReporte = conexionWeb.Reporte(InfoWeb, datosC);

            if (!bRegresa && !DtGeneral.CanceladoPorUsuario) 
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
            
            auditoria.GuardarAud_MovtosUni("*", myRpt.NombreReporte);
        }
        #endregion Imprimir

        #region Eliminar Producto

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "";
            int iOpcion = 2; //La opcion 2 indica que es una cancelacion
            string message = "¿ Desea eliminar el Producto seleccionado ?";
            string sCadena = "";

            //Se verifica que no este cancelada.
            if (lblCancelado.Visible != false)
            {
                General.msjUser("Producto previamente cancelado");
            }
            else 
            {
                txtId_Validating(txtId.Text, null);//Se manda llamar este evento para validar que exista el Producto.
                if (txtDescripcion.Text.Trim() != "") //Si no esta vacio, significa que si existe.
                {
                    if (General.msjCancelar(message) == DialogResult.Yes)
                    {
                        if(!ConexionLocal.Abrir())
                        {
                            General.msjErrorAlAbrirConexion();
                        }
                        else 
                        {
                            ConexionLocal.IniciarTransaccion();

                            sSql = String.Format("Exec spp_Mtto_CatProductos '{0}', '', '', '', " +
                                " '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '{1}', '{2}' ", 
                                txtId.Text.Trim(), DtGeneral.IdPersonal, iOpcion);

                            sSql = string.Format("Exec spp_Mtto_CatProductos \n" +
                                "\t@IdProducto = '{0}', @IdEstado = '{19}', @IdFarmacia = '{20}', @IdPersonal = '{21}', @iOpcion = '{22}' ",
                                txtId.Text.Trim(), DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal, iOpcion);

                            sCadena = sSql.Replace("'", "\"");
                            auditoria.GuardarAud_MovtosUni("*", sCadena);

                            if (myLeerProducto.Exec(sSql))
                            {
                                if(myLeerProducto.Leer())
                                {
                                    sMensaje = String.Format("{0}", myLeerProducto.Campo("Mensaje"));
                                }

                                ConexionLocal.CompletarTransaccion();
                                General.msjUser(sMensaje); //Este mensaje lo genera el SP
                                InicializarPantalla();
                            }
                            else
                            {
                                ConexionLocal.DeshacerTransaccion();
                                Error.GrabarError(myLeerProducto, "btnCancelar_Click");
                                General.msjError("Ocurrió un error al eliminar el Producto.");
                                //btnNuevo_Click(null, null);
                            }

                            ConexionLocal.Cerrar();
                        }
                    }
                }
            }
        }

        #endregion Eliminar Producto

        #region Exportar datos 
        private void btnGenerarPaqueteDatos_Click(object sender, EventArgs e)
        {
            FrmProductos_GenerarPaqueteDeDatos f = new FrmProductos_GenerarPaqueteDeDatos();
            f.ShowDialog(); 
        }
        #endregion Exportar datos
        
        #region Validaciones de Controles

        private bool ValidaDatos()
        {
            bool bRegresa = true;
            int iRenglones = 0; //i = 0,  
            string sMsjCambio = string.Format("Clave anterior [ {0} ] por esta otra [ {1} ].\n\n\n¿ Desea continuar con el cambio de Clave ?", 
                sClaveSSA, lblClaveSal.Text); 

            if (txtId.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Producto inválido, verifique.");
                txtId.Focus();                
            }

            if (bRegresa && txtClaveInternaSal.Text == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado una Clave de Interna, verifique.");
                txtClaveInternaSal.Focus();                    
            }

            // 2K110308-1012 
            // Validacion agregara para Liberacion de Modulo al Departamento de Compras 
            if (bRegresa && bInformacionGuardada)
            {
                if (sIdClaveSSA.Trim() != txtClaveInternaSal.Text.Trim())
                {
                    if (General.msjConfirmar(sMsjCambio, "Se detectado un cambio de Clave SSA") == DialogResult.No)
                    {
                        bRegresa = false;
                        txtClaveInternaSal.Focus(); 
                    }
                }
            }


            if (bRegresa && cboTipoProductos.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado el Tipo de Producto, verifique.");
                cboTipoProductos.Focus();
            }

            if (bRegresa && txtDescripcion.Text == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado la Descripción del Producto, verifique.");
                txtDescripcion.Focus();
            }

            if (bRegresa && txtDescripcionCorta.Text == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado la Descripción Corta del Producto, verifique.");
                txtDescripcionCorta.Focus();
            }

            if (bRegresa && cboClasificaciones.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado una Clasificación valida, verifique.");
                cboClasificaciones.Focus();
            }

            if (bRegresa && cboFamilias.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado una Familia valida, verifique.");
                cboFamilias.Focus();
            }

            if (bRegresa && cboSubFamilias.SelectedIndex == 0 )
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado una Sub-Familia valida, verifique.");
                cboSubFamilias.Focus();
            }

            if (bRegresa && txtIdLaboratorio.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado un Laboratorio valido, verifique.");
                txtIdLaboratorio.Focus();
            }

            if (bRegresa && cboPresentaciones.SelectedIndex  == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado una Presentación valida, verifique.");
                cboPresentaciones.Focus();
            }

            if (bRegresa && chkDescomponer.Checked)
            {
                if (int.Parse(txtContenido.Text) <= 0)
                {
                    bRegresa = false;
                    General.msjUser("El Contenido del Paquete debe ser mayor a cero.");
                    txtContenido.Focus();
                }
            }
            
            if (bRegresa && chkCodigoEAN.Checked)
            {
                iRenglones = myGridCodigosEAN.Rows;
                if (iRenglones <= 0)
                {
                    bRegresa = false;
                    myGridCodigosEAN.AddRow(); //Se agrega un renglon para que pueda capturar el usuario.
                    General.msjUser("Debe ingresar al menos un Codigo EAN, verifique.");
                    grdCodigosEAN.Focus();
                }
            }

            if (bRegresa ) // && txtId.Text == "*" )
            {
                if (Convert.ToDouble(txtPrecioMaximo.NumericText) == 0 && 
                    Convert.ToDouble(txtDescuento.NumericText) == 0 &&
                    Convert.ToDouble(txtUtilidad.NumericText) == 0)
                {
                    bRegresa = false;
                    General.msjUser("Debe capturar los datos para el Calculo del Precio de Venta.");
                    txtPrecioMaximo.Focus(); 
                }
            }

            if ( bRegresa && bEsModulo_MA ) 
            {
                bRegresa = validarInformacion_Comercial();
            }

            return bRegresa;
        }

        private bool validarInformacion_Comercial()
        {
            bool bRegresa = true;
            double dLimite_Inferior = General.GetFormatoNumerico_Double(txtMargenMininoUtilidad_01_Inferior.NumericText);
            double dLimite_Superior = General.GetFormatoNumerico_Double(txtMargenMininoUtilidad_02_Superior.NumericText);
            double dCopago = General.GetFormatoNumerico_Double(txtPorcentaje_CopagoColaborador.NumericText);


            if (bRegresa && dLimite_Inferior == 0)
            {
                bRegresa = false;
                General.msjUser("El porcentaje mínimo inferior de utilidad debe ser mayor a cero.");
                txtMargenMininoUtilidad_01_Inferior.Focus();
            }

            if (bRegresa && dLimite_Superior == 0)
            {
                bRegresa = false;
                General.msjUser("El porcentaje mínimo superior de utilidad debe ser mayor a cero.");
                txtMargenMininoUtilidad_02_Superior.Focus();
            }

            if (bRegresa && (dLimite_Inferior > dLimite_Superior)) 
            {
                bRegresa = false;
                General.msjUser("El Porcentaje mínimo inferior debe ser menor que el Porcentaje mínimo superior.");
                txtMargenMininoUtilidad_01_Inferior.Focus();
            }

            if (bRegresa && (dCopago < dLimite_Inferior ))
            {
                bRegresa = false;
                General.msjUser("El Porcentaje de Copago debe ser mayor que el Porcentaje mínimo inferior.");
                txtPorcentaje_CopagoColaborador.Focus();
            }

            if (bRegresa && (dCopago > dLimite_Superior))
            {
                bRegresa = false;
                General.msjUser("El Porcentaje de Copago debe ser menor que el Porcentaje mínimo superior.");
                txtPorcentaje_CopagoColaborador.Focus();
            }

            return bRegresa;
        }

        #endregion Validaciones de Controles

        #region Eventos
        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            string sCadena = "";

            if (e.KeyCode == Keys.F1)
            {
                myLeerProducto.DataSetClase = Ayuda.Productos("txtId_KeyDown");

                sCadena = Ayuda.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (myLeerProducto.Leer())
                {
                    CargaDatos();
                }
            }

        }

        private void txtClaveInternaSal_KeyDown(object sender, KeyEventArgs e)
        {
            string sCadena = "";

            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayuda.ClavesSSA_Sales("txtId_KeyDown");

                sCadena = Ayuda.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (myLeer.Leer())
                {
                    CargaDatosSalInterna();
                }
            }
        }

        private void cboFamilias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFamilias.Data != "0")
                LlenaSubFamilias();
        }

        private void txtClaveInternaSal_Validating(object sender, CancelEventArgs e)
        {
            string sCadena = "";

            if (txtClaveInternaSal.Text.Trim() != "")
            {
                myLeer.DataSetClase = Consultas.ClavesSSA_Sales(txtClaveInternaSal.Text.Trim(), false, "txtClaveInternaSal_Validating");

                sCadena = Consultas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (myLeer.Leer())
                {
                    CargaDatosSalInterna();
                }
                else
                {
                    txtClaveInternaSal.Focus();
                    lblClaveSal.Text = "";
                    lblDescripcionSal.Text = "";
                    toolTip.SetToolTip(lblDescripcionSal, "");
                }
            }
        }

        private void CargaDatosSalInterna()
        {
            //Se hace de esta manera para la ayuda.
            txtClaveInternaSal.Text = myLeer.Campo("IdClaveSSA_Sal");
            // sIdClaveSSA = txtClaveInternaSal.Text; 
            lblClaveSal.Text = myLeer.Campo("ClaveSSA");
            // sClaveSSA = lblClaveSal.Text; 

            lblDescripcionSal.Text = myLeer.Campo("Descripcion");
            toolTip.SetToolTip(lblDescripcionSal, lblDescripcionSal.Text); 
        }

        private void txtClaveInternaSal_TextChanged(object sender, EventArgs e)
        {
            lblClaveSal.Text = "";
            lblDescripcionSal.Text = "";
        }

        private void chkDescomponer_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDescomponer.Checked)
            {
                txtContenido.Enabled = true;
                txtContenido.Focus();
            }
            else
            {
                txtContenido.Text = "1";
                txtContenido.Enabled = false;
            }
        }

        private void chkCodigoEAN_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCodigoEAN.Checked)
                myGridCodigosEAN.BloqueaGrid(false);
            else
                myGridCodigosEAN.BloqueaGrid(true);
        }      
        #endregion Eventos

        #region Validaciones Grid CodigosEAN
        private void grdCodigosEAN_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if ((myGridCodigosEAN.ActiveRow == myGridCodigosEAN.Rows) && e.AdvanceNext)
            {
                string sValor = myGridCodigosEAN.GetValue(myGridCodigosEAN.ActiveRow, 1);
                if (sValor != "")
                {
                    myGridCodigosEAN.Rows = myGridCodigosEAN.Rows + 1;
                    myGridCodigosEAN.ActiveRow = myGridCodigosEAN.Rows;
                    myGridCodigosEAN.SetActiveCell(myGridCodigosEAN.Rows, 1);

                    myGridCodigosEAN.SetValue(myGridCodigosEAN.ActiveRow, (int)ColsEAN.ContenidoUnitario, 0);
                    myGridCodigosEAN.SetValue(myGridCodigosEAN.ActiveRow, (int)ColsEAN.ContenidoCorrugado, 0);
                    myGridCodigosEAN.SetValue(myGridCodigosEAN.ActiveRow, (int)ColsEAN.CajasCama, 0);
                    myGridCodigosEAN.SetValue(myGridCodigosEAN.ActiveRow, (int)ColsEAN.CajasTarima, 0);
                }
            }
        }


        private void grdCodigosEAN_KeyDown(object sender, KeyEventArgs e)
        {
            //int iRenglonActual = 0;
            
            //if (e.KeyCode == Keys.Enter)
            //{
            //    EliminaRenglonesVacios();
            //    if (chkCodigoEAN.Checked)
            //    {
            //        //if (myGridCodigosEAN.ActiveCol == 1)
            //            myGridCodigosEAN.AddRow();
            //    }
            //}
            //else
            //{
            //    iRenglonActual = myGridCodigosEAN.ActiveRow;
            //    if (myGridCodigosEAN.GetValue(iRenglonActual, 2).Trim() != "")
            //        myGridCodigosEAN.BloqueaRenglon(true, iRenglonActual); 
            //}

        }

        private void EliminaRenglonesVacios()
        {
            int i = 0, iRenglones = 0;
            string sCodigoEAN = "";

            iRenglones = myGridCodigosEAN.Rows;
            for (i = 1; i <= iRenglones; i++)
            {
                sCodigoEAN = myGridCodigosEAN.GetValue(i, 1);
                sCodigoEAN.Trim();
                sCodigoEAN.Replace("(null)", "");

                if (sCodigoEAN == "")
                    myGridCodigosEAN.DeleteRow(i);
            }
        }

        #endregion Validaciones Grid CodigosEAN

        private void txtIdLaboratorio_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdLaboratorio.Text.Trim() != "")
            {
                myLeer.DataSetClase = Consultas.Laboratorios(txtIdLaboratorio.Text, "txtIdLaboratorio_Validating");
                if (myLeer.Leer())
                {
                    txtIdLaboratorio.Text = myLeer.Campo("IdLaboratorio");
                    lblLaboratorio.Text = myLeer.Campo("Descripcion"); 
                }
            }
            else
            {
                txtIdLaboratorio.Text = "";
                lblLaboratorio.Text = "";
            }
        }

        private void txtIdLaboratorio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayuda.Laboratorios("txtIdLaboratorio_Validating");
                if (myLeer.Leer())
                {
                    txtIdLaboratorio.Text = myLeer.Campo("IdLaboratorio");
                    lblLaboratorio.Text = myLeer.Campo("Descripcion");
                } 
            }
        }

        private void cboSubFamilias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSubFamilias.SelectedIndex != 0)
            {
                CargarSegmentos(0);
            }
        }

        private void btnPresentacion_Click(object sender, EventArgs e)
        {
            FrmPresentaciones f = new FrmPresentaciones();
            Fg.CentrarForma(f);
            f.ShowDialog();
            LlenaPresentaciones();
        }

        private void txtCodigoEAN_Validating(object sender, CancelEventArgs e)
        {
            string sCadena = "";

            if (txtCodigoEAN.Text.Trim() != "")
            {
                string sSql = string.Format(" Select IdProducto, CodigoEAN, CodigoEAN_Interno " + 
                    " From CatProductos_CodigosRelacionados (NoLock) Where CodigoEAN = '{0}' ", txtCodigoEAN.Text);

                sCadena = sSql.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("", sCadena); 

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "txtCodigoEAN_Validating");
                    General.msjError("Ocurrió un error al válidar el CodigoEAN");
                }
                else
                {
                    if (leer.Leer())
                    {
                        txtCodigoEAN.Enabled = false;
                        txtCodigoEAN.Text = "";

                        txtId.Text = leer.Campo("IdProducto");
                        txtId_Validating(null, null);
                    }
                    else
                    {
                        General.msjUser("CodigoEAN no encontrado, verifique.");
                        e.Cancel = true;
                        txtCodigoEAN.SelectAll();
                    }
                }
            }
        }

        #region Cargar_Ver_Imagen
        private void grdCodigosEAN_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            string sIdProducto = "";
            string sEAN = "";
            ColsEAN columna = (ColsEAN)(e.Column + 1); 

            sEAN = myGridCodigosEAN.GetValue(myGridCodigosEAN.ActiveRow, (int)ColsEAN.EAN);
            if (columna == ColsEAN.VerImagen)
            {
                if (sEAN.Trim() == "")
                {
                    General.msjAviso("No ha seleccionado un Codigo EAN correcto. Verifique !!");
                }
                else
                {
                    sIdProducto = txtId.Text;

                    FrmProducto_KarruselImagenes f = new FrmProducto_KarruselImagenes();
                    f.MostrarPantalla(sIdProducto, sEAN);
                }
            }
        }
        #endregion Cargar_Ver_Imagen

        private void btnKarrusel_Imagenes_Click(object sender, EventArgs e)
        {
            DllFarmaciaSoft.Productos.FrmProducto_KarruselImagenes f = new DllFarmaciaSoft.Productos.FrmProducto_KarruselImagenes();
            f.ShowDialog(); 
        }

        
        //private void grdCodigosEAN_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        //{
        //    string sCodigoEAN = myGridCodigosEAN.GetValue(myGridCodigosEAN.ActiveRow, 1);
        //    string sIdProducto = txtId.Text;

        //    if (sCodigoEAN == "")
        //    {
        //        General.msjUser("No ha seleccionado un CodigoEAN válido, verifique.");
        //    }
        //    else
        //    {
        //        if (sIdProducto == "*")
        //        {
        //            General.msjUser("La información de Producto no ha sido registrada, no es posible accesar al Registro Sanitario.");
        //        }
        //        else
        //        {
        //            FrmProductosRegistrosSanitarios frm = new FrmProductosRegistrosSanitarios();
        //            frm.CargarProductosRegistrosSanitarios(sIdProducto, sCodigoEAN, txtDescripcion.Text);
        //        }
        //    }
        //}

    } //Llaves de la clase
}
