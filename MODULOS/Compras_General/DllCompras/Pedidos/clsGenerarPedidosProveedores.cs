using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

using DllFarmaciaSoft; 

namespace DllCompras.Pedidos
{
    public class clsGenerarPedidosProveedores
    {
        #region Declaracion de variables
        private string sIdEstado = DtGeneral.EstadoConectado;
        private string sIdFarmacia = DtGeneral.FarmaciaConectada;
        private string sIdPersonal = DtGeneral.IdPersonal;


        // private string sIdClaveSSA = "";
        // private string sClaveSSA = "";
        // private string sCodigoEAN = "";
        // private string sDescripcionClaveSSA = "";

        // int iTotalCantidad = 0;
        // int iExistencia = 0;
        // int iCantidad = 0;

        // DataSet pDtsLotes;
        basGenerales Fg = new basGenerales();
        clsLeer leer = new clsLeer();
        #endregion Declaracion de variables

        #region Constructores y Destructor 
        public clsGenerarPedidosProveedores()
        { 
        }

        public clsGenerarPedidosProveedores(string Estado, string Farmacia, string IdPesonal)
        {
            this.sIdEstado = Estado;
            this.sIdFarmacia = Farmacia;
            this.sIdPersonal = IdPesonal;
        }

        ~clsGenerarPedidosProveedores()
        {
        }
        #endregion Constructor y Destructor 
    }
}
