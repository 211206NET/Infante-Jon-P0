using Xunit;
using Models;
using CustomExceptions;
using System.Collections.Generic;

namespace Tests;

public class ModelsTest{
    [Fact]
    public void UserShouldCreate(){
        //Arrange
        //To test this case I make sure im using the models namespace
        
        //Act: Create user object
        User testUser = new User();

        //Assert: Assert that the user was created
        Assert.NotNull(testUser);
    }

    [Fact]
    public void UserShouldSetValue(){
        //Arrange
        User testUser = new User();
        int ID = 42;
        string username = "Jon";
        string password = "banana";
        List<ProductOrder> shoppingCart = new List<ProductOrder>();
        List<StoreOrder> finishedOrders = new List<StoreOrder>();

        //Act
        testUser.ID = ID;
        testUser.Username = username;
        testUser.Password = password;
        testUser.ShoppingCart = shoppingCart;
        testUser.FinishedOrders = finishedOrders;

        //Assert
        Assert.Equal(ID, testUser.ID);
        Assert.Equal(username, testUser.Username);
        Assert.Equal(password, testUser.Password);
        Assert.Equal(shoppingCart, testUser.ShoppingCart);
        Assert.Equal(finishedOrders, testUser.FinishedOrders);

    }

    [Fact]
    public void ProductShouldCreateAndSetValues(){
        //Arrange
        Product testProduct = new Product();
        int ID = 99;
        int storeID = 9994;
        string name = "Iphone 12";
        string description = "Brand new";
        //Strings are used for price and quantity due to previous implementation of a method comparing if 
        //the value of the property was an empty string when entered by the user.
        string price = "99";
        string quantity = "4";

        //Act
        testProduct.ID = ID;
        testProduct.storeID = storeID;
        testProduct.Name = name;
        testProduct.Description = description;
        testProduct.Price = price;
        testProduct.Quantity = quantity;

        //Assert
        Assert.Equal(ID, testProduct.ID);
        Assert.Equal(storeID, testProduct.storeID);
        Assert.Equal(name, testProduct.Name);
        Assert.Equal(description, testProduct.Description);
        Assert.Equal(price, testProduct.Price);
        Assert.Equal(quantity, testProduct.Quantity);
    }

    [Theory]
    [InlineData("v", "6.4")]
    [InlineData("banana", "ty")]
    [InlineData("you", "5.3")]
    [InlineData("undefined", "4.1")]
    [InlineData("t", "66.66")]
    public void ProductShouldNotCreateInvalidQuantityorPrice(string price, string quantity){
        //Arrange: Testing if the product will have an invalid price or quantity. 
        //Price should be a decimal
        //Quantity should be an integer
        Product testProduct = new Product();

        //Act: Using inline data as paramaters

        //Assert
        Assert.Throws<InputInvalidException>(() => testProduct.Price = price);
        Assert.Throws<InputInvalidException>(() => testProduct.Quantity = quantity);
    }
}