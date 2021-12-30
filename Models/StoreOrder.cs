namespace Models;

public class StoreOrder{

    public StoreOrder(){}

    public int? ID { get; set; }
    public int? userID { get; set; }
    public string? Date { get; set; }
    public double DateSeconds { get; set; }
    public decimal TotalAmount { get; set; }
    public List<ProductOrder>? Orders { get; set; }

}