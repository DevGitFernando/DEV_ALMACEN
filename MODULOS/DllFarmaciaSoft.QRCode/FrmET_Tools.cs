using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem.QRCode.Codec;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft; 


namespace DllFarmaciaSoft.QRCode
{
    public partial class FrmET_Tools : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsListView lst;

        public FrmET_Tools()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            lst = new clsListView(listvwDatos); 
        }

        private void FrmET_Tools_Load(object sender, EventArgs e)
        {
            CargarDatos(); 
        } 

        private void CargarDatos()
        {
            string sSql = "Select IdEstado, IdFarmacia, Folio, FechaRegistro " +
                "From QR_Etiquetas (NoLock) " + 
                "Order by IdEstado, IdFarmacia, Folio "; 

            lst.Limpiar();
            if (!leer.Exec(sSql))
            {
            }
            else
            {
                lst.CargarDatos(leer.DataSetClase, true, true); 
            } 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sSql =
                "Select IdPasillo, DescripcionPasillo, IdEstante, DescripcionEstante, IdEntrepaño, DescripcionEntrepaño " +
                "From vw_Pasillos_Estantes_Entrepaños (NoLock) " + 
                "-- Where IdPasillo = 1 and IdEstante = 1  ";

            sSql = "Select * From CatEstados (NoLock) Order By IdEstado ";
            sSql += "Select * From vw_Farmacias (NoLock) Order By IdEstado, IdFarmacia ";

            if (QR_General.Guardar(sSql))
            {
                QR_General.View_Encode();
            }

            CargarDatos(); 
        } 

        private void button2_Click(object sender, EventArgs e)
        {
            QR_General.AppTest(); 
        }

        private void listvwDatos_DoubleClick(object sender, EventArgs e)
        {
            DataSet dts = new DataSet(); 
            string sIdEstado = lst.GetValue(1);
            string sIdFarmacia = lst.GetValue(2);
            string sFolio = lst.GetValue(3);

            QR_General.IdEstado = sIdEstado;
            QR_General.IdFarmacia = sIdFarmacia;
            QR_General.Folio = sFolio;
            dts = QR_General.Leer();

            FrmQR_Reader f = new FrmQR_Reader(dts);
            f.ShowDialog(); 
        }

        private void button3_Click(object sender, EventArgs e)
        {

        } 
    }
}
