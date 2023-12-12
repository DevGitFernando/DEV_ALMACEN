using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Data;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Diagnostics;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Usuarios_y_Permisos;

using DllFarmaciaSoft;
using Dll_IRE_AMPM.wsClases;

namespace Dll_IRE_AMPM.wsClases
{
    public class ConsultaProductos
    {
        clsDatosConexion datosDeConexion; 
        clsConexionSQL cnn;
        clsLeer leer;
        clsGrabarError Error;
        basGenerales Fg = new basGenerales(); 

        string sFolioRegistro = ""; 
        string sMensajesError = "";

        public ConsultaProductos(clsDatosConexion DatosConexion)
        {
            datosDeConexion = DatosConexion;
            cnn = new clsConexionSQL(datosDeConexion);
            leer = new clsLeer(ref cnn);

            Error = new clsGrabarError(datosDeConexion, Dll_IRE_AMPM.GnDll_SII_AMPM.DatosApp, "RecetaElectronica"); 
        }

        public string Consultar(string Referencia_IdClinica, string IdFarmacia, int TipoDeBusqueda, string CriterioBusqueda)
        {
            ResponseGeneral respuesta = new ResponseGeneral(); 
            int iProducto = 0;
            string sRegresa = ""; 
            string sListaDeProductos = "";
            string sSql = "";

            sSql = string.Format("Exec spp_INT_AMPM__ConsultarExistenciasProductos  " + 
                " @Referencia_IdClinica = '{0}', @IdFarmacia = '{1}', @TipoDeConsulta = '{2}', @CriterioDeBusqueda = '{3}' ",
                Referencia_IdClinica, IdFarmacia, TipoDeBusqueda, CriterioBusqueda);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Consultar");
                respuesta.Estatus = 1;
                respuesta.Mensaje = "Error al obtener la información solicitada";
            }
            else
            {
                if (!leer.Leer())
                {
                    respuesta.Estatus = 2;
                    respuesta.Mensaje = "No se encontro información con los criteros solicitados";
                }
                else
                {
                    respuesta.Estatus = 0;
                    respuesta.Mensaje = "Se encontro información con los criteros solicitados";
                    leer.RegistroActual = 1;


                    leer.RenombrarTabla(1, "ExistenciaProductos");

                    respuesta.Add_Table(leer.DataTableClase); 
                }
            }

            return respuesta.GetResponse(); 
        }
    }
}
