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

////using DllFarmaciaSoft;
////using DllFarmaciaSoft.Lotes;
////using DllFarmaciaSoft.Procesos;
////using DllFarmaciaSoft.Devoluciones;
////using DllFarmaciaSoft.LimitesConsumoClaves;
////using DllFarmaciaSoft.Usuarios_y_Permisos;

using Dll_IMach4; 
using Dll_IMach4.Interface;

using Dll_IGPI; 
using Dll_IGPI.Interface; 


namespace DllRobotDispensador
{
    public enum RobotDispensador_Interface
    {
        Ninguno = 0,
        Medimat = 1, 
        ATP2 = 2, 
        GPI = 3 
    }
}

namespace DllRobotDispensador
{
    public interface IPuntoDeVenta_RobotDispensador
    {
        string FolioSolicitud { get; }
        bool PtoVtaOperable { get; }
        bool RequisicionRegistrada { get; }

        string ObtenerFolioSolicitud();
        bool Show(string IdProducto, string CodigoEAN);
        bool TerminarSolicitud(string FolioVenta);
        bool TerminarSolicitud(string Folio, string FolioVenta);
        void ValidarPuntoDeVenta();
    }

    public static class RobotDispensador
    {
        private static clsRobotDispensador pRobotDispensador;

        public static clsRobotDispensador Robot
        {
            get
            {
                if (pRobotDispensador == null)
                {
                    pRobotDispensador = new clsRobotDispensador();
                }

                return pRobotDispensador;
            }

            set { pRobotDispensador = value; }
        }
    }

    public class clsRobotDispensador : IPuntoDeVenta_RobotDispensador 
    {
        RobotDispensador_Interface tipoInterface = RobotDispensador_Interface.Ninguno;

        string sIdEmpresa = "";
        string sIdEstado = "";
        string sIdFarmacia = "";
        string sFolioSolicitud = "";
        bool bPtoVtaOperable = false;
        bool bRequisicionRegistrada = false;


        //private string sIdEmpresa = "001";
        private string sNombreEmpresa = "Intercontinental de Medicamentos";

        //private string sIdEstado = "";
        private string sNombreEstado = "";
        private string sClaveRenapo = "";
        //private string sIdFarmacia = "";
        private string sNombreFarmacia = "";

        private string sIdPersonalConectado = "";
        private string sNombrePersonal = "";
        private string sLoginUsuario = "";
        private string sPassUser = "";
        private bool bEsUsuarioTipoAdministrador = false;
        private string sRutaReportes = "";
        private DateTime dFechaMenorSistema = new DateTime();

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrabarError Error;

        Dll_IMach4.Interface.PuntoDeVenta IMedimat; // = new Dll_IMach4.Interface.PuntoDeVenta();
        Dll_IGPI.Interface.PuntoDeVenta IGPI; // = new Dll_IGPI.Interface.PuntoDeVenta(); 

        //clsRecetaElectronica__SIADISSEP receta_SIADISSEP;
        //clsRecetaElectronica__INTERMED receta_INTERMED;

        bool bConfiguracion_Cargada = false; 
        bool bRobotInstalado = false;
        bool bRobotnstalado_Validado = false;
        bool bEsServidorInterface = false;
        bool bEsClienteInterface = false;
        bool bEsClienteInterface_Validado = false;
        string sPuertoDeSalida = "";

        basGenerales Fg = new basGenerales();

        public clsRobotDispensador()
        {
            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, General.DatosApp, "RobotDispensador.RobotDispensador");

            ObtenerConfiguracion(false); 
        }

        #region Propiedades
        public RobotDispensador_Interface Interface
        {
            get { return tipoInterface; }
        }

        public string PuertoDeDispensacion
        {
            get { return sPuertoDeSalida; }
        }

        public string FolioSolicitud
        {
            get { return sFolioSolicitud; }
        }

        public bool PtoVtaOperable
        {
            get { return bPtoVtaOperable; }
        }

