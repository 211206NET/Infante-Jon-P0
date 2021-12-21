namespace BL;

public class StoreBL {
    private StoreRepo _dl;

    public StoreBL() {
        _dl = new StoreRepo();
    }
    /// <summary>
    /// Gets all stores
    /// </summary>
    /// <returns>list of all stores</returns>
    public List<Store> GetAllStores(){
        return _dl.GetAllStores();

    }
    /// <summary>
    /// Adds a new store to the list
    /// </summary>
    /// <param name="storeToAdd">store object to add</param>
    public void AddStore(Store storeToAdd){
        _dl.AddStore(storeToAdd);
    }

}
