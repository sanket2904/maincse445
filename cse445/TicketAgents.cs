namespace cse445;

// making ticket agents 1 through 5 

public class TicketAgent {
    // Each ticket agent is a thread created by a different object instantiated from the same class
    private int _id;
    private int counter = 0;
    // each ticket agent will have a budget and will be able to buy tickets for the cruise
    private int _budget;
    private MultiCellBuffer _buffer;
    private int PriceCut;
    private List<Cruise> cruiseList;
    protected BankService? bankService;
    public TicketAgent(int id, MultiCellBuffer buffer,int budget, List<Cruise> cruiseList,BankService service) {
        _id = id;
        _buffer = buffer;
        _budget = budget;
        this.cruiseList = cruiseList;
        bankService = service;
    }
    public void Start() {
        // this method will start the thread for the ticket agent
        Thread thread = new Thread(new ThreadStart(Run));
        thread.Name = "Ticket Agent " + _id;
        thread.Start();
    }

    public void Run() {

        GlobalConfirmationBuffer.buffer.AddedEvent += OrderConfirmationHandler;
        foreach(var cruise in cruiseList) {
            cruise.PriceCutEvent += PriceCutHandler;
        };
        var cruise1 = cruiseList[0];
        var cruise2 = cruiseList[1];
        var cruise3 = cruiseList[2];
        var order = new OrderClass();
        order.setSenderId("Ticket Agent " + _id);
        order.setReceiverId("1");
        order.setCardNo(bankService.cardNumber);
        order.setUnitPrice(PriceTracker1.currentPrice);
        order.setQuantity((int)_budget / PriceTracker1.currentPrice);
        var order2 = new OrderClass();
        order2.setSenderId("Ticket Agent " + _id);
        order2.setReceiverId("2");
        order2.setCardNo(bankService.cardNumber);
        order2.setUnitPrice(PriceTracker2.currentPrice);
        order2.setQuantity((int)_budget / PriceTracker2.currentPrice);
        var order3 = new OrderClass();
        order3.setSenderId("Ticket Agent " + _id);
        order3.setReceiverId("3");
        order3.setCardNo(bankService.cardNumber);
        order3.setUnitPrice(PriceTracker3.currentPrice);
        order3.setQuantity((int)_budget / PriceTracker3.currentPrice);
        // put order in buffer
        _buffer.SetOneCell(order);
        _buffer.SetOneCell(order2);
        _buffer.SetOneCell(order3);
    }

    public void PriceCutHandler(Object sender,int price) {
        if (counter == 0) {
            Console.WriteLine("Price cut event fired for Cruise");
            counter = 1;
            var order = new OrderClass();
            order.setSenderId("Ticket Agent " + _id);
            order.setReceiverId(""+sender);
            order.setCardNo(bankService.cardNumber);
            order.setUnitPrice(price);
            order.setQuantity((int)_budget / price);
            _buffer.SetOneCell(order);
            
        }
    }
    public void OrderConfirmationHandler(Object sender, OrderConfirmation order) {
        // this method will be called when the order confirmation event is fired
        // this method will print the confirmation id
        GlobalConfirmationBuffer.buffer.GetOneCell();
        if (order.getSenderId() == "Ticket Agent " + _id) {
            Console.WriteLine("Confirmation id is " + order.getConfirmationId() + " for " + order.getSenderId() + " and the order status is " + order.getOrderStatus());
        }
    }
    
}

// creating ticket agent 1 to 5 inheriting from ticket agent



public class TicketAgent1: TicketAgent {
    public TicketAgent1(int id, MultiCellBuffer buffer, int budget, List<Cruise> cruiseList, BankService service) : base(id, buffer, budget, cruiseList, service) {
    }


}
public class TicketAgent2: TicketAgent {
    public TicketAgent2(int id, MultiCellBuffer buffer, int budget, List<Cruise> cruiseList, BankService service) : base(id, buffer, budget, cruiseList, service) {
    }
}


public class TicketAgent3: TicketAgent {
    public TicketAgent3(int id, MultiCellBuffer buffer, int budget, List<Cruise> cruiseList, BankService service) : base(id, buffer, budget, cruiseList, service) {
    }
}

public class TicketAgent4: TicketAgent {
    public TicketAgent4(int id, MultiCellBuffer buffer, int budget, List<Cruise> cruiseList, BankService service) : base(id, buffer, budget, cruiseList, service) {
    }
}

public class TicketAgent5: TicketAgent {
    public TicketAgent5(int id, MultiCellBuffer buffer, int budget, List<Cruise> cruiseList, BankService service) : base(id, buffer, budget, cruiseList, service) {
    }
}






