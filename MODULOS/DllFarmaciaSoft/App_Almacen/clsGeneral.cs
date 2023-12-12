using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DllFarmaciaSoft.App_Almacen
{
    public class clsGeneral
    {
        clsLeer leer, leerGuardado;
        clsConexionSQL conn;
        clsGrabarError Error;
        int iCantidadAsignada;
        public bool VerificaFolios(string sEmpresa, string sEstado, string sFarmacia, string sFolioPedido, string sFolioSurtido)
        {
            bool bRegresa = false;
            //clsLeerWeb Query; // = new clsLeerWeb(General.Url, General.DatosConexion, new SC_SolutionsSystem.Data.clsDatosCliente("DllFarmaciaSoft", this.Name, "FrmEdoLogin_Load"));
            //clsDatosCliente datosCliente;
            clsLeer Query;

            string sSql = "";
            DataSet dtsDatosDeSesion = new DataSet();
            DataSet dtsAux = new DataSet();

            clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            Query = new clsLeer(ref cnn);

            try
            {
                    sSql = string.Format("Exec spp_ValidacionFolios '{0}', '{1}', '{2}', '{3}', '{4}' ", sEmpresa, sEstado, sFarmacia, sFolioPedido, sFolioSurtido);

                    if (!Query.Exec(sSql))
                    {
                        General.Error.GrabarError("Error en el vw_CFG_Empresas " + Query.Error, General.DatosConexion, "", Application.ProductVersion, "Login", "Login", Query.QueryEjecutado);
                    }
                    else
                    {
                        DataSet dts = new DataSet();
                        dts.Tables.Add(Query.DataTableClase);

                        foreach(DataRow row in dts.Tables[0].Rows)
                        {
                            if (row["Resultado"].ToString() == "S")
                            {
                                bRegresa = true;
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                string m = ex.Message;
                General.Error.GrabarError("Error de inicio " + General.DatosConexion.CadenaConexion, General.DatosConexion, "", Application.ProductVersion, "General", "General", "");
            }

            return bRegresa;
        }

        public DataSet ObtenerDatos(string sEmpresa, string sEstado, string sFarmacia, string sFolioSurtido, int iPedidoManual, int iEsValidacion)
        {
            string sSql = "";
            DataSet dts = new DataSet();
            clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            clsLeer Query = new clsLeer(ref cnn);

            try
            {
                sSql = string.Format("Exec spp_Mtto_Pedidos_Cedis_Cargar__OrdenDeSurtido \n" +
                                    "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioSurtido = '{3}', @EsManual = '{4}', @EsValidacion = '{5}' ",
                                    sEmpresa, sEstado, sFarmacia, sFolioSurtido, iPedidoManual, iEsValidacion);

                if (!Query.Exec(sSql))
                {
                    General.Error.GrabarError("Error " + Query.Error, General.DatosConexion, "", Application.ProductVersion, "Login", "Login", Query.QueryEjecutado);
                }
                else
                {
                    Query.RenombrarTabla(0,"Items");
                    dts.Tables.Add(Query.DataTableClase);
                }
            }
            catch (Exception ex)
            {
                string m = ex.Message;
                General.Error.GrabarError("Error de inicio " + General.DatosConexion.CadenaConexion, General.DatosConexion, "", Application.ProductVersion, "General", "General", "");
            }

            return dts;
        }

        public bool EnvioInformacion(string sEmpresa, string sEstado, string sFarmacia, string sFolioSurtido, string sIdPersonalSurtido, DataSet dts)
        {
            bool bResult = false;
            conn = new clsConexionSQL(General.DatosConexion);
            leer = new clsLeer(ref conn);
            leerGuardado = new clsLeer(ref conn);

            leerGuardado.DataSetClase = dts;

            sIdPersonalSurtido = GetPersonalS(sIdPersonalSurtido);

            if (Grabar_Personal_Surtido(sEmpresa, sEstado,sFarmacia,sFolioSurtido,sIdPersonalSurtido))
            {
                if (GuardarSurtimiento(sEmpresa, sEstado, sFarmacia, sFolioSurtido, sIdPersonalSurtido))
                {
                    bResult = Graba_Atencion_Surtido(sEmpresa, sEstado, sFarmacia, sFolioSurtido, sIdPersonalSurtido);
                }
            }

            return bResult;
        }

        private bool Grabar_Personal_Surtido(string sEmpresa, string sEstado, string sFarmacia, string sFolioSurtido, string sIdPersonalSurtido)
        {
            bool bRegresa = false;

            string sSql = string.Format(" Update Pedidos_Cedis_Enc_Surtido Set IdPersonalSurtido = '{4}' " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioSurtido = '{3}' ",
                sEmpresa, sEstado, sFarmacia, sFolioSurtido, sIdPersonalSurtido);

            bRegresa = leer.Exec(sSql);

            return bRegresa;
        }

        private bool GuardarSurtimiento(string sEmpresa, string sEstado, string sFarmacia, string sFolioSurtido, string sIdPersonalSurtido)
        {
            bool bRegresa = false;

            if (!conn.Abrir())
            {
                    General.msjErrorAlAbrirConexion();
            }
            else
            {
                    conn.IniciarTransaccion();

                    if (GuardaInformacion(sEmpresa, sEstado, sFarmacia, sFolioSurtido, sIdPersonalSurtido))
                    {
                        bRegresa = true;

                        bRegresa = ActualizarStatusSurtimiento(sEmpresa, sEstado, sFarmacia, sFolioSurtido);
                    }

                    if (bRegresa) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                    {
                        conn.CompletarTransaccion();
                    }
                    else
                    {
                        Error.GrabarError(leer, "btnGuardar_Click");
                        conn.DeshacerTransaccion();
                    }

                    conn.Cerrar();
            }

            return bRegresa;
        }

        private bool GuardaInformacion(string sEmpresa, string sEstado, string sFarmacia, string sFolioSurtido, string sIdPersonalSurtido)
        {
            bool bRegresa = true;
            string sSql = "";


            sSql = string.Format("Update Pedidos_Cedis_Det_Surtido_Distribucion Set CantidadAsignada = 0 " +
                                 "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioSurtido = '{3}'",
                                 sEmpresa, sEstado, sFarmacia, sFolioSurtido);

            bRegresa = leer.Exec(sSql);

            while (leerGuardado.Leer() && bRegresa)
            {
                iCantidadAsignada = leerGuardado.CampoInt("CantidadAsignada");

                if (iCantidadAsignada > 0)
                {

                    int iSurtimiento = leerGuardado.CampoInt("IdSurtimiento");
                    string sClaveSSA = leerGuardado.Campo("ClaveSSA");
                    string sSubFarmacia = leerGuardado.Campo("IdSubFarmacia");
                    string sProducto = leerGuardado.Campo("IdProducto");
                    string sCodigoEAN = leerGuardado.Campo("CodigoEAN");
                    string sLote = leerGuardado.Campo("ClaveLote");
                    int iPasillo = leerGuardado.CampoInt("IdPasillo");
                    int iEstante = leerGuardado.CampoInt("IdEstante");
                    int iEntrepaño = leerGuardado.CampoInt("IdEntrepaño");
                    string sObservaciones = leerGuardado.Campo("Observaciones");
                    int iCaja = leerGuardado.CampoInt("Caja");
                    int iValidado = leerGuardado.CampoInt("Validado");

                    sSql = string.Format(" Exec spp_Actualiza_Pedidos_Cedis_Det_Surtido_Distribucion " +
                        " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioSurtido = '{3}', @IdSurtimiento = '{4}', @ClaveSSA = '{5}', " +
                        " @IdSubFarmacia = '{6}', @IdProducto = '{7}', @CodigoEAN = '{8}', @ClaveLote = '{9}', " +
                        " @IdPasillo = '{10}', @IdEstante = '{11}', @IdEntrepaño = '{12}', @CantidadAsignada = '{13}', @Observaciones = '{14}', " +
                        " @sStatus = '{15}', @IdCaja = '{16}', @Validado = '{17}' ",
                    sEmpresa, sEstado, sFarmacia, sFolioSurtido, iSurtimiento, sClaveSSA, sSubFarmacia,
                    sProducto, sCodigoEAN, sLote, iPasillo, iEstante, iEntrepaño, iCantidadAsignada, sObservaciones, 'A', iCaja, iValidado);

                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        Error.GrabarError(leer, "btnGuardar_Click");
                    }
                }
            }

            return bRegresa;
        }

        private bool Graba_Atencion_Surtido(string sEmpresa, string sEstado, string sFarmacia, string sFolioSurtido, string sIdPersonalSurtido)
        {
            bool bRegresa = true;

            if (!conn.Abrir())
            {
                //General.msjErrorAlAbrirConexion();
                Error.GrabarError(leer, "Graba_Atencion_Surtido");
            }
            else
            {
                conn.IniciarTransaccion();
                string sSql = string.Format("Exec spp_Mtto_Pedidos_Cedis_Enc_Surtido_Atenciones '{0}', '{1}', '{2}', '{3}', '{4}', '{5}'",
                    sEmpresa, sEstado, sFarmacia, sFolioSurtido, sIdPersonalSurtido, "");

                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    Error.GrabarError(leer, "btnGuardar_Click");
                }

                if (bRegresa) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                {
                    conn.CompletarTransaccion();
                }
                else
                {
                    Error.GrabarError(leer, "btnGuardar_Click");
                    conn.DeshacerTransaccion();
                    //General.msjError("Ocurrió un error al guardar la información de Atencion Surtido.");
                }

                conn.Cerrar();
            }

            return bRegresa;
        }

        private bool ActualizarStatusSurtimiento(string sEmpresa, string sEstado, string sFarmacia, string sFolioSurtido)
        {
            bool bRegresa = true;
            string sSql = "", sStatus = "";
            string sModificaciones = "";

            sModificaciones = " , ModificacionesCaptura = (case when Status = 'A' then ModificacionesCaptura else ModificacionesCaptura + 1 end)";

            sStatus = "S";
            sSql = string.Format(" Update Pedidos_Cedis_Enc_Surtido Set Status = '{0}' {1}  " +
                                    " Where IdEmpresa = '{2}' and IdEstado = '{3}' and IdFarmacia = '{4}' and FolioSurtido = '{5}' ",
                                    sStatus, sModificaciones, sEmpresa, sEstado, sFarmacia, sFolioSurtido);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "btnGuardar_Click");
            }

            return bRegresa;
        }
    
        private string GetPersonalS(string sIdPersonalSurtido)
        {
            string sReturn;
            
            string sSql = string.Format("Select Top(1) * from CatPersonalCEDIS Where IdPersonal_Relacionado = '{0}' ",
                     sIdPersonalSurtido);

            bool  bRegresa = leer.Exec(sSql);

            if(bRegresa)
            {
                leer.Leer();
                sReturn = leer.Campo("IdPersonal");
            }
            else
            {
                sReturn = "";
                Error.GrabarError(leer, "btnGuardar_Click");
            }

            return sReturn;
        }
    }
}
