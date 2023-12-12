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
using DllFarmaciaSoft;

namespace OficinaCentral.FarmaciasConvenioVales
{
    public partial class FrmConfigurarFarmaciasConvenio : FrmBaseExt
    {
        //basGenerales Fg = new basGenerales();
        // clsGuardarSC Guardar = new clsGuardarSC();
        clsConsultas query;
        DataSet dtsDatos = new DataSet();
        DataSet dtsGrupos = new DataSet(), dtsUsuariosGrupo = new DataSet();
        DataSet dtsFarmaciasEstado = new DataSet();

        // DllFarmaciaSoft.Usuarios_y_Permisos.FrmUsuarios myUsuario;
        TreeNode myNodeSeleccionado; //, myNodoUsuario;
        int iIndexNodo = 0;
        string sIdClave = "";
        string sClaveDesc = "";
        string sIdGrupo = "";

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsConsultas leer = new clsConsultas(General.DatosConexion, "Configuracion", "GruposDeUsuarios", Application.ProductVersion,true);
        clsLeer reader, reader2;

        public FrmConfigurarFarmaciasConvenio()
        {
            InitializeComponent();
        }

        private void FrmGruposUsuarios_Load(object sender, EventArgs e)
        {
            query = new clsConsultas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
            reader = new clsLeer(ref cnn);
            reader2 = new clsLeer(ref cnn);

            CargarEstados();
            ObtenerFarmaciasEstado();
            ActualizaMenu("0");
        }

        #region Funciones 
        private void CargarEstados()
        {
            cboEstados.Clear();
            cboEstados.Add();
            cboEstados.Add(query.EstadosConFarmacias("CargarEstados()"), true, "IdEstado", "NombreEstado");

            cboEstados.SelectedIndex = 0;
        }

        private void ObtenerFarmaciasEstado()
        {
            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");
            cboFarmacias.SelectedIndex = 0;
            dtsFarmaciasEstado = query.Farmacias("ObtenerFarmaciasEstado()");            
        }

        private void CargarFarmacias()
        {
            try
            {
                cboFarmacias.Filtro = " Status = 'A' "; 
                cboFarmacias.Add(dtsFarmaciasEstado.Tables[0].Select(string.Format("IdEstado = '{0}'", cboEstados.Data)), true, "IdFarmacia", "NombreFarmacia");
            }
            catch { }
            
            cboFarmacias.SelectedIndex = 0;
        }

