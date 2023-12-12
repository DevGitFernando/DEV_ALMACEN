using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


using TestInformacion.wsInformacion;


namespace TestInformacion
{
    public partial class FrmTest : Form
    {
        public FrmTest()
        {
            InitializeComponent();
        }

        private void FrmTest_Load(object sender, EventArgs e)
        {
            Limpiar(); 
        }

        private void Limpiar()
        {
            txtIdEstado.Text = "20";
            txtIdFarmacia.Text = "0118";
            rdoCuadroBasico.Checked = true;
            txtClaveSSA.Text = "";

            txtUrl.Text = "intermed.homeip.net";

            lstvwResultados.Clear(); 
            ////dgResultados.DataSource = null;
            ////dgResultados.Rows.Clear(); 
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {

            DataSet dts = new DataSet();
            DataTable dtTable = new DataTable();

            // int iIdAcceso = Convert.ToInt32(txtIdAcceso.Text);
            string sFechaInicial = "";
            string sFechaFinal = "";

            //// Dirección web donde se aloja el servicio web.  
            string sUrl = string.Format("http://{0}/wsEnlaceFarmacia/wsInformacion.asmx", txtUrl.Text.Trim());
            // sUrl = "http://lapJesus/wsEnlaceFarmacia/wsInformacion.asmx";


            wsInformacion.wsInformacion inf = new TestInformacion.wsInformacion.wsInformacion();
            inf.Url = sUrl;  

            try
            {
                lstvwResultados.Clear(); 
                if (rdoCuadroBasico.Checked)
                {
                    //// Ejecutar consulta  
                    dts = inf.CuadroBasico(txtIdEstado.Text, txtIdFarmacia.Text); 
                dtTable = dts.Tables[0];
                }
                
                if ( rdoExistenciaClave.Checked ) 
                {
                    dts = inf.Existencia_Clave(txtIdEstado.Text, txtIdFarmacia.Text, txtClaveSSA.Text);
                    dtTable = dts.Tables[0];
                }

                if (rdoExistenciaClaveGrupos.Checked)
                {
                    dts = inf.Existencia_Clave_Grupo(txtIdEstado.Text, txtIdFarmacia.Text, txtClaveSSA.Text);
                    dtTable = dts.Tables[0];
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Error");
            }

            try
            {
                CargarResultados(dtTable); 
                // lstvwResultados.DataBindings = dgResultados.DataBindings; 
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al mostrar los datos");
            }

        }

        private void CargarResultados(DataTable Datos)
        {
            bool bRegresa = false;

            if (Datos != null)
            {
                lstvwResultados.Clear();
                foreach (DataColumn dc in Datos.Columns)
                {
                    lstvwResultados.Columns.Add(dc.ColumnName, 100, HorizontalAlignment.Left);
                }

                //Add each Row as a ListViewItem        
                foreach (DataRow dr in Datos.Rows)
                {
                    object[] items = dr.ItemArray; 

                    ListViewItem lvi = new ListViewItem(items[0].ToString()); 
                    lstvwResultados.Items.Add(lvi);

                    for(int i = 1; i<= items.Length-1; i++)
                    {
                        lvi.SubItems.Add(items[i].ToString());
                    }
                }

                lstvwResultados.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                // pListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            }
        }
    }
}
