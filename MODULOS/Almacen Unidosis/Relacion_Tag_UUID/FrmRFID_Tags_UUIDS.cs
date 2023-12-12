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

namespace Almacen_Unidosis.Relacion_Tag_UUID
{
    public partial class FrmRFID_Tags_UUIDS : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid myGrid;

        public FrmRFID_Tags_UUIDS()
        {
            InitializeComponent();

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            myGrid.BackColorColsBlk = Color.White;
            leer = new clsLeer(ref cnn);
        }

        private void FrmRFID_Tags_UUIDS_Load(object sender, EventArgs e)
        {
            Limpiar();
        }

        #region Botones

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
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

                    for (int iRow = 1; iRow <= myGrid.Rows && bRegresa; iRow++)
                    {
                        sSql = string.Format("Exec spp_Mtto_RFID_Tags_UUIDS @TAG = '{0}', @UUID = '{1}'",
                            myGrid.GetValue(iRow, 1), myGrid.GetValue(iRow, 2));

                        if (!leer.Exec(sSql))
                        {
                            bRegresa = false;
                        }
                    }

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
                        Limpiar();
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

        private void Limpiar()
        {
            Fg.IniciaControles();
            myGrid.Limpiar(false);
            txtTAG.Focus();
        }

        private void Agregar()
        {
            if (txtTAG.Text == "" && txtUUID.Text == "")
            {
                General.msjUser("Debe de capturar el Tag y el UUID, verifique.");
            }
            else
            {
                myGrid.Rows += 1;
                myGrid.SetValue(myGrid.Rows, 1, txtTAG.Text);
                myGrid.SetValue(myGrid.Rows, 2, txtUUID.Text);

                if (myGrid.BuscaRepetido(txtTAG.Text, myGrid.Rows, 1))
                {
                    General.msjUser("El Tag ya fue capturado en otro renglon, verifique.");
                    myGrid.LimpiarRenglon(myGrid.Rows);
                    myGrid.Rows -= 1;
                }
                else if (myGrid.BuscaRepetido(txtUUID.Text, myGrid.Rows, 2))
                {
                    General.msjUser("El UUID ya fue capturado en otro renglon, verifique.");
                    myGrid.LimpiarRenglon(myGrid.Rows);
                    myGrid.Rows -= 1;
                }
                txtTAG.Text = "";
                txtUUID.Text = "";
            }
        }

        #endregion Funciones Y Procedimientos

        private void FrmRFID_Tags_UUIDS_Shown(object sender, EventArgs e)
        {           
            if (!DtGeneral.EsModuloUnidosis())
            {
                this.Close(); 
            }
        }
    }
}
