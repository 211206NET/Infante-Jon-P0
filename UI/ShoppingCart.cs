namespace UI;

public class ShoppingCart {
    private StoreBL _bl;
    private UserBL _iubl;
    private ColorWrite _cw;

    public ShoppingCart(){
        _bl = new StoreBL();
        IURepo repo = new UserRepo();
        _iubl = new UserBL(repo);
        _cw = new ColorWrite();
    }
    public void Start(string userName){
        bool exit = false;
        while(!exit){
            List<User> users = _iubl.GetAllUsers();
            int currUserIndex = _iubl.GetCurrentUser(userName);
            List<ProductOrder> allProductOrders = users[currUserIndex].ShoppingCart!;
            int i = 0;
            _cw.WriteColor("\n================[Shopping Cart]================", ConsoleColor.DarkCyan);
                    Console.WriteLine("What would you like to do?\n");
                        foreach(ProductOrder pOrder in allProductOrders!){
                            Console.WriteLine($"[{i}]  {pOrder.ItemName} | Quantity: {pOrder.Quantity} || {pOrder.Date}\n     Total Price: ${pOrder.TotalPrice} ");
                            i++;
                    }
            Console.WriteLine("\n   Select the product's index to edit it's quantity.");
            _cw.WriteColor("\nEnter the [d] key to [Delete] an order by index.", ConsoleColor.DarkRed);
            _cw.WriteColor("  Or Enter [r] to [Return] to the Profile Menu", ConsoleColor.DarkYellow);
            Console.WriteLine("=============================================");

            string? input = Console.ReadLine();
            int prodOrderIndex;
            if (input == "d"){  
                int j = 0;
                    foreach(ProductOrder prodOrder in allProductOrders){
                        Console.WriteLine($"[{j}]  {prodOrder.ItemName}");
                        j++;
                    }
                    string? indexSelection = Console.ReadLine();
                    if(!int.TryParse(indexSelection, out prodOrderIndex)){
                        Console.WriteLine("\nPlease select a valid input!");
                    }
                    //Valid index found to delete the product
                    else {
                        if (prodOrderIndex >= 0 && prodOrderIndex < allProductOrders.Count){
                            exit = true;
                            //Calls the business logic of deleting a product by both indices
                            _iubl.DeleteProductOrder(currUserIndex, prodOrderIndex);
                        }
                        else{
                            Console.WriteLine("\nPlease select an index within range!");
                            }
                    }
                }
            //Returns to Profile Menu
            else if (input == "r"){
                exit = true;
            }
            else {
                if(!int.TryParse(input, out prodOrderIndex)){
                        Console.WriteLine("Please select a valid input!");
                    }
                    //Valid index found to edit a product
                    else{
                        //Check if index is in range
                        if (prodOrderIndex >= 0 && prodOrderIndex < allProductOrders.Count){
                            exit = true;
                            Console.WriteLine("New Quantity: ");
                            reEnter:
                            string? newQuantity = Console.ReadLine();
                            try {
                                _iubl.EditProductOrder(currUserIndex, prodOrderIndex, newQuantity!);
                                }
                            catch(InputInvalidException ex){
                                Console.WriteLine(ex.Message);
                                goto reEnter;

                             }
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