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
    public partial class FrmEmpresasFarmacias : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer, leer2, leer3;

        clsConsultas query;

        TreeNode myNodeSeleccionado; //, myNodoUsuario;
        DataSet dtsEstados;
        // DataSet dtsFarmacias;
        DataSet dtsDatos;

        string sClaveRenapo = "";
        string sIdFarmacia = "", sUsuario = "";  // , sNombreFarmacia = "", 
        string sIdCliente = "", sNombreCliente = "";

        clsResize formResize; 

        public FrmEmpresasFarmacias()
        {
            InitializeComponent();

            ////formResize = new clsResize(this); 

            leer = new clsLeer(ref cnn);
            leer2 = new clsLeer(ref cnn);
            leer3 = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name, false);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.DatosApp, this.Name);

            //General.Pantalla.AjustarTamaño(this, 90, 80); 
        }

        private void FrmEstadosClientes_Load(object sender, EventArgs e)
        {
            CargarEmpresas();
            CargarEstados();
        }

        private void CargarEmpresas()
        {
            cboEmpresas.Clear();
            cboEmpresas.Add("0", "<< Seleccione >>");
            cboEmpresas.Add(query.Empresas("CargarEmpresas()"), true, "IdEmpresa", "NombreCorto");
            cboEmpresas.SelectedIndex = 0;
        }

        private void CargarEstados()
        {
            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");
            dtsEstados = query.EmpresasEstados("CargarEstados()");
            // cboEstados.Add(query.ComboEstados("CargarEstados()"), true, "IdEstado", "Nombre");
            cboEstados.SelectedIndex = 0;
        }

        private void CargarFarmaciasEmpresa()
        {
            string sSql = string.Format("Select Distinct IdEstado, ClaveRenapo, Estado From vw_EmpresasFarmacias (noLock) Where IdEmpresa = '{0}' and Status = 'A' ", cboEmpresas.Data);
            string sSql_Aux = string.Format("Select * From vw_EmpresasFarmacias (noLock) Where IdEmpresa = '{0}' and Status = 'A' Order by IdEstado, IdFarmacia ", cboEmpresas.Data);

            //if (leer.Exec(sSql))
            //    dtsEstados = leer.DataSetClase;
            //dtsGrupos = query.Grupos(General.EntidadConectada);

            leer.DataSetClase = query.Empresas(cboEmpresas.Data , "CargarFarmaciasEmpresa()");
            //leer.Exec(sSql);
            twEstados.Nodes.Clear();
            twEstados.BeginUpdate();

            TreeNode myNode = new TreeNode();
            twEstados.Nodes.Clear();

            if (cboEmpresas.SelectedIndex != 0)
            {
                twEstados.Nodes.Clear();
                myNode = twEstados.Nodes.Add("EMPRESA : " + cboEmpresas.Text.ToUpper());
                myNode.Tag = "-1";
                myNode.ImageIndex = 0;
                myNode.SelectedImageIndex = 0;
            }

            while (leer.Leer())
            {
                //sIdFarmacia = leer.Campo("IdFarmacia");
                //sNombreFarmacia = leer.Campo("NombreFarmacia");

                // foreach (DataRow dt in dtsEstados.Tables[0].Rows)
                {
                    //TreeNode myNodeGrupo = myNode.Nodes.Add(sNombreFarmacia);
                    //myNodeGrupo.ImageIndex = 1;
                    //myNodeGrupo.SelectedImageIndex = 1;
                    //myNodeGrupo.Tag = "|x" + sIdFarmacia;

                    //string sQuery = " Select C.IdEstado, C.IdCliente, Ct.Nombre " +
                    //         " From CFG_EstadosFarmaciasClientes C (NoLock) " +
                    //         " Inner Join CatClientes Ct (NoLock) On ( C.IdCliente = Ct.IdCliente )  " +
                    //         " Where C.Status = 'A' and C.IdEstado = '" + cboEstados.Data + "' and C.IdFarmacia = '" + sIdFarmacia + "'";

                    //dtsUsuariosGrupo = query.GruposUsuarios(General.EntidadConectada, sIdGrupo);
                    leer2.Exec(sSql);

                    while (leer2.Leer())
                    {
                        sUsuario = leer2.Campo("IdEstado") + " - " + leer2.Campo("Estado");
                        TreeNode myNodeGrupoUsuario = myNode.Nodes.Add(sUsuario);
                        myNodeGrupoUsuario.ImageIndex = 1;
                        myNodeGrupoUsuario.SelectedImageIndex = 1;
                        myNodeGrupoUsuario.Tag = "p|" + sUsuario;

                        // leer3.DataRowsClase = leer2.DataTableClase.Select(string.Format( " IdEstado = '{0}' ", leer2.Campo("IdEstado"))); }
                        sSql_Aux = string.Format("Select * " + 
                            " From vw_EmpresasFarmacias (noLock) " + 
                            " Where IdEmpresa = '{0}' and IdEstado = '{1}' and Status = 'A' ", cboEmpresas.Data, leer2.Campo("IdEstado"));
                        leer3.Exec(sSql_Aux);
                        
                        while (leer3.Leer()) 
                        {
                            // sUsuario = leer3.Campo("IdEstado") + leer3.Campo("ClaveRenapo") + " - " + leer3.Campo("IdFarmacia") + " - " + leer3.Campo("Farmacia");
                            sUsuario = leer3.Campo("IdFarmacia") + " - " + leer3.Campo("Farmacia");
                            TreeNode myNodeGrupoUsuario_Farmacia = myNodeGrupoUsuario.Nodes.Add(sUsuario);
                            myNodeGrupoUsuario_Farmacia.ImageIndex = 4;
                            myNodeGrupoUsuario_Farmacia.SelectedImageIndex = 4;
                            myNodeGrupoUsuario_Farmacia.Tag = "||" + sUsuario;
                        }

                        ////// dtsUsuariosGrupo = leer.DataSetClase;
                        ////foreach (DataRow dtU in leer2.DataSetClase.Tables[0].Rows)
                        ////{
                        ////    sUsuario = dtU["IdEstado"].ToString() + dtU["ClaveRenapo"].ToString() + " - " + dtU["IdFarmacia"].ToString() + " - " + dtU["Farmacia"].ToString();
                        ////    TreeNode myNodeGrupoUsuario = myNode.Nodes.Add(sUsuario);
                        ////    myNodeGrupoUsuario.ImageIndex = 4;
                        ////    myNodeGrupoUsuario.SelectedImageIndex = 4;
                        ////    myNodeGrupoUsuario.Tag = "||" + sUsuario;
                        ////}
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

        private void CargarFarmacias()
        {
            bool bEsConsignacion = (bool)((DataRow)cboEmpresas.ItemActual.Item)["EsDeConsignacion"]; 
            string sSql = string.Format(" Select C.IdEstado, F.IdFarmacia, F.NombreFarmacia " +
                     " From CatEstados C (NoLock) " +
                     " Inner Join CatFarmacias F (NoLock) On ( C.IdEstado = F.IdEstado )  " +
                     " Where F.Status = 'A' and C.IdEstado = '{0}' and F.EsDeConsignacion = '{1}' ", 
                     cboEstados.Data, Convert.ToInt32(bEsConsignacion));


            //// 
            sSql = string.Format(" Select C.IdEstado, F.IdFarmacia, F.NombreFarmacia " +
                     " From CatEstados C (NoLock) " +
                     " Inner Join CatFarmacias F (NoLock) On ( C.IdEstado = F.IdEstado )  " +
                     " Where F.Status = 'A' and C.IdEstado = '{0}' " + 
                     " Order by IdEstado, IdFarmacia ", cboEstados.Data);

            if (leer.Exec(sSql))
                dtsDatos = leer.DataSetClase;

            //dtsDatos = query.Usuarios(General.EntidadConectada);

            object NewColListView = null;
            lwFarmacias.Columns.Clear();
            lwFarmacias.Items.Clear();
            lwFarmacias.View = System.Windows.Forms.View.Details;
            NewColListView = lwFarmacias.Columns.Add("Nombre de Farmacia", 290);

            if (leer.Leer())
            {
                ListViewItem itmX = null;
                string strValor = "";
                NewColListView = new object();

                foreach (DataRow dt in dtsDatos.Tables[0].Rows)
                {
                    strValor = dt["IdFarmacia"].ToString() + " - " + dt["NombreFarmacia"].ToString();
                    itmX = lwFarmacias.Items.Add(strValor, 4);
                    itmX.SubItems.Add("" + strValor);
                    itmX.SubItems[0].Tag = dt["IdFarmacia"].ToString();
                }
            }
        }

        #region Asignar Clientes 
        private void ActualizaMenu(string Tag)
        {
            mnEmpresas.Items[eliminarToolStripMenuItem.Name].Enabled = false;
            mnEmpresas.Items[eliminarClientesDelEstadoToolStripMenuItem.Name].Enabled = false;
            mnEmpresas.Items[actualizarToolStripMenuItem.Name].Enabled = true;

            if (Tag.Substring(0, 2) == "-1")
            {
                mnEmpresas.Items[eliminarClientesDelEstadoToolStripMenuItem.Name].Enabled = true;
                mnEmpresas.Items[eliminarToolStripMenuItem.Name].Enabled = false;
            }

            if (Tag.Substring(0, 2) == "||")
            {
                mnEmpresas.Items[eliminarClientesDelEstadoToolStripMenuItem.Name].Enabled = false;
                mnEmpresas.Items[eliminarToolStripMenuItem.Name].Enabled = true;
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
            string sValor = Valor.Substring(0, 2); 

            if ( //Valor == "-1" ||  // Valor.Substring(0, 2) != "|x" && 
                sValor != "p|") //&& Valor.Substring(0, 2) == "|0" && Valor.Substring(0, 2) == "||")
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

            string sSql = string.Format("Exec spp_Mtto_CFG_EmpresasFarmacias @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Status = 'A' ", 
                cboEmpresas.Data, cboEstados.Data, sIdCliente);

            // if (NewNode.Tag.ToString() == "-1" || NewNode.Tag.ToString().Substring(0, 2) != "||")
            {
                if (EsPadreValido(sIdFarmacia))
                {
                    if (!ExisteOpcion(NewNode, cboEstados.Data + sClaveRenapo + " - " + sIdCliente + " - " + sNombreCliente))
                    {
                        //CargarGrupos();
                        if (leer.Exec(sSql))
                        {
                            TreeNode myNodeRama = NewNode.Nodes.Add(cboEstados.Data + sClaveRenapo + " - " + sIdCliente + " - " + sNombreCliente);
                            myNodeRama.ImageIndex = 4;
                            myNodeRama.SelectedImageIndex = 4;
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
            sIdCliente = lwFarmacias.FocusedItem.SubItems[0].Tag.ToString();
            sNombreCliente = lwFarmacias.FocusedItem.SubItems[0].Text.Substring(6).Trim();
            DoDragDrop(e.Item, DragDropEffects.Move);
        }
        #endregion Asignar Clientes

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sQuery = "";
            string sTag = myNodeSeleccionado.Tag.ToString();

            try
            {
                if (sTag != "-1")
                {
                    if (sTag.Substring(0, 2) == "||")
                    {
                        sQuery = string.Format("Exec spp_Mtto_CFG_EmpresasFarmacias '{0}', '{1}', '{2}', 'C' ",
                            cboEmpresas.Data, sTag.Substring(2, 2), sTag.Substring(9, 4));
                    }

                    if (leer.Exec(sQuery))
                        twEstados.Nodes.Remove(myNodeSeleccionado);
                }
            }
            catch { }
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CargarFarmaciasEmpresa();
        }

        private void eliminarClientesDelEstadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sQuery = "";
            string sTag = myNodeSeleccionado.Tag.ToString(); 

            try
            {
                if (sTag.Substring(0, 2) == "-1")
                {
                    foreach (TreeNode tNodo in myNodeSeleccionado.Nodes)
                    {
                        sTag = tNodo.Tag.ToString();                        
                        sQuery = string.Format("Exec spp_Mtto_CFG_EmpresasFarmacias '{0}', '{1}', '{2}', 'C' ",
                            cboEmpresas.Data, sTag.Substring(2, 2), sTag.Substring(9, 4));

                        leer.Exec(sQuery);

                        //if (leer.Exec(sQuery))
                        //    twEstados.Nodes.Remove(tNodo);
                    }
                }
                CargarFarmaciasEmpresa();
            }
            catch { }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            CargarFarmacias();
        }

        private void cboEstados_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboEstados.SelectedIndex != 0)
            {
                sClaveRenapo = ((DataRow)cboEstados.ItemActual.Item)["ClaveRenapo"].ToString();
                //CargarFarmaciasEstado();
                CargarFarmacias();
            }
        }

        private void cboEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");

            twEstados.Nodes.Clear();
            lwFarmacias.Items.Clear();

            if (cboEmpresas.SelectedIndex != 0)
            {
                CargarEstados(); 
                CargarFarmaciasEmpresa();
                cboEstados.Add(dtsEstados.Tables[0].Select(string.Format("IdEmpresa = '{0}' ", cboEmpresas.Data)), true, "IdEstado", "NombreEstado_Completo");
            }
            //else
            //{
            //    twEstados.Nodes.Clear();
            //    lwFarmacias.Items.Clear();
            //}

            cboEstados.SelectedIndex = 0;
        }
    }
}
