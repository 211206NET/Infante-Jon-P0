namespace DL;

public class DBUserRepo : IURepo {

    private string _connectionString;
    public DBUserRepo(string connectionString){
        _connectionString = connectionString;

    }

    /// <summary>
    /// Adds a user object to the database
    /// </summary>
    /// <param name="userToAdd"></param>
    public void AddUser(User userToAdd){}

    /// <summary>
    /// Gets the entire list of users from the database[named customer inside the database due to special naming by SQL]
    /// </summary>
    /// <returns>Returns a list of customer(user) objects</returns>
    public List<User> GetAllUsers(){
        return new List<User>();
    }

    /// <summary>
    /// Gets the current user from the list of users by ID
    /// </summary>
    /// <param name="userID">current user ID selected</param>
    /// <returns>User Object</returns>
    public User GetCurrentUserByID(int userID){
        return new User();
    }

    /// <summary>
    /// Adds a product order to the user's shopping cart inside the database
    /// </summary>
    /// <param name="currUser">current user object inputted</param>
    /// <param name="currProdOrder">product order object to add to the database</param>
    public void AddProductOrder(User currUser, ProductOrder currProdOrder){}

    /// <summary>
    /// Edits a selected product order in the user's shopping cart, and saves it back to the database
    /// </summary>
    /// <param name="currUser">current user selected</param>
    /// <param name="prodOrderID">ID of the product order selected to edit</param>
    /// <param name="quantity">New quantity to update</param>
    public void EditProductOrder(User currUser, int prodOrderID, int quantity){}

    /// <summary>
    /// Remove a Product Order from the user's shopping cart and database
    /// </summary>
    /// <param name="currUser">current user object inputted</param>
    /// <param name="prodOrderID">the product order ID of the product order we have selected</param>
    public void DeleteProductOrder(User currUser, int prodOrderID){}

    /// <summary>
    /// Adds a store order to the user's list of previous orders and to the database
    /// </summary>
    /// <param name="currUser">current user object selected</param>
    /// <param name="currStoreOrder">storeOrder object to add to previous orders</param>
    public void AddUserStoreOrder(User currUser, StoreOrder currStoreOrder){}
    
    /// <summary>
    /// Removes every product order from the user's shopping cart, and deletes each instance inside the database
    /// </summary>
    /// <param name="currUser">current user object selected</param>
    public void ClearShoppingCart(User currUser){}

    //Unused with database implementation
    public int GetCurrentUserIndexByID(int userID){
        return 0;
    }
}