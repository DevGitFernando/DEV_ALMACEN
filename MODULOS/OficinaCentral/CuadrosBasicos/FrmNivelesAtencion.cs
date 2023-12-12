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
using DllFarmaciaSoft.Usuarios_y_Permisos;

namespace OficinaCentral.CuadrosBasicos
{
    public partial class FrmNivelesAtencion : FrmBaseExt
    {
        //basGenerales Fg = new basGenerales();
        // clsGuardarSC Guardar = new clsGuardarSC();
        clsConsultas query;
        DataSet dtsDatos = new DataSet();
        DataSet dtsGrupos = new DataSet(), dtsUsuariosGrupo = new DataSet();
        DataSet dtsClientes = new DataSet();

        FrmNivelesAtencion_Grupos myGrupo;
        // DllFarmaciaSoft.Usuarios_y_Permisos.FrmUsuarios myUsuario;
        TreeNode myNodeSeleccionado; //, myNodoUsuario;
        int iIndexNodo = 0;
        string sIdUsuario = "";
        string sLoginUser = "";
        string sIdGrupo = "";

        // clsOperacionesSupervizadas Permisos;
        string sPermisoPerfiles = "MODIFICAR_PERFILES";
        bool bPermisoPerfiles = false;

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsConsultas leer = new clsConsultas(General.DatosConexion, "Configuracion", "GruposDeUsuarios", Application.ProductVersion,true);
        clsLeer reader, reader2;

        public FrmNivelesAtencion()
        {
            InitializeComponent();
        }

        private void FrmGruposUsuarios_Load(object sender, EventArgs e)
        {
            query = new clsConsultas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
            reader = new clsLeer(ref cnn);
            reader2 = new clsLeer(ref cnn);

            SolicitarPermisosUsuario(); 
            CargarEstados();
            ActualizaMenu("0");
        }

        #region Permisos de Usuario 
        private void SolicitarPermisosUsuario()
        {
            ////// Valida si el usuario conectado tiene permiso sobre las opcione especiales 
            ////Permisos = new clsOperacionesSupervizadas(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);
            ////Permisos.Personal = DtGeneral.IdPersonal;
            bPermisoPerfiles = DtGeneral.PermisosEspeciales.TienePermiso(sPermisoPerfiles); 
        }
        #endregion Permisos de Usuario

        #region Funciones 
        private void CargarEstados()
        {
            string sSql = " Select Distinct IdEstado, Estado, ( IdEstado + ' - ' + Estado ) as Descripcion From vw_Farmacias_Urls (NoLock) Order by IdEstado ";
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
                    cboEstados.Add(reader.DataSetClase, true, "IdEstado", "Descripcion");
                }
            }

            //reader.Exec("Select *, (IdCliente + ' - ' + NombreCliente) as NombreCliente From vw_Claves_Precios_Asignados (NoLock) Order By IdEstado, IdCliente ");
            reader.Exec("Select Distinct IdEstado, IdCliente, (IdCliente + ' - ' + NombreCliente) as NombreCliente From vw_Claves_Precios_Asignados (NoLock) Order By IdEstado, IdCliente ");
            dtsClientes = reader.DataSetClase;

