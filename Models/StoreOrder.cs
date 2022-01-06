namespace Models;

public class StoreOrder{

    public StoreOrder(){}

    public StoreOrder(DataRow r){
        ID = (int) r["ID"];
        userID = (int) r["userID"];
        referenceID = (int) r["referenceID"];
        storeID = (int) r["storeID"];
        currDate = r["currDate"].ToString() ?? "";
        DateSeconds = (double)r["DateSeconds"];
    }

    public int? ID { get; set; }

    public int? userID { get; set; }

    public int? referenceID { get; set; }

    public int? storeID {get; set; }

    public string? currDate { get; set; }

    public double DateSeconds { get; set; }

    public decimal TotalAmount { get; set; }
    
    public List<ProductOrder>? Orders { get; set; }

}