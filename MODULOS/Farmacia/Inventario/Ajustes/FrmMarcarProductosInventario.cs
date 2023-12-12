using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft; 

namespace Farmacia.Inventario
{
    public partial class FrmMarcarProductosInventario : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        string sFormato = "###,###,###,##0";
        string sMsjGuardar = "";
        string sMsjError = ""; 

        public FrmMarcarProductosInventario()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, GnFarmacia.DatosApp, this.Name); 
        }

        private void FrmMarcarProductosInventario_Load(object sender, EventArgs e)
        {
            lblProductosActivos.Text = "0";
            lblProductosEnInventario.Text = "0";

            btnMarcar.Enabled = false;
            btnDesmarcar.Enabled = false;

            //// 
            BuscarProductosActivos(); 
            BuscarProductosEnInventario();
            BuscarProductosMarcados(); 
        }

        #region Botones 
        private void btnMarcar_Click(object sender, EventArgs e)
        {
            sMsjGuardar = "Se marcaron correctamente todos los productos de la Unidad.";
            sMsjError = "Ocurrió un error al marcar los productos.";
            MarcarProductosParaInventario(1); 
        }

        private void btnDesmarcar_Click(object sender, EventArgs e)
        {
            sMsjGuardar = "Se desmarcaron correctamente todos los productos de la Unidad.";
            sMsjError = "Ocurrió un error al desmarcar los productos.";
            MarcarProductosParaInventario(2); 
        }
        #endregion Botones

        #region Funciones y Procedimientos Privados 
        private void BuscarProductosActivos()
        {
            string sSql = string.Format("Select count(*) as Registros From FarmaciaProductos (NoLock) " + 
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Status = 'A' ", 
                 DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada );

            lblProductosActivos.Text = "0";
            btnMarcar.Enabled = false;

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "BuscarProductosActivos");
            }
            else
            {
                if (leer.Leer())
                {
                    if (leer.CampoInt("Registros") > 0)
                    {
                        lblProductosActivos.Text = leer.CampoInt("Registros").ToString(sFormato); 
                        btnMarcar.Enabled = true;
                    }
                }
            }
        }

        private void BuscarProductosMarcados()
        {
            string sSql = string.Format("Select count(*) as Registros From FarmaciaProductos (NoLock) " +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Status = 'I' ",
                 DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            lblMarcadosInventario.Text = "0";

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "BuscarProductosActivos");
            }
            else
            {
                if (leer.Leer())
                {
                    if (leer.CampoInt("Registros") > 0)
                    {
                        lblMarcadosInventario.Text = leer.CampoInt("Registros").ToString(sFormato);
                        btnDesmarcar.Enabled = true;
                    }
                }
            }
        }

        private void BuscarProductosEnInventario() 
        {
            string sSql = string.Format("Select count(*) as Registros From vw_Productos_Bloqueados_Por_Inventario (NoLock) " +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' ",
                 DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            lblProductosEnInventario.Text = "0";
            btnDesmarcar.Enabled = false;

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "BuscarProductosEnInventario"); 
            }
            else
            {
                btnDesmarcar.Enabled = true;
                if (leer.Leer())
                {
                    if (leer.CampoInt("Registros") > 0)
                    {
                        lblProductosEnInventario.Text = leer.CampoInt("Registros").ToString(sFormato);
                        btnDesmarcar.Enabled = false;
                    }
                }
            }
        }

        private void MarcarProductosParaInventario(int Opcion)
        {
            string sSql = string.Format("Exec spp_Mtto_AjustesInv_MarcarDesmarcar_Productos '{0}', '{1}', '{2}', '{3}' ",
                 DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, Opcion);

            if ( cnn.Abrir() )
            {
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "MarcarProductosParaInventario");  
                    cnn.DeshacerTransaccion();
                    General.msjError(sMsjError); 
                }
                else
                {
                    cnn.CompletarTransaccion();
                    General.msjUser(sMsjGuardar);

                    // Actualizar la informacion 
                    BuscarProductosActivos();
                    BuscarProductosEnInventario();
                    BuscarProductosMarcados(); 

                    btnMarcar.Enabled = false; 
                    btnDesmarcar.Enabled = false; 
                }

                cnn.Cerrar(); 
            }
            else 
            {
                General.msjAviso(General.MsjErrorAbrirConexion); 
            }
        }
        #endregion Funciones y Procedimientos Privados
   
    }
}
