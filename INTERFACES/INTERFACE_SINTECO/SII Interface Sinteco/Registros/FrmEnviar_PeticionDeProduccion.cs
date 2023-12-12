using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos;
using DllFarmaciaSoft.Usuarios_y_Permisos;



namespace SII_Interface_Sinteco.Registros
{
    public partial class FrmEnviar_PeticionDeProduccion : FrmBaseExt 
    {
        private enum Cols
        {
            Ninguna = 0,
            Codigo = 1, CodigoEAN = 2, IdSubFarmacia = 3, SubFarmacia = 4, ClaveLote = 5, MesesCaducar = 6, FechaEntrada = 7,
            FechaCaducidad = 8, Status = 9, Existencia = 10, FechaCaducidadMaxima = 11
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsAyudas Ayuda;
        clsConsultas Consulta;
        clsGrid grid; 

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        bool bEnvioHabilitado = false;
        string sMensaje_XML = "";
        string sGUID = "";
        bool bInformacion_Correcta = false;
        bool bMensajeEnviado = false;
        DateTime dtFechaLote = DateTime.Now; 

        public FrmEnviar_PeticionDeProduccion()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Ayuda = new clsAyudas(General.DatosConexion, Gn_ISINTECO.DatosApp, this.Name);
            Consulta = new clsConsultas(General.DatosConexion, Gn_ISINTECO.DatosApp, this.Name);
            Error = new clsGrabarError(General.DatosConexion, Gn_ISINTECO.DatosApp, this.Name);

            grid = new clsGrid(ref grdLotes, this);

            chkSoloExistencias.BackColor = General.BackColorBarraMenu; 


            cboCaractesSeparador.Clear();
            cboCaractesSeparador.Add("/", "   /   ");
            cboCaractesSeparador.Add("_", "   _   ");
            cboCaractesSeparador.SelectedIndex = 0; 
        }

        private void FrmEnviar_PeticionDeProduccion_Load(object sender, EventArgs e)
        {
            Gn_ISINTECO.Interface_SINTECO.GetConfiguracion();
            bEnvioHabilitado = Gn_ISINTECO.Interface_SINTECO.EnvioHabilitado;

            btnEnviarPeticion.Enabled = bEnvioHabilitado;

            InicializarPantalla();

            tmXML.Enabled = true;
            tmXML.Interval = 1000;
            tmXML.Start(); 
        }

        #region Botones 
        private void InicializarPantalla()
        {
            bInformacion_Correcta = false;

            cboAditamentos.Clear();
            cboAditamentos.Add("", "NO EXISTEN ADITAMENTOS");


            grid.Limpiar(true); 
            Fg.IniciaControles();
            dtpFechaCaducidad.Enabled = false;


            lblMensaje_SINTECO.Text = ""; 

            txtCodigoEAN.Focus(); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarPantalla(); 
        }

        private void btnEnviarPeticion_Click(object sender, EventArgs e)
        {
            bMensajeEnviado = false;

            if (!bInformacion_Correcta)
            {
                General.msjUser("El XML no esta completo, verifique.");
            }
            else
            {
                bMensajeEnviado = Gn_ISINTECO.Interface_SINTECO.EnviarMensaje(sMensaje_XML);

                if (bMensajeEnviado)
                {
                    General.msjUser("Mensaje enviado satisfactoriamente.");
                }
                else
                {
                    General.msjError(Gn_ISINTECO.Interface_SINTECO.MensajeError); 
                }
            }
        }

        private void btnAditamentos_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones

