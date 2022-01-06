
namespace DL;

public class DBStoreRepo : ISRepo {

    private string _connectionString;
    public DBStoreRepo(string connectionString){
        _connectionString = connectionString;

    }

    /// <summary>
    /// Adds a store to the database with the given store object
    /// </summary>
    /// <param name="storeToAdd"></param>
    public void AddStore(Store storeToAdd){
        string selectCmd = "SELECT * FROM Store";
        using(SqlConnection connection = new SqlConnection(_connectionString)){
            using(SqlDataAdapter dataAdapter = new SqlDataAdapter(selectCmd, connection)){
                //DataSet is essentially just a container that holds data in memory
                // it holds one or more DataTables
                DataSet storeSet = new DataSet();
                //names table 'Store'
                dataAdapter.Fill(storeSet, "Store");

                DataTable storeTable = storeSet.Tables["Store"]!;

                //Generates new row with the store table schema
                DataRow newRow = storeTable.NewRow()!;

                //Fill with new store information
                newRow["ID"] = storeToAdd.ID;
                newRow["Address"] = storeToAdd.Address ?? "";
                newRow["Name"] = storeToAdd.Name;
                newRow["City"] = storeToAdd.City ?? "";
                newRow["State"] = storeToAdd.State ?? "";
                
                //Add a new row to our restaurant table
                storeTable.Rows.Add(newRow);

                //We need to set which query to execute for changes
                //We need to set SqlDataAdapater.InsertCommand to let it know
                //how to insert the new records into the storeTable

                string insertCmd = $"INSERT INTO Store (ID, Address, Name, City, State) VALUES ({storeToAdd.ID}, '{storeToAdd.Address}', '{storeToAdd.Name}', '{storeToAdd.City}', '{storeToAdd.State}')";

                SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(dataAdapter);

                //we have to tell the adapter how to insert the data
                dataAdapter.InsertCommand = cmdBuilder.GetInsertCommand();
                //GetUpdateCommand, GetDeleteCommand
                //SqlDataAdapter.UpdateCommand (for your put/update operations)
                //SqlDataAdapter.DeleteCommand (for delete/destroy operations)
                
                //Tell the datadapter to update the DB with changes
                dataAdapter.Update(storeTable);
            }
        }
    }

    /// <summary>
    /// Gets a list of all the stores from the database. Fills in the nested lists within each store for
    /// products, store orders, and product orders inside each store order.
    /// </summary>
    /// <returns>List of Store Objects</returns>
    public List<Store> GetAllStores(){
        List<Store> allStores = new List<Store>();

        using SqlConnection connection = new SqlConnection(_connectionString);
        string storeSelect = "Select * From Store";
        string productSelect = "Select * From Product";
        string storeOrderSelect = "Select * From StoreOrder";
        string productOrderSelect = "Select * From ProductOrder";
        
        //A single dataSet to hold all our data
        DataSet StSet = new DataSet();

        //Three different adapters for different tables
        using SqlDataAdapter storeAdapter = new SqlDataAdapter(storeSelect, connection);
        using SqlDataAdapter productAdapter = new SqlDataAdapter(productSelect, connection);
        using SqlDataAdapter storeOrderAdapter = new SqlDataAdapter(storeOrderSelect, connection);
        using SqlDataAdapter productOrderAdapter = new SqlDataAdapter(productOrderSelect, connection);

        //Filling the Dataset with each table
        storeAdapter.Fill(StSet, "Store");
        productAdapter.Fill(StSet, "Product");
        storeOrderAdapter.Fill(StSet, "StoreOrder");
        productOrderAdapter.Fill(StSet, "ProductOrder");

        //Declaring each data table from the dataset
        DataTable? storeTable = StSet.Tables["Store"];
        DataTable? productTable = StSet.Tables["Product"];
        DataTable? storeOrderTable = StSet.Tables["StoreOrder"];
        DataTable? productOrderTable = StSet.Tables["ProductOrder"];

        if(storeTable != null){   
            foreach(DataRow row in storeTable.Rows){
                //Use store constructor with DataRow object to quickly create store with parameters
                Store store = new Store(row);

                //Assigns each product corresponding to the current store
                if (productTable != null){
                    store.Products = productTable.AsEnumerable().Where(r => (int) r["storeID"] == store.ID).Select(
                        r => new Product(r)
                    ).ToList();
                }
                //Assigns each store order corresponding to the current store
                if (storeOrderTable != null){
                    store.AllOrders = storeOrderTable.AsEnumerable().Where(r => (int) r["storeID"] == store.ID).Select(
                        r => new StoreOrder(r)
                    ).ToList();
                }
                //Adds each product order to each store order in the list of stores
                if(productTable != null){
                    foreach(StoreOrder storeOrder in store.AllOrders!){
                        storeOrder.Orders = productOrderTable!.AsEnumerable().Where(r => (int) r["storeOrderID"] == storeOrder.ID).Select(
                            r => new ProductOrder(r)
                        ).ToList();
                        }
                    }  
                //Add each store to the list of stores
                allStores.Add(store);
            }
        }
        return allStores;
    }
    /// <summary>
    /// Deletes an entire store from the database
    /// </summary>
    /// <param name="storeID">current store ID selected</param>
    public void DeleteStore(int storeID){
        
    }

