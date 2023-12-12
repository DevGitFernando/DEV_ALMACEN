using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem.Data;
using SC_SolutionsSystem;
using SC_SolutionsSystem.FuncionesGrid;

using DllFarmaciaSoft;

namespace Almacen.ControlDistribucion
{
    public partial class FrmDocumentosConfirmacionEntrega : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer Leer, LeerAux;
        clsConsultas Consultas;

        clsGrid myGridTrasf;
        clsGrid myGridVent;
        clsGrid myGridCartas;

        bool bCancelado, bGuardado;
        bool bDesmarcar = DtGeneral.PermisosEspeciales.TienePermiso("DESMARCAR_FOLIODEVUELTO");

        public FrmDocumentosConfirmacionEntrega()
        {
            InitializeComponent();
            Leer = new clsLeer(ref cnn);
            LeerAux = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, true);

            myGridTrasf = new clsGrid(ref grdTransferencias, this);
            myGridVent = new clsGrid(ref grdVentas, this);
            myGridCartas = new clsGrid(ref grdCartas, this);

            myGridTrasf.AjustarAnchoColumnasAutomatico = true;
            myGridVent.AjustarAnchoColumnasAutomatico = true;
            myGridCartas.AjustarAnchoColumnasAutomatico = true;

            myGridTrasf.EstiloDeGrid = eModoGrid.ModoRow;
            myGridVent.EstiloDeGrid = eModoGrid.ModoRow;
            myGridCartas.EstiloDeGrid = eModoGrid.ModoRow;
        }

        private void FrmDevolucionDeDoctos_Load(object sender, EventArgs e)
        {
            CargarCombos();
            LimpiarPantalla();
        }

        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            if (txtFolio.Text.Trim() == "" || txtFolio.Text.Trim() == "*")
            {
                txtFolio.Focus();
            }
            else
            {
                Leer.DataSetClase = Consultas.RutasDistribucionEnc(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, Fg.PonCeros(txtFolio.Text, 8), "txtFolio_Validating");

                if (Leer.Leer())
                {
                    CargarDatosRuta();
                }
            }

            if (!bDesmarcar)
            {
                BloqueaCeldas();
            }
        }

        private void BloqueaCeldas()
        {
            for (int i = 1; i <= myGridTrasf.Rows; i++)
            {
                if (myGridTrasf.GetValueBool(i, 5))
                {
                    myGridTrasf.BloqueaCelda(true, i, 5);
                }
            }

            for (int i = 1; i <= myGridVent.Rows; i++)
            {
                if (myGridVent.GetValueBool(i, 5))
                {
                    myGridVent.BloqueaCelda(true, i, 5);
                }
            }

            for (int i = 1; i <= myGridCartas.Rows; i++)
            {
                if (myGridCartas.GetValueBool(i, 7))
                {
                    myGridCartas.BloqueaCelda(true, i, 7);
                }
            }
        }

        private void CargarDatosRuta()
        {
            //IniciarToolBar(false, true);
            btnGuardar.Enabled = true;
            txtFolio.Text = Leer.Campo("Folio");
            txtFolio.Enabled = false;
            cboChofer.Data = Leer.Campo("IdPersonal");
            cboChofer.Enabled = false;
            cboVehiculo.Data = Leer.Campo("IdVehiculo");
            cboVehiculo.Enabled = false;
            txtObservaciones.Text = Leer.Campo("Observaciones");
            txtObservaciones.Enabled = false;
            dtpFechaRegistro.Value = Leer.CampoFecha("FechaRegistro");

            myGridTrasf.Limpiar(false);
            myGridVent.Limpiar(false);
            myGridCartas.Limpiar(false);

            if (Leer.Campo("Status").ToUpper() == "C")
            {
                lblCancelado.Visible = true;
                btnGuardar.Enabled = true;
            }

            Leer.DataSetClase = Consultas.DevolucionDeDoctosDet(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, Fg.PonCeros(txtFolio.Text, 8), "txtFolio_Validating");

            if (Leer.Leer())
            {
                LeerAux.Reset();
                LeerAux.DataRowsClase = Leer.DataTableClase.Select("Tipo = 'T'");

                if (LeerAux.Leer())
                {
                    myGridTrasf.LlenarGrid(LeerAux.DataSetClase);
                    //myGridTrasf.BloqueaGrid(true);
                }

                LeerAux.Reset();
                LeerAux.DataRowsClase = Leer.DataTableClase.Select("Tipo = 'V'");

                if (LeerAux.Leer())
                {
                    myGridVent.LlenarGrid(LeerAux.DataSetClase);
                    //myGridVent.BloqueaGrid(true);
                }
            }

            string sSql = string.Format("Select Distinct FolioCarta, Titulo_00 As Referencia, Tipo, FolioTransferenciaVenta, " +
                    "(Case When Tipo = 'T' Then 'TS' Else 'SV' End) + FolioTransferenciaVenta As Folio, CartaDevuelta, CartaDevuelta " +
                    "From RutasDistribucionDet_CartasCanje C (NoLock) " +
                    "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioRuta = '{3}'",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, Fg.PonCeros(txtFolio.Text, 8));

            if (Leer.Exec(sSql))
            {
                if (Leer.Leer())
                {
                    myGridCartas.LlenarGrid(Leer.DataSetClase);
                }
            }
            else
            {
                Error.GrabarError(Leer, "CargarDatosRuta()");
                General.msjError("Ocurrió un error al leer la información de la cartas de canje.");
            }
        }

        //private void IniciarToolBar(bool Guardar, bool Imprimir)
        //{
        //    btnGuardar.Enabled = Guardar;
        //    btnImprimir.Enabled = Imprimir;
        //}

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }


        private void LimpiarPantalla()
        {
            Fg.IniciaControles();

            lblCancelado.Text = "CANCELADO";
            lblCancelado.Visible = false;
            dtpFechaRegistro.Enabled = false;
            txtFolio.Text = "";
            txtFolio.Focus();
            cboChofer.SelectedIndex = 0;
            cboVehiculo.SelectedIndex = 0;
            txtObservaciones.Text = "";
            myGridTrasf.Limpiar(false);
            myGridVent.Limpiar(false);
            myGridCartas.Limpiar(false);

            bCancelado = false;

            //IniciarToolBar(true, false);
            btnGuardar.Enabled = false;
        }

        private void CargarCombos()
        {
            cboChofer.Clear();
            cboChofer.Add();
            string sSql = string.Format("Select IdPersonal, IdPersonal + ' -- ' + Personal As Personal From vw_PersonalCEDIS Where IdPuesto = '{0}'", 
                Fg.PonCeros((int)Puestos_CEDIS.Chofer, 2) );

            if (Leer.Exec(sSql))
            {
                if (Leer.Leer())
                {
                    cboChofer.Add(Leer.DataSetClase, true, "IdPersonal", "Personal");
                }
            }
            else
            {
                Error.GrabarError(Leer, "CargarCombos");
                General.msjError("Ocurrió un error al obtener información de los choferes.");
            }

            cboChofer.SelectedIndex = 0;

            cboVehiculo.Clear();
            cboVehiculo.Add();
            sSql = String.Format("Select IdVehiculo, (Idvehiculo + ' -- ' + Descripcion) As Vehiculo From vw_Vehiculos Where IdEstado = '{0}' And Idfarmacia = '{1}'",
                                 DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            if (Leer.Exec(sSql))
            {
                if (Leer.Leer())
                {
                    cboVehiculo.Add(Leer.DataSetClase, true, "Idvehiculo", "Vehiculo");
                }
            }
            else
            {
                Error.GrabarError(Leer, "CargarCombos");
                General.msjError("Ocurrió un error al obtener información de los vehículo.");
            }

            cboVehiculo.SelectedIndex = 0;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!cnn.Abrir())
            {
                General.msjErrorAlAbrirConexion();
            }
            else
            {
                if (GuardarDet())
                {
                    cnn.CompletarTransaccion();
                    //if (Leer.Leer())
                    //{
                        General.msjUser("Los cambios han sido efectuados correctamente");
                        LimpiarPantalla();
                    //}
                }
                else
                {
                    cnn.DeshacerTransaccion();
                    txtFolio.Text = "*";
                    Error.GrabarError(Leer, "btnGuardar_Click");
                    General.msjError("Ocurrió un error al grabar la información de la ruta de distribución.");
                }
            }
            cnn.Cerrar();
        }

        private bool GuardarDet()
        {
            bool bRegresa = false;

            bRegresa = GuardarDetTransf();

            if (bRegresa)
            {
                bRegresa = GuardarDetVent();
            }

            if (bRegresa)
            {
                bRegresa = GuardarDetCarta();
            }
            return bRegresa;
        }

        private bool GuardarDetTransf()
        {
            bool bRegresa = true;

            string sSql = "";
            string sTransferencia = "";

            for (int i = 1; i <= myGridTrasf.Rows; i++)
            {
                sTransferencia = myGridTrasf.GetValue(i, 1);

                if (myGridTrasf.GetValueBool(i, 4) != myGridTrasf.GetValueBool(i, 5))
                {
                    if (sTransferencia != "")
                    {
                        sSql = string.Format("Update D Set FolioDevuelto = {5}, FechaDevuelto = GetDate() From RutasDistribucionDet D " +
                                "Where Tipo = 'T' And IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And " +
                                "       Folio = '{3}' And FolioTransferenciaVenta = '{4}' ",
                                             DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtFolio.Text.Trim(),
                                             sTransferencia, myGridTrasf.GetValueInt(i, 5));
                        if (!Leer.Exec(sSql))
                        {
                            bRegresa = false;
                            break;
                        }
                    }
                }
            }
            return bRegresa;
        }

        private bool GuardarDetVent()
        {
            bool bRegresa = true;

            string sSql = "";
            string sVenta = "";

            for (int i = 1; i <= myGridVent.Rows; i++)
            {
                sVenta = myGridVent.GetValue(i, 1);

                if (myGridVent.GetValueBool(i, 4) != myGridVent.GetValueBool(i, 5))
                {
                    if (sVenta != "")
                    {
                        sSql = string.Format("Update D Set FolioDevuelto = {5}, FechaDevuelto = GetDate() From RutasDistribucionDet D " +
                                "Where Tipo = 'V' And IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And " +
                                "       Folio = '{3}' And FolioTransferenciaVenta = '{4}' ",
                                             DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtFolio.Text.Trim(),
                                             sVenta, myGridVent.GetValueInt(i, 5));
                        if (!Leer.Exec(sSql))
                        {
                            bRegresa = false;
                            break;
                        }
                    }
                }
            }
            return bRegresa;
        }

        private bool GuardarDetCarta()
        {
            bool bRegresa = true;

            string sSql = "";
            string sCarta = "";
            string sTipo = "";
            string SFolioVT = "";

            for (int i = 1; i <= myGridCartas.Rows; i++)
            {
                sCarta = myGridCartas.GetValue(i, 1);
                sTipo = myGridCartas.GetValue(i, 3);
                SFolioVT = myGridCartas.GetValue(i, 4);

                if (myGridCartas.GetValueBool(i, 6) != myGridCartas.GetValueBool(i, 7))
                {
                    if (sCarta != "")
                    {
                        sSql = string.Format("Update D Set CartaDevuelta = {7}, FechaDev = GetDate() From RutasDistribucionDet_CartasCanje D " +
                                "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And " +
                                "       FolioRuta = '{3}' And FolioCarta = '{4}' And FolioTransferenciaVenta = '{5}' And Tipo = '{6}'",
                                             DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtFolio.Text.Trim(),
                                             sCarta, myGridCartas.GetValue(i, 4), myGridCartas.GetValue(i, 3), myGridCartas.GetValueInt(i, 7));
                        if (!Leer.Exec(sSql))
                        {
                            bRegresa = false;
                            break;
                        }
                    }
                }
            }
            return bRegresa;
        }
    }
}
