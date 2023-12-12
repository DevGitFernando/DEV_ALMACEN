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

using DllFarmaciaSoft; 

namespace DllTransferenciaSoft.Configuraciones
{
    public partial class FrmConfigRespaldosBD : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        FolderBrowserDialog Folder = new FolderBrowserDialog();
        string sSP = "", sTabla = "";

        public FrmConfigRespaldosBD()
        {
            InitializeComponent();
            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(Transferencia.DatosApp, this.Name);

            cboIntervalos.Clear();
            cboIntervalos.Add("0", "<< Seleccione >>");
            cboIntervalos.Add("1", "Minutos");
            cboIntervalos.Add("2", "Horas");
            cboIntervalos.SelectedIndex = 0;

            this.Height = 235;
            this.Width = 625; 

        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles();
            CargarConfiguracion();
            txtNombreServidor.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int iServicioActivo = chkServicioHabilitado.Checked ? 1 : 0;
            int iEnvioFTP = Convert.ToInt32(chkEnvioFTP.Checked);

            int iLunes = Convert.ToInt32(chkLunes.Checked);
            int iMartes = Convert.ToInt32(chkMartes.Checked);
            int iMiercoles = Convert.ToInt32(chkMiercoles.Checked);
            int iJueves = Convert.ToInt32(chkJueves.Checked);
            int iViernes = Convert.ToInt32(chkViernes.Checked);
            int iSabado = Convert.ToInt32(chkSabado.Checked);
            int iDomingo = Convert.ToInt32(chkDomingo.Checked);
            string sHoraInicio = General.FechaHora(dtpHoraInicial.Value);
            string sHoraFin = General.FechaHora(dtpHoraFinal.Value); 

            if (validarDatos())
            {
                if (!cnn.Abrir())
                {
                    General.msjErrorAlAbrirConexion(); 
                }
                else 
                {
                    cnn.IniciarTransaccion();
                    string sSql = "";
                    sSql = string.Format("Exec {0} ", sSP);

                    sSql += string.Format("  @NombreServidor = '{0}', @RutaRespaldos = '{1}', " + 
                        " @RutaArchivosSistema = '{2}', @Tiempo = '{3}', @TipoTiempo = '{4}', @ServicioHabilitado = '{5}', " + 
                        " @EnvioFTP_Habilitado = '{6}', @bLunes = '{7}', @bMartes = '{8}', @bMiercoles = '{9}', " + 
                        " @bJueves = '{10}', @bViernes = '{11}', @bSabado = '{12}', @bDomingo = '{13}', " + 
                        " @HoraInicia = '{14}', @HoraFinaliza = '{15}' ",
                        txtNombreServidor.Text, txtRutaRespaldos.Text, txtRutaSistema.Text, 
                        upDownCada.Value, cboIntervalos.Data, iServicioActivo, 
                        iEnvioFTP, iLunes, iMartes, iMiercoles, iJueves, iViernes, iSabado, iDomingo,
                        sHoraInicio, sHoraFin);

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
            }
        }

        private bool validarDatos()
        {
            bool bRegresa = true;

            ////if (txtNombreServidor.Text.Trim() == "")
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("No ha capturado el Nombre Publico del Servidor, verifique.");
            ////    txtNombreServidor.Focus();
            ////}

            if (bRegresa && txtRutaRespaldos.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado la ruta de respaldos, verifique.");
                txtRutaRespaldos.Focus();
            }

            if (bRegresa && cboIntervalos.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccioado un intervalo válido, verifique.");
                cboIntervalos.Focus();
            }

            return bRegresa;
        }

        #endregion Botones 

        private void FrmConfigRespaldosBD_Load(object sender, EventArgs e)
        {
            if (Transferencia.ServicioEnEjecucion == TipoServicio.Cliente)
            {
                this.Text = "Configuración de respaldos de Base de Datos Punto de Venta";
                sSP = " spp_Net_CFGC_Respaldos ";
                sTabla = " Net_CFGC_Respaldos ";
            }

            if (Transferencia.ServicioEnEjecucion == TipoServicio.OficinaCentralRegional)
            {
                this.Text = "Configuración de respaldos de Base de Datos Oficina Central Reginal";
                sSP = " spp_Net_CFGS_Respaldos ";
                sTabla = " Net_CFGS_Respaldos ";
            }

            if (Transferencia.ServicioEnEjecucion == TipoServicio.OficinaCentral)
            {
                this.Text = "Configuración de respaldos de Base de Datos Oficina Central";
                sSP = " spp_Net_CFGSC_Respaldos ";
                sTabla = " Net_CFGSC_Respaldos ";
            } 

            btnNuevo_Click(null, null);
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
                    txtRutaSistema.Text = leer.Campo("RutaDeArchivosDeSistema"); 
                    txtNombreServidor.Text = leer.Campo("PublicNameServer");
                    txtRutaRespaldos.Text = leer.Campo("RutaDeRespaldos");
                    cboIntervalos.Data = leer.Campo("TipoTiempo");
                    chkServicioHabilitado.Checked = leer.CampoBool("ServicioHabilitado");


                    chkEnvioFTP.Checked = leer.CampoBool("EnvioFTP_Habilitado");
                    chkLunes.Checked = leer.CampoBool("bLunes");
                    chkMartes.Checked = leer.CampoBool("bMartes");
                    chkMiercoles.Checked = leer.CampoBool("bMiercoles");
                    chkJueves.Checked = leer.CampoBool("bJueves");
                    chkViernes.Checked = leer.CampoBool("bViernes");
                    chkSabado.Checked = leer.CampoBool("bSabado");
                    chkDomingo.Checked = leer.CampoBool("bDomingo");

                    dtpHoraInicial.Value = leer.CampoFecha("HoraInicia");
                    dtpHoraFinal.Value = leer.CampoFecha("HoraFinaliza"); 

                    try
                    {
                        upDownCada.Value = leer.CampoInt("Tiempo");
                    }
                    catch { }
                }
            }
        }

        private void cboIntervalos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iValorMin = 1, iValorMax = 59;
            TipoTiempo eTipo = (TipoTiempo)Convert.ToInt32(cboIntervalos.Data);

            //if (eTipo == TipoTiempo.Segundos)
            //    iValorMin = 30;
            //else 
            if (eTipo == TipoTiempo.Minutos)
            {
                iValorMin = 20;
                iValorMax = 59; 
            }
            else if (eTipo == TipoTiempo.Horas)
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
                txtRutaRespaldos.Text = Folder.SelectedPath;
            }
        }

        private void btnRutaSistema_Click(object sender, EventArgs e)
        {
            Folder.ShowDialog();
            if (Folder.SelectedPath != "")
            {
                txtRutaSistema.Text = Folder.SelectedPath;
            }
        } 
    }
}
