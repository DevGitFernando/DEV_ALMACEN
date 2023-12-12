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

using DllFarmaciaSoft;

namespace OficinaCentral.Configuraciones
{
    public partial class FrmClientesClavesSSA : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer, leer2, leer3;

        clsConsultas query;

        TreeNode myNodeSeleccionado; //, myNodoUsuario;
        // DataSet dtsEstados;
        // DataSet dtsFarmacias;
        DataSet dtsDatos;
        FrmCriterioDeBusqueda B;
        clsResize formResize;

        clsNodo_CuadroBasico itemNodo = new clsNodo_CuadroBasico();

        string sIdFarmacia = "", sNombreFarmacia = ""; // , sUsuario = "";
        string sIdCliente = "", sNombreCliente = "";
        string sIdClaveSSA = ""; 

        public FrmClientesClavesSSA()
        {
            InitializeComponent();

            formResize = new clsResize(this); 


            leer = new clsLeer(ref cnn);
            leer2 = new clsLeer(ref cnn);
            leer3 = new clsLeer(ref cnn); 
            query = new clsConsultas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name, false);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.DatosApp, this.Name);

            //General.Pantalla.AjustarTamaño(this, 90, 80);
        }

        private void FrmEstadosClientes_Load(object sender, EventArgs e)
        {
            Fg.IniciaControles(); 

            CargarGruposTerapeuticos();
            CargarClavesSSAClientes();
        }

        private void CargarGruposTerapeuticos()
        {
            cboGrupos.Clear();
            cboGrupos.Add("0", "<< TODOS >>");
            cboGrupos.Add(query.GruposTerapeuticos("CargarGruposTerapeuticos()"), true, "IdGrupoTerapeutico", "Descripcion");
            cboGrupos.SelectedIndex = 0;
        }

        private void CargarClavesSSA_Cliente(TreeNode Nodo, string IdCliente)
        {
            CargarClavesSSA_Cliente(Nodo, IdCliente, ""); 
        }
        
        private void CargarClavesSSA_Cliente(TreeNode Nodo, string IdCliente, string Criterio)
        {
            bool bCargar = false;
            clsNodo_CuadroBasico item = new clsNodo_CuadroBasico();
            clsNodo_CuadroBasico itemClave = new clsNodo_CuadroBasico();

            string sClaveSSA = "";
            string sQuery = string.Format(" Select S.IdCliente, S.IdClaveSSA, S.ClaveSSA, S.DescripcionClave " +
                    " From vw_Claves_Asignadas_A_Clientes S (NoLock) " +
                    " Where S.IdCliente = '{0}' and S.Status = 'A' and S.DescripcionClave like '%{1}%' " +
                    " Order by S.DescripcionClave ", IdCliente, Criterio);
            // order by S.DescripcionSal  

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
                Nodo.Nodes.Clear();

                item = (clsNodo_CuadroBasico)Nodo.Tag;

                leer3.Exec(sQuery);
                while (leer3.Leer())
                {
                    itemClave = new clsNodo_CuadroBasico();
                    itemClave.IdCliente = item.IdCliente;
                    itemClave.IdClaveSSA = leer3.Campo("IdClaveSSA");
                    itemClave.Separador = "||";

                    sClaveSSA = "[" + leer3.Campo("IdClaveSSA") + "]" + "   " + leer3.Campo("ClaveSSA") + " - " + leer3.Campo("DescripcionClave");
                    TreeNode myNodeRama = Nodo.Nodes.Add(sClaveSSA);
                    myNodeRama.ImageIndex = 2;
                    myNodeRama.SelectedImageIndex = 2;
                    myNodeRama.Tag = "||" + leer3.Campo("IdClaveSSA");

                    myNodeRama.Tag = itemClave; 
                }
                Cursor.Current = Cursors.Default;
                Nodo.ExpandAll(); 
            }

        }

        private void CargarClavesSSAClientes()
        {
            //string sSql = string.Format("Select * From CatEstados (noLock) Where Status = 'A' ");

            //if (leer.Exec(sSql))
            //    dtsEstados = leer.DataSetClase;
            //dtsGrupos = query.Grupos(General.EntidadConectada);

            string sClaveSSA = "";
            string sQuery = ""; 

            leer.DataSetClase = query.Clientes("CargarSalesClientes()"); 
            twClientes.Nodes.Clear();
            twClientes.BeginUpdate();

            clsNodo_CuadroBasico itemCliente = new clsNodo_CuadroBasico();
            clsNodo_CuadroBasico itemClave = new clsNodo_CuadroBasico();

            TreeNode myNode = new TreeNode();
            twClientes.Nodes.Clear();

            //if (cboGrupos.SelectedIndex != 0)
            {
                itemCliente.Separador = "-1";
                twClientes.Nodes.Clear();
                myNode = twClientes.Nodes.Add("CLIENTES");
                myNode.Tag = "-1";
                myNode.Tag = itemCliente; 
                myNode.ImageIndex = 0;
                myNode.SelectedImageIndex = 0;
            }

            sQuery = " Select C.IdEstado, C.IdCliente, Ct.Nombre " +
                     " From CFG_EstadosFarmaciasClientes C (NoLock) " +
                     " Inner Join CatClientes Ct (NoLock) On ( C.IdCliente = Ct.IdCliente )  " +
                     " Where C.Status = 'A' and C.IdEstado = '" + cboGrupos.Data + "' and C.IdFarmacia = '" + sIdFarmacia + "'";

            sQuery = string.Format(" Select C.IdCliente, C.IdClaveSSA_Sal, S.ClaveSSA, S.DescripcionSal " +
                    " From CFG_Clientes_Claves C (NoLock) " +
                    " Inner Join vw_ClavesSSA_Sales S (NoLock) On ( C.IdClaveSSA_Sal = S.IdClaveSSA_Sal ) " +
                    " Where " + // "C.IdCliente = '{0}' and " + 
                    " C.Status = 'A' " +
                    " Order by S.DescripcionSal " );
            // order by S.DescripcionSal  

            ////dtsUsuariosGrupo = query.GruposUsuarios(General.EntidadConectada, sIdGrupo);
            //// leer2.Exec(sQuery); 

            while (leer.Leer())
            {
                sIdFarmacia = leer.Campo("IdCliente");
                sNombreFarmacia = "[" + sIdFarmacia + "]" + "   " + leer.Campo("Nombre");

                // foreach (DataRow dt in dtsEstados.Tables[0].Rows)
                {
                    itemCliente = new clsNodo_CuadroBasico();
                    itemCliente.IdCliente = sIdFarmacia;
                    itemCliente.Separador = "|x";

                    TreeNode myNodeGrupo = myNode.Nodes.Add(sNombreFarmacia);
                    myNodeGrupo.ImageIndex = 1;
                    myNodeGrupo.SelectedImageIndex = 1;
                    myNodeGrupo.Tag = "|x" + sIdFarmacia;
                    myNodeGrupo.Tag = itemCliente; 

                    //// leer3.DataRowsClase = leer2.DataTableClase.Select(string.Format("IdCliente = '{0}'", sIdFarmacia)); 
                    // dtsUsuariosGrupo = leer.DataSetClase;
                    while( leer3.Leer() && 1 == 0 )
                    {
                        itemClave = new clsNodo_CuadroBasico();
                        itemClave.IdCliente = sIdFarmacia;
                        itemClave.IdClaveSSA = leer2.Campo("IdClaveSSA_Sal");
                        itemClave.Separador = "||";

                        sClaveSSA = "[" + leer3.Campo("IdClaveSSA_Sal") + "]" + "   " + leer3.Campo("ClaveSSA") + " - " + leer3.Campo("DescripcionSal");
                        TreeNode myNodeRama = myNodeGrupo.Nodes.Add(sClaveSSA);
                        myNodeRama.ImageIndex = 2;
                        myNodeRama.SelectedImageIndex = 2;
                        myNodeRama.Tag = "||" + leer2.Campo("IdClaveSSA_Sal");

                        myNodeRama.Tag = itemClave; 
                    }
                }
            }

            twClientes.EndUpdate();
            myNode.Expand();

            try
            {
                twClientes.Nodes[0].Text = twClientes.Nodes[0].Text;
            }
            catch { }
        }

        private void CargarSalesGrupo()
        {
            CargarSalesGrupo(false); 
        }

        private void CargarSalesGrupo(bool PermitirTodos)
        {
            // string sSql = " Select * From  ";

            if (cboGrupos.SelectedIndex != 0)
            {
                leer.DataSetClase = query.ClavesSSA_SalesGrupoTerapeutico(cboGrupos.Data, txtBuscar.Text.Trim(), "CargarSalesGrupo()");
            }
            else
            {
                if (PermitirTodos)
                {
                    leer.DataSetClase = query.ClavesSSA_SalesGrupoTerapeutico(0, txtBuscar.Text.Trim(), "CargarSalesGrupo()");
                }
                else
                {
                    leer.DataSetClase = query.ClavesSSA_SalesGrupoTerapeutico(cboGrupos.Data, txtBuscar.Text.Trim(), "CargarSalesGrupo()"); 
                }
            }
            
            dtsDatos = leer.DataSetClase;

            //if (leer.Exec(sSql))
            //    dtsDatos = leer.DataSetClase;

            ////dtsDatos = query.Usuarios(General.EntidadConectada);


            object NewColListView = null;
            lwSales.Columns.Clear();
            lwSales.Items.Clear();
            lwSales.View = System.Windows.Forms.View.Details;
            NewColListView = lwSales.Columns.Add("Nombre de Clave", 470);
            lblClave.Text = "";

            if (leer.Leer())
            {
                ListViewItem itmX = null;
                string strValor = "";
                NewColListView = new object();

                leer.RegistroActual = 1;
                while (leer.Leer())
                {
                    // dt["IdClaveSSA_Sal"].ToString() + " - " + 
                    strValor = "[" + leer.Campo("IdClaveSSA_Sal") + "]" + "   " + leer.Campo("ClaveSSA") + " - " + leer.Campo("Descripcion");
                    itmX = lwSales.Items.Add(strValor, 0);
                    itmX.SubItems.Add("" + strValor);
                    itmX.SubItems[0].Tag = leer.Campo("IdClaveSSA_Sal");
                }

                //////foreach (DataRow dt in dtsDatos.Tables[0].Rows)
                //////{
                //////    // dt["IdClaveSSA_Sal"].ToString() + " - " + 
                //////    strValor = dt["ClaveSSA"].ToString() + " - " + dt["Descripcion"].ToString();
                //////    itmX = lwSales.Items.Add(strValor, 0);
                //////    itmX.SubItems.Add("" + strValor);
                //////    itmX.SubItems[0].Tag = dt["IdClaveSSA_Sal"].ToString();
                //////}
            }
        }

        #region Asignar Clientes 
        private void ActualizaMenu(string Tag)
        {
            mnEstados.Items[eliminarToolStripMenuItem.Name].Enabled = false;
            mnEstados.Items[eliminarClientesDelEstadoToolStripMenuItem.Name].Enabled = false;
            mnEstados.Items[actualizarToolStripMenuItem.Name].Enabled = true;

            if (Tag == "|x")
            {
                mnEstados.Items[eliminarClientesDelEstadoToolStripMenuItem.Name].Enabled = true;
                mnEstados.Items[eliminarToolStripMenuItem.Name].Enabled = false;
            }

            if (Tag == "||")
            {
                mnEstados.Items[eliminarClientesDelEstadoToolStripMenuItem.Name].Enabled = false;
                mnEstados.Items[eliminarToolStripMenuItem.Name].Enabled = true;
            }
        }

        private void twEstados_AfterSelect(object sender, TreeViewEventArgs e)
        {
            myNodeSeleccionado = e.Node;

            if (twClientes.Nodes.Count > 0)
            {
                itemNodo = (clsNodo_CuadroBasico)myNodeSeleccionado.Tag;

                ActualizaMenu(itemNodo.Separador);

                if (!itemNodo.Separador.Contains("|"))
                {
                    sIdFarmacia = myNodeSeleccionado.Tag.ToString();
                } 
                else
                {
                    sIdFarmacia = myNodeSeleccionado.Parent.Tag.ToString();
                    sIdCliente = myNodeSeleccionado.Tag.ToString().Replace("|", "");
                    sIdClaveSSA = myNodeSeleccionado.Tag.ToString().Replace("|", "");
                    sIdCliente = sIdCliente.ToUpper().Replace("X", "");

                    sIdCliente = itemNodo.IdCliente;
                    sIdClaveSSA = itemNodo.IdClaveSSA;


                    //CargarClavesSSA_Cliente(myNodeSeleccionado, sIdCliente); 
                }
            }
        }

        private bool EsPadreValido(string Valor)
        {
            bool bRegresa = true;

            if (Valor == "-1" &&  // Valor.Substring(0, 2) != "|x" && 
                Valor.Substring(0, 2) == "00" && Valor.Substring(0, 2) == "|0" && Valor.Substring(0, 2) == "||")
                bRegresa = false;

            //if (myNodeSeleccionado.Tag.ToString() == "-1")
            //    bRegresa = false;

            return bRegresa;
        }

        private void twEstados_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode NewNode; 
            Control myCtrl = (Control)sender;
            Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
            string sSql = ""; 
            //clsNodo_CuadroBasico itemNodo = new clsNodo_CuadroBasico(); 


            NewNode = twClientes.GetNodeAt(pt);
            //sIdFarmacia = NewNode.Tag.ToString().Substring(2, 4);

            itemNodo = (clsNodo_CuadroBasico)NewNode.Tag; 
            //itemNodo.Separador = "||";
            //itemNodo.IdCliente = sIdFarmacia;
            //itemNodo.IdClaveSSA = sIdClaveSSA;
            itemNodo.Status = "A";
            itemNodo.IdClaveSSA = sIdClaveSSA;


            sSql = string.Format("Exec spp_Mtto_CFG_Clientes_Claves @IdCliente = '{0}', @IdClaveSSA_Sal = '{1}', @Status = '{2}' ",
                itemNodo.IdCliente, itemNodo.IdClaveSSA, itemNodo.Status);

            //if (NewNode.Parent.Tag.ToString() == "-1" || NewNode.Tag.ToString().Substring(0,2) == "|x")
            if (itemNodo.Separador  == "-1" || itemNodo.Separador == "|x")
            {
                //if (EsPadreValido(sIdFarmacia))
                {
                    // sIdCliente + " - " + sNombreCliente 
                    if (!ExisteOpcion(NewNode, sNombreCliente))
                    {
                        //CargarGrupos();
                        if (leer.Exec(sSql))
                        {
                            // sIdCliente + " - " +  
                            TreeNode myNodeRama = NewNode.Nodes.Add(sNombreCliente);
                            myNodeRama.ImageIndex = 2;
                            myNodeRama.SelectedImageIndex = 2;
                            myNodeRama.Tag = "||" + sIdClaveSSA;
                            myNodeRama.Tag = itemNodo;

                            // AsignarIcono(myNodeRama, RamaSeleccionada.TipoRama);
                        }
                        NewNode.Expand();
                    }
                }
            }
        }

        private void twEstados_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void twEstados_ItemDrag(object sender, ItemDragEventArgs e)
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

        //private void lwClientes_ItemDrag(object sender, ItemDragEventArgs e)
        //{
        //    sIdCliente = lwSales.FocusedItem.SubItems[0].Tag.ToString();
        //    sNombreCliente = lwSales.FocusedItem.SubItems[0].Text.Trim();
        //    sIdClaveSSA = lwSales.FocusedItem.SubItems[0].Tag.ToString(); 
        //    DoDragDrop(e.Item, DragDropEffects.Move);
        //}
        #endregion Asignar Clientes

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sQuery = "";

            try
            {            
                if (itemNodo.Separador != "-1")
                {
                    if (itemNodo.Separador == "||")
                    {
                        sQuery = string.Format("Exec spp_Mtto_CFG_Clientes_Claves @IdCliente = '{0}', @IdClaveSSA_Sal = '{1}', @Status = '{2}' ", 
                            itemNodo.IdCliente, itemNodo.IdClaveSSA, "C");
                    }

                    if(leer.Exec(sQuery))
                    {
                        twClientes.Nodes.Remove(myNodeSeleccionado);
                    }
                }
            }
            catch { }
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CargarClavesSSAClientes();
        }

        private void eliminarClientesDelEstadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sQuery = "";
            clsNodo_CuadroBasico nodo = new clsNodo_CuadroBasico(); 

            try
            {
                if (itemNodo.Separador == "|x")
                {
                    foreach (TreeNode tNodo in myNodeSeleccionado.Nodes)
                    {
                        nodo = (clsNodo_CuadroBasico)tNodo.Tag;
                        sQuery = string.Format("Exec spp_Mtto_CFG_Clientes_Claves @IdCliente = '{0}', @IdClaveSSA_Sal = '{1}', @Status = '{2}' ",
                            nodo.IdCliente, nodo.IdClaveSSA, "C");

                        leer.Exec(sQuery); 
                        //if (leer.Exec(sQuery))
                        //    twClientes.Nodes.Remove(tNodo);
                    }

                    // Actualizar la lista 
                    CargarClavesSSAClientes(); 
                }
            }
            catch { }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            CargarSalesGrupo();
        }

        private void cboEstados_SelectedValueChanged(object sender, EventArgs e)
        {
            //if (cboEstados.SelectedIndex != 0)
            {
                //CargarSalesClientes();
                // CargarSalesGrupo();
            }
        }

        private void lwSales_ItemDrag(object sender, ItemDragEventArgs e)
        {
            sIdCliente = lwSales.FocusedItem.SubItems[0].Tag.ToString();
            sNombreCliente = lwSales.FocusedItem.SubItems[0].Text.Trim();
            sIdClaveSSA = lwSales.FocusedItem.SubItems[0].Tag.ToString(); 
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void lwSales_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            lblClave.Text = lwSales.FocusedItem.SubItems[0].Text.Trim();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            CargarSalesGrupo(true); 
        }

        private void txtBuscar_VisibleChanged(object sender, EventArgs e)
        {
            CargarSalesGrupo(); 
        }

        private void cargarClavesAsignadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CargarClavesSSA_Cliente(myNodeSeleccionado, sIdCliente); 
        }

        private void buscarClavesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            B = new FrmCriterioDeBusqueda();
            string sCriterio = B.MostarCriterio();
            CargarClavesSSA_Cliente(myNodeSeleccionado, sIdCliente, sCriterio); 
        }
    }
}
