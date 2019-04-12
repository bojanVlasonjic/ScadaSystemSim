using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using ScadaModel;

namespace Service
{
    public class AlarmIO
    {

        private string currentPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
        private string filePath = "data/alarms.txt";

        public AlarmIO()
        {

        }

        public void writeAlarmToFile(Alarm alarm)
        {
            using (StreamWriter sw = File.AppendText(currentPath + filePath))
            {
                sw.WriteLine(alarm.displayAlarmData());
            }
        }
    }
}