namespace DL;

public interface IURepo {
    
    //No access modifiers. Interface members are implicetly public
    //Also lack method body
    List<User> GetAllUsers();

    void AddUser(User userToAdd);

    int GetCurrentUser(string userName);
    
    void AddProductOrder(int currUserIndex, ProductOrder currProdOrder);

    void EditProductOrder(int currUserIndex, int prodOrderIndex, string quantity);

    void DeleteProductOrder(int currUserIndex, int prodIndex);



}