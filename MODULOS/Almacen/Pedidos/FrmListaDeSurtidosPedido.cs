using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

// Implementacion de hilos 
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using Farmacia.Ventas;

using DllFarmaciaSoft;
using DllFarmaciaSoft.QRCode;
using DllFarmaciaSoft.QRCode.GenerarEtiquetas;
using Almacen.Pedidos.Validacion;
using Farmacia.Transferencias;
using Farmacia.EntradasConsignacion;

namespace Almacen.Pedidos
{
    public partial class FrmListaDeSurtidosPedido : FrmBaseExt 
    {
        enum Cols
        {
            Folio = 1, Fecha = 2, Status = 3, StatusPedido = 4, FolioTransferencia = 5, EsTransferencia = 6, EsManual = 7
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente datosCliente;
        clsLeer leer;
        clsConsultas query;
        clsListView lst;

        clsEtiquetasPedidosAlmacen etiquetas = new clsEtiquetasPedidosAlmacen();

        TipoDePedidoElectronico TipoPedido = TipoDePedidoElectronico.Ninguno;

        string sIdFarmacia = ""; 
        string sFarmacia = ""; 
        string sFolioPedido = ""; 
        string sFolioSurtido = "";

        string sPermisos_RegistrarSurtido = "PED_REGISTRAR_SURTIDO";
        string sPermisos_ModificarSurtido = "PED_MODIFICAR_SURTIMIENTO";
        string sPermisos_EnviarSurtido_A_Validacion = "PED_ENVIAR_SURTIDO_A_VALIDACION";
        string sPermisos_ValidarSurtimiento = "PED_VALIDAR_SURTIMIENTO";
        string sPermisos_Regresar_A_Surtimiento = "PED_REGRESAR_A_SURTIMIENTO";
        string sPermisos_GenerarSalida__Transferencia_Venta = "PED_GENERAR_SALIDA";
        string sPermisos_Enviar_A_Embarque = "PED_ENVIAR_A_EMBARQUE";

        bool bPermisos_RegistrarSurtido = false;
        bool bPermisos_ModificarSurtido = false;
        bool bPermisos_EnviarSurtido_A_Validacion = false;
        bool bPermisos_ValidarSurtimiento = false;
        bool bPermisos_Regresar_A_Surtimiento = false;
        bool bPermisos_GenerarSalida__Transferencia_Venta = false;
        bool bPermisos_Enviar_A_Embarque = false;
        bool bImplementaValidacionCiega = false;

        string sMsjTransito = "";
        bool bEsManual = false;


        public FrmListaDeSurtidosPedido(string IdFarmacia, string Farmacia, string FolioPedido)
        {
            InitializeComponent();

            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmListaDeSurtidosPedido");

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name);

            lst = new clsListView(listvwPedidos);

            sIdFarmacia = IdFarmacia;
            sFarmacia = Farmacia; 
            sFolioPedido = FolioPedido;

            SolicitarPermisosUsuario();

            sMsjTransito = "El folio de surtimiento se encuentra en Tránsito no es posible realizar cambios al surtido, verifique."; 
        }

        #region Permisos de Usuario
        /// <summary>
        /// Obtiene los privilegios para el usuario conectado 
        /// </summary>
        private void SolicitarPermisosUsuario()
        {
            ////// Valida si el usuario conectado tiene permiso sobre las opcione especiales  
            bPermisos_RegistrarSurtido = DtGeneral.PermisosEspeciales.TienePermiso(sPermisos_RegistrarSurtido); 
            bPermisos_ModificarSurtido = DtGeneral.PermisosEspeciales.TienePermiso(sPermisos_ModificarSurtido); 
            bPermisos_EnviarSurtido_A_Validacion = DtGeneral.PermisosEspeciales.TienePermiso(sPermisos_EnviarSurtido_A_Validacion); 
            bPermisos_ValidarSurtimiento = DtGeneral.PermisosEspeciales.TienePermiso(sPermisos_ValidarSurtimiento); 
            bPermisos_Regresar_A_Surtimiento = DtGeneral.PermisosEspeciales.TienePermiso(sPermisos_Regresar_A_Surtimiento); 
            bPermisos_GenerarSalida__Transferencia_Venta = DtGeneral.PermisosEspeciales.TienePermiso(sPermisos_GenerarSalida__Transferencia_Venta); 
            bPermisos_Enviar_A_Embarque = DtGeneral.PermisosEspeciales.TienePermiso(sPermisos_Enviar_A_Embarque);
            bImplementaValidacionCiega = GnFarmacia.ImplementaValidacionCiega;
        }

