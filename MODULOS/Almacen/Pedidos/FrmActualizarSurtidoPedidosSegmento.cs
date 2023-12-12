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

namespace Almacen.Pedidos
{
    public partial class FrmActualizarSurtidoPedidosSegmento : FrmBaseExt
    {
        //basGenerales Fg = new basGenerales();
        // clsGuardarSC Guardar = new clsGuardarSC();
        clsConsultasSC query;
        DataSet dtsDatos = new DataSet();
        DataSet dtsGrupos = new DataSet(), dtsUsuariosGrupo = new DataSet();
        DataSet dtsFarmacias = new DataSet();

        DllFarmaciaSoft.Usuarios_y_Permisos.FrmUsuarios myUsuario;
        TreeNode myNodeSeleccionado; //, myNodoUsuario;
        int iIndexNodo = 0;
        string sClaveSSA = "";
        string sSegmento = "";

        string sFolioSurtido = "";

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        //clsConsultas leer = new clsConsultas(General.DatosConexion, "Configuracion", "GruposDeUsuarios", Application.ProductVersion,true);
        clsLeer leer;
        clsResize formResize;

        FrmSegmento Segmento;

        public FrmActualizarSurtidoPedidosSegmento() 
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; 

            //if (Origen == 1)
            //{
                ////formResize = new clsResize(this);
                ////formResize.AjustarFuente = true;
                General.Pantalla.AjustarTamaño(this, 90, 80);


                //FrameUsuarios.Left = FrameGrupos.Left + FrameGrupos.Width + 5;
            //}

            ////ConfigurarInterface(); 
        }

        private void FrmGruposUsuarios_Load(object sender, EventArgs e)
        {
            query = new clsConsultasSC();
            leer = new clsLeer(ref cnn);

            InicializaPantalla(); 

            ActualizaMenu("0");

            //CargarSurtido();
            txtFolioSurtido.Focus();

        }

        private void InicializaPantalla()
        {
            Fg.IniciaControles();

            lwClaves.Columns.Clear();
            lwClaves.Items.Clear();


            twSegmentos.Nodes.Clear(); 
            twSegmentos.BeginUpdate(); 

            TreeNode myNode;
            twSegmentos.Nodes.Clear();
            myNode = twSegmentos.Nodes.Add("Segmentos de surtido");
            myNode.Tag = "-1";
            myNode.ImageIndex = 1;
            myNode.SelectedImageIndex = 1;
            twSegmentos.EndUpdate();
            myNode.Expand();


            txtFolioSurtido.Focus(); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializaPantalla();
        }

        private void CargarSurtido()
        {
            if (txtFolioSurtido.Text.Trim() != "")
            {
                CargaEncabezado();

                CargarPersonalYClaves();
                CargarClaves();
            }
        
        }

