using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Xml;

using Microsoft.VisualBasic; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;


using SC_SolutionsSystem.QRCode.Codec;

using DllFarmaciaSoft; 

namespace DllFarmaciaSoft.QRCode
{
    public static class QR_General
    {
        static clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        static clsLeer leer; 

        private static QRCodeEncoder pEncoder; // = new QRCodeEncoder(); 
        private static QRCodeDecoder pDecoder; 
        private static SC_SolutionsSystem.QRCode.QRCodeViewer pViewer; // = new SC_SolutionsSystem.QRCode.QRCodeViewer();
        private static SC_SolutionsSystem.QRCode.QrCodeSampleApp pApp;

        private static string sDatosLeidos = ""; 
        private static string sIdEmpresa = DtGeneral.EmpresaConectada; 
        private static string sIdEstado = DtGeneral.EstadoConectado; 
        private static string sIdFarmacia = DtGeneral.FarmaciaConectada;

        private static int iIdPasillo = 0;
        private static int iIdEstante = 0;
        private static int iIdEntrepaño = 0; 

        private static string sFolio = "";
        private static string sDatosXml = "";
        private static string sDataCode = "";

        private static basGenerales Fg = new basGenerales();
        private static SC_SolutionsSystem.Errores.clsGrabarError Error;

        private static bool bSDK_Inicializado = false; 

        #region Constructor de Clases 
        static QR_General()
        {
            LoadAssemblies_Neodynamic();


            leer = new clsLeer(ref cnn);

            pEncoder = new QRCodeEncoder();
            pEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            pEncoder.QRCodeScale = 4;
            pEncoder.QRCodeVersion = 7;
            pEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;

            pDecoder = new QRCodeDecoder(); 
            pViewer = new SC_SolutionsSystem.QRCode.QRCodeViewer();

            Error = new clsGrabarError(cnn.DatosConexion, DtGeneral.DatosApp, "QR_General"); 
        }
        #endregion Constructor de Clases

        #region Propiedades Publicas 
        public static string DatosDecodificados
        {
            get { return sDatosLeidos; }
            set 
            { 
                sDatosLeidos = value;
                LeerDatosDecodificados(); 
            }
        }

        public static string IdEmpresa
        {
            get { return sIdEmpresa; }
            set { sIdEmpresa = Fg.PonCeros(value, 3); }
        } 

        public static string IdEstado 
        {
            get { return sIdEstado; }
            set { sIdEstado = Fg.PonCeros(value, 2); }
        }

        public static string IdFarmacia
        {
            get { return sIdFarmacia; }
            set { sIdFarmacia = Fg.PonCeros(value, 4); }
        }

        public static string Folio
        {
            get { return sFolio; }
            set 
            { 
                sFolio = Fg.PonCeros(value, 8);
                sDataCode = sIdEstado + sIdFarmacia + sFolio; 
            }
        } 

        public static int IdPasillo 
        {
            get { return iIdPasillo; } 
            set { iIdPasillo = value; }
        }

        public static int IdEstante 
        {
            get { return iIdEstante; }
            set { iIdEstante = value; }
        }

        public static int IdEntrepaño 
        {
            get { return iIdEntrepaño; }
            set { iIdEntrepaño = value; }
        } 
        #endregion Propiedades Publicas

        #region Funciones y Procedimientos Privados 
        private static void LeerDatosDecodificados()
        {
            sIdEstado = Fg.Mid(sDatosLeidos, 1, 2);
            sIdFarmacia = Fg.Mid(sDatosLeidos, 3, 4);
            sFolio = Fg.Mid(sDatosLeidos, 7);
        }
        #endregion Funciones y Procedimientos Privados 

        #region Funciones y Procedimientos Publicos
        /// <summary>
        /// Descarga las Dlls necesarias para el diseño de etiquetas.
        /// </summary>
        public static void InicializarSDK()       
        {
            ////LoadAssemblies_Neodynamic(); 

            // Forza la inicializacion de la clase general 

        }

