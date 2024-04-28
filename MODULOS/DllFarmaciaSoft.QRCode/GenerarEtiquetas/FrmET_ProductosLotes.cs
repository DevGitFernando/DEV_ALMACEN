using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem.QRCode.Codec;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.QRCode;

using SC_SolutionsSystem.QRCode;
using SC_SolutionsSystem.SistemaOperativo; 

namespace DllFarmaciaSoft.QRCode.GenerarEtiquetas
{
    public partial class FrmET_ProductosLotes : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerInformacion;
        clsAyudas ayuda;
        clsGrid grid;

        private enum Cols
        {
            IdSubFarmacia = 1, SubFarmacia = 2, ClaveLote = 3, Caducidad = 4, Etiqueta = 5, EtiquetaSSA = 6, Cantidad = 7
        }

        QR_Reporte Impresion; // = new QR_Reporte();
        QR_Reporte ImpresionSSA; 
        DataSet dtsPosicion = new DataSet();
        DataSet dtsInformacion = new DataSet();
        DataSet dtsDetallado = new DataSet();        
        DataSet dtsReeimpresion = new DataSet();
        string sFolioGenerado = "";

        QR_Reader reader;

        bool bPosicion = false;
        bool bInformacion = false;

        bool bExisteImpresoraEtiquetas = DtGeneral.Impresoras.ExisteImpresora("etiquetas");
        // bool bExisteLector = false; // DtGeneral.Camaras.ExisteCamara("QReader");

        public FrmET_ProductosLotes()
        {
            QR_General.InicializarSDK();

            InitializeComponent();

            grid = new clsGrid(ref grdLotes, this);
            grid.AjustarAnchoColumnasAutomatico = true; 

            leer = new clsLeer(ref cnn);
            leerInformacion = new clsLeer(ref cnn);
            btnNuevo_Click(this, null);
        }

        #region Eventos
        private void txtCodigoEAN_TextChanged(object sender, EventArgs e)
        {
            lblProducto.Text = "";
            lblLaboratorio.Text = "";
            btnImprimir.Enabled = false;
        }

        private void txtCodigoEAN_Validating(object sender, CancelEventArgs e)
        {

        }

