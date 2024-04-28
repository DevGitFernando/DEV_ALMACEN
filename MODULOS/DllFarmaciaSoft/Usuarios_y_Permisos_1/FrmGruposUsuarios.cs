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

namespace DllFarmaciaSoft.Usuarios_y_Permisos
{
    public partial class FrmGruposUsuarios : FrmBaseExt
    {
        //basGenerales Fg = new basGenerales();
        // clsGuardarSC Guardar = new clsGuardarSC();
        clsConsultasSC query;
        DataSet dtsDatos = new DataSet();
        DataSet dtsGrupos = new DataSet(), dtsUsuariosGrupo = new DataSet();
        DataSet dtsFarmacias = new DataSet();

        FrmGrupos myGrupo;
        DllFarmaciaSoft.Usuarios_y_Permisos.FrmUsuarios myUsuario;
        TreeNode myNodeSeleccionado; //, myNodoUsuario;
        int iIndexNodo = 0;
        string sIdUsuario = "";
        string sLoginUser = "";
        string sIdGrupo = "";

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsConsultas leer = new clsConsultas(General.DatosConexion, "Configuracion", "GruposDeUsuarios", Application.ProductVersion,true);
        clsLeer reader;
        clsResize formResize;

        public FrmGruposUsuarios():this(1) 
        {
        }

        public FrmGruposUsuarios(int Origen) 
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; 

            if (Origen == 1)
            {
                ////formResize = new clsResize(this);
                ////formResize.AjustarFuente = true;
                General.Pantalla.AjustarTamaño(this, 90, 80);


                //FrameUsuarios.Left = FrameGrupos.Left + FrameGrupos.Width + 5;
            }

            ////ConfigurarInterface(); 
        }

        private void ConfigurarInterface()
        {
            ////int iAncho_Base = 0;
            ////int iAlto_Base = 0;
            ////int iAncho_Final = 0;
            ////int iAlto_Final = 0;
            ////int iAnchoPantalla = 0;
            ////int iAltoPantalla = 0;
            ////double dPorcentaje = 0;
            ////int iDesplazamiento = 0;


            ////FrameGrupos.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;

            ////iAnchoPantalla = this.Width;
            ////iAltoPantalla = this.Height; 

            ////iAlto_Base = FrameGrupos.Height;
            ////iAncho_Base = FrameGrupos.Width;

            ////General.Pantalla.AjustarTamaño(this, 90, 80);

            ////dPorcentaje = (this.Width / (double)iAnchoPantalla);


            ////iAlto_Final = FrameGrupos.Height;
            ////iAncho_Final = FrameGrupos.Width;

            ////iAncho_Base = cboEstados.Width;
            ////cboEstados.Width = (int)((double)cboEstados.Width * dPorcentaje);
            ////cboSucursales.Width = (int)((double)cboSucursales.Width * dPorcentaje);
            ////FrameGrupos.Width = (int)((double)FrameGrupos.Width * dPorcentaje);

            ////iDesplazamiento = (cboEstados.Width - iAncho_Base) + 70;
            ////lblFarmacia.Left += iDesplazamiento;
            ////cboSucursales.Left += iDesplazamiento; 


            ////FrameUsuarios.Width = FrameGrupos.Width; 
            ////FrameUsuarios.Height = FrameGrupos.Height;
            ////FrameUsuarios.Left = FrameGrupos.Width + 10;
            ////FrameEstadoFarmacia.Width = (FrameGrupos.Width * 2) + 2;

            ////this.Width = (FrameGrupos.Width * 2) + 20;

        }

        private void FrmGruposUsuarios_Load(object sender, EventArgs e)
        {
            query = new clsConsultasSC();
            reader = new clsLeer(ref cnn);

            CargarEstados();
            ActualizaMenu("0");

            mnUsuarios.Enabled = false;


            //CargarUsuarios();
            //CargarGrupos();
        }

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

