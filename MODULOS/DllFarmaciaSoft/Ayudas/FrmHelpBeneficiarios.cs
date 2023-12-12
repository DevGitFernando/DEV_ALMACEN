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
using SC_SolutionsSystem.FuncionesGenerales;

using DllFarmaciaSoft;

namespace DllFarmaciaSoft.Ayudas
{
    public partial class FrmHelpBeneficiarios : FrmBaseExt 
    {
        enum Cols
        {
            Folio = 1, Referencia = 2, EsVigente = 3, ApPaterno = 4, ApMaterno = 5, Nombre = 6,
            Sexo = 7, FechaNacimiento = 8, Edad = 9,
            FechaInicioVigencia = 10, FechaFinVigencia = 11, NumeroDeControl = 12
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeerWebExt leerWeb; 
        clsListView myListView;
        clsConsultas query;

        public string strResultado = "";
        // private bool bFormatearCamposNumericos = false;
        private int intColumnas = 0;
        // private bool bMuestraPantalla = true;

        public bool bAccesarA_BD_Local = false;
        DataSet dtResultado = new DataSet();

        // Variables para Controlar la informacion que se carga 
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        string sIdCliente = "";
        string sIdSubCliente = "";
        string sArchivoPadron = DtGeneral.CfgIniOficinaCentral;

        bool bFormatearReferencia = GnFarmacia.PadronFormatearRerefencia; 
        int iLargoReferencia = GnFarmacia.PadronLongitudRerefencia;
        int iLargoApellidoPaterno = GnFarmacia.PadronLongitudApellidoPaterno;
        int iLargoNombre = GnFarmacia.PadronLongitudNombre; 

        string sUrlCentral = ""; 

        public FrmHelpBeneficiarios()
        {
            InitializeComponent();

            // Para el manejo simplicado de Hilos 
            CheckForIllegalCrossThreadCalls = false; 

            //sIdCliente = Cliente;
            //sIdSubCliente = SubCliente;
            myListView = new clsListView(lvBeneficiarios);

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            cboSexo.Add("0", "Ambos");
            cboSexo.Add("F", "Femenino");
            cboSexo.Add("M", "Masculino");
            cboSexo.SelectedIndex = 0;

            cboVigencia.Add("2", "TODOS");
            cboVigencia.Add("1", "SI");
            cboVigencia.Add("0", "NO");
            cboVigencia.SelectedIndex = 1;  // Por Default solo mostrar los Beneficiarios Vigentes de SP 

        }

        private int Get()
        {
            return intColumnas;
        }

