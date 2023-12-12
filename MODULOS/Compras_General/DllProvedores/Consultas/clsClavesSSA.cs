using System;
using System.Collections; 
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

using DllProveedores;
using DllProveedores.wsProveedores;

namespace DllProveedores.Consultas
{
    public class clsClavesSSA : clsLeerWeb
    {
        clsLeer leer;
        bool bExisteError = false; 

        #region Constructores y Destructor        
        public clsClavesSSA(string Url, clsDatosCliente datosCliente):base(Url, datosCliente)
        {
        }
        #endregion Constructores y Destructor

        #region Propiedades
        public clsLeer Local
        {
            get { return leer; }
        } 
        #endregion Propiedades

        #region Funciones y Procedimientos Publicos
        public void Cargar()
        {
            string sSql = "Select IdClaveSSA_Sal, ClaveSSA, DescripcionSal, IdGrupoTerapeutico, GrupoTerapeutico, TipoCatalogo, TipoDeCatalogo, StatusClave " +
                " From vw_ClavesSSA_Sales (NoLock) Where StatusClave = 'A' ";
            bExisteError = false;
            if (!base.Exec(sSql))
            {
                bExisteError = true;
            }
        }

        public void Buscar_Clave(string Clave)
        {
            leer = new clsLeer();
            //string sSql = string.Format(" ClaveSSA = '{0}' ", Clave); 
            //leer.DataRowsClase = this.DataTableClase.Select(sSql); 

            string sSql = string.Format("Select IdClaveSSA_Sal, ClaveSSA, DescripcionSal, IdGrupoTerapeutico, GrupoTerapeutico, TipoCatalogo, TipoDeCatalogo, StatusClave " +
                " From vw_ClavesSSA_Sales (NoLock) Where StatusClave = 'A' and ClaveSSA = '{0}' ", Clave);
            
            bExisteError = false;
            if (!base.Exec(sSql))
            {
                bExisteError = true;
                General.msjError("Ocurrió un error al solicitar la información de la Clave.");
            }
            else
            {
                leer.DataSetClase = this.DataSetClase; 
            }
        } 
        #endregion Funciones y Procedimientos Publicos 

    }
}
