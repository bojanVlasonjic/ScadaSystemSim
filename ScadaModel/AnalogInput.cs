using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Text;

namespace ScadaModel
{
    [DataContract]
    public class AnalogInput : TagInput
    {
        [DataMember]
        private int lowLimits;

        [DataMember]
        private int highLimits;

        [DataMember]
        private string units;


        public AnalogInput()
        {

        }


        public AnalogInput(string id, string description, Driver driver, string io_address, int scanTime,
            scanType scan, readType read, int lowLim, int highLim, string units) : base(id, description,
                driver, io_address, scanTime, scan, read)
        {
            this.lowLimits = lowLim;
            this.highLimits = highLim;
            this.units = units;
        }

        public int LowLimits
        {
            get { return lowLimits; }
            set { lowLimits = value; }
        }

        public int HighLimits
        {
            get { return highLimits; }
            set { highLimits = value; }
        }

        public string Units
        {
            get { return units; }
            set { units = value; }
        }


        public override string displayTagData()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(base.displayTagData());
            sb.Append("Tag type: Analog input\n");
            sb.Append($"Low limits: {lowLimits}\n");
            sb.Append($"High limits: {highLimits}\n");
            sb.Append($"Units: {units}\n");

            return sb.ToString();
        }


    }
}