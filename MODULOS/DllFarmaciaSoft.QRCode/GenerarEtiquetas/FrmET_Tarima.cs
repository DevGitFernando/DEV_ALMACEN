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
    public partial class FrmET_Tarima : FrmBaseExt 
    {
        enum Cols
        {
            Orden = 1, ClaveSSA = 2, DescripcionClave = 3, IdProducto = 4, CodigoEAN = 5, NombreComercial = 6, 
            IdLaboratorio = 7, Laboratorio = 8, SubFarmacia = 9, Lote = 10, Caducidad = 11, Existencia = 12, 
            Etiquetar = 13 
        } 

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsAyudas ayuda; 
        clsLeer leer;
        clsLeer leerInformacion;
        clsLeer leerInformacion_Aux;
        // clsListView lst;
        clsGrid grid;  

        QR_Reporte Impresion; // = new QR_Reporte(); 
        DataSet dtsPosicion = new DataSet();
        DataSet dtsInformacion = new DataSet();
        DataSet dtsReeimpresion = new DataSet(); 
        string sFolioGenerado = "";

        string sEmpresa = DtGeneral.EmpresaConectada; 
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        QR_Reader reader; 

        bool bPosicion = false;
        bool bInformacion = false;

        bool bExisteImpresoraEtiquetas = DtGeneral.Impresoras.ExisteImpresora("etiquetas");
        bool bExisteLector = DtGeneral.Camaras.ExisteCamara("QReader");


        public FrmET_Tarima() 
        {
            QR_General.InicializarSDK();

            InitializeComponent();

            leer = new clsLeer(ref cnn);
            leerInformacion = new clsLeer(ref cnn);
            leerInformacion_Aux = new clsLeer(); 
            // lst = new clsListView(listvwDatos); 

            ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name); 

            grid = new clsGrid(ref grdProductos, this);
            grid.EstiloDeGrid = eModoGrid.ModoRow;
            grid.AjustarAnchoColumnasAutomatico = true; 

            //////string sSql = "Select * From CTL_UMedicas Where IdUMedica = '00137' ";
            //////leer.Exec(sSql);

            //////leer.DataSetClase.WriteXml(@"C:\Test_rpt.xml", XmlWriteMode.WriteSchema);

            //////impresion.Reporte = "ET_Tarima";
            //////impresion.Detalles = leer.DataSetClase; 
            //////impresion.AgregarEtiqueta(QR_General.Encoder.Encode("TEST_01"));
            //////impresion.AgregarEtiqueta(QR_General.Encoder.Encode("TEST_02"));
            //////impresion.AgregarEtiqueta(QR_General.Encoder.Encode("TEST_03"));
            //////impresion.AgregarEtiqueta(QR_General.Encoder.Encode("TEST_04"));
            //////impresion.GenerarXml(); 

            //////reader = new QR_Reader();
            //////reader.Camara = "Chicony USB 2.0 Camera"; 
            //////reader.Show(); 

            //////clsImpresoras impresora = new clsImpresoras(); 
            //////impresora.AbrirIniciar(DtGeneral.Impresoras.GetImpresora("etiquetas"));
            //////impresora.OnLine();
            //////impresora.Continuar();

        } 

        private void FrmET_Tarima_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null); 
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(); 
            lblCopias.BackColor = General.BackColorBarraMenu;
            chkMostrarImpresionEnPantalla.Checked = false;
            chkMostrarImpresionEnPantalla.BackColor = General.BackColorBarraMenu; 
            grid.Limpiar();

            chkMultiplesDatos.Checked = false; 
            nmCopias.Value = 1; 

            btnGenerarEtiqueta.Enabled = bExisteImpresoraEtiquetas;  
            btnImprimir.Enabled = bExisteImpresoraEtiquetas;
            btnImprimir.Enabled = false;
            btnReader.Enabled = bExisteLector;
            btnEjecutar.Enabled = true; 

            CargarPasillos(); 
            cboEstantes.Clear();
            cboEstantes.Add("0", "<< Seleccione >>");
            cboEstantes.SelectedIndex = 0;

            cboEntrepanos.Clear();
            cboEntrepanos.Add("0", "<< Seleccione >>");
            cboEntrepanos.SelectedIndex = 0;



            bPosicion = false;
            bInformacion = false; 
            dtsPosicion = new DataSet();
            dtsInformacion = new DataSet();

            txtIdPasillo.Focus();
            cboPasillos.Focus(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                ObtenerInformacionDeUbicacion(); 
            }
        }

        private void btnGenerarEtiqueta_Click(object sender, EventArgs e)
        {
            ////SendKeys.Send("{TAB}");
            ////SendKeys.Send("{ENTER}"); 
            if (validaDatos()) 
            {
                GenerarEtiqueta(); 
            }
        }

        private void btnReader_Click(object sender, EventArgs e)
        {
            reader = new QR_Reader(); 
            // reader.Camara = "Chicony USB 2.0 Camera";
            reader.Camara = DtGeneral.Camaras.GetCamara("QReader");
            reader.Show(); 

            if (reader.DatosLeidos && reader.Resultado.BarcodeFormat == ZXing.BarcodeFormat.QR_CODE)
            {
                btnGenerarEtiqueta.Enabled = false; 
                QR_General.DatosDecodificados = reader.DatosDecodificados;
                sFolioGenerado = QR_General.Folio;
                LeerInformacion();
                ////General.msjUser(reader.DatosDecodificados); 
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Reimprimir(); 
        }
        #endregion Botones

        #region Etiquetas 
        private bool validaDatos()
        {
            bool bRegresa = true;

            //////if (txtCodigoEAN.Text.Trim() == "")
            //////{
            //////    bRegresa = false;
            //////    General.msjUser("No ha capturado el Código EAN a etiquetar."); 
            //////    txtCodigoEAN.Focus(); 
            //////}

            //////if (bRegresa && txtClaveLote.Text.Trim() == "")
            //////{
            //////    bRegresa = false; 
            //////    General.msjUser("No ha capturado la Clave Lote a etiquetar.");
            //////    txtClaveLote.Focus(); 
            //////}

            //////if (bRegresa && txtPiezas.Text.Trim() == "0")
            //////{
            //////    bRegresa = false;
            //////    General.msjUser("Número de piezas inválido, verifique.");
            //////    txtPiezas.Focus(); 
            //////}

            if (bRegresa && !DatosPosicion())
            {
                bRegresa = false;
            } 

            if (bRegresa && !DatosDetalles()) 
            {
                bRegresa = false;
            } 

            return bRegresa; 
        }

        private string GetNombreReporte()
        {
            string sRegresa = "ET_Tarima";

            if (leerInformacion.Registros > 1)
            {
                chkMultiplesDatos.Checked = true;
                chkMultiplesDatos.Enabled = false; 
            } 

            if (chkMultiplesDatos.Checked)
            {
                sRegresa = "ET_Tarima_Varios";
            }

            return sRegresa; 
        }

        private void Reimprimir()
        {
            Impresion = new QR_Reporte();
            Impresion.NumeroDeCopias = (int)nmCopias.Value; 
            Impresion.Reporte = GetNombreReporte();

            GenerarDetalles(); 

            QR_General.IdPasillo = Convert.ToInt32(cboPasillos.Data);
            QR_General.IdEstante = Convert.ToInt32(cboEstantes.Data);
            QR_General.IdEntrepaño = Convert.ToInt32(cboEntrepanos.Data);

            QR_General.Folio = sFolioGenerado;
            QR_General.Codificar(); 
            Impresion.AgregarEtiqueta(QR_General.Encoder.Imagen);

            //Impresion.GenerarXml();
            // QR_General.GuardarXml(Impresion.Datos); 
            Impresion.EnviarAImpresora = !chkMostrarImpresionEnPantalla.Checked; 
            Impresion.Imprimir(true, false); 
            ////btnImprimir.Enabled = false; 
        } 

        private void GenerarEtiqueta()
        {
            Impresion = new QR_Reporte();
            Impresion.NumeroDeCopias = (int)nmCopias.Value; 
            Impresion.Reporte = GetNombreReporte();

            GenerarDetalles(); 

            QR_General.IdPasillo = Convert.ToInt32(cboPasillos.Data);
            QR_General.IdEstante = Convert.ToInt32(cboEstantes.Data); 
            QR_General.IdEntrepaño = Convert.ToInt32(cboEntrepanos.Data);

            QR_General.GenerarFolio();
            sFolioGenerado = QR_General.Folio; 
            Impresion.AgregarEtiqueta(QR_General.Encoder.Imagen);

            Impresion.GenerarXml(); 
            QR_General.GuardarXml(Impresion.Datos);

            Impresion.EnviarAImpresora = !chkMostrarImpresionEnPantalla.Checked; 
            Impresion.Imprimir();

            General.msjUser("Etiqueta generada " + sFolioGenerado);
            btnNuevo_Click(null, null); 
        }

        private void GenerarDetalles()
        {
            if (bPosicion)
            {
                Impresion.AgregarDetalles(dtsPosicion); 
            }

            if (bInformacion)
            {
                //////leerInformacion.Leer(); 
                //////leerInformacion.RegistroActual = 1;
                //////leerInformacion.GuardarDatos("ClaveLote", txtClaveLote.Text.Trim());
                //////leerInformacion.GuardarDatos("Cantidad", txtPiezas.Text.Replace(",","")); 
                //////leerInformacion.GuardarDatos("Caducidad", General.FechaYMD(dtpFechaCaducidad.Value));

                Impresion.AgregarDetalles(leerInformacion.DataSetClase); 

                //////if (leerInformacion.Registros == 1) 
                //////{
                //////    Impresion.AgregarDetalles(leerInformacion.DataSetClase); 
                //////}
                //////else 
                //////{
                //////    chkMultiplesDatos.Checked = true;
                //////    Impresion.AgregarDetalles(leerInformacion.DataSetClase.Clone()); 
                //////}      
            } 
        }

        private bool DatosPosicion()
        {
            bool bRegresa = false; 
            string sSql = string.Format(
                "Select * " +
                " From CatPasillos_Estantes_Entrepaños (NoLock)" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' " + 
                " and IdPasillo = '{3}' and IdEstante = '{4}' and IdEntrepaño = '{5}' ", 
                sEmpresa, sEstado, sFarmacia, cboPasillos.Data, cboEstantes.Data, cboEntrepanos.Data);

            if (!leer.Exec("Posicion", sSql))
            {
                Error.GrabarError(leer, "DatosPosicion()"); 
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjAviso("No se encontro la posición solicitada."); 
                }
                else 
                {
                    bRegresa = true; 
                    dtsPosicion = leer.DataSetClase; 
                }
            } 

            bPosicion = bRegresa; 
            return bRegresa; 
        }

        private bool DatosDetalles()
        {
            bool bRegresa = false;
            DataTable dtInf = leerInformacion_Aux.DataTableClase.Clone();
            DataTable dtDatos = leerInformacion_Aux.DataTableClase.Copy(); 

            for (int i = 1; i <= grid.Rows; i++)
            {
                if (grid.GetValueBool(i, (int)Cols.Etiquetar))
                {
                    bRegresa = true;
                    dtInf.Rows.Add(dtDatos.Rows[i-1].ItemArray);
                } 
            }

            leerInformacion.DataTableClase = dtInf;
            bInformacion = bRegresa; 
            return bRegresa; 
        }
        #endregion Etiquetas 

        #region Obtener informacion 
        private void GetCodigoEAN()
        {
            string sSql = string.Format(
                "Select IdProducto, CodigoEAN, Descripcion as NombreComercial, " + 
                " ClaveSSA, DescripcionCortaClave, IdLaboratorio, Laboratorio, " + 
                " space(20) as ClaveLote, 0 as Cantidad, convert(varchar(10), getdate(), 120) as Caducidad " + 
                "From vw_Productos_CodigoEAN P (NoLock) " + 
                "Where CodigoEAN = '{0}' ", txtCodigoEAN.Text.Trim());

            bInformacion = false;
            if (!leerInformacion.Exec("Informacion", sSql)) 
            {
                Error.GrabarError(leerInformacion, "GetCodigoEAN()");
            }
            else
            {
                if (leerInformacion.Leer())
                {
                    bInformacion = true; 
                    dtsInformacion = leerInformacion.DataSetClase;

                    lblProducto.Text = leerInformacion.Campo("NombreComercial");
                    lblLaboratorio.Text = leerInformacion.Campo("Laboratorio");
                }
            } 
        } 
        #endregion Obtener informacion

        #region Eventos 
        private void txtCodigoEAN_TextChanged(object sender, EventArgs e)
        {
            ////lblProducto.Text = "";
            ////lblLaboratorio.Text = "";
            ////txtClaveLote.Text = ""; 
            ////txtPiezas.Text = "0"; 
        } 

        private void txtCodigoEAN_Validating(object sender, CancelEventArgs e)
        {
            if (txtCodigoEAN.Text.Trim() != "")
            {
                ////GetCodigoEAN(); 
            }
        }

        private void txtCodigoEAN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.Productos_Por_Ubicacion(sEmpresa, sEstado, sFarmacia, cboPasillos.Data, cboEstantes.Data, cboEntrepanos.Data, "txtCodigoEAN_KeyDown");
                if (leer.Leer())
                {
                    txtCodigoEAN.Text = leer.Campo("CodigoEAN"); 
                }
            }
        }
        #endregion Eventos 

        #region Leer informacion 
        private void txtFolioEtiqueta_Validating(object sender, CancelEventArgs e)
        {
            if (txtFolioEtiqueta.Text.Trim() != "")
            {
                btnGenerarEtiqueta.Enabled = false;
                QR_General.Folio = txtFolioEtiqueta.Text; 
                sFolioGenerado = QR_General.Folio; 
                LeerInformacion(); 
            }  
        }

        private void LeerInformacion()
        {
            clsLeer leerDatos = new clsLeer();
            clsLeer datos = new clsLeer();

            QR_General.Folio = sFolioGenerado; 
            leerDatos.DataSetClase = QR_General.Leer();
            dtsReeimpresion = leerDatos.DataSetClase;

            txtFolioEtiqueta.Text = sFolioGenerado;
            txtFolioEtiqueta.Enabled = false;

            datos.DataTableClase = leerDatos.Tabla("Posicion");
            datos.Leer();
            cboPasillos.Data = datos.Campo("IdPasillo");
            cboEstantes.Data = datos.Campo("IdEstante");
            cboEntrepanos.Data = datos.Campo("IdEntrepaño"); 

            datos.DataTableClase = leerDatos.Tabla("Informacion");
            datos.Leer();
            ////txtCodigoEAN.Text = datos.Campo("CodigoEAN");
            grid.LlenarGrid(datos.DataSetClase);
            grid.BloqueaColumna(true, (int)Cols.Etiquetar); 

            chkMultiplesDatos.Checked = datos.Registros > 1;
            chkMultiplesDatos.Enabled = false; 

            ////////lblProducto.Text = datos.Campo("NombreComercial");
            ////////lblLaboratorio.Text = datos.Campo("Laboratorio");
            ////////txtClaveLote.Text = datos.Campo("ClaveLote");
            ////////txtPiezas.Text = datos.CampoInt("Cantidad").ToString();
            ////////dtpFechaCaducidad.Value = datos.CampoFecha("Caducidad");

            btnEjecutar.Enabled = false; 
            btnImprimir.Enabled = true; 
            dtsPosicion = new DataSet();
            dtsPosicion.Tables.Add(leerDatos.Tabla("Posicion")); 
            leerInformacion.DataSetClase = new DataSet();
            leerInformacion.DataTableClase = leerDatos.Tabla("Informacion");

            bPosicion = true;
            bInformacion = true;

            chkMultiplesDatos.Enabled = false; 
            cboPasillos.Enabled = false;
            cboEstantes.Enabled = false;
            cboEntrepanos.Enabled = false;
            txtCodigoEAN.Enabled = false;
            ////txtClaveLote.Enabled = false;
            ////dtpFechaCaducidad.Enabled = false;
            ////txtPiezas.Enabled = false; 
        }

        private void CargarInformacionDetalle(DataSet Informacion)
        {
        }
        #endregion Leer informacion

        private void button1_Click(object sender, EventArgs e)
        {
            //////////QR_General.Folio = "1";  
            //////////FrmQR_Reader f = new FrmQR_Reader(QR_General.Leer());
            //////////f.ShowDialog(); 

            //////this.LeerInformacion();  

            reader = new QR_Reader();
            reader.Camara = "Chicony USB 2.0 Camera";
            reader.Show();

            if (reader.DatosLeidos)
            {
                QR_General.DatosDecodificados = reader.DatosDecodificados; 
                sFolioGenerado = QR_General.Folio;
                LeerInformacion(); 
                ////General.msjUser(reader.DatosDecodificados); 
            }
        }

        #region Eventos Combos 
        private void cboPasillos_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboEstantes.Clear();
            cboEstantes.Add("0", "<< Seleccione >>");
            cboEstantes.SelectedIndex = 0;
            cboEntrepanos.Clear();
            cboEntrepanos.Add("0", "<< Seleccione >>");
            cboEntrepanos.SelectedIndex = 0;

            if (cboPasillos.SelectedIndex != 0)
            {
                CargarEstantes();
            }
        }

        private void cboEstantes_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboEntrepanos.Clear();
            cboEntrepanos.Add("0", "<< Seleccione >>");
            cboEntrepanos.SelectedIndex = 0;

            if (cboEstantes.SelectedIndex != 0)
            {
                CargaEntrepanos();
            }
        }

        private void cboEntrepanos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion Eventos combos 

        #region Cargar Combos
        private void CargarPasillos()
        {
            cboPasillos.Clear();
            cboPasillos.Add("0", "<< Seleccione >>");

            string sSql = string.Format(" Select IdPasillo, ( Cast(IdPasillo As varchar) + ' -- ' + DescripcionPasillo ) as NombrePasillo " +
                                    " From CatPasillos (Nolock) " +
                                    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' Order By IdPasillo ",
                                    sEmpresa, sEstado, sFarmacia);
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
            cboEstantes.Add("0", "<< Seleccione >>");

            string sSql = string.Format(" Select IdEstante, ( Cast(IdEstante As varchar) + ' -- ' + DescripcionEstante ) as NombreEstante " +
                                    " From CatPasillos_Estantes (Nolock) " +
                                    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And IdPasillo = '{3}' Order By IdEstante ",
                                    sEmpresa, sEstado, sFarmacia, cboPasillos.Data);

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
            cboEntrepanos.Add("0", "<< Seleccione >>");

            string sSql = string.Format(" Select IdEntrepaño, ( Cast(IdEntrepaño As varchar) + ' -- ' + DescripcionEntrepaño ) as NombreEntrepaño " +
                                    " From CatPasillos_Estantes_Entrepaños (Nolock) " +
                                    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}'  " +
                                    " And IdPasillo = '{3}' And IdEstante = '{4}' Order By IdEntrepaño ",
                                    sEmpresa, sEstado, sFarmacia, cboPasillos.Data, cboEstantes.Data);

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
        #endregion CargarCombos 
    
        #region Leer datos de ubicacion 
        private void LeerContenidoUbacion()
        { 
        } 
        #endregion Leer datos de ubicacion  

        #region Funciones y Procedimientos Privados 
        private bool validarDatos()
        {
            bool bRegresa = true;

            if (cboPasillos.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un rack válido, verifique.");
                cboPasillos.Focus(); 
            }

            if (bRegresa & cboEstantes.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un nivel válido, verifique.");
                cboEstantes.Focus();
            }

            if (bRegresa & cboEntrepanos.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un entrepaño válido, verifique.");
                cboEntrepanos.Focus();
            }

            return bRegresa; 
        } 

        private void ObtenerInformacionDeUbicacion()
        {
            string sFiltro = chkMultiplesDatos.Checked ? "   " : string.Format(" and CodigoEAN = '{0}' ", txtCodigoEAN.Text);
            string sSql = ""; 

            ////if ( chkMultiplesDatos.Checked )
            ////{
            ////    sFiltro = "  "; 
            ////}

            sSql = string.Format(
                "Select \n" + 
                "   Row_Number() OVER (order by DescripcionCortaClave, IdProducto, FechaCaducidad) as Orden, \n" + 
                "   ClaveSSA, DescripcionCortaClave, IdProducto, CodigoEAN, DescripcionProducto as NombreComercial, \n" +
                "   IdLaboratorio, Laboratorio, SubFarmacia, ClaveLote, \n" +
                "   convert(varchar(10), FechaCaducidad, 120) as Caducidad, cast (Existencia as int) as Cantidad \n" + 
                "From vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones (NoLock) \n" + 
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' \n" + 
                "      and IdPasillo = '{3}' and IdEstante = '{4}' and IdEntrepaño = '{5}' {6} \n"+
                "      and Existencia > 0 \n" + 
                "Order by DescripcionCortaClave, IdProducto, FechaCaducidad \n",  
                sEmpresa, sEstado, sFarmacia, cboPasillos.Data, cboEstantes.Data, cboEntrepanos.Data, sFiltro);

            grid.Limpiar(); 
            if (!leer.Exec("Informacion", sSql))
            {
                Error.GrabarError(leer, "ObtenerInformacionDeUbicacion()");
                General.msjError("Ocurrió un error al obtener la información de la posición."); 
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjUser("No se encontro información con los criterios especificados, verifique."); 
                }
                else 
                {
                    cboPasillos.Enabled = false;
                    cboEstantes.Enabled = false; 
                    cboEntrepanos.Enabled = false;
                    txtCodigoEAN.Enabled = false; 
                    chkMultiplesDatos.Enabled = false;  

                    grid.LlenarGrid(leer.DataSetClase); 
                    leerInformacion.DataSetClase = leer.DataSetClase.Clone();
                    leerInformacion_Aux.DataSetClase = leer.DataSetClase; 
                }
            }

            grid.BloqueaColumna(false, (int)Cols.Etiquetar); 
        } 
        #endregion Funciones y Procedimientos Privados 

        private void chkMultiplesDatos_CheckedChanged(object sender, EventArgs e)
        {
            txtCodigoEAN.Enabled = !chkMultiplesDatos.Checked; 
            txtCodigoEAN.Focus(); 
        }
    }
}
