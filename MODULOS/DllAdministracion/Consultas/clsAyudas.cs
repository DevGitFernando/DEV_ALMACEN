using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

namespace DllAdministracion
{
    public class clsAyudas
    {
        #region Declaración de variables
        // private wsFarmaciaSoftGn.wsConexion cnnWebServ; // = new wsConexion.wsConexionDB();
        private clsErrorManager error = new clsErrorManager();
        private clsLogError errorLog = new clsLogError();
        private DialogResult myResult = new DialogResult();

        private DataSet dtsError = new DataSet();
        private DataSet dtsClase = new DataSet();
        // private object objEnviar = null;
        // private object objRecibir = null;
        private string strCnnString = "";
        private bool bUsarCnnRedLocal = true, bExistenDatos = false, bEjecuto = false; //bError = false 
        private string sSql = "", strResultado = "", sOrderBy = ""; 
        private string strMsjNoDatos = "No existe información para mostrar.";
        string sInicio = "Set DateFormat YMD ";

        private clsConsultas query;
        private basGenerales Fg = new basGenerales();
        private Cls_Acceso_a_Datos_Sql Datos = new Cls_Acceso_a_Datos_Sql();
        private FrmAyuda Frm_Ayuda;

        private clsDatosConexion DatosConexion;
        private clsConexionSQL ConexionSql;        
        private string Name = "DllFarmaciaSoft.clsAyudas";
        string sNameDll = "";
        string sPantalla = "";
        string sVersion = "";
        bool bMostrarMsjLeerVacio = false;
        bool bEsPublicoGeneral = false;
        int iColumnaInicial = 1; 
        #endregion

        #region Constructores de clase y destructor
        public clsAyudas()
        {
            bUsarCnnRedLocal = General.ServidorEnRedLocal;
            strCnnString = Name;
            strCnnString = General.CadenaDeConexion;
            //Name = Name;
            // cnnWebServ = new wsConexion.wsConexionDB();
            // cnnWebServ.Url = General.Url;
        }

        public clsAyudas(string prtCnnString, string cnnWebUrl, bool bUsarRedLocal)
        {
            bUsarCnnRedLocal = bUsarRedLocal;
            strCnnString = prtCnnString;
            // cnnWebServ.Url = cnnWebUrl;
        }

        public clsAyudas(clsDatosConexion Conexion, clsDatosApp DatosApp, string Pantalla)
        {
            this.sNameDll = DatosApp.Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = DatosApp.Version;

            DatosConexion = Conexion;
            ConexionSql = new clsConexionSQL(Conexion);
            ConexionSql.SetConnectionString();
            query = new clsConsultas(DatosConexion, DatosApp.Modulo, Pantalla, DatosApp.Version);
        }

        public clsAyudas(clsDatosConexion Conexion, clsDatosApp DatosApp, string Pantalla, bool MostrarMsjLeerVacio)
        {
            this.sNameDll = DatosApp.Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = DatosApp.Version;
            this.bMostrarMsjLeerVacio = MostrarMsjLeerVacio;

            DatosConexion = Conexion;
            ConexionSql = new clsConexionSQL(Conexion);
            ConexionSql.SetConnectionString();
            query = new clsConsultas(DatosConexion, DatosApp.Modulo, Pantalla, DatosApp.Version);
        }

        public clsAyudas(clsDatosConexion Conexion, string Modulo, string Pantalla, string Version)
        {
            this.sNameDll = Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = Version;
            this.bMostrarMsjLeerVacio = false;

            DatosConexion = Conexion;
            ConexionSql = new clsConexionSQL(Conexion);
            ConexionSql.SetConnectionString();
            query = new clsConsultas(DatosConexion, Modulo, Pantalla, Version);
        }

        public clsAyudas(clsDatosConexion Conexion, string Modulo, string Pantalla, string Version, bool MostrarMsjLeerVacio)
        {
            this.sNameDll = Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = Version;
            this.bMostrarMsjLeerVacio = MostrarMsjLeerVacio;

            DatosConexion = Conexion;
            ConexionSql = new clsConexionSQL(Conexion);
            ConexionSql.SetConnectionString();
            query = new clsConsultas(DatosConexion, Modulo, Pantalla, Version, MostrarMsjLeerVacio);
        }

