using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

// Implementacion de hilos 
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;

namespace Almacen.PerfilesAtencion
{
    public partial class FrmPerfilesAtencionClavesSSADist : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente datosCliente;
        clsLeer leer;
        clsLeer reader;
        clsLeer reader2;
        clsConsultas query;
        clsAyudas ayuda;

        DataSet dtsGrupos = new DataSet();
        DataSet dtsDatos = new DataSet();

        TreeNode myNodeSeleccionado;
        int iIndexNodo = 0;
        string sIdClave = "";
        string sClaveDesc = "";
        string sIdGrupo = "";

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        public FrmPerfilesAtencionClavesSSADist()
        {
            InitializeComponent();

            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmPerfilesAtencionClavesSSADist");

            leer = new clsLeer(ref cnn);
            reader = new clsLeer(ref cnn);
            reader2 = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name);
        }

        private void FrmPerfilesAtencionClavesSSADist_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            lwUsuarios.Clear();
            twGrupos.Nodes.Clear();
            txtPerfilAtencion.Focus();
        }
        #endregion Funciones

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }
        #endregion Botones

        #region Eventos
        private void txtPerfilAtencion_Validating(object sender, CancelEventArgs e)
        {
            if (txtPerfilAtencion.Text.Trim() != "")
            {
                leer.DataSetClase = query.PerfilesAtencionDistribuidor(sEmpresa, sEstado, sFarmacia, txtPerfilAtencion.Text, "txtPerfilAtencion_Validating");

                if (leer.Leer())
                {
                    txtPerfilAtencion.Text = leer.Campo("IdPerfilAtencion");
                    lblPerfil.Text = leer.Campo("PerfilDeAtencion");
                    CargarGrupos();                    
                }

            }
        }

        private void txtPerfilAtencion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.PerfilesDeAtencionDistribuidor(sEmpresa, sEstado, sFarmacia, "txtPerfilAtencion_KeyDown");
                if (leer.Leer())
                {
                    txtPerfilAtencion.Text = leer.Campo("IdPerfilAtencion");
                    lblPerfil.Text = leer.Campo("PerfilDeAtencion");
                    CargarGrupos();                   
                }
            }
        }
        #endregion Eventos

        #region CargarDatos
        private void CargarGrupos()
        {
            int iMiembros = 0;
            query.MostrarMsjSiLeerVacio = false;
            dtsGrupos = query.PerfilesAtencionDistribuidor(sEmpresa, sEstado, sFarmacia, txtPerfilAtencion.Text, "CargarGrupos");

            twGrupos.Nodes.Clear();
            twGrupos.BeginUpdate();

            TreeNode myNode;
            twGrupos.Nodes.Clear();
            myNode = twGrupos.Nodes.Add("Perfiles");
            myNode.Tag = "-1";
            myNode.ImageIndex = 1;
            myNode.SelectedImageIndex = 1;

            reader.DataSetClase = dtsGrupos;
            // if (reader.Leer())
            {
                string sIdGrupo = "", sNombreGrupo = "", sMiembro = "";

                ////foreach (DataRow dt in dtsGrupos.Tables[0].Rows)
                if (reader.Leer())
                {
                    if (reader.Campo("Status").ToUpper() == "A")
                    {
                        sIdGrupo = reader.Campo("IdPerfilAtencion");
                        sNombreGrupo = reader.Campo("PerfilDeAtencion");

                        iMiembros = 0;
                        TreeNode myNodeGrupo = myNode.Nodes.Add(sNombreGrupo);
                        myNodeGrupo.ImageIndex = 1;
                        myNodeGrupo.SelectedImageIndex = 1;
                        myNodeGrupo.Tag = "||" + sIdGrupo;

                        reader2.DataSetClase = query.PerfilesAtencion_ClavesSSADistribuidor(sEmpresa, sEstado, sFarmacia, txtPerfilAtencion.Text, "CargarGrupos");
                        while (reader2.Leer())
                        {
                            if (reader2.Campo("StatusClaveSSA").ToUpper() == "A")
                            {
                                sMiembro = reader2.Campo("ClaveSSA") + " - " + reader2.Campo("DescripcionClaveSSA"); //dtU["Farmacia"].ToString();
                                TreeNode myNodeGrupoUsuario = myNodeGrupo.Nodes.Add(sMiembro);
                                myNodeGrupoUsuario.ImageIndex = 0;
                                myNodeGrupoUsuario.SelectedImageIndex = 0;
                                myNodeGrupoUsuario.Tag = "|" + reader2.Campo("ClaveSSA");
                                iMiembros++;
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

        private void CargarClaves(string Criterio)
        {
            Criterio = Criterio.Replace(" ", "%");

            string sSql = string.Format(
                "Select Row_Number() Over (Order By DescripcionClave) as Registro, " +
                " IdClaveSSA_Sal, ClaveSSA, ClaveSSA_Aux, DescripcionClave, 'Descripción clave' = DescripcionClave, StatusClave " +
                " From vw_ClavesSSA_Sales (NoLock) " +
                " Where DescripcionClave like '%{0}%' " +
                " Order By DescripcionClave ", Criterio);

            if (!reader.Exec(sSql))
            {
                Error.GrabarError(reader, "CargarClaves()");
                General.msjError("Ocurrió un error al obtener las Claves del Cliente.");
            }
            else
            {
                if (reader.Leer())
                {
                    dtsDatos = reader.DataSetClase;
                    ListViewItem itmX = null;
                    object NewColListView = null;
                    string strValor = "";

                    lwUsuarios.Columns.Clear();
                    lwUsuarios.Items.Clear();
                    lwUsuarios.View = System.Windows.Forms.View.Details;
                    NewColListView = lwUsuarios.Columns.Add("Núm", 80);
                    NewColListView = lwUsuarios.Columns.Add("Descripción clave", 500);

                    reader.RegistroActual = 1;
                    // foreach (DataRow dt in dtsDatos.Tables[0].Rows)
                    while (reader.Leer())
                    {
                        strValor = reader.CampoInt("Registro").ToString();
                        itmX = lwUsuarios.Items.Add(strValor, 0);

                        strValor = reader.Campo("ClaveSSA") + " - " + reader.Campo("Descripción clave");
                        itmX.SubItems.Add("" + strValor);
                        itmX.SubItems[0].Tag = reader.Campo("ClaveSSA");
                    }
                }
            }
        }
        #endregion CargarDatos

        #region Botones_Menus
        private void bntBuscarClaves_Click(object sender, EventArgs e)
        {
            FrmCriterioDeBusqueda B = new FrmCriterioDeBusqueda();
            string sCriterio = B.MostarCriterio();

            if (B.ExisteCriterio)
            {
                CargarClaves(sCriterio);
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sQuery = "";
            string sDefault = " Set Status = 'C', Actualizado = '0' ";
            bool bCargarGrupos = false;

            try
            {
                if (myNodeSeleccionado.Tag.ToString() != "-1")
                {
                    if (!myNodeSeleccionado.Tag.ToString().Contains("||"))
                    {
                        bCargarGrupos = true;
                        //// Cancelar todas las claves del perfil 
                        sQuery = sQuery + string.Format("Update CFGC_ALMN_DIST_CB_NivelesAtencion_ClavesSSA {4} " + 
                            " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' " +
                            " and IdPerfilAtencion = {3} ", sEmpresa, sEstado, sFarmacia, txtPerfilAtencion.Text, sDefault); 
                    }
                    else
                    {
                        //// Cancelar la clave de perfi seleccionada   
                        sQuery = string.Format("Update CFGC_ALMN_DIST_CB_NivelesAtencion_ClavesSSA {5} " + 
                            "  Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' " +
                            " and IdPerfilAtencion = {3} and ClaveSSA = '{4}' ", 
                            sEmpresa, sEstado, sFarmacia, txtPerfilAtencion.Text, sIdClave, sDefault);
                    }

                    if (reader.Exec(sQuery))
                    {
                        twGrupos.Nodes.Remove(myNodeSeleccionado);
                    }

                    // Recargar los grupos no importa que se hayan cancelado todos sus usuarios 
                    if (bCargarGrupos)
                    {
                        CargarGrupos();
                    }
                }
            }
            catch { }
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CargarGrupos();
        }
        #endregion Botones_Menus

        #region Eventos_TreeView
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
        #endregion Eventos_TreeView

        #region Asignar_Claves
        private void twGrupos_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode NewNode;
            Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
            NewNode = twGrupos.GetNodeAt(pt);
            sIdGrupo = NewNode.Tag.ToString();
            string sClave = lwUsuarios.FocusedItem.SubItems[1].Text;



            if (ValidarClave())
            {
                string sSql = string.Format("Exec spp_Mtto_CFGC_ALMN_DIST_CB_NivelesAtencion_ClavesSSA '{0}', '{1}', '{2}', '{3}', '{4}' ",
                                            sEmpresa, sEstado, sFarmacia, txtPerfilAtencion.Text, sIdClave);


                if (!ExisteOpcion(NewNode, sClave))
                {
                    if (reader.Exec(sSql))
                    {
                        //TreeNode myNodeRama = NewNode.Nodes.Add(sIdClave + " - " + sClaveDesc);
                        TreeNode myNodeRama = NewNode.Nodes.Add(sClave);
                        myNodeRama.ImageIndex = 0;
                        myNodeRama.SelectedImageIndex = 0;
                        myNodeRama.Tag = sIdClave;
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

        private bool ValidarClave()
        {
            bool bRegresa = false;
            string sSql = "";


            sSql = string.Format(" Select * From vw_CFGC_ALMN_DIST_CB_NivelesAtencion_ClavesSSA (NoLock)  " +
                                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and IdPerfilAtencion <> {3}  " +
                                " and ClaveSSA = '{4}' ", sEmpresa, sEstado, sFarmacia, txtPerfilAtencion.Text, sIdClave);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ValidarClave");
                General.msjError("Ocurrió un error al validar la clave");
            }
            else
            {
                if (leer.Leer())
                {
                    General.msjAviso("La clave : " + sIdClave + " ya se encuentra asignada en otro Perfil de Atención.. ");
                }
                else
                {
                    bRegresa = true;
                }
            }

            return bRegresa;
        }

        private void twGrupos_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void lwUsuarios_ItemDrag(object sender, ItemDragEventArgs e)
        {
            sIdClave = lwUsuarios.FocusedItem.SubItems[0].Tag.ToString();
            sClaveDesc = lwUsuarios.FocusedItem.SubItems[1].Text.Substring(6).Trim();
            DoDragDrop(e.Item, DragDropEffects.Move);
        }
        #endregion Asignar_Claves
    }
}
