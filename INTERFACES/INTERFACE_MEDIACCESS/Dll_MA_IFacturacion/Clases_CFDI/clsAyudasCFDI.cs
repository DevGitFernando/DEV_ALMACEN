using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

using DllFarmaciaSoft;

namespace Dll_MA_IFacturacion
{
    public partial class clsAyudas_CFDI
    {
        #region Declaración de variables
        // private wsFarmaciaSoftGn.wsConexion cnnWebServ; // = new wsConexion.wsConexionDB();
        private clsErrorManager error = new clsErrorManager();
        private clsLogError errorLog = new clsLogError();
        private DialogResult myResult = new DialogResult();

        private DataSet dtsError = new DataSet();
        private DataSet dtsClase = new DataSet();
        // private object objEnviar = null;
        // private object objRecibir = null;
        private string strCnnString = "";
        private bool bUsarCnnRedLocal = true, bExistenDatos = false, bEjecuto = false; //bError = false 
        private string sSql = "", strResultado = ""; // , sOrderBy = ""; 
        private string strMsjNoDatos = "No existe información para mostrar.";
        string sInicio = "Set DateFormat YMD ";

        private clsConsultas_CFDI query;
        private basGenerales Fg = new basGenerales();
        private Cls_Acceso_a_Datos_Sql Datos = new Cls_Acceso_a_Datos_Sql();
        private FrmAyuda Frm_Ayuda;

        private clsDatosConexion DatosConexion;
        private clsConexionSQL ConexionSql;
        private string Name = "DllFarmaciaSoft.clsAyudas";
        string sNameDll = "";
        string sPantalla = "";
        string sVersion = "";
        bool bMostrarMsjLeerVacio = false;
        bool bEsPublicoGeneral = false;
        int iColumnaInicial = 1;
        bool bEsClienteIMach = false;

        // Consulta ejecutada
        protected string sConsultaExec = "";

        #endregion

        #region Constructores de Clase y Destructor
        public clsAyudas_CFDI()
        {
            bUsarCnnRedLocal = General.ServidorEnRedLocal;
            strCnnString = Name;
            strCnnString = General.CadenaDeConexion;
            //Name = Name;
            // cnnWebServ = new wsConexion.wsConexionDB();
            // cnnWebServ.Url = General.Url;
        }

        public clsAyudas_CFDI(string prtCnnString, string cnnWebUrl, bool bUsarRedLocal)
        {
            bUsarCnnRedLocal = bUsarRedLocal;
            strCnnString = prtCnnString;
            // cnnWebServ.Url = cnnWebUrl;
        }

        public clsAyudas_CFDI(clsDatosConexion Conexion, clsDatosApp DatosApp, string Pantalla)
        {
            this.sNameDll = DatosApp.Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = DatosApp.Version;

            DatosConexion = Conexion;
            ConexionSql = new clsConexionSQL(Conexion);
            ConexionSql.SetConnectionString();
            query = new clsConsultas_CFDI(DatosConexion, DatosApp.Modulo, Pantalla, DatosApp.Version);
        }

        public clsAyudas_CFDI(clsDatosConexion Conexion, clsDatosApp DatosApp, string Pantalla, bool MostrarMsjLeerVacio)
        {
            this.sNameDll = DatosApp.Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = DatosApp.Version;
            this.bMostrarMsjLeerVacio = MostrarMsjLeerVacio;

            DatosConexion = Conexion;
            ConexionSql = new clsConexionSQL(Conexion);
            ConexionSql.SetConnectionString();
            query = new clsConsultas_CFDI(DatosConexion, DatosApp.Modulo, Pantalla, DatosApp.Version);
        }

        public clsAyudas_CFDI(clsDatosConexion Conexion, string Modulo, string Pantalla, string Version)
        {
            this.sNameDll = Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = Version;
            this.bMostrarMsjLeerVacio = false;

            DatosConexion = Conexion;
            ConexionSql = new clsConexionSQL(Conexion);
            ConexionSql.SetConnectionString();
            query = new clsConsultas_CFDI(DatosConexion, Modulo, Pantalla, Version);
        }

