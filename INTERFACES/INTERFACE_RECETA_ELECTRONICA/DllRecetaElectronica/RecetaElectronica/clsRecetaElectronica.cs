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

using Dll_IRE_INTERMED.Informacion; 


namespace DllFarmaciaSoft
{
    public enum ExpedienteElectronico_Interface
    {
        Ninguno = 0,
        SIADISEEP = 1,
        SIGHO = 2,
        AMPM = 3, 
        SESEQ = 4, 
        INTERMED = 900
    }
}

namespace DllRecetaElectronica.ECE
{
    public interface IRecetaElectronica
    {
        string FolioReceta { get; }
        string IdBeneficiario { get; }
        string IdMedico { get; }
        string FechaReceta { get; }
        string FolioVenta { get; set; }
        string ListaClavesSSA { get; }
        string ListaClavesSSA_Extended { get; }
        bool CapturaInformacion { get; }
        bool InformacionCargada { get; }
        string CIE_10 { get ; }
        string Servicio { get; }
        string Area { get; }
        string TipoDeSurtimiento { get; }


        bool RegistrarAtencion(clsLeer LeerGuardar, string FolioDeVenta);
        bool SeleccionarRecetasParaSurtido();
        bool SeleccionarRecetasParaSurtido(string IdCliente, string IdSubCliente);

        bool DescargarRecetas();
        bool DescargarRecetaEspecifica();
        bool VisualizarEstadisticas();
        DataSet ActualizarListadoDeRecetas();
    }

    public interface IRecetaElectronica_ECE
    {
        string FolioReferencia { get; }

        string FolioVenta { get; set; }
        DataSet Resultado_Busqueda { get; }
        DataSet ListadoDeRecetasParaSurtido { get; }
        string ListaClavesSSA { get; }

        string CLUES { get; }
        bool CapturaInformacion { get; }

        bool RegistrarAtencion(clsLeer LeerGuardar, string FolioReferencia, string FolioDeVenta);
        bool SeleccionarRecetasParaSurtido();
        bool SeleccionarRecetasParaSurtido(string IdCliente, string IdSubCliente);
        bool ObtenerRecetasParaSurtido(string IdCliente, string IdSubCliente, string Referencia_01, string Referencia_02, string Fecha_01, string Fecha_02);
        //void ObtenerInformacion();
        bool EnviarRecetasAtendidas(); 
        bool DescargarRecetas();
        bool DescargarRecetaEspecifica();
        bool VisualizarEstadisticas();
        DataSet ActualizarListadoDeRecetas(); 
    }


    public static class RecetaElectronica
    {
        private static clsRecetaElectronica pRecetaElectronica;

        public static clsRecetaElectronica Receta
        {
            get
            {
                if (pRecetaElectronica == null)
                {
                    pRecetaElectronica = new clsRecetaElectronica();
                }

                return pRecetaElectronica;
            }

            set { pRecetaElectronica = value; }
        }
    }

    public class clsRecetaElectronica : IRecetaElectronica
    {
        ExpedienteElectronico_Interface tipoInterface = ExpedienteElectronico_Interface.Ninguno;

        string sIdEmpresa = DtGeneral.EmpresaConectada;
        string sIdEstado = DtGeneral.EstadoConectado;
        string sIdFarmacia = DtGeneral.FarmaciaConectada;
        string sSecuenciador = "";

        string sIdCliente = "";
        string sIdSubCliente = ""; 

        string sFolioReceta = "";
        string sFechaReceta = "";
        string sIdBeneficiario = "";
        string sIdMedico = "";
        string sCIE_10 = "";
        string sIdServicio = "";
        string sIdArea = "";
        string sTipoDeSurtimiento = ""; 

        string sFolioVenta = "";
        string sFolioRegistro = "";
        string sListaClavesSSA = "";
        bool bInformacionCargada = false;
        bool bCapturaInformacion = false; 

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrabarError Error;

        clsRecetaElectronica__SIADISSEP receta_SIADISSEP;
        clsRecetaElectronica__INTERMED receta_INTERMED;
        clsRecetaElectronica__SIGHO receta_SIGHO;
        clsRecetaElectronica__AMPM receta_AMPM;
        clsRecetaElectronica__SESEQ receta_SESEQ;

