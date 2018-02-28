using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ProjectIthaca.Models;
using ProjectIthaca;
using System;

namespace ProjectIthaca.Tests
{
  [TestClass]
  public class ArtistTests : IDisposable
  {
    public ArtistTests()
    {
      DBConfiguration.ConnectionString =
      "server=localhost;user id=root;password=root;port=8889;database=project_ithaca_test;";
    }

    public void Dispose()
    {
      Artist.DeleteAll();
      Genre.DeleteAll();
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
    public void AddGenre_AddsGenreToArtist_GenreList()
    {
      //Arrange
      Artist testArtist = new Artist
      ("The Strokes", "2001", "A group from New York", true);
      testArtist.Save();

      Genre testGenre = new Genre
      ("Rock", "Evolved from Rock and Roll", "Current");
      testGenre.Save();

      //Act
      testArtist.AddGenre(testGenre);

      List<Genre> result = testArtist.GetGenres();
      List<Genre> testList = new List<Genre>{testGenre};

      //Assert
      CollectionAssert.AreEqual(result,testList);
    }

    [TestMethod]
    public void GetGenres_ReturnsAllArtistGenres_GenreList()
    {
      //Arrange
      Artist testArtist = new Artist
      ("Desert Dwellers", "2011", "An awesome band!", true);
      testArtist.Save();

      Genre testGenre1 = new Genre
      ("EDM", "Electronic Dance Music", "Post-Modern");
      testStylist1.Save();

      Genre testGenre2 = new Genre
      ("Jazz", "Influenced by R&B", "Late 19th Century");
      testStylist2.Save();

      //Act
      testArtist.AddGenre(testGenre1);
      List<Genre> result = testArtist.GetGenres();
      List<Genre> testList = new List<Genre> {testGenre1};

      //Assert
      CollectionAssert.AreEqual(result, testList);
    }


  }
}
