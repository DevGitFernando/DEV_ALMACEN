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

namespace DllFarmaciaSoft.OrdenesDeCompra
{
    public partial class FrmCheckListRecepcionProveedor : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        clsGrid Grid;

        DataSet dtsRetorno = new DataSet();
        DataSet dtsCheck = new DataSet();
                
        bool bFirma = false;
        bool bGuardo = false;
        private enum Cols
        {
            Ninguna = 0, IdGrupo = 1, IdMotivo = 2, Grupo = 3, Motivo = 4, SI = 5, SI_Firma = 6, 
            NO = 7, NO_Firma = 8, Rechazo = 9, Rechazo_Firma = 10, Comentario = 11  
        }

        public FrmCheckListRecepcionProveedor()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.Modulo, DtGeneral.Version, this.Name);

            Grid = new clsGrid(ref grdMotivos, this);
            Grid.EstiloGrid(eModoGrid.ModoRow);
            Grid.AjustarAnchoColumnasAutomatico = true;
        }

        private void FrmListadoMotivosDev_Load(object sender, EventArgs e)
        {
            bFirma = false;
        }

        #region Propiedades
        public bool Firma
        {
            get { return bFirma; }
            set { bFirma = value; }
        }

        public bool Guardo
        {
            get { return bGuardo; }
            set { bGuardo = value; }
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
            Insertar_Check_Retorno();
            this.Close();
        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            bFirma = false;
            Grid.Limpiar(true);
        }        

        private bool CargarCheck()
        {
            bool bRegresa = true;

            leer.DataSetClase = dtsCheck;

            if (leer.Leer())
            {
                Grid.LlenarGrid(leer.DataSetClase, false, false);
                Grid.BloqueaGrid(false);
            }
            else 
            {
                General.msjUser("No se encontro el check list para la opción solicitada.");
                bRegresa = false;
            }

            return bRegresa;
        }

        public void MostrarPantalla(DataSet dts)
        {
            this.dtsCheck = dts;
            Grid.Limpiar(true);
            bFirma = false;
            bGuardo = false;

            if (!CargarCheck())
            {               
                this.Hide();
            }

        }
        #endregion Funciones
        
        #region Guardar_Salir_Pantalla        
        private void Insertar_Check_Retorno()
        {
            dtsCheck = new DataSet();

            dtsCheck = clsCheckListRecepcionProveedor.PreparaDtsMotivos();

            for (int i = 1; i <= Grid.Rows; i++)
            {

                object[] objRow = {                   
                Grid.GetValue(i, (int)Cols.IdGrupo),
                Grid.GetValue(i, (int)Cols.IdMotivo),
                Grid.GetValue(i, (int)Cols.Grupo),
                Grid.GetValue(i, (int)Cols.Motivo),
                Grid.GetValueInt(i, (int)Cols.SI),
                Grid.GetValueInt(i, (int)Cols.SI_Firma),
                Grid.GetValueInt(i, (int)Cols.NO),
                Grid.GetValueInt(i, (int)Cols.NO_Firma),
                Grid.GetValueInt(i, (int)Cols.Rechazo),
                Grid.GetValueInt(i, (int)Cols.Rechazo_Firma),
                Grid.GetValue(i, (int)Cols.Comentario)
                                  };
                dtsCheck.Tables[0].Rows.Add(objRow);                
                
            }

            bFirma = Validar_Check();
            bGuardo = true;
            dtsRetorno = dtsCheck;
            
        }

        private bool Validar_Check()
        {
            bool bRegresa = false;
            int iSI = 0, iSI_Firma = 0, iNO = 0, iNO_Firma = 0, iRechazo = 0, iRechazo_Firma = 0;

            //////iSI_Firma = Grid.TotalizarColumna((int)Cols.SI_Firma);
            //////iNO_Firma = Grid.TotalizarColumna((int)Cols.NO_Firma);
            //////iRechazo_Firma = Grid.TotalizarColumna((int)Cols.Rechazo_Firma);

            //////bRegresa = (iSI_Firma + iNO_Firma + iRechazo_Firma) > 0 ? true : false;


            for (int i = 1; i <= Grid.Rows; i++)
            {
                iSI = 0; iSI_Firma = 0; iNO = 0; iNO_Firma = 0; iRechazo = 0; iRechazo_Firma = 0;

                iSI = Grid.GetValueInt(i, (int)Cols.SI);
                iSI_Firma = Grid.GetValueInt(i, (int)Cols.SI_Firma);
                iNO = Grid.GetValueInt(i, (int)Cols.NO);
                iNO_Firma = Grid.GetValueInt(i, (int)Cols.NO_Firma);
                iRechazo = Grid.GetValueInt(i, (int)Cols.Rechazo);
                iRechazo_Firma = Grid.GetValueInt(i, (int)Cols.Rechazo_Firma);

                if (iSI == 1)
                {
                    if (iSI_Firma == 1)
                    {
                        bRegresa = true;
                        break;
                    }
                }

                if (iNO == 1)
                {
                    if (iNO_Firma == 1)
                    {
                        bRegresa = true;
                        break;
                    }
                }

                if (iRechazo == 1)
                {
                    if (iRechazo_Firma == 1)
                    {
                        bRegresa = true;
                        break;
                    }
                }

            }

            return bRegresa;
        }
        #endregion Guardar_Salir_Pantalla

        #region Evento_Salir
        private void FrmListadoMotivosDev_FormClosing(object sender, FormClosingEventArgs e)
        {
            Insertar_Check_Retorno();
        }
        #endregion Evento_Salir

        #region Eventos_Grid
        private void grdMotivos_EditModeOff(object sender, EventArgs e)
        {
            Cols colActiva = (Cols)Grid.ActiveCol;

            switch (colActiva)
            {
                case Cols.SI:
                    {
                        if (Grid.GetValueBool(Grid.ActiveRow, (int)Cols.SI))
                        {
                            Grid.SetValue(Grid.ActiveRow, (int)Cols.NO, false);
                            Grid.SetValue(Grid.ActiveRow, (int)Cols.Rechazo, false);
                        }
                    }
                    break;
                case Cols.NO:
                    {
                        if (Grid.GetValueBool(Grid.ActiveRow, (int)Cols.NO))
                        {
                            Grid.SetValue(Grid.ActiveRow, (int)Cols.SI, false);
                            Grid.SetValue(Grid.ActiveRow, (int)Cols.Rechazo, false);
                        }
                    }
                    break;
                case Cols.Rechazo:
                    {
                        if (Grid.GetValueBool(Grid.ActiveRow, (int)Cols.Rechazo))
                        {
                            Grid.SetValue(Grid.ActiveRow, (int)Cols.SI, false);
                            Grid.SetValue(Grid.ActiveRow, (int)Cols.NO, false);
                        }
                    }
                    break;
            }
        }
        #endregion Eventos_Grid        
    }
}
