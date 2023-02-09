


namespace cse445;

public class OrderClass {
    // senderId can be taken as thread name or thread id
    private string senderId;
    // card number which is an integer value
    private int cardNo;
    // receiver id 
    private string receiverId ;
    // number of tickets requested or quantity
    private int quantity ;

    // unit price of the ticket
    private int unitPrice ;

    // methods to set and get the private variables of the class

    public void setSenderId(string senderId) {
        this.senderId = senderId;
    }

    public string getSenderId() {
        return this.senderId;
    }

    public void setCardNo(int cardNo) {
        this.cardNo = cardNo;
    }

    public int getCardNo() {
        return this.cardNo;
    }

    public void setReceiverId(string receiverId) {
        this.receiverId = receiverId;
    }

    public string getReceiverId() {
        return this.receiverId;
    }

    public void setQuantity(int quantity) {
        this.quantity = quantity;
    }

    public int getQuantity() {
        return this.quantity;
    }

    public void setUnitPrice(int unitPrice) {
        this.unitPrice = unitPrice;
    }

    public int getUnitPrice() {
        return this.unitPrice;
    }

    // constructor to initialize the variables

    public OrderClass() 
    {
        this.quantity = 0;
        this.unitPrice = 0;
        this.cardNo = 0;
        this.senderId = "";
        this.receiverId = "";
    }


}