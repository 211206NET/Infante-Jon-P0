namespace UI;

public class UserProfileMenu {
    private StoreBL _bl;

    public UserProfileMenu(){
        _bl = new StoreBL();
    }
    public void Start(string userName){
        bool exit = false;
        while(!exit){
            ColorWrite.wc("\n=================[Profile Menu]=================", ConsoleColor.DarkCyan);
            Console.WriteLine("What would you like to do?\n");
            Console.WriteLine("[1] Shopping Cart");
            Console.WriteLine("[2] Previous Orders");
            ColorWrite.wc("\n      Enter [r] to [Return] to the User Menu", ConsoleColor.DarkYellow);
            Console.WriteLine("=============================================");

            string? input = Console.ReadLine();

            switch (input){
                case "1":
                    ShoppingCart sCart = new ShoppingCart();
                    sCart.Start(userName!);  
                    break;
                case "2":
                    break;
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