            reader.Exec("Select *, (IdFarmacia + ' - ' + NombreFarmacia) as NombreDeFarmacia From CatFarmacias (NoLock) Order By IdEstado, IdFarmacia ");
            dtsFarmacias = reader.DataSetClase;

            cboEstados.SelectedIndex = 0;
        }

        private void CargarFarmacias()
        {
            cboSucursales.Clear();
            cboSucursales.Add("0", "<< Seleccione >>");

            if (cboEstados.SelectedIndex != 0)
            {
                try
                {
                    cboSucursales.Add(dtsFarmacias.Tables[0].Select("IdEstado = '" + cboEstados.Data + "'"), true, "IdFarmacia", "NombreDeFarmacia");
                }
                catch { }
            }
            cboSucursales.SelectedIndex = 0;
        }

        private void CargarGrupos()
        {
            string sSql = string.Format("Select * From Net_Grupos_De_Usuarios (noLock) Where IdEstado = '{0}' and IdSucursal = '{1}' ", cboEstados.Data, cboSucursales.Data);

            if (reader.Exec(sSql))
                dtsGrupos = reader.DataSetClase;
            //dtsGrupos = query.Grupos(General.EntidadConectada);

            twGrupos.Nodes.Clear();
            twGrupos.BeginUpdate();

            TreeNode myNode;
            twGrupos.Nodes.Clear();
            myNode = twGrupos.Nodes.Add("Grupos");
            myNode.Tag = "-1";
            myNode.ImageIndex = 1;
            myNode.SelectedImageIndex = 1;

            if (reader.Leer())
            {
                string sIdGrupo = "", sNombreGrupo = "", sUsuario = "";

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
                        " Where G.IdEstado = '" + cboEstados.Data + "' and G.IdSucursal = '" + cboSucursales.Data + "' " +
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
                            myNodeGrupoUsuario.Tag = "|" + dtU["IdPersonal"].ToString();
 
                        }
                    }
                }
            }

            twGrupos.EndUpdate();
            myNode.Expand();

            twGrupos.Nodes[0].Text = twGrupos.Nodes[0].Text;

        }

        private void CargarUsuarios()
        {
            string sSql = string.Format("Select * From Net_Usuarios (noLock) Where IdEstado = '{0}' and IdSucursal = '{1}' and Status = 'A' ", cboEstados.Data, cboSucursales.Data);

            if (reader.Exec(sSql))
                dtsDatos = reader.DataSetClase;

            //dtsDatos = query.Usuarios(General.EntidadConectada);

            if (reader.Leer())
            {
                ListViewItem itmX = null;
                object NewColListView = null;
                string strValor = "";

                lwUsuarios.Columns.Clear();
                lwUsuarios.Items.Clear();
                lwUsuarios.View = System.Windows.Forms.View.Details;
                NewColListView = lwUsuarios.Columns.Add("Usuario", 290);

                foreach (DataRow dt in dtsDatos.Tables[0].Rows)
                {
                    strValor = dt["IdPersonal"].ToString() + " - " + dt["LoginUser"].ToString();
                    itmX = lwUsuarios.Items.Add(strValor, 0);
                    itmX.SubItems.Add("" + strValor);
                    itmX.SubItems[0].Tag = dt["IdPersonal"].ToString();
                }
            }
        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myGrupo = new FrmGrupos();
            myGrupo.IdEstado = cboEstados.Data;
            myGrupo.IdFarmacia = cboSucursales.Data;

            Fg.CentrarForma(myGrupo);
            myGrupo.ShowDialog();

            if (myGrupo.bAceptar)
                CargarGrupos();
        }

        private void modificarNombreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myGrupo = new FrmGrupos();
            Fg.CentrarForma(myGrupo);

            myGrupo.IdEstado = cboEstados.Data;
            myGrupo.IdFarmacia = cboSucursales.Data;
            myGrupo.iIdGrupo = Convert.ToInt32(myNodeSeleccionado.Tag.ToString().Replace("|", ""));
            myGrupo.sNombreGrupo = myNodeSeleccionado.Text;
            myGrupo.ShowDialog();

            CargarGrupos();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sQuery = ""; 
            string sDefault = " Set FechaUpdate = getdate(), Status = 'C', Actualizado = '0' "; 

            try
            {
                if (myNodeSeleccionado.Tag.ToString() != "-1")
                {
                    if (!myNodeSeleccionado.Tag.ToString().Contains("|"))
                    {
                        //sQuery = string.Format("Delete From Net_Grupos_De_Usuarios Where IdEstado = '{0}' and IdSucursal = '{1}' and IdGrupo = '{2}' ", 
                        //    cboEstados.Data, cboSucursales.Data, sIdGrupo);

                        //sQuery = sQuery + string.Format("Delete From Net_Grupos_Usuarios_Miembros Where IdEstado = '{0}' and IdSucursal = '{1}' and IdGrupo = '{2}' ",
                        //    cboEstados.Data, cboSucursales.Data, sIdGrupo);

                        //sQuery = sQuery + string.Format("Delete From Net_Privilegios_Grupo Where IdEstado = '{0}' and IdSucursal = '{1}' and IdGrupo = '{2}' ",
                        //    cboEstados.Data, cboSucursales.Data, sIdGrupo);



                        //sQuery = string.Format("Update Net_Grupos_De_Usuarios {3} Where IdEstado = '{0}' and IdSucursal = '{1}' and IdGrupo = '{2}' ",
                        //    cboEstados.Data, cboSucursales.Data, sIdGrupo, sDefault);

                        sQuery = sQuery + string.Format("Update Net_Grupos_Usuarios_Miembros {3} Where IdEstado = '{0}' and IdSucursal = '{1}' and IdGrupo = '{2}' ",
                            cboEstados.Data, cboSucursales.Data, sIdGrupo, sDefault);

                        sQuery = sQuery + string.Format("Update Net_Privilegios_Grupo {3} Where IdEstado = '{0}' and IdSucursal = '{1}' and IdGrupo = '{2}' ",
                            cboEstados.Data, cboSucursales.Data, sIdGrupo, sDefault);

                    }
                    else
                    {
                        sQuery = string.Format("Update Net_Grupos_Usuarios_Miembros {4} Where IdEstado = '{0}' and IdSucursal = '{1}' and IdGrupo = '{2}' and IdPersonal = '{3}' ",
                            cboEstados.Data, cboSucursales.Data, sIdGrupo, sIdUsuario, sDefault);
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
            //sIdGrupo = "";

            if (twGrupos.Nodes.Count > 0)
            {
                ActualizaMenu(e.Node.Tag.ToString());

                if (!myNodeSeleccionado.Tag.ToString().Contains("|"))
                    sIdGrupo = myNodeSeleccionado.Tag.ToString();
                else
                {
                    sIdGrupo = myNodeSeleccionado.Parent.Tag.ToString();
                    sIdUsuario = myNodeSeleccionado.Tag.ToString().Replace("|", "");
                }

            }
            // MessageBox.Show(e.Node.FullPath.ToString());
        }

        private void ActualizaMenu(string Tag)
        {
            mnGrupos.Items[agregarToolStripMenuItem.Name].Enabled = true;
            mnGrupos.Items[eliminarToolStripMenuItem.Name].Enabled = true;
            mnGrupos.Items[modificarNombreToolStripMenuItem.Name].Enabled = true;
            mnGrupos.Items[actualizarToolStripMenuItem.Name].Enabled = true;            

            if (Tag == "0")
            {
                mnGrupos.Items[agregarToolStripMenuItem.Name].Enabled = false;
                mnGrupos.Items[eliminarToolStripMenuItem.Name].Enabled = false;
                mnGrupos.Items[modificarNombreToolStripMenuItem.Name].Enabled = false;
                mnGrupos.Items[actualizarToolStripMenuItem.Name].Enabled = false;    
            }
            else
            {
                if (Tag == "-1")
                {
                    mnGrupos.Items[agregarToolStripMenuItem.Name].Enabled = true;
                    mnGrupos.Items[eliminarToolStripMenuItem.Name].Enabled = false;
                    //mnGrupos.Items[1].Enabled = false;
                    //mnGrupos.Items[2].Enabled = false;
                }
                else
                {
                    if (Tag.Substring(0, 1) != "|")
                    {
                        mnGrupos.Items[agregarToolStripMenuItem.Name].Enabled = false;
                        mnGrupos.Items[eliminarToolStripMenuItem.Name].Enabled = true;
                        mnGrupos.Items[modificarNombreToolStripMenuItem.Name].Enabled = true;
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
            CargarUsuarios();
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarFarmacias();
        }

        private void cboSucursales_SelectedIndexChanged(object sender, EventArgs e)
        {
            mnUsuarios.Enabled = false;
            if (cboSucursales.SelectedIndex != 0)
            {
                CargarGrupos();
                CargarUsuarios();
                ActualizaMenu("2");
                mnUsuarios.Enabled = true;
            }
            else
            {
                twGrupos.Nodes.Clear();
                lwUsuarios.Items.Clear();
            }
        }

        private void agregarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CargarRegistroUsuarios("");
            //if (myUsuario.bAceptar)
            //    CargarGrupos();

        }

        private void CargarRegistroUsuarios(string IdPersonal)
        {
            myUsuario = new DllFarmaciaSoft.Usuarios_y_Permisos.FrmUsuarios();
            myUsuario.IdEstado = cboEstados.Data;
            myUsuario.IdFarmacia = cboSucursales.Data;
            myUsuario.Estado = cboEstados.Text;
            myUsuario.Farmacia = cboSucursales.Text;

            if ( IdPersonal != "" )
                myUsuario.IdPersonal = IdPersonal.Substring(0,4);

            Fg.CentrarForma(myUsuario);
            myUsuario.ShowDialog();

            CargarUsuarios();
        }

        private void eliminarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CargarRegistroUsuarios(lwUsuarios.FocusedItem.SubItems[0].Text.ToString());
        }

        #region Arrastrar usuarios 
        private void twGrupos_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode NewNode; //, NodoPadre;
            //string sIdGrupo = "";
            Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
            NewNode = twGrupos.GetNodeAt(pt);
            sIdGrupo = NewNode.Tag.ToString();

            string sSql = string.Format("Exec spp_Net_Grupos_Usuarios_Miembros '{0}', '{1}', '{2}', '{3}', '{4}' ", cboEstados.Data, cboSucursales.Data, sIdGrupo, sIdUsuario, sLoginUser);

            if (!ExisteOpcion(NewNode, sIdUsuario + " - " + sLoginUser))
            {
                //CargarGrupos();
                if (reader.Exec(sSql)) 
                {
                    TreeNode myNodeRama = NewNode.Nodes.Add(sIdUsuario + " - " + sLoginUser);
                    myNodeRama.ImageIndex = 0;
                    myNodeRama.SelectedImageIndex = 0;
                    myNodeRama.Tag = sIdUsuario;
                    // AsignarIcono(myNodeRama, RamaSeleccionada.TipoRama);
                }
                NewNode.Expand();
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
            sIdUsuario = lwUsuarios.FocusedItem.SubItems[0].Tag.ToString();
            sLoginUser = lwUsuarios.FocusedItem.SubItems[0].Text.Substring(6).Trim();
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        #endregion Arrastrar usuarios

        private void lwUsuarios_DragEnter(object sender, DragEventArgs e)
        {
            //sIdUsuario = lwUsuarios.FocusedItem.SubItems[0].Tag.ToString();
        }

        private void generarPasswordsStandarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmUsuariosPasswordsStandard f = new FrmUsuariosPasswordsStandard();
            f.ShowDialog(); 
        }
    }
}
