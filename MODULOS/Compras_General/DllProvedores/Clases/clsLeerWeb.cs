using System;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;

namespace DllProveedores
{
    public class clsLeerWeb : clsLeer
    {
        #region Declaracion de variables
        // private clsConexion_Odbc Cnn;
        private wsProveedores.wsCnnProveedores conexion;
        protected clsDatosConexion datosCnn = new clsDatosConexion();
        protected clsDatosCliente pDatosCliente;
        #endregion Declaracion de variables

        #region Constructor 
        public clsLeerWeb(string Url, clsDatosCliente datosCliente)
        {
            conexion = new wsProveedores.wsCnnProveedores();
            // this.datosCnn = datosCnn;
            this.pDatosCliente = datosCliente;

            if (!ValidarURL(Url))
                conexion = null;
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

                conexion = new wsProveedores.wsCnnProveedores();
                conexion.Url = base.sUrl;
                conexion.Timeout = 250000;

                base.dtsClase = conexion.GetExecute(pDatosCliente.DatosCliente(), Cadena);
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
            }
            return bRegresa;
        }        
    }
}