        private void Configurar_Menu()
        {
            bool bHabilitar = false;

            btnSurtirPedido.Enabled = bHabilitar;
            btnImprimirPedido.Enabled = bHabilitar;
            btnImprimirSurtimiento.Enabled = bHabilitar;
            btnImprimirSurtimientoCaratula.Enabled = bHabilitar;
            btnCapturarSurtido.Enabled = bHabilitar;

            btnEnviarA_EntregaValidacion.Enabled = bHabilitar; 
            btnEnviarAValidacion.Enabled = bHabilitar;

            btnImprimirValidacion.Enabled = bHabilitar;
            btnImprimirCajasSurtimiento.Enabled = bHabilitar;

            btnEnviarADocumentacion.Enabled = bHabilitar; 
            btnGenerarTransferencia.Enabled = bHabilitar;
            btnGenerarVenta.Enabled = bHabilitar;
            btnEmbarque.Enabled = bHabilitar;
            btnResurtirClavesIncompletas.Enabled = false; 


            ////if (bPermisos_MesaDeControl)
            ////{
            ////    btnSurtirPedido.Enabled = bPermisos_MesaDeControl;
            ////    btnImprimirPedido.Enabled = bPermisos_MesaDeControl;
            ////    btnImprimirSurtimiento.Enabled = bPermisos_MesaDeControl;
            ////    btnCapturarSurtido.Enabled = bPermisos_MesaDeControl;
            ////}

            ////if (bPermisos_ValidarSurtimiento)
            ////{
            ////    btnImprimirPedido.Enabled = bPermisos_ValidarSurtimiento;
            ////    btnImprimirSurtimiento.Enabled = bPermisos_ValidarSurtimiento;
            ////    btnEnviarAValidacion.Enabled = bPermisos_ValidarSurtimiento;
            ////}

            ////if (bPermisos_Transferencias)
            ////{
            ////    btnGenerarTransferencia.Enabled = false; 
            ////}


            if (DtGeneral.EsAdministrador)
            {
                bHabilitar = true; 

                btnSurtirPedido.Enabled = bHabilitar;
                btnImprimirPedido.Enabled = bHabilitar;
                btnImprimirSurtimiento.Enabled = bHabilitar;
                btnImprimirSurtimientoCaratula.Enabled = bHabilitar;
                btnImprimirCajasSurtimiento.Enabled = bHabilitar;
                btnCapturarSurtido.Enabled = bHabilitar;
                btnEnviarA_EntregaValidacion.Enabled = bHabilitar; 
                btnEnviarAValidacion.Enabled = bHabilitar;

                btnEnviarADocumentacion.Enabled = bHabilitar; 
                btnGenerarTransferencia.Enabled = bHabilitar;
                btnGenerarVenta.Enabled = bHabilitar;
                btnImprimirEtiquetas.Enabled = bHabilitar; 
            }

        }
        #endregion Permisos de Usuarios

        private void FrmListaDeSurtidosPedido_Load(object sender, EventArgs e)
        {
            Fg.IniciaControles();

            this.Text += " " + sFolioPedido;
            lblFarmacia.Text = this.sFarmacia;

            InicializarPantalla(); 
            //CargarFoliosSurtido(); 
        }
        
        #region Funciones 
        private void CargarFoliosSurtido()
        {
            // Folio = 1, Fecha = 2, Status = 3, StatusPedido = 4, FolioTransferencia = 5 

            int iTipoDePedido = 0;
            string sSql = "";

            btnEdicion.Enabled = false;
            btnTerminarEdicion.Enabled = false; 

            sSql = string.Format(
                "Select \n" +
                "\t'Folio de surtido' = FolioSurtido, FechaRegistro as Fecha, \n" +
                "\t'Status' = Status, 'Status de Pedido' = StatusPedido, \n" +
                "\t'Referencia' = FolioTransferenciaReferencia, \n" +
                "\tEsManual, EsTransferencia, IdFarmaciaPedido, TipoDePedido, TipoDePedidoDescripcion \n" +
                "From vw_PedidosCedis_Surtimiento (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmaciaPedido = '{2}' and FolioPedido = '{3}' \n" +
                "Order by FechaRegistro \n", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, sIdFarmacia, sFolioPedido);


            sSql = string.Format("Exec spp_Mtto_Pedidos_Cedis___Pedidos_ListaSurtidos \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioPedido = '{3}', @IdPersonal = '{4}', @Terminal = '{5}' \n", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, sIdFarmacia, sFolioPedido, DtGeneral.IdPersonal, General.NombreEquipo);


            lst.LimpiarItems();
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarFoliosSurtido()");
                General.msjError("Ocurrió un error al cargar la lista de folios de surtido.");
                this.Close(); 
            }
            else
            {
                if (leer.Leer())
                {
                    lst.CargarDatos(leer.DataSetClase); //, true, false);
                    lst.AnchoColumna((int)Cols.FolioTransferencia, 115);
                    //lst.AnchoColumna((int)Cols.EsTransferencia, 0);
                    //lst.AnchoColumna((int)Cols.EsManual, 0);
                    bEsManual = leer.CampoBool("EsManual");
                    iTipoDePedido = leer.CampoInt("TipoDePedido");

                    TipoPedido = (TipoDePedidoElectronico)iTipoDePedido;
                }
            }
        }

