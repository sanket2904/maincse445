

// namespace cse445;
// // creating 3 cruise classes along with each one firing a thread to run the cruise


// public class Cruise1 {
//     // this class uses the pricing model to determine the price of the cruise tickets and each cruise class will have different pricing model
//     private int t = 0;
//     private int availableTickets = 10000;
//     private int currentPrice;
//     private int oldPrice = 0;
//     // creating a price change event which will be fired when the price of the ticket is less than the current price
//     public delegate void PriceCutEventHandler(int price);
//     public event PriceCutEventHandler PriceCut;

//     private int season;
//     private PricingModel1 pricingModel;
//     private MultiCellBuffer _buffer;
//     public Cruise1(int season, MultiCellBuffer buffer) {
//         this.season = season;
//         _buffer = buffer;
//     }

//     public void Start() {
//         // this method will start the thread for the cruise
       
//         pricingModel = new PricingModel1(season);
//         currentPrice = pricingModel.GetTicketPrice(1, season); // we will supply the price to the order processing class
//         if (oldPrice == 0) {
//             oldPrice = currentPrice;
//         }
//         if (currentPrice < oldPrice) {
//             PriceCut(currentPrice);
            
//         }
//         oldPrice = currentPrice;
//         Thread thread = new Thread(Run);
//         thread.Start();
//     }
//     public void Run() {
//         // getting the order frrom the buffer and then firing up new thread for each order to process it
//         while (true) {
//             // much work to do 
//             OrderClass order = _buffer.GetOneCell();
//             Thread thread = new Thread(new ThreadStart(() => ProcessOrder(order)));
//             thread.Start();
//         }
//     }
// }



// // curise 2 class



// // creating a OrderProcessing class which will process the order and fire up a new thread, it will then check the validity of the credit card number and will calculate the price of the tickets and will then send the order to the client