        private void FrmHelpBeneficiarios_Load(object sender, EventArgs e)
        {
            Fg.IniciaControles();
            dtpFechaNacimiento.MaxDate = General.FechaSistema;
            cboVigencia.SelectedIndex = 1;  // Por Default solo mostrar los Beneficiarios Vigentes de SP 

            lblProcesando.Visible = false;
            progressBar.Visible = false;
            txtNombre.Focus();

            contexMenuBeneficiarios.Visible = false; 
            contexMenuBeneficiarios.Enabled = false; 

            // NOTA : 
            // Armar y ejecutar Query, usar los metodos de myListView para llenar el ListView 


            chkImportarBeneficiarios.Text = "Buscar Beneficiarios en Central"; 
            if (DtGeneral.PadronLocal)
            {
                chkImportarBeneficiarios.Text = "Buscar Beneficiarios en Padrón"; 
                sUrlCentral = General.Url;
                sArchivoPadron = DtGeneral.CfgIniPuntoDeVenta; 
                leerWeb = new clsLeerWebExt(ref cnn, sUrlCentral, sArchivoPadron, new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmHelpBeneficiarios"));
            }
            else
            {
                // Cargar Direccion Url del Servidor Central de Farmacia 
                leer.DataSetClase = query.DireccionServidorCentralDeFarmacia("FrmHelpBeneficiarios_Load");
                if (leer.Leer())
                {
                    sUrlCentral = leer.Campo("Url");
                    leerWeb = new clsLeerWebExt(ref cnn, sUrlCentral, sArchivoPadron, new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmHelpBeneficiarios"));
                }
            }
        }

        public DataSet ShowHelp(string Cliente, string SubCliente, bool ImportarBeneficiarios)
        {
            dtResultado = new DataSet();

            sIdCliente = Cliente;
            sIdSubCliente = SubCliente;

            chkImportarBeneficiarios.Enabled = ImportarBeneficiarios;
            chkImportarBeneficiarios.Visible = ImportarBeneficiarios;

            lblVigencia.Visible = ImportarBeneficiarios;
            cboVigencia.Visible = ImportarBeneficiarios;
            cboVigencia.Enabled = ImportarBeneficiarios;

            this.ShowDialog(); 
            return dtResultado;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ObtenerInformacion();
        }

        #region List View  
        private void ObtenerInformacion()
        {
            try
            {
                // strResultado = lvBeneficiarios.FocusedItem.SubItems[intColumnas].Text.ToString();
                strResultado = myListView.GetValue(lvBeneficiarios.Columns.Count); // La ultima columna es al que contiene la Clave 
                if (strResultado == "*")
                {
                    if (IntegrarBeneficiario())
                    {
                        dtResultado = query.Beneficiarios(sEstado, sFarmacia, sIdCliente, sIdSubCliente, strResultado, "ObtenerInformacion");
                        this.Hide();  
                    }
                }
                else
                {
                    dtResultado = query.Beneficiarios(sEstado, sFarmacia, sIdCliente, sIdSubCliente, strResultado, "ObtenerInformacion");
                    this.Hide();
                }
            }
            catch
            {
            }
        }

        private bool IntegrarBeneficiario()
        {
            bool bRegresa = true;
            string sSql = ""; 

            int iOpcion = 1; // Registro y/o Actualizacion de datos 
            string sFolio = myListView.GetValue((int)Cols.Folio);
            string sRefencia = myListView.GetValue((int)Cols.Referencia); 
            string sEsVigente = myListView.GetValue((int)Cols.EsVigente).Substring(0, 1).ToUpper();

            string sApPat = myListView.GetValue((int)Cols.ApPaterno);
            string sApMat = myListView.GetValue((int)Cols.ApMaterno);
            string sNombre = myListView.GetValue((int)Cols.Nombre);
            string sSexo = myListView.GetValue((int)Cols.Sexo).Substring(0, 1);
            string sFechaNac = myListView.GetValue((int)Cols.FechaNacimiento);
            string sFechaIniVig = myListView.GetValue((int)Cols.FechaInicioVigencia);
            string sFechaFinVig = myListView.GetValue((int)Cols.FechaFinVigencia);

            string sMsj = string.Format("La información del Beneficiario {0} {1} {2}\ncon Folio [ {3} ] se integrará al Catálogo,\n\n¿ Desea continuar ?", sApPat, sApMat, sNombre, sFolio); 

            if (sEsVigente == "N")
            {
                bRegresa = false;
                General.msjAviso("El Beneficiario no cuenta con Vigencia, no es posible integrar la información."); 
            }
            else
            {
                if (General.msjConfirmar(sMsj) == DialogResult.No)
                {
                    bRegresa = false; 
                }
                else
                {
                    sSql = String.Format("Set Dateformat YMD Exec spp_Mtto_CatBeneficiarios " +
                        " '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}'  ",
                        DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sIdCliente, sIdSubCliente,
                        "**", sNombre, sApPat, sApMat, sSexo, sFechaNac, sFolio, sFechaIniVig, sFechaFinVig, iOpcion, "", sRefencia);

                    sSql = String.Format("Set Dateformat YMD Exec spp_Mtto_CatBeneficiarios " +
                        " @IdEstado = '{0}', @IdFarmacia = '{1}', @IdCliente = '{2}', @IdSubCliente = '{3}', @IdBeneficiario = '{4}', " + 
                        " @Nombre = '{5}', @ApPaterno = '{6}', @ApMaterno = '{7}', @Sexo = '{8}', @FechaNacimiento = '{9}', @FolioReferencia = '{10}', " +
                        " @FechaInicioVigencia = '{11}', @FechaFinVigencia = '{12}', @iOpcion = '{13}', @Domicilio = '{14}', @FolioReferenciaAuxiliar = '{15}', @IdPersonal = '{16}',  @TipoDeBenenficiario = '{17}' ",
                        DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sIdCliente, sIdSubCliente,"**", 
                        sNombre, sApPat, sApMat, sSexo, sFechaNac, sFolio, 
                        sFechaIniVig, sFechaFinVig, iOpcion, "", sRefencia, DtGeneral.IdPersonal, 1);


                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        Error.GrabarError(leer, "IntegrarBeneficiario()"); 
                        General.msjError("Ocurrió un error al Integrar la información del Beneficiario.");
                    }
                    else
                    {
                        leer.Leer();
                        strResultado = leer.Campo("Clave");
                    }
                }
            }

            return bRegresa; 
        }

        private void lvBeneficiarios_KeyDown(object sender, KeyEventArgs e)
        {
            object obj = new object();
            if (e.KeyCode == Keys.Return)
            {
                ObtenerInformacion();
            }
        }

        private void lvBeneficiarios_DoubleClick(object sender, EventArgs e)
        {
            ObtenerInformacion();
        }
        #endregion List View  

        private bool validaPametros()
        {
            bool bRegresa = true;

            if(txtFolio.Text.Trim() != "")
            {
                if(txtFolio.Text.Trim().Length < iLargoReferencia)
                {
                    bRegresa = false;
                    General.msjUser(string.Format("El Folio de Derechohabiente debe contener al menos {0} digitos, verifique.", iLargoReferencia));
                    txtFolio.Focus();
                }
            }
            else
            {
                if(txtCURP.Text.Trim() == "")
                {
                    if(bRegresa && txtPaterno.Text.Trim().Length < iLargoApellidoPaterno)
                    {
                        bRegresa = false;
                        General.msjUser(string.Format("Debe escribir como minímo {0} letras del Apellido Paterno, verifique.", iLargoApellidoPaterno));
                        txtPaterno.Focus();
                    }

                    if(bRegresa && txtNombre.Text.Trim().Length < iLargoNombre)
                    {
                        bRegresa = false;
                        General.msjUser(string.Format("Debe escribir como minímo {0} letras del Nombre, verifique.", iLargoNombre));
                        txtNombre.Focus();
                    }
                }
            }

            if (bRegresa && chkImportarBeneficiarios.Checked )
            {
                string sMsj = "La busqueda de Beneficiarios se realizará en el Servidor de Beneficiarios.\n\n" + 
                    "Este proceso puede demorar algunos minutos.\n" + 
                    "¿ Desea continuar ? "; 
                if (General.msjConfirmar(sMsj) == DialogResult.No)
                {
                    bRegresa = false;
                }
            }

            return bRegresa;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Determinar el lugar de busqueda de informacion 
            if (chkImportarBeneficiarios.Checked)
            {
                // Realiza la busqueda en el Servidor Central, este tiene la Base de Datos de Padrones de SP 
                BuscarWeb();
            }
            else
            {
                // Realiza la busqueda de Beneficiarios en la Base de Datos Local 
                BuscarLocal();
            }
        }

        private void BuscarLocal()
        {
            lblProcesando.Visible = false;
            progressBar.Visible = false;

            contexMenuBeneficiarios.Visible = false;
            contexMenuBeneficiarios.Enabled = false; 

            string sApPat = txtPaterno.Text.Replace(" ", "%");
            string sApMat = txtMaterno.Text.Replace(" ", "%");
            string sNombre = txtNombre.Text.Replace(" ", "%");
            string sFolio = txtFolio.Text.Replace(" ", "%");
            string sCURP = txtCURP.Text.Replace(" ", "%");

            intColumnas = 13;  // Posicion de la Columna IdBeneficiario 
            string sSql = 
                " Select " + 
                " 'Folio de Beneficiario' = FolioReferencia, " +
                " 'Referencia' = FolioReferenciaAuxiliar, " +
                "	'Esta Vigente' = (case when EsVigente = 1 Then 'SI' Else 'NO' end), " + 
                "   CURP, " +
                "   'Apellido Paterno' = ApPaterno, 'Apellido Materno' = ApMaterno, 'Nombre (s)' = Nombre, SexoAux as Sexo,  " +
                "	'Fecha de Nacimiento' = convert(varchar(10), FechaNacimiento, 120), Edad,  " +
                "	'Esta Vigente' = (case when EsVigente = 1 Then 'SI' Else 'NO' end), " +
                "	'Fecha Inicia Vigencia' = convert(varchar(10), FechaInicioVigencia, 120), " +
                "	'Fecha Termina Vigencia' = convert(varchar(10), FechaFinVigencia, 120), " +
                "	'Número de Control' = IdBeneficiario  " +
                " From vw_Beneficiarios (NoLock) ";

            sSql = sSql + string.Format(" Where IdEstado = '{0}' and IdFarmacia = '{1}' and IdCliente = '{2}' and IdSubCliente = '{3}' ",
                sEstado, sFarmacia, sIdCliente, sIdSubCliente);
            sSql = sSql + string.Format(" and ApPaterno like '%{0}%' and ApMaterno like '%{1}%' and Nombre like '%{2}%' ",
                sApPat, sApMat, sNombre);
            sSql = sSql + string.Format(" and ( FolioReferencia like '%{0}%' and CURP like '%{1}%' ) ", sFolio, sCURP);

            if (cboSexo.SelectedIndex != 0)
            {
                sSql = sSql + string.Format(" and Sexo = '{0}' ", cboSexo.Data);
            }

            if (chkFechaNacimiento.Checked)
            {
                sSql = sSql + string.Format(" and convert(varchar(10), FechaNacimiento, 120) = '{0}' ", General.FechaYMD(dtpFechaNacimiento.Value));
            } 

            if (validaPametros())
            {
                myListView.Limpiar();
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "btnBuscar_Click");
                    General.msjError("Ocurrió un error al Obtener la lista de Beneficiarios.");
                }
                else
                {
                    if (!leer.Leer())
                    {
                        General.msjUser("No se encontro información coincidente con los criterios especificados.");
                        txtPaterno.Focus();
                    }
                    else
                    {
                        myListView.CargarDatos(leer.DataSetClase, true, true);
                        myListView.AjustarColumnas();
                    }
                }
            } 
        }

