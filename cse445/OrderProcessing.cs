namespace cse445;



public class OrderProcessing
{
    private Cruise _cruise;
    private BankService _bankService;

    public OrderProcessing(Cruise cruise, BankService bankService)
    {
        _cruise = cruise;
        _bankService = bankService;
    }


    public void Start(OrderClass order)
    {
        Thread thread = new Thread(new ThreadStart(() => Run(order)));
        thread.Start(order);
    }

    // this will be called by the cruise class with all the necessary daya

    public void Run(OrderClass order) {
        ProcessOrder(order);
    }




    public void ProcessOrder(OrderClass order)
    {
        int totalAmount = order.getUnitPrice() * order.getQuantity() + + locationCharge;

        if (_bankService.verifyCardNumber(creditCardNumber))
        {
            int availableFunds = _bankService.getAccountFunds();
            if (availableFunds >= totalAmount)
            {
                // Deduct the total amount from the bank account
                _bankService.accountFunds -= totalAmount;

                // Decrement the available tickets
                _cruise.availableTickets -= numberOfTickets;

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
