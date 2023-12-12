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
//using SC_SolutionsSystem.Reportes;
//using SC_SolutionsSystem.Errores;

//using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;

namespace Dll_SII_IRFID.Registro
{
    public partial class FrmRegistroDePresalida : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid myGrid;
        clsConsultas Consulta; 

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sFolioMovimiento = ""; 


        public FrmRegistroDePresalida()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Consulta = new clsConsultas(General.DatosConexion, Gn_RFID.DatosApp, this.Name); 

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            myGrid.BackColorColsBlk = Color.White;
        }

        private void FrmRegistroDePresalida_Load(object sender, EventArgs e)
        {
            InicializarPantalla();
        }

        private void FrmRegistroDePresalida_Shown(object sender, EventArgs e)
        {
            if (!DtGeneral.EsModuloUnidosis())
            {
                this.Close();
            }
        }

        private void FrmRegistroDePresalida_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    Agregar();
                    break;

                default:
                    break; 
            }
        }

        #region Botones 
        private void InicializarPantalla()
        {
            Fg.IniciaControles();
            IniciarToolBar();

            dtpFechaRegistro.Enabled = false; 
            myGrid.Limpiar(false);
            txtFolioMovto.Focus();
        }

        private void IniciarToolBar()
        {
            IniciarToolBar(false, false); 
        }

        private void IniciarToolBar(bool Guardar, bool Agregar)
        {
            btnGuardar.Enabled = Guardar;
            btnAgregar.Enabled = Agregar; 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarPantalla(); 
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Agregar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bRegresa = true;
            string sSql;

            if (myGrid.Rows == 0)
            {
                General.msjAviso("Debe Capturar almenos una relación, Verifique.");
            }
            else
            {
                if (!cnn.Abrir())
                {
                    General.msjErrorAlAbrirConexion();
                }
                else
                {
                    cnn.IniciarTransaccion();

                    bRegresa = GuardarInformacion(); 

                    ////for (int iRow = 1; iRow <= myGrid.Rows && bRegresa; iRow++)
                    ////{
                    ////    sSql = string.Format("Exec spp_Mtto_RFID_Tags_UUIDS @TAG = '{0}', @UUID = '{1}'",
                    ////        myGrid.GetValue(iRow, 1), myGrid.GetValue(iRow, 2));

                    ////    if (!leer.Exec(sSql))
                    ////    {
                    ////        bRegresa = false;
                    ////    }
                    ////}

                    if (!bRegresa)
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click()");
                        General.msjError("Ocurrió un error al guardar la información.");
                    }
                    else
                    {
                        cnn.CompletarTransaccion();
                        General.msjUser("Información guardada satisfactoriamente.");
                        InicializarPantalla(); 
                    }

                    cnn.Cerrar();
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones

        #region Funciones Y Procedimientos  
        private void Agregar()
        {
            if (txtTAG.Text == "" )
            {
                General.msjUser("No ha capturado un TAG valido, verifique.");
            }
            else
            {
                if (myGrid.BuscaRepetido(txtTAG.Text, myGrid.Rows, 1))
                {
                    General.msjUser("El TAG ya fue capturado en otro renglón, verifique.");
                    ////myGrid.LimpiarRenglon(myGrid.Rows);
                    ////myGrid.Rows -= 1;
                }
                else
                {
                    myGrid.Rows += 1;
                    myGrid.SetValue(myGrid.Rows, 1, txtTAG.Text);
                }
                txtTAG.Text = "";
                txtTAG.Focus();
            }
        }

        #endregion Funciones Y Procedimientos

        #region Guardar Informacion
        private bool GuardarInformacion()
        {
            bool bRegresa = false;

            bRegresa = Guardar_001_Encabezado();

            return bRegresa; 
        }

        private bool Guardar_001_Encabezado()
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_Mtto_RFID_BitacoraMovimientos_Enc " +
                "@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio = '{3}', @TipoDeMovto = '{4}', @IdPersonalRegistra = '{5}', @Opcion = '{6}'", 
                sEmpresa, sEstado, sFarmacia, "*", 1, DtGeneral.IdPersonal, 1);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                if (leer.Leer())
                {
                    sFolioMovimiento = leer.Campo("Clave");
                    bRegresa = true;
                }
            }

            //spp_Mtto_RFID_BitacoraMovimientos_Enc

            if (bRegresa)
            {
                bRegresa = Guardar_002_Detalle(); 
            }

            return bRegresa;
        }


        private bool Guardar_002_Detalle()
        {
            bool bRegresa = true;
            string sSql = "";
            string sTAG = ""; 

            for (int i = 1; i <= myGrid.Rows; i++) 
            {
                sTAG = myGrid.GetValue(i, 1);

                sSql = string.Format("Exec spp_Mtto_RFID_BitacoraMovimientos_Det   " + 
                    " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio = '{3}', @TAG = '{4}', @Opcion = '{5}'   ",
                     sEmpresa, sEstado, sFarmacia, sFolioMovimiento, sTAG, 1);

                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    break; 
                }
            }

            return bRegresa;
        }
        #endregion Guardar Informacion 

        #region Informacion 
        private void txtFolioMovto_Validating(object sender, CancelEventArgs e)
        {
            if (txtFolioMovto.Text.Trim() == "" || txtFolioMovto.Text.Trim() == "*")
            {
                txtFolioMovto.Enabled = false;
                txtFolioMovto.Text = "*";
                IniciarToolBar(true, true); 
                txtTAG.Focus(); 
            }
            else
            {
                leer.DataSetClase = Consulta.RFID_Movimientos(sEmpresa, sEstado, sFarmacia, txtFolioMovto.Text.Trim(), "txtFolioMovto_Validating");
                if (!leer.Leer())
                {
                    txtFolioMovto.Text = "";
                    txtFolioMovto.Focus();
                }
                else
                {
                    IniciarToolBar(false, false); 
                    Get_001_Encabezado(); 
                }
            }
        }

        private bool Get_001_Encabezado()
        {
            bool bRegresa = true; 
            string sSql = "";

            txtFolioMovto.Enabled = false;
            txtFolioMovto.Text = leer.Campo("Folio");
            dtpFechaRegistro.Value = leer.CampoFecha("FechaRegistro");


            if (bRegresa)
            {
                bRegresa = Get_002_Detalles(); 
            }

            return bRegresa;
        }

        private bool Get_002_Detalles()
        {
            bool bRegresa = false;
            string sSql = "";
            leer.DataSetClase = Consulta.RFID_MovimientosDetalles(sEmpresa, sEstado, sFarmacia, txtFolioMovto.Text.Trim(), "Get_002_Detalles()");

            if (leer.Leer())
            {
                myGrid.LlenarGrid(leer.DataSetClase); 
            }

            return bRegresa;
        }
        #endregion Informacion
    }
}
