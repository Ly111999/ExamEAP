using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService
{
    static IBankingRepository repository = new BankingRepository();
    public Service()
    {
        AddNewAccount(new Banking { name = "An", code = "00050553001", pass = "123456", balance = 1000000 });
        AddNewAccount(new Banking { name = "Lan", code = "00050553002", pass = "123456", balance = 1000000 });
    }

    public Banking AddNewAccount(Banking item)
    {
        Banking newAccount = repository.AddNewAccount(item);
        return item;
    }

    public Banking GetAccount(string code, string pass)
    {
        return repository.GetAccount(code, pass);
    }

    public bool TransferMoney(string sCode, string rCode, decimal amount)
    {
        bool transfer = repository.TransferMoney(sCode, rCode, amount);
        if (transfer)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
