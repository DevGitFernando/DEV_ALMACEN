using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Usuarios_y_Permisos;
using SC_SolutionsSystem.OfficeOpenXml;
using SC_SolutionsSystem.OfficeOpenXml.Data;
using SC_SolutionsSystem.SQL;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Inventario;
using DllFarmaciaSoft.Usuarios_y_Permisos;

namespace OficinaCentral.CuadrosBasicos
{
    public partial class FrmCuadrosBasicos : FrmBaseExt
    {
        //basGenerales Fg = new basGenerales();
        // clsGuardarSC Guardar = new clsGuardarSC();
        clsConsultas query;
        DataSet dtsDatos = new DataSet();
        DataSet dtsGrupos = new DataSet(), dtsUsuariosGrupo = new DataSet();
        DataSet dtsClientes = new DataSet();
        DataSet dtsSubClientes = new DataSet();

        // DllFarmaciaSoft.Usuarios_y_Permisos.FrmUsuarios myUsuario;
        TreeNode myNodeSeleccionado; //, myNodoUsuario;
        int iIndexNodo = 0;
        string sIdClave = "";
        string sClaveDesc = "";
        string sIdGrupo = "";

        //clsOperacionesSupervizadas Permisos;
        string sPermisoPerfiles = "MODIFICAR_PERFILES";
        bool bPermisoPerfiles = false;

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsConsultas leer = new clsConsultas(General.DatosConexion, "Configuracion", "GruposDeUsuarios", Application.ProductVersion,true);
        clsLeer reader, reader2;

        //clsLeerExcel excel;
        clsLeerExcelOpenOficce excel;
        //clsExportarExcelPlantilla xpExcel;
        OpenFileDialog openExcel = new OpenFileDialog();
        Thread thLoadFile;

        string sNombreHoja = "Perfiles".ToUpper();
        bool bExisteHoja = false;

        int iRegistrosHoja = 0;
        int iRegistrosProcesados = 0;

        string sTitulo = "";
        string sFile_In = "";
        string sFormato = "###, ###, ###, ##0";
        int iFolioCargaMasiva = 0;

        bool bValidandoInformacion = false;
        bool bSeEncontraronIndicencias = false;
        bool bActivarProceso = false;
        bool bErrorAlValidar = false;

        int iPosicion_Oculta = 0;
        int iPosicion_Mostrar_Top = 0;
        int iPosicion_Mostrar_Left = 0;


        public FrmCuadrosBasicos()
        {
            InitializeComponent();
        }

        private void FrmGruposUsuarios_Load(object sender, EventArgs e)
        {
            query = new clsConsultas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
            reader = new clsLeer(ref cnn);
            reader2 = new clsLeer(ref cnn);

            Inicializa();

            excel = new clsLeerExcelOpenOficce();


            FrameProceso.Left = 288;

            iPosicion_Oculta = (int)General.Pantalla.Ancho;
            iPosicion_Mostrar_Top = (FramePerfiles.Height / 2) - (FrameProceso.Height / 2);
            iPosicion_Mostrar_Left = (FramePrincipal.Width / 2) - (FrameProceso.Width / 2);

            MostrarEnProceso(false, 0);

            //SolicitarPermisosUsuario(); 
            //CargarEstados();
            //ActualizaMenu("0");
        }

        #region Botones 
        private void IniciarPantalla()
        {
            //lst.Limpiar();
            Fg.IniciaControles();

            lblProcesados.Text = "";
            lblProcesados.Visible = false;

            sTitulo = "Información ";
            IniciaToolBar();
        }

        private void IniciaToolBar()
        {
            IniciaToolBar(true, true, true, false);
        }

