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
        string jsonString = File.ReadAllText(filePath);
        List<Store> jsonDeserialized = JsonSerializer.Deserialize<List<Store>>(jsonString);
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
 
}