using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

namespace DllFarmaciaSoft.TitulosValidacion
{
    public partial class FrmTitulosEncabezadosListado : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas Consultas;
        clsAyudas Ayuda;

        clsListView lst;

        string sIdCliente = "";
        string sIdSubCliente = "";
        string sIdPrograma = "";
        string sIdSubPrograma = ""; 

        public FrmTitulosEncabezadosListado()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            lst = new clsListView(lstTitulos); 
        }

        private void FrmTitulosEncabezadosListado_Load(object sender, EventArgs e)
        {
            CargarEstados(); 
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(); 
            lst.LimpiarItems();

            lstTitulos.ContextMenuStrip = null; 
        }        

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            CargarListadoDeEncabezados(); 
        }

        private void CargarListadoDeEncabezados() 
        {
            string sSql = string.Format(
                "Select IdCliente + ' - ' + Cliente, ClienteTitulo, " +
                "IdSubCliente + ' - ' + SubCliente, SubClienteTitulo, " +
                "IdPrograma + ' - ' + Programa, ProgramaTitulo, " +
                "IdSubPrograma + ' - ' + SubPrograma, SubProgramaTitulo, Status, " + 
                "IdEstado, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma  " +
                "From vw_CFG_EX_Validacion_Titulos (NoLock) " + 
                "Where IdEstado = '{0}' ", cboEstados.Data); 

            lst.LimpiarItems();
            lstTitulos.ContextMenuStrip = null; 
            if ( !leer.Exec(sSql))
            {
                Error.GrabarError(leer, "btnEjecutar_Click");
                General.msjError("Ocurrió un error al obtener la información solicita."); 
            }
            else
            {
                lstTitulos.ContextMenuStrip = menuTitulos; 
                if (!leer.Leer())
                {
                    General.msjUser("No se encontro información de encabezados para el estado seleccionado."); 
                }
                else
                {
                    cboEstados.Enabled = false; 
                    lst.CargarDatos(leer.DataSetClase, true, false); 
                }
            }
        }
        #endregion Botones

        #region Funciones
        private void CargarEstados()
        {
            cboEstados.Clear();
            cboEstados.Add();

            leer.DataSetClase = Consultas.EstadosConFarmacias("CargarEstados");

            if (leer.Leer())
            {
                cboEstados.Add(leer.DataSetClase, true, "IdEstado", "NombreEstado");
            }

            cboEstados.SelectedIndex = 0;
        }
        #endregion Funciones 

        #region Informacion 
        private void MostrarEncabezados(int Tipo)
        {
            FrmTitulosEncabezados f = new FrmTitulosEncabezados(this, cboEstados.Data, Tipo);

            if (Tipo == 1)
            {
                f.MostrarPantalla(); 
            }

            if (Tipo == 2)
            {
                sIdCliente = lst.LeerItem().Campo("IdCliente");
                sIdSubCliente = lst.LeerItem().Campo("IdSubCliente");
                sIdPrograma = lst.LeerItem().Campo("IdPrograma");
                sIdSubPrograma = lst.LeerItem().Campo("IdSubPrograma");

                f.MostrarPantalla(sIdCliente, sIdSubCliente, sIdPrograma, sIdSubPrograma); 
            }

            if (f.SeModificoInformacion)
            {
                CargarListadoDeEncabezados(); 
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            MostrarEncabezados(1); 
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            MostrarEncabezados(2); 
        }
        #endregion Informacion
    }
}
