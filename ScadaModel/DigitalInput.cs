using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Text;

namespace ScadaModel
{
    [DataContract]
    public class DigitalInput: TagInput
    {
    	public DigitalInput(string id, string description, Driver driver, string io_address,
            int scanTime, scanType scan, readType read):base(id, description, driver, io_address, scanTime, scan, read)
        {
            
        }
        
        public DigitalInput()
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
            sb.Append("Tag type: Digital input\n");
          
            return sb.ToString();
        }

    }
}