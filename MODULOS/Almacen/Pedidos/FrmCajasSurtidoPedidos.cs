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
using SC_SolutionsSystem.Reportes;

using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft; 

namespace Almacen.Pedidos
{
    public partial class FrmCajasSurtidoPedidos : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer, leerGuardar;

        clsConsultas Consultas;
        clsAyudas Ayudas;

        clsListView lst;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        string sFolioPedido = "";
        string sFolioSurtido = "";
        string sMensaje = "";

        public FrmCajasSurtidoPedidos()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            leerGuardar = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);

            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            Ayudas = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);

            lst = new clsListView(lstCajas);
            lst.PermitirAjusteDeColumnas = false;
            lst.AnchoColumna(1, 90);
            lst.AnchoColumna(2, 105);
        }

        private void FrmCajasSurtidoPedidos_Load(object sender, EventArgs e)
        {
            IniciaPantalla();
        }

        #region Funciones
        private void IniciaPantalla()
        {
            Fg.IniciaControles(this, true);

            lst.LimpiarItems();
            CargarDetallesCajas();

            txtIdCaja.Focus();
        }

        private void CargarDetallesCajas()
        {
            string sSql = "";

            sSql = string.Format(" Select Convert(int, IdCaja) as Caja, IdCaja From Pedidos_Cedis_Cajas_Surtido_Distribucion (Nolock) " +
                                 " Where IdEmpresa = '{0}' and IdEstado = '{1}' " + 
                                 " and IdFarmacia = '{2}' and FolioPedido = '{3}' and FolioSurtido = '{4}' Order By IdCaja ",
                                 sEmpresa, sEstado, sFarmacia, sFolioPedido, sFolioSurtido);

            lst.LimpiarItems();
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarDetallesCajas()");
                General.msjError("Ocurrió un error al obtener los detalles de cajas..");
            }
            else
            {
                if (leer.Leer())
                {
                    lst.CargarDatos(leer.DataSetClase);
                    txtIdCaja.Text = "";
                    txtIdCaja.Focus();
                }
            }

            lst.AnchoColumna(1, 90);
            lst.AnchoColumna(2, 105);
        }
        #endregion Funciones

        #region Funciones_Publicas
        public void CargaPantalla(string FolioPedido, string FolioSurtido)
        {
            this.sFolioPedido = FolioPedido;
            this.sFolioSurtido = FolioSurtido;

            this.ShowDialog();
        }
        #endregion Funciones_Publicas

        #region Eventos
        private void txtIdCaja_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdCaja.Text.Trim() != "")
            {
                if (ValidarCajas())
                {
                    if (Guardar())
                    {
                        CargarDetallesCajas();
                        txtIdCaja.Focus();
                    }
                }
                else
                {
                    txtIdCaja.Focus();
                }
            }
        }
        #endregion Eventos

        #region Validaciones
        private bool ValidarCajas()
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format(" Select * From CatCajasDistribucion (Nolock) " +
                                 " Where IdEstado = '{0}' and IdFarmacia = '{1}' and Convert(int, IdCaja) = {2} ",
                                 sEstado, sFarmacia, txtIdCaja.Text);
                        
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ValidarCajas()");
                General.msjError("Ocurrió un error al validar la caja..");
                bRegresa = false;
            }
            else
            {
                if (leer.Leer())
                {
                    if (!leer.CampoBool("Disponible"))
                    {
                        General.msjAviso("La caja no esta disponible");
                        bRegresa = false;
                    }

                    if (!leer.CampoBool("Habilitada"))
                    {
                        General.msjAviso("La caja no esta Habilitada");
                        bRegresa = false;
                    }
                }
                else
                {
                    General.msjAviso("El numero de caja no esta asignado a la unidad. Verifique..!!");
                    bRegresa = false;
                }
            }

            return bRegresa;
        }
        #endregion Validaciones

        #region Asignar_Cajas
        private bool GuardarInformacionCajas(int iOpcion)
        {
            bool bRegresa = true;
            string sSql = "", sIdCaja = "";

            sIdCaja = txtIdCaja.Text;

            if (iOpcion == 1)
            {
                sIdCaja = lst.GetValue(1);
            }

            sSql = string.Format(" Exec spp_Mtto_Pedidos_Cedis_Cajas_Surtido_Distribucion '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}' ",
                                 sEmpresa, sEstado, sFarmacia, sFolioPedido, sFolioSurtido, sIdCaja, iOpcion);

            if (!leerGuardar.Exec(sSql))
            {                
                bRegresa = false;
            }
            else
            {
                if (leerGuardar.Leer())
                {
                    sMensaje = leerGuardar.Campo("Mensaje");
                }                
            }

            return bRegresa;
        }

        private bool Guardar()
        {
            bool bRegresa = true;

            if (!cnn.Abrir())
            {
                General.msjErrorAlAbrirConexion();
            }
            else
            {
                cnn.IniciarTransaccion();

                bRegresa = GuardarInformacionCajas(0);

                if (bRegresa) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                {
                    cnn.CompletarTransaccion();                        
                    General.msjUser(sMensaje);                           
                }
                else
                {
                    Error.GrabarError(leer, "Guardar()");
                    cnn.DeshacerTransaccion();
                    General.msjError("Ocurrió un error al guardar la información.");
                }

                cnn.Cerrar();
            }

            return bRegresa;
        }
        #endregion Asignar_Cajas

        #region Liberar_Cajas
        private void btnLiberarCaja_Click(object sender, EventArgs e)
        {
            if (lst.GetValue(1).Trim() != "")
            {
                if (LiberaCajas())
                {
                    CargarDetallesCajas();
                    txtIdCaja.Focus();
                }
            }
        }

        private bool LiberaCajas()
        {
            bool bRegresa = true;

            if (!cnn.Abrir())
            {
                General.msjErrorAlAbrirConexion();
            }
            else
            {
                cnn.IniciarTransaccion();

                bRegresa = GuardarInformacionCajas(1);

                if (bRegresa) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                {
                    cnn.CompletarTransaccion();
                    General.msjUser(sMensaje);
                }
                else
                {
                    Error.GrabarError(leer, "LiberaCajas()");
                    cnn.DeshacerTransaccion();
                    General.msjError("Ocurrió un error al guardar la información.");
                }

                cnn.Cerrar();
            }

            return bRegresa;
        }
        #endregion Liberar_Cajas
    }
}
