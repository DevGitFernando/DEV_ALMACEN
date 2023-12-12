using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Windows.Forms;

using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
// using SC_SolutionsSystem.Criptografia;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

using DllFarmaciaSoft;
using DllTransferenciaSoft;
using DllTransferenciaSoft.EnviarInformacion;
using DllTransferenciaSoft.Zip;
using DllTransferenciaSoft.ObtenerInformacion; 

namespace Dll_SII_INadro.ObtenerInformacion
{
    public class clsCliente_INadro : DllTransferenciaSoft.ObtenerInformacion.clsCliente 
    {
        #region Constructor y Destructor
        public clsCliente_INadro(string ArchivoCfg, clsDatosConexion DatosConexion):base(ArchivoCfg, DatosConexion) 
        {
        }
        #endregion Constructor y Destructor

        #region Funciones y Procedimientos Publicos
        public void Abrir_Directorio_PedidosUnidades()
        {
            General.AbrirDirectorio(sRutaObtencion);
        }
        #endregion Funciones y Procedimientos Publicos  

        #region Pedidos para Unidades   [ CEDIS ==> FARMACIA ] 
        public void Pedidos_Unidades(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioPedido)
        {
            bEsProcesoDePedidos = true;
            sRutaPedidosCedis = @"\\PEDIDOS_UNIDADES\\";

            sIdEmpresaPedido = IdEmpresa;
            sIdEstadoPedido = IdEstado;
            sIdFarmaciaPedido = IdFarmacia;
            sIdFarmaciaEnvio = IdFarmacia;
            sFolioPedido = FolioPedido; 

            if (GetRutaObtencion_PedidosUnidades()) 
            {
                Generar_Pedidos_Unidades(IdEmpresa, IdEstado, IdFarmacia, FolioPedido);
            }

            bEsProcesoDePedidos = false;
        }

        private bool Generar_Pedidos_Unidades(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioPedido)
        {
            bool bRegresa = false;
            // string sFile = "";
            string sTabla = "";
            int iOrden = 0;

            iTipoDeTablasEnvio = 2;
            ExistenTablasEnvioPedidos();
            if (leerCat.Registros > 0)
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    while (leerCat.Leer())
                    {
                        sTabla = leerCat.Campo("NombreTabla");
                        iOrden = leerCat.CampoInt("IdOrden");
                        bRegresa = GenerarTablaPedidos(sTabla, iOrden);
                        if (!bRegresa)
                        {
                            break;
                        }
                    }

                    if (bRegresa)
                    {
                        cnn.CompletarTransaccion();
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                    }

                    cnn.Cerrar();
                }
            }

            iTipoDeTablasEnvio = 3;
            if (bExistenArchivos)
            {
                Empacar(); // DestinoArchivos.Farmacia_A_Farmacia
                GenerarArchivosClientes(DestinoArchivos.Almacen_A_Farmacia);
            }


