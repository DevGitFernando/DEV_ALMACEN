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
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
//using SC_SolutionsSystem.ExportarDatos;  

using DllFarmaciaSoft;
using DllFarmaciaSoft.ExportarExcel;
using ClosedXML.Excel;

namespace Inventarios.DistribucionExcedentes
{
    public partial class FrmDistribucionExcedentes : FrmBaseExt
    {
        #region Enums 
        enum Cols_Excedentes
        {
            ClaveSSA = 1, Descripcion = 2, Consumo = 3, StockSugerido = 4, Existencia = 5,  Excedente = 6, Distribuir = 7  
        }
        #endregion Enums

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsConexionSQL cnnPCM = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerPCM;
        clsLeer leerExcedente; 
        clsConsultas query; 
        clsDatosCliente datosCliente; 

        clsGrid gridEx; 
        clsListView lst;
        clsListView lstFaltantesJuris;
        clsListView lstFaltantesOtrasJuris; 
        clsListView lstDistribuido;  


        DataSet dtsJurisdicciones = new DataSet();
        DataSet dtsExcedenteDistribuido = new DataSet(); 

        Thread thExcedentes; 
        Thread thFaltantes;
        Thread thFaltantesOtrasJuris;
        Thread thDistribuir;
        Thread thDistribuirOtrasJuris;

        //clsExportarExcelPlantilla xpExcel; 

        string sIdEmpresa = DtGeneral.EmpresaConectada;
        string sIdEstado = DtGeneral.EstadoConectado;
        string sIdFarmacia = DtGeneral.FarmaciaConectada;
        string sIdJurisdiccion = DtGeneral.Jurisdiccion;
        int iFarmacias_Proceso = 0;
        int iFarmacias_Procesadas = 0; 

        string sClaveSSA_Procesar = ""; 
        string sUrlServidorRegional = "";
        int iConsultando = 0;
        // bool bHabilitar_Distribucion = false;
        

        public FrmDistribucionExcedentes()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; 

            lst = new clsListView(lstvwExcedentes);
            lstFaltantesJuris = new clsListView(lstvwFaltantesJuris);
            lstFaltantesOtrasJuris = new clsListView(lstvwFaltantesOtrasJuris);
            lstDistribuido = new clsListView(lstvwDistribuido); 

            ////lstFaltantesJuris.FormatoNumericos = true;
            ////lstFaltantesOtrasJuris.FormatoNumericos = true; 


            gridEx = new clsGrid(ref grdExcedente, this);
            gridEx.EstiloDeGrid = eModoGrid.ModoRow;
            gridEx.SetOrder((int)Cols_Excedentes.ClaveSSA, (int)Cols_Excedentes.Distribuir, true); 


            Error = new clsGrabarError(General.DatosConexion, GnInventarios.DatosApp, this.Name);  
            leer = new clsLeer(ref cnn);
            leerPCM = new clsLeer(ref cnnPCM); 
            leerExcedente = new clsLeer(); 

            tabFaltantesJuris.Text = "Faltantes Jurisdicción " + "( " + DtGeneral.Jurisdiccion + " -- " + DtGeneral.JurisdiccionNombre + " )";


            sUrlServidorRegional = DtGeneral.UrlServidorCentral_Regional;
            datosCliente = new clsDatosCliente(GnInventarios.DatosApp, this.Name, "Obtener_PCM");

            query = new clsConsultas(General.DatosConexion, GnInventarios.DatosApp, this.Name, false); 


            this.Width = 1020; 
            this.Height = 572; 

            tabControl.Width = 998; 
            tabControl.Height = 515;

            grdExcedente.Top = 65;
            grdExcedente.Left = 6;
            grdExcedente.Height = grdExcedente.Height + FrameUbicaciones.Height + 5; 
            FrameUbicaciones.Visible = false; 

            FrameProceso.Top = 220;
            FrameProceso.Width = 706; 
            FrameProceso.Height = 100;
            MostrarEnProceso(false); 
            // FrameProceso.Visible = false; 


            if (DtGeneral.EsAlmacen && GnFarmacia.ManejaUbicaciones)
            {
                MostrarFrameUbicaciones(true);
            }
            //////////else
            //////////{
            //////////    AjustarFrameCriteriosExcedentes();
            //////////}
        }

        #region FORM 
        private void FrmDistribucionExcedentes_Load(object sender, EventArgs e)
        {
            // ObtenerJurisdicciones(); 
            LimpiarPantalla();

            BuscarDistribuciones(); 
        }

        private void ObtenerJurisdicciones()
        {
            clsLeer leerJuris = new clsLeer();
            dtsJurisdicciones = query.Jurisdicciones(sIdEstado, "ObtenerJurisdicciones()");

            try
            {
                leerJuris.DataRowsClase = dtsJurisdicciones.Tables[0].Select(string.Format(" IdJurisdiccion <> '{0}' ", sIdJurisdiccion));
                dtsJurisdicciones = leerJuris.DataSetClase; 
            }
            catch { }

            cboJurisdicciones.Clear();
            cboJurisdicciones.Add();
            cboJurisdicciones.Add(dtsJurisdicciones, true, "IdJurisdiccion", "NombreJurisdiccion");
            cboJurisdicciones.SelectedIndex = 0; 
        }

        private void ObtenerJurisdicciones_SinDistribucion()
        {
            clsLeer leerJuris = new clsLeer(ref cnn);
            string sSql = string.Format(" Select IdEstado, IdJurisdiccion, Descripcion, Descripcion as Jurisdiccion, " +
                " (IdJurisdiccion + ' -- ' + Descripcion) as NombreJurisdiccion " +
                " From CatJurisdicciones J (NoLock) " +
                " Where IdEstado = '{0}' " + 
                "   and Not Exists " + 
                " ( " +
                "   Select IdJurisdiccion From INV_Distribucion_Faltantes F (NoLock) " + 
                "       Where F.IdEstado = J.IdEstado and F.IdJurisdiccion = J.IdJurisdiccion " +
                " ) ", sIdEstado); 

            try
            {
                leerJuris.Exec(sSql);
                dtsJurisdicciones = leerJuris.DataSetClase;
                leerJuris.DataRowsClase = dtsJurisdicciones.Tables[0].Select(string.Format(" IdJurisdiccion <> '{0}' ", sIdJurisdiccion));
                dtsJurisdicciones = leerJuris.DataSetClase;
            }
            catch { }

            cboJurisdicciones.Clear();
            cboJurisdicciones.Add();
            cboJurisdicciones.Add(dtsJurisdicciones, true, "IdJurisdiccion", "NombreJurisdiccion");
            cboJurisdicciones.SelectedIndex = 0; 
        }

        private void ControBarraPrincipal(bool Bloquear)
        {
            Bloquear = !Bloquear; 

            btnNuevo.Enabled = Bloquear;
            btnEjecutar.Enabled = Bloquear;
            btnExportarExcel.Enabled = Bloquear;
            btnExportarExcelExcedentes.Enabled = Bloquear; 
        }

        private void ControlBotones(bool Habilitar)
        {
            bool bHabilitar = Habilitar;

            btnNuevo.Enabled = bHabilitar;
            btnEjecutar.Enabled = bHabilitar;

            btnNuevoFaltantes.Enabled = bHabilitar;
            btnEjecutarFaltantes.Enabled = bHabilitar;
            btnGenerarDistribucion.Enabled = bHabilitar;
            btnDetenerJuris.Enabled = !bHabilitar; 

            btnNuevoFaltantesOtrasJuris.Enabled = bHabilitar;
            btnEjecutarFaltantesOtrasJuris.Enabled = bHabilitar;
            btnGenerarDistribucionOtrasJuris.Enabled = bHabilitar;
            btnDetenerOtrasJuris.Enabled = !bHabilitar; 
        }

