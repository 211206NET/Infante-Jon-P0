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
        return new List<Store>();
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