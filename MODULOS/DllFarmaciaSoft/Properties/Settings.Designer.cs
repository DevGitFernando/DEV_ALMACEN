﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DllFarmaciaSoft.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
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
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost:65000/wsFarmaciaScSoft/wsFarmacia.asmx")]
        public string DllFarmaciaSoft_wsFarmacia_wsCnnCliente {
            get {
                return ((string)(this["DllFarmaciaSoft_wsFarmacia_wsCnnCliente"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost:65000/wsFarmaciaScSoft/wsFarmaciaSoftGn.asmx")]
        public string DllFarmaciaSoft_wsFarmaciaSoftGn_wsConexion {
            get {
                return ((string)(this["DllFarmaciaSoft_wsFarmaciaSoftGn_wsConexion"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost:65000/wsFarmaciaScSoft/wsOficinaCentral.asmx")]
        public string DllFarmaciaSoft_wsOficinaCentral_wsCnnOficinaCentral {
            get {
                return ((string)(this["DllFarmaciaSoft_wsOficinaCentral_wsCnnOficinaCentral"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost/wsPersonalFirma/wsHuellas.asmx")]
        public string DllFarmaciaSoft_WsHuellas_wsHuellas {
            get {
                return ((string)(this["DllFarmaciaSoft_WsHuellas_wsHuellas"]));
            }
        }
    }
}
