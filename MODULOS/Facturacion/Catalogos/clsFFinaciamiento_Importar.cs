using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.SQL;
using SC_SolutionsSystem.OfficeOpenXml;
using SC_SolutionsSystem.OfficeOpenXml.Data;

using DllFarmaciaSoft;
using Dll_IFacturacion;

namespace Facturacion.Catalogos
{
    class clsFFinaciamiento_Importar
    {
        //enum FuentesDeFinanciamiento_Configuracion
        //{
        //    Ninguna = 0,

        //    Insumo = 1,
        //    Servicio,
        //    Documentos,

        //    Insumo_Clave_Farmacia,
        //    Servicio_Clave_Farmacia,

        //    Insumo_Clave_Jurisdiccion,
        //    Servicio_Clave_Jurisdiccion,

        //    ExcepcionPrecios_Insumos,
        //    ExcepcionPrecios_Servicio
        //}

        clsConexionSQL cnn;
        clsLeer leer;
        public clsLeer LeerValidacionFinal;
        clsGrabarError Error;

        string sIdEmpresa = "";
        string sIdEstado = "";
        string sIdFarmacia = "";
        string sMensaje = "";
        DataSet dtsParametros;

        public int iRegistrosHoja = 0;
        public int iRegistrosProcesados = 0;

        string sTablaTrabajo = "";
        string sStoredProcedure = "";
        public clsLeerExcelOpenOficce excel;
        public string sHoja = "";

        public bool bErrorAlGuardar = false;
        public bool bErrorAlValidar = false;
        public bool bErrorAlProcesar = false;
        public bool bActivarProceso = false;
        public bool bValidandoInformacion = false;
        public bool bGuardandoInformacion = false;
        public bool bProcesandoInformacion = false;

        clsBulkCopy bulk;

        FuentesDeFinanciamiento_Configuracion FFInanciamiento_Configuracion;

        public clsFFinaciamiento_Importar(clsDatosConexion DatosConexion, clsDatosApp DatosApp)
        {
            this.cnn = new clsConexionSQL(DatosConexion);
            this.leer = new clsLeer(ref cnn);
            this.Error = new clsGrabarError(DatosConexion, DatosApp, "clsFFinaciamiento_Importar");

            bulk = new clsBulkCopy(General.DatosConexion);
        }


        public string Tipo
        {
            get { return GetConfiguracion(FFInanciamiento_Configuracion); }
            set { FFInanciamiento_Configuracion = (FuentesDeFinanciamiento_Configuracion)Enum.Parse(typeof(FuentesDeFinanciamiento_Configuracion), value); }
        }

        private void Configuracion_FFinanciamiento()
        {
            switch (FFInanciamiento_Configuracion)
            {
                case FuentesDeFinanciamiento_Configuracion.Insumo:
                    sTablaTrabajo = "FACT_Fuentes_De_Financiamiento__CargaMasiva";
                    sStoredProcedure = "";
                    break;
                case FuentesDeFinanciamiento_Configuracion.Servicio:
                    sTablaTrabajo = "FACT_Fuentes_De_Financiamiento__CargaMasiva";
                    sStoredProcedure = "";
                    break;
                case FuentesDeFinanciamiento_Configuracion.Documentos:
                    sTablaTrabajo = "FACT_Fuentes_De_Financiamiento_Detalles_Documentos__CargaMasiva";
                    sStoredProcedure = "";
                    break;
                case FuentesDeFinanciamiento_Configuracion.Insumo_Clave_Farmacia:
                    sTablaTrabajo = "FACT_Fuentes_De_Financiamiento__CargaMasiva";
                    sStoredProcedure = "";
                    break;
                case FuentesDeFinanciamiento_Configuracion.Servicio_Clave_Farmacia:
                    sTablaTrabajo = "FACT_Fuentes_De_Financiamiento__CargaMasiva";
                    sStoredProcedure = "";
                    break;
                case FuentesDeFinanciamiento_Configuracion.BeneficiariosJurisdiccion:
                    sTablaTrabajo = "FACT_Fuentes_De_Financiamiento__CargaMasiva";
                    sStoredProcedure = "";
                    break;
                case FuentesDeFinanciamiento_Configuracion.BeneficiariosRelacionados_Jurisdiccion:
                    sTablaTrabajo = "FACT_Fuentes_De_Financiamiento__CargaMasiva";
                    sStoredProcedure = "";
                    break;        
                case FuentesDeFinanciamiento_Configuracion.Insumo_Clave_Jurisdiccion:
                    sTablaTrabajo = "FACT_Fuentes_De_Financiamiento__CargaMasiva";
                    sStoredProcedure = "";
                    break;
                case FuentesDeFinanciamiento_Configuracion.Servicio_Clave_Jurisdiccion:
                    sTablaTrabajo = "FACT_Fuentes_De_Financiamiento__CargaMasiva";
                    sStoredProcedure = "";
                    break;
                case FuentesDeFinanciamiento_Configuracion.ExcepcionPrecios_Insumos:
                    sTablaTrabajo = "FACT_Fuentes_De_Financiamiento__CargaMasiva";
                    sStoredProcedure = "";
                    break;
                case FuentesDeFinanciamiento_Configuracion.ExcepcionPrecios_Servicio:
                    sTablaTrabajo = "FACT_Fuentes_De_Financiamiento__CargaMasiva";
                    sStoredProcedure = "";
                    break;
                case FuentesDeFinanciamiento_Configuracion.Grupos_De_Remisiones:
                    sTablaTrabajo = "FACT_Fuentes_De_Financiamiento__CargaMasiva";
                    sStoredProcedure = "";
                    break;

                    
            }
        }

