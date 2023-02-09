using cse445;
string num = Environment.GetCommandLineArgs()[0];  // read the argument from the command line


var buffer = cse445.Constants.buffer;  // get the buffer from the Constants class
if (num == "1") {
    // initiate the first TicketAgent from cse445
    cse445.TicketAgent1 ticketAgent1 = new cse445.TicketAgent1(1, buffer);
    
}
else if (num == "2") {
    // initiate the second TicketAgent from cse445
    cse445.TicketAgent2 ticketAgent2 = new cse445.TicketAgent2(2, buffer);
}
else if (num == "3") {
    // initiate the third TicketAgent from cse445
    cse445.TicketAgent3 ticketAgent3 = new cse445.TicketAgent3(3, buffer);
}
else if (num == "4") {
    // initiate the fourth TicketAgent from cse445
    cse445.TicketAgent4 ticketAgent4 = new cse445.TicketAgent4(4, buffer);
}
else if (num == "5") {
    // initiate the fifth TicketAgent from cse445
    cse445.TicketAgent5 ticketAgent5 = new cse445.TicketAgent5(5, buffer);
}