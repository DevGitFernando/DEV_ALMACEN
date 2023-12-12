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

namespace DllCompras.PerfilesPersonal
{
    public partial class FrmPerfilesPersonalClavesSSA : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer, leer2, leer3;

        clsConsultas query;

        TreeNode myNodeSeleccionado; //, myNodoUsuario;
        // DataSet dtsEstados;
        // DataSet dtsFarmacias;
        DataSet dtsDatos;
        FrmCriterioDeBusqueda B;
        FrmPersonalCompras Personal;

        string sIdPersonal = "", sNombreFarmacia = ""; // , sUsuario = "";
        string sIdCliente = "", sNombreCliente = "";
        string sIdClaveSSA = "";

        public FrmPerfilesPersonalClavesSSA()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            leer2 = new clsLeer(ref cnn);
            leer3 = new clsLeer(ref cnn); 
            query = new clsConsultas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name, false);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.DatosApp, this.Name);
        }

        private void FrmEstadosClientes_Load(object sender, EventArgs e)
        {
            Fg.IniciaControles();

            Cargar_Estados();
            CargarGruposTerapeuticos();
            //CargarPersonalCompras();
            cboFarmacia.Clear();
            cboFarmacia.Add();
            cboFarmacia.SelectedIndex = 0; 
        }

        #region Funciones
        private void Cargar_Estados()
        {
            string sSql = "";

            cboEdo.Clear();
            cboEdo.Add();

            sSql = string.Format("Select Distinct IdEstado, (IdEstado + ' - ' + NombreEstado) as NombreEstado, ClaveRenapo " +
                " From vw_EmpresasEstados (NoLock) " +
                " Where StatusEdo = 'A' Order by IdEstado ");

            if (!leer.Exec(sSql))
            {
                Error.LogError(leer.MensajeError);
                General.msjError("Ocurrió un error al obtener la lista de Estados.");
                
            }
            else
            {
                if (leer.Leer())
                {
                    cboEdo.Add(leer.DataSetClase, true, "IdEstado", "NombreEstado");
                }
            }
            cboEdo.SelectedIndex = 0;
        }

        private void CargarFarmacias()
        {
            string sSql = "";

            cboFarmacia.Clear();
            cboFarmacia.Add();

            sSql = string.Format(" Select IdFarmacia, (IdFarmacia + ' -- ' + Farmacia) as Farmacia From vw_Farmacias (Nolock) " +
	                            " Where IdEstado = '{0}' and IdTipoUnidad = '005' ", cboEdo.Data);

            if (!leer.Exec(sSql))
            {
                Error.LogError(leer.MensajeError);
                General.msjError("Ocurrió un error al obtener la lista de Farmacias.");

            }
            else
            {
                if (leer.Leer())
                {
                    cboFarmacia.Add(leer.DataSetClase, true, "IdFarmacia", "Farmacia");
                }
            }

            cboFarmacia.SelectedIndex = 0;            
        }

        private void CargarGruposTerapeuticos()
        {
            cboGrupos.Clear();
            cboGrupos.Add("0", "<< Seleccione >>");
            cboGrupos.Add(query.GruposTerapeuticos("CargarGruposTerapeuticos()"), true, "IdGrupoTerapeutico", "Descripcion");
            cboGrupos.SelectedIndex = 0;
        }

        private void CargarClavesSSA_Personal(TreeNode Nodo, string IdPersonal)
        {
            CargarClavesSSA_Personal(Nodo, IdPersonal, ""); 
        }
        
        private void CargarClavesSSA_Personal(TreeNode Nodo, string IdPersonal, string Criterio)
        {
            bool bCargar = false; 
            string sClaveSSA = "";
            string sQuery = string.Format(" Select S.IdPersonal, S.IdClaveSSA, S.ClaveSSA, S.DescripcionClave " +
                    " From vw_CFG_COM_Perfiles_Personal_ClavesSSA S (NoLock) " +
                    " Where S.IdEstado = '{0}' and S.IdFarmacia = '{1}' and S.IdPersonal = '{2}' and S.Status = 'A' and S.DescripcionClave like '%{3}%' " +
                    " Order by S.DescripcionClave ", cboEdo.Data, cboFarmacia.Data, IdPersonal, Criterio);
            // order by S.DescripcionSal  

            ////dtsUsuariosGrupo = query.GruposUsuarios(General.EntidadConectada, sIdGrupo); 
            if (Nodo.Nodes.Count == 0)
                bCargar = true;

            if (Criterio != "")
                bCargar = true;


            if (bCargar)
            {
                Cursor.Current = Cursors.WaitCursor;
                Nodo.Nodes.Clear(); 

                leer3.Exec(sQuery);
                while (leer3.Leer())
                {
                    sClaveSSA = leer3.Campo("ClaveSSA") + " - " + leer3.Campo("DescripcionClave");
                    TreeNode myNodeRama = Nodo.Nodes.Add(sClaveSSA);
                    myNodeRama.ImageIndex = 2;
                    myNodeRama.SelectedImageIndex = 2;
                    myNodeRama.Tag = "||" + leer3.Campo("IdClaveSSA");
                }
                Cursor.Current = Cursors.Default;
                Nodo.ExpandAll(); 
            }

        }

        private void CargarPersonalCompras()
        {
            string sSql = string.Format("Select C.IdPersonal, P.NombreCompleto as Nombre From CFG_COM_Perfiles_Personal C (Nolock) " +
	                                " Inner Join vw_Personal P (Nolock) On ( C.IdEstado = P.IdEstado and C.IdFarmacia = P.IdFarmacia and C.IdPersonal = P.IdPersonal ) " +
                                    " Where C.IdEstado = '{0}' and C.IdFarmacia = '{1}' and C.Status = 'A' ", cboEdo.Data, cboFarmacia.Data);

            if (!leer.Exec(sSql))
            {
                Error.LogError(leer.MensajeError);
                General.msjError("Ocurrió un error al obtener la lista de Personal.");
            }
            

            //leer.DataSetClase = query.Clientes("CargarClavesSSAPersonalCompras()"); 
            twClientes.Nodes.Clear();
            twClientes.BeginUpdate();

            TreeNode myNode = new TreeNode();
            twClientes.Nodes.Clear();            
            
            myNode = twClientes.Nodes.Add("PERSONAL_COMPRAS");
            myNode.Tag = "-1";
            myNode.ImageIndex = 0;
            myNode.SelectedImageIndex = 0;            

            
            string sQuery = " Select C.IdEstado, C.IdCliente, Ct.Nombre " +
                     " From CFG_EstadosFarmaciasClientes C (NoLock) " +
                     " Inner Join CatClientes Ct (NoLock) On ( C.IdCliente = Ct.IdCliente )  " +
                     " Where C.Status = 'A' and C.IdEstado = '" + cboGrupos.Data + "' and C.IdFarmacia = '" + sIdPersonal + "'";

            sQuery = string.Format(" Select C.IdCliente, C.IdClaveSSA_Sal, S.ClaveSSA, S.DescripcionSal " +
                    " From CFG_Clientes_Claves C (NoLock) " +
                    " Inner Join vw_ClavesSSA_Sales S (NoLock) On ( C.IdClaveSSA_Sal = S.IdClaveSSA_Sal ) " +
                    " Where " + // "C.IdCliente = '{0}' and " + 
                    " C.Status = 'A' " +
                    " Order by S.DescripcionSal " );            

            while (leer.Leer())
            {
                sIdPersonal = leer.Campo("IdPersonal");
                sNombreFarmacia = leer.Campo("Nombre");
                
                TreeNode myNodeGrupo = myNode.Nodes.Add(sNombreFarmacia);
                myNodeGrupo.ImageIndex = 1;
                myNodeGrupo.SelectedImageIndex = 1;
                myNodeGrupo.Tag = "|x" + sIdPersonal;                   
                
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

                foreach (DataRow dt in dtsDatos.Tables[0].Rows)
                {
                    // dt["IdClaveSSA_Sal"].ToString() + " - " + 
                    strValor = dt["ClaveSSA"].ToString() + " - " + dt["Descripcion"].ToString();
                    itmX = lwSales.Items.Add(strValor, 0);
                    itmX.SubItems.Add("" + strValor);
                    itmX.SubItems[0].Tag = dt["IdClaveSSA_Sal"].ToString();
                }

                //lwSales.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                //lwSales.Columns[0].Width = 420;
            }
        }
        #endregion Funciones

        #region Asignar Clientes
        private void ActualizaMenu(string Tag)
        {
            mnEstados.Items[eliminarToolStripMenuItem.Name].Enabled = false;
            mnEstados.Items[eliminarClientesDelEstadoToolStripMenuItem.Name].Enabled = false;
            mnEstados.Items[actualizarToolStripMenuItem.Name].Enabled = true;

            if (Tag.Substring(0, 2) == "|x")
            {
                mnEstados.Items[eliminarClientesDelEstadoToolStripMenuItem.Name].Enabled = true;
                mnEstados.Items[eliminarToolStripMenuItem.Name].Enabled = false;
            }

            if (Tag.Substring(0, 2) == "||")
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
                ActualizaMenu(e.Node.Tag.ToString());

                if (!myNodeSeleccionado.Tag.ToString().Contains("|"))
                {
                    sIdPersonal = myNodeSeleccionado.Tag.ToString();
                } 
                else
                {
                    sIdPersonal = myNodeSeleccionado.Parent.Tag.ToString();
                    sIdCliente = myNodeSeleccionado.Tag.ToString().Replace("|", "");
                    sIdClaveSSA = myNodeSeleccionado.Tag.ToString().Replace("|", "");
                    sIdCliente = sIdCliente.ToUpper().Replace("X", "");

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
            NewNode = twClientes.GetNodeAt(pt);
            sIdPersonal = NewNode.Tag.ToString().Substring(2, 4);

            string sSql = string.Format("Exec spp_Mtto_CFG_COM_Perfiles_Personal_ClavesSSA '{0}', '{1}', '{2}', '{3}', 'A' ",
                cboEdo.Data, cboFarmacia.Data, sIdPersonal, sIdClaveSSA);

            if (NewNode.Parent.Tag.ToString() == "-1" || NewNode.Tag.ToString().Substring(0,2) == "|x")
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
                if (myNodeSeleccionado.Tag.ToString() != "-1")
                {
                    if (myNodeSeleccionado.Tag.ToString().Substring(0,2) == "||")
                    {
                        sQuery = string.Format("Exec spp_Mtto_CFG_COM_Perfiles_Personal_ClavesSSA '{0}', '{1}', '{2}', '{3}', 'C' ",
                            cboEdo.Data, cboFarmacia.Data, sIdPersonal.Substring(2, 4), sIdCliente);
                    }

                    if (leer.Exec(sQuery))
                        twClientes.Nodes.Remove(myNodeSeleccionado);
                }
            }
            catch { }
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CargarPersonalCompras();
        }

        private void eliminarClientesDelEstadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sQuery = "";

            try
            {
                if (myNodeSeleccionado.Tag.ToString().Substring(0, 2) == "|x")
                {
                    foreach (TreeNode tNodo in myNodeSeleccionado.Nodes)
                    {
                        sQuery = string.Format("Exec spp_Mtto_CFG_COM_Perfiles_Personal_ClavesSSA '{0}', '{1}', '{2}', '{3}', 'C' ", 
                            cboEdo.Data, cboFarmacia.Data, myNodeSeleccionado.Tag.ToString().Substring(2, 4), tNodo.Tag.ToString().Substring(2, 4));

                        leer.Exec(sQuery); 
                        //if (leer.Exec(sQuery))
                        //    twClientes.Nodes.Remove(tNodo);
                    }

                    // Actualizar la lista 
                    CargarPersonalCompras(); 
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
            CargarClavesSSA_Personal(myNodeSeleccionado, sIdCliente); 
        }

        private void buscarClavesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            B = new FrmCriterioDeBusqueda();
            string sCriterio = B.MostarCriterio();
            CargarClavesSSA_Personal(myNodeSeleccionado, sIdCliente, sCriterio); 
        }

        private void agregarPersonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sIdPersonal = "";
            Personal = new FrmPersonalCompras();

            Personal.IdEstado = cboEdo.Data;
            Personal.IdFarmacia = cboFarmacia.Data;
            Personal.Estado = cboEdo.Text;
            Personal.Farmacia = cboFarmacia.Text;

            if (sIdPersonal != "")
                Personal.IdPersonal = sIdPersonal.Substring(0, 4);

            Fg.CentrarForma(Personal);
            Personal.ShowDialog();

            CargarPersonalCompras();
        }

        #region Eventos_Combos
        private void cboEdo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEdo.SelectedIndex != 0)
            {
                cboEdo.Enabled = false;
                CargarFarmacias();
            }
        }

        private void cboFarmacia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFarmacia.SelectedIndex != 0)
            {
                cboFarmacia.Enabled = false;
                CargarPersonalCompras();
            }
        }
        #endregion Eventos_Combos

        
    }
}
