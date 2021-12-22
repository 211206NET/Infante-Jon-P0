namespace BL;
public interface IUBL
{
    List<User> GetAllUsers();

    void AddUser(User usertToAdd);

    int GetCurrentUser(string userName);

    void AddProductOrder(int currUserIndex, ProductOrder currProdOrder);

    void DeleteProductOrder(int currUserIndex, int prodIndex);
    

}