        public void GuardarInformacion()
        {
            bool bRegresa = false;
            string sSql = "";
            int iTipo = 1;
            clsLeer leerGuardar = new clsLeer(ref cnn);


            bGuardandoInformacion = true;

            Configuracion_FFinanciamiento();

            excel.RegistroActual = 1;
            bRegresa = excel.Registros > 0;

            //iTipo = rdoTransferencia.Checked ? 1 : 0;

            

            bulk.NotifyAfter = 500;
            bulk.RowsCopied += new clsBulkCopy.RowsCopiedEventHandler(bulk_RowsCopied);
            bulk.OnCompled += new clsBulkCopy.RowsCopiedEventHandler(bulk_Compled);
            bulk.OnError += new clsBulkCopy.RowsCopiedEventHandler(bulk_Error);

            bulk.ClearColumns();
            bulk.DestinationTableName = sTablaTrabajo;

            //lblProcesados.Text = string.Format("Agregando información de control...");
            //// Agregar columnas 

            AgregarColumnas();


            //// Agregar columnas 


            //// Asignacion de Columnas 
            //lblProcesados.Text = string.Format("Procesando   {0}   de   {1}", iRegistrosProcesados.ToString(sFormato), iRegistrosHoja.ToString(sFormato));
            AsignarColumnasBulk();


            leerGuardar.Exec(string.Format("Delete From {0}", sTablaTrabajo));
            bRegresa = bulk.WriteToServer(excel.DataSetClase); //, System.Data.SqlClient.SqlBulkCopyOptions.Default); 

            //BloqueaHojas(false);
            //MostrarEnProceso(false, 3);

            if (!bRegresa)
            {
                Error.GrabarError(bulk.Error, "GuardarInformacion()");
                //General.msjError("Ocurrió un error al cargar la información de los pedidos.");
                bErrorAlGuardar = true;

                //IniciaToolBar(true, false, false, false, false, false, true);
            }
            else
            {
                leerGuardar.Exec(string.Format("Exec spp_FormatearTabla '{0}'  ", sTablaTrabajo));
                //General.msjUser("Información de Pedidos cargada satisfactoriamente.");
                bErrorAlGuardar = false;
                //IniciaToolBar(true, false, false, false, true, false, true);
            }

            bGuardandoInformacion = false;
        }

        public void ValidarInformacion()
        {
            int iTipo;
            ////tmValidacion.Enabled = true;
            ////tmValidacion.Interval = 1000;
            ////tmValidacion.Start(); 

            bValidandoInformacion = true;
            //bActivarProceso = false;
            bErrorAlValidar = false;
            clsLeer leerValidacion = new clsLeer();
            clsLeer leerRows = new clsLeer();
            leer = new clsLeer(ref cnn);
            LeerValidacionFinal = new clsLeer();
            DataSet dtsResultados = new DataSet();

            //IniciaToolBar(false, false, false, false, false, bActivarProceso, false);
            //BloqueaHojas(true);
            //MostrarEnProceso(true, 4);
            //lblProcesados.Visible = false;

            //iTipo = rdoVenta.Checked ? 1 : 2;
            //iTipo = (int)tpTipoDePedido;

            string sSql = string.Format("Exec sp_Proceso_FACT_Fuentes_De_Financiamiento__CargaMasiva_000_Validar_Datos_De_Entrada @Tipo = '{0}'", GetConfiguracion(FFInanciamiento_Configuracion));


            if (!leer.Exec(sSql))
            {
                bErrorAlValidar = true;
                bActivarProceso = !bActivarProceso;

                Error.GrabarError(leer, "ValidarInformacion()");
                
            }
            else
            {
                leer.RenombrarTabla(1, "Resultados");

                leerValidacion.DataTableClase = leer.Tabla("Resultados");

                dtsResultados = leer.DataSetClase;
                dtsResultados.Tables.Remove("Resultados");
                leer.DataSetClase = dtsResultados;

                for (int i = 1; leerValidacion.Leer(); i++)
                {
                    leer.RenombrarTabla(i, leerValidacion.Campo("Descripcion"));

                    leerRows.DataTableClase = leer.Tabla(leerValidacion.Campo("Descripcion"));

                    if (!bActivarProceso)
                        bActivarProceso = leerRows.Registros > 0 ? true : false;

                }

            }

            LeerValidacionFinal.DataSetClase = leer.DataSetClase.Copy();

            bValidandoInformacion = false;
            bActivarProceso = !bActivarProceso;
            //BloqueaHojas(false);
            //MostrarEnProceso(false, 4);
        }

