

namespace cse445;
// creating 3 cruise classes along with each one firing a thread to run the cruise


public class Cruise {
    // this class uses the pricing model to determine the price of the cruise tickets and each cruise class will have different pricing model
    private int t = 0;
    public int availableTickets = 10000;
    private int currentPrice;
    private int oldPrice = 0;
    // creating a price change event which will be fired when the price of the ticket is less than the current price
    public delegate void PriceCutEventHandler(int price);
    public event PriceCutEventHandler? PriceCut;

    private int season;
    protected PricingModel? pricingModel;
    private MultiCellBuffer _buffer;
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
    public void Run() {
        // getting the order frrom the buffer and then firing up new thread for each order to process it
        // this method will run the cruise and will fire up a new thread for each order
        
        while (true) {
            OrderClass order = _buffer.GetOneCell();
            if (order != null) {
                var processingOrder = new OrderProcessing(this,);
                Thread thread = new Thread(new ThreadStart());
                thread.Start(order);
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

    

}

// cruise2 class extends the cruise class and will have a different pricing model

class Cruise2 : Cruise {
    public Cruise2(int season, MultiCellBuffer buffer) : base(season, buffer) {
        this.pricingModel = new PricingModel2(season);
    }
}

// cruise3 class extends the cruise class and will have a different pricing model

class Cruise3 : Cruise {
    public Cruise3(int season, MultiCellBuffer buffer) : base(season, buffer) {
        this.pricingModel = new PricingModel3(season);
    }
}


// creating a OrderProcessing class which will process the order and fire up a new thread, it will then check the validity of the credit card number and will calculate the price of the tickets and will then send the order to the client
