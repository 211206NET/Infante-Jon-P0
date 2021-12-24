using CustomExceptions;
namespace Models;

public class ProductOrder {
    

    public ProductOrder(){}

    public string? ID { get; set; }

    public string? ItemName { get; set; }

    public string? TotalPrice { get; set; }

    private string? _quantity;
    public string? Quantity{ 
        
        get => _quantity;
        
        set {
            int newQ;
            //checks if the string is a valid integer

                if (!(int.TryParse(value, out newQ))){
                    throw new InputInvalidException("\nQuantity must be an integer. Please enter a valid input: ");
                }
                else{
                    if (newQ <= 0){
                        throw new InputInvalidException("\nQuantity must greater than 0. Please enter a valid amount:");
                    }
                }
            
            this._quantity = value;
        }
    }


}