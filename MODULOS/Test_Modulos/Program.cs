using System;
using System.Collections.Generic;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.SistemaOperativo;
using SC_SolutionsSystem.FuncionesGenerales;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Facturacion;
using DllFarmaciaSoft.QRCode;
using DllFarmaciaSoft.QRCode.GenerarEtiquetas;

using SC_SolutionsSystem.QRCode; 


using Almacen.Pedidos;
using Almacen.TraspasosEstatales; 

using OficinaCentral; 
using OficinaCentral.Catalogos; 
using OficinaCentral.Catalogos.Productos; 

namespace Test_Modulos
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
             
            General.DatosConexion.Servidor = @"JOB";
            //General.DatosConexion.BaseDeDatos = "SII_20_0004___CEDIS_OAXACA"; // "SII_OficinaCentral_Test";             
            //General.DatosConexion.BaseDeDatos = "SII_21_1182___CEDIS_Puebla"; // "SII_OficinaCentral_Test";        
            General.DatosConexion.BaseDeDatos = "SII_09_0002_Cedis_Clio___2K152021_0200";
            General.DatosConexion.BaseDeDatos = "SII_09_0011____H_Obregon"; 
            General.DatosConexion.Usuario = "sa";
            General.DatosConexion.Password = "1234";
            General.DatosConexion.ConexionDeConfianza = false;
            ////General.Url = "http://LapJesus/wsAlmacen/wsFarmacia.asmx";
            ////////////////////////////////General.ImpresionViaWeb = false; 
            ////////////////////////General.DatosConexion.BaseDeDatos = "SII_MEDICA"; // "SII_OficinaCentral_Test";


            ////////////////////DtGeneral.EmpresaConectada = "001";
            ////////////////////DtGeneral.EstadoConectado = "21";
            ////////////////////DtGeneral.FarmaciaConectada = "1182";
            ////////////////////DtGeneral.ClaveRENAPO = "PL";
            ////////////////////DtGeneral.IdPersonal = "0001";
            ////////////////////DtGeneral.RutaReportes = @"D:\SII_PUNTO_DE_ALMACEN\Reportes";
            ////////////////////DtGeneral.EsAlmacen = true; 

            DtGeneral.EmpresaConectada = "001";
            DtGeneral.EstadoConectado = "21";
            DtGeneral.Jurisdiccion = "6";
            DtGeneral.FarmaciaConectada = "1182";
            DtGeneral.ClaveRENAPO = "pl";
            DtGeneral.EsAdministrador = true;

            DtGeneral.RutaReportes = @"C:\Users\JesusDiaz\Desktop\Reporteador___Perfiles_De_Atencion";

            // Application.Run(new FrmIntegrarInformacion());


            string sCURP = "dimj830516hslzrs00"; 
            bool bCurp = DtGeneral.ValidarEstructura_CURP(sCURP); 

            //SC_SolutionsSystem.QRCode.Cam_Reader f = new SC_SolutionsSystem.QRCode.Cam_Reader();
            //f.Show(); 


            //////DtImpresoras.GetListaImpresoras();
            //////General.msjUser(DtImpresoras.GetImpresora("etiquetas")); 


            //////FrmProductosRegistrosSanitarios f = new FrmProductosRegistrosSanitarios();
            //////f.ShowInTaskbar = true;
            //////Application.Run(f);


            ////CodificacionSNK.Decodificar("00209000201007503001007663001|01LG150600|1612151027120510010001ABCDEFG");
            ////FrmCodificacionSNK f = new FrmCodificacionSNK();
            ////f.ShowDialog();

            //////FrmDecodificacionSNK fd = new FrmDecodificacionSNK();
            //////fd.ShowDialog(); 

            ////clsDataMatrix dm = new clsDataMatrix(); 
            ///////dm.CodificarImagen("00209000201|007503001007663|01LG150600|1612|15102712051001|0001ABCDEFG", @"D:\Img_DM.jpeg");

            ////dm.CodificarImagen("00209001101007503006916038001|01142127|1610151027120510010001ABCDEF1", @"D:\Img_DM_01.jpeg");
            ////dm.CodificarImagen("00209001101007503006916038001|01142127|1610151027120510010001ABCDEF2", @"D:\Img_DM_02.jpeg");
            ////dm.CodificarImagen("00209001101007503006916038001|01142127|1610151027120510010001ABCDEF3", @"D:\Img_DM_03.jpeg");
            ////dm.CodificarImagen("00209000201007503001007663001*01LG150600*1612151027120510010001ABCDEFG", @"D:\Img_DM_Dayana.jpeg"); 
            

            ////General.msjAviso("Proceso finalizado");

            //FrmMensajeCaptura f = new FrmMensajeCaptura(1, 50, 1, "");
            //f.ShowIcon = true; 
            //f.ShowDialog(); 



            //clsEtiquetasPedidosAlmacen etiquetas = new clsEtiquetasPedidosAlmacen();
            //etiquetas.GenerarEtiquetaSurtido("", true); 



            //clsCriptografo crypto = new clsCriptografo();
            //string sCadena = string.Format("{0}{1}{2}{3}", "13", "0001", "PHOENIXHGO", "PH03N1XHG0"); 
            //string sPassword = crypto.PasswordEncriptar(sCadena);

            //sPassword = crypto.PasswordDesencriptar("¸‹‡§¨Œ¥’µw¹…¢vq‰v­±”„ ”–•Š”rtqqqr‘‰†Š™‰ˆ‘‰tr™‰ˆqrªr’»–µpw”‡›‘‘p¦¯¸‘¯"); 
        }
    }
}
