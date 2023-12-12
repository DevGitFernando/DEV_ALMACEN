using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;

namespace DllFarmaciaSoft.Inventario
{
    public partial class FrmListaDeSubFarmacias : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid Grid;
        clsConsultas query;
        clsAyudas ayuda;
        clsDatosCliente DatosCliente;
        // wsFarmacia.wsCnnCliente conexionWeb; // = new Farmacia.wsFarmacia.wsCnnCliente();
        DataTable dtSubFarmacias = new DataTable("SubFarmacias");


        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sSubFarmaciasProcesar = "", sListadoSubFarmacias = "";
        string sAliasTabla = "";
        bool bEsParaSP = false;

        private enum Cols
        {
            Ninguno = 0,
            IdSubFarmacia = 1, Descripcion = 2, Tipo = 3, Procesar = 4 
        }

        ////public FrmListaDeSubFarmacias()
        ////{ 
        ////    InicializarModulo(); 
        ////} 

        public FrmListaDeSubFarmacias(string IdEstado, string IdFarmacia)
        {
            InicializarModulo();
            sEstado = IdEstado;
            sFarmacia = IdFarmacia; 
        }

        private void InicializarModulo()
        {
            InitializeComponent();
            cnn.SetConnectionString();

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name, true);
            ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name);

            Grid = new clsGrid(ref grdExistencia, this);
            Grid.EstiloGrid(eModoGrid.ModoRow);

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "");
            // conexionWeb = new Farmacia.wsFarmacia.wsCnnCliente();
            // conexionWeb.Url = General.Url;
        } 


        #region Propiedades Publicas
        public DataTable SubFarmaciasAProcesar
        {
            get
            {
                return dtSubFarmacias;
            }
        }

        public string CondicionSubFarmacias
        {
            get
            {
                sSubFarmaciasProcesar = ObtenerCondicionSubFarmacias();
                return sSubFarmaciasProcesar;
            }
        }

        public string ListadoSubFarmacias
        {
            get
            {
                sListadoSubFarmacias = ObtenerListadoSubFarmacias();
                return sListadoSubFarmacias;
            }
        }

        public string AliasTabla
        {
            get {return sAliasTabla;}
            set {sAliasTabla = value;}
        }

        public string Estado
        {
            get {return sEstado;}
            set {sEstado = value;}
        }

        public string Farmacia
        {
            get {return sFarmacia;}
            set {sFarmacia = value;}
        }

        public bool EsParaSP
        {
            get { return bEsParaSP; }
            set { bEsParaSP = value; }
        }

        #endregion Propiedades Publicas

        private void FrmExistenciaPorClaveSSA_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
            ObtieneSubFarmacias();
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            query.MostrarMsjSiLeerVacio = false;
            Fg.IniciaControles(this, true);            
            Grid.Limpiar(false);
            IniciaToolBar(false, true);
            query.MostrarMsjSiLeerVacio = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sIdSubFarmacia = "";
            bool bProcesar = false;

            CrearTabla();
            for (int i = 1; i <= Grid.Rows; i++)
            {
                if (bEsParaSP)
                {
                    sIdSubFarmacia = "''" + Grid.GetValue(i, (int)Cols.IdSubFarmacia) + "''";
                }
                else
                {
                    sIdSubFarmacia = "'" + Grid.GetValue(i, (int)Cols.IdSubFarmacia) + "'";
                }
                bProcesar = Grid.GetValueBool(i, (int)Cols.Procesar);

                if (bProcesar)
                {
                    object[] SubFarmacias = { sIdSubFarmacia };
                    dtSubFarmacias.Rows.Add(SubFarmacias);
                }
            }
            this.Hide();
        }

        #endregion Botones
       
        #region Funciones 

        private void IniciaToolBar(bool Nuevo, bool Guardar)
        {
            btnNuevo.Enabled = Nuevo;
            btnGuardar.Enabled = Guardar;
        }
        public void MostrarDetalle()
        {
            this.ShowDialog();
        }

        private void ObtieneSubFarmacias()
        {
            string sSql = "";

            if (sFarmacia != "")
            {
                sSql = string.Format(
                "Select IdSubFarmacia, Descripcion, Tipo, 0 as Procesar " +
                "From vw_Farmacias_SubFarmacias (NoLock) " +
                "Where IdEstado = '{0}' and IdFarmacia = '{1}' " +
                "Order By IdSubFarmacia ", sEstado, sFarmacia);
            }
            else
            {
                sSql = string.Format(
                "Select Distinct IdSubFarmacia, Descripcion, Tipo, 0 as Procesar " +
                "From vw_Farmacias_SubFarmacias (NoLock) " +
                "Where IdEstado = '{0}' " +
                "Order By IdSubFarmacia ", sEstado );
            }

            Grid.Limpiar(false); 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener las SubFarmacias.");
            }
            else
            {
                if (leer.Leer())
                {
                    Grid.LlenarGrid(leer.DataSetClase); 
                }
                else
                {
                    General.msjUser("No Existen SubFarmacias asignadas a esta Farmacia, verifique.");
                    btnNuevo_Click(this, null);
                }
            }
        }

        private void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 1; i <= Grid.Rows; i++)
            {
                Grid.SetValue(i, (int)Cols.Procesar, chkSeleccionar.Checked);
            }
        }

        private void CrearTabla()
        {
            dtSubFarmacias.Columns.Add("IdSubFarmacia", Type.GetType("System.String"));
            //dtSubFarmacias.Columns.Add("SubFarmacia", Type.GetType("System.String"));
            //dtSubFarmacias.Columns.Add("Procesar", Type.GetType("System.Boolean"));
        }

        private string ObtenerCondicionSubFarmacias()
        {
            string sSubFarmacias = "", sLista = "";

            foreach (DataRow dtRow in dtSubFarmacias.Select())
            {
                sLista = sLista + dtRow.ItemArray[0].ToString() + ", ";
            }

            if (sLista.Trim() != "")
            {
                sLista = sLista.Substring(0, sLista.Length - 2);//Es menos dos para quitarle la ultima coma.
                sSubFarmacias = "And " + sAliasTabla + "IdSubFarmacia In ( " + sLista + " )";
            }

            return sSubFarmacias;
        }

        private string ObtenerListadoSubFarmacias()
        {
            string sLista = "";

            foreach (DataRow dtRow in dtSubFarmacias.Select())
            {
                sLista = sLista + dtRow.ItemArray[0].ToString() + ", ";
            }

            if (sLista.Trim() != "")
            {
                sLista = sLista.Substring(0, sLista.Length - 2);//Es menos dos para quitarle la ultima coma.
            }

            return sLista;
        }
        #endregion Funciones

    }
}
