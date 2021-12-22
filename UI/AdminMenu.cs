namespace UI;
public class AdminMenu {
    private ColorWrite _cw;
    private StoreBL _bl;

    public AdminMenu(){
        _bl = new StoreBL();
        _cw = new ColorWrite();
    }

    public void Start(){
        bool exit = false;
        while(!exit){
            _cw.WriteColor("\n=================[Admin Menu]=================", ConsoleColor.DarkCyan);
            Console.WriteLine("What would you like to do?\n");
            Console.WriteLine("[1] Add a new Store");
            Console.WriteLine("[2] View all Stores");            
            _cw.WriteColor("\n  Enter [r] to [Return] to the Login Menu", ConsoleColor.DarkYellow);
            Console.WriteLine("============================================");
            
            string? input = Console.ReadLine();

            switch (input){
                case "1":
                    Console.WriteLine("Name: ");
                    string? name = Console.ReadLine();
                    Console.WriteLine("City: ");
                    string? city = Console.ReadLine();
                    Console.WriteLine("State: ");
                    string? state = Console.ReadLine();
                    Console.WriteLine("Address: ");
                    string? address = Console.ReadLine();

                    Store newStore= new Store{
                        Name = name!,
                        City = city!,
                        State = state!,
                        Address = address!
                    };
                    //Adds a store to the list of stores
                    _bl.AddStore(newStore);
                
                    break;
                case "2":
                    //Navigates to list of all stores
                    AllStoresMenu storesMenu = new AllStoresMenu(_bl);
                    storesMenu.Start();          
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