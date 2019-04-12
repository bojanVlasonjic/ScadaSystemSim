using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Text;

namespace ScadaModel
{
    [DataContract]
    [KnownType(typeof(DigitalOutput))]
    [KnownType(typeof(AnalogOutput))]
    public class TagOutput: Tag
    {
        [DataMember]
        private int initialValue;


        public TagOutput()
        {

        }

        public TagOutput(string id, string description, Driver driver, string io_address,
            int initVal): base(id, description, driver, io_address)
        {

            this.initialValue = initVal;
        }


        public int InitialValue
        {
            get { return initialValue; }
            set { initialValue = value; }
        }

        public override string ToString()
        {
            return base.ToString() + initialValue + "|";
        }

        public override string displayTagData()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(base.displayTagData());
            sb.Append($"Initial value {initialValue}\n");

            return sb.ToString();
        }

    }
}