using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;

using System.Threading;

namespace Planeacion.ObtenerInformacion
{
    public partial class FrmDescargarInformacionOperativa : FrmBaseExt
    {
        enum Cols
        {
            Ninguno = 0,
            IdEstado = 1,
            Fecha = 2,
            Consumos = 3,
            FechaActualizado = 4,
            Actualizar = 5
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsConsultas Consultas;
        clsLeer leer;
        clsLeer leertemp;
        clsLeer leerRegionales;
        clsGrid grid;
        Thread thPCM;
        clsDatosCliente datosCliente;


        bool bBuscarExistencia = false;

        string sFecha = "";
        int iRow = 0;

        int iMeses = 0;
        int iCaducidad = 0;

        public FrmDescargarInformacionOperativa()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            leerRegionales = new clsLeer(ref cnn);
            leertemp = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            grid = new clsGrid(ref grdConsumos, this);

            datosCliente = new clsDatosCliente(GnInventarios.DatosApp, this.Name, "FrmObtenerConsumos");
        }

        private void FrmObtenerConsumos_Load(object sender, EventArgs e)
        {
            CargarComboEstados();
            limpiar(true);
        }

        private void limpiar(bool IniciarControles)
        {
            if (IniciarControles)
            {
                Fg.IniciaControles();
            } 
            nmMeses.Text = 6.ToString();
            grid.Limpiar(false);

            cboEstados.Focus();
            nmMesesCaducidad.Value = 3;

            lblFolio.Visible = false;
            txtFolio.Visible = false;

            IniciarToolBar(true, false, false);
        }

        private void IniciarToolBar(bool Nuevo, bool Ejecutar, bool GenerarPlaneacion)
        {
            btnNuevo.Enabled = Nuevo;
            btnEjecutar.Enabled = Ejecutar;
            btnGenerarPlaneacion.Enabled = false;

            if (!txtFolio.Visible)
            {
                btnGenerarPlaneacion.Enabled = GenerarPlaneacion;
            }
        }



        private void CargarComboEstados()
        {
            cboEstados.Clear();
            cboEstados.Add();

            cboEstados.Add(Consultas.EstadosConFarmacias("CargarComboEstados()"), true, "IdEstado", "NombreEstado");

            cboEstados.SelectedIndex = 0;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            limpiar(true);
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (cboEstados.Data == "0")
            {
                General.msjAviso("Seleccione estado por favor.");
            }
            else
            {
                BloquearControles(true);
                CargarConsumos();
                BloquearControles(false);
                IniciarToolBar(true, true, true);
            }
        }

        private void CargarConsumos()
        {
            string sFolio = "";

            txtFolio.Text = sFolio;
            txtFolio.Visible = false;
            lblFolio.Visible = false;

            string sSql = string.Format(" Exec spp_PLN_OCEN_Resumen_Estado @IdEstado = '{0}', @NumMeses = '{1}'", 
                    cboEstados.Data, nmMeses.Text);


            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarConsumos()");
                General.msjError("Ocurrió un error al obtener la información.");
            }
            else
            {
                grid.Limpiar(false);
                grid.LlenarGrid(leer.DataSetClase);

                leertemp.DataTableClase = leer.Tabla(2);

                if(leertemp.Leer())
                {
                    lblExistencia.Text = leertemp.CampoDouble("Existencia").ToString("##,###,###,###,###,###,##0.###0");
                    lblExistencia.BackColor = Color.WhiteSmoke;
                    lblFechaRegistro.Text = leertemp.CampoFecha("FechaRegistro").ToString();

                    iMeses = Convert.ToInt32(nmMeses.Value);
                    iCaducidad = leertemp.CampoInt("MesesCaducidad");

                    nmMesesCaducidad.Value = iCaducidad;
                }

                leertemp.DataTableClase = leer.Tabla(3);

                if (leertemp.Leer())
                {
                    sFolio = leertemp.Campo("Folio");

                    if (sFolio != "*")
                    {
                        txtFolio.Text = sFolio;
                        txtFolio.Visible = true;
                        lblFolio.Visible = true;
                    }
                }

            }
        }

