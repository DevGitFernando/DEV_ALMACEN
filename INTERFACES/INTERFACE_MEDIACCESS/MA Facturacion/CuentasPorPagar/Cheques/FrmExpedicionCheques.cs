using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Drawing.Printing;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;

namespace MA_Facturacion.CuentasPorPagar
{
    public partial class FrmExpedicionCheques : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsAyudas Ayuda;
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;
        clsDatosCliente DatosCliente;
        public string sFile = "";
        private StreamReader streamToPrint;
        private Font printFont;
        StreamWriter f;
        int iFolio = 0, iFolioInicio = 0, iFolioFin = 0; 

        public FrmExpedicionCheques()
        {
            InitializeComponent();
            cnn.SetConnectionString();
            DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "CuentasporPagar");
            myLeer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name);
            Ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.DatosApp, this.Name);
        }

        private void FrmExpedicionCheques_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(this, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            InicializarBotones(true, false);
            dtpFechaRegistro.Enabled = false;
            lblCancelado.Visible = false;
            txtId.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Validacion())
            {
                GrabarInformacion(1);
            } 
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            GrabarInformacion(2);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            CrearArchivo();
            //bool bRegresa = true;

            //DatosCliente.Funcion = "Imprimir()";
            //clsImprimir myRpt = new clsImprimir(General.DatosConexion);

            //myRpt.RutaReporte = DtGeneral.RutaReportes;
            //myRpt.NombreReporte = "CNT_FormatoCheque";

            //myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
            //myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
            //myRpt.Add("IdCheque", txtId.Text.Trim());
            //myRpt.Add("CantidadConLetra", General.LetraMoneda(Convert.ToDouble(txtCantidad.Text)));

            //bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);


            //if (!bRegresa)
            //{
            //    General.msjError("Ocurrió un error al cargar el reporte.");
            //}
        }

        private void InicializarBotones(bool Guadar, bool Cancelar)
        {
            btnGuardar.Enabled = Guadar;
            btnCancelar.Enabled = Cancelar;
        }
        #endregion Botones

        #region Eventos
        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            if (txtId.Text.Trim() == "")
            {
                txtId.Enabled = false;
                txtId.Text = "*";
            }
            else
            {
                txtId.Text = Fg.PonCeros(txtId.Text, 6);

                myLeer.DataSetClase = Consultas.Cheque(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, txtId.Text, "txtId_Validating()");

                if (myLeer.Leer())
                {
                    CargaDatosDelCheque();
                }
                else
                {
                    //General.msjAviso("No se a encontrado el cheque.");
                    btnNuevo_Click(null, null);
                }
            }
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayuda.Cheque(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, "txtId_KeyDown()");

                if (myLeer.Leer())
                {
                    CargaDatosDelCheque();
                }
            }
        }

        private void txtChequera_Validating(object sender, CancelEventArgs e)
        {
            iFolio = 0;
            iFolioInicio = 0;
            iFolioFin = 0;

            if (txtChequera.Text != "")
            {
                txtChequera.Text = Fg.PonCeros(txtChequera.Text, 6);

                myLeer.DataSetClase = Consultas.Chequera(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, txtChequera.Text, "txtChequera_Validating()");

                if (myLeer.Leer())
                {

                    iFolioInicio = myLeer.CampoInt("FolioInicio");
                    iFolioFin = myLeer.CampoInt("FolioFin");

                    if (myLeer.Campo("UltimoFolio") == "")
                    {
                        iFolio = iFolioInicio;
                    }
                    else
                    {
                        iFolio = Convert.ToInt32(myLeer.Campo("UltimoFolio"));
                        iFolio += 1;
                    }

                    if (myLeer.Campo("Status") == "C")
                    {
                        txtChequera.Text = "";
                        lblChequera.Text = "";
                        General.msjAviso("La Chequera seleccionado se encuentra actualmente cancelado.");
                        txtChequera.Focus();
                    }
                    else
                    {
                        if (iFolio > iFolioFin)
                        {
                            txtChequera.Text = "";
                            lblChequera.Text = "";
                            General.msjAviso("La Chequera seleccionado no tiene folios disponibles.");
                            txtChequera.Focus();
                        }
                        else
                        {
                            CargarChequera();
                        }
                    }
                }
                else
                {
                    txtChequera.Text = "";
                    lblChequera.Text = "";
                    lblFolioCheque.Text = "";
                }
            }
        }

        private void txtChequera_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayuda.Chequera(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, "txtChequera_KeyDown");

                if (myLeer.Leer())
                {
                    CargarChequera();
                }
            }
        }

        private void txtBeneficiario_Validating(object sender, CancelEventArgs e)
        {
            if (txtBeneficiario.Text != "")
            {
                myLeer.DataSetClase = Consultas.BeneficiarioChequera(txtBeneficiario.Text, "txtBeneficiario_Validating()");

                myLeer.Leer();

                if (myLeer.Campo("Status") == "C")
                {
                    txtBeneficiario.Text = "";
                    lblBeneficiario.Text = "";
                    General.msjAviso("El beneficiario seleccionado se encuentra actualmente cancelado.");
                }
                else
                {
                    CargarBeneficiario();
                }
            }
        }

        private void txtBeneficiario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayuda.BeneficiarioChequera("txtBeneficiario_KeyDown()");

                if (myLeer.Leer())
                {
                    CargarBeneficiario();
                }
            }
        }

        #endregion Eventos

        #region Funciones y Procedimientos

        private void GrabarInformacion(int iOpcion)
        {
            string sMsjErr = "Ocurrió un error al guardar la información.";

            if (iOpcion != 1)
            {
                sMsjErr = "Ocurrió un error al cancelar la información.";
            }


            if (cnn.Abrir())
            {
                cnn.IniciarTransaccion();

                string sSql = String.Format("Exec spp_Mtto_CNT_Cheque '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, '{8}', {9}",
                        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, txtId.Text.Trim(), lblFolioCheque.Text.Trim(), txtDescripcion.Text.Trim(),
                        txtChequera.Text.Trim(), txtBeneficiario.Text.Trim(), txtCantidad.NumericText.Trim(), General.FechaYMD(dtpFechaRegistro.Value), iOpcion);

                if (myLeer.Exec(sSql))
                {
                    myLeer.Leer();
                    cnn.CompletarTransaccion();
                    General.msjUser(myLeer.Campo("Mensaje")); //Este mensaje lo genera el SP
                    btnNuevo_Click(null, null);
                }
                else
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(myLeer, "btnGuardar_Click");
                    General.msjError(sMsjErr);
                }

                cnn.Cerrar();
            }
            else
            {
                General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
            }
        }

        private void CargaDatosDelCheque()
        {
            txtId.Text = myLeer.Campo("IdCheque");
            txtDescripcion.Text = myLeer.Campo("Cheque");
            txtChequera.Text = myLeer.Campo("IdChequera");
            lblChequera.Text = myLeer.Campo("Chequera");
            txtBeneficiario.Text = myLeer.Campo("IdBeneficiario");
            lblBeneficiario.Text = myLeer.Campo("Beneficiario");
            txtCantidad.Text = myLeer.Campo("Cantidad");
            lblFolioCheque.Text = myLeer.Campo("FolioCheque");
            dtpFechaRegistro.Value = myLeer.CampoFecha("FechaRegistro");
            InicializarBotones(false, true);

            txtId.Enabled = false;
            if (myLeer.Campo("Status") == "C")
            {
                InicializarBotones(false, false);
                lblCancelado.Visible = true;
            }
        }

        private void CargarChequera()
        {
            txtChequera.Text = myLeer.Campo("IdChequera");
            lblChequera.Text = myLeer.Campo("Descripcion");
            lblFolioCheque.Text = iFolio.ToString();
            
        }

        private void CargarBeneficiario()
        {
            txtBeneficiario.Text = myLeer.Campo("IdBeneficiario");
            lblBeneficiario.Text = myLeer.Campo("Descripcion");
        }

        private bool Validacion()
        {
            bool BContinua = true;
            if (txtDescripcion.Text == "")
            {
                General.msjAviso("Debe capturar una descripción, verifique porfavor.");
                BContinua = false;
            }

            if(txtChequera.Text == "" && BContinua)
            {
                General.msjAviso("Debe capturar una chequera, verifique porfavor.");
                BContinua = false;
            }

            if (txtBeneficiario.Text == "" && BContinua)
            {
                General.msjAviso("Debe capturar beneficiario, verifique porfavor.");
                BContinua = false;
            }

            if (Convert.ToDouble(txtCantidad.NumericText) <= 0 && BContinua)
            {
                General.msjAviso("Debe capturar una cantidad mayor a cero, verifique porfavor.");
                BContinua = false;
            }

            return BContinua;
        }

        private void GenerarReglonesVacios(int Reglones)
        {
            for (int i = 1; i < Reglones; i++)
            {
                f.WriteLine("");
            }
        }

        private void CrearArchivo()
        {
            sFile = Application.StartupPath + @"\Cheque.txt";

            try
            {
                if (File.Exists(sFile))
                {
                    File.Delete(sFile);
                }
                f = new StreamWriter(sFile, true);

                GenerarReglonesVacios(4);

                f.WriteLine(Fg.PonFormato("", " ", 85)
                              + dtpFechaRegistro.Value.Year + "-" + dtpFechaRegistro.Value.Month + "-" + dtpFechaRegistro.Value.Day);
                GenerarReglonesVacios(3);
                string sCadena = (lblBeneficiario.Text + txtCantidad.Text);
                int iLargo = sCadena.Length;
                iLargo = 95 - iLargo;
                
                f.WriteLine("  " + lblBeneficiario.Text + Fg.PonFormato("", " ", iLargo) + "$" + txtCantidad.Text);
                GenerarReglonesVacios(2);
                f.WriteLine("  " +General.LetraMoneda(Convert.ToDouble(txtCantidad.Text)));
                f.Close();
                //General.AbrirDocumento(sFile);
                Printing();
            }
            catch { f.Close();  }
            
        }

        public void Printing()
        {
            try
            {
                streamToPrint = new StreamReader(sFile);
                try
                {
                    printFont = new Font("Arial", 10);
                    PrintDocument pd = new PrintDocument();
                    pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                    // Specify the printer to use.
                    PrintDocument docToPrint = new PrintDocument();
                    PrintDialog t = new PrintDialog();
                    pd.DocumentName = sFile;


                    if (t.ShowDialog() == DialogResult.OK)
                    {                        

                        if (pd.PrinterSettings.IsValid)
                        {
                            pd.Print();
                        }
                        else
                        {
                            MessageBox.Show("Printer is invalid.");
                        }
                    }
                }
                finally
                {
                    streamToPrint.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;
            String line = null;

            // calcula el numero de lineas por página.
            linesPerPage = ev.MarginBounds.Height /
               printFont.GetHeight(ev.Graphics);

            
            while (count < linesPerPage &&
               ((line = streamToPrint.ReadLine()) != null))
            {
                yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, Brushes.Black,
                   leftMargin, yPos, new StringFormat());
                count++;
            }

            // si existen mas lineas son para la siguiente página
            if (line != null)
                ev.HasMorePages = true;
            else
                ev.HasMorePages = false;
        }

        #endregion Funciones y Procedimientos
    }
}
