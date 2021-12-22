using System.Text.Json;

namespace DL;
public class UserRepo : IURepo{

    public UserRepo(){
    }
    //make path from UI folder to file location
    private string? filePath = "../DL/Users.json";

    /// <summary>
    /// Gets all users from a file
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
 
}
