using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;

using Microsoft.VisualBasic.FileIO;
using System.Text;
//using System.Web.Script.Serialization;
using System.Data;
using System.IO;
using System.Collections;

/// <summary>
/// Descripción breve de DtGeneral
/// </summary>
public static class DtGeneral
{
    #region Declaración de variables
    static clsDatosConexion datosCnn = new clsDatosConexion();
    static clsLeer leer;
    static clsLeer leerDataSet;
    static clsConexionSQL cnn;

    static string sRutaApp = HttpContext.Current.Server.MapPath("~");

    static string sEmpresa = string.Empty;
    static string sArbol = string.Empty;
    static string sNombreModulo = string.Empty;
    static string sNombreLogin = string.Empty;

    static DataSet dtsCatEstadosFarmacias = new DataSet("CatEstadosFarmacias");
    static DataSet dtsCIE10 = new DataSet("dtsCIE10");

    #endregion Declaración de variables
    
    #region Constructor
    static DtGeneral()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
        Init();
	}

    public static void Init()
    {
        General.ArchivoIni = "SII-Recetario";
        General.DatosConexion = GetConexion(General.ArchivoIni);
        cnn = new clsConexionSQL(General.DatosConexion);
        leer = new clsLeer(ref cnn);

        DatosDeConexion();
    }

    public static clsDatosConexion GetConexion(string ArchivoConexion)
    {
        clsCriptografo crypto = new clsCriptografo();
        basSeguridad fg = new basSeguridad(ArchivoConexion);

        datosCnn.Servidor = fg.Servidor;
        datosCnn.BaseDeDatos = fg.BaseDeDatos;
        datosCnn.Usuario = fg.Usuario;
        datosCnn.Password = fg.Password;
        datosCnn.TipoDBMS = fg.TipoDBMS;
        datosCnn.Puerto = fg.Puerto;
        datosCnn.NormalizarDatos();

        return datosCnn;
    }

    public static void DatosDeConexion()
    {
        try
        {
            StreamReader objReader = new StreamReader(sRutaApp + @"\DatosConexion.ini");
            string sLine = "";
            ArrayList arrText = new ArrayList();

            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null)
                    arrText.Add(sLine);
            }
            objReader.Close();

            //El orden de las variables depende del archivo de texto
            sEmpresa = SplitValor(arrText[0].ToString(), '=', 1);
            sArbol = SplitValor(arrText[1].ToString(), '=', 1);
            sNombreModulo = SplitValor(arrText[2].ToString(), '=', 1);
            sNombreLogin = SplitValor(arrText[3].ToString(), '=', 1);
        }
        catch { }
    }
    #endregion Constructor

    #region Funciones
    static public DataSet ExecQuery(string sSql, string sNombreTabla)
    {
        //Validar conexion activa
        ValidarLeer();

        DataSet dtsReturn = new DataSet();
        if (leer.Exec(sSql))
        {
            leer.RenombrarTabla(1, sNombreTabla);
                dtsReturn = leer.DataSetClase;
        }
        return dtsReturn.Copy();
    }

    static public bool ExecQueryBool(string sSql)
    {
        bool bReturn = false;

        //Validar conexion activa
        ValidarLeer();

        if (leer.Exec(sSql))
        {
            if (leer.Error.Message == "Sin error")
            {
                bReturn = true;
            }
        }
        return bReturn;
    }

    public static void ValidarLeer()
    {
        if (leer == null)
        {
            Init();
        }
    }

    public static DataSet getCatEstadosFarmacias()
    {
        if (leerDataSet == null)
        {
            leerDataSet = new clsLeer();
        }

        leerDataSet.DataSetClase = dtsCatEstadosFarmacias;

        if (!leerDataSet.Leer())
        {
            string sSql = string.Format("Set DateFormat YMD " +
                                    "Select " +
                                        "IdEstado, Estado , IdFarmacia, Farmacia " +
                                    "From vw_Farmacias (NoLock) " +
                                    "Where IdTipoUnidad = '007'");

            //Validar conexion activa
            ValidarLeer();

            if (!leer.Exec("catEstadosFarmacias", sSql))
            {
                // Grabar Error 
            }
            else
            {
                dtsCatEstadosFarmacias = leer.DataSetClase;
            }
        }

        return dtsCatEstadosFarmacias.Copy();
    }

    public static DataSet getEstados()
    {
        if (leerDataSet == null)
        {
            leerDataSet = new clsLeer();
        }

        leerDataSet.DataSetClase = dtsCatEstadosFarmacias;

        if (!leerDataSet.Leer())
        {
            getCatEstadosFarmacias();
        }

        leerDataSet.DataTableClase = dtsCatEstadosFarmacias.Tables["catEstadosFarmacias"].DefaultView.ToTable(true, "IdEstado", "Estado");
        
        return leerDataSet.DataSetClase.Copy();
    }

    public static DataSet GetCatalogoCIE10()
    {
        if (leerDataSet == null)
        {
            leerDataSet = new clsLeer();
        }

        leerDataSet.DataSetClase = dtsCIE10;
        if (!leerDataSet.Leer())
        {
            //Validar conexion activa
            ValidarLeer();

            string sSql = string.Format("Select " +
                                            "ClaveDiagnostico As 'Clave', Descripcion As 'Descripción'" +
                                        "From CatCIE10_Diagnosticos (NoLock) " +
                                        "Where Status = 'A' And ClaveDiagnostico Not In('000', '0000') And Len(ClaveDiagnostico) = 4 " +
                                        "Order By ClaveDiagnostico");

            dtsCIE10 = ExecQuery(sSql, "AyudaCIE10");
        }

        return dtsCIE10.Copy();
    }

    public static ArrayList GetInfoFarmacia(string sIdUMedica)
    {
        ArrayList aInfoFarmacia = new ArrayList();
        clsLeer myLeer = new clsLeer();
        DataSet dtsInfoFarmacia = new DataSet("dtsInfoFarmacia");

        string sSql = string.Format("Select * From INT_MA__CFG_FarmaciasClinicas (NoLock) " +
	                                "Where Referencia_MA = '{0}'", sIdUMedica);

        myLeer.DataSetClase = ExecQuery(sSql, "InfoFarmacia");

        if (myLeer.Leer())
        {
            aInfoFarmacia.Add(myLeer.Campo("IdEstado"));
            aInfoFarmacia.Add(myLeer.Campo("IdFarmacia"));
        }
        else
        {
            aInfoFarmacia.Add("");
            aInfoFarmacia.Add("");
        }


        return aInfoFarmacia;
    }

    public static DataSet ArbolNavegacion(string sEstado, string sSucursal, string sUsuario)
    {
        DataSet dtsPermisos = new DataSet("dtsPermisos");
        string sSql = string.Format(" Exec sp_Permisos '{0}', '{1}', '{2}', '{3}' ", sEstado, sSucursal, sArbol, sUsuario);
        dtsPermisos =  ExecQuery(sSql, "Arbol");
        return dtsPermisos.Copy();
    }
    #endregion Funciones

    #region Funciones Generales
    /// <summary>
    /// Funcion que realiza un split a un cadena de texto en base a un caracter separador, regresa el valor en la posición
    /// especificada.
    /// </summary>
    /// <param name="sValor">Cadena a realizar split</param>
    /// <param name="sSeparador">Caracter separador</param>
    /// <param name="iPosicionValor">Posicion que se desea obtener del split realizado (Esta inicia en 0)</param>
    /// <returns>Cadena con el resultado del split</returns>
    private static string SplitValor(string sValor, char sSeparador, int iPosicionValor)
    {
        string[] sCadena = sValor.Split(sSeparador);
        return sCadena[iPosicionValor];
    }
    #endregion Funciones Generales

    #region Propiedades
    public static clsConexionSQL DatosConexion
    {
        get { return cnn; }
    }
    public static string Empresa
    {
        get { return sEmpresa; }
    }

    public static string Arbol
    {
        get { return sArbol; }
    }

    public static string NombreModulo
    {
        get { return sNombreModulo; }
    }

    public static string NombreLogin
    {
        get { return sNombreLogin; }
    }

    public static DataSet CatEstadosFarmacias
    {
        get { return getCatEstadosFarmacias(); }
    }
    #endregion Propiedades
}