using CustomExceptions;
namespace Models;

public class Product {
    

    public Product(){}

    public string Name { get; set;}

    public string Description {get; set;}

    private string _price;
    public string Price{ 
        
        get => _price;
        
        set {
            float newP;
            //Checks if the string is a valid float number
            if (!(float.TryParse(value, out newP))){
                throw new InputInvalidException("Price must be a number.");
            }
            this._price = value;
        }
        }

    private string _quantity;
    public string Quantity{ 
        
        get => _quantity;
        
        set {
            int newQ;
            //checks if the string is a valid integere
            if (!(int.TryParse(value, out newQ))){
                throw new InputInvalidException("Quantity must be an integer.");
            }
            this._quantity = value;
        }
        }
    

}