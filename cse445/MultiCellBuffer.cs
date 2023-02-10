namespace cse445;


// creating a multi cell buffer to store the orders from the clients with sephamore to control the access to the buffer

// each cell will be a reference to the order object

// number of cells available must be less than the Ticket Agents 

public class MultiCellBuffer {
    private OrderClass?[] _buffer;
    private int _numberOfCells;
    private SemaphoreSlim _semaphore;
    // private object[] _locks;

    public MultiCellBuffer() {
        _numberOfCells = 3;
        _buffer = new OrderClass[3];
        _semaphore = new SemaphoreSlim(3);
        for(var i = 0; i < _numberOfCells; i++) {
            _buffer[i] = null;
        }
    }

    public void SetOneCell(OrderClass order) {
        
        _semaphore.WaitAsync();
        // Console.WriteLine(_semaphore.CurrentCount);
        int cellIndex = -1;
        for (int i = 0; i < _numberOfCells; i++) {
                if (_buffer[i] == null) {
                  
                    cellIndex = i;
                    break;
                }
            }
        lock (_buffer) { // lock the buffer cell 
           
            
           
            if (cellIndex != -1)  _buffer[cellIndex] = order;
            
        }
        
        
    }

    public OrderClass GetOneCell() {
        
        int cellIndex = -1;
        for (int i = 0; i < _numberOfCells; i++) {
                if (_buffer[i] != null) {
                    cellIndex = i;
                    _semaphore.Release();
                    OrderClass order = _buffer[cellIndex];
                    _buffer[cellIndex] = null;
                    return order;
                    
                }
            }
           
        lock (_buffer) { // lock the buffer cell
            
            
            if (cellIndex != -1) _buffer[cellIndex] = null;
           
        }
       
        _semaphore.Release();
        if (cellIndex == -1) {
            return null;
        }
        return _buffer[cellIndex];
    }
    // create a function which will return the same instance of the buffer
    
}
