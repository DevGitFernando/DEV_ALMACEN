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
using Dll_SII_IMediaccess;

using Dll_SII_IMediaccess.wsSrvMediaccess;

namespace Dll_SII_IMediaccess.Ventas_IME
{
    public class clsValidar_Vale
    {
        clsConexionSQL con = new clsConexionSQL(General.DatosConexion);
        clsLeer leer, leerDetalle, leerTemp;
        clsGrabarError Error;
        basGenerales Fg = new basGenerales(); 

        //Dll_SII_IMediaccess.wsSrvMediaccess.VerificaElegibilidadConCopago MA_wsValidacion; 

        string sIdEmpresa = DtGeneral.EmpresaConectada;
        string sIdEstado = DtGeneral.EstadoConectado;
        string sIdFarmacia = DtGeneral.FarmaciaConectada;

        string sIdSocioComercial = "";
        string sNombreSocioComercial = "";
        string sIdSucursalSocioComercial = "";
        string sNombreSucursalSocioComercial = ""; 
        string sFolioVale = "";

        bool bDatosObtenidos = false;
        bool bMA_EsValeManual = false; 

        string sIdCliente = "";
        string sClienteNombre = "";
        string sIdSubCliente = "";
        string sSubClienteNombre = "";
        string sIdPrograma = "";
        string sIdSubPrograma = "";
        string sIdBeneficiario = "";
        string sMa_IdClinica = GnDll_SII_IMediaccess.Ma_IdClinica;
        string sIdMedico = "";
        string sMA_FolioDeReceta = "";
        string sMA_FechaEmisionReceta = "";

        bool bValeSurtido = false; 
        string sListaClaves_Receta = "";

        string sError = "";

        bool bVale_Valido = false;
        bool bVale_Surtido_Parcial = false;
        bool bVale_Surtido_Completo = false;
        bool bVale_Valido_ParaSurtido = false;
        string sMensajeError_Vale = "";

        #region Constructores y Destructores de Clase 
        public clsValidar_Vale()
        {
            ////MA_wsValidacion = new VerificaElegibilidadConCopago();
            ////MA_wsValidacion.Url = GnDll_SII_IMediaccess.URL_Validaciones;
            ////MA_wsValidacion.Timeout = (int)((1000.00 * 60.00) * 3.0); // Minutos 

            leer = new clsLeer(ref con);
            Error = new clsGrabarError(General.DatosConexion, GnDll_SII_IMediaccess.DatosApp, "clsValidar_Vale()"); 
        }
        #endregion Constructores y Destructores de Clase

        #region Propiedades 
        public string IdSocioComercial
        {
            get { return sIdSocioComercial; }
            set { sIdSocioComercial = Fg.PonCeros(value, 8); }
        }

        public string NombreSocioComercial
        {
            get { return sNombreSocioComercial; }
            set { sNombreSocioComercial = value; }
        }

        public string IdSucursalSocioComercial
        {
            get { return sIdSucursalSocioComercial; }
            set { sIdSucursalSocioComercial = Fg.PonCeros(value, 8); }
        }

        public string NombreSucursalSocioComercial
        {
            get { return sNombreSucursalSocioComercial; }
            set { sNombreSucursalSocioComercial = value; }
        }

        public string FolioVale
        {
            get { return sFolioVale; }
            set { sFolioVale = Fg.PonCeros(value, 8); }
        }

        public string Mensaje_Error_Vale
        {
            get { return sMensajeError_Vale; }
        }

        ////public string Elegibilidad
        ////{
        ////    get { return sMA_NumElegibilidad; }
        ////}

        public bool Vale_Valido_ParaSurtido
        {
            get { return bVale_Valido_ParaSurtido; }
            set { bVale_Valido_ParaSurtido = value; }
        }

        public bool Vale_Con_Surtido_Parcial
        {
            get { return bVale_Surtido_Parcial; }
            set { bVale_Surtido_Parcial = value; }
        }

        public bool Vale_Con_Surtido_Completo
        {
            get { return bVale_Surtido_Completo; }
            set { bVale_Surtido_Completo = value; }
        }

        public string Mensaje__Vale_Valido_ParaSurtido
        {
            get 
            {
                string sRegresa;

                sRegresa = bVale_Valido_ParaSurtido ? "Vale valido para surtido" : "Vale no valido para surtido";
                sRegresa = bVale_Surtido_Parcial ? "Vale con surtido parcial" : sRegresa;
                sRegresa = bVale_Surtido_Completo ? "Vale surtido completo" : sRegresa;

                return sRegresa; 
            }
        }

