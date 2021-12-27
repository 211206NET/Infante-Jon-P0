namespace UI;

public class StoreMenu {
    private StoreBL _bl;

    public StoreMenu(StoreBL bl){
        _bl = bl;
    }
    public void Start(int index){
        bool exit = false;
        List<Store> allStores = _bl.GetAllStores();
        Store currStore = allStores[index];
        while(!exit){
            ColorWrite.wc("\n==================[Store Menu]=================", ConsoleColor.DarkCyan);
            Console.WriteLine($"Store: {currStore.Name}\n");
            Console.WriteLine("[1] Add a product");
            Console.WriteLine("[2] List all products");
            ColorWrite.wc("\n      Enter [r] to [Return] to the Admin Menu", ConsoleColor.DarkYellow);
            Console.WriteLine("=============================================");
            string? input = Console.ReadLine();

            switch (input){
                //Adding a new product
                case "1":
                    //initialize products list
                    if (currStore.Products == null){
                        currStore.Products = new List<Product>();
                     }
                    List<Product> products = currStore.Products!; 
                    int id = 0;
                    //get new product id
                    if (products.Count > 0){
                        foreach (Product product in products){
                            id++;
                        }
                    }
                    Console.WriteLine("Name: ");
                    string? name = Console.ReadLine();
                    Console.WriteLine("Description: ");
                    string? description = Console.ReadLine();
                    reEnterP:
                    Console.WriteLine("Price: ");
                    string? price = Console.ReadLine();
                    reEnterQ:
                    Console.WriteLine("Quantity: ");
                    string? quantity = Console.ReadLine();
                    try{
                    Product newProduct= new Product{
                        ID = id!,
                        Name = name!,
                        Description = description!,
                        Price = price!,
                        Quantity = quantity!
                    };
                    _bl.AddProduct(index, newProduct);
                    }
                    catch(InputInvalidException ex){
                        Console.WriteLine(ex.Message);
                        if (ex.Message.Substring(0, 1) == "P"){
                            goto reEnterP;
                        }
                        else{
                            goto reEnterQ;
                        }
                    }
                    //Add a product to the store
                    
                    Console.WriteLine($"\n{name} has been added to the current store!");
                    break;
                case "2":
                    ProductMenu prodMenu = new ProductMenu(_bl);
                    prodMenu.Start(index);
                    break;
                //Return to the Admin Menu
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