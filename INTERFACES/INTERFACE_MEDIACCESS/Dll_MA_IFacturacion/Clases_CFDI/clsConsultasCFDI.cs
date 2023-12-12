using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

using DllFarmaciaSoft;

namespace Dll_MA_IFacturacion
{
    public partial class clsConsultas_CFDI
    {
        #region Declaración de variables
        //private wsConexion.wsControlObras cnnWebServ; // = new wsConexion.wsConexionDB();
        private clsErrorManager error; // = new clsErrorManager();
        private clsLogError errorLog; //  = new clsLogError();
        //private DialogResult myResult = new DialogResult();

        //private DataSet dtsError = new DataSet();
        //private DataSet dtsClase = new DataSet();
        //////private string strCnnString = General.CadenaDeConexion;
        private bool bUsarCnnRedLocal = true;
        private bool bExistenDatos = false;
        private bool bEjecuto = false;
        private DataSet myDataset;
        string sQuery = "";
        string sInicio = "Set DateFormat YMD ";

        string sNameDll = "";
        string sPantalla = "";
        string sVersion = "";
        bool bMostrarMsjLeerVacio = true;
        bool bEsPublicoGeneral = false;
        bool bEsClienteIMach = false; //IMach4.EsClienteIMach4; 
        int iLenCodigoEAN = 15;

        //PRUEBA
        private basGenerales Fg; // = new basGenerales();
        private clsCriptografo Cryp; // = new clsCriptografo();
        ////private Cls_Acceso_a_Datos_Sql Datos = new Cls_Acceso_a_Datos_Sql();
        private clsDatosConexion DatosConexion;
        private clsConexionSQL ConexionSql;

        // Consulta ejecutada
        protected string sConsultaExec = "";
        #endregion

        #region Constructores de clase y destructor
        public clsConsultas_CFDI(clsDatosConexion Conexion, clsDatosApp DatosApp, string Pantalla)
        {
            //General.msjAviso(Conexion.CadenaConexion); 
            this.sNameDll = DatosApp.Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = DatosApp.Version;

            DatosConexion = Conexion;
            ConexionSql = new clsConexionSQL(Conexion);
            //ConexionSql.SetConnectionString();

            error = new clsErrorManager();
            errorLog = new clsLogError();
            Fg = new basGenerales();
            Cryp = new clsCriptografo();

        }

        public clsConsultas_CFDI(clsDatosConexion Conexion, clsDatosApp DatosApp, string Pantalla, bool MostrarMsjLeerVacio)
        {
            //General.msjAviso(Conexion.CadenaConexion); 
            this.sNameDll = DatosApp.Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = DatosApp.Version;
            this.bMostrarMsjLeerVacio = MostrarMsjLeerVacio;

            DatosConexion = Conexion;
            ConexionSql = new clsConexionSQL(Conexion);
            //ConexionSql.SetConnectionString();

            error = new clsErrorManager();
            errorLog = new clsLogError();
            Fg = new basGenerales();
            Cryp = new clsCriptografo();

        }

        public clsConsultas_CFDI(clsDatosConexion Conexion, string Modulo, string Pantalla, string Version)
        {
            //General.msjAviso(Conexion.CadenaConexion); 
            this.sNameDll = Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = Version;

            DatosConexion = Conexion;
            ConexionSql = new clsConexionSQL(Conexion);
            //ConexionSql.SetConnectionString();

            error = new clsErrorManager();
            errorLog = new clsLogError();
            Fg = new basGenerales();
            Cryp = new clsCriptografo();
        }

        public clsConsultas_CFDI(clsDatosConexion Conexion, string Modulo, string Pantalla, string Version, bool MostrarMsjLeerVacio)
        {
            //General.msjAviso(Conexion.CadenaConexion); 

            this.sNameDll = Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = Version;
            this.bMostrarMsjLeerVacio = MostrarMsjLeerVacio;

            DatosConexion = Conexion;
            ConexionSql = new clsConexionSQL(Conexion);
            //ConexionSql.SetConnectionString();

            error = new clsErrorManager();
            errorLog = new clsLogError();
            Fg = new basGenerales();
            Cryp = new clsCriptografo();
        }

        #endregion

        #region Propiedades publicas
        public bool ExistenDatos
        {
            get { return bExistenDatos; }
        }

        public bool Ejecuto
        {
            get { return bEjecuto; }
        }

        public bool MostrarMsjSiLeerVacio
        {
            get { return bMostrarMsjLeerVacio; }
            set { bMostrarMsjLeerVacio = value; }
        }

        public bool EsPublicoGeneral
        {
            get { return bEsPublicoGeneral; }
            set { bEsPublicoGeneral = value; }
        }

        /// <summary>
        /// Devuelve el ultimo Query ejecutado por la instancia de la clase
        /// </summary>
        public string QueryEjecutado
        {
            get { return sConsultaExec; }
        }
        #endregion

        #region Facturacion Electronica
        #region Emisores 
        public DataSet CFDI_Emisores(string Funcion)
        {
            return CFDI_Emisores("", Funcion); 
        }

