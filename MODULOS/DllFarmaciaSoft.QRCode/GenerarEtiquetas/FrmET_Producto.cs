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
    public partial class FrmET_Producto : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerInformacion;
        clsAyudas ayuda;
        
        QR_Reporte Impresion; // = new QR_Reporte(); 
        DataSet dtsPosicion = new DataSet();
        DataSet dtsInformacion = new DataSet();
        DataSet dtsReeimpresion = new DataSet(); 
        string sFolioGenerado = "";

        QR_Reader reader; 

        bool bPosicion = false;
        bool bInformacion = false;

        bool bExisteImpresoraEtiquetas = DtGeneral.Impresoras.ExisteImpresora("etiquetas");
        // bool bExisteLector = false; // DtGeneral.Camaras.ExisteCamara("QReader");


        public FrmET_Producto() 
        {
            QR_General.InicializarSDK();

            InitializeComponent();

            leer = new clsLeer(ref cnn);
            leerInformacion = new clsLeer(ref cnn); 
        } 

        private void FrmET_Producto_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null); 
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles();

            //btnGenerarEtiqueta.Enabled = bExisteImpresoraEtiquetas;  
            btnImprimir.Enabled = bExisteImpresoraEtiquetas;
            btnImprimir.Enabled = false;
            //btnReader.Enabled = bExisteLector;  

            bPosicion = false;
            bInformacion = false; 
            dtsPosicion = new DataSet();
            dtsInformacion = new DataSet();

            txtCodigoEAN.Enabled = true;

            txtCodigoEAN.Focus();
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
            //reader = new QR_Reader(); 
            //// reader.Camara = "Chicony USB 2.0 Camera";
            //reader.Camara = DtGeneral.Camaras.GetCamara("QReader");
            //reader.Show(); 

            //if (reader.DatosLeidos)
            //{
            //    btnGenerarEtiqueta.Enabled = false; 
            //    QR_General.DatosDecodificados = reader.DatosDecodificados;
            //    sFolioGenerado = QR_General.Folio;
            //    LeerInformacion();
            //    ////General.msjUser(reader.DatosDecodificados); 
            //}
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (validaDatos())
            {
                GenerarEtiqueta();
            }
        }
        #endregion Botones

        #region Etiquetas 
        private bool validaDatos()
        {
            bool bRegresa = true;

            if (txtCodigoEAN.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el Código EAN a etiquetar."); 
                txtCodigoEAN.Focus(); 
            }

            return bRegresa; 
        }

        private void GenerarEtiqueta()
        {
            Impresion = new QR_Reporte();
            Impresion.Reporte = "ET_Producto";
            Impresion.EnviarAImpresora = false;
            
            GenerarDetalles(); 

            //QR_General.GenerarFolio();
            //sFolioGenerado = QR_General.Folio; 
            //Impresion.AgregarEtiqueta(QR_General.Encoder.Imagen);

            Impresion.GenerarXml(); 
            //QR_General.GuardarXml(Impresion.Datos); 
            Impresion.Imprimir(true,false);

            //General.msjUser("Etiqueta generada " + sFolioGenerado);
            //btnNuevo_Click(null, null); 
        }

        private void GenerarDetalles()
        {
           
            if (bInformacion)
            {
                leerInformacion.Leer(); 
                leerInformacion.RegistroActual = 1;

                Impresion.AgregarDetalles(leerInformacion.DataSetClase); 
            } 
        }

        private bool DatosPosicion()
        {
            bool bRegresa = false;
            string sSql = "";
            //string sSql = string.Format(
            //    "Select * " +
            //    " From CatPasillos_Estantes_Entrepaños (NoLock)" +
            //    "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' " + 
            //    " and IdPasillo = '{3}' and IdEstante = '{4}' and IdEntrepaño = '{5}' ", 
            //    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, 
            //    txtIdPasillo.Text, txtIdEstante.Text, txtIdEntrepaño.Text);

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
        #endregion Etiquetas 

        #region Obtener informacion 
        private void GetCodigoEAN()
        {
            string sSql = string.Format(
                "Select IdProducto, CodigoEAN, CodigoEAN_Interno, Descripcion as NombreComercial, " +
                " ClaveSSA, DescripcionCortaClave, IdLaboratorio, Laboratorio " +
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

                    txtCodigoEAN.Enabled = false;

                    lblProducto.Text = leerInformacion.Campo("NombreComercial");
                    lblLaboratorio.Text = leerInformacion.Campo("Laboratorio");

                    btnImprimir.Enabled = true;
                }
                else
                { 
                    //No se encontro CodigoEAN
                    General.msjAviso("CodigoEAN no encontrado verfirique.");
                }
            } 
        } 
        #endregion Obtener informacion

        #region Eventos 
        private void txtCodigoEAN_TextChanged(object sender, EventArgs e)
        {
            lblProducto.Text = "";
            lblLaboratorio.Text = "";
            btnImprimir.Enabled = false;
        }

        //GetCodigoEAN(); 

        private void txtCodigoEAN_Validating(object sender, CancelEventArgs e)
        {
            if (txtCodigoEAN.Text.Trim() != "")
            {
                //GetCodigoEAN(); 
            }
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

        #region Leer informacion 
        private void LeerInformacion()
        {
            clsLeer leerDatos = new clsLeer();
            clsLeer datos = new clsLeer();

            QR_General.Folio = sFolioGenerado;
            leerDatos.DataSetClase = QR_General.Leer();
            dtsReeimpresion = leerDatos.DataSetClase; 

            datos.DataTableClase = leerDatos.Tabla("Posicion");
            datos.Leer();

            datos.DataTableClase = leerDatos.Tabla("Informacion");
            datos.Leer();
            txtCodigoEAN.Text = datos.Campo("CodigoEAN");
            lblProducto.Text = datos.Campo("NombreComercial");
            lblLaboratorio.Text = datos.Campo("Laboratorio");

            btnImprimir.Enabled = true; 


            dtsPosicion = new DataSet();
            dtsPosicion.Tables.Add(leerDatos.Tabla("Posicion")); 
            leerInformacion.DataSetClase = new DataSet();
            leerInformacion.DataTableClase = leerDatos.Tabla("Informacion");

            bPosicion = true;
            bInformacion = true;
            txtCodigoEAN.Enabled = false;
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
    }
}
