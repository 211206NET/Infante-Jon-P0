namespace UI;

public class AllStoresMenu {
    private StoreBL _bl;
    private ColorWrite _cw;

    public AllStoresMenu(StoreBL bl){
        _bl = bl;
        _cw = new ColorWrite();
    }  
    public void Start(){
        List<Store> allStores = _bl.GetAllStores();
        //No stores exist

        bool valid = false;
        while (!valid){
            if(allStores.Count == 0){
                Console.WriteLine("No stores found!");
                valid = true;
                }
            //Found stores
            else{   
            _cw.WriteColor("\n=================[All Stores]=================", ConsoleColor.DarkCyan);
            int i = 0;
            foreach(Store store in allStores){
                Console.WriteLine($"[{i}] {store.ToString()}");
                i++;
            }
            _cw.WriteColor("\nSelect the store's index to view its details.\n   Or enter [r] to [Return] to the Admin Menu.", ConsoleColor.DarkYellow);
            Console.WriteLine("=============================================");
            string? select = Console.ReadLine();
            int index;
            //Returns to menu
            if (select == "r"){
                valid = true;
                }
            else {
                //Checks for valid integer
                if(!int.TryParse(select, out index)){
                    Console.WriteLine("\nPlease select a valid input!");
                }
                else{
                    if (index >= 0 && index < allStores.Count){
                        valid = true;
                        //Opens up product menu                            
                        StoreMenu currStore = new StoreMenu(_bl);
                        currStore.Start(index);
                    }  
                    //Index out of range
                    else{
                        Console.WriteLine("\nPlease select an index within range!");
                    }   
                }
            }   
        }     
    }
    }
}