        private void IniciaToolBar(bool Nuevo, bool Exportar, bool Abrir, bool Guardar)
        {
            btnNuevo.Enabled = Nuevo;
            btnExportarExcel.Enabled = Exportar;
            btnAbrir.Enabled = Abrir;
            btnGuardar.Enabled = Guardar;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            IniciarPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (cboSubClientes.SelectedIndex != 0)
            {
                CargarGrupos();
                CargarClaves("");
                ActualizaMenu("2");
                bntBuscarClaves.Visible = true;
            }
            else
            {
                bntBuscarClaves.Visible = false;
                twGrupos.Nodes.Clear();
                lwUsuarios.Items.Clear();
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            string sSql = string.Format("Exec spp_PRCS_OCEN__Plantilla_CargaDePrecios  @IdEstado = '{0}', @IdCliente = '{1}', @IdSubCliente = '{2}', @Tipo = '{3}' ",
                cboEstados.Data, cboClientes.Data, cboSubClientes.Data, 5);

            if(!reader.Exec(sSql))
            {
                Error.GrabarError(reader, "btnExportarExcel_Click");
                General.msjError("Ocurrió un error al obtener la información para generar la plantilla de Carga de Cuadro Básico.");
            }
            else
            {
                Generar_Excel();
            }
        }

        private void Generar_Excel()
        {
            DllFarmaciaSoft.ExportarExcel.clsGenerarExcel excel = new DllFarmaciaSoft.ExportarExcel.clsGenerarExcel();
            string sNombreDocumento = string.Format("Plantilla_CuadrosBasicosUnidades___{0}", cboEstados.Text);
            string sNombreHoja = "CuadrosBasicos";
            string sConcepto = "";

            int iHoja = 1, iRenglon = 15;
            int iColBase = 2;
            int iColsEncabezado = 8;

            DateTime dtpFecha = General.FechaSistema;
            int iAño = dtpFecha.AddMonths(-1).Year, iMes = dtpFecha.AddMonths(-1).Month;
            //int iHoja = 1;
            string sEmpresa = DtGeneral.EmpresaConectadaNombre;
            string sEstado = DtGeneral.EstadoConectadoNombre;
            string sFarmacia = DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre;
            string sFechaImpresion = General.FechaSistemaFecha.ToString();

            excel = new DllFarmaciaSoft.ExportarExcel.clsGenerarExcel();
            excel.RutaArchivo = @"C:\\Excel";
            excel.NombreArchivo = sNombreDocumento;
            excel.AgregarMarcaDeTiempo = true;

            if(excel.PrepararPlantilla(sNombreDocumento))
            {
                excel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                iRenglon = 1;
                //excel.LlenarDetalleHorizontal(sNombreHoja, iRenglon, iColBase, leer.DataSetClase); 
                excel.InsertarTabla(sNombreHoja, iRenglon, iColBase, reader.DataSetClase);

                //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);

                excel.CerraArchivo();

                excel.AbrirDocumentoGenerado(true);
            }
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            if (validarParametros())
            {
                openExcel.Title = "Archivos de programación y ampliación de consumos";
                openExcel.Filter = "Archivos de Excel (*.xls;*.xlsx)| *.xls;*.xlsx";
                //openExcel.Filter = "Archivos de Texto (*.txt)| *.txt"; 
                openExcel.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
                openExcel.AddExtension = true;
                lblProcesados.Visible = false;

                // if (openExcel.FileName != "")
                if (openExcel.ShowDialog() == DialogResult.OK)
                {
                    sFile_In = openExcel.FileName;

                    BloqueaControles(true);
                    IniciaToolBar(false, false, false, false);

                    tmValidacion.Enabled = true;
                    tmValidacion.Interval = 1000;
                    tmValidacion.Start();


                    thLoadFile = new Thread(this.CargarArchivo);
                    thLoadFile.Name = "LeerArchivo";
                    thLoadFile.Start();
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sMensaje = "¿ Desea integrar la información de Cuadro Básico, este proceso no se podra deshacer ?";

            if (General.msjConfirmar(sMensaje) == System.Windows.Forms.DialogResult.Yes)
            {
                IntegrarInformacion();
            }
        }

        private bool validarParametros()
        {
            bool bRegresa = true;

            if (cboSubClientes.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un Sub-Cliente válido.");
                cboSubClientes.Focus();
            }

            return bRegresa;
        }

        private void MostrarEnProceso(bool Mostrar, int Proceso)
        {
            string sTituloProceso = "";

            iPosicion_Oculta = (int)General.Pantalla.Ancho;
            iPosicion_Mostrar_Top = (FramePerfiles.Height / 2) - (FrameProceso.Height / 2);
            iPosicion_Mostrar_Left = (FramePrincipal.Width / 2) - (FrameProceso.Width / 2);

            FrameProceso.Top = iPosicion_Mostrar_Top;
            if(Mostrar)
            {
                FrameProceso.Left = iPosicion_Mostrar_Left;
            }
            else
            {
                FrameProceso.Left = iPosicion_Oculta * 2;
            }



            if (Proceso == 1)
            {
                sTituloProceso = "Leyendo estructura del documento";
            }

            if (Proceso == 2)
            {
                sTituloProceso = "Leyendo información de hoja seleccionada";
            }

            if (Proceso == 3)
            {
                sTituloProceso = "Guardando información de hoja seleccionada";
            }

            if (Proceso == 4)
            {
                sTituloProceso = "Verificando información a integrar";
            }

            if (Proceso == 5)
            {
                sTituloProceso = "Integrando información ..... ";
            }

            FrameProceso.Text = sTituloProceso;

        }
        #endregion Botones
        
        #region Permisos de Usuario
        private void SolicitarPermisosUsuario()
        {
            ////// Valida si el usuario conectado tiene permiso sobre las opcione especiales 
            ////Permisos = new clsOperacionesSupervizadas(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);
            ////Permisos.Personal = DtGeneral.IdPersonal; 
            bPermisoPerfiles = DtGeneral.PermisosEspeciales.TienePermiso(sPermisoPerfiles);
        }
        #endregion Permisos de Usuario

        #region Combos 
        private void CargarEstados()
        {
            string sSql = "Select Distinct IdEstado, Estado, ( IdEstado + ' - ' + Estado ) as Descripcion From vw_Farmacias_Urls (NoLock) Order by IdEstado ";
            cboEstados.Clear();
            cboEstados.Add();

            if (!DtGeneral.EsAdministrador)
            {
                sSql = string.Format(" Select Distinct IdEstado, Estado, ( IdEstado + ' - ' + Estado ) as Descripcion From vw_Farmacias_Urls (NoLock) " +
                    " Where IdEstado = '{0}' Order by IdEstado ", DtGeneral.EstadoConectado);
            }

            if (!reader.Exec(sSql))
            {
                Error.GrabarError(reader, "CargarEstados()");
                General.msjError("Ocurrió un error al Cargar la Lista de Estados.");
            }
            else
            {
                if (reader.Leer())
                {
                    cboEstados.Add(reader.DataSetClase, true, "IdEstado", "Descripcion");
                }
            }

            //reader.Exec("Select *, (IdCliente + ' - ' + NombreCliente) as NombreCliente From vw_Claves_Precios_Asignados (NoLock) Order By IdEstado, IdCliente ");
            reader.Exec("Select Distinct IdEstado, IdCliente, (IdCliente + ' - ' + NombreCliente) as NombreCliente From vw_Claves_Precios_Asignados (NoLock) Order By IdEstado, IdCliente ");
            dtsClientes = reader.DataSetClase;

            // SE OBTIENE LOS SUB-CLIENTES
            reader.Exec("Select Distinct IdEstado, IdCliente, IdSubCliente, (IdSubCliente + ' - ' + NombreSubCliente) as NombreSubCliente From vw_Claves_Precios_Asignados (NoLock) Order By IdEstado, IdCliente, IdSubCliente ");
            dtsSubClientes = reader.DataSetClase;

            cboEstados.SelectedIndex = 0;
            if (!DtGeneral.EsAdministrador)
            {
                if (!bPermisoPerfiles)
                {
                    cboEstados.Data = DtGeneral.EstadoConectado;
                    cboEstados.Enabled = false;
                }
            }

        }

        private void CargarClientes()
        {
            cboClientes.Clear();
            cboClientes.Add("0", "<< Seleccione >>");

            if (cboEstados.SelectedIndex != 0)
            {
                try
                {
                    cboClientes.Add(dtsClientes.Tables[0].Select("IdEstado = '" + cboEstados.Data + "'"), true, "IdCliente", "NombreCliente");
                }
                catch { }
            }
            cboClientes.SelectedIndex = 0;
        }

        private void CargarSubClientes()
        {
            cboSubClientes.Clear();
            cboSubClientes.Add("0", "<< Seleccione >>");

            if (cboClientes.SelectedIndex != 0)
            {
                try
                {
                    cboSubClientes.Add(dtsSubClientes.Tables[0].Select("IdEstado = '" + cboEstados.Data + "'" + " And IdCliente = '" + cboClientes.Data + "'"), true, "IdSubCliente", "NombreSubCliente");
                }
                catch { }
            }
            cboSubClientes.SelectedIndex = 0;
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            bntBuscarClaves.Visible = false;

            cboClientes.SelectedIndex = 0;
            cboSubClientes.SelectedIndex = 0;

            CargarClientes();
        }

        private void cboClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboSubClientes.SelectedIndex = 0;
            if (cboClientes.SelectedIndex != 0)
            {
                CargarSubClientes();
            }
        }

        private void cboSubClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////if (cboSubClientes.SelectedIndex != 0)
            ////{
            ////    CargarGrupos();
            ////    CargarClaves("");
            ////    ActualizaMenu("2");
            ////    bntBuscarClaves.Visible = true;
            ////}
            ////else
            ////{
            ////    bntBuscarClaves.Visible = false;
            ////    twGrupos.Nodes.Clear();
            ////    lwUsuarios.Items.Clear();
            ////}
        }

        ////private void cboSucursales_SelectedIndexChanged(object sender, EventArgs e)
        ////{
        ////    if (cboClientes.SelectedIndex != 0)
        ////    {
        ////        CargarGrupos();
        ////        CargarClaves("");
        ////        ActualizaMenu("2");
        ////        bntBuscarClaves.Visible = true;
        ////    }
        ////    else
        ////    {
        ////        bntBuscarClaves.Visible = false;
        ////        twGrupos.Nodes.Clear();
        ////        lwUsuarios.Items.Clear();
        ////    }
        ////}
        #endregion Combos 

        #region Funciones
        private void Inicializa()
        {
            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");

            cboClientes.Clear();
            cboClientes.Add("0", "<< Seleccione >>");

            cboSubClientes.Clear();
            cboSubClientes.Add("0", "<< Seleccione >>");

            SolicitarPermisosUsuario();
            CargarEstados();
            ActualizaMenu("0");

            cboEstados.SelectedIndex = 0;
            cboClientes.SelectedIndex = 0;
            cboSubClientes.SelectedIndex = 0;

            cboEstados.Focus();
        }

        private void CargarGrupos()
        {
            int iMiembros = 0; 
            query.MostrarMsjSiLeerVacio = false;
            dtsGrupos = query.NivelesAtencion(cboEstados.Data, cboClientes.Data, "CargarGrupos()");

            twGrupos.Nodes.Clear();
            twGrupos.BeginUpdate();

            TreeNode myNode;
            twGrupos.Nodes.Clear();
            myNode = twGrupos.Nodes.Add("Perfiles");
            myNode.Tag = "-1";
            myNode.ImageIndex = 1;
            myNode.SelectedImageIndex = 1;

            reader.DataSetClase = dtsGrupos;
            // if (reader.Leer())
            {
                string sIdGrupo = "", sNombreGrupo = "", sMiembro = "";

                ////foreach (DataRow dt in dtsGrupos.Tables[0].Rows)
                while(reader.Leer())
                {
                    if (reader.Campo("Status").ToUpper() == "A")
                    {
                        sIdGrupo = reader.Campo("IdNivel");
                        sNombreGrupo = reader.Campo("Nivel");
                        sNombreGrupo = string.Format("[{0}] {1}", sIdGrupo, sNombreGrupo);

                        iMiembros = 0;
                        TreeNode myNodeGrupo = myNode.Nodes.Add(sNombreGrupo);
                        myNodeGrupo.ImageIndex = 1;
                        myNodeGrupo.SelectedImageIndex = 1;
                        myNodeGrupo.Tag = sIdGrupo;

                        reader2.DataSetClase = query.CuadrosBasicos(cboEstados.Data, cboClientes.Data, sIdGrupo, "CargarGrupos()");
                        while (reader2.Leer())
                        {
                            if (reader2.Campo("StatusMiembro").ToUpper() == "A")
                            {
                                sMiembro = string.Format("[{0}] {1} - {2}", reader2.Campo("IdClaveSSA"), reader2.Campo("ClaveSSA"), reader2.Campo("Descripcion Clave")); //dtU["Farmacia"].ToString();
                                
                                TreeNode myNodeGrupoUsuario = myNodeGrupo.Nodes.Add(sMiembro);
                                myNodeGrupoUsuario.ImageIndex = 0; 
                                myNodeGrupoUsuario.SelectedImageIndex = 0;
                                myNodeGrupoUsuario.Tag = "|" + reader2.Campo("IdClaveSSA");
                                iMiembros++;
                            }
                        }

                        sNombreGrupo += string.Format("   ( {0} )", iMiembros);
                        myNodeGrupo.Text = sNombreGrupo; 

                    }
                }
            }

            twGrupos.EndUpdate();
            myNode.Expand();

            twGrupos.Nodes[0].Text = twGrupos.Nodes[0].Text;

        }

        private void CargarClaves(string Criterio)
        {
            Criterio = Criterio.Replace(" ", "%"); 

            string sSql = string.Format(
                "Select Row_Number() Over (Order By DescripcionClave) as Registro, " + 
                " IdEstado, Estado, IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, " +
                " IdClaveSSA, ClaveSSA, ClaveSSA_Aux, DescripcionClave, 'Descripción clave' = DescripcionClave, Precio, Status, StatusRelacion " + 
                " From vw_Claves_Precios_Asignados (NoLock) " +
                " Where IdEstado = '{0}' And IdCliente = '{1}' And IdSubCliente = '{2}' and DescripcionClave like '%{3}%' " +
                " Order By DescripcionClave ", cboEstados.Data, cboClientes.Data, cboSubClientes.Data, Criterio); 
            
            ////dtsDatos = query.Farmacias(cboEstados.Data, "CargarFarmacias()");
            ////reader.DataSetClase = dtsDatos;

            if (!reader.Exec(sSql))
            {
                Error.GrabarError(reader, "CargarClaves()");
                General.msjError("Ocurrió un error al obtener las Claves del Cliente.");
            }
            else
            {
                if (reader.Leer())
                {
                    dtsDatos = reader.DataSetClase;
                    ListViewItem itmX = null;
                    object NewColListView = null;
                    string strValor = "";

                    lwUsuarios.Columns.Clear();
                    lwUsuarios.Items.Clear();
                    lwUsuarios.View = System.Windows.Forms.View.Details;
                    NewColListView = lwUsuarios.Columns.Add("Núm", 80);
                    NewColListView = lwUsuarios.Columns.Add("Descripción clave", 500);

                    reader.RegistroActual = 1; 
                    // foreach (DataRow dt in dtsDatos.Tables[0].Rows)
                    while( reader.Leer() )
                    {
                        strValor = reader.CampoInt("Registro").ToString();
                        itmX = lwUsuarios.Items.Add(strValor, 0);

                        strValor = "[" + reader.Campo("IdClaveSSA") + "]" + "   " + reader.Campo("ClaveSSA") + " - " + reader.Campo("Descripción clave");
                        itmX.SubItems.Add("" + strValor);
                        itmX.SubItems[0].Tag = reader.Campo("IdClaveSSA");
                    }
                }
            }
        }

        private void bntBuscarClaves_Click(object sender, EventArgs e)
        {
            FrmCriterioDeBusqueda B = new FrmCriterioDeBusqueda();
            string sCriterio = B.MostarCriterio();

            if (B.ExisteCriterio)
            {
                CargarClaves(sCriterio);
            }
        }
        #endregion Funciones

        #region Treeview  
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sQuery = ""; 
            string sDefault = " Set FechaUpdate = getdate(), Status = 'C', Actualizado = '0' "; 

            try
            {
                if (myNodeSeleccionado.Tag.ToString() != "-1")
                {
                    if (!myNodeSeleccionado.Tag.ToString().Contains("|"))
                    {
                        sQuery = sQuery + string.Format("Update CFG_CB_CuadroBasico_Claves {3} Where IdEstado = '{0}' and IdCliente = '{1}' and IdNivel = '{2}' ",
                            cboEstados.Data, cboClientes.Data, sIdGrupo, sDefault);

                        //sQuery = sQuery + string.Format("Update CFG_CB_NivelesAtencion {3} Where IdEstado = '{0}' and IdCliente = '{1}' and IdNivel = '{2}' ",
                        //    cboEstados.Data, cboClientes.Data, sIdGrupo, sDefault);

                    }
                    else
                    {
                        sQuery = string.Format("Update CFG_CB_CuadroBasico_Claves {4} Where IdEstado = '{0}' and IdCliente = '{1}' and IdNivel = '{2}' and IdClaveSSA_Sal = '{3}' ",
                            cboEstados.Data, cboClientes.Data, sIdGrupo, sIdClave, sDefault);
                    }

                    if ( reader.Exec(sQuery) )  
                        twGrupos.Nodes.Remove(myNodeSeleccionado);

                    // Recargar los grupos no importa que se hayan cancelado todos sus usuarios 
                    CargarGrupos(); 
                }
            }
            catch { }
        }

