using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

// Implementacion de hilos 
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Usuarios_y_Permisos;

namespace OficinaCentral.CuadrosBasicos
{
    public partial class FrmClavesExcluidasAbasto : FrmBaseExt
    {
        ////clsDatosConexion DatosDeConexion;
        clsConexionSQL cnn;
        ////clsConexionSQL cnnUnidad;
        clsLeer leer;
        clsLeer leerLocal; 
        clsConsultas Consultas;
        clsAyudas Ayudas;
        clsGrid Grid;

        clsDatosCliente DatosCliente;
        wsOficinaCentral.wsCnnOficinaCentral conexionWeb;
        ////Thread _workerThread;
        
        DataSet dtsEstados = new DataSet();
        DataSet dtsClaves = new DataSet();
        
        ////string sTablaFarmacia = "CTE_FarmaciasProcesar";
        string sValorGrid = "";

        ////bool bEjecutando = false;
        ////bool bSeEncontroInformacion = false;
        ////bool bSeEjecuto = false;

        //clsOperacionesSupervizadas Permisos;
        string sPermisoPerfiles = "MODIFICAR_PERFILES";
        bool bPermisoPerfiles = false;

        private enum Cols
        {
            Ninguna = 0,
            IdClaveSSA = 1, ClaveSSA = 2, Descripcion = 3, Procesar = 4
        }

        public FrmClavesExcluidasAbasto()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();

            DatosCliente = new clsDatosCliente(GnOficinaCentral.DatosApp, this.Name, "");
            conexionWeb = new wsOficinaCentral.wsCnnOficinaCentral();
            conexionWeb.Url = General.Url;

            CheckForIllegalCrossThreadCalls = false;
            DatosCliente = new clsDatosCliente(GnOficinaCentral.DatosApp, this.Name, "ReportesFacturacionUnidad");
            //leerWeb = new clsLeerWeb(ref cnn, General.Url, General.ArchivoIni, DatosCliente);  

            cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario = General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;

