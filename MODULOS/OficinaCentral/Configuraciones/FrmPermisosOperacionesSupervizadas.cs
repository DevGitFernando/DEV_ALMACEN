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
    public partial class FrmPermisosOperacionesSupervizadas : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer, leer2;

        clsConsultas query;

        TreeNode myNodeSeleccionado; //, myNodoUsuario;
        // DataSet dtsEstados;
        // DataSet dtsFarmacias;
        DataSet dtsDatos;
        string sIdFarmacia = "", sNombreFarmacia = "", sUsuario = "";
        string sIdCliente = "", sNombreCliente = "";

        clsResize formResize; 

        public FrmPermisosOperacionesSupervizadas()
        {
            InitializeComponent();

            formResize = new clsResize(this); 

            leer = new clsLeer(ref cnn);
            leer2 = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.DatosApp, this.Name);

            //General.Pantalla.AjustarTamaño(this, 80, 70); 
        }

        private void FrmEstadosClientes_Load(object sender, EventArgs e)
        {
            CargarEstados();
        }

        private void CargarEstados()
        {
            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");
            cboEstados.Add(query.ComboEstados("CargarEstados()"), true, "IdEstado", "Nombre");
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

            TreeNode myNode = new TreeNode();
            twEstados.Nodes.Clear();

            if (cboEstados.SelectedIndex != 0)
            {
                twEstados.Nodes.Clear();
                myNode = twEstados.Nodes.Add("FARMACIAS");
                myNode.Tag = "-1";
                myNode.ImageIndex = 0;
                myNode.SelectedImageIndex = 0;
            }

            while (leer.Leer())
            {
                sIdFarmacia = leer.Campo("IdFarmacia");
                sNombreFarmacia = leer.Campo("NombreFarmacia");

                // foreach (DataRow dt in dtsEstados.Tables[0].Rows)
                {
                    TreeNode myNodeGrupo = myNode.Nodes.Add(sNombreFarmacia);
                    myNodeGrupo.ImageIndex = 1;
                    myNodeGrupo.SelectedImageIndex = 1;
                    myNodeGrupo.Tag = "|x" + sIdFarmacia;

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
                        }
                    }
                }
            }

            twEstados.EndUpdate();
            myNode.Expand();

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
                        if (leer.Exec(sQuery))
                            twEstados.Nodes.Remove(tNodo);
                    }
                }
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
    }
}
