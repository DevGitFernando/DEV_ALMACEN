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
using Dll_IFacturacion; 

namespace Facturacion
{
    public partial class FrmParametrosFacturacion : FrmBaseExt
    {
        enum Cols
        {
            IdEstado = 1, IdFarmacia = 2, Arbol = 3, Parametro = 4, Valor = 5, Descripcion = 6, EsDeSistema = 7, EsEditable = 8 
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas query;
        clsGrid Grid;

        DataSet dtsFarmacias;
        string sIdEstado = "";
        string sIdFarmacia = DtGeneral.FarmaciaConectada;
        string sModulo = DtGeneral.ArbolModulo; 

        public FrmParametrosFacturacion()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Grid = new clsGrid(ref grdParametros, this);
            Grid.EstiloGrid(eModoGrid.ModoRow);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtIFacturacion.DatosApp, this.Name);

            query = new clsConsultas(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);
        }

        private void FrmParametrosFacturacion_Load(object sender, EventArgs e)
        {
            CargarEstados();
            CargarFarmacias();
            CargarModulos();

            btnNuevo_Click(null, null); 

            //////if (!GnConfiguracion.EsOficinaCentral)
            //////{
            //////    cboEstados.Enabled = false;
            //////    cboSucursales.Enabled = false;

            //////    cboEstados.Data = DtGeneral.EstadoConectado;
            //////    cboSucursales.Data = DtGeneral.FarmaciaConectada;
            //////}
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Grid.Limpiar(false);
            Fg.IniciaControles(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            CargarDatosFarmacia(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarConfiguracion(); 
        }
        #endregion Botones 

        #region Combos 
        private void CargarEstados()
        {
            string sSql = 
                "Select Distinct P.IdEstado, P.Estado \n " + 
	            "From vw_Farmacias P (NoLock) \n " + 
	            "Order By P.IdEstado "; 

            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarEstados()"); 
            }
            else
            {
                if (leer.Leer())
                {
                    cboEstados.Add(leer.DataSetClase, false, "IdEstado", "Estado");
                }
            }             
            
            cboEstados.SelectedIndex = 0;
        }

        private void CargarFarmacias()
        {
            // dtsFarmacias = query.Farmacias("CargarFarmacias");
            string sSql =
                "Select Distinct P.IdEstado, E.Estado, P.IdFarmacia, (P.IdFarmacia + ' -- ' + E.Farmacia) as Farmacia \n " +
                " From Net_CFGC_Parametros P (NoLock) \n " +
                " Inner Join vw_Farmacias E (NoLock) On ( P.IdEstado = E.IdEstado and P.IdFarmacia = E.IdFarmacia ) \n " +
                "Order By P.IdEstado, P.IdFarmacia ";

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarEstados()");
            }
            else
            {
                if (leer.Leer())
                {
                    dtsFarmacias = leer.DataSetClase; 
                    // cboEstados.Add(leer.DataSetClase, false, "IdEstado", "Nombre");
                }
            } 
        }

        private void CargarModulos()
        {
            string sSql = string.Format("Select distinct ArbolModulo as Arbol, M.Nombre as Modulo " +
                "From Net_CFG_Parametros_Facturacion P (NoLock) " + 
                "Inner Join Net_Arboles M (NoLock) On ( P.ArbolModulo = M.Arbol )");

            cboModulo.Clear();
            cboModulo.Add();

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarModulos");
            }
            else
            {
                cboModulo.Add(leer.DataSetClase, true, "Arbol", "Modulo"); 
            }

