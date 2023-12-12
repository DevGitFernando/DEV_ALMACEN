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

namespace DllFarmaciaSoft.Usuarios_y_Permisos_Clientes
{
    public partial class Frm_Ctes_GruposPermisos : FrmBaseExt
    {
        //private struct FormasArbol
        //{
        //    public string Rama;
        //    public string Forma;
        //    public int TipoRama;
        //    public string NameSpace;
        //}

        clsRamaArbolNavegacion RamaSeleccionada;
        //FormasArbol[] Formas = new FormasArbol[1];
        //basGenerales Fg = new basGenerales();
        clsGuardarSC guardarDatos = new clsGuardarSC();
        clsConsultasSC query;
        DataSet dtsDatos = new DataSet(), dtsPermisos = new DataSet();
        DataSet dtsGrupos = new DataSet(), dtsUsuariosGrupo = new DataSet();
        DataSet dtsFarmacias = new DataSet();

        TreeNode myNodeSeleccionado;
        TreeNode myNodePadreSelGrupos, myNodeSelGrupos;


        int iIndexNodo = 0;
        string sArbolSel = "";

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsConsultas leer = new clsConsultas(General.DatosConexion, "Configuracion", "GruposDeUsuarios", Application.ProductVersion, true);
        clsLeer reader;

        public Frm_Ctes_GruposPermisos()
        {
            InitializeComponent();
        }

        private void FrmGruposUsuarios_Load(object sender, EventArgs e)
        {
            query = new clsConsultasSC();
            reader = new clsLeer(ref cnn);
            CargarEstados();

            CargarArboles();
            //CargarGrupos();
        }

        private void CargarEstados()
        {
            string sSql = " Select Distinct IdEstado, (IdEstado + ' -- ' + Estado ) Estado " + 
                " From vw_Farmacias_Urls (NoLock) Order by IdEstado ";
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
                    cboEstados.Add(reader.DataSetClase, true, "IdEstado", "Estado");
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
                cboSucursales.Add(dtsFarmacias.Tables[0].Select("IdEstado = '" + cboEstados.Data + "'"), true, "IdFarmacia", "NombreDeFarmacia");
    
            cboSucursales.SelectedIndex = 0;
        }

        private void CargarArboles()
        {
            string sSql = 
                " Select Arbol, Nombre, Keyx From Net_Arboles (nolock) " + 
                " Where Arbol in ( 'CTER', 'CTRW', 'CTEU' )  Order by Nombre";
            //dtsDatos = query.Arboles_Administrativos();
            
            if (!reader.Exec(sSql))
            {
                Error.GrabarError(reader, "CargarArboles()");
                General.msjError("Ocurrió un error al obtener el listado del menú."); 
            }
            else 
            {
                cboArboles.Clear();
                cboArboles.Add("0", "<< Seleccione >>");

                if (reader.Leer())
                {
                    cboArboles.Add(reader.DataSetClase, true, "Arbol", "Nombre");
                }
                
                cboArboles.SelectedIndex = 0;
            }
        }

