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
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Usuarios_y_Permisos;

using DllFarmaciaSoft.ExportarExcel;

namespace DllFarmaciaSoft.Usuarios_y_Permisos
{
    public partial class FrmRpt_MatrizPermisosUsuario : FrmBaseExt
    {
        //private struct FormasArbol
        //{
        //    public string Rama;
        //    public string Forma;
        //    public int TipoRama;
        //    public string NameSpace;
        //}

        clsRamaArbolNavegacion RamaSeleccionada;
        //FormasArbol[] Formas = new FormasArbol[1];
        //basGenerales Fg = new basGenerales();
        clsGuardarSC guardarDatos = new clsGuardarSC();
        clsConsultasSC query;
        DataSet dtsDatos = new DataSet(), dtsPermisos = new DataSet();
        DataSet dtsGrupos = new DataSet(), dtsUsuariosGrupo = new DataSet();
        DataSet dtsFarmacias = new DataSet();



        DataSet dtsExportarPermisos = new DataSet();


        TreeNode myNodeSeleccionado;
        TreeNode myNodePadreSelGrupos, myNodeSelGrupos;


        int iIndexNodo = 0;
        string sArbolSel = "";
        string sUsuarioSel = ""; 

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsConsultas leer = new clsConsultas(General.DatosConexion, "Configuracion", "GruposDeUsuarios", Application.ProductVersion, true);
        clsLeer reader;

        public FrmRpt_MatrizPermisosUsuario()
        {
            InitializeComponent();
        }

        private void FrmGruposUsuarios_Load(object sender, EventArgs e)
        {
            query = new clsConsultasSC();
            reader = new clsLeer(ref cnn);

            LimpiarPantalla(); 

            CargarEstados(); 
            CargarArboles();
            //CargarGrupos();
        }

        private static Type GetType( TypeCode TipoDato )
        {
            return Type.GetType("System." + TipoDato.ToString());
        }
        public static DataSet PreparaDtsPermisos()
        {
            // Nombres de columnas no mayores de 10 caracteres 
            DataSet dts = new DataSet();
            DataTable dtGrupo = new DataTable("Grupos");
            DataTable dtPermiso_x_Grupo = new DataTable("Permisos_x_Grupos");

            dtGrupo.Columns.Add("Id Farmacia", GetType(TypeCode.String));
            dtGrupo.Columns.Add("Farmacia", GetType(TypeCode.String));
            dtGrupo.Columns.Add("Grupo", GetType(TypeCode.String));

            dtPermiso_x_Grupo.Columns.Add("Id Farmacia", GetType(TypeCode.String));
            dtPermiso_x_Grupo.Columns.Add("Farmacia", GetType(TypeCode.String));
            dtPermiso_x_Grupo.Columns.Add("Grupo", GetType(TypeCode.String));
            dtPermiso_x_Grupo.Columns.Add("Usuario", GetType(TypeCode.String));
            dtPermiso_x_Grupo.Columns.Add("Modulo", GetType(TypeCode.String));
            dtPermiso_x_Grupo.Columns.Add("Opcion", GetType(TypeCode.String));

            dts.Tables.Add(dtGrupo);
            dts.Tables.Add(dtPermiso_x_Grupo);

            return dts.Clone();
        }