        public void IntegrarPedidos()
        {
            bool bContinua = true;
            bErrorAlProcesar = false;
            string sSql = "";

            bProcesandoInformacion = true;


            clsLeer leer = new clsLeer(ref cnn);
            leer.Conexion.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;

            if (bContinua)
            {
                if (!cnn.Abrir())
                {
                    Error.LogError(cnn.MensajeError);
                    General.msjErrorAlAbrirConexion();
                }
                else
                {
                    cnn.IniciarTransaccion();

                    //BloqueaHojas(true);
                    //MostrarEnProceso(true, 5);
                    //IniciaToolBar(false, false, false, false, false, false, false);

                    sSql = string.Format(" Exec sp_Proceso_FACT_Fuentes_De_Financiamiento__CargaMasiva_001_Integrar @Tipo = '{0}'", GetConfiguracion(FFInanciamiento_Configuracion));

                    if (!leer.Exec(sSql))
                    {
                        bContinua = false;
                        bErrorAlProcesar = true;
                        //General.msjError("Ocurrió un Error al Procesar las Remisiones");
                        //Error.GrabarError(leer, "ProcesarRemisiones");
                    }

                    //BloqueaHojas(false);
                    //MostrarEnProceso(false, 5);

                    if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                    {
                        cnn.CompletarTransaccion();
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnProcesar_Click");
                        //IniciaToolBar(true, true, true, true, true, true, true);
                    }

                    cnn.Cerrar();
                }
            }

            bProcesandoInformacion = false;
        }

        private void AsignarColumnasBulk()
        {
            switch (FFInanciamiento_Configuracion)
            {
                case FuentesDeFinanciamiento_Configuracion.Insumo:
                    AsignarColumnasBulk_Productos();
                    break;
                case FuentesDeFinanciamiento_Configuracion.Servicio:
                    AsignarColumnasBulk_Productos();
                    AsignarColumnasBulk_Servicio();
                    break;
                case FuentesDeFinanciamiento_Configuracion.Documentos:
                    AsignarColumnasBulk_Documentos();
                    break;
                case FuentesDeFinanciamiento_Configuracion.Insumo_Clave_Farmacia:
                    AsignarColumnasBulk_Productos();
                    AsignarColumnasBulk_ClavesSSA__Farmacia();
                    break;
                case FuentesDeFinanciamiento_Configuracion.Servicio_Clave_Farmacia:
                    AsignarColumnasBulk_Productos();
                    AsignarColumnasBulk_ClavesSSA__Farmacia();
                    break;
                case FuentesDeFinanciamiento_Configuracion.BeneficiariosJurisdiccion:
                    AsignarColumnasBulk_BeneficiariosJurisdiccion();
                    break;
                case FuentesDeFinanciamiento_Configuracion.BeneficiariosRelacionados_Jurisdiccion:
                    AsignarColumnasBulk_BeneficiariosJurisdiccion();
                    break;
                case FuentesDeFinanciamiento_Configuracion.Insumo_Clave_Jurisdiccion:
                    AsignarColumnasBulk_Productos();
                    AsignarColumnasBulk_Clave_Jurisdiccion();
                    break;
                case FuentesDeFinanciamiento_Configuracion.Servicio_Clave_Jurisdiccion:
                    AsignarColumnasBulk_Productos();
                    AsignarColumnasBulk_Clave_Jurisdiccion();
                    break;
                case FuentesDeFinanciamiento_Configuracion.ExcepcionPrecios_Insumos:
                    AsignarColumnasBulk_Clave_ExcepcionPrecios();
                    break;
                case FuentesDeFinanciamiento_Configuracion.ExcepcionPrecios_Servicio:
                    AsignarColumnasBulk_Clave_ExcepcionPrecios();
                    break;
                case FuentesDeFinanciamiento_Configuracion.Grupos_De_Remisiones:
                    AsignarColumnasBulk_Clave_Grupos_De_Remisiones();
                    break;
            }
            
        }

        private void AsignarColumnasBulk_Productos()
        {
            bulk.AddColumn("IdFuente Financiamiento", "IdFuenteFinanciamiento");
            bulk.AddColumn("Id Financiamiento", "IdFinanciamiento");
            bulk.AddColumn("ClaveSSA", "ClaveSSA");
            bulk.AddColumn("Porcentaje participacion", "PorcParticipacion");
            bulk.AddColumn("Cantidad PresupuestadaPiezas", "CantidadPresupuestadaPiezas");
            bulk.AddColumn("Cantidad Presupuestada", "CantidadPresupuestada");
            bulk.AddColumn("CantidadAsignada", "CantidadAsignada");
            bulk.AddColumn("Cantidad Restante", "CantidadRestante");
            bulk.AddColumn("Status", "Status");
            bulk.AddColumn("Actualizado", "Actualizado");
            bulk.AddColumn("SAT_ClaveProducto_Servicio", "SAT_ClaveProducto_Servicio");
            bulk.AddColumn("SAT_UnidadDeMedida", "SAT_UnidadDeMedida");
            //bulk.AddColumn("Referencia_04", "Referencia_04");
            bulk.AddColumn("CostoBase", "CostoBase");
            bulk.AddColumn("Porcentaje_01", "Porcentaje_01");
            bulk.AddColumn("Porcentaje_02", "Porcentaje_02");
        }

