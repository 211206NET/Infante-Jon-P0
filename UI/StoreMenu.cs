namespace UI;

public class StoreMenu {
    private StoreBL _bl;

    public StoreMenu(StoreBL bl){
        _bl = bl;
    }
    public void Start(int storeID){
        bool exit = false;
        Store currStore = _bl.GetStoreByID(storeID);
        while(!exit){
            ColorWrite.wc("\n==================[Store Menu]=================", ConsoleColor.DarkCyan);
            Console.WriteLine($"Store: {currStore.Name}\n");
            Console.WriteLine("[1] Add a product");
            Console.WriteLine("[2] List all products");
            Console.WriteLine("[3] View order history");
            ColorWrite.wc("\n Enter the [d] key to [Delete] the current store", ConsoleColor.DarkRed);
            ColorWrite.wc("     Enter [r] to [Return] to the Admin Menu", ConsoleColor.DarkYellow);
            Console.WriteLine("=============================================");
            string? input = Console.ReadLine();

            switch (input){
                //Adding a new product
                case "1":
                    //initialize products list
                    if (currStore.Products == null){
                        currStore.Products = new List<Product>();
                     }
                    
                    //get new product id between 1 and 1,000,000
                    Random rnd = new Random();
                    int id = rnd.Next(1000000);
                    Console.WriteLine("Name: ");
                    string? name = Console.ReadLine();
                    Console.WriteLine("Description: ");
                    string? description = Console.ReadLine();
                    reEnterP:
                    Console.WriteLine("Price: ");
                    string? price = Console.ReadLine();
                    decimal newP;
                    if (!(decimal.TryParse(price, out newP))){
                            Console.WriteLine("Price must be a Decimal value.");
                            goto reEnterP;
                        }
                    reEnterQ:
                    Console.WriteLine("Quantity: ");
                    string? quantity = Console.ReadLine();
                    int newQ;
                    if (!(int.TryParse(quantity, out newQ))){
                            Console.WriteLine("Quantity must be an integer.");
                            goto reEnterQ;
                        }
                    try{
                        Product newProduct = new Product{
                            ID = id!,
                            storeID = storeID,
                            Name = name!,
                            Description = description!,
                            Price = newP!,
                            Quantity = newQ!
                        };
                        _bl.AddProduct(storeID, newProduct);
                    }
                    //Checks for valid quantity and price above 0
                    catch(InputInvalidException ex){
                        Console.WriteLine(ex.Message);
                        if (ex.Message.Substring(0, 1) == "P"){
                            goto reEnterP;
                        }
                        else{
                            goto reEnterQ;
                        }
                    }
                    //Added a product to the store 
                    Console.WriteLine($"\n{name} has been added to the current store!");
                    break;
                case "2":
                    ProductMenu prodMenu = new ProductMenu(_bl);
                    prodMenu.Start(storeID);
                    break;
                //Return to the Admin Menu
                case "3":
                    StoreOrderMenu sOrderMenu = new StoreOrderMenu(_bl);
                    sOrderMenu.Start(storeID);
                    break;
                //Return to the Admin Menu
                case "d":
                    Console.WriteLine("Are you sure you want to delete this store? [y/n]");
                    string? selection = Console.ReadLine();
                    if (selection == "y"){
                        _bl.DeleteStore(storeID);
                        Console.WriteLine("\nYour store was deleted!");
                        exit = true;
                    }
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