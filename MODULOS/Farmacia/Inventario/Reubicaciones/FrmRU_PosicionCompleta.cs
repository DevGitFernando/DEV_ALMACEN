using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos; 

namespace Farmacia.Inventario
{
    public partial class FrmRU_PosicionCompleta : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        clsConsultas Consultas;
        clsAyudas Ayudas;

        string sIdEmpresa = DtGeneral.EmpresaConectada; 
        string sIdEstado = DtGeneral.EstadoConectado;
        string sIdFarmacia = DtGeneral.FarmaciaConectada;

        string sPermiso_ReubicarADestino_ConExistencia = "REUBICACIONES_COMPLETAS";
        bool bPermiso_ReubicarADestino_ConExistencia = false;

        string sMensajeConSurtimiento = "Se encontrarón folios de surtimiento pendientes de generar traspasos ó dispersiones.\n\n" +
        "No se puede generar traspaso; Validar estatus de folios de surtimiento.";

        public FrmRU_PosicionCompleta()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name, false);
            Ayudas = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name, false);

            SolicitarPermisosUsuario(); 
        } 

        private void FrmRU_PosicionCompleta_Load(object sender, EventArgs e)
        {
            Limpiar(); 
        }

        #region Permisos de Usuario
        /// <summary>
        /// Obtiene los privilegios para el usuario conectado 
        /// </summary>
        private void SolicitarPermisosUsuario()
        {
            bPermiso_ReubicarADestino_ConExistencia = DtGeneral.PermisosEspeciales.TienePermiso(sPermiso_ReubicarADestino_ConExistencia);

        }
        #endregion Permisos de Usuario

        #region Botones 
        private void Limpiar()
        {
            Fg.IniciaControles();
            txtPasillo.Focus(); 
        }
        
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar(); 
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            if (validaDatos())
            { 
                // Llamar el modulo de Reubicaciones 
                FrmReubicacionProductos f = new FrmReubicacionProductos();
                f.MostrarReubicacion_Posicion(
                    txtPasillo.Text, txtEstante.Text, txtEntrepaño.Text,
                    txtIdPasilloDestino.Text, txtIdEstanteDestino.Text, txtIdEntrepañoDestino.Text);

                if (f.ReubicacionGenerada)
                {
                    Limpiar(); 
                }
            } 
        }
        #endregion Botones 
        
        #region Validaciones 
        private bool validaDatos()
        {
            bool bRegresa = true;
            string sUbicacionOrigen = string.Format("{0}-{1}-{2}", Fg.PonCeros(txtPasillo.Text, 4), Fg.PonCeros(txtEstante.Text, 4), Fg.PonCeros(txtEntrepaño.Text, 4));
            string sUbicacionDestino = string.Format("{0}-{1}-{2}", 
                    Fg.PonCeros(txtIdPasilloDestino.Text, 4), Fg.PonCeros(txtIdEstanteDestino.Text, 4), Fg.PonCeros(txtIdEntrepañoDestino.Text, 4));


            if (sUbicacionOrigen == sUbicacionDestino)
            {
                bRegresa = false;
                General.msjError("La ubicación destino no debe ser igual a la ubicación origen. Favor de verificar."); 
                txtPasillo.Focus(); 
            }

            if (bRegresa)
            {
                bRegresa = EsUbicacionVacia(txtPasillo.Text, txtEstante.Text, txtEntrepaño.Text, 1);
            }

            if (bRegresa)
            {
                bRegresa = EsUbicacionVacia(txtIdPasilloDestino.Text, txtIdEstanteDestino.Text, txtIdEntrepañoDestino.Text, 2);
            }

            return bRegresa; 
        }

        private bool EsUbicacionVacia(string Pasillo, string Estante, string Entrepaño, int Tipo)
        {
            bool bRegresa = false;
            bool bError = false;
            string sMensaje = "";

            string sSql = 
                string.Format("Select cast(sum(Existencia - ExistenciaEnTransito) as int) as Registros From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones (NoLock)  " + 
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' "  +
                " and IdPasillo = '{3}' and IdEstante = '{4}' and IdEntrepaño = '{5}' ", 
                sIdEmpresa, sIdEstado, sIdFarmacia,
                Pasillo, Estante, Entrepaño); 

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                bError = true; 
            }
            else
            {
                if (!leer.Leer())
                {
                    bRegresa = true; 
                }
                else 
                {
                    //bRegresa = leer.CampoInt("Registros") == 0 ? true : false;
                    if (Tipo == 1)
                    {
                        bRegresa = leer.CampoInt("Registros") == 0 ? true : false;
                    }
                    else
                    {
                        bRegresa = leer.CampoInt("Registros") == 0 ? true : false;
                    }
                }
            }

            if (!bError)
            {
                if (Tipo == 1)
                {
                    if (bRegresa)
                    {
                        sMensaje = "Ubicación origen seleccionada esta vacia. Favor de verificar.";
                        General.msjUser(sMensaje);
                    }
                    bRegresa = !bRegresa; 
                }

                if (Tipo == 2)
                {
                    if (!bRegresa)
                    {
                        sMensaje = "Ubicación destino seleccionada no esta vacia. Favor de verificar.";
                        if (!bPermiso_ReubicarADestino_ConExistencia)
                        {
                            General.msjUser(sMensaje);
                        }
                        else
                        {
                            sMensaje = "Ubicación destino seleccionada no esta vacia.\n\n¿ Desea continuar con la reubicación ?";
                            bRegresa = General.msjConfirmar(sMensaje) == System.Windows.Forms.DialogResult.Yes;
                        }
                    }
                }
            }

            return bRegresa; 
        }


        #endregion Validaciones

        #region Funciones 
        private void CargaDatosPasillo(int Tipo)
        {
            if (Tipo == 1)
            {
                //Se hace de esta manera para la ayuda.
                txtPasillo.Text = leer.Campo("IdPasillo");
                lblPasillo.Text = leer.Campo("DescripcionPasillo");
            }
            else
            {
                //Se hace de esta manera para la ayuda.
                txtIdPasilloDestino.Text = leer.Campo("IdPasillo");
                lblPasilloDestino.Text = leer.Campo("DescripcionPasillo");
            }

            if (leer.Campo("Status") == "C")
            {
                General.msjUser("Rack capturado esta cancelado. Favor de verificar.");

                if (Tipo == 1)
                {
                    txtPasillo.Text = "";
                    lblPasillo.Text = "";
                    txtPasillo.Focus();
                }
                else
                {
                    txtIdPasilloDestino.Text = "";
                    lblPasilloDestino.Text = "";
                    txtIdPasilloDestino.Focus();
                }
            }
        }

        private void CargaDatosEstante(int Tipo)
        {
            if (Tipo == 1)
            {
                //Se hace de esta manera para la ayuda.
                txtEstante.Text = leer.Campo("IdEstante");
                lblEstante.Text = leer.Campo("DescripcionEstante");
            }
            else
            {
                //Se hace de esta manera para la ayuda.
                txtIdEstanteDestino.Text = leer.Campo("IdEstante");
                lblEstanteDestino.Text = leer.Campo("DescripcionEstante");
            }

            if (leer.Campo("Status") == "C")
            {
                General.msjUser("Nivel capturado esta cancelado. Favor de verificar.");

                if (Tipo == 1)
                {
                    txtEstante.Text = "";
                    lblEstante.Text = "";
                    txtEstante.Focus();
                }
                else
                {
                    txtIdEstanteDestino.Text = "";
                    lblEstanteDestino.Text = "";
                    txtIdEstanteDestino.Focus();
                }
            }
        }

        private void CargaDatosEntrepaño(int Tipo)
        {
            if (Tipo == 1)
            {
                //Se hace de esta manera para la ayuda.
                txtEntrepaño.Text = leer.Campo("IdEntrepaño");
                lblEntrepaño.Text = leer.Campo("DescripcionEntrepaño");
            }
            else
            {
                //Se hace de esta manera para la ayuda.
                txtIdEntrepañoDestino.Text = leer.Campo("IdEntrepaño");
                lblEntrepañoDestino.Text = leer.Campo("DescripcionEntrepaño");
            }

            if (leer.Campo("Status") == "C")
            {
                General.msjUser("Posición capturada esta cancelada. Favor de verificar.");

                if (Tipo == 1)
                {
                    txtEntrepaño.Text = "";
                    lblEntrepaño.Text = "";
                    txtEntrepaño.Focus();
                }
                else
                {
                    txtIdEntrepañoDestino.Text = "";
                    lblEntrepañoDestino.Text = "";
                    txtIdEntrepañoDestino.Focus();
                }
            }
        }

        #endregion Funciones

        #region Origen
        #region Buscar Pasillo
        private void txtPasillo_Validating(object sender, CancelEventArgs e)
        {
            if (txtPasillo.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Pasillos(sIdEmpresa, sIdEstado, sIdFarmacia, txtPasillo.Text.Trim(), "txtPasillo_Validating");
                if (leer.Leer())
                {
                    CargaDatosPasillo(1);
                }
                else
                {
                    txtPasillo.Text = "";
                    txtPasillo.Focus();
                }
            }
        }

        private void txtPasillo_KeyDown(object sender, KeyEventArgs e)
        {
            string sCadena = "";

            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.Pasillos(sIdEmpresa, sIdEstado, sIdFarmacia, "txtId_KeyDown");
                sCadena = Ayudas.QueryEjecutado.Replace("'", "\"");

                if (leer.Leer())
                {
                    CargaDatosPasillo(1);
                }
            }
        }
        #endregion Buscar Pasillo

        #region Buscar Estante
        private void txtEstante_Validating(object sender, CancelEventArgs e)
        {
            if (txtEstante.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Pasillos_Estantes(sIdEmpresa, sIdEstado, sIdFarmacia, txtPasillo.Text.Trim(), txtEstante.Text.Trim(), "txtPasillo_Validating");
                if (leer.Leer())
                {
                    CargaDatosEstante(1);
                }
                else
                {
                    txtEstante.Text = "";
                    txtEstante.Focus();
                }
            }
        }

        private void txtEstante_KeyDown(object sender, KeyEventArgs e)
        {
            // string sCadena = "";

            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.Estantes(sIdEmpresa, sIdEstado, sIdFarmacia, txtPasillo.Text.Trim(), "txtId_KeyDown"); 
                if (leer.Leer())
                {
                    CargaDatosEstante(1);
                }
            }
        }
        #endregion Buscar Estante

        #region Buscar Entrepaño
        private void txtEntrepaño_Validating(object sender, CancelEventArgs e)
        {
            if (txtEntrepaño.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Pasillos_Estantes_Entrepaños(sIdEmpresa, sIdEstado, sIdFarmacia, txtPasillo.Text.Trim(), txtEstante.Text.Trim(), txtEntrepaño.Text.Trim(), "txtPasillo_Validating");
                if (leer.Leer())
                {
                    CargaDatosEntrepaño(1);
                }
                else
                {
                    txtEntrepaño.Text = "";
                    txtEntrepaño.Focus();
                }

            }
        }

        private void txtEntrepaño_KeyDown(object sender, KeyEventArgs e)
        {
            // string sCadena = "";

            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.Entrepaños(sIdEmpresa, sIdEstado, sIdFarmacia, txtPasillo.Text.Trim(), txtEstante.Text.Trim(), "txtId_KeyDown"); 
                if (leer.Leer())
                {
                    CargaDatosEntrepaño(1);
                }
            }
        }
        #endregion Buscar Entrepaño
        #endregion Origen 

        #region Destino 
        #region Buscar Pasillo
        private void txtIdPasilloDestino_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdPasilloDestino.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Pasillos(sIdEmpresa, sIdEstado, sIdFarmacia, txtIdPasilloDestino.Text.Trim(), "txtIdPasilloDestino_Validating");
                if (leer.Leer())
                {
                    CargaDatosPasillo(2);
                }
                else
                {
                    txtIdPasilloDestino.Text = "";
                    txtIdPasilloDestino.Focus();
                }

            }
        }

        private void txtIdPasilloDestino_KeyDown(object sender, KeyEventArgs e)
        {
            // string sCadena = "";

            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.Pasillos(sIdEmpresa, sIdEstado, sIdFarmacia, "txtId_KeyDown");

                if (leer.Leer())
                {
                    CargaDatosPasillo(2);
                }
            }
        }
        #endregion Buscar Pasillo

        #region Buscar Estante
        private void txtIdEstanteDestino_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdEstanteDestino.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Pasillos_Estantes(sIdEmpresa, sIdEstado, sIdFarmacia, txtIdPasilloDestino.Text.Trim(), txtIdEstanteDestino.Text.Trim(), "txtIdPasilloDestino_Validating");
                if (leer.Leer())
                {
                    CargaDatosEstante(2);
                }
                else
                {
                    txtIdEstanteDestino.Text = "";
                    txtIdEstanteDestino.Focus();
                }

            }
        }

        private void txtIdEstanteDestino_KeyDown(object sender, KeyEventArgs e)
        {
            // string sCadena = "";

            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.Estantes(sIdEmpresa, sIdEstado, sIdFarmacia, txtIdPasilloDestino.Text.Trim(), "txtId_KeyDown");
                if (leer.Leer())
                {
                    CargaDatosEstante(2);
                }
            }
        }
        #endregion Buscar Estante

        #region Buscar Entrepaño
        private void txtIdEntrepañoDestino_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdEstanteDestino.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Pasillos_Estantes_Entrepaños(sIdEmpresa, sIdEstado, sIdFarmacia, 
                    txtIdPasilloDestino.Text.Trim(), txtIdEstanteDestino.Text.Trim(), txtIdEntrepañoDestino.Text.Trim(), "txtIdPasilloDestino_Validating");
                if (leer.Leer())
                {
                    CargaDatosEntrepaño(2);
                }
                else
                {
                    txtIdEstanteDestino.Text = "";
                    txtIdEstanteDestino.Focus();
                }

            }
        }


        private void txtIdEntrepañoDestino_KeyDown(object sender, KeyEventArgs e)
        { 
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.Entrepaños(sIdEmpresa, sIdEstado, sIdFarmacia, txtIdPasilloDestino.Text.Trim(), txtIdEstanteDestino.Text.Trim(), "txtId_KeyDown");
                if (leer.Leer())
                {
                    CargaDatosEntrepaño(2); 
                }
            }
        }
        #endregion Buscar Entrepaño

        private void FrmRU_PosicionCompleta_Shown(object sender, EventArgs e)
        {
            //if (DtGeneral.TieneSurtimientosActivos())
            //{
            //    General.msjAviso(sMensajeConSurtimiento);
            //    this.Close();
            //}
        }
        #endregion Destino 

    }
}
