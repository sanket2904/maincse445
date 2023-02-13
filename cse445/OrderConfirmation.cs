namespace cse445;
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

public class OrderConfirmationBuffer {
    private OrderConfirmation[] _buffer;
    private int _size;
    
    public event EventHandler<OrderConfirmation> AddedEvent;
    private SemaphoreSlim _semaphore;
    private readonly object _lock = new object();
    public OrderConfirmationBuffer() {
        _size = 3;
        _buffer = new OrderConfirmation[3];
       _semaphore = new SemaphoreSlim(3);
       for(var i = 0; i < 3; i++) {
            _buffer[i] = null;
        }
    }

    public void SetOneCell(OrderConfirmation order) {
        _semaphore.Wait();
        int cellIndex = -1;
        for (int i = 0; i < 3; i++) {
            if (_buffer[i] == null) {
                cellIndex = i;
                _buffer[cellIndex] = order;
                break;
            }
        }
        lock (_lock) { // lock the buffer cell 
            if (cellIndex != -1)  _buffer[cellIndex] = order;
            // check if event is null
            // Console.WriteLine("Firing Event");
            AddedEvent?.Invoke(this, order);
            
        }
    }

    public OrderConfirmation GetOneCell() {
    int cellIndex = -1;
    OrderConfirmation order = null;
    for (int i = 0; i < 3; i++) {
        if (_buffer[i] != null) {
            cellIndex = i;
            order = _buffer[cellIndex];
            _buffer[cellIndex] = null;
            break;
        }
    }

    lock (_lock) { // lock the buffer cell
        if (cellIndex != -1) _buffer[cellIndex] = null;
    }

    if (order != null) {
        _semaphore.Release();
    }
    return order;
}

}


// creating a static class for OrderConfirmationBuffer

public static class GlobalConfirmationBuffer {
    public static OrderConfirmationBuffer buffer = new OrderConfirmationBuffer();
}