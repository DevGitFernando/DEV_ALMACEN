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
    public class Existencias
    {
        #region Declaracion de Variables 
        string sPrefijo = "EX";
        string sGUID = "";
        Label lblFechaProcesando = null;
        bool bGenerarHistorico = false;

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
        #endregion Declaracion de Variables 

        #region Constructor de Clase  
        public Existencias()
        {
            dtMarcaTiempo = General.FechaSistema; 
            sMarcaTiempo = string.Format("", dtMarcaTiempo.Year, dtMarcaTiempo.Month, dtMarcaTiempo.Day);


            sRutaDestino = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); 
            sRutaDestino += @"\DOCUMENTOS_NADRO\EXISTENCIAS_GENERADAS\"; 
            
            ////if (!Directory.Exists(sRutaDestino))
            ////{
            ////    Directory.CreateDirectory(sRutaDestino);
            ////}

            Error = new clsGrabarError(General.DatosConexion, GnDll_SII_INadro.DatosApp, "Dll_SII_INadro.GenerarArchivos.Existencias"); 
        }

        ~Existencias()
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
                    sRutaDestino += @"\EXISTENCIAS_GENERADAS\";
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
            General.msjUser("Archivos de existencia generados satisfactoriamente.");
        }

        ////public bool GenerarExistencias(DateTime FechaInicial, DateTime FechaFinal)
        ////{
        ////    bool bRegresa = false;

        ////    bRegresa = GenerarExistencias(1, 1, FechaInicial, FechaFinal);

        ////    return bRegresa;
        ////}

        ////public bool GenerarExistencias(int EsDeSurtimiento, int TipoDeCliente, DateTime FechaInicial, DateTime FechaFinal)
        ////{
        ////    bool bRegresa = false; 
        ////    string sSql = string.Format("Exec spp_INT_ND_ListadoDeClientes " + 
        ////        " @IdEstado = '{0}', @EsDeSurtimiento = '{1}', @TipoDeCliente = '{2}' ", DtGeneral.EstadoConectado, EsDeSurtimiento, TipoDeCliente);

        ////    leerExec = new clsLeer(ref cnn);
        ////    if (!leerExec.Exec(sSql))
        ////    {
        ////        Error.GrabarError(leerExec, "GenerarExistencias()");
        ////        General.msjError("Ocurrió un error al obtener el listado de clientes.");
        ////    }
        ////    else
        ////    {
        ////        if (!leerExec.Leer())
        ////        {
        ////            General.msjUser("No se encontrarón clientes para la generacion de existencias.");
        ////        }
        ////        else
        ////        {
        ////            leerExec.RegistroActual = 1;
        ////            while (leerExec.Leer())
        ////            {
        ////                bRegresa = GenerarExistencias(leerExec.Campo("Código Cliente"), FechaInicial, FechaFinal); 
        ////            }

        ////            ////General.msjUser("Archivos de existencia generados satisfactoriamente."); 
        ////        }
        ////    }

        ////    return bRegresa;
        ////}

        public bool GenerarExistencias(string IdFarmacia, string Cliente, DateTime FechaInicial, DateTime FechaFinal)
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

                bRegresa = GenerarExistencias(IdFarmacia, Cliente, sFecha);
                dtpFecha = dtpFecha.AddDays(1);
            }

            return bRegresa; 
        }

        public bool GenerarExistencias(string IdFarmacia, string Cliente, string FechaAProcesar)
        {
            bool bRegresa = false;
            int iTipoDeProceso = !bGenerarHistorico ? 1 : 0;

            string sSql = string.Format("Exec spp_INT_ND_GenerarExistencias " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @CodigoCliente = '{3}', " + 
                " @FechaDeProceso = '{4}', @TipoDeProceso = '{5}', @MostrarResultado = '{6}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, IdFarmacia, Cliente, FechaAProcesar, iTipoDeProceso, sGUID); 

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
                    Error.GrabarError(leer, "GenerarExistencias()"); 
                }
                else
                {
                    leerInf.DataTableClase = leer.Tabla(1);  
                    bRegresa = GenerarDocumento(Cliente); 
                }


                if (!bRegresa)
                {
                    ////General.msjError("Ocurrió un error al generar la existencia");
                    ////cnn.DeshacerTransaccion(); 
                }
                else
                {
                    ////cnn.CompletarTransaccion();
                    ////General.msjUser("Archivo de pedido generado satisfactoriamente.");
                }

                //cnn.Cerrar(); 
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
            string sClaveSSA_ND = "";
            string sDescripcionClave = "";
            string sDescripcionClaveComercial = "";
            string sCodigoEAN = ""; 
            string sCantidad = "";
            string sLote = "";
            string sCaducidad = "";
            string sFechaModificacion = "";

            if (!leerInf.Leer())
            {
                bRegresa = true;
                Error.GrabarError(string.Format("No se encontro existencia del cliente : {0}", Cliente), "GenerarDocumento()"); 
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
                        sClaveSSA_ND = leerInf.Campo("ClaveSSA_ND");
                        sDescripcionClave = leerInf.Campo("DescripcionClave");
                        sDescripcionClaveComercial = leerInf.Campo("DescripcionComercial");
                        sCodigoEAN = leerInf.Campo("CodigoEAN");
                        sCantidad = leerInf.Campo("Existencia");
                        sLote = FormatoCampos.Formatear_QuitarAsterisco(leerInf.Campo("ClaveLote"));
                        sCaducidad = Fg.Right(leerInf.Campo("Caducidad"), 8);
                        sFechaModificacion = Fg.Right(leerInf.Campo("FechaGeneracion"), 8);


                        sCodigoCliente = FormatoCampos.Formato_Digitos_Izquierda(sCodigoCliente, 7, "0");
                        sModulo = FormatoCampos.Formato_Digitos_Izquierda(sModulo, 2, "0");
                        sClaveSSA_ND = FormatoCampos.Formato_Caracter_Derecha(sClaveSSA_ND, 20, " ");
                        sDescripcionClave = FormatoCampos.Formato_Caracter_Derecha(sDescripcionClave, 1500, " ");
                        sDescripcionClaveComercial = FormatoCampos.Formato_Caracter_Derecha(sDescripcionClaveComercial, 35, " ");
                        sCodigoEAN = FormatoCampos.Formato_Digitos_Izquierda(sCodigoEAN, 15, "0");
                        sCantidad = FormatoCampos.Formato_Digitos_Izquierda(sCantidad, 7, "0");
                        sLote = FormatoCampos.Formato_Caracter_Derecha(sLote, 20, " ");
                        sCaducidad = FormatoCampos.Formato_Caracter_Derecha(sCaducidad, 8, "0");
                        sFechaModificacion = FormatoCampos.Formato_Caracter_Derecha(sFechaModificacion, 8, "0");


                        sCadena = string.Format
                            (
                                "{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|", 
                                sCodigoCliente, sModulo, sClaveSSA_ND, sDescripcionClave, sDescripcionClaveComercial, sCodigoEAN, sCantidad, sLote, sCaducidad, sFechaModificacion 
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