        public DataSet CFDI_Emisores(string IdEmisor, string Funcion)
        {
            myDataset = new DataSet();
            IdEmisor = Fg.PonCeros(IdEmisor, 8);
            string sMsjError = "Ocurrio un error al obtener los datos de Empresas";
            string sMsjNoEncontrado = "No se encontraron Empresas registradas, verifique.";
            string sFiltro = "";

            if (IdEmisor != "")
            {
                sFiltro = string.Format("Where IdEmisor = '{0}' ", IdEmisor); 
            }

            sQuery = sInicio + string.Format(
                "Select IdEmisor, NombreFiscal, NombreComercial, RFC, Telefonos, Fax, Email, DomExpedicion_DomFiscal, " +
                " Pais, IdEstado, Estado, IdMunicipio, Municipio, IdColonia, Colonia, Calle, NoExterior, NoInterior, CodigoPostal, Referencia, " +
                " EPais, EIdEstado, EEstado, EIdMunicipio, EMunicipio, IdIdColonia, EColonia, ECalle, ENoExterior, ENoInterior, ECodigoPostal, EReferencia, Status " +
                "From vw_CFDI_Emisores (NoLock) {0} ", sFiltro); 
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CFDI_Pagos__EmisorBancos(string RFC_Emisor, string RFC_Banco, string NumeroDeCuenta, bool MostrarCuentas, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de Bancos del Emisor ";
            string sMsjNoEncontrado = "Bancos no encontrados, verifique.";

            sQuery = sInicio + string.Format("Select Distinct RFC_Banco, NombreCorto, NombreRazonSocial, RFC_Banco, (RFC_Banco + ' -- ' + NombreRazonSocial) as NombreBanco " +
                " From vw_FACT_BancosCuentas_Emisor (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and RFC_Emisor = '{3}', And RFC_Banco = '{4}' " +
                " Group by RFC_Banco, NombreCorto, NombreRazonSocial " +
                " Order by NombreRazonSocial ", DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, RFC_Emisor, RFC_Banco);

            if (MostrarCuentas)
            {
                sQuery = sInicio + string.Format("Select RFC_Banco, NumeroDeCuenta, NombreCorto, NombreRazonSocial, " +
                    " RFC_Banco, (RFC_Banco + ' -- ' + NombreRazonSocial) as NombreBanco, (NombreCorto + ' -- ' + NumeroDeCuenta) as CuentaBanco, Status " +
                    " From vw_FACT_BancosCuentas_Emisor (NoLock) " +
                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and RFC_Emisor = '{3}' And RFC_Banco = '{4}' And NumeroDeCuenta = '{5}' " +
                    " Group by RFC_Banco, NumeroDeCuenta, NombreCorto, NombreRazonSocial, Status " +
                    " Order by NombreRazonSocial, NumeroDeCuenta ",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, RFC_Emisor, RFC_Banco, NumeroDeCuenta);
            }

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado, false);

            return myDataset;
        }

        public DataSet CFDI_Bancos(string RFC, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de Bancos del Emisor ";
            string sMsjNoEncontrado = "Bancos no encontrados, verifique.";
            string sFiltro = "Where Status = 'A' ";

            sQuery = sInicio + string.Format("Select * From CFDI__Bancos Where RFC = '{0}'", RFC);


            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado, false);

            return myDataset;
        }

