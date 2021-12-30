using System.Collections; 

namespace UI;

public class ShoppingCart {
    private StoreBL _bl;
    private UserBL _iubl;
    public ShoppingCart(){
        _bl = new StoreBL();
        IURepo repo = new UserRepo();
        _iubl = new UserBL(repo);
    }
    public void Start(int userID){
        bool exit = false;
        while(!exit){
            User currUser = _iubl.GetCurrentUserByID(userID);
            if(currUser.ShoppingCart == null){
                currUser.ShoppingCart = new List<ProductOrder>();
                }
            List<ProductOrder> allProductOrders = currUser.ShoppingCart!;
            int i = 0;
            decimal shopTotalPrice = 0;
            ColorWrite.wc("\n================[Shopping Cart]================", ConsoleColor.DarkCyan);
                foreach(ProductOrder pOrder in allProductOrders!){
                    Console.WriteLine($"[{i}]  {pOrder.ItemName} | Quantity: {pOrder.Quantity}\n     Total Price: ${pOrder.TotalPrice} ");
                    shopTotalPrice += Decimal.Parse(pOrder.TotalPrice!);
                    i++;
                }
                    if (i == 0){
                        Console.WriteLine("\t     Shopping Cart Empty!");
                    }
                    else{
                        Console.WriteLine("|--------------------------------------------|");
                        Console.WriteLine($"| Total Amount: ${shopTotalPrice}");
                        Console.WriteLine("|--------------------------------------------|");

                    }
            Console.WriteLine("\nSelect a product's index to edit it's amount");
            ColorWrite.wc("Enter the [c] key to [Checkout] and place your order", ConsoleColor.DarkGreen);
            ColorWrite.wc(" Enter the [d] key to [Delete] an order by index", ConsoleColor.DarkRed);
            ColorWrite.wc("  Or Enter [r] to [Return] to the Profile Menu", ConsoleColor.DarkYellow);
            Console.WriteLine("=============================================");
            //Get user input selection
            string? input = Console.ReadLine();
            int prodOrderIndex;
            //Delete a product order
            if (input == "d"){  
                int j = 0;
                //Checks if shopping cart is empty
                if (i == 0){
                    Console.WriteLine("\nThere are no items to delete!");
                }
                //Print list of products to delete from
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

                            //Gets the current product order, storeID, productID, and product by product order index
                            ProductOrder pOrder = allProductOrders[prodOrderIndex];
                            int storeID = (int)pOrder.storeID!;
                            int sProdID = (int)pOrder.productID!;
                            Product productSelected = _bl.GetProductByID(storeID, sProdID);
                            //Calculating the new quantity
                            int prodOrderQuantity = int.Parse(allProductOrders[prodOrderIndex].Quantity!);
                            int prodQuantity = int.Parse(productSelected.Quantity!);
                            string newProdQuantity = (prodQuantity! + prodOrderQuantity!).ToString();
                            //Puts the correct amount of stock back in the store
                            _bl.EditProduct(storeID, sProdID, productSelected.Description!, productSelected.Price!, newProdQuantity);
                            //Calls the business logic of deleting a product order from the shopping cart by both indices
                            _iubl.DeleteProductOrder(currUser, prodOrderIndex);
                        }
                        else{
                            Console.WriteLine("\nPlease select an index within range!");
                            }
                        }
                }
            }
            //checkout for each product corresponding to the user's orders and each store's orders
            else if (input == "c"){
                if(currUser.FinishedOrders == null) {
                    currUser.FinishedOrders = new List<StoreOrder>();
                    }
                if (allProductOrders.Count == 0){
                    Console.WriteLine("\nYou have no items to checkout!");
                }
                //Orders found to place
                else{
                    Console.WriteLine("\nReady to place your order? [y/n]");
                    string? inputYesorNo = Console.ReadLine();
                    if (inputYesorNo == "y"){

                        //get new store Order id between 1 and 1,000,000
                        Random rnd = new Random();
                        int id = rnd.Next(1000000);
                        //Make new list of product orders to add to the user store order and calculate total
                        decimal userpOrdersTotal = 0;
                        List<ProductOrder> userProductOrders = new List<ProductOrder>();
                        foreach(ProductOrder checkoutProduct in allProductOrders){
                            userpOrdersTotal += decimal.Parse(checkoutProduct.TotalPrice!);
                            userProductOrders.Add(checkoutProduct);
                        } 
                        string currTime = DateTime.Now.ToString();
                        double currTimeSeconds = DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds;
                        StoreOrder userStoreOrder = new StoreOrder{
                            ID = id!,
                            userID = currUser.ID,
                            TotalAmount = userpOrdersTotal!,
                            Date = currTime!,
                            DateSeconds = currTimeSeconds!,
                            Orders = userProductOrders,
                            };
                        //Adds to current user's store order list
                        _iubl.AddUserStoreOrder(currUser, userStoreOrder);
                        //Get each corresponding store from each product's ID and add to a dictionary
                        Dictionary<int, List<ProductOrder>> storeOrdersToPlace = new Dictionary<int,List<ProductOrder>>();
                        foreach(ProductOrder pOrder in allProductOrders){
                            //Getting the ID of the current store from the product id's string id code
                            int currStoreID = (int)pOrder.storeID!;
                            if (storeOrdersToPlace.ContainsKey(currStoreID)){
                                storeOrdersToPlace[currStoreID].Add(pOrder);
                                }
                            //If there is no key found
                            else{
                                List<ProductOrder> listP = new List<ProductOrder>();
                                listP.Add(pOrder);
                                //Assigns the initial first item to a new dictionary key (by store index, list of product orders)
                                storeOrdersToPlace.Add(currStoreID, listP);
                            }
                        }
                        //Iterate over dictionary with store indexes and corresponding product
                        List<Store> allStores = _bl.GetAllStores();
                        foreach(KeyValuePair<int, List<ProductOrder>> kv in storeOrdersToPlace){
                            //Get the store index from the current store ID [kv.Key]
                            int storeIndex =  _bl.GetStoreIndexByID(kv.Key);
                            if(allStores[storeIndex].AllOrders == null) {
                                allStores[storeIndex].AllOrders = new List<StoreOrder>();
                                }   
                            //get new store Order id between 1 and 1,000,000
                            int sid = rnd.Next(1000000);
                            //calcuate total order value for list of product orders
                            decimal StoreOrderTotalValue = 0;
                            foreach(ProductOrder pOrd in kv.Value){
                                StoreOrderTotalValue += decimal.Parse(pOrd.TotalPrice!);
                            }
                            StoreOrder storeOrderToAdd = new StoreOrder{
                                ID = sid!,
                                userID = currUser.ID!,
                                TotalAmount = StoreOrderTotalValue!,
                                Date = currTime!,
                                DateSeconds = currTimeSeconds!,
                                Orders = kv.Value
                            };
                            //Adds store order to current selected store
                            //kv.key is the store's ID
                            _bl.AddStoreOrder(kv.Key, storeOrderToAdd);
                        }
                        //Emptys current user's shopping cart
                        _iubl.ClearShoppingCart(currUser);
                    }
                }
            }
            //Returns to Profile Menu
            else if (input == "r"){
                exit = true;
            }
            //Gets index of a current product, or invalid input
            else {
                if(!int.TryParse(input, out prodOrderIndex)){
                       Console.WriteLine("\nPlease enter a valid input!");
                    }
                //Valid index found to edit a product
                else{
                    //Check if index is in range
                    if (prodOrderIndex >= 0 && prodOrderIndex < allProductOrders.Count){
                        Console.WriteLine("New Quantity: ");
                        reEnter:
                        string? newQuantity = Console.ReadLine();
                        //store ID and the store's productID is found from the prodect order's 
                        ProductOrder pOrder = allProductOrders[prodOrderIndex];
                        int storeID = (int)pOrder.storeID!;
                        int storeProdID = (int)pOrder.productID!;
                        Product productSelected = _bl.GetProductByID(storeID, storeProdID);
                        //Parsing to calculate new total quantity
                        int newQ;
                        int.TryParse(newQuantity!, out newQ);
                        int oldQ = int.Parse(productSelected!.Quantity!);
                        //Current quantity of the amount of products in the shopping cart
                        int currentPOrderQuantity = int.Parse(allProductOrders[prodOrderIndex].Quantity!);
                        try {
                            //Tries for invalid quantity type
                            _iubl.EditProductOrder(currUser, prodOrderIndex, newQuantity!);
                            //If the quantity is over the product's stock limit
                            if (newQ > (oldQ + currentPOrderQuantity)){
                                //Gets total amount of products from the current amount in the product order and the current amount in stock
                                Console.WriteLine(@$"\nThe amount you selected is too high!" + 
                                $"\nThe maximum amount you can order of this product is {(currentPOrderQuantity + oldQ)}");
                                //reset the product order to its original value
                                _iubl.EditProductOrder(currUser, prodOrderIndex, currentPOrderQuantity.ToString()!);
                                goto reEnter;
                            }
                            else{
                                Console.WriteLine("\nYour shopping cart item has been updated!");
                                //Update store product with new quantity.
                                _bl.EditProduct(storeID, storeProdID, productSelected.Description!, productSelected.Price!, ((oldQ + currentPOrderQuantity) - newQ).ToString());
                            }
                        }
                        //Input is not a valid integer
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