using CustomExceptions;
namespace Models;

public class ProductOrder {
    

    public ProductOrder(){}

    public string? ItemName { get; set; }

    public string? TotalPrice { get; set; }

    private string? _quantity;
    public string? Quantity{ 
        
        get => _quantity;
        
        set {
            int newQ;
            //checks if the string is a valid integer
            if (!(int.TryParse(value, out newQ))){
                throw new InputInvalidException("Quantity must be an integer.");
            }
            this._quantity = value;
        }
    }

    public string? Date { get; set; }


}