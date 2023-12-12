using System;
using System.Collections.Generic;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Idiomas;
using SC_SolutionsSystem.SistemaOperativo; 

using DllFarmaciaSoft;
using DllFarmaciaSoft.Facturacion;
using DllFarmaciaSoft.QRCode; 
using DllFarmaciaSoft.QRCode.GenerarEtiquetas;

using SC_SolutionsSystem.QRCode.Codec;

using DllTransferenciaSoft.IntegrarInformacion;

//using Almacen.Inventario; 
using Almacen.Pedidos; 
using Almacen.TraspasosEstatales;
//using DllFarmaciaSoft.QRCode;

namespace Almacen
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ////clsRedimensionarForms.Redimensionar = false; 
            ////clsRedimensionarForms.SetRatio(98, 85);  

            ////General.DatosConexion.Servidor = "GTO";
            ////General.DatosConexion.BaseDeDatos = "SII_20_0004___CEDIS_OAXACA"; // "SII_OficinaCentral_Test";             
            ////General.DatosConexion.BaseDeDatos = "SII_11_0003__CEDIS_GTO"; // "SII_OficinaCentral_Test";             
            ////General.DatosConexion.Usuario = "sa";
            ////General.DatosConexion.Password = "1234";
            ////General.DatosConexion.ConexionDeConfianza = false;
            ////General.Url = "http://LapJesus:8181/wsAlmacen/wsFarmacia.asmx";
            ////////////////////////////////////General.ImpresionViaWeb = false; 
            ////////////////////////////General.DatosConexion.BaseDeDatos = "SII_MEDICA"; // "SII_OficinaCentral_Test";


            ////DtGeneral.EmpresaConectada = "001";
            ////DtGeneral.EstadoConectado = "21";
            ////DtGeneral.FarmaciaConectada = "1182";
            ////DtGeneral.ClaveRENAPO = "PL";
            ////DtGeneral.IdPersonal = "0001";
            ////DtGeneral.RutaReportes = @"D:\SII_PUNTO_DE_ALMACEN\Reportes";
            ////DtGeneral.EsAlmacen = true;

            ////DtGeneral.EmpresaConectada = "001";
            ////DtGeneral.EstadoConectado = "21";
            ////DtGeneral.FarmaciaConectada = "182";
            ////DtGeneral.ClaveRENAPO = "pl";
            ////DtGeneral.EsAdministrador = true; 


            ////QRCodeEncoder pEncoder = new QRCodeEncoder();
            ////pEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            ////pEncoder.QRCodeScale = 3;
            ////pEncoder.QRCodeVersion = 12;
            ////pEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;

            ////string sDataCode = @"https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx?id=DDEA4CDB-ACFF-450D-A6B4-814BFA3F7734&re=TOP1203236G5&rr=LOED840623LY3&tt=1705.01&fe=ahc5Xw==";
            ////string sRuta = @"C:\CBB.png"; 

            ////pEncoder.Encode(sDataCode).Save(sRuta, System.Drawing.Imaging.ImageFormat.Png);

            try
            {
                //////////// Punto de Entrada        
                if (ConfiguracionRegional.Revisar())
                {
                    Application.Run(new FrmMain());
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
            


            //////FrmIntegrarPaquetesDeDatos f = new FrmIntegrarPaquetesDeDatos();
            //////f.ShowInTaskbar = true;
            //////f.ShowDialog(); 

            // clsCamaras camaras = new clsCamaras();


            //FrmCEDIS_SurtidoPedidos f = new FrmCEDIS_SurtidoPedidos();
            //f.ShowInTaskbar = true;
            //f.CargarPedido("2", "20", "103", "1"); 

            ////FrmIntegrarInventario_Externo f = new FrmIntegrarInventario_Externo();
            ////f.ShowInTaskbar = true;
            ////f.ShowDialog(); 

           

        }
    }
}
