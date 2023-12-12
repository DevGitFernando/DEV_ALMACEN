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

namespace DllFarmaciaSoft
{
    public enum ExpedienteElectronico_Interface
    {
        Ninguno = 0,
        SIADISEEP = 1,
        SIGHO = 2,
        INTERMED = 900
    }
}

namespace Farmacia.ECE
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

        bool InformacionCargada { get; }
        string CIE_10 { get ; }
        string Servicio { get; }
        string Area { get; }
        string TipoDeSurtimiento { get; }


        bool RegistrarAtencion(clsLeer LeerGuardar, string FolioDeVenta);
        bool SeleccionarRecetasParaSurtido(string IdCliente, string IdSubCliente); 
    }

    public interface IRecetaElectronica_ECE
    {
        string FolioVenta { get; set; }
        DataSet Resultado_Busqueda { get; }
        string ListaClavesSSA { get; }

        bool RegistrarAtencion(clsLeer LeerGuardar, string FolioReferencia, string FolioDeVenta);
        bool SeleccionarRecetasParaSurtido(string IdCliente, string IdSubCliente);
        //void ObtenerInformacion();

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
        string sFolioReceta = "";
        string sFechaReceta = "";
        string sIdBeneficiario = "";
        string sIdMedico = "";
        string sCIE_10 = "";
        string sIdServicio = "";
        string sIdArea = "";
        string sTipoDeSurtimiento = ""; 

        string sFolioVenta = "";
        string sListaClavesSSA = "";
        bool bInformacionCargada = false; 

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrabarError Error;

        clsRecetaElectronica__SIADISSEP receta_SIADISSEP;
        clsRecetaElectronica__INTERMED receta_INTERMED;
        clsRecetaElectronica__SIGHO receta_SIGHO;

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
        public bool InformacionCargada
        {
            get { return bInformacionCargada; }
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
                    break;

                case ExpedienteElectronico_Interface.INTERMED:
                    receta_INTERMED = new clsRecetaElectronica__INTERMED();
                    break;

                case ExpedienteElectronico_Interface.SIGHO:
                    receta_SIGHO = new clsRecetaElectronica__SIGHO();
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
                detallesReceta = new FrmRecetasElectronicas_Claves(dtsDetalles_Claves);

                detallesReceta.MdiParent = FormPadre; 
                detallesReceta.Show();
            }
        }

        public bool SeleccionarRecetasParaSurtido(string IdCliente, string IdSubCliente)
        {
            bool bExistenDatos = false;
            bool bRegresa = false;
            clsLeer leerResultado = new clsLeer(); 
            clsLeer leerResultado_Aux = new clsLeer();

            this.Reset(); 

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
                    while (leerResultado_Aux.Leer())
                    {
                        sListaClavesSSA += string.Format("'{0}', ", leerResultado_Aux.Campo("ClaveSSA"));
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
            }

            return bRegresa; 
        }
        #endregion Funciones y Procedimientos Publicos
    
    }
}
