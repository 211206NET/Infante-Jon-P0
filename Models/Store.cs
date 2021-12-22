namespace Models;

public class Store {
    
    public Store(){}

    public string? Name { get; set;}
    public string? Address{ get; set;}

    public string? City { get; set; }
    
    public string? State { get; set; }

    public List<Product>? Products { get; set; }

    public override string ToString(){
          return ($"Store: {this.Name}\n    City: {this.City}, State: {this.State}\n    Address: {this.Address}");
    }


}