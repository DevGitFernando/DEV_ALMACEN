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
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.Errores;

using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;

using Impinj.OctaneSdk;

namespace Dll_SII_IRFID.Demonio
{
    public partial class FrmTAGS_Invalidos : FrmBaseExt
    {
        clsListView lst;
        bool bCargandoDatos = false;
        ReaderTipo iTipoInformacion = ReaderTipo.Ninguno;
        int iTimer = 10; 

        public FrmTAGS_Invalidos(): this(ReaderTipo.Demonio) 
        { 
        }

        public FrmTAGS_Invalidos(ReaderTipo TipoInformacion)
        {
            InitializeComponent();

            iTipoInformacion = TipoInformacion;

            iTimer = iTipoInformacion == ReaderTipo.Monitor ? 1000 : 100;

            lst = new clsListView(lstTagsInvalidos);
            lst.PermitirAjusteDeColumnas = false;
            lst.LimpiarItems(); 
        }

        private void FrmTAGS_Invalidos_Load(object sender, EventArgs e)
        {

        }

        private void FrmTAGS_Invalidos_FormClosing(object sender, FormClosingEventArgs e)
        {
            Gn_RFID.Monitor_TAGS_Erroneos = false;
            tmGPO.Stop();
        }

        private void FrmTAGS_Invalidos_Shown(object sender, EventArgs e)
        {
            Gn_RFID.Monitor_TAGS_Erroneos = true;
            tmGPO.Interval = iTimer;
            tmGPO.Enabled = true;

            if (iTipoInformacion != ReaderTipo.Ninguno)
            {
                tmGPO.Start();
            }
        }

        private void tmGPO_Tick(object sender, EventArgs e)
        {
            CargarInformacion();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset_GPO();
        }

        private void FrmTAGS_Invalidos_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    Reset_GPO();
                    break;

                default:
                    break;
            }
        }

        private void Reset_GPO()
        {
            switch ( iTipoInformacion )
            {
                case ReaderTipo.Demonio:
                    Gn_RFID.DemonioRFID.AlertaApagar();
                    break;

                case ReaderTipo.Monitor:
                    Gn_RFID.MonitorRFID.AlertaApagar();
                    break;

                default:
                    break;
            }
        }

        private void CargarInformacion()
        {
            switch (iTipoInformacion)
            {
                case ReaderTipo.Demonio:
                    if (Gn_RFID.DemonioRFID.GPO_Encendido)
                    {
                        if (!bCargandoDatos)
                        {
                            bCargandoDatos = true;
                            lst.LimpiarItems();
                            lst.CargarDatos(Gn_RFID.DemonioRFID.ListadoDeTagsErroneos);
                            bCargandoDatos = false;
                        }
                    }
                    else
                    {
                        Gn_RFID.Monitor_TAGS_Erroneos = false;
                        this.Close();
                    }
                    break;

                case ReaderTipo.Monitor:
                    if (Gn_RFID.MonitorRFID.GPO_Encendido)
                    {
                        if (!bCargandoDatos)
                        {
                            bCargandoDatos = true;
                            lst.PermitirAjusteDeColumnas = true; 
                            lst.LimpiarItems();
                            lst.CargarDatos(Gn_RFID.MonitorRFID.ListadoDeTagsErroneos, true, true);
                            bCargandoDatos = false;

                            lst.AnchoColumna(1, 100);
                            lst.AnchoColumna(2, 200);
                            lst.AnchoColumna(3, 600);
                        }
                    }
                    else
                    {
                        Gn_RFID.Monitor_TAGS_Erroneos = false;
                        this.Close();
                    }
                    break;

                default:
                    break;
            }

            if (lst.Registros <= 0)
            {
                Gn_RFID.Monitor_TAGS_Erroneos = false;
                this.Close();
            }

        }
    }
}
