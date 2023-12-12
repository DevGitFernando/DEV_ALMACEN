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
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Procesos;
using DllFarmaciaSoft.Usuarios_y_Permisos;
using Farmacia.Procesos;

namespace Farmacia.TiempoAire
{
    public partial class FrmVentaTiempoAire : FrmBaseExt
    {
        private enum ColsCompanias
        {
            Ninguna = 0,
            IdCompania = 1, Descripcion = 2
        }

        private enum ColsMontos
        {
            Ninguna = 0,
            IdCompania = 1, IdMonto = 2, Descripcion = 3, Monto = 4
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer myLlenaDatos;
        clsGrid GridCompanias, GridMontos;
        clsConsultas query;
        clsAyudas ayuda;
        FrmIniciarSesionEnCaja Sesion;
        DataSet dtsCompanias;
        DataSet dtsMontosCompanias;

        FrmOperacionSupervizada f;  
        string sOperacion = "AUTORIZA_TIEMPO_AIRE_PERSONAL_INTERNO"; 

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sIdPersonalConectado = DtGeneral.IdPersonal;
        string sFechaSistema = General.FechaYMD(GnFarmacia.FechaOperacionSistema, "-");
        int iMonto = 0;

        public FrmVentaTiempoAire()
        {
            InitializeComponent();
            cnn.SetConnectionString();

            leer = new clsLeer(ref cnn);
            myLlenaDatos = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);

            GridCompanias = new clsGrid(ref grdCompanias, this);
            GridCompanias.EstiloGrid(eModoGrid.SeleccionSimple);
            GridCompanias.Limpiar(false);

            GridMontos = new clsGrid(ref grdMontos, this);
            GridMontos.EstiloGrid(eModoGrid.SeleccionSimple);
            GridMontos.Limpiar(false);

        }

        private void FrmVentaTiempoAire_Load(object sender, EventArgs e)
        {
            LlenaTipoVenta();
            btnNuevo_Click_1(null, null);

            tmAutorizado.Enabled = true;
            tmAutorizado.Start();
            tmSesion.Enabled = true;
            tmSesion.Start();
        }

        #region Inicializa 

        private void LlenaTipoVenta()
        {
            cboTipoVenta.Add("0", "<< Seleccione >>");
            cboTipoVenta.Add("1", "VENTA AL PUBLICO GENERAL");
            cboTipoVenta.Add("2", "VENTA A PERSONAL DE INTERMED");
        }

        #endregion Inicializa 

        #region Botones

        private void btnNuevo_Click_1(object sender, EventArgs e)
        {
            tmCompanias.Stop();
            tmCompanias.Enabled = false;

            query.MostrarMsjSiLeerVacio = false;
            Fg.IniciaControles(this, true);
            //grdExistencia.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.
            GridCompanias.Limpiar(false);
            GridMontos.Limpiar(false);
            txtIdPersonalTA.Enabled = false;
            txtIdAutoriza.Enabled = false;

            LlenarCompanias();
            query.MostrarMsjSiLeerVacio = true;

            //grdCompanias.Focus();
            txtCompania.Focus(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "", sFolioTA = "*", sPersonalAutoriza = "";
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion

            if (ValidaDatos())
            {
                // Si es venta al publico, el Personal que Autoriza es el Id del Personal Conectado.
                if( cboTipoVenta.Data == "1" )
                    sPersonalAutoriza = sIdPersonalConectado;
                else
                    sPersonalAutoriza = txtIdAutoriza.Text.Trim();

                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    sSql = String.Format("Exec spp_Mtto_Ventas_TiempoAire '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}' ",
                            sEmpresa, sEstado, sFarmacia, txtCompania.Text.Trim(), sFolioTA, txtIdMonto.Text.Trim(), iMonto, cboTipoVenta.Data, txtIdPersonalTA.Text.Trim(),
                            txtCelular.Text.Trim(), sIdPersonalConectado, sPersonalAutoriza, sFechaSistema, iOpcion);

                    if (leer.Exec(sSql))
                    {
                        if (leer.Leer())
                            sMensaje = String.Format("{0}", leer.Campo("Mensaje"));

                        cnn.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        btnNuevo_Click_1(null, null);
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al guardar la información.");
                        //btnNuevo_Click(null, null);

                    }

                    cnn.Cerrar();
                }
                else
                {
                    Error.LogError(cnn.MensajeError); 
                    General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo."); 
                }

            }
        }

        #endregion Botones