        #region Botones
        private void LimpiarPantalla()
        {
            Fg.IniciaControles(); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (cboSucursales.SelectedIndex != 0)
            {
                CargarGrupos();
            }
            else
            {
                twGruposPermisos.Nodes.Clear();
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            ////dtsExportarPermisos = PreparaDtsPermisos();
            ////TreeNode nodoRaiz = twGruposPermisos.Nodes[0];

            ////string sIdFarmacia = cboSucursales.Data; 
            ////string sFarmacia = cboSucursales.Text; 
            ////string sModulo = cboArboles.Text; 
            ////string sGrupo = ""; 
            //////object[] objGrupo;
            ////bool bGenerarDocumento = false;


            ////if( bGenerarDocumento )
            ////{
            ////    foreach(TreeNode grupo in nodoRaiz.Nodes)
            ////    {
            ////        sGrupo = grupo.Text;
            ////        object[] objGrupo = { sIdFarmacia, sFarmacia, sGrupo };
            ////        dtsExportarPermisos.Tables[0].Rows.Add(objGrupo);

            ////        foreach(TreeNode usuario in grupo.Nodes)
            ////        {
            ////            twGruposPermisos.SelectedNode = usuario;
            ////            CargarArbolNavegacion();

            ////            generarInformacionMatriz(sGrupo, sUsuarioSel, sModulo);
            ////        }
            ////    }
            ////}

            Generar_Excel(); 
        }

        private void Generar_Excel()
        {
            clsGenerarExcel excel = new clsGenerarExcel();
            string sNombreDocumento = "MATRIZ DE PERMISOS";
            string sNombreHoja = "MatrizDePermisos";
            string sConcepto = "MATRIZ DE PERMISOS";
            clsLeer leer = new clsLeer();
            clsLeer leerDatos = new clsLeer();

            string sIdFarmacia = cboSucursales.Data;
            string sFarmacia = cboSucursales.Text;
            string sModulo = cboArboles.Data;
            string sGrupo = "";


            int iHoja = 1, iRenglon = 15;
            int iColBase = 2;
            int iColsEncabezado = 8;

            TreeNode nodoRaiz = null;

            leerDatos.DataRowsClase = dtsFarmacias.Tables[0].Select(string.Format(" IdEstado = '{0}' and IdFarmacia = '{1}' ", cboEstados.Data, sIdFarmacia));
            dtsExportarPermisos = PreparaDtsPermisos();




            DateTime dtpFecha = General.FechaSistema;
            int iAño = dtpFecha.AddMonths(-1).Year, iMes = dtpFecha.AddMonths(-1).Month;

            //string sEmpresa = DtGeneral.EmpresaConectadaNombre;
            //string sEstado = DtGeneral.EstadoConectadoNombre;
            //string sFarmacia = DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre;
            string sFechaImpresion = General.FechaSistemaFecha.ToString();

            excel = new clsGenerarExcel();
            excel.RutaArchivo = @"C:\\Excel";
            excel.NombreArchivo = sNombreDocumento;
            excel.AgregarMarcaDeTiempo = true;

           
            if(excel.PrepararPlantilla(sNombreDocumento))
            {
                //foreach(string idfarmacia in  ) 
                while(leerDatos.Leer())
                {
                    sIdFarmacia = leerDatos.Campo("IdFarmacia");
                    sFarmacia = leerDatos.Campo("NombreFarmacia");

                    cboSucursales.Data = sIdFarmacia;
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(100);


                    CargarGrupos(cboEstados.Data, sIdFarmacia);
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(100);


                    nodoRaiz = twGruposPermisos.Nodes[0];

                    //// Obtener la informacion de los permisos 
                    foreach(TreeNode grupo in nodoRaiz.Nodes)
                    {
                        sGrupo = grupo.Text;
                        object[] objGrupo = { sIdFarmacia, sFarmacia, sGrupo };
                        dtsExportarPermisos.Tables[0].Rows.Add(objGrupo);

                        foreach(TreeNode usuario in grupo.Nodes)
                        {
                            twGruposPermisos.SelectedNode = usuario;
                            CargarArbolNavegacion(sIdFarmacia, sModulo);

                            Application.DoEvents();
                            System.Threading.Thread.Sleep(100);

                            generarInformacionMatriz(sIdFarmacia, sFarmacia, sGrupo, sUsuarioSel, sModulo);
                        }
                    }
                    //// Obtener la informacion de los permisos 
                }

                leerDatos.DataSetClase = dtsExportarPermisos;
                leer.DataTableClase = leerDatos.Tabla(2);


                excel.ArchivoExcel.Worksheets.Add(sNombreHoja);
                //excel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                //excel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, sFarmacia);
                excel.EscribirCeldaEncabezado(sNombreHoja, 4, iColBase, iColsEncabezado, 14, sConcepto);
                excel.EscribirCeldaEncabezado(sNombreHoja, 6, iColBase, iColsEncabezado, 12, string.Format("Fecha de Impresión: {0} ", General.FechaSistemaObtener()));

                iRenglon = 8;
                //excel.LlenarDetalleHorizontal(sNombreHoja, iRenglon, iColBase, leer.DataSetClase); 
                excel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);

                //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);

                excel.CerraArchivo();

                excel.AbrirDocumentoGenerado(true);
            }
        }

