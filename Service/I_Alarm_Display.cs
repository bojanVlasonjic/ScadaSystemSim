using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ScadaModel;

namespace Service
{

    public interface I_Alarm_Display_CB
    {
        [OperationContract(IsOneWay =true)]
        void logAlarmToConsole(Alarm alarm, int scannedValue);
    }

    [ServiceContract(CallbackContract = typeof(I_Alarm_Display_CB))]
    public interface I_Alarm_Display
    {
        [OperationContract]
        void initAlarmDisplay();
    }
}
