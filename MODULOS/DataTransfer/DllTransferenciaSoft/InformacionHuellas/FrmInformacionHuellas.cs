using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Threading; 
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

using DllFarmaciaSoft;

namespace DllTransferenciaSoft.InformacionHuellas
{   

    public partial class FrmInformacionHuellas : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsConexionSQL cnnWeb = new clsConexionSQL(General.DatosConexion);
        clsDatosConexion DatosDeConexion;
        clsLeer leer;
        clsLeer leerHuellas;
        clsLeer leerIntegrar;

        wsInformacionHuellas.wsHuellas huellasWeb = null;
        DllFarmaciaSoft.wsFarmacia.wsCnnCliente personalWeb = null;

        string sUrlHuellas = "";
        string sHostHuellas = "";

        string sUrlPersonal = "";
        string sHostPersonal = "";

        #region ManejoArchivos
        OpenFileDialog file = new OpenFileDialog();
        DataSet dtsIntegrarInfo = new DataSet();
        #endregion ManejoArchivos

        #region Hilos
        Thread thDescargar;
        Thread thIntegrar;
        #endregion Hilos
        public FrmInformacionHuellas()
        {
            InitializeComponent();

            huellasWeb = new wsInformacionHuellas.wsHuellas();
            huellasWeb.Url = General.Url;

            personalWeb = new DllFarmaciaSoft.wsFarmacia.wsCnnCliente();
            personalWeb.Url = General.Url;

            leer = new clsLeer(ref cnn);
            leerHuellas = new clsLeer(ref cnn);
            leerIntegrar = new clsLeer(ref cnn);

            MostrarEnProceso(false, 0);
        }

        private void FrmInformacionHuellas_Load(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
        }

