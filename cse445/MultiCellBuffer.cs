namespace cse445;


// creating a multi cell buffer to store the orders from the clients with sephamore to control the access to the buffer

// each cell will be a reference to the order object

// number of cells available must be less than the Ticket Agents 

public class MultiCellBuffer {
    private OrderClass?[] _buffer;
    private int _numberOfCells;
    private Semaphore _semaphore;
    // private object[] _locks;

    public MultiCellBuffer() {
        _numberOfCells = 3;
        _buffer = new OrderClass[_numberOfCells];
        _semaphore = new Semaphore(_numberOfCells, _numberOfCells);
       
    }

    public void SetOneCell(OrderClass order) {
        _semaphore.WaitOne();
        int cellIndex = -1;
        for (int i = 0; i < _numberOfCells; i++) {
            if (_buffer[i] == null) {
                cellIndex = i;
                break;
            }
        }
        lock (_buffer) { // lock the buffer cell 
            _buffer[cellIndex] = order;
        }
    }

    public OrderClass GetOneCell() {
        OrderClass? order = null;
        int cellIndex = -1;
        for (int i = 0; i < _numberOfCells; i++) {
            if (_buffer[i] != null) {
                cellIndex = i;
                break;
            }
        }
        lock (_buffer) { // lock the buffer cell
            _buffer[cellIndex] = null;
        }
        _semaphore.Release();
        return order;
    }
}