        #region Grid  
        private void LlenarCompanias()
        {
            leer = new clsLeer(ref cnn);

            GridCompanias.Limpiar(false);
            GridMontos.Limpiar(false);

            tmCompanias.Stop();
            tmCompanias.Enabled = false;

            leer.DataSetClase = query.CompaniasTA("LlenarCompanias");            
            if (leer.Leer())
            {
                dtsCompanias = leer.DataSetClase;
                GridCompanias.LlenarGrid(leer.DataSetClase);
                ObtenerMontosCompanias();
                LlenarMontos(1);

                tmCompanias.Start();
                tmCompanias.Enabled = true;
            }
            else
                General.msjUser("No existe información para mostrar bajo los criterios seleccionados");
            
        }

        private void ObtenerMontosCompanias()
        {
            myLlenaDatos = new clsLeer(ref cnn);

            string sSql = " Select * From CatCompaniasTA_Montos (NoLock) Where Status = 'A' Order By IdCompania";
                
            if (!myLlenaDatos.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener la información de los Montos de Tiempo Aire.");                
            }
            else
            {
                dtsMontosCompanias = myLlenaDatos.DataSetClase;
            }

        }

        private void LlenarMontos(int Renglon)
        {
            GridMontos.Limpiar(false);
            try
            {
                leer.DataSetClase = dtsMontosCompanias;
                string sCompania = GridCompanias.GetValue(Renglon, 1);
                GridMontos.AgregarRenglon(leer.DataTableClase.Select(string.Format("IdCompania = '{0}'", sCompania)), 4, false);
            }
            catch //( Exception ex )
            {
                //General.msjUser(ex.Message);
            }            
        }

        private bool buscaCompania()
        {
            bool bEncontrado = false;
            string sCompania = "";

            sCompania = Fg.PonCeros(txtCompania.Text, 2);

            for (int i = 1; i <= GridCompanias.Rows; i++)
            {
                if( sCompania == GridCompanias.GetValue(i,(int)ColsCompanias.IdCompania) )
                {
                    bEncontrado = true;
                    txtCompania.Text = GridCompanias.GetValue(i,(int)ColsCompanias.IdCompania);
                    lblDescCompania.Text = GridCompanias.GetValue(i, (int)ColsCompanias.Descripcion);
                    break;
                }
            }

            return bEncontrado;
        } 
        #endregion Grid 

        #region Buscar Personal  
        private void txtIdPersonal_Validating(object sender, CancelEventArgs e)
        {
            myLlenaDatos = new clsLeer(ref cnn);
            string sIdPersonal = Fg.PonCeros(txtIdPersonalTA.Text.Trim(), 4);

            if (txtIdPersonalTA.Text.Trim() != "")
            {
                //Se verifica que no se ingrese el numero de venta al publico si es venta a personal
                if (cboTipoVenta.Data == "2" && sIdPersonal == "0001")
                {
                    General.msjUser("La Clave de Personal no es válida para Venta a Personal Interno, verifique.");
                    txtIdPersonalTA.Focus();
                }
                else
                {
                    myLlenaDatos.DataSetClase = query.PersonalTA(txtIdPersonalTA.Text.Trim(), "txtId_Validating");
                    if (myLlenaDatos.Leer())
                        CargaDatos();
                    else
                        txtIdPersonalTA.Focus();
                }
            }
        }

        private void CargaDatos()
        {
            //Se hace de esta manera para la ayuda.
            txtIdPersonalTA.Text = myLlenaDatos.Campo("IdPersonal");
            lblPersonal.Text = myLlenaDatos.Campo("Nombre");

        }
        #endregion Buscar Personal 

        #region Eventos 
        private void txtCompania_Validating(object sender, CancelEventArgs e)
        {
            if (txtCompania.Text != "")
            {
                if (!buscaCompania())
                {
                    General.msjUser("La compañia ingresada no existe. Elija una de las compañias que se muestra en el listado");
                    txtCompania.Focus();
                }
            }
        }

        private void txtMonto_Validating(object sender, CancelEventArgs e)
        {
            myLlenaDatos = new clsLeer(ref cnn);

            if (txtIdMonto.Text != "")
            {
                myLlenaDatos.DataSetClase = query.CompaniasTA_Montos(txtCompania.Text.Trim(), txtIdMonto.Text, "txtMonto_Validating");
                {
                    if (myLlenaDatos.Leer())
                    {
                        txtIdMonto.Text = myLlenaDatos.Campo("IdMonto");
                        lblDescMonto.Text = myLlenaDatos.Campo("Descripcion");
                        iMonto = myLlenaDatos.CampoInt("Monto");
                    }
                    else                    
                        txtIdMonto.Focus();
                    
                }
            }
        }