        public DataSet CFDI_Pagos__ReceptorBancos(string RFC_Banco, string NumeroDeCuenta, bool MostrarCuentas, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de Bancos del Receptor ";
            string sMsjNoEncontrado = "Bancos no encontrados, verifique.";

            sQuery = sInicio + string.Format("Select RFC_Banco, NumeroDeCuenta, NombreCorto, NombreRazonSocial, " +
                " RFC_Banco, (RFC_Banco + ' -- ' + NombreRazonSocial) as NombreBanco, (NombreCorto + ' -- ' + NumeroDeCuenta) as CuentaBanco, Status " +
                " From vw_FACT_BancosCuentas_Receptor (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' And RFC_Banco = '{3}' And NumeroDeCuenta = '{4}' " +
                " Group by RFC_Banco, NumeroDeCuenta, NombreCorto, NombreRazonSocial, Status " +
                " Order by NombreRazonSocial, NumeroDeCuenta ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, RFC_Banco, NumeroDeCuenta);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado, false);

            return myDataset;
        }

        public DataSet CFDI_Pagos__EmisorBancos_Cuentas(bool MostrarCuentas, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de Bancos del Emisor ";
            string sMsjNoEncontrado = "Bancos no encontrados, verifique.";
            string sFiltro = "Where Status = 'A' ";

            sQuery = sInicio + string.Format("Select RFC_Banco, NombreCorto, NombreRazonSocial, (RFC_Banco + ' -- ' + NombreRazonSocial) as NombreBanco " +
                " From vw_BancosCuentas_Emisor (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Status = 'A' " +
                " Group by RFC_Banco, NombreCorto, NombreRazonSocial " +
                " Order by NombreRazonSocial ", DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            if (MostrarCuentas)
            {
                sQuery = sInicio + string.Format("Select RFC_Banco, NumeroDeCuenta, NombreCorto, NombreRazonSocial, " +
                    " (RFC_Banco + ' -- ' + NombreRazonSocial) as NombreBanco, (NombreCorto + ' -- ' + NumeroDeCuenta) as CuentaBanco " +
                    " From vw_BancosCuentas_Emisor (NoLock) " +
                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Status = 'A' " +
                    " Group by RFC_Banco, NumeroDeCuenta, NombreCorto, NombreRazonSocial " +
                    " Order by NombreRazonSocial, NumeroDeCuenta ", DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);
            }

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado, false);

            return myDataset;
        }

        public DataSet CFDI_Pagos__ReceptorBancos_Cuentas(bool MostrarCuentas, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de Bancos del Emisor ";
            string sMsjNoEncontrado = "Bancos no encontrados, verifique.";
            string sFiltro = "Where Status = 'A' ";

            sQuery = sInicio + string.Format("Select RFC_Banco, NombreCorto, NombreRazonSocial, (RFC_Banco + ' -- ' + NombreRazonSocial) as NombreBanco " +
                " From vw_BancosCuentas_Receptor (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Status = 'A' " +
                " Group by RFC_Banco, NombreCorto, NombreRazonSocial " +
                " Order by NombreRazonSocial ", DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            if (MostrarCuentas)
            {
                sQuery = sInicio + string.Format("Select RFC_Banco, NumeroDeCuenta, NombreCorto, NombreRazonSocial, " +
                    " (RFC_Banco + ' -- ' + NombreRazonSocial) as NombreBanco, (NombreCorto + ' -- ' + NumeroDeCuenta) as CuentaBanco " +
                    " From vw_BancosCuentas_Receptor (NoLock) " +
                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Status = 'A' " +
                    " Group by RFC_Banco, NumeroDeCuenta, NombreCorto, NombreRazonSocial " +
                    " Order by NombreRazonSocial, NumeroDeCuenta ", DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);
            }

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado, false);

            return myDataset;
        }

        public DataSet CFDI_Emisor_Configuracion(string IdEmisor, string Funcion)
        {
            myDataset = new DataSet();
            IdEmisor = Fg.PonCeros(IdEmisor, 4);
            string sMsjError = "Ocurrio un error al obtener los datos del Emisor";
            string sMsjNoEncontrado = "No se encontraron Emisores registrados, verifique."; 

            sQuery = sInicio + string.Format(
                "Exec spp_CFDI_Emisores_Configuracion @IdEmisor = '{0}' ", IdEmisor);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion Emisores

        #region Clientes 
        public DataSet CFDI_Clientes(string Funcion)
        {
            return CFDI_Clientes("", true, Funcion);
        }

        public DataSet CFDI_Clientes(string IdCliente, string Funcion)
        {
            return CFDI_Clientes(IdCliente, false, false, Funcion);
        }

        public DataSet CFDI_Clientes(string IdCliente, bool MostrarCancelados, string Funcion)
        {
            return CFDI_Clientes(IdCliente, MostrarCancelados, false, Funcion); 
        }

        public DataSet CFDI_Clientes(string IdCliente, bool MostrarCancelados, bool BusquedaPor_RFC, string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = "";
            // string sFiltro = "";
            string sMsjError = "Ocurrio un error al obtener los datos del Cliente";
            string sMsjNoEncontrado = "Clave de Cliente no encontrada, verifique.";
            string sFiltro = "";
            string sFiltroCancelados = "";

            if (IdCliente != "")
            {
                if (!BusquedaPor_RFC)
                {
                    sFiltro = string.Format("Where IdCliente = '{0}' ", Fg.PonCeros(IdCliente, 8));
                }
                else
                {
                    sFiltro = string.Format("Where RFC = '{0}' ", IdCliente.Trim());
                }

                sFiltroCancelados = !MostrarCancelados ? " And Status = 'A' " : "";
                sFiltro += sFiltroCancelados; 
            }

            sQuery = sInicio + string.Format(" Select IdCliente, IdCliente as IdReceptor, NombreFiscal as Nombre, NombreFiscal, NombreComercial, RFC, TipoDeCliente, " +
                " Telefonos, Fax, Email, Pais, IdEstado, Estado, IdMunicipio, Municipio, IdColonia, Colonia, " +
                " Calle, NoExterior, NoInterior, CodigoPostal, Referencia, Status " +
                " From vw_CFDI_Clientes_Informacion (NoLock) " +
                " {0} ", sFiltro);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CFDI_Clientes_Direcciones(string IdCliente, string Funcion)
        {
            myDataset = new DataSet();
            IdCliente = Fg.PonCeros(IdCliente.Trim(), 8);
            string sMsjError = "Ocurrio un error al obtener los datos del Cliente ";
            string sMsjNoEncontrado = "No se encontraron Cliente registrados, verifique.";

            sQuery = sInicio + string.Format(
                " Select IdDireccion, IdEstado, Pais, Estado, IdMunicipio, Municipio, IdColonia, Colonia, " +
                " 'Calle' = Calle, NumeroExterior, NumeroInterior, " +
                " CodigoPostal, 'Status' = StatusDomicilioDescripcion " +
                "From vw_CFDI_Clientes_Direcciones (NoLock) " +
                " Where IdCliente = '{0}' ", IdCliente);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado, false);

            return myDataset;
        }

        public DataSet CFDI_Clientes_Emails(string IdCliente, string Funcion)
        {
            myDataset = new DataSet();
            IdCliente = Fg.PonCeros(IdCliente.Trim(), 8);
            string sMsjError = "Ocurrio un error al obtener los datos del Cliente ";
            string sMsjNoEncontrado = "No se encontraron Cliente registrados, verifique.";

            sQuery = sInicio + string.Format(
                " Select IdEmail, IdTipoEMail, 'Tipo' = TipoMail, Email, 'Status' = StatusEmailDescripcion " +
                "From vw_CFDI_Clientes_EMails (NoLock) " +
                " Where IdCliente = '{0}' ", IdCliente);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado, false);

            return myDataset;
        }

        public DataSet CFDI_Clientes_Telefonos(string IdCliente, string Funcion)
        {
            myDataset = new DataSet();
            IdCliente = Fg.PonCeros(IdCliente.Trim(), 8);
            string sMsjError = "Ocurrio un error al obtener los datos del Cliente ";
            string sMsjNoEncontrado = "No se encontraron Cliente registrados, verifique.";

            sQuery = sInicio + string.Format(
                " Select IdTelefono, IdTipoTelefono, 'Tipo' = TipoTelefono, Telefono, 'Status' = StatusTelefonoDescripcion " +
                "From vw_CFDI_Clientes_Telefonos (NoLock) " +
                " Where IdCliente = '{0}' ", IdCliente);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado, false);

            return myDataset;
        }
        #endregion Clientes

