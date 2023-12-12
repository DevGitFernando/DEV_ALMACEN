using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.IO;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Inventario;
using Dll_SII_INadro;

namespace Dll_SII_INadro.GenerarArchivos
{
    public class Remisiones
    {
        #region Declaracion de Variables
        string sPrefijo = "RS";

        basGenerales Fg = new basGenerales();
        string sClaveCliente = "";
        DataSet dtsPedido;
        DateTime dtMarcaTiempo = DateTime.Now;
        string sMarcaTiempo = "";
        int iEsDeSurtimiento = 0;

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerInf;
        clsLeer leerExec;
        clsGrabarError Error;
        string sRutaDestino = "";
        int iCauses = 2012; 

        string sIdFarmacia = ""; 
        string sGUID = "";
        Label lblFechaProcesando = null;

        bool bGenerarHistorico = false;
        bool bSeparar_x_Causes = false; 

        string sFormato = "#######################0.#0";
        #endregion Declaracion de Variables

        #region Constructor de Clase
        public Remisiones()
        {
            dtMarcaTiempo = General.FechaSistema;
            sMarcaTiempo = string.Format("", dtMarcaTiempo.Year, dtMarcaTiempo.Month, dtMarcaTiempo.Day);


            sRutaDestino = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            sRutaDestino += @"\DOCUMENTOS_NADRO\REMISIONES\";
            CrearDirectorio(sRutaDestino); 

            Error = new clsGrabarError(General.DatosConexion, GnDll_SII_INadro.DatosApp, "Dll_SII_INadro.GenerarArchivos.Remisiones");
        }

        ~Remisiones()
        {
        }
        #endregion Constructor de Clase

        #region Propiedades 
        public int Causes
        {
            get { return iCauses; }
            set { iCauses = value; }
        }

        public string IdFarmacia
        {
            get
            {
                if (sIdFarmacia == null) sIdFarmacia = "";
                return sIdFarmacia;
            }
            set { sIdFarmacia = value; }
        }

        public string GUID
        {
            get
            {
                if (sGUID == null) sGUID = "";
                return sGUID;
            }
            set { sGUID = value; }
        }

        public Label EtiquetaFechaEnProceso
        {
            set { lblFechaProcesando = value; }
        }

        public string RutaDestinoReportes
        {
            get { return sRutaDestino; }
            set
            {
                sRutaDestino = value;
                if (sRutaDestino != "")
                {
                    sRutaDestino += @"\REMISIONES\";
                    CrearDirectorio(sRutaDestino);
                }
            }
        }

        public bool GenerarHistorico
        {
            get { return bGenerarHistorico; }
            set { bGenerarHistorico = value; }
        }

        public bool Separar_x_Causes
        {
            get { return bSeparar_x_Causes; }
            set { bSeparar_x_Causes = value; }
        }
        #endregion Propiedades

        #region Funciones y Procedimientos Publicos
        private void CrearDirectorio(string Directorio)
        {
            if (!Directory.Exists(Directorio))
            {
                Directory.CreateDirectory(Directorio);
            }
        }

        public void MsjFinalizado()
        {
            General.msjUser("Archivos de remisión generados satisfactoriamente.");
        }

        public bool GenerarRemisiones(DateTime FechaInicial, DateTime FechaFinal)
        {
            bool bRegresa = false;

            bRegresa = GenerarRemisiones(1, 1, FechaInicial, FechaFinal);

            return bRegresa;
        }

        public bool GenerarRemisiones(int EsDeSurtimiento, int TipoDeCliente, DateTime FechaInicial, DateTime FechaFinal)
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_INT_ND_ListadoDeClientes " +
                " @IdEstado = '{0}', @EsDeSurtimiento = '{1}', @TipoDeCliente = '{2}' ",
                DtGeneral.EstadoConectado, EsDeSurtimiento, TipoDeCliente);

            leerExec = new clsLeer(ref cnn);
            if (!leerExec.Exec(sSql))
            {
                Error.GrabarError(leerExec, "GenerarRemisiones()");
                ////General.msjError("Ocurrió un error al obtener el listado de clientes.");
            }
            else
            {
                if (!leerExec.Leer())
                {
                    ////General.msjUser("No se encontrarón clientes para la generacion de Remisiones.");
                }
                else
                {
                    leerExec.RegistroActual = 1;
                    while (leerExec.Leer())
                    {
                        bRegresa = GenerarRemisiones(leerExec.Campo("Código Cliente"), FechaInicial, FechaFinal);
                    }
                }
            }

