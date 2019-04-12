using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Text;

namespace ScadaModel
{
    [DataContract]
    public class DigitalOutput: TagOutput
    {
        
        public DigitalOutput()
        {

        }

        public DigitalOutput(string id, string description, Driver driver, string io_address,
            int initVal) : base(id, description, driver, io_address, initVal)
        {
           
        }


        public override string ToString()
        {
            return base.ToString();
        }

        public override string displayTagData()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(base.displayTagData());
            sb.Append("Tag type: Digital output\n");

            return sb.ToString();
        }
    }
}