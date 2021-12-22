namespace UI;

public class UserMenu {
    private StoreBL _bl;
    private ColorWrite _cw;

    public UserMenu(){
        _bl = new StoreBL();
        _cw = new ColorWrite();
    }
    public void Start(string userName){
        bool exit = false;
        while(!exit){
            _cw.WriteColor("\n==================[User Menu]==================", ConsoleColor.DarkCyan);
            Console.WriteLine("What would you like to do?\n");
            Console.WriteLine("[1] Browse Stores");
            Console.WriteLine("[2] View Profile");
            _cw.WriteColor("\n      Enter [r] to [Return] to the Login Menu", ConsoleColor.DarkYellow);
            Console.WriteLine("=============================================");

            string? input = Console.ReadLine();

            switch (input){
                case "1":
                    AllShoppingStoresMenu allSSMenu= new AllShoppingStoresMenu(_bl);
                    allSSMenu.Start(userName);
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