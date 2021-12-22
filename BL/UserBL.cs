
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
    /// Adds a product order to the user's shopping list
    /// </summary>
    /// <param name="currUserIndex">User's index in the user list</param>
    /// <param name="currProdOrder">New product order to be added to the user's shopping cart</param>
    public void AddProductOrder(int currUserIndex, ProductOrder currProdOrder){
        _dl.AddProductOrder(currUserIndex, currProdOrder);
    }

}
