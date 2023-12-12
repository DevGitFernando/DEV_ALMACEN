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
using SC_SolutionsSystem.Usuarios_y_Permisos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Usuarios_y_Permisos;

namespace DllCompras.PerfilesPersonal
{
    public partial class FrmCOM_ProductosParaCompra : FrmBaseExt 
    {
        clsConsultas query;
        DataSet dtsDatos = new DataSet();
        DataSet dtsGrupos = new DataSet(), dtsUsuariosGrupo = new DataSet();
        DataSet dtsClientes = new DataSet();
        DataSet dtsSubClientes = new DataSet();

        TreeNode myNodeSeleccionado; //, myNodoUsuario;
        //TreeNode myNodeProductos;
        FrmCriterioDeBusqueda B;

        int iIndexNodo = 0;
        string sClaveSSA_Seleccionada = "";
        string sIdProducto = "";
        string sCodigoEAN = "";
        string sNombreProducto = "";
        string sClave_Asignar = "";

        string sPermisoPerfiles = "MODIFICAR_PERFILES";
        bool bPermisoPerfiles = false;

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas Consultas;
        clsListView lst; 

        public FrmCOM_ProductosParaCompra()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, General.DatosApp, this.Name, false);
            Error = new clsGrabarError(General.DatosConexion, General.DatosApp, this.Name);
            lst = new clsListView(lstProductos); 
        }

        #region Form 
        private void FrmCOM_ProductosParaCompra_Load(object sender, EventArgs e)
        {
            lst.LimpiarItems(); 
            CargarEstados(); 
        }
        #endregion Form        

        #region Cargar informacion 
        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            twClaves.Nodes.Clear();
            lst.LimpiarItems();

            TreeNode myNode = new TreeNode();
            twClaves.Nodes.Clear();
            myNode = twClaves.Nodes.Add("CLAVES LICITADAS");
            myNode.Tag = "-1";
            myNode.ImageIndex = 0;
            myNode.SelectedImageIndex = 0; 
        }

        private void CargarEstados()
        {
            string sSql = " Select Distinct IdEstado, Estado From vw_Farmacias_Urls (NoLock) Order by IdEstado ";
            cboEstados.Clear();
            cboEstados.Add();

            ////if (!DtGeneral.EsAdministrador)
            ////{
            ////    sSql = string.Format(" Select Distinct IdEstado, Estado From vw_Farmacias_Urls (NoLock) " +
            ////        " Where IdEstado = '{0}' Order by IdEstado ", DtGeneral.EstadoConectado);
            ////}

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarEstados()");
                General.msjError("Ocurrió un error al Cargar la Lista de Estados.");
            }
            else
            {
                if (leer.Leer())
                {
                    cboEstados.Add(leer.DataSetClase, true, "IdEstado", "Estado");
                }
            }

            cboEstados.SelectedIndex = 0; 
        }

        private void CargarClavesSSA_Estado(TreeNode Nodo)
        {
            CargarClavesSSA_Estado(Nodo, "");
        }

        private void CargarClavesSSA_Estado(TreeNode Nodo, string Criterio)
        {
            bool bCargar = false;
            string sClaveSSA = "";
            string sQuery = string.Format(" Select S.ClaveSSA, S.DescripcionClave " +
                    " From vw_Claves_Precios_Asignados S (NoLock) " +
                    " Where S.IdEstado = '{0}' and S.Status = 'A' and S.DescripcionClave like '%{1}%' " +
                    " Group by S.ClaveSSA, S.DescripcionClave " + 
                    " Order by S.DescripcionClave " + 
                    " ", 
                    cboEstados.Data, Criterio); 

            ////dtsUsuariosGrupo = query.GruposUsuarios(General.EntidadConectada, sIdGrupo); 
            if (Nodo.Nodes.Count == 0)
            {
                bCargar = true;
            }

            if (Criterio != "")
            {
                bCargar = true;
            }

            if (bCargar)
            {
                Cursor.Current = Cursors.WaitCursor; 

                // Evitar duplicidad de datos
                Nodo.Nodes.Clear();

                leer.Exec(sQuery);
                while (leer.Leer())
                {
                    sClaveSSA = leer.Campo("ClaveSSA") + " - " + leer.Campo("DescripcionClave");
                    TreeNode myNodeRama = Nodo.Nodes.Add(sClaveSSA);
                    myNodeRama.ImageIndex = 2;
                    myNodeRama.SelectedImageIndex = 2;
                    myNodeRama.Tag = "|x" + leer.Campo("ClaveSSA");
                }
                Cursor.Current = Cursors.Default;
                Nodo.ExpandAll();
            }

        }

        private void CargarProductos_ClavesSSA(TreeNode Nodo)
        {
            bool bCargar = false;
            string sClaveSSA = "";
            
            sClaveSSA = Nodo.Tag.ToString().Replace("|x", "");            

            string sQuery = string.Format(
                " Select C.IdProducto, C.CodigoEAN, P.Descripcion " +
                " From COM_CFG_Productos_Compras C (NoLock) " +
                " Inner Join vw_Productos_CodigoEAN P (NoLock) On ( C.IdProducto = P.IdProducto and C.CodigoEAN = P.CodigoEAN ) " +
                " Where C.IdEstado = '{0}' and P.ClaveSSA = '{1}' and C.Status = 'A' ", cboEstados.Data, sClaveSSA);

            if (Nodo.Nodes.Count == 0)
                bCargar = true;       

            if (bCargar)
            {
                Cursor.Current = Cursors.WaitCursor;
                Nodo.Nodes.Clear();

                leer.Exec(sQuery);
                while (leer.Leer())
                {
                    sClaveSSA = leer.Campo("CodigoEAN") + " - " + leer.Campo("Descripcion");
                    TreeNode myNodeRama = Nodo.Nodes.Add(sClaveSSA);
                    myNodeRama.ImageIndex = 2;
                    myNodeRama.SelectedImageIndex = 2;
                    myNodeRama.Tag = "||" + leer.Campo("CodigoEAN");
                }
                Cursor.Current = Cursors.Default;
                Nodo.ExpandAll();
            }
        }

        private void CargarProductos_ClavesSSA_Relacionados(TreeNode Nodo)
        {
            string sClaveSSA = "";

            sClaveSSA = Nodo.Tag.ToString().Replace("|x", "");
            //sClaveSSA = Nodo.Tag.ToString().Replace("x", "");

            string sQuery = string.Format(
                " Select ( P.CodigoEAN + ' - ' + P.Descripcion ) as NombreProducto, P.IdProducto, P.CodigoEAN, P.ClaveSSA " +
                " From vw_Productos_CodigoEAN P (NoLock)  " +
                " Where P.ClaveSSA = '{0}' ", sClaveSSA);

            if (leer.Exec(sQuery))
            {
                dtsDatos = leer.DataSetClase;
            }

            //object NewColListView = null;
            //lstProductos.Columns.Clear();
            //lstProductos.Items.Clear();
            //lstProductos.View = System.Windows.Forms.View.Details;
            //NewColListView = lstProductos.Columns.Add("Nombre de Producto", 470);

            lst.LimpiarItems();

            lst.CargarDatos(leer.DataSetClase, true, false);
            ////if (leer.Leer())
            ////{
            ////    ListViewItem itmX = null;
            ////    string strValor = "";
            ////    NewColListView = new object();

            ////    foreach (DataRow dt in dtsDatos.Tables[0].Rows)
            ////    {

            ////        strValor = dt["CodigoEAN"].ToString() + " - " + dt["Descripcion"].ToString();
            ////        itmX = lstProductos.Items.Add(strValor, 0);
            ////        itmX.SubItems.Add("" + strValor);
            ////        itmX.SubItems[0].Tag = dt["IdProducto"].ToString();
            ////    }


            ////}

        }
        #endregion Cargar informacion

        #region Menus 
        private void btnClaves_CargarLista_Click(object sender, EventArgs e)
        {
            CargarClavesSSA_Estado(myNodeSeleccionado); 
        }

        private void btnClaves_BuscarClaves_Click(object sender, EventArgs e)
        {
            B = new FrmCriterioDeBusqueda();
            string sCriterio = B.MostarCriterio();

            CargarClavesSSA_Estado(myNodeSeleccionado, sCriterio); 
        }

        private void btnCargarProductosAsigandos_Click(object sender, EventArgs e)
        {
            CargarProductos_ClavesSSA(myNodeSeleccionado); 
        }

        private void btnCargarProductosRelacionados_Click(object sender, EventArgs e)
        {
            CargarProductos_ClavesSSA_Relacionados(myNodeSeleccionado);
        }

        private void eliminarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sQuery = "";
            string sCodigoEAN = "";
            
            sCodigoEAN = myNodeSeleccionado.Tag.ToString().Replace("||", "");

            try
            {
                if (myNodeSeleccionado.Tag.ToString().Substring(0, 2) == "||")
                {                    
                    sQuery = string.Format("Exec spp_Mtto_COM_CFG_Productos_Compras '{0}', '{1}', '{2}', 'C'  ",
                        cboEstados.Data, sCodigoEAN, sCodigoEAN);

                    if (leer.Exec(sQuery))
                    {
                        twClaves.Nodes.Remove(myNodeSeleccionado);
                    }                                        
                }
            }
            catch { }
        }
        #endregion Menus

        #region Treeview y Listview 
        private void twClaves_AfterSelect(object sender, TreeViewEventArgs e)
        {
            myNodeSeleccionado = e.Node;
            sClaveSSA_Seleccionada = ""; 

            if (twClaves.Nodes.Count > 0)
            {
                ActualizaMenuClaves(e.Node.Tag.ToString());

                if (myNodeSeleccionado.Tag.ToString().Substring(0, 2) == "|x")
                {
                    lblClave.Text = myNodeSeleccionado.Text;
                }
                else if (myNodeSeleccionado.Tag.ToString().Substring(0, 2) == "-1")
                {
                    lblClave.Text = "";
                }
            }
        }

        private void ActualizaMenuClaves(string Tag)
        {
            mnClaves.Items[btnClaves_CargarLista.Name].Enabled = false;
            mnClaves.Items[btnClaves_BuscarClaves.Name].Enabled = false;
            mnClaves.Items[btnCargarProductosAsigandos.Name].Enabled = false;
            mnClaves.Items[btnCargarProductosRelacionados.Name].Enabled = false;
            mnClaves.Items[btnEliminarProducto.Name].Enabled = false;

            if (Tag.Substring(0, 2) == "-1")
            {
                mnClaves.Items[btnClaves_CargarLista.Name].Enabled = true;
                mnClaves.Items[btnClaves_BuscarClaves.Name].Enabled = true;
            }

            if (Tag.Substring(0, 2) == "|x")
            {
                sClaveSSA_Seleccionada = Fg.Mid(Tag.ToString(), 4) ; 
                mnClaves.Items[btnCargarProductosAsigandos.Name].Enabled = true;
                mnClaves.Items[btnCargarProductosRelacionados.Name].Enabled = true;
            }

            if (Tag.Substring(0, 2) == "||")
            {
                sClaveSSA_Seleccionada = Fg.Mid(Tag.ToString(), 4);
                mnClaves.Items[btnEliminarProducto.Name].Enabled = true;
            }
        }

        private void twClaves_DragDrop(object sender, DragEventArgs e)
        {
            string sClaveEAN = "", sMensaje = "";

            TreeNode NewNode;
            Control myCtrl = (Control)sender;
            Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
            NewNode = twClaves.GetNodeAt(pt);
            sIdProducto = lst.LeerItem().Campo("IdProducto");
            sNombreProducto = lst.LeerItem().Campo("NombreProducto");
            sCodigoEAN = lst.LeerItem().Campo("CodigoEAN");
            sClave_Asignar = lst.LeerItem().Campo("ClaveSSA");

            sClaveEAN = NewNode.Tag.ToString().Replace("|x", "");

            string sSql = string.Format("Exec spp_Mtto_COM_CFG_Productos_Compras '{0}', '{1}', '{2}', 'A' ",
                cboEstados.Data, sIdProducto, sCodigoEAN );

            if (NewNode.Tag.ToString().Substring(0, 2) == "|x")
            {
                if (sClaveEAN == sClave_Asignar)
                {
                    if (!ExisteOpcion(NewNode, sCodigoEAN))
                    {
                        if (leer.Exec(sSql))
                        {
                            TreeNode myNodeRama = NewNode.Nodes.Add(sNombreProducto);
                            myNodeRama.ImageIndex = 2;
                            myNodeRama.SelectedImageIndex = 2;
                            myNodeRama.Tag = "||" + sCodigoEAN;
                        }
                        NewNode.Expand();
                    }
                }
                else
                {
                    sMensaje = "El Producto no pertenece a la Clave : " + sClaveEAN + " .. Verifique!!";
                    General.msjAviso(sMensaje);
                }
            }
        }

        private void twClaves_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void twClaves_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void lstProductos_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
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
        #endregion Treeview y Listview 
               
        
    }
}
