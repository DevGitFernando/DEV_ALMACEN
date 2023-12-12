using System;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;

namespace DllFarmaciaSoft
{
    public class clsLeerWebExt : clsLeer
    {
        #region Declaracion de variables
        // private clsConexion_Odbc Cnn;
        private wsFarmaciaSoftGn.wsConexion conexion;

        private int iTimeOut = 250000; 

        // protected clsDatosConexion datosCnn = new clsDatosConexion();
        protected clsDatosCliente pDatosCliente;
        // protected string sArchivoIni = "";
        #endregion Declaracion de variables

        #region Constructor 
        public clsLeerWebExt(string Url, string ArchivoIni, clsDatosCliente datosCliente): base()
        {
            conexion = new wsFarmaciaSoftGn.wsConexion();
            // this.datosCnn = DatosCnn;
            this.pDatosCliente = datosCliente;
            base.sArchivoIni = ArchivoIni;
            // base.Conexion.DatosConexion = DatosCnn;

            if (!ValidarURL(Url))
            {
                conexion = null;
            }
        }

        public clsLeerWebExt(ref clsConexionSQL Cnn, string Url, string ArchivoIni, clsDatosCliente datosCliente):base(ref Cnn) 
        {
            conexion = new wsFarmaciaSoftGn.wsConexion();
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
            get { return pDatosCliente; }
            set { pDatosCliente = value; }
        }

        public string UrlWebService
        {
            get { return base.sUrl; }
            set { ValidarURL(value); }
        }

        public int TimeOut
        {
            get { return iTimeOut; }
            set { iTimeOut = value; }
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

                conexion = new wsFarmaciaSoftGn.wsConexion();
                conexion.Url = base.sUrl;
                // conexion.Timeout = 250000;
                conexion.Timeout = iTimeOut; 

                base.dtsClase = conexion.ExecuteExt(pDatosCliente.DatosCliente(), base.sArchivoIni, Cadena);
                base.iRegistros = 0;
                base.iPosActualReg = 0;

                if (!SeEncontraronErrores(dtsClase))
                {
                    base.DataSetClase = dtsClase;
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
