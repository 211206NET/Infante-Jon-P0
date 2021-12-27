namespace UI;

public class LoginMenu {
    private IUBL _bl;
    

    public LoginMenu(IUBL bl){
        _bl = bl;
    }
    public void Start(){
        Console.WriteLine("\nWelcome to Jon's Used Hardware Franchise!");
        bool exit = false;
        while(!exit){
            ColorWrite.wc("\n==================[Login Menu]=================", ConsoleColor.DarkCyan);
            Console.WriteLine("What would you like to do?\n");
            Console.WriteLine("[1] Sign Up");
            Console.WriteLine("[2] Login as User");
            Console.WriteLine("[3] Login as Administrator");
            ColorWrite.wc("\n\t        Enter [x] to [Exit]", ConsoleColor.DarkRed);
            Console.WriteLine("=============================================");

            string? input = Console.ReadLine();

            switch (input){
                case "1":
                    Console.WriteLine("Username: ");
                    string? username = Console.ReadLine();
                    List<User> users = _bl.GetAllUsers();
                    bool userFound = false;
                    int id = 0;
                    foreach(User user in users){
                        id++;
                        if (user.Username == username){
                            Console.WriteLine("\nUser already registered!");
                            userFound = true;
                            break;
                        }
                    }
                    if (!userFound){
                        Console.WriteLine("Password: ");
                        string? password = Console.ReadLine();

                        User newUser = new User{
                            ID = id!,
                            Username = username!,
                            Password = password!,
                            };

                            _bl.AddUser(newUser);
                        UserMenu newuMenu = new UserMenu();
                        newuMenu.Start(username!);   
                    }

                    break;
                case "2":
                    Console.WriteLine("\nWhat is your username?");
                    string? getUsername = Console.ReadLine();
                    List<User> currUsers = _bl.GetAllUsers();
                    bool found = false;
                    string userPassword = "";
                    foreach(User currUser in currUsers){
                        if (currUser.Username == getUsername){
                            found = true;
                            userPassword = currUser.Password!;
                            }
                        }
                    //If the current username is not found in the database
                    if (found == false){
                        Console.WriteLine("\nUsername not found!");
                    }
                    else{
                        Console.WriteLine("Password");
                        string? getPassword = Console.ReadLine();
                        //Validates for the correct password
                        if (getPassword == userPassword){
                            Console.WriteLine("\nLogin successful!");
                            //User Menu initialization
                            UserMenu uMenu = new UserMenu();
                            uMenu.Start(getUsername!);        
                        }
                        else{
                            Console.WriteLine("\nIncorrect password.");
                        }
                    }
                    break;
                case "3":
                    Console.WriteLine("\nPlease enter your admin key to continue.");
                    string? inp = Console.ReadLine();
                    if (inp == "emily"){
                        Console.WriteLine("\nLogged in to admin account.");
                        //Opens up admin menu
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