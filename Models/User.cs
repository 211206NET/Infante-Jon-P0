﻿namespace Models;
public class User{

    public User(){}
    public string? Username { get; set; }
    public string? Password { get; set; }

    public List<ProductOrder>? ShoppingCart { get; set; }

}