        private void twGrupos_AfterSelect(object sender, TreeViewEventArgs e)
        {
            myNodeSeleccionado = e.Node;
            iIndexNodo = e.Node.Index;

            if (twGrupos.Nodes.Count > 0)
            {
                ActualizaMenu(e.Node.Tag.ToString());

                if (!myNodeSeleccionado.Tag.ToString().Contains("|"))
                    sIdGrupo = myNodeSeleccionado.Tag.ToString();
                else
                {
                    sIdGrupo = myNodeSeleccionado.Parent.Tag.ToString();
                    sIdClave = myNodeSeleccionado.Tag.ToString().Replace("|", "");
                }

            }
        }

        private void ActualizaMenu(string Tag)
        {
            mnGrupos.Items[eliminarToolStripMenuItem.Name].Enabled = true;
            mnGrupos.Items[actualizarToolStripMenuItem.Name].Enabled = true;            

            if (Tag == "0")
            {
                mnGrupos.Items[eliminarToolStripMenuItem.Name].Enabled = false;
                mnGrupos.Items[actualizarToolStripMenuItem.Name].Enabled = false;    
            }
            else
            {
                if (Tag == "-1")
                {
                    mnGrupos.Items[eliminarToolStripMenuItem.Name].Enabled = false;
                }
                else
                {
                    if (Tag.Substring(0, 1) != "|")
                    {
                        mnGrupos.Items[eliminarToolStripMenuItem.Name].Enabled = true;
                    }
                }
            }
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CargarGrupos();
        }