        private void MostrarEnProceso(bool Mostrar)
        {
            if (Mostrar)
            {
                FrameProceso.Left = 155; 
            }
            else
            {
                FrameProceso.Left = this.Width + 100; 
            }

            ////FrameProceso.Visible = Mostrar;
            ////System.Threading.Thread.Sleep(1000);
            ////this.Refresh(); 
        }

        private void chkTodas_CheckedChanged(object sender, EventArgs e)
        {
            gridEx.SetValue((int)Cols_Excedentes.Distribuir, chkTodas.Checked);
        }

        private void MostrarFrameUbicaciones(bool Mostrar)
        {

            FrameUbicaciones.Visible = true;
            grdExcedente.Left = 6;
            grdExcedente.Top= 122;
            grdExcedente.Height = grdExcedente.Height - ( FrameUbicaciones.Height + 5 );


            //////cboPasillos.Enabled = Mostrar;
            //////cboPasillos.Visible = Mostrar;
            //////lblPasillo.Visible = Mostrar;

            //////cboEstantes.Enabled = Mostrar;
            //////cboEstantes.Visible = Mostrar;
            //////lblEstante.Visible = Mostrar;

            //////cboEntrepanos.Enabled = Mostrar;
            //////cboEntrepanos.Visible = Mostrar;
            //////lblEntrepaño.Visible = Mostrar;

            //////FrameUbicaciones.Visible = Mostrar;
            //////FrameUbicaciones.Left = 6;
            //////FrameUbicaciones.Top = 68;

            //////grdExcedente.Top = grdExcedente.Top + 10;
            //////grdExcedente.Height = grdExcedente.Height - FrameUbicaciones.Height + 10;

            //////FrameCriteriosExcedentes.Width = grdExcedente.Width;
            //////FrameUbicaciones.Width = grdExcedente.Width; 
        }

        private void AjustarFrameCriteriosExcedentes()
        {
            //////FrameCriteriosExcedentes.Width = 978;
           
            //////lblMesesRev.Size = new Size(173, 20);
            //////lblMesesRev.Location = new Point(125, 20);

            //////nmMesesRevision.Size = new Size(70, 20);
            //////nmMesesRevision.Location = new Point(306, 20);

            //////lblMesesExist.Size = new Size(160, 20);
            //////lblMesesExist.Location = new Point(414, 20);

            //////nmMesesExistencia.Size = new Size(70, 20);
            //////nmMesesExistencia.Location = new Point(595, 20);

            ////////chkTodas.Size = new Size(203, 20);
            //////chkTodas.Location = new Point(723, 20);
        }
        #endregion FORM