        private void grdConsumos_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

        }

        private void grdConsumos_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            Cols columna = (Cols)e.Column + 1;
            iRow = e.Row + 1;
            sFecha = grid.GetValue(iRow, (int)Cols.Fecha);

            switch (columna)
            {


                case Cols.Actualizar:
                    bBuscarExistencia = false;
                    ObtenerInformacion();
                    break;

                default:
                    break;
            }

        }


        private void ObtenerInformacion()
        {
                thPCM = new Thread(this.ThObtenerPCM);
                thPCM.Name = "ObtenerFaltantes_Juris";
                thPCM.Start();

        }


        private void ThObtenerPCM()
        {
            bool bOcurrioError = false;

            string sUrlServidorRegional = "";

            BloquearControles(true);

            string sSql = string.Format(" Select * From vw_Regionales_Urls Where StatusUrl = 'A' And StatusRelacion = 'A' And FarmaciaStatus = 'A' And IdEstado = '{0}' ",
               cboEstados.Data );

            if (leerRegionales.Exec(sSql))
            {
                while (leerRegionales.Leer())
                {
                    sUrlServidorRegional = leerRegionales.Campo("UrlFarmacia");

                    Obtener_PCM(sUrlServidorRegional, sFecha, iRow);
                }
            }

            BloquearControles(false);
            IniciarToolBar(true, true, true);
        }


        private void BloquearControles(bool bBloquear)
        {
            grid.BloqueaGrid(bBloquear);
            nmMesesCaducidad.Enabled = !bBloquear;
            nmMeses.Enabled = !bBloquear;
            btnActulizarExistencia.Enabled = !bBloquear;

            if (bBloquear)
            {
                IniciarToolBar(!bBloquear, !bBloquear, !bBloquear);
            }


            cboEstados.Enabled = !bBloquear;
        }

        private bool Obtener_PCM(string sUrlServidorRegional, string sFecha, int iRenglon)
        {
            bool bRegresa = false;
            string sSql = "";
            int iNumeroMesesExistencia = 1;
            DateTime dtpFecha = General.FechaSistema;

            DataSet dtsPCM = new DataSet();
            clsLeerWebExt leerWeb = new clsLeerWebExt(sUrlServidorRegional, DtGeneral.CfgIniOficinaCentral, datosCliente);

            if (bBuscarExistencia)
            {
                sSql = string.Format("Exec spp_PLN_OCEN_ObtenerInformacion_Existencia @IdEstado = '{0}', @MesesCaducidad = {1}", cboEstados.Data, nmMesesCaducidad.Value);
            }
            else
            {
                sSql = string.Format("Exec spp_PLN_OCEN_ObtenerInformacion_ResumenPorMes @IdEstado = '{0}', @Año = {1}, @Mes = {2} \n \n ",
                                cboEstados.Data, sFecha.Substring(0, 4), sFecha.Substring(5, 2));
            }

            if (!leerWeb.Exec(sSql))
            {
                Error.GrabarError(General.DatosConexion, leerWeb.Error, "ObtenerConsumos");
                grid.ColorRenglon(iRenglon, Color.Red);
            }
            else
            {
                if (leerWeb.Leer())
                {
                    bRegresa = true;
                    //leer.DataSetClase = leerWeb.DataSetClase;

                    bRegresa = GrabarLocal(leerWeb.DataSetClase);
                }
            }

            return bRegresa;
        }

        private bool GrabarLocal(DataSet DataSet)
        {
            bool bRegresa = false;
            string sFormatoYMD = "Set DateFormat YMD \n\n";

            string sSql = "";



            clsLeer LeerLocal = new clsLeer();

            LeerLocal.DataSetClase = DataSet;

            if (!cnn.Abrir())
            {
                Error.LogError(cnn.MensajeError);
                General.msjErrorAlAbrirConexion();
            }
            else
            {
                bRegresa = true;
                cnn.IniciarTransaccion();


                if (bBuscarExistencia)
                {
                    sSql = string.Format("Delete PLN_OCEN_Existencia Where Idestado = '{0}'", cboEstados.Data);

                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                    }

                }

                if (bRegresa)
                {
                    for (int i = 1; LeerLocal.Leer() && bRegresa; i++)
                    {
                        sSql = sFormatoYMD;
                        sSql += LeerLocal.Campo("Resultado");

                        if (i > 100)
                        {
                            //sSql += "\n\nGo--#SQL ";
                            i = 1;
                        }

                        if (!leer.Exec(sSql))
                        {
                            bRegresa = false;
                        }
                    }
                }
            }

           if (!bBuscarExistencia)
            {
                sSql = string.Format("	Update O Set Consumo =  (Select Sum(Total) As Total From PLN_OCEN_Resumen_Por_Mes (NoLock) Where IdEstado = '{0}' And Año = '{1}' And Mes = '{2}'), FechaRegistro = GetDate() " +
                    "From PLN_OCEN_Resumen_Estado O Where IdEstado = '{0}' And Fecha = '{1}-{2}'", cboEstados.Data, sFecha.Substring(0, 4), sFecha.Substring(5, 2));

                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                }
            }



            if (bRegresa)
            {
                cnn.CompletarTransaccion();
                CargarConsumos();
            }
            else
            {
                cnn.DeshacerTransaccion();
                Error.GrabarError(leer, "GrabarLocal");

                if (!bBuscarExistencia)
                {
                    grid.ColorRenglon(iRow, Color.Red);
                }
                else
                {
                    lblExistencia.BackColor = Color.Red;
                }
            }
            cnn.Cerrar();




            return bRegresa;
        }

        private void btnActulizarExistencia_Click(object sender, EventArgs e)
        {
            bBuscarExistencia = true;
            thPCM = new Thread(this.ThObtenerPCM);
            thPCM.Name = "ObtenerFaltantes_Juris";
            thPCM.Start();
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            //limpiar(false);

            if (cboEstados.Data == "0")
            {
                IniciarToolBar(true, false, false);
            }
            else
            {
                IniciarToolBar(true, true, false);
            }
        }

        private void btnGenerarPlaneacion_Click(object sender, EventArgs e)
        {
            FrmGenerarPlaneacionAutomatica f = new FrmGenerarPlaneacionAutomatica(iMeses, iCaducidad);
            f.ShowDialog();

            CargarConsumos();
            IniciarToolBar(true, true, true);
        }

        private void nmMeses_ValueChanged(object sender, EventArgs e)
        {
            IniciarToolBar(true, true, false);
        }

        private void nmMesesCaducidad_ValueChanged(object sender, EventArgs e)
        {
            IniciarToolBar(true, true, false);
        }
    }
}
