﻿using System;
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
    public partial class FrmGrupos__Ubicaciones : FrmBaseExt
    {
        clsConsultas query;
        DataSet dtsDatos = new DataSet();
        DataSet dtsGrupos = new DataSet(), dtsUsuariosGrupo = new DataSet();
        DataSet dtsFarmacias = new DataSet();

        TreeNode myNodeSeleccionado; //, myNodoUsuario;
        int iIndexNodo = 0;
        string sIdUbicacion = "";
        string sIdGrupo = "";
        string sDescripcion = "";

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        //clsConsultas leer = new clsConsultas(General.DatosConexion, "Configuracion", "GruposDeUsuarios", Application.ProductVersion, true);
        clsLeer leer;


        public FrmGrupos__Ubicaciones()
        {
            InitializeComponent();
        }

        private void FrmRotacion_Claves_Load(object sender, EventArgs e)
        {
            query = new clsConsultas(General.DatosConexion, General.DatosApp, "FrmRotacion_Claves"); ;
            leer = new clsLeer(ref cnn);

            CargarGrupos();
            Cargar_Ubicaciones();
            //ActualizaMenu("0");
        }

        private void CargarGrupos()
        {
            string sSql = string.Format(" Select IdGrupo, NombreGrupo, " +
                                " (IdGrupo + ' - ' + NombreGrupo  + (Case When status <> 'A' Then ' (Cancelado)' Else '' End))  As Descripcion " +
                          " From CFGC_ALMN__GruposDeUbicaciones (NoLock) Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And IdGrupo > '000' " +
                          " Order By IdGrupo",
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
                    sIdGrupo = dt["IdGrupo"].ToString();
                    sNombreGrupo = dt["Descripcion"].ToString();

                    TreeNode myNodeGrupo = myNode.Nodes.Add(sNombreGrupo);
                    myNodeGrupo.ImageIndex = 1;
                    myNodeGrupo.SelectedImageIndex = 1;
                    myNodeGrupo.Tag = sIdGrupo;


                    string sQuery = string.Format("Select C.* From CFGC_ALMN__GruposDeUbicaciones_Det C (NoLock) " +
                                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And IdGrupo = '{3}' And C.Status = 'A'",
                                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sIdGrupo);

                    leer.Exec(sQuery);

                    if (leer.Leer())
                    {
                        dtsUsuariosGrupo = leer.DataSetClase;
                        foreach (DataRow dtU in dtsUsuariosGrupo.Tables[0].Rows)
                        {
                            sDescripcion = dtU["IdPasillo"].ToString() + "|" + dtU["IdEstante"].ToString() + "|" + dtU["IdEntrepaño"].ToString();
                            TreeNode myNodeGrupoUsuario = myNodeGrupo.Nodes.Add(sDescripcion);
                            myNodeGrupoUsuario.ImageIndex = 0;
                            myNodeGrupoUsuario.SelectedImageIndex = 0;
                            myNodeGrupoUsuario.Tag = dtU["IdPasillo"].ToString() + "|" + dtU["IdEstante"].ToString() + "|" + dtU["IdEntrepaño"].ToString();

                        }
                    }
                }
            }

            twGrupos.EndUpdate();
            myNode.Expand();

            twGrupos.Nodes[0].Text = twGrupos.Nodes[0].Text;

        }

        private void Cargar_Ubicaciones()
        {
            string sSql = string.Format("Select C.* From CatPasillos_Estantes_Entrepaños C (NoLock) " +
                    "Left Join CFG_ALMN_Ubicaciones_Estandar E (NoLock) " +
                    "    On(C.IdEmpresa = E.IdEmpresa And C.IdEstado = E.IdEstado And C.IdFarmacia = E.IdFarmacia And " +
                    "       C.IdPasillo = E.IdRack And C.IdEstante = E.IdNivel And C.IdEntrepaño = E.IdEntrepaño) "  +
                    "Where C.IdEmpresa = '{0}' And C.IdEstado = '{1}' And C.IdFarmacia = '{2}' And C.Status = 'A' And E.IdEmpresa Is null " +
                    "Order By C.IdPasillo",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

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
                NewColListView = lwUsuarios.Columns.Add("Ubicacion", 290);

                foreach (DataRow dt in dtsDatos.Tables[0].Rows)
                {
                    strValor = dt["IdPasillo"].ToString() + "|" + dt["IdEstante"].ToString() + "|" + dt["IdEntrepaño"].ToString();
                    itmX = lwUsuarios.Items.Add(strValor, 0);
                    itmX.SubItems.Add("" + strValor);
                    itmX.SubItems[0].Tag = dt["IdPasillo"].ToString() + "|" + dt["IdEstante"].ToString() + "|" + dt["IdEntrepaño"].ToString();
                }
            }
        }

        private void lwUsuarios_ItemDrag(object sender, ItemDragEventArgs e)
        {
            sIdUbicacion = lwUsuarios.FocusedItem.SubItems[0].Tag.ToString();
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
                    sIdUbicacion = myNodeSeleccionado.Tag.ToString().Replace("|", "");
                }

            }
        }

        private void twGrupos_DragDrop(object sender, DragEventArgs e)
        {
            string[] sArreglo;
            string sIdPasillo, sIdEstante, sIdEntrepaño;
            TreeNode NewNode; //, NodoPadre;
            //string sIdGrupo = "";
            Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
            NewNode = twGrupos.GetNodeAt(pt);
            sIdGrupo = NewNode.Tag.ToString();

            sArreglo = sIdUbicacion.Split('|');

            sIdPasillo = sArreglo[0];
            sIdEstante = sArreglo[1];
            sIdEntrepaño = sArreglo[2];

            string sSql = string.Format("Exec spp_CFGC_ALMN__GruposDeUbicaciones_Det @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdGrupo = '{3}', @sIdPasillo = {4}, @sIdEstante = {5}, @sIdEntrepaño = {6}",
                    sEmpresa, sEstado, sFarmacia, sIdGrupo, sIdPasillo, sIdEstante, sIdEntrepaño);

            if (!ExisteOpcion(NewNode, sIdUbicacion))
            {
                //CargarGrupos();
                if (leer.Exec(sSql))
                {
                    TreeNode myNodeRama = NewNode.Nodes.Add(sDescripcion);
                    myNodeRama.ImageIndex = 0;
                    myNodeRama.SelectedImageIndex = 0;
                    myNodeRama.Tag = sIdUbicacion;
                    // AsignarIcono(myNodeRama, RamaSeleccionada.TipoRama);
                    //Cargar_Ubicaciones();
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
            string[] sArreglo;
            string sIdPasillo, sIdEstante, sIdEntrepaño;

            try
            {
                if (myNodeSeleccionado.Tag.ToString() != "-1")
                {
                    sArreglo = myNodeSeleccionado.Tag.ToString().Split('|');

                    sIdPasillo = sArreglo[0];
                    sIdEstante = sArreglo[1];
                    sIdEntrepaño = sArreglo[2];

                    string sSql = string.Format("Exec spp_CFGC_ALMN__GruposDeUbicaciones_Det @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdGrupo = '{3}', @sIdPasillo = {4}, @sIdEstante = {5}, @sIdEntrepaño = {6},  @iOpcion = {7}",
                                    sEmpresa, sEstado, sFarmacia, sIdGrupo, sIdPasillo, sIdEstante, sIdEntrepaño, 2);

                    if (leer.Exec(sSql))
                        twGrupos.Nodes.Remove(myNodeSeleccionado);

                    CargarGrupos();
                    Cargar_Ubicaciones();
                }
            }
            catch { }
        }
    }
}
