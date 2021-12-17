using System.Text.RegularExpressions;

namespace UI;

public class LoginMenu {
    private IUBL _bl;

    public LoginMenu(IUBL bl){
        _bl = bl;
    }
    public void Start(){
        Console.WriteLine("\nWelcome to Jon's Used Hardware Store!\n");
        bool exit = false;
        while(!exit){
            WriteColor("=============[Login Menu]=============", ConsoleColor.DarkCyan);
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("[1] Sign Up");
            Console.WriteLine("[2] Login as User");
            Console.WriteLine("[3] Sign in as Administrator");
            WriteColor("\n           [Enter x to Exit]", ConsoleColor.DarkRed);
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
    static void WriteColor(string message, ConsoleColor color){
        var pieces = Regex.Split(message, @"(\[[^\]]*\])");

        for(int i=0;i<pieces.Length;i++)
        {
            string piece = pieces[i];
            
            if (piece.StartsWith("[") && piece.EndsWith("]"))
            {
                Console.ForegroundColor = color;
                piece = piece.Substring(1,piece.Length-2);          
            }
            
            Console.Write(piece);
            Console.ResetColor();
        }
        
        Console.WriteLine();
    }
}