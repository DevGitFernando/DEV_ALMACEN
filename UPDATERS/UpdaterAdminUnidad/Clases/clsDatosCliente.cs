using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Data;

using UpdaterAdminUnidad;
// using UpdaterAdminUnidad.Errores;

namespace UpdaterAdminUnidad.Data
{
    public class clsDatosCliente
    {
        private string sDll = "";
        private string sVersion = ""; 
        private string sClase = ""; 
        private string sFuncionProcedimiento = "";
        private string sIpHost = General.Ip;
        private string sHostName = General.NombreEquipo;

        //string[] sListaCampos = { "Dll", "Clase", "Funcion", "Consulta", "Ip", "SqlNumerror", "SqlTxtError", "SqlEstado", "HostName" };

        #region Constructor
        public clsDatosCliente(DataSet DatosCliente)
        {
            // clsGrabarError manError = new clsGrabarError();

            try
            {
                this.ExtraerDatos(DatosCliente);
            }
            catch (Exception ex)
            {
                ex.Source = ex.Source; 
            //    manError.LogError("[ Error al preparar el Datos de Cliente.... " + ex.Message + "] ", FileAttributes.Normal);
            }
        }

        public clsDatosCliente(clsDatosApp DatosApp, string Clase, string FuncionProcedimiento)
        {
            this.sDll = DatosApp.Modulo;
            this.sVersion = DatosApp.Version;
            this.sClase = Clase;
            this.sFuncionProcedimiento = FuncionProcedimiento;
        }

        public clsDatosCliente(string Dll, string Version, string Clase, string FuncionProcedimiento)
        {  
            this.sDll = Dll;
            this.sVersion = Version;
            this.sClase = Clase;
            this.sFuncionProcedimiento = FuncionProcedimiento;
        }
        #endregion Constructor

        #region Propiedades
        public string Dll
        {
            get { return sDll; }
        }

        public string Version
        {
            get { return sVersion; }
        }

        public string Clase
        {
            get { return sClase; }
        }

        public string Funcion
        {
            get { return sFuncionProcedimiento; }
            set { sFuncionProcedimiento = value; }
        }

        public string IpHostCliente
        {
            get { return sIpHost; }
        }

        public string HostNameCliente
        {
            get { return sHostName; }
        }
        #endregion Propiedades

        #region Funciones y Procedimientos publicos
        private void InicializarVariables()
        {
            sDll = "";
            sVersion = ""; 
            sClase = "";
            sFuncionProcedimiento = "";
            sIpHost = "";
            sHostName = "";
        }
        private void ExtraerDatos(DataSet Datos)
        {
            try
            {
                if (Datos != null)
                {
                    if (Datos.Tables.Count > 0)
                    {
                        DataRow dt = Datos.Tables["DatosCliente"].Rows[0];
                        sDll = dt["Dll"].ToString();
                        sVersion = dt["Version"].ToString();
                        sClase = dt["Clase"].ToString();
                        sFuncionProcedimiento = dt["Funcion"].ToString();
                        sIpHost = dt["IpHostCliente"].ToString();
                        sHostName = dt["HostNameCliente"].ToString();
                    }
                }
            }
            catch { InicializarVariables(); }
        }

        public DataSet DatosCliente()
        {
            DataSet dtsDatosCliente = new DataSet("DatosCliente");
            DataTable dtDatos = new DataTable("DatosCliente");

            try
            {
                dtDatos.Columns.Add("Dll", Type.GetType("System.String"));
                dtDatos.Columns.Add("Version", Type.GetType("System.String"));
                dtDatos.Columns.Add("Clase", Type.GetType("System.String"));
                dtDatos.Columns.Add("Funcion", Type.GetType("System.String"));
                dtDatos.Columns.Add("IpHostCliente", Type.GetType("System.String"));
                dtDatos.Columns.Add("HostNameCliente", Type.GetType("System.String"));
                dtDatos.Rows.Add(sDll, sVersion, sClase, sFuncionProcedimiento, sIpHost, sHostName);
            }
            catch { }

            dtsDatosCliente.Tables.Add(dtDatos);

            return dtsDatosCliente;
        }

        #endregion Funciones y Procedimientos publicos

    }
}
