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

using Dll_IGPI; 
using Dll_IGPI.Protocolos; 

namespace Dll_IGPI.Interface
{
    public partial class FrmEstadoDeDemanda : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid myGrid;
        clsI_R_Request R = new clsI_R_Request();

        bool bMostrarCambios = true; 
 

        public FrmEstadoDeDemanda()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn); 

            myGrid = new clsGrid(ref grdSolicitudes, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow); 
        }

        private void FrmEstadoDeDemanda_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);

            tmPeticiones.Interval = 1000;
            tmPeticiones.Enabled = true;
            tmPeticiones.Start(); 
        }

        #region Botones 
        private void InicializaToolBar(bool Ejecutar, bool Enviar_R)
        {
            btnEjecutar.Enabled = Ejecutar;
            btnEnviar_R.Enabled = Enviar_R;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializaToolBar(true, IGPI.EsServidorDeInterface); 
            myGrid.Limpiar(false); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            string sSql = string.Format("Select top 100 Consecutivo, IdProducto, CodigoEAN, DescripcionClave, " + 
                " CantidadSolicitada, CantidadSurtida, " + 
                " convert(varchar(40), FechaSurtido, 120) FechaSurtido, StatusIGPI_Aux " + 
                " From vw_IGPI_SolicitudesDeProductos (NoLock) " + 
                " Where StatusIGPI Not In ( '{0}', '{1}', '{2}' )" + 
                " Order by Consecutivo ", 
                (int)IGPI_StatusRespuesta_A.AcknowledgmentMessage,
                (int)IGPI_StatusRespuesta_A.Cancelled_ERP,
                (int)IGPI_StatusRespuesta_A.Quantity_Change_From_MEDIMAT_to_ERP
                );

            //sSql = string.Format("Select Consecutivo, IdProducto, CodigoEAN, DescripcionClave, " +
            //                " CantidadSolicitada, CantidadSurtida, FechaRegistro, StatusIGPI_Aux " +
            //                " From vw_IGPI_SolicitudesDeProductos (NoLock) " +
            //                " " +
            //                " Order by Consecutivo " );

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "btnEjecutar_Click");
                General.msjError("Ocurrió un error al obtener la Lista de Solicitudes.");
            }
            else
            {
                myGrid.Limpiar(false);
                if (leer.Leer())
                {
                    myGrid.LlenarGrid(leer.DataSetClase);
                }
            }
        }

        private void btnEnviar_R_Click(object sender, EventArgs e)
        {
            if (IGPI.EsServidorDeInterface)
            {
                IGPI_SerialPort.EnviarDatos(R.Solicitud); 

            } 
        }
        #endregion Botones 

        private void tmPeticiones_Tick(object sender, EventArgs e)
        {
            if ( bMostrarCambios ) 
                btnEjecutar_Click(null, null); 
        }

        private void FrmEstadoDeDemanda_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    {
                        bMostrarCambios = !bMostrarCambios;
                        if (!bMostrarCambios)
                        {
                            FrameSolicitudes.Text = "Solicitudes root"; 
                        }
                        else
                        {
                            FrameSolicitudes.Text = "Solicitudes"; 
                        }
                        break;
                    }
                default:
                    break; 
            }
        }
    }
}
