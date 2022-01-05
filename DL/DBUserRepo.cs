namespace DL;

public class DBUserRepo : IURepo {

    private string _connectionString;
    public DBUserRepo(string connectionString){
        _connectionString = connectionString;

    }
    public List<User> GetAllUsers(){
        return new List<User>();
    }

    public void AddUser(User userToAdd){}

    public User GetCurrentUserByID(int userID){
        return new User();
    }

    public int GetCurrentUserIndexByID(int userID){
        return 0;
    }

    public void AddProductOrder(User currUser, ProductOrder currProdOrder){}

    public void EditProductOrder(User currUser, int prodOrderIndex, int quantity){}

    public void DeleteProductOrder(User currUser, int prodOrderIndex){}

    public void AddUserStoreOrder(User currUser, StoreOrder currStoreOrder){}
    
    public void ClearShoppingCart(User currUser){}
}