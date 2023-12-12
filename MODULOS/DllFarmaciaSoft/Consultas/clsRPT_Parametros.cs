using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;

namespace DllFarmaciaSoft
{
    internal enum SeleccionParametros
    {
        Ninguno,
        Clientes,
        ClavesSSA, 
        Productos, 
        Laboratorios, 
        Personal 
    }

    public class clsRPT_Parametros
    {
        FrmRPT_Parametros parametros; 
        DataSet dtsClientes = null;
        DataSet dtsClavesSSA = null;
        DataSet dtsClavesSSA_Controlados = null;
        DataSet dtsClavesSSA_Antibioticos = null;
        DataSet dtsProductos = null;
        DataSet dtsLaboratorios = null;
        DataSet dtsPersonal = null; 

        #region Constructores 
        public clsRPT_Parametros()
        { 
        }
        #endregion Constructores

        #region Funciones y Procedimientos Publicos
        public void ClavesSSA(
            ref string ListaClavesSSA, ref string ListaDescripcionClavesSSA,
            string Titulo, string CampoClave, string CampoDescripcion,
            string CampoClaveConsulta, string CampoDescripcionConsulta,
            bool EsParaSP, string AliasTabla)
        {
            InicializarDataSet(ref dtsClavesSSA, "DtsClavesSSA", "DtClavesSSA");

            parametros = new FrmRPT_Parametros(SeleccionParametros.ClavesSSA, Titulo, CampoClave, CampoDescripcion, CampoClaveConsulta, CampoDescripcionConsulta,
                dtsClavesSSA, EsParaSP, AliasTabla);
            parametros.ShowDialog();

            ListaClavesSSA = parametros.ListaClaves;
            ListaDescripcionClavesSSA = parametros.ListaDescripciones;
            dtsClavesSSA = parametros.DtsParametros;
        }

        public void ClavesSSA_Controlados(ref string ListaClavesSSA, ref string ListaDescripcionClavesSSA,
            string Titulo, string CampoClave, string CampoDescripcion,
            string CampoClaveConsulta, string CampoDescripcionConsulta, 
            bool EsParaSP, string AliasTabla)
        {

            InicializarDataSet(ref dtsClavesSSA_Controlados, "DtsClavesSSA_Controlados", "DtClavesSSA_Controlados");

            parametros = new FrmRPT_Parametros(SeleccionParametros.ClavesSSA, Titulo, CampoClave, CampoDescripcion, CampoClaveConsulta, CampoDescripcionConsulta,
                dtsClavesSSA_Controlados, EsParaSP, AliasTabla);

            parametros.ClavesDeControlados = 1;
            parametros.ClavesDeAntibioticos = 2;
            parametros.ShowDialog();

            ListaClavesSSA = parametros.ListaClaves;
            ListaDescripcionClavesSSA = parametros.ListaDescripciones;
            dtsClavesSSA_Controlados = parametros.DtsParametros;
        }

        public void ClavesSSA_Antibioticos(ref string ListaClavesSSA, ref string ListaDescripcionClavesSSA,
            string Titulo, string CampoClave, string CampoDescripcion,
            string CampoClaveConsulta, string CampoDescripcionConsulta,
            bool EsParaSP, string AliasTabla)
        {

            InicializarDataSet(ref dtsClavesSSA_Antibioticos, "DtsClavesSSA_Antibioticos", "DtClavesSSA_Antibioticos");

            parametros = new FrmRPT_Parametros(SeleccionParametros.ClavesSSA, Titulo, CampoClave, CampoDescripcion, CampoClaveConsulta, CampoDescripcionConsulta,
                dtsClavesSSA_Antibioticos, EsParaSP, AliasTabla);

            parametros.ClavesDeControlados = 2;
            parametros.ClavesDeAntibioticos = 1;
            parametros.ShowDialog();

            ListaClavesSSA = parametros.ListaClaves;
            ListaDescripcionClavesSSA = parametros.ListaDescripciones;
            dtsClavesSSA_Antibioticos = parametros.DtsParametros;
        }

        public void Laboratorios(ref string ListaLaboratorios, ref string ListaDescripcionLaboratorios,
            string Titulo, string CampoClave, string CampoDescripcion,
            string CampoClaveConsulta, string CampoDescripcionConsulta,
            bool EsParaSP, string AliasTabla)
        {

            InicializarDataSet(ref dtsLaboratorios, "DtsLaboratorios", "DtClavesSSA_Laboratorios");

            parametros = new FrmRPT_Parametros(SeleccionParametros.Laboratorios, Titulo, CampoClave, CampoDescripcion, CampoClaveConsulta, CampoDescripcionConsulta,
                dtsLaboratorios, EsParaSP, AliasTabla);

            parametros.ShowDialog();

            ListaLaboratorios = parametros.ListaClaves;
            ListaDescripcionLaboratorios = parametros.ListaDescripciones;
            dtsLaboratorios = parametros.DtsParametros;
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados
        private void InicializarDataSet(ref DataSet Dts, string NombreDataSet, string NombreTabla)
        {
            if (Dts == null)
            {
                Dts = PrepararDataSet(NombreDataSet, NombreTabla); 
            }
        }

        private DataSet PrepararDataSet(string NombreDataSet, string NombreTabla)
        {
            DataSet dtsBase = new DataSet(NombreDataSet);
            DataTable dtTBase = new DataTable(NombreTabla);

            dtTBase.Columns.Add("Clave", Type.GetType("System.String"));
            dtTBase.Columns.Add("Descripcion", Type.GetType("System.String"));

            dtsBase.Tables.Add(dtTBase.Clone());

            return dtsBase.Clone();
        }
        #endregion Funciones y Procedimientos Privados
    }
}
