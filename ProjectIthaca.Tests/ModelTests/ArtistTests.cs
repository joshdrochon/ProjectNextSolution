using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ProjectIthaca.Models;
using ProjectIthaca;
using System;

namespace ProjectIthaca.Tests
{
  [TestClass]
  public class ClientTests : IDisposable
  {
    public ClientTests()
    {
      DBConfiguration.ConnectionString =
      "server=localhost;user id=root;password=root;port=8889;database=project_ithaca_test;";
    }

    public void Dispose()
    {
      Client.DeleteAll();
      Stylist.DeleteAll();
    }

    // [TestMethod]
    // public void GetAll_DatabaseEmptyAtFirst_0()
    // {
    //   //Arrange, Act
    //   int result = Client.GetAll().Count;
    //   Console.WriteLine("In line 29 in ClientTests there are " + result + " in the database.");
    //   //Assert
    //   Assert.AreEqual(0, result);
    // }
    //
    //
    // [TestMethod]
    // public void Save_SavesClientToDataBase_Client()
    // {
    //   //Arrange
    //   Client testClient = new Client
    //   ("Kilo Ren", "Kilo17@gmail.com", "06/12/12");
    //   int expectedResult = 1; //one client expected in database
    //   //Act
    //   testClient.Save();
    //   int actualResult = Client.GetAll().Count;
    //   //Assert
    //   Assert.AreEqual(expectedResult, actualResult);
    //   Console.WriteLine(expectedResult);
    //   Console.WriteLine(actualResult);
    // }
    //
    //
    // [TestMethod]
    // public void Save_AssignsIdToObject_Id()
    // {
    //   //Arrange
    //   Client testClient = new Client
    //   ("Han Solo", "Solo@gmail.com", "03/17/77");
    //
    //   //Act
    //   testClient.Save();
    //   Client savedClient = Client.GetAll()[0];
    //
    //   int result = savedClient.GetId();
    //   int testId = testClient.GetId();
    //
    //   //Assert
    //   Assert.AreEqual(result, testId);
    // }
    //
    // [TestMethod]
    // public void Find_FindsClientInDatabase_Client()
    // {
    //   //Arrange
    //   Client testClient = new Client
    //   ("Reese WitherFork", "ForkR@gmail.com", "02/29/2000");
    //   testClient.Save();
    //   //Act
    //   Client foundClient = Client.Find(testClient.GetId());
    //   //Assert
    //   Console.WriteLine("TEST CLIENT ID IS: " + testClient.GetId());
    //   Console.WriteLine("FOUND CLIENT IS IS: " + foundClient.GetId());
    //
    //   Assert.AreEqual(testClient, foundClient);
    // }

    [TestMethod]
    public void AddStylist_AddsStylistToClient_StylistList()
    {
      //Arrange
      Client testClient = new Client
      ("Ramen Noodles", "IamNoodle@gmail.com", "02/29/2019");
      testClient.Save();

      Stylist testStylist = new Stylist
      ("Finn", "Finn@gmail.com", "07/06/1941");
      testStylist.Save();

      //Act
      testClient.AddStylist(testStylist);

      List<Stylist> result = testClient.GetStylists();
      List<Stylist> testList = new List<Stylist>{testStylist};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetStylists_ReturnsAllClientStylists_StylistList()
    {
      //Arrange
      Client testClient = new Client
      ("Bill", "Nye@gmail.com", "06/13/2049");
      testClient.Save();

      Stylist testStylist1 = new Stylist
      ("Samuel Adams", "Adams@gmail.com", "06/13/1969");
      testStylist1.Save();

      Stylist testStylist2 = new Stylist
      ("Jimbob", "Jim@gmail.com", "09/17/2001");
      testStylist2.Save();

      //Act
      testClient.AddStylist(testStylist1);
      List<Stylist> result = testClient.GetStylists();
      List<Stylist> testList = new List<Stylist> {testStylist1};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }


  }
}
