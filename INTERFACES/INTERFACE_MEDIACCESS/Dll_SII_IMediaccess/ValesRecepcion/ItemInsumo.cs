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
    public class ItemInsumo
    {
        string sClaveInsumo = "";
        string sDescripcion = ""; 
        int iPiezas = 0;

        public ItemInsumo():this("", 0, "") 
        {
        }

        public ItemInsumo(string Clave, int Piezas, string Descripcion )
        {
            this.sClaveInsumo = Clave;
            this.iPiezas = Piezas;
            this.sDescripcion = Descripcion; 
        }

        public string ClaveInsumo 
        {
            get { return sClaveInsumo; }
            set { sClaveInsumo = value; } 
        }
        
        public string Descripcion 
        {
            get { return sDescripcion; }
            set { sDescripcion = value; } 
        }
        
        public int Piezas 
        {
            get { return iPiezas; }
            set { iPiezas = value; }
        }       
    }
}