        private void CargaEncabezado()
        {
            string sSql = string.Format(
                "Select \n" +
                "\tV.* \n" +
                "From vw_PedidosCedis_Surtimiento V (NoLock) \n" +
                "Where V.IdEmpresa = '{0}' and V.IdEstado = '{1}' and V.IdFarmacia = '{2}' and V.FolioSurtido = '{3}' \n" +
                "Order by V.FechaRegistro \n",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioSurtido);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargaEncabezado()");
                General.msjError("Ocurró un error al cargar el folio de surtido.");
                this.Close();
            }
            else
            {
                if (leer.Leer())
                {
                    txtFolioSurtido.Text = leer.Campo("FolioSurtido");
                    txtFolioSurtido.Enabled = false;
                    lblFarmaciaSurtido.Text = leer.Campo("Farmacia");
                    lblFechaRegistro.Text = General.FechaYMD(leer.CampoFecha("FechaRegistro"));
                    lblFolioPedido.Text = leer.Campo("FolioPedido");
                    lblFarmaciaPedido.Text = leer.Campo("FarmaciaSolicita");
                    lblFechaPedido.Text = General.FechaYMD(leer.CampoFecha("FechaRegistro"));
                    lblStatusSurtimiento.Text = leer.Campo("StatusPedido");
                }
            }
        }

        //public void CargarPedido(string FolioSurtido)
        //{
        //    sFolioSurtido = FolioSurtido;

        //    this.ShowDialog();
        //}

        private void CargarPersonalYClaves()
        {
            string sSql = string.Format("exec spp_Pedidos_Cedis_Enc_Surtido_Segmentos @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioSurtido = '{3}', @IdPersonal = '{4}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioSurtido, DtGeneral.IdPersonal);

            if (leer.Exec(sSql))
            {
                dtsGrupos = leer.DataSetClase;
            }


            //dtsGrupos = query.Grupos(General.EntidadConectada);

            twSegmentos.Nodes.Clear();
            twSegmentos.BeginUpdate();

            TreeNode myNode;
            twSegmentos.Nodes.Clear();
            myNode = twSegmentos.Nodes.Add("Segmentos de surtido");
            myNode.Tag = "-1";
            myNode.ImageIndex = 1;
            myNode.SelectedImageIndex = 1;

            if (leer.Leer())
            {
                string sIdGrupo = "", sNombreGrupo = "", sUsuario = "";

                foreach (DataRow dt in dtsGrupos.Tables[0].Rows)
                {
                    sIdGrupo = dt["Segmento"].ToString();
                    sNombreGrupo = dt["Segmento"].ToString() + " | " + dt["IdPersonal_Segmento"].ToString() + " | " + dt["NombreCompleto"].ToString();

                    TreeNode myNodeGrupo = myNode.Nodes.Add(sNombreGrupo);
                    myNodeGrupo.ImageIndex = 1;
                    myNodeGrupo.SelectedImageIndex = 1;
                    myNodeGrupo.Tag = sIdGrupo;


                    string sQuery = string.Format(
                        "Select \n" +
                        "\tD.IdEmpresa, D.IdEstado, D.IdFarmacia, D.FolioSurtido, D.ClaveSSA, S.DescripcionCortaClave, IIF(Sum(D.CantidadAsignada) > 0, '(Tiene cantidades asignadas)', '') As TieneCantidadAsignada, D.Segmento \n" +
                        "From Pedidos_Cedis_Det_Surtido_Distribucion D (NoLock) \n" +
                        "Inner Join vw_ClavesSSA_Sales S (NoLock)On(D.ClaveSSA = S.ClaveSSA) \n" +
                        "Where  D.IdEmpresa = '{0}' And D.IdEstado = '{1}' And D.IdFarmacia = '{2}' And D.FolioSurtido = '{3}' And Segmento = '{4}' And CantidadAsignada = 0 \n" +
                        //"Group By D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.FolioSurtido, D.ClaveSSA, S.DescripcionCortaClave, D.Segmento, D.IdPersonal_Segmento " +
                        "Group By D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.FolioSurtido, D.ClaveSSA, S.DescripcionCortaClave, D.Segmento \n" +
                        "Order By D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.FolioSurtido, D.ClaveSSA, S.DescripcionCortaClave, D.Segmento \n",
                        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioSurtido, sIdGrupo);

                    //dtsUsuariosGrupo = query.GruposUsuarios(General.EntidadConectada, sIdGrupo);
                    leer.Exec(sQuery);

                    if (leer.Leer())
                    {
                        dtsUsuariosGrupo = leer.DataSetClase;
                        foreach (DataRow dtU in dtsUsuariosGrupo.Tables[0].Rows)
                        {
                            sUsuario = dtU["ClaveSSA"].ToString() + " -- " + dtU["DescripcionCortaClave"].ToString() + " " + dtU["TieneCantidadAsignada"].ToString();
                            TreeNode myNodeGrupoUsuario = myNodeGrupo.Nodes.Add(sUsuario);
                            myNodeGrupoUsuario.ImageIndex = 0;
                            myNodeGrupoUsuario.SelectedImageIndex = 0;
                            myNodeGrupoUsuario.Tag = dtU["ClaveSSA"].ToString();
 
                        }
                    }
                }
            }

            twSegmentos.EndUpdate();
            myNode.Expand();

            twSegmentos.Nodes[0].Text = twSegmentos.Nodes[0].Text;

        }

        private void CargarClaves()
        {
            lwClaves.Columns.Clear();
            lwClaves.Items.Clear();

            string sQuery = string.Format(
                "Select \n" +
                "\tD.IdEmpresa, D.IdEstado, D.IdFarmacia, D.FolioSurtido, D.ClaveSSA, S.DescripcionCortaClave, IIF(Sum(D.CantidadAsignada) > 0, '(Tiene cantidades asignadas)', '') As TieneCantidadAsignada, D.Segmento \n" +
                "From Pedidos_Cedis_Det_Surtido_Distribucion D (NoLock) \n" +
                "Inner Join vw_ClavesSSA_Sales S (NoLock)On(D.ClaveSSA = S.ClaveSSA) \n" +
                "Where  D.IdEmpresa = '{0}' And D.IdEstado = '{1}' And D.IdFarmacia = '{2}' And D.FolioSurtido = '{3}' And Segmento = '{4}' And CantidadAsignada = 0 \n" +
                //"Group By D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.FolioSurtido, D.ClaveSSA, S.DescripcionCortaClave, D.Segmento, D.IdPersonal_Segmento " +
                "Group By D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.FolioSurtido, D.ClaveSSA, S.DescripcionCortaClave, D.Segmento \n" +
                "Order By D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.FolioSurtido, D.ClaveSSA, S.DescripcionCortaClave, D.Segmento \n",
            DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioSurtido, 0);

            if (leer.Exec(sQuery))
            {
                dtsDatos = leer.DataSetClase;
            }

            //dtsDatos = query.Usuarios(General.EntidadConectada);

            if (leer.Leer())
            {
                ListViewItem itmX = null;
                object NewColListView = null;
                string strValor = "";


                lwClaves.View = System.Windows.Forms.View.Details;
                NewColListView = lwClaves.Columns.Add("Claves", lwClaves.Width - 30);

                foreach (DataRow dt in dtsDatos.Tables[0].Rows)
                {
                    //strValor = dt["IdPersonal"].ToString() + " - " + dt["LoginUser"].ToString();
                    strValor = dt["ClaveSSA"].ToString() + " -- " + dt["DescripcionCortaClave"].ToString() + " " + dt["TieneCantidadAsignada"].ToString();
                    itmX = lwClaves.Items.Add(strValor, 0);
                    itmX.SubItems.Add("" + strValor);
                    itmX.SubItems[0].Tag = dt["ClaveSSA"].ToString();
                }
            }
        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Segmento = new FrmSegmento();
            Segmento.sFolioSurtido = sFolioSurtido;

            Fg.CentrarForma(Segmento);
            Segmento.ShowDialog();

            if (Segmento.bAceptar)
            {
                CargarPersonalYClaves();
            }
        }

        //private void modificarNombreToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    //myGrupo = new FrmGrupos();
        //    //Fg.CentrarForma(myGrupo);

        //    //myGrupo.IdEstado = cboEstados.Data;
        //    //myGrupo.IdFarmacia = cboSucursales.Data;
        //    //myGrupo.iIdGrupo = Convert.ToInt32(myNodeSeleccionado.Tag.ToString().Replace("|", ""));
        //    //myGrupo.sNombreGrupo = myNodeSeleccionado.Text;
        //    //myGrupo.ShowDialog();

        //    CargarPersonalYClaves();
        //}

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sQuery = ""; 
            string sDefault = " Set Segmento = 0 "; 

            try
            {
                if (myNodeSeleccionado.Tag.ToString() != "-1")
                {

                    sQuery = sQuery + string.Format("Update D {5} From Pedidos_Cedis_Det_Surtido_Distribucion D " +
                        "Where  D.IdEmpresa = '{0}' And D.IdEstado = '{1}' And D.IdFarmacia = '{2}' And D.FolioSurtido = '{3}' And D.ClaveSSA = '{4}' ",
                        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioSurtido, sClaveSSA, sDefault);

                    leer.Exec(sQuery);


                    // Recargar los grupos no importa que se hayan cancelado todos sus usuarios 
                    CargarPersonalYClaves();
                    CargarClaves();
                }
            }
            catch { }
        }

        private void twGrupos_AfterSelect(object sender, TreeViewEventArgs e)
        {
            myNodeSeleccionado = e.Node;
            iIndexNodo = e.Node.Index;
            //sIdGrupo = "";

            if (twSegmentos.Nodes.Count > 0)
            {
                ActualizaMenu(e.Node.Tag.ToString());

                if (myNodeSeleccionado.Level > 1)
                {
                    if (myNodeSeleccionado.Parent.Tag.ToString() != "-1")
                    {
                        sClaveSSA = myNodeSeleccionado.Tag.ToString();
                        sSegmento = myNodeSeleccionado.Parent.Tag.ToString();
                    }
                }
                //else
                //{
                //    sClaveSSA = myNodeSeleccionado.Parent.Tag.ToString();
                //    //sIdUsuario = myNodeSeleccionado.Tag.ToString().Replace("|", "");
                //}

            }
            // MessageBox.Show(e.Node.FullPath.ToString());
        }

        private void ActualizaMenu(string Tag)
        {
            mnGrupos.Items[agregarToolStripMenuItem.Name].Enabled = true;
            //mnGrupos.Items[eliminarToolStripMenuItem.Name].Enabled = false;
            //mnGrupos.Items[modificarNombreToolStripMenuItem.Name].Enabled = false;
            mnGrupos.Items[actualizarToolStripMenuItem.Name].Enabled = true;

            if (Tag == "1")
            {
                mnGrupos.Items[agregarToolStripMenuItem.Name].Enabled = true;
                //mnGrupos.Items[eliminarToolStripMenuItem.Name].Enabled = false;
                //mnGrupos.Items[modificarNombreToolStripMenuItem.Name].Enabled = false;
                mnGrupos.Items[actualizarToolStripMenuItem.Name].Enabled = true;
            }
            //else
            //{
            //    if (Tag.Substring(0, 1) == "|")
            //    {
            //        mnGrupos.Items[agregarToolStripMenuItem.Name].Enabled = false;
            //        mnGrupos.Items[eliminarToolStripMenuItem.Name].Enabled = true;
            //        mnGrupos.Items[modificarNombreToolStripMenuItem.Name].Enabled = true;
            //    }
            //}
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CargarPersonalYClaves();
        }


        #region Arrastrar usuarios 
        private void twGrupos_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode NewNode; //, NodoPadre;
            string sSegmento_Nuevo = "";
            //string sIdPersonal_Nuevo = "";
            //string sIdGrupo = "";

            try
            {
                Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
                NewNode = twSegmentos.GetNodeAt(pt);
                //sSegmento = NewNode.Tag.ToString();
                sSegmento_Nuevo = NewNode.Tag.ToString();
                //sIdPersonal_Nuevo = NewNode.Text.ToString().Substring(3, 5);


                string sSql = string.Format("Update D Set Segmento = {5} From Pedidos_Cedis_Det_Surtido_Distribucion D (NoLock) " +
                        "Where D.IdEmpresa = '{0}' And D.IdEstado = '{1}' And D.IdFarmacia = '{2}' And D.FolioSurtido = '{3}' And D.ClaveSSA = '{4}' ",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioSurtido, sClaveSSA, sSegmento_Nuevo);

                if (!ExisteOpcion(NewNode, sClaveSSA))
                {
                    //CargarGrupos();
                    if (leer.Exec(sSql))
                    {
                        //TreeNode myNodeRama = NewNode.Nodes.Add(sClaveSSA + " - ");
                        //myNodeRama.ImageIndex = 0;
                        //myNodeRama.SelectedImageIndex = 0;
                        //myNodeRama.Tag = sClaveSSA;
                        // AsignarIcono(myNodeRama, RamaSeleccionada.TipoRama);

                        CargarPersonalYClaves();
                        CargarClaves();
                    }
                    NewNode.Expand();
                }
            }
            catch (Exception Ex)
            { }
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

        private void lwClaves_ItemDrag(object sender, ItemDragEventArgs e)
        {
            sClaveSSA = lwClaves.FocusedItem.SubItems[0].Tag.ToString();
            //sLoginUser = lwClaves.FocusedItem.SubItems[0].Text.Substring(6).Trim();
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void txtFolioSurtido_Validating(object sender, CancelEventArgs e)
        {
            if (txtFolioSurtido.Text.Trim() != "")
            {
                sFolioSurtido = txtFolioSurtido.Text.Trim();

                sFolioSurtido = Fg.PonCeros(sFolioSurtido, 8);


                CargarSurtido();
            }
           
        }

        private void twGrupos_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }


        #endregion Arrastrar usuarios

    }
}
