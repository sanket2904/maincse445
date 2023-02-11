namespace cse445;



public class OrderProcessing
{
    private Cruise _cruise;
    private BankService _bankService;
    private OrderClass _order;

    private OrderConfirmationBuffer _orderConfirmationBuffer;
    public OrderProcessing(Cruise cruise, BankService bankService, OrderClass order)
    {
        _cruise = cruise;
        _order = order;
        _bankService = bankService;
       
    }


    public void Start()
    {
        Console.WriteLine("Order processing started");
        Thread thread = new Thread(new ThreadStart(Run));
        thread.Start();
        thread.Join();
       
    }

    // this will be called by the cruise class with all the necessary daya

    public void Run() {
        ProcessOrder(_order);
    }




    public void ProcessOrder(OrderClass order)
    {
        int totalAmount = order.getUnitPrice() * order.getQuantity();
        totalAmount = (int)(totalAmount * 1.07);
        totalAmount += 100;

        if (_bankService.verifyCardNumber(order.getCardNo()))
        {
            int availableFunds = _bankService.getAccountFunds();
            if (availableFunds >= totalAmount)
            {
                // Deduct the total amount from the bank account
                _bankService.accountFunds -= totalAmount;

                // Decrement the available tickets
                if (order.getReceiverId().Equals("1")) {
                    AvailableTickets.availableTickets1 -= order.getQuantity();
                } else if (order.getReceiverId().Equals("2")) {
                    AvailableTickets.availableTickets2 -= order.getQuantity();
                } else if (order.getReceiverId().Equals("3")) {
                    AvailableTickets.availableTickets3 -= order.getQuantity();
                }
                string confirmationId = new Guid().ToString();

                // Send order confirmation to the Ticket Agent
                OrderConfirmation orderConfirmation = new OrderConfirmation(order.getSenderId(),order.getReceiverId(),confirmationId, "Successful", totalAmount );
                
                
                GlobalConfirmationBuffer.buffer.SetOneCell(orderConfirmation);
                
                Console.WriteLine("Order processed successfully. Total charge: " + totalAmount);

                // Send confirmation to the ticket agent (implementation not shown)
            }
            else
            {
                Console.WriteLine("Order processing failed. Insufficient funds.");
            }
        }
        else
        {
            Console.WriteLine("Order processing failed. Invalid credit card number.");
        }
    }
}
