using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using Microsoft.VisualBasic.FileIO;

using DllFarmaciaSoft; 

namespace DllRegistrosSanitarios.Sincronizar
{
    public partial class FrmSincronizarDatos : FrmBaseExt 
    {
        clsConexionSQLite cnn;
        clsLeerSQLite leer, leer_AUX;

        clsListView lst; 
        clsLeerWebExt leerWeb;
        clsLeerWebExt leerWeb_Descarga;
        clsDatosCliente datosCliente;

        int iInserts_Updates = 0;
        int iErrores = 0;
        string sLista_MD5 = "";
        Thread thActualizacion; 

        public FrmSincronizarDatos()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; 

            cnn = new clsConexionSQLite(GnRegistrosSanitarios.DaseDeDatos_SQLite);
            leer = new clsLeerSQLite(ref cnn);
            leer_AUX = new clsLeerSQLite(ref cnn);
            datosCliente = new clsDatosCliente(GnRegistrosSanitarios.DatosApp, this.Name, ""); 


            leerWeb = new clsLeerWebExt(General.Url, DtGeneral.CfgIniOficinaCentral, datosCliente);
            leerWeb_Descarga = new clsLeerWebExt(General.Url, DtGeneral.CfgIniOficinaCentral, datosCliente);

            lst = new clsListView(listViewRegistros); 
        }

        private void FrmSincronizarDatos_Load(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            lst.Limpiar();
            lblAvance.Text = ""; 
        }

        private void btnSincronizarDatos_Click(object sender, EventArgs e)
        {
            clsLeer leerFiltro = new clsLeer();
            clsLeer leerFiltro_SQLite = new clsLeer(); 
            string sSql = "";
            string sFiltro = ""; 

            sSql = 
                "Select top 10 " + 
		        "Folio, IdProducto, CodigoEAN, CodigoEAN_Interno, NombreDocto, Consecutivo, Tipo, Año as Anio, RegistroSanitario, FechaRegistro, FechaVigencia, Vigente, " + 
		        "StatusRegistro, StatusRegistroAux, IdClaveSSA_Sal, ClaveSSA_Base, ClaveSSA, DescripcionClave, DescripcionCortaClave, " + 
		        "IdGrupoTerapeutico, GrupoTerapeutico, Descripcion, DescripcionCorta, IdClasificacion, Clasificacion, IdTipoProducto, TipoDeProducto, " + 
		        "TasaIva, EsControlado, EsSectorSalud, IdFamilia, Familia, IdSubFamilia, SubFamilia, IdSegmento, Segmento, IdLaboratorio, Laboratorio, " + 
		        "IdPresentacion, Presentacion, Despatilleo, ContenidoPaquete, ManejaCodigosEAN, Status, StatusCodigoRel " + 
	            "From vw_RegistrosSanitarios_CodigoEAN " + 
                "Where StatusRegistro <> '-1'  Order by Folio ";

            ////sSql =
            ////    "Select top 10 " +
            ////    "Folio, IdProducto, CodigoEAN, CodigoEAN_Interno, NombreDocto, Consecutivo, Tipo, Año as Anio " + 
            ////    "From vw_RegistrosSanitarios_CodigoEAN " +
            ////    "Where StatusRegistro <> '-1'  and Folio >= 10  ";


            lblAvance.Text = "";
            Application.DoEvents();

            Comparar_Depurar();

            OptenerPlantilla();

            sSql = "Select Distinct MD5 From RegistrosSanitarios_CodigoEAN  ";
            sSql = "Select Folio, CodigoEAN, MD5 From RegistrosSanitarios_CodigoEAN  ";

            sLista_MD5 = ""; 
            if (!leer.Exec(sSql))
            {
                General.msjError("Ocurrió un error al obtener la lista de registros sanitarios.");
            }
            else
            {
                //while (leer.Leer()) 
                //{
                //    sLista_MD5 += string.Format("'{0}',", leer.Campo("MD5"));
                //}
            
                //if ( sLista_MD5 != "") sLista_MD5 = Fg.Left(sLista_MD5, sLista_MD5.Length - 1);
            }

            //if (sLista_MD5 != "")
            //{
            //    sFiltro = string.Format(" And MD5 Not In ( {0} ) ", sLista_MD5);
            //}

            sFiltro = ""; 
            sSql = 
                string.Format(
                "Select " + 
                "Folio, IdProducto, CodigoEAN, CodigoEAN_Interno, NombreDocto, MD5, Consecutivo, Tipo, Año as Anio, RegistroSanitario, FechaRegistro, FechaVigencia, Vigente, 1 as Descargar " +
                "From vw_RegistrosSanitarios_CodigoEAN " + 
                "Where StatusRegistro <> '-1' {0} Order by Folio ", sFiltro); 


            lst.Limpiar();
            if (!leerWeb.Exec(sSql))
            {
                General.msjError("Ocurrió un error al obtener la información para sincronización.");
            }
            else
            {
                if (leer.Registros > 0)
                {
                    while (leerWeb.Leer())
                    {
                        leerFiltro.DataRowsClase = leer.DataTableClase.Select(string.Format(" Folio = '{0}' and CodigoEAN = '{1}' ", leerWeb.Campo("Folio"), leerWeb.Campo("CodigoEAN")));
                        //leerFiltro_SQLite.DataRowsClase = leer.DataTableClase.Select(string.Format(" Folio = '{0}' and CodigoEAN = '{1}' ", leerWeb.Campo("Folio"), leerWeb.Campo("CodigoEAN")));
                        
                        if (leerFiltro.Leer())
                        {
                            if (leerWeb.Campo("MD5") == leerFiltro.Campo("MD5"))
                            {
                                leerWeb.GuardarDatos("Descargar", "0"); 
                            }
                        }
                    }
                }



                lst.CargarDatos(leerWeb.DataSetClase, true, true);


                leerFiltro.DataRowsClase = leerWeb.DataTableClase.Select(" Descargar = 1 ");
                leerWeb.DataSetClase = leerFiltro.DataSetClase;


                lblAvance.Text = string.Format("Registros disponibles para descarga {0}", leerWeb.Registros);
            }

        }

