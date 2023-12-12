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
using SC_SolutionsSystem.FuncionesGrid;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Usuarios_y_Permisos;

namespace OficinaCentral.CuadrosBasicos
{
    public partial class FrmCB_ClavesExclusionPedidos : FrmBaseExt
    {        
        clsConsultas query;
        clsAyudas ayuda;
        DataSet dtsDatos = new DataSet();
        DataSet dtsGrupos = new DataSet(), dtsUsuariosGrupo = new DataSet();
        DataSet dtsClientes = new DataSet();
        DataSet dtsSubClientes = new DataSet();       
       
        
        string sPermisoPerfiles = "MODIFICAR_PERFILES";
        bool bPermisoPerfiles = false;
        string sValorGrid = "";

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        
        clsLeer reader, reader2, leer;
        clsGrid grid;        

        private enum Cols
        {
            Ninguna = 0,
            IdClaveSSA = 1, ClaveSSA = 2, Descripcion = 3, Tipo = 4, Bandera = 5, Excluir = 6
        }

        public FrmCB_ClavesExclusionPedidos()
        {
            InitializeComponent();

            grid = new clsGrid(ref grdClaves, this);
            grid.EstiloGrid(eModoGrid.ModoRow);
            grdClaves.EditModeReplace = true;
            grid.AjustarAnchoColumnasAutomatico = true; 

            grid.SetOrder((int)Cols.ClaveSSA, 1, true);
            grid.SetOrder((int)Cols.Descripcion, 1, true);
            grid.SetOrder((int)Cols.Tipo, 1, true);
            grid.SetOrder((int)Cols.Excluir, 1, true);

            //grid.ResizeColumns = true;
        }

        private void FrmGruposUsuarios_Load(object sender, EventArgs e)
        {
            query = new clsConsultas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
            ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
            leer = new clsLeer(ref cnn);
            reader = new clsLeer(ref cnn);
            reader2 = new clsLeer(ref cnn);

            btnNuevo_Click(null, null);
            //Inicializa();
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

        #region Combos 
        private void CargarEstados()
        {
            string sSql = " Select Distinct IdEstado, Estado, ( IdEstado + ' - ' + Estado ) as Descripcion From vw_Farmacias_Urls (NoLock) Order by IdEstado ";
            cboEstados.Clear();
            cboEstados.Add();

            if (!DtGeneral.EsAdministrador)
            {
                sSql = string.Format(" Select Distinct IdEstado, Estado, ( IdEstado + ' - ' + Estado ) as Descripcion From vw_Farmacias_Urls (NoLock) " +
                    " Where IdEstado = '{0}' Order by IdEstado ", DtGeneral.EstadoConectado);
            }

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

            // SE OBTIENE LOS SUB-CLIENTES
            reader.Exec("Select Distinct IdEstado, IdCliente, IdSubCliente, (IdSubCliente + ' - ' +NombreSubCliente) as NombreSubCliente From vw_Claves_Precios_Asignados (NoLock) Order By IdEstado, IdCliente, IdSubCliente ");
            dtsSubClientes = reader.DataSetClase;

            cboEstados.SelectedIndex = 0;
            if (!DtGeneral.EsAdministrador)
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

        private void CargarSubClientes()
        {
            cboSubClientes.Clear();
            cboSubClientes.Add("0", "<< Seleccione >>");

            if (cboClientes.SelectedIndex != 0)
            {
                try
                {
                    cboSubClientes.Add(dtsSubClientes.Tables[0].Select("IdEstado = '" + cboEstados.Data + "'" + " And IdCliente = '" + cboClientes.Data + "'"), true, "IdSubCliente", "NombreSubCliente");
                }
                catch { }
            }
            cboSubClientes.SelectedIndex = 0;
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboClientes.SelectedIndex = 0;
            cboSubClientes.SelectedIndex = 0;

            CargarClientes();
        }

        private void cboClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboSubClientes.SelectedIndex = 0;
            if (cboClientes.SelectedIndex != 0)
            {
                CargarSubClientes();
            }
        }

        private void cboSubClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////if (cboSubClientes.SelectedIndex == 0)
            ////{             
            ////    ////btnGuardar.Enabled = false;
            ////}
            grdClaves.Focus();
            grid.SetActiveCell(1, (int)Cols.ClaveSSA);
            btnGuardar.Enabled = true;
        }
        
        #endregion Combos 

