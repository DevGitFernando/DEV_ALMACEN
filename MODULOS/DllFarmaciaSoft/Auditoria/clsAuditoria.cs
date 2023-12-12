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

namespace DllFarmaciaSoft
{
    public class clsAuditoria
    {
        #region Declaración de variables
        
        private string strCnnString = General.CadenaDeConexion;

        string sEstado = "";
        string sFarmacia = ""; 
        string sUsuario = ""; 
        string sSesion = "";
        string sMacAddress = General.MacAddress;

        string sNameDll = "";
        string sPantalla = "";
        string sVersion = ""; 

        private clsDatosConexion DatosConexion;
        private clsConexionSQL ConexionSql;
        private clsLeer leer;

        private static string sIdSesion = "";
        #endregion

        public clsAuditoria(clsDatosConexion Conexion,
            string Estado, string Farmacia, string Usuario, string SesionActual, 
            string Modulo, string Pantalla, string Version)
        {
            this.sEstado = Estado;
            this.sFarmacia = Farmacia;
            this.sUsuario = Usuario;
            this.sSesion = SesionActual == "" ? "*" : SesionActual; 

            this.sNameDll = Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = Version;

            DatosConexion = Conexion;
            ConexionSql = new clsConexionSQL(Conexion); 
            ConexionSql.SetConnectionString();

            leer = new clsLeer(ref ConexionSql);
        } 
        
        #region Propiedades 
        public static string Sesion
        {
            get { return sIdSesion; }
            set { sIdSesion = value; }
        } 
        #endregion Propiedades

        #region Funciones Publicas
        public bool GuardarAud_LoginReg()
        {
            bool bRegresa = false;

            if (ConexionSql.Abrir())
            {
                ConexionSql.IniciarTransaccion();

                string sSql = string.Format(" Exec spp_Mtto_CteReg_Auditoria_Login '{0}', '{1}', '{2}', '{3}' ",
                                            sEstado, sUsuario, sSesion, sMacAddress);

                if (!leer.Exec(sSql))
                {
                    General.Error.GrabarError(leer.Error, ConexionSql.DatosConexion, this.sNameDll, this.sVersion, this.sPantalla, "GuardarAud_LoginReg", leer.QueryEjecutado);
                }
                else
                {
                    if (leer.Leer())
                    {
                        bRegresa = true;
                        sIdSesion = leer.Campo("Clave");
                    }
                    
                }

                if (bRegresa)
                {
                    ConexionSql.CompletarTransaccion();
                }
                else
                {
                    ConexionSql.DeshacerTransaccion();
                }
                ConexionSql.Cerrar();
            }
            

            return bRegresa;
        }

        public bool GuardarAud_MovtosReg( string Instruccion, string Url_Farmacia)
        {
            bool bRegresa = false;

            if (ConexionSql.Abrir())
            {
                ConexionSql.IniciarTransaccion();

                string sSql = string.Format(" Exec spp_Mtto_CteReg_Auditoria_Movimientos '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}'",
                                            sEstado, sUsuario, sSesion, "*", sMacAddress, sNameDll, sPantalla, Instruccion, Url_Farmacia);

                if (!leer.Exec(sSql))
                {
                    General.Error.GrabarError(leer.Error, ConexionSql.DatosConexion, this.sNameDll, this.sVersion, this.sPantalla, "GuardarAud_MovtosReg", leer.QueryEjecutado);
                }
                else
                {
                    bRegresa = true;
                }

                if (bRegresa)
                {
                    ConexionSql.CompletarTransaccion();
                }
                else
                {
                    ConexionSql.DeshacerTransaccion();
                }
                ConexionSql.Cerrar();
            }

            return bRegresa;
        }

        public bool GuardarAud_LoginUni()
        {
            bool bRegresa = false;

            if (ConexionSql.Abrir())
            {
                ConexionSql.IniciarTransaccion();

                string sSql = string.Format(" Exec spp_Mtto_CteUni_Auditoria_Login '{0}', '{1}', '{2}', '{3}', '{4}' ",
                                            sEstado, sFarmacia, sUsuario, sSesion, sMacAddress);

                if (!leer.Exec(sSql))
                {
                    General.Error.GrabarError(leer.Error, ConexionSql.DatosConexion, this.sNameDll, this.sVersion, this.sPantalla, "GuardarAud_LoginUni", leer.QueryEjecutado);
                }
                else
                {
                    if (leer.Leer())
                    {
                        bRegresa = true;
                        sIdSesion = leer.Campo("Clave");
                    }
                }

                if (bRegresa)
                {
                    ConexionSql.CompletarTransaccion();
                }
                else
                {
                    ConexionSql.DeshacerTransaccion();
                }
                ConexionSql.Cerrar();
            }

            return bRegresa;
        }

        public bool GuardarAud_MovtosUni(string Movto, string Instruccion)
        {
            bool bRegresa = false;

            if (ConexionSql.Abrir())
            {
                ConexionSql.IniciarTransaccion();

                string sSql = string.Format(" Exec spp_Mtto_CteUni_Auditoria_Movimientos '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}' ",
                                            sEstado, sFarmacia, sUsuario, sSesion, Movto, sMacAddress, sNameDll, sPantalla, Instruccion );

                if (!leer.Exec(sSql))
                {
                    General.Error.GrabarError(leer.Error, ConexionSql.DatosConexion, this.sNameDll, this.sVersion, this.sPantalla, "GuardarAud_MovtosUni", leer.QueryEjecutado);
                }
                else
                {
                    bRegresa = true;
                }

                if (bRegresa)
                {
                    ConexionSql.CompletarTransaccion();
                }
                else
                {
                    ConexionSql.DeshacerTransaccion();
                }
                ConexionSql.Cerrar();
            }

            return bRegresa;
        }
        #endregion Funciones Publicas
    }
}