        #region Funciones y Procedimientos Privados 
        private string GenerarXML()
        {
            string sRegresa = "";
            string sBIS = chkLoteBIS.Checked ? "[-]" : "";
            string sClaveLote = lblClaveLote.Text + sBIS;
            string sAditamento = "";


            if (cboAditamentos.SelectedIndex != 0)
            {
                sAditamento = string.Format("{0}{1}", cboAditamentos.Data, cboCaractesSeparador.Data); 
            }

            int iCantidad = Convert.ToInt32(nmCantidad.Value);
            string sParte_01_02 = string.Format("{0}{1}{2}TE{3}", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, Fg.PonCeros(txtReferencia.Text.Trim(), 8));

            string sEncabezado = string.Format(@"<sinteco_message message_id={1}{0}{1} xmlns={1}http://www.sintecorobotics.com/namelocation{1}>", sGUID, Fg.Comillas());
            string sParte_01 = string.Format("  <restock_in_transit restock_code={3}{0}{3} delivery_code={3}{0}{3} quantity_on_route={3}{1}{3} priority={3}{2}{3}>", sParte_01_02, iCantidad, "N", Fg.Comillas());
            string sParte_Componentes = "";

            sParte_Componentes = "      <components>" + "\n";
            sParte_Componentes += string.Format("       <medication medication_code={5}{7}{0}{6}{1}{5} medication_pharmacy_code={5}{2}{5} medication_lot_number={5}{3}{5} medication_exp_date={5}{4}{5}/> \n",
                lblClaveSSA.Text, txtRegistroSanitario.Text.Trim(), lblClaveSSA.Text, sClaveLote,
                General.FechaYMD(dtpFechaCaducidad.Value, "-"), Fg.Comillas(), cboCaractesSeparador.Data, sAditamento);
            sParte_Componentes += "     </components>"; 


            sMensaje_XML = sEncabezado + "\n";
            sMensaje_XML += sParte_01 + "\n";
            ////sMensaje_XML += sParte_01_02 + "\n";
            sMensaje_XML += sParte_Componentes + "\n";
            sMensaje_XML += "   </restock_in_transit>" + "\n";
            sMensaje_XML += "</sinteco_message>" + "\n";

            //sMensaje_XML = sRegresa;
            sRegresa = sMensaje_XML;


            lblMensaje_SINTECO.Text = sMensaje_XML;



            bInformacion_Correcta = true;
            if (lblClaveSSA.Text == "" && bInformacion_Correcta)
            {
                bInformacion_Correcta = false;
            }

            if (txtRegistroSanitario.Text.Trim() == "" && bInformacion_Correcta)
            {
                bInformacion_Correcta = false;
            }

            if (lblClaveLote.Text.Trim() == "" && bInformacion_Correcta)
            {
                bInformacion_Correcta = false;
            }

            if (lblClaveLote.Text.Trim() == "" && bInformacion_Correcta)
            {
                bInformacion_Correcta = false;
            }

            if (txtReferencia.Text.Trim() == "" && bInformacion_Correcta)
            {
                bInformacion_Correcta = false;
            }

            if (nmCantidad.Value <= 0 && bInformacion_Correcta)
            {
                bInformacion_Correcta = false;
            }

            return sRegresa; 
        }
        #endregion Funciones y Procedimientos Privados

        #region Buscar Producto 
        private void txtCodigoEAN_Validating(object sender, CancelEventArgs e)
        {
            string sIdProducto = Fg.PonCeros(txtCodigoEAN.Text.Trim(), 8);
            string sCodigoEAN_Seleccionado = "";

            if (txtCodigoEAN.Text.Trim() != "")
            {
                if (!GnFarmacia.ValidarSeleccionCodigoEAN(sIdProducto, ref sCodigoEAN_Seleccionado))
                {
                    btnNuevo_Click(null, null);
                }
                else
                {
                    leer.DataSetClase = Consulta.ProductosFarmacia(sEmpresa, sEstado, sFarmacia, sCodigoEAN_Seleccionado, "txtId_Validating");
                    if (leer.Leer())
                    {
                        CargaDatos();
                    }
                    else
                    {
                        btnNuevo_Click(null, null);
                    }
                }
            }
        }

        private void CargaDatos()
        {
            txtCodigoEAN.Enabled = false;
            txtCodigoEAN.Text = leer.Campo("CodigoEAN");
            lblDescripcion.Text = leer.Campo("Descripcion");
            lblClaveSSA.Text = leer.Campo("ClaveSSA");
            lblDescripcionClaveSSA.Text = leer.Campo("DescripcionSal");
            lblLaboratorio.Text = leer.Campo("Laboratorio");

            sGUID = Guid.NewGuid().ToString();

            Buscar_Aditamentos(); 

            Buscar_Lotes();
        }

        private void txtCodigoEAN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.ProductosFarmacia(sEmpresa, sEstado, sFarmacia, "txtId_KeyDown");

