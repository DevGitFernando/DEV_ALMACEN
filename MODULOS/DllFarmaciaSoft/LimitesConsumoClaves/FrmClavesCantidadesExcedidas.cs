using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_ControlsCS;
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;

namespace DllFarmaciaSoft.LimitesConsumoClaves
{
    public partial class FrmClavesCantidadesExcedidas : FrmBaseExt  
    {       

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;       

        DataSet dtsProductosDiferencias;
        clsListView lst;

        string sTabla = "";
        public bool ErrorAlValidarSalida = false;


        bool bRevision_a_Nivel_Farmacia = false;
        int ibRevision_a_Nivel_Farmacia = 0; 
        string sIdCliente = "";
        string sIdSubCliente = ""; 
        string sIdPrograma = "";
        string sIdSubPrograma = "";
        string sTipoDispensacion = "";

        public FrmClavesCantidadesExcedidas(bool Revision_a_Nivel_Farmacia, string TipoDispensacion)
        {
            InitializeComponent();

            bRevision_a_Nivel_Farmacia = Revision_a_Nivel_Farmacia;
            ibRevision_a_Nivel_Farmacia = bRevision_a_Nivel_Farmacia ? 1 : 0;
            sTipoDispensacion = TipoDispensacion; 

            lst = new clsListView(lstClaves);
            lst.OrdenarColumnas = true;
            lst.PermitirAjusteDeColumnas = true;


            if (bRevision_a_Nivel_Farmacia)
            {
                this.Text = "Se detectaron Claves con consumo excedido de acuerdo a programación mensual.";
            }
            else
            {
                this.Text = "Se detectaron Claves con consumo excedido de acuerdo a programación mensual por programa de atención.";
            }

        }

        private void FrmProductosConDiferencias_Load(object sender, EventArgs e)
        {
            
        }

        public bool VerificarCantidadesConExceso(DataSet Lotes, string IdCliente, string IdSubCliente, string IdPrograma, string IdSubPrograma )
        {
            sIdCliente = IdCliente;
            sIdSubCliente = IdSubCliente; 
            sIdPrograma = IdPrograma;
            sIdSubPrograma = IdSubPrograma;

            return VerificarCantidadesConExceso(Lotes, false);
        }

        public bool VerificarCantidadesConExceso(DataSet Lotes, bool MostrarMsj)
        {
            bool bRegresa = false;
            ErrorAlValidarSalida = false;

            cnn = new clsConexionSQL(General.DatosConexion);
            leer = new clsLeer(ref cnn);
            dtsProductosDiferencias = Lotes;            

            if (GenerarTabla())
            {
                bRegresa = VerificarDatos();
            }

            ////// Mostrar los Lotes con Errores 
            if (bRegresa && ErrorAlValidarSalida)
            {
                bRegresa = false;
                this.ShowDialog();
            }
            

            return bRegresa;
        }

        #region Funciones y Procedimientos Privados
        private bool GenerarTabla()
        {
            bool bRegresa = true;
            string sSql = string.Format(" Select Top 0 IdEmpresa, IdEstado, IdFarmacia, " +
                " cast('' as varchar(6)) As IdClaveSSA, cast('' as varchar(50)) As ClaveSSA, cast('' as varchar(max)) As Descripcion,  " +
                " IdProducto, CodigoEAN, Existencia as Cantidad " +
                " Into {0} From FarmaciaProductos_CodigoEAN ", GetTabla()); 

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "GenerarTabla()");
                General.msjError("Ocurrió un error al Revisar la lista de productos.");
            }
            else
            {
                bRegresa = CargarDatos();
            }

            return bRegresa;
        }

        private bool VerificarDatos()
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_ValidaCantidadClavesSubPerfil " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdCliente = '{3}', @IdSubCliente = '{4}', " +
                " @IdPrograma = '{5}', @IdSubPrograma = '{6}', @Tabla = '{7}', @TipoProceso = '{8}', @TipoDeRecetaAtendida = '{9}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                sIdCliente, sIdSubCliente, sIdPrograma, sIdSubPrograma, sTabla, ibRevision_a_Nivel_Farmacia, sTipoDispensacion); 
            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "VerificarDatos()");
                General.msjError("Ocurrió un error al Revisar la lista de Claves.");
            }
            else
            {
                if (leer.Leer())
                {
                    ErrorAlValidarSalida = true;
                    lst.CargarDatos(leer.DataSetClase, true, true);
                    lst.AnchoColumna(1, 120);
                    lst.AnchoColumna(2, 320);
                    lst.AnchoColumna(3, 90);
                    lst.AnchoColumna(4, 90);
                    lst.AnchoColumna(5, 90);
                    lst.AnchoColumna(6, 120);
                }
            }

            EliminarTablaProceso();

            return bRegresa;
        }

        private bool CargarDatos()
        {
            bool bRegresa = true;
            string sSql = "";
            clsLeer leerLotes = new clsLeer();


            leerLotes.DataSetClase = dtsProductosDiferencias;
            while (leerLotes.Leer())
            {
                sSql = string.Format("Insert Into {0} ( IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, Cantidad ) " +
                    "Select '{1}', '{2}', '{3}', '{4}', '{5}', '{6}'  " +
                    "", sTabla,
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                    leerLotes.Campo("IdProducto"), leerLotes.Campo("CodigoEAN"),
                    leerLotes.Campo("Cantidad"));

                if (leerLotes.CampoInt("Cantidad") > 0)
                {
                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }

            return bRegresa;
        }

        private string MarcaTiempo()
        {
            string sRegresa = "";
            DateTime dt = DateTime.Now;

            sRegresa += Fg.PonCeros(dt.Year, 4);
            sRegresa += Fg.PonCeros(dt.Month, 2);
            sRegresa += Fg.PonCeros(dt.Day, 2);
            sRegresa += "_";
            sRegresa += Fg.PonCeros(dt.Hour, 2);
            sRegresa += Fg.PonCeros(dt.Minute, 2);
            sRegresa += Fg.PonCeros(dt.Second, 2);

            return sRegresa;
        }

        private string GetTabla()
        {
            string sRegresa = "tmpCantClavesSubPerfil_" + General.MacAddress + "_" + MarcaTiempo();
            sTabla = sRegresa;
            return sRegresa;
        }

        private void EliminarTablaProceso()
        {
            string sSql = string.Format("If Exists ( Select Name From sysobjects (noLock) Where name = '{0}' and xType = 'U' )   Drop Table {0} ", sTabla);

            if (!DtGeneral.EsEquipoDeDesarrollo)
            {
                leer.Exec(sSql);
            }
        }
        #endregion Funciones y Procedimientos Privados
    }
}
