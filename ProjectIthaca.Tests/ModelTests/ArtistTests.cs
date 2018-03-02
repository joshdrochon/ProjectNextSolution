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

    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Artist.GetAll().Count;
      Console.WriteLine("In line 29 in ArtistTests there are " + result + " in the database.");
      //Assert
      Assert.AreEqual(0, result);
    }


    [TestMethod]
    public void Save_SavesArtistToDataBase_Client()
    {
      //Arrange
      Artist testArtist = new Artist
      ("The Strokes", "2001", "A group from NYC", true);
      int expectedResult = 1; //one client expected in database
      //Act
      testArtist.Save();
      int actualResult = Artist.GetAll().Count;
      //Assert
      Assert.AreEqual(expectedResult, actualResult);
      Console.WriteLine(expectedResult);
      Console.WriteLine(actualResult);
    }


    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      Artist testArtist = new Artist
      ("The Beatles", "1960", "British invasion, bro!", false);

      //Act
      testArtist.Save();
      Artist savedArtist = Artist.GetAll()[0];

      int result = savedArtist.GetId();
      int testId = testArtist.GetId();

      //Assert
      Assert.AreEqual(result, testId);
    }

    [TestMethod]
    public void Find_FindsArtistInDatabase_Artist()
    {
      //Arrange
      Artist testArtist = new Artist
      ("Desert Dwellers", "2011", "Damn good music!", true);
      testArtist.Save();
      //Act
      Artist foundArtist = Artist.Find(testArtist.GetId());
      //Assert
      Console.WriteLine("TEST CLIENT ID IS: " + testArtist.GetId());
      Console.WriteLine("FOUND CLIENT IS IS: " + foundArtist.GetId());

      Assert.AreEqual(testArtist, foundArtist);
    }

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
      testGenre1.Save();

      Genre testGenre2 = new Genre
      ("Jazz", "Influenced by R&B", "Late 19th Century");
      testGenre2.Save();

      //Act
      testArtist.AddGenre(testGenre1);
      List<Genre> result = testArtist.GetGenres();
      List<Genre> testList = new List<Genre> {testGenre1};

      //Assert
      CollectionAssert.AreEqual(result, testList);
    }


  }
}
