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
            _cw.WriteColor("\n=============[Admin Menu]=============", ConsoleColor.DarkCyan);
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("[1] Add a new Store");
            Console.WriteLine("[2] View all Stores");
            _cw.WriteColor("\n Enter [r] to [Return] to the Main Menu", ConsoleColor.DarkYellow);
            Console.WriteLine("====================================");
            
            string input = Console.ReadLine();

            switch (input){
                case "1":
                    Console.WriteLine("Name: ");
                    string name = Console.ReadLine();
                    Console.WriteLine("City: ");
                    string city = Console.ReadLine();
                    Console.WriteLine("State: ");
                    string state = Console.ReadLine();
                    Console.WriteLine("Address: ");
                    string address = Console.ReadLine();

                    Store newStore= new Store{
                        Name = name,
                        City = city,
                        State = state,
                        Address = address
                    };

                    _bl.AddStore(newStore);
                
                    break;
                case "2":
                    List<Store> allStores = _bl.GetAllStores();
                            if(allStores.Count == 0){
                                Console.WriteLine("No stores found!");
                            }
                            else{
                                Console.WriteLine("Here are all your stores!");
                                for(int i = 0; i < allStores.Count; i++){
                                    Console.WriteLine($"[{i}] Store: {allStores[i].Name}\n    City: {allStores[i].City}, State: {allStores[i].State}");
                                    Console.WriteLine($"    Address: {allStores[i].Address}");
                            }
                            }
                        bool valid = false;
                        while (!valid){
                            _cw.WriteColor("\nSelect one from the store's index to view or edit it's products.\nOr press [r] to [Return] to the Admin Menu.", ConsoleColor.DarkYellow);
                            string select = Console.ReadLine();
                            int index;
                            if (select != "r"){
                                if(!int.TryParse(select, out index)){
                                    Console.WriteLine("Please select a valid input!");
                                }
                            else{
                                valid = true;
                            }
                                
                    }
                    }

                    break;
                case "r":
                    exit = true;
                    break;                
                default:
                    Console.WriteLine("I did not expect that command! Please try again with a valid input.");
                    break;
            }
        }
    }
}