        private void AsignarColumnasBulk_Servicio()
        {
            bulk.AddColumn("Costo", "Costo");
            bulk.AddColumn("Agrupacion", "Agrupacion");
            bulk.AddColumn("CostoUnitario", "CostoUnitario");
            bulk.AddColumn("TasaIva", "TasaIva");
            bulk.AddColumn("Iva", "Iva");
            bulk.AddColumn("ImporteNeto", "ImporteNeto");
            //bulk.AddColumn("Agrupacion", "Agrupacion");    
        }

        private void AsignarColumnasBulk_Documentos()
        {
            bulk.AddColumn("IdFuente Financiamiento", "IdFuenteFinanciamiento");
            bulk.AddColumn("Id Financiamiento", "IdFinanciamiento");
            bulk.AddColumn("IdDocumento", "IdDocumento");
            bulk.AddColumn("NombreDocumento", "NombreDocumento");
            bulk.AddColumn("IdFuenteFinanciamiento_Relacionado", "IdFuenteFinanciamiento_Relacionado");
            bulk.AddColumn("IdFinanciamiento_Relacionado", "IdFinanciamiento_Relacionado");
            bulk.AddColumn("IdDocumento_Relacionado", "IdDocumento_Relacionado");
            bulk.AddColumn("EsRelacionado", "EsRelacionado");
            bulk.AddColumn("OrigenDeInsumo", "OrigenDeInsumo");
            bulk.AddColumn("TipoDeDocumento", "TipoDeDocumento");
            bulk.AddColumn("TipoDeInsumo", "TipoDeInsumo");
            bulk.AddColumn("ValorNominal", "ValorNominal");
            bulk.AddColumn("ImporteAplicado", "ImporteAplicado");
            bulk.AddColumn("ImporteAplicado_Aux", "ImporteAplicado_Aux");
            bulk.AddColumn("ImporteRestante", "ImporteRestante");
            bulk.AddColumn("AplicaFarmacia", "AplicaFarmacia");
            bulk.AddColumn("AplicaAlmacen", "AplicaAlmacen");
            bulk.AddColumn("EsProgramaEspecial", "EsProgramaEspecial");
            bulk.AddColumn("TipoDeBeneficiario", "TipoDeBeneficiario");
            bulk.AddColumn("Status", "Status");
        }

        private void AsignarColumnasBulk_ClavesSSA__Farmacia()
        {
            bulk.AddColumn("IdEstado", "IdEstado");
            bulk.AddColumn("IdFarmacia", "IdFarmacia");
            bulk.AddColumn("Referencia 01", "Referencia_01");
            bulk.AddColumn("Referencia 02", "Referencia_02");
            bulk.AddColumn("Referencia 03", "Referencia_03");
            bulk.AddColumn("Referencia 05", "Referencia_05");
        }

        private void AsignarColumnasBulk_Clave_Jurisdiccion()
        {
            bulk.AddColumn("IdEstado", "IdEstado");
            bulk.AddColumn("IdFarmacia", "IdFarmacia");
            bulk.AddColumn("IdCliente", "IdCliente");
            bulk.AddColumn("IdSubCliente", "IdSubCliente");
            bulk.AddColumn("IdBeneficiario", "IdBeneficiario");
            bulk.AddColumn("Referencia 01", "Referencia_01");
            bulk.AddColumn("Referencia 02", "Referencia_02");
            bulk.AddColumn("Referencia 03", "Referencia_03");
            bulk.AddColumn("Referencia 05", "Referencia_05");
        }

        private void AsignarColumnasBulk_Clave_ExcepcionPrecios()
        {
            bulk.AddColumn("IdFuente Financiamiento", "IdFuenteFinanciamiento");
            bulk.AddColumn("Id Financiamiento", "IdFinanciamiento");
            bulk.AddColumn("Referencia 01", "Referencia_01");
            bulk.AddColumn("Referencia 02", "Referencia_02");
            bulk.AddColumn("Referencia 03", "Referencia_03");
            bulk.AddColumn("Referencia 05", "Referencia_05");
            bulk.AddColumn("ClaveSSA", "ClaveSSA");

            bulk.AddColumn("Tipo", "Tipo");
            bulk.AddColumn("PrecioBase", "PrecioBase");
            bulk.AddColumn("Incremento", "Incremento");
            bulk.AddColumn("PorcentajeIncremento", "PorcentajeIncremento");
            bulk.AddColumn("PrecioFinal", "PrecioFinal");
        }


