using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ScadaModel;

namespace Service
{
    [ServiceContract]
    interface I_RTU
    {
        [OperationContract]
        void sendDataToSvc(string address, int value);

        [OperationContract]
        bool register_RTU(string address);

        [OperationContract]
        string getAvailableAddresses();
    }
}
