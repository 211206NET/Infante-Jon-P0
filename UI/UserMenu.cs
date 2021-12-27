namespace UI;

public class UserMenu {
    private StoreBL _bl;

    public UserMenu(){
        _bl = new StoreBL();
    }
    public void Start(string userName){
        bool exit = false;
        while(!exit){
            ColorWrite.wc("\n==================[User Menu]==================", ConsoleColor.DarkCyan);
            Console.WriteLine($"What would you like to do {userName}?\n");
            Console.WriteLine("[1] Browse Stores");
            Console.WriteLine("[2] View Profile");
            ColorWrite.wc("\n     Enter [r] to [Return] to the Login Menu", ConsoleColor.DarkYellow);
            Console.WriteLine("=============================================");

            string? input = Console.ReadLine();

            switch (input){
                case "1":
                    AllShoppingStoresMenu allSSMenu= new AllShoppingStoresMenu(_bl);
                    allSSMenu.Start(userName);
                    break;
                case "2":
                    UserProfileMenu upMenu = new UserProfileMenu();
                    upMenu.Start(userName!);  
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