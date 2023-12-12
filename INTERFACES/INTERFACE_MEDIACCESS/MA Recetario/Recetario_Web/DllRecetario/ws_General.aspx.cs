using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Text;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;

using DllMediaccess.GetInformacion;

public partial class DllRecetario_ws_General : System.Web.UI.Page
{
    public static basGenerales Fg = new basGenerales();

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("~");
    }

    #region Catalogos
    [WebMethod]
    public static string getCatEstadosFarmacias()
    {
        return ClsToolsHtml.SerializerDataSet(DtGeneral.CatEstadosFarmacias);
    }
    #endregion Catalogos

    [WebMethod]
    //public static string Autenticar(string sEstado, string sUnit, string sUser, string sPass)
    public static string Autenticar(string sUser, string sPass)
    {
        string sReturn = "";
        DataSet dtsProvedor = new DataSet("dtsProvedor");
        clsLeer myLeer = new clsLeer();

        dtsProvedor = MediaccessGetInformacion.AccesoProveedor(sUser, sPass);
        myLeer.DataSetClase = dtsProvedor;
        
        if (myLeer.Leer())
        {
            string sValida = myLeer.Campo("validaField");
            string[] sInfoMedico = myLeer.Campo("medicoField").Split('|');
            string[] sInfoClinica = myLeer.Campo("clinicaField").Split('|');
            string sMsgValida = myLeer.Campo("msgValidaField");

            if (sValida == "1")
            {
                //clsLogin.Empresa = DtGeneral.Empresa;
                //clsLogin.Estado = sEstado;
                //clsLogin.Sucursal = sUnit;
                //clsLogin.Usuario = sUser;
                //clsLogin.Password = sPass;
                //clsLogin.InfoMedico = sInfoMedico;
                
                //if (clsLogin.AutenticarUsuarioLogin())
                //{
                //    HttpContext.Current.Request.Cookies["Recetario"]["Autenticado"] = (true).ToString();
                //}
                //else
                //{
                //    sReturn = "Verifique sus datos";
                //}

                //if (sReturn != "")
                //{
                //    //sReturn = clsLogin.ErrorAutenticacion;
                //    sReturn = "Datos de inicio de sesión inválidos";
                //}

                System.Collections.ArrayList aInfoFarmacia = DtGeneral.GetInfoFarmacia(sInfoClinica[0]);


                if (HttpContext.Current.Request.Cookies["Recetario"] == null)
                {
                    HttpCookie MyCookie = new HttpCookie("Recetario");
                    DateTime now = DateTime.Now;

                    //MyCookie.Value = now.ToString();
                    //MyCookie.Expires = now.AddDays(1);
                    MyCookie.Expires = now.AddHours(8);
                    HttpContext.Current.Response.Cookies.Add(MyCookie);
                }
                else
                {
                    HttpCookie currentUserCookie = HttpContext.Current.Request.Cookies["Recetario"];
                    HttpContext.Current.Response.Cookies.Remove("Recetario");
                    currentUserCookie.Expires = DateTime.Now.AddDays(-1);
                    //currentUserCookie.Value = null;
                    HttpContext.Current.Response.SetCookie(currentUserCookie);
                }

                HttpContext.Current.Request.Cookies["Recetario"]["Autenticado"] = (true).ToString();

                HttpContext.Current.Request.Cookies["Recetario"]["IdEstado"] = aInfoFarmacia[0].ToString();
                HttpContext.Current.Request.Cookies["Recetario"]["IdSucursal"] = aInfoFarmacia[1].ToString();
                HttpContext.Current.Request.Cookies["Recetario"]["IdClinica"] = sInfoClinica[0];

                HttpContext.Current.Request.Cookies["Recetario"]["ClaveMedico"] = sUser;
                HttpContext.Current.Request.Cookies["Recetario"]["Cedula"] = sInfoMedico[3];
                HttpContext.Current.Request.Cookies["Recetario"]["NombreMedico"] = sInfoMedico[2];
                HttpContext.Current.Request.Cookies["Recetario"]["Especialidad"] = sInfoMedico[1];
                HttpContext.Current.Request.Cookies["Recetario"]["Domicilio"] = string.Format("{0} , {1}", sInfoMedico[4], sInfoMedico[5]);
                HttpContext.Current.Request.Cookies["Recetario"]["Ciudad"] = sInfoMedico[6];
                HttpContext.Current.Request.Cookies["Recetario"]["Telefono"] = sInfoMedico[7];
            }
            else
            {
                sReturn = sMsgValida;
            }
        }
        else
        {
            sReturn = "El servidor de MediAccess no respondío a dicha solicitud. Inténtenlo nuevamente.";
        }

        return sReturn;
    }

    [WebMethod]
    public static string BuscarAfiliado(string Afiliado, string Nombre, string ApellidoPaterno, string ApellidoMaterno)
    {
        string sReturn = string.Empty;
        DataSet dtsAfiliado = new DataSet("dtsAfiliado");
        clsLeer myLeer = new clsLeer();
        string sClass = "table table-striped table-bordered table-hover";

        dtsAfiliado = MediaccessGetInformacion.BuscarAfiliado(Afiliado, Nombre, ApellidoPaterno, ApellidoMaterno);

        myLeer.DataTableClase = dtsAfiliado.Tables["Resultado"].DefaultView.ToTable(false, "codAfiliadoField", "correlativoField", "nombreAfiliadoField", "codPlanField", "descripcion_PlanField", "codProductoField", "productoField", "codEmpresaField", "nombreEmpresaField", "codPeriodoField", "codvigenciaField", "vigenciaField");

        string[,] sColumnas = { 
                                  { "codAfiliadoField", "No. Afiliado" },
                                  { "correlativoField", "Sequencial" }, 
                                  { "nombreAfiliadoField", "Nombre" }, 
                                  { "codPlanField", "Id" },
                                  { "descripcion_PlanField", "Plan" },
                                  { "codProductoField", "Id" },
                                  { "productoField", "Producto"},
                                  { "codEmpresaField", "Id" },
                                  { "nombreEmpresaField", "Empresa" },
                                  { "codPeriodoField", "Periodo" },
                                  { "codvigenciaField", "Vigente" },
                                  { "vigenciaField", "Mensaje" }
                              };

        sReturn = ClsToolsHtml.DtsToTableHtml(myLeer.DataSetClase, "TableAfiliado", sClass, sColumnas);
        return ValidarResultado(sReturn, "TableAfiliado", sClass);
    }

    [WebMethod]
    public static string getCIE(string sDescripcion)
    {
        string sReturn = string.Empty;
        DataSet dtsCIE = new DataSet("dtsCIE");
        string sClass = "table table-striped table-bordered table-hover";

        dtsCIE = MediaccessGetInformacion.BuscarDiagnostico(sDescripcion);

        string[,] sColumnas = { 
                                  { "ClaveDiagnostico", "Clave" },
                                  { "Descripcion", "Descripción" }
                              };

        sReturn = ClsToolsHtml.DtsToTableHtml(dtsCIE, "TableDiagnostico", sClass, sColumnas);
        return ValidarResultado(sReturn, "TableDiagnostico", sClass);
    }

    [WebMethod]
    public static string getProducto(string CodProducto, int CodPlan, string Busqueda)
    {
        string sReturn = "";
        DataSet dtsProducto = new DataSet("dtsProductos");
        clsLeer myLeer = new clsLeer();
        string sClass = "table table-striped table-bordered table-hover";
        string sIdProductos = string.Empty;

        dtsProducto = MediaccessGetInformacion.BuscaMedicamento(CodProducto, CodPlan, Busqueda, HttpContext.Current.Request.Cookies["Recetario"]["IdClinica"]);

        myLeer.DataSetClase = dtsProducto;

        int iCon = 1;
        int iRowTotal = myLeer.DataTableClase.Rows.Count;

        sIdProductos = "( ";
        while (myLeer.Leer())
        {
            sIdProductos += string.Format("'{0}'", myLeer.Campo("idField"));
            if (iCon < iRowTotal)
            {
                sIdProductos += ", ";
            }
            iCon++;
        }

        sIdProductos += " )";

        string sSql = string.Format("Select " +
		                                "IdProducto, DescripcionSal, DescripcionProducto, Presentacion, ContenidoPaquete, Existencia " +
	                                "From vw_ExistenciaPorProducto (NoLock) " +
	                                //"Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' " +
                                    "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Existencia > 0 " +
                                    "And IdProducto In {3}", DtGeneral.Empresa, HttpContext.Current.Request.Cookies["Recetario"]["IdEstado"], HttpContext.Current.Request.Cookies["Recetario"]["IdSucursal"], sIdProductos);


        sReturn = ClsToolsHtml.DtsToTableHtml(DtGeneral.ExecQuery(sSql, "TableProducto"), "TableProducto", sClass);
        return ValidarResultado(sReturn, "TableProducto", sClass);
    }

    [WebMethod]
    public static string getProcedimientos(string CodProducto, string Busqueda)
    {
        string sReturn = string.Empty;
        DataSet dtsProcedimientos = new DataSet("dtsProcedimientos");
        string sClass = "table table-striped table-bordered table-hover";

        dtsProcedimientos = MediaccessGetInformacion.BusquedaProcedimiento(HttpContext.Current.Request.Cookies["Recetario"]["IdClinica"], CodProducto, Busqueda);

        string[,] sColumnas = { 
                                  { "claveField", "Clave" },
                                  { "descripcionField", "Descripción" },
                                  { "importeField", "Importe"}
                              };

        sReturn = ClsToolsHtml.DtsToTableHtml(dtsProcedimientos, "TableProcedimientos", sClass, sColumnas);
        return ValidarResultado(sReturn, "TableProcedimientos", sClass);
    }

    [WebMethod]
    public static string getLabGab(string CodProducto, string Busqueda, int TipoBusqueda)
    {
        string sReturn = string.Empty;
        DataSet dtsLabGab = new DataSet("dtsLabGab");
        string sClass = "table table-striped table-bordered table-hover";

        dtsLabGab = MediaccessGetInformacion.BusquedaLabGab(HttpContext.Current.Request.Cookies["Recetario"]["IdClinica"], CodProducto, Busqueda, TipoBusqueda);

        string[,] sColumnas = { 
                                  { "claveField", "Clave" },
                                  { "descripcionField", "Descripción" },
                                  { "importeField", "Importe"}
                              };

        sReturn = ClsToolsHtml.DtsToTableHtml(dtsLabGab, "TableLabGab", sClass, sColumnas);
        return ValidarResultado(sReturn, "TableLabGab", sClass);
    }

    private static string ValidarResultado(string sTabla, string sIdTabla, string sClass)
    {
        string sMensaje = string.Empty;
        if (sTabla.Length >= 102400)
        {
            sMensaje = "El resultado de la consulta excede el límite de registros permitidos para mostrar. Intente con parámetros más específicos.";
            sTabla = string.Format("<table id=\"{0}\" class=\"{1}\" cellspacing=\"0\">" +
                                        "<thead>" +
                                            "<tr>" +
                                                "<th>Mensaje</th>" +
                                            "</tr>" +
                                        "</thead>" +
                                        "<tbody>" +
                                            "<tr>" +
                                                "<td>{2}</td>" +
                                            "</tr>" +
                                        "</tbody>" +
                                    "</table>", sIdTabla, sClass, sMensaje);
        }
        return sTabla;
    }

    [WebMethod]
    public static string Autorizacion(int CodEmpresa, string CodAfiliado, int Correlativo, string CodPeriodo, string Comentario, string Diagnostico, string Procedimientos, string Medicamentos, string laboratorio, string Gabinete) 
    {
        string sReturn = string.Empty;
        DataSet dtsAutorizacion = new DataSet("dtsAutorizacion");
        
        string Clinica = HttpContext.Current.Request.Cookies["Recetario"]["IdClinica"];
        string UsrInsert = HttpContext.Current.Request.Cookies["Recetario"]["ClaveMedico"];
        string ip = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];
        int CodtipoCuenta = Convert.ToInt32(Clinica);
        string Folio = "C00000000001"; //Obtener valor desde sql server;

        dtsAutorizacion = MediaccessGetInformacion.Autorizacion(CodEmpresa, CodAfiliado, Correlativo, CodPeriodo, Clinica, UsrInsert, ip, CodtipoCuenta, Comentario, Diagnostico, Procedimientos, Medicamentos, laboratorio, Gabinete, Folio);

        sReturn = ClsToolsHtml.SerializerDataSet(dtsAutorizacion);

        return sReturn;
    }
}