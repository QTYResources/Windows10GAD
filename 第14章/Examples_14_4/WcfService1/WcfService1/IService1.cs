using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using System.Collections.ObjectModel;

namespace WcfService1
{
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string HelloWCF();

        [OperationContract]
        List<string> GetMessage();

        [OperationContract]
        string InsertMessage(string name, string description);
    }
}