        private bool MarcarFolioParaEntregaValidacion()
        {
            string message = "Esta operación cambiará el Status del Folio de Surtido como disponible para Documentación (Generar transferencia), ¿ Desea continuar ?";
            string sFolioSurtido = lst.GetValue((int)Cols.Folio);
            string sStatus = lst.GetValue((int)Cols.Status);
            bool bRegresa = false;

            message = "El folio de surtido se enviará a Entrega Validación, ¿ Desea continuar ?";

            if (sStatus != "S")
            {
                General.msjUser("El folio de surtido no tiene registrada la captura de surtimiento, verifique.");
            }
            else
            {
                if (!cnn.Abrir())
                {
                    General.msjErrorAlAbrirConexion();
                }
                else
                {
                    if (General.msjConfirmar(message) == DialogResult.Yes)
                    {
                        cnn.IniciarTransaccion();

                        string sSql = string.Format(
                            "Update Pedidos_Cedis_Enc_Surtido Set Status = 'V' " +
                            " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmaciaPedido = '{2}' and FolioSurtido = '{3}' \n ",
                        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, sIdFarmacia, sFolioSurtido);

                        sSql += string.Format(" Exec spp_Mtto_Pedidos_Cedis_Enc_Surtido_Atenciones '{0}', '{1}', '{2}', '{3}', '{4}', '{5}'",
                            DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioSurtido, DtGeneral.IdPersonal, "");

                        if (!leer.Exec(sSql))
                        {
                            // this.Close();
                        }
                        else
                        {
                            bRegresa = true;
                        }

                        if (bRegresa)
                        {
                            cnn.CompletarTransaccion();
                            ImprimirValidacion();
                            CargarFoliosSurtido();
                            General.msjAviso("El folio de surtido ha sido enviado a entrega para validación exitosamente.");
                        }
                        else
                        {
                            Error.GrabarError(leer, "MarcarFolioParaValidacion()");
                            cnn.DeshacerTransaccion();
                            General.msjError("Ocurrió un error al actualizar el status del Folio de Surtido.");
                        }
                    }

                    cnn.Cerrar();
                }
            }
            return bRegresa;
        }

        private bool MarcarFolioParaValidacion()
        {
            string message = "Esta operación cambiará el Status del Folio de Surtido como disponible para Documentación (Generar Traspaso), ¿ Desea continuar ?";
            string sFolioSurtido = lst.GetValue((int)Cols.Folio);  
            string sStatus = lst.GetValue((int)Cols.Status);
            bool bRegresa = false;

            message = "El folio de surtido se enviará a Validación, ¿ Desea continuar ?";
            
            if (sStatus != "TV")
            {
                General.msjUser("El folio de surtido no tiene no ha sido entregado para validación, verifique.");
            }
            else
            {
                if (!cnn.Abrir())
                {
                    General.msjErrorAlAbrirConexion();
                }
                else
                {
                    if (General.msjConfirmar(message) == DialogResult.Yes)
                    {
                        cnn.IniciarTransaccion();

                        string sSql = string.Format(
                            "Update Pedidos_Cedis_Enc_Surtido Set Status = 'V' " +
                            " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmaciaPedido = '{2}' and FolioSurtido = '{3}' \n ",
                        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, sIdFarmacia, sFolioSurtido);

                        sSql += string.Format(" Exec spp_Mtto_Pedidos_Cedis_Enc_Surtido_Atenciones '{0}', '{1}', '{2}', '{3}', '{4}', '{5}'",
                            DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioSurtido, DtGeneral.IdPersonal, "");

                        if (!leer.Exec(sSql))
                        {                            
                            // this.Close();
                        }
                        else
                        {
                            bRegresa = true;
                        }

                        if (bRegresa)
                        {
                            cnn.CompletarTransaccion();
                            ImprimirValidacion();
                            CargarFoliosSurtido();
                            General.msjAviso("El folio de surtido ha sido enviado a validación exitosamente.");
                        }
                        else
                        {
                            Error.GrabarError(leer, "MarcarFolioParaValidacion()");
                            cnn.DeshacerTransaccion();
                            General.msjError("Ocurrió un error al actualizar el status del Folio de Surtido.");
                        }
                    }

                    cnn.Cerrar();
                }
            }
            return bRegresa;
        }

        private bool MarcarFolioParaEntregaDocumentacion()
        {
            string message = "Esta operación cambiará el Status del Folio de Surtido como disponible para Documentación (Generar transferencia), ¿ Desea continuar ?";
            string sFolioSurtido = lst.GetValue((int)Cols.Folio);
            string sStatus = lst.GetValue((int)Cols.Status);
            bool bRegresa = false;

            message = "El folio de surtido se enviará a Documentación, ¿ Desea continuar ?";

            if (sStatus != "TD")
            {
                General.msjUser("El folio de surtido no tiene registrada la captura de surtimiento, verifique.");
            }
            else
            {
                if (!cnn.Abrir())
                {
                    General.msjErrorAlAbrirConexion();
                }
                else
                {
                    if (General.msjConfirmar(message) == DialogResult.Yes)
                    {
                        cnn.IniciarTransaccion();

                        string sSql = string.Format(
                            "Update Pedidos_Cedis_Enc_Surtido Set Status = 'D' " +
                            " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmaciaPedido = '{2}' and FolioSurtido = '{3}' \n ",
                        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, sIdFarmacia, sFolioSurtido);

                        sSql += string.Format(" Exec spp_Mtto_Pedidos_Cedis_Enc_Surtido_Atenciones '{0}', '{1}', '{2}', '{3}', '{4}', '{5}'",
                            DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioSurtido, DtGeneral.IdPersonal, "");

                        if (!leer.Exec(sSql))
                        {
                            // this.Close();
                        }
                        else
                        {
                            bRegresa = true;
                        }

                        if (bRegresa)
                        {
                            cnn.CompletarTransaccion();
                            ImprimirValidacion();
                            CargarFoliosSurtido();
                            General.msjAviso("El folio de surtido ha sido enviado a Documentación exitosamente.");
                        }
                        else
                        {
                            Error.GrabarError(leer, "MarcarFolioParaEntregaDocumentacion()");
                            cnn.DeshacerTransaccion();
                            General.msjError("Ocurrió un error al actualizar el status del Folio de Surtido.");
                        }
                    }

                    cnn.Cerrar();
                }
            }
            return bRegresa;
        }

