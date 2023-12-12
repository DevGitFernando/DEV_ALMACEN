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

namespace Dll_SII_IMediaccess.Validaciones_MA
{
    public class clsValidar_Elegibilidad
    {
        enum InformacionElegibilidad
        {
            Ninguna = 0,
            Empresa = 0,
            ReferenciaBeneficiario = 1,
            NombreBeneficiario = 2,
            ReferenciaProveedor = 3,
            NombreProveedor = 4,
            Copago = 5,
            TipoCopago = 6,
            Producto = 7
        }

        clsConexionSQL con = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrabarError Error;
        basGenerales Fg = new basGenerales(); 

        Dll_SII_IMediaccess.wsSrvMediaccess.VerificaElegibilidadConCopago MA_wsValidacion; 

        string sIdEmpresa = DtGeneral.EmpresaConectada;
        string sIdEstado = DtGeneral.EstadoConectado;
        string sIdFarmacia = DtGeneral.FarmaciaConectada;

        string sMA_NumElegibilidad = "";
        string sMA_EmpresaRazonSocial = "";
        string sMA_PlanProducto = "";
        string sMA_ReferenciaBeneficiario = "";
        string sMA_NombreBeneficiario = "";
        string sMA_ReferenciaProveedor = "";
        string sMA_NombreProveedor = "";
        double dMA_Copago = 0;
        string sMA_CopagoEN = "";
        TipoDeCopago tpCopago = TipoDeCopago.Ninguno;
        string sMA_FolioDeReceta = "";
        string sMA_FolioDeConsecutivo = "";
        string sMA_FolioDeReceta_Asociado = "";
        string sMA_CIE_10_Principal = "";
        string sMA_FechaEmisionReceta = "";
        bool bMA_EsRecetaManual = true;
        int iMA_NumDeRecetas = 0;

        string sIdCliente = "";
        string sIdSubCliente = "";
        string sIdPrograma = "";
        string sIdSubPrograma = "";
        string sIdBeneficiario = "";
        string sMa_IdClinica = GnDll_SII_IMediaccess.Ma_IdClinica;
        string sIdMedico = "";
        bool bRecetaSurtida = false; 
        string sListaClaves_Receta = "";

        string sError = "";

        bool bElegilidad_Valida = false;
        bool bElegilidad_Valida_ParaSurtido = false;
        string sMensajeError_Elegibilidad = "";

        #region Constructores y Destructores de Clase 
        public clsValidar_Elegibilidad()
        {
            MA_wsValidacion = new VerificaElegibilidadConCopago();
            MA_wsValidacion.Url = GnDll_SII_IMediaccess.URL_Validaciones;
            MA_wsValidacion.Timeout = (int)((1000.00 * 60.00) * 3.0); // Minutos 

            leer = new clsLeer(ref con);
            Error = new clsGrabarError(General.DatosConexion, GnDll_SII_IMediaccess.DatosApp, "clsValidar_Elegibilidad()"); 
        }
        #endregion Constructores y Destructores de Clase

        #region Propiedades 
        public string Mensaje_Error_Elegibilidad
        {
            get { return sMensajeError_Elegibilidad; }
        }

        public string Elegibilidad
        {
            get { return sMA_NumElegibilidad; }
        }

        public bool Elegilidad_Valida_ParaSurtido
        {
            get { return bElegilidad_Valida_ParaSurtido; }
        }

        public string Mensaje__Elegilidad_Valida_ParaSurtido
        {
            get 
            {
                string sRegresa = bElegilidad_Valida_ParaSurtido ? "Elegiblidad valida para surtido" : "Elegiblidad no valida para surtido"; 
                return sRegresa; 
            }
        }

        public Color Color__Elegilidad_Valida_ParaSurtido
        {
            get 
            {
                Color colFuente = bElegilidad_Valida_ParaSurtido ? Color.Black : Color.Red;
                return colFuente; 
            }
        }

        public string EmpresaRazonSocial
        {
            get { return sMA_EmpresaRazonSocial; }
        }

        public string PlanProducto
        {
            get { return sMA_PlanProducto; }
        }

        public string IdClinica
        {
            get { return sMa_IdClinica; }
        }
        
        public string ReferenciaBeneficiario
        {
            get { return sMA_ReferenciaBeneficiario; }
        }

        public string NombreBeneficiario
        {
            get { return sMA_NombreBeneficiario; }
        }

        public string ReferenciaProveedor
        {
            get { return sMA_ReferenciaProveedor; }
        }

        public string NombreProveedor
        {
            get { return sMA_NombreProveedor; }
        }

