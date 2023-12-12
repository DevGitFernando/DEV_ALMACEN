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

namespace Configuracion.Configuracion
{
    public partial class FrmPermisosOperacionesSupervizadasPorFarmaciaHuellas : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer, leer2;

        clsConsultas query;

        TreeNode myNodeSeleccionado; //, myNodoUsuario;
        // DataSet dtsEstados;
        DataSet dtsFarmacias;
        // DataSet dtsDatos;
        string sIdFarmacia = ""; // , sNombreFarmacia = "", 
        // string sUsuario = "";
        string sIdPersonal = "", sNombrePersonal = "";

        public FrmPermisosOperacionesSupervizadasPorFarmaciaHuellas()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            leer2 = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnConfiguracion.DatosApp, this.Name, false);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnConfiguracion.DatosApp, this.Name);
        }

        private void FrmEstadosClientes_Load(object sender, EventArgs e)
        {
            CargarEstados();
            ListaDeFarmacias();
            CargarUsuarios(); 
        }

        private void CargarEstados()
        {
            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");
            cboEstados.Add(query.EstadosConFarmacias("CargarEstados()"), true, "IdEstado", "Estado");
            cboEstados.SelectedIndex = 0;
        }

        private void ListaDeFarmacias()
        {
            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>"); 
            dtsFarmacias = query.Farmacias("ListaDeFarmacias()");
            cboFarmacias.SelectedIndex = 0; 
        } 

        private void CargarUsuarios()
        {
            string sSql = string.Format("Select *, (ApPaterno + ' ' + ApMaterno + ' ' + Nombre) As NombreCompleto " + 
                " From CatPersonalHuellas P (NoLock)  " +
                " Where  Status = 'A' " ); 
                //"P.IdEstado = '{0}' and IdFarmacia = '{1}'" , cboEstados.Data, cboFarmacias.Data); 

            //dtsDatos = query.Usuarios(General.EntidadConectada);

            leer.Exec(sSql);
            if (leer.Leer())
            {
                ListViewItem itmX = null;
                object NewColListView = null;
                string strValor = "";

                lwPersonal.Columns.Clear();
                lwPersonal.Items.Clear();
                lwPersonal.View = System.Windows.Forms.View.Details;
                NewColListView = lwPersonal.Columns.Add("Usuario", 290);

                foreach (DataRow dt in leer.DataRowsClase)
                {
                    strValor = dt["IdPersonal"].ToString() + " - " + dt["NombreCompleto"].ToString();
                    //strValor = "  " + dt["NombreCompleto"].ToString();
                    itmX = lwPersonal.Items.Add(strValor, 0);
                    itmX.SubItems.Add("" + strValor);
                    itmX.SubItems[0].Tag = dt["IdPersonal"].ToString();
                }
            }
        }

        private void CargarOperaciones()
        {
            string sSql = string.Format(" Select * From Net_Operaciones_SupervisadasPorFarmaciaHuellas (NoLock) " ); 

            twOperaciones.Nodes.Clear();
            twOperaciones.BeginUpdate();

            TreeNode myNode;
            twOperaciones.Nodes.Clear();
            myNode = twOperaciones.Nodes.Add("Permisos especiales");
            myNode.Tag = "-1";
            myNode.ImageIndex = 1;
            myNode.SelectedImageIndex = 1;

            leer2.Exec(sSql); 
            while (leer2.Leer())
            {
                string sIdGrupo = "", sNombreGrupo = "", sUsuario = "";

                sIdGrupo = leer2.Campo("IdOperacion");
                sNombreGrupo = leer2.Campo("Nombre");

                TreeNode myNodeGrupo = myNode.Nodes.Add(sIdGrupo + " - " + sNombreGrupo);
                myNodeGrupo.ImageIndex = 1;
                myNodeGrupo.SelectedImageIndex = 1;
                myNodeGrupo.Tag = "|x" + sIdGrupo;

                //dtsUsuariosGrupo = query.GruposUsuarios(General.EntidadConectada, sIdGrupo);
                sSql = string.Format("Select N.*, (ApPaterno + ' ' + ApMaterno + ' ' + Nombre) As NombreCompleto " +
                        " From Net_Permisos_Operaciones_SupervisadasPorFarmaciaHuellas N (noLock) " +
                        //" Inner Join Net_Usuarios U (noLock) On ( N.IdPersonal = U.IdPersonal ) " +
                        " Inner Join CatPersonalHuellas P (NoLock) On ( N.IdPersonal = P.IdPersonal )  " +
                        " Where N.IdEstado = '{0}' and N.IdFarmacia = '{1}' and N.IdOperacion = '{2}' and N.Status = 'A' ",
                        cboEstados.Data, cboFarmacias.Data, sIdGrupo); 

                leer.Exec(sSql); 
                while (leer.Leer())
                {
                    // sUsuario = dtU["LoginUser"].ToString(); 
                    sUsuario = leer.Campo("IdPersonal") + " - " + leer.Campo("NombreCompleto") ; 
                    TreeNode myNodeGrupoUsuario = myNodeGrupo.Nodes.Add(sUsuario);
                    myNodeGrupoUsuario.ImageIndex = 2;
                    myNodeGrupoUsuario.SelectedImageIndex = 2;
                    myNodeGrupoUsuario.Tag = "||" + leer.Campo("IdPersonal");
                }
            }

            twOperaciones.EndUpdate();
            myNode.Expand();

            twOperaciones.Nodes[0].Text = twOperaciones.Nodes[0].Text;
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

            if (twOperaciones.Nodes.Count > 0)
            {
                ActualizaMenu(e.Node.Tag.ToString());

                if (!myNodeSeleccionado.Tag.ToString().Contains("|"))
                    sIdFarmacia = myNodeSeleccionado.Tag.ToString();
                else
                {
                    sIdFarmacia = myNodeSeleccionado.Parent.Tag.ToString();
                    sIdPersonal = myNodeSeleccionado.Tag.ToString().Replace("|", "");
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
            NewNode = twOperaciones.GetNodeAt(pt);
            sIdFarmacia = NewNode.Tag.ToString().Substring(2, 4);

            string sSql = string.Format("Exec spp_Net_Permisos_Operaciones_SupervisadasPorFarmaciaHuellas '{0}', '{1}', '{2}', '{3}', '1' ",
                cboEstados.Data, cboFarmacias.Data, sIdPersonal, sIdFarmacia);

            if (NewNode.Parent.Tag.ToString() == "-1" || NewNode.Tag.ToString().Substring(0,2) == "|x")
            {
                //if (EsPadreValido(sIdFarmacia))
                {
                    if (!ExisteOpcion(NewNode, sIdPersonal + " - " + sNombrePersonal))
                    {
                        //CargarGrupos();
                        if (leer.Exec(sSql))
                        {
                            TreeNode myNodeRama = NewNode.Nodes.Add(sIdPersonal + " - " + sNombrePersonal);
                            myNodeRama.ImageIndex = 2;
                            myNodeRama.SelectedImageIndex = 2;
                            myNodeRama.Tag = "||" + sIdPersonal;
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
            sIdPersonal = lwPersonal.FocusedItem.SubItems[0].Tag.ToString();
            sNombrePersonal = lwPersonal.FocusedItem.SubItems[0].Text.Substring(11).Trim();
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
                        sQuery = string.Format("Exec spp_Net_Permisos_Operaciones_SupervisadasPorFarmaciaHuellas '{0}', '{1}', '{2}', '{3}', '2' ",
                            cboEstados.Data, cboFarmacias.Data, sIdPersonal, sIdFarmacia.Substring(2, 4));
                    }

                    if (leer.Exec(sQuery))
                        twOperaciones.Nodes.Remove(myNodeSeleccionado);
                }
            }
            catch { }
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CargarOperaciones(); 
        }

        private void eliminarClientesDelEstadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sQuery = "";
            string sIdOper = ""; 
            try
            {
                if (myNodeSeleccionado.Tag.ToString().Substring(0, 2) == "|x")
                {
                    sIdOper = myNodeSeleccionado.Tag.ToString().Substring(2, 4); 
                    foreach (TreeNode tNodo in myNodeSeleccionado.Nodes)
                    {
                        sQuery = string.Format("Exec spp_Net_Permisos_Operaciones_SupervisadasPorFarmaciaHuellas '{0}', '{1}', '{2}', '{3}', '2' ",
                                                   cboEstados.Data, cboFarmacias.Data, tNodo.Text.Substring(0, 4), sIdOper);
                        leer.Exec(sQuery); 
                    }
                    CargarOperaciones(); 
                }
            }
            catch { }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            //CargarUsuarios();
        }

        private void cboEstados_SelectedValueChanged(object sender, EventArgs e)
        {
            cboFarmacias.Clear();
            cboFarmacias.Add(); 
            if ( cboEstados.SelectedIndex!= 0 ) 
            {
                cboFarmacias.Add(dtsFarmacias.Tables[0].Select(string.Format("IdEstado = '{0}' ", cboEstados.Data)), true, "IdFarmacia", "NombreFarmacia");
            }
            cboFarmacias.SelectedIndex = 0; 
        }

        private void cboFarmacias_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lwPersonal.Items.Clear();
            twOperaciones.Nodes.Clear(); 

            if ( cboFarmacias.SelectedIndex != 0 ) 
            {
                CargarOperaciones();
            }
        }
    }
}
