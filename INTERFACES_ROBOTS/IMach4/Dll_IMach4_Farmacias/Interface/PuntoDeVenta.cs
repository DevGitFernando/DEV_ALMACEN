using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Data;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Diagnostics;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

namespace Dll_IMach4.Interface
{
    public class PuntoDeVenta
    {
        #region Declaracion de Variables
        string Name = "Dll_IMach4.Interface.PuntoDeVenta";
        clsConexionSQL cnn;
        clsLeer leer;
        clsGrabarError Error;

        bool bPtoVtaOperable = false;
        bool bEsClienteIMach4 = IMach4.EsClienteIMach4;
        string sFolioSolicitud = "";
        int iRequerimiento = 0;
        bool bRequisicionRegistrada = false;

        #endregion Declaracion de Variables

        #region Constructores y Descructor de Clase
        public PuntoDeVenta()
        {
            cnn = new clsConexionSQL(General.DatosConexion);
            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, IMach4.DatosApp, Name);
        }

        ~PuntoDeVenta()
        {
        }
        #endregion Constructores y Descructor de Clase

        #region Propiedades Publicas
        public string FolioSolicitud
        {
            get { return sFolioSolicitud; }
        }

        public bool PtoVtaOperable
        {
            get { return bPtoVtaOperable; }
        }

        public bool RequisicionRegistrada
        {
            get { return bRequisicionRegistrada; }
        }

        #endregion Propiedades Publicas

        #region Funciones y Procedimientos Publicos
        public string ObtenerFolioSolicitud()
        {
            return ObtenerFolioSolicitud("*", "");
        }

        public bool TerminarSolicitud(string FolioVenta)
        {
            ObtenerFolioSolicitud(sFolioSolicitud, FolioVenta);
            return bPtoVtaOperable;
        }

        public bool TerminarSolicitud(string Folio, string FolioVenta)
        {
            ObtenerFolioSolicitud(Folio, FolioVenta);
            return bPtoVtaOperable;
        }

        public void Show(string IdProducto, string CodigoEAN)
        {
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados
        private void PrepararConexiones(ref clsLeer Exec)
        {
            //leer = Exec;
            //Error = new clsGrabarError(Exec.DatosConexion, IMach4.DatosApp, Name);
        }

        private string ObtenerFolioSolicitud(string Folio, string FolioVenta)
        {
            return sFolioSolicitud;
        }

        private bool RegistrarSolicitud(string IdProducto, string CodigoEAN)
        {
            return bRequisicionRegistrada;
        }

        #endregion Funciones y Procedimientos Privados

    }
}
