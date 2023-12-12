using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_ControlsCS;
using DllFarmaciaSoft;

namespace Almacen.Catalogos
{
    public partial class FrmRotacion_Claves : FrmBaseExt
    {
        clsConsultas query;
        DataSet dtsDatos = new DataSet();
        DataSet dtsGrupos = new DataSet(), dtsUsuariosGrupo = new DataSet();
        DataSet dtsFarmacias = new DataSet();

        TreeNode myNodeSeleccionado; //, myNodoUsuario;
        int iIndexNodo = 0;
        string sIdClave = "";
        string sIdGrupo = "";
        string sDescripcion = "";

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        //clsConsultas leer = new clsConsultas(General.DatosConexion, "Configuracion", "GruposDeUsuarios", Application.ProductVersion, true);
        clsLeer leer;


        public FrmRotacion_Claves()
        {
            InitializeComponent();
        }

        private void FrmRotacion_Claves_Load(object sender, EventArgs e)
        {
            query = new clsConsultas(General.DatosConexion, General.DatosApp, "FrmRotacion_Claves"); ;
            leer = new clsLeer(ref cnn);

            CargarGrupos();
            CargarClaves();
            //ActualizaMenu("0");
        }

        private void CargarGrupos()
        {
            string sSql = string.Format(" Select IdRotacion, NombreRotacion, " +
                                " (IdRotacion + ' - ' + NombreRotacion  + (Case When status <> 'A' Then ' (Cancelado)' Else '' End))  As Descripcion " +
                          " From vw_CFGC_ALMN__Rotacion (NoLock) Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' " +
                          " Order By IdRotacion",
                          DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);


            if (leer.Exec(sSql))
                dtsGrupos = leer.DataSetClase;
            //dtsGrupos = query.Grupos(General.EntidadConectada);

            twGrupos.Nodes.Clear();
            twGrupos.BeginUpdate();

            TreeNode myNode;
            twGrupos.Nodes.Clear();
            myNode = twGrupos.Nodes.Add("Grupos");
            myNode.Tag = "-1";
            myNode.ImageIndex = 1;
            myNode.SelectedImageIndex = 1;

            if (leer.Leer())
            {
                string sIdGrupo = "", sNombreGrupo = "", sDescripcion = "";

                foreach (DataRow dt in dtsGrupos.Tables[0].Rows)
                {
                    sIdGrupo = dt["IdRotacion"].ToString();
                    sNombreGrupo = dt["Descripcion"].ToString();

                    TreeNode myNodeGrupo = myNode.Nodes.Add(sNombreGrupo);
                    myNodeGrupo.ImageIndex = 1;
                    myNodeGrupo.SelectedImageIndex = 1;
                    myNodeGrupo.Tag = sIdGrupo;


                    string sQuery = string.Format("Select S.* From CFGC_ALMN__Rotacion_Claves C (NoLock) Inner Join CatClavesSSA_Sales S (NoLock) On (C.IdClaveSSA = S.IdClaveSSA_Sal) " +
                                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And IdRotacion = '{3}' And C.Status = 'A'",
                                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sIdGrupo);

                    leer.Exec(sQuery);

                    if (leer.Leer())
                    {
                        dtsUsuariosGrupo = leer.DataSetClase;
                        foreach (DataRow dtU in dtsUsuariosGrupo.Tables[0].Rows)
                        {
                            sDescripcion = dtU["ClaveSSA"].ToString() + " - " + dtU["Descripcion"].ToString();
                            TreeNode myNodeGrupoUsuario = myNodeGrupo.Nodes.Add(sDescripcion);
                            myNodeGrupoUsuario.ImageIndex = 0;
                            myNodeGrupoUsuario.SelectedImageIndex = 0;
                            myNodeGrupoUsuario.Tag = dtU["IdClaveSSA_Sal"].ToString();

                        }
                    }
                }
            }

            twGrupos.EndUpdate();
            myNode.Expand();

            twGrupos.Nodes[0].Text = twGrupos.Nodes[0].Text;

        }

