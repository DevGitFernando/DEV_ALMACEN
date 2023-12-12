using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Data.Odbc;

using Microsoft.VisualBasic;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;

using Dll_SII_IMediaccess;

namespace Dll_SII_IMediaccess.ValesRecepcion
{
    public class ResponseLogin
    {
        bool bError = false;
        int iStatus = 0;
        string sMensaje = "";

        string sCliente = "";
        string sSucursal = "";
        string sIdentificador = "";

        public ResponseLogin() 
        { 
        }

        ////public ResponseLogin(string IdCliente, string IdSucursal)
        ////{
        ////    this.sCliente = IdCliente;
        ////    this.sSucursal = IdSucursal; 
        ////}

        public string Identificador
        {
            get { return sIdentificador; }
            set { sIdentificador = value; }
        }

        public string Cliente
        {
            get { return sCliente; }
            set { sCliente = value; }
        }

        public string Sucursal
        {
            get { return sSucursal; }
            set { sSucursal = value; }
        }

        public bool Error
        {
            get { return bError; }
            set { bError = value; }
        }

        public int Estatus
        {
            get { return iStatus; }
            set { iStatus = value; }
        }

        public string Mensaje
        {
            get { return sMensaje; }
            set { sMensaje = value; }
        }
    }

    public class ResponseSolicitud
    {
        bool bError = false;
        int iStatus = 0;
        string sMensaje = "";

        public bool Error 
        {
            get { return bError; }
            set { bError = value; } 
        }

        public int Estatus
        {
            get { return iStatus; }
            set { iStatus = value; }
        }

        public string Mensaje 
        {
            get { return sMensaje; }
            set { sMensaje = value; } 
        }    
    }
}