        private void BuscarWeb()
        {
            if (validaPametros())
            {
                Thread t = new Thread(this.SolicitarInformacion);
                t.Name = "SolicitarInformacion";
                t.Start(); 
            }
        }

        private void SolicitarInformacion()
        {
            // this.Enabled = false;

            myListView.Limpiar();

            contexMenuBeneficiarios.Visible = false;
            contexMenuBeneficiarios.Enabled = false; 

            lblProcesando.Visible = true;
            progressBar.Visible = true;
            // progressBar.Style = ProgressBarStyle.Marquee;

            string sApPat = txtPaterno.Text.Replace(" ", "%");
            string sApMat = txtMaterno.Text.Replace(" ", "%");
            string sNombre = txtNombre.Text.Replace(" ", "%");
            string sFolio = txtFolio.Text.Replace(" ", "%");
            string sSexo = cboSexo.Data;
            string sFechaNac = General.FechaYMD(dtpFechaNacimiento.Value);

            if (cboSexo.SelectedIndex == 0)
            {
                sSexo = "";
            }

            if (!chkFechaNacimiento.Checked)
            {
                sFechaNac = "";
            }

            string sSql = string.Format(" Exec spp_Web_ObtenerBeneficiarios '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}' ",
                DtGeneral.EstadoConectado, cboVigencia.Data, sFolio, sNombre, sApPat, sApMat, sSexo, sFechaNac, sIdCliente );


            // Revisar que los parametos cumplan con las condiciones minimas necesarios para solicitar la informacion al servidor central 
            // if (validaPametros())
            {
                intColumnas = 10;  // Posicion de la Columna IdBeneficiario 
                if (!leerWeb.Exec(sSql))
                {
                    Error.LogError(leerWeb.Error.Message); 
                    General.msjError("Ocurrió un error al Obtener la lista de Beneficiarios.");
                }
                else
                {
                    if (!leerWeb.Leer())
                    {
                        lblProcesando.Visible = false; 
                        progressBar.Visible = false; 

                        General.msjUser("No se encontro información coincidente con los criterios especificados.");
                        txtPaterno.Focus();
                    }
                    else
                    {
                        lblProcesando.Visible = false;
                        progressBar.Visible = false;

                        //if (leerWeb.Leer())
                        {
                            myListView.CargarDatos(leerWeb.DataSetClase, true, true);
                            myListView.AjustarColumnas();

                            contexMenuBeneficiarios.Visible = true;
                            contexMenuBeneficiarios.Enabled = true; 
                        }
                    }
                }
            }

            lblProcesando.Visible = false;
            progressBar.Visible = false; 

            // this.Enabled = true;
        }

        private void integrerarInformaciónDeBeneficiarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IntegrarBeneficiario(); 
        }

        private void chkImportarBeneficiarios_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            if (txtFolio.Text.Trim() != "")
            {
                if (bFormatearReferencia)
                {
                    txtFolio.Text = Fg.PonCeros(txtFolio.Text, iLargoReferencia);
                }
            }
        }
    } 
}
