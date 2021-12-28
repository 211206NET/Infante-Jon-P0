using System.Text.Json;

namespace DL;
public class UserRepo : IURepo{

    public UserRepo(){
    }
    //make path from UI folder to file location
    private string? filePath = "../DL/Users.json";

    /// <summary>
    /// Gets all users from the file
    /// </summary>
    /// <returns>List of all users</returns>
    public List<User> GetAllUsers(){
        //returns all restaurants written in the file
        string jsonString = File.ReadAllText(filePath!);
        List<User> jsonDeserialized = JsonSerializer.Deserialize<List<User>>(jsonString)!;
        return jsonDeserialized!;
    }   

    /// <summary>
    /// Adds a user to the file
    /// </summary>
    /// <param name="userToAdd">User object</param>
    public void AddUser(User userToAdd){
        List<User> allUsers = GetAllUsers();
        allUsers.Add(userToAdd);
        string jsonString = JsonSerializer.Serialize(allUsers)!;
        File.WriteAllText(filePath!, jsonString!);

    }
    /// <summary>
    /// Returns index number of the current user accessing the store
    /// </summary>
    /// <param name="userName">The user logged in currently</param>
    public int GetCurrentUser(string userName){
        List<User> allUsers = GetAllUsers();
        int currIndex = 0;
        int i = 0;
        foreach (User user in allUsers){
            if (user.Username == userName){
                currIndex = i;
            }
            else{
                return 0;
            }
        }
        return currIndex;
    }
    
    /// <summary>
    /// Adds a product to the shopping cart
    /// </summary>
    /// <param name="currUserIndex">Current user index in the user list</param>
    /// <param name="currProdOrder">The current product object</param>
    public void AddProductOrder(int currUserIndex, ProductOrder currProdOrder){
        List<User> allUsers = GetAllUsers();
        User currUser = allUsers[currUserIndex!];
        if(currUser.ShoppingCart == null)
            {
                currUser.ShoppingCart = new List<ProductOrder>();
            }
        currUser.ShoppingCart!.Add(currProdOrder!);
        string jsonString = JsonSerializer.Serialize(allUsers)!;
        File.WriteAllText(filePath!, jsonString!);
    
    }
    /// <summary>
    /// Edits an existing product order in the shopping cart
    /// </summary>
    /// <param name="currUserIndex">Current user's index</param>
    /// <param name="prodOrderIndex">Index of the product in the shopping cart</param>
    /// <param name="quantity">New Updates quantity</param>
    public void EditProductOrder(int currUserIndex, int prodOrderIndex, string quantity){
        //Selected the currrent product based off the users index and the product order's index in the shopping cart
        List<User> allUsers = GetAllUsers();
        List<ProductOrder> allProdOrders = allUsers[currUserIndex].ShoppingCart!;
        ProductOrder currProduct = allProdOrders[prodOrderIndex]!;
        string oldQuantity = currProduct.Quantity!;
        //First check to throw exception if quantity is not an integer
        currProduct.Quantity = quantity;
        //Replacing the old quantity back in for calculations
        currProduct.Quantity = oldQuantity;
        //Calculating the new total amount
        int intQuantity = int.Parse(quantity);
        decimal currentTotal = decimal.Parse(currProduct.TotalPrice!);
        int currentQuantity = int.Parse(currProduct.Quantity!);
        decimal itemPrice = (currentTotal / currentQuantity);
        string newTotal = (itemPrice * intQuantity).ToString();
        //Declaring new quantity, total
        currProduct.TotalPrice = newTotal;
        currProduct.Quantity = quantity;
        string jsonString = JsonSerializer.Serialize(allUsers);
        File.WriteAllText(filePath!, jsonString!);
    }
    /// <summary>
    /// Delete's a product order from the user's shopping cart
    /// </summary>
    /// <param name="currUserIndex">Current user's index to parse user list</param>
    /// <param name="prodIndex">Current product orders' index</param>
    public void DeleteProductOrder(int currUserIndex, int prodIndex){
        List<User> allUsers = GetAllUsers();
        List<ProductOrder> allProdOrders = allUsers[currUserIndex].ShoppingCart!;
        allProdOrders!.RemoveAt(prodIndex);
        string jsonString = JsonSerializer.Serialize(allUsers);
        File.WriteAllText(filePath!, jsonString!);
    }
    /// <summary>
    /// Adds a store order to the user's store order list
    /// </summary>
    /// <param name="currUserIndex">Current user's index to parse user list</param>
    /// <param name="currStoreOrder">Store order to add</param>
    public void AddUserStoreOrder(int currUserIndex, StoreOrder currStoreOrder){
        List<User> allUsers = GetAllUsers();
        User currUser = allUsers[currUserIndex];
        if(currUser.FinishedOrders == null) {
            currUser.FinishedOrders = new List<StoreOrder>();
        }
        currUser.FinishedOrders.Add(currStoreOrder);
        string jsonString = JsonSerializer.Serialize(allUsers);
        File.WriteAllText(filePath!, jsonString!);
    }
    /// <summary>
    /// Clears a user's shopping cart
    /// </summary>
    /// <param name="currUserIndex">Current user's index to parse list</param>
    public void ClearShoppingCart(int currUserIndex){
        List<User> allUsers = GetAllUsers();
        User currUser = allUsers[currUserIndex];
        currUser.ShoppingCart!.Clear();
        string jsonString = JsonSerializer.Serialize(allUsers);
        File.WriteAllText(filePath!, jsonString!);
    }
}
