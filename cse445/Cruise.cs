

namespace cse445;
// creating 3 cruise classes along with each one firing a thread to run the cruise


public class Cruise {
    // this class uses the pricing model to determine the price of the cruise tickets and each cruise class will have different pricing model
    protected int t = 0;
    
    public virtual event EventHandler<int> PriceCutEvent;
    // creating a price change event which will be fired when the price of the ticket is less than the current price
    

    protected int season;
    protected PricingModel? pricingModel;
    protected MultiCellBuffer _buffer;
    public Cruise(int season, MultiCellBuffer buffer) {
        this.season = season;
        _buffer = buffer;
    }

    

    public virtual void Start() {
       
    }
    public virtual void Run() {
      
        
    }
    // creating a function to process order
    
}



// curise1 class extends the cruise class and will have a different pricing model


class Cruise1 : Cruise {
    public override event EventHandler<int> PriceCutEvent;
    public Cruise1(int season, MultiCellBuffer buffer) : base(season, buffer) {
        // create a random number generator from 1 to 4 to determine the season

        int seasonNumber = 1;
        this.pricingModel = new PricingModel1(seasonNumber);
    }
    public override void Start()
    {
       
        Thread thread = new Thread(Run);
        thread.Start();
        
    }

    public void OrderReqestHandler(object sender, OrderClass order) {
        OrderClass _order = _buffer.GetOneCell();
        foreach(var bankService in BankServiceList.bankserviceList) {
                    if (bankService.getCreditCard() == order.getCardNo()) {
                        // reduce the available tickets
                        var processingOrder = new OrderProcessing(this,bankService,order);
                        processingOrder.Start();
                    }
                    // manually decrementing the tickets price 
                }
                // decrease the price 
        PriceTracker1.currentPrice = PriceTracker1.currentPrice - 5;
        if (PriceTracker1.currentPrice < PriceTracker1.oldPrice && PriceTracker1.currentPrice > 0) {
            this.PriceCutEvent?.Invoke("1", PriceTracker1.currentPrice);
        }
        PriceTracker1.oldPrice = PriceTracker1.currentPrice;
    }
    

    public override void Run() {
        
        pricingModel = new PricingModel1(season);
        _buffer.RequestCruise1 += OrderReqestHandler;
        PriceTracker1.currentPrice = pricingModel.GetTicketPrice(1, season); 
       
        // we will supply the price to the order processing class
        if (PriceTracker1.oldPrice == 0) {
            PriceTracker1.oldPrice = PriceTracker1.currentPrice;
        }
        // GlobalPriceRequestBuffer.buffer.RequestEventCruise += PriceRequestEvent;
        if (PriceTracker1.currentPrice < PriceTracker1.oldPrice) {
           
            this.PriceCutEvent?.Invoke("1", PriceTracker1.currentPrice);
            
        }
        PriceTracker1.oldPrice = PriceTracker1.currentPrice;
        while( AvailableTickets.availableTickets1 > 0) {
           
        }
        
    }

    

}

// cruise2 class extends the cruise class and will have a different pricing model

class Cruise2 : Cruise {
    public override event EventHandler<int> PriceCutEvent;
    public Cruise2(int season, MultiCellBuffer buffer) : base(season, buffer) {
        int seasonNumber = 1;
        this.pricingModel = new PricingModel2(seasonNumber);
    }

    public override void Start() {
       
        Thread thread = new Thread(Run);
        thread.Start();
        
    }
    public void OrderReqestHandler(object sender, OrderClass order) {
        OrderClass _order = _buffer.GetOneCell();
        foreach(var bankService in BankServiceList.bankserviceList) {
                    if (bankService.getCreditCard() == order.getCardNo()) {
                        // reduce the available tickets
                        
                        var processingOrder = new OrderProcessing(this,bankService,order);
                        processingOrder.Start();
                       
                        
                    }
                    // manually decrementing the tickets price 


                }
                // decrease the price 
        PriceTracker2.currentPrice = PriceTracker2.currentPrice - 20;
        if (PriceTracker2.currentPrice < PriceTracker2.oldPrice && PriceTracker2.currentPrice > 0) {
            this.PriceCutEvent?.Invoke("2", PriceTracker2.currentPrice);
        }
        PriceTracker2.oldPrice = PriceTracker2.currentPrice;
    }
    public override void Run() {
        
        _buffer.RequestCruise2  += OrderReqestHandler;
        pricingModel = new PricingModel2(season);
        PriceTracker2.currentPrice = pricingModel.GetTicketPrice(1, season); // we will supply the price to the order processing class
        if (PriceTracker2.oldPrice == 0) {
            PriceTracker2.oldPrice = PriceTracker2.currentPrice;
        }
       
        if (PriceTracker2.currentPrice < PriceTracker2.oldPrice ) {
            this.PriceCutEvent?.Invoke("2", PriceTracker2.currentPrice);
        }
        PriceTracker2.oldPrice = PriceTracker2.currentPrice;

        while( AvailableTickets.availableTickets2 > 0){
            
        }
       
    }
}

// cruise3 class extends the cruise class and will have a different pricing model

class Cruise3 : Cruise {
    public override event EventHandler<int> PriceCutEvent;
    // lock for event
    private readonly object _lock = new object();
    public Cruise3(int season, MultiCellBuffer buffer) : base(season, buffer) {
        int seasonNumber = 1;
        this.pricingModel = new PricingModel3(seasonNumber);
    }

    public override void Start() {
       
        Thread thread = new Thread(Run);
        thread.Start();
        
    }

    public void OrderReqestHandler(object sender, OrderClass order) {
        OrderClass _order = _buffer.GetOneCell();
        foreach(var bankService in BankServiceList.bankserviceList) {
            if (bankService.getCreditCard() == order.getCardNo()) {
                var processingOrder = new OrderProcessing(this,bankService,order);
                processingOrder.Start();
            }
        }

        PriceTracker3.currentPrice = PriceTracker3.currentPrice - 20;
        if (PriceTracker3.currentPrice < PriceTracker3.oldPrice && PriceTracker3.currentPrice > 0) {
            
            lock(_lock) {
                this.PriceCutEvent?.Invoke("3", PriceTracker3.currentPrice);
            }
        
        }
        PriceTracker3.oldPrice = PriceTracker3.currentPrice;
            
    }


    public override void Run() {
        
        _buffer.RequestCruise3  += OrderReqestHandler;
        pricingModel = new PricingModel3(season);
        PriceTracker3.currentPrice = pricingModel.GetTicketPrice(1, season); // we will supply the price to the order processing class
        if (PriceTracker3.oldPrice == 0) {
            PriceTracker3.oldPrice = PriceTracker3.currentPrice;
        }
       
        
        PriceTracker3.oldPrice = PriceTracker3.currentPrice;
        while( AvailableTickets.availableTickets3 > 0) {
           
        }
        
    }
}


// creating a OrderProcessing class which will process the order and fire up a new thread, it will then check the validity of the credit card number and will calculate the price of the tickets and will then send the order to the client

// creating a static variable to keep track of the prices of the tickets and fire the price cut event

public static class PriceTracker1 {
    public static int currentPrice = 0;
    public static int oldPrice = 0;

}

public static class PriceTracker2 {
    public static int currentPrice = 0;
    public static int oldPrice = 0;

}

public static class PriceTracker3 {
    public static int currentPrice = 0;
    public static int oldPrice = 0;

}