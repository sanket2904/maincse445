namespace cse445;
class Program
{
    static void Main(string[] args)
    {
        
        var b1 = new BankService(10000);
        var b2 = new BankService(10000);
        var b3 = new BankService(10000);
        var b4 = new BankService(10000);
        var b5 = new BankService(10000);

        BankServiceList.bankserviceList.Add(b1);
        BankServiceList.bankserviceList.Add(b2);
        BankServiceList.bankserviceList.Add(b3);
        BankServiceList.bankserviceList.Add(b4);
        BankServiceList.bankserviceList.Add(b5);
        
        var buffer = new MultiCellBuffer();

    }
}

