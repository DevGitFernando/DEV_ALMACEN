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
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Criptografia;
using SC_SolutionsSystem.FuncionesGrid; 

using DllFarmaciaSoft;



namespace OficinaCentral.Catalogos
{
    public partial class FrmCFDI_PermisosEspeciales : FrmBaseExt
    {
        enum Cols
        {
            Ninguna = 0, 
            Clave = 1, Descripcion, Status, MD5
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas consultas;

        clsGrid grid;
        string sValor = ""; 

        public FrmCFDI_PermisosEspeciales()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            consultas = new clsConsultas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name, false);

            grid = new clsGrid(ref grdFarmacias, this);
            grid.EstiloDeGrid = eModoGrid.ModoRow;
            //grdFarmacias.EditModePermanent = true;
            grdFarmacias.EditModeReplace = true;
        }

        private void FrmCFDI_PermisosEspeciales_Load(object sender, EventArgs e)
        {
            IniciarPantalla(); 
        }

        #region Botones 
        private void IniciarPantalla()
        {
            grid.Limpiar(false);
            Fg.IniciaControles();

            txtIdEstado.Focus();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            IniciarPantalla(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                GuardarInformacion(); 
            }
        }
        #endregion Botones

        #region Controles 
        private void txtIdEstado_Validated(object sender, EventArgs e)
        {
            if (txtIdEstado.Text != "")
            {
                leer.DataSetClase = consultas.Estados(txtIdEstado.Text, "txtIdEstado_Validated");
                if (leer.Leer())
                {
                    InformacionEstado(); 
                }
            }
        }

        private void InformacionEstado()
        {
            txtIdEstado.Enabled = false;
            txtIdEstado.Text = leer.Campo("IdEstado");
            lblEstado.Text = leer.Campo("Nombre");

            CargarFarmaciasRelacionadas(); 
        }

        //private void txtIdFarmacia_Validating(object sender, CancelEventArgs e)
        //{
        //    if (txtIdEstado.Text != "")
        //    {
        //        if (txtIdFarmacia.Text != "")
        //        {
        //            leer.DataSetClase = consultas.Farmacias(txtIdEstado.Text, txtIdFarmacia.Text, "txtIdEstado_Validated");
        //            if (leer.Leer())
        //            {
        //                InformacionFarmacia();
        //            }
        //        }
        //    }
        //}

        //private void InformacionFarmacia()
        //{
        //    txtIdFarmacia.Enabled = false;
        //    txtIdFarmacia.Text = leer.Campo("IdFarmacia");
        //    lblFarmacia.Text = leer.Campo("Farmacia");

        //    CargarFarmaciasRelacionadas();
        //}

        //private void CargarFarmaciasRelacionadas()
        //{
        //    leer.DataSetClase = consultas.FarmaciasRelacionadas(txtIdEstado.Text, txtIdFarmacia.Text, "CargarFarmaciasRelacionadas()");

        //    if (leer.Leer())
        //    {
        //        grid.LlenarGrid(leer.DataSetClase);
        //    }
        //    else
        //    {
        //        grid.Limpiar(true);                                                         
        //    }

        //}
        #endregion Controles

        #region Funciones y Procedimientos Privados 
        private void CargarFarmaciasRelacionadas()
        {
            leer.DataSetClase = consultas.CFDI_PermisosEspeciales(txtIdEstado.Text, "CargarFarmaciasRelacionadas()");

            if(leer.Leer())
            {
                grid.LlenarGrid(leer.DataSetClase);
            }
            else
            {
                grid.Limpiar(true);
            }

        }

        private bool validarDatos()
        {
            bool bRegresa = true;

            return bRegresa;
        }