        private void AsignarColumnasBulk_BeneficiariosJurisdiccion()
        {
            bulk.AddColumn("IdFuente Financiamiento", "IdFuenteFinanciamiento");
            bulk.AddColumn("IdBeneficiario", "IdBeneficiario");
            bulk.AddColumn("Status", "Status");
            bulk.AddColumn("Actualizado", "Actualizado");

            bulk.AddColumn("NombreBeneficiario", "NombreBeneficiario");

            bulk.AddColumn("IdEstado", "IdEstado");
            bulk.AddColumn("IdFarmacia", "IdFarmacia");
            bulk.AddColumn("IdCliente", "IdCliente");
            bulk.AddColumn("IdSubCliente", "IdSubCliente");
            bulk.AddColumn("IdBeneficiario_Relacionado", "IdBeneficiario_Relacionado");
        }

        private void AsignarColumnasBulk_Clave_Grupos_De_Remisiones()
        {
            bulk.AddColumn("IdFuente Financiamiento", "IdFuenteFinanciamiento");
            bulk.AddColumn("Id Financiamiento", "IdFinanciamiento");
            bulk.AddColumn("Referencia 01", "Referencia_01");
            bulk.AddColumn("IdGrupo", "IdGrupo");
            bulk.AddColumn("ClaveSSA", "ClaveSSA");
            bulk.AddColumn("TipoRemision", "TipoRemision");
            bulk.AddColumn("FechaVigencia", "FechaVigencia");
            bulk.AddColumn("Status", "Status");
        }


        private void AgregarColumnas()
        {
            switch (FFInanciamiento_Configuracion)
            {
                case FuentesDeFinanciamiento_Configuracion.Insumo:
                    AgregarColumnasProductos();
                    break;
                case FuentesDeFinanciamiento_Configuracion.Servicio:
                    AgregarColumnasProductos();
                    AgregarColumnasServicio();
                    break;
                case FuentesDeFinanciamiento_Configuracion.Documentos:
                    AgregarColumnasDocumentos();
                    break;
                case FuentesDeFinanciamiento_Configuracion.Insumo_Clave_Farmacia:
                    AgregarColumnasProductos();
                    break;
                case FuentesDeFinanciamiento_Configuracion.Servicio_Clave_Farmacia:
                    AgregarColumnasProductos();
                    break;
                case FuentesDeFinanciamiento_Configuracion.BeneficiariosJurisdiccion:
                    AgregarColumnasBeneficiariosJurisdiccion();
                    break;
                case FuentesDeFinanciamiento_Configuracion.BeneficiariosRelacionados_Jurisdiccion:
                    AgregarColumnasBeneficiariosJurisdiccion();
                    break;
                case FuentesDeFinanciamiento_Configuracion.Insumo_Clave_Jurisdiccion:
                    AgregarColumnasProductos();
                    AgregarColumnasClave_Jurisdiccion();
                    break;
                case FuentesDeFinanciamiento_Configuracion.Servicio_Clave_Jurisdiccion:
                    AgregarColumnasProductos();
                    AgregarColumnasClave_Jurisdiccion();
                    break;
                case FuentesDeFinanciamiento_Configuracion.ExcepcionPrecios_Insumos:
                    AgregarColumnasClave_ExcepcionPrecios();
                    break;
                case FuentesDeFinanciamiento_Configuracion.ExcepcionPrecios_Servicio:
                    AgregarColumnasClave_ExcepcionPrecios();
                    break;
                case FuentesDeFinanciamiento_Configuracion.Grupos_De_Remisiones:
                    break;
            }
        }