        public Color Color__Vale_Valido_ParaSurtido
        {
            get 
            {
                Color colFuente;

                colFuente = bVale_Valido_ParaSurtido ? Color.Black : Color.Red;
                colFuente = bVale_Surtido_Parcial ? Color.YellowGreen : colFuente;
                colFuente = bVale_Surtido_Completo ? Color.Red : colFuente;

                return colFuente; 
            }
        }
        
        public bool Vale_Valido
        {
            get { return bVale_Valido;  }
        }

        public bool ValeManual
        {
            get { return bMA_EsValeManual; }
        }
        #endregion Propiedades 

        #region Propiedades Dispensacion
        public string MA_FolioDeReceta
        {
            get {
                if (sMA_FolioDeReceta == "")
                {
                    ObtenerIdBeneficiarioIdMedico();
                }
                return sMA_FolioDeReceta; }
        }

        public string MA_FechaEmisionReceta
        {
            get {
                if (sMA_FechaEmisionReceta == "")
                {
                    ObtenerIdBeneficiarioIdMedico();
                }
                return sMA_FechaEmisionReceta; }
        }

        public string IdCliente
        {
            get { return sIdCliente; }
            set { sIdCliente = Fg.PonCeros(value, 4); }
        }

        public string ClienteNombre 
        {
            get { return sClienteNombre; }
            set { sClienteNombre = value; }
        }

        public string IdSubCliente
        {
            get { return sIdSubCliente; }
            set { sIdSubCliente = Fg.PonCeros(value, 4); }
        }

        public string SubClienteNombre
        {
            get { return sSubClienteNombre; }
            set { sSubClienteNombre = value; }
        }

        public string IdPrograma
        {
            get { return sIdPrograma; }
            set { sIdPrograma = Fg.PonCeros(value, 4); }
        }

        public string IdSubPrograma
        {
            get { return sIdSubPrograma; }
            set { sIdSubPrograma = Fg.PonCeros(value, 4); }
        }

        public string IdBeneficiario
        {
            get {
                    if (sIdBeneficiario == "")
                    {
                        ObtenerIdBeneficiarioIdMedico();
                    }
                    return sIdBeneficiario;
                }
        }

        public string IdMedico
        {
            get {
                    if (sIdMedico == "")
                    {
                        ObtenerIdBeneficiarioIdMedico();
                    }
                
                    return sIdMedico;
                }
        }

        public string ListaClaves_Receta
        {
            get { return sListaClaves_Receta; }
        }

        public string ListaClaves_Receta_Extended
        {
            get { return sListaClaves_Receta.Replace("'", "''"); }
        }
        
        public bool ValeSurtido
        {
            get { return bValeSurtido; }
        }

        public clsLeer Detalle
        {
            get { return leerDetalle; }
        }

        public int Registros_Detalle
        {
            get { return leerDetalle.Registros; }
        }
        #endregion Propiedades Dispensacion

