namespace UI;
public class ProductMenu {
    private StoreBL _bl;

    public ProductMenu(StoreBL bl){
        _bl = bl;
    }
    public void Start(int index){
        bool valid = false;
        while (!valid){
            //Find our current products list
            List<Store> allStores = _bl.GetAllStores();
            Store currStore = allStores[index]!;
            List<Product> allProducts = currStore.Products!;
            if(allProducts == null || allProducts.Count == 0){
                Console.WriteLine("\nNo products found!");
                valid = true;
                }
            else{
            ColorWrite.wc("\n================[All Products]=================", ConsoleColor.DarkCyan);
            int i = 0;
            //Iterate over each product
            foreach(Product prod in allProducts){
                Console.WriteLine($"[{i}]  {prod.Name} | ${prod.Price} || Quantity: {prod.Quantity}\n     {prod.Description}");
                i++;
            }
            Console.WriteLine("\n   Select the product's index to edit it.");
            ColorWrite.wc("Enter the [d] key to [Delete] an item by its index.", ConsoleColor.DarkRed);
            ColorWrite.wc("   Or enter [r] to [Return] to the Store Menu.", ConsoleColor.DarkYellow);
            Console.WriteLine("=============================================");
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
                    Console.WriteLine("\nPlease select a valid input!");
                }
                //Valid index found to delete the product
                else {
                    if (prodIndex >= 0 && prodIndex < allProducts.Count){
                        //Calls the business logic of deleting a product by both indices
                        _bl.DeleteProduct(index, prodIndex);
                    }
                    else{
                        Console.WriteLine("\nPlease select an index within range!");
                        }
                }
            }
            else {
                if(!int.TryParse(select, out prodIndex)){
                    Console.WriteLine("Please select a valid input!");
                }
                //Valid index found to edit a product
                else{
                    //Check if index is in range
                    if (prodIndex >= 0 && prodIndex < allProducts.Count){
                        //Get our current product selected
                        Product currProduct = allProducts[prodIndex];
                        Console.WriteLine($"\n{currProduct.Name}\n\nEdit Description: ");
                        string? newDescription = Console.ReadLine();
                        //Loops back up if input validation fails
                        reEnterP:
                        Console.WriteLine("Price: ");
                        string? newPrice = Console.ReadLine();
                        reEnterQ:
                        Console.WriteLine("Quantity: ");
                        string? newQuantity = Console.ReadLine();

                        //If the input from the user is blank, keep the current product's information
                        newDescription = isEmpty(currProduct, "d", newDescription!);
                        newPrice = isEmpty(currProduct, "p", newPrice!);
                        newQuantity = isEmpty(currProduct, "q", newQuantity!);
                        //Calls the Business Logic of editing the product
                        //Checks if newprice and newquantity are respectively floats and ints
                        try {
                            _bl.EditProduct(index, prodIndex, newDescription, newPrice, newQuantity);
                            Console.WriteLine("\nYour product has been edited successfully!");
                        }
                        catch(InputInvalidException ex){
                            Console.WriteLine(ex.Message);
                            //If the Price is incorrect
                            if (ex.Message.Substring(0, 1) == "P"){
                                goto reEnterP;
                            }
                            //If the Quantity is incorrect
                            else{
                                goto reEnterQ;
                            }
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
        /// <summary>
        /// Takes in a string of text and determinies if it is empty or not. If it is empty, replace the
        /// text with the current Products text for that paramater of the product
        /// </summary>
        /// <param name="cProduct">Current product</param>
        /// <param name="descriptor">d for Description, p for Price, q for Quantity</param>
        /// <param name="input">Empty string or updated paramater for the Product</param>
        public string isEmpty(Product cProduct, string descriptor, string input){
            //If the string isn't empty, return that same string
            if (input != ""){
                return input;
                }
            //If the string is empty, keep the old values of the Product
            else {
                //Desciption denoted by beginning letter of each paramater in product
                if (descriptor == "d"){
                    return cProduct.Description!;
                }
                else if (descriptor == "p"){
                    return cProduct.Price!;
                }
                else if (descriptor == "q"){
                    return cProduct.Quantity!;
                }
                else {
                    return "";
                }
            }       
            }       
}