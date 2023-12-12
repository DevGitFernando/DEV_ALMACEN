using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

using DllFarmaciaSoft;

namespace DllTransferenciaSoft.Procesos
{
    public partial class FrmGeneraPaqueteHuellas : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);        
        //clsDatosConexion DatosDeConexion;
        clsLeer leer;
        clsLeer leerHuellas;
        clsLeer leerIntegrar;

        SaveFileDialog save = new SaveFileDialog();

        wsInformacionHuellas.wsHuellas huellasWeb = null;

        #region Hilos
        Thread thGeneraPaquete;        
        #endregion Hilos

        string sUrlHuellas = "";

        public FrmGeneraPaqueteHuellas()
        {
            InitializeComponent();

            ////Lineas de Prueba
            ////cnn.DatosConexion.Servidor = "lapfernando";
            ////cnn.DatosConexion.BaseDeDatos = "SII_Administrativos";
            ////cnn.DatosConexion.Usuario = "sa";
            ////cnn.DatosConexion.Password = "1234";

            leer = new clsLeer(ref cnn);
            leerHuellas = new clsLeer(ref cnn);
            leerIntegrar = new clsLeer(ref cnn);

            MostrarEnProceso(false, 0);
        }

        private void FrmGeneraPaqueteHuellas_Load(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            GeneraUrlHuellas();
        }

        #region Botones
        private void btnGeneraPaquete_Click(object sender, EventArgs e)
        {
            string sMarcaTiempo = "";

            sMarcaTiempo = General.FechaYMD(General.FechaSistema).Replace("/", "");
            sMarcaTiempo = sMarcaTiempo.Replace("-", "");
            sMarcaTiempo += "-" + General.FechaSistemaHora.Substring(0, 5).Replace(":", "");

            save.Filter = "xml files (*.xml)|*.xml";
            save.FileName = "CATALOGO_HUELLAS_" + sMarcaTiempo;

            if (save.ShowDialog() == DialogResult.OK)
            {
                thGeneraPaquete = new Thread(this.GeneraPaqueteHuellas);
                thGeneraPaquete.Name = "Generar Paquete Huellas";
                thGeneraPaquete.Start();
            }
        }
        #endregion Botones

        #region Funciones
        private void HabilitaControles(bool Valor)
        {
            btnGeneraPaquete.Enabled = Valor;
        }        

        private void GeneraUrlHuellas()
        {
            General.DatosDeServicioWeb.PaginaASMX = "wsHuellas";

            sUrlHuellas = General.DatosDeServicioWeb.DireccionUrl;
        }

        private void MostrarEnProceso(bool Mostrar, int Proceso)
        {
            string sTituloProceso = "";

            if (Mostrar)
            {
                FrameProceso.Left = 53;
            }
            else
            {
                FrameProceso.Left = this.Width + 100;
            }

            if (Proceso == 1)
            {
                sTituloProceso = "Generando Paquete Huellas";
            }          

            FrameProceso.Text = sTituloProceso;

        }
        #endregion Funciones

        #region Genera_Paquete_Huellas
        private void thGeneraPaqueteHuellas()
        {
            GeneraPaqueteHuellas();
        }

        private void GeneraPaqueteHuellas()
        {
            //bool bRetorno = false;
            bool bContinua = false;           

            DataSet dtsInfoHuellas = null;  //= new DataSet();

            HabilitaControles(false);
            MostrarEnProceso(true, 1);

            try
            {                
                huellasWeb = new wsInformacionHuellas.wsHuellas();
                huellasWeb.Url = sUrlHuellas;

                //// Linea de Prueba
                //huellasWeb.Url = "http://lapfernando/wsAdministrativos/wsHuellas.asmx";
                try
                {
                    dtsInfoHuellas = huellasWeb.InformacionHuellas();
                    bContinua = true;
                   
                }
                catch (Exception ex2)
                {
                    Error.GrabarError(ex2.Message, "GeneraPaqueteHuellas");
                    General.msjError("Ocurrió un error al generar el paquete de Huellas.");
                }
            }
            catch (Exception ex1)
            {
                Error.GrabarError(ex1.Message, "GeneraPaqueteHuellas");
                General.msjAviso("No fue posible establecer comunicación con el servidor de Huellas, intente de nuevo.");
            }

            // Revisar la respuesta del Servidor de Huellas 
            if (dtsInfoHuellas == null)
            {
                bContinua = false;
            }

            if (bContinua)
            {
                try
                {
                    // Escribir el esquema y los datos a un archivo XML con FileStream.
                    System.IO.FileStream streamWrite = new System.IO.FileStream(save.FileName, System.IO.FileMode.Create);

                    // Utilice WriteXml para escribir el documento XML.
                    dtsInfoHuellas.WriteXml(streamWrite, XmlWriteMode.WriteSchema);
                    streamWrite.Close();
                }
                catch (Exception ex3)
                {
                    Error.GrabarError(ex3.Message, "GeneraPaqueteHuellas");
                    General.msjAviso("No fue posible generar el Paquete, intente de nuevo.");
                    bContinua = false;
                }
                
            }

            if (bContinua)
            {
                General.msjAviso("La Generación del Paquete de Huellas, se realizo Satisfactoriamente...");
            }

            HabilitaControles(true);
            MostrarEnProceso(false, 1);
            //return bRetorno;
        }
        #endregion Genera_Paquete_Huellas     
        
    }
}
