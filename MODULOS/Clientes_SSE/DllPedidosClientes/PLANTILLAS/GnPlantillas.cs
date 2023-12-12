using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Resources;
using System.IO;

using System.Threading;
using System.Security.Permissions;
using System.Security.Policy;

using Microsoft.VisualBasic.FileIO; 

namespace DllPedidosClientes
{
    public enum ListaPlantillas
    {
        SurmientoClavesDispensada = 1, 
        NivelDeAbasto = 2, 
        ClavesNegadas_28 = 3, 
        ClavesNegadas_29 = 4, 
        ClavesNegadas_30 = 5, 
        ClavesNegadas_31 = 6, 

        EdoJuris_ClavesNegadas_28 = 7, 
        EdoJuris_ClavesNegadas_29 = 8, 
        EdoJuris_ClavesNegadas_30 = 9, 
        EdoJuris_ClavesNegadas_31 = 10,

        EdoJuris_Proximos_Caducar = 11,
        EdoJuris_Dispensacion = 12,
        EdoJurisUnidad_Dispensacion = 13,
        EdoJuris_TBC_Surtimiento = 14, 
        EdoJuris_TBC_Surtimiento_NoCauses = 15,
 
        EdoMovtos_Entradas = 16,
        EdoMovtos_Salidas = 17,
        EdoMovtos_Transferencias = 18, 
        // CTE_REG_EdoJuris_Surtimiento 

        ProductosMovimientos_Secretaria = 19,
        CuadrosBasicos = 20,
        Existencia_Por_ClaveSSA = 21,
        CuadrosBasicos_Farmacias = 22,
        Salidas_Antibioticos_Controlados = 23,
        Salidas_Medicos_Diagnosticos = 24,
        Salidas_Costos_Programas_Atencion = 25,
        Salidas_Costos_Programas_Atencion_Concentrado_Farmacia = 26, 
        Cortes_Diarios_Farmacias = 27,  

        EdoJuris_Dispensacion_Concentrado = 28,
        Kardex_Antibioticos_Controlados_Farmacia = 29
    }

    internal static class GnPlantillas
    {
        #region Declaracion de Variables 
        static byte[] byFile; 
        static string sPlantillaGenerada = "";
        static string sRutaPlantillas = Application.StartupPath + @"\\PLANTILLAS\"; 
        #endregion Declaracion de Variables

        #region Propiedades Publicas 
        public static string Documento
        {
            get { return sPlantillaGenerada; } 
        }

        public static string RutaDePlantillas
        {
            get { return sRutaPlantillas; }
            set { sRutaPlantillas = value; }
        }

        #endregion Declaracion de Variables

        #region Funciones y Procedimientos Publicos
        private static void PrepararEscritura(string NombreArchivo)
        {
            try
            {
                FileInfo f = new FileInfo(NombreArchivo);

                f.Attributes = FileAttributes.Normal;

                f = null;
            }
            catch (Exception ex)
            {
            }
        }