        #region Facturacion en sitio 
        public DataSet CFDI_Datos_FacturacionEnSitio(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioVenta, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrio un error al obtener los datos del Cliente";
            string sMsjNoEncontrado = "Información de venta no encontrada, verifique.";
            string sFiltro = "";


            sQuery = sInicio + string.Format("Exec spp_INT_MA__FACT_GetInformacion_FacturarVenta " +
                "  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioVenta = '{3}' ", 
                IdEmpresa, IdEstado, IdFarmacia, FolioVenta);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion Facturacion en sitio

        #region Catalogos diversos
        public DataSet CFDI_Regimenes_Fiscales(string Funcion)
        {
            return CFDI_Regimenes_Fiscales("", Funcion); 
        }

        public DataSet CFDI_Regimenes_Fiscales(string IdReginen, string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = "";
            // string sFiltro = "";
            string sMsjError = "Ocurrio un error al obtener los datos del Cliente";
            string sMsjNoEncontrado = "Clave de Cliente no encontrada, verifique.";
            string sFiltro = "";

            if (IdReginen != "")
            {
                sFiltro = string.Format("Where IdReginen = '{0}' ", Fg.PonCeros(IdReginen, 8));
            }

            sQuery = sInicio + string.Format(" Select '' as Activo, IdRegimen, Descripcion, ( IdRegimen + ' - ' + Descripcion ) as Regimen " + 
                " From CFDI_RegimenFiscal (NoLock) " +
                " sFiltro {0} ", sFiltro);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        ////public DataSet CFDI_FormasDePago(string Funcion) 
        ////{
        ////    return CFDI_FormasDePago("", Funcion); 
        ////}

        ////public DataSet CFDI_FormasDePago(string IdFormaDePago, string Funcion)
        ////{
        ////    myDataset = new DataSet();
        ////    // string sExtra = "";
        ////    // string sFiltro = "";
        ////    string sMsjError = "Ocurrio un error la información de Forma de pago";
        ////    string sMsjNoEncontrado = "Forma de pago no encontrada, verifique.";
        ////    string sFiltro = "";

        ////    if (IdFormaDePago != "")
        ////    {
        ////        sFiltro = string.Format(" Where IdFormaDePago = '{0}' ", Fg.PonCeros(IdFormaDePago, 2));
        ////    }


        ////    sQuery = sInicio + string.Format(" Select IdFormaDePago, Descripcion, Status, " + 
        ////        " (IdFormaDePago + ' -- ' + Descripcion) as NombreFormaDePago " +
        ////        " From CFDI_FormaDePago (NoLock) {0}  ", sFiltro);

        ////    myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

        ////    return myDataset;
        ////}

        ////public DataSet CFDI_MetodosDePago(string Funcion)
        ////{
        ////    return CFDI_MetodosDePago("", Funcion);
        ////}

        ////public DataSet CFDI_MetodosDePago(string IdMetodoDePago, string Funcion)
        ////{
        ////    return CFDI_MetodosDePago(IdMetodoDePago, false, Funcion); 
        ////}

        ////public DataSet CFDI_MetodosDePago(string IdMetodoDePago, bool EsDocumento, string Funcion)
        ////{
        ////    myDataset = new DataSet();
        ////    // string sExtra = "";
        ////    // string sFiltro = "";
        ////    string sMsjError = "Ocurrio un error la información de Metodo de pago";
        ////    string sMsjNoEncontrado = "Metodo de pago no encontrado, verifique.";
        ////    string sFiltro = " Where 1 = 1 ";

        ////    if (IdMetodoDePago != "") 
        ////    {
        ////        sFiltro += string.Format(" And IdMetodoDePago = '{0}' ", Fg.PonCeros(IdMetodoDePago, 2));
        ////    }

        ////    if (!EsDocumento)
        ////    {
        ////        sFiltro += string.Format(" And EsDocumento = 0 ");
        ////    }


        ////    sQuery = sInicio + string.Format(" Select IdMetodoDePago, Descripcion, Status, " +
        ////        " (IdMetodoDePago + ' --' + Descripcion) as NombreMetodoDePago " +
        ////        " From CFDI_MetodoDePago (NoLock) {0}  " +
        ////        " Order by IdMetodoDePago ", sFiltro);

        ////    myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

        ////    return myDataset;
        ////}

        ////public DataSet CFDI_MetodosDePago_Listado(string IdMetodoDePago, string Funcion)
        ////{
        ////    myDataset = new DataSet();
        ////    // string sExtra = "";
        ////    // string sFiltro = "";
        ////    string sMsjError = "Ocurrió un error la información de Método de pago";
        ////    string sMsjNoEncontrado = "Método de pago no encontrado, verifique.";
        ////    string sFiltro = "";

        ////    if (IdMetodoDePago != "")
        ////    {
        ////        sFiltro = string.Format(" Where IdMetodoDePago = '{0}' ", Fg.PonCeros(IdMetodoDePago, 2));
        ////    }


        ////    sQuery = sInicio + string.Format(" Select IdMetodoPago as IdMetodoPago, Descripcion, Status, " +
        ////        " (IdMetodoPago + ' --' + Descripcion) as NombreMetodoDePago " +
        ////        " From CFDI_MetodoDePago (NoLock) {0}  ", sFiltro);

        ////    sQuery = sInicio + string.Format(" Select IdMetodoDePago, Descripcion, " +
        ////        " (case when RequiereReferencia = 1 then 'SI' else 'NO' end) as RequiereReferencia, " +
        ////        " cast(0 as numeric(14,2)) as Importe, cast('' as varchar(10)) as Referencia \n " +
        ////        " From CFDI_MetodoDePago (NoLock) {0}  " +
        ////        " Order by IdMetodoDePago ", sFiltro);

        ////    myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

        ////    return myDataset;
        ////}

        public DataSet CFDI_FormasDePago(string VersionCFDI, string Funcion)
        {
            return CFDI_FormasDePago("", VersionCFDI, Funcion);
        }

        public DataSet CFDI_FormasDePago(string IdFormaDePago, string VersionCFDI, string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = "";
            // string sFiltro = "";
            string sMsjError = "Ocurrió un error la información de Forma de pago";
            string sMsjNoEncontrado = "Forma de pago no encontrada, verifique.";
            string sFiltro = "Where Status = 'A' ";

            if (IdFormaDePago != "")
            {
                sFiltro = string.Format(" And IdFormaDePago = '{0}' ", Fg.PonCeros(IdFormaDePago, 2));
            }

            //if (VersionCFDI == "3.2")
            //{
            //    sQuery = sInicio + string.Format(" Select IdFormaDePago, Descripcion, Status, " +
            //        " (IdFormaDePago + ' --' + Descripcion) as NombreFormaDePago " +
            //        " From FACT_CFD_FormasDePago (NoLock) {0}  ", sFiltro);
            //}


            //if (VersionCFDI == "3.3")
            {
                sQuery = sInicio + string.Format(" Select IdFormaDePago as IdFormaDePago, Descripcion, " +
                    " (case when RequiereReferencia = 1 then 'SI' else 'NO' end) as RequiereReferencia, " +
                    " cast(0 as numeric(14,2)) as Importe, cast('' as varchar(10)) as Referencia \n " +
                    " From CFDI_FormasDePago (NoLock) {0}  " +
                    " Order by IdFormaDePago ", sFiltro);
            }

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado, false);

            return myDataset;
        }

        public DataSet CFDI_ComplementoPagos__FormasDePago(string IdFormaDePago_Excluida, string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = "";
            // string sFiltro = "";
            string sMsjError = "Ocurrió un error la información de Forma de pago";
            string sMsjNoEncontrado = "Formas de pago no encontradas, verifique.";
            string sFiltro = "Where Status = 'A' ";

            if (IdFormaDePago_Excluida != "")
            {
                sFiltro += string.Format(" and IdFormaDePago <> '{0}' ", Fg.PonCeros(IdFormaDePago_Excluida, 2));
            }

            sQuery = sInicio + string.Format(" Select IdFormaDePago as IdFormaDePago, Descripcion, (IdFormaDePago + ' -- ' + Descripcion) as Descripcion_FormaDePago, " +
                " (case when RequiereReferencia = 1 then 'SI' else 'NO' end) as RequiereReferencia, " +
                " cast(0 as numeric(14,2)) as Importe, cast('' as varchar(10)) as Referencia \n " +
                " From CFDI_FormasDePago (NoLock) {0}  " +
                " Order by IdFormaDePago ", sFiltro);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado, false);

            return myDataset;
        }

