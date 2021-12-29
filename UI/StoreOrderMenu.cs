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
        while(!exit){
            ColorWrite.wc("\n====================[Orders]===================", ConsoleColor.DarkCyan);
            foreach(StoreOrder storeorder in allOrders){
                User userWhoOrdered = _iubl.GetCurrentUserByID((int)storeorder.userID!);
                Console.WriteLine($"\nPlaced on {storeorder.Date} by {userWhoOrdered.Username}");
                Console.WriteLine("|------------------------------------------|");
                foreach(ProductOrder pOrder in storeorder.Orders!){
                    Console.WriteLine($"| {pOrder.ItemName} | Qty: {pOrder.Quantity} || ${pOrder.TotalPrice}");
                }
                Console.WriteLine("|------------------------------------------|");
                Console.WriteLine($"| Total Price: ${storeorder.TotalAmount}");
            }
            ColorWrite.wc("\n    Enter [r] to [Return] to the Store Menu", ConsoleColor.DarkYellow);
            Console.WriteLine("============================================");

            string? input = Console.ReadLine();

            switch (input){
                case "r":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("\nI did not expect that command! Please try again with a valid input.");
                    break;       
            }
        }

    }
 }