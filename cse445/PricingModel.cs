

namespace cse445;
// creating new pricing model for each cruise class





public class PricingModel {
    protected int _availableTickets = 1000;
    protected int _season;
    public PricingModel(int season) {
       this._season = season;
    }


    public virtual int GetTicketPrice(int numberOfTickets, int season) {
        int price = 0;
        if (_season == 1 ) { // spring season
            price = 80; 
        } else if (_season == 2) { // summer season
            price = 150;
        } else if (_season == 3) { // fall season
            price = 120;
        } else if (_season == 4) { // winter season
            price = 150;
        } 
        // if available tickets are less than 50 then the price will be 200
        if (AvailableTickets.availableTickets1 < 50) {
            price = 185;
        }
        if (numberOfTickets > 25) {
            price = price - (price * 20 / 100);
        }
        return price;
    }
}



public class PricingModel1 : PricingModel {
    public PricingModel1(int season) : base(season) {
        
    }
}



// creating price model for the cruise class 2

public class PricingModel2 : PricingModel {
    public PricingModel2(int season) : base(season) {
       
    }
    public override int GetTicketPrice(int numberOfTickets, int season)
    {
        int price = 0;
        if (_season == 1 ) { // spring season
            price = 100; 
        } else if (_season == 2) { // summer season
            price = 160;
        } else if (_season == 3) { // fall season
            price = 130;
        } else if (_season == 4) { // winter season
            price = 160;
        } 
        // if available tickets are less than 50 then the price will be 200
        if (AvailableTickets.availableTickets2 < 50) {
            price = 200;
        }
        if (numberOfTickets > 35) {
            price = price - (price * 20 / 100);
        }
        return price;
    }
}


// crating price model for the cruise class 3


public class PricingModel3:PricingModel {
    //creating price model based on the number of availabe tickets and based on the number of tickets requested along with the season of the year
   
    public PricingModel3( int season):base(season) {
       
    }
        
    

   // ticket price is between 40 and 200 after the season based pricing if the number of tickets requested is above 25 then the client will get 20% discount
    public override int GetTicketPrice(int numberOfTickets, int season) {
        int price = 0;
        if (_season == 1 ) { // spring season
            price = 120; 
        } else if (_season == 2) { // summer season
            price = 170;
        } else if (_season == 3) { // fall season
            price = 140;
        } else if (_season == 4) { // winter season
            price = 170;
        } 
        // if available tickets are less than 50 then the price will be 200
        if (AvailableTickets.availableTickets3 < 50) {
            price = 200;
        }
        if (numberOfTickets > 45) {
            price = price - (price * 20 / 100);
        }
        return price;
    }

}




// static management for available tickets for each cruise 

public static class AvailableTickets {
    public static int availableTickets1 = 1000;
    public static int availableTickets2 = 1500;
    public static int availableTickets3 = 2000;
}