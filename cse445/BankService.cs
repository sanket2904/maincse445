

using System.Collections;
using System.Security.Cryptography;
using System.Text;


namespace cse445;

public class BankService 
{
    public int cardNumber;
    public int accountFunds;
    string[] creditCardList = new string[0];


    //generate unique credit card number
    public static Random RNG= new Random();
    string CreateCreditCardNumber()
    {
        //do while loop to make sure unique credit card number is generated
        bool check = false;
        string newCC;
        do
        {
            //generate new random 16 digit credit card number
            var builder = new StringBuilder();
            while (builder.Length < 16)
            {
                builder.Append(RNG.Next(10).ToString());
            }
            newCC = builder.ToString();
            //check if newly generated credit card already exist in array
            check = verifyCardNumber(newCC);
        }while(check);

        //add new credit card number into new array of credit cards with size +1
        string[] newArr = new string[creditCardList.Length + 1];
        for(int i = 0; i < creditCardList.Length; i++)
        {
            newArr[i] = creditCardList[i]; 
        }
        newArr[newArr.Length - 1] = newCC;
        //credit card list is now updated with the new credit card 
        creditCardList = newArr;
        return newCC;
    }

    //Verify if Credit Card number exists
    public bool verifyCardNumber(string cardNumber)
    {

        // decrypt the credit card number
        

        for (int i = 0; i < creditCardList.Length; i++)
        {
            if (creditCardList[i].Equals(cardNumber))
                return true;
        }
        return false;
    }

    //Constructor
    public BankService(int accountFunds)
    {
        cardNumber = CreateCreditCardNumber();
        this.accountFunds = accountFunds;
    }

    //

    //get credit card number
    public int getCreditCard()
    {
        return cardNumber;
    }

    //get account funds amount
    public int getAccountFunds() 
    {
        return accountFunds;
    }

    //allows user to depost funds into account
    public void depositFunds(int funds)
    {
        this.accountFunds = this.accountFunds + funds;
    }

    public void withdrawalFunds(int funds)
    {
        this.accountFunds = this.accountFunds - funds;
    }

    //
    public string purchase(string cardNumber, int cost) 
    {
        // this method will verify the card number using the bank service
        if(verifyCardNumber(cardNumber) && getAccountFunds() >= cost) 
        {
            withdrawalFunds(cost);
            return "Valid";
        }
        else
            return "Not Valid";
    }
    

}


public static class BankServiceList {
    public static List<BankService> bankserviceList = new List<BankService>();
}