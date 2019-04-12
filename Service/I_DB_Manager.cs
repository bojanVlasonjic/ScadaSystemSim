using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ScadaModel;

namespace Service
{
    [ServiceContract]
    interface I_DB_Manager
    {
        [OperationContract]
        bool addTag(Tag tag, string driver);

        [OperationContract]
        bool removeTag(string tagID);

        [OperationContract]
        bool addAlarm(Alarm alarm);

        [OperationContract]
        bool removeAlarm(string tagID, string alarmID);

        [OperationContract]
        List<string> getRTDAddresses();

        [OperationContract]
        List<string> getSDAvailableAddr();

        [OperationContract]
        Tag[] getTags();

        [OperationContract]
        Tag getTagForEditing(string tagID);

        [OperationContract]
        List<Alarm> getAlarms(string tagID);

        [OperationContract]
        bool sendManualValue(string tagID, int value);

    }
}