        private void txtIdAutoriza_Validating(object sender, CancelEventArgs e)
        {

        }

        private void txtCompania_TextChanged(object sender, EventArgs e)
        {
            lblDescCompania.Text = "";
            txtIdMonto.Text = ""; 
        }

        private void txtMonto_TextChanged(object sender, EventArgs e)
        {
            lblDescMonto.Text = "";
            iMonto = 0;
        }

        private void txtIdPersonal_TextChanged(object sender, EventArgs e)
        {
            lblPersonal.Text = "";
        }

        private void cboTipoVenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtIdPersonalTA.Text = "";
            txtIdPersonalTA.Enabled = false;
            txtIdAutoriza.Enabled = false;
            lblPersonal.Text = "";            

            if (cboTipoVenta.Data == "1")
            {
                txtIdPersonalTA.Text = "0001";
                txtIdPersonal_Validating(this, null);                
            }
            else
            {
                txtIdPersonalTA.Enabled = true;
                txtIdAutoriza.Enabled = true;
            }
        }   
        #endregion Eventos

        #region Validacion de Datos

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (lblDescCompania.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Compañia inválida, verique.");
                txtCompania.Focus();                
            }

            if (bRegresa && lblDescMonto.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Monto inválida, verifique.");
                txtIdMonto.Focus();
            }

            if (bRegresa && cboTipoVenta.Data == "0")
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un Tipo de Venta");
                cboTipoVenta.Focus();
            }

            if (bRegresa && lblPersonal.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Personal para recarga inválido, verifique.");
                txtIdPersonalTA.Focus();
            }

            if (bRegresa && txtCelular.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Número de Celular inválido, verique.");
                txtCelular.Focus();
            }

            if (bRegresa && txtConfirmar.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Confirmación de Número de Celular inválido, verifique.");
                txtConfirmar.Focus();
            }

            if (bRegresa && ( txtConfirmar.Text.Trim() != txtCelular.Text.Trim() )  )
            {
                bRegresa = false;
                General.msjUser("El Número de Celular y el Número de Confirmacion no coinciden. Verifique");
                txtConfirmar.Focus();
            }

            if (bRegresa && cboTipoVenta.Data != "1")
            {
                f = new FrmOperacionSupervizada();
                f.PersonalAutoriza = DtGeneral.IdPersonal;
                f.NombreOperacion = sOperacion;
                f.ShowDialog();

                bRegresa = f.Autorizado;
                txtIdAutoriza.Text = f.PersonalAutoriza; 
            }


            return bRegresa;
        }

        #endregion Validacion de Datos

        private void tmCompanias_Tick(object sender, EventArgs e)
        {
            if (this.ActiveControl.Name.ToUpper() == grdCompanias.Name.ToUpper())
            {
                if (GridCompanias.GetValue(GridCompanias.ActiveRow, 1) != "")
                {
                    LlenarMontos(GridCompanias.ActiveRow);
                }
            }
        }

        private void tmAutorizado_Tick(object sender, EventArgs e)
        {
            tmAutorizado.Enabled = false; 
            tmAutorizado.Stop(); 

            if (DtGeneral.EsAlmacen || DtGeneral.EsEmpresaDeConsignacion)
            {
                General.msjUser("No esta autorizado para entrar a esta opcion");                
                this.Close(); 
            }
        }

        private void tmSesion_Tick(object sender, EventArgs e)
        {
            tmSesion.Enabled = false;
            FrmFechaSistema Fecha = new FrmFechaSistema();
            Fecha.ValidarFechaSistema();

            if (Fecha.Exito)
            {
                GnFarmacia.Parametros.CargarParametros();
                Fecha.Close();

                Sesion = new FrmIniciarSesionEnCaja();
                Sesion.VerificarSesion();

                if (!Sesion.AbrirVenta)
                {
                    this.Close();
                }
                else
                {
                    Sesion.Close();
                    Sesion = null;
                    btnNuevo_Click_1(null, null);
                }
            }
            else
            {
                this.Close();
            }
        }

        private void txtIdPersonalTA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLlenaDatos.DataSetClase = ayuda.PersonalTA("txtId_KeyDown"); 
                if (myLlenaDatos.Leer())
                {
                    txtIdPersonalTA.Text = myLlenaDatos.Campo("IdPersonal");
                    txtIdPersonal_Validating(null, null);
                }
            } 
        }
    }
}
