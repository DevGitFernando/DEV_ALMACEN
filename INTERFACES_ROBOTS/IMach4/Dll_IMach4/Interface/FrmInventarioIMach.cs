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

using Dll_IMach4.Protocolos;
using Dll_IMach4.wsIMach4; 

namespace Dll_IMach4.Interface
{
    public partial class FrmInventarioIMach : FrmBaseExt 
    {
        enum Cols
        {
            Ninguna = 0, 
            CodigoEAN = 1,
            Descripcion = 2,
            Cantidad = 3,
            SolicitarStock_B = 4,
            Stock_B = 5
        }


        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer, registros;
        clsGrid gridClaves, gridProductos;
        DataSet dtsProductos = new DataSet();
        clsDatosCliente DatosCliente;
        wsIMach4.wsCnnCliente conexionWeb;
        Cols ColActiva = Cols.Ninguna; 



        public FrmInventarioIMach()
        {
            InitializeComponent();

            DatosCliente = new clsDatosCliente(IMach4.DatosApp, this.Name, "");
            conexionWeb = new wsIMach4.wsCnnCliente();
            conexionWeb.Url = General.Url;


            registros = new clsLeer(); 
            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(IMach4.DatosApp, this.Name); 

            gridClaves = new clsGrid(ref grdClaves, this);
            gridProductos = new clsGrid(ref grdProductos, this);

            gridClaves.EstiloGrid(eModoGrid.SeleccionSimple);
            gridClaves.SetOrder(true); 

            gridProductos.EstiloGrid(eModoGrid.SeleccionSimple);
            gridProductos.SetOrder(true); 
        }

