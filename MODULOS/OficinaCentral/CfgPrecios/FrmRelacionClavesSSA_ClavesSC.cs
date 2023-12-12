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

namespace OficinaCentral.CfgPrecios
{
    public partial class FrmRelacionClavesSSA_ClavesSC : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer, leer2;

        clsConsultas query;
        clsAyudas ayuda; 

        TreeNode myNodeSeleccionado; //, myNodoUsuario;
        // DataSet dtsEstados;
        // DataSet dtsFarmacias;
        DataSet dtsDatos;
        string sIdFarmacia = "", sNombreFarmacia = "";  // , sUsuario = "";
        string sIdCliente = "", sNombreCliente = "";
        string sIdClaveSSA = "";
        int iMultiplo = 0;
        bool bAfectarVenta = false;
        bool bAfectarConsigna = false;

        clsNodo_CuadroBasico nodoItem = new clsNodo_CuadroBasico();
        clsNodo_CuadroBasico nodoClave = new clsNodo_CuadroBasico();

        public FrmRelacionClavesSSA_ClavesSC()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            leer2 = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name, false);
            ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name, false); 
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.DatosApp, this.Name);

            CargarEstados(); 
            CargarGruposTerapeuticos(); 
        }

        private void FrmEstadosClientes_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();   
            // CargarClavesSSA_Relacionadas();
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void btnNuevo_Clave_Click( object sender, EventArgs e )
        {
            txtClaveSSA.Enabled = true;
            txtClaveSSA.Text = "";
            txtClaveSSA.Focus(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones

        private void FrmRelacionClavesSSA_ClavesSC_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    // LimpiarPantalla(); 
                    break;

                default:
                    break; 
            }
        }

        private void LimpiarPantalla()
        {
            Fg.IniciaControles();
            HabilitarCaptura(false);

            twClientes.Nodes.Clear();
            lwSales.Items.Clear(); 

            cboGrupos.SelectedIndex = 0;
            txtClaveSSA.Enabled = false;
            txtClaveSSA.Enabled = false;

            btnExportarExcel.Enabled = false;

            cboEstados.Enabled = true;
            cboEstados.Focus();


        }

        #region Combos 
        private void CargarEstados()
        {

            cboCliente.Clear();
            cboCliente.Add("0", "<< Seleccione >>");
            cboCliente.SelectedIndex = 0;

            cboSubCliente.Clear();
            cboSubCliente.Add("0", "<< Seleccione >>");
            cboSubCliente.SelectedIndex = 0;


            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");

            if (!leer.Exec(" Select distinct IdEstado, Estado as Nombre, (IdEstado + ' - ' + Estado) as NombreEstado From vw_Farmacias (NoLock) Order by IdEstado "))
            {
                Error.GrabarError(leer, "CargarEstados()");
                General.msjError("Ocurrió un error al cargar la lista de estados."); 
            }
            else
            {
                cboEstados.Add(leer.DataSetClase, true, "IdEstado", "NombreEstado");
            }

            cboEstados.SelectedIndex = 0;
        }

        private void CargarGruposTerapeuticos()
        {
            cboGrupos.Clear();
            cboGrupos.Add("0", "<< TODOS >>");
            cboGrupos.Add(query.GruposTerapeuticos("CargarGruposTerapeuticos()"), true, "IdGrupoTerapeutico", "DescripcionGrupo");
            cboGrupos.SelectedIndex = 0;
        }

        private void LlenaCliente()
        {
            leer.DataSetClase =  query.ComboCliente("LlenaCliente");
            if (leer.Leer())
            {
                LlenaComboCliente();
            }
        }

        private void LlenaComboCliente()
        {
            //Se hace de esta manera para la ayuda.
            cboCliente.Enabled = true; 
            cboCliente.Clear();
            cboCliente.Add("0", "<< Seleccione >>");
            cboCliente.Add(leer.DataSetClase, true);
            cboCliente.SelectedIndex = 0;

            cboSubCliente.Clear();
            cboSubCliente.Add("0", "<< Seleccione >>");
            cboSubCliente.SelectedIndex = 0;
        }

        private void LlenaComboSubCliente()
        {
            //Se hace de esta manera para la ayuda.
            cboSubCliente.Enabled = true; 
            cboSubCliente.Clear();
            cboSubCliente.Add("0", "<< Seleccione >>");
            cboSubCliente.Add(leer.DataSetClase, true);
            cboSubCliente.SelectedIndex = 0;

            lblClaveSSA.Text = "";
            lblDescripcion.Text = "";
        }   
        #endregion Combos 

        private void CargarClavesSSA_Relacionadas()
        {
            //string sSql = string.Format("Select * From CatEstados (noLock) Where Status = 'A' ");

            //if (leer.Exec(sSql))
            //    dtsEstados = leer.DataSetClase;
            //dtsGrupos = query.Grupos(General.EntidadConectada);

            string sClaveSSA = "";
            int iPic = 2;
            leer.DataSetClase = query.ClavesSSA_Asignadas_A_ClaveSSA(cboEstados.Data, cboCliente.Data, cboSubCliente.Data, txtClaveSSA.Text, "CargarClavesSSA_Relacionadas()"); 
            twClientes.Nodes.Clear();
            twClientes.BeginUpdate();

            clsNodo_CuadroBasico nodo = new clsNodo_CuadroBasico();

            TreeNode myNode = new TreeNode();
            twClientes.Nodes.Clear();

            //if (cboGrupos.SelectedIndex != 0)
            {
                nodo.Separador = "-1";
                nodo.IdCliente = cboCliente.Data;
                nodo.IdSubCliente = cboSubCliente.Data;
                nodo.IdClaveSSA = lblClaveSSA.Text; 

                sClaveSSA = txtClaveSSA.Text + " - " + lblClaveSSA.Text + " - " + lblDescripcion.Text;
                sClaveSSA = txtClaveSSA.Text + " - " + lblDescripcion.Text;
                twClientes.Nodes.Clear();
                myNode = twClientes.Nodes.Add(sClaveSSA);
                myNode.Tag = "-1";
                myNode.Tag = nodo; 
                myNode.ImageIndex = 0;
                myNode.SelectedImageIndex = 0;
            }

            while (leer.Leer())
            {
                sIdFarmacia = leer.Campo("IdCliente");
                sNombreFarmacia = leer.Campo("Nombre");
                sClaveSSA = leer.Campo("ClaveSSA_Relacionada") + " - " + leer.Campo("Multiplo") + " - " + leer.CampoInt("AfectaVenta") + " - " + leer.CampoInt("AfectaConsigna") + " - " + leer.Campo("DescripcionRelacionada");
                sClaveSSA = String.Format("{0} - {1} - {2} - {3} - {4}", 
                        leer.Campo("ClaveSSA_Relacionada"), leer.Campo("Multiplo"), 
                        Convert.ToInt32(leer.CampoBool("AfectaVenta")), Convert.ToInt32(leer.CampoBool("AfectaConsigna")), 
                        leer.Campo("DescripcionRelacionada")); 


                // foreach (DataRow dt in dtsEstados.Tables[0].Rows)
                {
                    iPic = 2;
                    if (leer.Campo("Status").ToUpper() == "A") 
                    {
                        nodo = new clsNodo_CuadroBasico();
                        nodo.IdCliente = cboCliente.Data;
                        nodo.IdSubCliente = cboSubCliente.Data;
                        nodo.IdClaveSSA = leer.Campo("IdClaveSSA");
                        nodo.Status = "A";
                        nodo.Separador = "||";

                        nodo.IdClaveSSA_Relacionada = leer.Campo("IdClaveSSA_Relacionada");
                        nodo.Multiplo = leer.CampoInt("Multiplo");
                        nodo.AfectaVenta = leer.CampoBool("AfectaVenta");
                        nodo.AfectaConsigna = leer.CampoBool("AfectaConsigna");



                        TreeNode myNodeGrupo = myNode.Nodes.Add(sClaveSSA);
                        myNodeGrupo.ImageIndex = iPic;
                        myNodeGrupo.SelectedImageIndex = iPic; 
                        myNodeGrupo.Tag = "||" + leer.Campo("IdClaveSSA_Relacionada") + "||" + leer.CampoInt("Multiplo") + "||" + leer.CampoInt("AfectaVenta") + "||" + leer.CampoInt("AfectaConsigna");
                        myNodeGrupo.Tag = nodo; 
                    }
                }
            }

            twClientes.EndUpdate();
            myNode.Expand();

            try
            {
                twClientes.Nodes[0].Text = twClientes.Nodes[0].Text;
            }
            catch { }
        }

        private void CargarSalesGrupo()
        {
            CargarSalesGrupo(false); 
        }

        private void CargarSalesGrupo(bool PermitirTodos)
        {
            // string sSql = " Select * From  ";

            if (cboGrupos.SelectedIndex != 0)
            {
                leer.DataSetClase = query.ClavesSSA_SalesGrupoTerapeutico(cboGrupos.Data, txtBuscar.Text.Trim(), "CargarSalesGrupo()");
            }
            else
            {
                if (PermitirTodos)
                {
                    leer.DataSetClase = query.ClavesSSA_SalesGrupoTerapeutico(0, txtBuscar.Text.Trim(), "CargarSalesGrupo()");
                }
                else
                {
                    leer.DataSetClase = query.ClavesSSA_SalesGrupoTerapeutico(cboGrupos.Data, txtBuscar.Text.Trim(), "CargarSalesGrupo()");
                }
            }

            dtsDatos = leer.DataSetClase;
            //////// string sSql = " Select * From  "; 
            //////leer.DataSetClase = query.ClavesSSA_SalesGrupoTerapeutico(cboEstados.Data, cboGrupos.Data, txtClaveSSA.Text.Trim(), "CargarSalesGrupo()");
            ////leer.DataSetClase = query.ClavesSSA_SalesGrupoTerapeutico(cboEstados.Data, cboGrupos.Data, txtClaveSSA.Text.Trim(), txtBuscar.Text.Trim(), "CargarSalesGrupo()");
            ////// leer.DataSetClase = query.ClavesSSA_SalesGrupoTerapeutico(cboGrupos.Data, txtBuscar.Text.Trim(), "CargarSalesGrupo()");
            ////dtsDatos = leer.DataSetClase;

            //if (leer.Exec(sSql))
            //    dtsDatos = leer.DataSetClase;

            ////dtsDatos = query.Usuarios(General.EntidadConectada);

            clsNodo_CuadroBasico nodo = new clsNodo_CuadroBasico(); 

            object NewColListView = null;
            lwSales.Columns.Clear();
            lwSales.Items.Clear();
            lwSales.View = System.Windows.Forms.View.Details;
            NewColListView = lwSales.Columns.Add("Nombre de Clave", lwSales.Width - 15);
            lblClave.Text = "";

            if (leer.Leer())
            {
                ListViewItem itmX = null;
                string strValor = "";
                NewColListView = new object();

                foreach (DataRow dt in dtsDatos.Tables[0].Rows)
                {
                    nodo = new clsNodo_CuadroBasico();
                    nodo.IdCliente = cboCliente.Data;
                    nodo.IdSubCliente = cboSubCliente.Data;
                    nodo.IdClaveSSA = dt["IdClaveSSA_Sal"].ToString(); 

                    // dt["IdClaveSSA_Sal"].ToString() + " - " + 
                    strValor = dt["ClaveSSA"].ToString() + " - " + dt["Descripcion"].ToString();
                    itmX = lwSales.Items.Add(strValor, 0);
                    itmX.SubItems.Add("" + strValor);
                    itmX.SubItems[0].Tag = dt["IdClaveSSA_Sal"].ToString();

                    itmX.SubItems[0].Tag = nodo;
                }

                //lwSales.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                //lwSales.Columns[0].Width = 420;
            }
        }

        #region Asignar Clientes 
        private void ActualizaMenu(string Tag)
        {
            mnEstados.Items[eliminarToolStripMenuItem.Name].Enabled = false;
            mnEstados.Items[eliminarClientesDelEstadoToolStripMenuItem.Name].Enabled = false;
            mnEstados.Items[actualizarToolStripMenuItem.Name].Enabled = true;

            if (Tag.Substring(0, 2) == "-1")
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
           

            string []sListaValores; 
            string sValor = ""; 

            if (twClientes.Nodes.Count > 0)
            {
                nodoItem = (clsNodo_CuadroBasico)myNodeSeleccionado.Tag;
                ActualizaMenu(nodoItem.Separador);

                //if(!nodoItem.Separador.Contains("|"))
                //{
                //    sIdFarmacia = myNodeSeleccionado.Tag.ToString();
                //}
                //else
                //{
                //    sIdFarmacia = myNodeSeleccionado.Parent.Tag.ToString();
                //    sIdCliente = myNodeSeleccionado.Tag.ToString().Replace("|", "");

                //    sValor = myNodeSeleccionado.Tag.ToString().Replace("||", "|");
                //    sListaValores = sValor.Split('|');

                //    sIdClaveSSA = sListaValores[1];
                //    iMultiplo = Convert.ToInt32("0" + sListaValores[2]);
                //    bAfectarVenta = Convert.ToBoolean(Convert.ToInt32("0" + sListaValores[3]));
                //    bAfectarConsigna = Convert.ToBoolean(Convert.ToInt32("0" + sListaValores[4]));
                //}
            }

            lblClave.Text = myNodeSeleccionado.Text; 
        }

        private bool EsPadreValido(string Valor)
        {
            bool bRegresa = true;

            if (Valor == "-1" &&  // Valor.Substring(0, 2) != "|x" && 
                Valor.Substring(0, 2) == "00" && Valor.Substring(0, 2) == "|0" && Valor.Substring(0, 2) == "||")
            {
                bRegresa = false;
            }

            //if (myNodeSeleccionado.Tag.ToString() == "-1")
            //    bRegresa = false;

            return bRegresa;
        }

        private void twEstados_DragDrop( object sender, DragEventArgs e )
        {
            string sSql = "";
            bool bAgregar = false; 
            TreeNode NewNode = new TreeNode();
            Control myCtrl = (Control)sender;
            Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
            clsNodo_CuadroBasico nodo; // = new clsNodo_CuadroBasico();
            clsNodo_CuadroBasico nodoSeleccionado = new clsNodo_CuadroBasico();

            NewNode = twClientes.GetNodeAt(pt);
            nodoSeleccionado = (clsNodo_CuadroBasico)NewNode.Tag;


            if(nodoSeleccionado.Separador != "-1")
            {
                if(nodoSeleccionado.Separador != "")
                {
                    NewNode = NewNode.Parent;
                    nodoSeleccionado = (clsNodo_CuadroBasico)NewNode.Tag;
                }
            }

            if(nodoSeleccionado.Separador == "-1")
            {
                if(nodoSeleccionado.IdClaveSSA == nodoClave.IdClaveSSA)
                {
                    General.msjAviso("La clave relacionada no puede ser igual a la Clave Principal");
                }
                else
                {
                    if(ExisteOpcion(NewNode, nodoClave.IdClaveSSA))
                    {
                        General.msjAviso("Clave previamente relacionada");
                    }
                    else 
                    {
                        FrmMultiplo f = new FrmMultiplo(1, false, false);
                        f.ShowInTaskbar = false;
                        f.ShowDialog();

                        bAgregar = f.MultiploAsignado;
                        iMultiplo = f.iMultiplo;
                        bAfectarVenta = f.Afecta_Venta;
                        bAfectarConsigna = f.Afecta_Consigna;

                    }

                    if(bAgregar)
                    {
                        nodo = new clsNodo_CuadroBasico();
                        nodo.IdCliente = nodoSeleccionado.IdCliente;
                        nodo.IdSubCliente = nodoSeleccionado.IdSubCliente;
                        nodo.IdClaveSSA = nodoSeleccionado.IdClaveSSA;
                        nodo.Status = "A";
                        nodo.Separador = "||";

                        nodo.Multiplo = iMultiplo;
                        nodo.IdClaveSSA_Relacionada = nodoClave.IdClaveSSA;
                        nodo.AfectaVenta = bAfectarVenta;
                        nodo.AfectaVenta = bAfectarConsigna;


                        sSql = string.Format("Exec spp_Mtto_CFG_ClavesSSA_ClavesRelacionadas \n" +
                            "\t@IdEstado = '{0}', @IdCliente = '{1}', @IdSubCliente = '{2}', @IdClaveSSA = '{3}', @IdClaveSSA_Relacionada = '{4}', @Status = '{5}', @Multiplo = '{6}', \n" +
                            "\t@AfectaVenta = '{7}', @AfectaConsigna = '{8}' \n",
                            cboEstados.Data, nodo.IdCliente, nodo.IdSubCliente, nodo.IdClaveSSA, nodo.IdClaveSSA_Relacionada, nodo.Status,
                            nodo.Multiplo, Convert.ToUInt32(nodo.AfectaVenta), Convert.ToUInt32(nodo.AfectaConsigna));


                        //CargarGrupos();
                        if(!leer.Exec(sSql))
                        {
                            Error.GrabarError(leer, "twClaves_DragDrop");
                        }
                        else
                        {
                            CargarClavesSSA_Relacionadas();
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
            //DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private bool ExisteOpcion(TreeNode myNodeRama, string Rama)
        {
            bool bRegresa = false;
            string sRamaBuscar = myNodeRama.FullPath.ToUpper() + "|" + Rama.ToUpper();
            string myRama = "";
            clsNodo_CuadroBasico item = new clsNodo_CuadroBasico();

            foreach (TreeNode nodoItem_x in myNodeRama.Nodes)
            {
                item = (clsNodo_CuadroBasico)nodoItem_x.Tag;
                myRama = item.IdClaveSSA; 

                myRama = nodoItem_x.FullPath.ToUpper();

                if (item.IdClaveSSA_Relacionada == Rama)
                {
                    bRegresa = true;
                    break;
                }
            }

            return bRegresa;
        }
        #endregion Asignar Clientes

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sQuery = "";

            try
            {
            
                if (nodoItem.Separador != "-1")
                {
                    if (nodoItem.Separador == "||")
                    {
                        sQuery = string.Format("Exec spp_Mtto_CFG_ClavesSSA_ClavesRelacionadas \n" +
                            "\t@IdEstado = '{0}', @IdCliente = '{1}', @IdSubCliente = '{2}', @IdClaveSSA = '{3}', @IdClaveSSA_Relacionada = '{4}', @Status = '{5}', @Multiplo = '{6}' \n",
                            cboEstados.Data, nodoItem.IdCliente, nodoItem.IdSubCliente, 
                            nodoItem.IdClaveSSA, nodoItem.IdClaveSSA_Relacionada, "C", nodoItem.Multiplo, false, false );
                    }

                    if (leer.Exec(sQuery))
                    {
                        twClientes.Nodes.Remove(myNodeSeleccionado);
                    }
                }
            }
            catch { }
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CargarClavesSSA_Relacionadas(); 
            // CargarDatosDeClaveSSA(); 
        }

        private void cambiarMultiploToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sSql = ""; 

            FrmMultiplo f = new FrmMultiplo(nodoItem.Multiplo, nodoItem.AfectaVenta, nodoItem.AfectaConsigna);
            f.ShowInTaskbar = false; 
            f.ShowDialog();


            if (f.MultiploAsignado)
            {
                iMultiplo = f.iMultiplo;
                bAfectarVenta = f.Afecta_Venta;
                bAfectarConsigna = f.Afecta_Consigna; 


                sSql = string.Format("Exec spp_Mtto_CFG_ClavesSSA_ClavesRelacionadas \n" +
                    " @IdEstado = '{0}', @IdCliente = '{1}', @IdSubCliente = '{2}', @IdClaveSSA = '{3}', @IdClaveSSA_Relacionada = '{4}', @Status = '{5}', @Multiplo = '{6}' " +
                    " @AfectaVenta = '{7}', @AfectaConsigna = '{8}' ", 
                    cboEstados.Data, cboCliente.Data, cboSubCliente.Data, Fg.PonCeros(txtClaveSSA.Text, 6), sIdClaveSSA, "A",
                    f.iMultiplo, Convert.ToUInt32(f.Afecta_Venta), Convert.ToUInt32(f.Afecta_Consigna));

                sSql = string.Format("Exec spp_Mtto_CFG_ClavesSSA_ClavesRelacionadas \n" +
                    "\t@IdEstado = '{0}', @IdCliente = '{1}', @IdSubCliente = '{2}', @IdClaveSSA = '{3}', @IdClaveSSA_Relacionada = '{4}', \n" +
                    "\t@Status = '{5}', @Multiplo = '{6}', @AfectaVenta = '{7}', @AfectaConsigna = '{8}' \n",
                    cboEstados.Data, cboCliente.Data, cboSubCliente.Data, nodoItem.IdClaveSSA, nodoItem.IdClaveSSA_Relacionada, "A",
                    f.iMultiplo, Convert.ToUInt32(f.Afecta_Venta), Convert.ToUInt32(f.Afecta_Consigna));


                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "twClaves_DragDrop");
                }
                else
                {
                    CargarClavesSSA_Relacionadas();
                }
            }
        }

        private void eliminarClientesDelEstadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sQuery = "";

            try
            {
                if (nodoItem.Separador == "-1")
                {
                    clsNodo_CuadroBasico nodo = new clsNodo_CuadroBasico();
                    foreach (TreeNode tNodo in myNodeSeleccionado.Nodes)
                    {
                        nodo = (clsNodo_CuadroBasico)tNodo.Tag; 

                        sQuery = string.Format("Exec spp_Mtto_CFG_ClavesSSA_ClavesRelacionadas \n" +
                            "\t@IdEstado = '{0}', @IdCliente = '{1}', @IdSubCliente = '{2}', @IdClaveSSA = '{3}', @IdClaveSSA_Relacionada = '{4}', @Status = '{5}', @Multiplo = '{6}' \n",
                            cboEstados.Data, nodo.IdCliente, nodo.IdSubCliente,
                            nodo.IdClaveSSA, nodo.IdClaveSSA_Relacionada, "C", nodo.Multiplo, Convert.ToUInt32(nodo.AfectaVenta), Convert.ToUInt32(nodo.AfectaConsigna));

                        // leer.Exec(sQuery); 
                        if (leer.Exec(sQuery))
                        {
                            twClientes.Nodes.Remove(tNodo);
                        }
                    }

                    // Actualizar la lista 
                    // CargarClavesSSA_Relacionadas(); 
                }
            }
            catch { }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            CargarSalesGrupo();
        }

        private void cboEstados_SelectedValueChanged(object sender, EventArgs e)
        {
            //if (cboEstados.SelectedIndex != 0)
            {
                //CargarSalesClientes();
                //CargarSalesGrupo();
            }
        }

        private void lwSales_ItemDrag(object sender, ItemDragEventArgs e)
        {
            sIdCliente = lwSales.FocusedItem.SubItems[0].Tag.ToString();
            sNombreCliente = lwSales.FocusedItem.SubItems[0].Text.Trim();
            sIdClaveSSA = lwSales.FocusedItem.SubItems[0].Tag.ToString();

            nodoClave = (clsNodo_CuadroBasico)lwSales.FocusedItem.SubItems[0].Tag;

            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void lwSales_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            lblClave.Text = lwSales.FocusedItem.SubItems[0].Text.Trim();
        }

        private void txtClaveSSA_Validating(object sender, CancelEventArgs e)
        {
            if (txtClaveSSA.Text.Trim() != "")
            {
                leer.DataSetClase = query.ClavesSSA_Sales(txtClaveSSA.Text.Trim(),  true, "txtClaveSSA_Validating");
                if (leer.Leer())
                {
                    CargarDatosDeClaveSSA();
                }
            }
        }

        private void txtClaveSSA_TextChanged(object sender, EventArgs e)
        {
            lblClaveSSA.Text = "";
            lblDescripcion.Text = "";

            twClientes.Nodes.Clear();
        }

        private void CargarDatosDeClaveSSA()
        {
            lblClaveSSA.Visible = false;
            txtClaveSSA.Enabled = false; 
            txtClaveSSA.Text = leer.Campo("ClaveSSA");
            lblClaveSSA.Text = leer.Campo("IdClaveSSA_Sal");
            lblDescripcion.Text = leer.Campo("Descripcion");

            CargarClavesSSA_Relacionadas(); 
        }

        private void txtClaveSSA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1) 
            {
                leer.DataSetClase = ayuda.ClavesSSA_Sales("txtClaveSSA_KeyDown");

                if (leer.Leer())
                {
                    CargarDatosDeClaveSSA();
                }
            }
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEstados.SelectedIndex != 0)
            {
                cboEstados.Enabled = false;
                leer.DataSetClase = query.ComboCliente("cboCliente_Validating");
                if (leer.Leer())
                {
                    LlenaComboCliente();
                }
            }
        }

        private void HabilitarCaptura(bool bValor)
        {
            //chkClaveInterna.Enabled = bValor; 
            txtClaveSSA.Enabled = bValor;
            cboGrupos.Enabled = bValor;
            txtBuscar.Enabled = bValor;
            btnAgregar.Enabled = bValor; 
        }

        private void btnExportarExcel_Click( object sender, EventArgs e )
        {
            string sSql = string.Format("Exec spp_PRCS_OCEN__Plantilla_CargaDePrecios  @IdEstado = '{0}', @IdCliente = '{1}', @IdSubCliente = '{2}', @Tipo = '{3}' ",
                cboEstados.Data, cboCliente.Data, cboSubCliente.Data, 3);

            if(!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "btnExportarExcel_Click");
                General.msjError("Ocurrió un error al obtener la información para generar la plantilla de Carga de Cuadro Básico.");
            }
            else
            {
                Generar_Excel();
            }
        }

        private void Generar_Excel()
        {
            DllFarmaciaSoft.ExportarExcel.clsGenerarExcel excel = new DllFarmaciaSoft.ExportarExcel.clsGenerarExcel();
            string sNombreDocumento = string.Format("Plantilla_ClavesRelacionadas___{0}", cboEstados.ItemActual.GetItem("Nombre"));
            string sNombreHoja = "ClavesRelacionadas";
            string sConcepto = "";

            int iHoja = 1, iRenglon = 15;
            int iColBase = 2;
            int iColsEncabezado = 8;

            DateTime dtpFecha = General.FechaSistema;
            int iAño = dtpFecha.AddMonths(-1).Year, iMes = dtpFecha.AddMonths(-1).Month;
            //int iHoja = 1;
            string sEmpresa = DtGeneral.EmpresaConectadaNombre;
            string sEstado = DtGeneral.EstadoConectadoNombre;
            string sFarmacia = DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre;
            string sFechaImpresion = General.FechaSistemaFecha.ToString();

            excel = new DllFarmaciaSoft.ExportarExcel.clsGenerarExcel();
            excel.RutaArchivo = @"C:\\Excel";
            excel.NombreArchivo = sNombreDocumento;
            excel.AgregarMarcaDeTiempo = true;

            if(excel.PrepararPlantilla(sNombreDocumento))
            {
                excel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                iRenglon = 1;
                //excel.LlenarDetalleHorizontal(sNombreHoja, iRenglon, iColBase, leer.DataSetClase); 
                excel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);

                //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);

                excel.CerraArchivo();

                excel.AbrirDocumentoGenerado(true);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            CargarSalesGrupo(true); 
        }

        private void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCliente.SelectedIndex != 0)
            {
                leer.DataSetClase = query.ComboSubCliente(cboCliente.Data, "cboCliente_Validating");
                if (leer.Leer())
                {
                    cboCliente.Enabled = false; 
                    LlenaComboSubCliente();
                }
            }
        }

        private void cboSubCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            HabilitarCaptura(false);
            if (cboEstados.SelectedIndex != 0)
            {
                if (cboSubCliente.SelectedIndex != 0)
                {
                    cboSubCliente.Enabled = false; 
                    HabilitarCaptura(true);
                    txtClaveSSA.Enabled = true;

                    btnExportarExcel.Enabled = true;
                    //chkClaveInterna.Enabled = true; 
                }
            }
        }
    }
}
