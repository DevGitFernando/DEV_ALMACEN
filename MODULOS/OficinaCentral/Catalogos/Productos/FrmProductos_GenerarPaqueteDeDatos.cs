using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Productos;
using DllFarmaciaSoft.Usuarios_y_Permisos;
using DllTransferenciaSoft; 
using OficinaCentral.Catalogos.Productos;

namespace OficinaCentral.Catalogos
{
    public partial class FrmProductos_GenerarPaqueteDeDatos : FrmBaseExt
    {
        DllTransferenciaSoft.ObtenerInformacion.clsCliente ClienteTransferencias =
            new DllTransferenciaSoft.ObtenerInformacion.clsCliente(DtGeneral.CfgIniOficinaCentral, General.DatosConexion);

        public FrmProductos_GenerarPaqueteDeDatos()
        {
            InitializeComponent();
        }

        private void FrmProductos_GenerarPaqueteDeDatos_Load(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

        }

        private void btnProcesarArchivos_Click(object sender, EventArgs e)
        {
            ClienteTransferencias = new DllTransferenciaSoft.ObtenerInformacion.clsCliente(DtGeneral.CfgIniPuntoDeVenta, General.DatosConexion);

            if (!ClienteTransferencias.AltaProductos(txtIdProducto_01_Inicial.Text, txtIdProducto_02_Final.Text, 1))
            {
                General.msjError("Ocurrió un error al generar el paquete de datos solicitado.");
            }
            else 
            {
                General.msjAviso("Generación de Paquete de Datos terminada.");
                ClienteTransferencias.Abrir_Directorio_Transferencias();
            }
        }
    }
}
