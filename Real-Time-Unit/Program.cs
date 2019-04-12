using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ScadaModel;
using System.ServiceModel;

namespace Real_Time_Unit
{
    class Program
    {
        static ServiceReference1.I_RTUClient service = new ServiceReference1.I_RTUClient(); //service

        static void Main(string[] args)
        {

            int scanValue;
            Random random = new Random();

            //TODO: DIGITALNI POTPIS!

            Console.WriteLine("Unesite BROJ adrese na koju zelite da se povezete: ");
            string address = "a" + Console.ReadLine();

            try
            {
                while (service.register_RTU(address) == false)
                {

                    Console.WriteLine("Unesena adresa nije dostupna. Dostupne adrese su: ");
                    Console.WriteLine(service.getAvailableAddresses());

                    Console.WriteLine("Unesite BROJ adrese na koju zelite da se povezete: ");
                    address = "a" + Console.ReadLine();

                }

            }catch(FaultException e)
            {
                Console.WriteLine(e.GetBaseException());
                Console.ReadKey();
                return;
            }
            
            Console.WriteLine("Uspesno povezivanje na adresu");

            //Ocitavanje vrednosti i slanje na servis
            while(true)
            {

                scanValue = random.Next(Constants.RTU_low_limit, Constants.RTU_high_limit);

                Console.WriteLine($"Sending {scanValue} from RTU to Service on address {address}");
                service.sendDataToSvc(address, scanValue);

                Thread.Sleep(3000);
            }

        }
    }
}