        public clsAyudas_CFDI(clsDatosConexion Conexion, string Modulo, string Pantalla, string Version, bool MostrarMsjLeerVacio)
        {
            this.sNameDll = Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = Version;
            this.bMostrarMsjLeerVacio = MostrarMsjLeerVacio;

            DatosConexion = Conexion;
            ConexionSql = new clsConexionSQL(Conexion);
            ConexionSql.SetConnectionString();
            query = new clsConsultas_CFDI(DatosConexion, Modulo, Pantalla, Version, MostrarMsjLeerVacio);
        }
        #endregion Constructores de Clase y Destructor     

        #region Funciones y procedimientos publicos 
        #region Facturacion Electronica 
        #region Propiedades
        public bool ExistenDatos
        {
            get { return bExistenDatos; }
        }

        public bool MostrarMsjSiLeerVacio
        {
            get { return bMostrarMsjLeerVacio; }
            set { bMostrarMsjLeerVacio = value; }
        }

        public bool EsPublicoGeneral
        {
            get { return bEsPublicoGeneral; }
            set
            {
                bEsPublicoGeneral = value;
                query.EsPublicoGeneral = value;
            }
        }
        /// <summary>
        /// Devuelve el ultimo Query ejecutado por la instancia de la clase
        /// </summary>
        public string QueryEjecutado
        {
            get { return sConsultaExec; }
        }
        #endregion Propiedades 

        public DataSet CFDI_Clientes(string Funcion)
        {
            return CFDI_Clientes(true, Funcion);
        }

        public DataSet CFDI_Clientes(bool MostrarCancelados, string Funcion)
        {
            string strMsj = "Catálogo de Clientes";
            string sFiltro = !MostrarCancelados ? "Where Status = 'A' ": ""; 
            sConsultaExec = ""; 


            sSql = 
                string.Format(
                " Select 'Activo' = (case when Status = 'A' then 'SI' else 'NO' end), " + 
                " 'Nombre del cliente'= NombreFiscal, 'Nombre comercial' = NombreComercial, RFC, " + 
                " 'Ubicación' = (Pais + ', ' + Estado), 'Clave de cliente' = IdCliente " +
                " From vw_CFDI_Clientes_Informacion (noLock) " + 
                " {0} ", sFiltro); 

            dtsClase = new DataSet();
            sConsultaExec = sSql; 

            if (1 == 1)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, sSql, "Error en la ayuda 'Clientes_CFDI'", false, 2);
                dtsClase = new DataSet();

