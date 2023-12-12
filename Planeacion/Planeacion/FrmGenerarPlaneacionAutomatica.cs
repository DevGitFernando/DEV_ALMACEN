using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;


namespace Planeacion.ObtenerInformacion
{
    public partial class FrmGenerarPlaneacionAutomatica : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsConsultas Consultas;
        clsLeer leer;

        int iMeses_A_Promediar = 0;
        int imeses_Caducidad = 0;

        public FrmGenerarPlaneacionAutomatica(int Meses_A_Promediar, int meses_Caducidad)
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            iMeses_A_Promediar = Meses_A_Promediar;
            imeses_Caducidad = meses_Caducidad;

        }

        private void FrmRegistroDePlaneacion_Load(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void limpiar()
        {
            Fg.IniciaControles();

            nmAlta.Value = 3;
            nmAlta.Maximum = 6;

            nmMedia.Value = 2;

            nmBaja.Value = 1;
            nmBaja.Minimum = 0;

            nmDiasStockSeguridasd.Value = 15;
            lblMesesCaducidad.Text = imeses_Caducidad.ToString();
            lblMeses.Text = iMeses_A_Promediar.ToString();

        }


        private void btnAutorizar_Click(object sender, EventArgs e)
        {
            //Guardar(true);

            if (ValidaDatos())
            {
                Generar();
            }
        }

        private bool ValidaDatos()
        {
            bool bReregresa = true;

            if (General.msjConfirmar("Esta seguro de realizar el pedido con los parametros establecidos?") == DialogResult.No)
            {
                bReregresa = false;
            }


            return bReregresa;
            
        }


        private void Generar()
        {
            bool bContinua = false;

            string sFolio = "";

            string sSql = string.Format(" Exec spp_PLN_OCEN_GenerarPedido @IdEstado = '{0}',  @Meses_A_Promediar = {1}, " +
                                        " @Meses_Stock_AltaRotacion = {2} , @Meses_Stock_MediaRotacion = {3}, @Meses_Stock_BajaRotacion = {4}, @Dias_StockSeguridad = {5}, @IdPersonalGenera = '{6}', @Meses_Caducidad = '{7}'",
                        DtGeneral.EstadoConectado, iMeses_A_Promediar,
                        nmAlta.Text, nmMedia.Text, nmBaja.Text, nmDiasStockSeguridasd.Text, DtGeneral.IdPersonal, lblMesesCaducidad.Text);

            if (!cnn.Abrir())
            {
                Error.LogError(cnn.MensajeError);
                General.msjErrorAlAbrirConexion();
            }
            else
            {
                cnn.IniciarTransaccion();

                if (leer.Exec(sSql))
                {

                    if (leer.Leer())
                    {
                        bContinua = true;
                        sFolio = leer.Campo("Folio");
                    }
                }

                if (bContinua)
                {
                    cnn.CompletarTransaccion();
                    General.msjUser("Información guardada exitosamente con el folio: " + sFolio);
                    this.Close();
                }
                else
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(leer, "btnGuardar_Click");
                    General.msjError("Ocurrio un error al guardar la información.");

                }

                cnn.Cerrar();
            }
        }

        private void nmAlta_ValueChanged(object sender, EventArgs e)
        {
            nmMedia.Maximum = nmAlta.Value;
            nmBaja.Maximum = nmAlta.Value;
        }

        private void nmMedia_ValueChanged(object sender, EventArgs e)
        {
            nmAlta.Minimum = nmMedia.Value;
            nmBaja.Maximum = nmMedia.Value;
        }

        private void nmBaja_ValueChanged(object sender, EventArgs e)
        {
            nmAlta.Minimum = nmBaja.Value;
            nmMedia.Minimum = nmBaja.Value;
        }
    }
}
