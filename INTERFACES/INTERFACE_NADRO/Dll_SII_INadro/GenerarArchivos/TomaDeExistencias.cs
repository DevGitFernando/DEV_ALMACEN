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
    public class TomaDeExistencias
    {
        #region Declaracion de Variables 
        string sPrefijo = "TE";

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
        public TomaDeExistencias()
        {
            dtMarcaTiempo = General.FechaSistema; 
            sMarcaTiempo = string.Format("", dtMarcaTiempo.Year, dtMarcaTiempo.Month, dtMarcaTiempo.Day);


            sRutaDestino = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); 
            sRutaDestino += @"\DOCUMENTOS_NADRO\TOMA_DE_EXISTENCIAS\"; 
            
            if (!Directory.Exists(sRutaDestino))
            {
                Directory.CreateDirectory(sRutaDestino);
            }

            Error = new clsGrabarError(General.DatosConexion, GnDll_SII_INadro.DatosApp, "Dll_SII_INadro.GenerarArchivos.TomaDeExistencias"); 
        }

        ~TomaDeExistencias()
        { 
        }
        #endregion Constructor de Clase

        #region Funciones y Procedimientos Publicos 
        public void MsjFinalizado()
        {
            General.msjUser("Archivos de existencia generados satisfactoriamente.");
        }

        public bool GenerarTomaDeExistencias(DateTime FechaInicial, DateTime FechaFinal)
        {
            bool bRegresa = false;

            bRegresa = GenerarTomaDeExistencias(1, 1, FechaInicial, FechaFinal);

            return bRegresa;
        }

        public bool GenerarTomaDeExistencias(int EsDeSurtimiento, int TipoDeCliente, DateTime FechaInicial, DateTime FechaFinal)
        {
            bool bRegresa = false; 
            string sSql = string.Format("Exec spp_INT_ND_ListadoDeClientes " + 
                " @IdEstado = '{0}', @EsDeSurtimiento = '{1}', @TipoDeCliente = '{2}' ", DtGeneral.EstadoConectado, EsDeSurtimiento, TipoDeCliente);

            leerExec = new clsLeer(ref cnn);
            if (!leerExec.Exec(sSql))
            {
                Error.GrabarError(leerExec, "GenerarTomaDeExistencias()");
                General.msjError("Ocurrió un error al obtener el listado de clientes.");
            }
            else
            {
                if (!leerExec.Leer())
                {
                    General.msjUser("No se encontrarón clientes para la generacion de existencias.");
                }
                else
                {
                    leerExec.RegistroActual = 1;
                    while (leerExec.Leer())
                    {
                        bRegresa = GenerarTomaDeExistencias( leerExec.Campo("Código Cliente"), FechaInicial, FechaFinal); 
                    }

                    ////General.msjUser("Archivos de existencia generados satisfactoriamente."); 
                }
            }

            return bRegresa;
        }

        public bool GenerarTomaDeExistencias(string Cliente, DateTime FechaInicial, DateTime FechaFinal)
        {
            bool bRegresa = false;
            string sFecha = ""; //  General.FechaYMD(FechaAProcesar); 
            DateTime dtpFecha = FechaInicial;

            while (dtpFecha <= FechaFinal)
            {
                sFecha = General.FechaYMD(dtpFecha);

                bRegresa = GenerarTomaDeExistencias(Cliente, sFecha);

                dtpFecha = dtpFecha.AddDays(1);
            }

            return bRegresa;
        }

        public bool GenerarTomaDeExistencias(string Cliente, string FechaAProcesar)
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_INT_ND_GenerarTomaDeExistencias " + 
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @CodigoCliente = '{2}', @IdFarmacia = '{3}', @FechaDeProceso = '{4}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, Cliente, DtGeneral.FarmaciaConectada, FechaAProcesar); 

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
                    Error.GrabarError(leer, "GenerarTomaDeExistencias()"); 
                }
                else
                {
                    leerInf.DataTableClase = leer.Tabla(1);  
                    bRegresa = GenerarDocumento(Cliente); 
                }


                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al generar la toma de existencia");
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
            string sTipoDeToma = "";
            string sConfiguracion = "";
            string sFolioDeToma = ""; 

            string sClaveSSA_ND = "";
            string sDescripcionClave = "";
            string sDescripcionClaveComercial = "";
            string sCodigoEAN = ""; 
            string sCantidadTeorica = "";
            string sCantidadFisica = "";
            string sLote = "";
            string sCaducidad = "";

            string sUsuarioToma = "";
            string sUsuarioAutoriza = "";
            string sMotivoDiferencias = ""; 
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
                        sTipoDeToma = leerInf.Campo("TipoDeToma");
                        sConfiguracion = leerInf.Campo("ConfiguracionCiclica");
                        sFolioDeToma = leerInf.Campo("FolioDeToma");

                        sClaveSSA_ND = leerInf.Campo("ClaveSSA_ND");
                        sDescripcionClave = leerInf.Campo("DescripcionClave");
                        sDescripcionClaveComercial = leerInf.Campo("DescripcionComercial");
                        sCodigoEAN = leerInf.Campo("CodigoEAN");

                        sCantidadTeorica = leerInf.Campo("ExistenciaTeorica");
                        sCantidadFisica = leerInf.Campo("ExistenciaFisica");
                        sLote = FormatoCampos.Formatear_QuitarAsterisco(leerInf.Campo("ClaveLote"));
                        sCaducidad = Fg.Right(leerInf.Campo("Caducidad"), 8);

                        sUsuarioToma = leerInf.Campo("UsuarioToma");
                        sUsuarioAutoriza = leerInf.Campo("UsuarioAutoriza");
                        sMotivoDiferencias = leerInf.Campo("MotivoDiferencias");

                        sFechaModificacion = Fg.Right(leerInf.Campo("FechaGeneracion"), 8);


                        sCodigoCliente = FormatoCampos.Formato_Digitos_Izquierda(sCodigoCliente, 7, "0");
                        sModulo = FormatoCampos.Formato_Digitos_Izquierda(sModulo, 2, "0");

                        sTipoDeToma = FormatoCampos.Formato_Digitos_Izquierda(sTipoDeToma, 2, "0");
                        sConfiguracion = FormatoCampos.Formato_Digitos_Izquierda(sConfiguracion, 2, "0");
                        sFolioDeToma = FormatoCampos.Formato_Caracter_Derecha(sFolioDeToma, 20, " ");

                        sClaveSSA_ND = FormatoCampos.Formato_Caracter_Derecha(sClaveSSA_ND, 20, " ");
                        sDescripcionClave = FormatoCampos.Formato_Caracter_Derecha(sDescripcionClave, 1500, " ");
                        sDescripcionClaveComercial = FormatoCampos.Formato_Caracter_Derecha(sDescripcionClaveComercial, 35, " ");
                        sCodigoEAN = FormatoCampos.Formato_Digitos_Izquierda(sCodigoEAN, 15, "0");
                        sCantidadTeorica = FormatoCampos.Formato_Digitos_Izquierda(sCantidadTeorica, 7, "0");
                        sCantidadFisica = FormatoCampos.Formato_Digitos_Izquierda(sCantidadFisica, 7, "0");

                        sLote = FormatoCampos.Formato_Caracter_Derecha(sLote, 20, " ");
                        sCaducidad = FormatoCampos.Formato_Caracter_Derecha(sCaducidad, 8, "0");

                        sUsuarioToma = FormatoCampos.Formatear_QuitarCaracteres(sUsuarioToma);
                        sUsuarioAutoriza = FormatoCampos.Formatear_QuitarCaracteres(sUsuarioAutoriza);
                        sMotivoDiferencias = FormatoCampos.Formatear_QuitarCaracteres(sMotivoDiferencias);

                        sFechaModificacion = FormatoCampos.Formato_Caracter_Derecha(sFechaModificacion, 8, "0");


                        sCadena = string.Format
                            (
                                "{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}|{15}|{16}",
                                sCodigoCliente, sModulo, sTipoDeToma, sConfiguracion, sFolioDeToma, 
                                sClaveSSA_ND, sDescripcionClave, sDescripcionClaveComercial, sCodigoEAN, 
                                sCantidadTeorica, sCantidadFisica, sLote, sCaducidad, 
                                sUsuarioToma, sUsuarioAutoriza, sMotivoDiferencias, 
                                sFechaModificacion 
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