        private void RegresarASurtimientos()
        {
            string sFolioSurtido = lst.GetValue((int)Cols.Folio);  
            string sStatus = lst.GetValue((int)Cols.Status);
            bool bRegresa = false;

            FrmRegresarASurtido f = new FrmRegresarASurtido();
            f.ShowDialog();
            bool bContinua = f.bContinua;
            string sObservaciones = f.sObservaciones;

            if (bContinua)
            {
                if (!cnn.Abrir())
                {
                    General.msjErrorAlAbrirConexion();
                }
                else
                {
                    cnn.IniciarTransaccion();

                    string sSql = string.Format(
                            "Update Pedidos_Cedis_Enc_Surtido Set Status = 'S' " +
                            " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmaciaPedido = '{2}' and FolioSurtido = '{3}' \n ",
                          DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, sIdFarmacia, sFolioSurtido);

                    sSql += string.Format("Exec spp_Mtto_Pedidos_Cedis_Enc_Surtido_Atenciones \n" +
                    "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioSurtido = '{3}', @IdPersonal = '{4}', @Observaciones = '{5}' \n",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioSurtido, DtGeneral.IdPersonal, sObservaciones);

                    if (!leer.Exec(sSql))
                    {
                            // this.Close();
                    }
                    else
                    {
                        bRegresa = true;
                    }

                    if (bRegresa)
                    {
                        cnn.CompletarTransaccion();
                        CargarFoliosSurtido();
                        General.msjAviso("El folio de surtido ha sido cambiado a surtimiento exitosamente.");
                    }
                    else
                    {
                        Error.GrabarError(leer, "RegresarASurtimiento()");
                        cnn.DeshacerTransaccion();
                        General.msjError("Ocurrió un error al actualizar el status del Folio de Surtido.");
                    }
                }
                cnn.Cerrar();
             }
          }
        //}

        private void CancelarSurtimiento()
        {
            string message = "Esta operación cancelará el Folio de Surtido, ¿ Desea continuar ?";
            string sFolioSurtido = lst.GetValue((int)Cols.Folio);
            string sStatus = lst.GetValue((int)Cols.Status);
            bool bRegresa = false;
            string sObservaciones = "";
            string sSql = ""; 
            clsObservaciones obs = null; //new clsObservaciones(); 

            //message = "El folio de surtido se enviara a Validación, ¿ Desea continuar ?";

            //if (sStatus != "A")
            //{
            //    General.msjUser("No es posibe cancelar el folio de surtido, cuenta con registro de surtimiento, verifique.");
            //}
            //else

            if(General.msjConfirmar(message) == DialogResult.Yes)
            {
                obs = new clsObservaciones();
                obs.MaxLength = 100;
                obs.Encabezado = "Cancelación de folio de surtido";
                obs.Show();
                sObservaciones = obs.Observaciones; 
                bRegresa = obs.Exito; 
            }

            if ( bRegresa ) 
            {
                if (!cnn.Abrir())
                {
                    General.msjErrorAlAbrirConexion();
                }
                else
                {
                    //if (General.msjConfirmar(message) == DialogResult.Yes)
                    {
                        cnn.IniciarTransaccion(); 

                        sSql += string.Format("Exec spp_Mtto_Pedidos_Cedis_Enc_Surtido__Cancelacion " +
                            "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', " + 
                            "\t@FolioSurtido = '{3}', @IdPersonal = '{4}', @Observaciones = '{5}' ",
                            DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioSurtido, DtGeneral.IdPersonal, sObservaciones);

                        if (!leer.Exec(sSql))
                        {
                            // this.Close();
                            bRegresa = false;
                        }
                        else
                        {
                            if (leer.Leer())
                            {
                                bRegresa = true;
                                message = leer.Campo("Mensaje");
                                General.msjAviso(message);
                            }

                            CargarFoliosSurtido();
                        }

                        if (bRegresa)
                        {
                            cnn.CompletarTransaccion();
                        }
                        else
                        {
                            Error.GrabarError(leer, "CancelarSurtimiento()");
                            cnn.DeshacerTransaccion();
                            General.msjError("Ocurrió un error al cancelar el Folio de Surtido.");
                        }
                    }

                    cnn.Cerrar();
                }
            }
        }


        #endregion Funciones 

        #region Botones Menu 
        private void InicializarPantalla()
        {
            CargarFoliosSurtido();
        }
        private void btnNuevo_Click( object sender, EventArgs e )
        {
            InicializarPantalla();
        }

        private void btnEdicion_Click( object sender, EventArgs e )
        {
            GetReserva(1); 
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
            string sFolioDeSurtido = lst.GetValue((int)Cols.Folio); 

            sSql = string.Format("Exec spp_Mtto_Pedidos_Cedis___ReservarOrdenDeSurtido \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioPedido = '{3}', \n" +
                "\t@FolioSurtido = '{4}', @IdPersonal = '{5}', @Terminal = '{6}', @TipoDeProceso = '{7}' \n",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, sIdFarmacia, sFolioPedido, sFolioDeSurtido, 
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

                CargarFoliosSurtido(); 
            }

            return bRegresa; 
        }
        #endregion Botones Menu 

