using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;

using DllFarmaciaSoft;

namespace DllFarmaciaSoft.Devoluciones
{
    public partial class FrmListadoMotivosDev : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        clsGrid Grid;

        string sIdTipoMovto = "";

        DataSet dtsRetorno = new DataSet();
        DataSet dtsMotivos = new DataSet();
        bool bBloquearCaptura = false; 

        bool bMarcoMotivos = false;

        private enum Cols
        {
            Ninguna = 0, IdTipoMovto = 1, IdMotivo = 2, Motivo = 3, Marcar = 4, Status = 5  
        }

        public FrmListadoMotivosDev()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.Modulo, DtGeneral.Version, this.Name);

            Grid = new clsGrid(ref grdMotivos, this);
            Grid.EstiloGrid(eModoGrid.ModoRow);
            Grid.AjustarAnchoColumnasAutomatico = true;

            this.Width = 670;
            this.Height = 380; 
        }

        private void FrmListadoMotivosDev_Load(object sender, EventArgs e)
        {
            bMarcoMotivos = false;
        }

        #region Propiedades
        public bool MarcoMotivos
        {
            get { return bMarcoMotivos; }
            set { bMarcoMotivos = value; }
        }

        public DataSet Retorno
        {
            get { return dtsRetorno; }
            set { dtsRetorno = value; }
        }
        #endregion Propiedades

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Insertar_Motivos_Retorno();
            this.Close();
        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            bMarcoMotivos = false;
            Grid.Limpiar(true);
        }

        private bool CargarDatos()
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format(" Select * From Movtos_Inv_Tipos Where IdTipoMovto_Inv = '{0}' ", sIdTipoMovto);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarDatos");
                General.msjError("Ocurrio un error al consultar el tipo de movimiento.");
                bRegresa = false;
            }
            else
            {
                if (leer.Leer())
                {
                    lblIdTipo.Text = sIdTipoMovto;
                    lblDescripcion.Text = leer.Campo("Descripcion");
                    bRegresa = CargarMotivos();
                }
                else
                {
                    General.msjUser("No se encontro el tipo de movimiento.");
                    bRegresa = false;
                }
            }

            return bRegresa;
        }

        private bool CargarMotivos()
        {
            bool bRegresa = true;
            
            leer.DataSetClase = dtsMotivos;

            if (leer.Leer())
            {
                Grid.LlenarGrid(leer.DataSetClase, false, false);
                ////Grid.BloqueaGrid(false); 
                ////Grid.BloqueaGrid(bBloquearCaptura); 
            }
            else 
            {
                General.msjUser("No se encontro cuestionario de motivos para la opción solicitada.");
                bRegresa = false;
            }


            ///// Deshabilitar los Cancelados 
            for (int i = 1; i <= Grid.Rows; i++)
            {
                if (!Grid.GetValueBool(i, (int)Cols.Status))
                {
                    Grid.BloqueaCelda(true, i, (int)Cols.Marcar); 
                }
            }

            return bRegresa;
        }

        public void MostrarPantalla(DataSet dts, string IdTipoMovto, bool BloquearCaptura)
        {
            this.dtsMotivos = dts;
            this.sIdTipoMovto = IdTipoMovto;
            this.bBloquearCaptura = BloquearCaptura; 
            Grid.Limpiar(true);

            if (!CargarDatos())
            {               
                this.Hide();
            }

        }
        #endregion Funciones

        #region Guarda_Informacion_Motivos

        #endregion Guarda_Informacion_Motivos

        #region Guardar_Salir_Pantalla        
        private void Insertar_Motivos_Retorno()
        {
            dtsMotivos = new DataSet();
     
            dtsMotivos = clsMotivosDevoluciones.PreparaDtsMotivos();

            for (int i = 1; i <= Grid.Rows; i++)
            {

                object[] objRow = {                   
                Grid.GetValue(i, (int)Cols.IdTipoMovto),
                Grid.GetValue(i, (int)Cols.IdMotivo), 
                Grid.GetValue(i, (int)Cols.Motivo),
                Grid.GetValueInt(i, (int)Cols.Marcar), 
                Grid.GetValueInt(i, (int)Cols.Status)
                                  }; 
                dtsMotivos.Tables[0].Rows.Add(objRow);

                if (Grid.GetValueInt(i, (int)Cols.Marcar) == 1)
                {
                    bMarcoMotivos = true;
                }
                
            }

            dtsRetorno = dtsMotivos;

            
        }
        #endregion Guardar_Salir_Pantalla

        private void FrmListadoMotivosDev_FormClosing(object sender, FormClosingEventArgs e)
        {
            Insertar_Motivos_Retorno();
        }
    }
}
