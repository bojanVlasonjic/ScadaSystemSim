using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Text;

namespace ScadaModel
{
    [DataContract]
    public class SimulationDriver: Driver
    {
        [DataMember]
        private const int numOfAddresses = 10;

        [DataMember]
        private double amplitude = 100.0;

        [DataMember]
        private Dictionary<string, int> signal_values; // adress: value from signal

        [DataMember]
        private Dictionary<string, int> address_signals; // address: signal index(sine  = 0, cosine = 1, ... Rect=5)

        public SimulationDriver()
        {
            signal_values = new Dictionary<string, int>();
            address_signals = new Dictionary<string, int>();
            Random random = new Random();

            //inicijalizacija adresa i njihovih default-nih vrednosti
            for (int i = 1; i <= numOfAddresses; i++)
            {
                signal_values["a" + i] = 0;
                address_signals["a" + i] = random.Next(1, 5);
            }

        }


        public int readValue(string address)
        {

            if(!address_signals.ContainsKey(address))
            {
                return -1;
            }

            switch(address_signals[address])
            {
                case 1:
                    return (int)Sine();
                case 2:
                    return (int)Cosine();
                case 3:
                    return (int)Ramp();
                case 4:
                    return (int)Triangle();
                case 5:
                    return (int)Rectangle();

                default:
                    return 0;
            }
        }

        public List<string> getAvailableAddresses()
        {
            List<string> availAddr = new List<string>();

            foreach (string key in signal_values.Keys)
            {
                //ako nikakve vrednosti nisu upisivane na adresu, znaci da je slobodna
                if (signal_values[key] == 0)
                {
                    availAddr.Add(key);
                }
            }

            return availAddr;
        }


        public bool manualWrite(string address, int value)
        {
            if(!signal_values.ContainsKey(address))
            {
                return false;
            }

            signal_values[address] = value;
            return true;
        }

        public int manualRead(string address)
        {
            if(!signal_values.ContainsKey(address))
            {
                return -1;
            }

            return signal_values[address];
        }

        /* SIMULATION SIGNALS */

        public  double Sine()
        {
            return amplitude * Math.Sin((double)DateTime.Now.Second / 60 * Math.PI);
        }
        public double Cosine()
        {
            return amplitude * Math.Cos((double)DateTime.Now.Second / 60 * Math.PI);
        }
        public double Ramp()
        {
            return amplitude * DateTime.Now.Second / 60;
        }

        public double Triangle()
        {
            return ((2 * amplitude) / Math.PI) * Math.Asin(Math.Sin(2 * Math.PI * DateTime.Now.Second / 60.0));
        }

        public double Rectangle()
        {
            return amplitude * Math.Sign(Math.Sin((DateTime.Now.Second % 10) / 5.0));
        }
    }
}