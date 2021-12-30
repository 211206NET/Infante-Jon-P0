namespace UI;

public class StoreOrderMenu {
    private StoreBL _bl;
    private UserBL _iubl;

    public StoreOrderMenu(StoreBL bl){
        _bl = bl;
        IURepo repo = new UserRepo();
        _iubl = new UserBL(repo);
    }
    public void Start(int storeID){
        bool exit = false;
        Store currStore = _bl.GetStoreByID(storeID!);
        List<StoreOrder> allOrders = currStore.AllOrders!;
        bool reversed = false;
        while(!exit){
            if(allOrders == null || allOrders.Count == 0){
                Console.WriteLine("\nNo Orders found!");
                exit = true;
                }
            else{
            ColorWrite.wc("\n====================[Orders]===================", ConsoleColor.DarkCyan);
            foreach(StoreOrder storeorder in allOrders!){
                User userWhoOrdered = _iubl.GetCurrentUserByID((int)storeorder.userID!);
                Console.WriteLine($"\nPlaced on {storeorder.Date} by {userWhoOrdered.Username}");
                Console.WriteLine("|-------------------------------------------|");
                foreach(ProductOrder pOrder in storeorder.Orders!){
                    Console.WriteLine($"| {pOrder.ItemName} | Qty: {pOrder.Quantity} || ${pOrder.TotalPrice}");
                }
                Console.WriteLine("|-------------------------------------------|");
                Console.WriteLine($"| Total Price: ${storeorder.TotalAmount}");
            }
            if(!reversed){
                ColorWrite.wc("\nEnter [s] to to [Sort] your orders by most recent", ConsoleColor.Cyan);
            }
            else{
                ColorWrite.wc("\nEnter [s] to to [Sort] your orders by last ordered", ConsoleColor.Cyan);
            }
            ColorWrite.wc("    Enter [r] to [Return] to the Store Menu", ConsoleColor.DarkYellow);
            Console.WriteLine("============================================");

            string? input = Console.ReadLine();

            switch (input){
                case "r":
                    exit = true;
                    break;
                case "s":
                    //Sorts the orders in most recent first
                    if (!reversed){
                        reversed = true;
                        allOrders.Reverse();
                    }
                    //Sorts the orders by last ordered first
                    else{
                        reversed = false;
                        allOrders.Reverse();
                    }
                    break;
                default:
                    Console.WriteLine("\nI did not expect that command! Please try again with a valid input.");
                    break;       
            }
        }
        }
    }
 }