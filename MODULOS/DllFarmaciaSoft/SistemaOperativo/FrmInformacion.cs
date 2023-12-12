using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales; 
//using SC_ControlsCS;

using DllFarmaciaSoft.SistemaOperativo; 

namespace DllFarmaciaSoft.SistemaOperativo
{
    public partial class FrmInformacion : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsWebLeer leerWeb; 
        clsGridView grid;
        clsListView lst; 

        ArrayList pModulos = new ArrayList(); 

        bool bRegistrarVersiones = false;

        string BaseVersiones = "If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_Terminales_Versiones' and xType = 'U' ) " +
                   " Begin  " +
                   " CREATE TABLE dbo.CFG_Terminales_Versiones (  " +
                   "     [Servidor] [varchar](100) COLLATE Latin1_General_CI_AI NOT NULL DEFAULT (''),  " +
                   "     [Dll] [varchar](100) COLLATE Latin1_General_CI_AI NOT NULL DEFAULT (''),  " +
                   "     [Version] [varchar](50) COLLATE Latin1_General_CI_AI NOT NULL DEFAULT (''),  " +
                   "     [FechaSistema] [smalldatetime] NOT NULL DEFAULT (getdate()),  " +
                   "     [HostName] [varchar](100) COLLATE Latin1_General_CI_AI NOT NULL DEFAULT (host_name()),  " +
                   "     [MAC_Address] [varchar](100) COLLATE Latin1_General_CI_AI NOT NULL DEFAULT (''),  " +
                   "     [Keyx] int identity(1,1)  " +
                   " ) ON [PRIMARY] " +
                   " End ";


        string BaseVersiones_TV = "If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_Terminales_Versiones_TV' and xType = 'U' ) " +
                   " Begin  " +
                   " CREATE TABLE dbo.CFG_Terminales_Versiones_TV (  " +
                   "     [HostName] [varchar](100) COLLATE Latin1_General_CI_AI NOT NULL DEFAULT (host_name()),  " +
                   "     [MAC_Address] [varchar](20) COLLATE Latin1_General_CI_AI NOT NULL DEFAULT (''),  " +
                   "     [Version_TV] [varchar](20) COLLATE Latin1_General_CI_AI NOT NULL DEFAULT (''),  " +
                   "     [ID_TV] [varchar](20) COLLATE Latin1_General_CI_AI NOT NULL DEFAULT ('')  " +
                   " ) ON [PRIMARY] " +
                   " End ";  

