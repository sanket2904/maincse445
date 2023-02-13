namespace cse445;

public class PriceRequest {
    public int price { get; set; } = 0;
}


// price request buffer 
public class PriceRequestBuffer {
    private List<PriceRequest> _buffer;
    private int _size;

    public event EventHandler<PriceRequest> RequestEventCruise;
    public event EventHandler<PriceRequest> RequestEventAgent;
    private SemaphoreSlim _semaphore;
    public PriceRequestBuffer() {
        _size = 3;
        _buffer = new List<PriceRequest>();
        _semaphore = new SemaphoreSlim(3);
    }

    public void SetOneCell(PriceRequest price) {
        
        _semaphore.WaitAsync();
        lock(_buffer) {
           
            _buffer.Add(price);
        }
        
        if (price.price == 0) RequestEventCruise?.Invoke(this, price);
        else RequestEventAgent?.Invoke(this, price);
        
    }

    public PriceRequest GetOneCell() {
        PriceRequest price;
        lock(_buffer) {
            price = _buffer[0];
            _buffer.RemoveAt(0);
        }
        _semaphore.Release();
        return price;
    }
}