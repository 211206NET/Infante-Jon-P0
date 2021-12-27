using System.Globalization;

namespace UI;
public class ShoppingStoreMenu {
    private StoreBL _bl;
    private  UserBL _iubl;

    public ShoppingStoreMenu(){
        _bl = new StoreBL();
        IURepo repo = new UserRepo();
        _iubl = new UserBL(repo);
    }
    public void Start(int index, string userName){

                    bool valid = false;
                    while (!valid){
                        //Find our current products list
                        List<Store> allStores = _bl.GetAllStores();
                        Store currStore = allStores[index]!;
                        List<Product> allProducts = currStore.Products!;
                        //If the products list hasn't been initialzied or is empty
                        if(allProducts == null || allProducts.Count == 0){
                            Console.WriteLine("\nNo products found!");
                            valid = true;
                            }
                        else{
                        ColorWrite.wc("\n================[All Products]=================", ConsoleColor.DarkCyan);
                        Console.WriteLine($"{currStore.Name}\n");
                        int i = 0;
                        //Iterate over each product
                        foreach(Product prod in allProducts){
                            Console.WriteLine($"[{i}]  {prod.Name} | ${prod.Price} || Quantity: {prod.Quantity}\n     {prod.Description}");
                            i++;
                        }
                        Console.WriteLine("\nSelect the product's index to make a purchase.");
                        ColorWrite.wc("Or enter [r] to [Return] to the the list of Stores", ConsoleColor.DarkYellow);
                        Console.WriteLine("=============================================");
                        string? select = Console.ReadLine();
                        int prodIndex = 0;
                        //Return to the Product Menu
                        if (select == "r"){
                            valid = true;
                            }
                        else {
                            if(!int.TryParse(select, out prodIndex)){
                                Console.WriteLine("\nPlease select a valid input!");
                            }
                            //Valid index found to edit a product
                            else{
                                //Check if index is in range
                                if (prodIndex >= 0 && prodIndex < allProducts.Count){
                                    //Get product to make a purchase
                                    Product selectedProduct = allProducts[prodIndex!];
                                    Console.WriteLine($"How many {selectedProduct.Name}s would you like to order?");
                                    enterAmount:
                                    string? userInput = Console.ReadLine();
                                    int userInt;
                                    if(!int.TryParse(userInput, out userInt!)){
                                        Console.WriteLine("\nPlease select a valid input!");
                                    }
                                    else{
                                        int prodQuantity = int.Parse(selectedProduct.Quantity!);
                                        if(prodQuantity == 0){
                                            Console.WriteLine("\nSorry, we are out of stock of this item!");
                                        }
                                        else if(userInt < 0 || userInt > prodQuantity){
                                            Console.WriteLine($"You may only purchase up to {prodQuantity} {selectedProduct.Name}s\nPlease enter a valid amount.");
                                            goto enterAmount;
                                        }
                                        else{
                                            //Get the current user's index
                                            int currIndex = _iubl.GetCurrentUser(userName);
                                            //Get total quantity and price of current product
                                            decimal prodPrice = decimal.Parse(selectedProduct.Price!);
                                            string newQuantity = (prodQuantity - userInt).ToString();
                                            //Updates quantity remaining of the product
                                            _bl.EditProduct(index, prodIndex, selectedProduct.Description!, selectedProduct.Price!, newQuantity);
                                            //Add product order to user's shopping cart
                                            //get new productorder id
                                            string prodID = selectedProduct.ID.ToString()!;
                                            //Product Order's ID includes the current store's id and the current product's id. 
                                            string id = $"{currStore.ID.ToString()}#{prodID.ToString()!}"; 
                                            ProductOrder currOrder = new ProductOrder{
                                                    ID = id!,
                                                    ItemName = selectedProduct.Name!,
                                                    TotalPrice = (userInt * prodPrice).ToString(),
                                                    Quantity = (userInt!).ToString(),
                                                };
                                            _iubl.AddProductOrder(currIndex, currOrder);
                                        }
                                    }
                                }
                                //Integer out of range of the product list's index
                                else{
                                    Console.WriteLine("\nPlease select an index within range!");
                                }
                            }
                        }
                    }
                }
            }
        }   
