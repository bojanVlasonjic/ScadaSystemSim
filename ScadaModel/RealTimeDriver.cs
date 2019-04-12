using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Text;

namespace ScadaModel
{
    [DataContract]
    public class RealTimeDriver: Driver
    {
        
        [DataMember]
        private const int numOfAddresses = 5;

        [DataMember]
        private Dictionary<string, int> rtu_signals; // kljuc adresa, vrednost ocitava rtu

        public RealTimeDriver()
        {
            rtu_signals = new Dictionary<string, int>();

            //adrese unapred predefinisane, inicijalne vrednosti na adresama su 0
            for(int i = 1; i <= numOfAddresses; i++)
            {
                rtu_signals["a" + i] = 0;
            }

        }

        public Dictionary<string, int> RTU_Signals
        {
            get { return rtu_signals; }
        }

        public bool addressAvailable(string address)
        {
            if(rtu_signals.ContainsKey(address))
            {
                if(rtu_signals[address] == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public string getAvailableAddresses()
        {
            StringBuilder sb = new StringBuilder();

            foreach(string key in rtu_signals.Keys)
            {
                //ako nikakve vrednosti nisu upisivane na adresu, znaci da je slobodna
                if (rtu_signals[key] == 0)
                {
                    sb.Append(key);
                    sb.Append("; ");
                }
            }

            return sb.ToString();
        }

        public List<string> getAddresses()
        {
            return rtu_signals.Keys.ToList();
        }
     
        public bool addValue(string address, int value)
        {
            if(rtu_signals.ContainsKey(address))
            {
                rtu_signals[address] = value;
                return true;

            }

            return false;
        }

        public int readValue(string address)
        {
            if(rtu_signals.ContainsKey(address))
            {
                return rtu_signals[address];
            }

            return -1;
        }

    }
}