        public static bool GenerarPlantilla(ListaPlantillas Plantilla, string Nombre)
        {
            bool bRegresa = false; 

            string Archivo = Path.Combine(sRutaPlantillas, Nombre);
            sPlantillaGenerada = ""; 

            try 
            {
                if (!Directory.Exists(sRutaPlantillas))
                {
                    Directory.CreateDirectory(sRutaPlantillas); 
                }

                PrepararEscritura(Archivo); 
                if (File.Exists(Archivo))
                {
                    File.Delete(Archivo); 
                }

                if (!File.Exists(Archivo))
                {
                    // byte[] byFile = SC_CFD.Properties.Resources.diCrPKI;
                    byFile = GetPlantilla(Plantilla);  
                    Microsoft.VisualBasic.FileIO.FileSystem.WriteAllBytes(Archivo, byFile, false);
                    bRegresa = true;
                    sPlantillaGenerada = Archivo; 
                }

                //FileInfo f = new FileInfo(sFile);
                //f.Attributes = FileAttributes.Hidden; 
            }
            catch { }

            return bRegresa;
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private static string NombrePlantilla(ListaPlantillas Plantilla)
        {
            string sRegresa = "";

            switch (Plantilla)
            {
                #region Unidad 
                case ListaPlantillas.SurmientoClavesDispensada:
                    sRegresa = "CTE_REG_Surtimiento_Claves";
                    break;

                case ListaPlantillas.NivelDeAbasto:
                    sRegresa = "CTE_REG_Nivel_De_Abasto";
                    break;

                case ListaPlantillas.ClavesNegadas_28:
                    sRegresa = "CTE_REG_Claves_Negadas_28";
                    break;

                case ListaPlantillas.ClavesNegadas_29:
                    sRegresa = "CTE_REG_Claves_Negadas_29";
                    break;

                case ListaPlantillas.ClavesNegadas_30:
                    sRegresa = "CTE_REG_Claves_Negadas_30";
                    break;

                case ListaPlantillas.ClavesNegadas_31:
                    sRegresa = "CTE_REG_Claves_Negadas_31";
                    break;

                case ListaPlantillas.Kardex_Antibioticos_Controlados_Farmacia:
                    sRegresa = "Kardex_Antibioticos_Controlados_Farmacia";
                    break;
                #endregion Unidad

                #region Regionales
                case ListaPlantillas.EdoJuris_ClavesNegadas_28:
                    sRegresa = "CTE_REG_EdoJuris_Claves_Negadas_28";
                    break;

                case ListaPlantillas.EdoJuris_ClavesNegadas_29:
                    sRegresa = "CTE_REG_EdoJuris_Claves_Negadas_29";
                    break;

                case ListaPlantillas.EdoJuris_ClavesNegadas_30:
                    sRegresa = "CTE_REG_EdoJuris_Claves_Negadas_30";
                    break;

                case ListaPlantillas.EdoJuris_ClavesNegadas_31:
                    sRegresa = "CTE_REG_EdoJuris_Claves_Negadas_31";
                    break;

                case ListaPlantillas.EdoJuris_Proximos_Caducar:
                    sRegresa = "CTE_REG_EdoJuris_Proximos_Caducar";
                    break;

                case ListaPlantillas.EdoJuris_Dispensacion:
                    sRegresa = "CTE_REG_EdoJuris_Dispensacion";
                    break;

                case ListaPlantillas.EdoJuris_Dispensacion_Concentrado:
                    sRegresa = "CTE_REG_EdoJuris_Dispensacion_Concentrado";
                    break;

                case ListaPlantillas.EdoJurisUnidad_Dispensacion:
                    sRegresa = "CTE_REG_EdoJurisUnidad_Dispensacion";
                    break;

                case ListaPlantillas.EdoJuris_TBC_Surtimiento:
                    sRegresa = "CTE_REG_EdoJuris_TBC_Surtimiento";
                    break;

                case ListaPlantillas.EdoJuris_TBC_Surtimiento_NoCauses:
                    sRegresa = "CTE_REG_EdoJuris_TBC_Surtimiento_NoCauses";
                    break;

                case ListaPlantillas.EdoMovtos_Entradas:
                    sRegresa = "CTE_REG_EdoMovtos_Entradas";
                    break;

                case ListaPlantillas.EdoMovtos_Salidas:
                    sRegresa = "CTE_REG_EdoMovtos_Salidas";
                    break;

                case ListaPlantillas.EdoMovtos_Transferencias:
                    sRegresa = "CTE_REG_EdoMovtos_Transferencias";
                    break;

                case ListaPlantillas.ProductosMovimientos_Secretaria:
                    sRegresa = "CTE_REG_Productos_Movimientos_Secretaria";
                    break;

                case ListaPlantillas.CuadrosBasicos:
                    sRegresa = "CTE_REG_Cuadros_Basicos";
                    break;

                case ListaPlantillas.Existencia_Por_ClaveSSA:
                    sRegresa = "CTE_REG_Existencia_Claves";
                    break;

                case ListaPlantillas.CuadrosBasicos_Farmacias:
                    sRegresa = "CTE_REG_Cuadros_Basicos_Farmacias";
                    break;

                case ListaPlantillas.Salidas_Antibioticos_Controlados:
                    sRegresa = "CTE_REG_Salidas_Antibioticos_Controlados";
                    break;

                case ListaPlantillas.Salidas_Medicos_Diagnosticos:
                    sRegresa = "CTE_REG_Salidas_Medicos_Diagnosticos";
                    break;

                case ListaPlantillas.Salidas_Costos_Programas_Atencion:
                    sRegresa = "CTE_REG_Salidas_Costos_Programas_Atencion";
                    break;

                case ListaPlantillas.Salidas_Costos_Programas_Atencion_Concentrado_Farmacia:
                    sRegresa = "CTE_REG_Salidas_Costos_Programas_Atencion_ConcentradoFarmacia";
                    break; 

                case ListaPlantillas.Cortes_Diarios_Farmacias:
                    sRegresa = "CTE_REG_CortesDiarios_Farmacias";
                    break; 

                #endregion Regionales

                default:
                    break;
            } 

            return sRegresa + ".xls"; 
        }

        private static byte[] GetPlantilla(ListaPlantillas Plantilla)
        {
            byte[] byPlantilla = new byte[1];

            switch (Plantilla)
            {
                #region Unidad 
                case ListaPlantillas.SurmientoClavesDispensada:
                    byPlantilla = DllPedidosClientes.Properties.Resources.CTE_REG_Surtimiento_Claves; 
                    break;

                case ListaPlantillas.NivelDeAbasto:
                    byPlantilla = DllPedidosClientes.Properties.Resources.CTE_REG_Nivel_De_Abasto;
                    break; 

                case ListaPlantillas.ClavesNegadas_28:
                    byPlantilla = DllPedidosClientes.Properties.Resources.CTE_REG_Claves_Negadas_28;
                    break;

                case ListaPlantillas.ClavesNegadas_29:
                    byPlantilla = DllPedidosClientes.Properties.Resources.CTE_REG_Claves_Negadas_29;
                    break;

                case ListaPlantillas.ClavesNegadas_30:
                    byPlantilla = DllPedidosClientes.Properties.Resources.CTE_REG_Claves_Negadas_30;
                    break;

                case ListaPlantillas.ClavesNegadas_31:
                    byPlantilla = DllPedidosClientes.Properties.Resources.CTE_REG_Claves_Negadas_31;
                    break;

                case ListaPlantillas.Kardex_Antibioticos_Controlados_Farmacia:
                    byPlantilla = DllPedidosClientes.Properties.Resources.Kardex_Antibioticos_Controlados_Farmacia;
                    break;
                #endregion Unidad

                #region Regionales
                case ListaPlantillas.EdoJuris_ClavesNegadas_28:
                    byPlantilla = DllPedidosClientes.Properties.Resources.CTE_REG_EdoJuris_Claves_Negadas_28;
                    break;

                case ListaPlantillas.EdoJuris_ClavesNegadas_29:
                    byPlantilla = DllPedidosClientes.Properties.Resources.CTE_REG_EdoJuris_Claves_Negadas_29;
                    break;

                case ListaPlantillas.EdoJuris_ClavesNegadas_30:
                    byPlantilla = DllPedidosClientes.Properties.Resources.CTE_REG_EdoJuris_Claves_Negadas_30;
                    break;

                case ListaPlantillas.EdoJuris_ClavesNegadas_31:
                    byPlantilla = DllPedidosClientes.Properties.Resources.CTE_REG_EdoJuris_Claves_Negadas_31;
                    break;

                case ListaPlantillas.EdoJuris_Proximos_Caducar:
                    byPlantilla = DllPedidosClientes.Properties.Resources.CTE_REG_EdoJuris_Proximos_Caducar;
                    break;

                case ListaPlantillas.EdoJuris_Dispensacion:
                    byPlantilla = DllPedidosClientes.Properties.Resources.CTE_REG_EdoJuris_Dispensacion;
                    break;

                case ListaPlantillas.EdoJuris_Dispensacion_Concentrado:
                    byPlantilla = DllPedidosClientes.Properties.Resources.CTE_REG_EdoJuris_Dispensacion_Concentrado;
                    break;

                case ListaPlantillas.EdoJurisUnidad_Dispensacion:
                    byPlantilla = DllPedidosClientes.Properties.Resources.CTE_REG_EdoJurisUnidad_Dispensacion;
                    break;

                case ListaPlantillas.EdoJuris_TBC_Surtimiento:
                    byPlantilla = DllPedidosClientes.Properties.Resources.CTE_REG_EdoJuris_TBC_Surtimiento;
                    break;

                case ListaPlantillas.EdoJuris_TBC_Surtimiento_NoCauses:
                    byPlantilla = DllPedidosClientes.Properties.Resources.CTE_REG_EdoJuris_TBC_Surtimiento_NoCauses;
                    break;

                case ListaPlantillas.EdoMovtos_Entradas:
                    byPlantilla = DllPedidosClientes.Properties.Resources.CTE_REG_EdoMovtos_Entradas;
                    break;

                case ListaPlantillas.EdoMovtos_Salidas:
                    byPlantilla = DllPedidosClientes.Properties.Resources.CTE_REG_EdoMovtos_Salidas;
                    break;

                case ListaPlantillas.EdoMovtos_Transferencias:
                    byPlantilla = DllPedidosClientes.Properties.Resources.CTE_REG_EdoMovtos_Transferencias;
                    break;

                case ListaPlantillas.ProductosMovimientos_Secretaria:
                    byPlantilla = DllPedidosClientes.Properties.Resources.CTE_REG_Productos_Movimientos_Secretaria;
                    break;

                case ListaPlantillas.CuadrosBasicos:
                    byPlantilla = DllPedidosClientes.Properties.Resources.CTE_REG_Cuadros_Basicos;
                    break;

                case ListaPlantillas.Existencia_Por_ClaveSSA:
                    byPlantilla = DllPedidosClientes.Properties.Resources.CTE_REG_Existencia_Claves;
                    break;

                case ListaPlantillas.CuadrosBasicos_Farmacias:
                    byPlantilla = DllPedidosClientes.Properties.Resources.CTE_REG_Cuadros_Basicos_Farmacias;
                    break;

                case ListaPlantillas.Salidas_Antibioticos_Controlados:
                    byPlantilla = DllPedidosClientes.Properties.Resources.CTE_REG_Salidas_Antibioticos_Controlados;
                    break;

                case ListaPlantillas.Salidas_Medicos_Diagnosticos:
                    byPlantilla = DllPedidosClientes.Properties.Resources.CTE_REG_Salidas_Medicos_Diagnosticos;
                    break;

                case ListaPlantillas.Salidas_Costos_Programas_Atencion:
                    byPlantilla = DllPedidosClientes.Properties.Resources.CTE_REG_Salidas_Costos_Programas_Atencion;
                    break;

                case ListaPlantillas.Salidas_Costos_Programas_Atencion_Concentrado_Farmacia:
                    byPlantilla = DllPedidosClientes.Properties.Resources.CTE_REG_Salidas_Costos_Programas_Atencion_ConcentradoFarmacia;
                    break;

                case ListaPlantillas.Cortes_Diarios_Farmacias: 
                    byPlantilla = DllPedidosClientes.Properties.Resources.CTE_REG_CortesDiarios_Farmacias;
                    break;

                #endregion Regionales

                default:
                    break; 
            } 

            return byPlantilla;
        }
        #endregion Funciones y Procedimientos Privados
    }
}
