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
            _cw.WriteColor("\n==============[Admin Menu]==============", ConsoleColor.DarkCyan);
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("[1] Add a new Store");
            Console.WriteLine("[2] View all Stores");            
            _cw.WriteColor("\n  Enter [r] to [Return] to the Login Menu", ConsoleColor.DarkYellow);
            Console.WriteLine("======================================");
            
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
                    //Lists all stores
                    FindStores();
          
                    break;
                case "3":

                case "r":
                    exit = true;
                    break;                
                default:
                    Console.WriteLine("I did not expect that command! Please try again with a valid input.");
                    break;
            }
        }        
    }
        public void FindStores(){
            List<Store> allStores = _bl.GetAllStores();
                if(allStores.Count == 0){
                    Console.WriteLine("No stores found!");
                }
                else{
                    Console.WriteLine("\nHere are all your stores!\n");
                    for(int i = 0; i < allStores.Count; i++){
                        Console.WriteLine($"[{i}] Store: {allStores[i].Name}\n    City: {allStores[i].City}, State: {allStores[i].State}");
                        Console.WriteLine($"    Address: {allStores[i].Address}");
                    }
                }
                bool valid = false;
                while (!valid){
                    _cw.WriteColor("\nSelect the store's index to view or edit it's products.\nOr enter [r] to [Return] to the Admin Menu.", ConsoleColor.DarkYellow);
                    string? select = Console.ReadLine();
                    int index;
                    //Returns to menu
                    if (select == "r"){
                        valid = true;
                        }
                    else {
                        //Checks for valid integer
                        if(!int.TryParse(select, out index)){
                            Console.WriteLine("Please select a valid input!");
                        }
                        else{
                            valid = true;
                            //Opens up product menu                            
                            StoreMenu currStore = new StoreMenu(_bl);
                            currStore.Start(index);
                        }  
                    }
                }        
        }

    }