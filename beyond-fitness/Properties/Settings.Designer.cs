﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebHome.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    public sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("系統管理員 <WebMaster@beyond-fitness.bigbox.info>")]
        public string WebMaster {
            get {
                return ((string)(this["WebMaster"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("localhost")]
        public string SmtpServer {
            get {
                return ((string)(this["SmtpServer"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("20160")]
        public int UserTimeoutInMinutes {
            get {
                return ((int)(this["UserTimeoutInMinutes"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("300")]
        public int ResourceMaxWidth {
            get {
                return ((int)(this["ResourceMaxWidth"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("~/CourseContract/CreateContractPdf")]
        public string CreateContractPdf {
            get {
                return ((string)(this["CreateContractPdf"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2083")]
        public int DefaultCoach {
            get {
                return ((int)(this["DefaultCoach"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\EINVTurnkey\\UpCast\\B2CSTORAGE")]
        public string EINVTurnKeyPath {
            get {
                return ((string)(this["EINVTurnKeyPath"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=.\\sqlexpress;Initial Catalog=BeyondFitnessProd2;Integrated Security=T" +
            "rue")]
        public string BFDbConnection {
            get {
                return ((string)(this["BFDbConnection"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://10.0.1.201")]
        public string HostDomain {
            get {
                return ((string)(this["HostDomain"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\EINVTurnkey\\UpCast\\B2PMESSAGE")]
        public string EINVTurnKeyB2P {
            get {
                return ((string)(this["EINVTurnKeyB2P"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool UseSSL {
            get {
                return ((bool)(this["UseSSL"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("96938fc3a87869160831460153f84b70")]
        public string ChannelSecret {
            get {
                return ((string)(this["ChannelSecret"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("9cKb6ExKnQMNeUiOk02agKk8c+yforhhQ2KO1wqnoGQ7A7qfzo8TgpNaSiw4ZwWqCNjk7ds8gFK38hE0a" +
            "c2Bfxjja2tPFvkzzVF5J3G4Klj1fxcrBBYwC7L9wi3Xbx5e95oyFRCIPeUKgq/pgwOfzgdB04t89/1O/" +
            "w1cDnyilFU=")]
        public string ChannelToken {
            get {
                return ((string)(this["ChannelToken"]));
            }
        }
    }
}
