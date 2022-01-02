using CustomExceptions;
namespace Models;

public class ProductOrder {
    

    public ProductOrder(){}

    public int? ID { get; set; }
    public int? userID { get; set; }
    public int? storeID { get; set; }
    public int? productID { get; set; }

    public string? ItemName { get; set; }

    public decimal TotalPrice { get; set; }

    private int? _quantity;
    public int? Quantity{ 
        
        get => _quantity;
        
        set {
            if (value <= 0){
                throw new InputInvalidException("\nQuantity must greater than 0. Please enter a valid amount:");
            }                   
            
            this._quantity = value;
        }
    }


}