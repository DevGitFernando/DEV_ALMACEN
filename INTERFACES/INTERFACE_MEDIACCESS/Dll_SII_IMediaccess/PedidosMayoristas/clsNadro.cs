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
    public class clsNadro : clsPedidosMayoristas 
    {
        public clsNadro(string IdEstado, string IdFarmacia, string IdProveedor, string NombreClase)
            : base(IdEstado, IdFarmacia, IdProveedor, "clsNadro") 
        {
        }

        #region Funciones y Procedimientos Publicos
        public bool EnviarPedido(string FolioPedido)
        {
            bool bRegresa = false;
            return bRegresa;
        }
        #endregion Funciones y Procedimientos Publicos 
    }
}
