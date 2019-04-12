using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.IO;
using ScadaModel;

namespace AlarmDisplay
{

    public class AlarmDisplayCallBack: ServiceReference1.I_Alarm_DisplayCallback
    {

        public void logAlarmToConsole(Alarm alarm, int scannedValue)
        {
            Console.WriteLine(alarm.displayAlarmData());
            Console.WriteLine($"Value that was scanned: {scannedValue}\n\n\n");
            
        }

    }

    class Program
    {

        static ServiceReference1.I_Alarm_DisplayClient service;

        static void Main(string[] args)
        {
            InstanceContext ic = new InstanceContext(new AlarmDisplayCallBack());
            service = new ServiceReference1.I_Alarm_DisplayClient(ic);

            service.initAlarmDisplay();
            Console.ReadLine();
        }
    }
}
