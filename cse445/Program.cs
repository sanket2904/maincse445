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

        var cruise1 = new Cruise(1, buffer);
        var cruise2 = new Cruise(2, buffer);
        var cruise3 = new Cruise(3, buffer);
        var TicketAgent = new TicketAgent(1, buffer, 1000, new List<Cruise> {cruise1, cruise2, cruise3}, b1);


        cruise1.Start();
        cruise2.Start();
        cruise3.Start();
        TicketAgent.Start();
        

    }
}

