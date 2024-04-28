using System;
using System.Collections; 
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Data;
using System.IO;
using System.Xml;
using System.Windows.Forms; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes; 

using SC_SolutionsSystem.QRCode.Codec;

using DllFarmaciaSoft; 

namespace DllFarmaciaSoft.QRCode
{
    public class QR_Reporte
    {
        #region Declaracion de Variables 

        clsImprimir reporte; 
        string sNombreReporte = "Reporte"; 
        //string sRutaReporte = Application.StartupPath + @"\Etiquetas\";
        string sRutaReporte = DtGeneral.RutaReportes + @"\Etiquetas\";

        string sIdEmpresa = DtGeneral.EmpresaConectada;
        string sEmpresa = DtGeneral.EmpresaConectadaNombre;
        string sEmpresaNombreCorto = DtGeneral.EmpresaDatos.NombreCorto; 
        string sDireccionEmpresa = DtGeneral.EmpresaDomicilio; 
        string sIdEstado = DtGeneral.EstadoConectado;
        string sEstado = DtGeneral.EstadoConectadoNombre; 
        string sIdFarmacia = DtGeneral.FarmaciaConectada;
        string sFarmacia = DtGeneral.FarmaciaConectadaNombre;
        string sIdJurisdiccion = DtGeneral.Jurisdiccion;
        string sJurisdiccion = DtGeneral.JurisdiccionNombre;
        string sMensaje_Default = "Impresión generada satisfactoriamente.";
        string sMensaje = ""; 
        clsInformacionEmpresa datosEmpresa = DtGeneral.EmpresaDatos; 
        DateTime dtFecha = new DateTime(); 


        ArrayList pImagenes = new ArrayList();
        DataSet dtsReporte = new DataSet("Reporte");
        DataSet dtsDetalles = new DataSet("Detalles");
        bool bExistenDatosImpresion = false;
        private int iNumeroDeCopias = 1;
        bool bEnviarAImpresora = true; 

        basGenerales Fg = new basGenerales(); 

        #endregion Declaracion de Variables 

        #region Constructores y Destructor de Clases 
        public QR_Reporte()
        {
            if (!Directory.Exists(sRutaReporte))
            {
                Directory.CreateDirectory(sRutaReporte); 
            }

            sMensaje = sMensaje_Default; 
        } 
        #endregion Constructores y Destructor de Clases

        #region Propiedades Publicas 
        public string Reporte
        {
            get { return sNombreReporte; }
            set { sNombreReporte = value; }
        }

        public DataSet Datos
        {
            get { return dtsReporte.Copy();  }
        }

        public DataSet Detalles
        {
            get { return dtsDetalles.Copy(); }
            set { dtsDetalles = value.Copy(); } 
        }

        public int NumeroDeCopias
        {
            get { return iNumeroDeCopias; }
            set
            {
                iNumeroDeCopias = value > 0 ? value : 1;
            }
        }

        public string Mensaje
        {
            get { return sMensaje; }
            set { sMensaje = value; }
        } 

        public bool EnviarAImpresora
        {
            get { return bEnviarAImpresora; }
            set { bEnviarAImpresora = value; }
        }
        #endregion Propiedades Publicas

        #region Impresion 
        public bool GenerarXml()
        {
            bool bRegresa = false; 
            string sFile = Path.Combine(sRutaReporte, sNombreReporte + ".xml") ;

            dtsReporte = new DataSet("Reporte");
            GenerarEncabezado();
            GenerarDetalles();
            GenerarListaImagenes();

            try
            {
                dtsReporte.WriteXml(sFile, XmlWriteMode.WriteSchema); 
                bRegresa = true; 
            }
            catch { }

            return bRegresa; 
        }

        private bool GenerarXmlImpresion()
        {
            bool bRegresa = false;
            string sFile = Path.Combine(sRutaReporte, sNombreReporte + ".xml");

            // dtsReporte = new DataSet("Reporte");
            // dtsReporte = Datos.Copy(); 

            try
            {
                dtsReporte.WriteXml(sFile, XmlWriteMode.WriteSchema);
                bRegresa = true;
            }
            catch { }

            return bRegresa; 
        }

        public bool Imprimir()
        {
            return(Imprimir( false, false )); 
        }