        public FrmInformacion()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn); 
            //grid = new clsGridView(ref grdModulos);
            lst = new clsListView(lvwModulos);


            clsDatosCliente DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "FrmInformacion");
            leerWeb = new clsWebLeer(General.Url, General.ArchivoIni, DatosCliente); 
        }

        public FrmInformacion(bool RegistroDeVersiones)
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn); 
            //grid = new clsGridView(ref grdModulos);
            lst = new clsListView(lvwModulos);

            clsDatosCliente DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "FrmInformacion");
            leerWeb = new clsWebLeer(General.Url, General.ArchivoIni, DatosCliente); 

            bRegistrarVersiones = RegistroDeVersiones; 
        }

        #region Propiedades 
        public bool RegistrarVersiones
        {
            get { return bRegistrarVersiones; }
            set { bRegistrarVersiones = value; }
        }
        #endregion Propiedades

        #region Funciones y Procedimientos Publicos  
        public void Procesar() 
        {
            if (bRegistrarVersiones)
            {
                string sSql = "";
                string sSvr = General.DatosConexion.Servidor;
                if (!leerWeb.Exec(BaseVersiones))
                {
                    Error.GrabarError(leerWeb, "Procesar()");
                }
                else
                {
                    RegistrarMAC_Terminales();
                    Registrar_MAC_TV(); 

                    foreach (clsDatosApp d in pModulos)
                    {
                        sSql = string.Format(
                            "If Not Exists ( Select * From CFG_Terminales_Versiones (NoLock) " +
                            "   Where Dll = '{0}' and Version = '{1}' and HostName = '{2}' and MAC_Address = '{3}' ) " +
                            "   Begin " +
                            "       Insert Into CFG_Terminales_Versiones ( Servidor, Dll, Version, HostName, MAC_Address ) " +
                            "              Values ( '{4}', '{0}', '{1}', '{2}', '{3}' ) " +
                            "   End ",
                            d.Modulo, d.Version, General.NombreEquipo, General.MacAddress, sSvr);

                        if (!leerWeb.Exec(sSql))
                        {
                            Error.GrabarError(leerWeb, "Registro"); 
                        }
                    } 
                }
            } 
        }

        private void RegistrarMAC_Terminales()
        {
            string sSql = ""; 
            sSql = string.Format(
                "If Not Exists ( Select * From CFGC_Terminales (NoLock) " +
                "   Where Nombre = '{0}' and MAC_Address = '{1}' ) " +
                "   Begin " +
                "       Insert Into CFGC_Terminales ( IdTerminal, Nombre, MAC_Address, EsServidor, Status, Actualizado ) " +
                // "              Values ( '{4}', '{0}', '{1}', '{2}', '{3}' ) " + 
                "       Select right('0000' + cast(IsNull( ( Select max(cast(IdTerminal as int)) + 1 From CFGC_Terminales (NoLock) ), 1)as varchar),4) as IdTerminal, " + 
                "              '{0}' as Host, '{1}' as MAC_Address, 0 as EsServidor, 'A' as Status, 0 As Actualizado " + 
                "   End ", 
                General.NombreEquipo, General.MacAddress );

            if (!leerWeb.Exec(sSql))
            {
                Error.GrabarError(leerWeb, "RegistrarMAC_Terminales()");
            }
        }

        private void Registrar_MAC_TV()
        {
            string sSql = ""; 
            clsLeer leer = new clsLeer(); 
            clsVersionesTeamViewer team = new clsVersionesTeamViewer();
            team.Obtener_IDS();

            leer.DataSetClase = team.ListaID_TV;
            if (leerWeb.Exec(BaseVersiones_TV))
            {
                while (leer.Leer())
                {
                    sSql = string.Format(
                        "If Not Exists ( Select * From CFG_Terminales_Versiones_TV (NoLock) " +
                        "   Where MAC_Address = '{0}' and ID_TV = '{3}' ) " +
                        "   Begin " +
                        "       Insert Into CFG_Terminales_Versiones_TV ( HostName, MAC_Address, Version_TV, ID_TV ) " +
                        "              Values ( '{0}', '{1}', '{2}', '{3}' ) " +
                        "   End ",
                        leer.Campo("Host_Name"), leer.Campo("MAC_Address"), leer.Campo("Version_TV"), leer.Campo("ID_TV"));
                    if (!leerWeb.Exec(sSql))
                    {
                        Error.GrabarError(leerWeb, "Registro_TV");
                    }
                }
            }
        }

        public void Add(string Modulo)
        {
            clsDatosApp daModulo = CrearInstanciaSNK(Modulo); 
            pModulos.Add(daModulo); 
        }

        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados
        private clsDatosApp CrearInstanciaSNK(string Modulo)
        {
            clsDatosApp obj = new clsDatosApp(Modulo, "0.0.0.0");

            try
            {
                obj = new clsDatosApp(Modulo); 
                ////Assembly AssemblyCargado = Assembly.Load(Modulo); 
                ////AssemblyName x = new AssemblyName(AssemblyCargado.FullName);
                ////obj = new clsDatosApp(x.Name, x.Version.ToString()); 
            }
            catch 
            {
            }

            return obj;
        }        
        #endregion Funciones y Procedimientos Privados

        private void FrmInformacion_Load(object sender, EventArgs e)
        {
            //grid.Limpiar();
            lst.LimpiarItems();
            ListViewItem itmX = null;

            foreach (clsDatosApp d in pModulos)
            {
                //grdModulos.Rows.Add(d.Modulo, d.Version);
                itmX = lvwModulos.Items.Add(d.Modulo);
                itmX.SubItems.Add("" + d.Version);
                itmX.SubItems.Add("" + d.InformacionAdicionalVersion);
            }

            lst.Ordenar(1); 
        }
    }
}