                if (strResultado != "")
                {
                    dtsClase = query.CFDI_Clientes(strResultado, MostrarCancelados, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            return dtsClase;
        }

        public DataSet CFDI_Bancos(string Funcion)
        {
            string strMsj = "Catálogo de Bancos";
            sConsultaExec = "";

            sSql = sInicio + string.Format("Select *,RFC From CFDI__Bancos ");

            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Socios Comerciales'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase, false, 2);

                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.CFDI_Bancos(strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet CFDI_Pagos__EmisorBancos(string RFC_Emisor, string RFC_Banco, string Funcion)
        {
            string strMsj = "Catálogo de Socios Comerciales";
            sConsultaExec = "";

            string sRFC = "", sNumero = "";

            sSql = sInicio + string.Format(
                    "Select RFC_Banco, NumeroDeCuenta, NombreCorto, NombreRazonSocial, " +
                    "   (RFC_Banco + ' -- ' + NombreRazonSocial) as NombreBanco, (NombreCorto + ' -- ' + NumeroDeCuenta) as CuentaBanco, Status, " +
                    "   RFC_Banco + '-$-' + NumeroDeCuenta" +
                    " From vw_FACT_BancosCuentas_Emisor (NoLock) " +
                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and RFC_Emisor = '{3}' And RFC_Banco = '{4}' " +
                    " Group by RFC_Banco, NumeroDeCuenta, NombreCorto, NombreRazonSocial, Status " +
                    " Order by NombreRazonSocial, NumeroDeCuenta ",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, RFC_Emisor, RFC_Banco);

            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Socios Comerciales'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase, false, 2);

                if (strResultado.Length > 0)
                {
                    sRFC = strResultado.Substring(0, strResultado.IndexOf("-$-"));
                    sNumero = strResultado.Substring(strResultado.IndexOf("-$-") + 3);
                }

                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.CFDI_Pagos__EmisorBancos(RFC_Emisor, sRFC, sNumero, true, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet CFDI_Pagos__ReceptorBancos(string RFC_Banco, string Funcion)
        {
            string strMsj = "Catálogo de Socios Comerciales";
            sConsultaExec = "";

            string sRFC = "", sNumero = "";

            sSql = sInicio + string.Format(
                    "Select RFC_Banco, NumeroDeCuenta, NombreCorto, NombreRazonSocial, " +
                    "   (RFC_Banco + ' -- ' + NombreRazonSocial) as NombreBanco, (NombreCorto + ' -- ' + NumeroDeCuenta) as CuentaBanco, Status, " +
                    "   RFC_Banco + '-$-' + NumeroDeCuenta" +
                    " From vw_FACT_BancosCuentas_Receptor (NoLock) " +
                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' And RFC_Banco = '{3}'" +
                    " Group by RFC_Banco, NumeroDeCuenta, NombreCorto, NombreRazonSocial, Status " +
                    " Order by NombreRazonSocial, NumeroDeCuenta ",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, RFC_Banco);

            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Socios Comerciales'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase, false, 2);

                sRFC = strResultado.Substring(0, strResultado.IndexOf("-$-"));
                sNumero = strResultado.Substring(strResultado.IndexOf("-$-") + 3);

                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.CFDI_Pagos__ReceptorBancos(sRFC, sNumero, true, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        ////public DataSet CFDI_Conceptos(string Funcion)
        ////{
        ////    string strMsj = "Catálogo de Conceptos";

        ////    sSql = "Select Descripcion as Concepto, 'Clave de Concepto' = IdProducto " +
        ////        " From vw_Productos_CodigosEAN (NoLock) Order by Descripcion ";

        ////    sSql = "Select 'Codigo EAN' = CodigoEAN, Descripcion as Producto, " +
        ////        " Familia, 'Sub-Familia' = SubFamilia, 'Miembro de Sub-Familia' = MiembroSubFamilia, " +
        ////        " Tamaño, Marca, 'Número de calzado' = NumeracionCalzado, Estante, Departamento,  "
        ////        + " 'Clave de Producto' = IdProducto, CodigoEAN " +
        ////        " From vw_Productos_CodigosEAN (NoLock) Order by Descripcion "; 
        ////    dtsClase = new DataSet();
        ////    ////dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Conceptos'", "");

        ////    sConsultaExec = sSql; 
        ////    if (1 == 1)
        ////    {
        ////        bExistenDatos = false;
        ////        strResultado = MostrarForma(strMsj, dtsClase, 2);
        ////        dtsClase = new DataSet();
        ////        if (strResultado != "")
        ////        {
        ////            dtsClase = query.CFDI_Productos(strResultado, Funcion);
        ////            ValidaDatos(ref dtsClase);
        ////        }
        ////    }
        ////    else
        ////    {
        ////        MsjNoDatos(ref dtsClase, strMsj);
        ////    }

        ////    return dtsClase;
        ////}

        public DataSet CFDI_Conceptos_Especiales(string IdEstado, string Funcion)
        {
            string strMsj = "Catálogo de Conceptos especiales";

            sSql = "Select Descripcion as Concepto, 'Clave de Concepto' = IdConcepto " + 
                " From CFDI_ConceptosEspeciales (NoLock) " + 
                " Order by Descripcion ";
            dtsClase = new DataSet();
            ////dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Conceptos especiales'", "");
            sConsultaExec = sSql; 

            if (1==1)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    //dtsClase = query.CFDI_Conceptos_Especiales(IdEstado, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        ////public DataSet CFDI_Conceptos_Licitacion(string IdSucursal, string IdCliente, string NumeroDeContrato, string Funcion)
        ////{
        ////    string strMsj = "Catálogo de Conceptos";

        ////    sSql = string.Format("Select 'Codigo EAN' = CodigoEAN, 'Descripción de producto ' = Producto, " +
        ////        " 'Descripción de licitacion' = DescripcionLicitacion, 'Clave de Producto' = IdProducto, CodigoEAN " +
        ////        " From vw_CFDI_Productos_Licitacion (NoLock) " + 
        ////        " Where IdSucursal = '{0}' and IdCliente = '{1}' and NumeroDeContrato = '{2}' " + 
        ////        " Order by Producto ", IdSucursal, IdCliente, NumeroDeContrato);
        ////    dtsClase = new DataSet();
        ////    ////dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Conceptos'", "");

        ////    sConsultaExec = sSql;
        ////    if (1 == 1)
        ////    {
        ////        bExistenDatos = false;
        ////        strResultado = MostrarForma(strMsj, dtsClase, 2);
        ////        dtsClase = new DataSet();
        ////        if (strResultado != "")
        ////        {
        ////            dtsClase = query.CFDI_Productos_Licitacion(IdSucursal, IdCliente, NumeroDeContrato, strResultado, Funcion);
        ////            ValidaDatos(ref dtsClase);
        ////        }
        ////    }
        ////    else
        ////    {
        ////        MsjNoDatos(ref dtsClase, strMsj);
        ////    }

        ////    return dtsClase;
        ////}        
        #endregion Facturacion Electronica

        #region Tipos de Correo y Telefono
        public DataSet TiposDeTelefono(string Funcion)
        {
            string strMsj = "Catálogo de Tipos de Teléfono";
            sConsultaExec = "";

            sSql = "Select Descripcion as 'Tipo De Telefono', 'Clave de Tipo de Telefono' = IdTipoTelefono From CFDI_TiposTelefonos (NoLock) Order by Descripcion ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'TiposDeTelefono'", "");
            sConsultaExec = sSql;

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.TiposDeTelefonos(strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet TiposDeMail(string Funcion)
        {
            string strMsj = "Catálogo de Tipos de E-Mail";
            sConsultaExec = "";

            sSql = "Select Descripcion as 'Tipo De E-Mail', 'Clave de Tipo de E-Mail' = IdTipoEMail From CFDI_TiposEmail (NoLock) Order by Descripcion ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'TiposDeMail'", "");
            sConsultaExec = sSql;

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.TiposDeMail(strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet Correos(string Funcion)
        {
            string strMsj = "Catálogo de Tipos de Correo";

            sSql =
                "Select 'Tipo de Correo' = Descripcion, 'Clave' = IdTipoEMail " +
                " From CFDI_TiposEmail (NoLock)  ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Correos'", "");
            sConsultaExec = sSql;

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Correos(strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet Telefonos(string Funcion)
        {
            string strMsj = "Catálogo de Tipos de Teléfono";

            sSql =
                "Select 'Tipo de Teléfono' = Descripcion, 'Clave' = IdTipoTelefono " +
                " From CFDI_TiposTelefonos (NoLock)  ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Telefonos'", "");
            sConsultaExec = sSql;

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Telefonos(strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet ProveedoresEMail(string Funcion)
        {
            string strMsj = "Catálogo de Proveedores de EMail";
            sConsultaExec = "";

            sSql = "Select 'Proveedor de Servicio' = Descripcion, 'Clave de Proveedor de Email' = IdProveedorEMail " +
                " From CFDI_ProveedoresEMail (NoLock) Order by Descripcion ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Marcas'", "");
            sConsultaExec = sSql;

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.ProveedoresEMail(strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet UnidadesDeMedida(string Funcion)
        {
            string strMsj = "Catálogo de Unidades de Medida";
            sConsultaExec = "";

            sSql = "Select 'Unidad de medida' = Descripcion, 'Clave Unidad de Medida' = IdUnidadDeMedida From CFDI_UnidadesDeMedida (NoLock) Order by Descripcion ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'UnidadesDeMedida'", "");
            sConsultaExec = sSql;

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.UnidadesDeMedida(strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }
        #endregion Tipos de Correo y Telefono

        #region Geograficos
        public DataSet CFDI_Estados(string Funcion)
        {
            string strMsj = "Catálogo de Estados";

            sSql = "Select 'Estado' = Nombre, 'Clave de Estado' = IdEstado " +
                " From CatEstados (NoLock) " +
                " Order by Nombre ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'CFDI Estados'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.CFDI_Estados(strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet CFDI_Municipios(string IdEstado, string Funcion)
        {
            string strMsj = "Catálogo de Municipios";

            sSql = string.Format("Select 'Municipio' = Descripcion, 'Clave de Municipio' = IdMunicipio " +
                " From CatMunicipios (NoLock) " +
                " Where IdEstado = '{0}' " +
                " Order by Descripcion ", Fg.PonCeros(IdEstado, 2));
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'CFDI Municipios'", "");

            sConsultaExec = sSql;
            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase, true);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.CFDI_Municipios(IdEstado, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet CFDI_Colonias(string IdEstado, string IdMunicipio, string Funcion)
        {
            string strMsj = "Catálogo de Colonias";

            sSql = string.Format("Select 'Colonia' = Descripcion, 'Código Postal' = CodigoPostal, 'Clave de Colonia' = IdColonia " +
                " From CatColonias (NoLock) " +
                " Where IdEstado = '{0}' and IdMunicipio = '{1}' " +
                " Order by Descripcion ", Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdMunicipio, 4));
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'CFDI Colonias'", "");

            sConsultaExec = sSql;
            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase, true);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.CFDI_Colonias(IdEstado, IdMunicipio, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }
        #endregion Geograficos
        #endregion Funciones y procedimientos publicos

        #region Funciones y procedimientos privados
        private void MsjNoDatos(ref DataSet dts, string strMsj)
        {
            if (bEjecuto)
            {
                dts = new DataSet("Vacio");
                MessageBox.Show(strMsjNoDatos, strMsj, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private string MostrarForma(string strMsj, DataSet dts)
        {
            return MostrarForma(strMsj, dts, false, 1);
        }

        private string MostrarForma(string strMsj, DataSet dts, int ColInicialCombo)
        {
            return MostrarForma(strMsj, dts, false, ColInicialCombo);
        }

        private string MostrarForma(string strMsj, DataSet dts, bool AccesarLocal)
        {
            return MostrarForma(strMsj, dts, AccesarLocal, 1);
        }

        /// <summary>
        /// Muestra la pantalla de Ayudas, diseño para manejo de grandes catalgos.
        /// </summary>
        /// <param name="Titulo">Titulo de la Consulta</param>
        /// <param name="Consulta">Consulta a ejecutar</param>
        /// <param name="AccesarLocal">Determina si se accesan los datos del servidor</param>
        /// <param name="ColInicialCombo">Columna inicial de busqueda</param>
        /// <returns></returns>
        private string MostrarForma(string Titulo, string Consulta, string MsjError, bool AccesarLocal, int ColInicialCombo)
        {
            //// return MostrarForma(strMsj, dts, AccesarLocal, ColInicialCombo, false, "", ""); 
            Frm_Ayuda = new FrmAyuda(ConexionSql);
            Frm_Ayuda.Text = Titulo;
            Frm_Ayuda.bAccesarA_BD_Local = AccesarLocal;
            Frm_Ayuda.CargarAyuda(Consulta, MsjError, ColInicialCombo);
            Fg.CentrarForma(Frm_Ayuda);

            Frm_Ayuda.MostrarPantalla();

            string sRegresa = Frm_Ayuda.strResultado;

            return sRegresa;
        }

        private string MostrarForma(string Titulo, DataSet dts, bool AccesarLocal, int ColInicialCombo)
        {
            //// return MostrarForma(strMsj, dts, AccesarLocal, ColInicialCombo, false, "", ""); 
            Frm_Ayuda = new FrmAyuda();
            Frm_Ayuda.Text = Titulo;
            Frm_Ayuda.bAccesarA_BD_Local = AccesarLocal;
            Frm_Ayuda.dtsAyuda = dts;
            Frm_Ayuda.pfConfiguraListView(ColInicialCombo);
            Fg.CentrarForma(Frm_Ayuda);
            Frm_Ayuda.ShowDialog();

            string sRegresa = Frm_Ayuda.strResultado;

            return sRegresa;
        }

        ////private string MostrarForma(string strMsj, DataSet dts, bool AccesarLocal, int ColInicialCombo, 
        ////    bool ConsutalDinamica, string OrigenDeDatos, string Ordenamiento)
        ////{
        ////    Frm_Ayuda = new FrmAyuda();

        ////    Frm_Ayuda.Conexion = this.ConexionSql; 
        ////    Frm_Ayuda.ConsultaDinamica = ConsutalDinamica;
        ////    Frm_Ayuda.OrigenDeDatos = OrigenDeDatos;
        ////    Frm_Ayuda.Ordenamiento = Ordenamiento;

        ////    Frm_Ayuda.Text = strMsj;
        ////    Frm_Ayuda.bAccesarA_BD_Local = AccesarLocal;
        ////    Frm_Ayuda.dtsAyuda = dts;
        ////    Frm_Ayuda.pfConfiguraListView(ColInicialCombo);

        ////    Fg.CentrarForma(Frm_Ayuda);
        ////    Frm_Ayuda.ShowDialog();
        ////    string sRegresa = Frm_Ayuda.strResultado;

        ////    return sRegresa;
        ////} 

        private void ValidaDatos(ref DataSet dtsValidar)
        {
            if (error.ExistenErrores(dtsValidar))
            {
                // Buscar en el dataset la tabla de errores                    
                myResult = error.MostrarVentanaError(true, false, dtsValidar);
                dtsValidar = new DataSet("Vacio");
            }

            bExistenDatos = ExistenDatosEnDataset(dtsValidar);
        }

        private DataSet EjecutarQuery(string Funcion, string prtQuery, string MensajeError, string MensajeNoEncontrado)
        {
            clsLeer Leer = new clsLeer(ref ConexionSql);
            DataSet dtsResultados = new DataSet();

            sConsultaExec = prtQuery;

            bEjecuto = false;
            bExistenDatos = false;
            if (!Leer.Exec(" Set DateFormat YMD " + prtQuery))
            {
                General.Error.GrabarError(Leer.Error, ConexionSql.DatosConexion, this.sNameDll, this.sVersion, this.sPantalla, Funcion, Leer.QueryEjecutado);
                General.msjError(MensajeError);
            }
            else
            {
                bEjecuto = true;
                if (!Leer.Leer())
                {
                    //if (bMostrarMsjLeerVacio)
                    //    General.msjUser(MensajeNoEncontrado);
                }
                else
                {
                    bExistenDatos = true;
                    dtsResultados = Leer.DataSetClase;
                }
            }

            return dtsResultados;
        }

        private object EjecutarQuery(string prtQuery, string prtTabla)
        {
            object objRetorno = null;
            DataSet dtsRetorno = new DataSet("Vacio");
            Datos.CadenaDeConexion = strCnnString;
            bExistenDatos = false;

            sConsultaExec = prtQuery;

            try
            {
                if (bUsarCnnRedLocal)
                {
                    objRetorno = (object)Datos.ObtenerDataset(prtQuery, prtTabla);
                }
                else
                {
                    // objRetorno = (object)cnnWebServ.ObtenerDataset(strCnnString, prtQuery, prtTabla);
                }

                dtsRetorno = (DataSet)objRetorno;
                if (error.ExistenErrores(dtsRetorno))
                {
                    // Buscar en el dataset la tabla de errores                    
                    myResult = error.MostrarVentanaError(true, false, dtsRetorno);
                    dtsRetorno = new DataSet("Vacio");
                    objRetorno = (object)dtsRetorno;
                }

                bExistenDatos = ExistenDatosEnDataset(dtsRetorno);

            }
            catch (Exception e)
            {
                e = (Exception)objRetorno;
                dtsRetorno = new DataSet("Vacio");
                objRetorno = (object)dtsRetorno;

                errorLog = new clsLogError(e);
                error = new clsErrorManager(errorLog.ListaErrores);
                myResult = error.MostrarVentanaError(true, false, errorLog.ListaErrores);
            }

            return objRetorno;
        }

        private bool ExistenDatosEnDataset(DataSet dtsRevisar)
        {
            bool bRegresa = false;

            if (dtsRevisar.Tables.Count > 0)
            {
                if (dtsRevisar.Tables[0].Rows.Count > 0)
                    bRegresa = true;
            }

            return bRegresa;
        }
        #endregion

    }
}
