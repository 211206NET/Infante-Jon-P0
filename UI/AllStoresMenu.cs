namespace UI;

public class AllStoresMenu {
    private StoreBL _bl;

    public AllStoresMenu(StoreBL bl){
        _bl = bl;
    }  
    public void Start(){
        bool valid = false;
        while (!valid){
            List<Store> allStores = _bl.GetAllStores();
            //No stores exist
            if(allStores.Count == 0){
                Console.WriteLine("\nNo stores found!");
                valid = true;
                }
            //Found stores
            else{
            int i = 0;
            ColorWrite.wc("\n=================[All Stores]==================", ConsoleColor.DarkCyan);
            foreach(Store store in allStores){
                Console.WriteLine($"[{i}] {store.ToString()}");
                i++;
            }
            ColorWrite.wc("\n Select the store's ID to view its details.\n   Or enter [r] to [Return] to the Admin Menu.", ConsoleColor.DarkYellow);
            Console.WriteLine("=============================================");
            string? select = Console.ReadLine();
            int storeID;
            //Returns to menu
            if (select == "r"){
                valid = true;
                }
            else {
                //Checks for valid integer
                if(!int.TryParse(select, out storeID)){
                    Console.WriteLine("\nPlease select a valid input!");
                }
                else{
                    if (storeID >= 0 && storeID < allStores.Count){
                        storeID = (int)allStores[storeID].ID!;
                        valid = true;
                        //Opens up product menu                            
                        StoreMenu currStore = new StoreMenu(_bl);
                        currStore.Start(storeID);
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