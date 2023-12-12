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
    public partial class FrmSubCuadroBasico : FrmBaseExt
    {        
        clsConsultas query;
        DataSet dtsDatos = new DataSet();
        DataSet dtsClientes = new DataSet();
               
        string sPermisoPerfiles = "MODIFICAR_PERFILES";
        bool bPermisoPerfiles = false;

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);        
        clsLeer reader, reader2, leer;
        clsGrid grid;
        
        private enum Cols
        {
            Ninguna = 0,
            IdClaveSSA = 1, ClaveSSA = 2, Descripcion = 3, Cantidad = 4, Bandera = 5, SubCB = 6
        }

        public FrmSubCuadroBasico()
        {
            InitializeComponent();

            query = new clsConsultas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
            leer = new clsLeer(ref cnn);
            reader = new clsLeer(ref cnn);
            reader2 = new clsLeer(ref cnn);

            grid = new clsGrid(ref grdClaves, this);
            grid.EstiloGrid(eModoGrid.ModoRow);

            grid.SetOrder((int)Cols.ClaveSSA, 1, true);
            grid.SetOrder((int)Cols.Descripcion, 1, true);
            grid.SetOrder((int)Cols.Cantidad, 1, true);
            grid.SetOrder((int)Cols.SubCB, 1, true);

            //grid.ResizeColumns = true;
        }

        private void FrmGruposUsuarios_Load(object sender, EventArgs e)
        {   
            btnNuevo_Click(null, null);
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
            string sSql = " Select Distinct IdEstado, Estado From vw_Farmacias_Urls (NoLock) Order by IdEstado ";
            cboEstados.Clear();
            cboEstados.Add();

            if (!DtGeneral.EsAdministrador)
            {
                sSql = string.Format(" Select Distinct IdEstado, Estado From vw_Farmacias_Urls (NoLock) " +
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
                    cboEstados.Add(reader.DataSetClase, true, "IdEstado", "Estado");
                }
            }            

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

        private void CargarFarmacias()
        {
            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");

            string sSql = "";

            sSql = string.Format(" Select *, (IdFarmacia + ' -- ' + Farmacia) as NombreFarmacia From vw_Farmacias (nolock) " +
                                 " Where IdEstado = '{0}' and IdFarmacia <> '0001' and Status = 'A' ", cboEstados.Data );

            if (!reader.Exec(sSql))
            {
                General.msjError("Ocurrió un error al consultar los Programas");
                Error.GrabarError(reader, "CargarFarmacias");
            }
            else
            {
                if (reader.Leer())
                {
                    cboFarmacias.Add(reader.DataSetClase, true, "IdFarmacia", "NombreFarmacia");
                }
            }

            cboFarmacias.SelectedIndex = 0;
        }

        private void CargarClientes()
        {
            cboCliente.Clear();
            cboCliente.Add("0", "<< Seleccione >>");
            
            string sSql = "";

            sSql = string.Format(" Select Distinct IdCliente, Cliente From vw_CB_CuadroBasico_Farmacias (nolock) " +
                                 " Where IdEstado = '{0}' and IdFarmacia = '{1}' ", cboEstados.Data, cboFarmacias.Data);

            if (!leer.Exec(sSql))
            {
                General.msjError("Ocurrió un error al consultar los Clientes");
                Error.GrabarError(leer, "CargarClientes");
            }
            else
            {
                if (leer.Leer())
                {
                    cboCliente.Add(leer.DataSetClase, true, "IdCliente", "Cliente");
                }
            }
            cboCliente.SelectedIndex = 0;
            
        }

        private void CargarNiveles()
        {
            cboNivel.Clear();
            cboNivel.Add("0", "<< Seleccione >>");
            
            string sSql = "";

            sSql = string.Format(" Select Distinct IdNivel, Nivel From vw_CB_CuadroBasico_Farmacias (nolock) " +
                                 " Where IdEstado = '{0}' and IdFarmacia = '{1}' and IdCliente = '{2}' ", cboEstados.Data, cboFarmacias.Data, cboCliente.Data);

            if (!leer.Exec(sSql))
            {
                General.msjError("Ocurrió un error al consultar los Niveles");
                Error.GrabarError(leer, "CargarNiveles");
            }
            else
            {
                if (leer.Leer())
                {
                    cboNivel.Add(leer.DataSetClase, true, "IdNivel", "Nivel");
                }
            }
            
            cboNivel.SelectedIndex = 0;
            
        }

        private void CargarProgramas()
        {
            cboPrograma.Clear();
            cboPrograma.Add("0", "<< Seleccione >>");

            string sSql = "";

            sSql = string.Format(" Select Distinct IdPrograma, Programa From vw_Clientes_Programas_Asignados_Unidad (nolock) " +
                                 " Where IdEstado = '{0}' and IdFarmacia = '{1}' and IdCliente = '{2}' ", cboEstados.Data, cboFarmacias.Data, cboCliente.Data);

            if (!reader2.Exec(sSql))
            {
                General.msjError("Ocurrió un error al consultar los Programas");
                Error.GrabarError(reader2, "CargarProgramas");
            }
            else
            {
                if (reader2.Leer())
                {
                    cboPrograma.Add(reader2.DataSetClase, true, "IdPrograma", "Programa");
                }
            }
            
            cboPrograma.SelectedIndex = 0;
        }

        private void CargarSubProgramas()
        {
            cboSubPrograma.Clear();
            cboSubPrograma.Add("0", "<< Seleccione >>");

            string sSql = "";

            sSql = string.Format(" Select Distinct IdSubPrograma, SubPrograma From vw_Clientes_Programas_Asignados_Unidad (nolock) " +
                                 " Where IdEstado = '{0}' and IdFarmacia = '{1}' and IdCliente = '{2}' and IdPrograma = '{3}' ", 
                                 cboEstados.Data, cboFarmacias.Data, cboCliente.Data, cboPrograma.Data);

            if (!reader2.Exec(sSql))
            {
                General.msjError("Ocurrió un error al consultar los Sub-Programas");
                Error.GrabarError(reader2, "CargarSubProgramas");
            }
            else
            {
                if (reader2.Leer())
                {
                    cboSubPrograma.Add(reader2.DataSetClase, true, "IdSubPrograma", "SubPrograma");
                }
            }


            cboSubPrograma.SelectedIndex = 0;
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboEstados.SelectedIndex != 0)
            {
                cboEstados.Enabled = false;
                CargarFarmacias();
            }
        }

        private void cboFarmacias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFarmacias.SelectedIndex != 0)
            {
                cboFarmacias.Enabled = false;
                CargarClientes();              
            }
        }
            
        private void cboNivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboNivel.SelectedIndex != 0)
            {
                cboNivel.Enabled = false;
                CargarProgramas();
            }
        }

        private void cboPrograma_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPrograma.SelectedIndex != 0)
            {
                CargarSubProgramas();
                cboPrograma.Enabled = false;
            }
        }

        private void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCliente.SelectedIndex != 0)
            {
                CargarNiveles();
                cboCliente.Enabled = false;
            }
        }

        private void cboSubPrograma_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSubPrograma.SelectedIndex != 0)
            {
                cboSubPrograma.Enabled = false;
            }
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

            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");

            cboCliente.Clear();
            cboCliente.Add("0", "<< Seleccione >>");

            cboNivel.Clear();
            cboNivel.Add("0", "<< Seleccione >>");

            cboPrograma.Clear();
            cboPrograma.Add("0", "<< Seleccione >>");

            cboSubPrograma.Clear();
            cboSubPrograma.Add("0", "<< Seleccione >>");

            SolicitarPermisosUsuario();
            CargarEstados();           

            cboEstados.SelectedIndex = 0;
            cboFarmacias.SelectedIndex = 0;
            cboCliente.SelectedIndex = 0;
            cboNivel.SelectedIndex = 0;
            cboPrograma.SelectedIndex = 0;
            cboSubPrograma.SelectedIndex = 0;

            cboEstados.Focus();
        }        

        private void CargarClaves(string Criterio)
        {            

            string sSql = string.Format(
                " Select C.IdClaveSSA, C.ClaveSSA, C.DescripcionClave, IsNull(S.Cantidad, 0) as Cantidad, (case when IsNull(S.IdClaveSSA, 0) = 0 Then 0 Else 1 End) As Bandera, " +
	            " case when IsNull(S.IdClaveSSA, 0) = 0 Then 0 Else 1 End As Marca " +
	            " From vw_CB_CuadroBasico_Farmacias C (nolock) " +
	            " Left Join CFG_CB_Sub_CuadroBasico_Claves S (Nolock)  " +
		            " On ( C.IdEstado = S.IdEstado and C.IdFarmacia = S.IdFarmacia and C.IdCliente = S.IdCliente and C.IdNivel = S.IdNivel " +
                        " and C.IdClaveSSA = S.IdClaveSSA and S.Status = 'A' and S.IdPrograma = '{4}' and S.IdSubPrograma = '{5}' ) " +
                " Where C.IdEstado = '{0}' and C.IdFarmacia = '{1}' and C.IdCliente = '{2}' and C.IdNivel = {3}  " +
                " Order By Bandera desc, C.DescripcionClave ",
                cboEstados.Data, cboFarmacias.Data, cboCliente.Data, cboNivel.Data, cboPrograma.Data, cboSubPrograma.Data);

            grid.Limpiar(false);

            if (!reader.Exec(sSql))
            {
                Error.GrabarError(reader, "CargarClaves()");
                General.msjError("Ocurrió un error al obtener las Claves.");
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
                if (cnn.Abrir())
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
                else
                {
                    Error.LogError(cnn.MensajeError);
                    General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo.");
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
            int iSubCB = 0, iBandera = 0, iCant = 0;

            for (int i = 1; i <= grid.Rows; i++)
            {
                sIdClaveSSA = grid.GetValue(i, (int)Cols.IdClaveSSA);
                sClaveSSA = grid.GetValue(i, (int)Cols.ClaveSSA);
                iSubCB = grid.GetValueInt(i, (int)Cols.SubCB);
                iBandera = grid.GetValueInt(i, (int)Cols.Bandera);
                iCant = grid.GetValueInt(i, (int)Cols.Cantidad);

                if (iSubCB != iBandera)
                {                    
                    sSql = string.Format("Exec spp_Mtto_CFG_CB_Sub_CuadroBasico_Claves '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}' ",
                                  cboEstados.Data, cboCliente.Data, cboNivel.Data, cboFarmacias.Data, cboPrograma.Data, cboSubPrograma.Data,
                                  sIdClaveSSA, iCant );

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

            if (bRegresa && cboFarmacias.SelectedIndex == 0)
            {
                General.msjAviso("No ha seleccionado la Farmacia.. Verifique !!");
                bRegresa = false;
                cboFarmacias.Focus();
            }

            if (bRegresa && cboCliente.SelectedIndex == 0)
            {
                General.msjAviso("No ha seleccionado el Cliente.. Verifique !!");
                bRegresa = false;
                cboCliente.Focus();
            }

            if (bRegresa && cboNivel.SelectedIndex == 0)
            {
                General.msjAviso("No ha seleccionado el Nivel.. Verifique !!");
                bRegresa = false;
                cboNivel.Focus();
            }

            if (bRegresa && cboPrograma.SelectedIndex == 0)
            {
                General.msjAviso("No ha seleccionado el Programa.. Verifique !!");
                bRegresa = false;
                cboPrograma.Focus();
            }

            if (bRegresa && cboSubPrograma.SelectedIndex == 0)
            {
                General.msjAviso("No ha seleccionado el Sub-Programa.. Verifique !!");
                bRegresa = false;
                cboSubPrograma.Focus();
            }

            return bRegresa;
        }

        //private bool ValidaDatosGrabar()
        //{
        //    bool bRegresa = true;

        //    if (cboEstados.SelectedIndex == 0)
        //    {
        //        General.msjAviso("No ha seleccionado el estado.. Verifique !!");
        //        bRegresa = false;
        //        cboEstados.Focus();
        //    }

        //    if (bRegresa && cboFarmacias.SelectedIndex == 0)
        //    {
        //        General.msjAviso("No ha seleccionado la Farmacia.. Verifique !!");
        //        bRegresa = false;
        //        cboFarmacias.Focus();
        //    }

        //    if (bRegresa && cboCliente.SelectedIndex == 0)
        //    {
        //        General.msjAviso("No ha seleccionado el Cliente.. Verifique !!");
        //        bRegresa = false;
        //        cboCliente.Focus();
        //    }

        //    if (bRegresa && cboNivel.SelectedIndex == 0)
        //    {
        //        General.msjAviso("No ha seleccionado el Nivel.. Verifique !!");
        //        bRegresa = false;
        //        cboNivel.Focus();
        //    }

        //    if (bRegresa && cboPrograma.SelectedIndex == 0)
        //    {
        //        General.msjAviso("No ha seleccionado el Programa.. Verifique !!");
        //        bRegresa = false;
        //        cboPrograma.Focus();
        //    }

        //    if (bRegresa && cboSubPrograma.SelectedIndex == 0)
        //    {
        //        General.msjAviso("No ha seleccionado el Sub-Programa.. Verifique !!");
        //        bRegresa = false;
        //        cboSubPrograma.Focus();
        //    }

        //    return bRegresa;
        //}
        #endregion Guardar_Informacion       

        #region Eventos
        private void chkTodas_CheckedChanged(object sender, EventArgs e)
        {
            grid.SetValue((int)Cols.SubCB, chkTodas.Checked);
        }
        #endregion Eventos                

    }//LLAVES DE LA CLASE
}
