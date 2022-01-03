namespace UI;

//Design pattern useful for making a similarly shaped object
public static class MenuFactory{
    public static IMenu GetMenu(string menuString){
        StoreBL sbl = new StoreBL();
        IURepo repo = new UserRepo();
        UserBL iubl = new UserBL(repo);

        switch(menuString){
            //root
            case "login":
                return new LoginMenu(iubl);
            //under login
            case "admin":
                return new AdminMenu(sbl);
            //under admin
            case "allStores":
                return new AllStoresMenu(sbl);
            //invalid menu selected
            default:
                Console.WriteLine("Invalid Menu selected");
                return new LoginMenu(iubl);
        }

    }
}