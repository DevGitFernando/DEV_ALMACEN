using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;

using DllProveedores;
using DllProveedores.wsProveedores;

namespace DllProveedores.Consultas
{
    public class clsProductosCodigosEAN : clsLeerWeb
    {
        clsLeer leer;
        bool bExisteError = false; 

        #region Constructores y Destructor 
        public clsProductosCodigosEAN(string Url, clsDatosCliente datosCliente)
            : base(Url, datosCliente)
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
            string sSql = "Select IdProducto, CodigoEAN, CodigoEAN_Interno, TasaIva, IdClaveSSA_Sal, " + 
                " ClaveSSA, DescripcionSal, IdGrupoTerapeutico, GrupoTerapeutico, Descripcion, DescripcionCorta, " + 
                " IdClasificacion, Clasificacion, EsControlado, EsSectorSalud, " + 
                " IdLaboratorio, Laboratorio, IdPresentacion, Presentacion, Status " +
                " From vw_Productos_CodigoEAN (NoLock) Where Status = 'A' ";
            bExisteError = false;
            if (!base.Exec(sSql)) 
            {
                bExisteError = true;
            }
        }

        public void Buscar_Clave(string Clave)
        {
            leer = new clsLeer();
            string sSql = string.Format(" IdClaveSSA_Sal = '{0}' or ClaveSSA = '{0}' ", Clave);

            leer.DataRowsClase = this.DataTableClase.Select(sSql);
        }

        public void Buscar_CodigoEAN(string CodigoEAN)
        {
            leer = new clsLeer();
            string sSql = string.Format(" CodigoEAN = '{0}' ", CodigoEAN);

            leer.DataRowsClase = this.DataTableClase.Select(sSql);
        } 

        public void Buscar_CodigoEAN(string Clave, string CodigoEAN)
        {
            leer = new clsLeer();
            //string sSql = string.Format(" IdClaveSSA_Sal = '{0}' and CodigoEAN = '{1}' ", Clave, CodigoEAN); 
            //leer.DataRowsClase = this.DataTableClase.Select(sSql); 

            string sSql = string.Format("Select IdProducto, CodigoEAN, CodigoEAN_Interno, TasaIva, IdClaveSSA_Sal, " +
                " ClaveSSA, DescripcionSal, IdGrupoTerapeutico, GrupoTerapeutico, Descripcion, DescripcionCorta, " +
                " IdClasificacion, Clasificacion, EsControlado, EsSectorSalud, " +
                " IdLaboratorio, Laboratorio, IdPresentacion, Presentacion, Status " +
                " From vw_Productos_CodigoEAN (NoLock) " + 
                " Where Status = 'A' and IdClaveSSA_Sal = '{0}' and CodigoEAN = '{1}' ", 
                Clave, CodigoEAN);

            bExisteError = false;
            if (!base.Exec(sSql))
            {
                bExisteError = true;
                General.msjError("Ocurrió un error al solicitar la información del CodigoEAN.");
            }
            else
            {
                leer.DataSetClase = this.DataSetClase;
            }

        } 
        #endregion Funciones y Procedimientos Publicos
    }
}
