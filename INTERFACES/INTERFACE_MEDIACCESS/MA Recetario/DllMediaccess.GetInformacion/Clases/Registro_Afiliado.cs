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
    public class Registros_AccesoProveedor
    {
        public AccesoProveedorCto[] ListaProveedor = null;

        public DataSet Listado()
        {
            DataSet dtsResultado = new DataSet("dtsProvedor");
            DataTable dtDatos = new DataTable("Resultado");

            if (ListaProveedor != null)
            {
                dtsResultado = GetListado();
            }

            return dtsResultado;
        }

        private DataSet GetListado()
        {
            DataSet dtsResultado = new DataSet("dtsProvedor");
            DataTable dtDatos = new DataTable("Resultado");
            //AfiliadoCto[] listaResultados = null;

            dtDatos.Columns.Add("validaField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("clinicaField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("medicoField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("msgValidaField", System.Type.GetType("System.String"));

            try
            {
                if (ListaProveedor != null)
                {
                    foreach (AccesoProveedorCto Proveedor in ListaProveedor)
                    {
                        object[] obj = 
                        {
                            Proveedor.Valida,
                            Proveedor.Clinica,
                            Proveedor.Medico,
                            Proveedor.MsgValida
                        };

                        dtDatos.Rows.Add(obj);
                    }
                }
            }
            catch
            {
                dtDatos.Rows.Clear();
            }

            dtsResultado.Tables.Add(dtDatos.Copy());

            return dtsResultado;
        }
    }


    public class Registros_Afiliados 
    {
        public AfiliadoCto[] ListaAfiliados = null;

        public DataSet Listado()
        {
            DataSet dtsResultado = new DataSet("DtsAfiliados");
            DataTable dtDatos = new DataTable("Resultado");

            if (ListaAfiliados != null)
            {
                dtsResultado = GetListado(); 
            }

            return dtsResultado; 
        }

        private DataSet GetListado()
        {
            DataSet dtsResultado = new DataSet("DtsAfiliados");
            DataTable dtDatos = new DataTable("Resultado");
            //AfiliadoCto[] listaResultados = null;

            dtDatos.Columns.Add("codEmpresaField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("nombreEmpresaField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("codAfiliadoField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("nombreAfiliadoField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("nombresField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("apellidoPaternoField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("apellidoMaternoField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("correlativoField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("codEstadoField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("estadoField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("codProductoField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("productoField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("sexoField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("codParentescoField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("fechaNacimientoField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("codGrupoField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("parentescoField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("vigenciaField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("fechaAntiguedadField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("codEstatusAfiliadoField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("estatusAfiliadoField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("coberturaField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("nombreComercialField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("codPeriodoField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("fechaFinServicioField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("codVipField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("codPlanField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("descripcion_PlanField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("numCertiField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("polizaField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("codPymeColectivoField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("descPymeColectivoField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("codvigenciaField", System.Type.GetType("System.String"));


            try
            {
                if (ListaAfiliados != null)
                {
                    foreach (AfiliadoCto afiliado in ListaAfiliados)
                    {
                        object[] obj = 
                        {
                            afiliado.CodEmpresa, afiliado.NombreEmpresa,         
                            afiliado.CodAfiliado, afiliado.NombreAfiliado,         
                            afiliado.Nombres, afiliado.ApellidoPaterno,         
                            afiliado.ApellidoMaterno, afiliado.Correlativo,         
                            afiliado.CodEstado, afiliado.Estado,         
                            afiliado.CodProducto, afiliado.Producto,         
                            afiliado.Sexo, afiliado.CodParentesco,         
                            afiliado.FechaNacimiento, afiliado.CodGrupo,         
                            afiliado.Parentesco, afiliado.Vigencia,         
                            afiliado.FechaAntiguedad, afiliado.CodEstatusAfiliado,         
                            afiliado.EstatusAfiliado, afiliado.Cobertura,         
                            afiliado.nombreComercial, afiliado.CodPeriodo,         
                            afiliado.FechaFinServicio, afiliado.codVip,         
                            afiliado.codPlan, afiliado.descripcion_Plan,         
                            afiliado.NumCerti, afiliado.Poliza,         
                            afiliado.CodPymeColectivo, afiliado.descPymeColectivo,         
                            afiliado.Codvigencia 
                        };

                        dtDatos.Rows.Add(obj);
                    }
                }
            }
            catch 
            {
                dtDatos.Rows.Clear(); 
            }

            dtsResultado.Tables.Add(dtDatos.Copy()); 

            return dtsResultado; 
        }
    }

    public class Registros_Medicamentos
    {
        public Medicamento[] ListaMedicamentos = null;

        public DataSet Listado()
        {
            DataSet dtsResultado = new DataSet("DtsMedicamentos");
            DataTable dtDatos = new DataTable("Resultado");

            if (ListaMedicamentos != null)
            {
                dtsResultado = GetListado();
            }

            return dtsResultado;
        }

        private DataSet GetListado()
        {
            DataSet dtsResultado = new DataSet("DtsMedicamentos");
            DataTable dtDatos = new DataTable("Resultado");
            //AfiliadoCto[] listaResultados = null;

            dtDatos.Columns.Add("idField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("nombreSalField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("tagField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("procesoField", System.Type.GetType("System.String"));

            try
            {
                if (ListaMedicamentos != null)
                {
                    foreach (Medicamento medicamento in ListaMedicamentos)
                    {
                        object[] obj = 
                        {
                            medicamento.Id,
                            medicamento.NombreSal,
                            medicamento.Tag,
                            medicamento.proceso
                        };

                        dtDatos.Rows.Add(obj);
                    }
                }
            }
            catch
            {
                dtDatos.Rows.Clear();
            }

            dtsResultado.Tables.Add(dtDatos.Copy());

            return dtsResultado;
        }
    }

    public class Registros_Diagnosticos
    {
        public DiagnosticoCto[] ListaDiagnosticos = null;

        public DataSet Listado()
        {
            DataSet dtsResultado = new DataSet("DtsDiagnosticos");
            DataTable dtDatos = new DataTable("Resultado");

            if (ListaDiagnosticos != null)
            {
                dtsResultado = GetListado();
            }

            return dtsResultado;
        }

        private DataSet GetListado()
        {
            DataSet dtsResultado = new DataSet("DtsAfiliados");
            DataTable dtDatos = new DataTable("Resultado");
            //AfiliadoCto[] listaResultados = null;

            dtDatos.Columns.Add("ClaveDiagnostico", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("Descripcion", System.Type.GetType("System.String"));
            try
            {
                if (ListaDiagnosticos != null)
                {
                    foreach (DiagnosticoCto diagnostico in ListaDiagnosticos)
                    {
                        object[] resultDiagnostico = diagnostico.diagnostico.Split('-');
                        object[] obj = 
                        {
                            resultDiagnostico[0].ToString().Trim(),
                            resultDiagnostico[1].ToString().Trim()
                        };

                        dtDatos.Rows.Add(obj);
                    }
                }
            }
            catch
            {
                dtDatos.Rows.Clear();
            }

            dtsResultado.Tables.Add(dtDatos.Copy());

            return dtsResultado;
        }
    }

    public class Registros_Procedimientos
    {
        public ProcedimientoCto[] ListaProcedimientos = null;

        public DataSet Listado()
        {
            DataSet dtsResultado = new DataSet("DtsProcedimientos");
            DataTable dtDatos = new DataTable("Resultado");

            if (ListaProcedimientos != null)
            {
                dtsResultado = GetListado();
            }

            return dtsResultado;
        }

        private DataSet GetListado()
        {
            DataSet dtsResultado = new DataSet("DtsProcedimientos");
            DataTable dtDatos = new DataTable("Resultado");
            //AfiliadoCto[] listaResultados = null;

            dtDatos.Columns.Add("claveField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("descripcionField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("importeField", System.Type.GetType("System.String"));

            try
            {
                if (ListaProcedimientos != null)
                {
                    foreach (ProcedimientoCto procedimiento in ListaProcedimientos)
                    {
                        object[] obj = 
                        {
                            procedimiento.Clave,
                            procedimiento.Descripcion,
                            procedimiento.Importe
                        };

                        dtDatos.Rows.Add(obj);
                    }
                }
            }
            catch
            {
                dtDatos.Rows.Clear();
            }

            dtsResultado.Tables.Add(dtDatos.Copy());

            return dtsResultado;
        }
    }

    public class Registros_LaboratoriosYGabinetes
    {
        public LabGabCto[] ListaLaboratoriosYGabinetes = null;

        public DataSet Listado()
        {
            DataSet dtsResultado = new DataSet("DtsProcedimientos");
            DataTable dtDatos = new DataTable("Resultado");

            if (ListaLaboratoriosYGabinetes != null)
            {
                dtsResultado = GetListado();
            }

            return dtsResultado;
        }

        private DataSet GetListado()
        {
            DataSet dtsResultado = new DataSet("DtsProcedimientos");
            DataTable dtDatos = new DataTable("Resultado");
            //AfiliadoCto[] listaResultados = null;

            dtDatos.Columns.Add("claveField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("descripcionField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("importeField", System.Type.GetType("System.String"));

            try
            {
                if (ListaLaboratoriosYGabinetes != null)
                {
                    foreach (LabGabCto LabGab in ListaLaboratoriosYGabinetes)
                    {
                        object[] obj = 
                        {
                            LabGab.Clave, LabGab.Descripcion, LabGab.Importe
                        };

                        dtDatos.Rows.Add(obj);
                    }
                }
            }
            catch
            {
                dtDatos.Rows.Clear();
            }

            dtsResultado.Tables.Add(dtDatos.Copy());

            return dtsResultado;
        }
    }

    public class Registros_Autorizar
    {
        public AutorizacionCto[] ListaAutorizacion = null;

        public DataSet Listado()
        {
            DataSet dtsResultado = new DataSet("DtsAutorizacion");
            DataTable dtDatos = new DataTable("Resultado");

            if (ListaAutorizacion != null)
            {
                dtsResultado = GetListado();
            }

            return dtsResultado;
        }

        private DataSet GetListado()
        {
            DataSet dtsResultado = new DataSet("DtsAutorizacion");
            DataTable dtDatos = new DataTable("Resultado");
            //AfiliadoCto[] listaResultados = null;

            dtDatos.Columns.Add("validaField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("validaMSGField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("autorizacionField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("medicamentosField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("laboratorioField", System.Type.GetType("System.String"));
            dtDatos.Columns.Add("gabineteField", System.Type.GetType("System.String"));
            
            try
            {
                if (ListaAutorizacion != null)
                {
                    foreach (AutorizacionCto Autorizacion in ListaAutorizacion)
                    {
                        object[] obj = 
                        {
                            Autorizacion.Valida, Autorizacion.ValidaMSG,
                            Autorizacion.Autorizacion, Autorizacion.Medicamentos,
                            Autorizacion.Laboratorio, Autorizacion.Gabinete
                        };

                        dtDatos.Rows.Add(obj);
                    }
                }
            }
            catch
            {
                dtDatos.Rows.Clear();
            }

            dtsResultado.Tables.Add(dtDatos.Copy());

            return dtsResultado;
        }
    }

}
