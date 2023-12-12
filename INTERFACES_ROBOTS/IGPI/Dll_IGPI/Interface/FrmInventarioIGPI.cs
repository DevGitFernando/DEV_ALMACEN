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
using SC_SolutionsSystem.Reportes;

using Dll_IGPI.Protocolos;
using Dll_IGPI.wsGPI; 

namespace Dll_IGPI.Interface
{
    public partial class FrmInventarioIGPI : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer, registros;
        clsGrid gridClaves, gridProductos;
        DataSet dtsProductos = new DataSet();
        clsDatosCliente DatosCliente;
        wsGPI.wsCnnCliente conexionWeb; 


        public FrmInventarioIGPI()
        {
            InitializeComponent();

            DatosCliente = new clsDatosCliente(IGPI.DatosApp, this.Name, "");
            conexionWeb = new wsGPI.wsCnnCliente();
            conexionWeb.Url = General.Url;


            registros = new clsLeer(); 
            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(IGPI.DatosApp, this.Name); 

            gridClaves = new clsGrid(ref grdClaves, this);
            gridProductos = new clsGrid(ref grdProductos, this);

            gridClaves.EstiloGrid(eModoGrid.SeleccionSimple);
            gridClaves.SetOrder(true); 

            gridProductos.EstiloGrid(eModoGrid.SeleccionSimple);
            gridProductos.SetOrder(true);

            tmK_Expire.Interval = (1000 * 10);
        }