        DataSet dtsResultado;
        DataSet dtsEncabezado;
        DataSet dtsDetalles;
        DataSet dtsDetalles_Claves; 

        basGenerales Fg = new basGenerales();
        FrmRecetasElectronicas_Claves detallesReceta;
        bool bMostrandoDetallesReceta = false; 

        public clsRecetaElectronica()
        {
            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, GnFarmacia.DatosApp, "Farmacia.RecetaElectronica.RecetaElectronica");

            dtsResultado = new DataSet();
            dtsEncabezado = new DataSet();
            dtsDetalles = new DataSet();
            dtsDetalles_Claves = new DataSet(); 

            ObtenerConfiguracion(); 
        }

        #region Propiedades

        public ExpedienteElectronico_Interface Interface
        {
            get { return tipoInterface; }
        }

        public bool InformacionCargada
        {
            get { return bInformacionCargada; }
        }

        public bool CapturaInformacion
        {
            get { return bCapturaInformacion; }
            set { bCapturaInformacion = value; }
        }       

        public bool MostrandoDetallesReceta
        {
            get { return bMostrandoDetallesReceta; }
            set { bMostrandoDetallesReceta = value; }
        }

        public string FolioReceta
        {
            get { return sFolioReceta; }
        }


        public string FolioRegistro
        {
            get { return sFolioRegistro; }
            set { sFolioRegistro = value; }
        }

        public string IdBeneficiario
        {
            get { return sIdBeneficiario; }
        }

        public string IdMedico
        {
            get { return sIdMedico; }
        }

        public string FechaReceta
        {
            get { return sFechaReceta; }
        }

        public string FolioVenta
        {
            get { return sFolioVenta; }
            set { sFolioVenta = value; }
        }

        public string CIE_10
        {
            get { return sCIE_10; }
            set { sCIE_10 = value; }
        }

        public string Servicio
        {
            get { return sIdServicio; }
        }

        public string Area
        {
            get { return sIdArea; }
        }

        public string TipoDeSurtimiento
        {
            get { return sTipoDeSurtimiento; }
        }

        public string ListaClavesSSA
        {
            get { return sListaClavesSSA; }
        }

        public string ListaClavesSSA_Extended
        {
            get { return sListaClavesSSA.Replace("'", "''"); }
        }

        public DataSet Detalles_Claves
        {
            get { return dtsDetalles_Claves; }
        }

        #endregion Propiedades

        #region Funciones y Procedimientos Publicos 
        public void Reset()
        {
            dtsResultado = new DataSet();
            dtsEncabezado = new DataSet();
            dtsDetalles = new DataSet();

            sSecuenciador = ""; 
            sFolioReceta = "";
            sFechaReceta = "";
            sIdBeneficiario = "";
            sIdMedico = "";
            sCIE_10 = "";
            sIdServicio = "";
            sIdArea = "";
            sTipoDeSurtimiento = "";
            sListaClavesSSA = "";

            CerrarDetallesDeReceta(); 

            bInformacionCargada = false;
            ////bCapturaInformacion = false; 

        }