        #endregion

        #region Funciones y procedimientos publicos
        public bool ExistenDatos
        {
            get { return bExistenDatos; }
        }

        public bool MostrarMsjSiLeerVacio
        {
            get { return bMostrarMsjLeerVacio; }
            set { bMostrarMsjLeerVacio = value; }
        }

        public bool EsPublicoGeneral
        {
            get { return bEsPublicoGeneral; }
            set 
            { 
                bEsPublicoGeneral = value;
                query.EsPublicoGeneral = value; 
            }
        }

        #region Modulos SII

        public DataSet Farmacias(string Funcion, string IdEstado)
        {
            //DataSet dtsClase = new DataSet();
            string strMsj = "Catalogo de Estados";

            IdEstado = Fg.PonCeros(IdEstado, 2);
            sSql = "Select 'Farmacia' = Farmacia, 'Num. Farmacia' = IdFarmacia From vw_Farmacias (NoLock) Where IdEstado = '" + IdEstado + "' Order by Farmacia ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda de Farmacias", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Farmacias(IdEstado, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        #endregion Modulos SII

        #endregion Funciones y procedimientos publicos

        #region Funciones y procedimientos privados
        private void MsjNoDatos(ref DataSet dts, string strMsj)
        {
            if (bEjecuto)
            {
                dts = new DataSet("Vacio");
                MessageBox.Show(strMsjNoDatos, strMsj, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private string MostrarForma(string strMsj, DataSet dts)
        {
            return MostrarForma(strMsj, dts, false, 1);
        }

        private string MostrarForma(string strMsj, DataSet dts, bool AccesarLocal)
        {
            return MostrarForma(strMsj, dts, AccesarLocal, 1);
        }

        /// <summary>
        /// Muestra la pantalla de Ayudas, diseño para manejo de grandes catalgos.
        /// </summary>
        /// <param name="Titulo">Titulo de la Consulta</param>
        /// <param name="Consulta">Consulta a ejecutar</param>
        /// <param name="AccesarLocal">Determina si se accesan los datos del servidor</param>
        /// <param name="ColInicialCombo">Columna inicial de busqueda</param>
        /// <returns></returns>
        private string MostrarForma(string Titulo, string Consulta, string MsjError, bool AccesarLocal, int ColInicialCombo)
        {
            //// return MostrarForma(strMsj, dts, AccesarLocal, ColInicialCombo, false, "", ""); 
            Frm_Ayuda = new FrmAyuda(ConexionSql);
            Frm_Ayuda.Text = Titulo;
            Frm_Ayuda.bAccesarA_BD_Local = AccesarLocal;
            Frm_Ayuda.CargarAyuda(Consulta, MsjError, ColInicialCombo); 
            Fg.CentrarForma(Frm_Ayuda);
            
            Frm_Ayuda.MostrarPantalla(); 

            string sRegresa = Frm_Ayuda.strResultado;

            return sRegresa;
        }

        private string MostrarForma(string Titulo, DataSet dts, bool AccesarLocal, int ColInicialCombo)
        {
            //// return MostrarForma(strMsj, dts, AccesarLocal, ColInicialCombo, false, "", ""); 
            Frm_Ayuda = new FrmAyuda();
            Frm_Ayuda.Text = Titulo;
            Frm_Ayuda.bAccesarA_BD_Local = AccesarLocal;
            Frm_Ayuda.dtsAyuda = dts;
            Frm_Ayuda.pfConfiguraListView(ColInicialCombo);
            Fg.CentrarForma(Frm_Ayuda);
            Frm_Ayuda.ShowDialog();

            string sRegresa = Frm_Ayuda.strResultado;

            return sRegresa;
        }

        ////private string MostrarForma(string strMsj, DataSet dts, bool AccesarLocal, int ColInicialCombo, 
        ////    bool ConsutalDinamica, string OrigenDeDatos, string Ordenamiento)
        ////{
        ////    Frm_Ayuda = new FrmAyuda();

        ////    Frm_Ayuda.Conexion = this.ConexionSql; 
        ////    Frm_Ayuda.ConsultaDinamica = ConsutalDinamica;
        ////    Frm_Ayuda.OrigenDeDatos = OrigenDeDatos;
        ////    Frm_Ayuda.Ordenamiento = Ordenamiento;

        ////    Frm_Ayuda.Text = strMsj;
        ////    Frm_Ayuda.bAccesarA_BD_Local = AccesarLocal;
        ////    Frm_Ayuda.dtsAyuda = dts;
        ////    Frm_Ayuda.pfConfiguraListView(ColInicialCombo);

        ////    Fg.CentrarForma(Frm_Ayuda);
        ////    Frm_Ayuda.ShowDialog();
        ////    string sRegresa = Frm_Ayuda.strResultado;

        ////    return sRegresa;
        ////} 

        private void ValidaDatos(ref DataSet dtsValidar)
        {
            if (error.ExistenErrores(dtsValidar))
            {
                // Buscar en el dataset la tabla de errores                    
                myResult = error.MostrarVentanaError(true, false, dtsValidar);
                dtsValidar = new DataSet("Vacio");
            }

            bExistenDatos = ExistenDatosEnDataset(dtsValidar);
        }

        private DataSet EjecutarQuery(string Funcion, string prtQuery, string MensajeError, string MensajeNoEncontrado)
        {
            clsLeer Leer = new clsLeer(ref ConexionSql);
            DataSet dtsResultados = new DataSet();

            bEjecuto = false;
            bExistenDatos = false;
            if (!Leer.Exec( " Set DateFormat YMD " + prtQuery))
            {
                General.Error.GrabarError(Leer.Error, ConexionSql.DatosConexion, this.sNameDll, this.sVersion, this.sPantalla, Funcion, Leer.QueryEjecutado);
                General.msjError(MensajeError);
            }
            else
            {
                bEjecuto = true;
                if (!Leer.Leer())
                {
                    //if (bMostrarMsjLeerVacio)
                    //    General.msjUser(MensajeNoEncontrado);
                }
                else
                {
                    bExistenDatos = true;
                    dtsResultados = Leer.DataSetClase;
                }
            }

            return dtsResultados;
        }

        private object EjecutarQuery(string prtQuery, string prtTabla)
        {
            object objRetorno = null;
            DataSet dtsRetorno = new DataSet("Vacio");
            Datos.CadenaDeConexion = strCnnString;
            bExistenDatos = false;


            try
            {
                if (bUsarCnnRedLocal)
                {
                    objRetorno = (object)Datos.ObtenerDataset(prtQuery, prtTabla);
                }
                else
                {
                    // objRetorno = (object)cnnWebServ.ObtenerDataset(strCnnString, prtQuery, prtTabla);
                }

                dtsRetorno = (DataSet)objRetorno;
                if (error.ExistenErrores(dtsRetorno))
                {
                    // Buscar en el dataset la tabla de errores                    
                    myResult = error.MostrarVentanaError(true, false, dtsRetorno);
                    dtsRetorno = new DataSet("Vacio");
                    objRetorno = (object)dtsRetorno;
                }

                bExistenDatos = ExistenDatosEnDataset(dtsRetorno);

            }
            catch (Exception e)
            {
                e = (Exception)objRetorno;
                dtsRetorno = new DataSet("Vacio");
                objRetorno = (object)dtsRetorno;

                errorLog = new clsLogError(e);
                error = new clsErrorManager(errorLog.ListaErrores);
                myResult = error.MostrarVentanaError(true, false, errorLog.ListaErrores);
            }

            return objRetorno;
        }

        private bool ExistenDatosEnDataset(DataSet dtsRevisar)
        {
            bool bRegresa = false;

            if (dtsRevisar.Tables.Count > 0)
            {
                if (dtsRevisar.Tables[0].Rows.Count > 0)
                    bRegresa = true;
            }

            return bRegresa;
        }
        #endregion
    }
}