        /// <summary>
        /// Reemplazar los caracteres espciales
        /// </summary>
        /// <param name="Cadena"></param>
        /// <returns></returns>
        public string Normalizar(string Cadena)
        {
            string sRegresa = Cadena;

            string consignos = "áàäéèëíìïóòöúùuñÁÀÄÉÈËÍÌÏÓÒÖÚÙÜÑçÇ";
            string sinsignos = "aaaeeeiiiooouuunAAAEEEIIIOOOUUUNcC";

            if (Cadena != "")
            {
                for (int i = 0; i <= consignos.Length - 1; i++)
                {
                    sRegresa = sRegresa.Replace(consignos[i], sinsignos[i]);
                }
            }

            return sRegresa;
        }

        private void btnSincronizarSQLite_Click(object sender, EventArgs e)
        {
            btnNuevo.Enabled = false;
            btnSincronizarDatos.Enabled = false;
            btnSincronizarSQLite.Enabled = false; 

            thActualizacion = new Thread(ActualizarRegistros);
            thActualizacion.Name = "Actualizacion__SQLite";
            thActualizacion.Start(); 
        }

        private void ActualizarRegistros()
        {
            string sSql = "";
            string sSql_Descarga_Base = "";
            string sSql_Descarga = "";
            string sSql_Descarga_NoExists = "";
            string sCampos = "";
            string sValores_Update = "";
            string sValores = "";
            string sValorObtenido = "";
            string sDocumento = "", sDocumentoNombre = "";

            int iProcesados = 0;
            int iRegistros = leerWeb.Registros;

            bool bDescargado = false;

            FileInfo Dir = null;

            iInserts_Updates = 0;
            iErrores = 0;


            sSql_Descarga =
                string.Format(
                "Select top 10 " +
                "   R.Folio, R.IdProducto, R.CodigoEAN, R.CodigoEAN_Interno, R.NombreDocto, R.MD5, C.Documento as Documento_Byte, R.Consecutivo, R.Tipo, R.Año as Anio, " + 
                "   R.RegistroSanitario, R.FechaRegistro, R.FechaVigencia, R.Vigente, " +
                "   R.StatusRegistro, R.StatusRegistroAux, R.IdClaveSSA_Sal, R.ClaveSSA_Base, R.ClaveSSA, R.DescripcionClave, R.DescripcionCortaClave, " +
                "   R.IdGrupoTerapeutico, R.GrupoTerapeutico, R.Descripcion, R.DescripcionCorta, R.IdClasificacion, R.Clasificacion, R.IdTipoProducto, R.TipoDeProducto, " +
                "   R.TasaIva, R.EsControlado, R.EsSectorSalud, R.IdFamilia, R.Familia, R.IdSubFamilia, R.SubFamilia, R.IdSegmento, R.Segmento, R.IdLaboratorio, R.Laboratorio, " +
                "   R.IdPresentacion, R.Presentacion, R.Despatilleo, R.ContenidoPaquete, R.ManejaCodigosEAN, R.Status, R.StatusCodigoRel " +
                "From vw_RegistrosSanitarios_CodigoEAN R " +
                "Inner Join CatRegistrosSanitarios C (NoLock) On ( R.Folio = C.Folio ) " +
                "Where R.StatusRegistro <> '-1' ");

            sSql_Descarga_NoExists =
                string.Format(
                "Select top 10 " +
                "   R.Folio, R.IdProducto, R.CodigoEAN, R.CodigoEAN_Interno, R.NombreDocto, R.MD5, R.Consecutivo, R.Tipo, R.Año as Anio, " +
                "   R.RegistroSanitario, R.FechaRegistro, R.FechaVigencia, R.Vigente, " +
                "   R.StatusRegistro, R.StatusRegistroAux, R.IdClaveSSA_Sal, R.ClaveSSA_Base, R.ClaveSSA, R.DescripcionClave, R.DescripcionCortaClave, " +
                "   R.IdGrupoTerapeutico, R.GrupoTerapeutico, R.Descripcion, R.DescripcionCorta, R.IdClasificacion, R.Clasificacion, R.IdTipoProducto, R.TipoDeProducto, " +
                "   R.TasaIva, R.EsControlado, R.EsSectorSalud, R.IdFamilia, R.Familia, R.IdSubFamilia, R.SubFamilia, R.IdSegmento, R.Segmento, R.IdLaboratorio, R.Laboratorio, " +
                "   R.IdPresentacion, R.Presentacion, R.Despatilleo, R.ContenidoPaquete, R.ManejaCodigosEAN, R.Status, R.StatusCodigoRel " +
                "From vw_RegistrosSanitarios_CodigoEAN R " +
                "Where R.StatusRegistro <> '-1' ");


            ////sSql_Descarga =
            ////    string.Format(
            ////    "Select top 10 " +
            ////    "   R.Folio, R.IdProducto, R.CodigoEAN, R.CodigoEAN_Interno, R.NombreDocto, R.MD5, '' as Documento_Byte, R.Consecutivo, R.Tipo, R.Año as Anio, " +
            ////    "   R.RegistroSanitario, R.FechaRegistro, R.FechaVigencia, R.Vigente, " +
            ////    "   R.StatusRegistro, R.StatusRegistroAux, R.IdClaveSSA_Sal, R.ClaveSSA_Base, R.ClaveSSA, R.DescripcionClave, R.DescripcionCortaClave, " +
            ////    "   R.IdGrupoTerapeutico, R.GrupoTerapeutico, R.Descripcion, R.DescripcionCorta, R.IdClasificacion, R.Clasificacion, R.IdTipoProducto, R.TipoDeProducto, " +
            ////    "   R.TasaIva, R.EsControlado, R.EsSectorSalud, R.IdFamilia, R.Familia, R.IdSubFamilia, R.SubFamilia, R.IdSegmento, R.Segmento, R.IdLaboratorio, R.Laboratorio, " +
            ////    "   R.IdPresentacion, R.Presentacion, R.Despatilleo, R.ContenidoPaquete, R.ManejaCodigosEAN, R.Status, R.StatusCodigoRel " +
            ////    "From vw_RegistrosSanitarios_CodigoEAN R " +
            ////    "Inner Join CatRegistrosSanitarios C (NoLock) On ( R.Folio = C.Folio ) " +
            ////    "Where R.StatusRegistro <> '-1' ");



            lblAvance.Text = string.Format("Procesando {0} de {1}", iProcesados, iRegistros);
            Application.DoEvents();

            leerWeb.RegistroActual = 1;
            while (leerWeb.Leer())
            {
                iProcesados++;
                lblAvance.Text = string.Format("Procesando {0} de {1}", iProcesados, iRegistros);
                Application.DoEvents();

                sDocumentoNombre = leerWeb.Campo("MD5") + ".SRS";
                sDocumento = Path.Combine(GnRegistrosSanitarios.Ruta_DB_RegistrosSanitarios, sDocumentoNombre);

                Dir = new FileInfo(sDocumento);

                if (!Dir.Exists)
                {
                    try
                    {
                        sSql_Descarga_Base = sSql_Descarga + string.Format(" and R.Folio = '{0}' and R.CodigoEAN = '{1}' ", leerWeb.Campo("Folio"), leerWeb.Campo("CodigoEAN"));
                        if (!leerWeb_Descarga.Exec(sSql_Descarga_Base))
                        {
                            iErrores++;
                        }
                        else
                        {
                            if (leerWeb_Descarga.Leer())
                            {
                                if (sCampos == "")
                                {
                                    foreach (string sColumna in leerWeb_Descarga.ColumnasNombre)
                                    {
                                        if (sColumna.ToUpper() != "Documento_Byte".ToUpper())
                                        {
                                            sCampos += sColumna + ", ";
                                        }
                                        
                                    }

                                    sCampos = sCampos.Trim();
                                    sCampos = Fg.Left(sCampos, sCampos.Length - 1);
                                }


                                sValores = "";
                                sValores_Update = "";
                                foreach (string sColumna in leerWeb_Descarga.ColumnasNombre)
                                {
                                    sValorObtenido = leerWeb_Descarga.Campo(sColumna);
                                    if (sColumna.ToUpper() != "Documento_Byte".ToUpper())
                                    {
                                        sValores_Update += string.Format(" {0} = '{1}', ", sColumna, sValorObtenido);
                                        sValores += string.Format("'{0}', ", sValorObtenido);
                                    }
                                }


                              bDescargado = Fg.ConvertirBytesEnArchivo(leerWeb_Descarga.Campo("MD5") + ".SRS", GnRegistrosSanitarios.Ruta_DB_RegistrosSanitarios,
                                        leerWeb_Descarga.CampoByte("Documento_Byte"), true);

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        sSql = ex.Message;
                    }

                    if (!bDescargado)
                    {
                        General.msjAviso("Error");
                    }


                    sValores = sValores.Trim();
                    sValores = Fg.Left(sValores, sValores.Length - 1);

                    sValores_Update = sValores_Update.Trim();
                    sValores_Update = Fg.Left(sValores_Update, sValores_Update.Length - 1);


                    //sValores = Normalizar(sValores); 
                    sSql = "";
                    sSql = string.Format("Update RegistrosSanitarios_CodigoEAN Set {0} \n ", sValores_Update);
                    sSql += string.Format("Where Folio = '{0}' and CodigoEAN = '{1}' \n ",
                        leerWeb_Descarga.Campo("Folio"), leerWeb_Descarga.Campo("CodigoEAN"));

                    Dir = new FileInfo(sDocumento);

                    if (Dir.Exists)
                    {
                        if (!leer.Exec(sSql))
                        {
                            iErrores++;
                        }
                        else
                        {
                            sSql += "\n \n";
                            sSql = "";
                            sSql += string.Format("Insert Into RegistrosSanitarios_CodigoEAN ( {0} ) \n ", sCampos);
                            sSql += string.Format("Select {0} \n ", sValores);
                            //sSql += string.Format("From RegistrosSanitarios_CodigoEAN D \n ");
                            sSql += string.Format("Where Not Exists ( select * from RegistrosSanitarios_CodigoEAN Where Folio = '{0}' and CodigoEAN = '{1}' )  \n ",
                                leerWeb_Descarga.Campo("Folio"), leerWeb_Descarga.Campo("CodigoEAN"));
                            if (!leer.Exec(sSql))
                            {
                                iErrores++;
                            }
                            else
                            {
                                iInserts_Updates++;
                            }
                        }
                    }
                }
                else
                {
                    sSql_Descarga_Base = sSql_Descarga_NoExists + string.Format(" and R.Folio = '{0}' and R.CodigoEAN = '{1}' ", leerWeb.Campo("Folio"), leerWeb.Campo("CodigoEAN"));
                    if (!leerWeb_Descarga.Exec(sSql_Descarga_Base))
                    {
                        iErrores++;
                    }
                    else
                    {
                        if (leerWeb_Descarga.Leer())
                        {
                            if (sCampos == "")
                            {
                                foreach (string sColumna in leerWeb_Descarga.ColumnasNombre)
                                {
                                    sCampos += sColumna + ", ";
                                }

                                sCampos = sCampos.Trim();
                                sCampos = Fg.Left(sCampos, sCampos.Length - 1);
                            }


                            sValores = "";
                            sValores_Update = "";
                            foreach (string sColumna in leerWeb_Descarga.ColumnasNombre)
                            {
                                sValorObtenido = leerWeb_Descarga.Campo(sColumna);
                                if (sColumna.ToUpper() == "Documento_Byte".ToUpper())
                                {
                                    sValorObtenido = ""; // Normalizar(sValorObtenido);
                                }

                                sValores_Update += string.Format(" {0} = '{1}', ", sColumna, sValorObtenido);
                                sValores += string.Format("'{0}', ", sValorObtenido);
                            }
                        }

                        sValores = sValores.Trim();
                        sValores = Fg.Left(sValores, sValores.Length - 1);

                        sValores_Update = sValores_Update.Trim();
                        sValores_Update = Fg.Left(sValores_Update, sValores_Update.Length - 1);


                        //sValores = Normalizar(sValores); 
                        sSql = "";
                        sSql = string.Format("Update RegistrosSanitarios_CodigoEAN Set {0} \n ", sValores_Update);
                        sSql += string.Format("Where Folio = '{0}' and CodigoEAN = '{1}' \n ",
                            leerWeb_Descarga.Campo("Folio"), leerWeb_Descarga.Campo("CodigoEAN"));


                        if (!leer.Exec(sSql))
                        {
                            iErrores++;
                        }
                        else
                        {
                            sSql += "\n \n";
                            sSql = "";
                            sSql += string.Format("Insert Into RegistrosSanitarios_CodigoEAN ( {0} ) \n ", sCampos);
                            sSql += string.Format("Select {0} \n ", sValores);
                            //sSql += string.Format("From RegistrosSanitarios_CodigoEAN D \n ");
                            sSql += string.Format("Where Not Exists ( select * from RegistrosSanitarios_CodigoEAN Where Folio = '{0}' and CodigoEAN = '{1}' )  \n ",
                                leerWeb_Descarga.Campo("Folio"), leerWeb_Descarga.Campo("CodigoEAN"));
                            if (!leer.Exec(sSql))
                            {
                                iErrores++;
                            }
                            else
                            {
                                iInserts_Updates++;
                            }
                        }
                    }
                }

                //iProcesados--;
                lblAvance.Text = string.Format("Procesando {0} de {1}", iProcesados, iRegistros);
                Application.DoEvents();

            }

            btnNuevo.Enabled = true;
            btnSincronizarDatos.Enabled = true;
            btnSincronizarSQLite.Enabled = true; 
        }

        private void Comparar_Depurar()
        {
            string sSql = string.Format("Select Distinct(MD5) From RegistrosSanitarios_CodigoEAN ");
            string sSql_AUX = "";
            string[] sListaArchivos = Directory.GetFiles(GnRegistrosSanitarios.Ruta_DB_RegistrosSanitarios, "*.SRS", System.IO.SearchOption.TopDirectoryOnly );
            string sMD5 = "", sMD5_AUX2 = "", sMD5_AUX3 = "";
            int iPos = 0, iExtencion = 4;
            bool bEncontrado = false;

            int iRuta = GnRegistrosSanitarios.Ruta_DB_RegistrosSanitarios.Length;

            if (leer.Exec(sSql))
            {
                if (leer.Registros > 0)
                {
                    while (leer.Leer())
                    {
                        bEncontrado = false;

                        sMD5 = leer.Campo("MD5");

                        foreach (string sMD5_AUX in sListaArchivos)
                        {
                            sMD5_AUX2 = sMD5_AUX.Remove(0, iRuta);
                            iPos = sMD5_AUX2.Length - iExtencion;

                            sMD5_AUX3 = sMD5_AUX2.Remove(iPos, iExtencion);

                            if (sMD5 == sMD5_AUX3)
                            {
                                bEncontrado = true;
                                break;
                            }
                        }

                        if (!bEncontrado)
                        {
                            sSql_AUX = string.Format("Delete From RegistrosSanitarios_CodigoEAN Where MD5 = '{0}'", sMD5);

                            leer_AUX.Exec(sSql_AUX);
                        }
                    }
                }

                foreach (string sMD5_AUX in sListaArchivos)
                {
                    bEncontrado = false;

                    sMD5_AUX2 = sMD5_AUX.Remove(0, iRuta);
                    iPos = sMD5_AUX2.Length - iExtencion;

                    sMD5_AUX3 = sMD5_AUX2.Remove(iPos, iExtencion);

                    if (leer.Registros > 0)
                    {
                        leer.RegistroActual = 1;

                        while (leer.Leer())
                        {
                            sMD5 = leer.Campo("MD5");

                            if (sMD5 == sMD5_AUX3)
                            {
                                bEncontrado = true;
                                break;
                            }
                        }
                    }

                    if (!bEncontrado)
                    {
                        File.Delete(sMD5_AUX);
                    }
                }
            }

        }

        private void OptenerPlantilla()
        {
            byte[] btReporte = null;
            string sReporte = @"Rpt_RegistrosSanitarios.xlsx";
            string sRutaSalida = Path.Combine(Application.StartupPath + @"\\Plantillas\", "");
            string sRutaDeReportes = DtGeneral.RutaReportes;

            DllFarmaciaSoft.wsOficinaCentral.wsCnnOficinaCentral webService = new DllFarmaciaSoft.wsOficinaCentral.wsCnnOficinaCentral();

            webService.Url = General.Url;

            try
            {
                btReporte = webService.ReporteExcel(sReporte, sRutaDeReportes);

            } catch {}


            if (btReporte != null)
            {
                if (!Directory.Exists(sRutaSalida))
                {
                    Directory.CreateDirectory(sRutaSalida); 
                }

                try
                {
                    //// Forzar el borrado del archivo existente 
                    if (File.Exists(sRutaSalida + sReporte))
                    {
                        File.Delete(sRutaSalida + sReporte); 
                    }
                }
                catch { }

                Microsoft.VisualBasic.FileIO.FileSystem.WriteAllBytes(sRutaSalida + sReporte, btReporte, false);                              

            }
        }
    }
}
