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
    public partial class FrmConfigObtenerInformacion : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        FolderBrowserDialog Folder = new FolderBrowserDialog();

        string sSP = "", sTabla = "";

        public FrmConfigObtenerInformacion()
        {
            InitializeComponent();
            leer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(Transferencia.Modulo, Transferencia.Version, this.Name);
        }

        private void FrmConfigObtenerInformacion_Load(object sender, EventArgs e)
        {
            ////if (DtGeneral.ConfiguracionOficinaCentral)
            ////{
            ////    this.Text = "Configuración de obtención de información Oficina Central";
            ////    sSP = " spp_CFGS_ConfigurarObtencion ";
            ////    sTabla = " CFGS_ConfigurarObtencion ";
            ////}
            ////else
            ////{
            ////    this.Text = "Configuración de obtención de información Punto de Venta";
            ////    sSP = " spp_CFGC_ConfigurarObtencion ";
            ////    sTabla = " CFGC_ConfigurarObtencion ";
            ////}


            if (Transferencia.ServicioEnEjecucion == TipoServicio.Cliente)
            {
                this.Text = "Configuración de obtención de información Punto de Venta";
                sSP = " spp_CFGC_ConfigurarObtencion ";
                sTabla = " CFGC_ConfigurarObtencion ";
            }

            if (Transferencia.ServicioEnEjecucion == TipoServicio.ClienteOficinaCentralRegional)
            {
                this.Text = "Configuración de obtención de información Cliente Regional";
                sSP = " spp_CFGCR_ConfigurarObtencion ";
                sTabla = " CFGCR_ConfigurarObtencion ";
            }

            if (Transferencia.ServicioEnEjecucion == TipoServicio.OficinaCentral)
            {
                this.Text = "Configuración de obtención de información Oficina Central";
                sSP = " spp_CFGSC_ConfigurarObtencion ";
                sTabla = " CFGSC_ConfigurarObtencion ";
            }

            if (Transferencia.ServicioEnEjecucion == TipoServicio.OficinaCentralRegional)
            {
                this.Text = "Configuración de obtención de información Oficina Central Regional";
                sSP = " spp_CFGS_ConfigurarObtencion ";
                sTabla = " CFGS_ConfigurarObtencion ";
            }

            btnNuevo_Click(null, null);
        }

        private void IniciarCombos()
        {
            cboPeriocidad.Clear();
            cboPeriocidad.Add("0", "<< Seleccione >>");
            cboPeriocidad.Add("1", "Diario");
            cboPeriocidad.Add("2", "Semanalmente");
            cboPeriocidad.SelectedIndex = 0;

            cboIntervalos.Clear();
            cboIntervalos.Add("0", "<< Seleccione >>");
            cboIntervalos.Add("1", "Minutos");
            cboIntervalos.Add("2", "Horas");
            cboIntervalos.SelectedIndex = 0;

            cboUnidadPaquetes.Clear();
            cboUnidadPaquetes.Add("0", "<< Seleccione >>");
            cboUnidadPaquetes.Add("2", "Kilobytes");
            cboUnidadPaquetes.Add("3", "Megabytes");
            cboUnidadPaquetes.SelectedIndex = 0;            

        }

        #region Botones 
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int iServicioActivo = chkServicioHabilitado.Checked ? 1 : 0;
            int iTipoReplicacion = chkReplicacionPorPeriodo.Checked ? 1 : 0;
            string sSql = "";

            // @Periodicidad = '{0}', @Semanas = '{1}', @bLunes = '{2}', @bMartes = '{3}', @bMiercoles = '{4}', @bJueves = '{5}', @bViernes = '{6}', @bSabado = '{7}', @bDomingo = '{8}', @HoraEjecucion = '{9}', @bRepetirProceso = '{10}', @Tiempo = '{11}', @TipoTiempo = '{12}', @bFechaTerminacion = '{13}', @FechaTerminacion = '{14}', @RutaUbicacionArchivos = '{15}', @RutaUbicacionArchivosEnviados = '{16}', @ServicioHabilitado = '{17}', @DiasRevision = '{18}'
            sSql = string.Format("Exec {0} '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}' ",
                sSP, cboPeriocidad.Data, upDownSemanas.Value, 
                chkLunes.Checked.ToString(), chkMartes.Checked.ToString(), chkMiercoles.Checked.ToString(),
                chkJueves.Checked.ToString(), chkViernes.Checked.ToString(), chkSabado.Checked.ToString(), chkDomingo.Checked.ToString(),
                General.Hora(dtpHoraEjecucion.Value), chkRepetirProceso.Checked.ToString(), upDownCada.Value, cboIntervalos.Data,
                       chkFechaTerminacion.Checked.ToString(), General.FechaYMD(dtpFechaTerminacion.Value, "-"), 
                       txtRuta.Text.Trim(), txtRutaEnviados.Text.Trim(), iServicioActivo 
                );

            sSql = string.Format("Exec {0}  ", sSP);  
            sSql += string.Format(
                " @Periodicidad = '{0}', @Semanas = '{1}', @bLunes = '{2}', @bMartes = '{3}', @bMiercoles = '{4}', " + 
                " @bJueves = '{5}', @bViernes = '{6}', @bSabado = '{7}', @bDomingo = '{8}', @HoraEjecucion = '{9}', " + 
                " @bRepetirProceso = '{10}', @Tiempo = '{11}', @TipoTiempo = '{12}', @bFechaTerminacion = '{13}', @FechaTerminacion = '{14}', " +
                " @RutaUbicacionArchivos = '{15}', @RutaUbicacionArchivosEnviados = '{16}', @ServicioHabilitado = '{17}', " +
                " @TipoReplicacion = '{18}', @DiasRevision = '{19}', @TipoDePaquete = '{20}', @TamañoDePaquete = '{21}' ",
                cboPeriocidad.Data, upDownSemanas.Value,
                chkLunes.Checked.ToString(), chkMartes.Checked.ToString(), chkMiercoles.Checked.ToString(),
                chkJueves.Checked.ToString(), chkViernes.Checked.ToString(), chkSabado.Checked.ToString(), chkDomingo.Checked.ToString(),
                General.Hora(dtpHoraEjecucion.Value), chkRepetirProceso.Checked.ToString(), upDownCada.Value, cboIntervalos.Data,
                       chkFechaTerminacion.Checked.ToString(), General.FechaYMD(dtpFechaTerminacion.Value, "-"),
                       txtRuta.Text.Trim(), txtRutaEnviados.Text.Trim(), iServicioActivo, iTipoReplicacion, nmDiasRevision.Value, 
                       cboUnidadPaquetes.Data, nmTamañoArchivos.Value 
                );

            if (validarDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    if (!leer.Exec(sSql))
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al grabar la información.");
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
                    General.msjAviso("No fue posible establecer conexión con el servidor, intente de nuevo.");
                }
            }

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FrameRepetirProceso.Enabled = false;
            IniciarCombos();
            Fg.IniciaControles();

            lblTamaño.Text = ""; 
            CargarConfiguracion();
            upDownSemanas.Enabled = false;

            dtpHoraEjecucion.Value = Convert.ToDateTime("1900/01/01 00:01:00");
            dtpHoraEjecucion.Enabled = false;

            cboPeriocidad.Focus();

            if (Transferencia.ServicioEnEjecucion == TipoServicio.ClienteOficinaCentralRegional)
            {
                cmdConfigurarProcesoIntegracion.Enabled = false;
            }
        }
        #endregion Botones

        private bool validarDatos()
        {
            bool bRegresa = true;

            if (cboPeriocidad.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado una periodicidad válida, verifique.");
                cboPeriocidad.Focus();
            }

            if ( chkRepetirProceso.Checked )
            {
                if (bRegresa && cboIntervalos.SelectedIndex == 0)
                {
                    bRegresa = false;
                    General.msjUser("No ha seleccionado un intervalo válido, verifique.");
                    cboIntervalos.Focus();
                }
            }

            if (bRegresa && txtRuta.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("La ha especificado la ruta para los archivos generados, verifique.");
                txtRuta.Focus();
            }


            return bRegresa;
        }

        private void CargarConfiguracion()
        {
            string sSql = string.Format(" Select * From {0} (NoLock)", sTabla);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarConfiguracion()");
            }
            else
            {
                if (leer.Leer())
                {
                    cboPeriocidad.Data = leer.Campo("Periodicidad");
                    
                    upDownSemanas.Value = leer.CampoInt("Semanas");
                    chkLunes.Checked = leer.CampoBool("bLunes");
                    chkMartes.Checked = leer.CampoBool("bMartes");
                    chkMiercoles.Checked = leer.CampoBool("bMiercoles");
                    chkJueves.Checked = leer.CampoBool("bJueves");
                    chkViernes.Checked = leer.CampoBool("bViernes");
                    chkSabado.Checked = leer.CampoBool("bSabado");
                    chkDomingo.Checked = leer.CampoBool("bDomingo");

                    dtpHoraEjecucion.Value = leer.CampoFecha("HoraEjecucion");
                    chkRepetirProceso.Checked = leer.CampoBool("bRepetirProceso");
                    cboIntervalos.Data = leer.Campo("TipoTiempo");
                    cboUnidadPaquetes.Data = leer.Campo("TipoDePaquete"); 


                    try
                    {
                        upDownCada.Value = leer.CampoInt("Tiempo");
                    }
                    catch 
                    {
                        upDownCada.Value = upDownCada.Minimum; 
                    }


                    try
                    {
                        nmDiasRevision.Value = leer.CampoInt("DiasRevision"); 
                    }
                    catch 
                    {
                        nmDiasRevision.Value = nmDiasRevision.Minimum; 
                    }

                    try
                    {
                        nmTamañoArchivos.Value = leer.CampoInt("TamañoDePaquete"); 
                    }
                    catch 
                    {
                        nmTamañoArchivos.Value = nmTamañoArchivos.Minimum; 
                    }
                    Calcular_Tamaños(Convert.ToInt32(cboUnidadPaquetes.Data));


                    chkReplicacionPorPeriodo.Checked = leer.CampoBool("TipoReplicacion");
                    chkFechaTerminacion.Checked = leer.CampoBool("bFechaTerminacion");
                    dtpFechaTerminacion.Value = leer.CampoFecha("FechaTerminacion");
                    txtRuta.Text = leer.Campo("RutaUbicacionArchivos");
                    txtRutaEnviados.Text = leer.Campo("RutaUbicacionArchivosEnviados");
                    chkServicioHabilitado.Checked = leer.CampoBool("ServicioHabilitado"); 
                }
            }

        }

        private void cboPeriocidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool bChk = false;
            bool bChkEna = false;
            Peridiocidad ePer = (Peridiocidad)Convert.ToInt32(cboPeriocidad.Data);
            
            if (ePer == Peridiocidad.Diariamente)
            {
                bChk = true;
                FrameSemanalmente.Text = "Cada día";
                lblSemanasDias.Text = "Días";
            }
            else if (ePer == Peridiocidad.Semanalmente)
            {
                bChkEna = true;
                FrameSemanalmente.Text = "Semanalmente";
                lblSemanasDias.Text = "Semana el";
            }
            else
            {
                FrameSemanalmente.Text = "Configurar";
                lblSemanasDias.Text = "";
            }

            chkLunes.Checked = bChk;
            chkMartes.Checked = bChk;
            chkMiercoles.Checked = bChk;
            chkJueves.Checked = bChk;
            chkViernes.Checked = bChk;
            chkSabado.Checked = bChk;
            chkDomingo.Checked = bChk;

            chkLunes.Enabled = bChkEna;
            chkMartes.Enabled = bChkEna;
            chkMiercoles.Enabled = bChkEna;
            chkJueves.Enabled = bChkEna;
            chkViernes.Enabled = bChkEna;
            chkSabado.Enabled = bChkEna;
            chkDomingo.Enabled = bChkEna;
            upDownSemanas.Enabled = bChkEna;        


        }

        private void cboIntervalos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iValorMin = 1, iValorMax = 1;
            TipoTiempo eTipo = (TipoTiempo)Convert.ToInt32(cboIntervalos.Data);

            if (eTipo == TipoTiempo.Minutos)
            {
                iValorMax = 59;
            }
            else if (eTipo == TipoTiempo.Horas)
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

        private void chkRepetirProceso_CheckedChanged(object sender, EventArgs e)
        {
            FrameRepetirProceso.Enabled = chkRepetirProceso.Checked;
        }

        private void cmdRuta_Click(object sender, EventArgs e)
        {
            Folder.ShowDialog();
            if (Folder.SelectedPath != "")
            {
                txtRuta.Text = Folder.SelectedPath;
            }
        }

        private void cmdConfigurarProcesoIntegracion_Click(object sender, EventArgs e)
        {
            FrmConfigIntegrarInformacion f = new FrmConfigIntegrarInformacion();
            Fg.CentrarForma(f);
            f.ShowDialog();
        }

        private void cmdRutaEnviados_Click(object sender, EventArgs e)
        {
            Folder.ShowDialog();
            if (Folder.SelectedPath != "")
            {
                txtRutaEnviados.Text = Folder.SelectedPath;
            }
        }

        private void Calcular_Tamaños(int Tamaño)
        {
            int iValorMin = 1, iValorMax = 1;
            TamañoFiles eTipo = (TamañoFiles)Convert.ToInt32(Tamaño);

            lblTamaño.Text = string.Format("Mínimi 0  Máximo 0 ");
            if (eTipo == TamañoFiles.KB)
            {
                iValorMax = (1024) * 5;
            }
            else if (eTipo == TamañoFiles.MB)
            {
                iValorMax = 200;
            }
            else
            {
                iValorMin = 0;
                iValorMax = 0;
            }

            nmTamañoArchivos.Minimum = iValorMin;
            nmTamañoArchivos.Maximum = iValorMax;

            lblTamaño.Text = string.Format("Mínimimo {0}  Máximo {1}", iValorMin.ToString("###,###,###,###,###"), iValorMax.ToString("###,###,###,###,###")); 

        }

        private void cboUnidadPaquetes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Calcular_Tamaños(Convert.ToInt32(cboUnidadPaquetes.Data));
        }
    }
}
