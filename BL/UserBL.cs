
namespace BL;
public class UserBL : IUBL
{
    private IURepo _dl;

    public UserBL(IURepo repo) {
        _dl = repo;
    }
    /// <summary>
    /// Gets all users
    /// </summary>
    /// <returns>list of all users</returns>
    public List<User> GetAllUsers(){
        return _dl.GetAllUsers();

    }
    /// <summary>
    /// Adds a new user to the list
    /// </summary>
    /// <param name="userToAdd">user object to add</param>
    public void AddUser(User userToAdd){
        _dl.AddUser(userToAdd);
    }
    /// <summary>
    /// Returns the index of the current user
    /// </summary>
    /// <param name="userName">current user logged in</param>
    public int GetCurrentUser(string userName){
        return _dl.GetCurrentUser(userName);
    }
    /// <summary>
    /// Adds a product order to the user's shopping list
    /// </summary>
    /// <param name="currUserIndex">User's index in the user list</param>
    /// <param name="currProdOrder">New product order to be added to the user's shopping cart</param>
    public void AddProductOrder(int currUserIndex, ProductOrder currProdOrder){
        _dl.AddProductOrder(currUserIndex, currProdOrder);
    }
    /// <summary>
    /// Edits an existing product's order by quantity
    /// </summary>
    /// <param name="currUserIndex">Current user's index</param>
    /// <param name="prodOrderIndex">Product order's index in the shopping cart</param>
    /// <param name="quantity">New quantity to be update to</param>
    public void EditProductOrder(int currUserIndex, int prodOrderIndex, string quantity){
        _dl.EditProductOrder(currUserIndex, prodOrderIndex, quantity);
    }
    /// <summary>
    /// Deletes a product from your shopping list
    /// </summary>
    /// <param name="currUserIndex">current index of the user</param>
    /// <param name="prodIndex">Product to delete at index</param>
    public void DeleteProductOrder(int currUserIndex, int prodIndex){
        _dl.DeleteProductOrder(currUserIndex, prodIndex);
    }
    /// <summary>
    /// Adds a store order to the user's order list
    /// </summary>
    /// <param name="currUserIndex">Current user's index to parse</param>
    /// <param name="currStoreOrder">Store order to add</param>
    public void AddUserStoreOrder(int currUserIndex, StoreOrder currStoreOrder){
        _dl.AddUserStoreOrder(currUserIndex, currStoreOrder);
    }
    /// <summary>
    /// Clears the user's shopping cart
    /// </summary>
    /// <param name="currUserIndex">Current user's index to parse</param>
    public void ClearShoppingCart(int currUserIndex){
        _dl.ClearShoppingCart(currUserIndex);
    }
}
