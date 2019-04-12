using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Text;

namespace ScadaModel
{
    [DataContract]
    [Flags]
    public enum scanType {
        [EnumMember]
        ON = 1,
        [EnumMember]
        OFF = 2
    }

    [DataContract]
    [Flags]
    public enum readType {
        [EnumMember]
        AUTO = 1,
        [EnumMember]
        MANUAL = 2
    }

    [DataContract]
    [KnownType(typeof(DigitalInput))]
    [KnownType(typeof(AnalogInput))]
    public class TagInput: Tag
    {
        [DataMember]
        private int scanTime;

        [DataMember]
        private List<Alarm> alarms;

        [DataMember]
        private scanType scan;

        [DataMember]
        private readType read;


        public TagInput()
        {
            this.alarms = new List<Alarm>();
        }

        public TagInput(string id, string description, Driver driver, string io_address,
            int scanTime, scanType scan, readType read):base(id, description, driver, io_address)
        {
            this.scanTime = scanTime;
            this.scan = scan;
            this.read = read;
            this.alarms = new List<Alarm>();
        }


        public int ScanTime
        {
            get { return scanTime; }
            set { scanTime = value; }
        }

        public List<Alarm> Alarms
        {
            get { return alarms; }
            set { alarms = value; }
        }

        public scanType Scan
        {
            get { return scan; }
            set { scan = value; }

        }

        public readType Read
        {
            get { return read; }
            set { read = value; }
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public void addAlarm(Alarm alarm)
        {
            this.alarms.Add(alarm);
        }

        public string toString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(base.ToString());

            sb.Append(this.scanTime);
            sb.Append("|");

            sb.Append(this.scan);
            sb.Append("|");

            sb.Append(this.read);
            sb.Append("|");

            return sb.ToString();

        }

        public override string displayTagData()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(base.displayTagData());

            sb.Append($"Scan time: {this.scanTime}\n");
            sb.Append($"Scan type: {this.scan}\n");
            sb.Append($"Read type: {this.read}\n");

            return sb.ToString();
        }


    }
}