        #region Botones 
        private void btnVerHistorial_Click(object sender, EventArgs e)
        {
            FrmListaDeAtencionesSurtido f = new FrmListaDeAtencionesSurtido();
            f.LevantaPantalla(lst.GetValue((int)Cols.Folio));
        }

        private void btnCancelarSurtimiento_Click(object sender, EventArgs e)
        {
            CancelarSurtimiento();
        }

        private void btnSurtirPedido_Click(object sender, EventArgs e)
        {
            sFolioSurtido = lst.GetValue((int)Cols.Folio);

            if (sFolioSurtido != "")
            {
                FrmCEDIS_SurtidoPedidos f = new FrmCEDIS_SurtidoPedidos(false, bEsManual); 
                f.CargarPedido(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, sIdFarmacia, sFolioSurtido, sFolioPedido);
                CargarFoliosSurtido(); 
            } 
        } 

        private void btnImprimirPedido_Click(object sender, EventArgs e)
        {
            sFolioSurtido = lst.GetValue((int)Cols.Folio); 

            bool bRegresa = true;
            //dImporte = Importe; 

            if (sFolioSurtido != "")
            {
                datosCliente.Funcion = "Imprimir()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.RutaReporte = DtGeneral.RutaReportes;

                myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
                myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
                myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
                myRpt.Add("Folio", sFolioSurtido);
                myRpt.NombreReporte = "PtoVta_Pedidos_CEDIS__SurtidoParcial";

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, datosCliente);
                // bRegresa = DtGeneral.ExportarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente, @"PRUEBA.pdf", FormatosExportacion.PortableDocFormat); 

                if (!bRegresa && !DtGeneral.CanceladoPorUsuario)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }

        private void btnCapturarSurtido_Click(object sender, EventArgs e)
        {
            RegistrarSurtimiento(1); 
        }

        private void btnResurtirClavesIncompletas_Click(object sender, EventArgs e)
        {
            string message = "Esta operación cambiará el Status del Folio de Surtido como disponible para Documentación (Generar transferencia), ¿ Desea continuar ?";
            string sFolioSurtido = lst.GetValue((int)Cols.Folio);
            string sStatus = lst.GetValue((int)Cols.Status);
            bool bRegresa = false;
            
            message = "Las claves incompletas del folio de surtido se procesarán para su resurtido, ¿ Desea continuar ?";

            if (sStatus != "S")
            {
                General.msjUser("El folio de surtido no tiene registrada la captura de surtimiento, verifique.");
            }
            else
            {
                if (!cnn.Abrir())
                {
                    General.msjErrorAlAbrirConexion();
                }
                else
                {
                    if (General.msjConfirmar(message) == DialogResult.Yes)
                    {
                        cnn.IniciarTransaccion();

                        string sSql = "";
                        sSql += string.Format(" Exec spp_ALM_GenerarReDistribucion__Surtimiento " + 
                            " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioSurtido = '{3}', @IdPersonal = '{4}' ",
                            DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioSurtido, DtGeneral.IdPersonal );

                        if (leer.Exec(sSql))     
                        {
                            cnn.CompletarTransaccion();
                            General.msjAviso("Se ha generaro del resurtido de las claves incompletas exitosamente.");
                            ImprimirSurtimiento();
                            CargarFoliosSurtido();
                        }
                        else
                        {
                            Error.GrabarError(leer, "MarcarFolioParaValidacion()");
                            cnn.DeshacerTransaccion();
                            General.msjError("Ocurrió un error al resurtir las claves faltantes.");
                        }
                    }

                    cnn.Cerrar();
                }
            }            
        }      

        private void btnModificarSurtimiento_Click(object sender, EventArgs e)
        {
            RegistrarSurtimiento(2); 
        }

        private void btnImprimirSurtimiento_Click(object sender, EventArgs e)
        {
            ImprimirSurtimiento();
        }

        private void btnImprimirSurtimientoCaratula_Click( object sender, EventArgs e )
        {
            ImprimirSurtimiento_Caratula(false);
        }

        private void btnValidarSurtimiento_Click(object sender, EventArgs e)
        {
            RegistrarSurtimiento(3); 
        }

        private void btnEnviarADocumentacion_Click(object sender, EventArgs e)
        {
            MarcarFolioParaEntregaDocumentacion(); 
        }

        private void RegistrarSurtimiento(int TipoCaptura)
        {
            string sStatus = lst.GetValue((int)Cols.Status);
            bool bMostrarPantalla = false; 


            ////if (sStatus == "T")
            ////{
            ////    General.msjUser(sMsjTransito);
            ////}
            if (TipoCaptura == 1)
            {
                // Captura del surtimiento inicial 
                bMostrarPantalla = sStatus == "A";
                if (!bMostrarPantalla)
                {
                    General.msjUser("El folio de surtido no es válido para la Captura de surtimiento."); 
                }
            }

            if (TipoCaptura == 2)
            {
                // Modificacion de surtimiento 
                bMostrarPantalla = sStatus == "S";
                if (!bMostrarPantalla)
                {
                    General.msjUser("El folio de surtido no es válido para la Modificación de surtimiento."); 
                }
            }

            if (TipoCaptura == 3)
            {
                // Validar surtiemiento, habilitar para la generación de transferencia 
                bMostrarPantalla = sStatus == "V";   // 
                if (!bMostrarPantalla)
                {
                    General.msjUser("El folio de surtido no es válido para la Validación de surtimiento."); 
                }
            }


            if (bMostrarPantalla)
            {
                if(bImplementaValidacionCiega && TipoCaptura == 3)
                {
                    FrmPedidosValidacionCiega h = new FrmPedidosValidacionCiega();
                    h.CargarPedido(lst.GetValue((int)Cols.Folio), TipoCaptura);
                    CargarFoliosSurtido();
                }
                else 
                {
                    FrmActualizarSurtidoPedidos f = new FrmActualizarSurtidoPedidos();
                    f.CargarPedido(lst.GetValue((int)Cols.Folio), TipoCaptura);
                    CargarFoliosSurtido();
                }
            }
        }

