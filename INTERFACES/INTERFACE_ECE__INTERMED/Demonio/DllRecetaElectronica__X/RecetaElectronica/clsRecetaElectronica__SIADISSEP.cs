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
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos;
using DllFarmaciaSoft.Devoluciones;
using DllFarmaciaSoft.LimitesConsumoClaves;
using DllFarmaciaSoft.Usuarios_y_Permisos;

namespace DllRecetaElectronica.ECE
{
    internal class clsRecetaElectronica__SIADISSEP : IRecetaElectronica_ECE 
    {
        string sIdEmpresa = DtGeneral.EmpresaConectada;
        string sIdEstado = DtGeneral.EstadoConectado;
        string sIdFarmacia = DtGeneral.FarmaciaConectada;
        string sIdPersonal = DtGeneral.IdPersonal; 

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrabarError Error;

        string sFolioVenta = "";
        DataSet dtsResultado;
        string sListaClavesSSA = "";

        FrmListadoRecetasElectronicas listadoDeRecetas;
        string sFolioReferencia = ""; 

        public clsRecetaElectronica__SIADISSEP()
        {
            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, GnFarmacia.DatosApp, "Farmacia.ECE.clsRecetaElectronica__SIADISSEP");

            dtsResultado = new DataSet(); 
        }

        #region Propiedades
        public string FolioVenta
        {
            get { return sFolioVenta; }
            set { sFolioVenta = value; }
        }

        public string ListaClavesSSA       
        {
            get { return sListaClavesSSA; }
        }

        public DataSet Resultado_Busqueda
        {
            get { return dtsResultado; }
        }
        #endregion Propiedades

        #region Funciones y Procedimientos Publicos
        public bool SeleccionarRecetasParaSurtido(string IdCliente, string IdSubCliente)
        {
            bool bRegresa = false;             

            string sSql = string.Format("Exec spp_INT_SIADISSEP__RecetasElectronicas_0009_ObtenerRecetasParaSurtido " + 
                "  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}' ", sIdEmpresa, sIdEstado, sIdFarmacia);

            dtsResultado = new DataSet();
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "SeleccionarRecetasParaSurtido");
                General.msjAviso("Ocurrió un error al obtener la información de recetas.");
            }
            else
            {
                ////bRegresa = true; 
                listadoDeRecetas = new FrmListadoRecetasElectronicas(leer.DataSetClase);
                sFolioReferencia = listadoDeRecetas.SeleccionarReceta();

                if (sFolioReferencia != "")
                {
                    bRegresa = ObtenerInformacion(IdCliente, IdSubCliente); 
                }
            }

            return bRegresa; 
        }

        public bool RegistrarAtencion(clsLeer LeerGuardar, string FolioReferencia, string FolioDeVenta)
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_INT_SIADISSEP__RecetasElectronicas_0011_MarcarRecetaSurtido " +
                "  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioInterface = '{3}', @FolioSurtido = '{4}' ",
                sIdEmpresa, sIdEstado, sIdFarmacia, FolioReferencia, FolioDeVenta);

            if (LeerGuardar.Exec(sSql))
            {
                bRegresa = true;
            }

            return bRegresa; 
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private bool ObtenerInformacion(string IdCliente, string IdSubCliente)
        {
            bool bRegresa = false;

            string sSql = string.Format("Exec spp_INT_SIADISSEP__RecetasElectronicas_0010_ObtenerRecetasParaSurtido_Detalles " +
                "  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdCliente = '{3}', @IdSubCliente = '{4}', @FolioInterface = '{5}', @IdPersonal = '{6}' ",
                sIdEmpresa, sIdEstado, sIdFarmacia, IdCliente, IdSubCliente, sFolioReferencia, sIdPersonal);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ObtenerInformacion");
                General.msjAviso("Ocurrió un error al obtener la información de la receta seleccionada.");
            }
            else
            {
                if (leer.Leer())
                {
                    bRegresa = true;
                    leer.RenombrarTabla(1, "Encabezado");
                    leer.RenombrarTabla(2, "Detalles");
                    ////leer.RenombrarTabla(3, "Detalles_Claves");
                    dtsResultado = leer.DataSetClase;
                }
            }

            return bRegresa;  
        }
        #endregion Funciones y Procedimientos Privados
    }
}