        private void generarInformacionMatriz( string IdFarmacia, string Farmacia, string Grupo, string Usuario, string Modulo )
        {
            string sSql = "";
            //dtsPermisos = leerPermisos.DataSetClase.Clone();

            foreach(TreeNode nodo in twNavegador.Nodes)
            {
                //sModulo = nodo.Text;
                getNodes(nodo, IdFarmacia, Farmacia, Grupo, Usuario, Modulo);
            }

            //leerPermisos.DataSetClase = dtsPermisos;
            //if(!leerPermisos.Leer())
            //{
            //}
            //else
            //{
            //}
        }

        private void getNodes( TreeNode NodoPadre, string IdFarmacia, string Farmacia, string Grupo, string Usuario, string Modulo)
        {
            foreach(TreeNode nodo in NodoPadre.Nodes)
            {
                if(nodo.Nodes.Count > 0)
                {
                    getNodes(nodo, IdFarmacia, Farmacia, Grupo, Usuario, Modulo);
                }
                else
                {
                    if(((clsRamaArbolNavegacion)nodo.Tag).TipoRama == "3")
                    {
                        clsRamaArbolNavegacion rama = (clsRamaArbolNavegacion)nodo.Tag;

                        object[] obj = { IdFarmacia, Farmacia, Grupo, Usuario, Modulo, nodo.FullPath };
                        dtsExportarPermisos.Tables[1].Rows.Add(obj);
                    }
                }
            }
        }
        #endregion Botones

        private void CargarEstados()
        {
            string sSql = " Select Distinct IdEstado, (IdEstado + ' -- ' + Estado ) Estado From vw_Farmacias_Urls (NoLock) Order by IdEstado ";
            cboEstados.Clear();
            cboEstados.Add();

            if (!reader.Exec(sSql))
            {
                Error.GrabarError(reader, "CargarEstados()");
                General.msjError("Ocurrió un error al Cargar la Lista de Estados."); 
            }
            else
            {
                if (reader.Leer())
                {
                    cboEstados.Add(reader.DataSetClase, true, "IdEstado", "Estado");
                }
            }

            //cboEstados.Clear();
            //cboEstados.Add("0", "<< Seleccione >>");
            //cboEstados.Add(leer.ComboEstados("CargarEstados()"), true, "IdEstado", "Nombre");

            reader.Exec("Select *, (IdFarmacia + ' - ' + NombreFarmacia) as NombreDeFarmacia From CatFarmacias (NoLock) Where Status = 'A' Order By IdEstado, IdFarmacia ");
            dtsFarmacias = reader.DataSetClase;

            cboEstados.SelectedIndex = 0; 

        }

        private void CargarFarmacias()
        {
            cboSucursales.Clear();
            cboSucursales.Add("0", "<< Seleccione >>");

            if (cboEstados.SelectedIndex != 0)
                cboSucursales.Add(dtsFarmacias.Tables[0].Select("IdEstado = '" + cboEstados.Data + "'"), true, "IdFarmacia", "NombreDeFarmacia");
    
            cboSucursales.SelectedIndex = 0;
        }

        private void CargarArboles()
        {
            dtsDatos = query.Arboles();

            if (query.ExistenDatos)
            {
                cboArboles.Clear();
                cboArboles.Add("0", "<< Seleccione >>");
                cboArboles.Add(dtsDatos, true, "Arbol", "Nombre");
                cboArboles.SelectedIndex = 0;
            }
        }

