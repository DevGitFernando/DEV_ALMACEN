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
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Usuarios_y_Permisos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Usuarios_y_Permisos;

namespace OficinaCentral.CuadrosBasicos
{
    public partial class FrmCOM_ProductosParaCompra : FrmBaseExt 
    {
        clsConsultas query;
        DataSet dtsDatos = new DataSet();
        DataSet dtsGrupos = new DataSet(), dtsUsuariosGrupo = new DataSet();
        DataSet dtsClientes = new DataSet();
        DataSet dtsSubClientes = new DataSet();

        TreeNode myNodeSeleccionado; //, myNodoUsuario;
        FrmCriterioDeBusqueda B;

        int iIndexNodo = 0;
        string sClaveSSA_Seleccionada = "";
        string sClaveDesc = "";
        string sIdGrupo = "";

        string sPermisoPerfiles = "MODIFICAR_PERFILES";
        bool bPermisoPerfiles = false;

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas Consultas;
        clsListView lst; 

        public FrmCOM_ProductosParaCompra()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, General.DatosApp, this.Name, false);
            Error = new clsGrabarError(General.DatosConexion, General.DatosApp, this.Name);
            lst = new clsListView(lstProductos); 
        }

        #region Form 
        private void FrmCOM_ProductosParaCompra_Load(object sender, EventArgs e)
        {
            lst.LimpiarItems(); 
            CargarEstados(); 
        }
        #endregion Form 

        #region Botones 
        #endregion Botones

        #region Cargar informacion 
        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            twClaves.Nodes.Clear();
            lst.LimpiarItems();

            TreeNode myNode = new TreeNode();
            twClaves.Nodes.Clear();
            myNode = twClaves.Nodes.Add("Claves");
            myNode.Tag = "-1";
            myNode.ImageIndex = 0;
            myNode.SelectedImageIndex = 0; 
        }

        private void CargarEstados()
        {
            string sSql = " Select Distinct IdEstado, Estado, ( IdEstado + ' - ' + Estado ) as Descripcion From vw_Farmacias_Urls (NoLock) Order by IdEstado ";
            cboEstados.Clear();
            cboEstados.Add();

            if (!DtGeneral.EsAdministrador)
            {
                sSql = string.Format(" Select Distinct IdEstado, Estado From vw_Farmacias_Urls (NoLock) " +
                    " Where IdEstado = '{0}' Order by IdEstado ", DtGeneral.EstadoConectado);
            }

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarEstados()");
                General.msjError("Ocurrió un error al Cargar la Lista de Estados.");
            }
            else
            {
                if (leer.Leer())
                {
                    cboEstados.Add(leer.DataSetClase, true, "IdEstado", "Descripcion");
                }
            }

            cboEstados.SelectedIndex = 0; 
        }

        private void CargarClavesSSA_Estado(TreeNode Nodo)
        {
            CargarClavesSSA_Estado(Nodo, "");
        }

        private void CargarClavesSSA_Estado(TreeNode Nodo, string Criterio)
        {
            bool bCargar = false;
            string sClaveSSA = "";
            string sQuery = string.Format(" Select S.ClaveSSA, S.DescripcionClave " +
                    " From vw_Claves_Precios_Asignados S (NoLock) " +
                    " Where S.IdEstado = '{0}' and S.Status = 'A' and S.DescripcionClave like '%{1}%' " +
                    " Group by S.ClaveSSA, S.DescripcionClave " + 
                    " Order by S.DescripcionClave " + 
                    " ", 
                    cboEstados.Data, Criterio); 

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

                leer.Exec(sQuery);
                while (leer.Leer())
                {
                    sClaveSSA = leer.Campo("ClaveSSA") + " - " + leer.Campo("DescripcionClave");
                    TreeNode myNodeRama = Nodo.Nodes.Add(sClaveSSA);
                    myNodeRama.ImageIndex = 2;
                    myNodeRama.SelectedImageIndex = 2;
                    myNodeRama.Tag = "||x" + leer.Campo("ClaveSSA");
                }
                Cursor.Current = Cursors.Default;
                Nodo.ExpandAll();
            }

        }

        private void CargarProductos_ClavesSSA(TreeNode Nodo)
        {
            bool bCargar = false;
            string sClaveSSA = "";
            string sQuery = string.Format(
                " Select S.ClaveSSA, S.DescripcionClave " +
                " From vw_Claves_Precios_Asignados S (NoLock) " +
                " Where S.IdEstado = '{0}' and S.Status = 'A' and S.DescripcionClave like '%{1}%' " +
                " Group by S.ClaveSSA, S.DescripcionClave " +
                " Order by S.DescripcionClave " +
                " ",
                sClaveSSA_Seleccionada);

            ////dtsUsuariosGrupo = query.GruposUsuarios(General.EntidadConectada, sIdGrupo); 
            if (Nodo.Nodes.Count == 0)
            {
                bCargar = true;
            } 

            if (bCargar)
            {
                Cursor.Current = Cursors.WaitCursor;

                // Evitar duplicidad de datos
                Nodo.Nodes.Clear();

                leer.Exec(sQuery);
                while (leer.Leer())
                {
                    sClaveSSA = leer.Campo("CodigoEAN") + " - " + leer.Campo("Descripcion");
                    TreeNode myNodeRama = Nodo.Nodes.Add(sClaveSSA);
                    myNodeRama.ImageIndex = 2;
                    myNodeRama.SelectedImageIndex = 2;
                    myNodeRama.Tag = "||x" + leer.Campo("ClaveSSA");
                }
                Cursor.Current = Cursors.Default;
                Nodo.ExpandAll();
            }

        }
        #endregion Cargar informacion

        #region Menus 
        private void btnClaves_CargarLista_Click(object sender, EventArgs e)
        {
            CargarClavesSSA_Estado(myNodeSeleccionado); 
        }

        private void btnClaves_BuscarClaves_Click(object sender, EventArgs e)
        {
            B = new FrmCriterioDeBusqueda();
            string sCriterio = B.MostarCriterio();

            CargarClavesSSA_Estado(myNodeSeleccionado, sCriterio); 
        }

        private void btnCargarProductosAsigandos_Click(object sender, EventArgs e)
        {
            CargarProductos_ClavesSSA(myNodeSeleccionado); 
        }

        private void btnCargarProductosRelacionados_Click(object sender, EventArgs e)
        {

        }
        #endregion Menus

        #region Treeview y Listview 
        private void twClaves_AfterSelect(object sender, TreeViewEventArgs e)
        {
            myNodeSeleccionado = e.Node;
            sClaveSSA_Seleccionada = ""; 

            if (twClaves.Nodes.Count > 0)
            {
                ActualizaMenuClaves(e.Node.Tag.ToString()); 
            }
        }

        private void ActualizaMenuClaves(string Tag)
        {
            mnClaves.Items[btnClaves_CargarLista.Name].Enabled = false;
            mnClaves.Items[btnClaves_BuscarClaves.Name].Enabled = false;
            mnClaves.Items[btnCargarProductosAsigandos.Name].Enabled = false;
            mnClaves.Items[btnCargarProductosRelacionados.Name].Enabled = false;

            if (Tag.Substring(0, 2) == "-1")
            {
                mnClaves.Items[btnClaves_CargarLista.Name].Enabled = true;
                mnClaves.Items[btnClaves_BuscarClaves.Name].Enabled = true;
            }

            if (Tag.Substring(0, 3) == "||x")
            {
                sClaveSSA_Seleccionada = Fg.Mid(Tag.ToString(), 4) ; 
                mnClaves.Items[btnCargarProductosAsigandos.Name].Enabled = true;
                mnClaves.Items[btnCargarProductosRelacionados.Name].Enabled = true;
            }            
        }
        #endregion Treeview y Listview 
    }
}
