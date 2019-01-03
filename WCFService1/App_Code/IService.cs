using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
[ServiceContract]
public interface IService
{

    // TODO: Add your service operations here
    [OperationContract]
    [WebInvoke(Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "AddAccount/")]
    Banking AddNewAccount(Banking banking);

    [OperationContract]
    [WebInvoke(Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "AddAccount/")]
    Banking GetAccount(string code, string pass);

    [OperationContract]
    [WebInvoke(Method = "PUT",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "Transfer/")]
    bool TransferMoney(string sCode, string rCode, decimal amount);
}
