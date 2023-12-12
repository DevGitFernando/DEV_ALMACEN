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
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.ExportarExcel;

using ClosedXML.Excel;

namespace Almacen.Pedidos
{
    public partial class FrmPedidos_RegistroDeIncidencias : FrmBaseExt
    {
        enum Cols
        {
            Ninguno = 0, 
            Secuencial = 1,
            Id, 
            Incidencia, Descripcion, Status, EsInformativo, Observaciones_Adicionales 
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente datosCliente;
        clsLeer leer;
        clsLeer leer_Datos;
        clsConsultas query;
        clsAyudas ayuda;
        clsGrid grid;

        int iId = 0;
        DataSet dtsDetalles_Incidencias; 


        public FrmPedidos_RegistroDeIncidencias(int Id, DataSet Informacion)
        {
            InitializeComponent();
            //iAnchoPantalla = (int)((double)Screen.PrimaryScreen.WorkingArea.Size.Width * 0.95);
            //this.Width = iAnchoPantalla;
            //General.Pantalla.AjustarTamaño(this, 90, 80);


            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmPedidos_RegistroDeIncidencias");

            leer = new clsLeer(ref cnn);
            leer_Datos = new clsLeer(); 
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name);

            iId = Id;
            dtsDetalles_Incidencias = Informacion;
            leer.DataSetClase = dtsDetalles_Incidencias;
            leer_Datos.DataSetClase = dtsDetalles_Incidencias;

            grid = new clsGrid(ref grdDetalles, this);

            grid.OcultarColumna(true, Cols.Secuencial);
            grid.OcultarColumna(true, Cols.Id);
            grid.AjustarAnchoColumnasAutomatico = true; 
        }

        private void InicializarPantalla()
        {
            Fg.IniciaControles();
            grid.Limpiar();

            CargarListaDeIncidencias(); 
        }

        public DataSet Detalles_Incidencias
        {
            get { return dtsDetalles_Incidencias; }
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarPantalla();
        }

        private void btnGuardar_Click( object sender, EventArgs e )
        {
            int iId = 0;
            int iStatus = 0;
            string sObservaciones = "";
            int iRow = 0;

            for(int i = 1; i <= grid.Rows; i++)
            {
                iId = grid.GetValueInt(i, Cols.Secuencial);
                iStatus = Convert.ToInt32(grid.GetValueBool(i, Cols.Status));
                sObservaciones = grid.GetValue(i, Cols.Observaciones_Adicionales);

                leer_Datos.GuardarDatos(iId, "Status_Incidencia", iStatus);
                leer_Datos.GuardarDatos(iId, "Observaciones_Adicionales", sObservaciones);
            }

            dtsDetalles_Incidencias = leer_Datos.DataSetClase; 

            this.Hide(); 
        }

        private void btnSalir_Click( object sender, EventArgs e )
        {
            this.Close();
        }
        #endregion Botones 

        private void FrmPedidos_RegistroDeIncidencias_Load(object sender, EventArgs e)
        {
            //CargarJurisdicciones();
            //CargarStatusPedidos();
            //CargarRutas();
            InicializarPantalla();
        }

        private void CargarListaDeIncidencias()
        {
            clsLeer datos = new clsLeer();
            datos.DataRowsClase = leer.Tabla(1).Select(string.Format(" Id = '{0}' ", iId));

            grid.LlenarGrid(datos.DataSetClase, false, false);
        }
    }
}
