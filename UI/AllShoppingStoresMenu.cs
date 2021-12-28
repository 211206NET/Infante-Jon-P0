namespace UI;

public class AllShoppingStoresMenu {
    private StoreBL _bl;

    public AllShoppingStoresMenu(StoreBL bl){
        _bl = bl;
    }  
    public void Start(int userID){  
        
        List<Store> allStores = _bl.GetAllStores();
            bool exit = false;
            while (!exit){
                if(allStores.Count == 0){
                    Console.WriteLine("\nNo stores found!");
                    exit = true;
                }
                else{
                int i = 0;
                ColorWrite.wc("\n==================[All Stores]=================", ConsoleColor.DarkCyan);
                foreach(Store store in allStores){
                    Console.WriteLine($"[{i}] {store.ToString()}");
                    i++;
                }
                ColorWrite.wc("\n     Select the store's ID to browse.\n   Or enter [r] to [Return] to the User Menu.", ConsoleColor.DarkYellow);
                Console.WriteLine("=============================================");
                string? select = Console.ReadLine();
                int storeIndex;
                //Returns to menu
                if (select == "r"){
                    exit = true;
                    }
                else {
                    //Checks for valid integer
                    if(!int.TryParse(select, out storeIndex)){
                        Console.WriteLine("\nPlease select a valid input!");
                    }
                    else{
                        if (storeIndex >= 0 && storeIndex < allStores.Count){
                            int storeID = (int)allStores[storeIndex].ID!;
                            //Opens up store product menu
                            ShoppingStoreMenu ssMenu = new ShoppingStoreMenu();
                            ssMenu.Start(storeID, userID);
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