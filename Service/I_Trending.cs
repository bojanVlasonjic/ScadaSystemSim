using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ScadaModel;

namespace Service
{

    public interface I_Trending_CB
    {
        [OperationContract(IsOneWay = true)]
        void addNewValue(string tagID, int value);
    }

    [ServiceContract(CallbackContract = typeof(I_Trending_CB))]
    public interface I_Trending
    {
        [OperationContract]
        void initTrending();
    }
}
