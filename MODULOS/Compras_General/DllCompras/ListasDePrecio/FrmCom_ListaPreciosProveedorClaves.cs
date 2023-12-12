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
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft; 

namespace DllCompras.ListasDePrecio
{
    public partial class FrmCom_ListaPreciosProveedorClaves : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid grid;

        clsAyudas ayuda = new clsAyudas();
        clsConsultas query;

        clsAuditoria auditoria;

        public FrmCom_ListaPreciosProveedorClaves()
        {
            InitializeComponent();

            // Inicializar las Variables Generales 
            leer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DllCompras.GnCompras.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, GnCompras.Modulo, this.Name, GnCompras.Version);
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            grid = new clsGrid(ref grdListaDePrecios, this);
            grid.EstiloGrid(eModoGrid.ModoRow);
            grid.FrozenColumnas = 4;
            grid.AjustarAnchoColumnasAutomatico = true; 

            auditoria = new clsAuditoria(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                                        DtGeneral.IdPersonal, DtGeneral.IdSesion, GnCompras.Modulo, this.Name, GnCompras.Version);
        }

        private void FrmCom_ListaPreciosProveedorClaves_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }        

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            grid.Limpiar(false);
            txtId.Focus();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            CargarDatosClavesSAL();
        }
        #endregion Botones        

        #region Eventos
        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            string sCadena = "";

            if (txtId.Text != "")
            {
                string sSql = String.Format(" SELECT * FROM dbo.vw_Proveedores ( NOLOCK ) " +
                                            " WHERE IdProveedor = '{0}'", Fg.PonCeros(txtId.Text, 4));

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "txtId_Validating");
                    General.msjError("Error al buscar el Proveedor");
                }
                else 
                {
                    if (!leer.Leer())
                    {
                        General.msjUser("Clave de Proveedor no encontrada, verifique.");
                        txtId.Focus();
                    }
                    else 
                    {
                        // Se llenan los datos del Proveedor.
                        txtId.Text = leer.Campo("IdProveedor");
                        lblProveedor.Text = leer.Campo("Nombre");
                        HabilitarCampos(false);
                    }

                    sCadena = sSql.Replace("'", "\"");
                    auditoria.GuardarAud_MovtosUni("*", sCadena);
                }
            }
            else
            {
                txtId.Focus();
            }
        }
        #endregion Eventos

        #region Funciones
        private void HabilitarCampos(bool bTipo)
        {
            txtId.Enabled = bTipo;
        }
        private void CargarDatosClavesSAL()
        {
            string sCadena = "";

            string sSql = 
                string.Format(" Select ClaveSSA, DescripcionClave, CodigoEAN, StatusPrecioAux, Precio, " +
                    " Descuento, TasaIva, Iva, Importe, FechaRegistro, FechaFinVigencia  " +
                    " From vw_COM_OCEN_ListaDePreciosClaves (NoLock) " +
                    " Where IdProveedor = '{0}'" + 
                    " Order By DescripcionClave " , txtId.Text);

            if (leer.Exec(sSql))
            {
                if (leer.Leer())
                {
                    grid.LlenarGrid(leer.DataSetClase);
                }
                else
                {
                    General.msjUser("El proveedor no tiene Claves resgistradas.");
                    HabilitarCampos(true);
                    txtId.Focus();
                }

                sCadena = sSql.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);
            }
            else
            {
                Error.GrabarError(leer, "CargarDatosClavesSAL");
                General.msjError("Error al buscar las ClavesSAL");
            }
        }
        #endregion Funciones
    }
}