        private void actualizarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CargarClaves("");
        }

        private void agregarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }

        private void eliminarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }

        #endregion Treeview
        
        #region Arrastrar Claves 
        private void twGrupos_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode NewNode; 
            Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
            NewNode = twGrupos.GetNodeAt(pt);
            sIdGrupo = NewNode.Tag.ToString();
            string sClave = lwUsuarios.FocusedItem.SubItems[1].Text;
            int iOpcion = 1;

            string sSql = string.Format("Exec spp_Mtto_CFG_CB_CuadroBasico_Claves @IdEstado = '{0}', @IdCliente = '{1}', @IdNivel = '{2}', @IdClaveSSA_Sal = '{3}', @iOpcion = '{4}' ",
                cboEstados.Data, cboClientes.Data, sIdGrupo, sIdClave, iOpcion);

            //if (!ExisteOpcion(NewNode, sIdClave + " - " + sClaveDesc))
            if (!ExisteOpcion(NewNode, sClave))
            {
                //CargarGrupos();
                if (reader.Exec(sSql)) 
                {
                    //TreeNode myNodeRama = NewNode.Nodes.Add(sIdClave + " - " + sClaveDesc);
                    TreeNode myNodeRama = NewNode.Nodes.Add(sClave);
                    myNodeRama.ImageIndex = 0;
                    myNodeRama.SelectedImageIndex = 0;
                    myNodeRama.Tag = sIdClave;
                    // AsignarIcono(myNodeRama, RamaSeleccionada.TipoRama);
                }
                NewNode.Expand();
            }
        }

        private bool ExisteOpcion(TreeNode myNodeRama, string Rama)
        {
            bool bRegresa = false;
            string sRamaBuscar = myNodeRama.FullPath.ToUpper() + "|" + Rama.ToUpper();
            string myRama = "";

            foreach (TreeNode Nodo in myNodeRama.Nodes)
            {
                myRama = Nodo.FullPath.ToUpper();

                if (sRamaBuscar == myRama)
                {
                    bRegresa = true;
                    break;
                }
            }

            return bRegresa;
        }

        private void twGrupos_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void twGrupos_ItemDrag(object sender, ItemDragEventArgs e)
        {
            // DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void lwUsuarios_ItemDrag(object sender, ItemDragEventArgs e)
        {
            sIdClave = lwUsuarios.FocusedItem.SubItems[0].Tag.ToString();
            sClaveDesc = lwUsuarios.FocusedItem.SubItems[1].Text.Substring(8).Trim();
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        #endregion Arrastrar Claves


        #region Funciones y Procedimientos Privados
        ////private void Inicializa()
        ////{
        ////    cboEstados.Clear();
        ////    cboEstados.Add("0", "<< Seleccione >>");

        ////    cboClientes.Clear();
        ////    cboClientes.Add("0", "<< Seleccione >>");

        ////    cboSubClientes.Clear();
        ////    cboSubClientes.Add("0", "<< Seleccione >>");

        ////    CargarEstados();

        ////    cboEstados.SelectedIndex = 0;
        ////    cboClientes.SelectedIndex = 0;
        ////    cboSubClientes.SelectedIndex = 0;

        ////    cboEstados.Focus();
        ////}

        private void CargarArchivo()
        {
            string sHoja = "";
            bool bRegresa = false;

            bValidandoInformacion = true;
            MostrarEnProceso(true, 1);
            //FrameResultado.Text = sTitulo;

            //excel = new clsLeerExcel(sFile_In); 
            excel = new clsLeerExcelOpenOficce(sFile_In);
            excel.GetEstructura();

            //lst.Limpiar();
            Thread.Sleep(1000);

            //bRegresa = excel.Hojas.Registros > 0;
            while (excel.Hojas.Leer())
            {
                sHoja = excel.Hojas.Campo("Hoja").ToUpper();
                if (sHoja == sNombreHoja)
                {
                    bRegresa = true;
                    break;
                }
            }

            bExisteHoja = bRegresa;
            if (bRegresa)
            {
                bRegresa = LeerHoja();
            }

            MostrarEnProceso(false, 1);
            bValidandoInformacion = false;
            bActivarProceso = bRegresa;


            ////IniciaToolBar(true, true, true, false); 


            //////if (!bRegresa)
            //////{
            //////    IniciaToolBar(true, true, true, false); 
            //////}
            //////else
            //////{
            //////    IniciaToolBar(true, true, false, true); 
            //////    //General.msjUser("Información precargada");
            //////}

            //return bRegresa;
        }

        private bool LeerHoja()
        {
            bool bRegresa = false;

            //FrameResultado.Text = sTitulo;
            //lst.Limpiar();
            excel.LeerHoja(sNombreHoja);

            //FrameResultado.Text = sTitulo;
            if (excel.Leer())
            {
                bRegresa = true;
                iRegistrosHoja = excel.Registros;
                //FrameResultado.Text = string.Format("{0}: {1} registros ", sTitulo, iRegistrosHoja.ToString(sFormato));
                //lst.CargarDatos(excel.DataSetClase, true, true);
            }

            if (bRegresa)
            {
                bRegresa = CargarInformacionDeHoja();
            }

            return bRegresa;
        }

        private bool CargarInformacionDeHoja()
        {
            bool bRegresa = false;
            lblProcesados.Visible = true;


            clsBulkCopy bulk = new clsBulkCopy(General.DatosConexion);

            bulk.NotifyAfter = 500;
            bulk.RowsCopied += new clsBulkCopy.RowsCopiedEventHandler(bulk_RowsCopied);
            bulk.OnCompled += new clsBulkCopy.RowsCopiedEventHandler(bulk_Compled);
            bulk.OnError += new clsBulkCopy.RowsCopiedEventHandler(bulk_Error);

            bulk.ClearColumns();
            bulk.DestinationTableName = "CFG_CB_CuadroBasico_Claves__CargaMasiva";
            bulk.AddColumn("IdEstado", "IdEstado");
            bulk.AddColumn("IdCliente", "IdCliente");
            bulk.AddColumn("IdNivel", "IdNivel");


            if (excel.ExisteTablaColumna(1, "IdClaveSSA_Sal".ToUpper()))
            {
                bulk.AddColumn("IdClaveSSA_Sal", "IdClaveSSA_Sal");
            }


            if (excel.ExisteTablaColumna(1, "Clave SSA".ToUpper()))
            {
                bulk.AddColumn("Clave SSA", "ClaveSSA");
            }

            if (excel.ExisteTablaColumna(1, "EmiteVale".ToUpper()))
            {
                bulk.AddColumn("EmiteVale", "EmiteVale");
            }


            bulk.AddColumn("Status", "Status");


            reader.Exec("Truncate table CFG_CB_CuadroBasico_Claves__CargaMasiva ");

            bRegresa = bulk.WriteToServer(excel.DataSetClase); //, System.Data.SqlClient.SqlBulkCopyOptions.Default); 


            if (bRegresa)
            {
                bRegresa = validarInformacion();
            }

            return bRegresa;
        }

        private void bulk_RowsCopied(RowsCopiedEventArgs e)
        {
            lblProcesados.Text = string.Format("Procesados {0} de {1}", e.RowsCopied.ToString(sFormato), excel.Registros.ToString(sFormato));
        }

        private void bulk_Compled(RowsCopiedEventArgs e)
        {
            lblProcesados.Text = string.Format("Procesados {0} de {1}", e.RowsCopied.ToString(sFormato), excel.Registros.ToString(sFormato));
        }

        private void bulk_Error(RowsCopiedEventArgs e)
        {
            lblProcesados.Text = string.Format("Ocurrio un error al procesar {0} de {1}", e.RowsCopied.ToString(sFormato), excel.Registros.ToString(sFormato));
            Error.GrabarError(e.Error, "bulk_Error");
        }

        private bool validarInformacion()
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_PRCS_OCEN__IntegracionDeCuadrosBasicos_000_Validar_Datos  @IdEstado = '{0}', @IdCliente = '{1}'  ",
                cboEstados.Data, cboClientes.Data );

            if (!reader.Exec(sSql))
            {
                Error.GrabarError(reader, "validarInformacion()");
            }
            else
            {
                bRegresa = ValidarInformacion_Entrada();
            }

            return bRegresa;
        }

        private bool ValidarInformacion_Entrada()
        {
            bool bRegresa = true;
            clsLeer leerValidacion = new clsLeer();
            clsLeer leerResultado = new clsLeer();
            DataSet dtsResultados = new DataSet();

            ////leer.RenombrarTabla(1, "IMSS no registrados");
            ////leer.RenombrarTabla(2, "Condiciones de pago");
            ////leer.RenombrarTabla(3, "Método de pago");
            ////leer.RenombrarTabla(4, "Cuenta de Pago");
            reader.RenombrarTabla(1, "Resultados");
            leerResultado.DataTableClase = reader.Tabla(1);
            while (leerResultado.Leer())
            {
                reader.RenombrarTabla(leerResultado.CampoInt("Orden"), leerResultado.Campo("NombreTabla"));
            }

            dtsResultados = reader.DataSetClase;
            dtsResultados.Tables.Remove("Resultados");
            reader.DataSetClase = dtsResultados;

            foreach (DataTable dt in reader.DataSetClase.Tables)
            {
                if (!bActivarProceso)
                {
                    leerValidacion.DataTableClase = dt.Copy();
                    bActivarProceso = leerValidacion.Registros > 0;
                }
            }

            bRegresa = !bActivarProceso;

            return bRegresa;
        }

        private bool IntegrarInformacion()
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_PRCS_OCEN__IntegracionDeCuadrosBasicos_001  @IdEstado = '{0}', @IdCliente = '{1}', @IdSubCliente = '{2}' ",
                cboEstados.Data, cboClientes.Data, cboSubClientes.Data );

            btnGuardar.Enabled = false;
            if (!reader.Exec(sSql))
            {
                Error.GrabarError(reader, "IntegrarInformacion()");
                btnGuardar.Enabled = true;
                General.msjError("Ocurrió un error al integrar la información.");
            }
            else
            {
                General.msjUser("Cuadro Básico integrado satisfactoriamente.");
            }

            return bRegresa;
        }
        #endregion Funciones y Procedimientos Privados

        #region Eventos
        private void BloqueaControles(bool Bloquear)
        {
            bool bBloquear = !Bloquear;

            cboEstados.Enabled = bBloquear;
            cboClientes.Enabled = bBloquear;
            cboSubClientes.Enabled = bBloquear;
        }

        private void tmValidacion_Tick(object sender, EventArgs e)
        {
            tmValidacion.Stop();
            tmValidacion.Enabled = false;

            if (!bValidandoInformacion)
            {
                if (!bExisteHoja)
                {
                    General.msjAviso("El archivo cargado no contiene la hoja llamada Perfiles.");
                }
                else
                {
                    if (bActivarProceso)
                    {
                        BloqueaControles(true);
                        IniciaToolBar(true, true, false, true);
                    }
                    else
                    {
                        BloqueaControles(false);
                        IniciaToolBar(true, true, true, false);
                        if (!bErrorAlValidar)
                        {
                            FrmIncidencias f = new FrmIncidencias("Cuadro Básico (Perfiles)", reader.DataSetClase);
                            f.ShowDialog(this);
                        }
                    }
                }
            }
            else
            {
                tmValidacion.Enabled = true;
                tmValidacion.Start();
            }
        }
        #endregion Eventos


        
    }//LLAVES DE LA CLASE
}