        private void AgregarColumnasProductos()
        {
            //// Formatear columnas 
            if(excel.ExisteTablaColumna(1, "Porcentaje participacion"))
            {
                excel.DataSetClase = TipoColumna(excel.DataSetClase, sHoja, "Porcentaje participacion", TypeCode.Double);
            }

            if(excel.ExisteTablaColumna(1, "Cantidad PresupuestadaPiezas"))
            {
                excel.DataSetClase = TipoColumna(excel.DataSetClase, sHoja, "Cantidad PresupuestadaPiezas", TypeCode.Double);
            }

            if(excel.ExisteTablaColumna(1, "Cantidad Presupuestada"))
            {
                excel.DataSetClase = TipoColumna(excel.DataSetClase, sHoja, "Cantidad Presupuestada", TypeCode.Double);
            }

            if(excel.ExisteTablaColumna(1, "CantidadAsignada"))
            {
                excel.DataSetClase = TipoColumna(excel.DataSetClase, sHoja, "CantidadAsignada", TypeCode.Double);
            }

            if(excel.ExisteTablaColumna(1, "Cantidad Restante"))
            {
                excel.DataSetClase = TipoColumna(excel.DataSetClase, sHoja, "Cantidad Restante", TypeCode.Double);
            }

            if(excel.ExisteTablaColumna(1, "Actualizado"))
            {
                excel.DataSetClase = TipoColumna(excel.DataSetClase, sHoja, "Actualizado", TypeCode.Int32);
            }
            //// Formatear columnas 



            //// Agregar columnas faltantes 
            if(!excel.ExisteTablaColumna(1, "SAT_ClaveProducto_Servicio"))
            {
                excel.DataSetClase = AgregarColumna(excel.DataSetClase, sHoja, "SAT_ClaveProducto_Servicio", "String", "");
            }

            if (!excel.ExisteTablaColumna(1, "SAT_UnidadDeMedida"))
            {
                excel.DataSetClase = AgregarColumna(excel.DataSetClase, sHoja, "SAT_UnidadDeMedida", "String", "");
            }

            if (!excel.ExisteTablaColumna(1, "CostoBase"))
            {
                excel.DataSetClase = AgregarColumna(excel.DataSetClase, sHoja, "CostoBase", "Double", "0.0000");
            }

            if (!excel.ExisteTablaColumna(1, "Porcentaje_01"))
            {
                excel.DataSetClase = AgregarColumna(excel.DataSetClase, sHoja, "Porcentaje_01", "Double", "0.0000");
            }

            if (!excel.ExisteTablaColumna(1, "Porcentaje_02"))
            {
                excel.DataSetClase = AgregarColumna(excel.DataSetClase, sHoja, "Porcentaje_02", "Double", "0.0000");
            }

            if (!excel.ExisteTablaColumna(1, "Referencia 04"))
            {
                excel.DataSetClase = AgregarColumna(excel.DataSetClase, sHoja, "Referencia 04", "String", "");
            }

            if (!excel.ExisteTablaColumna(1, "Referencia 01"))
            {
                excel.DataSetClase = AgregarColumna(excel.DataSetClase, sHoja, "Referencia 01", "String", "");
            }

            if (!excel.ExisteTablaColumna(1, "Referencia 05"))
            {
                excel.DataSetClase = AgregarColumna(excel.DataSetClase, sHoja, "Referencia 05", "String", "");
            }
            //// Agregar columnas faltantes 
        }

        private void AgregarColumnasServicio()
        {
            //// Formatear columnas 
            if(excel.ExisteTablaColumna(1, "Porcentaje participacion"))
            {
                excel.DataSetClase = TipoColumna(excel.DataSetClase, sHoja, "Porcentaje participacion", TypeCode.Double);
            }

            if(excel.ExisteTablaColumna(1, "Cantidad PresupuestadaPiezas"))
            {
                excel.DataSetClase = TipoColumna(excel.DataSetClase, sHoja, "Cantidad PresupuestadaPiezas", TypeCode.Double);
            }

            if(excel.ExisteTablaColumna(1, "Cantidad Presupuestada"))
            {
                excel.DataSetClase = TipoColumna(excel.DataSetClase, sHoja, "Cantidad Presupuestada", TypeCode.Double);
            }

            if(excel.ExisteTablaColumna(1, "CantidadAsignada"))
            {
                excel.DataSetClase = TipoColumna(excel.DataSetClase, sHoja, "CantidadAsignada", TypeCode.Double);
            }

            if(excel.ExisteTablaColumna(1, "Cantidad Restante"))
            {
                excel.DataSetClase = TipoColumna(excel.DataSetClase, sHoja, "Cantidad Restante", TypeCode.Double);
            }

            if(excel.ExisteTablaColumna(1, "Costo"))
            {
                excel.DataSetClase = TipoColumna(excel.DataSetClase, sHoja, "Costo", TypeCode.Double);
            }

            if(excel.ExisteTablaColumna(1, "Agrupacion"))
            {
                excel.DataSetClase = TipoColumna(excel.DataSetClase, sHoja, "Agrupacion", TypeCode.Double);
            }

            if(excel.ExisteTablaColumna(1, "CostoUnitario"))
            {
                excel.DataSetClase = TipoColumna(excel.DataSetClase, sHoja, "CostoUnitario", TypeCode.Double);
            }

            if(excel.ExisteTablaColumna(1, "TasaIva"))
            {
                excel.DataSetClase = TipoColumna(excel.DataSetClase, sHoja, "TasaIva", TypeCode.Double);
            }

            if(excel.ExisteTablaColumna(1, "Iva"))
            {
                excel.DataSetClase = TipoColumna(excel.DataSetClase, sHoja, "Iva", TypeCode.Double);
            }

            if(excel.ExisteTablaColumna(1, "ImporteNeto"))
            {
                excel.DataSetClase = TipoColumna(excel.DataSetClase, sHoja, "ImporteNeto", TypeCode.Double);
            }

            if(excel.ExisteTablaColumna(1, "Actualizado"))
            {
                excel.DataSetClase = TipoColumna(excel.DataSetClase, sHoja, "Actualizado", TypeCode.Int32);
            }
            // CostoUnitario	TasaIva	Iva	ImporteNeto
            //// Formatear columnas 


            //// Agregar columnas faltantes 
            if(!excel.ExisteTablaColumna(1, "PorcParticipacion"))
            {
                excel.DataSetClase = AgregarColumna(excel.DataSetClase, sHoja, "PorcParticipacion", "Double", "0.0000");
            }

            if (!excel.ExisteTablaColumna(1, "Costo"))
            {
                excel.DataSetClase = AgregarColumna(excel.DataSetClase, sHoja, "Costo", "Double", "0.0000");
            }

            if (!excel.ExisteTablaColumna(1, "Agrupacion"))
            {
                excel.DataSetClase = AgregarColumna(excel.DataSetClase, sHoja, "Agrupacion", "Double", "0.0000");
            }

            if (!excel.ExisteTablaColumna(1, "CostoUnitario"))
            {
                excel.DataSetClase = AgregarColumna(excel.DataSetClase, sHoja, "CostoUnitario", "Double", "0.0000");
            }

            if (!excel.ExisteTablaColumna(1, "TasaIva"))
            {
                excel.DataSetClase = AgregarColumna(excel.DataSetClase, sHoja, "TasaIva", "Double", "0.0000");
            }

            if (!excel.ExisteTablaColumna(1, "Iva"))
            {
                excel.DataSetClase = AgregarColumna(excel.DataSetClase, sHoja, "Iva", "Double", "0.0000");
            }

            if (!excel.ExisteTablaColumna(1, "ImporteNeto"))
            {
                excel.DataSetClase = AgregarColumna(excel.DataSetClase, sHoja, "ImporteNeto", "Double", "0.0000");
            }
            //// Agregar columnas faltantes 
        }

