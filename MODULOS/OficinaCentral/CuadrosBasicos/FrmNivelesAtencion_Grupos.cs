using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;

namespace OficinaCentral.CuadrosBasicos
{
    public partial class FrmNivelesAtencion_Grupos : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsAyudas Ayuda;
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;

        clsDatosCliente DatosCliente;
        wsOficinaCentral.wsCnnOficinaCentral conexionWeb;

        string sIdEstado = "";
        string sIdCliente = "";
        int iIdNivel = 0;
        string sNombreGrupo = "";
        public bool bAceptar = false;

        #region Propiedades publicas

        public string IdEstado
        {
            get
            {
                return sIdEstado;
            }

            set
            {
                sIdEstado = value;
            }
        }

        public string IdCliente
        {
            get
            {
                return sIdCliente;
            }

            set
            {
                sIdCliente = value;
            }
        }

        public int IdNivel
        {
            get
            {
                return iIdNivel;
            }

            set
            {
                iIdNivel = value;
            }
        }

        public string NombreGrupo
        {
            get
            {
                return sNombreGrupo;
            }

            set
            {
                sNombreGrupo = value;
            }
        }
        #endregion

        public FrmNivelesAtencion_Grupos()
        {
            InitializeComponent();
            ConexionLocal.SetConnectionString();

            myLeer = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.Modulo, GnOficinaCentral.Version, this.Name);

            DatosCliente = new clsDatosCliente(GnOficinaCentral.DatosApp, this.Name, "");
            conexionWeb = new OficinaCentral.wsOficinaCentral.wsCnnOficinaCentral();
            conexionWeb.Url = General.Url;
        }

        private void FrmLaboratorios_Load(object sender, EventArgs e)
        {
            txtNombreGrupo.Text = sNombreGrupo;
            txtNombreGrupo.Focus();
        }

        #region Botones
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string sSql = "";
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion

            if (ValidaDatos())
            {
                if (ConexionLocal.Abrir())
                {
                    ConexionLocal.IniciarTransaccion();

                    sSql = String.Format("Exec spp_Mtto_CFG_CB_NivelesAtencion '{0}', '{1}', '{2}', '{3}', '{4}' ",
                            sIdEstado, sIdCliente, iIdNivel, txtNombreGrupo.Text.Trim(), iOpcion);

                    if (myLeer.Exec(sSql))
                    {
                        ConexionLocal.CompletarTransaccion();
                        this.Hide();
                        //General.msjUser(sMensaje); //Este mensaje lo genera el SP
                    }
                    else
                    {
                        ConexionLocal.DeshacerTransaccion();
                        Error.GrabarError(myLeer, "btnAceptar_Click");
                        General.msjError("Ocurrió un error al guardar la información.");

                    }

                    bAceptar = true;
                    ConexionLocal.Cerrar();
                }
                else
                {
                    General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
                }

            }

        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            bAceptar = false;
            this.Hide();
        }
        #endregion Botones

        #region Validaciones de Controles

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtNombreGrupo.Text == "")
            {
                General.msjUser("Ingrese el Nombre del Grupo por favor");
                txtNombreGrupo.Focus();
                bRegresa = false;
            }
            
            return bRegresa;
        }

        #endregion Validaciones de Controles
    
    } //Llaves de la clase
}
