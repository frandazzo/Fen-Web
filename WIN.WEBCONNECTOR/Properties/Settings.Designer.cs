﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Il codice è stato generato da uno strumento.
//     Versione runtime:4.0.30319.42000
//
//     Le modifiche apportate a questo file possono provocare un comportamento non corretto e andranno perse se
//     il codice viene rigenerato.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WIN.WEBCONNECTOR.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("500")]
        public int PacketSize {
            get {
                return ((int)(this["PacketSize"]));
            }
            set {
                this["PacketSize"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool CheckInternetConnection {
            get {
                return ((bool)(this["CheckInternetConnection"]));
            }
            set {
                this["CheckInternetConnection"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("500")]
        public int MaxQueryResult {
            get {
                return ((int)(this["MaxQueryResult"]));
            }
            set {
                this["MaxQueryResult"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("500")]
        public int MaxFiscalCodeRequests {
            get {
                return ((int)(this["MaxFiscalCodeRequests"]));
            }
            set {
                this["MaxFiscalCodeRequests"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool RegionalDeploy {
            get {
                return ((bool)(this["RegionalDeploy"]));
            }
            set {
                this["RegionalDeploy"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://www.fenealgest.it/servizi/WebServices/JoinAllWebService.asmx")]
        public string WIN_WEBCONNECTOR_JoinAllServices_JoinAllWebService {
            get {
                return ((string)(this["WIN_WEBCONNECTOR_JoinAllServices_JoinAllWebService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://www.fenealgest.it/servizi/WebServices/SharetopIntegration.asmx")]
        public string WIN_WEBCONNECTOR_SharetopServices_SharetopIntegration {
            get {
                return ((string)(this["WIN_WEBCONNECTOR_SharetopServices_SharetopIntegration"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://www.fenealgest.it/servizi/WebServices/FenealgestwebServices.asmx")]
        public string WIN_WEBCONNECTOR_FenealgestServices_FenealgestwebServices {
            get {
                return ((string)(this["WIN_WEBCONNECTOR_FenealgestServices_FenealgestwebServices"]));
            }
        }
    }
}