        public bool RequisicionRegistrada
        {
            get { return bRequisicionRegistrada; }
        }

        public bool EsClienteInterface
        {
            get 
            {
                bEsClienteInterface = localEsClienteInterface(); 

                return bEsClienteInterface; 
            }
        }

        public string PuertoDeSalida
        {
            get 
            {
                sPuertoDeSalida = localPuertoDeSalida();

                return sPuertoDeSalida; 
            }
        }

        public string EmpresaConectada
        {
            get { return sIdEmpresa; }
            set { sIdEmpresa = Fg.PonCeros(value, 3); }
        }

        public string EmpresaConectadaNombre
        {
            get { return sNombreEmpresa; }
            set { sNombreEmpresa = value; }
        }

        public string EstadoConectado
        {
            get { return sIdEstado; }
            set { sIdEstado = Fg.PonCeros(value, 2); }
        }

        public string EstadoConectadoNombre
        {
            get { return sNombreEstado; }
            set { sNombreEstado = value; }
        }

        public string ClaveRENAPO
        {
            get { return sClaveRenapo; }
            set { sClaveRenapo = value.ToUpper(); }
        }

        public string FarmaciaConectada
        {
            get { return sIdFarmacia; }
            set { sIdFarmacia = Fg.PonCeros(value, 4); }
        }

        public string FarmaciaConectadaNombre
        {
            get { return sNombreFarmacia; }
            set { sNombreFarmacia = value; }
        }

        public string IdPersonal
        {
            get { return sIdPersonalConectado; }
            set { sIdPersonalConectado = value; }
        }

        public string NombrePersonal
        {
            get { return sNombrePersonal; }
            set { sNombrePersonal = value; }
        }

        public string LoginUsuario
        {
            get { return sLoginUsuario; }
            set { sLoginUsuario = value; }
        }

        public string PasswordUsuario
        {
            get { return sPassUser; }
            set
            {
                if (sPassUser == "")
                    sPassUser = value;
            }
        }

        public bool EsAdministrador
        {
            get { return bEsUsuarioTipoAdministrador; }
            set { bEsUsuarioTipoAdministrador = value; }
        }

        public DateTime FechaMinimaSistema
        {
            get { return dFechaMenorSistema; }
            set { dFechaMenorSistema = value; }
        }

        ////public string RutaReportes
        ////{
        ////    get
        ////    {
        ////        if (sRutaReportes == "")
        ////        {
        ////            sRutaReportes = pParametros.GetValor("RutaReportes");
        ////        }
        ////        return sRutaReportes;
        ////    }
        ////    set { sRutaReportes = value; }
        ////}
        #endregion Propiedades

        #region Funciones y Procedimientos Privados
        private bool localEsClienteInterface()
        {
            bool bRegresa = false;

            ////ObtenerConfiguracion(); 
            switch (tipoInterface)
            {
                case RobotDispensador_Interface.Medimat:
                    bRegresa = Dll_IMach4.IMach4.EsClienteIMach4;
                    break;

                case RobotDispensador_Interface.GPI:
                    bRegresa = Dll_IGPI.IGPI.EsClienteIGPI;
                    break;
            }

            return bRegresa;
        }

        private string localPuertoDeSalida()
        {
            string sRegresa = "";

            ////ObtenerConfiguracion(); 
            switch (tipoInterface)
            {
                case RobotDispensador_Interface.Medimat:
                    sRegresa = Dll_IMach4.IMach4.PuertoDeDispensacion;
                    break;

                case RobotDispensador_Interface.GPI:
                    sRegresa = Dll_IGPI.IGPI.PuertoDeDispensacion;
                    break;
            }

            return sRegresa;
        }

