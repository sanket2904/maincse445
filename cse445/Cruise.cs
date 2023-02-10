

namespace cse445;
// creating 3 cruise classes along with each one firing a thread to run the cruise


public class Cruise {
    // this class uses the pricing model to determine the price of the cruise tickets and each cruise class will have different pricing model
    protected int t = 0;
    
    protected int currentPrice;
    protected int oldPrice = 0;
    // creating a price change event which will be fired when the price of the ticket is less than the current price
    public delegate void PriceCutEventHandler(int price);
    public event PriceCutEventHandler? PriceCut;

    protected int season;
    protected PricingModel? pricingModel;
    protected MultiCellBuffer _buffer;
    public Cruise(int season, MultiCellBuffer buffer) {
        this.season = season;
        _buffer = buffer;
    }

    

    public void Start() {
        // this method will start the thread for the cruise
       
        pricingModel = new PricingModel1(season);
        currentPrice = pricingModel.GetTicketPrice(1, season); // we will supply the price to the order processing class
        if (oldPrice == 0) {
            oldPrice = currentPrice;
        }
        if (currentPrice < oldPrice) {
            PriceCut(currentPrice);
            
        }
        oldPrice = currentPrice;
        Thread thread = new Thread(Run);
        thread.Start();
    }
    public virtual void Run() {
        // getting the order frrom the buffer and then firing up new thread for each order to process it
        // this method will run the cruise and will fire up a new thread for each order
        
        while (true) {
            OrderClass order = _buffer.GetOneCell();
            
            if (order != null) {
                foreach(var bankService in BankServiceList.bankserviceList) {
                    
                    if (bankService.getCreditCard() == order.getCardNo()) {
                        var processingOrder = new OrderProcessing(this,bankService);
                        Thread thread = new Thread(new ThreadStart(() => processingOrder.Start(order)));
                        thread.Start();
                    }
                }
                
            }
        }
    }
    // creating a function to process order
    
}



// curise1 class extends the cruise class and will have a different pricing model


class Cruise1 : Cruise {
    public Cruise1(int season, MultiCellBuffer buffer) : base(season, buffer) {
        this.pricingModel = new PricingModel1(season);
    }

    public override void Run() {
        while (true) {
            OrderClass order = _buffer.GetOneCell();
            
            if (order != null && order.getReceiverId().Equals("1")) {
                foreach(var bankService in BankServiceList.bankserviceList) {
                    if (bankService.getCreditCard() == order.getCardNo()) {
                        // reduce the available tickets
                       
                        var processingOrder = new OrderProcessing(this,bankService);
                        Thread thread = new Thread(new ThreadStart(() => processingOrder.Start(order)));
                        thread.Start();
                    }
                }
                
            }
        }
    }

    

}

// cruise2 class extends the cruise class and will have a different pricing model

class Cruise2 : Cruise {
    public Cruise2(int season, MultiCellBuffer buffer) : base(season, buffer) {
        this.pricingModel = new PricingModel2(season);
    }
    public override void Run() {
        while (true) {
            OrderClass order = _buffer.GetOneCell();
            
            if (order != null && order.getReceiverId().Equals("2")) {
                foreach(var bankService in BankServiceList.bankserviceList) {
                    if (bankService.getCreditCard() == order.getCardNo()) {
                        var processingOrder = new OrderProcessing(this,bankService);
                        Thread thread = new Thread(new ThreadStart(() => processingOrder.Start(order)));
                        thread.Start();
                    }
                }
                
            }
        }
    }
}

// cruise3 class extends the cruise class and will have a different pricing model

class Cruise3 : Cruise {
    public Cruise3(int season, MultiCellBuffer buffer) : base(season, buffer) {
        this.pricingModel = new PricingModel3(season);
    }
    public override void Run() {
        while (true) {
            OrderClass order = _buffer.GetOneCell();
            
            if (order != null && order.getReceiverId().Equals("3")) {
                foreach(var bankService in BankServiceList.bankserviceList) {
                    if (bankService.getCreditCard() == order.getCardNo()) {
                        var processingOrder = new OrderProcessing(this,bankService);
                        Thread thread = new Thread(new ThreadStart(() => processingOrder.Start(order)));
                        thread.Start();
                    }
                }
                
            }
        }
    }
}


// creating a OrderProcessing class which will process the order and fire up a new thread, it will then check the validity of the credit card number and will calculate the price of the tickets and will then send the order to the client