        #region Funciones
        private void Inicializa()
        {
            Fg.IniciaControles(this, true);
            grid.Limpiar(true);

            btnGuardar.Enabled = false;

            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");

            cboClientes.Clear();
            cboClientes.Add("0", "<< Seleccione >>");

            cboSubClientes.Clear();
            cboSubClientes.Add("0", "<< Seleccione >>");

            SolicitarPermisosUsuario();
            CargarEstados();           

            cboEstados.SelectedIndex = 0;
            cboClientes.SelectedIndex = 0;
            cboSubClientes.SelectedIndex = 0;

            cboEstados.Focus();
        }        

        private void CargarClaves(string Criterio)
        {
            Criterio = Criterio.Replace(" ", "%"); 

            string sSql = string.Format(
                " Select C.IdClaveSSA_Sal, C.ClaveSSA, C.DescripcionClave, C.TipoDeClaveDescripcion,  " +
                " cast((IsNull(V.ExcluirDePedido, 0)) as Int) as Bandera, IsNull(V.ExcluirDePedido, 0) as ExcluirPedido  " +
                " From vw_ClavesSSA_Sales C (NoLock)  " + 
                " Right Join CFG_Claves_Excluir_Generacion_Pedidos V (NoLock)  " + 
                    " On ( C.IdClaveSSA_Sal = V.IdClaveSSA_Sal AND C.ClaveSSA = V.ClaveSSA )  " +
                " Where V.IdEstado = '{0}' And V.IdCliente = '{1}' And V.IdSubCliente = '{2}'   " + 
                " Order By C.DescripcionClave ", cboEstados.Data, cboClientes.Data, cboSubClientes.Data);

            grid.Limpiar(false);

            if (!reader.Exec(sSql))
            {
                Error.GrabarError(reader, "CargarClaves()");
                General.msjError("Ocurrió un error al obtener las Claves del Cliente.");
            }
            else
            {
                if (reader.Leer())
                {
                    grid.LlenarGrid(reader.DataSetClase);
                    btnGuardar.Enabled = true;                    
                }
            }
        }        
        #endregion Funciones       

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Inicializa();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidaDatos())
            {
                if(!cnn.Abrir())
                {
                    General.msjErrorAlAbrirConexion(); 
                }
                else 
                {
                    bool bExito = false;                    
                    cnn.IniciarTransaccion();

                    bExito = Guarda_Informacion();

                    if (!bExito)
                    {
                        cnn.DeshacerTransaccion();                       
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al grabar la información.");                        
                    }
                    else
                    {
                        cnn.CompletarTransaccion();
                        General.msjUser("La Información se guardó satisfactoriamente...");
                        btnNuevo_Click(null, null);
                    }

                    cnn.Cerrar();
                }
            }
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (ValidaDatos())
            {
                CargarClaves("");
            }
        }
        #endregion Botones

        #region Guardar_Informacion
        private bool Guarda_Informacion()
        {
            bool bRegresa = true;
            string sSql = "", sIdClaveSSA = "", sClaveSSA = "";
            int iExcluir = 0, iBandera = 0;

            for (int i = 1; i <= grid.Rows; i++)
            {
                sIdClaveSSA = grid.GetValue(i, (int)Cols.IdClaveSSA);
                sClaveSSA = grid.GetValue(i, (int)Cols.ClaveSSA);
                iExcluir = grid.GetValueInt(i, (int)Cols.Excluir);
                iBandera = grid.GetValueInt(i, (int)Cols.Bandera);

                if (iExcluir != iBandera)
                {
                    sSql = string.Format("Exec spp_Mtto_CFG_Claves_Excluir_Generacion_Pedidos \n" +
                        "\t@IdEstado = '{0}', @IdCliente = '{1}', @IdSubCliente = '{2}', @IdClaveSSA_Sal = '{3}',\n" +
                        "\t@ClaveSSA = '{4}', @ExcluirDePedido = '{5}' \n",
                         cboEstados.Data, cboClientes.Data, cboSubClientes.Data, sIdClaveSSA, sClaveSSA, iExcluir);

                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }

            return bRegresa;
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (cboEstados.SelectedIndex == 0)
            {
                General.msjAviso("No ha seleccionado el estado.. Verifique !!");
                bRegresa = false;
                cboEstados.Focus();
            }

            if (bRegresa && cboClientes.SelectedIndex == 0)
            {
                General.msjAviso("No ha seleccionado el Cliente.. Verifique !!");
                bRegresa = false;
                cboClientes.Focus();
            }

            if (bRegresa && cboSubClientes.SelectedIndex == 0)
            {
                General.msjAviso("No ha seleccionado el Sub-Cliente.. Verifique !!");
                bRegresa = false;
                cboSubClientes.Focus();
            }

            return bRegresa;
        }
        #endregion Guardar_Informacion                     

        #region Eventos_Grid
        private void grdClaves_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if ((grid.ActiveRow == grid.Rows) && e.AdvanceNext)
            {
                if (grid.GetValue(grid.ActiveRow, (int)Cols.ClaveSSA) != "" && grid.GetValue(grid.ActiveRow, (int)Cols.Descripcion) != "")
                {
                    grid.Rows = grid.Rows + 1;
                    grid.ActiveRow = grid.Rows;
                    grid.SetActiveCell(grid.Rows, (int)Cols.ClaveSSA);
                }
            }
        }

        private void grdClaves_EditModeOn(object sender, EventArgs e)
        {
            sValorGrid = grid.GetValue(grid.ActiveRow, (int)Cols.ClaveSSA);
        }

        private void grdClaves_EditModeOff(object sender, EventArgs e)
        {
            switch (grid.ActiveCol)
            {
                case (int)Cols.ClaveSSA:
                    {
                        ObtenerDatosClaveSSA();
                    }

                    break;                
            }
        }

        private void grdClaves_KeyDown(object sender, KeyEventArgs e)
        {
            if (grid.ActiveCol == (int)Cols.ClaveSSA)
            {
                if (e.KeyCode == Keys.F1)
                {
                    leer.DataSetClase = ayuda.ClavesSSA_Sales("grdClaves_KeyDown");
                    if (leer.Leer())
                    {
                        grid.SetValue(grid.ActiveRow, (int)Cols.ClaveSSA, leer.Campo("ClaveSSA"));
                        ObtenerDatosClaveSSA();
                    }

                }

                if (e.KeyCode == Keys.Delete)
                {
                    grid.DeleteRow(grid.ActiveRow);

                    if (grid.Rows == 0)
                        grid.Limpiar(true);
                }
            }
        }

        private void limpiarColumnas()
        {
            for (int i = 2; i <= grid.Columns; i++) //Columnas. Nota: Inicia a partir de la 2da.
            {
                grid.SetValue(grid.ActiveRow, i, "");
            }
        }

        private void chkTodas_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void EliminarRenglonesVacios()
        {
            for (int i = 1; i <= grid.Rows; i++) //Renglones.
            {
                if (grid.GetValue(i, 2).Trim() == "") //Si la columna oculta IdProducto esta vacia se elimina.
                    grid.DeleteRow(i);
            }

            if (grid.Rows == 0) // Si No existen renglones, se inserta 1.
                grid.AddRow();
        }

        private void ObtenerDatosClaveSSA()
        {
            string sCodigo = ""; // , sSql = "";            

            sCodigo = grid.GetValue(grid.ActiveRow, (int)Cols.ClaveSSA);

            if (sCodigo.Trim() == "")
            {
                General.msjUser("Clave no encontrada.");
                grid.LimpiarRenglon(grid.ActiveRow);
            }
            else
            {

                leer.DataSetClase = query.ClavesSSA_Sales(sCodigo, true, "ObtenerDatosClaveSSA");
                if (!leer.Leer())
                {
                    //General.msjUser("Producto no encontrado ó no esta Asignado al Proveedor.");
                    grid.LimpiarRenglon(grid.ActiveRow);
                }
                else
                {
                    CargaDatosProducto();
                }                
            }

        }

        private void CargaDatosProducto()
        {
            int iRowActivo = grid.ActiveRow;

            if (!grid.BuscaRepetido(leer.Campo("CodigoEAN"), iRowActivo, 1))
                {
                    grid.SetValue(iRowActivo, (int)Cols.IdClaveSSA, leer.Campo("IdClaveSSA_Sal"));
                    grid.SetValue(iRowActivo, (int)Cols.ClaveSSA, leer.Campo("ClaveSSA"));
                    grid.SetValue(iRowActivo, (int)Cols.Descripcion, leer.Campo("DescripcionClave"));
                    grid.SetValue(iRowActivo, (int)Cols.Tipo, leer.Campo("TipoDeClaveDescripcion"));
                    grid.SetValue(iRowActivo, (int)Cols.Bandera, 0);
                    grid.SetValue(iRowActivo, (int)Cols.Excluir, 0);

                    grid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.ClaveSSA);
                    grid.SetActiveCell(iRowActivo, (int)Cols.Excluir);
                }
                else
                {
                    General.msjUser("Este Clave SSA ya se encuentra capturada en otro renglon.");
                    grid.SetValue(grid.ActiveRow, (int)Cols.Excluir, 0);
                    limpiarColumnas();
                    grid.SetActiveCell(grid.ActiveRow, (int)Cols.ClaveSSA);
                }

            
        }
        #endregion Eventos_Grid        

    }//LLAVES DE LA CLASE
}
