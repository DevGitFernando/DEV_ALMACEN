using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;

namespace DllRecetaElectronica.Catalogos
{
    public partial class FrmAMPM_FirmasDigitales_Medicos : FrmBaseExt
    {
        clsConexionSQL con = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        PictureBox picture = new PictureBox();
        string sNombreFirma = ""; 
        string sRutaFirma = "";
        string sBase64_Firma = ""; 

        public FrmAMPM_FirmasDigitales_Medicos()
        {
            InitializeComponent();

            leer = new clsLeer(ref con);
            Error = new clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name); 

        }

        private void FrmAMPM_FirmasDigitales_Medicos_Load(object sender, EventArgs e)
        {
            InicializarPantalla();
        }

        #region Botones 
        private void InicializarPantalla()
        {
            InicializarToolBar(false, true);

            picFirma.Image = null; 
            Fg.IniciaControles(); 
        }

        private void InicializarToolBar(bool Guardar, bool CargarFirma)
        {
            btnGuardar.Enabled = Guardar;
            btnCargarFirma.Enabled = CargarFirma; 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarPantalla(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sSql = string.Format("Exec spp_INT_AMPM__MedicosFirmas " +
                " @NombreFirma = '{0}', @FirmaDigital = '{1}' ", sNombreFirma, sBase64_Firma);

            if (!leer.Exec(sSql))
            {
                General.msjError("Ocurrió un error al cargar la firma digital.");
            }
            else
            {
                General.msjUser("Firma digital guardada satisfactoriamente.");
                InicializarPantalla(); 
            }
        }

        private void btnCargarFirma_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileOpen = new OpenFileDialog();
            fileOpen.Title = "";
            fileOpen.Multiselect = false;
            fileOpen.Filter = "(*.PNG;*.BMP;*.JPG;*.GIF)|*.PNG;*.BMP;*.JPG;*.GIF"; ; 

            if (fileOpen.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                sRutaFirma = fileOpen.FileName;

                sBase64_Firma = Fg.ConvertirArchivoEnStringB64(sRutaFirma); 

                picture.Image = Image.FromFile(sRutaFirma);
                picFirma.Image = picture.Image;

                InicializarToolBar(true, false); 
            }
        }
        #endregion Botones
    }
}
