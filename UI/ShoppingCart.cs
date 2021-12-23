using System.Collections; 

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
            User currUser = users[currUserIndex!];
            if(currUser.ShoppingCart == null)
                {
                currUser.ShoppingCart = new List<ProductOrder>();
                }
            List<ProductOrder> allProductOrders = users[currUserIndex].ShoppingCart!;
            int i = 0;
            _cw.WriteColor("\n================[Shopping Cart]================", ConsoleColor.DarkCyan);
            Console.WriteLine("What would you like to do?\n");
                foreach(ProductOrder pOrder in allProductOrders!){
                    Console.WriteLine($"[{i}]  {pOrder.ItemName} | Quantity: {pOrder.Quantity}\n     Total Price: ${pOrder.TotalPrice} ");
                    i++;
                }
                    if (i == 0){
                        Console.WriteLine("Shopping Cart Empty!");
                    }
            Console.WriteLine("\nSelect a product's index to edit it's amount");
            _cw.WriteColor(" Enter the [d] key to [Delete] an order by index", ConsoleColor.DarkRed);
            _cw.WriteColor("  Or Enter [r] to [Return] to the Profile Menu", ConsoleColor.DarkYellow);
            Console.WriteLine("=============================================");

            string? input = Console.ReadLine();
            int prodOrderIndex;
            //Method for getting the matching product fromthe current product order index
            ArrayList GetProduct(int prodIndex){
                ArrayList tempArray = new ArrayList();
                List<Store> allStores = _bl.GetAllStores();
                //Splits the current product order's id to get the store id and product id
                string[] splitString = allProductOrders[prodOrderIndex]!.ID!.Split('#');
                int storeIndex = int.Parse(splitString[0]);
                int storeProdIndex = int.Parse(splitString[1]);
                Product productSelected = allStores[storeIndex].Products![storeProdIndex];

                tempArray.Add(productSelected);
                tempArray.Add(storeIndex);
                tempArray.Add(storeProdIndex);


                return tempArray;
                }
            if (input == "d"){  
                int j = 0;
                if (i == 0){
                    Console.WriteLine("\nThere are no orders to delete!");
                }
                else{
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
                            //Calls the business logic of deleting a product by both indices
                            _iubl.DeleteProductOrder(currUserIndex, prodOrderIndex);
                            //Gets the current product by product order index
                            ArrayList prodArray = GetProduct(prodOrderIndex);
                            Product productSelected = (Product)prodArray[0]!;
                            int sIndex = (int)prodArray[1]!;
                            int sProdIndex = (int)prodArray[2]!;
                            //Calculating the new quantity
                            int prodOrderQuantity = int.Parse(allProductOrders[prodOrderIndex].Quantity!);
                            int prodQuantity = int.Parse(productSelected.Quantity!);
                            string newProdQuantity = (prodQuantity! + prodOrderQuantity!).ToString();
                            //Puts the correct amount of stock back in the store
                            _bl.EditProduct(sIndex, sProdIndex, productSelected.Description!, productSelected.Price!, newProdQuantity);

                        }
                        else{
                            Console.WriteLine("\nPlease select an index within range!");
                            }
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
                            Console.WriteLine("New Quantity: ");
                            reEnter:
                            string? newQuantity = Console.ReadLine();
                            //add check to see if quantity is above the store's product quantity
                            //Gets the current product by product order index
                            ArrayList prod2Array = GetProduct(prodOrderIndex);
                            Product productSelected = (Product)prod2Array[0]!;
                            int storeIndex = (int)prod2Array[1]!;
                            int storeProdIndex = (int)prod2Array[2]!;
                            
                            int newQ = int.Parse(newQuantity!);
                            int oldQ = int.Parse(productSelected!.Quantity!);
                            int currentPOrderQuantity = int.Parse(allProductOrders[prodOrderIndex].Quantity!);
                            try {
                                //Tries for invalid quantity type
                                _iubl.EditProductOrder(currUserIndex, prodOrderIndex, newQuantity!);
                                //If the quantity is over the product stock's limit
                                if (newQ > (oldQ + currentPOrderQuantity)){
                                    //Gets total amount of products from the current amount in the product order and the current amount in stock
                                    Console.WriteLine(@$"The amount you selected is too high!" + 
                                    $"\nThe maximum amount you can order of this product is {(currentPOrderQuantity + oldQ)}");
                                    //reset the product order to its original value
                                    _iubl.EditProductOrder(currUserIndex, prodOrderIndex, currentPOrderQuantity.ToString()!);
                                    goto reEnter;
                                }
                                else{
                                    Console.WriteLine("Your shopping cart item has been updated!");
                                    //Update store product with new quantity.
                                    _bl.EditProduct(storeIndex, storeProdIndex, productSelected.Description!, productSelected.Price!, ((oldQ + currentPOrderQuantity) - newQ).ToString());

                                }
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