namespace UI;
public class LoginMenu {
    private IUBL _bl;

    public LoginMenu(IUBL bl){
        _bl = bl;
    }
    public void Start(){
        Console.WriteLine("Welcome to Jon's Used Hardware Store!");
        bool exit = false;
        while(!exit){
            Console.WriteLine("=============Login Menu=============");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("[1] Sign Up");
            Console.WriteLine("[2] Login as User");
            Console.WriteLine("[3] Sign in as Administrator");
            Console.WriteLine("[x] Exit");            
            Console.WriteLine("====================================");

            string input = Console.ReadLine();

            switch (input){
                case "1":
                    Console.WriteLine("Username: ");
                    string username = Console.ReadLine();
                    Console.WriteLine("Password: ");
                    string password = Console.ReadLine();

                    User newUser = new User{
                        Username = username,
                        Password = password,
                    };

                    _bl.AddUser(newUser);

                    break;
                case "2":
                    List<User> users = _bl.GetAllUsers();
                    foreach(User user in users){
                        Console.WriteLine(user.Username + " " +  user.Password);
                    }
                    break;
                case "3":
                    break;
                case "x":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("I did not expect that command! Please try again with a valid input.");
                    break;
            }   
        }
    }
}