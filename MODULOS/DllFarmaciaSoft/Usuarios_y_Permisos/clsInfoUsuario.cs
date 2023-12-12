using System;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;

namespace DllFarmaciaSoft.Usuarios_y_Permisos
{
    public class clsInfoUsuario
    {
        #region Declaracion de Variables 
        private string sIdEstado = "";
        private string sIdFarmacia = "";
        private string sIdPersonal = "";
        private string sNombreCompleto = "";
        private string sLoginUser = "";
        private string sIdGrupoUsers = "";
        private string sNombreGrupoUsers = "";

        private bool bUsurioTipoAdministrador = false;
        private string sAdminGrupo = "ADMINISTRADORES";
        #endregion Declaracion de Variables

        #region Construnctores y Destructor de Clase
        public clsInfoUsuario()
        {
        }

        public clsInfoUsuario(clsLeer Datos)
        {
        }

        ~clsInfoUsuario()
        { 
        }
        #endregion Construnctores y Destructor de Clase

        #region Propiedades 
        public string Estado
        {
            get { return sIdEstado; }
            set 
            { 
                if ( sIdEstado == "" )
                    sIdEstado = value; 
            }
        }

        public string Farmacia
        {
            get { return sIdFarmacia; }
            set 
            {
                if (sIdFarmacia == "")
                    sIdFarmacia = value; 
            }
        }

        public string IdPersonal
        {
            get { return sIdPersonal; }
            set 
            {
                if (sIdPersonal == "")
                    sIdPersonal = value; 
            }
        }

        public string Nombre
        {
            get { return sNombreCompleto; }
            set 
            {
                if (sNombreCompleto == "")
                    sNombreCompleto = value; 
            }
        }

        public string Login
        {
            get { return sLoginUser; }
            set 
            {
                if (sLoginUser == "")
                    sLoginUser = value; 
            }
        }

        public string IdGrupoDeUsuarios
        {
            get { return sIdGrupoUsers; }
            set 
            {
                if (sIdGrupoUsers == "") 
                    sIdGrupoUsers = value; 
            }
        }

        public string GrupoDeUsuarios
        {
            get { return sNombreGrupoUsers; }
            set 
            {
                if (sNombreGrupoUsers == "")
                {
                    bUsurioTipoAdministrador = false;
                    sNombreGrupoUsers = value;

                    if (sNombreGrupoUsers.ToUpper() == sAdminGrupo)
                        bUsurioTipoAdministrador = true;
                }
            }
        }

        public bool EsAdministrador
        {
            get { return bUsurioTipoAdministrador; }
        }

        #endregion Propiedades

    }
}