                if (leer.Leer())
                {
                    CargaDatos();
                }
            }
        }

        private bool Buscar_Aditamentos()
        {
            bool bRegresa = false;
            string sSql = "";
            string sCodigo = txtCodigoEAN.Text.Trim();
            string sCodEAN = txtCodigoEAN.Text.Trim();

            cboAditamentos.Clear();

            sSql = string.Format("Select * From SINTECO_CFG_Productos_Aditamentos (NoLock) Where CodigoEAN = '{0}' and Status = 'A' ", sCodEAN);
            if (!leer.Exec(sSql))
            {
                General.msjError("Ocurrió un error al obtener la lista de Adimamentos del Producto.");
            }
            else
            {
                if (!leer.Leer())
                {
                    cboAditamentos.Add("", "NO EXISTEN ADITAMENTOS");
                }
                else
                {
                    cboAditamentos.Add("", "<< SELECCIONE ADITAMENTO >>");
                    cboAditamentos.Add(leer.DataSetClase, true, "ClaveAditamento", "DescripcionAditamento");
                }
            }

            cboAditamentos.SelectedIndex = 0; 

            return bRegresa; 
        }

        private bool Buscar_Lotes()
        {
            bool bRegresa = false;
            string sSql = "";
            string sCodigo = txtCodigoEAN.Text.Trim();
            string sCodEAN = txtCodigoEAN.Text.Trim();
            string sFiltroExistencia = chkSoloExistencias.Checked ? "   And cast(L.Existencia as Int) > 0   " : " ";  

            sSql = string.Format("Set DateFormat YMD Select L.IdProducto, L.CodigoEAN, L.IdSubFarmacia, F.Descripcion As SubFarmacia, L.ClaveLote, " +
                   " datediff(mm, getdate(), L.FechaCaducidad) as MesesCad, " +
                   " Convert( varchar(10), L.FechaRegistro, 120) as FechaReg, Convert(varchar(10), L.FechaCaducidad, 120 ) as FechaCad, " +
                   " (Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, cast(L.Existencia as Int) as Existencia, " +
                   " Convert( varchar(10), ( DateAdd( month, 2, L.FechaCaducidad) ), 120 ) as FechaCadMaxima " +
                   " From FarmaciaProductos_CodigoEAN_Lotes L (NoLock) " +
                   " Inner join CatFarmacias_SubFarmacias F (NoLock) On ( L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and L.IdSubFarmacia = F.IdSubFarmacia ) " +
                   " Where L.IdEstado =  '{0}' and L.IdFarmacia = '{1}' and L.IdProducto = '{2}' and L.CodigoEAN = '{3}' ", 
                   sEstado, sFarmacia, sCodigo, sCodEAN);

            sSql = string.Format("Set DateFormat YMD Select L.IdProducto, L.CodigoEAN, L.IdSubFarmacia, F.Descripcion As SubFarmacia, L.ClaveLote, " +
                    " datediff(mm, getdate(), L.FechaCaducidad) as MesesCad, " +
                    " Convert( varchar(10), L.FechaRegistro, 120) as FechaReg, Convert(varchar(10), L.FechaCaducidad, 120 ) as FechaCad, " +
                    " (Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, cast(L.Existencia as Int) as Existencia, " +
                    " Convert( varchar(10), ( DateAdd( month, 2, L.FechaCaducidad) ), 120 ) as FechaCadMaxima " +
                    " From FarmaciaProductos_CodigoEAN_Lotes L (NoLock) " +
                    " Inner join CatFarmacias_SubFarmacias F (NoLock) On ( L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and L.IdSubFarmacia = F.IdSubFarmacia ) " +
                    " Where L.IdEstado =  '{0}' and L.IdFarmacia = '{1}' and L.CodigoEAN = '{2}'  {3} ",
                    sEstado, sFarmacia, sCodEAN, sFiltroExistencia);

            lblMensaje_SINTECO.Text = ""; 
            lblSubFarmacia.Text = "";
            lblClaveLote.Text = "";
            dtpFechaCaducidad.Value = General.FechaSistema; 

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Buscar_Lotes()");
                General.msjError("Ocurrió un error al obtener el listado de lotes del producto.");
            }
            else
            {
                if (leer.Leer())
                {
                    grid.LlenarGrid(leer.DataSetClase);
                }
                else
                {
                    bRegresa = false;
                    General.msjUser("El Producto ingresado no tiene registrado Lotes, verifique.");
                    btnNuevo_Click(null, null);
                }
            }

            return bRegresa;
        }
        #endregion Buscar Producto

        private void grdLotes_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sSubFarmacia = grid.GetValue(e.Row + 1, (int)Cols.SubFarmacia);
            string sClaveLote = grid.GetValue(e.Row + 1, (int)Cols.ClaveLote);
            DateTime dFechaCaducidad = grid.GetValueFecha(e.Row + 1, (int)Cols.FechaCaducidad);

            dtFechaLote = dFechaCaducidad; 

            lblSubFarmacia.Text = sSubFarmacia;
            lblClaveLote.Text = sClaveLote;
            dtpFechaCaducidad.Value = dFechaCaducidad; 

        }

        private void chkSoloExistencias_CheckedChanged(object sender, EventArgs e)
        {
            Buscar_Lotes(); 
        }

        private void tmXML_Tick(object sender, EventArgs e)
        {
            GenerarXML(); 
        }

        private void chkLoteBIS_CheckedChanged(object sender, EventArgs e)
        {
            dtpFechaCaducidad.Enabled = chkLoteBIS.Checked;

            if (!chkLoteBIS.Checked)
            {
                dtpFechaCaducidad.Value = dtFechaLote;                 
            }
        }
    }
}
