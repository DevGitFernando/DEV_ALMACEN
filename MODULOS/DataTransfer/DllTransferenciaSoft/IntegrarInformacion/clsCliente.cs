using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Windows.Forms;

using SC_SolutionsSystem.Data;

namespace DllTransferenciaSoft.IntegrarInformacion
{
    /// <summary>
    /// Integrar informacion en Clientes(Farmacias)
    /// </summary>
    public class clsCliente : clsIntegrarInformacion
    {
        public clsCliente(clsDatosConexion DatosConexion):base(DatosConexion, TipoServicio.Cliente)
        {
        }

        public clsCliente(clsDatosConexion DatosConexion, Encoding Encode): base(DatosConexion, TipoServicio.Cliente, Encode)
        {
        }



        //////#region Declaracion de variables
        //////clsConexionSQL cnn;
        //////clsDatosConexion datosCnn;
        //////clsLeer leerCat;
        //////clsLeer leerDet;
        //////clsLeer leerExec;
        //////clsLeer leer;
        //////clsLeer leerDestinos;

        //////ArrayList pFiles;
        //////// FileInfo[] pFiles;

        //////string sRutaIntegracion = @"C:\\";
        //////string sRutaIntegrados = @"C:\\";
        
        //////bool bExistenArchivos = false;
        //////string sCveRenapo = "FF";
        //////string sCveOrigenArchivo = "00FF";
        //////string sArchivoGenerado = "";
        //////string sArchivoCfg = "";

        //////// Variables de funciones generales
        //////basGenerales Fg = new basGenerales();
        //////clsGrabarError Error;
        //////clsDatosApp datosApp = new clsDatosApp(Transferencia.Modulo + ".IntegrarInformacion", Transferencia.Version);
        //////#endregion Declaracion de variables 

        //////public clsCliente(clsDatosConexion DatosConexion)
        //////{
        //////    this.datosCnn = DatosConexion;
        //////    cnn = new clsConexionSQL(this.datosCnn);
        //////    Error = new clsGrabarError(datosApp, "clsCliente");

        //////    // Preparar acceso a datos 
        //////    leerCat = new clsLeer(ref cnn);
        //////    leerDet = new clsLeer(ref cnn);
        //////    leerExec = new clsLeer(ref cnn);
        //////    leer = new clsLeer(ref cnn);
        //////    leerDestinos = new clsLeer(ref cnn);
        //////}

        //////#region Obtener Configuraciones()
        //////private bool GetRutaIntegracion()
        //////{
        //////    bool bRegresa = false;
        //////    string sSql = "Select * From CFGC_ConfigurarIntegracion (NoLock) ";

        //////    if (!leer.Exec(sSql))
        //////    {
        //////        Error.GrabarError(leer, "GetRutaIntegracion()");
        //////        bRegresa = false;
        //////    }
        //////    else
        //////    {
        //////        if (!leer.Leer())
        //////        {
        //////            General.msjError("No se encontro la información de configuración de integración de información, reportarlo al departamento de sistemas.");
        //////        }
        //////        else
        //////        {
        //////            bRegresa = true;
        //////            sRutaIntegracion = leer.Campo("RutaArchivosRecibidos") + @"\\";
        //////            sRutaIntegrados = leer.Campo("RutaArchivosIntegrados") + @"\\";
        //////        }
        //////    }
        //////    return bRegresa;
        //////}
        //////#endregion Obtener Configuraciones()

        //////#region Funciones y Procedimientos Privados 
        //////private bool GetListaArchivos()
        //////{
        //////    bool bRegresa = false;
        //////    string []sFiles = Directory.GetFiles(sRutaIntegracion, "*." + Transferencia.ExtArchivosGenerados);
        //////    pFiles = new ArrayList();

        //////    foreach (string sFile in sFiles)
        //////    {
        //////        FileInfo f = new FileInfo(sFile);
        //////        pFiles.Add(f);
        //////    }

        //////    if (pFiles.Count > 0)
        //////        bRegresa = true;

        //////    return bRegresa;
        //////}
        //////#endregion Funciones y Procedimientos Privados 

        //////#region Integrar informacion 
        //////public bool Integrar()
        //////{
        //////    bool bRegresa = false;
        //////    string sFileInt = "";
        //////    string sOrigen = "", sDestino = "";

        //////    if (GetRutaIntegracion())
        //////    {
        //////        bRegresa = true;
        //////        GetListaArchivos();

        //////        foreach(FileInfo f in pFiles)
        //////        {
        //////            sOrigen = sRutaIntegracion + @"\" + f.Name;
        //////            sDestino = sRutaIntegracion + @"\" + f.Name.Replace(Transferencia.ExtArchivosGenerados, Transferencia.ExtArchivosZip);

        //////            LimpiarDirectorio(sRutaIntegracion, "sql");
        //////            Desempacar(sOrigen, sDestino, sRutaIntegracion);
        //////            if (IntegrarArchivos())
        //////            {
        //////                try
        //////                {
        //////                    sFileInt = sRutaIntegrados + f.Name;
        //////                    if (File.Exists(sFileInt))
        //////                        File.Delete(sFileInt);
        //////                    File.Move(f.FullName, sFileInt);
        //////                }
        //////                catch { }
        //////            }
        //////            LimpiarDirectorio(sRutaIntegracion, "sql");
        //////        }
        //////    }

        //////    return bRegresa;
        //////}

        //////private void LimpiarDirectorio(string Ruta, string Extencion)
        //////{
        //////    foreach (string f in Directory.GetFiles(Ruta, "*." + Extencion))
        //////    {
        //////        File.Delete(f);
        //////    }
        //////}

        //////private bool IntegrarArchivos()
        //////{
        //////    bool bExito = true;
        //////    string []sDatos = Directory.GetFiles(sRutaIntegracion, "*.sql");
        //////    foreach (string sFile in sDatos)
        //////    {
        //////        if (!IntegrarArchivo(sFile))
        //////        {
        //////            bExito = false;
        //////            break;
        //////        }
        //////        else
        //////        {
        //////            File.Delete(sFile);
        //////        }
        //////    }

        //////    return bExito;
        //////}

        //////private bool IntegrarArchivo(string Archivo)
        //////{
        //////    bool bExito = false;
        //////    try
        //////    {
        //////        string sValor = "";
        //////        StreamReader reader = new StreamReader(Archivo);
        //////        sValor = " Set DateFormat YMD " + reader.ReadToEnd() + "";
        //////        reader.Close();

        //////        if (cnn.Abrir())
        //////        {
        //////            cnn.IniciarTransaccion();
                    
        //////            bExito = leer.Exec(sValor);

        //////            if (!bExito)
        //////            {
        //////                cnn.DeshacerTransaccion();
        //////                Error.GrabarError(leer, Archivo, "IntegrarArchivo");
        //////            }
        //////            else
        //////            {
        //////                cnn.CompletarTransaccion();
        //////            }

        //////            cnn.Cerrar();
        //////        }

        //////        reader = null;
        //////        GC.Collect();
        //////    }
        //////    catch { }
        //////    return bExito;
        //////}
        //////#endregion Integrar informacion

        //////private void Desempacar(string ArchivoRutaOrigen, string ArchivoRutaDestino, string RutaDestino)
        //////{
        //////    ZipUtil zip = new ZipUtil();
        //////    bool bRegresa = zip.Descomprimir(RutaDestino, ArchivoRutaOrigen);
        //////}
    }
}