        private void CargarClaves()
        {
            string sSql = string.Format("Select C.* From CatClavesSSA_Sales C (NoLock) " +
                    "Left Join CFGC_ALMN__Rotacion_Claves R (NoLock) On (C.IdClaveSSA_Sal = R.IdClaveSSA  And R.Status = 'A') " +
                    "Where C.Status = 'A' And R.IdClaveSSA Is NUll And " +
                    "   ClaveSSA in (Select ClaveSSA From FarmaciaProductos F (NoLock) Inner Join vw_Productos P (NoLock) On (F.IdProducto = P.Idproducto)) " +
                    "Order By C.Descripcion");

            if (leer.Exec(sSql))
                dtsDatos = leer.DataSetClase;

            //dtsDatos = query.Usuarios(General.EntidadConectada);

            if (leer.Leer())
            {
                ListViewItem itmX = null;
                object NewColListView = null;
                string strValor = "";

                lwUsuarios.Columns.Clear();
                lwUsuarios.Items.Clear();
                lwUsuarios.View = System.Windows.Forms.View.Details;
                NewColListView = lwUsuarios.Columns.Add("ClavesSSA", 290);

                foreach (DataRow dt in dtsDatos.Tables[0].Rows)
                {
                    strValor = dt["ClaveSSA"].ToString() + " - " + dt["Descripcion"].ToString();
                    itmX = lwUsuarios.Items.Add(strValor, 0);
                    itmX.SubItems.Add("" + strValor);
                    itmX.SubItems[0].Tag = dt["IdClaveSSA_Sal"].ToString();
                }
            }
        }

        private void lwUsuarios_ItemDrag(object sender, ItemDragEventArgs e)
        {
            sIdClave = lwUsuarios.FocusedItem.SubItems[0].Tag.ToString();
            sDescripcion = lwUsuarios.FocusedItem.SubItems[0].Text;
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void twGrupos_AfterSelect(object sender, TreeViewEventArgs e)
        {
            myNodeSeleccionado = e.Node;
            iIndexNodo = e.Node.Index;
            //sIdGrupo = "";

            if (twGrupos.Nodes.Count > 0)
            {
                if (myNodeSeleccionado.Level == 1)
                {
                    sIdGrupo = myNodeSeleccionado.Tag.ToString();
                }
                else if (myNodeSeleccionado.Level == 2)
                {
                    sIdGrupo = myNodeSeleccionado.Parent.Tag.ToString();
                    sIdClave = myNodeSeleccionado.Tag.ToString().Replace("|", "");
                }

            }
        }

        private void twGrupos_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode NewNode; //, NodoPadre;
            //string sIdGrupo = "";
            Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
            NewNode = twGrupos.GetNodeAt(pt);
            sIdGrupo = NewNode.Tag.ToString();

            string sSql = string.Format("Exec spp_CFGC_ALMN__Rotacion_Claves '{0}', '{1}', '{2}', '{3}', '{4}' ",
                    sEmpresa, sEstado, sFarmacia, sIdGrupo, sIdClave);

            if (!ExisteOpcion(NewNode, sIdClave))
            {
                //CargarGrupos();
                if (leer.Exec(sSql))
                {
                    TreeNode myNodeRama = NewNode.Nodes.Add(sDescripcion);
                    myNodeRama.ImageIndex = 0;
                    myNodeRama.SelectedImageIndex = 0;
                    myNodeRama.Tag = sIdClave;
                    // AsignarIcono(myNodeRama, RamaSeleccionada.TipoRama);
                    CargarClaves();
                }
                NewNode.Expand();
            }
        }

        private void twGrupos_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
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

        private void Eliminar_toolStrip_Click(object sender, EventArgs e)
        {
            try
            {
                if (myNodeSeleccionado.Tag.ToString() != "-1")
                {

                    string sQuery = string.Format("Update CFGC_ALMN__Rotacion_Claves Set  Status = 'C', Actualizado = '0' " +
                                "Where IdEmpresa = '{0}' And IdEstado = '{1}' and IdFarmacia = '{2}' and IdRotacion = '{3}' and IdClaveSSA = '{4}' ",
                            DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sIdGrupo, sIdClave);

                    if (leer.Exec(sQuery))
                        twGrupos.Nodes.Remove(myNodeSeleccionado);

                    CargarGrupos();
                    CargarClaves();
                }
            }
            catch { }
        }
    }
}
