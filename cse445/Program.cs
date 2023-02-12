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

        var cruise1 = new Cruise1(1, buffer);
        var cruise2 = new Cruise2(2, buffer);
        var cruise3 = new Cruise3(3, buffer);
        var TicketAgent = new TicketAgent(1, buffer, 1230, new List<Cruise> {cruise1, cruise2, cruise3}, b1);
        // creating total of 5 ticket agents
        var TicketAgent2 = new TicketAgent(2, buffer, 1500, new List<Cruise> {cruise1, cruise2, cruise3}, b2);
        var TicketAgent3 = new TicketAgent(3, buffer, 1800, new List<Cruise> {cruise1, cruise2, cruise3}, b3);
        var TicketAgent4 = new TicketAgent(4, buffer, 1300, new List<Cruise> {cruise1, cruise2, cruise3}, b4);
        var TicketAgent5 = new TicketAgent(5, buffer, 2100, new List<Cruise> {cruise1, cruise2, cruise3}, b5);

        cruise1.Start();
        cruise2.Start();
        cruise3.Start();

        TicketAgent.Start();
        

    }
}

public static class GlobalPriceRequestBuffer {
    public static PriceRequestBuffer buffer = new PriceRequestBuffer();
}
