using System.Text.Json;

namespace DL;
public class StoreRepo{

    public StoreRepo(){
    }
    //make path from UI folder to file location
    private string filePath = "../DL/Stores.json";

    /// <summary>
    /// Gets all stores from a file
    /// </summary>
    /// <returns>List of all stores</returns>
    public List<Store> GetAllStores(){
        //returns all restaurants written in the file
        string jsonString = File.ReadAllText(filePath)!;
        List<Store> jsonDeserialized = JsonSerializer.Deserialize<List<Store>>(jsonString)!;
        return jsonDeserialized;
    }   

    /// <summary>
    /// Adds a store to the file
    /// </summary>
    /// <param name="storeToAdd">Store object</param>
    public void AddStore(Store storeToAdd){
        List<Store> allStores = GetAllStores();
        allStores.Add(storeToAdd);
        string jsonString = JsonSerializer.Serialize(allStores);
        File.WriteAllText(filePath, jsonString);
    }
    public void AddProduct(int storeIndex, Product productToAdd){
    List<Store> allStores = GetAllStores();
    
    Store currStore = allStores[storeIndex];
    if(currStore.Products == null){
        currStore.Products = new List<Product>();
        }
    currStore.Products.Add(productToAdd);
    string jsonString = JsonSerializer.Serialize(allStores);
    File.WriteAllText(filePath, jsonString);
    }
    /// <summary>
    /// Deletes a product from the current store
    /// </summary>
    /// <param name="storeIndex">Store selected</param>
    /// <param name="prodIndex">Product selected</param>
    public void DeleteProduct(int storeIndex, int prodIndex){
        List<Store> allStores = GetAllStores();
        Store currStore = allStores[storeIndex];
        currStore.Products!.RemoveAt(prodIndex);
        string jsonString = JsonSerializer.Serialize(allStores);
        File.WriteAllText(filePath, jsonString);
    }       
    /// <summary>
    /// Edits the product with the correct description, price, and quantity
    /// </summary>
    /// <param name="storeIndex">Index of store to edit the product</param>
    /// <param name="prodIndex">Product selected's index</param>
    /// <param name="description">New description</param>
    /// <param name="price">New price</param>
    /// <param name="quantity">New quantity</param>
    public void EditProduct(int storeIndex, int prodIndex, string description, string price, string quantity){
        List<Store> allStores = GetAllStores();
        Store currStore = allStores[storeIndex];
        Product currProduct = currStore.Products![prodIndex];
        currProduct.Description = description;
        currProduct.Price = price;
        currProduct.Quantity = quantity;
        string jsonString = JsonSerializer.Serialize(allStores);
        File.WriteAllText(filePath, jsonString);
    }
    /// <summary>
    /// Adds an order to the store's list of orders corresponding to the correct store
    /// </summary>
    /// <param name="storeIndex">Index of the current store</param>
    /// <param name="storeOrderToAdd">StoreOrder object to add</param>
    public void AddStoreOrder(int storeIndex, StoreOrder storeOrderToAdd){
        List<Store> allStores = GetAllStores();
        Store currStore = allStores[storeIndex];
        //If no store orders exist yet
        if(currStore.AllOrders == null){
            currStore.AllOrders = new List<StoreOrder>();
            }
        currStore.AllOrders.Add(storeOrderToAdd);
        string jsonString = JsonSerializer.Serialize(allStores);
        File.WriteAllText(filePath, jsonString);
    }

}