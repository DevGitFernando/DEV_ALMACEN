#region USING
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Data.Odbc;
using System.Windows.Forms;
using System.Threading;
using System.ServiceModel;
using System.Net; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;

using DllFarmaciaSoft;
using Dll_IFacturacion.Configuracion;
#endregion USING

#region USING WEB SERVICES
using Dll_IFacturacion.xsasvrCancelCFDService;
using Dll_IFacturacion.xsasvrCFDService;
using Dll_IFacturacion.xsasvrFileReceiverService;
#endregion USING WEB SERVICES 


namespace Dll_IFacturacion.XSA
{
    internal partial class FrmDescargarDocumentos : FrmBaseExt 
    {
        string sUrl = "";
        public bool ExisteDocumento = false; 
        private string sDocumento = "";
        //HttpRequest http;
        WebClient web; 

        public FrmDescargarDocumentos(string Url, string Documento)
        {
            InitializeComponent(); 
            sUrl = Url;
            sDocumento = Documento; 
        }

        private void FrmDescargarDocumentos_Load(object sender, EventArgs e)
        {
            tmDescarga.Interval = 1000; 
            tmDescarga.Enabled = true;
            tmDescarga.Start(); 
        }

        private void tmDescarga_Tick(object sender, EventArgs e)
        {
            tmDescarga.Stop();
            tmDescarga.Enabled = false;

            //// http://xsa5.factura-e.biz/xsamanager/downloadCfdWebView?serie=GRO&folio=13&tipo=PDF&rfc=PFA070614LD5&key=98706e390a7aeba1728ebeeb7ac2c488

            web = new WebClient();
            web.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            web.DownloadFile(sUrl, sDocumento);
            ExisteDocumento = File.Exists(sDocumento);
            this.Hide(); 
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            sDocumento = "X";
        }

        private void webView_FileDownload(object sender, EventArgs e)
        {
        }
    }
}