        private void FrmInventarioIMach_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null); 
            // btnEjecutar.Enabled = IMach4.EsServidorDeInterface; 
        }

        private void InicializaToolBar(bool Ejecutar, bool Imprimir, bool Solicitar_B)
        {
            btnEjecutar.Enabled = Ejecutar;
            btnImprimir.Enabled = Imprimir;
            btnEnviar_B_SolicitarInventario.Enabled = Solicitar_B;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            tmK.Stop();
            tmK.Enabled = false;

            InicializaToolBar(IMach4.EsServidorDeInterface, false, false); 

            rdoConcentradoClaves.Checked = true; 
            gridClaves.Limpiar();
            gridProductos.Limpiar();


            // Cargar la informacion de Totales 
            lblClavesDistintas.Text = "0";
            lblProductos.Text = "0";
            lblUnidadesClaves.Text = "0";
            lblUnidadesProductos.Text = "0"; 
        }

        private void btnEnviar_B_SolicitarInventario_Click( object sender, EventArgs e )
        {
            string sSql = "Select top 2 CodigoEAN From vw_IMach_ExistenciaK_EAN (NoLock) Order By DescClaveSSA";

            if(!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "btnEnviar_B_SolicitarInventario_Click");
                General.msjError("Ocurrió un error al obtener los Códigos EAN");
            }
            else
            {
                while(leer.Leer())
                {
                    SolicitarInventario(leer.Campo("CodigoEAN"));
                }


                General.msjUser("Información de existencias actualizada.");
                btnEnviar_B_SolicitarInventario.Enabled = false; 
            }
        }

        private void SolicitarInventario( string CodigoEAN )
        {
            clsI_B_Request B = new clsI_B_Request();

            B.Dialogo = "B";
            B.Parametros.RequestLocationNumber = IMach4.PuertoDeDispensacion;
            B.Parametros.ProductCode = CodigoEAN;


            if(IMach4.Protocolo_Comunicacion == ProtocoloConexion_IMach.SERIAL)
            {
                IMach4_SerialPort.EnviarDatos(B.Solicitud);
            }

            if(IMach4.Protocolo_Comunicacion == ProtocoloConexion_IMach.TCP_IP)
            {
                IMach4_Winsock.EnviarDatos(B.Solicitud);
            }
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            clsI_K_Request K = new clsI_K_Request(); 
            clsI_K_Informacion.PrepararRecepcion(); 

            K.Dialogo = "K"; 
            K.Parametros.RequestLocationNumber = IMach4.Terminal; 
            K.Parametros.LineNumber = Fg.PonCeros(IMach4.Registros_K, 2); 

            tmK.Enabled = true;
            tmK.Start();

            gridClaves.Limpiar();
            gridProductos.Limpiar();

            InicializaToolBar(false, false, false); 

            tabInventario.SelectedIndex = 0; 
            tabpClaves.Focus();


            if (IMach4.Protocolo_Comunicacion == ProtocoloConexion_IMach.SERIAL)
            {
                IMach4_SerialPort.Solicitud_K_Iniciada = true;
                IMach4_SerialPort.EnviarDatos(K.Respuesta());
            }

            if (IMach4.Protocolo_Comunicacion == ProtocoloConexion_IMach.TCP_IP)
            {
                IMach4_Winsock.Solicitud_K_Iniciada = true;
                IMach4_Winsock.EnviarDatos(K.Respuesta());
            }

        }

        private void tmK_Tick(object sender, EventArgs e)
        {
            bool bSolicitud_K_Completada = false;

            if (IMach4.Protocolo_Comunicacion == ProtocoloConexion_IMach.SERIAL)
            {
                bSolicitud_K_Completada = IMach4_SerialPort.Solicitud_K_Completada;
            }

            if (IMach4.Protocolo_Comunicacion == ProtocoloConexion_IMach.TCP_IP)
            {
                bSolicitud_K_Completada = IMach4_Winsock.Solicitud_K_Completada;
            }

            if (bSolicitud_K_Completada)
            {
                tmK.Stop();
                tmK.Enabled = false;

                GenerarInformacion();

                InicializaToolBar(IMach4.EsServidorDeInterface, true, true);
            }
        }

        private void GenerarInformacion()
        {
            bool bRegresa = false; 
            string sSql = " Truncate Table IMach_ExistenciaK ";
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
                        sSql = string.Format(" Insert Into IMach_ExistenciaK ( CodigoEAN, Cantidad ) select '{0}', '{1}' ",
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
                    sSql = " Insert Into IMach_ExistenciaK_Historico ( CodigoEAN, Cantidad ) " + 
                           " Select CodigoEAN, Cantidad From IMach_ExistenciaK (NoLock) ";
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
            string sSql = " Select ClaveSSA, DescClaveSSA, Existencia From vw_IMach_ExistenciaK_Clave (NoLock) Order by DescClaveSSA ";

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "GenerarInformacion().");
                General.msjError(sMsjError);
            }
            else
            {
                gridClaves.LlenarGrid(leer.DataSetClase);
                sSql = " Select CodigoEAN, NombreComercial, Existencia, ClaveSSA From vw_IMach_ExistenciaK_EAN (NoLock) Order by NombreComercial ";
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

        private void grdProductos_ButtonClicked( object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e )
        {
            ColActiva = (Cols)e.Column + 1;

            switch(ColActiva)
            { 
            }
        }

        private void SolicitarInventario( int Renglon )
        {
            clsI_B_Request B = new clsI_B_Request();
            string CodigoEAN = gridProductos.GetValue(Renglon, Cols.CodigoEAN);

            B.Dialogo = "B";
            B.Parametros.RequestLocationNumber = IMach4.PuertoDeDispensacion;
            B.Parametros.ProductCode = CodigoEAN;


            if(IMach4.Protocolo_Comunicacion == ProtocoloConexion_IMach.SERIAL)
            {
                IMach4_SerialPort.EnviarDatos(B.Solicitud);
            }

            if(IMach4.Protocolo_Comunicacion == ProtocoloConexion_IMach.TCP_IP)
            {
                IMach4_Winsock.EnviarDatos(B.Solicitud);
            }
        }

        private void ImprimirInventario()
        {
            if (validarImpresion())
            {
                DatosCliente.Funcion = "ImprimirInventario()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                byte[] btReporte = null;

                myRpt.RutaReporte = IMach4.RutaReportes;
                myRpt.NombreReporte = "IMach4_ExistenciasClaves.rpt";

                if (rdoDetalladoClaves.Checked)
                {
                    myRpt.NombreReporte = "IMach4_ExistenciasClavesDetallado.rpt";
                }

                if (rdoDetalladoProductos.Checked)
                {
                    myRpt.NombreReporte = "IMach4_ExistenciasProductos.rpt";
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