        private void CargarGrupos()
        {
            CargarGrupos(cboEstados.Data, cboSucursales.Data);
        }
        private void CargarGrupos(string IdEstado, string IdFarmacia )
        {
            string sSql = string.Format("Select * From Net_Grupos_De_Usuarios (noLock) " +
                " Where IdEstado = '{0}' and IdSucursal = '{1}' ", 
                IdEstado, IdFarmacia);

            if (reader.Exec(sSql))
            {
                dtsGrupos = reader.DataSetClase;
            }

            //dtsGrupos = query.Grupos(General.EntidadConectada);

            string sIdGrupo = "", sNombreGrupo = "", sUsuario = ""; 
            string sNombreOpcion = ""; 
            twGruposPermisos.Nodes.Clear();
            twGruposPermisos.BeginUpdate();

            TreeNode myNode;
            twGruposPermisos.Nodes.Clear();
            myNode = twGruposPermisos.Nodes.Add("Grupos");
            myNode.Tag = "-1";
            myNode.ImageIndex = 1;
            myNode.SelectedImageIndex = 1; 

            if (reader.Leer())
            {
                foreach (DataRow dt in dtsGrupos.Tables[0].Rows)
                {
                    sIdGrupo = dt["IdGrupo"].ToString();
                    sNombreGrupo = dt["NombreGrupo"].ToString();

                    TreeNode myNodeGrupo = myNode.Nodes.Add(sNombreGrupo);
                    myNodeGrupo.ImageIndex = 1;
                    myNodeGrupo.SelectedImageIndex = 1;
                    myNodeGrupo.Tag = sIdGrupo;  

                    string sQuery = " Select G.IdSucursal, G.IdGrupo, G.NombreGrupo, U.IdPersonal, (U.IdPersonal + ' - ' + M.LoginUser) as LoginUser " +
                        " From Net_Grupos_De_Usuarios G (NoLock) " +
                        " Inner Join Net_Grupos_Usuarios_Miembros M (NoLock) On ( G.IdEstado = M.IdEstado and G.IdSucursal = M.IdSucursal and G.IdGrupo = M.IdGrupo ) " +
                        " Left Join Net_Usuarios U (NoLock) On ( U.IdEstado = M.IdEstado and U.IdSucursal = M.IdSucursal and U.IdPersonal = M.IdPersonal ) " +
                        " Where G.IdEstado = '" + IdEstado + "' and G.IdSucursal = '" + IdFarmacia + "' " +
                        " and G.IdGrupo = '" + sIdGrupo + "' and M.Status = 'A' " +
                        " Order by  G.NombreGrupo, M.LoginUser ";

                    //dtsUsuariosGrupo = query.GruposUsuarios(General.EntidadConectada, sIdGrupo);
                    reader.Exec(sQuery);

                    if (reader.Leer())
                    {
                        dtsUsuariosGrupo = reader.DataSetClase;
                        foreach (DataRow dtU in dtsUsuariosGrupo.Tables[0].Rows)
                        {
                            sUsuario = dtU["LoginUser"].ToString();
                            TreeNode myNodeGrupoUsuario = myNodeGrupo.Nodes.Add(sUsuario);
                            myNodeGrupoUsuario.ImageIndex = 0;
                            myNodeGrupoUsuario.SelectedImageIndex = 0;
                            myNodeGrupoUsuario.Tag = "|" + sUsuario;

                        }
                    }
                }
            }

            twGruposPermisos.EndUpdate();
            twGruposPermisos.Nodes[0].Text = twGruposPermisos.Nodes[0].Text;
            myNode.Expand();
        }

        private int TipoDeRama(string TipoRama)
        {
            int iRegresa = 0;

            if(TipoRama == "1" || TipoRama == "2")
            {
                iRegresa = 2;
            }
            else
            {
                iRegresa = 3;
            }

            return iRegresa;
        }

