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
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.ExportarExcel;

using ClosedXML.Excel;

namespace Almacen.Pedidos
{
    public partial class FrmPedidos_LiberacionDeIncidencias : FrmBaseExt
    {
        private enum Cols
        {
            Ninguna = 0,
            IdEmpresa = 1, IdEstado, IdFarmacia, FolioSurtido, IdSurtimiento, ClaveSSA, IdSubFarmacia,
            IdProducto, CodigoEAN, Descripcion, ClaveLote, IdPasillo, IdEstante, IdEntrepaño,
            SKU, IdPersonal, FechaRegistro, IdIncidencia, IncidenciaDesc, Observaciones, Actualizado, Liberar, ObservacionesLiberacion
        }

        public bool InformacionGuardada = false; 

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente datosCliente;
        clsLeer leer;
        clsLeer leer_Datos;
        clsConsultas query;
        clsAyudas ayuda;
        clsGrid grid;

        int iId = 0;
        DataSet dtsDetalles_Incidencias;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        int iIdSurtimiento = 0, iIdPasillo = 0, iIdEstante = 0, iIdEntrepaño = 0;
        string sFolioSurtido = "", sIdSubFarmacia, sIdProducto = "", sCodigoEAN = "", sClaveLote = "", sSKU = "";

        public FrmPedidos_LiberacionDeIncidencias( string FolioSurtido, string IdSubFarmacia, int IdSurtimiento, string IdProducto, string CodigoEAN, string ClaveLote, string SKU,
            int IdPasillo, int IdEstante, int IdEntrepaño )
        {
            InitializeComponent();
            //iAnchoPantalla = (int)((double)Screen.PrimaryScreen.WorkingArea.Size.Width * 0.95);
            //this.Width = iAnchoPantalla;
            //General.Pantalla.AjustarTamaño(this, 90, 80);


            sFolioSurtido = FolioSurtido;
            sIdSubFarmacia = IdSubFarmacia;
            sIdProducto = IdProducto;
            sCodigoEAN = CodigoEAN;
            sClaveLote = ClaveLote;
            sSKU = SKU;
            iIdSurtimiento = IdSurtimiento;
            iIdPasillo = IdPasillo;
            iIdEstante = IdEstante;
            iIdEntrepaño = IdEntrepaño;



            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmPedidos_LiberacionDeIncidencias");

            leer = new clsLeer(ref cnn);
            leer_Datos = new clsLeer(); 
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name);
            leer.DataSetClase = dtsDetalles_Incidencias;
            leer_Datos.DataSetClase = dtsDetalles_Incidencias;

            grid = new clsGrid(ref grdIncidencias, this);
            grid.AjustarAnchoColumnasAutomatico = true; 
        }

        private void InicializarPantalla()
        {
            Fg.IniciaControles();
            grid.Limpiar();

            ObtenerIncidencias(); 
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarPantalla();
        }

        private void btnGuardar_Click( object sender, EventArgs e )
        {
            bool bRegresa = false; 


            if(!cnn.Abrir())
            {
                General.msjErrorAlAbrirConexion();
            }
            else 
            {
                cnn.IniciarTransaccion();

                bRegresa = Liberar();
                if(!bRegresa)
                {
                    Error.GrabarError(leer, "btnGuardar_Click");
                    cnn.DeshacerTransaccion();
                    General.msjError("Ocurrió un error al obtener la información de las Incidencias.");
                }
                else
                {
                    cnn.CompletarTransaccion();

                    General.msjUser("Información de Incidencias guardada satisfactoriamente.");
                }

                cnn.Cerrar();

                if(bRegresa)
                {
                    InformacionGuardada = true; 
                    this.Hide(); 
                }
            } 
        }
        #endregion Botones 

        private void FrmPedidos_LiberacionDeIncidencias_Load(object sender, EventArgs e)
        {
            InicializarPantalla();
        }

        #region Funciones y Procedimientos 
        private bool Liberar()
        {
            string sSql = "";
            bool bRegresa = true;
            bool bLiberar = false, bLimpiar = true;
            //int IdSurtimiento, iIdPasillo, iIdEstante, iIdEntrepaño;
            //string sIdProducto, sCodigoEAN, sClaveLote, sSKU, sIdIncidencia, sFolioSurtido, sObservacionesLiberacion;
            string sIdIncidencia = "", sObservacionesLiberacion = "";


            for(int i = 1; grid.Rows >= i; i++)
            {
                bLiberar = grid.GetValueBool(i, (int)Cols.Liberar);

                //if(bLiberar)
                {
                    //iIdSurtimiento = grid.GetValueInt(i, (int)Cols.IdSurtimiento);
                    //iIdPasillo = grid.GetValueInt(i, (int)Cols.IdPasillo);
                    //iIdEstante = grid.GetValueInt(i, (int)Cols.IdEstante);
                    //iIdEntrepaño = grid.GetValueInt(i, (int)Cols.IdEntrepaño);

                    //sIdProducto = grid.GetValue(i, (int)Cols.IdProducto);
                    //sCodigoEAN = grid.GetValue(i, (int)Cols.CodigoEAN);
                    //sClaveLote = grid.GetValue(i, (int)Cols.ClaveLote);
                    //sSKU = grid.GetValue(i, (int)Cols.SKU);
                    //sFolioSurtido = grid.GetValue(i, (int)Cols.FolioSurtido);

                    sIdIncidencia = grid.GetValue(i, (int)Cols.IdIncidencia);
                    sObservacionesLiberacion = grid.GetValue(i, (int)Cols.ObservacionesLiberacion);

                    sSql = string.Format(" Exec spp_Mtto_Pedidos_Cedis_Det_Surtido_Incidencias_LIberar \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}',\n " +
                        "\t@IdSurtimiento = {3}, @IdProducto = '{4}', @CodigoEAN = '{5}', @ClaveLote = '{6}',\n " +
                        "\t@IdPasillo = {7}, @IdEstante = {8}, @IdEntrepaño = {9}, @SKU = '{10}', @IdIncidencia = '{11}', @IdPersonalValida = '{12}', @Observaciones_Liberacion = '{13}' \n",
                         sEmpresa, sEstado, sFarmacia, iIdSurtimiento, sIdProducto, sCodigoEAN, sClaveLote, iIdPasillo,
                         iIdEstante, iIdEntrepaño, sSKU, sIdIncidencia, DtGeneral.IdPersonal, sObservacionesLiberacion);

                    if(!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }

            return bRegresa;
        }
        public void ObtenerIncidencias()
        {
            string sSql = "";
            int iTipo = 0;

            sSql = string.Format(" Exec spp_RPT_Pedidos_Cedis_Det_Surtido_Incidencias \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Tipo = {3}, \n" +
                "\t@FolioSurtido = '{4}', @IdSurtimiento = '{5}', @IdSubFarmacia = '{6}', \n" +
                "\t@IdProducto = '{7}', @CodigoEAN = '{8}', @ClaveLote = '{9}', \n" +
                "\t@IdPasillo = '{10}', @IdEstante = '{11}', @IdEntrepaño = '{12}', @SKU = '{13}' \n", 
                sEmpresa, sEstado, sFarmacia, iTipo, 
                sFolioSurtido, iIdSurtimiento, sIdSubFarmacia, sIdProducto, sCodigoEAN, sClaveLote, 
                iIdPasillo, iIdEstante, iIdEntrepaño, sSKU 
                );


            grid.Limpiar(false);
            if(!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener la información de las Incidencias.");
            }
            else
            {
                grid.LlenarGrid(leer.DataSetClase);
            }
        }

        #endregion Funciones y Procedimientos 
    }
}
