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
using SC_SolutionsSystem.FTP;

using DllFarmaciaSoft;
using Dll_SII_IMediaccess; 

namespace Dll_SII_IMediaccess.PedidosMayoristas
{
    public class clsPedidosMayoristas
    {
        clsConexionSQL cnn = new clsConexionSQL();
        clsLeer leer;
        clsGrabarError Error;

        string sIdEstado = "";
        string sIdFarmacia = "";
        string sReferenciaFarmacia_Proveedor = ""; 

        bool bConfiguracionValida = false;
        string sIdProveedor = "";
        string sUrl_Envio = "";
        string sUsuario = ""; 
        string sPassword = "";

        clsFTP localFTP = new clsFTP(); 

        public clsPedidosMayoristas(string IdEstado, string IdFarmacia, string IdProveedor, string NombreClase)
        {
            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, GnDll_SII_IMediaccess.DatosApp, NombreClase);

            sIdEstado = IdEstado;
            sIdFarmacia = IdFarmacia;
            sIdProveedor = IdProveedor;
        }

        #region Funciones y Propiedades Publicas 
        public string IdEstado
        {
            get { return sIdEstado; }
            set { sIdEstado = value; }
        }

        public string IdFarmacia
        {
            get { return sIdFarmacia; }
            set { sIdFarmacia = value; }
        }

        public string IdProveedor
        {
            get { return sIdProveedor; }
            set { sIdProveedor = value; }
        }

        public string Url_Envio 
        {
            get { return sUrl_Envio; }
            set { sUrl_Envio = value; }
        }

        public string Usuario 
        {
            get { return sUsuario; }
            set { sUsuario = value; }
        }

        public string Password 
        {
            get { return sPassword; }
            set { sPassword = value; }
        }

        public clsFTP FTP
        {
            get { return localFTP; }
            set { localFTP = value; }
        }
        #endregion Funciones y Propiedades Publicas

        #region Funciones y Procedimientos Publicos 
        public DataSet InformacionDePedido(string FolioPedido)
        {
            DataSet dtsResultado = new DataSet("Resultado");
            string sSql = string.Format("Select * From INT_MA__PROV_Mayoristas (NoLock) Where IdProveedor = '{0}' ", IdProveedor);

            if (!leer.Exec(sSql))
            { 
            }

            dtsResultado = leer.DataSetClase; 

            return dtsResultado;
        }
        #endregion Funciones y Procedimientos Publicos 

        #region Funciones y Procedimientos Privados
        private void Obtener_Configuracion__Proveedor()
        {
            string sSql = string.Format("Select * From INT_MA__PROV_Mayoristas (NoLock) Where IdProveedor = '{0}' ", IdProveedor);

            bConfiguracionValida = false;
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Obtener_Configuracion__Proveedor()"); 
            }
            else
            {
                if (leer.Leer())
                {

                    bConfiguracionValida = true;
                    sReferenciaFarmacia_Proveedor = leer.Campo("CodigoCliente");
                    sUrl_Envio = leer.Campo("Url_Envio");
                    sUsuario = leer.Campo("Usuario");
                    sPassword = leer.Campo("Password");

                    localFTP = new clsFTP(sUrl_Envio, sUsuario, sPassword, false); 
                }
            }

            if ( bConfiguracionValida ) 
            {
                Obtener_Configuracion__Proveedor_Unidad(); 
            }
        }

        private void Obtener_Configuracion__Proveedor_Unidad()
        {
            string sSql = string.Format(
                " Select * " +
                " From INT_MA__PROV_Mayoristas__Farmacias (NoLock) " + 
                " Where IdProveedor = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' ", 
                IdProveedor, sIdEstado, sIdFarmacia);

            bConfiguracionValida = false;
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Obtener_Configuracion__Proveedor()");
            }
            else
            {
                if (leer.Leer())
                {

                    bConfiguracionValida = true;
                    sReferenciaFarmacia_Proveedor = leer.Campo("CodigoCliente");
                    sUrl_Envio = leer.Campo("Url_Envio");
                    sUsuario = leer.Campo("Usuario");
                    sPassword = leer.Campo("Password");
                }
            }

            if (bConfiguracionValida)
            {
            }
        }
        #endregion Funciones y Procedimientos Privados
    }
}