        private void AgregarColumnasDocumentos()
        {
            if (excel.ExisteTablaColumna(1, "EsRelacionado"))
            {
                excel.DataSetClase = TipoColumna(excel.DataSetClase, sHoja, "EsRelacionado", TypeCode.Boolean);
            }

            if (excel.ExisteTablaColumna(1, "AplicaFarmacia"))
            {
                excel.DataSetClase = TipoColumna(excel.DataSetClase, sHoja, "AplicaFarmacia", TypeCode.Boolean);
            }

            if (excel.ExisteTablaColumna(1, "AplicaAlmacen"))
            {
                excel.DataSetClase = TipoColumna(excel.DataSetClase, sHoja, "AplicaAlmacen", TypeCode.Boolean);
            }

            if (excel.ExisteTablaColumna(1, "EsProgramaEspecial"))
            {
                excel.DataSetClase = TipoColumna(excel.DataSetClase, sHoja, "EsProgramaEspecial", TypeCode.Boolean);
            }

        }

        private void AgregarColumnasBeneficiarios()
        {
            if (excel.ExisteTablaColumna(1, "NombreBeneficiario"))
            {
                excel.DataSetClase = TipoColumna(excel.DataSetClase, sHoja, "NombreBeneficiario", TypeCode.String);
            }

            if (excel.ExisteTablaColumna(1, "IdEstado"))
            {
                excel.DataSetClase = TipoColumna(excel.DataSetClase, sHoja, "IdEstado", TypeCode.String);
            }

            if (excel.ExisteTablaColumna(1, "IdFarmacia"))
            {
                excel.DataSetClase = TipoColumna(excel.DataSetClase, sHoja, "IdFarmacia", TypeCode.String);
            }

            if (excel.ExisteTablaColumna(1, "IdCliente"))
            {
                excel.DataSetClase = TipoColumna(excel.DataSetClase, sHoja, "IdCliente", TypeCode.String);
            }

            if (excel.ExisteTablaColumna(1, "IdSubCliente"))
            {
                excel.DataSetClase = TipoColumna(excel.DataSetClase, sHoja, "IdSubCliente", TypeCode.String);
            }

            if (excel.ExisteTablaColumna(1, "IdBeneficiario_Relacionado"))
            {
                excel.DataSetClase = TipoColumna(excel.DataSetClase, sHoja, "IdBeneficiario_Relacionado", TypeCode.String);
            }

        }

        private void AgregarColumnasClave_Jurisdiccion()
        {
            if (excel.ExisteTablaColumna(1, "PorcParticipacion"))
            {
                excel.DataSetClase = TipoColumna(excel.DataSetClase, sHoja, "PorcParticipacion", TypeCode.Double);
            }
        }

        private void AgregarColumnasClave_ExcepcionPrecios()
        {
            if (excel.ExisteTablaColumna(1, "Tipo"))
            {
                excel.DataSetClase = TipoColumna(excel.DataSetClase, sHoja, "Tipo", TypeCode.Int32);
            }
        }

