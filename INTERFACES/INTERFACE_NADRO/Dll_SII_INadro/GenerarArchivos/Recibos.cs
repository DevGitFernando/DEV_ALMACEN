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
    public class Recibos
    {
        #region Declaracion de Variables
        string sPrefijo = "RE";

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
        public Recibos()
        {
            dtMarcaTiempo = General.FechaSistema;
            sMarcaTiempo = string.Format("", dtMarcaTiempo.Year, dtMarcaTiempo.Month, dtMarcaTiempo.Day);


            sRutaDestino = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            sRutaDestino += @"\DOCUMENTOS_NADRO\RECIBOS\";

            if (!Directory.Exists(sRutaDestino))
            {
                Directory.CreateDirectory(sRutaDestino);
            }

            Error = new clsGrabarError(General.DatosConexion, GnDll_SII_INadro.DatosApp, "Dll_SII_INadro.GenerarArchivos.Recibos");
        }

        ~Recibos()
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
                    sRutaDestino += @"\RECIBOS\";
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
            General.msjUser("Archivos de recibo generados satisfactoriamente.");
        }

        ////public bool GenerarRecibos(DateTime FechaInicial, DateTime FechaFinal)
        ////{
        ////    bool bRegresa = false;

        ////    bRegresa = GenerarRecibos(1, 1, FechaInicial, FechaFinal);

        ////    return bRegresa;
        ////}

        ////public bool GenerarRecibos(int EsDeSurtimiento, int TipoDeCliente, DateTime FechaInicial, DateTime FechaFinal)
        ////{
        ////    bool bRegresa = false;
        ////    string sSql = string.Format("Exec spp_INT_ND_ListadoDeClientes " +
        ////        " @IdEstado = '{0}', @EsDeSurtimiento = '{1}', @TipoDeCliente = '{2}' ",
        ////        DtGeneral.EstadoConectado, EsDeSurtimiento, TipoDeCliente);

        ////    leerExec = new clsLeer(ref cnn);
        ////    if (!leerExec.Exec(sSql))
        ////    {
        ////        Error.GrabarError(leerExec, "GenerarRecibos()");
        ////        General.msjError("Ocurrió un error al obtener el listado de clientes.");
        ////    }
        ////    else
        ////    {
        ////        if (!leerExec.Leer())
        ////        {
        ////            General.msjUser("No se encontrarón clientes para la generacion de recibos.");
        ////        }
        ////        else
        ////        {
        ////            leerExec.RegistroActual = 1;
        ////            while (leerExec.Leer())
        ////            {
        ////                bRegresa = GenerarRecibos(leerExec.Campo("Código Cliente"), FechaInicial, FechaFinal);
        ////            }
        ////        }
        ////    }

        ////    return bRegresa;
        ////}

        public bool GenerarRecibos(string IdFarmacia, string Cliente, DateTime FechaInicial, DateTime FechaFinal)
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
                bRegresa = GenerarRecibos(IdFarmacia, Cliente, sFecha);

                dtpFecha = dtpFecha.AddDays(1);
            }

            return bRegresa;
        }

        private bool GenerarRecibos(string IdFarmacia, string Cliente, string FechaAProcesar)
        {
            bool bRegresa = false;
            int iTipoDeProceso = !bGenerarHistorico ? 1 : 0;

            string sSql = string.Format("Exec spp_INT_ND_GenerarRecibos " +
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
                    Error.GrabarError(leer, "GenerarRecibos()");
                }
                else
                {
                    leerInf.DataTableClase = leer.Tabla(1);
                    bRegresa = GenerarDocumento(Cliente);
                }


                if (!bRegresa)
                {
                    ////General.msjError("Ocurrió un error al generar el recibo");
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

            string sIdHospitalOrigen = "";
            string sModuloOrigen = "";
            string sMotivo = "";
            string sNombreTercero = "";
            string sMotivo2 = "";
            string sEstatus = "";
            string sClaveSSA_ND = "";
            string sDescripcionClave = "";
            string sDescripcionClaveComercial = "";
            string sCodigoEAN = "";
            string sCodigoNadro = "";
            string sCantidadPedida = "";
            string sCantidadRecibida = "";
            string sLote = "";
            string sCaducidad = "";
            string sEstatusArticulo = "";


            if (!leerInf.Leer())
            {
                bRegresa = true;
                Error.GrabarError(string.Format("No se encontro información de recibo del cliente : {0}", Cliente), "GenerarDocumento()");
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
                        sFechaDeSurtido = leerInf.Campo("FechaDeRecibo");
                        sFechaDeDocumento = leerInf.Campo("FechaDeDocumento");
 
                        sIdHospitalOrigen = leerInf.Campo("IdHospitalOrigen");
                        sModuloOrigen = leerInf.Campo("IdModuloOrigen");
                        sMotivo = FormatoCampos.Formatear_QuitarCaracteres(leerInf.Campo("Motivo"));
                        sNombreTercero = FormatoCampos.Formatear_QuitarCaracteres(leerInf.Campo("NombreTercero"));
                        sMotivo2 = FormatoCampos.Formatear_QuitarCaracteres(leerInf.Campo("Motivo2"));
                        sEstatus = leerInf.Campo("Estatus");

                        sClaveSSA_ND = leerInf.Campo("ClaveSSA_ND");
                        sDescripcionClave = leerInf.Campo("DescripcionClave");
                        sDescripcionClaveComercial = leerInf.Campo("DescripcionComercial");
                        sCodigoEAN = leerInf.Campo("CodigoEAN");
                        sCodigoNadro = leerInf.Campo("CodigoNadro");
                        sCantidadPedida = leerInf.Campo("CantidadPedida");
                        sCantidadRecibida = leerInf.Campo("CantidadRecibida");
                        sLote = FormatoCampos.Formatear_QuitarAsterisco(leerInf.Campo("ClaveLote"));
                        sCaducidad = leerInf.Campo("Caducidad");
                        sEstatusArticulo = leerInf.Campo("EstatusArticulo");




                        sCodigoCliente = FormatoCampos.Formato_Digitos_Izquierda(sCodigoCliente, 7, "0");
                        sModulo = FormatoCampos.Formato_Digitos_Izquierda(sModulo, 2, "0");
                        sTicket = FormatoCampos.Formato_Digitos_Izquierda(sTicket, 12, "0");
                        ////sFolio = sFolio;  //// Este dato se manda vacio 

                        sFechaDeSurtido = Fg.Right(sFechaDeSurtido, 8);
                        sFechaDeDocumento = Fg.Right(sFechaDeDocumento, 8);

                        sIdHospitalOrigen = FormatoCampos.Formato_Digitos_Izquierda(sIdHospitalOrigen, 7, "0");
                        sModuloOrigen = FormatoCampos.Formato_Digitos_Izquierda(sModuloOrigen, 2, "0"); 
                        sEstatus = FormatoCampos.Formato_Caracter_Derecha(sEstatus, 1, "0");

                        sClaveSSA_ND = FormatoCampos.Formato_Caracter_Derecha(sClaveSSA_ND, 20, " ");
                        sDescripcionClave = FormatoCampos.Formato_Caracter_Derecha(sDescripcionClave, 1500, " ");
                        sDescripcionClaveComercial = FormatoCampos.Formato_Caracter_Derecha(sDescripcionClaveComercial, 35, " ");
                        sCodigoEAN = FormatoCampos.Formato_Digitos_Izquierda(sCodigoEAN, 15, "0");
                        sCodigoNadro = FormatoCampos.Formato_Digitos_Izquierda(sCodigoNadro, 7, "0");
                        sCantidadPedida = FormatoCampos.Formato_Digitos_Izquierda(sCantidadPedida, 7, "0");
                        sCantidadRecibida = FormatoCampos.Formato_Digitos_Izquierda(sCantidadRecibida, 7, "0");
                        sLote = FormatoCampos.Formato_Caracter_Derecha(sLote, 20, " ");
                        sCaducidad = FormatoCampos.Formato_Caracter_Derecha(sCaducidad, 8, "0");
                        sEstatusArticulo = FormatoCampos.Formato_Caracter_Derecha(sEstatusArticulo, 1, "0");

                        sCadena = string.Format
                            (
                                "{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}|{15}|{16}|{17}|{18}|{19}|{20}|{21}|{22}|",
                                sCodigoCliente, sModulo, sTicket, sOrigen, sFolio,
                                sFechaDeSurtido, sFechaDeDocumento, sIdHospitalOrigen, sModuloOrigen, sMotivo,
                                sNombreTercero, sMotivo2, sEstatus, sClaveSSA_ND, sDescripcionClave,
                                sDescripcionClaveComercial, sCodigoEAN, sCodigoNadro, sCantidadPedida, sCantidadRecibida, 
                                sLote, sCaducidad, sEstatusArticulo 
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
