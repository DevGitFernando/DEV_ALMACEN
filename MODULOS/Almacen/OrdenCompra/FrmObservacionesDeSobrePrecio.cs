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

namespace Almacen.OrdenCompra
{
    public partial class FrmObservacionesDeSobrePrecio : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer Leer;
        clsGrid myGrid;
        public DataSet dtsMotivosEnc = new DataSet();
        public DataSet dtsMotivosDet = new DataSet();

        private bool bContinuar = false;
        private string sIdProducto = "", sCodigoEAN = "", sDescripcion = "", sObservaciones = "";
        private double dPrecioCaja = 0.0000, dPrecioReferencia = 0.0000;

        private string sIdEmpresa = "", sIdEstado = "", sIdFarmacia = "", sFolioOrden = "";

        private enum Cols { clave = 1, Descripción = 2, Seleccion = 3 }

        public FrmObservacionesDeSobrePrecio(DataRow[] RowsDet, DataRow[] RowsEnc)
        {
            InitializeComponent();
            Leer = new clsLeer(ref cnn);
            myGrid = new clsGrid(ref grdMotivos, this);
            CargarMotivos(RowsDet, RowsEnc);
        }

        private void FrmObservacionesDeSobrePrecio_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(this, null);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtObservaciones.Text = "";
            lblCodigo.Text = sIdProducto;
            lblCodigoEAN.Text = sCodigoEAN;
            lblArticulo.Text = sDescripcion;
            lblDiferencia.Text = dPrecioCaja.ToString("###,###,##0.###0");
            lblPorcentaje.Text = dPrecioReferencia.ToString("###,###,##0.###0");
            txtObservaciones.Text = sObservaciones;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string Status;

            if (ValidaDatos())
            {
                dtsMotivosDet = clsMotivosSobreCompra.PreparaDtsMotivosDet();

                for (int i = 1; i <= myGrid.Rows; i++)
                {
                    Status = "C";
                    if (myGrid.GetValueBool(i, (int)Cols.Seleccion))
                    {
                        Status = "A";
                    }
                    object[] objRowDet = {
                                    sIdEmpresa,  
                                    sIdEstado,
                                    sIdFarmacia,
                                    sFolioOrden,
                                    sIdProducto,
                                    sCodigoEAN,
                                    myGrid.GetValue(i, (int)Cols.clave),  
                                    myGrid.GetValue(i, (int)Cols.Descripción),  
                                    Status
                                  };
                    dtsMotivosDet.Tables[0].Rows.Add(objRowDet);
                }

                dtsMotivosEnc = clsMotivosSobreCompra.PreparaDtsMotivosEnc();

                object[] objRowEnc = {
                                    sIdEmpresa,  
                                    sIdEstado,
                                    sIdFarmacia,
                                    sFolioOrden,
                                    sIdProducto,
                                    sCodigoEAN,
                                    dPrecioCaja,
                                    dPrecioReferencia,
                                    txtObservaciones.Text.Trim()
                                  };
                dtsMotivosEnc.Tables[0].Rows.Add(objRowEnc);

                bContinuar = true;
            }

            if (bContinuar)
            {
                this.Close();
            }
        }

        private void CargarMotivos(DataRow[] RowsDet, DataRow[] RowsEnc)
        {
            Leer.DataRowsClase = RowsDet;
            myGrid.Rows = 0;
            while (Leer.Leer())
            {
                myGrid.Rows += 1;
                myGrid.SetValue(myGrid.Rows, (int)Cols.clave, Leer.Campo("IdMotivoSobrePrecio"));
                myGrid.SetValue(myGrid.Rows, (int)Cols.Descripción, Leer.Campo("Descripcion"));
                
                if (Leer.Campo("Status") == "A")
                {
                    myGrid.SetValue(myGrid.Rows, (int)Cols.Seleccion, true);
                }
            }

            Leer.DataRowsClase = RowsEnc;
            Leer.Leer();

            sIdEmpresa = Leer.Campo("IdEmpresa");
            sIdEstado = Leer.Campo("IdEstado");
            sIdFarmacia = Leer.Campo("IdFarmacia");
            sFolioOrden = Leer.Campo("FolioOrden");
            sIdProducto = Leer.Campo("IdProducto");
            sCodigoEAN = Leer.Campo("CodigoEAN");
            dPrecioCaja = Leer.CampoDouble("PrecioCaja");
            dPrecioReferencia = Leer.CampoDouble("PrecioReferencia");
            sObservaciones = Leer.Campo("Observaciones");
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtObservaciones.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado las observaciones, verifique.");
                txtObservaciones.Focus();
            }
            if (bRegresa)
            {
                bool bContinua = false;
                for (int iRow = 1; !bContinua && iRow <= myGrid.Rows; iRow++)
                {
                    bContinua = myGrid.GetValueBool(iRow, (int)Cols.Seleccion);
                }

                if (!bContinua)
                {
                    bRegresa = false;
                    General.msjUser("No ha seleccionado al menos un motivo de sobre precio, verifique.");
                }
            }

            return bRegresa;
        }

        #region PropiedadesPublicas

        public string Descripcion
        {
            get { return sDescripcion; }
            set { sDescripcion = value; }
        }

        public bool Continuar
        {
            get { return bContinuar; }
            set { bContinuar = value; }
        }

        #endregion PropiedadesPublicas
    }
}