        private DataSet MenuPermisos()
        {
            return MenuPermisos(cboSucursales.Data, cboArboles.Data); 
        }

        private DataSet MenuPermisos(string IdFarmacia, string Arbol)
        {
            DataSet dts = new DataSet();
            clsLeer leerArbol = new clsLeer(ref cnn);

            string sSql = string.Format(" Exec sp_Permisos '{0}', '{1}', '{2}', '{3}' ",
                cboEstados.Data, IdFarmacia, Arbol, sUsuarioSel);

            leerArbol.Exec(sSql);
            dts = leerArbol.DataSetClase; 

            return dts; 
        }

        private void CargarArbolNavegacion()
        {
            CargarArbolNavegacion(cboSucursales.Data, cboArboles.Data); 
        }
        private void CargarArbolNavegacion( string IdFarmacia, string Arbol )
        {
            clsArbolNavegacion myArbol = new clsArbolNavegacion("Kiubo");
            clsRamaArbolNavegacion myRamaArbol;
            //DataColumn myDataColumn;
            TreeNode myNode;
            clsLeer leerArbol = new clsLeer(); 

            int vlNodos = 0;
            string sArbol = cboArboles.Data;
            string sRaiz = cboArboles.Text;
            string sRutaRaiz = "";
            sArbolSel = sArbol;


            //leerArbol.DataSetClase = query.ArbolNavegacion(sArbol);

            twNavegador.Nodes.Clear();
            twNavegador.BeginUpdate();
            twNavegador.Nodes.Clear();
            myNode = twNavegador.Nodes.Add("Cargando permisos de usuario");
            myNode.Tag = "1";
            myNode.ImageIndex = 0;
            myNode.SelectedImageIndex = 0;
            twNavegador.EndUpdate();
            System.Threading.Thread.Sleep(250); 

            leerArbol.DataSetClase = MenuPermisos(IdFarmacia, Arbol); 

            twNavegador.Nodes.Clear();
            twNavegador.BeginUpdate();
            twNavegador.Nodes.Clear();
            myNode = twNavegador.Nodes.Add(sRaiz);
            myNode.Tag = "1";
            myNode.ImageIndex = 0;
            myNode.SelectedImageIndex = 0;



            if (leerArbol.Registros > 0)
            {
                //Formas = new FormasArbol[dtsPermisos.Tables["Arbol"].Rows.Count];

                //foreach (DataRow rw in dtsPermisos.Tables["Arbol"].Rows)
                while(leerArbol.Leer()) 
                {
                    myRamaArbol = new clsRamaArbolNavegacion();

                    myRamaArbol.Arbol = leerArbol.Campo("Arbol");
                    myRamaArbol.Rama = leerArbol.CampoInt("RAMA");
                    myRamaArbol.Nombre = leerArbol.Campo("NOMBRE");
                    myRamaArbol.Padre = leerArbol.CampoInt("PADRE");
                    myRamaArbol.Forma = leerArbol.Campo("FORMALOAD");
                    myRamaArbol.myNameSpace = leerArbol.Campo("GrupoOpciones");
                    myRamaArbol.Orden = leerArbol.CampoInt("IDORDEN");
                    myRamaArbol.TipoRama = leerArbol.Campo("TipoRama");
                    myRamaArbol.RutaCompleta = ">" + myRamaArbol.Arbol + "|" + leerArbol.Campo("RutaCompleta");
                    myRamaArbol.RutaCompleta = myRamaArbol.RutaCompleta.Replace("|", "||");

                    if (vlNodos == 0)
                    {
                        myRamaArbol.Nombre = sRaiz;
                        RamaSeleccionada = myRamaArbol;
                    }

                    myArbol.Ramas.Add(myRamaArbol);
                    //Formas[vlNodos].Forma = myRamaArbol.Forma;
                    //Formas[vlNodos].Rama = myRamaArbol.Rama.ToString();
                    //Formas[vlNodos].TipoRama = Fg.Val(rw["TipoRama"].ToString());
                    //Formas[vlNodos].NameSpace = myRamaArbol.myNameSpace.ToString();
                    vlNodos += 1;
                }

                // Obtener el nombre del arbol
                leerArbol.RegistroActual = 1;
                if (leerArbol.Leer())
                {
                    sRaiz = leerArbol.Campo("Nombre");
                    sRutaRaiz = leerArbol.Campo("RutaCompleta");

                    // Asignar el nombre del arbol
                    myArbol.Nombre = sRaiz;
                    AgregarRamasHijas(myNode, myArbol, "", true);

                    myNode.ImageIndex = 0;
                    myNode.SelectedImageIndex = 0;
                    myNode.Tag = RamaSeleccionada;
                }
            }
            twNavegador.EndUpdate();
            myNode.Expand();
        }