        #region Botones 
        #region Excedentes 
        private void LimpiarPantalla()
        {
            dtsExcedenteDistribuido = new DataSet(); 
            iConsultando = 0;
            iFarmacias_Procesadas = 0;
            iFarmacias_Proceso = 0; 

            leerExcedente = new clsLeer(); 
            sClaveSSA_Procesar = "";

            rdoBuenaCaducidad.Checked = true;

            ObtenerJurisdicciones();             
            Fg.IniciaControles();

            gridEx.Limpiar(); 
            lst.Limpiar();
            lstFaltantesJuris.Limpiar();
            lstFaltantesOtrasJuris.Limpiar();
            lstDistribuido.Limpiar();
            TituloCadudicades(1); 

            if (DtGeneral.EsAlmacen && GnFarmacia.ManejaUbicaciones)
            {
                CargarPasillos();

                cboEstantes.Clear();
                cboEstantes.Add("0", "<< Todo >>");
                cboEstantes.SelectedIndex = 0;

                cboEntrepanos.Clear();
                cboEntrepanos.Add("0", "<< Todo >>");
                cboEntrepanos.SelectedIndex = 0;
            }

            btnNuevoFaltantes.Enabled = false;
            btnEjecutarFaltantes.Enabled = false; 
            btnNuevoFaltantesOtrasJuris.Enabled = false;
            btnEjecutarFaltantesOtrasJuris.Enabled = false;
            btnGenerarDistribucion.Enabled = false;
            btnGenerarDistribucionOtrasJuris.Enabled = false;
            btnDetenerJuris.Enabled = false;
            btnDetenerOtrasJuris.Enabled = false; 


            btnExportarExcelExcedentes.Enabled = false;
            btnExportarExcel.Enabled = false;  

            nmMesesRevision.Value = nmMesesRevision.Maximum;
            nmMesesExistencia.Value = nmMesesExistencia.Minimum;
            chkTodas.Checked = true;

            lblProceso_Juris.BackColor = toolStripBarraMenu.BackColor;
            lblProceso_OtrasJuris.BackColor = toolStripBarraMenu.BackColor;

            lblProceso_Juris.Text = "";
            lblProceso_OtrasJuris.Text = "";

            lblDistribucion.BackColor = toolStripBarraMenu.BackColor;
            lblDistribucionOtrasJuris.BackColor = toolStripBarraMenu.BackColor;

            lblDistribucion.Visible = false; 
            lblDistribucionOtrasJuris.Visible = false;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            PrepararTablas_DeProceso(); 
            LimpiarPantalla(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            thExcedentes = new Thread(this.ThObtenerExcedentes);
            thExcedentes.Name = "ObtenerFaltantes_Juris";
            thExcedentes.Start(); 
        }

        private void btnExportarExcelExcedentes_Click(object sender, EventArgs e)
        {
            GenerarExcel_Excedentes(); 
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            GenerarExcel_Distribucion(); 
        }
        #endregion Excedentes

        #region Faltantes
        private void btnNuevoFaltantes_Click(object sender, EventArgs e)
        {
            lstFaltantesJuris.Limpiar(); 
        }

        private void btnEjecutarFaltantes_Click(object sender, EventArgs e)
        {
            thFaltantes = new Thread(this.ThObtenerFaltantesJuris);
            thFaltantes.Name = "ObtenerFaltantes_Juris";
            thFaltantes.Start(); 
        }

        private void btnGenerarDistribucion_Click(object sender, EventArgs e)
        {
            thDistribuir = new Thread(this.ThDistribuirExcedentes);
            thDistribuir.Name = "Distribuir_Excedentes";
            thDistribuir.Start(); 
        }

        private void btnDetenerJuris_Click(object sender, EventArgs e)
        {
            try
            {
                thDistribuir.Interrupt();
            }
            catch { }
            finally
            {
                thDistribuir = null;
                ControBarraPrincipal(false);
                MostrarEnProceso(false); 
                ControlBotones(true); 
            }
        }
        #endregion Faltantes 

        #region Faltantes Otras Jurisdicciones 
        private void btnNuevoFaltantesOtrasJuris_Click(object sender, EventArgs e)
        {
            lstFaltantesOtrasJuris.Limpiar(); 
        }

        private void btnEjecutarFaltantesOtrasJuris_Click(object sender, EventArgs e)
        {
            thFaltantesOtrasJuris = new Thread(this.ThObtenerFaltantesOtrasJuris);
            thFaltantesOtrasJuris.Name = "ObtenerFaltantes_OtrasJuris";
            thFaltantesOtrasJuris.Start(); 
        }

        private void btnGenerarDistribucionOtrasJuris_Click(object sender, EventArgs e)
        {
            thDistribuirOtrasJuris = new Thread(this.ThDistribuirExcedentes_OtrasJurisdicciones);
            thDistribuirOtrasJuris.Name = "Distribuir_Excedentes_OtrasJurisdicciones";
            thDistribuirOtrasJuris.Start(); 
        }

        private void btnDetenerOtrasJuris_Click(object sender, EventArgs e)
        {
            try
            {
                thDistribuirOtrasJuris.Interrupt();
            }
            catch { }
            finally
            {
                thDistribuirOtrasJuris = null;
                ControBarraPrincipal(false);
                MostrarEnProceso(false); 
                ControlBotones(true); 
            }
        }
        #endregion Faltantes Otras Jurisdicciones 
        
        #region Distribuido
        private void btnNuevo_Distribucion_Click(object sender, EventArgs e)
        {
            lstDistribuido.Limpiar(); 
        }

        private void btnEjecutar_Distribucion_Click(object sender, EventArgs e)
        {
            Obtener_Distribucion_Generada(); 
        }

        private void btnExportarDistribuido_Click(object sender, EventArgs e)
        {
            GenerarExcel_Excedentes_Distribuido(); 
        }
        #endregion Distribuido 
 
        #endregion Botones 

        #region Hilos 
        private void ThObtenerExcedentes()
        {
            try
            {
                btnNuevo.Enabled = false;
                btnEjecutar.Enabled = false;
                btnExportarExcel.Enabled = false;
                btnExportarExcelExcedentes.Enabled = false;

                MostrarEnProceso(true);
                ObtenerExcedentes();
                MostrarEnProceso(false);

                btnNuevo.Enabled = true;
                btnEjecutar.Enabled = true;
            }
            catch { } 
        }

        private void ThObtenerFaltantesJuris()
        {
            // bool bHabilitar = false;

            ////btnNuevo.Enabled = bHabilitar;
            ////btnEjecutar.Enabled = bHabilitar;

            lblProceso_Juris.Text = "";
            lblDistribucion.Visible = false; 
            ControlBotones(false); 

            ControBarraPrincipal(true); 
            MostrarEnProceso(true); 

            if (PrepararClaves_Procesar())
            {
                Obtener_PCM(sIdJurisdiccion);
            }

            ////bHabilitar = true; 
            ////btnNuevo.Enabled = bHabilitar;
            ////btnEjecutar.Enabled = bHabilitar;

            ControlBotones(true); 

            ControBarraPrincipal(false); 
            MostrarEnProceso(false); 
        }

        private void ThObtenerFaltantesOtrasJuris()
        {
            if (cboJurisdicciones.SelectedIndex == 0)
            {
                General.msjUser("No ha seleccionado una Jurisdicción válida, verifique.");
            }
            else
            {
                lblProceso_OtrasJuris.Text = "";
                lblDistribucionOtrasJuris.Visible = false; 
                ControlBotones(false); 

                ControBarraPrincipal(true);
                MostrarEnProceso(true);

                ////////////////////////////////////////////////////////////////// 
                // SE AGREGA PrepararClaves_Procesar PARA PERMITIR QUE OTRAS JURIS HAGAN SU DISTRIBUCION.
                if (PrepararClaves_Procesar())
                {
                    Obtener_PCM(cboJurisdicciones.Data);
                }
                ////////////////////////////////////////////////////////////////// 


                ControBarraPrincipal(false);
                MostrarEnProceso(false);

                ControlBotones(true); 
            }
        }

        private void ThDistribuirExcedentes()
        {
            btnGenerarDistribucion.Enabled = false;
            ControBarraPrincipal(true);

            DistribuirExcedente(sIdJurisdiccion);   
            Obtener_Distribucion_Generada();

            lblDistribucion.Visible = true; 

            ControBarraPrincipal(false);
            btnGenerarDistribucion.Enabled = true;  
        }

        private void ThDistribuirExcedentes_OtrasJurisdicciones()
        {
            if (cboJurisdicciones.SelectedIndex == 0)
            {
                General.msjUser("No ha seleccionado una Jurisdicción válida, verifique.");
            }
            else
            {
                ////btnGenerarDistribucionOtrasJuris.Enabled = false;
                ControBarraPrincipal(true);
                ControlBotones(false);

                DistribuirExcedente(cboJurisdicciones.Data);
                Obtener_Distribucion_Generada();
                lblDistribucionOtrasJuris.Visible = true; 


                ControlBotones(true);
                ControBarraPrincipal(false);
                ////btnGenerarDistribucionOtrasJuris.Enabled = true;
            }
        } 
        #endregion Hilos  

        #region Funciones y Procedimientos Privados
        private bool PrepararTablas_DeProceso() 
        {
            string sSql = "";
            bool bRegresa = false; 

            sSql = "Delete From INV_Distribucion_Excedentes \n";
            sSql += string.Format("Delete From INV_Distribucion_Faltantes Where IdEstado = '{0}' \n", sIdEstado);
            sSql += string.Format("Delete From INV_Distribucion_Existencias Where IdEstado = '{0}' ", sIdEstado);

            leer.Exec(sSql); 

            return bRegresa; 
        }

        private bool BuscarDistribuciones()
        {
            bool bRegresa = false; 
            string sSql = 
                string.Format(" select top 1 * from INV_Distribucion_Excedentes (NoLock) " + 
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' ", 
                sIdEmpresa, sIdEstado, sIdFarmacia);

            if (leer.Exec(sSql))
            {
                bRegresa = leer.Leer(); 
            } 

            if (bRegresa)
            {
                if (General.msjConfirmar("Se encontro un listado anterior de claves con Excedentes, desea cargar este listado.") == DialogResult.No)
                {
                    bRegresa = false; 
                } 

                if (bRegresa)
                {
                    nmMesesRevision.Value = Convert.ToDecimal(leer.CampoInt("MesesAnalisis"));
                    nmMesesExistencia.Value = Convert.ToDecimal(leer.CampoInt("MesesStock")); 
                    ObtenerExcedenteGuardado() ;

                    // Mostrar_PCM(sIdJurisdiccion); 
                } 
            }

            return bRegresa; 
        }

        private void ObtenerExcedenteGuardado()
        {
            string sSql = string.Format(
                " Select " +
                    " 'Clave SSA' = R.ClaveSSA, 'Descripción Clave' = R.DescripcionClave, " +
                    " 'Consumo' = R.Cantidad_Consumo, " +
                    " 'Stock sugerido' = R.ExistenciaMeses_Piezas, R.Existencia, R.Excedente, " + 
                    " (case when IsNull(E.ClaveSSA, 'x') = 'x' then 0 else 1 end) as Procesar " + 
                " From Rpt_Consumos_Claves R (NoLock)  " +
                " Left Join INV_Distribucion_Excedentes  E  (NoLock) On ( R.IdEmpresa = E.IdEmpresa and R.IdEstado = E.IdEstado and R.IdFarmacia = E.IdFarmacia and R.ClaveSSA = E.ClaveSSA ) " +
                " Where EsExcedente = 1  and R.IdEmpresa = '{0}' and R.IdEstado = '{1}' and R.IdFarmacia = '{2}' ", sIdEmpresa, sIdEstado, sIdFarmacia);

            if (!leer.Exec(sSql))
            {
            }
            else
            {
                if (leer.Leer())
                {
                    leerExcedente.DataSetClase = leer.DataSetClase;

                    btnNuevoFaltantes.Enabled = true;
                    btnEjecutarFaltantes.Enabled = true;
                    btnGenerarDistribucion.Enabled = true;
                    btnExportarExcel.Enabled = true;
                    btnExportarExcelExcedentes.Enabled = true;

                    gridEx.LlenarGrid(leer.DataSetClase); 
                }
            }
        }


        private void ObtenerExcedentes()
        {
            ObtenerExcedentes(1); 
        }

        private void ObtenerExcedentes(int Tipo)
        {
            string sSql = "", sWhereCaducidad = string.Format(" And MesesParaCaducar >= {0}", nmMesesCaducidad.Value);
            int iEsAlmacen = 0;

            if (rdoProximoCaducar.Checked)
            {
                sWhereCaducidad = string.Format(" And MesesParaCaducar Between 1 And {0}", nmMesesCaducidad.Value);
            }

            if (DtGeneral.EsAlmacen && GnFarmacia.ManejaUbicaciones)
            {
                iEsAlmacen = 1;

                sSql = string.Format("Exec spp_INV_Consumos_Claves @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', " + 
                    " @FechaRevision = '{3}', @MesRevision = '{4}', @MesesConsumo = '{5}', @TipoInformacion = '1', " + 
                    " @WhereMesesPorCaducar = '{7}', @EsAlmacen = '{8}', @IdPasillo = '{9}', @IdEstante = '{10}', @IdEntrepaño = '{11}'  ",
                    sIdEmpresa, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                    General.FechaYMD(General.FechaSistema), (int)nmMesesRevision.Value, (int)nmMesesExistencia.Value, Tipo, sWhereCaducidad,
                    iEsAlmacen, cboPasillos.Data, cboEstantes.Data, cboEntrepanos.Data);

            }
            else
            {
                sSql = string.Format("Exec spp_INV_Consumos_Claves @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', " + 
                    " @FechaRevision = '{3}', @MesRevision = '{4}', @MesesConsumo = '{5}', @TipoInformacion = '1', @WhereMesesPorCaducar = '{7}'  ",
                    sIdEmpresa, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                    General.FechaYMD(General.FechaSistema), (int)nmMesesRevision.Value, (int)nmMesesExistencia.Value, Tipo, sWhereCaducidad);
            }

            lst.Limpiar();
            gridEx.Limpiar(); 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ObtenerExcedentes()");
                General.msjError("Ocurrió un error al obtener la lista de Claves con excedente."); 
            }
            else
            {
                if (leer.Leer())
                {
                    leerExcedente.DataSetClase = leer.DataSetClase;  

                    btnNuevoFaltantes.Enabled = true; 
                    btnEjecutarFaltantes.Enabled = true; 
                    btnGenerarDistribucion.Enabled = true;
                    btnExportarExcel.Enabled = true;
                    btnExportarExcelExcedentes.Enabled = true;

                    //Se activan los botones de otras jurisdicciones para permitir la distribucion de estas.
                    btnNuevoFaltantesOtrasJuris.Enabled = true;
                    btnEjecutarFaltantesOtrasJuris.Enabled = true;
                    btnGenerarDistribucionOtrasJuris.Enabled = true;


                    gridEx.LlenarGrid(leer.DataSetClase);
                    gridEx.SetValue((int)Cols_Excedentes.Distribuir, 1);


                    ////////lst.CargarDatos(leer.DataSetClase, true, true);
                    ////////lst.AnchoColumna((int)Cols_Excedentes.Descripcion, 550);
                }
            } 
        }

