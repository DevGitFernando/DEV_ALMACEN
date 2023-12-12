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
    public partial class FrmConfigurarConexionChecador : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas query;
        clsAyudas Ayuda;

       
        public FrmConfigurarConexionChecador()
        {
            InitializeComponent();
            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, "ConfigurarConexionChecador", this.Name, Application.ProductVersion);
            Ayuda = new clsAyudas(General.DatosConexion, "ConfigurarConexionChecador", this.Name, Application.ProductVersion);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(Transferencia.Modulo, Transferencia.Version, this.Name);
        }

        private void FrmConfigurarConexionChecador_Load(object sender, EventArgs e)
        {            
            btnNuevo_Click(null, null);            
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Grabar(1);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Grabar(2);
        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            CargarDatos();
            txtServidor.Focus();
        }

        private void CargarDatos()
        {
            leer.DataSetClase = query.Url_ChecadorUnidad("CargarDatos");

            if (leer.Leer())
            {
                txtServidor.Text = leer.Campo("Servidor");
                txtWebService.Text = leer.Campo("WebService");
                txtPagina.Text = leer.Campo("PaginaWeb");
            }
        }
        #endregion Funciones

        #region GuardaInformacion
        private bool validarDatos()
        {
            bool bRegresa = true;
                        
            if (txtServidor.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha especificado el Servidor, verifique.");
                txtServidor.Focus();
            }

            if (txtWebService.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha especificado el Servicio Web, verifique.");
                txtWebService.Focus();
            }

            if (txtPagina.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha especificado la Pagina Web, verifique.");
                txtPagina.Focus();
            }

            return bRegresa;
        }

        private void Grabar(int iOpcion)
        {
            string sSql = string.Format("Exec spp_Mtto_CFGC_Checador '{0}', '{1}', '{2}', '{3}' ",
                txtServidor.Text, txtWebService.Text, txtPagina.Text, iOpcion);

            if (validarDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();
                    if (!leer.Exec(sSql))
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "Grabar");
                        General.msjError("Ocurrió un error al guardar la información.");
                    }
                    else
                    {
                        cnn.CompletarTransaccion();
                        General.msjUser("Información guardada satisfactoriamente.");
                        btnNuevo_Click(null, null);
                    }
                    cnn.Cerrar();
                }
            }
        }
        #endregion GuardaInformacion
    }
}