            leer = new clsLeer(ref cnn);
            leerLocal = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
            Ayudas = new clsAyudas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);


            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name);
            //DtGeneralPedidos.FarmaciaConectada = General.EntidadConectada;

            Grid = new clsGrid(ref grdClaves, this);
            Grid.EstiloGrid(eModoGrid.ModoRow);

            CargarEstados();
        }

        private void FrmClavesExcluidasAbasto_Load(object sender, EventArgs e)
        {
            SolicitarPermisosUsuario(); 
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

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bContinua = false;
            EliminarRenglonesVacios();
            
            if (cnn.Abrir())
            {
                cnn.IniciarTransaccion();
                bContinua = GuardarInformacion();

                if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                {
                    cnn.CompletarTransaccion();
                    General.msjUser(" Información Guardada Satisfactoriamente ");                            
                    btnNuevo_Click(null, null);
                }
                else
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(leer, "btnGuardar_Click");
                    General.msjError("Ocurrió un error al guardar la información.");
                }
                cnn.Cerrar();
            }
            else
            {
                General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
            }            
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            //CargarClaves();            
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            Grid.Limpiar(false);

            IniciaToolBar(false, false, false);

            cboEstados.SelectedIndex = 0;
            CargarEstados(); 

            cboClientes.Clear();
            cboClientes.Add();
            cboClientes.SelectedIndex = 0;

            cboEstados.Focus();
        }

        private bool GuardarInformacion()
        {
            bool bRegresa = true;
            string sSql = "";
            string sIdClaveSSA = "";
            int iOpcion = 0;

            for (int i = 1; i <= Grid.Rows; i++)
            {
                sIdClaveSSA = Grid.GetValue(i, (int)Cols.IdClaveSSA);
                iOpcion = Grid.GetValueInt(i, (int)Cols.Procesar);

                if (sIdClaveSSA != "")
                {
                    sSql = string.Format("Exec spp_Mtto_CFG_Claves_Excluir_NivelAbasto '{0}', '{1}', '{2}', '{3}' ",
                                         cboEstados.Data, cboClientes.Data, sIdClaveSSA, iOpcion);
                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }

            return bRegresa;
        }

        private void EliminarRenglonesVacios()
        {
            for (int i = 1; i <= Grid.Rows; i++) //Renglones.
            {
                if (Grid.GetValue(i, 1).Trim() == "") 
                    Grid.DeleteRow(i);
            }

            if (Grid.Rows == 0) // Si No existen renglones, se inserta 1.
                Grid.AddRow();
        }

        private void IniciaToolBar(bool Guardar, bool Ejecutar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnEjecutar.Enabled = Ejecutar;
            btnImprimir.Enabled = Imprimir;
        }
        #endregion Funciones

        #region Cargar Combos
        private void CargarEstados()
        {
            if (cboEstados.NumeroDeItems == 0)
            {
                cboEstados.Clear();
                cboEstados.Add();

                //cboEstados.Add(DtGeneralPedidos.Estados, true, "IdEstado", "Estado");

                string sSql = " Select Distinct IdEstado, Estado, ( IdEstado + ' - ' + Estado ) as Descripcion From vw_Claves_Precios_Asignados (Nolock) Order By IdEstado ";
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "CargarEstados()");
                    General.msjError("Ocurrió un error al obtener la lista de Estados.");
                }
                else
                {
                    cboEstados.Add(leer.DataSetClase, true, "IdEstado", "Descripcion"); 
                }
            }

            cboEstados.SelectedIndex = 0;
            if (!(DtGeneral.EsAdministrador || bPermisoPerfiles))
            {
                cboEstados.Data = DtGeneral.EstadoConectado;
                cboEstados.Enabled = false;
            }
            //cboEstados.Data = DtGeneralPedidos.EstadoConectado;
            //cboEstados.Enabled = false;
        }

        private void CargarClientes()
        {            
            cboClientes.Clear();
            cboClientes.Add(); 

            string sSql = string.Format(" Select Distinct IdCliente, (IdCliente + ' - ' + NombreCliente) as NombreCliente From vw_Claves_Precios_Asignados (Nolock) " +
                                         " Where IdEstado = '{0}' Order By IdCliente ", cboEstados.Data );

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarClientes()");
                General.msjError("Ocurrió un error al obtener la lista de Clientes.");
            }
            else
            {
                cboClientes.Add(leer.DataSetClase, true, "IdCliente", "NombreCliente");
            }
            
            cboClientes.SelectedIndex = 0;
        }
        #endregion Cargar Combos

        #region Eventos
        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEstados.SelectedIndex != 0)
            {
                cboEstados.Enabled = false;
                CargarClientes();
            }
        }

        private void cboClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboClientes.SelectedIndex != 0)
            {
                cboClientes.Enabled = false;
                CargarClaves();
                IniciaToolBar(true, false, false);
            }
        }
        #endregion Eventos

        #region Grid
        private void CargarClaves()
        {
            string sSql = string.Format(" Select E.IdClaveSSA, C.ClaveSSA, DescripcionClave, " +
                                        " Case When E.Status = 'A' Then 1 Else 0 End As Procesar " +
	                                    " From CFG_Claves_Excluir_NivelAbasto E (Nolock) " +
	                                    " Left Join vw_Claves_Precios_Asignados C (Nolock) " +
                                            " On ( E.IdEstado = C.IdEstado And E.IdCliente = C.IdCliente And E.IdClaveSSA = C.IdClaveSSA ) " +
	                                    " Where E.IdEstado = '{0}' And E.IdCliente = '{1}' ", cboEstados.Data, cboClientes.Data);

            Grid.Limpiar(false);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarClaves()");
                General.msjError("Ocurrió un error al obtener las Claves.");
            }
            else
            {
                if (leer.Leer())
                {
                    Grid.LlenarGrid(leer.DataSetClase);
                    Grid.BloqueaColumna(true, (int)Cols.IdClaveSSA);
                }
                else
                {
                    Grid.Limpiar(true);
                }
            }
        }

        private void grdClaves_KeyDown(object sender, KeyEventArgs e)
        {
            if (Grid.ActiveCol == (int)Cols.IdClaveSSA)
            {
                
                if (e.KeyCode == Keys.F1)
                {
                    leer.DataSetClase = Ayudas.ClavesSSA_PreciosAsignados(cboEstados.Data, cboClientes.Data, "grdClaves_KeyDown");
                    if (leer.Leer())
                    {
                        Grid.SetValue(Grid.ActiveRow, (int)Cols.IdClaveSSA, leer.Campo("IdClaveSSA"));
                        CargaDatosProducto();
                    }

                }
            }
        }

        private void grdClaves_EditModeOff(object sender, EventArgs e)
        {
            Cols iCol = (Cols)Grid.ActiveCol;

            switch (iCol)
            {
                case Cols.IdClaveSSA:
                    ObtenerDatosClave();
                    break;
            }
        }

        private void grdClaves_EditModeOn(object sender, EventArgs e)
        {
            sValorGrid = Grid.GetValue(Grid.ActiveRow, (int)Cols.IdClaveSSA);
        }

        private void grdClaves_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if ((Grid.ActiveRow == Grid.Rows) && e.AdvanceNext)
            {
                if (Grid.GetValue(Grid.ActiveRow, 1) != "" && Grid.GetValue(Grid.ActiveRow, 3) != "")
                {
                    Grid.Rows = Grid.Rows + 1;
                    Grid.ActiveRow = Grid.Rows;
                    Grid.SetActiveCell(Grid.Rows, 1);
                    Grid.BloqueaCelda(false, Grid.ActiveRow, (int)Cols.IdClaveSSA);
                }
            }
        }

        private void ObtenerDatosClave()
        {
            string sIdClaveSSA = "";           

            sIdClaveSSA = Grid.GetValue(Grid.ActiveRow, (int)Cols.IdClaveSSA);


            if (sIdClaveSSA.Trim() != "")
            {
                leer.DataSetClase = Consultas.ClavesSSA_PreciosAsignados(cboEstados.Data, cboClientes.Data, sIdClaveSSA, "ObtenerDatosClave()");
                
                {
                    if (!leer.Leer())
                    {
                        General.msjUser("La Clave no Existe ó no tiene Asignado Precio.");
                        Grid.LimpiarRenglon(Grid.ActiveRow);
                    }
                    else
                    {
                        CargaDatosProducto();
                    }
                }
            }
        }

        private void CargaDatosProducto()
        {
            int iRowActivo = Grid.ActiveRow;

            if (!Grid.BuscaRepetido(leer.Campo("IdClaveSSA"), iRowActivo, (int)Cols.IdClaveSSA))
            {
                Grid.SetValue(iRowActivo, (int)Cols.ClaveSSA, leer.Campo("ClaveSSA"));
                Grid.SetValue(iRowActivo, (int)Cols.IdClaveSSA, leer.Campo("IdClaveSSA"));
                Grid.SetValue(iRowActivo, (int)Cols.Descripcion, leer.Campo("DescripcionClave"));
                Grid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.IdClaveSSA);
                Grid.SetActiveCell(iRowActivo, (int)Cols.Procesar);

            }
            else
            {
                General.msjUser("Esta Clave ya se encuentra capturada en otro renglón.");
                Grid.SetValue(Grid.ActiveRow, (int)Cols.Procesar, 0);
                limpiarColumnas();
                Grid.SetActiveCell(Grid.ActiveRow, 4);
            }


        }

        private void limpiarColumnas()
        {
            for (int i = 2; i <= Grid.Columns; i++) //Columnas. Nota: Inicia a partir de la 2da.
            {
                Grid.SetValue(Grid.ActiveRow, i, "");
            }
        }
        #endregion Grid    

        
    }
}