        public DataSet CFDI_MetodosDePago(string VersionCFDI, string Funcion)
        {
            return CFDI_MetodosDePago("", VersionCFDI, Funcion);
        }

        public DataSet CFDI_MetodosDePago(string IdMetodoDePago, string VersionCFDI, string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = "";
            // string sFiltro = "";
            string sMsjError = "Ocurrió un error la información de Método de pago";
            string sMsjNoEncontrado = "Método de pago no encontrado, verifique.";
            string sFiltro = "";

            if (IdMetodoDePago != "")
            {
                sFiltro = string.Format(" Where IdMetodoDePago = '{0}' ", Fg.PonCeros(IdMetodoDePago, 2));
            }

            //if (VersionCFDI == "3.2")
            //{
            //    sQuery = sInicio + string.Format(" Select IdMetodoPago as IdMetodoPago, Descripcion, Status, " +
            //        " (IdMetodoPago + ' --' + Descripcion) as NombreMetodoDePago " +
            //        " From FACT_CFD_MetodosPago (NoLock) {0}  ", sFiltro);
            //}

            //if (VersionCFDI == "3.3")
            //{
            //    sQuery = sInicio + string.Format(" Select IdMetodoPago as IdMetodoPago, Descripcion, Status, " +
            //        " (IdMetodoPago + ' --' + Descripcion) as NombreMetodoDePago " +
            //        " From FACT_CFD_MetodosPago (NoLock) {0}  ", sFiltro);
            //}


            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CFDI_MetodosDePago_Listado(string IdMetodoDePago, string VersionCFDI, string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = "";
            // string sFiltro = "";
            string sMsjError = "Ocurrió un error la información de Método de pago";
            string sMsjNoEncontrado = "Método de pago no encontrado, verifique.";
            string sFiltro = "";
            string sNombreCampo = " IdMetodoDePago ";

            if (IdMetodoDePago != "")
            {
                sFiltro = string.Format(" Where IdMetodoPago = '{0}' ", Fg.PonCeros(IdMetodoDePago, 2));
            }

            //if (VersionCFDI == "3.2")
            //{
            //    sQuery = sInicio + string.Format(" Select IdMetodoPago as IdFormaDePago, Descripcion, " +
            //        " (case when RequiereReferencia = 1 then 'SI' else 'NO' end) as RequiereReferencia, " +
            //        " cast(0 as numeric(14,2)) as Importe, cast('' as varchar(10)) as Referencia \n " +
            //        " From FACT_CFD_MetodosPago (NoLock) {0}  " +
            //        " Order by IdMetodoPago ", sFiltro);
            //}

            //if (VersionCFDI == "3.3")
            {
                //if (sFiltro == "")
                //{
                //    sFiltro += string.Format(" Where Version = '{0}' ", VersionCFDI);
                //}
                //else
                //{
                //    sFiltro += string.Format(" and Version = '{0}' ", VersionCFDI);
                //}

                sFiltro = "Where Status = 'A' ";
                sFiltro += string.Format(" and Version = '{0}' ", VersionCFDI);


                sQuery = sInicio + string.Format(" Select IdMetodoDePago as IdFormaDePago, Descripcion, (IdMetodoDePago + ' -- ' + Descripcion) as DescripcionMetodoDePago " +
                    " From CFDI_MetodosPago (NoLock) {0}  " +
                    " Order by IdMetodoDePago ", sFiltro);
            }


            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado, false);

            return myDataset;
        }

