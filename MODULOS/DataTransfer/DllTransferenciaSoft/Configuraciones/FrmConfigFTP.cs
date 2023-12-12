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

namespace DllTransferenciaSoft.Configuraciones
{
    public partial class FrmConfigFTP : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        FolderBrowserDialog Folder = new FolderBrowserDialog();


        string sSP = "", sTabla = ""; 

        public FrmConfigFTP()
        {
            InitializeComponent();
            leer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(Transferencia.Modulo, Transferencia.Version, this.Name);

            cboIntervalos.Clear();
            cboIntervalos.Add("0", "<< Seleccione >>");
            // cboIntervalos.Add("1", "Segundos");
            cboIntervalos.Add("2", "Minutos");
            cboIntervalos.Add("3", "Horas");
            cboIntervalos.SelectedIndex = 0;

            sSP = " spp_CFGR_ConfigurarFTP_BDS ";
            sTabla = " CFGR_ConfigurarFTP_BDS ";

            if (Transferencia.ServicioEnEjecucion == TipoServicio.OficinaCentral)
            {
                this.Text = "Configuración directorio FTP de Oficina Central";
                sSP = " spp_CFGS_ConfigurarFTP_BDS ";
                sTabla = " CFGS_ConfigurarFTP_BDS ";
            }

            if (Transferencia.ServicioEnEjecucion == TipoServicio.OficinaCentralRegional)
            {
                this.Text = "Configuración directorio FTP de Oficina Central Regional";
                sSP = " spp_CFGR_ConfigurarFTP_BDS ";
                sTabla = " CFGR_ConfigurarFTP_BDS ";
            }
        }

        private void FrmConfigFTP_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int iHabilitado = 0; 
            if (validarDatos())
            {
                iHabilitado = chkHabilitado.Checked ? 1 : 0;
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();
                    // sSP = " spp_CFG_ConfigurarIntegracionBDS "; 
                    string sSql = string.Format("Exec {0} '{1}', '{2}', '{3}', '{4}' ",
                        sSP, txtRutaRecibidos.Text, iHabilitado, upDownCada.Value, cboIntervalos.Data);

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
                    General.msjAviso(General.MsjErrorAbrirConexion);
                }
            }
        }

        private bool validarDatos()
        {
            bool bRegresa = true;

            if (txtRutaRecibidos.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el directorio FTP del servidor, verifique.");
                txtRutaRecibidos.Focus();
            } 

            if (bRegresa && cboIntervalos.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un intervalo para la ejecución del proceso."); 
                cboIntervalos.Focus(); 
            }

            return bRegresa;
        }

        private void CargarConfiguracion() 
        { 
            // sTabla = " CFG_ConfigurarIntegracionBDS ";  
            string sSql = string.Format(" Select * From {0} (NoLock) ", sTabla); 

            if (!leer.Exec(sSql)) 
            {
                Error.GrabarError(leer, "CargarConfiguracion()"); 
            }
            else
            {
                if (leer.Leer())
                {
                    txtRutaRecibidos.Text = leer.Campo("RutaFTP_BDS_Integrar"); 
                    chkHabilitado.Checked = leer.CampoBool("Habilitado");  
                    cboIntervalos.Data = leer.Campo("TipoTiempo"); 
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

            if (eTipo == TiempoIntegracion.Minutos)
            {
                iValorMax = 59;
            }
            else if (eTipo == TiempoIntegracion.Horas)
            {
                iValorMax = 23;
            }
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
    }
}
