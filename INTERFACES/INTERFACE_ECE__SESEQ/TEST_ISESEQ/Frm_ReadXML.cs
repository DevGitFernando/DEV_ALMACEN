using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data; 

using DllFarmaciaSoft;
using Dll_ISESEQ; 
using Dll_ISESEQ.Informacion;
using Dll_ISESEQ.wsClases; 

namespace TEST_ISESEQ
{
    public partial class Frm_ReadXML : Form
    {
        ResponseRecetaElectronica respuesta; // = new ResponseRecetaElectronica();
        TipoProcesoReceta tpProceso; // = TipoProcesoReceta.Ninguno; 
        RecetaElectronica receta; //  = new RecetaElectronica(datosCnn);
        ColectivoElectronico colectivo; //  = new ColectivoElectronico(datosCnn); 


        clsDatosConexion datosCnn; // = new clsDatosConexion(AbrirConexionEx(sFileConexion));
        clsConexionSQL cnn; //= new clsConexionSQL(datosCnn);


        public Frm_ReadXML()
        {
            InitializeComponent();

            cnn = new clsConexionSQL(General.DatosConexion); 


            respuesta = new ResponseRecetaElectronica();
            tpProceso = TipoProcesoReceta.Ninguno; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sFilePath = @"H:\Ultimo.xml";
            string Informacion_XML = "";
            string sRegresa = "";

            cnn = new clsConexionSQL(General.DatosConexion);
            receta = new RecetaElectronica(General.DatosConexion);
            colectivo = new ColectivoElectronico(General.DatosConexion); 


            DataSet dts = new DataSet();
            DataSet dtsX = new DataSet(); 
            //dtsX.ReadXml(@"C:\Users\jesus\Desktop\PL__Receta_Electronica\ColectivoMedicamentos_V02____2K161130.xml");
            //dtsX = dts;


            System.IO.StreamReader reader = new System.IO.StreamReader(sFilePath);
            Informacion_XML = reader.ReadToEnd();
            reader.Close();
            reader = null;

            tpProceso = receta.TipoDeDocumento(Informacion_XML);


            if (tpProceso == TipoProcesoReceta.SurteReceta)
            {
                sRegresa = receta.Guardar(Informacion_XML).GetString();
            }

            if (tpProceso == TipoProcesoReceta.ColectivoMedicamentos)
            {
                colectivo.CLUES_Default = "";
                colectivo.CLUES_NombreUnidad_Default = "";
                sRegresa = colectivo.Guardar(Informacion_XML).GetString();
            }


            General.msjUser(sRegresa); 
        }
    }
}