        private bool Obtener_Url_Huellas()
        {
            bool bRegresa = true;

            string sSql = string.Format(" Select ('http://' + IsNull(Servidor, '') + '/' + IsNull(WebService, '') + '/' + IsNull(PaginaWeb, '') + '.asmx') as UrlHuellas, " +
                                        " IsNull(Status, 'S') as StatusUrl, Servidor, WebService, PaginaWeb  From CFGC_Huellas (NoLock)  ");

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "Obtener_Url_Huellas()");
                General.msjError("Ocurrió un error al obtener la Url del servidor de Huellas");
            }
            else
            {
                if (leer.Leer())
                {
                    sUrlHuellas = leer.Campo("UrlHuellas");
                    sHostHuellas = leer.Campo("Servidor");
                }
                else
                {
                    bRegresa = false;
                }
            }

            return bRegresa;
        }

        private bool Obtener_Url_Personal()
        {
            bool bRegresa = true;

            string sSql = string.Format(" Select ('http://' + IsNull(Servidor, '') + '/' + IsNull(WebService, '') + '/' + IsNull(PaginaWeb, '') + '.asmx') as Url, " +
                                        " IsNull(Status, 'S') as StatusUrl, Servidor, WebService, PaginaWeb  From CFGC_Checador (NoLock)  ");

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "Obtener_Url_Personal()");
                General.msjError("Ocurrió un error al obtener la Url de Personal");
            }
            else
            {

                if (leer.Leer())
                {
                    sUrlPersonal = leer.Campo("Url");
                    sHostPersonal = leer.Campo("Servidor");
                }
                else
                {
                    bRegresa = false;
                }
            }

            return bRegresa;
        }

        private bool validarDatosDeConexion()
        {
            bool bRegresa = false;

            try
            {               
                personalWeb.Url = sUrlPersonal;
                DatosDeConexion = new clsDatosConexion(personalWeb.ConexionEx(DtGeneral.CfgIniChecadorPersonal));                
                DatosDeConexion.Servidor = sHostPersonal;
                bRegresa = true;
            }
            catch (Exception ex1)
            {
                Error.GrabarError(leer.DatosConexion, ex1, "validarDatosDeConexion()");
                General.msjAviso("No fue posible establecer conexión con la Unidad, intente de nuevo.");
            }

            return bRegresa;
        }

        private void HabilitarControles(bool Valor)
        {
            btnDescargaInfo.Enabled = Valor;
            btnIntegraInfo.Enabled = Valor;
        }

        private void MostrarEnProceso(bool Mostrar, int Proceso)
        {
            string sTituloProceso = "";

            if (Mostrar)
            {
                FrameProceso.Left = 12;
            }
            else
            {
                FrameProceso.Left = this.Width + 100;
            }

            if (Proceso == 1)
            {
                sTituloProceso = "Descargando Información Huellas";
            }

            if (Proceso == 2)
            {
                sTituloProceso = "Integrando Información Huellas";
            }

            FrameProceso.Text = sTituloProceso;

        }
        #endregion Funciones

        #region Botones
        private void btnDescargaInfo_Click(object sender, EventArgs e)
        {
            if (Obtener_Url_Personal())
            {
                if (validarDatosDeConexion())
                {
                    if (Obtener_Url_Huellas())
                    {
                        thDescargar = new Thread(this.DescargaInformacionHuellas);
                        thDescargar.Name = "Descargar Información Huellas";
                        thDescargar.Start();
                    }
                }
            }
        }

        private void btnIntegraInfo_Click(object sender, EventArgs e)
        {
            dtsIntegrarInfo = new DataSet();

            if (Obtener_Url_Personal())
            {
                if (validarDatosDeConexion())
                {
                    file.Filter = "xml files (*.xml)|*.xml";

                    if (file.ShowDialog() == DialogResult.OK)
                    {
                        // Escribir el esquema y los datos a un archivo XML con FileStream.
                        System.IO.FileStream streamRead = new System.IO.FileStream(file.FileName, System.IO.FileMode.Open);

                        try
                        {
                            dtsIntegrarInfo.ReadXml(streamRead, XmlReadMode.ReadSchema);
                            thIntegrar = new Thread(this.IntegrarPaqueteHuellas);
                            thIntegrar.Name = "Integrar Información Huellas";
                            thIntegrar.Start();
                        }
                        catch (Exception ex1)
                        {
                            Error.GrabarError(ex1.Message, "btnIntegraInfo_Click");
                            General.msjAviso("No fue posible leer el archivo de Huellas, intente de nuevo.");
                        }

                        //streamRead.Close();
                    }
                    
                }           
            }
        }
        #endregion Botones

        #region DescargaInformacion
        private void thDescargaInformacionHuellas()
        {
            DescargaInformacionHuellas();
        }
        private void DescargaInformacionHuellas()
        {
            bool bRetorno = false, bSalir = false;
            bool bContinua = false;

            DataSet dtsInfoHuellas = null;  //= new DataSet();

            HabilitarControles(false);
            cnnWeb = new clsConexionSQL(DatosDeConexion);
            leerHuellas = new clsLeer(ref cnnWeb);
            leerIntegrar = new clsLeer(ref cnnWeb);

            try
            {
                // sUrlHuellas = "http://lapfernando/wsAdministrativos/wsChecador.asmx"; 
                huellasWeb = new wsInformacionHuellas.wsHuellas();
                huellasWeb.Url = sUrlHuellas;


                try
                {
                    dtsInfoHuellas = huellasWeb.InformacionHuellas();
                    bContinua = true;
                }
                catch (Exception ex2)
                {
                    Error.GrabarError(ex2.Message, "IntegrarInformacionHuellas");
                    General.msjError("Ocurrió un error al Obtener los datos para la Integracion.");
                }
            }
            catch (Exception ex1)
            {
                Error.GrabarError(ex1.Message, "IntegrarInformacionHuellas");
                General.msjAviso("No fue posible establecer comunicación con el servidor de Huellas, intente de nuevo.");
            }


            // Revisar la respuesta del Servidor de Huellas 
            if (dtsInfoHuellas == null)
            {
                bContinua = false;
            }
            
            if (bContinua)
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    MostrarEnProceso(true, 1);
                    
                    for (int i = 0; i < dtsInfoHuellas.Tables.Count; i++)
                    {
                        leerHuellas.DataTableClase = dtsInfoHuellas.Tables[i];
                        while (leerHuellas.Leer())
                        {
                            string sCadena = leerHuellas.Campo("Resultado");

                            if (!leerIntegrar.Exec(sCadena))
                            {
                                bSalir = true;
                                break;
                            }
                            else
                            {
                                bRetorno = true;
                            }
                        }
                        if (bSalir)
                        {
                            bRetorno = false;
                            break;
                        }
                    }

                    if (bRetorno)
                    {
                        cnn.CompletarTransaccion();
                        General.msjAviso("La Descarga de Información de Personal, se realizo Satisfactoriamente...");
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leerHuellas, "IntegrarInformacionHuellas");
                        General.msjError("Ocurrió un error al Integrar la información de Huellas.");
                    }
                }
            }

            HabilitarControles(true);

            MostrarEnProceso(false, 1);

            //return bRetorno;
        }
        #endregion DescargaInformacion

        #region Integrar_Paquete_Huellas
        private void thIntegrarPaqueteHuellas()
        {
            IntegrarPaqueteHuellas();
        }

        private void IntegrarPaqueteHuellas()
        {
            bool bRetorno = false, bSalir = false;
            bool bContinua = true;

            DataSet dtsInfoHuellas = new DataSet();

            HabilitarControles(false);
            cnnWeb = new clsConexionSQL(DatosDeConexion);
            leerHuellas = new clsLeer(ref cnnWeb);
            leerIntegrar = new clsLeer(ref cnnWeb);            

            // Revisar la respuesta del Servidor de Huellas 
            if (dtsIntegrarInfo == null)
            {
                bContinua = false;
            }

            if (bContinua)
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    MostrarEnProceso(true, 2);

                    for (int i = 0; i < dtsIntegrarInfo.Tables.Count; i++)
                    {
                        leerHuellas.DataTableClase = dtsIntegrarInfo.Tables[i];
                        while (leerHuellas.Leer())
                        {
                            string sCadena = leerHuellas.Campo("Resultado");

                            if (!leerIntegrar.Exec(sCadena))
                            {
                                bSalir = true;
                                break;
                            }
                            else
                            {
                                bRetorno = true;
                            }
                        }
                        if (bSalir)
                        {
                            bRetorno = false;
                            break;
                        }
                    }

                    if (bRetorno)
                    {
                        cnn.CompletarTransaccion();
                        General.msjAviso("La Integración de Información de Huellas, se realizo Satisfactoriamente...");
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leerHuellas, "IntegrarPaqueteHuellas");
                        General.msjError("Ocurrió un error al Integrar la información de Huellas.");
                    }
                }
            }

            HabilitarControles(true);
            MostrarEnProceso(false, 2);

            //return bRetorno;
        }
        #endregion Integrar_Paquete_Huellas
        
    }
}
