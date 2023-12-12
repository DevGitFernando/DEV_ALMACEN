using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Errores;

using Dll_MA_IFacturacion;
using Dll_MA_IFacturacion.CFDI;

namespace Dll_MA_IFacturacion.CFDI
{
    public static class clsLog_CFDI 
    {
        static clsGrabarError log; 
        static clsLog_CFDI()
        {
            log = new clsGrabarError(General.DatosConexion, DtIFacturacion.DatosApp, "clsLog_CFDI");
            ////log.NombreLogErorresTextFile = "SC.CFDI_log";  
        }

        public static void Log(string Clase, string Metodo, string Mensaje)
        {
            try
            {
                string sMensaje = string.Format("\nMétodo: {0}.{1}\t\tMensaje :{2}", Clase, Metodo, Mensaje);
                log.LogError(sMensaje);
            }
            catch 
            { 
            }
        }
    }
}
