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

namespace Dll_IFacturacion.XSA
{
    internal partial class FrmDescripcion_FacturaConcentrada : FrmBaseExt 
    {
        public string Descripcion = "";
        public bool DescripcionCapturada = false; 

        public FrmDescripcion_FacturaConcentrada(string DescripcionActual)
        {
            InitializeComponent();

            this.Descripcion = DescripcionActual; 
        }

        private void FrmDescripcion_FacturaConcentrada_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
            txtDescripcion.Text = this.Descripcion;
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles();
            txtDescripcion.Focus(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            DescripcionCapturada = true;
            Descripcion = txtDescripcion.Text.Trim();
            this.Hide(); 
        }
        #endregion Botones
    }
}
