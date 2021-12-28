namespace UI;

public class UserMenu {
    private StoreBL _bl;
    private UserBL _iubl;

    public UserMenu(){
        _bl = new StoreBL();
        IURepo repo = new UserRepo();
        _iubl = new UserBL(repo);
    }
    public void Start(int userID){
        bool exit = false;
        User currUser =  _iubl.GetCurrentUserByID(userID);
        while(!exit){
            ColorWrite.wc("\n==================[User Menu]==================", ConsoleColor.DarkCyan);
            Console.WriteLine($"What would you like to do {currUser.Username}?\n");
            Console.WriteLine("[1] Browse Stores");
            Console.WriteLine("[2] View Profile");
            ColorWrite.wc("\n     Enter [r] to [Return] to the Login Menu", ConsoleColor.DarkYellow);
            Console.WriteLine("=============================================");

            string? inputSelection = Console.ReadLine();

            switch (inputSelection){
                case "1":
                    AllShoppingStoresMenu allSSMenu= new AllShoppingStoresMenu(_bl);
                    allSSMenu.Start(userID);
                    break;
                case "2":
                    UserProfileMenu upMenu = new UserProfileMenu();
                    upMenu.Start(userID);  
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