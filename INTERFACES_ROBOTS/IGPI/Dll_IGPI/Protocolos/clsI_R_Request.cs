﻿using System;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

using Dll_IGPI;

namespace Dll_IGPI.Protocolos
{
    public class clsI_R_Request
    {
        #region Declaracion de Variables  
        IGPIParametros pMach = new IGPIParametros(); 
        private basGenerales Fg = new basGenerales();

        // private string sMensajeRecibido = ""; 
        private string sDialogo = "R";
        private string sDialogoDescripcion = "Verificación de Comunicación.";
        // private string sRequestLocationNumber = "001";
        // private string sProtocolVersion = "0113";
        private string sProtocolVersion = IGPI.ProtocolVersion; 
        private string sProtocolosInstalados = "AIPOSRBK"; //"000EGABSOPLRKI";
        private string sSolicitud = "";
        private bool bResponder = false; 
        #endregion Declaracion de Variables

        #region Constructores y Destructor de Clase 
        public clsI_R_Request()
        {
            // R0010113011EGABSOPLRKI
            // sSolicitud = sDialogo + sRequestLocationNumber + sProtocolVersion + sProtocolosInstalados + "028\0";
            bResponder = false; 
            pMach.Dialogo = "R";
            pMach.RequestLocationNumber = "001";
            
            pMach.ProtocolVersion = "113";
            pMach.ProtocolVersion = "114";


            pMach.ProtocolVersion = IGPI.ProtocolVersion; 
            
            
            //////sProtocolosInstalados = "000EGABSOPLRKI";
            //////sProtocolosInstalados = "0Eb +GASOPLRKI";
            //////sProtocolosInstalados = "000Eb+ GASOPLRKI";

            //////sProtocolosInstalados = "000b + GABOSPRKI";
            //////// no Mandar nada 
            //////sProtocolosInstalados = ""; 

            ////// Original 
            ////sSolicitud = pMach.Dialogo + pMach.RequestLocationNumber + 
            ////    pMach.ProtocolVersion + sProtocolosInstalados + 
            ////    pMach.HumanReadableFormat;


            sSolicitud = pMach.Dialogo + pMach.RequestLocationNumber +
                pMach.ProtocolVersion + pMach.HumanReadableFormat + sProtocolosInstalados + "\0";


            sSolicitud = pMach.Dialogo + pMach.RequestLocationNumber +
                pMach.ProtocolVersion + "003" + sProtocolosInstalados + "\0";

            bResponder = true; 
        }
        #endregion Constructores y Destructor de Clase

        #region Propiedades Publicas 
        public string Dialogo
        {
            get { return sDialogo; } 
        }

        public string DialogoDescripcion
        {
            get { return sDialogoDescripcion; }
        }

        public bool GenerarRespuesta
        {
            get { return bResponder; }
        }

        public string Solicitud 
        {
            get { return sSolicitud; }
        }
        #endregion Propidades Publicas  

        #region Funciones y Procedimientos Privados
        #endregion Funciones y Procedimientos Privados
    }
}
