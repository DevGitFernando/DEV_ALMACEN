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
    internal partial class FrmRPT_Parametros : FrmBaseExt
    {
        enum Cols
        {
            Ninguna = 0, 
            Clave = 1, Descripcion = 2 
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerDatos;
        clsLeer leerExec;
        clsAyudas Ayuda = new clsAyudas();
        clsConsultas Consultas;
        clsDatosCliente DatosCliente;

        DataSet dtsParametros; 
        clsGrid grid;
        SeleccionParametros tipoParametros = SeleccionParametros.Ninguno;
        string sValorSeleccionado = "";
        Cols colActiva = Cols.Ninguna; 

        public int ClavesDeControlados = 2;
        public int ClavesDeAntibioticos = 2; 

        bool bEsParaSP = false; 
        string sCampoClave = "";
        string sCampoDescripcion = "";

        string sCampoClaveConsulta = "";
        string sCampoDescripcionConsulta = "";

        string sListaClaves = "";
        string sListaDescripciones = "";
        string sAliasTabla = "";
        string sSeparador = ""; 

        public FrmRPT_Parametros(SeleccionParametros Parametros, string Titulo, 
            string CampoClave, string CampoDescripcion,
            string CampoClaveConsulta, string CampoDescripcionConsulta, 
            DataSet ListaParametros, bool EsParaSP, string AliasTabla)
        {
            InitializeComponent();


            leer = new clsLeer(ref cnn);
            leerExec = new clsLeer(ref cnn);
            leerDatos = new clsLeer(ref cnn);
            DatosCliente = new clsDatosCliente(GnInventarios.DatosApp, this.Name, "");

            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name, false);
            Ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name, false);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            dtsParametros = ListaParametros; 

            grid = new clsGrid(ref grdParametros, this);
            grdParametros.EditModeReplace = true; 
            grid.EstiloDeGrid = eModoGrid.ModoRow;
            grid.LlenarGrid(dtsParametros);

            if (grid.Rows == 0)
            {
                grid.Limpiar(true);
            }

            this.Text = Titulo;
            this.ControlBox = false; 

            tipoParametros = Parametros;
            sCampoClave = CampoClave;
            sCampoDescripcion = CampoDescripcion;
            sCampoClaveConsulta = CampoClaveConsulta;
            sCampoDescripcionConsulta = CampoDescripcionConsulta; 

            dtsParametros = ListaParametros;
            bEsParaSP = EsParaSP;
            sAliasTabla = AliasTabla.Replace(".", "").Replace(" ", "");
            sSeparador = bEsParaSP ? "''" : "'";
            ////sSeparador = "'";

            Configurar_Interface();
        }

        private void FrmRPT_Parametros_Load(object sender, EventArgs e)
        {

        }

        #region Propiedades Publicas 
        public DataSet DtsParametros
        {
            get { return dtsParametros; }
            set { dtsParametros = value; }
        }

        public string ListaClaves
        {
            get { return sListaClaves; }
        }

        public string ListaDescripciones
        {
            get { return sListaDescripciones; }
        }
        #endregion Propiedades Publicas

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            grid.Limpiar(true);
            grid.SetActiveCell(1, 1); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            RegresarInformacion(true);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            RegresarInformacion(false);
        }
        #endregion Botones

        #region Funciones y Procedimientos Privados 
        private void Configurar_Interface()
        {
            switch (tipoParametros)
            {
                case SeleccionParametros.ClavesSSA:
                    grid.PonerEncabezado((int)Cols.Clave, "Clave SSA");
                    grid.PonerEncabezado((int)Cols.Descripcion, "Descripción Clave SSA"); 
                    break;

                case SeleccionParametros.Laboratorios:
                    grid.PonerEncabezado((int)Cols.Clave, "Clave Labotarorio");
                    grid.PonerEncabezado((int)Cols.Descripcion, "Nombre labotario");
                    break;

                default:
                    break;
            }
        }

        private void RegresarInformacion(bool Actualizar)
        {
            clsLeer leerDatos = new clsLeer();
            DataSet dtsDatos = dtsParametros.Clone();
            //object[] objDatos = null ; 

            string sDato_Clave = "";
            string sDato_Descripcion = "";

            sListaClaves = "";
            sListaDescripciones = "";


            if (!Actualizar)
            {
                leerDatos.DataSetClase = dtsParametros;
            }
            else
            {
                for (int i = 1; i <= grid.Rows; i++)
                {
                    object[] objDatos = 
                    { 
                        grid.GetValue(i, (int)Cols.Clave),  
                        grid.GetValue(i, (int)Cols.Descripcion) 
                    };
                    dtsDatos.Tables[0].Rows.Add(objDatos);
                }

                leerDatos.DataSetClase = dtsDatos; 
            }

            while (leerDatos.Leer())
            {
                sDato_Clave = leerDatos.Campo("Clave");
                sDato_Descripcion = leerDatos.Campo("Descripcion");

                if (sDato_Clave != "")
                {
                    sListaClaves += string.Format("{0}{1}{0}, ", sSeparador, sDato_Clave);
                }
                else
                {
                    if (sDato_Descripcion != "")
                    {
                        sListaDescripciones += string.Format(" {0} like {1}%{2}%{1} or ", sCampoDescripcion, sSeparador, sDato_Descripcion.Replace(" ", "%"));
                    }
                }
            }

            ////Quitar la ultima "coma" 
            if (sListaClaves != "")
            {
                sListaClaves = sListaClaves.Trim();
                sListaClaves = Fg.Left(sListaClaves, sListaClaves.Length - 1);

                sListaClaves = string.Format("{0} in ( {1} ) ", sCampoClave, sListaClaves); 
            }

            ////Quitar el ultimo "or" 
            if (sListaDescripciones != "")
            {
                sListaDescripciones = sListaDescripciones.Trim();
                sListaDescripciones = Fg.Left(sListaDescripciones, sListaDescripciones.Length - 2);
                sListaDescripciones = sListaDescripciones.Trim();
                sListaDescripciones = string.Format("( {0} ) ", sListaDescripciones); 
            }


            dtsParametros = leerDatos.DataSetClase;
            this.Hide();
        }
        #endregion Funciones y Procedimientos Privados

        #region Grid
        private void grdParametros_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if ((grid.ActiveRow == grid.Rows) && e.AdvanceNext)
            {
                if (grid.GetValue(grid.ActiveRow, (int)Cols.Clave) != "" || grid.GetValue(grid.ActiveRow, (int)Cols.Descripcion) != "")
                {
                    grid.Rows = grid.Rows + 1;
                    grid.ActiveRow = grid.Rows;
                    grid.SetActiveCell(grid.Rows, (int)Cols.Clave);
                }
            }
        }

        private void grdParametros_EditModeOff(object sender, EventArgs e)
        {
            colActiva = (Cols)grid.ActiveCol; 
            sValorSeleccionado = grid.GetValue(grid.ActiveRow, (int)Cols.Clave);

            if (colActiva == Cols.Clave)
            {
                switch (tipoParametros)
                {
                    case SeleccionParametros.ClavesSSA:
                        ClavesSSA_Consulta();
                        break;

                    case SeleccionParametros.Laboratorios:
                        Laboratorios_Consulta();
                        break;

                    default:
                        break;
                }
            }
        }

        private void grdParametros_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    int iRow = grid.ActiveRow;
                    grid.DeleteRow(iRow);
                }
                catch { }

                if (grid.Rows == 0)
                {
                    grid.Limpiar(true);
                }
            }

            if (e.KeyCode == Keys.F1)
            {
                switch (tipoParametros)
                {
                    case SeleccionParametros.ClavesSSA:
                        ClavesSSA_Ayuda();
                        break;

                    case SeleccionParametros.Laboratorios:
                        Laboratorios_Ayuda();
                        break;

                    default:
                        break;
                }
            }
        }
        #endregion Grid


        #region Componentes
        #region Claves SSA 
        private void ClavesSSA_Ayuda()
        {
            leer.DataSetClase = Ayuda.ClavesSSA_Sales(ClavesDeControlados, ClavesDeAntibioticos, true, "ClavesSSA_Ayuda");
            ClavesSSA_CargarInformacion();
        }

        private void ClavesSSA_Consulta()
        {
            if (sValorSeleccionado != "")
            {
                leer.DataSetClase = Consultas.ClavesSSA_Sales(sValorSeleccionado, true, ClavesDeControlados, ClavesDeAntibioticos, "ClavesSSA_Consulta()");
                ClavesSSA_CargarInformacion();
            }
        }

        private void ClavesSSA_CargarInformacion()
        {
            if (leer.Leer())
            {
                int iRowActivo = grid.ActiveRow;

                grid.SetValue(iRowActivo, (int)Cols.Clave, leer.Campo(sCampoClaveConsulta));
                grid.SetValue(iRowActivo, (int)Cols.Descripcion, leer.Campo(sCampoDescripcionConsulta));

                grid.SetActiveCell(iRowActivo, (int)Cols.Clave);
            }
        }
        #endregion Claves SSA

        #region Laboratorios
        private void Laboratorios_Ayuda()
        {
            leer.DataSetClase = Ayuda.Laboratorios("Laboratorios_Ayuda");
            Laboratorios_CargarInformacion();
        }

        private void Laboratorios_Consulta()
        {
            if (sValorSeleccionado != "")
            {
                leer.DataSetClase = Consultas.Laboratorios(sValorSeleccionado, "Laboratorios_Consulta()");
                Laboratorios_CargarInformacion();
            }
        }

        private void Laboratorios_CargarInformacion()
        {
            if (leer.Leer())
            {
                int iRowActivo = grid.ActiveRow;

                grid.SetValue(iRowActivo, (int)Cols.Clave, leer.Campo(sCampoClaveConsulta));
                grid.SetValue(iRowActivo, (int)Cols.Descripcion, leer.Campo(sCampoDescripcionConsulta));

                grid.SetActiveCell(iRowActivo, (int)Cols.Clave);
            }
        }
        #endregion Laboratorios 

        #endregion Componentes
    }
}