        private void CargarGrupos()
        {
            query.MostrarMsjSiLeerVacio = false;
            dtsGrupos = query.Farmacias(cboEstados.Data, cboFarmacias.Data,"CargarGrupos()");

            twGrupos.Nodes.Clear();
            twGrupos.BeginUpdate();

            TreeNode myNode;
            twGrupos.Nodes.Clear();
            myNode = twGrupos.Nodes.Add(string.Format("Farmacias del estado de {0}", cboEstados.Text) );
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
                        sIdGrupo = reader.Campo("IdFarmacia");
                        sNombreGrupo = reader.Campo("Farmacia");

                        TreeNode myNodeGrupo = myNode.Nodes.Add(sNombreGrupo);
                        myNodeGrupo.ImageIndex = 1;
                        myNodeGrupo.SelectedImageIndex = 1;
                        myNodeGrupo.Tag = sIdGrupo;

                        reader2.DataSetClase = query.FarmaciasConvenio_Miembros(cboEstados.Data, sIdGrupo, "CargarGrupos()");
                        while (reader2.Leer())
                        {
                            if (reader2.Campo("Status").ToUpper() == "A")
                            {
                                sMiembro = reader2.Campo("IdFarmaciaConvenio") + " - " + reader2.Campo("FarmaciaConvenio") + " - " + reader2.Campo("Direccion"); 
                                TreeNode myNodeGrupoUsuario = myNodeGrupo.Nodes.Add(sMiembro);
                                myNodeGrupoUsuario.ImageIndex = 0;
                                myNodeGrupoUsuario.SelectedImageIndex = 0;
                                myNodeGrupoUsuario.Tag = "|" + reader2.Campo("IdFarmaciaConvenio");
                            }
                        }
                    }
                }
            }

            twGrupos.EndUpdate();
            myNode.Expand();

            twGrupos.Nodes[0].Text = twGrupos.Nodes[0].Text;

        }

        private void CargarFarmaciasConvenio()
        {
            query.MostrarMsjSiLeerVacio = false;
            dtsDatos = query.FarmaciasConvenio(cboEstados.Data, "CargarFarmacias()");
            query.MostrarMsjSiLeerVacio = true;
            reader.DataSetClase = dtsDatos;
            
            dtsDatos = reader.DataSetClase;
            ListViewItem itmX = null;
            object NewColListView = null;
            string strValor = "";

            lwUsuarios.Columns.Clear();
            lwUsuarios.Items.Clear();
            lwUsuarios.View = System.Windows.Forms.View.Details;
            NewColListView = lwUsuarios.Columns.Add("Farmacias convenio", lwUsuarios.Width - 21);

            if (reader.Leer())
            {
                reader.RegistroActual = 1; 
                // foreach (DataRow dt in dtsDatos.Tables[0].Rows)
                while( reader.Leer() )
                {
                    strValor = reader.Campo("IdFarmaciaConvenio") + " - " + reader.Campo("Nombre") + " - " + reader.Campo("Direccion");
                    itmX = lwUsuarios.Items.Add(strValor, 0);
                    itmX.SubItems.Add("" + strValor);
                    itmX.SubItems[0].Tag = reader.Campo("IdFarmaciaConvenio"); 
                }
            }
            
        } 
        #endregion Funciones

        #region Treeview  
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sQuery = ""; 
            string sDefault = " Set Status = 'C', Actualizado = '0' "; 

            try
            {
                if (myNodeSeleccionado.Tag.ToString() != "-1")
                {
                    if (!myNodeSeleccionado.Tag.ToString().Contains("|"))
                    {
                        // No se permite borrar Farmacias del Estado.
                        ////sQuery = sQuery + string.Format("Update CFG_Farmacias_ConvenioVales {2} Where IdEstado = '{0}' and IdFarmacia = '{1}' ",
                        ////    cboEstados.Data, sIdGrupo, sDefault);
                    }
                    else
                    {
                        sQuery = string.Format("Update CFG_Farmacias_ConvenioVales {3} Where IdEstado = '{0}' and IdFarmacia = '{1}' and IdFarmaciaConvenio = '{2}' ",
                            cboEstados.Data, sIdGrupo, sIdClave, sDefault);
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
            CargarFarmaciasConvenio();
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
            string sClave = lwUsuarios.FocusedItem.SubItems[0].Text;
            int iOpcion = 1;

            string sSql = string.Format("Exec spp_Mtto_CFG_Farmacias_ConvenioVales '{0}', '{1}', '{2}', '{3}' ",
                cboEstados.Data, sIdGrupo, sIdClave, iOpcion);

            //if (!ExisteOpcion(NewNode, sIdClave + " - " + sClaveDesc))
            if (!ExisteOpcion(NewNode, sClave))
            {
                if (Fg.Mid(sIdGrupo, 1, 1) != "|")
                {
                    //CargarGrupos();
                    if (reader.Exec(sSql))
                    {
                        //TreeNode myNodeRama = NewNode.Nodes.Add(sIdClave + " - " + sClaveDesc);
                        TreeNode myNodeRama = NewNode.Nodes.Add(sClave);
                        myNodeRama.ImageIndex = 0;
                        myNodeRama.SelectedImageIndex = 0;
                        myNodeRama.Tag = "|" + sIdClave;
                        // AsignarIcono(myNodeRama, RamaSeleccionada.TipoRama);
                    }
                    NewNode.Expand();
                }
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
            sClaveDesc = lwUsuarios.FocusedItem.SubItems[0].Text.Substring(6).Trim();
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        #endregion Arrastrar Claves

        #region Eventos 
        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");
            if (cboEstados.SelectedIndex != 0)
            {
                CargarFarmacias();
                CargarFarmaciasConvenio();
            }
            else
            {
                twGrupos.Nodes.Clear();
                lwUsuarios.Items.Clear();
            }
        }

        private void cboFarmacias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFarmacias.SelectedIndex != 0)
            {
                CargarGrupos();
                //CargarFarmaciasConvenio();
                ActualizaMenu("2");
            }
            else
            {
                twGrupos.Nodes.Clear();
                lwUsuarios.Items.Clear();
            }
        }
        #endregion Eventos 

    }
}
