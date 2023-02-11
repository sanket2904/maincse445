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

        

        // listen to price cut event
        // this will listen to the price cut event and then will create a new order object and will put it in the buffer
        foreach(var cruise in cruiseList) {
            cruise.PriceCutEvent += PriceCutHandler;
        };

        // listen to order confirmation event
        // this will listen to the order confirmation event and will print the confirmation id
        GlobalConfirmationBuffer.buffer.AddedEvent += OrderConfirmationHandler;
        
        

    }

    public void PriceCutHandler(Object sender,int price) {
        // this method will be called when the price cut event is fired
        // this method will create a new order object and will put it in the buffer
        Console.WriteLine("Price cut event fired by the cruise ");

    }
    public void OrderConfirmationHandler(Object sender, OrderConfirmation order) {
        // this method will be called when the order confirmation event is fired
        // this method will print the confirmation id
        if (order == GlobalConfirmationBuffer.buffer.GetOneCell()) {
           
            Console.WriteLine("Confirmation id is " + order.getConfirmationId());
        }
    }
    
}
// creating all the ticket agents


// ticket agent 4

// creating an orderConfirmation classs

public class OrderConfirmation {
    private string _senderId;
    private string _receiverId;
    private string _confirmationId;
    private string _orderStatus;

    
    private int _totalAmount;    

    public OrderConfirmation(string senderId, string receiverId, string confirmationId, string orderStatus, int totalAmount) {
        _senderId = senderId;
        _receiverId = receiverId;
        _confirmationId = confirmationId;
        _orderStatus = orderStatus;
       
        _totalAmount = totalAmount;
    }

    public string getSenderId() {
        return _senderId;
    }

    public string getReceiverId() {
        return _receiverId;
    }

    public string getConfirmationId() {
        return _confirmationId;
    }

    public string getOrderStatus() {
        return _orderStatus;
    }

    
}


// creating an orderConfirmationBuffer for ticket agents


public class OrderConfirmationBuffer {
    private List<OrderConfirmation> _buffer;
    private int _size;
    
    public event EventHandler<OrderConfirmation> AddedEvent;
    private SemaphoreSlim _semaphore;
    
    public OrderConfirmationBuffer() {
        _size = 3;
        _buffer = new List<OrderConfirmation>();
       _semaphore = new SemaphoreSlim(3);
    }

    public void SetOneCell(OrderConfirmation order) {
        _semaphore.WaitAsync();
        lock(_buffer) {
            _buffer.Add(order);
        }
        AddedEvent?.Invoke(this, order);

    }

    public OrderConfirmation GetOneCell() {
        OrderConfirmation order;
        lock(_buffer) {
            order = _buffer[0];
            _buffer.RemoveAt(0);
        }
        _semaphore.Release();
        return order;
    }
}


// creating a static class for OrderConfirmationBuffer

public static class GlobalConfirmationBuffer {
    public static OrderConfirmationBuffer buffer = new OrderConfirmationBuffer();
}