        public void ObtenerConfiguracion()
        {
            string sSql = string.Format(
                "Select * From INT_RecetaElectronica (Nolock) Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Status = 'A' ", 
                sIdEmpresa, sIdEstado, sIdFarmacia);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ObtenerConfiguracion()");
                General.msjError("Ocurrió un error al configurar la interface de Expediente Electrónico.");
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjUser("No se encontro información de configuración de Interface de Expediente Electrónico.");
                }
                else
                {
                    if (leer.Registros == 1)
                    {
                        tipoInterface = (ExpedienteElectronico_Interface)leer.CampoInt("Interface");
                        Inicializar_Interface();
                    }
                    else
                    {
                        General.msjError("Se encontro información de configuración de multiples Interfaces de Expediente Electrónico");
                    }
                }
            }
        }

        private void Inicializar_Interface()
        {
            switch (tipoInterface)
            {
                case ExpedienteElectronico_Interface.SIADISEEP:
                    receta_SIADISSEP = new clsRecetaElectronica__SIADISSEP();
                    bCapturaInformacion = receta_SIADISSEP.CapturaInformacion;
                    break;

                case ExpedienteElectronico_Interface.INTERMED:
                    receta_INTERMED = new clsRecetaElectronica__INTERMED();
                    bCapturaInformacion = receta_INTERMED.CapturaInformacion;
                    break;

                case ExpedienteElectronico_Interface.SIGHO:
                    receta_SIGHO = new clsRecetaElectronica__SIGHO();
                    bCapturaInformacion = receta_SIGHO.CapturaInformacion;
                    break;

                case ExpedienteElectronico_Interface.AMPM:
                    receta_AMPM = new clsRecetaElectronica__AMPM();
                    //bCapturaInformacion = receta_AMPM.CapturaInformacion;
                    break;

                case ExpedienteElectronico_Interface.SESEQ:
                    receta_SESEQ = new clsRecetaElectronica__SESEQ();
                    bCapturaInformacion = receta_SESEQ.CapturaInformacion; 
                    break;

            }
        }

        public void CerrarDetallesDeReceta()
        {
            try
            {
                if (detallesReceta != null)
                {
                    detallesReceta.Close();
                    detallesReceta = null;
                }
            }
            catch 
            { 
            }
        }


        public void MostrarDetallesDeReceta(Form FormPadre)
        {
            if (bMostrandoDetallesReceta)
            {
                detallesReceta.Activate(); 
            }
            else 
            {
                bMostrandoDetallesReceta = true;
                detallesReceta = new FrmRecetasElectronicas_Claves(sFolioReceta, dtsDetalles_Claves);

                detallesReceta.MdiParent = FormPadre; 
                detallesReceta.Show();
            }
        }

        public DataSet ActualizarListadoDeRecetas()
        {
            DataSet dtsResultado_Update = new DataSet(); 

            switch(tipoInterface)
            {
                case ExpedienteElectronico_Interface.SIADISEEP:
                    receta_SIADISSEP.FolioReferencia = sFolioRegistro;
                    dtsResultado_Update = receta_SIADISSEP.ActualizarListadoDeRecetas();
                    break;

                case ExpedienteElectronico_Interface.INTERMED:
                    receta_INTERMED.FolioReferencia = sFolioRegistro;
                    dtsResultado_Update = receta_INTERMED.ActualizarListadoDeRecetas();
                    break;

                case ExpedienteElectronico_Interface.SIGHO:
                    receta_SIGHO.FolioReferencia = sFolioRegistro;
                    dtsResultado_Update = receta_SIGHO.ActualizarListadoDeRecetas();
                    break;

                case ExpedienteElectronico_Interface.AMPM:
                    receta_AMPM.FolioReferencia = sFolioRegistro;
                    dtsResultado_Update = receta_AMPM.ActualizarListadoDeRecetas();
                    break;

                case ExpedienteElectronico_Interface.SESEQ:
                    receta_SESEQ.FolioReferencia = sFolioRegistro;
                    dtsResultado_Update = receta_SESEQ.ActualizarListadoDeRecetas();
                    break;

            }

            return dtsResultado_Update.Copy();
        }

        public bool SeleccionarRecetasParaSurtido()
        {
            return SeleccionarRecetasParaSurtido(sIdCliente, sIdSubCliente);
        }

        public bool SeleccionarRecetasParaSurtido(string IdCliente, string IdSubCliente)
        {
            bool bExistenDatos = false;
            bool bRegresa = false;
            clsLeer leerResultado = new clsLeer(); 
            clsLeer leerResultado_Aux = new clsLeer();

            this.Reset();

            sIdCliente = IdCliente;
            sIdSubCliente = IdSubCliente;

            if (tipoInterface == ExpedienteElectronico_Interface.Ninguno)
            {
                General.msjError("No se ha configurado el tipo de Interface de Expediente Electrónico.");
            }
            else
            {
                switch (tipoInterface)
                {
                    case ExpedienteElectronico_Interface.SIADISEEP:
                        receta_SIADISSEP.SeleccionarRecetasParaSurtido(IdCliente, IdSubCliente);
                        dtsResultado = receta_SIADISSEP.Resultado_Busqueda; 
                        break;

                    case ExpedienteElectronico_Interface.INTERMED:
                        receta_INTERMED.SeleccionarRecetasParaSurtido(IdCliente, IdSubCliente);
                        dtsResultado = receta_INTERMED.Resultado_Busqueda;
                        break;

                    case ExpedienteElectronico_Interface.SIGHO:
                        receta_SIGHO.SeleccionarRecetasParaSurtido(IdCliente, IdSubCliente);
                        dtsResultado = receta_SIGHO.Resultado_Busqueda;
                        break;

                    case ExpedienteElectronico_Interface.AMPM:
                        receta_AMPM.SeleccionarRecetasParaSurtido(IdCliente, IdSubCliente);
                        dtsResultado = receta_AMPM.Resultado_Busqueda;
                        break;

                    case ExpedienteElectronico_Interface.SESEQ:
                        receta_SESEQ.SeleccionarRecetasParaSurtido(IdCliente, IdSubCliente);
                        dtsResultado = receta_SESEQ.Resultado_Busqueda;
                        break;
                }
            }


            leerResultado.DataSetClase = dtsResultado;
            if (leerResultado.Leer())
            {
                bRegresa = true;

                leerResultado_Aux.DataTableClase = leerResultado.Tabla(1);
                if (leerResultado_Aux.Leer())
                {
                    bInformacionCargada = true;

                    sSecuenciador = leerResultado_Aux.Campo("Secuenciador");
                    sFolioReceta = leerResultado_Aux.Campo("FolioReceta");
                    sFechaReceta = leerResultado_Aux.Campo("FechaReceta");
                    sIdBeneficiario = leerResultado_Aux.Campo("IdBeneficiario");
                    sIdMedico = leerResultado_Aux.Campo("IdMedico");
                    sCIE_10 = leerResultado_Aux.Campo("CIE_10");
                    sIdServicio = leerResultado_Aux.Campo("IdServicio");
                    sIdArea = leerResultado_Aux.Campo("IdArea");
                    sTipoDeSurtimiento = leerResultado_Aux.Campo("TipoDispensacion");
                }


                leerResultado_Aux.DataTableClase = leerResultado.Tabla(2);
                if (leerResultado_Aux.Leer())
                {
                    dtsDetalles_Claves = leerResultado_Aux.DataSetClase; 

                    sListaClavesSSA = "";
                    leerResultado_Aux.RegistroActual = 1;

                    if (tipoInterface == ExpedienteElectronico_Interface.AMPM)
                    {
                        while (leerResultado_Aux.Leer())
                        {
                            sListaClavesSSA += string.Format("'{0}', ", leerResultado_Aux.Campo("CodigoEAN"));
                        }
                    }
                    else
                    {
                        while (leerResultado_Aux.Leer())
                        {
                            sListaClavesSSA += string.Format("'{0}', ", leerResultado_Aux.Campo("ClaveSSA"));
                        }
                    }

                    sListaClavesSSA = sListaClavesSSA.Trim();
                    sListaClavesSSA = Fg.Mid(sListaClavesSSA, 1, sListaClavesSSA.Length - 1); 
                }
            }

            return bRegresa; 
        }

        public bool RegistrarAtencion(clsLeer LeerGuardar, string FolioDeVenta)
        {
            bool bRegresa = false;

            switch (tipoInterface)
            {
                case ExpedienteElectronico_Interface.SIADISEEP:
                    bRegresa = receta_SIADISSEP.RegistrarAtencion(LeerGuardar, sSecuenciador, FolioDeVenta);
                    break;

                case ExpedienteElectronico_Interface.INTERMED:
                    bRegresa = receta_INTERMED.RegistrarAtencion(LeerGuardar, sSecuenciador, FolioDeVenta);
                    break;

                case ExpedienteElectronico_Interface.SIGHO:
                    bRegresa = receta_SIGHO.RegistrarAtencion(LeerGuardar, sSecuenciador, FolioDeVenta);
                    break;

                case ExpedienteElectronico_Interface.AMPM:
                    bRegresa = receta_AMPM.RegistrarAtencion(LeerGuardar, sSecuenciador, FolioDeVenta);
                    break;

                case ExpedienteElectronico_Interface.SESEQ:
                    bRegresa = receta_SESEQ.RegistrarAtencion(LeerGuardar, sSecuenciador, FolioDeVenta);
                    break;
            }

            return bRegresa; 
        }

        public bool EnviarRecetasAtendidas()
        {
            bool bRegresa = false;

            this.Reset();

            if(tipoInterface == ExpedienteElectronico_Interface.Ninguno)
            {
                General.msjError("No se ha configurado el tipo de Interface de Expediente Electrónico.");
            }
            else
            {
                switch(tipoInterface)
                {
                    case ExpedienteElectronico_Interface.SIADISEEP:
                        break;

                    case ExpedienteElectronico_Interface.INTERMED:
                        bRegresa = receta_INTERMED.EnviarRecetasAtendidas();
                        break;

                    case ExpedienteElectronico_Interface.SIGHO:
                        break;

                    case ExpedienteElectronico_Interface.AMPM:
                        break;

                    case ExpedienteElectronico_Interface.SESEQ:
                        break;
                }
            }

            return bRegresa;
        }

        public bool DescargarRecetas()
        {
            bool bRegresa = false;

            this.Reset();

            if(tipoInterface == ExpedienteElectronico_Interface.Ninguno)
            {
                General.msjError("No se ha configurado el tipo de Interface de Expediente Electrónico.");
            }
            else
            {
                switch(tipoInterface)
                {
                    case ExpedienteElectronico_Interface.SIADISEEP:
                        break;

                    case ExpedienteElectronico_Interface.INTERMED:
                        bRegresa = receta_INTERMED.DescargarRecetas();
                        break;

                    case ExpedienteElectronico_Interface.SIGHO:
                        break;

                    case ExpedienteElectronico_Interface.AMPM:
                        break;

                    case ExpedienteElectronico_Interface.SESEQ:
                        break;
                }
            }

            return bRegresa;
        }
        public bool DescargarRecetaEspecifica()
        {
            bool bRegresa = false;

            this.Reset();

            if(tipoInterface == ExpedienteElectronico_Interface.Ninguno)
            {
                General.msjError("No se ha configurado el tipo de Interface de Expediente Electrónico.");
            }
            else
            {
                switch(tipoInterface)
                {
                    case ExpedienteElectronico_Interface.SIADISEEP:
                        break;

                    case ExpedienteElectronico_Interface.INTERMED:
                        bRegresa = receta_INTERMED.DescargarRecetaEspecifica();
                        sFolioRegistro = receta_INTERMED.FolioReferencia;
                        break;

                    case ExpedienteElectronico_Interface.SIGHO:
                        break;

                    case ExpedienteElectronico_Interface.AMPM:
                        break;

                    case ExpedienteElectronico_Interface.SESEQ:
                        break;
                }
            }

            return bRegresa;
        }

        public bool VisualizarEstadisticas()
        {
            bool bRegresa = false;

            this.Reset();

            if(tipoInterface == ExpedienteElectronico_Interface.Ninguno)
            {
                General.msjError("No se ha configurado el tipo de Interface de Expediente Electrónico.");
            }
            else
            {
                switch(tipoInterface)
                {
                    case ExpedienteElectronico_Interface.SIADISEEP:
                        break;

                    case ExpedienteElectronico_Interface.INTERMED:
                        break;

                    case ExpedienteElectronico_Interface.SIGHO:
                        break;

                    case ExpedienteElectronico_Interface.AMPM:
                        break;

                    case ExpedienteElectronico_Interface.SESEQ:
                        receta_SESEQ.VisualizarEstadisticas();
                        break;
                }
            }

            return bRegresa;
        }
        #endregion Funciones y Procedimientos Publicos

    }
}
