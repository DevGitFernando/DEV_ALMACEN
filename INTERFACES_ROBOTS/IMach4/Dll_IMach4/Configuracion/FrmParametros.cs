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
using SC_SolutionsSystem.Errores;

using Dll_IMach4; 

namespace Dll_IMach4.Configuracion
{
    public partial class FrmParametros : FrmBaseExt
    {
        enum cols
        {
            Parametro = 1, Valor = 2, Descripcion = 3 
        }


        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid Grid;
        public bool SoloLectura = false; 

        public FrmParametros()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Grid = new clsGrid(ref grdParametros, this);

            grdParametros.EditModeReplace = true; 
            Grid.EstiloGrid(eModoGrid.ModoRow);
            Error = new clsGrabarError(General.DatosConexion, IMach4.DatosApp, this.Name);

            // Ayudas = new clsAyudas(General.DatosConexion, IMach4.Modulo, this.Name, IMach4.Version);

        }

        private void FrmParametros_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null); 
        }

        private void CargarParametros()
        {
            // Cargar los Parametros del sistema 
            clsParametrosIMach Parametros = new clsParametrosIMach(General.DatosConexion, IMach4.DatosApp); 
            Parametros.CargarParametros();

            string sSql = string.Format("Select NombreParametro, Valor, Descripcion " +
                " From IMach_Net_Parametros (NoLock) " + 
                " Order by NombreParametro ");

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargandoDatos");
                General.msjError("Ocurrió un error al obtener la lista de parametros.");
            }
            else
            {
                Grid.Limpiar(false);
                Grid.LlenarGrid(leer.DataSetClase);
                CargaDescripcion(1);
            }
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles();
            Grid.Limpiar(false);
            CargarParametros();

            if (SoloLectura)
            {
                btnNuevo.Enabled = false;
                btnGuardar.Enabled = false; 
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Guardar(); 
        }

        private void Guardar()
        {
            bool bExito = true;
            string sSql = "";
            string sArbol = "", sParametro = "", sValor = "";
            string sDescripcion = ""; 

            if (!cnn.Abrir())
            {
                General.msjErrorAlAbrirConexion(); 
            }
            else 
            {
                cnn.IniciarTransaccion();

                for (int i = 1; i <= Grid.Rows; i++)
                {
                    sParametro = Grid.GetValue(i, (int)cols.Parametro);
                    sValor = Grid.GetValue(i, (int)cols.Valor);
                    sDescripcion = Grid.GetValue(i, (int)cols.Descripcion);

                    sSql = string.Format(" Exec spp_Mtto_IMach_Net_Parametros '{0}', '{1}', '{2}', 1 ", sParametro, sValor, sDescripcion); 
                    if (!leer.Exec(sSql))
                    {
                        bExito = false;
                        break;
                    }
                }


                if (bExito)
                {
                    cnn.CompletarTransaccion();
                    General.msjUser("Información guardada satisfactoriamente.");
                }
                else
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(leer, "btnAceptar_Click");
                    General.msjError("Ocurrió un error al grabar la información.");
                }

                cnn.Cerrar();
            }

        }


        private void grdParametros_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            CargaDescripcion(e.NewRow + 1);
        }

        private void CargaDescripcion(int Renglon)
        {
            lblDescripcion.Text = Grid.GetValue(Renglon, (int)cols.Descripcion);
        }

    }
}
