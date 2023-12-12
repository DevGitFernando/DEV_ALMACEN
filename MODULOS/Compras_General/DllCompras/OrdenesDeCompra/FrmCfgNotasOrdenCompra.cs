using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGrid;

using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;

namespace DllCompras.OrdenesDeCompra
{
    public partial class FrmCfgNotasOrdenCompra : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer; 

        public FrmCfgNotasOrdenCompra()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, GnCompras.DatosApp, this.Name); 
        }

        private void FrmCfgNotasOrdenCompra_Load(object sender, EventArgs e)
        {
            GrabarParametro(0); 
            CargarParametro(); 
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            CargarParametro(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GrabarParametro(1); 
        }
        #endregion Botones 

        #region Funciones y Procedimientos Privados 
        private void GrabarParametro(int Tipo)
        {
            string sSql = string.Format(" Exec spp_Mtto_Net_CFGS_Parametros '{0}', '{1}', '{2}', '{3}', '{4}' ",
                "COMP", "NotaOrdenDeCompra", txtValorParametro.Text.Trim(), "Especifica las Notas para el Proveedor en las Ordenes de Compras.", Tipo); 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "GrabarParametro");
            }

            sSql = string.Format(" Exec spp_Mtto_COM_Nota_OrdenDeCompra '{0}', '{1}', '{2}', '{3}', '{4}' ",
                "COMR", "NotaOrdenDeCompra", txtValorParametro.Text.Trim(), "Especifica las Notas para el Proveedor en las Ordenes de Compras.", Tipo);
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "GrabarParametro");
            }
        }

        private void CargarParametro()
        {
            string sSql = string.Format("Select top 1 ArbolModulo, NombreParametro, Valor, Descripcion  " + // EsDeSistema
                 " From COM_Nota_OrdenDeCompra (NoLock) Where ArbolModulo in ( 'COMP', 'COMR' ) and NombreParametro = 'NotaOrdenDeCompra' "); 
            if (!leer.Exec(sSql))
            {
                //bRegresa = false;
                Error.GrabarError(leer, "CargarParametros"); 
            }
            else
            {
                //dtsParametros = leer.DataSetClase;
                if( leer.Leer() )
                {
                    txtValorParametro.Text = leer.Campo("Valor"); 
                }
            } 
        }
        #endregion Funciones y Procedimientos Privados
    }
}
