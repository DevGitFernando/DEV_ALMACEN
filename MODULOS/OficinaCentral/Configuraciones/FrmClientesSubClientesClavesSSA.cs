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
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;

namespace OficinaCentral.Configuraciones
{
    public partial class FrmClientesSubClientesClavesSSA : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer, leer2, leer3;

        clsConsultas query;

        clsDatosCliente DatosCliente;
        wsOficinaCentral.wsCnnOficinaCentral conexionWeb;

        clsNodo_CuadroBasico itemNodo = new clsNodo_CuadroBasico();
        TreeNode myNodeSeleccionado; //, myNodoUsuario;
        // DataSet dtsEstados;
        // DataSet dtsFarmacias;
        // DataSet dtsDatos;
        FrmCriterioDeBusqueda B;

        string sIdSubCliente = "", sNombreSubCliente = ""; // , sUsuario = "";
        string sIdCliente = ""; // , sNombreCliente = "";
        string sIdClaveSSA = "", sNombreClaveSSA;

        string sClaveSeleccionada = "";
        clsResize formResize; 

        public FrmClientesSubClientesClavesSSA()
        {
            InitializeComponent();
            formResize = new clsResize(this);

            leer = new clsLeer(ref cnn);
            leer2 = new clsLeer(ref cnn);
            leer3 = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name, false);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.DatosApp, this.Name);

            DatosCliente = new clsDatosCliente(GnOficinaCentral.DatosApp, this.Name, "");
            conexionWeb = new OficinaCentral.wsOficinaCentral.wsCnnOficinaCentral();
            conexionWeb.Url = General.Url;

            //General.Pantalla.AjustarTamaño(this, 90, 80);
        }

        private void FrmClientesSubClientesClavesSSA_Load(object sender, EventArgs e)
        {
            Fg.IniciaControles();
            CargarClavesSSAClientes();
        }

        private void CargarClavesSSAClientes()
        {
            //string sSql = string.Format("Select * From CatEstados (noLock) Where Status = 'A' ");

            //if (leer.Exec(sSql))
            //    dtsEstados = leer.DataSetClase;
            //dtsGrupos = query.Grupos(General.EntidadConectada);
            clsNodo_CuadroBasico item = new clsNodo_CuadroBasico();


            leer.DataSetClase = query.Clientes_ClavesSSA("CargarSalesClientes()"); 
            twClientes.Nodes.Clear();
            twClientes.BeginUpdate();

            TreeNode myNode = new TreeNode();
            twClientes.Nodes.Clear();

            //if (cboGrupos.SelectedIndex != 0)
            {
                item.Separador = "-1";

                twClientes.Nodes.Clear();
                myNode = twClientes.Nodes.Add("CLIENTES");
                myNode.Tag = "-1";
                myNode.Tag = item;
                myNode.ImageIndex = 0;
                myNode.SelectedImageIndex = 0;
            }

            while (leer.Leer())
            {
                sIdSubCliente = leer.Campo("IdCliente");
                sNombreSubCliente = "[" + sIdSubCliente + "]" + "   " + leer.Campo("Nombre");

                // foreach (DataRow dt in dtsEstados.Tables[0].Rows)
                {
                    item = new clsNodo_CuadroBasico();

                    item.IdCliente = leer.Campo("IdCliente");
                    item.IdSubCliente = leer.Campo("IdSubCliente");
                    item.Separador= "|x"; 

                    TreeNode myNodeGrupo = myNode.Nodes.Add(sNombreSubCliente);
                    myNodeGrupo.ImageIndex = 1;
                    myNodeGrupo.SelectedImageIndex = 1;
                    myNodeGrupo.Tag = "|x" + sIdSubCliente;
                    myNodeGrupo.Tag = item; 

                    ////////string sClaveSSA = ""; 
                    ////////string sQuery = string.Format(" Select C.IdClaveSSA_Sal, S.ClaveSSA, S.DescripcionSal " +
                    ////////        " From CFG_Clientes_Claves C (NoLock) " +
                    ////////        " Inner Join vw_ClavesSSA_Sales S (NoLock) On ( C.IdClaveSSA_Sal = S.IdClaveSSA_Sal ) " +
                    ////////        " Where C.IdCliente = '{0}' and C.Status = 'A' " +
                    ////////        " Order By S.DescripcionSal ", sIdSubCliente);
                    ////////// order by S.DescripcionSal  


                    ////////////dtsUsuariosGrupo = query.GruposUsuarios(General.EntidadConectada, sIdGrupo);
                    ////////leer2.Exec(sQuery); 
                    ////////// dtsUsuariosGrupo = leer.DataSetClase;
                    ////////while( leer2.Leer() )
                    ////////{
                    ////////    sClaveSSA = leer2.Campo("ClaveSSA") + " - " + leer2.Campo("DescripcionSal");
                    ////////    TreeNode myNodeRama = myNodeGrupo.Nodes.Add(sClaveSSA);
                    ////////    myNodeRama.ImageIndex = 2;
                    ////////    myNodeRama.SelectedImageIndex = 2;
                    ////////    myNodeRama.Tag = "||" + leer2.Campo("IdClaveSSA_Sal");  
                    ////////}
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

        private void CargarClavesSSA_Cliente(TreeNode Nodo, string IdCliente)
        {
            CargarClavesSSA_Cliente(Nodo, IdCliente, ""); 
        }

        private void CargarClavesSSA_Cliente(TreeNode Nodo, string IdCliente, string Criterio)
        {
            clsNodo_CuadroBasico nodo = new clsNodo_CuadroBasico(); 
            bool bCargar = false; 
            string sClaveSSA = "";
            string sQuery = string.Format(" Select S.IdCliente, S.IdClaveSSA, S.ClaveSSA, S.DescripcionClave " +
                    " From vw_Claves_Asignadas_A_Clientes S (NoLock) " + 
                    " Where S.IdCliente = '{0}' and S.Status = 'A' and S.DescripcionClave like '%{1}%' " +
                    " Order by S.DescripcionClave ", IdCliente, Criterio); 

            ////dtsUsuariosGrupo = query.GruposUsuarios(General.EntidadConectada, sIdGrupo); 
            if (Nodo.Nodes.Count == 0)
            {
                bCargar = true;
            }

            if (Criterio != "")
            {
                bCargar = true;
            }


            if (bCargar)
            {
                Cursor.Current = Cursors.WaitCursor;
                CargarSubClientesClavesSSA();

                // Evitar duplicidad de datos
                Nodo.Nodes.Clear(); 

                leer3.Exec(sQuery);
                while (leer3.Leer())
                {
                    nodo.IdCliente = IdCliente; 
                    nodo.IdClaveSSA = leer3.Campo("IdClaveSSA");
                    nodo.Separador = "||";

                    sClaveSSA = "[" + leer3.Campo("IdClaveSSA") + "]" + "   " + leer3.Campo("ClaveSSA") + " - " + leer3.Campo("DescripcionClave");
                    TreeNode myNodeRama = Nodo.Nodes.Add(sClaveSSA);
                    myNodeRama.ImageIndex = 2;
                    myNodeRama.SelectedImageIndex = 2;
                    myNodeRama.Tag = "||" + leer3.Campo("IdClaveSSA");

                    myNodeRama.Tag = nodo; 
                }
                Cursor.Current = Cursors.Default;
                Nodo.ExpandAll();
            }

        }

        private void CargarClavesSSA_SubClientes(TreeNode Nodo, string IdCliente, string IdSubCliente)
        {
            CargarClavesSSA_SubClientes(Nodo, IdCliente, IdSubCliente, ""); 
        }

        private void CargarClavesSSA_SubClientes(TreeNode Nodo, string IdCliente, string IdSubCliente, string Criterio)
        {
            bool bCargar = false;
            clsNodo_CuadroBasico item = new clsNodo_CuadroBasico();
            clsNodo_CuadroBasico itemClave = new clsNodo_CuadroBasico();

            string sClaveSSA = "";
            string sQuery = ""; 
                        //string sQuery = string.Format(" Select C.IdCliente, C.IdClaveSSA_Sal, S.ClaveSSA, S.DescripcionSal " +
                        //        " From CFG_Clientes_SubClientes_Claves C (NoLock) " +
                        //        " Inner Join vw_ClavesSSA_Sales S (NoLock) On ( C.IdClaveSSA_Sal = S.IdClaveSSA_Sal ) " +
                        //        " Where C.IdCliente = '{0}' and C.IdSubCliente = '{1}' and C.Status = 'A' " +
                        //        " Order by S.DescripcionSal ", IdCliente, IdSubCliente);

            sQuery = string.Format(" Select S.IdCliente, S.IdClaveSSA, S.ClaveSSA, S.DescripcionClave " +
                    " From vw_Claves_Asignadas_A_SubClientes S (NoLock) " +
                    " Where S.IdCliente = '{0}' and S.IdSubCliente = '{1}' and S.Status = 'A' and S.DescripcionClave like '%{2}%' " +
                    " Order by S.DescripcionClave ",
                    itemNodo.IdCliente, itemNodo.IdSubCliente, 
                    ////IdCliente, IdSubCliente, 
                    Criterio 
                    ); 

            ////dtsUsuariosGrupo = query.GruposUsuarios(General.EntidadConectada, sIdGrupo); 
            if (Nodo.Nodes.Count == 0)
            {
                bCargar = true;
            }

            if (Criterio != "")
            {
                bCargar = true;
            }


            if (bCargar)
            {
                Cursor.Current = Cursors.WaitCursor;

                // Evitar duplicidad de datos
                Nodo.Nodes.Clear();

                item = (clsNodo_CuadroBasico)Nodo.Tag;

                leer3.Exec(sQuery); 
                while (leer3.Leer())
                {

                    itemClave = new clsNodo_CuadroBasico();
                    itemClave.IdCliente = IdCliente;
                    itemClave.IdSubCliente = IdSubCliente;
                    itemClave.IdClaveSSA = leer3.Campo("IdClaveSSA");
                    itemClave.Separador = "||";

                    sClaveSSA = "[" + leer3.Campo("IdClaveSSA") + "]" + "   " + leer3.Campo("ClaveSSA") + " - " + leer3.Campo("DescripcionClave");
                    TreeNode myNodeRama = Nodo.Nodes.Add(sClaveSSA);
                    myNodeRama.ImageIndex = 2;
                    myNodeRama.SelectedImageIndex = 2;
                    myNodeRama.Tag = "||" + leer3.Campo("IdClaveSSA");

                    myNodeRama.Tag = itemClave; 
                }

                Cursor.Current = Cursors.Default;
                Nodo.ExpandAll();
            }
        }

        #region Asignar Clientes 
        private void ActualizaMenuClientes(string Tag)
        {
            mnClientes.Items[cargarClavesAsignadasToolStripMenuItem.Name].Enabled = false;
            mnClientes.Items[buscarClavesToolStripMenuItem.Name].Enabled = false;

            if (Tag == "|x")
            {
                mnClientes.Items[cargarClavesAsignadasToolStripMenuItem.Name].Enabled = true;
                mnClientes.Items[buscarClavesToolStripMenuItem.Name].Enabled = true;
            }
        }

        private void ActualizaMenu(string Tag)
        {
            bool bActivo = false;
            mnSubClientes.Items[eliminarToolStripMenuItem.Name].Enabled = false;
            mnSubClientes.Items[eliminarClientesDelEstadoToolStripMenuItem.Name].Enabled = false;
            mnSubClientes.Items[actualizarToolStripMenuItem.Name].Enabled = true; 

            sIdSubCliente = ""; 
            if (Tag.Substring(0, 2) == "|x")
            {
                mnSubClientes.Items[eliminarClientesDelEstadoToolStripMenuItem.Name].Enabled = true;
                mnSubClientes.Items[eliminarToolStripMenuItem.Name].Enabled = false;

                bActivo = true; 
                sIdSubCliente = Fg.Mid(Tag, 3);
                //CargarClavesSSA_SubClientes(myNodeSeleccionado, sIdCliente, sIdSubCliente); 
            }

            if (Tag.Substring(0, 2) == "||")
            {
                mnSubClientes.Items[eliminarClientesDelEstadoToolStripMenuItem.Name].Enabled = false;
                mnSubClientes.Items[eliminarToolStripMenuItem.Name].Enabled = true;
            }

            mnSubClientes.Items[cargarClavesAsignadasToolStripMenuItem1.Name].Enabled = bActivo;
            mnSubClientes.Items[buscarClavesToolStripMenuItem1.Name].Enabled = bActivo; 
        }

        private void ActualizaListaSubClientes(TreeNode Node)
        {
            string Tag = Node.Tag.ToString();
            clsNodo_CuadroBasico nodo = (clsNodo_CuadroBasico)Node.Tag;

            if (nodo.Separador == "|x")
            {
                sIdCliente = nodo.IdCliente; // Tag.Substring(2, 4);
                //CargarSubClientesClavesSSA();
                //CargarClavesSSA_Cliente(myNodeSeleccionado, sIdCliente.ToUpper().Replace("X", "")); 
            }

            if (nodo.Separador == "||")
            {
                sIdClaveSSA = Tag.ToString().Replace("|", "");
                sNombreClaveSSA = Node.Text;

                sIdClaveSSA = nodo.IdClaveSSA; 

                Tag = nodo.IdCliente;
                if (sIdCliente != Tag)
                {
                    sIdCliente = Tag;
                    //CargarSubClientesClavesSSA();
                    //CargarClavesSSA_Cliente(myNodeSeleccionado, sIdCliente.ToUpper().Replace("X", "")); 
                }
            }
        }

        private void twEstados_AfterSelect(object sender, TreeViewEventArgs e)
        {
            myNodeSeleccionado = e.Node;

            if (twClientes.Nodes.Count > 0)
            {
                itemNodo = (clsNodo_CuadroBasico)myNodeSeleccionado.Tag;
                itemNodo = (clsNodo_CuadroBasico)e.Node.Tag;

                sClaveSeleccionada = myNodeSeleccionado.Text;

                ActualizaMenuClientes(itemNodo.Separador); 
                ActualizaListaSubClientes(e.Node); 
                
                //if (!myNodeSeleccionado.Tag.ToString().Contains("|"))
                //    sIdSubCliente = myNodeSeleccionado.Tag.ToString();
                //else
                //{
                //    sIdSubCliente = myNodeSeleccionado.Parent.Tag.ToString();
                //    sIdCliente = myNodeSeleccionado.Tag.ToString().Replace("|", "");
                //    sIdClaveSSA = myNodeSeleccionado.Tag.ToString().Replace("|", "");
                //}
            }
        }

        private void twSubClientes_AfterSelect(object sender, TreeViewEventArgs e)
        {
            myNodeSeleccionado = e.Node;

            if (twSubClientes.Nodes.Count > 0)
            {
                itemNodo = (clsNodo_CuadroBasico)myNodeSeleccionado.Tag;

                sIdCliente = itemNodo.IdCliente;
                sIdSubCliente = itemNodo.IdSubCliente; 

                ActualizaMenu(itemNodo.Separador);

                //if (!myNodeSeleccionado.Tag.ToString().Contains("|"))
                //    sIdSubCliente = myNodeSeleccionado.Tag.ToString();
                //else
                //{
                //    sIdSubCliente = myNodeSeleccionado.Parent.Tag.ToString();
                //    sIdCliente = myNodeSeleccionado.Tag.ToString().Replace("|", "");
                //    sIdClaveSSA = myNodeSeleccionado.Tag.ToString().Replace("|", "");
                //}
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

        private void twSubClientes_DragDrop(object sender, DragEventArgs e)
        {
            string sSql = "";
            TreeNode NewNode; 
            Control myCtrl = (Control)sender;
            Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
            clsNodo_CuadroBasico nodo;             
            
            NewNode = twSubClientes.GetNodeAt(pt);
            //itemNodo = (clsNodo_CuadroBasico)myNodeSeleccionado.Tag;
            //sIdSubCliente = NewNode.Tag.ToString().Substring(2, 4);
            nodo = (clsNodo_CuadroBasico)NewNode.Tag;
            //nodo = itemNodo; 


            sSql = string.Format("Exec spp_Mtto_CFG_Clientes_SubClientes_Claves @IdCliente = '{0}', @IdSubCliente = '{1}', @IdClaveSSA_Sal = '{2}', @Status = '{3}'",
                nodo.IdCliente, nodo.IdSubCliente, itemNodo.IdClaveSSA, "A");

            //if (NewNode.Parent.Tag.ToString() == "-1" || NewNode.Tag.ToString().Substring(0,2) == "|x")
            if(nodo.Separador == "-1" || nodo.Separador == "|x")
            {
                //if (EsPadreValido(sIdSubCliente))
                {
                    // sIdCliente + " - " + sNombreCliente 
                    if (!ExisteOpcion(NewNode, sNombreClaveSSA))
                    {
                        //CargarGrupos();
                        if (!leer.Exec(sSql))
                        {
                            Error.GrabarError(leer, "twSubClientes_DragDrop"); 
                            General.msjUser("Error ==> " + leer.MensajeError); 
                        }
                        else 
                        {
                            nodo.Separador = "||";

                            // sIdCliente + " - " +  
                            TreeNode myNodeRama = NewNode.Nodes.Add(sNombreClaveSSA);
                            myNodeRama.ImageIndex = 2;
                            myNodeRama.SelectedImageIndex = 2;
                            myNodeRama.Tag = "||" + sIdClaveSSA;
                            myNodeRama.Tag = nodo; 
                            // AsignarIcono(myNodeRama, RamaSeleccionada.TipoRama);
                        }
                        NewNode.Expand();
                    }
                }
            }
        }

        private void twSubClientes_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void twSubClientes_ItemDrag(object sender, ItemDragEventArgs e)
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

        //private void lwClientes_ItemDrag(object sender, ItemDragEventArgs e)
        //{
        //    sIdCliente = lwSales.FocusedItem.SubItems[0].Tag.ToString();
        //    sNombreCliente = lwSales.FocusedItem.SubItems[0].Text.Trim();
        //    sIdClaveSSA = lwSales.FocusedItem.SubItems[0].Tag.ToString(); 
        //    DoDragDrop(e.Item, DragDropEffects.Move);
        //}
        #endregion Asignar Clientes

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sQuery = "";
            string sTag = ""; 
            try
            {
                if (itemNodo.Separador != "-1")
                {
                    //sTag = myNodeSeleccionado.Tag.ToString();
                    if (itemNodo.Separador == "||")
                    {
                        sQuery = string.Format("Exec spp_Mtto_CFG_Clientes_SubClientes_Claves  @IdCliente = '{0}', @IdSubCliente = '{1}', @IdClaveSSA_Sal = '{2}', @Status = '{3}' ",
                            itemNodo.IdCliente, itemNodo.IdSubCliente, itemNodo.IdClaveSSA, "C");
                    }

                    if (leer.Exec(sQuery))
                    {
                        twClientes.Nodes.Remove(myNodeSeleccionado);
                    }
                }
            }
            catch { }
        }

        private void imprimirReporteDeClavesAsignadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DatosCliente.Funcion = "btnImprimir_Click()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            byte[] btReporte = null;
            // bool bRegresa = false;  
            // string sEstado = ""; 

            myRpt.RutaReporte = GnOficinaCentral.RutaReportes;
            myRpt.NombreReporte = "Central_Listado_ClavesSSA_Asignadas_Cte_SubCte"; 


            myRpt.Add("@IdCliente", sIdCliente);
            myRpt.Add("@IdSubCliente", sIdSubCliente);


            DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
            DataSet datosC = DatosCliente.DatosCliente();

            conexionWeb.Timeout = 300000; 
            btReporte = conexionWeb.Reporte(InfoWeb, datosC);

            if (!myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true))
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CargarSubClientesClavesSSA();
        }

        private void eliminarClientesDelEstadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sQuery = "";

            try
            {
                if (itemNodo.Separador == "|x")
                {
                    clsNodo_CuadroBasico nodo; 
                    foreach (TreeNode tNodo in myNodeSeleccionado.Nodes)
                    {
                        nodo = (clsNodo_CuadroBasico)tNodo.Tag;
                        sQuery = string.Format("Exec spp_Mtto_CFG_Clientes_SubClientes_Claves @IdCliente = '{0}', @IdSubCliente = '{1}', @IdClaveSSA_Sal = '{2}', @Status = '{3}'", 
                            nodo.IdCliente, nodo.IdSubCliente, nodo.IdClaveSSA, "C");

                        leer.Exec(sQuery); 
                        //if (leer.Exec(sQuery))
                        //    twClientes.Nodes.Remove(tNodo);
                    }

                    // Actualizar la lista 
                    CargarSubClientesClavesSSA(); 
                }
            }
            catch { }
        }

        private void btnMnActualizarListaClientes_Click(object sender, EventArgs e)
        {
            CargarClavesSSAClientes(); 
        }

        private void CargarSubClientesClavesSSA()
        {
            string sRaiz = "SUB-CLIENTES";
            string sSql = string.Format("Select * From vw_Clientes_SubClientes (NoLock) Where IdCliente = '{0}' ", sIdCliente);
            clsNodo_CuadroBasico nodo = new clsNodo_CuadroBasico(); 

            leer.Exec(sSql); 
            twSubClientes.Nodes.Clear();
            twSubClientes.BeginUpdate();

            TreeNode myNode = new TreeNode();
            twSubClientes.Nodes.Clear();

            if (leer.Leer())
            {
                sRaiz = leer.Campo("NombreCliente");
            }

            //if (cboGrupos.SelectedIndex != 0)
            {
                nodo.Separador = "-1"; 
                
                twSubClientes.Nodes.Clear();
                myNode = twSubClientes.Nodes.Add(sRaiz);
                myNode.Tag = "-1";
                myNode.Tag = nodo; 
                myNode.ImageIndex = 0;
                myNode.SelectedImageIndex = 0;
            }

            leer.RegistroActual = 1; 
            while (leer.Leer())
            {
                sIdSubCliente = leer.Campo("IdSubCliente");
                sNombreSubCliente = "[" + sIdSubCliente + "]" + "   " + leer.Campo("NombreSubCliente");

                nodo = new clsNodo_CuadroBasico();

                nodo.IdCliente = sIdCliente;
                nodo.IdSubCliente = sIdSubCliente;
                nodo.Separador = "|x"; 

                // foreach (DataRow dt in dtsEstados.Tables[0].Rows)
                {
                    TreeNode myNodeGrupo = myNode.Nodes.Add(sNombreSubCliente);
                    myNodeGrupo.ImageIndex = 1;
                    myNodeGrupo.SelectedImageIndex = 1;
                    myNodeGrupo.Tag = "|x" + sIdSubCliente;
                    myNodeGrupo.Tag = nodo;

                    ////string sClaveSSA = "";
                    ////string sQuery = string.Format(" Select C.IdClaveSSA_Sal, S.ClaveSSA, S.DescripcionSal " +
                    ////        " From CFG_Clientes_SubClientes_Claves C (NoLock) " +
                    ////        " Inner Join vw_ClavesSSA_Sales S (NoLock) On ( C.IdClaveSSA_Sal = S.IdClaveSSA_Sal ) " +
                    ////        " Where C.IdCliente = '{0}' and C.IdSubCliente = '{1}' and C.Status = 'A' " +
                    ////        " Order by S.DescripcionSal ", sIdCliente, sIdSubCliente);
                    ////// order by S.DescripcionSal  
                    ////leer2.Exec(sQuery); 
                    ////while (leer2.Leer())
                    ////{
                    ////    sClaveSSA = leer2.Campo("ClaveSSA") + " - " + leer2.Campo("DescripcionSal");
                    ////    TreeNode myNodeRama = myNodeGrupo.Nodes.Add(sClaveSSA);
                    ////    myNodeRama.ImageIndex = 2;
                    ////    myNodeRama.SelectedImageIndex = 2;
                    ////    myNodeRama.Tag = "||" + leer2.Campo("IdClaveSSA_Sal"); 
                    ////}
                }
            }

            twSubClientes.EndUpdate();
            myNode.Expand();

        }

        private void cargarClavesAsignadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsNodo_CuadroBasico nodo = (clsNodo_CuadroBasico)myNodeSeleccionado.Tag;
            CargarClavesSSA_Cliente(myNodeSeleccionado, nodo.IdCliente); 
        }

        private void buscarClavesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            B = new FrmCriterioDeBusqueda();
            string sCriterio = B.MostarCriterio();

            CargarClavesSSA_Cliente(myNodeSeleccionado, sIdCliente, sCriterio); 
        }

        private void twClientes_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            //CargarClavesSSA_Cliente(myNodeSeleccionado, sIdCliente, myNodeSeleccionado.Text);
            //myNodeSeleccionado.Text = sClaveSeleccionada;
            //twClientes.LabelEdit = false; 
        }

        private void cargarClavesAsignadasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            clsNodo_CuadroBasico nodo = (clsNodo_CuadroBasico)myNodeSeleccionado.Tag;
            CargarClavesSSA_SubClientes(myNodeSeleccionado, nodo.IdCliente, nodo.IdSubCliente); 
        }

        private void buscarClavesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            B = new FrmCriterioDeBusqueda();
            string sCriterio = B.MostarCriterio();
            CargarClavesSSA_SubClientes(myNodeSeleccionado, sIdCliente, sIdSubCliente, sCriterio); 
        }
    }
}
