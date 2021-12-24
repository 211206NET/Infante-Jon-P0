namespace UI;

public class AllShoppingStoresMenu {
    private StoreBL _bl;
    private ColorWrite _cw;

    public AllShoppingStoresMenu(StoreBL bl){
        _bl = bl;
        _cw = new ColorWrite();
    }  
    public void Start(string userName){  
        
        List<Store> allStores = _bl.GetAllStores();
            bool exit = false;
            while (!exit){
                if(allStores.Count == 0){
                    Console.WriteLine("\nNo stores found!");
                    exit = true;
                }
                else{
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
                            ShoppingStoreMenu ssMenu = new ShoppingStoreMenu();
                            ssMenu.Start(index, userName);
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