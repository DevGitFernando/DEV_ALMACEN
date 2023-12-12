using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Clase que contiene los paramétros especificos para cada Reporte declarados en el archivo de configuración "queryPersonalizado.ini",
/// formateado en JSON. Para acceder invocar Reportes seguido del cual se desea obtener sus propiedades.
/// Ejemplo: Reportes.ReportesDispensacion.@default
/// </summary>
public class clsReportes
{
	public clsReportes()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
    }

    public class ReportesDispensacion
    {
        public string @default { get; set; }
        public string personalizado { get; set; }
        public string exportarExcel { get; set; }
        public string exportarExcelPersonalizado { get; set; }
    }

    public class MedicosDiagnostico
    {
        public string @default { get; set; }
        public string personalizado { get; set; }
    }

    public class DispensancionClaves
    {
        public string @default { get; set; }
        public string personalizado { get; set; }
    }

    public class Reportes
    {
        public ReportesDispensacion ReportesDispensacion { get; set; }
        public MedicosDiagnostico MedicosDiagnostico { get; set; }
        public DispensancionClaves DispensancionClaves { get; set; }
    }
}