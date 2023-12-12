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
    public class Pedidos
    {
        #region Declaracion de Variables 
        string sPrefijo = "PV";

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
        public Pedidos(int EsDeSurtimiento)
        {
            iEsDeSurtimiento = EsDeSurtimiento; 
            dtMarcaTiempo = General.FechaSistema; 
            sMarcaTiempo = string.Format("", dtMarcaTiempo.Year, dtMarcaTiempo.Month, dtMarcaTiempo.Day);

            sRutaDestino = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            sRutaDestino += @"\DOCUMENTOS_NADRO\PEDIDOS_GENERADOS\";

            Error = new clsGrabarError(General.DatosConexion, GnDll_SII_INadro.DatosApp, "Dll_SII_INadro.GenerarArchivos.Pedidos"); 
        }

        ~Pedidos()
        { 
        }
        #endregion Constructor de Clase

        #region Funciones y Procedimientos Publicos 
        public void MsjFinalizado()
        {
            General.msjUser("Archivos de Pedidos generados satisfactoriamente.");
        }

        public bool GenerarPedidos()
        {
            bool bRegresa = false;

            bRegresa = GenerarPedidos(1, 1); 

            return bRegresa;
        }

        public bool GenerarPedidos(int EsDeSurtimiento, int TipoDeCliente)
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_INT_ND_ListadoDeClientes " +
                " @IdEstado = '{0}', @EsDeSurtimiento = '{1}', @TipoDeCliente = '{2}' ", DtGeneral.EstadoConectado, EsDeSurtimiento, TipoDeCliente);

            leerExec = new clsLeer(ref cnn); 
            if (!leerExec.Exec(sSql))
            {
                Error.GrabarError(leerExec, "GenerarPedidos()");
                General.msjError("Ocurrió un error al obtener el listado de clientes.");
            }
            else
            {
                if (!leerExec.Leer())
                {
                    General.msjUser("No se encontrarón clientes para la generacion de pedidos.");
                }
                else
                {
                    leerExec.RegistroActual = 1;
                    while (leerExec.Leer())
                    {
                        bRegresa = GenerarPedidos(leerExec.Campo("Código Cliente"));  
                    }

                    ////General.msjUser("Archivos de existencia generados satisfactoriamente."); 
                }
            }

            return bRegresa;
        } 

        public bool GenerarPedidos(string Cliente)
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_INT_ND_ListadoUnidadesPedidos " + 
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @CodigoCliente = '{3}', " + 
                " @EsDeSurtimiento = '{4}', @GenerarPedido = '{5}', @IdPersonal = '{6}'", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,  
                Cliente, iEsDeSurtimiento, 1, DtGeneral.IdPersonal); 

            leer = new clsLeer(ref cnn); 
            leerInf = new clsLeer();

            if (!cnn.Abrir())
            {
                ////General.msjErrorAlAbriConexion(); 
            }
            else
            {
                cnn.IniciarTransaccion();
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "GenerarPedidos()"); 
                }
                else
                {
                    leerInf.DataTableClase = leer.Tabla(2); 
                    bRegresa = GenerarDocumento(); 
                }


                if (!bRegresa)
                {
                    cnn.DeshacerTransaccion(); 
                    ////General.msjError("Ocurrió un error al generar el Pedido"); 
                }
                else
                {
                    cnn.CompletarTransaccion();
                    ////General.msjUser("Archivo de pedido generado satisfactoriamente.");
                }

                cnn.Cerrar(); 
            }
            return bRegresa; 
        } 
        #endregion Funciones y Procedimientos Publicos 

        #region Funciones y Procedimientos Privados 
        private bool GenerarDocumento()
        {
            bool bRegresa = false;
            string sFileName = "";
            string sCadena = "";
            string sCodigoCliente = "";
            string sClaveSSA_ND = "";
            string sCantidad = "";  

            if (leerInf.Leer())
            {
                try
                {
                    //sRutaDestino += @"\DOCUMENTOS_NADRO\PEDIDOS_GENERADOS\";
                    if (!Directory.Exists(sRutaDestino))
                    {
                        Directory.CreateDirectory(sRutaDestino); 
                    }


                    ////sRutaDestino += leerInf.Campo("FechaGeneracion"); 
                    sFileName = sRutaDestino + @"\" + string.Format("{0}{1}{2}.txt", 
                        sPrefijo, leerInf.Campo("CodigoCliente"), Fg.Right(leerInf.Campo("FechaGeneracion"), 6));
                    leerInf.RegistroActual = 1;

                    StreamWriter fileOut = new StreamWriter(sFileName);

                    while (leerInf.Leer())
                    {
                        sCodigoCliente = leerInf.Campo("CodigoCliente");
                        sClaveSSA_ND = leerInf.Campo("ClaveSSA_ND");
                        sCantidad = leerInf.Campo("CantidadAsignada");

                        sCodigoCliente = FormatoCampos.Formato_Digitos_Izquierda(sCodigoCliente, 7, "0");
                        sClaveSSA_ND = FormatoCampos.Formato_Caracter_Derecha(sClaveSSA_ND, 20, " ");
                        sCantidad = FormatoCampos.Formato_Digitos_Izquierda(sCantidad, 7, "0"); 

                        sCadena = string.Format
                            (
                                "{0}{1}{2}{3}", sCodigoCliente, sClaveSSA_ND, FormatoCampos.Formato_Digitos_Izquierda("", 8, "0"), sCantidad 
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
