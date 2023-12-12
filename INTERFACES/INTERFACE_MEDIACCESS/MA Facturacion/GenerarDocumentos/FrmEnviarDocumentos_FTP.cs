using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

// Implementacion de hilos 
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Inventario;
using DllFarmaciaSoft.Reporteador;

using Dll_MA_IFacturacion; 

namespace MA_Facturacion.GenerarDocumentos
{
    public partial class FrmEnviarDocumentos_FTP : FrmBaseExt
    {
        clsDatosConexion DatosDeConexion = new clsDatosConexion();
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion); 
        clsLeer leer;

        public FrmEnviarDocumentos_FTP()
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtIFacturacion.DatosApp, this.Name); 
        }
    }
}