        private void btnEnviarA_EntregaValidacion_Click(object sender, EventArgs e)
        {
            MarcarFolioParaEntregaValidacion();
        }

        private void btnEnviarAValidacion_Click(object sender, EventArgs e)
        {
            MarcarFolioParaValidacion();
        }

        private void btnImprimirValidacion_Click(object sender, EventArgs e)
        {
            ImprimirValidacion();
        }

        private void btnImprimirCajasSurtimiento_Click(object sender, EventArgs e)
        {
            ImprimirCajas();
        }

        private void RegresarASurtimiento_Click(object sender, EventArgs e)
        {
            RegresarASurtimientos();
        }

        private void btnGenerarTransferencia_Click(object sender, EventArgs e)
        {
            string sStatus = lst.GetValue((int)Cols.Status); 

            if (sStatus != "D")
            {
                General.msjUser("El folio de surtimiento no esta habilitado para Documentación (Generar transferencia), verifique.");
            }
            else  
            {
                FrmTransferenciaSalidas_Base F = new FrmTransferenciaSalidas_Base();
                F.CargarPedido(lst.GetValue((int)Cols.Folio), sFolioPedido);
                CargarFoliosSurtido(); 
            }
        }

        private void btnGenerarVenta_Click(object sender, EventArgs e)
        {
            string sStatus = lst.GetValue((int)Cols.Status);
            string sIdFarmacia_Pedido = lst.LeerItem().Campo("IdFarmaciaPedido"); 

            if (sStatus != "D")
            {
                General.msjUser("El folio de surtimiento no esta habilitado para Documentación (Generar venta), verifique.");
            }
            else
            {
                if (TipoPedido == TipoDePedidoElectronico.Ventas)
                {
                    FrmVentas F = new FrmVentas();
                    F.CargaDetallesGeneraVenta(sIdFarmacia_Pedido, sFolioPedido, lst.GetValue((int)Cols.Folio));
                }

                if (TipoPedido == TipoDePedidoElectronico.SociosComerciales)
                {
                    FrmSalidasVentas_Comerciales F = new FrmSalidasVentas_Comerciales();
                    F.CargaDetallesGeneraVenta(sIdFarmacia_Pedido, sFolioPedido, lst.GetValue((int)Cols.Folio));
                }

                CargarFoliosSurtido();
            }
        }

        private void btnImprimirEtiquetas_Click( object sender, EventArgs e )
        {
            ////ImprimirEtiqueta(true); 
        }

        private void btnImprimirEtiquetas_01_Impresora_Click( object sender, EventArgs e )
        {
            ImprimirEtiqueta(false);
        }

        private void btnImprimirEtiquetas_02_VistaPrevia_Click( object sender, EventArgs e )
        {
            ImprimirEtiqueta(true);
        }

        private void ImprimirEtiqueta(bool VistaPrevia)
        {
            string sStatus = lst.GetValue((int)Cols.Status);
            sFolioSurtido = lst.GetValue((int)Cols.Folio);

            if (sFolioSurtido != "")
            {
                if (sStatus != "E")
                {
                    General.msjUser("El folio de surtimiento no esta habilitado para Documentación, verifique.");
                }
                else
                {
                    if(VistaPrevia)
                    {
                        etiquetas.GenerarEtiquetaSurtido(this, DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioSurtido, VistaPrevia);
                    }
                    else
                    {
                        if(!etiquetas.ExisteImpresoraDeEtiquetas)
                        {
                            General.msjAviso("No se detecto la configuración de impresora de etiquetas.");
                        }
                        else
                        {
                            etiquetas.GenerarEtiquetaSurtido(this, DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioSurtido, VistaPrevia);
                        }
                    }
                }
            }
        }