        private bool PrepararClaves_Procesar()
        {
            bool bRegresa = false;
            string sSql = ""; 
            string sClaveSSA = "";
            int iExcedente = 0;
            int iMesesAnalisis = (int)nmMesesRevision.Value;
            int iMesesExistencia = (int)nmMesesExistencia.Value; 

            sClaveSSA_Procesar = "";

            if (cnn.Abrir())
            {
                bRegresa = true; 
                // cnn.IniciarTransaccion();

                sSql = "Delete From INV_Distribucion_Excedentes \n";
                sSql += string.Format("Delete From INV_Distribucion_Faltantes Where IdEstado = '{0}' \n", sIdEstado);
                sSql += string.Format("Delete From INV_Distribucion_Existencias Where IdEstado = '{0}' ", sIdEstado);


                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    Error.GrabarError(leer, "PrepararClaves_Procesar()"); 
                    ////General.msjError("Ocurrió un error al obtener el Promedio de Consumo Mensual");  
                }
                else 
                {
                    for (int i = 1; i <= gridEx.Rows; i++)
                    {
                        if (gridEx.GetValueBool(i, (int)Cols_Excedentes.Distribuir))
                        {
                            sClaveSSA = gridEx.GetValue(i, (int)Cols_Excedentes.ClaveSSA);
                            iExcedente = gridEx.GetValueInt(i, (int)Cols_Excedentes.Excedente);

                            sClaveSSA_Procesar += string.Format("'{0}', ", sClaveSSA);

                            sSql = string.Format("Exec spp_INV_Excedentes '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}' ",
                                sIdEmpresa, sIdEstado, sIdFarmacia, iMesesAnalisis, iMesesExistencia, sClaveSSA, iExcedente);
                            if (!leer.Exec(sSql))
                            {
                                bRegresa = false;
                                break;
                            }
                        }
                    }

                    if (sClaveSSA_Procesar != "") 
                    {
                        sClaveSSA_Procesar = sClaveSSA_Procesar.Trim();
                        sClaveSSA_Procesar = Fg.Mid(sClaveSSA_Procesar, 1, sClaveSSA_Procesar.Length - 1); 
                    }
                }

                if (bRegresa)
                {
                    // cnn.CompletarTransaccion(); 

                    ////////////btnNuevoFaltantesOtrasJuris.Enabled = true;  
                    ////////////btnEjecutarFaltantesOtrasJuris.Enabled = true;
                    ////////////btnGenerarDistribucionOtrasJuris.Enabled = true; 
                }
                else 
                {
                    // cnn.DeshacerTransaccion();
                    General.msjError("Error al procesar distribución."); 
                }
    
                cnn.Cerrar(); 
            }

