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
    /// <summary>
    /// Adds a product to the current selected store
    /// </summary>
    /// <param name="index">index of the store in the store list</param>
    /// <param name="productToAdd">product we are adding to the store</param>
    public void AddProduct(int index, Product productToAdd){
        _dl.AddProduct(index, productToAdd);
    }
    /// <summary>
    /// Deletes a product from the current selected store and product index
    /// </summary>
    /// <param name="storeIndex">Store's current index</param>
    /// <param name="prodIndex">Product's current index</param>
    public void DeleteProduct(int storeIndex, int prodIndex){
        _dl.DeleteProduct(storeIndex, prodIndex);

    }
    /// <summary>
    /// Edits and updates the product selected in the current store
    /// </summary>
    /// <param name="storeIndex">Current store index</param>
    /// <param name="prodIndex">Index of the product to edit</param>
    /// <param name="description">Product's new description</param>
    /// <param name="price">Product's new price</param>
    /// <param name="quantity">Product's new quantity</param>
    public void EditProduct(int storeIndex, int prodIndex, string description, string price, string quantity){
        _dl.EditProduct(storeIndex, prodIndex, description, price, quantity);
    }
    /// <summary>
    /// Takes the current lists of product orders, packages them in a store order and adds to list
    /// </summary>
    /// <param name="storeIndex">Index of current store selected</param>
    /// <param name="storeOrderToAdd">Store order packaged and ready to add</param>
    public void AddStoreOrder(int storeIndex, StoreOrder storeOrderToAdd){
        _dl.AddStoreOrder(storeIndex, storeOrderToAdd);
    }

}