        private void Inicializar_Interface()
        {
            switch (tipoInterface)
            {
                case RobotDispensador_Interface.Medimat:

                    IMach4.EmpresaConectada = RobotDispensador.Robot.EmpresaConectada; // = DtGeneral.EmpresaConectada;
                    IMach4.EmpresaConectadaNombre = RobotDispensador.Robot.EmpresaConectadaNombre; //  = DtGeneral.EmpresaConectadaNombre;
                    IMach4.EstadoConectado = RobotDispensador.Robot.EstadoConectado; //  = DtGeneral.EstadoConectado;
                    IMach4.EstadoConectadoNombre = RobotDispensador.Robot.EstadoConectadoNombre; //  = DtGeneral.EstadoConectadoNombre;
                    IMach4.FarmaciaConectada = RobotDispensador.Robot.FarmaciaConectada; //  = DtGeneral.FarmaciaConectada;
                    IMach4.FarmaciaConectadaNombre = RobotDispensador.Robot.FarmaciaConectadaNombre; //  = DtGeneral.FarmaciaConectadaNombre;

                    IMach4.IdPersonal = RobotDispensador.Robot.IdPersonal; //  = DtGeneral.IdPersonal;
                    IMach4.NombrePersonal = RobotDispensador.Robot.NombrePersonal; //  = DtGeneral.NombrePersonal;
                    IMach4.LoginUsuario = RobotDispensador.Robot.LoginUsuario; //  = DtGeneral.LoginUsuario;
                    IMach4.PasswordUsuario = RobotDispensador.Robot.PasswordUsuario; //  = DtGeneral.PasswordUsuario;

                    IMedimat = new Dll_IMach4.Interface.PuntoDeVenta();
                    break;


                case RobotDispensador_Interface.GPI:

                    Dll_IGPI.IGPI.EmpresaConectada = RobotDispensador.Robot.EmpresaConectada; // = DtGeneral.EmpresaConectada;
                    Dll_IGPI.IGPI.EmpresaConectadaNombre = RobotDispensador.Robot.EmpresaConectadaNombre; //  = DtGeneral.EmpresaConectadaNombre;
                    Dll_IGPI.IGPI.EstadoConectado = RobotDispensador.Robot.EstadoConectado; //  = DtGeneral.EstadoConectado;
                    Dll_IGPI.IGPI.EstadoConectadoNombre = RobotDispensador.Robot.EstadoConectadoNombre; //  = DtGeneral.EstadoConectadoNombre;
                    Dll_IGPI.IGPI.FarmaciaConectada = RobotDispensador.Robot.FarmaciaConectada; //  = DtGeneral.FarmaciaConectada;
                    Dll_IGPI.IGPI.FarmaciaConectadaNombre = RobotDispensador.Robot.FarmaciaConectadaNombre; //  = DtGeneral.FarmaciaConectadaNombre;

                    Dll_IGPI.IGPI.IdPersonal = RobotDispensador.Robot.IdPersonal; //  = DtGeneral.IdPersonal;
                    Dll_IGPI.IGPI.NombrePersonal = RobotDispensador.Robot.NombrePersonal; //  = DtGeneral.NombrePersonal;
                    Dll_IGPI.IGPI.LoginUsuario = RobotDispensador.Robot.LoginUsuario; //  = DtGeneral.LoginUsuario;
                    Dll_IGPI.IGPI.PasswordUsuario = RobotDispensador.Robot.PasswordUsuario; //  = DtGeneral.PasswordUsuario;

                    IGPI = new Dll_IGPI.Interface.PuntoDeVenta();
                    break;
            }
        }

        private bool validarInterface_Instadala()
        {
            bool bRegresa = false;
            string sSql = string.Format(
                " Select Name From Sysobjects (NoLock) Where Name = 'IMach_CFGC_Clientes' and xType = 'U' " +
                " Union " +
                " Select Name From Sysobjects (NoLock) Where Name = 'IGPI_CFGC_Clientes' and xType = 'U' "
                );

            if (!leer.Exec(sSql))
            {
            }
            else
            {
                bRegresa = leer.Leer();
            }
            return bRegresa;
        }