            return bRegresa; 
        }

        private bool Obtener_PCM(string IdJurisdiccion)
        {
            bool bRegresa = false;
            string sSql = "";
            string sTabla = string.Format("tmpPCM_{0}", General.MacAddress); 

            DataSet dtsPCM = new DataSet();
            clsLeerWebExt leerWeb = new clsLeerWebExt(sUrlServidorRegional, DtGeneral.CfgIniOficinaCentral, datosCliente);

            sSql = string.Format(
                "If Not Exists ( Select Name From sysobjects (noLock) Where Name = '{0}' and xType = 'U' ) \n " + 
	            "Begin \n " + 
		        "    Select top 0 IdEmpresa, IdEstado, IdFarmacia, IdJurisdiccion, ClaveSSA, \n " + 
			    "        cast(0 as int) as Consumo, cast(0 as int) as ConsumoMensual, cast(0 as int) as StockSugerido \n " + 
		        "    Into {0}  \n " +
                "    From ADMI_Consumos_Claves 	\n " + 
                "End \n \n ", sTabla); 

            sSql += string.Format("Exec spp_INV_PCM_Consumos_Claves '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '1', '{7}' \n \n ",
                            sIdEmpresa, sIdEstado, sIdFarmacia, IdJurisdiccion,
                            General.FechaYMD(General.FechaSistema), (int)nmMesesRevision.Value, (int)nmMesesExistencia.Value, sTabla);

            sSql += string.Format( 
                " Select IdEmpresa, IdEstado, IdFarmacia, IdJurisdiccion, ClaveSSA, Consumo, ConsumoMensual, StockSugerido " + 
                " From {0} (NoLock) " + 
                " Where ClaveSSA in ( {1} ) ", sTabla, sClaveSSA_Procesar);  

            iConsultando = 0; 
            if (IdJurisdiccion == sIdJurisdiccion)
            {
                lstFaltantesJuris.Limpiar();
            }
            else
            {
                lstFaltantesOtrasJuris.Limpiar();
            }

            if (!leerWeb.Exec(sSql)) 
            {
                Error.GrabarError(General.DatosConexion, leerWeb.Error, "Obtener_PCM");
                General.msjError("Ocurrió un error al obtener el Promedio de Consumo Mensual"); 
            }
            else 
            {
                if (!leerWeb.Leer())
                {
                    General.msjUser("No se encontrarón Unidades con PCM de las claves solicitadas."); 
                }
                else 
                {
                    bRegresa = true;
                    dtsPCM = leerWeb.DataSetClase;

                    sSql = string.Format("If Exists ( Select Name From sysobjects (noLock) Where Name = '{0}' and xType = 'U') Drop Table {0} ", sTabla);
                    leerWeb.Exec(sSql); 
                }
            } 

            if (bRegresa)
            {
                if (Registrar_PCM(dtsPCM, IdJurisdiccion))
                {
                    if (Mostrar_PCM(IdJurisdiccion))
                    {
                        ObtenerExistencias(IdJurisdiccion); 
                        System.Threading.Thread.Sleep(1000); 
                        Mostrar_PCM(IdJurisdiccion, 1);

                        int iPage = tabControl.SelectedIndex;  
                        tabControl.SelectTab(3); 
                        tabControl.SelectTab(iPage); 

                    }
                } 
            }

            return bRegresa; 
        }

        private bool Registrar_PCM(DataSet PCM, string IdJurisdiccion)
        { 
            bool bRegresa = false;
            string sSql = "";
            clsLeer l = new clsLeer();
            clsLeer leerPCM_Aux = new clsLeer(ref cnnPCM);

            sSql = string.Format("Delete From INV_Distribucion_Faltantes " + 
                " Where IdEstado = '{0}' and IdJurisdiccion = '{1}' ", sIdEstado, IdJurisdiccion);
            if (leerPCM_Aux.Exec(sSql))
            {
                l.DataSetClase = PCM;
                while (l.Leer())
                {
                    bRegresa = true;
                    sSql = string.Format("Insert Into INV_Distribucion_Faltantes ( IdEmpresa, IdEstado, IdFarmacia, IdJurisdiccion, ClaveSSA, Consumo, ConsumoMensual, StockSugerido, StockAsignado )  \n");
                    sSql += string.Format("Select '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', 0 ",
                        l.Campo("IdEmpresa"), l.Campo("IdEstado"), l.Campo("IdFarmacia"), l.Campo("IdJurisdiccion"),
                        l.Campo("ClaveSSA"), l.CampoInt("Consumo"), l.CampoInt("ConsumoMensual"), l.CampoInt("StockSugerido"));
                    if (!leerPCM.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    } 
                } 

                if (bRegresa)
                {
                    sSql = string.Format("Delete From INV_Distribucion_Faltantes " +
                        " Where IdEstado = '{0}'  " + 
                        "   And Not Exists " + 
                        "   ( " + 
                        "       Select * From INV_Distribucion_Excedentes E (NoLock) " +
                        "       Where E.ClaveSSA = INV_Distribucion_Faltantes.ClaveSSA " + 
                        "   )  ", sIdEstado );
                    bRegresa = leer.Exec(sSql); 
                } 
            }

            return bRegresa; 
        }

        private bool Mostrar_PCM(string IdJurisdiccion)
        {
            return Mostrar_PCM(IdJurisdiccion, 0); 
        }

        private bool Mostrar_PCM(string IdJurisdiccion, int Tipo)
        {
            bool bRegresa = false; 
            string sSql = "";
            // string sJoin = "Left Join ";
            string sValidado = "''";  

            leerPCM = new clsLeer(ref cnnPCM); 

            //////if (Tipo > 0) 
            //////    sJoin = "Inner Join "; 
            if (Tipo > 0)
            {
                sValidado = " ( case when IsNull(E.Existencia, -1) < 0 then 'NO' else 'SI' end) "; 
            }


            sSql = string.Format("" +
                " Select 'Núm. Unidad' = F.IdFarmacia, 'Nombre unidad' = F.Farmacia, 'Jurisdicción' = F.IdJurisdiccion + ' -- ' + F.Jurisdiccion, \n " +
                " 'Tipo de unidad' = F.TipoDeUnidad, " +
                " 'Clave SSA' = D.ClaveSSA, " +
                " 'Descripción Clave' = ( Select Top 1 DescripcionClave From vw_ClavesSSA_Sales C Where D.ClaveSSA = C.ClaveSSA ), \n " +
                " 'Consumo' = D.Consumo, 'Consumo mensual' = ConsumoMensual, IsNull(E.Existencia, 0) as Existencia, " + 
                " 'Existencia validada' = {1},  \n " +
                " 'Stock faltante' = (case when (D.StockSugerido - IsNull(E.Existencia, 0)) > 0 then (D.StockSugerido - IsNull(E.Existencia, 0)) else 0 end), \n " +
                " 'Stock asignado' = D.StockAsignado \n " +
                " From INV_Distribucion_Faltantes D (NoLock) \n " +
                " Inner Join vw_Farmacias F (NoLock) On ( D.IdEstado = F.IdEstado and D.IdFarmacia = F.IdFarmacia ) \n  " +
                " Left Join INV_Distribucion_Existencias E (NoLock) On ( D.IdEmpresa = E.IdEmpresa and D.IdEstado = E.IdEstado and D.IdFarmacia = E.IdFarmacia and D.ClaveSSA = E.ClaveSSA ) \n  " +
                " Where D.IdJurisdiccion = '{0}' \n  " +
                " Order by F.IdTipoUnidad, D.StockSugerido Desc ", IdJurisdiccion, sValidado);

            if (IdJurisdiccion == sIdJurisdiccion)
            {
                lstFaltantesJuris.Limpiar();
            }
            else 
            {
                lstFaltantesOtrasJuris.Limpiar();
            }

            //// 
            if (!leerPCM.Exec(sSql))
            {
                Error.GrabarError(leerPCM, "Mostrar_PCM"); 
            }
            else 
            {
                if (!leerPCM.Leer()) 
                {
                    General.msjUser("No se encontrarón Unidades con faltante de las Claves seleccionadas.");  
                }
                else 
                {
                    bRegresa = true; 
                    if (IdJurisdiccion == sIdJurisdiccion)
                    {
                        lstFaltantesJuris.CargarDatos(leerPCM.DataSetClase, true, true);
                    }
                    else
                    {
                        lstFaltantesOtrasJuris.CargarDatos(leerPCM.DataSetClase, true, true);
                    }
                }
            }

            //  Tiempo de espera 
            Thread.Sleep(1000); 

            return bRegresa; 
        }

        private bool DistribuirExcedente(string IdJurisdiccion)
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_INV_Distribucion_Claves_Excedentes '{0}', '{1}', '{2}'", sIdEmpresa, sIdEstado, IdJurisdiccion);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "DistribuirExcedente");
                General.msjError("Ocurrió un error al generar la distribución."); 
            }
            else
            {
                bRegresa = true; 
            }

            if (bRegresa)
            {
                Mostrar_PCM(IdJurisdiccion, 1);

                ////if (IdJurisdiccion != sIdJurisdiccion)
                ////{
                ////    ObtenerJurisdicciones_SinDistribucion();
                ////} 
            }

            return bRegresa; 
        }

        #endregion Funciones y Procedimientos Privados  

        #region Exportar Excedentes Distribucion
        private bool GenerarExcel_Excedentes()
        {
            bool bRegresa = false;
            btnExportarExcelExcedentes.Enabled = false;

            // if (GetDatosExcel_Distribucion()) 
            {
                ExportarExcel_Excedentes();
            }

            btnExportarExcelExcedentes.Enabled = true;
            return bRegresa;
        }

        private void ExportarExcel_Excedentes()
        {
            clsGenerarExcel generarExcel = new clsGenerarExcel();
            int iColBase = 2;
            int iRenglon = 2;
            string sNombreHoja = "";
            //string sNombre = string.Format("PERIODO DEL {0} AL {1} ", General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
            string sNombreFile = "Excedentes";

            //leer.DataSetClase = leerExportarExcel.DataSetClase;

            leer.RegistroActual = 1;


            int iColsEncabezado = iColBase + leer.Columnas.Length - 1;
            //iColsEncabezado = iColBase + 8;

            generarExcel.RutaArchivo = @"C:\\Excel";
            generarExcel.NombreArchivo = "";
            generarExcel.AgregarMarcaDeTiempo = true;

            if (generarExcel.PrepararPlantilla(sNombreFile))
            {
                sNombreHoja = "Hoja1";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre);
                //generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, sNombre);
                iRenglon++;
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 14, string.Format("Fecha de generación: {0} ", General.FechaSistema), XLAlignmentHorizontalValues.Left);
                iRenglon++;

                generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);
                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                //generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(iColBase, iColBase).Width = 30;

                generarExcel.CerraArchivo();

                generarExcel.AbrirDocumentoGenerado(true);
            }

        }


        /*
        private bool ExportarExcel_Excedentes()
        {
            bool bRegresa = false;
            // int iRenglon = 8;
            int iRow = 8;

            string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\INV_Excedentes_Claves.xls";
            DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "INV_Excedentes_Claves.xls", datosCliente);

            xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
            xpExcel.AgregarMarcaDeTiempo = true;

            this.Cursor = Cursors.Default;
            if (xpExcel.PrepararPlantilla())
            {
                this.Cursor = Cursors.WaitCursor;
                xpExcel.GeneraExcel();

                xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 2);
                xpExcel.Agregar(DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre, 3, 2);
                xpExcel.Agregar("Fecha de impresión: " + leer.CampoFecha("FechaReporte").ToString(), 6, 2);

                // iRenglon = 9;
                iRow = 9;

                leerExcedente.RegistroActual = 1; 
                while (leerExcedente.Leer())
                {
                    xpExcel.Agregar(leerExcedente.Campo("Clave SSA"), iRow, 2);
                    xpExcel.Agregar(leerExcedente.Campo("Descripción Clave"), iRow, 3);

                    xpExcel.Agregar(leerExcedente.CampoInt("Consumo"), iRow, 4);
                    xpExcel.Agregar(leerExcedente.CampoInt("Stock Sugerido"), iRow, 5);
                    xpExcel.Agregar(leerExcedente.CampoInt("Existencia"), iRow, 6);
                    xpExcel.Agregar(leerExcedente.CampoInt("Excedente"), iRow, 7); 

                    iRow++;
                }

                // Finalizar el Proceso 
                xpExcel.CerrarDocumento();

                if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                {
                    xpExcel.AbrirDocumentoGenerado();
                }
            }
            this.Cursor = Cursors.Default;

            return bRegresa;

        }
        */

        private bool GenerarExcel_Excedentes_Distribuido()
        {
            bool bRegresa = false;
            btnExportarDistribuido.Enabled = false;

            // if (GetDatosExcel_Distribucion()) 
            {
                if (!Obtener_Distribucion_Generada())
                {
                    General.msjAviso("No se encontro información para exportar"); 
                }
                else
                {
                    ExportarExcel_Excedentes_Distribuidos();
                }
            }

            btnExportarDistribuido.Enabled = true;
            return bRegresa;
        }

        private void ExportarExcel_Excedentes_Distribuidos()
        {
            clsGenerarExcel generarExcel = new clsGenerarExcel();
            int iColBase = 2;
            int iRenglon = 2;
            string sNombreHoja = "";
            //string sNombre = string.Format("PERIODO DEL {0} AL {1} ", General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
            string sNombreFile = "Excedentes_Distribuidos";

            //leer.DataSetClase = leerExportarExcel.DataSetClase;

            leer.RegistroActual = 1;


            int iColsEncabezado = iColBase + leer.Columnas.Length - 1;
            //iColsEncabezado = iColBase + 8;

            generarExcel.RutaArchivo = @"C:\\Excel";
            generarExcel.NombreArchivo = "";
            generarExcel.AgregarMarcaDeTiempo = true;

            if (generarExcel.PrepararPlantilla(sNombreFile))
            {
                sNombreHoja = "Hoja1";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre);
                //generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, sNombre);
                iRenglon++;
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 14, string.Format("Fecha de generación: {0} ", General.FechaSistema), XLAlignmentHorizontalValues.Left);
                iRenglon++;

                generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);
                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                //generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(iColBase, iColBase).Width = 30;

                generarExcel.CerraArchivo();

                generarExcel.AbrirDocumentoGenerado(true);
            }

        }

        /*
        private bool ExportarExcel_Excedentes_Distribuidos()
        {
            bool bRegresa = false;
            // int iRenglon = 8;
            int iRow = 8;

            clsLeer leerDistr = new clsLeer();
            leerDistr.DataSetClase = dtsExcedenteDistribuido; 

            string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\INV_Excedentes_Claves_Distribuidas.xls";
            DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "INV_Excedentes_Claves_Distribuidas.xls", datosCliente);

            xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
            xpExcel.AgregarMarcaDeTiempo = true;

            this.Cursor = Cursors.Default;
            if (xpExcel.PrepararPlantilla())
            {
                this.Cursor = Cursors.WaitCursor;
                xpExcel.GeneraExcel();

                xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 2);
                xpExcel.Agregar(DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre, 3, 2);
                xpExcel.Agregar("Fecha de impresión: " + General.FechaSistema.ToString(), 6, 2);

                // iRenglon = 9;
                iRow = 9;

                leerDistr.RegistroActual = 1;
                while (leerDistr.Leer())
                {
                    xpExcel.Agregar(leerDistr.Campo("Clave SSA"), iRow, 2);
                    xpExcel.Agregar(leerDistr.Campo("Descripción Clave"), iRow, 3);

                    xpExcel.Agregar(leerDistr.Campo("Presentación"), iRow, 4);
                    xpExcel.Agregar(leerDistr.Campo("Insumo"), iRow, 5);

                    xpExcel.Agregar(leerDistr.CampoInt("Excedente"), iRow, 6);
                    xpExcel.Agregar(leerDistr.CampoInt("Distribuido"), iRow, 7);
                    xpExcel.Agregar(leerDistr.CampoInt("Sobrante"), iRow, 8); 

                    iRow++;
                }

                // Finalizar el Proceso 
                xpExcel.CerrarDocumento();

                if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                {
                    xpExcel.AbrirDocumentoGenerado();
                }
            }
            this.Cursor = Cursors.Default;

            return bRegresa;

        }
        */
        #endregion Exportar Excedentes Distribucion

        #region Exportar Distribucion
        private bool GenerarExcel_Distribucion()
        {
            bool bRegresa = false;
            btnExportarExcel.Enabled = false;

            if (GetDatosExcel_Distribucion())
            {
                ExportarExcel_Distribucion();
            }

            btnExportarExcel.Enabled = true;
            return bRegresa;
        }

        private bool GetDatosExcel_Distribucion()
        {
            bool bRegresa = false;
            string sSql = "";

            sSql = string.Format("" +
                " Select D.IdFarmacia, F.Farmacia, D.IdJurisdiccion, F.Jurisdiccion, F.TipoDeUnidad, " +
                " D.ClaveSSA, 'DescripcionClave' = ( Select Top 1 DescripcionClave From vw_ClavesSSA_Sales C Where D.ClaveSSA = C.ClaveSSA ), " +
                " D.Consumo, D.ConsumoMensual, D.StockSugerido, D.StockAsignado, getdate() as FechaReporte " +
                " From INV_Distribucion_Faltantes D (NoLock) " +
                " Inner Join vw_Farmacias F (NoLock) On ( D.IdEstado = F.IdEstado and D.IdFarmacia = F.IdFarmacia )" +
                " Where D.StockAsignado > 0 " + 
                " Order by D.IdJurisdiccion, D.IdFarmacia, D.ClaveSSA ");

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "GetDatosExcel()"); 
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjUser("No se encontro información para generar el documento, verifique."); 
                }
                else
                {
                    bRegresa = true;
                }
            }

            return bRegresa; 
        }

        private void ExportarExcel_Distribucion()
        {
            clsGenerarExcel generarExcel = new clsGenerarExcel();
            int iColBase = 2;
            int iRenglon = 2;
            string sNombreHoja = "";
            //string sNombre = string.Format("PERIODO DEL {0} AL {1} ", General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
            string sNombreFile = "Distribucion";

            //leer.DataSetClase = leerExportarExcel.DataSetClase;

            leer.RegistroActual = 1;


            int iColsEncabezado = iColBase + leer.Columnas.Length - 1;
            //iColsEncabezado = iColBase + 8;

            generarExcel.RutaArchivo = @"C:\\Excel";
            generarExcel.NombreArchivo = "";
            generarExcel.AgregarMarcaDeTiempo = true;

            if (generarExcel.PrepararPlantilla(sNombreFile))
            {
                sNombreHoja = "Hoja1";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre);
                //generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, sNombre);
                iRenglon++;
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 14, string.Format("Fecha de generación: {0} ", General.FechaSistema), XLAlignmentHorizontalValues.Left);
                iRenglon++;

                generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);
                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                //generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(iColBase, iColBase).Width = 30;

                generarExcel.CerraArchivo();

                generarExcel.AbrirDocumentoGenerado(true);
            }

        }

        /*
        private bool ExportarExcel_Distribucion() 
        {
            bool bRegresa = false;
            // int iRenglon = 8; 
            int iRow = 8;  

            string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\INV_Distribucion_Excedentes.xls";  
            DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "INV_Distribucion_Excedentes.xls", datosCliente); 

            xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
            xpExcel.AgregarMarcaDeTiempo = true;

            this.Cursor = Cursors.Default;
            if (xpExcel.PrepararPlantilla())
            {
                this.Cursor = Cursors.WaitCursor;
                xpExcel.GeneraExcel();

                xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 2);
                xpExcel.Agregar(DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre, 3, 2);
                xpExcel.Agregar("Fecha de impresión: " + leer.CampoFecha("FechaReporte").ToString(), 6, 2);

                // iRenglon = 9;
                iRow = 9;

                leer.RegistroActual = 1; 
                while (leer.Leer())
                { 
                    xpExcel.Agregar(leer.Campo("IdJurisdiccion"), iRow, 2);
                    xpExcel.Agregar(leer.Campo("Jurisdiccion"), iRow, 3);
                    xpExcel.Agregar(leer.Campo("IdFarmacia"), iRow, 4);
                    xpExcel.Agregar(leer.Campo("Farmacia"), iRow, 5);

                    xpExcel.Agregar(leer.Campo("ClaveSSA"), iRow, 6);
                    xpExcel.Agregar(leer.Campo("DescripcionClave"), iRow, 7); 

                    xpExcel.Agregar(leer.CampoInt("Consumo"), iRow, 8);
                    xpExcel.Agregar(leer.CampoInt("ConsumoMensual"), iRow, 9);
                    xpExcel.Agregar(leer.CampoInt("StockSugerido"), iRow, 10);
                    xpExcel.Agregar(leer.CampoInt("StockAsignado"), iRow, 11);  

                    iRow++;
                }

                // Finalizar el Proceso 
                xpExcel.CerrarDocumento();

                if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                {
                    xpExcel.AbrirDocumentoGenerado();
                }
            }
            this.Cursor = Cursors.Default;

            return bRegresa;

        }
        */
        #endregion Exportar Distribucion

        #region Obtener Existencias 
        private class DatosGetExistencia
        {
            public string IdEmpresa = "";
            public string IdEstado = "";
            public string IdFarmacia = ""; 
            public string IdJurisdiccion = "";
            public string Url = ""; 
        }

        private void ObtenerExistencias(string IdJurisdiccion)
        {
            clsConexionSQL cnnExistencias = new clsConexionSQL(General.DatosConexion);
            clsLeer leerExistenciaUnidades = new clsLeer(ref cnnExistencias);
            iFarmacias_Proceso = 0;
            iFarmacias_Procesadas = 0; 

            string sSql = string.Format( 
                "Select U.IdEmpresa, U.IdEstado, U.IdFarmacia, F.IdJurisdiccion, U.UrlFarmacia " + 
                "From vw_Farmacias_Urls U (NoLock) " + 
	            "Inner Join CatFarmacias F (NoLock) On ( U.IdEstado = F.IdEstado and U.IdFarmacia = F.IdFarmacia ) " + 
                "Where U.IdEstado = '{0}' and F.IdJurisdiccion = '{1}' and F.IdFarmacia Not in ( '{2}' ) and StatusUrl = 'A' " +
                "   and Exists " + 
                " ( " + 
                "       Select IdFarmacia From INV_Distribucion_Faltantes E (NoLock)  " +
                "       Where E.IdEstado = U.IdEstado and E.IdFarmacia = U.IdFarmacia " + 
                " )  " , sIdEstado, IdJurisdiccion, sIdFarmacia );


            leerExistenciaUnidades.Exec(sSql);

            iFarmacias_Procesadas = 0; 
            iFarmacias_Proceso = leerExistenciaUnidades.Registros; 

            while (leerExistenciaUnidades.Leer())
            {
                DatosGetExistencia d = new DatosGetExistencia();
                d.IdEmpresa = leerExistenciaUnidades.Campo("IdEmpresa");
                d.IdEstado = leerExistenciaUnidades.Campo("IdEstado");
                d.IdFarmacia = leerExistenciaUnidades.Campo("IdFarmacia"); 
                d.IdJurisdiccion = IdJurisdiccion;
                d.Url = leerExistenciaUnidades.Campo("UrlFarmacia");
                iConsultando++;



                this.ObtenerExistencia_Unidad(d); 

                ////////////////Thread th = new Thread(this.ObtenerExistencia_Unidad);
                ////////////////th.Name = string.Format("Get_Existencia__{0}", d.IdFarmacia);
                ////////////////th.Start(d);
                //////////////// break; 
            }

            while (iConsultando > 0)
            {
                Thread.Sleep(500); 
            }
        }

        private void ObtenerExistencia_Unidad(object DireccionUrl) 
        {
            DatosGetExistencia Url = (DatosGetExistencia)DireccionUrl; 
            clsLeerWebExt l = new clsLeerWebExt(Url.Url, DtGeneral.CfgIniPuntoDeVenta, datosCliente);
            clsConexionSQL cnnLocal = new clsConexionSQL(General.DatosConexion);
            clsLeer leerLocal = new clsLeer(ref cnnLocal); 

            string sSql = string.Format(
                " Select E.IdEmpresa, E.IdEstado, E.IdFarmacia, right('0000' + {3}, 3) as IdJurisdiccion, E.ClaveSSA, cast(sum(E.Existencia) as int) as Existencia  " +
                " From vw_ExistenciaPorProducto E (NoLock) " + 
	            ////" Inner Join vw_Productos P (NoLock) On ( E.IdProducto = P.IdProducto ) " +
                " Where E.IdEmpresa = '{0}' and E.IdEstado = '{1}' and E.IdFarmacia = '{2}' and E.ClaveSSA in ( {4} )  " + 
	            " Group by E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.ClaveSSA",
                Url.IdEmpresa, Url.IdEstado, Url.IdFarmacia, Url.IdJurisdiccion, sClaveSSA_Procesar);


            RegistrarAvance_Existencias(Url.IdJurisdiccion);

            l.TimeOut = 500000; 
            if (!l.Exec(sSql))
            {
                Error.GrabarError(General.DatosConexion, l.Error, "ObtenerExistencia_Unidad"); 
            }
            else
            {
                while (l.Leer())
                {
                    sSql = string.Format("Insert Into INV_Distribucion_Existencias ( IdEmpresa, IdEstado, IdFarmacia, IdJurisdiccion, ClaveSSA, Existencia )  \n");
                    sSql += string.Format("Select '{0}', '{1}', '{2}', '{3}', '{4}', '{5}'  ",
                        Url.IdEmpresa, Url.IdEstado, Url.IdFarmacia, Url.IdJurisdiccion,
                        l.Campo("ClaveSSA"), l.CampoInt("Existencia")); 
                    if (!leerLocal.Exec(sSql)) 
                    {
                        // break;
                    }
                } 
            }

            iFarmacias_Procesadas++;  
            iConsultando--;
            RegistrarAvance_Existencias(Url.IdJurisdiccion); 
        }

        private void RegistrarAvance_Existencias(string IdJurisdiccion)
        {
            string sMsj = string.Format("Procesado {0} de {1} ", iFarmacias_Procesadas, iFarmacias_Proceso); 

            if (IdJurisdiccion == sIdJurisdiccion)
            {
                lblProceso_Juris.Text = sMsj;
            }
            else
            {
                lblProceso_OtrasJuris.Text = sMsj;
            }
        }
        #endregion Obtener Existencias  

        #region Obtener y Exportar Distribucion  
        private bool Obtener_Distribucion_Generada()
        {
            bool bRegresa = false; 
            string sSql = string.Format(
                "Select " + 
                " 'Clave SSA' = E.ClaveSSA, 'Descripción clave' = C.DescripcionClave, " + 
                " 'Presentación' = C.Presentacion, 'Insumo' = C.TipoDeClaveDescripcion, " + 
                " E.Excedente, E.Distribuido, (E.Excedente - E.Distribuido) as Sobrante " + 
                " From INV_Distribucion_Excedentes E (NoLock) " + 
                " Inner Join vw_ClavesSSA_Sales C (NoLock) On ( E.ClaveSSA = C.ClaveSSA )" + 
                " Where IdEstado = '{0}' and IdFarmacia = '{1}' ", sIdEstado, sIdFarmacia ) ;

            lstDistribuido.Limpiar(); 
            if (!leer.Exec(sSql)) 
            {
                Error.GrabarError(leer, "Obtener_Distribucion_Generada()");  
            }
            else
            {
                if (leer.Leer()) 
                {
                    dtsExcedenteDistribuido = leer.DataSetClase; 
                    lstDistribuido.CargarDatos(leer.DataSetClase, true, true);
                    lstDistribuido.AnchoColumna(2, 350);
                    bRegresa = true; 
                }
            }

            return bRegresa; 

        } 
        #endregion Obtener y Exportar Distribucion  

        #region Ubicaciones
        private void CargarPasillos()
        {
            cboPasillos.Clear();
            cboPasillos.Add("*", "<< Todo >>");

            string sSql = string.Format(" Select IdPasillo, ( Cast(IdPasillo As varchar) + ' -- ' + DescripcionPasillo ) as Pasillo " +
                                    " From CatPasillos (Nolock) " +
                                    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' Order By IdPasillo ",
                                    sIdEmpresa, sIdEstado, sIdFarmacia);

            if (!leer.Exec(sSql))
            {
                General.msjError("Ocurrió Un Error al buscar la Información.");
            }
            else
            {
                if (leer.Leer())
                {
                    cboPasillos.Add(leer.DataSetClase, true);
                }
            }
            cboPasillos.SelectedIndex = 0;
        }

        private void CargarEstantes()
        {
            cboEstantes.Clear();
            cboEstantes.Add("*", "<< Todo >>");

            string sSql = string.Format(" Select IdEstante, (Cast(IdEstante As varchar) + ' -- ' + DescripcionEstante ) as Estante " +
                                    " From CatPasillos_Estantes (Nolock) " +
                                    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And IdPasillo = '{3}' Order By IdEstante ",
                                    sIdEmpresa, sIdEstado, sIdFarmacia, cboPasillos.Data);

            if (!leer.Exec(sSql))
            {
                General.msjError("Ocurrió Un Error al buscar la Información.");
            }
            else
            {
                if (leer.Leer())
                {
                    cboEstantes.Add(leer.DataSetClase, true);
                }
            }
            cboEstantes.SelectedIndex = 0;
        }

        private void CargaEntrepanos()
        {
            // int iCont = 0;
            // string sText = "";

            cboEntrepanos.Clear();
            cboEntrepanos.Add("*", "<< Todo >>");

            string sSql = string.Format(" Select IdEntrepaño, (Cast(IdEntrepaño As varchar) + ' -- ' + DescripcionEntrepaño ) as Entrepaño " +
                                    " From CatPasillos_Estantes_Entrepaños (Nolock) " +
                                    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}'  " +
                                    " And IdPasillo = '{3}' And IdEstante = '{4}' Order By IdEntrepaño ",
                                    sIdEmpresa, sIdEstado, sIdFarmacia, cboPasillos.Data, cboEstantes.Data);

            if (!leer.Exec(sSql))
            {
                General.msjError("Ocurrió Un Error al buscar la Información.");
            }
            else
            {
                if (leer.Leer())
                {
                    cboEntrepanos.Add(leer.DataSetClase, true);
                }
            }

            cboEntrepanos.SelectedIndex = 0;
        }

        private void cboPasillos_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboEstantes.Clear();
            cboEstantes.Add("0", "<< Todo >>");
            cboEstantes.SelectedIndex = 0;
            cboEntrepanos.Clear();
            cboEntrepanos.Add("0", "<< Todo >>");
            cboEntrepanos.SelectedIndex = 0;

            if (cboPasillos.SelectedIndex != 0)
            {
                CargarEstantes();
            }
        }

        private void cboEstantes_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboEntrepanos.Clear();
            cboEntrepanos.Add("0", "<< Todo >>");
            cboEntrepanos.SelectedIndex = 0;

            if (cboEstantes.SelectedIndex != 0)
            {
                CargaEntrepanos();
            }
        }
        #endregion Ubicaciones

        #region Caducidades 
        private void nmMesesCaducidad_ValueChanged(object sender, EventArgs e)
        {
            TituloCadudicades((int)nmMesesCaducidad.Value); 
        }

        private void TituloCadudicades(int Meses)
        {
            string sCaducidad = "";
            string sProximos = "";

            sCaducidad = string.Format("Caducidad de mas de {0} meses", Meses);
            sProximos = string.Format("Próximos a caducar ( de 1 a {0} meses )", Meses);

            rdoBuenaCaducidad.Text = sCaducidad;
            rdoProximoCaducar.Text = sProximos;
        }
        #endregion Caducidades
    }
}
