using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

// Implementacion de hilos 
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;

namespace Almacen.Vales_Servicio_A_Domicilio
{
    public partial class FrmListadoDeServiciosADomicilio : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente datosCliente;
        clsLeerWebExt leerWeb;
        clsLeer leer;
        clsAyudas ayuda;
        clsConsultas query;

        public FrmListadoDeServiciosADomicilio()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
        }

        private void FrmListadoDeServiciosADomicilio_Load(object sender, EventArgs e)
        {

        }
    }
}
