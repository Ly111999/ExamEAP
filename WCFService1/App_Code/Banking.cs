using System;
using System.Collections.Generic;
using System.IdentityModel;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel.Web;
using System.Web;

/// <summary>
/// Summary description for Banking
/// </summary>
///
/// 
[DataContract]
public class Banking
{
    [DataMember]
    public int id { get; set; }
    [DataMember]
    public string code { get; set; }
    [DataMember]
    public string pass { get; set; }
    [DataMember]
    public decimal balance { get; set; }
    [DataMember]
    public string name { get; set; }
    [DataMember]
    public decimal amount { get; set; }
}

public abstract class IBankingRepository
{
    public abstract Banking GetAccount(string code, string pass);
    public abstract Banking AddNewAccount(Banking item);
    public abstract bool TransferMoney(string sCode, string rCode, decimal amount);
}

public class BankingRepository : IBankingRepository
{
    private List<Banking> accounts = new List<Banking>();
    private int counter = 1;

    public BankingRepository()
    {
        AddNewAccount(new Banking { name = "An",code = "00050553001", pass = "123456", balance = 10000});
        AddNewAccount(new Banking { name = "Lan", code = "00050553002", pass = "123456", balance = 10000 });
    }

    public override Banking AddNewAccount(Banking item)
    {
        if (item == null)
            throw new NotImplementedException();
        item.id = counter++;
        accounts.Add(item);
        return item;
    }

    public override Banking GetAccount(string code, string pass)
    {
        var c = accounts.Find(b => b.code == code);
        if (!c.pass.Equals(pass))
        {
            throw new WebFaultException<string>("Bad Request", HttpStatusCode.BadRequest);
        }

        return c;
    }

    public override bool TransferMoney(string sCode, string rCode, decimal amount)
    {
        var s = accounts.Find(b => b.code == sCode);
        if (s == null) return false;

        var r = accounts.Find(c => c.code == rCode);
        if (r == null) return false;

        if (s.balance < amount)
        {
            throw new WebFaultException<string>("Do not have enough money", HttpStatusCode.BadRequest);
        }

        var newSBalance = s.balance - amount;
        var newRBalance = r.balance + amount;

        Banking accSUpdate = new Banking{name = s.name, balance = newSBalance, code = s.code, pass = s.pass};
        Banking accRUpdate = new Banking{name = r.name, balance = newRBalance, code = r.code, pass = r.pass };

        accounts.RemoveAt(s.id);
        accounts.RemoveAt(r.id);

        accounts.Add(accSUpdate);
        accounts.Add(accRUpdate);
        return true;
    }
}