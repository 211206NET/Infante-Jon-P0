namespace UI;
public class ShoppingStoreMenu {
    private StoreBL _bl;
    private ColorWrite _cw;

    public ShoppingStoreMenu(){
        _bl = new StoreBL();
        _cw = new ColorWrite();
    }
    public void Start(int index){
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
                                    valid = true;
                                }
                            }
                        }
                    }
                }
            }
        }   
