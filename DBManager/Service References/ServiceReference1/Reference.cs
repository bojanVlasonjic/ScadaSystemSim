﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBManager.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.I_RTU")]
    public interface I_RTU {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/I_RTU/sendDataToSvc", ReplyAction="http://tempuri.org/I_RTU/sendDataToSvcResponse")]
        void sendDataToSvc(string address, int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/I_RTU/sendDataToSvc", ReplyAction="http://tempuri.org/I_RTU/sendDataToSvcResponse")]
        System.Threading.Tasks.Task sendDataToSvcAsync(string address, int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/I_RTU/register_RTU", ReplyAction="http://tempuri.org/I_RTU/register_RTUResponse")]
        bool register_RTU(string address);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/I_RTU/register_RTU", ReplyAction="http://tempuri.org/I_RTU/register_RTUResponse")]
        System.Threading.Tasks.Task<bool> register_RTUAsync(string address);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/I_RTU/getAvailableAddresses", ReplyAction="http://tempuri.org/I_RTU/getAvailableAddressesResponse")]
        string getAvailableAddresses();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/I_RTU/getAvailableAddresses", ReplyAction="http://tempuri.org/I_RTU/getAvailableAddressesResponse")]
        System.Threading.Tasks.Task<string> getAvailableAddressesAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface I_RTUChannel : DBManager.ServiceReference1.I_RTU, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class I_RTUClient : System.ServiceModel.ClientBase<DBManager.ServiceReference1.I_RTU>, DBManager.ServiceReference1.I_RTU {
        
        public I_RTUClient() {
        }
        
        public I_RTUClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public I_RTUClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public I_RTUClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public I_RTUClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void sendDataToSvc(string address, int value) {
            base.Channel.sendDataToSvc(address, value);
        }
        
        public System.Threading.Tasks.Task sendDataToSvcAsync(string address, int value) {
            return base.Channel.sendDataToSvcAsync(address, value);
        }
        
        public bool register_RTU(string address) {
            return base.Channel.register_RTU(address);
        }
        
        public System.Threading.Tasks.Task<bool> register_RTUAsync(string address) {
            return base.Channel.register_RTUAsync(address);
        }
        
        public string getAvailableAddresses() {
            return base.Channel.getAvailableAddresses();
        }
        
        public System.Threading.Tasks.Task<string> getAvailableAddressesAsync() {
            return base.Channel.getAvailableAddressesAsync();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.I_DB_Manager")]
    public interface I_DB_Manager {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/I_DB_Manager/addTag", ReplyAction="http://tempuri.org/I_DB_Manager/addTagResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ScadaModel.TagInput))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ScadaModel.DigitalInput))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ScadaModel.AnalogInput))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ScadaModel.TagOutput))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ScadaModel.DigitalOutput))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ScadaModel.AnalogOutput))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ScadaModel.RealTimeDriver))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ScadaModel.SimulationDriver))]
        bool addTag(ScadaModel.Tag tag, string driver);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/I_DB_Manager/addTag", ReplyAction="http://tempuri.org/I_DB_Manager/addTagResponse")]
        System.Threading.Tasks.Task<bool> addTagAsync(ScadaModel.Tag tag, string driver);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/I_DB_Manager/removeTag", ReplyAction="http://tempuri.org/I_DB_Manager/removeTagResponse")]
        bool removeTag(string tagID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/I_DB_Manager/removeTag", ReplyAction="http://tempuri.org/I_DB_Manager/removeTagResponse")]
        System.Threading.Tasks.Task<bool> removeTagAsync(string tagID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/I_DB_Manager/addAlarm", ReplyAction="http://tempuri.org/I_DB_Manager/addAlarmResponse")]
        bool addAlarm(ScadaModel.Alarm alarm);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/I_DB_Manager/addAlarm", ReplyAction="http://tempuri.org/I_DB_Manager/addAlarmResponse")]
        System.Threading.Tasks.Task<bool> addAlarmAsync(ScadaModel.Alarm alarm);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/I_DB_Manager/removeAlarm", ReplyAction="http://tempuri.org/I_DB_Manager/removeAlarmResponse")]
        bool removeAlarm(string tagID, string alarmID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/I_DB_Manager/removeAlarm", ReplyAction="http://tempuri.org/I_DB_Manager/removeAlarmResponse")]
        System.Threading.Tasks.Task<bool> removeAlarmAsync(string tagID, string alarmID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/I_DB_Manager/getRTDAddresses", ReplyAction="http://tempuri.org/I_DB_Manager/getRTDAddressesResponse")]
        string[] getRTDAddresses();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/I_DB_Manager/getRTDAddresses", ReplyAction="http://tempuri.org/I_DB_Manager/getRTDAddressesResponse")]
        System.Threading.Tasks.Task<string[]> getRTDAddressesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/I_DB_Manager/getSDAvailableAddr", ReplyAction="http://tempuri.org/I_DB_Manager/getSDAvailableAddrResponse")]
        string[] getSDAvailableAddr();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/I_DB_Manager/getSDAvailableAddr", ReplyAction="http://tempuri.org/I_DB_Manager/getSDAvailableAddrResponse")]
        System.Threading.Tasks.Task<string[]> getSDAvailableAddrAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/I_DB_Manager/getTags", ReplyAction="http://tempuri.org/I_DB_Manager/getTagsResponse")]
        ScadaModel.Tag[] getTags();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/I_DB_Manager/getTags", ReplyAction="http://tempuri.org/I_DB_Manager/getTagsResponse")]
        System.Threading.Tasks.Task<ScadaModel.Tag[]> getTagsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/I_DB_Manager/getTagForEditing", ReplyAction="http://tempuri.org/I_DB_Manager/getTagForEditingResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ScadaModel.TagInput))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ScadaModel.DigitalInput))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ScadaModel.AnalogInput))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ScadaModel.TagOutput))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ScadaModel.DigitalOutput))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ScadaModel.AnalogOutput))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ScadaModel.RealTimeDriver))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ScadaModel.SimulationDriver))]
        ScadaModel.Tag getTagForEditing(string tagID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/I_DB_Manager/getTagForEditing", ReplyAction="http://tempuri.org/I_DB_Manager/getTagForEditingResponse")]
        System.Threading.Tasks.Task<ScadaModel.Tag> getTagForEditingAsync(string tagID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/I_DB_Manager/getAlarms", ReplyAction="http://tempuri.org/I_DB_Manager/getAlarmsResponse")]
        ScadaModel.Alarm[] getAlarms(string tagID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/I_DB_Manager/getAlarms", ReplyAction="http://tempuri.org/I_DB_Manager/getAlarmsResponse")]
        System.Threading.Tasks.Task<ScadaModel.Alarm[]> getAlarmsAsync(string tagID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/I_DB_Manager/sendManualValue", ReplyAction="http://tempuri.org/I_DB_Manager/sendManualValueResponse")]
        bool sendManualValue(string tagID, int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/I_DB_Manager/sendManualValue", ReplyAction="http://tempuri.org/I_DB_Manager/sendManualValueResponse")]
        System.Threading.Tasks.Task<bool> sendManualValueAsync(string tagID, int value);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface I_DB_ManagerChannel : DBManager.ServiceReference1.I_DB_Manager, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class I_DB_ManagerClient : System.ServiceModel.ClientBase<DBManager.ServiceReference1.I_DB_Manager>, DBManager.ServiceReference1.I_DB_Manager {
        
        public I_DB_ManagerClient() {
        }
        
        public I_DB_ManagerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public I_DB_ManagerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public I_DB_ManagerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public I_DB_ManagerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool addTag(ScadaModel.Tag tag, string driver) {
            return base.Channel.addTag(tag, driver);
        }
        
        public System.Threading.Tasks.Task<bool> addTagAsync(ScadaModel.Tag tag, string driver) {
            return base.Channel.addTagAsync(tag, driver);
        }
        
        public bool removeTag(string tagID) {
            return base.Channel.removeTag(tagID);
        }
        
        public System.Threading.Tasks.Task<bool> removeTagAsync(string tagID) {
            return base.Channel.removeTagAsync(tagID);
        }
        
        public bool addAlarm(ScadaModel.Alarm alarm) {
            return base.Channel.addAlarm(alarm);
        }
        
        public System.Threading.Tasks.Task<bool> addAlarmAsync(ScadaModel.Alarm alarm) {
            return base.Channel.addAlarmAsync(alarm);
        }
        
        public bool removeAlarm(string tagID, string alarmID) {
            return base.Channel.removeAlarm(tagID, alarmID);
        }
        
        public System.Threading.Tasks.Task<bool> removeAlarmAsync(string tagID, string alarmID) {
            return base.Channel.removeAlarmAsync(tagID, alarmID);
        }
        
        public string[] getRTDAddresses() {
            return base.Channel.getRTDAddresses();
        }
        
        public System.Threading.Tasks.Task<string[]> getRTDAddressesAsync() {
            return base.Channel.getRTDAddressesAsync();
        }
        
        public string[] getSDAvailableAddr() {
            return base.Channel.getSDAvailableAddr();
        }
        
        public System.Threading.Tasks.Task<string[]> getSDAvailableAddrAsync() {
            return base.Channel.getSDAvailableAddrAsync();
        }
        
        public ScadaModel.Tag[] getTags() {
            return base.Channel.getTags();
        }
        
        public System.Threading.Tasks.Task<ScadaModel.Tag[]> getTagsAsync() {
            return base.Channel.getTagsAsync();
        }
        
        public ScadaModel.Tag getTagForEditing(string tagID) {
            return base.Channel.getTagForEditing(tagID);
        }
        
        public System.Threading.Tasks.Task<ScadaModel.Tag> getTagForEditingAsync(string tagID) {
            return base.Channel.getTagForEditingAsync(tagID);
        }
        
        public ScadaModel.Alarm[] getAlarms(string tagID) {
            return base.Channel.getAlarms(tagID);
        }
        
        public System.Threading.Tasks.Task<ScadaModel.Alarm[]> getAlarmsAsync(string tagID) {
            return base.Channel.getAlarmsAsync(tagID);
        }
        
        public bool sendManualValue(string tagID, int value) {
            return base.Channel.sendManualValue(tagID, value);
        }
        
        public System.Threading.Tasks.Task<bool> sendManualValueAsync(string tagID, int value) {
            return base.Channel.sendManualValueAsync(tagID, value);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.I_Trending", CallbackContract=typeof(DBManager.ServiceReference1.I_TrendingCallback))]
    public interface I_Trending {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/I_Trending/initTrending", ReplyAction="http://tempuri.org/I_Trending/initTrendingResponse")]
        void initTrending();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/I_Trending/initTrending", ReplyAction="http://tempuri.org/I_Trending/initTrendingResponse")]
        System.Threading.Tasks.Task initTrendingAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface I_TrendingCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/I_Trending/addNewValue")]
        void addNewValue(string tagID, int value);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface I_TrendingChannel : DBManager.ServiceReference1.I_Trending, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class I_TrendingClient : System.ServiceModel.DuplexClientBase<DBManager.ServiceReference1.I_Trending>, DBManager.ServiceReference1.I_Trending {
        
        public I_TrendingClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public I_TrendingClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public I_TrendingClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public I_TrendingClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public I_TrendingClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void initTrending() {
            base.Channel.initTrending();
        }
        
        public System.Threading.Tasks.Task initTrendingAsync() {
            return base.Channel.initTrendingAsync();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.I_Alarm_Display", CallbackContract=typeof(DBManager.ServiceReference1.I_Alarm_DisplayCallback))]
    public interface I_Alarm_Display {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/I_Alarm_Display/initAlarmDisplay", ReplyAction="http://tempuri.org/I_Alarm_Display/initAlarmDisplayResponse")]
        void initAlarmDisplay();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/I_Alarm_Display/initAlarmDisplay", ReplyAction="http://tempuri.org/I_Alarm_Display/initAlarmDisplayResponse")]
        System.Threading.Tasks.Task initAlarmDisplayAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface I_Alarm_DisplayCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/I_Alarm_Display/logAlarmToConsole")]
        void logAlarmToConsole(ScadaModel.Alarm alarm, int scannedValue);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface I_Alarm_DisplayChannel : DBManager.ServiceReference1.I_Alarm_Display, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class I_Alarm_DisplayClient : System.ServiceModel.DuplexClientBase<DBManager.ServiceReference1.I_Alarm_Display>, DBManager.ServiceReference1.I_Alarm_Display {
        
        public I_Alarm_DisplayClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public I_Alarm_DisplayClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public I_Alarm_DisplayClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public I_Alarm_DisplayClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public I_Alarm_DisplayClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void initAlarmDisplay() {
            base.Channel.initAlarmDisplay();
        }
        
        public System.Threading.Tasks.Task initAlarmDisplayAsync() {
            return base.Channel.initAlarmDisplayAsync();
        }
    }
}
