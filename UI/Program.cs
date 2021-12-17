//Initialize start of program at login menu

IURepo repo = new UserRepo();
UserBL bl = new UserBL(repo);
LoginMenu menu = new LoginMenu(bl);
menu.Start();