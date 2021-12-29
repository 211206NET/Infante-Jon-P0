namespace UI;

public class UserOrderMenu {
    private StoreBL _bl;
    private UserBL _iubl;

    public UserOrderMenu(){
        _bl = new StoreBL();
        IURepo repo = new UserRepo();
        _iubl = new UserBL(repo);
    }
    public void Start(int userID){
        bool exit = false;
        User currUser = _iubl.GetCurrentUserByID(userID);
        List<StoreOrder> finishedOrders = currUser.FinishedOrders!;
        while(!exit){
            ColorWrite.wc("\n====================[Orders]===================", ConsoleColor.DarkCyan);
            foreach(StoreOrder storeorder in finishedOrders){
                Console.WriteLine($"\n{storeorder.Date}");
                Console.WriteLine("|-------------------------------------------|");
                foreach(ProductOrder pOrder in storeorder.Orders!){
                    Console.WriteLine($"| {pOrder.ItemName} | Qty: {pOrder.Quantity} || ${pOrder.TotalPrice}");
                }
                Console.WriteLine("|-------------------------------------------|");
                Console.WriteLine($"| Total Price: ${storeorder.TotalAmount}");
            }
            ColorWrite.wc("\n    Enter [r] to [Return] to the Profile Menu", ConsoleColor.DarkYellow);
            Console.WriteLine("=============================================");

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