        private void txtCodigoEAN_KeyDown(object sender, KeyEventArgs e)
        {
            ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.Productos("ayuda");
                if (leer.Leer())
                {
                    txtCodigoEAN.Text = leer.Campo("CodigoEAN");
                    GetCodigoEAN();
                }
            }
            if (e.KeyCode == Keys.Enter)
            {
                GetCodigoEAN();
            }
        }

        #endregion Eventos

        private void GetCodigoEAN()
        {
            string sSql = string.Format(
                "Select IdProducto, CodigoEAN, CodigoEAN_Interno, Descripcion as NombreComercial, " +
                " ClaveSSA, DescripcionCortaClave, IdLaboratorio, Laboratorio " +
                "From vw_Productos_CodigoEAN P (NoLock) " +
                "Where CodigoEAN = '{0}' ", txtCodigoEAN.Text.Trim());

            bInformacion = false;
            if (!leerInformacion.Exec("Encavezado", sSql))
            {
                Error.GrabarError(leerInformacion, "GetCodigoEAN()");
            }
            else
            {
                if (leerInformacion.Leer())
                {
                    bInformacion = true;
                    dtsInformacion = leerInformacion.DataSetClase;

                    txtCodigoEAN.Enabled = false;

                    lblProducto.Text = leerInformacion.Campo("NombreComercial");
                    lblLaboratorio.Text = leerInformacion.Campo("Laboratorio");

                    btnImprimir.Enabled = true;
                    GetDetallado();
                }
                else
                {
                    //No se encontro CodigoEAN
                    General.msjAviso("CodigoEAN no encontrado. Favor de verfiricar.");
                }
            }
        }

        private void GetDetallado()
        {
            string sSql = string.Format(
                        "Select E.IdSubFarmacia, E.SubFarmacia, E.ClaveLote, Convert(Varchar(10), E.FechaCaducidad, 120) As FechaCaducidad " +
                        "From vw_ExistenciaPorCodigoEAN_Lotes E (NoLock) " +
                        "Where E.IdEmpresa = '{0}' And E.IdEstado = '{1}' And  E.IdFarmacia = '{2}' And E.CodigoEAN = '{3}' ",
                        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada ,txtCodigoEAN.Text.Trim());

            bInformacion = false;
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "GetDetallado()");
            }
            else
            {
                if (leer.Leer())
                {
                    bInformacion = true;
                    dtsDetallado = leer.DataSetClase;

                    grid.LlenarGrid(dtsDetallado);

                    btnImprimir.Enabled = true;
                }
                else
                {
                    //No se encontro CodigoEAN
                    General.msjAviso("Lotes no encontrados para el CodigoEAN. Favor de verfiricar.");
                }
            }
        }

        private void GetDetallado(int Row)
        {
            string sSql = string.Format(
                        "Select P.IdProducto, P.CodigoEAN, P.CodigoEAN_Interno, P.Descripcion as NombreComercial, " +
                        "P.ClaveSSA, P.DescripcionCortaClave, P.IdLaboratorio, P.Laboratorio, E.IdSubFarmacia, E.SubFarmacia, " +
                        "(Case When Left(E.ClaveLote,1) = '*' Then '1' + Right(E.ClaveLote,Len(E.ClaveLote)-1) Else '0' + E.ClaveLote End) As LoteConFormato, E.ClaveLote, " +
                        "Convert(Varchar(10), E.FechaCaducidad, 120) As FechaCaducidad " +
                        "From vw_ExistenciaPorCodigoEAN_Lotes E (NoLock) "+
                        "Inner Join vw_Productos_CodigoEAN P (NoLock) On (E.CodigoEAN = P.CodigoEAN) " +
                        "Where E.IdEmpresa = '{0}' And E.IdEstado = '{1}' And  E.IdFarmacia = '{2}' And E.CodigoEAN = '{3}' And IdSubFarmacia = '{4}' And CLaveLote = '{5}'",
                        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtCodigoEAN.Text.Trim(),
                        grid.GetValue(Row, (int)Cols.IdSubFarmacia), grid.GetValue(Row, (int)Cols.ClaveLote));

            bInformacion = false;
            if (!leerInformacion.Exec("Informacion", sSql))
            {
                Error.GrabarError(leerInformacion, "GetDetallado()");
            }
            else
            {
                if (leerInformacion.Leer())
                {
                    bInformacion = true;
                    Impresion = new QR_Reporte();
                    ImpresionSSA = new QR_Reporte();
                    Impresion.Reporte = "ET_ProductoLote";
                    ImpresionSSA.Reporte = "ET_ProductoSSA";
                    Impresion.EnviarAImpresora = !chkMostrarImpresionEnPantalla.Checked;
                    ImpresionSSA.EnviarAImpresora = !chkMostrarImpresionEnPantalla.Checked;

                    int INumDeCompias = grid.GetValueInt(Row, (int)Cols.Cantidad);
                    if (INumDeCompias < 1)
                    {
                        INumDeCompias = 1;
                    }
                    Impresion.NumeroDeCopias = INumDeCompias;
                    ImpresionSSA.NumeroDeCopias = INumDeCompias;

                    GenerarDetalles();

                    Impresion.GenerarXml();
                    ImpresionSSA.GenerarXml();
                    if (grid.GetValueBool(Row, (int)Cols.Etiqueta))
                    {
                        Impresion.Imprimir(true, false);
                    }

                    if (grid.GetValueBool(Row, (int)Cols.EtiquetaSSA))
                    {
                        ImpresionSSA.Imprimir(true, false);
                    }
                }
                else
                {
                    //No se encontro CodigoEAN
                    General.msjAviso("Lotes no encontrados para el CodigoEAN. Favor de verfiricar.");
                }
            }
        }

        private void GenerarDetalles()
        {

            if (bInformacion)
            {
                leerInformacion.Leer();
                leerInformacion.RegistroActual = 1;

                Impresion.AgregarDetalles(leerInformacion.DataSetClase);
                ImpresionSSA.AgregarDetalles(leerInformacion.DataSetClase);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles();

            grid.Limpiar(false);

            //btnGenerarEtiqueta.Enabled = bExisteImpresoraEtiquetas;
            chkMostrarImpresionEnPantalla.BackColor = General.BackColorBarraMenu;
            btnImprimir.Enabled = bExisteImpresoraEtiquetas;
            btnImprimir.Enabled = false;
            //btnReader.Enabled = bExisteLector;  

            bPosicion = false;
            bInformacion = false;
            dtsPosicion = new DataSet();
            dtsInformacion = new DataSet();
            dtsDetallado = new DataSet();

            txtCodigoEAN.Enabled = true;

            txtCodigoEAN.Focus();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            for (int Row = 1; Row <= grid.Rows; Row++)
            {
                if (grid.GetValueBool(Row, (int)Cols.Etiqueta) || grid.GetValueBool(Row, (int)Cols.EtiquetaSSA))
                {
                    GetDetallado(Row);
                }
            }
        }
    }
}
