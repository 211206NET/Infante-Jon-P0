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
        bool timeSort = false;
        bool costSort = false;
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
            if(!timeSort){
                ColorWrite.wc("\nEnter [s] to to [Sort] orders by most recent", ConsoleColor.Magenta);
            }
            else{
                ColorWrite.wc("\nEnter [s] to to [Sort] orders by first ordered", ConsoleColor.Magenta);
            }
            if(costSort){
                ColorWrite.wc(" Enter [c] to [Sort] orders by most expensive", ConsoleColor.Green);
            }
            else{
                ColorWrite.wc(" Enter [c] to [Sort] orders by least expensive", ConsoleColor.Green);
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
                    if (!timeSort){
                        timeSort = true;
                        allOrders.Sort((x, y) => y.DateSeconds.CompareTo(x.DateSeconds));
                    }
                    //Sorts the orders by last ordered first
                    else{
                        timeSort = false;
                        allOrders.Sort((x, y) => x.DateSeconds.CompareTo(y.DateSeconds));
                    }
                    break;
                case "c":
                    //Sorts the orders in most expensive first
                    if (!costSort){
                        costSort = true;
                        allOrders.Sort((x, y) => x.TotalAmount.CompareTo(y.TotalAmount));
                    }
                    //Sorts the orders by least expensive first
                    else{
                        costSort = false;
                        allOrders.Sort((x, y) => y.TotalAmount.CompareTo(x.TotalAmount));
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