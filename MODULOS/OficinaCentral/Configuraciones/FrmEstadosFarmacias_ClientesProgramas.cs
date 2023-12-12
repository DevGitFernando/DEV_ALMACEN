using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;

using DllFarmaciaSoft;

namespace OficinaCentral.Configuraciones
{
    public partial class FrmEstadosFarmacias_ClientesProgramas : FrmBaseExt
    {
        private enum Cols
        {
            IdSubPrograma = 1, Descripcion = 2, Status = 3, StatusAux = 4 
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas query;
        clsGrid gridClientes, gridProgramas;

        DataSet dtsFarmacias, dtsSubClientes;
        string sEncProgramas = "Programas y Sub-Programas por Clientes por Farmacia";
        string sIdSubCliente = "", sEsActivo = "";

        public FrmEstadosFarmacias_ClientesProgramas()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn); 
            query = new clsConsultas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name, false); 
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.DatosApp, this.Name);

            gridClientes = new clsGrid(ref grdSubClientes, this);
            gridProgramas = new clsGrid(ref grdSubProgramas, this);

            gridClientes.EstiloGrid(eModoGrid.SeleccionSimple);
            gridProgramas.EstiloGrid(eModoGrid.ModoRow);

            gridClientes.AjustarAnchoColumnasAutomatico = true;
            gridProgramas.AjustarAnchoColumnasAutomatico = true; 

        }

        #region Botones 
        private void LimpiarPantalla()
        {
            tmSubClientes.Enabled = false;
            tmSubClientes.Stop();

            gridClientes.Limpiar(false);
            gridProgramas.Limpiar(false);

            cboProgramas.Enabled = false;
            btnAsignarProgramas.Enabled = false;

            FrameProgramas.Text = sEncProgramas;
            Fg.IniciaControles();
            cboEstados.Focus();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones

        private void FrmEstadosFarmacias_ClientesProgramas_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();
            CargarInformacion();
        }

        #region Obtener Informacion 
        private void CargarInformacion()
        {
            CargarEstados_Farmacias();

            cboClientes.Clear();
            cboClientes.Add("0", "<< Seleccione >>");
            cboClientes.SelectedIndex = 0;

            CargarProgramas();
        }

        private void CargarEstados_Farmacias()
        {
            string sSql = string.Format(
                "Select distinct C.IdEstado, E.Nombre, ( C.IdEstado + ' - ' + E.Nombre ) as NombreEstado \n" +
                "From CFG_EstadosFarmaciasClientesSubClientes C (NoLock) \n" +
                "Inner Join CatEstados E (NoLock) On ( C.IdEstado = E.IdEstado ) \n" + 
                "Order by C.IdEstado "); 

            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");
            if ( leer.Exec(sSql ) )
            {
                cboEstados.Add(leer.DataSetClase, true, "IdEstado", "NombreEstado");
            }
            cboEstados.SelectedIndex = 0;



            sSql = string.Format(
                "Select distinct C.IdEstado, E.IdFarmacia, E.Farmacia, \n" +
                "\t(E.IdFarmacia + ' -- ' + E.Farmacia) as NombreFarmacia \n" +
                "From CFG_EstadosFarmaciasClientesSubClientes C (NoLock) \n" +
                "Inner Join vw_Farmacias E (NoLock) On ( C.IdEstado = E.IdEstado and C.IdFarmacia = E.IdFarmacia ) \n"+ 
                "Order by C.IdEstado, E.IdFarmacia ");
            
            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");
            if (leer.Exec(sSql))
            {
                dtsFarmacias = leer.DataSetClase;
            }
            cboFarmacias.SelectedIndex = 0;
        }

        private void CargarClientes()
        {
            string sSql = string.Format(
                "Select C.IdCliente, ( C.IdCliente + ' - ' + E.NombreCliente) as NombreCliente \n" +
                "From CFG_EstadosFarmaciasClientesSubClientes C (NoLock) \n" +
                "Inner Join vw_Clientes_SubClientes E (NoLock) On ( C.IdCliente = E.IdCliente and C.IdSubCliente = E.IdSubCliente) \n" +
                "Where C.IdEstado = '{0}' and C.IdFarmacia = '{1}' " +
                "Group by C.IdCliente,  E.NombreCliente \n" +
                "Order by C.IdCliente, E.NombreCliente \n", cboEstados.Data, cboFarmacias.Data ); 

            cboClientes.Clear();
            cboClientes.Add("0", "<< Seleccione >>");
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarClientes");
                General.msjError("Ocurrió un error al obtener la lista de clientes.");
            }
            else
            {
                cboClientes.Add(leer.DataSetClase, true, "IdCliente", "NombreCliente");
            }
            cboClientes.SelectedIndex = 0;

            // Cargar los subclientes asignados a la farmacia 
            sSql = string.Format(
                "Select C.IdCliente, C.IdSubCliente, E.NombreSubCliente, \n" +
                "\t(case when C.Status = 'A' then 'Activo' else 'Cancelado' end) as Status, C.Status as StatusAux \n" +
                "From CFG_EstadosFarmaciasClientesSubClientes C (NoLock) \n" +
                "Inner Join vw_Clientes_SubClientes E (NoLock) On ( C.IdCliente = E.IdCliente and C.IdSubCliente = E.IdSubCliente) \n" +
                "Where C.IdEstado = '{0}' and C.IdFarmacia = '{1}' \n" +
                "Group by C.IdCliente, C.IdSubCliente, E.NombreSubCliente, C.Status \n" +
                "Order by C.IdCliente, C.IdSubCliente, E.NombreSubCliente \n", cboEstados.Data, cboFarmacias.Data );
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarClientes");
                General.msjError("Ocurrió un error al obtener la lista de clientes.");
            }
            else
            {
                dtsSubClientes = leer.DataSetClase;
            }

            tmSubClientes.Enabled = true;
            tmSubClientes.Start();

        }

        private void CargarProgramas()
        {
            cboProgramas.Clear();
            cboProgramas.Add("0", "<< Seleccione >>");
            cboProgramas.Add(query.ComboProgramas("CargarProgramas()"), true, "IdPrograma", "NombrePrograma");
            cboProgramas.SelectedIndex = 0;
            cboProgramas.Enabled = false;
        }

        public void CargarSubProgramas()
        {
            string sSql = string.Format(
                "Select P.IdSubPrograma, P.SubPrograma, \n" +
                "\t(case when IsNull(C.Status, '') = '' Then 0 else case when C.Status = 'A' then 1 else 0 end end) as Status, \n" +
                "\t(case when IsNull(C.Status, '') = '' Then 0 else case when C.Status = 'A' then 1 else 0 end end) as StatusAux \n" +
                "\tFrom vw_Programas_SubProgramas P (NoLock) \n" +
                "\tLeft Join \n" +
                "( \n" +
                "\t\tSelect C.IdEstado, C.IdFarmacia, C.IdCliente, C.IdSubCliente, \n" +
                "\t\tC.IdPrograma, C.IdSubPrograma, C.Status, V.NombreCliente, V.NombreSubCliente \n" +
                "\t\tFrom CFG_EstadosFarmaciasProgramasSubProgramas C (NoLock) \n" +
                "\t\tInner Join vw_Clientes_SubClientes v (NoLock) On ( C.IdCliente = v.IdCliente and C.IdSubCliente = V.IdSubCliente ) \n" +
                "\t\tWhere C.IdCliente = '{2}' and C.IdSubCliente = '{3}' \n" +
                "\t) C \n" +
                "\tOn ( P.IdPrograma = C.IdPrograma and P.IdSubPrograma = C.IdSubPrograma and C.IdEstado = '{0}' and C.IdFarmacia = '{1}' ) \n" +
                "Where P.IdPrograma = '{4}' \n" + 
                "Order by P.IdPrograma, P.IdSubPrograma \n",
                cboEstados.Data, cboFarmacias.Data, cboClientes.Data, sIdSubCliente, cboProgramas.Data);  

            gridProgramas.Limpiar(false); 
            if (sEsActivo == "A")
            {
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "CargarSubProgramas()");
                    General.msjError("Ocurrió un error al obtener la lista de Sub-Programas.");
                }
                else
                {
                    if(leer.Leer())
                    {
                        gridProgramas.LlenarGrid(leer.DataSetClase);
                    }
                }
            }
        }
        #endregion Obtener Informacion 

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");
            if (cboEstados.SelectedIndex != 0)
            {
                string sWhere = string.Format(" IdEstado = '{0}' ", cboEstados.Data);
                try
                {
                    cboFarmacias.Add(dtsFarmacias.Tables[0].Select(sWhere), true, "IdFarmacia", "NombreFarmacia");
                }
                catch { }
            }
            cboFarmacias.SelectedIndex = 0;
        }

        private void cboEstados_Validating(object sender, CancelEventArgs e)
        {
            if (cboEstados.SelectedIndex != 0)
                cboEstados.Enabled = false;
        }

        private void cboFarmacias_Validating(object sender, CancelEventArgs e)
        {
            if (cboFarmacias.SelectedIndex != 0)
            {
                cboFarmacias.Enabled = false;
                CargarClientes();
            }
        }

        private void cboClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sWhere = string.Format(" IdCliente = '{0}' ", cboClientes.Data);
            gridClientes.Limpiar(false);

            try
            {
                if(cboClientes.SelectedIndex != 0)
                {
                    gridClientes.AgregarRenglon(dtsSubClientes.Tables[0].Select(sWhere), 5, false);
                }
            }
            catch { }
        }

        private void cboClientes_Validating(object sender, CancelEventArgs e)
        {
            if (cboClientes.SelectedIndex != 0)
            {
                cboClientes.Enabled = false;
                cboProgramas.Enabled = true;
            }
        }

        private void tmSubClientes_Tick(object sender, EventArgs e)
        {
            FrameProgramas.Text = sEncProgramas;
            sEsActivo = "C";

            if (gridClientes.Rows > 0)
            {
                string sValor = gridClientes.GetValue(gridClientes.ActiveRow, 3);
                if (sValor != "")
                    FrameProgramas.Text = sEncProgramas + " : " + sValor;


                sEsActivo = gridClientes.GetValue(gridClientes.ActiveRow, 5);
                if (sIdSubCliente != gridClientes.GetValue(gridClientes.ActiveRow, 2))
                {
                    sIdSubCliente = gridClientes.GetValue(gridClientes.ActiveRow, 2);
                    //cboProgramas.SelectedIndex = 0;
                    CargarSubProgramas();
                }
            }

            // Solo los SubClientes activos se pueden modificar 
            if (sEsActivo == "A")
                cboProgramas.Enabled = true;
            else
                cboProgramas.Enabled = false;
        }

        private void cboProgramas_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridProgramas.Limpiar(false);
            btnAsignarProgramas.Enabled = false;

            if (cboProgramas.SelectedIndex != 0)
            {
                btnAsignarProgramas.Enabled = true;
                CargarSubProgramas();
            }
        }

        private void btnAsignarProgramas_Click(object sender, EventArgs e)
        {
            bool bExito = true;
            string sSql = "";
            string sIdSubPrograma = "", sStatus = "";

            if(!cnn.Abrir())
            {
                General.msjErrorAlAbrirConexion();
            }
            else
            {
                cnn.IniciarTransaccion();

                for (int i = 1; i <= gridProgramas.Rows; i++)
                {
                    sStatus = "A";
                    sIdSubPrograma = gridProgramas.GetValue(i, (int)Cols.IdSubPrograma);
                    if (!gridProgramas.GetValueBool(i, (int)Cols.Status))
                    {
                        sStatus = "C";
                    }

                    sSql = string.Format(" Exec spp_Mtto_CFG_EstadosFarmaciasProgramasSubProgramas " +
                        "\t@IdEstado = '{0}', @IdFarmacia = '{1}', @IdCliente = '{2}', @IdSubCliente = '{3}', \n" +
                        "\t@IdPrograma = '{4}', @IdSubPrograma = '{5}', @Status = '{6}' \n", 
                            cboEstados.Data, cboFarmacias.Data, cboClientes.Data, sIdSubCliente, cboProgramas.Data, sIdSubPrograma, sStatus); 
                    // Solo grabar los cambios detectados 
                    if (gridProgramas.GetValueBool(i, (int)Cols.Status) != gridProgramas.GetValueBool(i, (int)Cols.StatusAux))
                    {
                        if (!leer.Exec(sSql))
                        {
                            bExito = false;
                            break;
                        }
                    }
                }

                if (!bExito)
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(leer, "btnAsignarProgramas_Click");
                    General.msjError("Ocurrió un error al guardar la información.");
                }
                else
                {
                    cnn.CompletarTransaccion();
                    General.msjUser("Información guardada satisfactoriamente.");
                    cboProgramas.SelectedIndex = 0;
                }

                cnn.Cerrar();
            }
        }
    }
}