        private void CargarGrupos()
        {
            string sSql = string.Format("Select * From Net_CTE_Grupos_De_Usuarios (noLock) " + 
                "Where IdEstado = '{0}' and IdFarmacia = '{1}' ", cboEstados.Data, cboSucursales.Data);

            if (reader.Exec(sSql))
                dtsGrupos = reader.DataSetClase;

            //dtsGrupos = query.Grupos(General.EntidadConectada);

            string sIdGrupo = "", sNombreGrupo = ""; //, sUsuario = "";
            twGruposPermisos.Nodes.Clear();
            twGruposPermisos.BeginUpdate();

            TreeNode myNode;
            twGruposPermisos.Nodes.Clear();
            myNode = twGruposPermisos.Nodes.Add("Grupos");
            myNode.Tag = "-1";
            myNode.ImageIndex = 1;
            myNode.SelectedImageIndex = 1;

            if (reader.Leer())
            {
                // Cargar los permisos por usuario
                //dtsPermisos = query.GruposPrivilegios(General.EntidadConectada);
                sSql = string.Format(" Select G.IdFarmacia, G.IdGrupo, G.Arbol, G.Rama," +
                                        " ( Select Top 1 Nombre From Net_Navegacion N (NoLock) Where N.Arbol = G.Arbol and N.Rama = G.Rama ) as Ruta, " +
                                        " G.Ruta as RutaCompleta, G.TipoRama " +
                                        " From Net_CTE_Privilegios_Grupo G (NoLock) " +
                                        " Where G.IdEstado = '{0}' and G.IdFarmacia = '{1}' and G.Status = 'A' " +
                                        " Order by G.Rama ", cboEstados.Data, cboSucursales.Data);
                //sSql = string.Format("Select * From Net_Grupos_De_Usuarios (noLock) Where IdEstado = '{0}' and IdSucursal = '{1}' ", cboEstados.Data, cboSucursales.Data);


                if (reader.Exec(sSql))
                    dtsPermisos = reader.DataSetClase;

                //if (reader.Leer())
                {
                    foreach (DataRow dt in dtsGrupos.Tables[0].Rows)
                    {
                        sIdGrupo = dt["IdGrupo"].ToString();
                        sNombreGrupo = dt["NombreGrupo"].ToString();

                        TreeNode myNodeGrupo = myNode.Nodes.Add(myNode.FullPath + "|" + sNombreGrupo, sNombreGrupo);
                        myNodeGrupo.ImageIndex = 1;
                        myNodeGrupo.SelectedImageIndex = 1;
                        myNodeGrupo.Tag = "||" + sIdGrupo;

                        DataRow[] dtRows = dtsPermisos.Tables[0].Select("IdGrupo = '" + sIdGrupo + "'");
                        foreach (DataRow dtU in dtRows)
                        {
                            //sUsuario = dtU["LoginUser"].ToString();
                            TreeNode myNodeGrupoUsuario = myNodeGrupo.Nodes.Add(dtU["Ruta"].ToString());
                            myNodeGrupoUsuario.ImageIndex = TipoDeRama(dtU["TipoRama"].ToString());
                            myNodeGrupoUsuario.SelectedImageIndex = TipoDeRama(dtU["TipoRama"].ToString());
                            myNodeGrupoUsuario.Tag = ">" + dtU["Arbol"].ToString() + "||" + sIdGrupo + "||" + dtU["Rama"].ToString();

                        }
                    }
                }
            }
            twGruposPermisos.EndUpdate();
            myNode.Expand();
        }

        private int TipoDeRama(string TipoRama)
        {
            int iRegresa = 0;

            if (TipoRama == "1" || TipoRama == "2")
                iRegresa = 2;
            else
                iRegresa = 3;

            return iRegresa;
        }

