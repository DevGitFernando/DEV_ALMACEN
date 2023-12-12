using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections;
using System.Collections.Generic;
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
    public class ItemBeneficiario : ItemPersona
    {
        public ItemBeneficiario():base() 
        { 
        }
    }

    public class ItemMedico : ItemPersona
    {
        public ItemMedico(): base()
        {
        }
    }

    public class ItemPersona
    {
        string sNombre = "";
        string sApPaterno = "";
        string sApMaterno = "";
        string sReferencia = "";
        string sDireccion = "";
        string sTelefono = "";
        string sSexo = "";
        DateTime dtFechaNacimiento = DateTime.Now;
        DateTime dtFechaFinVigencia = DateTime.Now;

        public ItemPersona()
        { 
        }

        public string Nombre
        {
            get { return sNombre; }
            set { sNombre = value; }
        }

        public string ApPaterno
        {
            get { return sApPaterno; }
            set { sApPaterno = value; }
        }

        public string ApMaterno
        {
            get { return sApMaterno; }
            set { sApMaterno = value; }
        }

        public string Referencia
        {
            get { return sReferencia; }
            set { sReferencia = value; }
        }

        public string Direccion
        {
            get { return sDireccion; }
            set { sDireccion = value; }
        }

        public string Telefono
        {
            get { return sTelefono; }
            set { sTelefono = value; }
        }

        public string Sexo
        {
            get { return sSexo; }
            set { sSexo = value; }
        }

        public DateTime FechaNacimiento
        {
            get { return dtFechaNacimiento; }
            set { value = dtFechaNacimiento; }
        }

        public DateTime FechaFinVigencia
        {
            get { return dtFechaFinVigencia; }
            set { value = dtFechaFinVigencia; }
        }
    }
}
