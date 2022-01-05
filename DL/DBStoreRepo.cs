using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;

namespace DL;

public class DBStoreRepo : ISRepo {

    private string _connectionString;
    public DBStoreRepo(string connectionString){
        _connectionString = connectionString;

    }
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
        productAdapter.Fill(StSet, "ProductOrder");

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
                        r =>
                            new Product {
                                ID = (int) r["ID"],
                                storeID = (int) r["storeID"],
                                Name = r["Name"].ToString() ?? "",
                                Description = r["Description"].ToString() ?? "",
                                Price = (decimal) r["Price"],
                                Quantity = (int) r["Quantity"]
                            }
                    ).ToList();
                }
                //Assigns each store order corresponding to the current store
                if (storeOrderTable != null){
                    store.AllOrders = storeOrderTable.AsEnumerable().Where(r => (int) r["storeID"] == store.ID).Select(
                        r =>
                            new StoreOrder {
                                ID = (int) r["ID"],
                                userID = (int) r["userID"],
                                referenceID = (int) r["referenceID"],
                                storeID = (int) r["storeID"],
                                Date = r["Date"].ToString() ?? "",
                                DateSeconds = (double)r["DateSeconds"]
                            }
                    ).ToList();
                }
                //Adds each product order to each store order in the list of stores
                if(productTable != null){
                    foreach(StoreOrder storOrder in store.AllOrders){
                        storeOrder.Orders = productOrderTable.AsEnumerable().Where(r => (int) r["storeOrderID"] == storeOrder.ID).Select(
                            r =>
                                new ProductOrder {
                                    ID = (int) r["ID"],
                                    userID = (int) r["userID"],
                                    referenceID = (int) r["referenceID"],
                                    storeID = (int) r["storeID"],
                                    Date = r["Date"].ToString() ?? "",
                                    DateSeconds = (double)r["DateSeconds"]
                                }
                        ).ToList();
                        }
                    }
                
                //Add each store to the list of stores
                allStores.Add(store);
            }
        }
        return allStores;
    }

    public void DeleteStore(int storeID){}

    public Store GetStoreByID(int storeID){
        return new Store();
    }
    
    public int GetStoreIndexByID(int storeID){
        return 0;
    }

    public Product GetProductByID(int storeID, int prodID){
        return new Product();
    }

    public int GetProductIndexByID(int storeID, int prodID){
        return 0;
    }

    public void AddProduct(int storeID, Product productToAdd){}

    public void DeleteProduct(int storeID, int prodID){}

    public void EditProduct(int storeID, int prodID, string description, decimal price, int quantity){}
    
    public void AddStoreOrder(int storeID, StoreOrder storeOrderToAdd){}

}