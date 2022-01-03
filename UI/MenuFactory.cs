namespace UI;

//Design pattern useful for making a similarly shaped object
public static class MenuFactory{
    public static IMenu GetMenu(string menuString){
        ISRepo sRepo = new StoreRepo();
        StoreBL sbl = new StoreBL(sRepo);
        IURepo uRepo = new UserRepo();
        UserBL iubl = new UserBL(uRepo);

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