        private bool GuardarInformacion()
        {
            bool bRegresa = false;

            if (!cnn.Abrir())
            {
                General.msjErrorAlAbrirConexion();
            }
            else
            {
                cnn.IniciarTransaccion();

                bRegresa = GuardarInformacion_Detalles(); 

                if (!bRegresa)
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(leer, "GuardarInformacion()");
                }
                else
                {
                    cnn.CompletarTransaccion();
                    General.msjUser("Información guardada satisfactoriamente.");
                    IniciarPantalla(); 
                }
                
                cnn.Cerrar();
            }

            return bRegresa;
        }

        private bool GuardarInformacion_Detalles()
        {
            bool bRegresa = true;
            string sCadena_Encriptar = "";
            //string sCadena_Encriptada = ""; 
            string sSql = "";
            string sIdFarmaciaRelacionada = "";
            string sTipo = "";
            string sStatus = ""; 
            string sMD5 = "";
            clsCriptografo crypto = new clsCriptografo(); 
          

            for (int i = 1; i <= grid.Rows; i++)
            {
                sIdFarmaciaRelacionada = "0001"; // grid.GetValue(i, (int)Cols.IdFarmacia);
                sStatus = grid.GetValueBool(i, (int)Cols.Status) ? "A" : "C";
                sTipo = grid.GetValue(i, Cols.Clave); 


                sCadena_Encriptar = string.Format("{0}|{1}|{2}|{3}", 
                    txtIdEstado.Text, sIdFarmaciaRelacionada, sStatus, Fg.PonCeros(sTipo, 4) );

                sMD5 = crypto.Encriptar(sCadena_Encriptar, 17); 


                sSql = string.Format("Exec spp_Mtto_CFG_CFDI_PermisosEspeciales " +
                    "  @IdEstado = '{0}', @IdFarmacia = '{1}', @Tipo = '{2}', @Status = '{3}', @MD5 = '{4}' ", 
                    txtIdEstado.Text, sIdFarmaciaRelacionada, sTipo, sStatus, sMD5);

                if(sIdFarmaciaRelacionada != "")
                {
                    if(!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }

            return bRegresa;
        }
        #endregion Funciones y Procedimientos Privados

        #region Grid 
        private void grdFarmacias_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if (grid.GetValue(grid.ActiveRow, (int)Cols.Clave) != "")
            {
                grid.Rows = grid.Rows + 1;
                grid.ActiveRow = grid.Rows;
                grid.SetActiveCell(grid.Rows, (int)Cols.Descripcion);
            }
        }

        private void grdFarmacias_EditModeOff(object sender, EventArgs e)
        {
            Cols colActiva = (Cols)grid.ActiveCol;

            switch (colActiva)
            {
                ////case Cols.IdFarmacia:
                ////    sValor = grid.GetValue(grid.ActiveRow, (int)Cols.IdFarmacia);
                ////    leer.DataSetClase = consultas.Farmacias(txtIdEstado.Text, sValor, "grdFarmacias_EditModeOff");
                ////    if (leer.Leer())
                ////    {
                ////        CargarInformacion_Farmacia();
                ////    }
                ////    break; 

                default:
                    break; 
            }
        }

        private void grdFarmacias_EditModeOn(object sender, EventArgs e)
        {
            sValor = grid.GetValue(grid.ActiveRow, (int)Cols.Clave); 
        }

        private void grdFarmacias_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void CargarInformacion_Farmacia()
        {
            int iRenglon = grid.ActiveRow;

            ////if(!leer.CampoBool("EsAlmacen"))
            ////{
            ////    General.msjUser("La unidad seleccionada no es Almacén.");
            ////    grid.LimpiarRenglon(iRenglon);
            ////    grid.SetActiveCell(iRenglon, (int)Cols.IdFarmacia); 
            ////}
            ////else
            ////{
            ////    grid.SetValue(iRenglon, (int)Cols.IdFarmacia, leer.Campo("IdFarmacia"));
            ////    grid.SetValue(iRenglon, (int)Cols.Farmacia, leer.Campo("Farmacia"));
            ////    grid.SetValue(iRenglon, (int)Cols.Status, 0);
            ////}
        }
        #endregion Grid

    }
}