        public double Copago
        {
            get { return dMA_Copago; }
        }

        public string CopagoEN
        {
            get { return sMA_CopagoEN; }
        }

        public TipoDeCopago TipoCopago
        {
            get { return tpCopago; }
        }

        public string TituloCopago
        {
            get 
            {
                string sRegresa = "";

                if ( bElegilidad_Valida_ParaSurtido ) 
                {
                    sRegresa = string.Format("En {0}, {1} ", sMA_CopagoEN, dMA_Copago);
                }

                return sRegresa; 
            }
        }

        public bool Elegilidad_Valida
        {
            get { return bElegilidad_Valida;  }
        }

        public string MA_FolioDeReceta
        {
            get { return sMA_FolioDeReceta; }
        }

        public string MA_FolioDeConsecutivo
        {
            get { return sMA_FolioDeConsecutivo; }
            set { sMA_FolioDeConsecutivo = value; }
        }

        public string MA_FolioDeReceta_Asociado
        {
            get { return sMA_FolioDeReceta_Asociado; }
        }

        public string MA_CIE_10_Principal
        {
            get { return sMA_CIE_10_Principal; }
        }

        public string MA_FechaEmisionReceta
        {
            get { return sMA_FechaEmisionReceta; }
        }

        public string MA_FolioDeReceta_Manual
        {
            get { return sMA_FolioDeReceta; }
            set { sMA_FolioDeReceta = value; }
        }

        public bool MA_EsRecetaManual
        {
            get { return bMA_EsRecetaManual; }
        }

        public int MA_NumDeReceta
        {
            get { return iMA_NumDeRecetas; }
        }

        #endregion Propiedades 

        #region Propiedades Dispensacion 
        public string IdCliente
        {
            get { return sIdCliente; }
        }

        public string IdSubCliente
        {
            get { return sIdSubCliente; }
        }

        public string IdPrograma
        {
            get { return sIdPrograma; }
        }

        public string IdSubPrograma
        {
            get { return sIdSubPrograma; }
        }

        public string IdBeneficiario
        {
            get { return sIdBeneficiario; }
        }

        public string IdMedico
        {
            get { return sIdMedico; }
        }

        public string ListaClaves_Receta
        {
            get { return sListaClaves_Receta; }
        }

        public string ListaClaves_Receta_Extended
        {
            get { return sListaClaves_Receta.Replace("'", "''"); }
        }
        
        public bool RecetaSurtida
        {
            get { return bRecetaSurtida; }
        }
        #endregion Propiedades Dispensacion

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

        public bool Validar_FolioDeReceta(string NumeroDeElegibilidad, string FolioDeReceta, string Consecutivo)
        {
            bool bRegresa = false;
            bRecetaSurtida = false; 
            string sSql = string.Format("Exec spp_INT_MA__ConsultarFolioDeReceta " +
                " @Elegibilidad = '{0}', @Folio_MA = '{1}', @Consecutivo = '{2}'   \n", NumeroDeElegibilidad, FolioDeReceta, Consecutivo);

            sSql += string.Format("Exec spp_INT_MA__Configurar_InformacionDispensacion " +
                "@IdEstado = '{0}', @IdFarmacia = '{1}', @IdCliente = '{2}', @IdSubCliente = '{3}', @IdPrograma = '{4}', @IdSubPrograma = '{5}', @Status = '{6}' ", 
                sIdEstado, sIdFarmacia, sIdCliente, sIdSubCliente, sIdPrograma, sIdSubPrograma, "A" );

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Validar_FolioDeReceta");
            }
            else
            {
                if (leer.Leer())
                {
                    bRegresa = true;
                    sMA_FolioDeReceta = leer.Campo("FolioReceta");
                    sMA_CIE_10_Principal = leer.Campo("CIE_10_Principal");
                    sMA_FechaEmisionReceta = leer.Campo("FechaEmisionReceta");
                    bMA_EsRecetaManual = leer.CampoBool("EsRecetaManual");
                    iMA_NumDeRecetas = leer.CampoInt("NumDeRecetas");

                    if (leer.CampoInt("StatusSurtido") != 0)
                    {
                        bRegresa = false;
                        bRecetaSurtida = true; 
                    }

                    sListaClaves_Receta = "";
                    leer.RegistroActual = 1;
                    while(leer.Leer())
                    {
                        sListaClaves_Receta += string.Format("'{0}', ", leer.Campo("ClaveSSA"));
                    }

                    sListaClaves_Receta = sListaClaves_Receta.Trim();
                    sListaClaves_Receta = Fg.Mid(sListaClaves_Receta, 1, sListaClaves_Receta.Length - 1); 
                }
            }

