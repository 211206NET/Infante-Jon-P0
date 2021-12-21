namespace UI;

public class LoginMenu {
    private IUBL _bl;
    private ColorWrite _cw;

    public LoginMenu(IUBL bl){
        _bl = bl;
        _cw = new ColorWrite();
    }
    public void Start(){
        Console.WriteLine("\nWelcome to Jon's Used Hardware Store!\n");
        bool exit = false;
        while(!exit){
            _cw.WriteColor("==============[Login Menu]==============", ConsoleColor.DarkCyan);
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("[1] Sign Up");
            Console.WriteLine("[2] Login as User");
            Console.WriteLine("[3] Sign in as Administrator");
            _cw.WriteColor("\n\t    Enter [x] to [Exit]", ConsoleColor.DarkRed);
            Console.WriteLine("======================================");

            string input = Console.ReadLine();

            switch (input){
                case "1":
                    Console.WriteLine("Username: ");
                    string username = Console.ReadLine();
                    List<User> users = _bl.GetAllUsers();
                    bool userFound = false;
                    foreach(User user in users){
                        if (user.Username == username){
                            Console.WriteLine("\nUser already registered!\n");
                            userFound = true;
                            break;
                        }
                    }
                    if (!userFound){
                        Console.WriteLine("Password: ");
                        string password = Console.ReadLine();

                        User newUser = new User{
                            Username = username,
                            Password = password,
                            };

                            _bl.AddUser(newUser);
                    }
                    break;
                case "2":
                    Console.WriteLine("\nWhat is your username?");
                    string getUsername = Console.ReadLine();
                    List<User> currUsers = _bl.GetAllUsers();
                    bool found = false;
                    string userPassword = "";
                    foreach(User currUser in currUsers){
                        if (currUser.Username == getUsername){
                            found = true;
                            userPassword = currUser.Password;
                            }
                        }
                    if (found == false){
                        Console.WriteLine("\nUsername not found!\n");
                    }
                    else{
                        Console.WriteLine("Password");
                        string getPassword = Console.ReadLine();
                        if (getPassword == userPassword){
                            Console.WriteLine("\nLogin successful!\n");
                            //Profile Menu initialization        
                        }
                        else{
                            Console.WriteLine("\nIncorrect password.\n");
                        }
                    }
                    break;
                case "3":
                    Console.WriteLine("\nPlease enter your admin key to continue.");
                    string inp = Console.ReadLine();
                    if (inp == "emily"){
                        Console.WriteLine("\nLogged in to admin account.");
                        AdminMenu admin = new AdminMenu();
                        admin.Start();
                    }
                    else{
                        Console.WriteLine("\nIncorrect key!\n");
                    }
                    break;
                case "x":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("\nI did not expect that command! Please try again with a valid input.");
                    break;
            }   
        }
    }
}