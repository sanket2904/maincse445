namespace cse445;

// making ticket agents 1 through 5 

public class TicketAgent {
    // Each ticket agent is a thread created by a different object instantiated from the same class
    private int _id;

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
        thread.Start();
    }

    public void Run() {
        // make an initial order based on the budget
        // get first cruise
        // get price

        var cruise1 = cruiseList[0];
        var order = new OrderClass();
        order.setSenderId("Ticket Agent " + _id);
        order.setReceiverId("Cruise");
        order.setCardNo(bankService.CreateCreditCardNumber());
        order.setUnitPrice(cruise1.currentPrice);
        order.setQuantity((int)_budget / cruise1.currentPrice);

        // put order in buffer

        _buffer.SetOneCell(order);


        // this will listen to the price cut event and then will create a new order object and will put it in the buffer
        foreach(var cruise in cruiseList) {
            cruise.PriceCut += PriceCutHandler; // this will listen to the price cut event from all three cruise classes
        };
        
        Console.WriteLine("test ticket agent");

    }

    public void PriceCutHandler(int price) {
        // this method will be called when the price cut event is fired
        // this method will create a new order object and will put it in the buffer
        Console.WriteLine("Price cut event fired by the cruise ");

    }
    
}
// creating all the ticket agents


// ticket agent 4