        private void FrmInventarioIGPI_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null); 
            // btnEjecutar.Enabled = IGPI.EsServidorDeInterface; 
        }

        private void InicializaToolBar(bool Ejecutar, bool Imprimir)
        {
            btnEjecutar.Enabled = Ejecutar;
            btnImprimir.Enabled = Imprimir;  
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            tmK.Stop();
            tmK.Enabled = false;

            tmK_Expire.Stop();
            tmK_Expire.Enabled = false;

            InicializaToolBar(IGPI.EsServidorDeInterface, false); 

            rdoConcentradoClaves.Checked = true; 
            gridClaves.Limpiar();
            gridProductos.Limpiar();


            // Cargar la informacion de Totales 
            lblClavesDistintas.Text = "0";
            lblProductos.Text = "0";
            lblUnidadesClaves.Text = "0";
            lblUnidadesProductos.Text = "0"; 
        } 

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            clsI_K_Request K = new clsI_K_Request(); 
            clsI_K_Informacion.PrepararRecepcion(); 

            K.Dialogo = "K"; 
            K.Parametros.RequestLocationNumber = IGPI.Terminal; 
            K.Parametros.LineNumber = Fg.PonCeros(IGPI.Registros_K, 2); 

            tmK.Enabled = true;
            tmK.Start();

            tmK_Expire.Enabled = true;
            tmK_Expire.Start(); 

            gridClaves.Limpiar();
            gridProductos.Limpiar();

            InicializaToolBar(false, false); 

            tabInventario.SelectedIndex = 0; 
            tabpClaves.Focus();


            if (IGPI.Protocolo_Comunicacion == ProtocoloConexion_IGPI.SERIAL)
            {
                IGPI_SerialPort.Solicitud_K_Iniciada = true;
                IGPI_SerialPort.EnviarDatos(K.Respuesta());
            }

            if (IGPI.Protocolo_Comunicacion == ProtocoloConexion_IGPI.TCP_IP)
            {
                IGPI_Winsock.Solicitud_K_Iniciada = true;
                IGPI_Winsock.EnviarDatos(K.Respuesta());
            }

        }

        private void tmK_Expire_Tick(object sender, EventArgs e)
        {
            ////tmK_Expire.Stop();
            ////tmK_Expire.Enabled = false;

            ////if (IGPI.Protocolo_Comunicacion == ProtocoloConexion_IGPI.TCP_IP)
            ////{
            ////    IGPI_Winsock.Solicitud_K_Interrumpir();
            ////}
        }

        private void tmK_Tick(object sender, EventArgs e)
        {
            bool bSolicitud_K_Completada = false;
            bool bSolicitud_K_Interrumpida = false;

            if (IGPI.Protocolo_Comunicacion == ProtocoloConexion_IGPI.SERIAL)
            {
                bSolicitud_K_Completada = IGPI_SerialPort.Solicitud_K_Completada;
                bSolicitud_K_Interrumpida = true;
            }

            if (IGPI.Protocolo_Comunicacion == ProtocoloConexion_IGPI.TCP_IP)
            {
                bSolicitud_K_Completada = IGPI_Winsock.Solicitud_K_Completada;
                bSolicitud_K_Interrumpida = IGPI_Winsock.Solicitud_K_Interrumpida;
            }

            if (bSolicitud_K_Completada)
            {
                tmK.Stop();
                tmK.Enabled = false;

                GenerarInformacion();

                InicializaToolBar(IGPI.EsServidorDeInterface, true);
            }

            ////if (bSolicitud_K_Interrumpida)
            ////{
            ////    tmK.Stop();
            ////    tmK.Enabled = false;

            ////    InicializaToolBar(IGPI.EsServidorDeInterface, false);
            ////}
        }

        private void GenerarInformacion()
        {
            bool bRegresa = false;
            string sSql = " Truncate Table IGPI_ExistenciaK ";
            string sMsjError = "Ocurrió un error al Cargar la Información del Inventario.";

            if (cnn.Abrir())
            {
                cnn.IniciarTransaccion();
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "GenerarInformacion()");
                    General.msjError(sMsjError); 
                }
                else
                {
                    bRegresa = true; 
                    registros.DataSetClase = clsI_K_Informacion.Registros;

                    while (registros.Leer())
                    {
                        sSql = string.Format(" Insert Into IGPI_ExistenciaK ( CodigoEAN, Cantidad ) select '{0}', '{1}' ",
                            registros.Campo("CodigoEAN"), registros.Campo("Cantidad"));
                        if (!leer.Exec(sSql))
                        {
                            bRegresa = false;
                            break; 
                        }
                    }
                }

                if (bRegresa)
                {
                    // Generar el Historico de K 
                    sSql = " Insert Into IGPI_ExistenciaK_Historico ( CodigoEAN, Cantidad ) " +
                           " Select CodigoEAN, Cantidad From IGPI_ExistenciaK (NoLock) ";
                    bRegresa = leer.Exec(sSql); 
                }

                if (bRegresa)
                {
                    cnn.CompletarTransaccion();
                }
                else
                {
                    cnn.DeshacerTransaccion();
                }
            }
            cnn.Cerrar();

            if (bRegresa)
            {
                CargarInformacion(); 
            }


            // Cargar la informacion de Totales 
            lblClavesDistintas.Text = gridClaves.Rows.ToString(); 
            lblProductos.Text = gridProductos.Rows.ToString();
            lblUnidadesClaves.Text = gridClaves.TotalizarColumna(3).ToString();
            lblUnidadesProductos.Text = gridProductos.TotalizarColumna(3).ToString(); 
        } 

        private void CargarInformacion()
        {
            string sMsjError = "Ocurrió un error al Cargar la Información del Inventario.";
            string sSql = " Select ClaveSSA, DescClaveSSA, Existencia From vw_IGPI_ExistenciaK_Clave (NoLock) Order by DescClaveSSA ";

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "GenerarInformacion().");
                General.msjError(sMsjError);
            }
            else
            {
                gridClaves.LlenarGrid(leer.DataSetClase);
                sSql = " Select CodigoEAN, NombreComercial, Existencia, ClaveSSA From vw_IGPI_ExistenciaK_EAN (NoLock) Order by NombreComercial ";
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "GenerarInformacion()..");
                    General.msjError(sMsjError);
                }
                else
                {
                    dtsProductos = leer.DataSetClase;
                    //gridProductos.LlenarGrid(leer.DataSetClase); 
                    CargarProductos(1); 
                }
            }
        }

        private void grdClaves_Leave(object sender, EventArgs e)
        {
            
        }

        private void CargarProductos(int Renglon)
        {
            try
            {
                DataTable dt = dtsProductos.Tables[0].Copy();
                dt.Rows.Clear();

                try
                {
                    string sClave = string.Format(" ClaveSSA = '{0}' ", gridClaves.GetValue(Renglon, 1));

                    foreach (DataRow row in dtsProductos.Tables[0].Select(sClave))
                    {
                        dt.Rows.Add(row.ItemArray);
                    }

                    leer.DataTableClase = dt;
                }
                catch (Exception ex)
                {
                    General.msjError(ex.Message);
                }

                gridProductos.Limpiar();

                if (leer.Leer())
                {
                    gridProductos.LlenarGrid(leer.DataSetClase);
                }
            }
            catch { }
        }

        private void tabInventario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabInventario.SelectedIndex == 1)
            {
                CargarProductos(gridClaves.ActiveRow);
            }
        }

        private bool validarImpresion()
        {
            bool bRegresa = true;

            if (gridClaves.Rows <= 0 )
            {
                bRegresa = false;
                General.msjUser("No hay información para generar la impresión."); 
            }

            return bRegresa; 
        }

        private void ImprimirInventario()
        {
            if (validarImpresion())
            {
                DatosCliente.Funcion = "ImprimirInventario()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                byte[] btReporte = null;

                myRpt.RutaReporte = IGPI.RutaReportes;
                myRpt.NombreReporte = "IGPI_ExistenciasClaves.rpt";

                if (rdoDetalladoClaves.Checked)
                {
                    myRpt.NombreReporte = "IGPI_ExistenciasClavesDetallado.rpt";
                }

                if (rdoDetalladoProductos.Checked)
                {
                    myRpt.NombreReporte = "IGPI_ExistenciasProductos.rpt";
                }

                DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                DataSet datosC = DatosCliente.DatosCliente();

                btReporte = conexionWeb.Reporte(InfoWeb, datosC);

                if (!myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true)) 
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirInventario(); 
        }
    }
}