            cboModulo.SelectedIndex = 0;
        }
        #endregion Combos

        private void CargarDatosFarmacia()
        {
            string sFiltro = " 0 ";
            clsParametrosFacturacion Parametros = new clsParametrosFacturacion(General.DatosConexion, new clsDatosApp("", ""), 
                cboEstados.Data, sIdFarmacia, cboModulo.Data);       
            Parametros.CargarParametros();

            if (DtGeneral.EsAdministrador)
            {
                sFiltro = " 0, 1 ";
            }
            // sFiltro = " 0 "; 

            string sSql = string.Format("Select IdEstado, IdFarmacia, ArbolModulo, NombreParametro, Valor, Descripcion, " +
                " cast(EsDeSistema as int) as EsDeSistema, cast(EsEditable as int) as EsEditable " +
                " From Net_CFG_Parametros_Facturacion (NoLock) " +
                " Where IdEstado = '{0}' and IdFarmacia = '{1}' and ArbolModulo = '{2}' And EsDeSistema in ( {3} )  ",
                cboEstados.Data, sIdFarmacia, cboModulo.Data, sFiltro);

            cboEstados.Enabled = false;
            cboModulo.Enabled = false;
            Grid.Limpiar(false);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarDatosFarmacia");
                General.msjError("Ocurrió un error al cargar los parametros de la Farmacia.");
            }
            else
            {
                //cboEstados.Enabled = false;
                //cboSucursales.Enabled = false;
                // cboModulo.Enabled = false; 
                Grid.LlenarGrid(leer.DataSetClase);

                // Asegurar que los Parametros de sistema no sean modificados 
                for (int i = 1; i <= Grid.Rows; i++)
                {
                    if (Grid.GetValueInt(i, (int)Cols.EsDeSistema) == 1)
                    {
                        Grid.BloqueaRenglon(true, i);
                    }
                    else
                    {
                        if (!Grid.GetValueBool(i, (int)Cols.EsEditable))
                        { 
                            if (!DtGeneral.EsAdministrador)
                            {
                                Grid.BloqueaCelda(true, i, (int)Cols.Valor);
                            } 

                            Grid.ColorRenglon(i, Color.Khaki); 
                        } 
                    } 
                } 
                CargaDescripcion(1);
            }
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////cboSucursales.Clear();
            ////cboSucursales.Add("0", "<< Seleccione >>");

            ////if (cboEstados.SelectedIndex != 0)
            ////{
            ////    cboSucursales.Add(dtsFarmacias.Tables[0].Select(string.Format("IdEstado = '{0}' ", cboEstados.Data)), false, "IdFarmacia", "Farmacia");
            ////}
            ////cboSucursales.SelectedIndex = 0;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
        }

        private void GuardarConfiguracion() 
        {
            bool bExito = true;
            string sSql = "";
            string sArbol = "", sParametro = "", sValor = "";

            sIdEstado = cboEstados.Data;
            sModulo = cboModulo.Data; 

            if (!cnn.Abrir())
            {
                General.msjAviso("No se pudo establecer conexión con el servidor. Intente de nuevo");
            }
            else 
            {
                cnn.IniciarTransaccion();

                for (int i = 1; i <= Grid.Rows; i++)
                {
                    sArbol = Grid.GetValue(i, (int)Cols.Arbol);
                    sParametro = Grid.GetValue(i, (int)Cols.Parametro);
                    sValor = Grid.GetValue(i, (int)Cols.Valor);

                    sSql = string.Format(" Exec spp_Mtto_Net_CFG_Parametros_Facturacion '{0}', '{1}', '{2}', '{3}', '{4}', '', '{5}', '{6}', 1 ",
                        sIdEstado, sIdFarmacia, sArbol, sParametro, sValor, Grid.GetValueInt(i, (int)Cols.EsDeSistema), (int)Cols.EsEditable);

                    // Los parametros de Sistema no se actualizan 
                    if (Grid.GetValueInt(i, (int)Cols.EsDeSistema) == 1)
                    {
                        // Solo Oficina Central modifica los parametros de sistema
                        //if (GnConfiguracion.EsOficinaCentral) 
                        {
                            if (!leer.Exec(sSql))
                            {
                                bExito = false;
                                break;
                            } 
                        }
                    }
                    else
                    {
                        if (!leer.Exec(sSql))
                        {
                            bExito = false;
                            break;
                        }
                    }
                }


                if (bExito)
                {
                    cnn.CompletarTransaccion();
                    General.msjUser("Información guardada satisfactoriamente.");
                }
                else
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(leer, "btnAceptar_Click");
                    General.msjError("Ocurrió un error al grabar la información.");
                }

                cnn.Cerrar();
            } 
        }

        private void cboSucursales_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////if (cboSucursales.SelectedIndex != 0)
            ////    CargarDatosFarmacia();
        }

        private void grdParametros_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            CargaDescripcion(e.NewRow + 1);
        }

        private void CargaDescripcion(int Renglon)
        {
            lblDescripcion.Text = Grid.GetValue(Renglon, (int)Cols.Descripcion);
        }

        private void cboModulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid.Limpiar(false); 
        } 
    }
}
