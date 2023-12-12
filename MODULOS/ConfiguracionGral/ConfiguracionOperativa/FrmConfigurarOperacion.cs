using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading; 


using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores; 
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.ExportarDatos; 

using DllFarmaciaSoft;

namespace Configuracion.ConfiguracionOperativa
{
    public partial class FrmConfigurarOperacion : FrmBaseExt
    {
        enum Cols
        {
            IdFarmacia = 1, Farmacia = 2, Procesar = 3, Procesado = 4
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerUsuarios;
        clsCriptografo crypto = new clsCriptografo();
        clsConsultas Consulta; 

        DataSet dtsFarmacias_Base;
        DataSet dtsFarmacias_Destino;
        clsGrid grid;

        Thread thProcesar;
        string sIdFarmaciaProceso_Base = "";
        string sIdFarmaciaProceso = "";
        string sIdFarmaciaProceso_Origen = "";

        public FrmConfigurarOperacion()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            leerUsuarios = new clsLeer(ref cnn);
            Consulta = new clsConsultas(General.DatosConexion, GnConfiguracion.DatosApp, this.Name); 
            Error = new clsGrabarError(General.DatosConexion, GnConfiguracion.DatosApp, this.Name);

            MostrarEnProceso(false); 

            grid = new clsGrid(ref grdUnidades, this);
            grid.EstiloDeGrid = eModoGrid.ModoRow; 

            CargarCombos(); 
        }

        #region Form 
        private void FrmConfigurarOperacion_Load(object sender, EventArgs e)
        {
            InicializarPantalla(); 
        }
        #endregion Form
        
        #region Botones
        private void InicializarPantalla()
        {
            grid.Limpiar(false); 

            Fg.IniciaControles();
            IniciarToolBar(true, true, false); 
            cboEstado_Base.Focus(); 
        }

        private void IniciarToolBar(bool Nuevo, bool Ejecutar, bool Generar)
        {
            btnNuevo.Enabled = Nuevo;
            btnEjecutar.Enabled = Ejecutar;
            btnGenerarDocumentos.Enabled = Generar;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarPantalla(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (validarConsulta_Farmacias())
            {
                CargarFarmacias();
            }
        }

        private void btnGenerarDocumentos_Click(object sender, EventArgs e)
        {

            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            btnGenerarDocumentos.Enabled = false;

            FrameConfiguraciones_01_Generales.Enabled = false;
            FrameConfiguraciones_02_Base.Enabled = false;
            FrameCopiarConfiguracionBase.Enabled = false; 

            thProcesar = new Thread(thProcesarConfiguracion);
            thProcesar.Name = "Procesar configuración";
            thProcesar.Start();
        }
        #endregion Botones

        #region Funciones y Procedimientos Privados
        private void CargarCombos()
        {
            string sSql = "Select Distinct IdEmpresa, NombreEmpresa From vw_EmpresasEstados (NoLock) Order By IdEmpresa ";

            cboEstado_Base.Clear();
            cboEstado_Base.Add("0", "<< Seleccione >>");
            cboFarmacia_Base.Clear();
            cboFarmacia_Base.Add("0", "<< Seleccione >>");

            cboEstado_Destino.Clear();
            cboEstado_Destino.Add("0", "<< Seleccione >>");
            cboFarmacia_Destino.Clear();
            cboFarmacia_Destino.Add("0", "<< Seleccione >>");

            sSql = "Select distinct IdEstado, (IdEstado + ' -- ' + Estado) as Estado  " +
                    " From vw_Farmacias (NoLock) Order By IdEstado  ";
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarCombos(1)"); 
            }
            else
            {
                cboEstado_Base.Add(leer.DataSetClase, true, "IdEstado", "Estado");
                cboEstado_Destino.Add(leer.DataSetClase, true, "IdEstado", "Estado");

                sSql = "Select distinct IdEstado, IdFarmacia, (IdFarmacia + ' -- ' + Farmacia) as Farmacia " +
                        " From vw_Farmacias (NoLock) Order By IdEstado, IdFarmacia  ";
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "CargarCombos(2)");
                }
                else
                {
                    dtsFarmacias_Base = leer.DataSetClase;
                    dtsFarmacias_Destino = leer.DataSetClase; 
                }
            }

