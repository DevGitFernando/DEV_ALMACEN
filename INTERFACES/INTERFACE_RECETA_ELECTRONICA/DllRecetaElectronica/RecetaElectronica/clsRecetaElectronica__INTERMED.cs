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


using Dll_IRE_INTERMED;
using Dll_IRE_INTERMED.Clases;
using Dll_IRE_INTERMED.Informacion;

namespace DllRecetaElectronica.ECE
{
    internal class clsRecetaElectronica__INTERMED : IRecetaElectronica_ECE 
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
        DataSet dtsListadoDeRecetasParaSurtido; 
        string sListaClavesSSA = "";
        bool bCapturaInformacion = false;
        string sCLUES = "";

        string sIdCliente = "";
        string sIdSubCliente = ""; 

        FrmListadoRecetasElectronicas listadoDeRecetas;
        string sFolioReferencia = ""; 

        public clsRecetaElectronica__INTERMED()
        {
            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, GnFarmacia.DatosApp, "Farmacia.ECE.clsRecetaElectronica__INTERMED");

            dtsResultado = new DataSet();
            dtsListadoDeRecetasParaSurtido = new DataSet();

            ObtenerConfiguracion();
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

        public bool CapturaInformacion
        {
            get { return bCapturaInformacion; }
        }

        public DataSet Resultado_Busqueda
        {
            get { return dtsResultado; }
        }

        public DataSet ListadoDeRecetasParaSurtido
        {
            get { return dtsListadoDeRecetasParaSurtido; }
        }

        public string FolioReferencia
        {
            get { return sFolioReferencia; }
            set { sFolioReferencia = value; }
        }
        public string CLUES
        {
            get { return sCLUES; }
        }
        #endregion Propiedades

        #region Funciones y Procedimientos Publicos
        public bool ObtenerRecetasParaSurtido(string IdCliente, string IdSubCliente, string Referencia_01, string Referencia_02, string Fecha_01, string Fecha_02)
        {
            bool bRegresa = false;
            return bRegresa;
        }

        public bool SeleccionarRecetasParaSurtido()
        {
            return SeleccionarRecetasParaSurtido(sIdCliente, sIdSubCliente);
        }

        public bool SeleccionarRecetasParaSurtido(string IdCliente, string IdSubCliente)
        {
            bool bRegresa = false;

            sIdCliente = IdCliente;
            sIdSubCliente = IdSubCliente; 

            string sSql = string.Format("Exec spp_INT_RE_INTERMED__RecetasElectronicas_0009_ObtenerRecetasParaSurtido " + 
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

        public DataSet ActualizarListadoDeRecetas()
        {
            string sSql = string.Format("Exec spp_INT_RE_INTERMED__RecetasElectronicas_0009_ObtenerRecetasParaSurtido " +
                "@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio = '{3}' ", sIdEmpresa, sIdEstado, sIdFarmacia, sFolioReferencia);

            dtsListadoDeRecetasParaSurtido = new DataSet();
            if(!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "SeleccionarRecetasParaSurtido");
                General.msjAviso("Ocurrió un error al obtener la información de recetas.");
            }
            else
            {
                dtsListadoDeRecetasParaSurtido = leer.DataSetClase;
            }

            return dtsListadoDeRecetasParaSurtido;
        }

        public bool RegistrarAtencion(clsLeer LeerGuardar, string FolioReferencia, string FolioDeVenta)
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_INT_RE_INTERMED__RecetasElectronicas_0011_MarcarRecetaSurtido " +
                "  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioInterface = '{3}', @FolioSurtido = '{4}' ",
                sIdEmpresa, sIdEstado, sIdFarmacia, FolioReferencia, FolioDeVenta);

            if (LeerGuardar.Exec(sSql))
            {
                bRegresa = true;
            }

            return bRegresa; 
        }

        public bool EnviarRecetasAtendidas()
        {
            bool bRegresa = false;

            FrmInformacionDeRecetas recetas = new FrmInformacionDeRecetas(TipoProcesoReceta.EnviarInformacionSurtido);

            bRegresa = recetas.ProcesarSolicitudDeInformacion();

            return bRegresa;
        }

        public bool DescargarRecetas()
        {
            bool bRegresa = false;

            FrmInformacionDeRecetas recetas = new FrmInformacionDeRecetas(TipoProcesoReceta.DescargaRecetasMasivo);

            bRegresa = recetas.ProcesarSolicitudDeInformacion(); 

            return bRegresa;
        }
        public bool DescargarRecetaEspecifica()
        {
            bool bRegresa = false;

            FrmRecetaEspecifica Receta = new FrmRecetaEspecifica(sCLUES);

            Receta.ShowDialog();
            sFolioReferencia = Receta.Folio;

            bRegresa = sFolioReferencia != "";

            return bRegresa;
        }
        public bool VisualizarEstadisticas()
        {
            bool bRegresa = false;

            return bRegresa;
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private bool ObtenerConfiguracion()
        {
            bool bRegresa = false;

            string sSql = string.Format("Select *, Referencia_SIADISSEP as CLUES \n" +
                "From INT_RE_INTERMED__CFG_Farmacias_UMedicas (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' ",
                sIdEmpresa, sIdEstado, sIdFarmacia);


            bCapturaInformacion = false;
            if(!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ObtenerConfiguracion");
                //General.msjAviso("Ocurrió un error al obtener la información de la receta seleccionada.");
            }
            else
            {
                if(leer.Leer())
                {
                    bRegresa = true;
                    sCLUES = leer.Campo("CLUES");
                    bCapturaInformacion = leer.CampoBool("CapturaInformacion");
                }
            }

            return bRegresa;
        }

        private bool ObtenerInformacion(string IdCliente, string IdSubCliente)
        {
            bool bRegresa = false;

            string sSql = string.Format("Exec spp_INT_RE_INTERMED__RecetasElectronicas_0010_ObtenerRecetasParaSurtido_Detalles " +
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
