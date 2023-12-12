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
    public partial class FrmCom_ListaPrecioClaves : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid grid;

        clsAyudas ayuda = new clsAyudas();
        clsConsultas query;

        clsAuditoria auditoria;

        // Esta variable se utiliza para saber si la manda llamar el listado de claves ofertadas
        bool bExterna = false;

        public FrmCom_ListaPrecioClaves()
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

            auditoria = new clsAuditoria( General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, 
                                        DtGeneral.IdPersonal, DtGeneral.IdSesion, GnCompras.Modulo, this.Name, GnCompras.Version);
            
        }

        private void FrmCom_ListaPrecioClaves_Load(object sender, EventArgs e)
        {
            if (!bExterna)
            {
                btnNuevo_Click(null, null);
            }
            else
            {
                btnNuevo.Enabled = false;       
            }
        }

        #region Funciones

        private void HabilitarCampos(bool bTipo)
        {
            txtId.Enabled = bTipo;            
        }

        private void CargarDatosProveedores()
        {
            string sCadena = "";

            string sSql = 
                string.Format(" Select IdProveedor, Nombre, CodigoEAN, StatusPrecioAux, Precio, " +
                    " Descuento, TasaIva, Iva, Importe, FechaRegistro, FechaFinVigencia " +  
                    " From vw_COM_OCEN_ListaDePreciosClaves (NoLock) " + 
                    " Where IdClaveSSA = '{0}'" + 
                    " Order by Precio Desc, IdProveedor ", Fg.PonCeros(txtId.Text, 4));

            if (leer.Exec(sSql))
            {
                if (leer.Leer())
                {
                    grid.LlenarGrid(leer.DataSetClase);
                }
                else
                {
                    General.msjUser("No existen Proveedores asignados para la Clave.");
                    HabilitarCampos(true);
                    txtId.Focus();
                }

                sCadena = sSql.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);
            }
            else
            {
                Error.GrabarError(leer, "CargarDatosProveedores");
                General.msjError("Error al buscar los Proveedores");
            }
        }

        public void MostrarClaveProveedores(string IdClaveSSA)
        {
            txtId.Text = IdClaveSSA;
            txtId_Validating(null, null);
            CargarDatosProveedores();
            bExterna = true;
            this.ShowDialog();      
        }

        #endregion Funciones

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            grid.Limpiar(false);
            txtId.Focus();
        }
        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            CargarDatosProveedores();
        }
        #endregion Botones

        #region Eventos
        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            string sCadena = "";
            if (txtId.Text != "")
            {
                string sSql = String.Format(" SELECT * FROM dbo.vw_ClavesSSA_Sales ( NOLOCK ) " +
                                            " WHERE ClaveSSA = '{0}'", txtId.Text);

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "txtId_Validating");
                    General.msjError("Error al buscar la Clave");
                }
                else 
                {
                    if (!leer.Leer())
                    {
                        General.msjUser("Clave interna no encontrada, verifique.");
                        txtId.Focus();
                    }
                    else 
                    {
                        // Se llenan los datos de la clave interna.
                        txtId.Text = leer.Campo("ClaveSSA");
                        lblClaveSSA.Text = leer.Campo("IdClaveSSA_Sal");
                        lblDes.Text = leer.Campo("DescripcionSal");
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

        
    }
}
