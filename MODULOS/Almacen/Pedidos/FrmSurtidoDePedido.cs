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
using SC_SolutionsSystem.Reportes;

using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;

namespace Almacen.Pedidos
{
    public partial class FrmSurtidoDePedido : FrmBaseExt
    {

        clsDatosConexion DatosDeConexion;
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas Consultas;
        clsAyudas Ayuda;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sJurisdiccion = DtGeneral.Jurisdiccion;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        public FrmSurtidoDePedido()
        {
            InitializeComponent();


            leer = new clsLeer(ref cnn);

            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            Ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);
        }

        private void FrmSurtidoDePedido_Load( object sender, EventArgs e )
        {
            //Fg.IniciaControles();
            IniciarPantalla(); 
        }

        #region Botones 
        private void IniciarPantalla()
        {
            Fg.IniciaControles();

            btnCargarSurtido.Enabled = false;
            btnEdicion.Enabled = false;
            btnTerminarEdicion.Enabled = false; 

            txtFolio_01_Pedido.Focus(); 
        }
        private void btnNuevo_Click( object sender, EventArgs e )
        {
            IniciarPantalla();
        }

        private void btnEdicion_Click( object sender, EventArgs e )
        {
            GetReserva(1);
        }
        private void btnCargarSurtido_Click( object sender, EventArgs e )
        {
            FrmActualizarSurtidoPedidos f = new FrmActualizarSurtidoPedidos();
            f.ShowInTaskbar = false;
            f.CargarPedido(txtFolio_02_Surtido.Text.Trim(), 1);

            validarSurtido(); 
        }
        private void btnTerminarEdicion_Click( object sender, EventArgs e )
        {
            GetReserva(0);
        }
        private void btnSalir_Click( object sender, EventArgs e )
        {
            this.Close();
        }

        private bool GetReserva( int Tipo )
        {
            bool bRegresa = false;
            bool bResultado = false;
            int iResultado = 0;
            string sMsjResultado = "";
            string sSql = "";
            string sFolioDeSurtido = "";

            sSql = string.Format("Exec spp_Mtto_Pedidos_Cedis___ReservarOrdenDeSurtido \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioPedido = '{3}', \n" +
                "\t@FolioSurtido = '{4}', @IdPersonal = '{5}', @Terminal = '{6}', @TipoDeProceso = '{7}' \n",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, 
                txtFolio_01_Pedido.Text.Trim(), txtFolio_02_Surtido.Text.Trim(),
                DtGeneral.IdPersonal, General.NombreEquipo, Tipo
                );


            if(!cnn.Abrir())
            {
                General.msjErrorAlAbrirConexion();
            }
            else
            {
                cnn.IniciarTransaccion();

                if(!leer.Exec(sSql))
                {
                    bRegresa = false;
                }
                else
                {
                    bRegresa = true;

                    leer.Leer();
                    bResultado = leer.CampoBool("ProcesoCorrecto");
                    iResultado = leer.CampoInt("Resultado");
                    sMsjResultado = leer.Campo("Mensaje");
                }

                if(!bRegresa)
                {
                    Error.GrabarError(leer, "Reserva");
                    cnn.DeshacerTransaccion();
                    General.msjError("Ocurrió un error al procesar el folio de surtido.");
                }
                else
                {
                    if(!bResultado)
                    {
                        cnn.DeshacerTransaccion();
                        General.msjError(sMsjResultado);
                    }
                    else
                    {
                        cnn.CompletarTransaccion();
                        General.msjUser(sMsjResultado);
                    }
                }

                cnn.Cerrar();

                validarSurtido();
            }

            return bRegresa;
        }
        #endregion Botones 

        private void txtFolio_01_Pedido_TextChanged( object sender, EventArgs e )
        {
            txtFolio_02_Surtido.Text = "";
        }