        private void localObtenerConfiguracion(bool MostrarMensaje)
        {
            string sSql = string.Format(
                "Select * From INT_RobotDispensador (Nolock) Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Status = 'A' ",
                sIdEmpresa, sIdEstado, sIdFarmacia);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ObtenerConfiguracion()");
                General.msjError("Ocurrió un error al configurar la interface de Robot Dispensador.");
            }
            else
            {
                if (!leer.Leer())
                {
                    if (MostrarMensaje)
                    {
                        General.msjUser("No se encontro información de configuración de Interface de Robot Dispensador.");
                    }
                }
                else
                {
                    if (leer.Registros == 1)
                    {
                        tipoInterface = (RobotDispensador_Interface)leer.CampoInt("Interface");
                        Inicializar_Interface();
                    }
                    else
                    {
                        ////General.msjError("Se encontro información de configuración de multiples Interfaces de Robot Dispensador.");
                    }
                }
            }
        }
        #endregion Funciones y Procedimientos Privados

        #region Funciones y Procedimientos Publicos
        public void Reset()
        {

            sFolioSolicitud = "";
            bPtoVtaOperable = false;
            bRequisicionRegistrada = false;
        }

        public void ObtenerConfiguracion(bool MostrarMensaje)
        {
            ////if (!bConfiguracion_Cargada)
            if ( validarInterface_Instadala() ) 
            {
                ////bConfiguracion_Cargada = true;
                localObtenerConfiguracion(MostrarMensaje);

            }
        }

        public string ObtenerFolioSolicitud()
        {
            string sRegresa = "";

            ////ObtenerConfiguracion(); 
            switch (tipoInterface)
            {
                case RobotDispensador_Interface.Medimat:
                    sRegresa = IMedimat.ObtenerFolioSolicitud();
                    break;

                case RobotDispensador_Interface.GPI:
                    sRegresa = IGPI.ObtenerFolioSolicitud();
                    break;
            }

            return sRegresa;
        }

        public bool Show(string IdProducto, string CodigoEAN)
        {
            bool bRegresa = false;

            ////ObtenerConfiguracion();
            switch (tipoInterface)
            {
                case RobotDispensador_Interface.Medimat:
                    bRegresa = IMedimat.Show(IdProducto, CodigoEAN);
                    break;

                case RobotDispensador_Interface.GPI:
                    bRegresa = IGPI.Show(IdProducto, CodigoEAN);
                    break;
            }

            return bRegresa; 
        }

        public bool TerminarSolicitud(string FolioVenta)
        {
            bool bRegresa = false;

            ////ObtenerConfiguracion();
            switch (tipoInterface)
            {
                case RobotDispensador_Interface.Medimat:
                    bRegresa = IMedimat.TerminarSolicitud(FolioVenta);
                    break;

                case RobotDispensador_Interface.GPI:
                    bRegresa = IGPI.TerminarSolicitud(FolioVenta);
                    break;
            }

            return bRegresa; 
        }

        public bool TerminarSolicitud(string Folio, string FolioVenta)
        {
            bool bRegresa = false;

            ////ObtenerConfiguracion();
            switch (tipoInterface)
            {
                case RobotDispensador_Interface.Medimat:
                    bRegresa = IMedimat.TerminarSolicitud(Folio, FolioVenta);
                    break;

                case RobotDispensador_Interface.GPI:
                    bRegresa = IGPI.TerminarSolicitud(Folio, FolioVenta);
                    break;
            }

            return bRegresa; 
        }

        public void ValidarPuntoDeVenta()
        {
            ////ObtenerConfiguracion();
            switch (tipoInterface)
            {
                case RobotDispensador_Interface.Medimat:
                    Dll_IMach4.IMach4.ValidarPuntoDeVenta();
                    break;

                case RobotDispensador_Interface.GPI:
                    Dll_IGPI.IGPI.ValidarPuntoDeVenta();
                    break;
            }
        }
        #endregion Funciones y Procedimientos Publicos
    }
}
