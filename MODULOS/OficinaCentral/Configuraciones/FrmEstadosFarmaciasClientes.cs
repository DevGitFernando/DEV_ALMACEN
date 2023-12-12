using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;

using DllFarmaciaSoft;

namespace OficinaCentral.Configuraciones
{
    public partial class FrmEstadosFarmaciasClientes : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer, leer2;
        clsGrid grid;

        clsConsultas query;

        TreeNode myNodeSeleccionado; //, myNodoUsuario;
        // DataSet dtsEstados;
        // DataSet dtsFarmacias;
        DataSet dtsDatos;
        string sIdFarmacia = "", sNombreFarmacia = "", sUsuario = "";
        string sIdCliente = "", sNombreCliente = "";
        clsResize formResize; 

        public FrmEstadosFarmaciasClientes()
        {
            InitializeComponent();

            //formResize = new clsResize(this); 

            leer = new clsLeer(ref cnn);
            leer2 = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name, false);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.DatosApp, this.Name);

            grid = new clsGrid(ref grdSubClientes, this);
            grid.EstiloGrid(eModoGrid.ModoRow);
            grid.AjustarAnchoColumnasAutomatico = true; 

            //General.Pantalla.AjustarTamaño(this, 90, 80);
        }

        private void FrmEstadosClientes_Load(object sender, EventArgs e)
        {
            CargarEstados();
        }

        private void CargarEstados()
        {
            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");
            cboEstados.Add(query.EstadosConFarmacias("CargarEstados()"), true, "IdEstado", "NombreEstado");
            cboEstados.SelectedIndex = 0;
        }

        private void CargarFarmaciasEstado()
        {
            //string sSql = string.Format("Select * From CatEstados (noLock) Where Status = 'A' ");

            //if (leer.Exec(sSql))
            //    dtsEstados = leer.DataSetClase;
            //dtsGrupos = query.Grupos(General.EntidadConectada);

            leer.DataSetClase = query.Farmacias(cboEstados.Data, "CargarFarmaciasEstado()"); 
            twEstados.Nodes.Clear();
            twEstados.BeginUpdate();

            twFarmaciasClientes.Nodes.Clear();
            twFarmaciasClientes.BeginUpdate();

            TreeNode myNode = new TreeNode();
            TreeNode myNodeAux = new TreeNode();
            twEstados.Nodes.Clear();

            if (cboEstados.SelectedIndex != 0)
            {
                twEstados.Nodes.Clear();
                myNode = twEstados.Nodes.Add("FARMACIAS");
                myNode.Tag = "-1";
                myNode.ImageIndex = 0;
                myNode.SelectedImageIndex = 0;

                twFarmaciasClientes.Nodes.Clear();
                myNodeAux = twFarmaciasClientes.Nodes.Add("FARMACIAS");
                myNodeAux.Tag = "-1";
                myNodeAux.ImageIndex = 0;
                myNodeAux.SelectedImageIndex = 0;
            }

            while (leer.Leer())
            {
                sIdFarmacia = leer.Campo("IdFarmacia");
                sNombreFarmacia = sIdFarmacia + " -- " + leer.Campo("Farmacia");

                // foreach (DataRow dt in dtsEstados.Tables[0].Rows)
                {
                    TreeNode myNodeGrupo = myNode.Nodes.Add(sNombreFarmacia);
                    myNodeGrupo.ImageIndex = 1;
                    myNodeGrupo.SelectedImageIndex = 1;
                    myNodeGrupo.Tag = "|x" + sIdFarmacia;

                    TreeNode myNodeGrupoAux = myNodeAux.Nodes.Add(sNombreFarmacia);
                    myNodeGrupoAux.ImageIndex = 1;
                    myNodeGrupoAux.SelectedImageIndex = 1;
                    myNodeGrupoAux.Tag = "|x" + sIdFarmacia;

                    string sQuery = " Select C.IdEstado, C.IdCliente, Ct.Nombre " +
                             " From CFG_EstadosFarmaciasClientes C (NoLock) " +
                             " Inner Join CatClientes Ct (NoLock) On ( C.IdCliente = Ct.IdCliente )  " +
                             " Where C.Status = 'A' and C.IdEstado = '" + cboEstados.Data + "' and C.IdFarmacia = '" + sIdFarmacia + "'";

                    //dtsUsuariosGrupo = query.GruposUsuarios(General.EntidadConectada, sIdGrupo);
                    leer2.Exec(sQuery);

                    if (leer2.Leer())
                    {
                        // dtsUsuariosGrupo = leer.DataSetClase;
                        foreach (DataRow dtU in leer2.DataSetClase.Tables[0].Rows)
                        {
                            sUsuario = dtU["IdCliente"].ToString() + " - " + dtU["Nombre"].ToString();
                            TreeNode myNodeGrupoUsuario = myNodeGrupo.Nodes.Add(sUsuario);
                            myNodeGrupoUsuario.ImageIndex = 2;
                            myNodeGrupoUsuario.SelectedImageIndex = 2;
                            myNodeGrupoUsuario.Tag = "||" + dtU["IdCliente"].ToString();

                            TreeNode myNodeGrupoUsuarioAux = myNodeGrupoAux.Nodes.Add(sUsuario);
                            myNodeGrupoUsuarioAux.ImageIndex = 2;
                            myNodeGrupoUsuarioAux.SelectedImageIndex = 2;
                            myNodeGrupoUsuarioAux.Tag = "||" + dtU["IdCliente"].ToString();

                        }
                    }
                }
            }

            twEstados.EndUpdate();
            myNode.Expand();

            twFarmaciasClientes.EndUpdate();
            myNodeAux.Expand();
            // twFarmaciasClientes.ExpandAll(); 

            try
            {
                twEstados.Nodes[0].Text = twEstados.Nodes[0].Text;
            }
            catch { } 

        }

        private void CargarClientes()
        {
            string sSql = " Select C.IdEstado, C.IdCliente, Ct.Nombre " +
                     " From CFG_EstadosClientes C (NoLock) " +
                     " Inner Join CatClientes Ct (NoLock) On ( C.IdCliente = Ct.IdCliente )  " +
                     " Where C.Status = 'A' and C.IdEstado = '" + cboEstados.Data + "'";

            if (leer.Exec(sSql))
                dtsDatos = leer.DataSetClase;

            //dtsDatos = query.Usuarios(General.EntidadConectada);

            object NewColListView = null;
            lwClientes.Columns.Clear();
            lwClientes.Items.Clear();
            lwClientes.View = System.Windows.Forms.View.Details;
            NewColListView = lwClientes.Columns.Add("Nombre de Cliente", 290);

            if (leer.Leer())
            {
                ListViewItem itmX = null;
                string strValor = "";
                NewColListView = new object();

                foreach (DataRow dt in dtsDatos.Tables[0].Rows)
                {
                    strValor = dt["IdCliente"].ToString() + " - " + dt["Nombre"].ToString();
                    itmX = lwClientes.Items.Add(strValor, 0);
                    itmX.SubItems.Add("" + strValor);
                    itmX.SubItems[0].Tag = dt["IdCliente"].ToString();
                }
            }
        }

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

            if (twEstados.Nodes.Count > 0)
            {
                ActualizaMenu(e.Node.Tag.ToString());

                if (!myNodeSeleccionado.Tag.ToString().Contains("|"))
                    sIdFarmacia = myNodeSeleccionado.Tag.ToString();
                else
                {
                    sIdFarmacia = myNodeSeleccionado.Parent.Tag.ToString();
                    sIdCliente = myNodeSeleccionado.Tag.ToString().Replace("|", "");
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
            NewNode = twEstados.GetNodeAt(pt);
            sIdFarmacia = NewNode.Tag.ToString();

            string sSql = string.Format("Exec spp_Mtto_CFG_EstadosFarmaciasClientes '{0}', '{1}', '{2}', 'A' ", 
                cboEstados.Data, sIdFarmacia.Substring(2, 4), sIdCliente);

            if (NewNode.Parent.Tag.ToString() == "-1" || NewNode.Tag.ToString().Substring(0,2) == "|x")
            {
                //if (EsPadreValido(sIdFarmacia))
                {
                    if (!ExisteOpcion(NewNode, sIdCliente + " - " + sNombreCliente))
                    {
                        //CargarGrupos();
                        if (leer.Exec(sSql))
                        {
                            TreeNode myNodeRama = NewNode.Nodes.Add(sIdCliente + " - " + sNombreCliente);
                            myNodeRama.ImageIndex = 2;
                            myNodeRama.SelectedImageIndex = 2;
                            myNodeRama.Tag = "||" + sIdCliente;
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

        private void lwClientes_ItemDrag(object sender, ItemDragEventArgs e)
        {
            sIdCliente = lwClientes.FocusedItem.SubItems[0].Tag.ToString();
            sNombreCliente = lwClientes.FocusedItem.SubItems[0].Text.Substring(6).Trim();
            DoDragDrop(e.Item, DragDropEffects.Move);
        }
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
                        sQuery = string.Format("Exec spp_Mtto_CFG_EstadosFarmaciasClientes '{0}', '{1}', '{2}', 'C' ", 
                            cboEstados.Data, sIdFarmacia.Substring(2, 4), sIdCliente);
                    }

                    if (leer.Exec(sQuery))
                        twEstados.Nodes.Remove(myNodeSeleccionado);
                }
            }
            catch { }
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CargarFarmaciasEstado();
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
                        sQuery = string.Format("Exec spp_Mtto_CFG_EstadosFarmaciasClientes '{0}', '{1}', '{2}', 'C' ", 
                            cboEstados.Data, myNodeSeleccionado.Tag.ToString().Substring(2, 4), tNodo.Tag.ToString().Substring(2, 4));

                        leer.Exec(sQuery);
                        //if (leer.Exec(sQuery))
                        //    twEstados.Nodes.Remove(tNodo);
                    }
                }
                CargarFarmaciasEstado();
            }
            catch { }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            CargarClientes();
        }

        private void cboEstados_SelectedValueChanged(object sender, EventArgs e)
        {
            //if (cboEstados.SelectedIndex != 0)
            {
                CargarFarmaciasEstado();
                CargarClientes();
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            tmGrid.Enabled = false;
            tmGrid.Stop();

            if (tabControl.SelectedIndex == 1)
            {
                grid.Limpiar(false);
                CargarFarmaciasEstado();
                twFarmaciasClientes.ExpandAll();
            }
        }

        private void CargarSubClientes(string IdEstado, string IdFarmacia, string IdCliente )
        {
            /*
	            Select C.IdCliente, C.NombreCliente, C.IdSubCliente, C.NombreSubCliente, 
		             IsNull(Fc.IdEstado, '') as IdEstado, IsNull(F.Estado, '') as Estado, 
		             IsNull(Fc.IdFarmacia, '') as IdFarmacia, IsNull(F.Farmacia, '') as Farmacia, 
		             IsNull(Fc.Status, '') as StatusRelacion, 
		             (case when IsNull(Fc.Status, '') = '' then 0 else case when Fc.Status = 'A' then 1 else 0 end end) as StatusRelacionAux 
	            From vw_Clientes_SubClientes C (NoLock) 
	            Left Join CFG_EstadosFarmaciasClientesSubClientes Fc (NoLock) On ( C.IdCliente = Fc.IdCliente and C.IdSubCliente = Fc.IdSubCliente ) 
	            Left Join vw_Farmacias F (NoLock) On ( Fc.IdEstado = F.IdEstado and Fc.IdFarmacia = F.IdFarmacia ) 
             */

            string sSql = string.Format(" Select '{0}', '{1}', '{2}', C.IdSubCliente, C.NombreSubCliente, " + 
		             " (case when IsNull(Fc.Status, '') = '' then 0 else case when Fc.Status = 'A' then 1 else 0 end end) as StatusRelacionAux " + 
	                 " From vw_Clientes_SubClientes C (NoLock) " +
                     " Left Join CFG_EstadosFarmaciasClientesSubClientes Fc (NoLock) On ( C.IdCliente = Fc.IdCliente and C.IdSubCliente = Fc.IdSubCliente and Fc.IdEstado = '{0}' and Fc.IdFarmacia = '{1}') " + 
                     " Where C.IdCliente = '{2}' ", IdEstado, IdFarmacia, IdCliente );

        if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarSubClientes");
                General.msjError("Ocurrió un error al obtener la lista de SubClientes.");
            }
            else
            {
                grid.LlenarGrid(leer.DataSetClase);
                lblSubCliente.Text = grid.GetValue(1, 5);
            }
        }

        private void twFarmaciasClientes_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode myNode = e.Node;
            string sFarmacia = "", sCliente = "";

            grid.Limpiar(false);
            lblSubCliente.Text = "";

            tmGrid.Enabled = false;
            tmGrid.Stop();

            if (twFarmaciasClientes.Nodes.Count > 0)
            {
                if (myNode.Tag.ToString().Contains("||"))
                {
                    sFarmacia = myNode.Parent.Tag.ToString().Substring(2);
                    sCliente = myNode.Tag.ToString().Replace("|", "");
                    CargarSubClientes(cboEstados.Data, sFarmacia, sCliente);

                    tmGrid.Enabled = true;
                    tmGrid.Start();
                }
            }
        }

        private void tmGrid_Tick(object sender, EventArgs e)
        {
            if (this.ActiveControl.Name.ToUpper() == grdSubClientes.Name.ToUpper())
            {
                lblSubCliente.Text = "";
                if ( grid.Rows > 0 )
                    lblSubCliente.Text = grid.GetValue(grid.ActiveRow, 5);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sSql = "", sStatus = "A";
            bool bExito = true;

            if (cnn.Abrir())
            {
                cnn.IniciarTransaccion();

                for (int i = 1; i <= grid.Rows; i++)
                {
                    sStatus = "A";
                    if ( !grid.GetValueBool(i, 6) )
                        sStatus = "C";

                    sSql = string.Format(" Exec spp_Mtto_CFG_EstadosFarmaciasClientesSubClientes '{0}', '{1}', '{2}', '{3}', '{4}' ", 
                        grid.GetValue(i, 1), grid.GetValue(i, 2), 
                        grid.GetValue(i, 3), grid.GetValue(i, 4), sStatus);
                    if (!leer.Exec(sSql))
                    {
                        bExito = false;
                        break;
                    }
                }

                if (!bExito)
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(leer, "btnGuardar_Click");
                    General.msjError("Ocurrió un error al guardar la información de subclientes.");
                }
                else
                {
                    cnn.CompletarTransaccion();
                    General.msjUser("Información guardada satisfactoriamente.");
                }

                cnn.Cerrar();
            }
            else
            {
                General.msjAviso("No fue posible establecer comunicación con el servidor, intente de nuevo.");
            }

        }
    }
}
