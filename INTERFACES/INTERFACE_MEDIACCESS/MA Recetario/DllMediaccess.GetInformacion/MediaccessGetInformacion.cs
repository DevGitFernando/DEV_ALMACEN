using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Data; 
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Data.Odbc;

using Microsoft.VisualBasic;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;


using DllMediaccess.GetInformacion.wsMediaccessInformacion; 


namespace DllMediaccess.GetInformacion
{
    public static class MediaccessGetInformacion
    {
        static string sFile_URL = "";
        static string sURL_Conexion = "http://mediaccess.dyndns.org:8081/SvcSassSiMACprueba/SvcSassSi.asmx";
        static bool bConfiguracionCargada = false;

        static SvcSassSi web = null;

        static MediaccessGetInformacion()
	    {

            if (!bConfiguracionCargada)
            {
                web = new SvcSassSi();
                web.Url = sURL_Conexion; 
            }
	    }

        public static DataSet AccesoProveedor(string CodCuenta, string password)
        {
            DataSet dtsResultado = new DataSet("dtsProvedor");
            DataTable dtDatos = new DataTable("Resultado");
            Registros_AccesoProveedor Proveedor = new Registros_AccesoProveedor();

            try
            {
                Proveedor.ListaProveedor = web.AccesoProveedor(CodCuenta, password);
                dtsResultado = Proveedor.Listado();
            }
            catch (System.Exception ex)
            {
                dtsResultado.Tables.Add(dtDatos.Copy());
            }

            return dtsResultado;
        }

        public static DataSet BuscarAfiliado(string Afiliado, string Nombre, string ApellidoPaterno, string ApellidoMaterno)
        {
            return BuscarAfiliado("", "", Afiliado, "", "", "", ApellidoPaterno, ApellidoMaterno, Nombre, "");
        }

        private static DataSet BuscarAfiliado(string Empresa, string Producto, string Afiliado, string Correlativo, string NumCerti, string Parentesco,
            string ApellidoPaterno, string ApellidoMaterno, string Nombre, string EstatusAfiliado)       
        {
            DataSet dtsResultado = new DataSet("DtsAfiliados");
            DataTable dtDatos = new DataTable("Resultado");
            Registros_Afiliados Afiliados = new Registros_Afiliados(); 

            try
            {
                Afiliados.ListaAfiliados = web.BuscarAfiliado(Empresa, Producto, Afiliado, Correlativo, NumCerti, Parentesco, ApellidoPaterno, ApellidoMaterno, Nombre, EstatusAfiliado);
                dtsResultado = Afiliados.Listado(); 
            }
            catch (System.Exception ex )
            {
                dtsResultado.Tables.Add(dtDatos.Copy()); 
            }

            return dtsResultado; 
        }

        public static DataSet BuscarDiagnostico(string CadenaBusqueda)
        {
            DataSet dtsResultado = new DataSet("DtsDiagnosticos");
            DataTable dtDatos = new DataTable("Resultado");
            Registros_Diagnosticos diagnosticos = new Registros_Diagnosticos();

            try
            {
                diagnosticos.ListaDiagnosticos = web.BusquedaDiagnostico(CadenaBusqueda);
                dtsResultado = diagnosticos.Listado();
            }
            catch (System.Exception ex)
            {
                dtsResultado.Tables.Add(dtDatos.Copy());
            }

            return dtsResultado;
        }

        public static DataSet BuscaMedicamento(string CodProducto, int CodPlan, string Busqueda, string Cve_prov)
        {
            DataSet dtsResultado = new DataSet("DtsMedicamentos");
            DataTable dtDatos = new DataTable("Resultado");
            Registros_Medicamentos medicamentos = new Registros_Medicamentos();

            try
            {
                medicamentos.ListaMedicamentos = web.BuscaMedicamento(CodProducto, CodPlan, Busqueda, Cve_prov);
                dtsResultado = medicamentos.Listado();
            }
            catch (System.Exception ex)
            {
                dtsResultado.Tables.Add(dtDatos.Copy());
            }

            return dtsResultado;
        }

        public static DataSet BusquedaProcedimiento(string CodClinica, string CodProducto, string CadenaBusqueda)
        {
            DataSet dtsResultado = new DataSet("DtsProcedimientos");
            DataTable dtDatos = new DataTable("Resultado");
            Registros_Procedimientos procedimientos = new Registros_Procedimientos();

            try
            {
                procedimientos.ListaProcedimientos = web.BusquedaProcedimiento(CodClinica, CodProducto, CadenaBusqueda);
                dtsResultado = procedimientos.Listado();
            }
            catch (System.Exception ex)
            {
                dtsResultado.Tables.Add(dtDatos.Copy());
            }

            return dtsResultado;
        }

        public static DataSet Autorizacion(int CodEmpresa, string CodAfiliado, int Correlativo, string CodPeriodo, string Clinica, string UsrInsert, string ip, int CodtipoCuenta, string Comentario, string Diagnostico, string Procedimientos, string Medicamentos, string laboratorio, string Gabinete, string Folio)
        {
            DataSet dtsResultado = new DataSet("DtsAfiliados");
            DataTable dtDatos = new DataTable("Resultado");
            Registros_Autorizar Autorizar = new Registros_Autorizar();

            try
            {
                Autorizar.ListaAutorizacion = web.Autorizar(CodEmpresa, CodAfiliado, Correlativo, CodPeriodo, Clinica, UsrInsert, ip, CodtipoCuenta, Comentario, Diagnostico, Procedimientos, Medicamentos, laboratorio, Gabinete, Folio);
                dtsResultado = Autorizar.Listado();
            }
            catch (System.Exception ex)
            {
                dtsResultado.Tables.Add(dtDatos.Copy());
            }

            return dtsResultado;
        }

        public static DataSet BusquedaLabGab(string CodClinica, string CodProducto, string CadenaBusqueda, int TipoBusqueda)
        {
            DataSet dtsResultado = new DataSet("DtsLabGab");
            DataTable dtDatos = new DataTable("Resultado");
            Registros_LaboratoriosYGabinetes LabGab = new Registros_LaboratoriosYGabinetes();

            try
            {
                LabGab.ListaLaboratoriosYGabinetes = web.BusquedaLabGab(CodClinica, CodProducto, CadenaBusqueda, TipoBusqueda);
                dtsResultado = LabGab.Listado();
            }
            catch (System.Exception ex)
            {
                dtsResultado.Tables.Add(dtDatos.Copy());
            }

            return dtsResultado;
        }
    }
}

