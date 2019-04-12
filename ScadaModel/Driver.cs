using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace ScadaModel
{
    [DataContract]
    [KnownType(typeof(RealTimeDriver))]
    [KnownType(typeof(SimulationDriver))]
    public class Driver
    {
     
        public Driver()
        {

        }

    }
}