        private void txtFolio_01_Pedido_Validating( object sender, CancelEventArgs e )
        {
            if(txtFolio_01_Pedido.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Folio_Pedidos_CEDIS_Enc(sEmpresa, sEstado, sFarmacia, txtFolio_01_Pedido.Text.Trim(), "txtFolio_Validating");
                if(leer.Leer())
                {
                    txtFolio_01_Pedido.Enabled = false;
                    txtFolio_01_Pedido.Text = leer.Campo("Folio");

                    dtpFechaRegistro_01_Pedido.Enabled = false;
                    dtpFechaRegistro_01_Pedido.Value = leer.CampoFecha("FechaRegistro");
                } 
            }
        }

        private void txtFolio_02_Surtido_Validating( object sender, CancelEventArgs e )
        {
            if(txtFolio_01_Pedido.Text.Trim() != "" && txtFolio_02_Surtido.Text.Trim() != "")
            {
                validarSurtido();
            }
        }

        private void validarSurtido()
        {
            string sSql = "";
            bool bEsEditable = false;
            bool bEnEdicion = false;
            bool bEdicionBloqueada = false;

            //if(txtFolio_01_Pedido.Text.Trim() != "" && txtFolio_02_Surtido.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Folio_Pedidos__Surtidos_Enc(sEmpresa, sEstado, sFarmacia, txtFolio_02_Surtido.Text.Trim(), "txtFolio_02_Surtido_Validating");
                if(!leer.Leer())
                {
                    General.msjUser("No se encontro el folio de surtido.");
                }
                else 
                {
                    if(txtFolio_01_Pedido.Text.Trim() != leer.Campo("FolioPedido"))
                    {
                        General.msjAviso("El folio de surtido pertenece a otro Pedido.");
                        txtFolio_02_Surtido.Text = "";
                        txtFolio_02_Surtido.Focus();
                    }
                    else
                    {

                        txtFolio_02_Surtido.Enabled = false;
                        txtFolio_02_Surtido.Text = leer.Campo("FolioSurtido");

                        dtpFechaRegistro_02_Surtido.Enabled = false;
                        dtpFechaRegistro_02_Surtido.Value = leer.CampoFecha("FechaRegistro");
                        lblStatus_Surtido.Text = leer.Campo("StatusPedido");
                        btnCargarSurtido.Enabled = true;

                        if(!leer.CampoBool("HabilitadoParaSurtido"))
                        {
                            btnCargarSurtido.Enabled = false;
                        }


                        sSql = string.Format("Exec spp_Mtto_Pedidos_Cedis___Pedidos_ListaSurtidos \n" +
                            "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioPedido = '{3}', @FolioSurtido = '{4}', @IdPersonal = '{5}', @Terminal = '{6}' \n",
                            DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                            txtFolio_01_Pedido.Text, txtFolio_02_Surtido.Text.Trim(), DtGeneral.IdPersonal, General.NombreEquipo);

                        if(!leer.Exec(sSql))
                        {
                            Error.GrabarError(leer, "CargarFoliosSurtido()");
                            General.msjError("Ocurrió un error al cargar la lista de folios de surtido.");
                        }
                        else
                        {
                            btnEdicion.Enabled = false;
                            btnCargarSurtido.Enabled = false;
                            btnTerminarEdicion.Enabled = false;

                            leer.Leer(); 
                            lblEnEdicion.Text = leer.Campo("Editable");
                            lblPersonal.Text = leer.Campo("Personal");
                            lblTerminal.Text = leer.Campo("Terminal");



                            bEsEditable = leer.CampoBool("EsEditable");
                            bEnEdicion = leer.CampoBool("EnEdicion");
                            bEdicionBloqueada = leer.CampoBool("EdicionBloqueada");

                            btnEdicion.Enabled = bEsEditable;
                            btnTerminarEdicion.Enabled = bEnEdicion;

                            if(bEnEdicion)
                            {
                                btnTerminarEdicion.Enabled = !bEdicionBloqueada;
                                btnEdicion.Enabled = false;
                            }

                            if(bEsEditable && bEnEdicion)
                            {
                                btnCargarSurtido.Enabled = true;
                            }
                        }
                    }
                }
            }
        }

        private void label6_Click( object sender, EventArgs e )
        {

        }
    }
}