        public DataSet CFDI_Series_y_Folios(string IdEmpresa, string IdSucursal, string IdTipoDocumento, string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = "";
            // string sFiltro = "";
            string sMsjError = "Ocurrio un error la información de Metodo de pago";
            string sMsjNoEncontrado = "Metodo de pago no encontrado, verifique.";
            string sFiltro = "";

            if (IdTipoDocumento != "")
            {
                sFiltro = string.Format(" and IdTipoDocumento = '{0}' ", IdTipoDocumento); 
            }

            sQuery = sInicio + string.Format("Select IdEmpresa, IdSucursal, Bloqueado, Asignado, " +
                " AñoAprobacion, NumAprobacion, IdTipoDocumento, TipoDeDocumento, Serie, NombreDocumento, FolioInicial, FolioFinal, FolioUtilizado, IdentificadorSerie, Status " +
                " From vw_CFDI_Sucursales_Series (NoLock) " +
                " Where IdEmpresa = '{0}' and IdSucursal = '{1}' {2} and Asignado = 'SI' ",
                IdEmpresa, IdSucursal, sFiltro);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CFDI_Series_y_Folios(string IdEmpresa, string IdEstado, string IdFarmacia, string IdTipoDocumento, string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = "";
            // string sFiltro = "";
            string sMsjError = "Ocurrió un error al obtener la información de Serie de facturación";
            string sMsjNoEncontrado = "Serie de facturación no encontradas, verifique.";
            string sFiltro = "";
            string sFiltroUnidad = ""; 

            if (IdTipoDocumento != "")
            {
                sFiltro = string.Format(" and IdTipoDocumento = '{0}' ", IdTipoDocumento);
            }

            sFiltroUnidad = string.Format(" IdEmpresa = '{0}' ", IdEmpresa);
            if (IdEstado != "")
            {
                sFiltroUnidad += string.Format(" and IdEstado = '{0}' ", IdEstado);
            }

            if (IdFarmacia != "")
            {
                sFiltroUnidad += string.Format(" and IdFarmacia = '{0}' ", IdFarmacia);
            }

            sQuery = sInicio + string.Format("Select IdEmpresa, IdEstado, IdFarmacia, Bloqueado, Asignado, " +
                " AñoAprobacion, NumAprobacion, IdTipoDocumento, TipoDeDocumento, Serie, NombreDocumento, FolioInicial, FolioFinal, FolioUtilizado, IdentificadorSerie, Status " +
                " From vw_FACT_CFD_Sucursales_Series (NoLock) " +
                " Where {0} {1} and Asignado = 'SI' ",
                sFiltroUnidad, sFiltro);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CFDI_TipoDeDocumentos(string IdTipoDocumento, string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = "";
            // string sFiltro = "";
            string sMsjError = "Ocurrio un error la información de Metodo de pago";
            string sMsjNoEncontrado = "Metodo de pago no encontrado, verifique.";
            string sFiltro = "";

            if (IdTipoDocumento != "")
            {
                sFiltro = string.Format(" Where IdTipoDocumento = '{0}' ", IdTipoDocumento); 
            }

            sQuery = sInicio + string.Format("Select IdTipoDocumento, NombreDocumento as Documento, Alias, Status " +
                " From CFDI_TiposDeDocumentos (NoLock) " +
                " {0} Order by IdTipoDocumento ", sFiltro); 

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CFDI_UnidadesDeMedida(string Funcion)
        {
            return CFDI_UnidadesDeMedida("", Funcion); 
        }

        public DataSet CFDI_UnidadesDeMedida(string IdUnidad, string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = "";
            // string sFiltro = "";
            string sMsjError = "Ocurrio un error la información de Metodo de pago";
            string sMsjNoEncontrado = "Metodo de pago no encontrado, verifique.";
            string sFiltro = "";

            if (IdUnidad != "")
            {
                sFiltro = string.Format(" Where IdUnidad = '{0}' ", IdUnidad);
            }

            sQuery = sInicio + string.Format("Select IdUnidad, Descripcion, Status, (IdUnidad + ' - ' + Descripcion ) as UnidadDeMedida  " +
                " From CFDI_UnidadesDeMedida (NoLock) " +
                " {0} Order by IdUnidad ", sFiltro);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CFDI_UsosDeCFDI(string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = "";
            // string sFiltro = "";
            string sMsjError = "Ocurrió un error al obtener la información de Unidades de Medida";
            string sMsjNoEncontrado = "Unidad de Medida no encontrada, verifique.";
            string sFiltro = " Where Status = 'A' ";

            sQuery = sInicio + string.Format("Select Clave, Descripcion, (Clave + ' - ' + Descripcion) as UsoDeCFDI, Status " +
                " From CFDI_UsosDeCFDI (NoLock) " +
                " {0} " +
                " Order by Clave, Descripcion ", sFiltro);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CFDI_Monedas(string Funcion)
        {
            return CFDI_Monedas("", Funcion);
        }

        public DataSet CFDI_Monedas(string Moneda, string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = "";
            // string sFiltro = "";
            string sMsjError = "Ocurrió un error al obtener la información de Monedas ";
            string sMsjNoEncontrado = "Monedas no encontradas, verifique.";
            string sFiltro = "Where Status = 'A' ";

            if (Moneda != "")
            {
                sFiltro += string.Format(" and Clave = '{0}' ", Moneda);
            }

            sQuery = sInicio + string.Format("Select Clave, Descripcion, (Clave + ' - ' + Descripcion) as Moneda, Status " +
                " From CFDI_TiposDeMoneda (NoLock) " +
                " {0} " +
                " Order by Clave, Descripcion ", sFiltro);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }               
        #endregion Catalogos diversos

        #region Tipos de Correo y Telefono
        public DataSet Correos(string IdTipoCorreo, string Funcion)
        {
            myDataset = new DataSet();
            IdTipoCorreo = Fg.PonCeros(IdTipoCorreo.Trim(), 4);
            string sMsjError = "Ocurrio un error al obtener los datos de Tipos de Correo ";
            string sMsjNoEncontrado = "No se encontraron Tipos de Correo registrados, verifique.";

            sQuery = sInicio + string.Format(
                " Select * " +
                "From CFDI_TiposEmail (NoLock) Where IdTipoEMail = '{0}' ", IdTipoCorreo);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Telefonos(string IdTipoCorreo, string Funcion)
        {
            myDataset = new DataSet();
            IdTipoCorreo = Fg.PonCeros(IdTipoCorreo.Trim(), 4);
            string sMsjError = "Ocurrio un error al obtener los datos de Tipos de Telefonos ";
            string sMsjNoEncontrado = "No se encontraron Tipos de Telefonos registrados, verifique.";

            sQuery = sInicio + string.Format(
                " Select * " +
                "From CFDI_TiposTelefonos (NoLock) Where IdTipoTelefono = '{0}' ", IdTipoCorreo);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet TiposDeTelefonos(string Funcion)
        {
            return TiposDeTelefonos("", "Funcion");
        }

        public DataSet TiposDeTelefonos(string IdTipoTelefono, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrio un error al obtener los datos del Tipo de Telefono ";
            string sMsjNoEncontrado = "Clave de Tipo de Telefono no encontrada, verifique.";
            string sWhere = "";

            if (IdTipoTelefono != "")
            {
                sWhere = string.Format(" And IdTipoTelefono = '{0}'", Fg.PonCeros(IdTipoTelefono, 4));
            }

            sQuery = sInicio + string.Format(" Select * From CFDI_TiposTelefonos (NoLock) Where 1 = 1 {0} ", sWhere);

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet TiposDeMail(string Funcion)
        {
            return TiposDeMail("", "Funcion");
        }

        public DataSet TiposDeMail(string IdTipoMail, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrio un error al obtener los datos del Tipo de E-Mail ";
            string sMsjNoEncontrado = "Clave de Tipo de E-Mail no encontrada, verifique.";
            string sWhere = "";

            if (IdTipoMail != "")
            {
                sWhere = string.Format(" And IdTipoEMail = '{0}'", Fg.PonCeros(IdTipoMail, 4));
            }

            sQuery = sInicio + string.Format(" Select * From CFDI_TiposEmail (NoLock) Where 1 = 1 {0} ", sWhere);

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ProveedoresEMail(string IdProveedorEMail, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrio un error al obtener los datos de Proveedor de EMail ";
            string sMsjNoEncontrado = "Clave de Proveedor de EMail no encontrada, verifique.";
            string sWhere = "";

            if (IdProveedorEMail != "")
            {
                sWhere = string.Format(" And IdProveedorEMail = '{0}'", Fg.PonCeros(IdProveedorEMail, 4));
            }

            sQuery = sInicio + string.Format(" Select * " +
                " From CFDI_ProveedoresEMail (NoLock) Where 1 = 1 {0} ", sWhere);

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado, false);

            return myDataset;
        }

        public DataSet UnidadesDeMedida(string IdUnidadDeMedida, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrio un error al obtener los datos de Unidad de Medida ";
            string sMsjNoEncontrado = "Clave de Unidad de Medida no encontrada, verifique.";
            string sWhere = "";

            if (IdUnidadDeMedida != "")
            {
                sWhere = string.Format(" And IdUnidadDeMedida = '{0}'", Fg.PonCeros(IdUnidadDeMedida, 4));
            }

            sQuery = sInicio + string.Format(" Select * From CFDI_UnidadesDeMedida (NoLock) Where 1 = 1 {0} ", sWhere);

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion Tipos de Correo y Telefono

        #region Geograficos
        public DataSet CFDI_Estados(string IdEstado, string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = "";
            // string sFiltro = "";
            string sMsjError = "Ocurrio un error la información de Estados";
            string sMsjNoEncontrado = "Estado no encontrado, verifique.";
            string sFiltro = "";

            if (IdEstado != "")
            {
                sFiltro = string.Format(" Where IdEstado = '{0}' ", Fg.PonCeros(IdEstado, 2));
            }

            sQuery = sInicio + string.Format("Select IdEstado, Nombre as Descripcion, Status, (IdEstado + ' - ' + Nombre) as Estado  " +
                " From CatEstados (NoLock) " +
                " {0} Order by IdEstado ", sFiltro);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CFDI_Municipios(string IdEstado, string IdMunicipio, string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = "";
            // string sFiltro = "";
            string sMsjError = "Ocurrio un error la información de Municipios";
            string sMsjNoEncontrado = "Municipio no encontrado, verifique.";
            string sFiltro = "";

            if (IdEstado != "")
            {
                sFiltro += string.Format(" and IdEstado = '{0}' ", Fg.PonCeros(IdEstado, 2));
            }

            if (IdMunicipio != "")
            {
                sFiltro += string.Format(" and IdMunicipio = '{0}' ", Fg.PonCeros(IdMunicipio, 4));
            }

            sQuery = sInicio + string.Format("Select IdEstado, IdMunicipio, Descripcion, Status, (IdMunicipio + ' - ' + Descripcion ) as Municipio  " +
                " From CatMunicipios (NoLock) " +
                " Where 1 = 1 {0} " + 
                " Order by IdEstado, IdMunicipio ", sFiltro);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CFDI_Colonias(string IdEstado, string IdMunicipio, string IdColonia, string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = "";
            // string sFiltro = "";
            string sMsjError = "Ocurrio un error la información de Colonias";
            string sMsjNoEncontrado = "Colona no encontrada, verifique.";
            string sFiltro = "";

            if (IdEstado != "")
            {
                sFiltro += string.Format(" and IdEstado = '{0}' ", Fg.PonCeros(IdEstado, 2));
            }

            if (IdMunicipio != "")
            {
                sFiltro += string.Format(" and IdMunicipio = '{0}' ", Fg.PonCeros(IdMunicipio, 4));
            }

            if (IdColonia != "")
            {
                sFiltro += string.Format(" and IdColonia = '{0}' ", Fg.PonCeros(IdColonia, 4));
            }

            sQuery = sInicio + string.Format("Select IdEstado, IdMunicipio, IdColonia, Descripcion, Status, (IdColonia + ' - ' + Descripcion ) as Colonia, CodigoPostal  " +
                " From CatColonias (NoLock) " +
                " Where 1 = 1 {0} " +
                " Order by IdEstado, IdMunicipio, IdColonia ", sFiltro);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        } 
        #endregion Geograficos 
        #endregion Facturacion Electronica 

        #region Funciones y procedimientos privados
        //////private DataSet EjecutarQuery(string prtQuery, string NombreTabla)
        //////{
        //////    clsLeer leer = new clsLeer();
        //////    leer.DataSetClase = EjecutarQuery("Funcion local", prtQuery, "Ocurrió un error al obtener la información", "Información no encontrada");

        //////    if (!leer.SeEncontraronErrores())
        //////    {
        //////        leer.RenombrarTabla(1, NombreTabla);
        //////    }

        //////    return leer.DataSetClase;
        //////}

        private DataSet EjecutarQuery(string Funcion, string Query, string MensajeError, string MensajeNoEncontrado)
        {
            return EjecutarQuery(Funcion, Query, "LeerGenerico", MensajeError, MensajeNoEncontrado, bMostrarMsjLeerVacio);
        }

        private DataSet EjecutarQuery(string Funcion, string Query, string MensajeError, string MensajeNoEncontrado, bool MostrarMensajeVacio)
        {
            return EjecutarQuery(Funcion, Query, "LeerGenerico", MensajeError, MensajeNoEncontrado, MostrarMensajeVacio);
        }

        private DataSet EjecutarQuery(string Funcion, string Query, string NombreTabla, string MensajeError, string MensajeNoEncontrado, bool MostrarMensajeVacio)
        {
            clsLeer Leer = new clsLeer(ref ConexionSql);
            DataSet dtsResultados = new DataSet();

            bEjecuto = false;
            Leer.Conexion.SetConnectionString();
            sConsultaExec = Query;
            if (!Leer.Exec(NombreTabla, Query))
            {
                General.Error.GrabarError(Leer.Error, ConexionSql.DatosConexion, this.sNameDll, this.sVersion, this.sPantalla, Funcion, Leer.QueryEjecutado);
                General.msjError(MensajeError);
            }
            else
            {
                bEjecuto = true;
                if (!Leer.Leer())
                {
                    if (MostrarMensajeVacio)
                    {
                        General.msjUser(MensajeNoEncontrado);
                    }
                }

                ////else
                {
                    dtsResultados = Leer.DataSetClase;
                }

            }

            return dtsResultados;
        }

        private bool ExistenDatosEnDataset(DataSet dtsRevisar)
        {
            bool bRegresa = false;

            if (dtsRevisar.Tables.Count > 0)
            {
                if (dtsRevisar.Tables[0].Rows.Count > 0)
                {
                    bRegresa = true;
                }
            }

            return bRegresa;
        }
        #endregion

    }
}
