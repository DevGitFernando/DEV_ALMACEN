using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_ControlsCS;
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;

namespace DllFarmaciaSoft.Lotes
{
    internal partial class FrmVerificarSalidaLotes_Farmacia : FrmBaseExt 
    {
        public DataSet dtsLotes = clsLotes.PreparaDtsLotes();
        clsGrid myGrid;

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        // clsConsultas query;

        string sTabla = "";
        public bool ErrorAlValidarSalida = false; 

        public FrmVerificarSalidaLotes_Farmacia()
        {
            InitializeComponent();

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow); 
        }

        private void FrmVerificarSalidaLotes_Farmacia_Load(object sender, EventArgs e)
        {
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name); 
        }

        public bool VerificarExistenciasConError(DataSet Lotes)
        {
            return VerificarExistenciasConError(Lotes, false); 
        }

        public bool VerificarExistenciasConError(DataSet Lotes, bool MostrarMsj) 
        {
            bool bRegresa = false;
            ErrorAlValidarSalida = false; 

            cnn = new clsConexionSQL(General.DatosConexion);
            leer = new clsLeer(ref cnn);
            dtsLotes = Lotes;
            myGrid.Limpiar(false); 

            if (GenerarTabla())
            {
                bRegresa = VerificarDatos(); 
            }

            // Mostrar los Lotes con Errores 
            if (bRegresa && ErrorAlValidarSalida) 
            {
                bRegresa = false;
                this.ShowDialog();
            }
            else
            {
                if (MostrarMsj)
                {
                    General.msjUser("No se encontrarón diferencias entre las Existencias y las Cantidades capturadas."); 
                }
            }

            return bRegresa; 
        }

        #region Funciones y Procedimientos Privados 
        private bool GenerarTabla()
        {
            bool bRegresa = true;
            string sSql = string.Format(" Select Top 0 IdEmpresa, IdEstado, IdFarmacia, SKU, IdSubFarmacia, " + 
                " IdProducto, CodigoEAN, ClaveLote, Existencia as Cantidad " +
                " Into {0} From FarmaciaProductos_CodigoEAN_Lotes ", GetTabla());

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "GenerarTabla()");
                General.msjError("Ocurrió un error al Revisar la lista de productos."); 
            }
            else
            {
                bRegresa = CargarDatos(); 
            }

            return bRegresa; 
        }

        private bool VerificarDatos()
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_Verificar_Salida @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @TablaBase = '{3}' ",                   
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sTabla);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "VerificarDatos()");
                General.msjError("Ocurrió un error al Revisar la lista de productos."); 
            }
            else
            {
                if (leer.Leer())
                {
                    ErrorAlValidarSalida = true; 
                    myGrid.LlenarGrid(leer.DataSetClase); 
                }
            }

            EliminarTablaProceso(); 

            return bRegresa; 
        }

        private bool CargarDatos()
        {
            bool bRegresa = true;
            string sSql = ""; 
            clsLeer leerLotes = new clsLeer();


            leerLotes.DataSetClase = dtsLotes;
            if (leerLotes.ExisteTablaColumna(1, "Cantidad"))
            {
                leerLotes.DataRowsClase = dtsLotes.Tables[0].Select(" Cantidad > 0 ");
            }

            while (leerLotes.Leer())
            {
                sSql = string.Format("Insert Into {0} ( IdEmpresa, IdEstado, IdFarmacia, SKU, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, Cantidad ) " +
                    "Select '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}' " +
                    "", sTabla,
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                    leerLotes.Campo("SKU"), 
                    leerLotes.Campo("IdSubFarmacia"), leerLotes.Campo("IdProducto"), leerLotes.Campo("CodigoEAN"), 
                    leerLotes.Campo("ClaveLote"), leerLotes.Campo("Cantidad"));

                if (leerLotes.CampoInt("Cantidad") > 0)
                {
                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }

            return bRegresa; 
        }

        private string MarcaTiempo()
        {
            string sRegresa = "";
            DateTime dt = DateTime.Now;

            sRegresa += Fg.PonCeros(dt.Year, 4);
            sRegresa += Fg.PonCeros(dt.Month, 2);
            sRegresa += Fg.PonCeros(dt.Day, 2);
            sRegresa += "_";
            sRegresa += Fg.PonCeros(dt.Hour, 2);
            sRegresa += Fg.PonCeros(dt.Minute, 2);
            sRegresa += Fg.PonCeros(dt.Second, 2);

            return sRegresa; 
        }

        private string GetTabla()
        {
            string sRegresa = "tmpListaLotes_" + General.MacAddress + "_" + MarcaTiempo();
            sTabla = sRegresa; 
            return sRegresa; 
        }

        private void EliminarTablaProceso()
        {
            string sSql = string.Format("If Exists ( Select Name From sysobjects (noLock) Where name = '{0}' and xType = 'U' )   Drop Table {0} ", sTabla);

            if (!DtGeneral.EsEquipoDeDesarrollo)
            {
                leer.Exec(sSql);
            }
        }
        #endregion Funciones y Procedimientos Privados

    }
}
