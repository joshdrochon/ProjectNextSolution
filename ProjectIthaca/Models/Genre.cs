using System.Collections.Generic;
using MySql.Data.MySqlClient;
using ProjectIthaca;
using System;

namespace ProjectIthaca.Models
{
  public class Genre
  {
    private int _id;
    private string _name;
    private string _description;
    private string _era;


    public Genre(string Name, string Description, string Era, int Id=0)
    {
      this._id = Id;
      this._name = Name;
      this._description = Description;
      this._era= Era;
    }

    //_id getter/setter
    public int GetId()
    {
      return _id;
    }
    public void SetId(int newId)
    {
      _id = newId;
    }

    //_name getter/setter
    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }

    //_description getter/setter
    public string GetDescription()
    {
      return _description;
    }
    public void SetDescription(string newDescription)
    {
      _description = newDescription;
    }

    //_era getter/setter
    public string GetEra()
    {
      return _era;
    }
    public void SetEra(string newEra)
    {
      _era = newEra;
    }

    public override bool Equals(System.Object otherGenre)
    {
      if (!(otherGenre is Genre))
      {
        return false;
      }
      else
      {
        Genre newGenre = (Genre) otherGenre;
        bool idEquality = (this.GetId() == newGenre.GetId());

        return (idEquality);
      }
    }

    public static List<Genre> GetAll()
    {
      List<Genre> allGenres = new List<Genre>();
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM genres;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int genreId = rdr.GetInt32(0);
        string genreName = rdr.GetString(1);
        string genreDescription = rdr.GetString(2);
        string genreEra = rdr.GetString(3);

        Genre newGenre = new Genre
        (genreName, genreDescription, genreEra, genreId);

        allGenres.Add(newGenre);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allGenres;
    }

    public List<Artist> GetArtists()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT artist_id FROM genres_artists WHERE genre_id = @genreId;";

      MySqlParameter genreIdParameter = new MySqlParameter();
      genreIdParameter.ParameterName = "@GenreId";
      genreIdParameter.Value = _id;
      cmd.Parameters.Add(genreIdParameter);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      List<int> artistIds = new List<int> {};
      while(rdr.Read())
      {
          int artistId = rdr.GetInt32(0);
          artistIds.Add(artistId);
      }
      rdr.Dispose();

      List<Artist> artists = new List<Artist> {};
      foreach (int artistId in artistIds)
      {
        var artistQuery = conn.CreateCommand() as MySqlCommand;
        artistQuery.CommandText = @"SELECT * FROM artists WHERE id = @ArtistId;";

        MySqlParameter artistIdParameter = new MySqlParameter();
        artistIdParameter.ParameterName = "@ArtistId";
        artistIdParameter.Value = artistId;
        artistQuery.Parameters.Add(artistIdParameter);

        var artistQueryRdr = artistQuery.ExecuteReader() as MySqlDataReader;
        while(artistQueryRdr.Read())
        {
          int thisArtistId = artistQueryRdr.GetInt32(0);
          string artistName = artistQueryRdr.GetString(1);
          string artistDebut = artistQueryRdr.GetString(2);
          string artistDescription = artistQueryRdr.GetString(3);
          bool artistActive = artistQueryRdr.GetBoolean(4);

          Artist foundArtist = new Artist(artistName, artistDebut, artistDescription, artistActive, thisArtistId);
          artists.Add(foundArtist);
        }

        artistQueryRdr.Dispose();

      }

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return artists;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;

      cmd.CommandText = @"INSERT INTO genres (id, name, description, era)
      VALUES (@genreId, @genreName, @genreDescription, @genreEra);";

      MySqlParameter id = new MySqlParameter();
      id.ParameterName = "@genreId";
      id.Value = this._id;
      cmd.Parameters.Add(id);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@genreName";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      MySqlParameter description= new MySqlParameter();
      description.ParameterName = "@genreDescription";
      description.Value = this._description;
      cmd.Parameters.Add(description);

      MySqlParameter era = new MySqlParameter();
      era.ParameterName = "@genreEra";
      era.Value = this._era;
      cmd.Parameters.Add(era);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM genres;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public static Genre Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM genres WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int genreId = 0;
      string genreName = "";
      string genreDescription = "";
      string genreEra = "";


      while(rdr.Read())
      {
        //arguments in rdr methods correspond to index of the table rows
        genreId = rdr.GetInt32(0);
        genreName = rdr.GetString(1);
        genreDescription = rdr.GetString(2);
        genreEra = rdr.GetString(3);
      }

      Genre foundGenre = new Genre
      (genreName, genreDescription, genreEra, genreId);

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }

      return foundGenre;
    }

    public void AddArtist(Artist newArtist)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO genres_artists
      (genre_id, artist_id) VALUES (@GenreId, @ArtistId);";

      MySqlParameter genre_id = new MySqlParameter();
      genre_id.ParameterName = "@GenreId";
      genre_id.Value = this._id;
      cmd.Parameters.Add(genre_id);

      MySqlParameter artist_id = new MySqlParameter();
      artist_id.ParameterName = "@ArtistId";
      artist_id.Value = newArtist.GetId();
      cmd.Parameters.Add(artist_id);

      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

  }
}
