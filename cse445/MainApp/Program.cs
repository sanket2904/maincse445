namespace cse445;

class Program
{
    static void Main(string[] args)
    {
        
        // initialize multicell buffer
        if (args.Length == 1) {
            if (args[0] == "1") { // use ticket agent 1 and start a new process
                TicketAgent1 ta1 = new TicketAgent1();
                ta1.Start();
            }
            
        } 
        
    }
}