            cboEstados.SelectedIndex = 0;
            if (!DtGeneral.EsAdministrador )
            {
                if (!bPermisoPerfiles)
                {
                    cboEstados.Data = DtGeneral.EstadoConectado;
                    cboEstados.Enabled = false;
                }
            } 
        }

        private void CargarClientes()
        {
            cboClientes.Clear();
            cboClientes.Add("0", "<< Seleccione >>");

            if (cboEstados.SelectedIndex != 0)
            {
                try
                {
                    cboClientes.Add(dtsClientes.Tables[0].Select("IdEstado = '" + cboEstados.Data + "'"), true, "IdCliente", "NombreCliente");
                }
                catch { }
            }
            cboClientes.SelectedIndex = 0;
        }

        private void CargarGrupos()
        {
            int iMiembros = 0; 
            query.MostrarMsjSiLeerVacio = false;
            dtsGrupos = query.NivelesAtencion(cboEstados.Data, cboClientes.Data, "CargarGrupos()");

            twGrupos.Nodes.Clear();
            twGrupos.BeginUpdate();

            TreeNode myNode;
            twGrupos.Nodes.Clear();
            myNode = twGrupos.Nodes.Add("Perfiles");
            myNode.Tag = "-1";
            myNode.ImageIndex = 1;
            myNode.SelectedImageIndex = 1;

            reader.DataSetClase = dtsGrupos;
            if (reader.Leer())
            {
                string sIdGrupo = "", sNombreGrupo = "", sMiembro = "";

                foreach (DataRow dt in dtsGrupos.Tables[0].Rows)
                {
                    if (dt["Status"].ToString() == "A")
                    {
                        sIdGrupo = dt["IdNivel"].ToString();
                        sNombreGrupo = dt["Nivel"].ToString();

                        iMiembros = 0;
                        TreeNode myNodeGrupo = myNode.Nodes.Add(sNombreGrupo);
                        myNodeGrupo.ImageIndex = 1;
                        myNodeGrupo.SelectedImageIndex = 1;
                        myNodeGrupo.Tag = sIdGrupo;

                        reader2.DataSetClase = query.NivelesAtencion_Miembros(cboEstados.Data, cboClientes.Data, sIdGrupo, "CargarGrupos()");
                        if (reader2.Leer())
                        {
                            dtsUsuariosGrupo = reader2.DataSetClase;
                            foreach (DataRow dtU in dtsUsuariosGrupo.Tables[0].Rows)
                            {
                                if (dtU["StatusMiembro"].ToString() == "A")
                                {
                                    sMiembro = dtU["IdFarmacia"].ToString() + " - " + dtU["Farmacia"].ToString(); //dtU["Farmacia"].ToString();
                                    TreeNode myNodeGrupoUsuario = myNodeGrupo.Nodes.Add(sMiembro);
                                    myNodeGrupoUsuario.ImageIndex = 0;
                                    myNodeGrupoUsuario.SelectedImageIndex = 0;
                                    myNodeGrupoUsuario.Tag = "|" + dtU["IdFarmacia"].ToString();
                                    iMiembros++; 
                                }
                            }
                        }

                        sNombreGrupo += string.Format("   ( {0} )", iMiembros); 
                        myNodeGrupo.Text = sNombreGrupo; 
                    }
                }
            }

            twGrupos.EndUpdate();
            myNode.Expand();

            twGrupos.Nodes[0].Text = twGrupos.Nodes[0].Text;

        }

        private void CargarFarmacias(string Criterio)
        {
            clsLeer leerFarmacias = new clsLeer();
            string sFiltro = string.Format(" and Farmacia like '%{0}%' ", Criterio);
            string sSql = string.Format(" Select Row_Number() Over (Order By IdFarmacia) as Registro, * " +
                " From vw_Farmacias (NoLock) " +
                " Where IdEstado = '{0}' {1} and Status = 'A' ", cboEstados.Data, sFiltro);

            reader.Exec(sSql); 
            // reader.DataSetClase = leerFarmacias.DataSetClase; 

            if (reader.Leer())
            {
                ListViewItem itmX = null;
                object NewColListView = null;
                string strValor = "";

                lwUsuarios.Columns.Clear();
                lwUsuarios.Items.Clear();
                lwUsuarios.View = System.Windows.Forms.View.Details;
                NewColListView = lwUsuarios.Columns.Add("Núm", 80);
                NewColListView = lwUsuarios.Columns.Add("Farmacia", 500);

                dtsDatos = reader.DataSetClase; 
                foreach (DataRow dt in dtsDatos.Tables[0].Rows)
                {
                    strValor = dt["Registro"].ToString();
                    itmX = lwUsuarios.Items.Add(strValor, 0);

                    strValor = dt["IdFarmacia"].ToString() + " - " + dt["Farmacia"].ToString();
                    itmX.SubItems.Add("" + strValor);
                    itmX.SubItems[0].Tag = dt["IdFarmacia"].ToString();
                }
            }
        }

        #endregion Funciones

        #region Treeview 
        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myGrupo = new FrmNivelesAtencion_Grupos();
            myGrupo.IdEstado = cboEstados.Data;
            myGrupo.IdCliente = cboClientes.Data;

            Fg.CentrarForma(myGrupo);
            myGrupo.ShowDialog();

            if(myGrupo.bAceptar)
            {
                CargarGrupos();
            }
        }

        private void modificarNombreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myGrupo = new FrmNivelesAtencion_Grupos();
            Fg.CentrarForma(myGrupo);

            myGrupo.IdEstado = cboEstados.Data;
            myGrupo.IdCliente = cboClientes.Data;
            myGrupo.IdNivel =  Convert.ToInt32(myNodeSeleccionado.Tag.ToString().Replace("|", ""));
            myGrupo.NombreGrupo = myNodeSeleccionado.Text;
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
                        sQuery = sQuery + string.Format("Update CFG_CB_NivelesAtencion_Miembros {3} Where IdEstado = '{0}' and IdCliente = '{1}' and IdNivel = '{2}' ",
                            cboEstados.Data, cboClientes.Data, sIdGrupo, sDefault);

                        sQuery = sQuery + string.Format("Update CFG_CB_NivelesAtencion {3} Where IdEstado = '{0}' and IdCliente = '{1}' and IdNivel = '{2}' ",
                            cboEstados.Data, cboClientes.Data, sIdGrupo, sDefault);

                    }
                    else
                    {
                        sQuery = string.Format("Update CFG_CB_NivelesAtencion_Miembros {4} Where IdEstado = '{0}' and IdCliente = '{1}' and IdNivel = '{2}' and IdFarmacia = '{3}' ",
                            cboEstados.Data, cboClientes.Data, sIdGrupo, sIdUsuario, sDefault);
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
            CargarFarmacias("");
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnBuscarFarmacias.Visible = false; 
            CargarClientes();
        }

        private void cboSucursales_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboClientes.SelectedIndex != 0)
            {
                CargarGrupos();
                CargarFarmacias("");
                btnBuscarFarmacias.Visible = true; 

                ActualizaMenu("2");
            }
            else
            {
                btnBuscarFarmacias.Visible = false; 
                twGrupos.Nodes.Clear();
                lwUsuarios.Items.Clear();
            }
        }

        private void agregarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }

        private void eliminarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }

        #endregion Treeview
        
        #region Arrastrar usuarios
        private void twGrupos_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode NewNode; //, NodoPadre;
            //string sIdGrupo = "";
            Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
            NewNode = twGrupos.GetNodeAt(pt);
            sIdGrupo = NewNode.Tag.ToString();
            int iOpcion = 1;

            string sSql = string.Format("Exec spp_Mtto_CFG_CB_NivelesAtencion_Miembros '{0}', '{1}', '{2}', '{3}', '{4}' ",
                cboEstados.Data, cboClientes.Data, sIdGrupo, sIdUsuario, iOpcion);

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
            sLoginUser = lwUsuarios.FocusedItem.SubItems[1].Text.Substring(6).Trim();
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        #endregion Arrastrar usuarios

        private void btnBuscarFarmacias_Click(object sender, EventArgs e)
        {
            FrmCriterioDeBusqueda B = new FrmCriterioDeBusqueda();
            string sCriterio = B.MostarCriterio();

            if (B.ExisteCriterio)
            {
                CargarFarmacias(sCriterio);
            }
        }
    }
}