            cboEstado_Base.SelectedIndex = 0;
            cboEstado_Destino.SelectedIndex = 0;
            cboFarmacia_Base.SelectedIndex = 0;
            cboFarmacia_Destino.SelectedIndex = 0;
        }

        private void MostrarEnProceso(bool Mostrar)
        {
            if (Mostrar)
            {
                //FrameProceso.Left = 67;
                FrameProceso.Left = 0;
                FrameProceso.BringToFront();
            }
            else
            {
                FrameProceso.Left = this.Width + 100;
            }
        }

        private bool validarConsulta_Farmacias()
        {
            bool bRegresa = true;

            if (bRegresa & cboEstado_Base.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un estado base válido, verifique.");
                cboEstado_Base.Focus();
            }

            if (bRegresa & cboFarmacia_Base.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado una farmacia base válida, verifique.");
                cboFarmacia_Base.Focus();
            }

            if (bRegresa & cboEstado_Destino.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un estado destino válido, verifique.");
                cboEstado_Base.Focus();
            }

            return bRegresa;
        }

        private void CargarFarmacias()
        {
            bool bRegresa = false;
            string sSqlFarmacias = "";
            string sFiltro = "";

            if (cboFarmacia_Destino.SelectedIndex != 0)
            {
                sFiltro = string.Format(" and IdFarmacia = '{0}' ", cboFarmacia_Destino.Data);
            }

            if(txtFarmaciaInicial.Text.Trim() != "" || txtFarmaciaFinal.Text.Trim() != "")
            {
                if(txtFarmaciaInicial.Text.Trim() != "" && txtFarmaciaFinal.Text.Trim() != "")
                {
                    sFiltro = string.Format(" and IdFarmacia between '{0}' and '{1}' ", Fg.PonCeros(txtFarmaciaInicial.Text, 4), Fg.PonCeros(txtFarmaciaFinal.Text, 4));
                }
                else
                {
                    if(txtFarmaciaInicial.Text.Trim() != "" )
                    {
                        sFiltro = string.Format(" and IdFarmacia >= '{0}' ", Fg.PonCeros(txtFarmaciaInicial.Text, 4));
                    }

                    if(txtFarmaciaFinal.Text.Trim() != "")
                    {
                        sFiltro = string.Format(" and IdFarmacia <= '{0}' ", Fg.PonCeros(txtFarmaciaFinal.Text, 4));
                    }
                }
            }


            sSqlFarmacias = string.Format("Select IdFarmacia, Farmacia, 0, 0 " +
                " From vw_Farmacias (NoLock) " + 
                "Where Status = 'A' And IdEstado = '{0}' {1} ",
                cboEstado_Destino.Data, sFiltro); 

            if (!leer.Exec(sSqlFarmacias))
            {
                Error.GrabarError(leer, "CargarFarmacias()");
                General.msjError("Ocurrió un error al obtener la lista de farmacias.");
            }
            else
            {
                bRegresa = leer.Leer();
                grid.LlenarGrid(leer.DataSetClase);
            }

            IniciarToolBar(true, !bRegresa, bRegresa);
        }
        #endregion Funciones y Procedimientos Privados

        #region Eventos 
        private void cboEstado_Base_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboFarmacia_Base.Clear();
            cboFarmacia_Base.Add("0", "<< Seleccione >>");

            if (cboEstado_Base.SelectedIndex != 0)
            {
                cboFarmacia_Base.Filtro = string.Format(" IdEstado = '{0}' ", cboEstado_Base.Data);
                cboFarmacia_Base.Add(dtsFarmacias_Base, true, "IdFarmacia", "Farmacia");
            }

            cboFarmacia_Base.SelectedIndex = 0;
        }

        private void cboEstado_Destino_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboFarmacia_Destino.Clear();
            cboFarmacia_Destino.Add("0", "<< Seleccione >>");

            if (cboEstado_Destino.SelectedIndex != 0)
            {
                cboFarmacia_Destino.Filtro = string.Format(" IdEstado = '{0}' ", cboEstado_Destino.Data);
                cboFarmacia_Destino.Add(dtsFarmacias_Destino, true, "IdFarmacia", "Farmacia");
            }

            cboFarmacia_Destino.SelectedIndex = 0;
        }
        #endregion Eventos

        #region Procesamiento 
        private void thProcesarConfiguracion()
        {
            string sSql = "";
            int iDesplazamiento = Convert.ToInt32("0" + txtDesplazamientoFarmacias.Text.Trim());
            sIdFarmaciaProceso_Origen = cboFarmacia_Base.Data; 

            for (int i = 1; i <= grid.Rows; i++)
            { 
                if ( grid.GetValueBool(i, (int)Cols.Procesar))
                {
                    sIdFarmaciaProceso_Base = grid.GetValue(i, (int)Cols.IdFarmacia);
                    sIdFarmaciaProceso = grid.GetValue(i, (int)Cols.IdFarmacia);

                    if(chk_11_ConfiguracionBase.Checked)
                    {
                        sIdFarmaciaProceso_Origen = grid.GetValue(i, (int)Cols.IdFarmacia);
                        sIdFarmaciaProceso = Fg.PonCeros( Convert.ToInt32(sIdFarmaciaProceso_Base) + iDesplazamiento, 4);
                    }                    
                    

                    if (Procesar())
                    {
                        grid.SetValue(i, (int)Cols.Procesado, 1);  
                    }
                }
            }

            if (chk_09_Productos.Checked ) 
            {
                Procesar_09_Productos(); 
            }

            if ( chk_10_Contraseñas.Checked  )
            {
                Procesar_10_Contraseñas(); 
            }



            btnNuevo.Enabled = true;
            btnEjecutar.Enabled = false;
            btnGenerarDocumentos.Enabled = true;

            FrameConfiguraciones_01_Generales.Enabled = true;
            FrameConfiguraciones_02_Base.Enabled = true;
            FrameCopiarConfiguracionBase.Enabled = true;
        }

        private bool Procesar()
        {
            bool bRegresa = false;

            bRegresa = Procesar_01_SubFarmacias();

            if(bRegresa)
            {
                if(chk_11_ConfiguracionBase.Checked)
                {
                    bRegresa = Procesar_12_Perfiles();
                } 
            }


            return bRegresa; 
        }

        private bool Procesar_01_SubFarmacias()
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_CFG_OP__01__SubFarmacias  @IdEstadoBase = '{0}', @IdFarmaciaBase = '{1}', @IdEstadoDestino = '{2}', @IdFarmaciaDestino = '{3}' ",
                cboEstado_Base.Data, sIdFarmaciaProceso_Origen, cboEstado_Destino.Data, sIdFarmaciaProceso);

            if (chk_01_SubFarmacias.Checked)
            {
                bRegresa = leer.Exec(sSql);
            }

            if (bRegresa)
            {
                bRegresa = Procesar_02_MovimientosDeInventario(); 
            }

            return bRegresa; 
        }

        private bool Procesar_02_MovimientosDeInventario()
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_CFG_OP__02__Movimientos  @IdEstadoBase = '{0}', @IdFarmaciaBase = '{1}', @IdEstadoDestino = '{2}', @IdFarmaciaDestino = '{3}' ",
                cboEstado_Base.Data, sIdFarmaciaProceso_Origen, cboEstado_Destino.Data, sIdFarmaciaProceso);

            if (chk_02_MovimientosDeInventario.Checked)
            {
                bRegresa = leer.Exec(sSql);
            }

            if (bRegresa)
            {
                bRegresa = Procesar_03_Clientes_SubClientes();
            }

            return bRegresa;
        }

        private bool Procesar_03_Clientes_SubClientes()
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_CFG_OP__03__Clientes_SubClientes  @IdEstadoBase = '{0}', @IdFarmaciaBase = '{1}', @IdEstadoDestino = '{2}', @IdFarmaciaDestino = '{3}' ",
                cboEstado_Base.Data, sIdFarmaciaProceso_Origen, cboEstado_Destino.Data, sIdFarmaciaProceso);

            if (chk_03_Clientes_y_SubClientes.Checked)
            {
                bRegresa = leer.Exec(sSql);
            }

            if (bRegresa)
            {
                bRegresa = Procesar_04_Programas_SubProgramas(); 
            }

            return bRegresa;
        }

        private bool Procesar_04_Programas_SubProgramas()
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_CFG_OP__04__Programas_SubProgramas  @IdEstadoBase = '{0}', @IdFarmaciaBase = '{1}', @IdEstadoDestino = '{2}', @IdFarmaciaDestino = '{3}' ",
                cboEstado_Base.Data, sIdFarmaciaProceso_Origen, cboEstado_Destino.Data, sIdFarmaciaProceso);

            if (chk_04_Programas_y_SubProgramas.Checked)
            {
                bRegresa = leer.Exec(sSql);
            }

            if (bRegresa)
            {
                bRegresa = Procesar_05_Servicios_y_Areas(); 
            }

            return bRegresa;
        }

        private bool Procesar_05_Servicios_y_Areas()
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_CFG_OP__05__Servicios_Areas  @IdEstadoBase = '{0}', @IdFarmaciaBase = '{1}', @IdEstadoDestino = '{2}', @IdFarmaciaDestino = '{3}' ",
                cboEstado_Base.Data, sIdFarmaciaProceso_Origen, cboEstado_Destino.Data, sIdFarmaciaProceso);

            if (chk_05_Servicios_y_Areas.Checked)
            {
                bRegresa = leer.Exec(sSql);
            }

            if (bRegresa)
            {
                bRegresa = Procesar_06_Personal();
            }

            return bRegresa;
        }

        private bool Procesar_06_Personal()
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_CFG_OP__06__Personal  @IdEstadoBase = '{0}', @IdFarmaciaBase = '{1}', @IdEstadoDestino = '{2}', @IdFarmaciaDestino = '{3}' ",
                cboEstado_Base.Data, sIdFarmaciaProceso_Origen, cboEstado_Destino.Data, sIdFarmaciaProceso);

            if (chk_06_Personal.Checked)
            {
                bRegresa = leer.Exec(sSql);
            }

            if (bRegresa)
            {
                bRegresa = Procesar_07_Usuarios(); 
            }

            return bRegresa;
        }

        private bool Procesar_07_Usuarios()
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_CFG_OP__07__Usuarios  @IdEstadoBase = '{0}', @IdFarmaciaBase = '{1}', @IdEstadoDestino = '{2}', @IdFarmaciaDestino = '{3}' ",
                cboEstado_Base.Data, sIdFarmaciaProceso_Origen, cboEstado_Destino.Data, sIdFarmaciaProceso);

            if (chk_07_Usuarios.Checked)
            {
                bRegresa = leer.Exec(sSql);
            }

            if (bRegresa)
            {
                bRegresa = Procesar_09_Usuarios_Permisos(); 
            }

            return bRegresa;
        }

        private bool Procesar_09_Usuarios_Permisos()
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_CFG_OP__08__Permisos  @IdEstadoBase = '{0}', @IdFarmaciaBase = '{1}', @IdEstadoDestino = '{2}', @IdFarmaciaDestino = '{3}' ",
                cboEstado_Base.Data, sIdFarmaciaProceso_Origen, cboEstado_Destino.Data, sIdFarmaciaProceso);

            if (chk_08_Permisos.Checked)
            {
                bRegresa = leer.Exec(sSql);
            }

            return bRegresa;
        }

        private bool Procesar_09_Productos()
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_CFG_OP__09__ProductosEstado  @IdEstado = '{0}' ", cboEstado_Destino.Data);

            if (chk_09_Productos.Checked)
            {
                bRegresa = leer.Exec(sSql);
            }

            return bRegresa;
        }

        private bool Procesar_10_Contraseñas() 
        {
            bool bRegresa = true;
            string sSql = "";
            string sCadena = "";
            string sPass = "";
            string sIdEstado = "";
            string sIdFarmacia = "";
            string sIdPersonal = "";
            string sLogin = "";
            int iUsers = 0;
            string sFechaExec = "";
            DateTime dtPass = DateTime.Now; 
            //string sSql = ""; // string.Format("Exec spp_CFG_OP__09__ProductosEstado  @IdEstado = '{0}' ", cboEstado_Destino.Data);

            if (chk_10_Contraseñas.Checked)
            {

                grid.SetValue((int)Cols.Procesado, 0);

                ////General.msjAviso("Iniciando generación de passwords.");


                if (!cnn.Abrir())
                {
                    General.msjErrorAlAbrirConexion();
                }
                else
                {
                    cnn.IniciarTransaccion();
                    sFechaExec = General.FechaYMD(dtPass, "");
                    sFechaExec += General.Hora(dtPass, "");

                    for (int i = 1; i <= grid.Rows; i++)
                    {
                        if (grid.GetValueBool(i, (int)Cols.Procesar))
                        {
                            sIdFarmaciaProceso = grid.GetValue(i, (int)Cols.IdFarmacia);

                            sSql = string.Format("Select IdEstado, Estado, IdFarmacia, Farmacia, IdPersonal, NombreCompleto, LoginUser, Status " +
                                " From vw_Personal U (NoLock) " +
                                " Where IdEstado = '{0}' and IdFarmacia = '{1}' " +
                                "Order by IdEstado, IdFarmacia, IdPersonal ",
                                cboEstado_Destino.Data, sIdFarmaciaProceso);

                            leerUsuarios.Exec(sSql);
                            while (leerUsuarios.Leer())
                            {
                                dtPass = DateTime.Now;
                                sFechaExec = General.FechaYMD(dtPass, "");
                                sFechaExec += General.Hora(dtPass, "");

                                sIdEstado = leerUsuarios.Campo("IdEstado");
                                sIdFarmacia = leerUsuarios.Campo("IdFarmacia");
                                sIdPersonal = leerUsuarios.Campo("IdPersonal");
                                sLogin = leerUsuarios.Campo("LoginUser");
                                ////sLogin = Fg.Mid(sLogin, 1, sLogin.Length - 1);

                                sCadena = sIdEstado + sIdFarmacia + sIdPersonal;
                                sCadena += Fg.Mid(clsMD5.GenerarMD5(sCadena + sFechaExec), 1, 8).ToUpper();
                                sPass = crypto.PasswordEncriptar(sCadena);
                                sFechaExec = "";

                                sSql = string.Format("Exec spp_Net_Usuarios_Mtto   " + 
                                    " @IdEstado = '{0}', @IdSucursal = '{1}', @IdPersonal = '{2}', @LoginUser = '{3}', @Password = '{4}', @iTipo = '{5}' ",
                                    sIdEstado, sIdFarmacia, sIdPersonal, sLogin, sPass, 1);


                                //lblFarmacia.Text = leerUsuarios.Campo("IdFarmacia") + " -- " + leerUsuarios.Campo("Farmacia");
                                //lblNombreUsuario.Text = leerUsuarios.Campo("NombreCompleto");

                                if (!leer.Exec(sSql))
                                {
                                    bRegresa = false;
                                    break;
                                }

                                ///// Avance   
                                iUsers++;
                                //lblAvance.Text = string.Format("{0} / {1}", iUsers, iNumUsuarios);
                                this.Refresh();
                            }

                            grid.SetValue(i, (int)Cols.Procesado, 1);
                        }
                    }


                    if (!bRegresa)
                    {
                        Error.GrabarError(leer.Error, "Procesar_10_Contraseñas()");
                        cnn.DeshacerTransaccion();
                        General.msjError("Ocurrió un error al actualizar los Passwords.");
                    }
                    else
                    {
                        cnn.CompletarTransaccion();
                        General.msjUser("Passwords actualizados satisfactoriamente.");
                        //btnEjecutar.Enabled = true;
                        //btnGenerarPassword.Enabled = false;
                        //btnExportarExcel.Enabled = false;

                        //lblFarmacia.Text = "";
                        //lblNombreUsuario.Text = "";
                    }

                    cnn.Cerrar(); 


                }
            }

            return bRegresa;
        }

        #region Configuracion base 
        private bool Procesar_12_Perfiles()
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_CFG_OP__10__NivelesAtencion_Miembros  @IdEstadoBase = '{0}', @IdFarmaciaBase = '{1}', @IdEstadoDestino = '{2}', @IdFarmaciaDestino = '{3}' ",
                cboEstado_Base.Data, sIdFarmaciaProceso_Base, cboEstado_Destino.Data, sIdFarmaciaProceso);

            if(chk_12_Perfiles.Checked)
            {
                bRegresa = leer.Exec(sSql);
            }

            if(bRegresa)
            {
                bRegresa = Procesar_13_Conexiones(); 
            }

            return bRegresa;
        }

        private bool Procesar_13_Conexiones()
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_CFG_OP__11__ConfigurarConexiones  @IdEstadoBase = '{0}', @IdFarmaciaBase = '{1}', @IdEstadoDestino = '{2}', @IdFarmaciaDestino = '{3}' ",
                cboEstado_Base.Data, sIdFarmaciaProceso_Base, cboEstado_Destino.Data, sIdFarmaciaProceso);

            if(chk_13_Conexiones.Checked)
            {
                bRegresa = leer.Exec(sSql);
            }

            if(bRegresa)
            {
                bRegresa = Procesar_14_EmpresaRelacionada(); 
            }

            return bRegresa;
        }

        private bool Procesar_14_EmpresaRelacionada()
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_CFG_OP__12__CFG_EmpresasFarmacias  @IdEstadoBase = '{0}', @IdFarmaciaBase = '{1}', @IdEstadoDestino = '{2}', @IdFarmaciaDestino = '{3}' ",
                cboEstado_Base.Data, sIdFarmaciaProceso_Base, cboEstado_Destino.Data, sIdFarmaciaProceso);

            if(chk_14_EmpresaRelacionada.Checked)
            {
                bRegresa = leer.Exec(sSql);
            }

            if(bRegresa)
            {
                bRegresa = Procesar_15_FarmaciasConvenio();
            }

            return bRegresa;
        }

        private bool Procesar_15_FarmaciasConvenio()
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_CFG_OP__13__CFG_Farmacias_ConvenioVales  @IdEstadoBase = '{0}', @IdFarmaciaBase = '{1}', @IdEstadoDestino = '{2}', @IdFarmaciaDestino = '{3}' ",
                cboEstado_Base.Data, sIdFarmaciaProceso_Base, cboEstado_Destino.Data, sIdFarmaciaProceso);

            if(chk_15_FarmaciasConvenio.Checked)
            {
                bRegresa = leer.Exec(sSql);
            }

            if(bRegresa)
            {
                bRegresa = Procesar_16_ProveedoresVales();
            }

            return bRegresa;
        }

        private bool Procesar_16_ProveedoresVales()
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_CFG_OP__14__CatFarmacias_ProveedoresVales  @IdEstadoBase = '{0}', @IdFarmaciaBase = '{1}', @IdEstadoDestino = '{2}', @IdFarmaciaDestino = '{3}' ",
                cboEstado_Base.Data, sIdFarmaciaProceso_Base, cboEstado_Destino.Data, sIdFarmaciaProceso);

            if(chk_16_ProveedoresVales.Checked)
            {
                bRegresa = leer.Exec(sSql);
            }

            return bRegresa;
        }
        #endregion Configuracion base 
        #endregion Procesamiento

        private void chkMarcarDesmarcar_CheckedChanged(object sender, EventArgs e)
        {
            grid.SetValue((int)Cols.Procesar, chkMarcarDesmarcar.Checked);
        }
    }
}