        private void CargarArbolNavegacion()
        {
            clsArbolNavegacion myArbol = new clsArbolNavegacion("Kiubo");
            clsRamaArbolNavegacion myRamaArbol;
            //DataColumn myDataColumn;
            TreeNode myNode;
            clsLeer leer = new clsLeer(); 

            int vlNodos = 0;
            string sArbol = cboArboles.Data;
            string sRaiz = cboArboles.Text;
            string sRutaRaiz = "";
            sArbolSel = sArbol;


            dtsPermisos = query.ArbolNavegacion(sArbol);
            leer.DataSetClase = dtsPermisos;

            twNavegador.Nodes.Clear();
            twNavegador.BeginUpdate();
            twNavegador.Nodes.Clear();
            myNode = twNavegador.Nodes.Add(sRaiz);
            myNode.Tag = "1";
            myNode.ImageIndex = 0;
            myNode.SelectedImageIndex = 0;



            if (query.ExistenDatos)
            {
                //Formas = new FormasArbol[dtsPermisos.Tables["Arbol"].Rows.Count];

                //foreach (DataRow rw in dtsPermisos.Tables["Arbol"].Rows)
                while(leer.Leer()) 
                {
                    myRamaArbol = new clsRamaArbolNavegacion();

                    ////myRamaArbol.Arbol = rw["Arbol"].ToString();
                    ////myRamaArbol.Rama = Fg.Val(rw["RAMA"].ToString());
                    ////myRamaArbol.Nombre = rw["NOMBRE"].ToString();
                    ////myRamaArbol.Padre = Fg.Val(rw["PADRE"].ToString());
                    ////myRamaArbol.Forma = rw["FORMALOAD"].ToString();
                    ////myRamaArbol.myNameSpace = rw["GrupoOpciones"].ToString();
                    ////myRamaArbol.Orden = Fg.Val(rw["IDORDEN"].ToString());
                    ////myRamaArbol.TipoRama = rw["TipoRama"].ToString();
                    ////myRamaArbol.RutaCompleta = rw["RutaCompleta"].ToString();

                    myRamaArbol.Arbol = leer.Campo("Arbol");
                    myRamaArbol.Rama = leer.CampoInt("RAMA");
                    myRamaArbol.Nombre = leer.Campo("NOMBRE");
                    myRamaArbol.Padre = leer.CampoInt("PADRE");
                    myRamaArbol.Forma = leer.Campo("FORMALOAD");
                    myRamaArbol.myNameSpace = leer.Campo("GrupoOpciones");
                    myRamaArbol.Orden = leer.CampoInt("IDORDEN");
                    myRamaArbol.TipoRama = leer.Campo("TipoRama");
                    myRamaArbol.RutaCompleta = leer.Campo("RutaCompleta");
                    myRamaArbol.Status = leer.Campo("Status"); 

                    if (vlNodos == 0)
                    {
                        myRamaArbol.Nombre = sRaiz;
                        RamaSeleccionada = myRamaArbol;
                    }

                    myArbol.Ramas.Add(myRamaArbol);
                    //Formas[vlNodos].Forma = myRamaArbol.Forma;
                    //Formas[vlNodos].Rama = myRamaArbol.Rama.ToString();
                    //Formas[vlNodos].TipoRama = Fg.Val(rw["TipoRama"].ToString());
                    //Formas[vlNodos].NameSpace = myRamaArbol.myNameSpace.ToString();
                    vlNodos += 1;
                }

                // Obtener el nombre del arbol
                leer.DataSetClase = dtsPermisos;
                if (leer.Leer()) 
                {
                    // sRaiz = dtsPermisos.Tables["Arbol"].Rows[0]["Nombre"].ToString();
                    // sRutaRaiz = dtsPermisos.Tables["Arbol"].Rows[0]["RutaCompleta"].ToString();

                    sRaiz = leer.Campo("Nombre");
                    sRutaRaiz = leer.Campo("RutaCompleta"); 

                    // Asignar el nombre del arbol
                    myArbol.Nombre = sRaiz;
                    AgregarRamasHijas(myNode, myArbol, "", true);

                    myNode.ImageIndex = 0;
                    myNode.SelectedImageIndex = 0;
                    myNode.Tag = RamaSeleccionada;
                }
            }
            twNavegador.EndUpdate();
            myNode.Expand();
        }

        private void AgregarRamasHijas(TreeNode NodoPadre, clsArbolNavegacion Arbol, string TipoRama, bool DefineImages)
        {
            //bool bNodoTerminal = true;
            string sPrefijo = ""; 

            foreach (clsRamaArbolNavegacion Rama in Arbol.Ramas)
            {
                sPrefijo = ""; 
                if (Rama.Padre.ToString() == NodoPadre.Tag.ToString())
                {
                    //bNodoTerminal = false;
                    if (Rama.Status == "C")
                    {
                        sPrefijo = "C - ";
                    }

                    TreeNode myNode = NodoPadre.Nodes.Add(sPrefijo + Rama.Nombre);
                    myNode.Tag = Rama.Rama.ToString();
                    AgregarRamasHijas(myNode, Arbol, Rama.TipoRama, DefineImages);
                    myNode.Tag = Rama;
                    // myNode.Tag = Rama.TipoRama.ToString();
                }
            }

            if (DefineImages)
            {
                if (TipoRama == "2" || TipoRama == "3")
                {
                    if (TipoRama == "2")
                    {
                        // Nodo
                        NodoPadre.ImageIndex = 1;
                        NodoPadre.SelectedImageIndex = 1;
                    }
                    else
                    {
                        // Hoja
                        NodoPadre.ImageIndex = 2;
                        NodoPadre.SelectedImageIndex = 2;
                    }
                }
            }
        }