            return bRegresa; 
        }

        public bool Validar_Elegibilidad(string NumeroDeProveedor, string NumeroDeElegibilidad)
        {            
            bool bRegresa = false;
            bool bBuscarConProveedor = true;
            string sSql = string.Format("Exec spp_INT_MA__ConsultarElegibilidades " +
                " @Elegibilidad = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}' ", 
                NumeroDeElegibilidad, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada );

            bElegilidad_Valida = false;
            bElegilidad_Valida_ParaSurtido = false;
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Validar_Elegibilidad");
            }
            else
            {
                if (leer.Leer())
                {
                    bRegresa = true; 
                    bBuscarConProveedor = false;
                    sMA_NumElegibilidad = NumeroDeElegibilidad;
                    sMA_EmpresaRazonSocial = leer.Campo("Empresa_y_RazonSocial");
                    sMA_PlanProducto = leer.Campo("PlanProducto");
                    sMA_ReferenciaBeneficiario = leer.Campo("ReferenciaEmpleado");
                    sMA_NombreBeneficiario = leer.Campo("NombreEmpleado");
                    sMA_ReferenciaProveedor = leer.Campo("ReferenciaMedico");
                    sMA_NombreProveedor = leer.Campo("NombreMedico");
                    dMA_Copago = leer.CampoDouble("Copago");
                    tpCopago = (TipoDeCopago)leer.CampoInt("CopagoEn");
                    sMA_CopagoEN = tpCopago.ToString();

                    sIdCliente = leer.Campo("IdCliente");
                    sIdSubCliente = leer.Campo("IdSubCliente");
                    sIdPrograma = leer.Campo("IdPrograma");
                    sIdSubPrograma = leer.Campo("IdSubPrograma");
                    sIdBeneficiario = leer.Campo("IdBeneficiario");
                    sIdMedico = leer.Campo("IdMedico");
                    sMA_FolioDeReceta_Asociado = leer.Campo("FolioReceta");
                    sMa_IdClinica = leer.Campo("IdClinica");

                    if (leer.CampoBool("Elegibilidad_DisponibleSurtido")) 
                    {
                        bElegilidad_Valida = true;
                        bElegilidad_Valida_ParaSurtido = true;
                    }

                    //bElegilidad_Valida_ParaSurtido = bes
                }
            }

            if (bBuscarConProveedor)
            {
                bRegresa = Validar_Elegibilidad__ConProveedor(NumeroDeProveedor, NumeroDeElegibilidad); 
            }

