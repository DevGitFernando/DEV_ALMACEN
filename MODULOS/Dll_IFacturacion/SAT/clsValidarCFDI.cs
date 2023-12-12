using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region USING
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Data.Odbc;
using System.Windows.Forms;
using System.Threading;
using System.ServiceModel;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;

using DllFarmaciaSoft;
using Dll_IFacturacion.Configuracion;
#endregion USING

//using Dll_IFacturacion.SAT_ConsultaQR;

namespace Dll_IFacturacion.SAT
{
    public class clsValidarCFDI
    {
        public clsValidarCFDI()
        {          
        }

        public void Validar(string UUID, string Serie, string Folio, string RFC_Emisor, string RFC_Receptor, double Importe, string SegmentoSello)
        {
            FrmValidarCFDI validar = new FrmValidarCFDI(UUID, Serie, Folio, RFC_Emisor, RFC_Receptor, Importe, SegmentoSello);
            validar.ShowInTaskbar = false;
            validar.ShowDialog(); 
        }
    }
}