        private void btnEmbarque_Click(object sender, EventArgs e)
        {
            ////string sStatus = lst.GetValue((int)Cols.Status);
            ////string sFolioSurtido = lst.GetValue((int)Cols.Folio);  
            ////string sIdFarmacia_Pedido = lst.LeerItem().Campo("IdFarmaciaPedido");
            ////int iTipo = btnGenerarTransferencia.Enabled ? 1 : 2; 


            ////if (sStatus != "E")
            ////{
            ////    General.msjUser("El folio de surtimiento no esta habilitado para Embarque, verifique.");
            ////}
            ////else 
            ////{
            ////    FrmAsignarChoferTraslado f = new FrmAsignarChoferTraslado
            ////        (   
            ////            DtGeneral.EmpresaConectada, 
            ////            DtGeneral.EstadoConectado, 
            ////            DtGeneral.FarmaciaConectada, sFolioSurtido, iTipo
            ////        );
            ////    f.ShowDialog(this);
            ////    CargarFoliosSurtido();
            ////} 

            string message = "Esta operación cambiará el Status del Folio de Surtido a Embarques, ¿ Desea continuar ?";
            string sFolioSurtido = lst.GetValue((int)Cols.Folio);
            string sStatus = lst.GetValue((int)Cols.Status);
            bool bRegresa = false;

            message = "El folio de surtido se enviará a Embarque, ¿ Desea continuar ?";

            if (sStatus != "E")
            {
                General.msjUser("El folio de surtimiento no esta habilitado para Embarque, verifique.");
            }
            else
            {
                if (!cnn.Abrir())
                {
                    General.msjErrorAlAbrirConexion();
                }
                else
                {
                    if (General.msjConfirmar(message) == DialogResult.Yes)
                    {
                        cnn.IniciarTransaccion();

                        string sSql = string.Format(
                            "Update Pedidos_Cedis_Enc_Surtido Set Status = 'T' \n" +
                            "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmaciaPedido = '{2}' and FolioSurtido = '{3}' \n\n ",
                            DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, sIdFarmacia, sFolioSurtido);

                        sSql += string.Format("Exec spp_Mtto_Pedidos_Cedis_Enc_Surtido_Atenciones \n" +
                            "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioSurtido = '{3}', @IdPersonal = '{4}', @Observaciones = '{5}' \n",
                            DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioSurtido, DtGeneral.IdPersonal, "");

                        if (!leer.Exec(sSql))
                        {
                            // this.Close();
                        }
                        else
                        {
                            bRegresa = true;
                        }

                        if (bRegresa)
                        {
                            cnn.CompletarTransaccion();
                            CargarFoliosSurtido();
                            General.msjAviso("El folio de surtido ha sido enviado a embarques exitosamente.");
                        }
                        else
                        {
                            Error.GrabarError(leer, "btnEmbarque()");
                            cnn.DeshacerTransaccion();
                            General.msjError("Ocurrió un error al actualizar el status del Folio de Surtido.");
                        }
                    }

                    cnn.Cerrar();
                }
            }            
        }  
        #endregion Botones

        #region Imprimir
        private void ImprimirSurtimiento()
        {
            bool bRegresa = true;

            datosCliente.Funcion = "ImprimirSurtimiento()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);

            myRpt.RutaReporte = DtGeneral.RutaReportes;

            myRpt.Add("@IdEmpresa", DtGeneral.EmpresaConectada);
            myRpt.Add("@IdEstado", DtGeneral.EstadoConectado);
            myRpt.Add("@IdFarmacia", DtGeneral.FarmaciaConectada);
            myRpt.Add("@Folio", lst.GetValue((int)Cols.Folio));

            myRpt.NombreReporte = GnFarmacia.Vta_Impresion_Personalizada_Surtido_De_Pedidos;// "PtoVta_Cedis_SurtidoPedidos_Det";

            if (lst.LeerItem().CampoBool("EsManual"))
            {
                myRpt.NombreReporte = "PtoVta_Cedis_SurtidoPedidos_Det_Manual";
            }

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, datosCliente);

