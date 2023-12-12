using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;

using DllFarmaciaSoft;

namespace Configuracion.Configuracion
{
    public partial class FrmTerminales : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid Grid;
        clsConsultas Consultas;
        clsAyudas Ayuda;

        private enum Cols
        {
            Ninguna = 0,
            IdTerminal = 1, Nombre = 2, MAC = 3, EsServidor = 4, Habilitar = 5 
        }

        public FrmTerminales()
        {
            InitializeComponent();

            cnn.SetConnectionString();
            leer = new clsLeer(ref cnn); 
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);

            grdTerminales.EditModeReplace = false;
            Grid = new clsGrid(ref grdTerminales, this);
            Grid.EstiloGrid(eModoGrid.Normal);
        }

        private void FrmTerminales_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(true);
            Grid.Limpiar(true);
            LlenarGrid(); 
            // IniciarToolbar(true, false, false, false);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sMensaje = "Información guardada satisfactoriamente.";

            if (ValidaDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();
                    if (GuardaTerminales())
                    {
                        cnn.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al guardar la Información.");
                    }

                    cnn.Cerrar();
                }
                else
                {
                    General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
                }

            }

        }

        private bool GuardaTerminales()
        {
            bool bRegresa = true;
            string sSql = "", sTerminal = "";
            // int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion
            int iEsServidor = 0;
            string sHabilitar = "C";

            for (int i = 1; i <= Grid.Rows; i++)
            {
                sTerminal = Grid.GetValue(i, (int)Cols.IdTerminal);
                iEsServidor = 0;
                sHabilitar = "C";

                if (Grid.GetValueBool(i, (int)Cols.EsServidor))
                {
                    iEsServidor = 1;
                }

                if (Grid.GetValueBool(i, (int)Cols.Habilitar))
                {
                    sHabilitar = "A";
                }

                ////sSql = String.Format("Exec spp_Mtto_IMach_CFGC_Clientes_Terminales '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ",
                ////            txtIdCliente.Text.Trim(), sTerminal, iAsignar, iHabilitar, iVentanilla, iOpcion);
                sSql = string.Format(" Update CFGC_Terminales Set EsServidor = {1}, Status = '{2}' " + 
                    " Where IdTerminal = '{0}' ", sTerminal, iEsServidor, sHabilitar); 

                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    break;
                }
            }

            return bRegresa;
        }
        #endregion Botones

        #region Funciones
        private void IniciarToolbar(bool bNuevo, bool bGuardar, bool bCancelar, bool bImprimir)
        {
            btnNuevo.Enabled = bNuevo;
            btnGuardar.Enabled = bGuardar; 
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            ////if (txtIdCliente.Text.Trim() == "")
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("Ingrese el IdCliente por favor");
            ////    txtIdCliente.Focus();
            ////}

            ////if (bRegresa )
            ////{
            ////    bRegresa = validarCapturaTerminales();
            ////}

            return bRegresa;
        }
        #endregion Funciones 

        #region Grid
        private void LlenarGrid()
        {
            string sSql = String.Format("Select IdTerminal, Nombre, MAC_Address, EsServidor, 1 as Habilitar " +
                " From CFGC_Terminales (NoLock) " );

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener las Terminales.");
            }
            else
            {
                Grid.LlenarGrid(leer.DataSetClase, false, false);
            }

            FrameTerminales.Text = string.Format("Terminales registradas : {0}", Grid.Rows ); 
            Marcar_MAC_Local(); 
        }

        private void Marcar_MAC_Local()
        {
            string sMAC = ""; 
            for (int i = 1; i <= Grid.Rows; i++)
            {
                sMAC = Grid.GetValue(i, (int)Cols.MAC); 
                if ( sMAC == General.MacAddress ) 
                {
                    Grid.ColorRenglon(i, Color.SeaGreen  );
                    Grid.ColorRenglon(i, Color.Tan  ); 
                }
            } 
        }
        #endregion Grid

    }
}