        private void AgregarRamasHijas(TreeNode NodoPadre, clsArbolNavegacion Arbol, string TipoRama, bool DefineImages)
        {
            //bool bNodoTerminal = true;

            foreach (clsRamaArbolNavegacion Rama in Arbol.Ramas)
            {
                if (Rama.Padre.ToString() == NodoPadre.Tag.ToString())
                {
                    //bNodoTerminal = false;
                    TreeNode myNode = NodoPadre.Nodes.Add(Rama.Nombre);
                    myNode.Tag = Rama.Rama.ToString();
                    AgregarRamasHijas(myNode, Arbol, Rama.TipoRama, DefineImages);
                    myNode.Tag = Rama;
                    // myNode.Tag = Rama.TipoRama.ToString();
                }
            }

            if (DefineImages)
            {
                if (TipoRama == "2" || TipoRama == "3")
                {
                    if (TipoRama == "2")
                    {
                        // Nodo
                        NodoPadre.ImageIndex = 1;
                        NodoPadre.SelectedImageIndex = 1;
                    }
                    else
                    {
                        // Hoja
                        NodoPadre.ImageIndex = 2;
                        NodoPadre.SelectedImageIndex = 2;
                    }
                }
            }
        }

        private void twGruposPermisos_DragDrop(object sender, DragEventArgs e)
        {
        }

        private void AsignarIcono(TreeNode myNode, string TipoRama)
        {
            if (TipoRama == "1" || TipoRama == "2")
            {
                // Nodo
                myNode.ImageIndex = 2;
                myNode.SelectedImageIndex = 2;
            }
            else
            {
                // Hoja
                myNode.ImageIndex = 3;
                myNode.SelectedImageIndex = 3;
            }
        } 

