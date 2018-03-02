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

    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Genre.GetAll().Count;
      Console.WriteLine("Line 29 " + result);
      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      Genre testGenre = new Genre
      ("Rock", "Influenced by R&B", "20th Century");

      //Act
      testGenre.Save();
      Genre savedGenre = Genre.GetAll()[0];

      int result = testGenre.GetId();
      int testId = savedGenre.GetId();

      //Assert
      Assert.AreEqual(result, testId);
    }


    [TestMethod]
    public void Save_SavesToDataBase_Genre()
    {
      //Arrange
      Genre testGenre = new Genre
      ("Jazz", "Originates from US", "Roaring 20s");
      int expectedResult = 1;
      //Act
      testGenre.Save();
      int actualResult = Genre.GetAll().Count;
      //Assert
      Assert.AreEqual(expectedResult, actualResult);

      Console.WriteLine(expectedResult);
      Console.WriteLine(actualResult);
    }

    [TestMethod]
    public void Find_FindsGenretInDatabase_Genre()
    {
      //Arrange
      Genre testGenre = new Genre
      ("Hip Hop", "Let's get down, yo!", "post-modern");
      testGenre.Save();
      //Act
      Genre foundGenre = Genre.Find(testGenre.GetId());
      //Assert
      Assert.AreEqual(testGenre, foundGenre);
    }

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