            return bRegresa; 
        }

        private bool Validar_Elegibilidad__ConProveedor(string NumeroDeProveedor, string NumeroDeElegibilidad)
        {
            bool bRegresa = false;
            string sRetorno = "";
            string[] sRetorno_Valores = null;

            bElegilidad_Valida = false;
            bElegilidad_Valida_ParaSurtido = false; 
            sMensajeError_Elegibilidad = "";  // "Elegibilidad invalida para atención.";
            sMA_NumElegibilidad = "";
            sMA_EmpresaRazonSocial = "";
            sMA_PlanProducto = ""; 
            sMA_ReferenciaBeneficiario = "";
            sMA_NombreBeneficiario = "";
            sMA_ReferenciaProveedor = "";
            sMA_NombreProveedor = "";
            dMA_Copago = 0;
            tpCopago = TipoDeCopago.Ninguno;
            sMA_CopagoEN = "";

            sError = ""; 
            try
            {
                sRetorno = MA_wsValidacion.ObtenDatos(NumeroDeProveedor, NumeroDeElegibilidad);
                bRegresa = true;
                
                try
                {
                    sRetorno = sRetorno.Replace("||", "^");
                    sRetorno_Valores = sRetorno.Split('^'); 
                }
                catch 
                { }

                if (sRetorno_Valores.Length <= 2)
                {
                    try
                    {
                        sMensajeError_Elegibilidad = "Elegibilidad invalida para atención.";
                        sMensajeError_Elegibilidad = sRetorno_Valores[1];
                    }
                    catch { }
                }
                else 
                {
                    bElegilidad_Valida = true;
                    bElegilidad_Valida_ParaSurtido = true; 

                    ////sMA_NumElegibilidad = NumeroDeElegibilidad;
                    ////sMA_EmpresaRazonSocial = sRetorno_Valores[0];
                    ////sMA_ReferenciaBeneficiario = sRetorno_Valores[1];
                    ////sMA_NombreBeneficiario = sRetorno_Valores[2];
                    ////sMA_ReferenciaProveedor = sRetorno_Valores[3];
                    ////sMA_NombreProveedor = sRetorno_Valores[4];
                    ////dMA_Copago = Convert.ToDouble("0" + sRetorno_Valores[5]);
                    ////tpCopago = sRetorno_Valores[6].Trim().Contains("%") ? TipoDeCopago.Porcentaje : TipoDeCopago.Monto;
                    ////sMA_CopagoEN = tpCopago.ToString();

                    sMA_NumElegibilidad = NumeroDeElegibilidad;
                    sMA_EmpresaRazonSocial = GetValor(sRetorno_Valores, InformacionElegibilidad.Empresa);
                    sMA_ReferenciaBeneficiario = GetValor(sRetorno_Valores, InformacionElegibilidad.ReferenciaBeneficiario);
                    sMA_NombreBeneficiario = GetValor(sRetorno_Valores, InformacionElegibilidad.NombreBeneficiario);
                    sMA_ReferenciaProveedor = GetValor(sRetorno_Valores, InformacionElegibilidad.ReferenciaProveedor);
                    sMA_NombreProveedor = GetValor(sRetorno_Valores, InformacionElegibilidad.NombreProveedor);
                    dMA_Copago = Convert.ToDouble("0" + GetValor(sRetorno_Valores, InformacionElegibilidad.Copago));
                    tpCopago = GetValor(sRetorno_Valores, InformacionElegibilidad.TipoCopago).Trim().Contains("%") ? TipoDeCopago.Porcentaje : TipoDeCopago.Monto;
                    sMA_CopagoEN = tpCopago.ToString();
                    sMA_PlanProducto = GetValor(sRetorno_Valores, InformacionElegibilidad.Producto);
                    sMA_PlanProducto = sMA_PlanProducto == "" ? sMA_EmpresaRazonSocial : sMA_PlanProducto;

                    bElegilidad_Valida = guardarElegibilidad(); 
                }
            }
            catch (Exception ex)
            {
                bRegresa = false;
                sError = ex.Message; 
            }

            return bRegresa; 
        }

        private string GetValor(string[] Valores, InformacionElegibilidad Item)
        {
            string sRegresa = "";
            int iPosicion = (int)Item;

            try
            {
                sRegresa = Valores[iPosicion];
            }
            catch (Exception ex)
            {
            }

            return sRegresa; 
        }

        private bool guardarElegibilidad()
        {
            bool bRegresa = false; 
            string sSql = string.Format("Exec spp_INT_MA__RegistrarElegibilidades " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdPersonal = '{3}', @Elegibilidad = '{4}', " + 
                " @Empresa_y_RazonSocial = '{5}', @NombreEmpleado = '{6}', @ReferenciaEmpleado = '{7}', " +
                " @NombreMedico = '{8}', @ReferenciaMedico = '{9}', @Copago = '{10}', @CopagoEn = '{11}', @Plan_Producto = '{12}' ",
                sIdEmpresa, sIdEstado, sIdFarmacia, DtGeneral.IdPersonal, sMA_NumElegibilidad, sMA_EmpresaRazonSocial,
                sMA_NombreBeneficiario, sMA_ReferenciaBeneficiario, sMA_NombreProveedor, sMA_ReferenciaProveedor,
                dMA_Copago, (int)tpCopago, sMA_PlanProducto);

            if (!con.Abrir())
            {
                General.msjError("Ocurrió un error al registrar la información.");
            }
            else
            {
                con.IniciarTransaccion(); 

                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    sError = "Ocurrió un error al registrar la información.";
                    Error.GrabarError(leer, "guardarElegibilidad()");
                }
                else
                {
                    if (leer.Leer())
                    {
                        bRegresa = true;
                        sIdCliente = leer.Campo("IdCliente");
                        sIdSubCliente = leer.Campo("IdSubCliente");
                        sIdPrograma = leer.Campo("IdPrograma");
                        sIdSubPrograma = leer.Campo("IdSubPrograma");
                        sIdBeneficiario = leer.Campo("IdBeneficiario");
                        sIdMedico = leer.Campo("IdMedico");
                        sMA_FolioDeReceta_Asociado = leer.Campo("FolioReceta"); 
                    }
                }

                if (!bRegresa)
                {
                    con.DeshacerTransaccion(); 
                }
                else
                {
                    con.CompletarTransaccion(); 
                }

                con.Cerrar(); 
            }

            return bRegresa; 
        }
        #endregion Funciones y Procedimientos Publicos
     }
}
