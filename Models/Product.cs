using CustomExceptions;
namespace Models;

public class Product {
    

    public Product(){}

    public int? ID { get; set; }
    public int? storeID {get; set; }
    public string? Name { get; set;}

    public string? Description {get; set;}

    private string? _price;
    public string? Price{ 
        
        get => _price;
        
        set {
            decimal newP;
            //Checks if the string is a valid float number
            if (!(decimal.TryParse(value, out newP))){
                throw new InputInvalidException("Price must be a Decimal value.");
            }
            this._price = value;
            }
        }

    private string? _quantity;
    public string? Quantity{ 
        
        get => _quantity;
        
        set {
            int newQ;
            //checks if the string is a valid integer
            if (!(int.TryParse(value, out newQ))){
                throw new InputInvalidException("Quantity must be an integer.");
            }
            else{
                if (newQ < 0){
                    throw new InputInvalidException("\nQuantity must be 0 or higher. Please enter a valid amount:");
                    }
                }   
            this._quantity = value;
        }
    }
}