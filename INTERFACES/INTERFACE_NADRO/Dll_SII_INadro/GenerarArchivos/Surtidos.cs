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
    public class Surtidos
    {
        #region Declaracion de Variables 
        string sPrefijo = "SU";

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

        string sGUID = "";
        Label lblFechaProcesando = null;
        bool bGenerarHistorico = false;

        #endregion Declaracion de Variables 

        #region Constructor de Clase  
        public Surtidos()
        {
            dtMarcaTiempo = General.FechaSistema; 
            sMarcaTiempo = string.Format("", dtMarcaTiempo.Year, dtMarcaTiempo.Month, dtMarcaTiempo.Day);


            sRutaDestino = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); 
            sRutaDestino += @"\DOCUMENTOS_NADRO\SURTIDOS\"; 
            
            if (!Directory.Exists(sRutaDestino))
            {
                Directory.CreateDirectory(sRutaDestino);
            }

            Error = new clsGrabarError(General.DatosConexion, GnDll_SII_INadro.DatosApp, "Dll_SII_INadro.GenerarArchivos.Surtidos"); 
        }

        ~Surtidos()
        { 
        }
        #endregion Constructor de Clase

        #region Propiedades 
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

        public bool GenerarHistorico
        {
            get { return bGenerarHistorico; }
            set { bGenerarHistorico = value; }
        }

        public string RutaDestinoReportes
        {
            get { return sRutaDestino; }
            set
            {
                sRutaDestino = value;
                if (sRutaDestino != "")
                {
                    sRutaDestino += @"\SURTIDOS\";
                    CrearDirectorio(sRutaDestino);
                }
            }
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
            General.msjUser("Archivos de surtido generados satisfactoriamente."); 
        }

        public bool GenerarSurtidos(string IdFarmacia, string Cliente, DateTime FechaInicial, DateTime FechaFinal)
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
                bRegresa = GenerarSurtidos(IdFarmacia, Cliente, sFecha);

                dtpFecha = dtpFecha.AddDays(1); 
            }

            return bRegresa; 
        }

        private bool GenerarSurtidos(string IdFarmacia, string Cliente, string FechaAProcesar)
        {
            bool bRegresa = false;
            int iTipoDeProceso = !bGenerarHistorico ? 1 : 0;

            string sSql = string.Format("Exec spp_INT_ND_GenerarSurtidos " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @CodigoCliente = '{3}', " + 
                " @FechaInicial = '{4}', @FechaFinal = '{5}', @GUID = '{6}', @TipoDeProceso = '{7}', @MostrarResultado = '{8}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, IdFarmacia, Cliente, FechaAProcesar, FechaAProcesar, sGUID, iTipoDeProceso, 1); 

            leer = new clsLeer(ref cnn); 
            leerInf = new clsLeer();

            if (!cnn.Abrir())
            {
                ////General.msjErrorAlAbriConexion(); 
            }
            else
            {
                ////cnn.IniciarTransaccion();
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "GenerarSurtidos()"); 
                }
                else
                {
                    leerInf.DataTableClase = leer.Tabla(1);  
                    bRegresa = GenerarDocumento(Cliente); 
                }


                if (!bRegresa)
                {
                    ////General.msjError("Ocurrió un error al generar el surtido");
                    ////cnn.DeshacerTransaccion(); 
                }
                else
                {
                    ////cnn.CompletarTransaccion();
                    ////General.msjUser("Archivo de pedido generado satisfactoriamente.");
                }

                cnn.Cerrar(); 
            }
            return bRegresa; 
        } 
        #endregion Funciones y Procedimientos Publicos 

        #region Funciones y Procedimientos Privados 
        private bool GenerarDocumento(string Cliente)
        {
            bool bRegresa = false;
            string sFileName = "";
            string sCadena = "";

            string sCodigoCliente = "";
            string sModulo = ""; 
            string sTicket = ""; 
            string sOrigen = "";
            string sFolio = ""; 
            string sFechaDeSurtido = "";
            string sFechaDeDocumento = ""; 
            string sTipoDeSeguro = "";
            string sDiagnostico = "";
            string sNumeroDePoliza = "";
            string sNombreAfiliado = ""; 
            string sCedulaMedico = "";
            string sNombreMedico = ""; 
            string sArea = "";
            string sIdHospitalDestino = "";
            string sModuloDestino = ""; 
            string sMotivo = "";
            string sIdSucursalDestino = "";
            string sMotivo2 = "";
            string sEstatus = "";  
            string sClaveSSA_ND = "";
            string sDescripcionClave = "";
            string sDescripcionClaveComercial = "";
            string sCodigoEAN = ""; 
            string sCantidadPedida = "";
            string sCantidadSurtida = "";
            string sLote = "";
            string sCaducidad = "";
            string sEstatusArticulo = "";


            if (!leerInf.Leer())
            {
                bRegresa = true;
                Error.GrabarError(string.Format("No se encontro información de surtido del cliente : {0}", Cliente), "GenerarDocumento()"); 
            }
            else 
            {
                try
                {
                    ////sRutaDestino += leerInf.Campo("FechaGeneracion"); 
                    leerInf.RegistroActual = 1; 
                    sFileName = sRutaDestino + @"\" + string.Format("{0}{1}{2}.txt", sPrefijo, leerInf.Campo("CodigoCliente"), 
                        Fg.Right(leerInf.Campo("FechaGeneracion"), 6));

                    StreamWriter fileOut = new StreamWriter(sFileName);

                    while (leerInf.Leer())
                    {

                        sCodigoCliente = leerInf.Campo("CodigoCliente");
                        sModulo = leerInf.Campo("Modulo");
                        sTicket = leerInf.Campo("Ticket");
                        sOrigen = leerInf.Campo("Origen");
                        sFolio = FormatoCampos.Formatear_QuitarCaracteres(leerInf.Campo("Folio"));
                        sFechaDeSurtido = leerInf.Campo("FechaDeSurtido");
                        sFechaDeDocumento = leerInf.Campo("FechaDeDocumento");
                        sTipoDeSeguro = leerInf.Campo("TipoDeSeguro");

                        sDiagnostico = FormatoCampos.Formatear_QuitarCaracteres(leerInf.Campo("Diagnostico"));
                        sNumeroDePoliza = FormatoCampos.Formatear_QuitarCaracteres(leerInf.Campo("NumeroDePoliza"));
                        sNombreAfiliado = FormatoCampos.Formatear_QuitarCaracteres(leerInf.Campo("NombreAfiliado")); 
                        sCedulaMedico = FormatoCampos.Formatear_QuitarCaracteres(leerInf.Campo("CedulaMedico"));
                        sNombreMedico = FormatoCampos.Formatear_QuitarCaracteres(leerInf.Campo("NombreMedico"));
                        sArea = FormatoCampos.Formatear_QuitarCaracteres(leerInf.Campo("Area")); 
                        sIdHospitalDestino = leerInf.Campo("IdHospitalDestino");
                        sModuloDestino = leerInf.Campo("IdModuloDestino");                        
                        sMotivo = FormatoCampos.Formatear_QuitarCaracteres(leerInf.Campo("Motivo"));
                        sIdSucursalDestino = leerInf.Campo("IdSucursalDestino");
                        sMotivo2 = FormatoCampos.Formatear_QuitarCaracteres(leerInf.Campo("Motivo2"));
                        sEstatus = leerInf.Campo("Estatus"); 

                        sClaveSSA_ND = leerInf.Campo("ClaveSSA_ND");
                        sDescripcionClave = leerInf.Campo("DescripcionClave");
                        sDescripcionClaveComercial = leerInf.Campo("DescripcionComercial");
                        sCodigoEAN = leerInf.Campo("CodigoEAN");
                        sCantidadPedida = leerInf.Campo("CantidadPedida");
                        sCantidadSurtida = leerInf.Campo("CantidadSurtida");
                        sLote = FormatoCampos.Formatear_QuitarAsterisco(leerInf.Campo("ClaveLote"));
                        sCaducidad = leerInf.Campo("Caducidad");
                        sEstatusArticulo = leerInf.Campo("EstatusArticulo");




                        sCodigoCliente = FormatoCampos.Formato_Digitos_Izquierda(sCodigoCliente, 7, "0");
                        sModulo = FormatoCampos.Formato_Digitos_Izquierda(sModulo, 2, "0");
                        sTicket = FormatoCampos.Formato_Digitos_Izquierda(sTicket, 12, "0");
                        ////sFolio = sFolio;  //// Este dato se manda vacio 

                        sFechaDeSurtido = Fg.Right(sFechaDeSurtido, 8);
                        sFechaDeDocumento = Fg.Right(sFechaDeDocumento, 8);

                        sIdHospitalDestino = FormatoCampos.Formato_Digitos_Izquierda(sIdHospitalDestino, 7, "0");
                        sModuloDestino = FormatoCampos.Formato_Digitos_Izquierda(sModuloDestino, 2, "0");

                        sIdSucursalDestino = FormatoCampos.Formato_Digitos_Izquierda(sIdSucursalDestino, 2, "0"); 
                        sEstatus = FormatoCampos.Formato_Caracter_Derecha(sEstatus, 1, "0"); 

                        sClaveSSA_ND = FormatoCampos.Formato_Caracter_Derecha(sClaveSSA_ND, 20, " ");
                        sDescripcionClave = FormatoCampos.Formato_Caracter_Derecha(sDescripcionClave, 1500, " ");
                        sDescripcionClaveComercial = FormatoCampos.Formato_Caracter_Derecha(sDescripcionClaveComercial, 35, " ");
                        sCodigoEAN = FormatoCampos.Formato_Digitos_Izquierda(sCodigoEAN, 15, "0");
                        sCantidadPedida = FormatoCampos.Formato_Digitos_Izquierda(sCantidadPedida, 7, "0");
                        sCantidadSurtida = FormatoCampos.Formato_Digitos_Izquierda(sCantidadSurtida, 7, "0");
                        sLote = FormatoCampos.Formato_Caracter_Derecha(sLote, 20, " ");
                        sCaducidad = FormatoCampos.Formato_Caracter_Derecha(sCaducidad, 8, "0");
                        sEstatusArticulo = FormatoCampos.Formato_Caracter_Derecha(sEstatusArticulo, 1, "0"); 

                        sCadena = string.Format
                            (
                                "{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}|{15}|{16}|{17}|{18}|{19}|{20}|{21}|{22}|{23}|{24}|{25}|{26}|{27}|{28}|", 
                                sCodigoCliente, sModulo, sTicket, sOrigen, sFolio, 
                                sFechaDeSurtido, sFechaDeDocumento, sTipoDeSeguro, sDiagnostico, sNumeroDePoliza, 
                                sNombreAfiliado, sCedulaMedico, sNombreMedico, sArea, sIdHospitalDestino, 
                                sModuloDestino, sMotivo, sIdSucursalDestino, sMotivo2, sEstatus, 
                                sClaveSSA_ND, sDescripcionClave, sDescripcionClaveComercial, sCodigoEAN, sCantidadPedida, 
                                sCantidadSurtida, sLote, sCaducidad, sEstatusArticulo 
                            );
                        fileOut.WriteLine(sCadena);

                    }
                    fileOut.Close(); 
                    bRegresa = true; 
                } 
                catch { } 
            }

            return bRegresa; 
        }
        #endregion Funciones y Procedimientos Privados
    }
}
