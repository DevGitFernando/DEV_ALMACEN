using System;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;

namespace DllPedidosClientes
{
    public class clsLeerWeb : clsLeer
    {
        #region Declaracion de variables
        // private clsConexion_Odbc Cnn;
        // private wsCnnClienteAdmin.wsCnnClientesAdmin conexion;
        private wsCnnClienteAdmin.wsCnnClientesAdmin conexion;
        // protected clsDatosConexion datosCnn = new clsDatosConexion();
        protected clsDatosCliente pDatosCliente;
        // protected string sArchivoIni = "";
        #endregion Declaracion de variables

        #region Constructor 
        public clsLeerWeb(string Url, string ArchivoIni, clsDatosCliente datosCliente): base()
        {
            conexion = new wsCnnClienteAdmin.wsCnnClientesAdmin();
            // this.datosCnn = DatosCnn;
            this.pDatosCliente = datosCliente;
            base.sArchivoIni = ArchivoIni;
            // base.Conexion.DatosConexion = DatosCnn;

            if (!ValidarURL(Url))
            {
                conexion = null;
            }
        }

        public clsLeerWeb(ref clsConexionSQL Cnn, string Url, string ArchivoIni, clsDatosCliente datosCliente)
            : base(ref Cnn)
        {
            conexion = new wsCnnClienteAdmin.wsCnnClientesAdmin();
            // this.datosCnn = DatosCnn;
            this.pDatosCliente = datosCliente;
            base.sArchivoIni = ArchivoIni;
            // base.Conexion.DatosConexion = DatosCnn;

            if (!ValidarURL(Url))
            {
                conexion = null;
            }
        }
        #endregion Constructor

        #region Propiedades
        public clsDatosCliente DatosCliente
        {
            get
            {
                return pDatosCliente;
            }
            set
            {
                pDatosCliente = value;
            }
        }

        public string UrlWebService
        {
            get
            {
                return base.sUrl;
            }
            set
            {
                ValidarURL(value);
            }
        }
        #endregion Propiedades

        public override bool Exec(string Cadena)
        {
            return Exec(NombreTabla, Cadena);
        }

        public override bool Exec(string Tabla, string Cadena)
        {
            bool bRegresa = false;
            //object objRecibir = null;

            try
            {
                base.bHuboError = false;
                base.sConsultaExec = Cadena;
                base.myException = new Exception("Sin error");

                conexion = new wsCnnClienteAdmin.wsCnnClientesAdmin();
                conexion.Url = base.sUrl;
                conexion.Timeout = 500000;

                base.dtsClase = conexion.ExecuteExt(pDatosCliente.DatosCliente(), base.sArchivoIni, Cadena);
                base.iRegistros = 0;
                base.iPosActualReg = 0;

                if (!SeEncontraronErrores(dtsClase))
                {
                    base.DataSetClase = dtsClase;
                    // base.RenombrarTabla(1, Tabla); 
                    bRegresa = true;
                }
                else
                {
                    try
                    {
                        myException = new Exception(this.Errores[0].Mensaje);
                    }
                    catch { }
                }
            }
            catch (Exception e1)
            {
                bRegresa = false;
                base.bHuboError = true;
                base.myException = e1;// (Exception)objRecibir;
                base.ObtenerErrores(e1);
                base.dtsClase = base.dtsListaErrores; 
            }
            return bRegresa;
        }        
    }
}
