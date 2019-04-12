using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Runtime.Serialization;

namespace ScadaModel
{
    [DataContract]
    [KnownType(typeof(DigitalInput))]
    [KnownType(typeof(DigitalOutput))]
    [KnownType(typeof(AnalogInput))]
    [KnownType(typeof(AnalogOutput))]
    public class Tag
    {
        [DataMember]
        private string id;

        [DataMember]
        private string description;

        [DataMember]
        private Driver driver;

        [DataMember]
        private string io_address;

        [DataMember]
        private string driver_id;
        

        public Tag() { }

        public Tag(string id, string description, Driver driver, string io_address)
        {
            this.id = id;
            this.description = description;
            this.driver = driver;
            this.io_address = io_address;

        }

        /* Getters and setters */

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public Driver Driver
        {
            get { return driver; }
            set { driver = value; }
        }

        public string IOAddress
        {
            get { return io_address; }
            set { io_address = value; }
        }

        public string DriverID
        {
            get { return driver_id; }
            set { driver_id = value; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(this.id);
            sb.Append("|");

            sb.Append(this.description);
            sb.Append("|");

            if(driver is SimulationDriver)
            {
                sb.Append("Simulation_driver");
            } else
            {
                sb.Append("Real_time_driver");
            }

            sb.Append("|");

            sb.Append(this.IOAddress);
            sb.Append("|");

            return sb.ToString();

        }


        public virtual string displayTagData()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"Tag id {this.id}\n");
            sb.Append($"Tag descripiton: {this.description}\n");

            if (driver is SimulationDriver)
            {
                sb.Append("Driver: Simulation_driver\n");
            }
            else
            {
                sb.Append("Driver: Real_time_driver\n");
            }

            sb.Append($"IO Address: {this.IOAddress}\n");
         
            return sb.ToString();
        }

    }
}