namespace cse445;
class Program
{
    static void Main(string[] args)
    {
        var b1 = new BankService(200000);
        var b2 = new BankService(200000);
        var b3 = new BankService(200000);
        var b4 = new BankService(200000);
        var b5 = new BankService(200000);
        BankServiceList.bankserviceList.Add(b1);
        BankServiceList.bankserviceList.Add(b2);
        BankServiceList.bankserviceList.Add(b3);
        BankServiceList.bankserviceList.Add(b4);
        BankServiceList.bankserviceList.Add(b5);
        var buffer = new MultiCellBuffer();
        var cruise1 = new Cruise1(1, buffer);
        var cruise2 = new Cruise2(2, buffer);
        var cruise3 = new Cruise3(3, buffer);
        cruise1.Start();
        Thread.Sleep(500);
        cruise2.Start();
        Thread.Sleep(500);
        cruise3.Start();
        // giving cruise time to initialize
        Thread.Sleep(1000);
        var TicketAgent = new TicketAgent1(1, buffer, 12300, new List<Cruise> {cruise1, cruise2, cruise3}, b1);
        // creating total of 5 ticket agents
        var TicketAgent2 = new TicketAgent2(2, buffer, 15000, new List<Cruise> {cruise1, cruise2, cruise3}, b2);
        var TicketAgent3 = new TicketAgent3(3, buffer, 18000, new List<Cruise> {cruise1, cruise2, cruise3}, b3);
        var TicketAgent4 = new TicketAgent4(4, buffer, 13000, new List<Cruise> {cruise1, cruise2, cruise3}, b4);
        var TicketAgent5 = new TicketAgent5(5, buffer, 21000, new List<Cruise> {cruise1, cruise2, cruise3}, b5);
        TicketAgent.Start();
        TicketAgent2.Start();
        TicketAgent3.Start();
        TicketAgent4.Start();
        TicketAgent5.Start();
    }
}

public static class GlobalPriceRequestBuffer {
    public static PriceRequestBuffer buffer = new PriceRequestBuffer();
}
