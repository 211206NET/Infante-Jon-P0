namespace BL;
public interface IUBL
{
    List<User> GetAllUsers();

    void AddUser(User userToAdd);

    int GetCurrentUser(string userName);

    void AddProductOrder(int currUserIndex, ProductOrder currProdOrder);
            
    void EditProductOrder(int currUserIndex, int prodOrderIndex, string quantity);

    void DeleteProductOrder(int currUserIndex, int prodIndex);
    
    void AddUserStoreOrder(int currUserIndex, StoreOrder currStoreOrder);
    void ClearShoppingCart(int currUserIndex);

}