        private void twGruposPermisos_AfterSelect(object sender, TreeViewEventArgs e)
        {           

            try
            {
                myNodeSelGrupos = e.Node;
                myNodePadreSelGrupos = e.Node.Parent;
            }
            catch 
            {
                myNodePadreSelGrupos = e.Node.Parent;
            }

            try
            {
                string sTag = Fg.Mid(myNodeSelGrupos.Tag.ToString(), 1, 1);

                if(sTag == "|")
                {
                    sTag = Fg.Mid(myNodeSelGrupos.Tag.ToString(), 8).Trim();
                    sUsuarioSel = sTag;
                }
                else
                {
                    myNodeSelGrupos = e.Node.FirstNode;
                    sUsuarioSel = ""; 
                    if(myNodeSelGrupos != null)
                    {
                        sTag = Fg.Mid(myNodeSelGrupos.Tag.ToString(), 1, 1);
                        sTag = Fg.Mid(myNodeSelGrupos.Tag.ToString(), 8).Trim();
                        sUsuarioSel = sTag;
                    }
                }
            }
            catch { }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Borrar");
            string sDefault = " Set FechaUpdate = getdate(), Status = 'C', Actualizado = '0' "; 

            bool bRama = false;
            clsRamaArbolNavegacion Rama;
            TreeNode myNodoBorrar;

            try
            {
                string sQuery = "", sMyGrupo = myNodePadreSelGrupos.Tag.ToString();
                sMyGrupo = sMyGrupo.Replace("|", "");
                sMyGrupo = sMyGrupo.Replace(">", "");                

                try
                {
                    Rama = (clsRamaArbolNavegacion)myNodeSelGrupos.Tag;
                    myNodoBorrar = myNodeSelGrupos;
                    bRama = true;

                    ////sQuery = string.Format("Delete From Net_Privilegios_Grupo Where IdEstado = '{0}' and IdSucursal = '{1}' and IdGrupo = '{2}' and Arbol = '{3}' and Rama = '{4}' ",
                    ////    cboEstados.Data, cboSucursales.Data, sMyGrupo, Rama.Arbol, Rama.Rama);

                    sQuery = string.Format(" Update Net_Privilegios_Grupo {5} " + 
                        " Where IdEstado = '{0}' and IdSucursal = '{1}' and IdGrupo = '{2}' and Arbol = '{3}' and Rama = '{4}' ", 
                        cboEstados.Data, cboSucursales.Data, sMyGrupo, Rama.Arbol, Rama.Rama, sDefault ); 
                }
                catch
                {
                    myNodoBorrar = myNodeSelGrupos;
                    sMyGrupo = myNodeSelGrupos.Tag.ToString();
                    string sRama = "";
                    string []sPartes;

                    if (sMyGrupo.Substring(0, 1) == ">")
                    {
                        bRama = true;
                        sMyGrupo = sMyGrupo.Replace(">", "");
                        sMyGrupo = sMyGrupo.Substring(sMyGrupo.IndexOf('|'));
                        sMyGrupo = sMyGrupo.Replace("||", "|");
                        sMyGrupo = sMyGrupo.Substring(1);
                        sPartes = sMyGrupo.Split('|');

                        sMyGrupo = sPartes[0];
                        sRama = sPartes[1];

                        //sQuery = string.Format("Delete From Net_Privilegios_Grupo Where IdEstado = '{0}' and IdSucursal = '{1}' and IdGrupo = '{2}' and Rama = '{3}' ",
                        //    cboEstados.Data, cboSucursales.Data, sMyGrupo, sRama);
                        
                        sQuery = string.Format("Update Net_Privilegios_Grupo {4} " + 
                            " Where IdEstado = '{0}' and IdSucursal = '{1}' and IdGrupo = '{2}' and Rama = '{3}' ",
                            cboEstados.Data, cboSucursales.Data, sMyGrupo, sRama, sDefault); 
                    }
                    else
                    {
                        sMyGrupo = sMyGrupo.Replace("|", "");

                        //sQuery = string.Format("Delete From Net_Privilegios_Grupo Where IdEstado = '{0}' and IdSucursal = '{1}' and IdGrupo = '{2}' ",
                        //    cboEstados.Data, cboSucursales.Data, sMyGrupo);

                        sQuery = string.Format("Update Net_Privilegios_Grupo {3} " + 
                            " Where IdEstado = '{0}' and IdSucursal = '{1}' and IdGrupo = '{2}' ",
                            cboEstados.Data, cboSucursales.Data, sMyGrupo, sDefault);
                    }
                }

                if (sMyGrupo != "-1")
                {
                    if (guardarDatos.Exec(sQuery)) 
                    {
                        if(bRama)
                        {
                            twGruposPermisos.Nodes.Remove(myNodeSelGrupos);
                        }
                        else
                        {
                            myNodeSelGrupos.Nodes.Clear();
                        }
                    }
                }
            }
            catch { }
        }

        private void actualizarPermisosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CargarArbolNavegacion(); 
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarFarmacias();
        }

        private void cboSucursales_SelectedIndexChanged(object sender, EventArgs e)
        {
            twGruposPermisos.Nodes.Clear(); 
        }

        private void cboArboles_SelectedIndexChanged(object sender, EventArgs e)
        {
            // if (cboArboles.SelectedIndex == 0)
            {
                twNavegador.Nodes.Clear();
                //CargarArbolNavegacion();
            }
        }
    }
}
