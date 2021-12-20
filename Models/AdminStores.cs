namespace Models;
public class AdminStores {
    //In a restaurant, I want to store the name, city, and state.

    //This is a propety, a type member
    //Access modifier controls the visibility of type and type members
    //Public, Private, Internal, and Protected
    //By default, class member has private access modifier
    //Class by default are internal

    public AdminStores(){
        this.Stores = new List<Store>();
    }

    public List<Store> Stores { get; set; }


}