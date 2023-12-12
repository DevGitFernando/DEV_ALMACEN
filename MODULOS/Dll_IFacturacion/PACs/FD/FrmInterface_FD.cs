using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

// Implementacion de hilos 
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.ExportarDatos;


using Dll_IFacturacion;
using Dll_IFacturacion.CFDI;
using Dll_IFacturacion.CFDI.CFDFunctions;

namespace Dll_IFacturacion.FD
{
    public partial class FrmInterface_FD : FrmBaseExt 
    {
        fdGenerarDocumento fd = new fdGenerarDocumento(General.DatosConexion);
        PAC_Info rfc_Emisor;

        public FrmInterface_FD(PAC_Info RFC_Emisor)
        {
            InitializeComponent();

            rfc_Emisor = RFC_Emisor; 
        }

        private void btnObtenerXML_Click(object sender, EventArgs e)
        {
            if (fd.ObtenerXML(txtUUID.Text))
            { 
            }
        }

        ////private void button1_Click(object sender, EventArgs e)
        ////{
        ////    if (fd.ConsultarCreditos(rfc_Emisor))
        ////    {
        ////        General.msjAviso( string.Format("Timbres disponibles: {0}", fd.Creditos_Disponibles) ); 
        ////    }
        ////}
    }
}
