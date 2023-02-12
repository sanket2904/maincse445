namespace cse445;


// creating a multi cell buffer to store the orders from the clients with sephamore to control the access to the buffer

// each cell will be a reference to the order object

// number of cells available must be less than the Ticket Agents 

public class MultiCellBuffer {
    private OrderClass?[] _buffer;
    private int _numberOfCells;
    private Semaphore _semaphore;

    public event EventHandler<OrderClass> RequestCruise1;
    public event EventHandler<OrderClass> RequestCruise2;
    public event EventHandler<OrderClass> RequestCruise3;

    private readonly object _lock = new object();
    // private object[] _locks;

    public MultiCellBuffer() {
        _numberOfCells = 3;
        _buffer = new OrderClass[3];
        _semaphore = new Semaphore(3,3);
        for(var i = 0; i < _numberOfCells; i++) {
            _buffer[i] = null;
        }
    }

    public void SetOneCell(OrderClass order) {
        
        
        _semaphore.WaitOne();
        
        if (order != null) {
        

            int cellIndex = -1;
            for (int i = 0; i < _numberOfCells; i++) {
                    if (_buffer[i] == null) {
                        _buffer[i] = order;
                        cellIndex = i;
                        break;
                    }
            }
             if (order.getReceiverId().Equals("1")) {
                   
                    RequestCruise1?.Invoke(this, order);
                }
                else if (order.getReceiverId().Equals("2")) {
                    
                    RequestCruise2?.Invoke(this, order);
                }
                else if (order.getReceiverId().Equals("3")) {
                    
                    RequestCruise3?.Invoke(this, order);
                }
            lock (_lock) { // lock the buffer cell 
            
               
                if (cellIndex != -1)  _buffer[cellIndex] = order;
                
            }
        
        }
        
        
    }

    public OrderClass GetOneCell() {
        
        int cellIndex = -1;
        
        OrderClass order = null;
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
    public bool IsEmpty() {
        for (int i = 0; i < _numberOfCells; i++) {
            if (_buffer[i] != null) {
                return false;
            }
        }
        return true;
    }
    // create a function which will return the same instance of the buffer
    
}