        public bool Imprimir(bool GenerarXml, bool MostrarMensaje) 
        {
            dtsReporte = new DataSet("Reporte"); 
            GenerarEncabezado();
            GenerarDetalles();
            GenerarListaImagenes();
            bExistenDatosImpresion = validarDatosImpresion();

            if (GenerarXml)
            {
                GenerarXmlImpresion();
            }

            if (bExistenDatosImpresion)
            {
                reporte = new clsImprimir(General.DatosConexion);
                reporte.RutaReporte = sRutaReporte;
                reporte.NombreReporte = sNombreReporte;
                reporte.NumeroDeCopias = iNumeroDeCopias; 
                reporte.OrigenDeDatosDataSet = true;
                reporte.OrigenDeDatosReporte = dtsReporte; 
                reporte.Impresora = DtGeneral.Impresoras.GetImpresora("etiquetas");
                reporte.EnviarAImpresora = bEnviarAImpresora;
                reporte.TituloReporte = "Etiquetas";

                reporte.CargarReporte(true, true);
                bExistenDatosImpresion = !reporte.ErrorAlGenerar; 
            }

            ////if (bEnviarAImpresora)
            {
                if (MostrarMensaje)
                {
                    General.msjUser(sMensaje);
                }
            }

            return bExistenDatosImpresion; 
        }

        //////public bool ReImprimir(DataSet Datos)
        //////{
        //////    GenerarXmlReeimpresion(Datos); 
        //////    bExistenDatosImpresion = validarDatosImpresion(); 

        //////    if (bExistenDatosImpresion)
        //////    {
        //////        reporte = new clsImprimir(General.DatosConexion);
        //////        reporte.RutaReporte = sRutaReporte;
        //////        reporte.NombreReporte = sNombreReporte;
        //////        reporte.OrigenDeDatosDataSet = true;
        //////        reporte.OrigenDeDatosReporte = dtsReporte.Copy();
        //////        reporte.Impresora = DtGeneral.Impresoras.GetImpresora("etiquetas");
        //////        reporte.EnviarAImpresora = true; 
        //////        reporte.CargarReporte(true, true);
        //////    }

        //////    return bExistenDatosImpresion;
        //////}

        private bool validarDatosImpresion()
        {
            bool bRegresa = true;

            bRegresa = dtsReporte.Tables.Count > 0;
            foreach (DataTable dt in dtsReporte.Tables)
            {
                if (dt.Rows.Count == 0)
                {
                    bRegresa = false;
                    break; 
                }
            } 

            return bRegresa; 
        }
        #endregion Impresion 

        #region Funciones y Procedimientos Publicos 
        public void SetMensajeDefault()
        {
            sMensaje = sMensaje_Default; 
        }

        public bool AgregarDetalles(DataSet Datos)
        {
            bool bRegresa = false;

            try
            {
                foreach (DataTable dt in Datos.Tables)
                {
                    AgregarDetalles(dt); 
                }
                bRegresa = true;
            }
            catch { }

            return bRegresa; 
        }

        public bool AgregarDetalles(DataTable Datos)
        {
            bool bRegresa = false;

            try
            {
                dtsDetalles.Tables.Add(Datos.Copy());
                bRegresa = true; 
            }
            catch { }

            return bRegresa; 
        }

        public bool AgregarEtiqueta(Bitmap Imagen)
        {
            bool bRegresa = false;

            try
            { 
                pImagenes.Add(Imagen); 
            } 
            catch { }

            return bRegresa; 
        }
        #endregion Funciones y Procedimientos Publicos 