            iTipoDeTablasEnvio = 1;
            return bRegresa;
        }

        private bool ExistenTablasEnvioPedidos()
        {
            bool bRegresa = true;
            string sSql = "Select * From INT_ND_RP_EnvioPedidosCedis (NoLock) Where Status = 'A' Order By IdOrden, NombreTabla ";

            // leerCat = new clsLeer(ref cnn);
            if (!leerCat.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leerCat, "ExistenTablasEnvioPedidos()");
            }

            return bRegresa;
        }

        protected bool GenerarTablaPedidos(string Tabla, int Orden)
        {
            ////  
            string sFile = base.GeneraNombreArchivoTabla(Tabla, Orden);
            bool bExito = true;

            if (base.ExisteTabla_A_Procesar(Tabla))
            {
                if (PreparaDatosTablaPedidos(Tabla, Datos.Obtener))
                {
                    if (CrearArchivoPedidos(Tabla, sFile))
                    {
                        bExito = PreparaDatosTablaPedidos(Tabla, Datos.Procesado);
                    }
                    else
                    {
                        bExito = false;
                    }
                }
                else
                {
                    bExito = false;
                }
            }
            return bExito;
        }

        private bool PreparaDatosTablaPedidos(string Tabla, Datos Efecto)
        {
            // Prepara los datos de la tabla seleccionada para solo copiar los datos necesarios
            bool bRegresa = true;
            string sSql = "";
            string sEfecto = "0";
            string sWhere = "0";
            string sWherePedido = "";

            if (Efecto == Datos.Obtener)
            {
                sEfecto = "2";
            }
            else if (Efecto == Datos.Procesado)
            {
                sEfecto = "1";
                sWhere = "2";
            }

            sWherePedido = string.Format(", [ IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioPedido = '{3}' ] ",
                                            sIdEmpresaPedido, sIdEstadoPedido, sIdFarmaciaPedido, sFolioPedido);

            //sSql = string.Format(" Exec spp_CFG_PrepararDatos '{0}', '{1}', '{2}' ", Tabla, sEfecto, sWhere);
            sSql = string.Format(" Exec spp_CFG_PrepararDatos '{0}', '{1}', '{2}' {3} ", Tabla, sEfecto, sWhere, sWherePedido);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "PreparaDatosTablaPedidos");
            }

            return bRegresa;
        }

        private bool CrearArchivoPedidos(string Tabla, string Nombre)
        {
            bool bRegresa = true;
            int iReg = 0, iVueltas = 0; // , iRegistros = 0;
            int iPaquete = 0;
            string sArchivoDestSql = sRutaObtencion + "" + Nombre + Fg.PonCeros(iPaquete, 4) + ".sql";
            StreamWriter f = null;

            if (ObtenerDatosTablaPedidos(Tabla))
            {
                try
                {
                    // File.Delete(sArchivoDestSql);
                    if (leerExec.Registros > 0)
                    {
                        bRegresa = true;
                        bExistenArchivos = true;
                        while (leerExec.Leer())
                        {
                            if (iVueltas == 0)
                            {
                                iVueltas++;
                                iPaquete++;
                                sArchivoDestSql = sRutaObtencion + "" + Nombre + "_" + Fg.PonCeros(iPaquete, 4) + ".sql";
                                File.Delete(sArchivoDestSql);
                                f = new StreamWriter(sArchivoDestSql);
                            }

                            f.WriteLine(leerExec.Campo(1));
                            iReg++;

                            // Agregar el separador de Registros 
                            if (iReg >= Transferencia.RegistrosSQL)
                            {
                                f.WriteLine(Transferencia.SQL);
                                f.WriteLine("");
                                iReg = 0;
                                iVueltas++;
                            }

                            // Generar archivos de 200 Registros ==> 300-450 Kb 
                            if (iVueltas >= 5)
                            {
                                // Cerrar el archivo con los Bloques Completos 
                                f.WriteLine(Transferencia.SQL);
                                f.WriteLine("");
                                f.Close();
                                iVueltas = 0;
                            }
                        }

                        if (iVueltas != 0)
                        {
                            // Cerrar el archivo en caso de no completar los bloques de Registros 
                            f.WriteLine(Transferencia.SQL);
                            f.WriteLine("");
                            f.Close();
                        }
                    }
                }
                catch //( Exception ex )
                {
                    //General.msjError(ex.Message);
                    bRegresa = false;
                }
            }

            return bRegresa;
        }

        private bool ObtenerDatosTablaPedidos(string Tabla)
        {
            bool bRegresa = true;

            // Asegurar que los Clientes envien la informacion al Servidor Regional para Reenvio a Servidor Central 
            string sSql = string.Format(" Exec spp_CFG_ObtenerDatos '{0}', [ Where Actualizado = 2 ], '0' ", Tabla);

            sSql = string.Format(" Exec spp_CFG_ObtenerDatos '{0}', [  Where IdEmpresa = '{1}' and IdEstado = '{2}' and IdFarmacia = '{3}' " +
                                " and FolioPedido = '{4}' ], '0' ", Tabla, sIdEmpresaPedido, sIdEstadoPedido, sIdFarmaciaPedido, sFolioPedido);


            if (!leerExec.Exec(Tabla, sSql))
            {
                bRegresa = false;
                Error.GrabarError(leerExec, "ObtenerDatosTablaPedidos()");
            }
            return bRegresa;
        }

        private bool GetRutaObtencion_PedidosUnidades()
        {
            bool bRegresa = true;
            string sSql = "Select * From CFGC_ConfigurarObtencion (NoLock) ";

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "GetRutaObtencion()");
                bRegresa = false;
            }
            else
            {
                if (!leer.Leer())
                {
                    bRegresa = false;
                    General.msjError("No se encontro la información de configuración de envio de información, reportarlo al departamento de sistemas.");
                }
                else
                {
                    sRutaObtencion = Application.StartupPath + @"\\" + sRutaPedidosCedis;
                    sRutaEnviados = sRutaObtencion; // leer.Campo("RutaUbicacionArchivosEnviados") + @"\\";

                    //// Revisar que existen las Rutas 
                    if (!Directory.Exists(sRutaObtencion))
                    {
                        Directory.CreateDirectory(sRutaObtencion);
                    }

                    if (!Directory.Exists(sRutaEnviados))
                    {
                        Directory.CreateDirectory(sRutaEnviados);
                    }
                }
            }
            return bRegresa;
        }
        #endregion Pedidos para Unidades   [ CEDIS ==> FARMACIA ]

        #region Respuesta para CEDIS    [ FARMACIA ==> CEDIS ]
        #endregion Respuesta para CEDIS    [ FARMACIA ==> CEDIS ]

    }
}
