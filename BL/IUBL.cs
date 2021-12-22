namespace BL;
public interface IUBL
{
    List<User> GetAllUsers();

    void AddUser(User usertToAdd);

    void AddProductOrder(int currUserIndex, ProductOrder currProdOrder);

}