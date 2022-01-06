namespace DL;

public class DBUserRepo : IURepo {

    private string _connectionString;
    public DBUserRepo(string connectionString){
        _connectionString = connectionString;

    }

    /// <summary>
    /// Adds a user object to the database
    /// </summary>
    /// <param name="userToAdd"></param>
    public void AddUser(User userToAdd){
        string selectCmd = "SELECT * FROM Customer";
        using(SqlConnection connection = new SqlConnection(_connectionString)){
            using(SqlDataAdapter dataAdapter = new SqlDataAdapter(selectCmd, connection)){
                //DataSet is essentially just a container that holds data in memory
                // it holds one or more DataTables
                DataSet customerSet = new DataSet();
                //names table 'customer'
                dataAdapter.Fill(customerSet, "Customer");

                DataTable customerTable = customerSet.Tables["customer"]!;

                //Generates new row with the customer table schema
                DataRow newRow = customerTable.NewRow()!;

                //Fill with new customer information
                newRow["ID"] = userToAdd.ID;
                newRow["Username"] = userToAdd.Username ?? "";
                newRow["Password"] = userToAdd.Password ?? "";
                
                //Add a new row to our restaurant table
                customerTable.Rows.Add(newRow);

                //We need to set which query to execute for changes
                //We need to set SqlDataAdapater.InsertCommand to let it know
                //how to insert the new records into the customerTable

                string insertCmd = $"INSERT INTO Customer (ID, Username, Password) VALUES ({userToAdd.ID}, '{userToAdd.Username}', '{userToAdd.Password}')";

                SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(dataAdapter);

                //we have to tell the adapter how to insert the data
                dataAdapter.InsertCommand = cmdBuilder.GetInsertCommand();
                //GetUpdateCommand, GetDeleteCommand
                //SqlDataAdapter.UpdateCommand (for your put/update operations)
                //SqlDataAdapter.DeleteCommand (for delete/destroy operations)
                
                //Tell the datadapter to update the DB with changes
                dataAdapter.Update(customerTable);

        }
    }
    }

    /// <summary>
    /// Gets the entire list of users from the database[named customer inside the database due to special naming by SQL]
    /// </summary>
    /// <returns>Returns a list of customer(user) objects</returns>
    public List<User> GetAllUsers(){
        List<User> allUsers = new List<User>();

        using SqlConnection connection = new SqlConnection(_connectionString);
        string customerSelect = "Select * From Customer";
        string productOrderSelect = "Select * From ProductOrder";
        string storeOrderSelect = "Select * From StoreOrder";
        
        //A single dataSet to hold all our data
        DataSet usSet = new DataSet();

        //Three different adapters for different tables
        using SqlDataAdapter customerAdapter = new SqlDataAdapter(customerSelect, connection);
        using SqlDataAdapter productOrderAdapter = new SqlDataAdapter(productOrderSelect, connection);
        using SqlDataAdapter storeOrderAdapter = new SqlDataAdapter(storeOrderSelect, connection);

        //Filling the Dataset with each table
        customerAdapter.Fill(usSet, "Customer");
        productOrderAdapter.Fill(usSet, "ProductOrder");
        storeOrderAdapter.Fill(usSet, "StoreOrder");

        //Declaring each data table from the dataset
        DataTable? customerTable = usSet.Tables["Customer"];
        DataTable? productOrderTable = usSet.Tables["ProductOrder"];
        DataTable? storeOrderTable = usSet.Tables["StoreOrder"];

        if(customerTable != null){   
            foreach(DataRow row in customerTable.Rows){
                //Use customer constructor with DataRow object to quickly create user with parameters
                User user = new User(row);

                //Assigns each product order corresponding to the current user
                if (productOrderTable != null){
                    user.ShoppingCart = productOrderTable.AsEnumerable().Where(r => (int) r["userID"] == user.ID && r["storeOrderID"] == null).Select(
                        r => new ProductOrder(r)
                    ).ToList();
                }
                //Assigns each store order corresponding to the current user [uses reference id to match user id]
                if (storeOrderTable != null){
                    user.FinishedOrders = storeOrderTable.AsEnumerable().Where(r => (int) r["referenceID"] == user.ID).Select(
                        r => new StoreOrder(r)
                    ).ToList();
                }
                //Adds each product order to each store order in the list of users
                if(productOrderTable != null){
                    foreach(StoreOrder storeOrder in user.FinishedOrders!){
                        storeOrder.Orders = productOrderTable!.AsEnumerable().Where(r => (int) r["storeOrderID"] == storeOrder.ID).Select(
                            r => new ProductOrder(r)
                        ).ToList();
                        }
                    }  
                //Add each store to the list of users
                allUsers.Add(user);
            }
        }
        return allUsers;
    }

    /// <summary>
    /// Gets the current user from the list of users by ID
    /// </summary>
    /// <param name="userID">current user ID selected</param>
    /// <returns>User Object</returns>
    public User GetCurrentUserByID(int userID){
        List<User> allUsers = GetAllUsers();
        foreach(User user in allUsers){
            if(user.ID == userID){
                return user;
            }
        }
        //User not found
        return new User();
    }

    /// <summary>
    /// Adds a product order to the user's shopping cart inside the database
    /// </summary>
    /// <param name="currUser">current user object inputted</param>
    /// <param name="currProdOrder">product order object to add to the database</param>
    public void AddProductOrder(User currUser, ProductOrder currProdOrder){

    }

    /// <summary>
    /// Edits a selected product order in the user's shopping cart, and saves it back to the database
    /// </summary>
    /// <param name="currUser">current user selected</param>
    /// <param name="prodOrderID">ID of the product order selected to edit</param>
    /// <param name="quantity">New quantity to update</param>
    public void EditProductOrder(User currUser, int prodOrderID, int quantity){

    }

    /// <summary>
    /// Remove a Product Order from the user's shopping cart and database
    /// </summary>
    /// <param name="currUser">current user object inputted</param>
    /// <param name="prodOrderID">the product order ID of the product order we have selected</param>
    public void DeleteProductOrder(User currUser, int prodOrderID){

    }

    /// <summary>
    /// Adds a store order to the user's list of previous orders and to the database
    /// </summary>
    /// <param name="currUser">current user object selected</param>
    /// <param name="currStoreOrder">storeOrder object to add to previous orders</param>
    public void AddUserStoreOrder(User currUser, StoreOrder currStoreOrder){

    }

    /// <summary>
    /// Removes every product order from the user's shopping cart, and deletes each instance inside the database
    /// </summary>
    /// <param name="currUser">current user object selected</param>
    public void ClearShoppingCart(User currUser){
        
    }

    //Unused with database implementation
    public int GetCurrentUserIndexByID(int userID){
        return 0;
    }
}