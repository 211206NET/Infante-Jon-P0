namespace UI;

public class StoreMenu {
    private StoreBL _bl;
    private ColorWrite _cw;

    public StoreMenu(StoreBL bl){
        _bl = bl;
        _cw = new ColorWrite();
    }
    public void Start(int index){
        bool exit = false;
        List<Store> allStores = _bl.GetAllStores();
        Store currStore = allStores[index];
        List<Product> products = currStore.Products;
        while(!exit){
            _cw.WriteColor("\n==============[Store Menu]==============", ConsoleColor.DarkCyan);
            Console.WriteLine($"Store: {currStore.Name}\n");
            Console.WriteLine("[1] Add a product");
            Console.WriteLine("[2] List all products");
            _cw.WriteColor("\n   Enter [r] to [Return] to the Admin Menu", ConsoleColor.DarkYellow);
            Console.WriteLine("========================================");
            string? input = Console.ReadLine();

            switch (input){
                //Adding a new product
                case "1": 
                    Console.WriteLine("Name: ");
                    string? name = Console.ReadLine();
                    Console.WriteLine("Description: ");
                    string? description = Console.ReadLine();
                    Console.WriteLine("Price: ");
                    string? price = Console.ReadLine();
                    Console.WriteLine("Quantity: ");
                    string? quantity = Console.ReadLine();

                   Product newProduct= new Product{
                        Name = name!,
                        Description = description!,
                        Price = price!,
                        Quantity = quantity!
                    };
                    //Add a product to the store
                    _bl.AddProduct(index, newProduct);
                    Console.WriteLine($"{name} has been added to the current store!");
                    break;
                case "2":
                    FindProducts(index);
                    break;
                //Return to the Admin Menu
                case "r":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("I did not expect that command! Please try again with a valid input.");
                    break;
            }
     }
    }
    public void FindProducts(int index){
            //Find our current products list
            List<Store> allStores = _bl.GetAllStores();
            Store currStore = allStores[index];
            List<Product> allProducts = currStore.Products;
            if(allProducts == null){
                Console.WriteLine("\nNo products found!");
            }
            else{
                Console.WriteLine("\nHere are all your products!\n");
                int i = 0;
                //Iterate over each product
                foreach(Product prod in allProducts){
                    Console.WriteLine($"[{i}]  {prod.Name} | ${prod.Price} || Quantity: {prod.Quantity}\n     {prod.Description}");
                    i++;
                }
                bool valid = false;
                while (!valid){
                    Console.WriteLine("\nSelect the product's index to edit it.");
                    _cw.WriteColor("Enter the [d] key to [Delete] an item by its index.", ConsoleColor.DarkRed);
                    _cw.WriteColor("Or enter [r] to [Return] to the Store Menu.", ConsoleColor.DarkYellow);
                    string? select = Console.ReadLine();
                    int prodIndex;
                    //Return to the Product Menu
                    if (select == "r"){
                        valid = true;
                        }
                    //Selection to delete a product by index
                    else if(select == "d"){
                        int j = 0;
                        foreach(Product prod in allProducts){
                            Console.WriteLine($"[{j}]  {prod.Name}");
                            j++;
                        }
                        string? indexSelection = Console.ReadLine();
                        if(!int.TryParse(indexSelection, out prodIndex)){
                            Console.WriteLine("Please select a valid input!");
                        }
                        //Valid index found to delete the product
                        else {
                            valid = true;
                            _bl.DeleteProduct(index, prodIndex);
                        }
                    }
                    else {
                        if(!int.TryParse(select, out prodIndex)){
                            Console.WriteLine("Please select a valid input!");
                        }
                        //Valid index found to edit a product
                        else{
                            valid = true;
                            //Get our current product selected
                            Product currProduct = allProducts[prodIndex];
                            Console.WriteLine($"\n{currProduct.Name}\n\nEdit Description: ");
                            string? newDescription = Console.ReadLine();
                            Console.WriteLine("Edit Price: ");
                            string? newPrice = Console.ReadLine();
                            Console.WriteLine("Edit Quantity: ");
                            string? newQuantity = Console.ReadLine();
                            //If the input from the user is blank, keep the current product's information
                            newDescription = isEmpty(currProduct, "d", newDescription!);
                            newPrice = isEmpty(currProduct, "p", newPrice!);
                            newQuantity = isEmpty(currProduct, "q", newQuantity!);

                            _bl.EditProduct(index, prodIndex, newDescription, newPrice, newQuantity);                        
                        }  
                    }
                }
            }
        }
        /// <summary>
        /// Takes in a string of text and determinies if it is empty or not. If it is empty, replace the
        /// text with the current Products text for that paramater of the product
        /// </summary>
        /// <param name="cProduct">Current product</param>
        /// <param name="descriptor">d for Description, p for Price, q for Quantity</param>
        /// <param name="input">Empty string or updated paramater for the Product</param>
        public string isEmpty(Product cProduct, string descriptor, string input){
            if (input != ""){
                return input;
                 }
            else {
                //Desciption denoted by beginning letter of each paramater in product
                if (descriptor == "d"){
                    return cProduct.Description;
                }
                else if (descriptor == "p"){
                    return cProduct.Price;
                }
                else if (descriptor == "q"){
                    return cProduct.Quantity;
                }
                else {
                    return "";
                }
             }       
        }
}