        #region Funciones y Procedimientos Privados 
        private bool GenerarEncabezado()
        {
            bool bRegresa = false;
            DataTable dt = new DataTable("Encabezado");

            try
            {
                dtFecha = General.FechaSistema;
                object[] obj = { sIdEmpresa, sEmpresa, sEmpresaNombreCorto, 
                                   datosEmpresa.Domicilio, 
                                   datosEmpresa.Colonia, 
                                   datosEmpresa.CodigoPostal,  
                                   datosEmpresa.EdoCiudad, 
                                   sIdEstado, sEstado, sIdFarmacia, sFarmacia, sIdJurisdiccion, sJurisdiccion, dtFecha.Date };

                dt.Columns.Add("IdEmpresa", System.Type.GetType("System.String"));
                dt.Columns.Add("Empresa", System.Type.GetType("System.String"));
                dt.Columns.Add("EmpresaNombreCorto", System.Type.GetType("System.String")); 
                dt.Columns.Add("Domicilio", System.Type.GetType("System.String"));
                dt.Columns.Add("Colonia", System.Type.GetType("System.String"));
                dt.Columns.Add("CodigoPostal", System.Type.GetType("System.String"));
                dt.Columns.Add("Ciudad", System.Type.GetType("System.String"));

                dt.Columns.Add("IdEstado", System.Type.GetType("System.String"));
                dt.Columns.Add("Estado", System.Type.GetType("System.String"));
                dt.Columns.Add("IdFarmacia", System.Type.GetType("System.String"));
                dt.Columns.Add("Farmacia", System.Type.GetType("System.String"));
                dt.Columns.Add("IdJurisdiccion", System.Type.GetType("System.String"));
                dt.Columns.Add("Jurisdiccion", System.Type.GetType("System.String"));
                dt.Columns.Add("Fecha", System.Type.GetType("System.DateTime"));
                dt.Rows.Add(obj);

                bRegresa = true; 
            }
            catch { }

            dtsReporte.Tables.Add(dt.Copy());

            return bRegresa; 
        }

        private bool GenerarDetalles()
        {
            bool bRegresa = false;

            try
            {
                foreach (DataTable dt in dtsDetalles.Tables)
                {
                    dtsReporte.Tables.Add(dt.Copy());
                }
                bRegresa = true;
            } 
            catch { }  
            return bRegresa;
        } 

        private bool GenerarListaImagenes()
        {
            bool bRegresa = false;
            string sCampo = ""; 
            DataTable dt = new DataTable("Etiquetas");

            try
            {
                object[] objImagenes = new object[pImagenes.Count];
                for (int i = 0; i <= pImagenes.Count - 1; i++)
                {
                    sCampo = string.Format("Etiqueta_{0}", Fg.PonCeros(i + 1, 4));
                    dt.Columns.Add(sCampo, System.Type.GetType("System.Byte[]"));
                    objImagenes[i] = ImagenToByte((Bitmap)pImagenes[i]);
                }

                //////DataRow dtR = dt.NewRow();
                //////for (int i = 0; i <= pImagenes.Count - 1; i++)
                //////{
                //////    sCampo = string.Format("Etiqueta_{0}", Fg.PonCeros(i + 1, 4));
                //////    dtR[sCampo] = ImagenToByte((Bitmap)pImagenes[i]); 
                //////}

                dt.Rows.Add(objImagenes);
                //dt.Rows.Add(dtR);
                bRegresa = true; 
            }
            catch (Exception ex) 
            {
                ex.Source = ex.Source;
            } 

            dtsReporte.Tables.Add(dt.Copy());

            return bRegresa; 
        }

        private byte[] ImagenToByte(Bitmap Imagen)
        { 
            byte[] byImagen = null;
            string sFile = Path.Combine(sRutaReporte, "Imagen.bmp") ;
            PictureBox pic = new PictureBox(); 

            try
            {
                pic.Image = Imagen;
                pic.Image.Save(sFile, System.Drawing.Imaging.ImageFormat.Png); 

                // Leer la imagen generada 
                FileStream fs = new FileStream(sFile, FileMode.Open, FileAccess.Read); 
                BinaryReader br = new BinaryReader(fs); 
                byte[] imagen = new byte[(int)fs.Length];

                br.Read(imagen, 0, (int)fs.Length); 
                br.Close(); 
                fs.Close(); 

                try
                {
                    File.Delete(sFile);
                }
                catch { }

                byImagen = imagen;
            }
            catch { } 

            return byImagen;
        }

        private Image ByteImagen( byte[] Imagen )
        {
            Image bitImagen = null;

            try
            {
                MemoryStream ms = new MemoryStream(Imagen);
                bitImagen = Image.FromStream(ms);
            }
            catch { } 

            return bitImagen;
        } 
        #endregion Funciones y Procedimientos Privados
    } 
}