        private void AgregarColumnasBeneficiariosJurisdiccion()
        {
            if (!excel.ExisteTablaColumna(1, "NombreBeneficiario"))
            {
                excel.DataSetClase = AgregarColumna(excel.DataSetClase, sHoja, "NombreBeneficiario", "String", "");
            }

            if (!excel.ExisteTablaColumna(1, "IdEstado"))
            {
                excel.DataSetClase = AgregarColumna(excel.DataSetClase, sHoja, "IdEstado", "String", "");
            }
            if (!excel.ExisteTablaColumna(1, "IdFarmacia"))
            {
                excel.DataSetClase = AgregarColumna(excel.DataSetClase, sHoja, "IdFarmacia", "String", "");
            }
            if (!excel.ExisteTablaColumna(1, "IdCliente"))
            {
                excel.DataSetClase = AgregarColumna(excel.DataSetClase, sHoja, "IdCliente", "String", "");
            }
            if (!excel.ExisteTablaColumna(1, "IdSubCliente"))
            {
                excel.DataSetClase = AgregarColumna(excel.DataSetClase, sHoja, "IdSubCliente", "String", "");
            }
            if (!excel.ExisteTablaColumna(1, "IdBeneficiario_Relacionado"))
            {
                excel.DataSetClase = AgregarColumna(excel.DataSetClase, sHoja, "IdBeneficiario_Relacionado", "String", "");
            }
        }


        private static DataSet AgregarColumna(DataSet Datos, string Tabla, string Columna, string TipoDeDatos, string ValorDefault)
        {
            DataSet dts = Datos.Copy();
            DataTable dtConceptos;
            DataColumn dtColumnaNueva;
            clsLeer leer = new clsLeer();

            leer.DataSetClase = Datos;
            if (leer.ExisteTabla(Tabla))
            {
                dtConceptos = leer.Tabla(Tabla);
                if (!leer.ExisteTablaColumna(Tabla, Columna))
                {
                    dtColumnaNueva = new DataColumn(Columna, System.Type.GetType(string.Format("System.{0}", TipoDeDatos)));
                    dtColumnaNueva.DefaultValue = ValorDefault;
                    dtConceptos.Columns.Add(dtColumnaNueva);

                    dts.Tables.Remove(Tabla);
                    dts.Tables.Add(dtConceptos.Copy());
                }
            }

            return dts.Copy();
        }

        //private static DataSet TipoColumna(DataSet Datos, string Tabla, string Columna, string TipoDeDatos, string ValorDefault)
        private static DataSet TipoColumna(DataSet Datos, string Tabla, string NombreColumna, TypeCode typeCode)
        {
            DataSet dts = Datos.Copy();
            DataTable dt;
            clsLeer leer = new clsLeer();
            int iRenglones = 0; 

            leer.DataSetClase = Datos;
            dt = leer.Tabla(Tabla);

            try
            {
                Type type = Type.GetType("System." + typeCode);


                // Agregar columna Temporal
                DataColumn dc = new DataColumn(NombreColumna + "_new", type);
                
                //Darle la posición de a la columna nueva de la original
                int ordinal = dt.Columns[NombreColumna].Ordinal;
                dt.Columns.Add(dc);
                dc.SetOrdinal(ordinal);

                //leer el valor y convertirlo 
                foreach (DataRow dr in dt.Rows)
                {
                    iRenglones++;
                    if (typeCode == TypeCode.Boolean)
                    {
                        switch (dr[NombreColumna].ToString())
                        {
                            case "0":
                                dr[dc.ColumnName] = false;
                                break;
                            case "1":
                                dr[dc.ColumnName] = true;
                                break;
                            default:
                                dr[dc.ColumnName] = Convert.ChangeType(dr[NombreColumna], typeCode);
                                break;
                        }
                    }
                    else
                    {
                        dr[dc.ColumnName] = Convert.ChangeType(dr[NombreColumna], typeCode);
                    }
                }

                
                // remover columna vieja
                dt.Columns.Remove(NombreColumna);


                // Cambiar nombre a columna nueva
                dc.ColumnName = NombreColumna;
            }
            catch ( Exception exception )
            {
                exception = null;
            }

            leer.DataTableClase = dt;

            return leer.DataSetClase.Copy();
        }

        private void bulk_RowsCopied(RowsCopiedEventArgs e)
        {
            //lblProcesados.Text = string.Format("Procesados {0} de {1}", e.RowsCopied.ToString(sFormato_INT), excel.Registros.ToString(sFormato_INT));
        }

        private void bulk_Compled(RowsCopiedEventArgs e)
        {
            //lblProcesados.Text = string.Format("Procesados {0} de {1}", e.RowsCopied.ToString(sFormato_INT), excel.Registros.ToString(sFormato_INT));
        }

        private void bulk_Error(RowsCopiedEventArgs e)
        {
            //lblProcesados.Text = string.Format("Ocurrio un error al procesar {0} de {1}", e.RowsCopied.ToString(sFormato_INT), excel.Registros.ToString(sFormato_INT));
            Error.GrabarError(e.Error, "bulk_Error");
        }

        private string DarFormato(string Valor)
        {
            string sRegresa = Valor.Trim();

            sRegresa = sRegresa.Replace("'", "");
            sRegresa = sRegresa.Replace(",", "");

            return sRegresa;
        }

        private string GetConfiguracion(FuentesDeFinanciamiento_Configuracion Configuracion)
        {
            string sRegresa = "";
            int iFF = (int)Configuracion;

            sRegresa = iFF.ToString();

            return sRegresa;
        }

    }
}
