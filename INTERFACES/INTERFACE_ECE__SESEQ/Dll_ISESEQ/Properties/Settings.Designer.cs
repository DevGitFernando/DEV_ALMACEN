﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dll_ISESEQ.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost:1060/wsInterfaceISESEQ.asmx")]
        public string Dll_ISESEQ_wsAcuseProcesos_RE_ws_Cnn_ISESEQ {
            get {
                return ((string)(this["Dll_ISESEQ_wsAcuseProcesos_RE_ws_Cnn_ISESEQ"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost:21734/wsSII_INT_ISESEQ/wsISESEQ.asmx")]
        public string Dll_ISESEQ_wsAcuseProcesos_cnnInterna_wsISESEQ {
            get {
                return ((string)(this["Dll_ISESEQ_wsAcuseProcesos_cnnInterna_wsISESEQ"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://apps.seseqro.gob.mx:8181/finanzas/webService/recepcionMovInv.php")]
        public string Dll_ISESEQ_wsInformacionOperacion_SESEQ {
            get {
                return ((string)(this["Dll_ISESEQ_wsInformacionOperacion_SESEQ"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://apps.seseqro.gob.mx:8181/finanzas/webService/recepcionMovInv.php")]
        public string Dll_ISESEQ_wsInformacionOperativa_SESEQ__Web_Service_SESEQ {
            get {
                return ((string)(this["Dll_ISESEQ_wsInformacionOperativa_SESEQ__Web_Service_SESEQ"]));
            }
        }
    }
}