    /// <summary>
    /// Gets the current store details from the list of stores
    /// </summary>
    /// <param name="storeID">selected storeID</param>
    /// <returns>Store object</returns>
    public Store GetStoreByID(int storeID){
        List<Store> allStores = GetAllStores();
        foreach(Store store in allStores){
            if(store.ID == storeID){
                return store;
            }
        }
        //Cant find any stores with that id
        return new Store();
    }

    /// <summary>
    /// Adds a product to the selected store inside the database
    /// </summary>
    /// <param name="storeID">current storeID selected</param>
    /// <param name="productToAdd">Product object to add to t he database</param>
    public void AddProduct(int storeID, Product productToAdd){
        string selectCmd = "SELECT * FROM Product";
        using(SqlConnection connection = new SqlConnection(_connectionString)){
            using(SqlDataAdapter dataAdapter = new SqlDataAdapter(selectCmd, connection)){
                //DataSet is essentially just a container that holds data in memory
                // it holds one or more DataTables
                DataSet productSet = new DataSet();
                //names table 'product'
                dataAdapter.Fill(productSet, "Product");

                DataTable productTable = productSet.Tables["Product"]!;

                //Generates new row with the product table schema
                DataRow newRow = productTable.NewRow()!;

                //Fill with new product information
                newRow["ID"] = productToAdd.ID;
                newRow["storeID"] = productToAdd.storeID;
                newRow["Name"] = productToAdd.Name ?? "";
                newRow["Description"] = productToAdd.Description ?? "";
                newRow["Price"] = productToAdd.Price;
                newRow["Quantity"] = productToAdd.Quantity;
                
                //Add a new row to our restaurant table
                productTable.Rows.Add(newRow);

                //We need to set which query to execute for changes
                //We need to set SqlDataAdapater.InsertCommand to let it know
                //how to insert the new records into the productTable
                string insertCmd = $"INSERT INTO Product (ID, storeID, Name, Description, Price, Quantity) VALUES ({productToAdd.ID}, {productToAdd.storeID}, '{productToAdd.Name}', '{productToAdd.Description}', {productToAdd.Price}, {productToAdd.Quantity})";

                SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(dataAdapter);

                //we have to tell the adapter how to insert the data
                dataAdapter.InsertCommand = cmdBuilder.GetInsertCommand();
                
                //Tell the datadapter to update the DB with changes
                dataAdapter.Update(productTable);
            }
        }
    }

    /// <summary>
    /// Returns a product by the ID given
    /// </summary>
    /// <param name="storeID">current store ID</param>
    /// <param name="prodID">product object ID selected</param>
    /// <returns>Product Object</returns>
    public Product GetProductByID(int storeID, int prodID){
        Store currStore = GetStoreByID(storeID);
        foreach(Product product in currStore.Products!){
            if(product.ID == prodID){
                return product;
            }
        }
        //Cant find any Products with that id
        return new Product();
    }

    /// <summary>
    /// Delets a product from the database
    /// </summary>
    /// <param name="storeID">current store ID</param>
    /// <param name="prodID">selected product ID</param>
    public void DeleteProduct(int storeID, int prodID){

    }

    /// <summary>
    /// Edits a product and saves it back to the database
    /// </summary>
    /// <param name="storeID">current store ID</param>
    /// <param name="prodID">selected product ID</param>
    /// <param name="description">New description to update</param>
    /// <param name="price">New price entered to update</param>
    /// <param name="quantity">New quantity to update</param>
    public void EditProduct(int storeID, int prodID, string description, decimal price, int quantity){

    }

    /// <summary>
    /// Adds a store order to the database with the correct information to be retrieved later
    /// </summary>
    /// <param name="storeID">current store ID</param>
    /// <param name="storeOrderToAdd">Store object to add to the database</param>
    public void AddStoreOrder(int storeID, StoreOrder storeOrderToAdd){

    }
        
    //Unused with database implementation
    public int GetProductIndexByID(int storeID, int prodID){
        return 0;
    }
    //Unused with database imnlementation
    public int GetStoreIndexByID(int storeID){
        return 0;
    }

}