﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;

using DllFarmaciaSoft;
using DllTransferenciaSoft.ObtenerInformacion;
using DllFarmaciaSoft.Web;

using DllTransferenciaSoft.Servicio; 

namespace DllTransferenciaSoft.Configuraciones
{
    public partial class FrmConfigurarConexionClientesRegionales : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas query;
        clsAyudas Ayuda;

        string sError = "Ocurrió un error al guardar la información.";
        string sExito = "Información guardada satisfactoriamente.";

        Thread _workerThread; 

        public FrmConfigurarConexionClientesRegionales()
        {
            InitializeComponent();
            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, "ConfigurarROC", this.Name, Application.ProductVersion);
            Ayuda = new clsAyudas(General.DatosConexion, "ConfigurarROC", this.Name, Application.ProductVersion);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(Transferencia.Modulo, Transferencia.Version, this.Name);

            CheckForIllegalCrossThreadCalls = false; 
        }

        private void InicarPantalla()
        {
            btnCancelar.Enabled = true;
            Fg.IniciaControles(this, true);
            txtIdEstado.Focus();
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicarPantalla();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Grabar("A");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string sSql = string.Format("Update CFGSC_ConfigurarConexiones Set Status = 'C' Where IdEstado = '{0}' and IdFarmacia = '{1}' ", Fg.PonCeros(txtIdEstado.Text, 2), Fg.PonCeros(txtIdFarmacia.Text, 4));

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "btnCancelar_Click");
                General.msjError("Ocurrió un error al cancelar la información.");
            }
            else
            {
                General.msjUser("Información cancelada satisfactoriamente.");
                btnNuevo_Click(null, null);
            }

        }

        private void btnTestConexion_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                _workerThread = new Thread(this.TestConexion);
                _workerThread.Name = "Probar conexión";
                _workerThread.Start(); 
            }
        }

        private void TestConexion()
        {
            string sDireccion = "";
            string sSSL = chkSSL.Checked ? "s" : ""; 

            toolStripBarraMenu.Enabled = false;
            FrameInfUnidad.Enabled = false;
            FrameDatosConexion.Enabled = false;


            sDireccion = string.Format("http{0}://{1}/{2}/{3}.asmx", sSSL, txtServidor.Text.Trim(), txtWebService.Text.Trim(), txtPagina.Text.Trim());  
            
            FrmTestConexion frm = new FrmTestConexion(sDireccion);
            frm.ShowDialog();

            toolStripBarraMenu.Enabled = true;
            FrameInfUnidad.Enabled = true;
            FrameDatosConexion.Enabled = true;
            _workerThread = null; 
        }
        #endregion Botones

        private void FrmConfigurarConexionClientesRegionales_Load(object sender, EventArgs e)
        {
            InicarPantalla();
        }

        private void txtIdEstado_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdEstado.Text.Trim() != "")
            {
                //string sSql = string.Format("Select * From CatEstados (NoLock) Where IdEstado = '{0}' ", Fg.PonCeros(txtIdEstado.Text, 2));
                leer.DataSetClase = query.Estados(txtIdEstado.Text, "txtIdEstado_Validating");
                e.Cancel = !CargarDatos(1);
            }
        }

        private bool CargarDatos(int Opcion)
        {
            bool bRegresa = true;

            if (Opcion == 1)
            {
                if (leer.Leer())
                {
                    txtIdEstado.Enabled = false;
                    txtIdEstado.Text = leer.Campo("IdEstado");
                    lblEstado.Text = leer.Campo("Nombre");
                }
                else
                {
                    bRegresa = false;
                    General.msjUser("Clave de estado no encontrada, verifique.");
                    txtIdEstado.Text = "";
                    lblEstado.Text = "";
                }
            }

            if (Opcion == 2)
            {
                if (leer.Leer())
                {
                    txtIdFarmacia.Enabled = false;
                    txtIdFarmacia.Text = leer.Campo("IdFarmacia");
                    lblFarmacia.Text = leer.Campo("Farmacia");
                    CargarDatosConfiguracion();
                }
                else
                {
                    bRegresa = false;
                    General.msjUser("La farmacia seleccionada no existes ó no pertenence al estado seleccionado, verifique.");
                    txtIdFarmacia.Text = "";
                    lblFarmacia.Text = ""; 
                }
            }

            return bRegresa;
        }

        private void CargarDatosConfiguracion()
        {
            string sSql = string.Format(" Select * From CFGSC_ConfigurarConexiones (NoLock) " + 
                " Where IdEstado = '{0}' and IdFarmacia = '{1}' ", Fg.PonCeros(txtIdEstado.Text, 2), Fg.PonCeros(txtIdFarmacia.Text, 4));
            btnCancelar.Enabled = true;

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarDatosConfiguracion()");
                General.msjError("Ocurrió un error al obtener la configuración de la farmacia.");
            }
            else
            {
                if (leer.Leer())
                {
                    txtServidor.Text = leer.Campo("Servidor");
                    txtWebService.Text = leer.Campo("WebService");
                    txtPagina.Text = leer.Campo("PaginaWeb");
                    chkSSL.Checked = leer.CampoBool("SSL");

                    if (leer.Campo("Status").ToUpper() == "C")
                    {
                        btnCancelar.Enabled = false;
                        General.msjUser("La configuración de la farmacia seleccionada actualmente se encuentra cancelada.");
                    }
                }
            }
        }

        private void txtIdFarmacia_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdFarmacia.Text.Trim() != "")
            {
                //string sSql = string.Format("Select * From CatFarmacias (NoLock) Where IdEstado = '{0}' and IdFarmacia = '{1}' ", Fg.PonCeros(txtIdEstado.Text, 2), Fg.PonCeros(txtIdFarmacia.Text,4));
                leer.DataSetClase = query.Farmacias(txtIdEstado.Text, txtIdFarmacia.Text, "txtIdFarmacia_Validating");
                e.Cancel = !CargarDatos(2);
            }
        }

        private void txtIdEstado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Estados("txtIdEstado_KeyDown");
                if (Ayuda.ExistenDatos)
                {
                    CargarDatos(1);
                }
            }
        }

        private void txtIdFarmacia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Farmacias("txtIdFarmacia_KeyDown", txtIdEstado.Text);
                if (Ayuda.ExistenDatos)
                {
                    CargarDatos(2);
                }
            }
        }

        private bool validarDatos()
        {
            bool bRegresa = true;

            if (txtIdEstado.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha especificado el Estado, verifique."); 
                txtIdEstado.Focus(); 
            }

            if (txtIdFarmacia.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha especificado la Unidad, verifique.");
                txtIdFarmacia.Focus(); 
            }

            if (txtServidor.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha especificado el Servidor destino, verifique."); 
                txtServidor.Focus(); 
            }

            if (txtWebService.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha especificado el Servicio Web destino, verifique.");
                txtWebService.Focus();
            }

            if (txtPagina.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha especificado la Pagina destino, verifique.");
                txtPagina.Focus();
            } 

            return bRegresa; 
        }

        private void Grabar(string Status)
        {
            int iSSL = chkSSL.Checked ? 1 : 0;
            string sSql = string.Format("Exec spp_CFGSC_ConfigurarConexiones  " + 
                " @IdEstado = '{0}', @IdFarmacia = '{1}', @Servidor = '{2}', @WebService = '{3}', @PaginaWeb = '{4}', @SSL = '{5}', @Status = '{6}' ", 
                Fg.PonCeros(txtIdEstado.Text,2), Fg.PonCeros(txtIdFarmacia.Text, 4),
                txtServidor.Text, txtWebService.Text, txtPagina.Text, iSSL, Status.ToUpper());

            if (validarDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();
                    if (!leer.Exec(sSql))
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "Grabar");
                        General.msjError(sError);
                    }
                    else
                    {
                        cnn.CompletarTransaccion();
                        General.msjUser(sExito);
                        btnNuevo_Click(null, null);
                    }
                    cnn.Cerrar();
                }
            }
        }

        private void btnEnviarInformacion_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                if (General.msjConfirmar("Toda la información de los catálogos y configuraciones se enviaran a la unidad seleccionada,\n\n¿ Desea continuar ?") == DialogResult.Yes)
                {
                    toolStripBarraMenu.Enabled = false;
                    FrameInfUnidad.Enabled = false;
                    FrameDatosConexion.Enabled = false;

                    Thread.Sleep(500);
                    _workerThread = new Thread(this.ReplicarInformacion_General);
                    _workerThread.Name = "ReplicarInformacion";
                    _workerThread.Start();
                }
            }
        }

        private void btnEnviarInformacionEstado_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                if (General.msjConfirmar("Toda la información de los catálogos y configuraciones se generara solo para la unidad seleccionada,\n\n¿ Desea continuar ?") == DialogResult.Yes)
                {
                    toolStripBarraMenu.Enabled = false;
                    FrameInfUnidad.Enabled = false;
                    FrameDatosConexion.Enabled = false;

                    Thread.Sleep(500);
                    _workerThread = new Thread(this.ReplicarInformacion_EstadoEspecifico);
                    _workerThread.Name = "ReplicarInformacion";
                    _workerThread.Start();
                }
            }
        }

        private void ReplicarInformacion_General()
        {
            ReplicarInformacion(false); 
        }

        private void ReplicarInformacion_EstadoEspecifico()
        {
            ReplicarInformacion(true); 
        }

        private void ReplicarInformacion(bool SoloEstadoEspecifico)
        {
            clsOficinaCentral cliente = new clsOficinaCentral(DtGeneral.CfgIniOficinaCentral, General.DatosConexion,
                DtGeneral.IdOficinaCentral, DtGeneral.IdFarmaciaCentral, txtIdEstado.Text, txtIdFarmacia.Text);

            cliente.SoloEstadoEspecificado = SoloEstadoEspecifico; 
            cliente.GenerarArchivos(); 
            cliente.EnviarArchivos(); 
            if (cliente.ResultadoEnvio)
            {
                General.msjUser("Información enviada satisfactoriamente.");
            }
            else
            {
                General.msjAviso("La información se generó satisfactoriamente, pero no fue posible enviarla a la Unidad.");
            }

            toolStripBarraMenu.Enabled = true;
            FrameInfUnidad.Enabled = true;
            FrameDatosConexion.Enabled = true;
            _workerThread = null; 
        }

        private void FrmConfigurarConexionClientesRegionales_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F12:
                    AbrirQueryAnalizer(); 
                    break; 

                default:
                    break; 
            }
        }

        private void AbrirQueryAnalizer()
        {
            string sDireccion = "http://" + txtServidor.Text.Trim() + "/" + txtWebService.Text.Trim() + "/" + txtPagina.Text.Trim() + ".asmx";
            FrmExecWebService exec = new FrmExecWebService(sDireccion, "Configuracion");

            exec.MdiParent = this.MdiParent; 
            exec.Show(); 
        }

        private void FrmConfigurarConexionClientesRegionales_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_workerThread != null)
            {
                _workerThread.Suspend();
                _workerThread = null; 
            }
        }

        private void btnActivarServicios_Click(object sender, EventArgs e)
        {
            FrmSvrRemoto f = new FrmSvrRemoto();
            f.ShowDialog(); 
        }

    }
}
