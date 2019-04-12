using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Text;

namespace ScadaModel
{
    [DataContract]
    public class Alarm
    {
        [DataMember]
        private string alarmID;

        [DataMember]
        private string tagID;

        [DataMember]
        private DateTime alarmTime;

        [DataMember]
        private int lowLimit;

        [DataMember]
        private int highLimit;

        public Alarm()
        {

        }

        public Alarm(string alarm_id, string tag_id, int lowLimit, int highLimit)
        {
            this.alarmID = alarm_id;
            this.tagID = tag_id;
            this.lowLimit = lowLimit;
            this.highLimit = highLimit;
        }


        public string AlarmID
        {
            get { return alarmID; }
            set { alarmID = value; }
        }

        public string TagID
        {
            get { return tagID; }
            set { tagID = value; }
        }

        public DateTime AlarmTime
        {
            get { return alarmTime; }
            set { alarmTime = value; }
        }

        public int LowLimit
        {
            get { return lowLimit; }
            set { lowLimit = value; }
        }

        public int HightLimit
        {
            get { return highLimit; }
            set { highLimit = value; }
        }

        public string displayAlarmData()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"Alarm name: '{AlarmID}'\n");
            sb.Append($"For tag: '{TagID}'\n");
            sb.Append($"Alarm low limit: {LowLimit}\n");
            sb.Append($"Alarm high limit: {HightLimit}\n");
            sb.Append($"Detected at: {AlarmTime}\n\n");

            return sb.ToString();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(alarmID + "; " + tagID + "; ");

            if(alarmTime != null)
            {
                sb.Append(alarmTime + "; ");
            }

            sb.Append(lowLimit + "; " + highLimit);

            return sb.ToString();
        }

    }
}