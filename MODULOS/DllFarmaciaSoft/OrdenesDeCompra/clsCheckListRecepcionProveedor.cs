using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;

namespace DllFarmaciaSoft.OrdenesDeCompra
{
    public class clsCheckListRecepcionProveedor
    {
        #region DeclaracionVariables
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosConexion datosCnn;
        clsLeer leer;

        DataSet dtsRetorno = new DataSet();
        clsGrabarError Error;       

        string sEmpresa = "";
        string sEstado = "";
        string sFarmacia = "";
        bool bFirma = false;
        bool bGuardo = false;
        #endregion DeclaracionVariables

        #region Constructor
        public clsCheckListRecepcionProveedor(clsDatosConexion DatosConexion)
        {
            cnn = new clsConexionSQL(DatosConexion);
            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError();

            dtsRetorno = null;      
                        
        }
        #endregion Constructor 

        #region Propiedades
        public DataSet Retorno
        {
            get { return dtsRetorno; }
            set { dtsRetorno = value; }
        }

        public bool Firma
        {
            get { return bFirma; }
            set { bFirma = value; }
        }

        public bool Guardo
        {
            get { return bGuardo; }
            set { bGuardo = value; }
        }
        #endregion Propiedades

        #region Funciones Publicas
        public void Mostrar_CheckList()
        {
            bool bMostrar = false;

            FrmCheckListRecepcionProveedor f = new FrmCheckListRecepcionProveedor();

            if (dtsRetorno == null)
            {
                if (CargarCheckList())
                {
                    bMostrar = true;
                    f.MostrarPantalla(dtsRetorno);
                }
            }
            else
            {
                bMostrar = true;
                f.MostrarPantalla(dtsRetorno);
                                
            }

            if (bMostrar)
            {
                f.ShowDialog();
            }

            dtsRetorno = f.Retorno;
            bFirma = f.Firma;
            bGuardo = f.Guardo;
        }
        #endregion Funciones Publicas

        #region Generar_Dataset
        public static DataSet PreparaDtsMotivos()
        {
             
            DataSet dts = new DataSet();
            DataTable dtCheck = new DataTable("CheckList");

            dtCheck.Columns.Add("IdGrupo", GetType(TypeCode.String));
            dtCheck.Columns.Add("IdMotivo", GetType(TypeCode.String));
            dtCheck.Columns.Add("Grupo", GetType(TypeCode.String));
            dtCheck.Columns.Add("Motivo", GetType(TypeCode.String));
            dtCheck.Columns.Add("SI", GetType(TypeCode.Int32));
            dtCheck.Columns.Add("SI_Firma", GetType(TypeCode.Int32));
            dtCheck.Columns.Add("NO", GetType(TypeCode.Int32));
            dtCheck.Columns.Add("NO_Firma", GetType(TypeCode.Int32));
            dtCheck.Columns.Add("Rechazo", GetType(TypeCode.Int32));
            dtCheck.Columns.Add("Rechazo_Firma", GetType(TypeCode.Int32));
            dtCheck.Columns.Add("Comentario", GetType(TypeCode.String));

            dts.Tables.Add(dtCheck);

            return dts.Clone();
        }

        private static Type GetType(TypeCode TipoDato)
        {
            return Type.GetType("System." + TipoDato.ToString());
        }
        #endregion Generar_Dataset

        #region Funciones_Privadas
        private bool CargarCheckList()
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format(" Select IdGrupo, IdMotivo, Grupo, Motivo, cast(SI as Int) as SI, cast(SI_Firma as Int) as SI_Firma,  " +
                " cast(NO as Int) as NO, cast(NO_Firma as Int) as NO_Firma, cast(Rechazo as Int) as Rechazo, cast(Rechazo_Firma as Int) as Rechazo_Firma, " +
                " cast('' as varchar(200)) as Comentario From vw_COM_CheckList_Recepcion  Order By IdGrupo, IdMotivo "); 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarCheckList");
                General.msjError("Ocurrio un error al obtener los datos del check list.");
                bRegresa = false;
            }
            else
            {
                if (leer.Leer())
                {
                    dtsRetorno = leer.DataSetClase;
                }
                else
                {
                    General.msjUser("No se encontro informacion del check list.");
                    bRegresa = false;
                }
            }

            return bRegresa;
        }
        #endregion Funciones_Privadas

        
    }
}
