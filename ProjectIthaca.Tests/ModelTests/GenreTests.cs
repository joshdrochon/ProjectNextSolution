using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ProjectIthaca.Models;
using ProjectIthaca;
using System;

namespace ProjectIthaca.Tests
{
  [TestClass]
  public class GenreTests : IDisposable
  {
    public GenreTests()
    {
      DBConfiguration.ConnectionString =
      "server=localhost;user id=root;password=root;port=8889;database=project_ithaca_test;";
    }

    public void Dispose()
    {
      Genre.DeleteAll();
      Artist.DeleteAll();
    }




    // [TestMethod]
    // public void GetAll_DatabaseEmptyAtFirst_0()
    // {
    //   //Arrange, Act
    //   int result = Stylist.GetAll().Count;
    //   Console.WriteLine("Line 29 " + result);
    //   //Assert
    //   Assert.AreEqual(0, result);
    // }
    //
    // [TestMethod]
    // public void Save_AssignsIdToObject_Id()
    // {
    //   //Arrange
    //   Stylist testStylist = new Stylist
    //   ("Yoda", "Yoda@gmail.com", "03/14/18");
    //
    //   //Act
    //   testStylist.Save();
    //   Stylist savedStylist = Stylist.GetAll()[0];
    //
    //   int result = savedStylist.GetId();
    //   int testId = testStylist.GetId();
    //
    //   //Assert
    //   Assert.AreEqual(result, testId);
    // }
    //
    //
    // [TestMethod]
    // public void Save_SavesToDataBase_Stylist()
    // {
    //   //Arrange
    //   Stylist testStylist = new Stylist
    //   ("Luke Skywalker", "Skywalker@gmail.com", "03/14/17");
    //   int expectedResult = 1;
    //   //Act
    //   testStylist.Save();
    //   int actualResult = Stylist.GetAll().Count;
    //   //Assert
    //   Assert.AreEqual(expectedResult, actualResult);
    //   Console.WriteLine(expectedResult);
    //   Console.WriteLine(actualResult);
    // }
    //
    // [TestMethod]
    // public void Find_FindsStylestInDatabase_Stylist()
    // {
    //   //Arrange
    //   Stylist testStylist = new Stylist
    //   ("Jack Sparrow", "SparrowMe@gmail.com", "04/12/1999");
    //   testStylist.Save();
    //   //Act
    //   Stylist foundStylist = Stylist.Find(testStylist.GetId());
    //   //Assert
    //   Assert.AreEqual(testStylist, foundStylist);
    // }
    //
    // [TestMethod]
    // public void GetClients_GetsAllClientsWithStylist_ClientList()
    // {
    //   //create a Stylist object
    //   Stylist testStylist = new Stylist
    //   ("Huckleberry Finn", "HuckFinn@gmail.com", "07/07/1939");
    //   testStylist.Save();
    //
    //   //create two Client objects and add them to the testStylist via GetId()
    //   Client firstClient = new Client("Bob", "b1952@uw.edu", "12/25/2049", testStylist.GetId());
    //   firstClient.Save();
    //   Client secondClient = new Client("Sam", "Samwise@gmail.com", "N/A", testStylist.GetId());
    //   secondClient.Save();
    //   //add both Client objects to a list
    //   List<Client> testClientList = new List<Client> {firstClient, secondClient};
    //   //call GetClient() method on teststylist set equal to a list
    //   List<Client> resultClientList = testStylist.GetClients();
    //   //assert both lists are equal and contain 2 client objects
    //   CollectionAssert.AreEqual(testClientList, resultClientList);
    //   // Assert.AreEqual(testClientList.Count, 2);

    [TestMethod]
    public void Test_AddArtist_AddsArtistToGenre()
    {
      //Arrange
      Genre testGenre = new Genre
      ("Hip Hop", "Grab your umbrella for a Lil Wayne", "Current");
      testGenre.Save();

      Artist testArtist = new Artist
      ("Muse", "1999", "An awesome group from UK", true);
      testArtist.Save();

      Artist testArtist2 = new Artist
      ("The Beatles", "1959", "British Invasion anyone?", false);
      testArtist2.Save();

      //Act
      testGenre.AddArtist(testArtist);
      testGenre.AddArtist(testArtist2);

      List<Artist> result = testGenre.GetArtists();
      List<Artist> testList = new List<Artist>{};
      testList.Add(testArtist);
      testList.Add(testArtist2);
      //Assert
      CollectionAssert.AreEqual(result, testList);
    }

    [TestMethod]
    public void GetArtists_ReturnsAllGenreArtists_ArtistList()
    {
      //Arrange
      Genre testGenre = new Genre
      ("DubStep", "Robots making music!", "Post-Modern");
      testGenre.Save();

      Artist testArtist1 = new Artist
      ("Lindsey Stirling", "2007", "Violinst with some good vibes!", true);
      testArtist1.Save();

      Artist testArtist2 = new Artist
      ("The Doors", "1965", "Mix of this and that!", false);
      testArtist2.Save();

      //Act
      testGenre.AddArtist(testArtist1);
      List<Artist> savedArtists = testGenre.GetArtists();
      List<Artist> testList = new List<Artist> {testArtist1};

      //Assert
      CollectionAssert.AreEqual(savedArtists, testList);
    }

  }
}