        #region Guardar informacion
        public static bool Guardar(string Datos) 
        {
            bool bRegresa = false;

            if (GenerarFolio())
            {
                if (GetXml(Datos))
                {
                    bRegresa = GuardarXml(sDatosXml, 1);
                }
            }

            return bRegresa; 
        }

        public static bool GuardarXml(DataSet Datos)
        {
            bool bRegresa = false;

            if (GetXml(Datos))
            {
                bRegresa = GuardarXml(sDatosXml, 1);
            }

            return bRegresa; 
        }

        public static bool GuardarXml(string DatosXml)
        {
            return GuardarXml(DatosXml, 1); 
        }

        public static bool GenerarFolio()
        {
            bool bRegresa = false;
            string sSql = "";
            DateTime dt = new DateTime();
            string sFecha = General.FechaYMD(dt);

            sSql = string.Format("Exec spp_QR_Etiquetas '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}' ",
                            sIdEstado, sIdFarmacia, "*", iIdPasillo, iIdEstante, iIdEntrepaño, "", 1);
            bRegresa = leer.Exec(sSql); 

            if (bRegresa)
            {
                leer.Leer();
                sFolio = leer.Campo("Folio");
                sDataCode = sIdEstado + sIdFarmacia + sFolio + "__" + sFecha;
                sDataCode = sIdEstado + sIdFarmacia + sFolio;

                Codificar(); 
            }

            return bRegresa;
        }

        public static void GenerarUbicacion()
        {
            sDataCode = DtGeneral.EstadoConectado + "|" + DtGeneral.FarmaciaConectada + "|" + IdPasillo + "|" + iIdEstante + "|" + IdEntrepaño;
            Codificar();
        }

        public static void GenerarData(string sData)
        {
            
            sDataCode = sData;
            Codificar();
        }

        private static bool GuardarXml(string DatosXml, int Opcion) 
        {
            bool bRegresa = false;
            string sSql = "";  


            // sDatosXml = " "; 
            // if (GetXml(DatosXml))
            {
                sSql = string.Format("Exec spp_QR_Etiquetas '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}' ",
                                sIdEstado, sIdFarmacia, "*", iIdPasillo, iIdEstante, iIdEntrepaño, "", 1);

                ////sSql = string.Format("Exec spp_QR_Etiquetas '{0}', '{1}', '{2}', '{3}', {4}, '{5}', '{6}', '{7}' ",
                ////                sIdEstado, sIdFarmacia, sFolio, DatosXml, Opcion);

                sSql = string.Format("Exec spp_QR_Etiquetas '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}' ",
                                sIdEstado, sIdFarmacia, sFolio, iIdPasillo, iIdEstante, iIdEntrepaño, DatosXml, 1);
                bRegresa = leer.Exec(sSql);

                if (bRegresa)
                { 
                    leer.Leer();
                    sFolio = leer.Campo("Folio"); 
                    sDataCode = sIdEstado + sIdFarmacia + sFolio;
                    Codificar(); 
                } 
            }

            return bRegresa;
        }

        private static bool GetXml(DataSet Datos)
        {
            bool bRegresa = false;
            Stream st = null ;
            sDatosXml = " ";


            try
            {
                Datos.Namespace = "SC_SolutionsSystems";
                Datos.DataSetName = "QR_Code";
                sDatosXml = Datos.GetXml();
                Datos.WriteXml(st, XmlWriteMode.WriteSchema); 

                bRegresa = true; 
            }
            catch { } 

            return bRegresa; 
        }

        private static bool GetXml(string Datos)
        {
            bool bRegresa = false;
            DataSet dts = new DataSet(); 
            sDatosXml = " "; 

            if ( leer.Exec("QR_Etiqueta", Datos) )
            {
                dts = leer.DataSetClase; 
                dts.Namespace = "SC_SolutionsSystems";
                dts.DataSetName = "QR_Code";
                sDatosXml = dts.GetXml();
                bRegresa = true; 
            } 

            return bRegresa;
        }
        #endregion Guardar informacion 

