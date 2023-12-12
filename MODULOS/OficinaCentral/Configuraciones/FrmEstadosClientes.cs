using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;

using DllFarmaciaSoft; 

namespace OficinaCentral.Configuraciones
{
    public partial class FrmEstadosClientes : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        TreeNode myNodeSeleccionado; //, myNodoUsuario;
        DataSet dtsEstados;
        DataSet dtsDatos;
        string sIdEstado = "", sNombreEstado = "", sUsuario = "";
        string sIdCliente = "", sNombreCliente = "";

        clsResize formResize; 

        public FrmEstadosClientes()
        {
            InitializeComponent();

            formResize = new clsResize(this); 

            leer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.DatosApp, this.Name);

            //General.Pantalla.AjustarTamaño(this, 90, 80); 
        }

        private void FrmEstadosClientes_Load(object sender, EventArgs e)
        {
            CargarEstadosClientes();
            CargarClientes();
        }

        private void CargarEstadosClientes()
        {
            string sSql = string.Format("Select Distinct IdEstado, Estado as Nombre From vw_Farmacias (NoLock) ");

            if (leer.Exec(sSql))
                dtsEstados = leer.DataSetClase;

            //dtsGrupos = query.Grupos(General.EntidadConectada);

            twEstados.Nodes.Clear();
            twEstados.BeginUpdate();

            TreeNode myNode;
            twEstados.Nodes.Clear();
            myNode = twEstados.Nodes.Add("ESTADOS");
            myNode.Tag = "-1";
            myNode.ImageIndex = 1;
            myNode.SelectedImageIndex = 1;

            if (leer.Leer())
            {
                foreach (DataRow dt in dtsEstados.Tables[0].Rows)
                {
                    sIdEstado = dt["IdEstado"].ToString();
                    sNombreEstado = dt["Nombre"].ToString();

                    TreeNode myNodeGrupo = myNode.Nodes.Add(sNombreEstado);
                    myNodeGrupo.ImageIndex = 1;
                    myNodeGrupo.SelectedImageIndex = 1;
                    myNodeGrupo.Tag = "|x" + sIdEstado;

                    string sQuery = " Select C.IdEstado, C.IdCliente, Ct.Nombre " +
                             " From CFG_EstadosClientes C (NoLock) " +
                             " Inner Join CatClientes Ct (NoLock) On ( C.IdCliente = Ct.IdCliente )  " +
                             " Where C.Status = 'A' and C.IdEstado = '" + sIdEstado + "'";

                    //dtsUsuariosGrupo = query.GruposUsuarios(General.EntidadConectada, sIdGrupo);
                    leer.Exec(sQuery);

                    if (leer.Leer())
                    {
                        // dtsUsuariosGrupo = leer.DataSetClase;
                        foreach (DataRow dtU in leer.DataSetClase.Tables[0].Rows)
                        {
                            sUsuario = dtU["IdCliente"].ToString() + " - " + dtU["Nombre"].ToString();
                            TreeNode myNodeGrupoUsuario = myNodeGrupo.Nodes.Add(sUsuario);
                            myNodeGrupoUsuario.ImageIndex = 0;
                            myNodeGrupoUsuario.SelectedImageIndex = 0;
                            myNodeGrupoUsuario.Tag = "||" + dtU["IdCliente"].ToString();
                        }
                    }
                }
            }

            twEstados.EndUpdate();
            myNode.Expand();

            twEstados.Nodes[0].Text = twEstados.Nodes[0].Text; 
        }

        private void CargarClientes()
        {
            string sSql = string.Format("Select * From CatClientes (noLock) Where Status = 'A' ");

            if (leer.Exec(sSql))
                dtsDatos = leer.DataSetClase;

            //dtsDatos = query.Usuarios(General.EntidadConectada);

            if (leer.Leer())
            {
                ListViewItem itmX = null;
                object NewColListView = null;
                string strValor = "";

                lwClientes.Columns.Clear();
                lwClientes.Items.Clear();
                lwClientes.View = System.Windows.Forms.View.Details;
                NewColListView = lwClientes.Columns.Add("Nombre de Cliente", 350);

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
            //toolTip.SetToolTip(twEstados, "");

            if (twEstados.Nodes.Count > 0)
            {

                ActualizaMenu(e.Node.Tag.ToString());

                if (!myNodeSeleccionado.Tag.ToString().Contains("|"))
                {
                    sIdEstado = myNodeSeleccionado.Tag.ToString();
                }
                else
                {
                    sIdEstado = myNodeSeleccionado.Parent.Tag.ToString();
                    sIdCliente = myNodeSeleccionado.Tag.ToString().Replace("|", "");
                    //toolTip.SetToolTip(twEstados, e.Node.Text);
                }
            }
        }

        private bool EsPadreValido(string Valor)
        {
            bool bRegresa = false;

            if (Valor != "-1" &&  // Valor.Substring(0, 2) != "|x" && 
                Valor.Substring(0, 2) != "00" && Valor.Substring(0, 2) != "|0" && Valor.Substring(0, 2) != "||")
                bRegresa = true;

            if (myNodeSeleccionado.Tag.ToString() == "-1")
                bRegresa = false;



            return bRegresa;
        }

        private void twEstados_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode NewNode; 
            Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
            NewNode = twEstados.GetNodeAt(pt);

            sIdEstado = myNodeSeleccionado.Tag.ToString();
            sIdEstado = NewNode.Tag.ToString();


            string sSql = string.Format("Exec spp_Mtto_CFG_EstadosClientes '{0}', '{1}', 'A' ", sIdEstado.Substring(2,2), sIdCliente );

            if (EsPadreValido(sIdEstado))
            {
                if (!ExisteOpcion(NewNode, sIdCliente + " - " + sNombreCliente))
                {
                    //CargarGrupos();
                    if (leer.Exec(sSql))
                    {
                        TreeNode myNodeRama = NewNode.Nodes.Add(sIdCliente + " - " + sNombreCliente);
                        myNodeRama.ImageIndex = 0;
                        myNodeRama.SelectedImageIndex = 0;
                        myNodeRama.Tag = "||" + sIdCliente;
                        // AsignarIcono(myNodeRama, RamaSeleccionada.TipoRama);
                    }
                    NewNode.Expand();
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
                        sQuery  = string.Format("Exec spp_Mtto_CFG_EstadosClientes '{0}', '{1}', 'C' ", 
                            sIdEstado.Substring(2, 2), sIdCliente);
                    }

                    if (leer.Exec(sQuery))
                        twEstados.Nodes.Remove(myNodeSeleccionado);
                }
            }
            catch { }
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CargarEstadosClientes();
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
                        sQuery = string.Format("Exec spp_Mtto_CFG_EstadosClientes '{0}', '{1}', 'C' ", 
                            myNodeSeleccionado.Tag.ToString().Substring(2, 2), tNodo.Tag.ToString().Substring(2, 4));
                        
                        leer.Exec(sQuery);

                        //if (leer.Exec(sQuery))
                        //    twEstados.Nodes.Remove(tNodo);
                    }
                    CargarEstadosClientes();
                }
            }
            catch { }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            CargarClientes();
        }
        //    toolTip.SetToolTip(lwClientes, lwClientes.FocusedItem.Text);

    }
}
