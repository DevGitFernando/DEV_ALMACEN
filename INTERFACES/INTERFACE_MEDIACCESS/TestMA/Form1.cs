using System;
using System.Collections; 
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using TestMA.wsIMediaccess;

using SC_SolutionsSystem;

using Dll_SII_IMediaccess;
using Dll_SII_IMediaccess.Clases;

namespace TestMA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            wsServiciosMediaccess web = new wsServiciosMediaccess();
            RecetaElectronica rec = new RecetaElectronica(General.DatosConexion); 
            string sRegresa = "";
            string sDatos_Receta = ""; 
            //ResponsePublicacionReM respuesta = null;
            
            //ArrayList listaProductos = new ArrayList();
            //ProductoReceta[] productosReceta = null; 
            //ProductoReceta producto = null;
            //int iProducto = 0; 


            //producto = new ProductoReceta();
            //producto.Id = 1;
            //producto.Cantidad = 5;
            //listaProductos.Add(producto);

            //producto = new ProductoReceta();
            //producto.Id = 11;
            //producto.Cantidad = 15;
            //listaProductos.Add(producto);

            //producto = new ProductoReceta();
            //producto.Id = 21;
            //producto.Cantidad = 25;
            //listaProductos.Add(producto);

            //producto = new ProductoReceta();
            //producto.Id = 31;
            //producto.Cantidad = 35;
            //listaProductos.Add(producto);


            //productosReceta = new ProductoReceta[listaProductos.Count];
            //foreach (ProductoReceta p in listaProductos)
            //{
            //    productosReceta[iProducto] = p;
            //    iProducto++; 
            //}

            web.Url = "http://localhost/wsMA/Public/wsServiciosIMediaccess.asmx";
            web.Url = "http://intermedcom.cloudapp.net/wsIMediaccess_test/wsServiciosIMediaccess.asmx";

            sDatos_Receta = "<datos_receta><producto><id>3054</id><cantidad>1</cantidad></producto><producto><id>1029</id><cantidad>10</cantidad></producto></datos_receta>";
            sRegresa = web.PublicacionReM(210, "001090011", "Pancho Lopez", "Pancho Pantera", "Todologo", 0, "Cobertura Amplia", "2016-02-04", "EE500010002", "A009", "", "", "", sDatos_Receta);


            sDatos_Receta = "<datos_receta><producto><id>3054</id><cantidad>1</cantidad></producto><producto><id>1029</id><cantidad>10</cantidad></producto></datos_receta>";
            sRegresa = web.PublicacionReM(500005800860, "61204", "MA. FERNANDA RODRIGUEZ HERNANDEZ", "DR.Bocanegra Hernández Rubén Dario",
                "Medicina Interna", 0, "PRODUCTOS SIS NOVA", "2016-02-15", "E006529584", "A009", "", "", "", sDatos_Receta);
            
            
            ////sRegresa = web.PublicacionReM(21, "001090011", "Pancho Lopez", "Pancho Pantera", "Todologo", 0, "Cobertura Amplia", "2016-02-04", "E500010002", "A009", "", "", "", sDatos_Receta);

            ////rec.Guardar(21, "001090011", "Pancho Lopez", "Pancho Pantera", "Todologo", 0, "Cobertura Amplia", "2016-02-04", "E500010002", "A009", "", "", "", sDatos_Receta);

            //////////respuesta = web.PublicacionReM(0, "", "", "", "", 0, "", "2016-02-04", "", "", "", "", "", null);

            //General.msjUser(respuesta.Error); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            wsServiciosMediaccess web = new wsServiciosMediaccess();
            string sRegresa = "";

            web.Url = "http://localhost/wsMA/Public/wsServiciosIMediaccess.asmx";
            web.Url = "http://intermedcom.cloudapp.net/wsIMediaccess/wsServiciosIMediaccess.asmx";
            web.Url = "http://intermedcom.cloudapp.net/wsIMediaccess_Test/wsServiciosIMediaccess.asmx";

            sRegresa = web.BusquedaMedicamentosxID(0, 1, "parace 500", "002090011");
            General.msjUser(sRegresa); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
