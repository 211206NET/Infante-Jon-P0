
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
        //Establishing new connection
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        //Our insert command to add a store
        string sqlCmd = "INSERT INTO Store (ID, Address, Name, City, State) VALUES (@ID, @address, @name, @city, @state)"; 
        using SqlCommand cmdAddStore = new SqlCommand(sqlCmd, connection);
        //Adding paramaters
        cmdAddStore.Parameters.AddWithValue("@ID", storeToAdd.ID);
        cmdAddStore.Parameters.AddWithValue("@address", storeToAdd.Address);
        cmdAddStore.Parameters.AddWithValue("@name", storeToAdd.Name);
        cmdAddStore.Parameters.AddWithValue("@city", storeToAdd.City);
        cmdAddStore.Parameters.AddWithValue("@state", storeToAdd.State);
        //Executing command
        cmdAddStore.ExecuteNonQuery();
        connection.Close();
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
                if(productOrderTable != null){
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
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        //Deletes all the products of the current store
        string sqlCascadeDelCmd = $"DELETE FROM Product WHERE storeID=@stID";
        string sqlDelCmd = $"DELETE FROM Store WHERE ID=@storeID";
        using SqlCommand cmdcasc = new SqlCommand(sqlCascadeDelCmd, connection);
        cmdcasc.Parameters.AddWithValue("@stID", storeID);
        using SqlCommand cmddelstore = new SqlCommand(sqlDelCmd, connection);
        cmddelstore.Parameters.AddWithValue("@storeID", storeID);

        //Deletes all the products with the storeID selected
        cmdcasc.ExecuteNonQuery();
        //Deletes the current store after all products with the store id are removed
        cmddelstore.ExecuteNonQuery();
        connection.Close();
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
        //Establishing new connection
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        //Our insert command to add a product
        string sqlCmd = "INSERT INTO Product (ID, storeID, Name, Description, Price, Quantity) VALUES (@ID, @storeID, @name, @desc, @price, @qty)"; 
        using SqlCommand cmdAddProduct = new SqlCommand(sqlCmd, connection);
        //Adding paramaters
        cmdAddProduct.Parameters.AddWithValue("@ID", productToAdd.ID);
        cmdAddProduct.Parameters.AddWithValue("@storeID", productToAdd.storeID);
        cmdAddProduct.Parameters.AddWithValue("@name", productToAdd.Name);
        cmdAddProduct.Parameters.AddWithValue("@desc", productToAdd.Description);
        cmdAddProduct.Parameters.AddWithValue("@price", productToAdd.Price);
        cmdAddProduct.Parameters.AddWithValue("@qty", productToAdd.Quantity);
        //Executing command
        cmdAddProduct.ExecuteNonQuery();
        connection.Close();
        
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
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        //Deletes a single product by id
        string sqlDelCmd = $"DELETE FROM Product WHERE ID = @prodID";
        using SqlCommand cmdDelProd = new SqlCommand(sqlDelCmd, connection);
        cmdDelProd.Parameters.AddWithValue("@prodID", prodID);
        //Deletes the current product selected
        cmdDelProd.ExecuteNonQuery();
        connection.Close();
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
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        //Updates a single product by id, and passed in requirements
        string sqlEditCmd = $"UPDATE Product SET Description = @desc, Price = @prc, Quantity = @qty WHERE ID = @prodID";
        using SqlCommand cmdEditProd = new SqlCommand(sqlEditCmd, connection);
        //Adds the paramaters to the sql command
        cmdEditProd.Parameters.AddWithValue("@desc", description);
        cmdEditProd.Parameters.AddWithValue("@prc", price);
        cmdEditProd.Parameters.AddWithValue("@qty", quantity);
        cmdEditProd.Parameters.AddWithValue("@prodID", prodID);
        //Edits the current product selected
        cmdEditProd.ExecuteNonQuery();
        connection.Close();
    }

    /// <summary>
    /// Adds a store order to the database with the correct information to be retrieved later
    /// </summary>
    /// <param name="storeID">current store ID</param>
    /// <param name="storeOrderToAdd">Store object to add to the database</param>
    public void AddStoreOrder(int storeID, StoreOrder storeOrderToAdd){
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        string sqlInsertCmd = "INSERT INTO StoreOrder (ID, userID, referenceID, storeID, currDate, DateSeconds, TotalAmount) VALUES (@ID, @uID, @refID, @stID, @date, @dateS, @tAmount)";
        //Creates the new sql command
        using SqlCommand cmd = new SqlCommand(sqlInsertCmd, connection);
        //Adds the paramaters to the insert command
        cmd.Parameters.AddWithValue("@ID", storeOrderToAdd.ID);
        cmd.Parameters.AddWithValue("@uID", storeOrderToAdd.userID);
        cmd.Parameters.AddWithValue("@refID", storeOrderToAdd.referenceID);
        cmd.Parameters.AddWithValue("@stID", storeOrderToAdd.storeID);
        cmd.Parameters.AddWithValue("@date", storeOrderToAdd.currDate);
        cmd.Parameters.AddWithValue("@dateS", storeOrderToAdd.DateSeconds);
        cmd.Parameters.AddWithValue("@tAmount", storeOrderToAdd.TotalAmount);
        //Executes the insert command
        cmd.ExecuteNonQuery();
        connection.Close();
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