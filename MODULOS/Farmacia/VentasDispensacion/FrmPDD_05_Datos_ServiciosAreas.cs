using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos;
using DllFarmaciaSoft.Devoluciones;

using Farmacia.Procesos;
using Farmacia.Vales;

namespace Farmacia.VentasDispensacion
{
    public partial class FrmPDD_05_Datos_ServiciosAreas : FrmBaseExt 
    {
        clsConexionSQL cnn;
        clsLeer leer;
        clsConsultas query;
        clsAyudas ayuda;

        DataSet dtsAreas;

        private string sIdEmpresa = DtGeneral.EmpresaConectada;
        private string sIdEstado = "";
        private string sIdFarmacia = "";

        public string sFolioVenta = "";
        public string sIdServicio = "";
        public string sIdArea = "";
        public bool bEsVale = false;

        public FrmPDD_05_Datos_ServiciosAreas()
        {
            InitializeComponent();
        }

        public FrmPDD_05_Datos_ServiciosAreas(string IdEstado, string IdFarmacia, string FolioVenta)
        {
            InitializeComponent();

            this.sIdEstado = IdEstado;
            this.sIdFarmacia = IdFarmacia;
            this.sFolioVenta = FolioVenta;

            cnn = new clsConexionSQL(General.DatosConexion);
            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, false);
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, false);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);
        }

        #region Eventos_Forma
        private void FrmPDD_05_Datos_ServiciosAreas_Load(object sender, EventArgs e)
        {
            CargarServicios();
            Fg.IniciaControles();            

            cboServicios.Data = sIdServicio;
            cboAreas.Data = sIdArea;           

            // Cargar la informacion Guardada 
            CargarInformacionAdicionalDeVentas();
        }

        private void FrmPDD_05_Datos_ServiciosAreas_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F12:
                    if (ValidarInformacion())
                    {
                        this.Close();
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion Eventos_Forma

        #region Cargar informacion
        private void CargarServicios()
        {
            cboServicios.Clear();
            cboServicios.Add("0", "<< Seleccione >>");
            cboServicios.Add(query.Farmacia_Servicios(sIdEstado, sIdFarmacia, "CargarServicios()"), true, "IdServicio", "Servicio");
            cboServicios.SelectedIndex = 0;

            cboAreas.Clear();
            cboAreas.Add("0", "<< Seleccione >>");
            dtsAreas = query.Farmacia_ServiciosAreas(sIdEstado, sIdFarmacia, "CargarServicios()");
            cboAreas.SelectedIndex = 0;            
        }

        private void CargarInformacionAdicionalDeVentas()
        {
            
            if (sFolioVenta == "*")
            {
                
            }
            else
            {
                DateTimePicker dtFechaActual = new DateTimePicker();                

                if (!bEsVale)
                {
                    leer.DataSetClase = query.VentaDispensacion_InformacionAdicional(sIdEmpresa, sIdEstado, sIdFarmacia, sFolioVenta, "CargarInformacionAdicionalDeVentas()");
                }
                else
                {
                    leer.DataSetClase = query.ValesEmision_InformacionAdicional(sIdEmpresa, sIdEstado, sIdFarmacia, sFolioVenta, "CargarInformacionAdicionalDeVentas()");
                }

                if (leer.Leer())
                {                   
                    cboServicios.Data = leer.Campo("IdServicio");
                    cboAreas.Data = leer.Campo("IdArea");

                    FrameDatos.Enabled = false;
                }
            }
        }
        #endregion Cargar informacion

        #region Validacion
        private bool ValidarInformacion()
        {
            bool bRegresa = true;

            if (cboServicios.SelectedIndex == 0)
            {
               
            }

            if (bRegresa && cboAreas.SelectedIndex == 0)
            {
                
            }
           
                        
            sIdServicio = cboServicios.Data;
            sIdArea = cboAreas.Data;            

            return bRegresa;
        }
        #endregion Validacion

        private void lblCerrar_Click(object sender, EventArgs e)
        {
            if (ValidarInformacion())
            {
                this.Close();
            }
        }

        private void cboServicios_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboAreas.Clear();
            cboAreas.Add("0", "<< Seleccione >>");
            if (cboServicios.SelectedIndex != 0)
            {
                try
                {
                    string sWhere = string.Format(" IdServicio = '{0}' ", cboServicios.Data);
                    cboAreas.Add(dtsAreas.Tables[0].Select(sWhere), true, "IdArea", "Area_Servicio");
                }
                catch { }
            }
            cboAreas.SelectedIndex = 0;
        }
    }
}
