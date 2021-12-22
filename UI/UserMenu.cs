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
                    List<Store> allStores = _bl.GetAllStores();
                    _cw.WriteColor("\n==================[All Stores]=================", ConsoleColor.DarkCyan);
                    int i = 0;
                    foreach(Store store in allStores){
                        Console.WriteLine($"[{i}] {store.ToString()}");
                        i++;
                    }
                    _cw.WriteColor("\n    Select the store's index to browse.\n   Or enter [r] to [Return] to the User Menu.", ConsoleColor.DarkYellow);
                    Console.WriteLine("=============================================");
                    string? select = Console.ReadLine();
                    int index;
                    //Returns to menu
                    if (select == "r"){
                        exit = true;
                        }
                    else {
                        //Checks for valid integer
                        if(!int.TryParse(select, out index)){
                            Console.WriteLine("\nPlease select a valid input!");
                        }
                        else{
                            if (index >= 0 && index < allStores.Count){
                                exit = true;
                                //Opens up store product menu
                            }  
                            //Index out of range
                            else{
                                Console.WriteLine("\nPlease select an index within range!");
                            }
                        }
                    }
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