            if(!bRegresa && !DtGeneral.CanceladoPorUsuario)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }

        }

        private void ImprimirSurtimiento_Caratula( bool Confirmacion )
        {
            bool bRegresa = true;

            datosCliente.Funcion = "ImprimirSurtimiento()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);

            myRpt.RutaReporte = DtGeneral.RutaReportes;

            myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
            myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
            myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
            myRpt.Add("Folio", lst.GetValue((int)Cols.Folio));
            myRpt.NombreReporte = GnFarmacia.Vta_Impresion_Personalizada_Surtido_De_Pedidos_Caratula; // "PtoVta_Cedis_SurtidoPedidos_Det";

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, datosCliente);

            if(!bRegresa && !DtGeneral.CanceladoPorUsuario)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }
        private void ImprimirValidacion()
        {
            bool bRegresa = true;

            datosCliente.Funcion = "ImprimirSurtimiento()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);

            myRpt.RutaReporte = DtGeneral.RutaReportes;

            myRpt.Add("@IdEmpresa", DtGeneral.EmpresaConectada);
            myRpt.Add("@IdEstado", DtGeneral.EstadoConectado);
            myRpt.Add("@IdFarmacia", DtGeneral.FarmaciaConectada);
            myRpt.Add("@Folio", lst.GetValue((int)Cols.Folio));
            myRpt.NombreReporte = "PtoVta_Cedis_SurtidoPedidos_Validacion";

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, datosCliente);

            if(!bRegresa && !DtGeneral.CanceladoPorUsuario)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }

        }

        private void ImprimirCajas()
        {
            bool bRegresa = true;

            datosCliente.Funcion = "ImprimirCajas()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);

            myRpt.RutaReporte = DtGeneral.RutaReportes;

            myRpt.Add("@IdEmpresa", DtGeneral.EmpresaConectada);
            myRpt.Add("@IdEstado", DtGeneral.EstadoConectado);
            myRpt.Add("@IdFarmacia", DtGeneral.FarmaciaConectada);
            myRpt.Add("@FolioSurtido", lst.GetValue((int)Cols.Folio));
            myRpt.NombreReporte = "PtoVta_Caja_Embarque_FolioSurtido";

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, datosCliente);

            if (!bRegresa && !DtGeneral.CanceladoPorUsuario)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }

        }
        #endregion Imprimir 

        #region Manipular menu 
        private void listvwPedidos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sStatus = lst.GetValue((int)Cols.Status);
            bool bEsEditable = lst.LeerItem().CampoBool("EsEditable");
            bool bEnEdicion = lst.LeerItem().CampoBool("EnEdicion");
            bool bEdicionBloqueada = lst.LeerItem().CampoBool("EdicionBloqueada");
            bool bExisteFolioReferencia = lst.LeerItem().Campo("Referencia") != "";


            bEsManual = lst.LeerItem().CampoBool("EsManual");

            btnEdicion.Enabled = bEsEditable;
            btnTerminarEdicion.Enabled = bEnEdicion;

            if(bEnEdicion)
            {
                btnTerminarEdicion.Enabled = !bEdicionBloqueada;
                btnEdicion.Enabled = false;
            }

            btnCancelarSurtimiento.Enabled = false;
            btnCapturarSurtido.Enabled = false;
            btnModificarSurtimiento.Enabled = false;
            btnResurtirClavesIncompletas.Enabled = false;
            
            btnEnviarA_EntregaValidacion.Enabled = false; 
            btnEnviarAValidacion.Enabled = false;
            
            btnValidarSurtimiento.Enabled = false;
            btnImprimirValidacion.Enabled = false;
            btnImprimirCajasSurtimiento.Enabled = false;
            RegresarASurtimiento.Enabled = false;

            btnEnviarADocumentacion.Enabled = false; 
            btnGenerarTransferencia.Enabled = false;
            btnGenerarVenta.Enabled = false;
            btnImprimirEtiquetas.Enabled = false; 
            btnEmbarque.Enabled = false;


            //////bPermisos_RegistrarSurtido = false;
            //////bPermisos_ModificarSurtido = false;
            //////bPermisos_EnviarSurtido_A_Validacion = false;
            //////bPermisos_ValidarSurtimiento = false;
            //////bPermisos_Regresar_A_Surtimiento = false;
            //////bPermisos_GenerarSalida__Transferencia_Venta = false;
            //////bPermisos_Enviar_A_Embarque = false;


            ////// 20210511.1650 Jesús Díaz, validar que el usuario/equipo conectado tenga derechos de edición sobre el Folio de Surtido
            if(bEsEditable && bEnEdicion)
            {

                switch(sStatus)
                {
                    case "A":
                        btnCancelarSurtimiento.Enabled = bPermisos_RegistrarSurtido;
                        btnCapturarSurtido.Enabled = bPermisos_RegistrarSurtido;
                        break;

                    case "S":
                        btnCancelarSurtimiento.Enabled = bPermisos_RegistrarSurtido;
                        btnModificarSurtimiento.Enabled = bPermisos_ModificarSurtido;
                        btnResurtirClavesIncompletas.Enabled = bEsManual ? false : bPermisos_ModificarSurtido;
                        btnResurtirClavesIncompletas.Visible = !bEsManual;

                        btnEnviarA_EntregaValidacion.Enabled = bPermisos_EnviarSurtido_A_Validacion;
                        break;

                    case "TV": 
                        btnEnviarAValidacion.Enabled = bPermisos_EnviarSurtido_A_Validacion;
                        break;

                    case "V":
                        btnImprimirCajasSurtimiento.Enabled = bPermisos_ValidarSurtimiento;
                        btnImprimirValidacion.Enabled = bPermisos_ValidarSurtimiento;
                        btnValidarSurtimiento.Enabled = bPermisos_ValidarSurtimiento;
                        RegresarASurtimiento.Enabled = bPermisos_Regresar_A_Surtimiento;
                        break;

                    case "TD":
                        btnEnviarADocumentacion.Enabled = bPermisos_ValidarSurtimiento; 
                        break; 

                    case "D":
                        //if (lst.GetValueBool(lst.RenglonActivo, (int)Cols.EsTransferencia))
                        btnImprimirCajasSurtimiento.Enabled = true;
                        RegresarASurtimiento.Enabled = bPermisos_Regresar_A_Surtimiento;
                        switch(TipoPedido)
                        {
                            case TipoDePedidoElectronico.Transferencias:
                            case TipoDePedidoElectronico.Transferencias_InterEstatales:
                                btnGenerarTransferencia.Enabled = bPermisos_GenerarSalida__Transferencia_Venta;
                                btnImprimirEtiquetas.Enabled = bPermisos_GenerarSalida__Transferencia_Venta;
                                break;

                            case TipoDePedidoElectronico.Ventas:
                            case TipoDePedidoElectronico.SociosComerciales:
                                btnGenerarVenta.Enabled = bPermisos_GenerarSalida__Transferencia_Venta;
                                btnImprimirEtiquetas.Enabled = bPermisos_GenerarSalida__Transferencia_Venta;
                                break;
                        }
                        break;

                    case "E":
                    case "R":
                        btnImprimirCajasSurtimiento.Enabled = true;
                        btnEmbarque.Enabled = bPermisos_Enviar_A_Embarque;
                        btnImprimirEtiquetas.Enabled = bPermisos_Enviar_A_Embarque;
                        break;

                    default:
                        break;
                }
            }
            else
            {
                if(bExisteFolioReferencia)
                {
                    //btnEmbarque.Enabled = bPermisos_Enviar_A_Embarque;
                    btnImprimirCajasSurtimiento.Enabled = true;
                    btnImprimirEtiquetas.Enabled = bPermisos_GenerarSalida__Transferencia_Venta;
                }
            }
        }


        #endregion Manipular menu
    }
}