        private void twNavegador_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                myNodeSeleccionado = e.Node;
                RamaSeleccionada = (clsRamaArbolNavegacion)myNodeSeleccionado.Tag;
                iIndexNodo = e.Node.Index;
            }
            catch { }
        }

        private void twGruposPermisos_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode NewNode, NodoPadre;
            string sIdGrupo = "";
            Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
            NewNode = twGruposPermisos.GetNodeAt(pt);

            sIdGrupo = NewNode.Tag.ToString();
            if (sIdGrupo.Substring(0, 2) == "||")
            {
                if ( ! ExisteOpcion(NewNode, RamaSeleccionada.Nombre) )
                {
                    sIdGrupo = sIdGrupo.Replace("|", "");
                    if (InsertarPermisos(sIdGrupo, myNodeSeleccionado))
                    {
                        // El nodo seleccionado es un grupo
                        TreeNode myNodeRama = NewNode.Nodes.Add(NewNode.FullPath + "|" + RamaSeleccionada.Nombre, RamaSeleccionada.Nombre);
                        myNodeRama.ImageIndex = 0;
                        myNodeRama.SelectedImageIndex = 0;
                        myNodeRama.Tag = RamaSeleccionada;
                        AsignarIcono(myNodeRama, RamaSeleccionada.TipoRama);
                    }
                }
                NewNode.Expand();
            }
            else
            {
                NodoPadre = NewNode.Parent;
                sIdGrupo = NodoPadre.Tag.ToString();

                if (sIdGrupo.Substring(0, 2) == "||" || sIdGrupo.Substring(0, 1) == ">")
                {
                    if (!ExisteOpcion(NodoPadre, RamaSeleccionada.Nombre))
                    {
                        sIdGrupo = sIdGrupo.Replace(">", "");
                        sIdGrupo = sIdGrupo.Replace("|", "");
                        if (InsertarPermisos(sIdGrupo, myNodeSeleccionado))
                        {
                            // El nodo seleccionado es un grupo
                            TreeNode myNodeRama = NodoPadre.Nodes.Add(NodoPadre.FullPath + "|" +RamaSeleccionada.Nombre, RamaSeleccionada.Nombre);
                            myNodeRama.ImageIndex = 0;
                            myNodeRama.SelectedImageIndex = 0;
                            myNodeRama.Tag = RamaSeleccionada;
                            AsignarIcono(myNodeRama, RamaSeleccionada.TipoRama);
                        }
                    }
                    NewNode.Expand();
                }

            }

        }

        private void AsignarIcono(TreeNode myNode, string TipoRama)
        {
            if (TipoRama == "1" || TipoRama == "2")
            {
                // Nodo
                myNode.ImageIndex = 2;
                myNode.SelectedImageIndex = 2;
            }
            else
            {
                // Hoja
                myNode.ImageIndex = 3;
                myNode.SelectedImageIndex = 3;
            }
        }

        private bool InsertarPermisos(string IdGrupo, TreeNode Nodo )
        {
            bool bRegresa = false;
            clsRamaArbolNavegacion Rama = (clsRamaArbolNavegacion)Nodo.Tag;
            string sQuery = "";

            //sQuery = "Insert Into Net_Privilegios_Grupo ( IdEstado, IdSucursal, IdGrupo, Arbol, Ruta, TipoRama, Rama, RutaCompleta  ) " +
            //    " values ( '" + cboSucursales.Data + "', '" + IdGrupo + "', '" + sArbolSel + "', '" + Nodo.FullPath + "', '" + Rama.TipoRama +  "', '" + Rama.Rama.ToString() + "', '" + Rama.RutaCompleta + "' ) ";

            //sQuery = string.Format("Insert Into Net_Privilegios_Grupo ( IdEstado, IdSucursal, IdGrupo, Arbol, Ruta, TipoRama, Rama, RutaCompleta ) " +
            //    " values ( '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}' ) ", cboEstados.Data, cboSucursales.Data, IdGrupo, sArbolSel, Nodo.FullPath, Rama.TipoRama, Rama.Rama.ToString(), Rama.RutaCompleta);

            sQuery = string.Format(" Exec spp_Mtto_Net_CTE_Privilegios_Grupo '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', 'A' ",
                cboEstados.Data, cboSucursales.Data, IdGrupo, sArbolSel, Nodo.FullPath, Rama.TipoRama, Rama.Rama.ToString(), Rama.RutaCompleta); 

            bRegresa = reader.Exec(sQuery);

            return bRegresa;
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

        private bool ExisteOpcion(TreeNode myNodeRama, clsRamaArbolNavegacion Rama)
        {
            bool bRegresa = false;
            string sRutaNodo = "", sRutaRama = "";
            clsRamaArbolNavegacion myRamaBuscar;
            sRutaRama = Rama.Arbol.ToUpper() + "|" + Rama.RutaCompleta.ToUpper();
            sRutaRama = sRutaRama.Replace(">", "");

            foreach (TreeNode Nodo in myNodeRama.Nodes)
            {
                try
                {
                    myRamaBuscar = (clsRamaArbolNavegacion)Nodo.Tag;
                    sRutaNodo = myRamaBuscar.Arbol.ToUpper() + "|" + myRamaBuscar.RutaCompleta.ToUpper();
                }
                catch
                {
                    sRutaNodo = Nodo.Tag.ToString().Replace(">", "");
                    sRutaNodo = sRutaNodo.Replace("||", "|");
                }

                if (sRutaNodo == sRutaRama)
                {
                    bRegresa = true;
                    break;
                }
                         
            }
            return bRegresa;
        }

        private void twGruposPermisos_DragOver(object sender, DragEventArgs e)
        {
            //MessageBox.Show(e.Data.ToString());
        }

        private void twNavegador_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void twNavegador_DragDrop(object sender, DragEventArgs e)
        {
        }

        private void twNavegador_DragOver(object sender, DragEventArgs e)
        {
        }

        private void twGruposPermisos_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void twGruposPermisos_AfterSelect(object sender, TreeViewEventArgs e)
        {
            eliminarToolStripMenuItem.Text = "Eliminar opción";
            eliminarToolStripMenuItem.Visible = true;

            try
            {
                myNodeSelGrupos = e.Node;
                myNodePadreSelGrupos = e.Node.Parent;
            }
            catch 
            {
                myNodePadreSelGrupos = e.Node.Parent;
            }

            try
            {
                string sTag = myNodeSelGrupos.Tag.ToString();

                if (sTag.Contains("-1"))
                {
                    eliminarToolStripMenuItem.Visible = false;
                }
                else if (myNodeSelGrupos.Nodes.Count > 0 || sTag.Contains("||"))
                    eliminarToolStripMenuItem.Text = "Eliminar opciónes de grupo"; 
            }
            catch { }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Borrar");
            string sDefault = " Set FechaUpdate = getdate(), Status = 'C', Actualizado = '0' "; 

            bool bRama = false;
            clsRamaArbolNavegacion Rama;
            TreeNode myNodoBorrar;

            try
            {
                string sQuery = "", sMyGrupo = myNodePadreSelGrupos.Tag.ToString();
                sMyGrupo = sMyGrupo.Replace("|", "");
                sMyGrupo = sMyGrupo.Replace(">", "");                

                try
                {
                    Rama = (clsRamaArbolNavegacion)myNodeSelGrupos.Tag;
                    myNodoBorrar = myNodeSelGrupos;
                    bRama = true;

                    ////sQuery = string.Format("Delete From Net_Privilegios_Grupo Where IdEstado = '{0}' and IdSucursal = '{1}' and IdGrupo = '{2}' and Arbol = '{3}' and Rama = '{4}' ",
                    ////    cboEstados.Data, cboSucursales.Data, sMyGrupo, Rama.Arbol, Rama.Rama);

                    sQuery = string.Format(" Update Net_CTE_Privilegios_Grupo {5} " +
                        " Where IdEstado = '{0}' and IdFarmacia = '{1}' and IdGrupo = '{2}' and Arbol = '{3}' and Rama = '{4}' ", 
                        cboEstados.Data, cboSucursales.Data, sMyGrupo, Rama.Arbol, Rama.Rama, sDefault ); 
                }
                catch
                {
                    myNodoBorrar = myNodeSelGrupos;
                    sMyGrupo = myNodeSelGrupos.Tag.ToString();
                    string sRama = "";
                    string []sPartes;

                    if (sMyGrupo.Substring(0, 1) == ">")
                    {
                        bRama = true;
                        sMyGrupo = sMyGrupo.Replace(">", "");
                        sMyGrupo = sMyGrupo.Substring(sMyGrupo.IndexOf('|'));
                        sMyGrupo = sMyGrupo.Replace("||", "|");
                        sMyGrupo = sMyGrupo.Substring(1);
                        sPartes = sMyGrupo.Split('|');

                        sMyGrupo = sPartes[0];
                        sRama = sPartes[1];

                        //sQuery = string.Format("Delete From Net_Privilegios_Grupo Where IdEstado = '{0}' and IdSucursal = '{1}' and IdGrupo = '{2}' and Rama = '{3}' ",
                        //    cboEstados.Data, cboSucursales.Data, sMyGrupo, sRama);

                        sQuery = string.Format("Update Net_CTE_Privilegios_Grupo {4} " + 
                            " Where IdEstado = '{0}' and IdFarmacia = '{1}' and IdGrupo = '{2}' and Rama = '{3}' ",
                            cboEstados.Data, cboSucursales.Data, sMyGrupo, sRama, sDefault); 
                    }
                    else
                    {
                        sMyGrupo = sMyGrupo.Replace("|", "");

                        //sQuery = string.Format("Delete From Net_Privilegios_Grupo Where IdEstado = '{0}' and IdSucursal = '{1}' and IdGrupo = '{2}' ",
                        //    cboEstados.Data, cboSucursales.Data, sMyGrupo);

                        sQuery = string.Format("Update Net_CTE_Privilegios_Grupo {3} " +
                            " Where IdEstado = '{0}' and IdFarmacia = '{1}' and IdGrupo = '{2}' ",
                            cboEstados.Data, cboSucursales.Data, sMyGrupo, sDefault);
                    }
                }

                if (sMyGrupo != "-1")
                {
                    if (guardarDatos.Exec(sQuery)) 
                    {
                        if ( bRama )
                            twGruposPermisos.Nodes.Remove(myNodeSelGrupos);
                        else
                        {
                            myNodeSelGrupos.Nodes.Clear();
                        }
                    }
                }
            }
            catch { }
        }

        private void actualizarPermisosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CargarGrupos();
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarFarmacias();
        }

        private void cboSucursales_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSucursales.SelectedIndex != 0)
            {
                CargarGrupos();
            }
            else
            {
                twGruposPermisos.Nodes.Clear();
            }
        }

        private void cboArboles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboArboles.SelectedIndex != 0)
            {
                CargarArbolNavegacion();
            }
            else
            {
                twNavegador.Nodes.Clear();
            }
        }
    }
}
