using System.Globalization;

namespace UI;
public class ShoppingStoreMenu {
    private StoreBL _bl;
    private  UserBL _iubl;
    private ColorWrite _cw;

    public ShoppingStoreMenu(){
        _bl = new StoreBL();
        IURepo repo = new UserRepo();
        _iubl = new UserBL(repo);
        _cw = new ColorWrite();
    }
    public void Start(int index, string userName){
                //Find our current products list
                List<Store> allStores = _bl.GetAllStores();
                Store currStore = allStores[index]!;
                List<Product> allProducts = currStore.Products!;

                    bool valid = false;
                    while (!valid){
                        if(allProducts == null || allProducts.Count == 0){
                            Console.WriteLine("\nNo products found!");
                            valid = true;
                            }
                        else{
                        _cw.WriteColor("\n================[All Products]=================", ConsoleColor.DarkCyan);
                        Console.WriteLine($"{currStore.Name}\n");
                        int i = 0;
                        //Iterate over each product
                        foreach(Product prod in allProducts){
                            Console.WriteLine($"[{i}]  {prod.Name} | ${prod.Price} || Quantity: {prod.Quantity}\n     {prod.Description}");
                            i++;
                        }
                        Console.WriteLine("\nSelect the product's index to make a purchase.");
                        _cw.WriteColor("  Or enter [r] to [Return] to the the User Menu.", ConsoleColor.DarkYellow);
                        Console.WriteLine("=============================================");
                        string? select = Console.ReadLine();
                        int prodIndex;
                        //Return to the Product Menu
                        if (select == "r"){
                            valid = true;
                            }
                        else {
                            if(!int.TryParse(select, out prodIndex)){
                                Console.WriteLine("Please select a valid input!");
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
                                        Console.WriteLine("Please select a valid input!");
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
                                            int prodPrice = int.Parse(selectedProduct.Price!);
                                            string newQuantity = (prodQuantity - userInt).ToString();
                                            //Updates quantity remaining of the product
                                            _bl.EditProduct(index, prodIndex, selectedProduct.Description!, selectedProduct.Price!, newQuantity);
                                            //Add product order to user's shopping cart
                                            string currTime = DateTime.Now.ToString();
                                            ProductOrder currOrder = new ProductOrder{
                                                    ItemName = selectedProduct.Name!,
                                                    TotalPrice = (userInt * prodPrice).ToString(),
                                                    Quantity = (userInt!).ToString(),
                                                    Date = currTime!
                                                };
                                            _iubl.AddProductOrder(currIndex, currOrder);
                                        }
                                    }
                                    
                                    valid = true;
                                }
                            }
                        }
                    }
                }
            }
        }   
