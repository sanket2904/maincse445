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

    public void ProcessOrder(int unitPrice, int numberOfTickets, int tax, int locationCharge, string creditCardNumber)
    {
        int totalAmount = unitPrice * numberOfTickets + tax + locationCharge;

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
