namespace DL;

public interface IURepo {
    
    //No access modifiers. Interface members are implicetly public
    //Also lack method body
    List<User> GetAllUsers();

    void AddUser(User userToAdd);

}