        #region Leer informacion  
        public static DataSet Leer()
        {
            return Leer(sFolio); 
        }

        public static DataSet Leer(string Folio)
        {
            DataSet dts = new DataSet();
            StringReader xmlSR; 
            string sXml = ""; 

            string sSql = string.Format(
                "Select * " +
                "From QR_Etiquetas (NoLock) " + 
                "Where IdEstado = '{0}' and IdFarmacia = '{1}' and Folio = '{2}' ", sIdEstado, sIdFarmacia, Fg.PonCeros(Folio, 8));

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Leer()"); 
                General.msjError("Ocurrió un error al obtener la información de la etiqueta."); 
            }
            else
            {
                if (leer.Leer())
                {
                    try
                    {
                        sXml = leer.Campo("Datos");
                        xmlSR = new System.IO.StringReader(sXml);
                        dts.ReadXml(xmlSR);
                    }
                    catch { } 
                }
            } 

            return dts; 
        }
        #endregion Leer informacion 
        #endregion Funciones y Procedimientos Publicos

        #region Codificar - Decodificar
        public static void Codificar() 
        {
            pEncoder.Encode(sDataCode); 
        }

        public static QRCodeEncoder Encoder
        {
            get { return pEncoder; }
            set { pEncoder = value; }
        }

        public static QRCodeDecoder Decoder
        {
            get { return pDecoder; }
            set { pDecoder = value; }
        }
        #endregion Codificar - Decodificar 

        #region Viewer 
        public static void AppTest()
        {
            pApp = new SC_SolutionsSystem.QRCode.QrCodeSampleApp();
            pApp.ShowInTaskbar = true;
            pApp.ShowDialog(); 
        } 

        public static void View_Encode()
        {
            pViewer.ImageView = pEncoder.Imagen;
            pViewer.Show();
        }

        public static void View_Decode()
        {
            pViewer.ImageView = pDecoder.Imagen;
            pViewer.Show();
        }
        #endregion Viewer

        #region Carga dinamica de dlls
        private static void LoadAssemblies_Neodynamic()
        {
            //////byte[] byFile;
            //////int iAssembliesCargados = 0;
            //////string sError = "";
            //////string sFileName = "";


            //////// BINDING FLAGS
            //////System.Reflection.BindingFlags flags = (System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public |
            //////System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly);

            //////// CULTURE INFO
            //////System.Globalization.CultureInfo clinf = new System.Globalization.CultureInfo(System.Globalization.CultureInfo.CurrentCulture.Name);

            //////if (!bSDK_Inicializado)
            //////{
            //////    bSDK_Inicializado = true; 
            //////    try
            //////    {
            //////        sFileName = "Neodynamic.SDK.ThermalLabel.Dll";
            //////        byFile = DllFarmaciaSoft.QRCode.Properties.Resources.Neodynamic_SDK_ThermalLabel;
            //////        Microsoft.VisualBasic.FileIO.FileSystem.WriteAllBytes(sFileName, byFile, false);
            //////        iAssembliesCargados++;
            //////    }
            //////    catch (System.Exception ex)
            //////    {
            //////        sError = ex.Message;
            //////    }

            //////    try
            //////    {
            //////        sFileName = "Neodynamic.Windows.ThermalLabelEditor.Dll";
            //////        byFile = DllFarmaciaSoft.QRCode.Properties.Resources.Neodynamic_Windows_ThermalLabelEditor;
            //////        Microsoft.VisualBasic.FileIO.FileSystem.WriteAllBytes(sFileName, byFile, false);
            //////        iAssembliesCargados++;
            //////    }
            //////    catch (System.Exception ex)
            //////    {
            //////        sError = ex.Message;
            //////    }
            //////}

            ////////bSDK_Inicializado = iAssembliesCargados == 2;
        }
        #endregion Carga dinamica de dlls
    }
}
