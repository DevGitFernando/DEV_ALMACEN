using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Diagnostics;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;

using Proveedores_Web;
using System.Web.Services;
using System.Text;

using CrystalDecisions.CrystalReports.Engine;

//public partial class Opciones_frmOrdenesColocadas : System.Web.UI.Page
public partial class Opciones_frmOrdenesColocadas : BasePage
    {
        DataSet dtsOrdenesColocadas = new DataSet("OrdenesColocadas");
        clsLeer leerResultado = new clsLeer();
        clsLeer leer = new clsLeer();
        string sSql = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["__EVENTTARGET"] == "Ejecutar")
            {
                //Metodo_Click(this, new EventArgs());
                if (Session["sSQLWeb"] != null && dtpFechaInicial.Text == "")
                {
                    Ejecutar(Session["sSQLWeb"].ToString());
                }
                else
                {
                    Ejecutar("");
                }
            }
            if (Request.Form["__EVENTTARGET"] == "MarcarOrdenLeida")
            {
                //Metodo_Click(this, new EventArgs());
                MarcarOrdenLeida(Request.Form["__IdFolio"], Request.Form["__IdFarmaciaRecibe"]);
            }

            if (Request.Form["__EVENTTARGET"] == "GenerarReporte")
            {
                GenerarReporte(Request.Form["__IdFolio"], Request.Form["__IdFarmacia"]);
            }
            if (!IsPostBack)
            {
                //Cargar Estados
                cboEstados.Add();
                cboEstados.Add(DtGeneral.EstadosPorOrdenes, true, "IdEstado", "Estado");
                Session["sSQLWeb"] = "";
            }
            else 
            {
                //Checar valores del post del formulario
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Form Elements:<br /><br />");
                foreach (string key in HttpContext.Current.Request.Form.AllKeys)
                {
                    sb.AppendFormat("{0}={1}<br />", key, HttpContext.Current.Request.Form[key]);
                }
            }
        }
        private void Ejecutar(string sQuery)
        {
            string sEstado = cboEstados.Text.Substring(0,2);
            sSql = string.Empty;

            if (sQuery == "")
            {
                sSql = string.Format("Set DateFormat YMD " +
                                    "Select " +
                                        "Folio, NomEstadoEntrega, EntregarEn, FarmaciaEntregarEn, Domicilio, CONVERT(VARCHAR(10), FechaRequeridaEntrega, 120) as FechaRequeridaEntrega, " +
                                        "CONVERT(VARCHAR(10),FechaColocacion, 120) as FechaColocacion, Observaciones, IdFarmacia " +
                                    "From vw_OrdenesCompras_Claves_Enc O (NoLock) " +
                                    "Where " +
                                        "IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And IdProveedor = '{3}' " +
                                        "And Status = '{4}' And FechaColocacion Between '{5}' And '{6}' " + 
                                        "And Not Exists " + 
			                                "(" +  
				                                "Select *" + 
				                                "from COM_OCEN_Proveedores_Ordenes_Leidas L (NoLock) " +  
				                                "Where " + 
					                                "O.IdProveedor = L.IdProveedor And O.IdEmpresa = L.IdEmpresa And O.IdEstado = L.IdEstado " + 
					                                "And O.EntregarEn = L.IdFarmacia And O.Folio = L.FolioOrden " + 
			                                ")", DtGeneral.Empresa, sEstado, DtGeneral.Sucursal, DtGeneral.IdProveedor, "OC", dtpFechaInicial.Text, dtpFechaFinal.Text);

                //Variable de Sesion Fecha para generar excel
                Session["FechaInicial"] = dtpFechaInicial.Text;
                Session["FechaFinal"] = dtpFechaFinal.Text;
                Session["sSQLWeb"] = sSql;
            }
            else
            {
                sSql = sQuery;
            }
            //General.ArchivoIni = "SII-Provedores";
            //General.ArchivoIni = "SII-Provedores-Web";
            //General.DatosConexion = DtGeneral.GetConexion(General.ArchivoIni);
            //clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            clsConexionSQL cnn = DtGeneral.DatosConexion;
            leer = new clsLeer(ref cnn);

            leer.Leer();
            if (!leer.Exec(sSql))
            {
                //Error
            }
            else
            {
                lstOrdenes.Dispose();
                leer.RenombrarTabla(1, "Detalle");

                dtsOrdenesColocadas = leer.DataSetClase;

                //Variables de Sesión
                Session["OrdenesColocadas"] = dtsOrdenesColocadas;

                lstOrdenes.DataSource = leer.Tabla("Detalle").Copy();
                lstOrdenes.DataMember = "Detalle";

                LayoutTemplate template = new LayoutTemplate();
                template.Columunas(AjustarColumnas(leer.ColumnasNombre));
                lstOrdenes.LayoutTemplate = template;

                ItemTemplate itemTemplate = new ItemTemplate();
                lstOrdenes.ItemTemplate = itemTemplate;

                lstOrdenes.DataBind();

                string sFecha = string.Empty;
                sFecha = (string)Session["FechaInicial"];
                sFecha = (string)Session["FechaFinal"];

                dtpFechaInicial.Text = Session["FechaInicial"].ToString();
                dtpFechaFinal.Text = Session["FechaFinal"].ToString();

            }
        }

        private void MarcarOrdenLeida(string sFolio, string sIdFarmaciaRecibe)
        {
            string sEstado = cboEstados.Text.Substring(0, 2);

            sSql = string.Format("Set DateFormat YMD " +
                                  "Exec spp_Mtto_COM_OCEN_Proveedores_Ordenes_Leidas '{0}', '{1}','{2}','{3}','{4}' ", DtGeneral.IdProveedor, DtGeneral.Empresa, sEstado, sIdFarmaciaRecibe, sFolio);
            clsConexionSQL cnn = DtGeneral.DatosConexion;
            leer = new clsLeer(ref cnn);

            leer.Leer();
            if (!leer.Exec(sSql))
            {
                //Error
            }
            else
            {
                Ejecutar(Session["sSQLWeb"].ToString());
            }
        
        }

        private void Download_Reporte(byte[] rReporte, string sNombreReporte, bool AgregarMarcaDeTiempo)
        {
            string sNombreDocumentoDescarga = string.Empty;

            if (sNombreReporte == "")
            {
                sNombreDocumentoDescarga = this.Title;
            }
            else
            {
                sNombreDocumentoDescarga = sNombreReporte;
                sNombreDocumentoDescarga = sNombreDocumentoDescarga.Replace(" ", "_");
                sNombreDocumentoDescarga = HttpUtility.UrlEncode(sNombreDocumentoDescarga, System.Text.Encoding.UTF8);
            }

            if (AgregarMarcaDeTiempo)
            {
                sNombreDocumentoDescarga = sNombreDocumentoDescarga + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss");
            }

            sNombreDocumentoDescarga = sNombreDocumentoDescarga.Replace(" ", "_");
            sNombreDocumentoDescarga = HttpUtility.UrlEncode(sNombreDocumentoDescarga, System.Text.Encoding.UTF8);

            Response.BinaryWrite(rReporte);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;  filename=" + sNombreDocumentoDescarga + ".pdf");
            Response.Flush();
            Response.End();
        }

        private string[] AjustarColumnas(string[] Cols)
        {
            for (int i = 0; i < Cols.Length; i++)
            {
                if (Cols[i].ToString() == "NomEstadoEntrega")
                {
                    Cols[i] = "Estado de entrega";
                }
                if (Cols[i].ToString() == "EntregarEn")
                {
                    Cols[i] = "Núm. Unidad";
                }
                if (Cols[i].ToString() == "FarmaciaEntregarEn")
                {
                    Cols[i] = "Entregar en";
                }
                if (Cols[i].ToString() == "FechaRequeridaEntrega")
                {
                    Cols[i] = "Fecha requerida de entrega";
                }
                if (Cols[i].ToString() == "FechaColocacion")
                {
                    Cols[i] = "Fecha de asignación";
                }
            }
            return Cols;
        }

        private void GenerarReporte(string sFolio, string sIdFarmacia)
        {
            string sEstado = cboEstados.Text.Substring(0, 2);
            string sNombreReporte = string.Empty;

            ReportDocument reporte = new ReportDocument();

            clsImprimir myRpt = new clsImprimir(General.DatosConexion);

            //myRpt.RutaReporte = DtGeneral.RutaReportesCR;
            //myRpt.RutaReporte = DtGeneral.RutaAplicacion + @"/REPORTES/";
            myRpt.RutaReporte = DtGeneral.RutaReportes;

            myRpt.Add("IdEmpresa", DtGeneral.Empresa);
            myRpt.Add("IdEstado", sEstado);
            myRpt.Add("IdFarmacia", sIdFarmacia);
            myRpt.Add("Folio", sFolio);
            myRpt.NombreReporte = "COM_OrdenDeCompra_CodigosEAN";

            myRpt.CargarReporte(false, false);

            reporte = myRpt.ReporteWeb;
            MemoryStream mStream = (MemoryStream)reporte.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

            sNombreReporte = string.Format("OC_{0}_{1}", DtGeneral.NombreCortoEmpresa, sFolio);

            Download_Reporte(mStream.ToArray(), sNombreReporte, true);
        }
    }