        #region Funciones y Procedimientos Privados
        private void ObtenerIdBeneficiarioIdMedico()
        {
            string sSql = string.Format("Exec spp_INT_MA__ConsultarVale @IdSocioComercial = '{0}', @IdSucursal = '{1}', @Folio_Vale = '{2}', " +
                    "@IdCliente = '{3}', @IdSubCliente = '{4}', @IdEstado = '{5}', @IdFarmacia = '{6}'",
                    sIdSocioComercial, sIdSucursalSocioComercial, sFolioVale, sIdCliente, sIdSubCliente, sIdEstado, sIdFarmacia);

            if (!bDatosObtenidos && !bMA_EsValeManual)
            {
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "ObtenerIdBeneficiarioIdMedico");
                }
                else
                {
                    if (leer.Leer())
                    {
                        sIdBeneficiario = leer.Campo("IdBeneficiario");
                        sIdMedico = leer.Campo("IdMedico");
                        sMA_FolioDeReceta = leer.Campo("NumReceta");
                        sMA_FechaEmisionReceta = leer.Campo("FechaReceta");
                        bMA_EsValeManual = leer.CampoBool("EsValeManual");
                    }
                }
            }
        }
        #endregion  Funciones y Procedimientos Privados

        #region Funciones y Procedimientos Publicos
        public bool RegistrarAtencion_Elegibilidad(clsLeer Registro, string NumeroDeElegibilidad, string FolioReceta, 
            string IdEmpresa, string IdEstado, string IdFarmacia, string FolioDispensacion, string IdPersonal)
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_INT_MA__RegistrarAtencionElegibilidades " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdPersonal = '{3}', " + 
                " @Elegibilidad = '{4}', @FolioReceta = '{5}', @FolioDispensacion = '{6}'  \n",
                IdEmpresa, IdEstado, IdFarmacia, IdPersonal, 
                NumeroDeElegibilidad, FolioReceta, FolioDispensacion);

            if (!Registro.Exec(sSql))
            {
                //Error.GrabarError(Registro, "RegistrarAtencion_Elegibilidad");
            }
            else
            {
                bRegresa = true; 
            }

            return bRegresa;
        }


        public bool RegistrarAtencion_Vale(clsLeer Registro, string idSocioComercial, string IdSucursal, string FolioVale, string IdEmpresa, string IdEstado, string IdFarmacia, string FolioDispensacion, string IdPersonal)
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_INT_IME__RegistroDeVales_003_Surtidos " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioVenta = '{3}'," +
                " @IdSocioComercial = '{4}', @IdSucursal = '{5}', @Folio_Vale = '{6}'  \n",
                IdEmpresa, IdEstado, IdFarmacia, FolioDispensacion, idSocioComercial, IdSucursal, FolioVale);

            if (Registro.Exec(sSql))
            {
                bRegresa = true;
            }

            return bRegresa;
        }

        public bool Validar_Vale(string SocioComerciales, string IdSocioComercial_sucursal, string Folio)
        {            
            bool bRegresa = false;
            string sFiltro = "";
            Folio = Fg.PonCeros(Folio, 8);

            sIdSocioComercial = SocioComerciales;
            sIdSucursalSocioComercial = IdSocioComercial_sucursal;
            sFolioVale = Folio;


            leerDetalle = new clsLeer(ref con);
            leerTemp  = new clsLeer();

            string sSql = string.Format("Select *, (CantidadSolicitada - CantidadSurtida) As Cantidad " +
                " From INT_IME__RegistroDeVales_002_Claves (NoLock) " +
                "Where IdSocioComercial = '{0}' And IdSucursal = '{1}' And Folio_Vale = '{2}' ",
                SocioComerciales, IdSocioComercial_sucursal, Folio);

            bVale_Valido = false;
            bVale_Valido_ParaSurtido = false;
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Validar_Elegibilidad");
            }
            else
            {
                if (leer.Leer())
                {
                    bRegresa = true;
                    bVale_Valido = true;

                    leerDetalle.DataSetClase = leer.DataSetClase;

                    if (leerDetalle.Registros > 0)
                    {
                        bVale_Valido_ParaSurtido = true;
                    }

                    sFiltro = "CantidadSurtida > 0";
                    leerTemp.DataRowsClase = leerDetalle.DataSetClase.Tables[0].Select(sFiltro);

                    if (leerTemp.Registros > 0)
                    {
                        bVale_Valido_ParaSurtido = true;
                        bVale_Surtido_Parcial = true;
                    }

                    sFiltro = "Cantidad = 0";
                    leerTemp.DataRowsClase = leerDetalle.DataSetClase.Tables[0].Select(sFiltro);

                    if (leerTemp.Registros > 0)
                    {
                        bVale_Valido_ParaSurtido = false;
                        bVale_Surtido_Completo = true;
                    }
                }
                else
                {
                    bRegresa = true;
                }
            }

            if (bVale_Valido_ParaSurtido)
            {
                ConsultarEncabezado();
            }

            return bRegresa; 
        }

        public void ConsultarEncabezado()
        {
            string sSql = string.Format("	Select * From INT_IME__RegistroDeVales_001_Encabezado (NoLock) " +
                    "Where IdSocioComercial = '{0}' And IdSucursal = '{1}' And Folio_Vale = '{2}' ",
                    sIdSocioComercial, sIdSucursalSocioComercial, sFolioVale);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ConsultarEncabezado()");
            }
            else
            {
                if (leer.Leer())
                {
                    bMA_EsValeManual = leer.CampoBool("EsValeManual");
                }
            }
        }

        #endregion Funciones y Procedimientos Publicos
     }
}
