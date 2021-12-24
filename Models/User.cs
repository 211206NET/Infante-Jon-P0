namespace Models;
public class User{

    public User(){}
    public int? ID { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }

    public List<ProductOrder>? ShoppingCart { get; set; }
    public List<StoreOrder>? FinishedOrders { get; set; }

}
