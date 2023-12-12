using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;

using DllFarmaciaSoft;
using DllTransferenciaSoft.IntegrarInformacion; 

namespace DllTransferenciaSoft.Configuraciones
{
    public partial class FrmConfigIntegrarInformacion : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        FolderBrowserDialog Folder = new FolderBrowserDialog();


        string sSP = "", sTabla = "";

        public FrmConfigIntegrarInformacion()
        {
            InitializeComponent();
            leer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(Transferencia.Modulo, Transferencia.Version, this.Name);

            cboIntervalos.Clear();
            cboIntervalos.Add("0", "<< Seleccione >>");
            cboIntervalos.Add("1", "Segundos");
            cboIntervalos.Add("2", "Minutos");
            cboIntervalos.Add("3", "Horas");
            cboIntervalos.SelectedIndex = 0;

        }

        private void FrmConfigIntegrarInformacion_Load(object sender, EventArgs e)
        {
            ////if (DtGeneral.ConfiguracionOficinaCentral)
            ////{
            ////    this.Text = "Configuración de integración de información Oficina Central";
            ////    sSP = " spp_CFGS_ConfigurarIntegracion ";
            ////    sTabla = " CFGS_ConfigurarIntegracion ";
            ////}
            ////else
            ////{
            ////    this.Text = "Configuración de integración de información Punto de Venta";
            ////    sSP = " spp_CFGC_ConfigurarIntegracion ";
            ////    sTabla = " CFGC_ConfigurarIntegracion ";
            ////}


            if (Transferencia.ServicioEnEjecucion == TipoServicio.Cliente)
            {
                this.Text = "Configuración de integración de información Punto de Venta";
                sSP = " spp_CFGC_ConfigurarIntegracion ";
                sTabla = " CFGC_ConfigurarIntegracion ";
            }

            if (Transferencia.ServicioEnEjecucion == TipoServicio.ClienteOficinaCentralRegional)
            {
                this.Text = "Configuración de integración de información Cliente Regional";
                sSP = " spp_CFGCR_ConfigurarIntegracion ";
                sTabla = " CFGCR_ConfigurarIntegracion ";
            }

            if (Transferencia.ServicioEnEjecucion == TipoServicio.OficinaCentral)
            {
                this.Text = "Configuración de integración de información Oficina Central";
                sSP = " spp_CFGSC_ConfigurarIntegracion ";
                sTabla = " CFGSC_ConfigurarIntegracion ";
            }

            if (Transferencia.ServicioEnEjecucion == TipoServicio.OficinaCentralRegional)
            {
                this.Text = "Configuración de integración de información Oficina Central Regional";
                sSP = " spp_CFGS_ConfigurarIntegracion ";
                sTabla = " CFGS_ConfigurarIntegracion ";
            }

            btnNuevo_Click(null, null);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int iServicioActivo = chkServicioHabilitado.Checked ? 1 : 0;

            if (validarDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();
                    string sSql = string.Format("Exec {0} '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}' ", 
                        sSP, txtRutaRecibidos.Text, txtIntegrados.Text, chkFechaTerminacion.Checked.ToString(),
                        General.FechaYMD(dtpFechaTerminacion.Value), upDownCada.Value, cboIntervalos.Data, iServicioActivo);

                    if (!leer.Exec(sSql))
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al guardar la información.");
                    }
                    else
                    {
                        cnn.CompletarTransaccion();
                        General.msjUser("Información guardada satisfactoriamente.");
                    }
                    cnn.Cerrar();
                }
                else
                {
                    General.msjAviso("No se pudo establecer conexión con el servidor, intente de nuevo.");
                }
            }
        }

        private bool validarDatos()
        {
            bool bRegresa = true;

            if (txtRutaRecibidos.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado la ruta de archivos recibidos, verifique.");
                txtRutaRecibidos.Focus();
            }

            if (bRegresa && txtIntegrados.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado la ruta de archivos integrados, verifique.");
                txtIntegrados.Focus();
            }


            return bRegresa;
        }

        private void CargarConfiguracion()
        {
            string sSql = string.Format(" Select * From {0} (NoLock) ", sTabla);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarConfiguracion()");
            }
            else
            {
                if (leer.Leer())
                {
                    txtRutaRecibidos.Text = leer.Campo("RutaArchivosRecibidos");
                    txtIntegrados.Text = leer.Campo("RutaArchivosIntegrados");
                    chkFechaTerminacion.Checked = leer.CampoBool("bFechaTerminacion");
                    dtpFechaTerminacion.Value = leer.CampoFecha("FechaTerminacion");
                    cboIntervalos.Data = leer.Campo("TipoTiempo");
                    chkServicioHabilitado.Checked = leer.CampoBool("ServicioHabilitado"); 

                    try
                    {
                        upDownCada.Value = leer.CampoInt("Tiempo");
                    }
                    catch { }
                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles();
            CargarConfiguracion();
            txtRutaRecibidos.Focus();
        }

        private void cboIntervalos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iValorMin = 1, iValorMax = 59;
            TiempoIntegracion eTipo = (TiempoIntegracion)Convert.ToInt32(cboIntervalos.Data);

            if (eTipo == TiempoIntegracion.Segundos)
                iValorMin = 30;
            else if (eTipo == TiempoIntegracion.Minutos)
                iValorMax = 59;
            else if (eTipo == TiempoIntegracion.Horas)
                iValorMax = 23;
            else 
            {
                iValorMin = 0;
                iValorMax = 0;
            }

            upDownCada.Minimum = iValorMin;
            upDownCada.Maximum = iValorMax;
        }

        private void cmdRutaRecibidos_Click(object sender, EventArgs e)
        {
            Folder.ShowDialog();
            if (Folder.SelectedPath != "")
            {
                txtRutaRecibidos.Text = Folder.SelectedPath;
            }
        }

        private void cmdRutaIntegracion_Click(object sender, EventArgs e)
        {
            Folder.ShowDialog();
            if (Folder.SelectedPath != "")
            {
                txtIntegrados.Text = Folder.SelectedPath;
            }
        }

        private void btnIntegrarPaquetesDeDatos_Click(object sender, EventArgs e)
        {
            FrmIntegrarPaquetesDeDatos f = new FrmIntegrarPaquetesDeDatos();
            f.ShowDialog(this); 
        }

    }
}