            return bRegresa;
        }

        public bool GenerarRemisiones(string Cliente, DateTime FechaInicial, DateTime FechaFinal)
        {
            bool bRegresa = false;
            string sFecha = ""; //  General.FechaYMD(FechaAProcesar); 
            DateTime dtpFecha = FechaInicial;

            while (dtpFecha <= FechaFinal)
            {
                sFecha = General.FechaYMD(dtpFecha);

                if (lblFechaProcesando != null)
                {
                    lblFechaProcesando.Text = sFecha; 
                }

                sGUID = Guid.NewGuid().ToString(); 
                bRegresa = GenerarRemisiones(Cliente, sFecha);

                dtpFecha = dtpFecha.AddDays(1);
            }

            return bRegresa;
        }

        private bool GenerarRemisiones(string Cliente, string FechaAProcesar)
        {
            bool bRegresa = false;
            int iTipoExec = bGenerarHistorico ? 2 : 1;
            int iSepararCauses = bSeparar_x_Causes ? 1 : 0;

            string sSql = string.Format("Exec spp_INT_ND_GenerarRemisiones " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @CodigoCliente = '{2}', @FechaDeProceso = '{3}', @GUID = '{4}', @IdFarmacia = '{5}', @Año_Causes = '{6}', " +
                " @TipoDeProceso = '{7}', @SepararCauses = '{8}'",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, Cliente, FechaAProcesar, sGUID, sIdFarmacia, iCauses, iTipoExec, iSepararCauses);

            leer = new clsLeer(ref cnn);
            leerInf = new clsLeer();

            ////if (!cnn.Abrir())
            ////{
            ////    ////General.msjErrorAlAbriConexion();
            ////}
            ////else
            {
                ////cnn.IniciarTransaccion();
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "GenerarRemisiones()");
                }
                else
                {
                    leerInf.DataTableClase = leer.Tabla(1); 
                    bRegresa = GenerarDocumento(Cliente, leerInf);
                }


                if (!bRegresa)
                {
                    ////General.msjError("Ocurrió un error al generar la remisión");
                    ////cnn.DeshacerTransaccion(); 
                }
                else
                {
                    ////cnn.CompletarTransaccion();
                    ////General.msjUser("Archivo de pedido generado satisfactoriamente.");
                }

                ////cnn.Cerrar();
            }
            return bRegresa;
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados
        private bool GenerarDocumento(string Cliente, clsLeer Datos)
        {
            bool bRegresa = true; 
            clsLeer leer_Informacion = new clsLeer();
            clsLeer leerFiltro = new clsLeer();
            string sFiltro = "";

            leerFiltro = Datos;


            sFiltro = string.Format(" EsCauses = '{0}' ", 1 );
            leer_Informacion.DataRowsClase = leerFiltro.DataTableClase.Select(sFiltro);
            if (leer_Informacion.Leer())
            {
                GenerarDocumento(Cliente, leer_Informacion.DataSetClase, 1); 
            }

            sFiltro = string.Format(" EsCauses = '{0}' ", 0);
            leer_Informacion.DataRowsClase = leerFiltro.DataTableClase.Select(sFiltro);
            if (leer_Informacion.Leer())
            {
                GenerarDocumento(Cliente, leer_Informacion.DataSetClase, 0); 
            }

            //// Siempre regresar TRUE 
            bRegresa = true; 
            return bRegresa;

        }

        private bool GenerarDocumento(string Cliente, DataSet Informacion, int EsCauses) 
        {
            bool bRegresa = false;
            clsLeer leer_Informacion = new clsLeer(); 


            string sFileName = "";
            string sFileName_Aux = "";
            string sCadena = "";

            string sCodigoCliente = "";
            string sModulo = "";
            string sFechaDeRemision = "";
            string sAnexo = "";
            string sTipo = "";
            string sFolioRemision = ""; 
            string sClaveSSA_ND = "";
            string sClaveSSA_ND_Mascara = "";
            string sDescripcionClave = "";
            string sPrecioUnitario = ""; 
            string sCantidad = "";
            string sSubTotal = "";
            string sIva = "";
            string sTotal = "";
            string sFile_Causes_NoCauses = EsCauses == 1 ? "" : "_N";
            

            CrearDirectorio(Path.Combine(sRutaDestino, sIdFarmacia));

            leer_Informacion.DataSetClase = Informacion; 
            if (!leer_Informacion.Leer())
            {
                bRegresa = true;
                ////Error.GrabarError(string.Format("No se encontro información de remisiones del cliente : {0}", Cliente), "GenerarDocumento()");
            }
            else
            {
                try
                {
                    ////sRutaDestino += leer_Informacion.Campo("FechaGeneracion"); 
                    leer_Informacion.RegistroActual = 1;
                    sFileName_Aux = sRutaDestino + @"\" + sIdFarmacia + @"\";
                    sFileName = string.Format("{0}{1}{2}_{3}{4}.txt", sPrefijo, leer_Informacion.Campo("CodigoCliente"),
                        Fg.Right(leer_Informacion.Campo("FechaGeneracion"), 6), sIdFarmacia, sFile_Causes_NoCauses);


                    StreamWriter fileOut = new StreamWriter(Path.Combine(sFileName_Aux, sFileName));
                    while (leer_Informacion.Leer())
                    {

                        sCodigoCliente = leer_Informacion.Campo("CodigoCliente");
                        sModulo = leer_Informacion.Campo("Modulo");
                        sFechaDeRemision = Fg.Right(leer_Informacion.Campo("FechaRemision"), 8);
                        sAnexo = leer_Informacion.Campo("IdAnexo");
                        sTipo = leer_Informacion.Campo("Tipo");
                        sFolioRemision = leer_Informacion.Campo("FolioRemision");
                        sClaveSSA_ND = leer_Informacion.Campo("ClaveSSA_ND");
                        sClaveSSA_ND_Mascara = leer_Informacion.Campo("ClaveSSA_Mascara");
                        sDescripcionClave = leer_Informacion.Campo("Descripcion_Mascara");
                        sPrecioUnitario = leer_Informacion.CampoDouble("PrecioVenta").ToString(sFormato);
                        sCantidad = leer_Informacion.Campo("Cantidad");
                        sSubTotal = leer_Informacion.CampoDouble("SubTotal").ToString(sFormato); 
                        sIva = leer_Informacion.CampoDouble("Iva").ToString(sFormato);
                        sTotal = leer_Informacion.CampoDouble("ImporteTotal").ToString(sFormato);  


                        sCodigoCliente = FormatoCampos.Formato_Digitos_Izquierda(sCodigoCliente, 7, "0");
                        sModulo = FormatoCampos.Formato_Digitos_Izquierda(sModulo, 2, "0");
                        sTipo = FormatoCampos.Formato_Caracter_Derecha(sTipo, 2, " ");
                        sClaveSSA_ND = FormatoCampos.Formato_Caracter_Derecha(sClaveSSA_ND, 20, " ");
                        sClaveSSA_ND_Mascara = FormatoCampos.Formato_Caracter_Derecha(sClaveSSA_ND_Mascara, 20, " ");
                        sDescripcionClave = FormatoCampos.Formato_Caracter_Derecha(sDescripcionClave, 1500, " ");
                        sCantidad = FormatoCampos.Formato_Digitos_Izquierda(sCantidad, 7, "0");
                        sSubTotal = FormatoCampos.Formato_Digitos_Izquierda(sSubTotal, 10, "0");
                        sIva = FormatoCampos.Formato_Digitos_Izquierda(sIva, 10, "0");
                        sTotal = FormatoCampos.Formato_Digitos_Izquierda(sTotal, 10, "0");

                        sCadena = string.Format
                            (
                                "{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|",
                                sCodigoCliente, sModulo, sFechaDeRemision, sAnexo, sTipo,
                                sFolioRemision, sClaveSSA_ND_Mascara, sDescripcionClave, sPrecioUnitario, sCantidad, 
                                sSubTotal, sIva, sTotal, sClaveSSA_ND
                            );
                        fileOut.WriteLine(sCadena);
                    }
                    fileOut.Close();
                    bRegresa = true;

                    sFileName_Aux = Path.Combine(sFileName_Aux, sFileName);
                    sFileName = Path.Combine(sRutaDestino, sFileName);
                    File.Copy(sFileName_Aux, sFileName); 

                }
                catch (Exception ex)
                {
                    Error.GrabarError(ex.Message, "GenerarDocumento()___Error_Al_Generar_Documento");  
                } 
            }

            return bRegresa;
        }
        #endregion Funciones y Procedimientos Privados
    }
}
