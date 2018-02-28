using System.Collections.Generic;
using MySql.Data.MySqlClient;
using ProjectIthaca;
using System;

namespace ProjectIthaca.Models
{
  public class Artist
  {
    private int _id;
    private string _name;
    private string _debut;
    private string _description;
    private bool _active;

    public Artist(string Name, string Debut, string Description, bool Active, int Id=0)
    {
      this._id = Id;
      this._name = Name;
      this._debut = Debut;
      this._description = Description;
      this._active = Active;
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

    //_debut getter/setter
    public string GetDebut()
    {
      return _debut;
    }
    public void SetDebut(string newDebut)
    {
      _debut = newDebut;
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

    //_active setter/getter
    public bool GetActive()
    {
      return _active
    }
    public void SetActive(bool newActive)
    {
      _active = newActive
    }

    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Artist))
      {
        return false;
      }
      else
      {
        Artist newClient = (Artist) otherArtist;
        bool idEquality = this.GetId() == newArtist.GetId();

        return (idEquality);

      }
    }

    public static List<Artist> GetAll()
    {
      List<Artist> allArtists = new List<Artist>();
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM artists;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int artistId = rdr.GetInt32(0);
        string artistName = rdr.GetString(1);
        string artistDebut = rdr.GetString(2);
        string artistDescription = rdr.GetString(3);
        bool artistActive = rdr.GetBool(4);

        Artist newArtist = new Artist
        (artistName, artistDebut, artistDescription, artistActive, artistId);
        allClients.Add(newArtist);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allArtists;
    }

    public List<Genre> GetGenres()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT genre_id FROM genres_artits WHERE artist_id = @artisttId;";

      MySqlParameter artistIdParameter = new MySqlParameter();
      artistIdParameter.ParameterName = "@artistId";
      artistIdParameter.Value = _id;
      cmd.Parameters.Add(artistIdParameter);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      List<int> genreIds = new List<int> {};
      while(rdr.Read())
      {
        int genreId = rdr.GetInt32(0);
        genreIds.Add(genreId);
      }
      rdr.Dispose(); //can only have one open reader @ a time

      List<Genre> genres = new List<Genre> {};
      foreach (int genreId in genreIds)
      {
        var genreQuery = conn.CreateCommand() as MySqlCommand;
        genreQuery.CommandText = @"SELECT * FROM genre WHERE id = @GenreId;";

        MySqlParameter genreIdParameter = new MySqlParameter();
        genreIdParameter.ParameterName = "@GenreId";
        genreIdParameter.Value = genreId;
        genreQuery.Parameters.Add(genreIdParameter);

        var genreQueryRdr = genreQuery.ExecuteReader() as MySqlDataReader;
        while(genreQueryRdr.Read())
        {
          int thisGenreId = genreQueryRdr.GetInt32(0);
          string genreName = genreQueryRdr.GetString(1);
          string genreDescription = genreQueryRdr.GetString(2);
          string genreEra = genreQueryRdr.GetString(3);

          Genre foundGenre = new Genre(genreName, genreDescription, genreEra, thisGenreId);
          stylists.Add(foundGenre);
        }
        genreQueryRdr.Dispose();
      }
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return genres;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;

      cmd.CommandText = @"INSERT INTO artists (id, name, debut, description, active)
      VALUES (@artistId, @artistName, @AristDebut, @artistDescription, @artistActive);";

      MySqlParameter id = new MySqlParameter();
      id.ParameterName = "@artistId";
      id.Value = this._id;
      cmd.Parameters.Add(id);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@artistName";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      MySqlParameter debut = new MySqlParameter();
      debut.ParameterName = "@artistDebut";
      debut.Value = this._debut;
      cmd.Parameters.Add(debut);

      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@artistDescription";
      description.Value = this._description;
      cmd.Parameters.Add(description);

      MySqlParameter active = new MySqlParameter();
      active.ParameterName = "@artistActive";
      active.Value = this._active;
      cmd.Parameters.Add(active);


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
      cmd.CommandText = @"DELETE FROM artist;";
      cmd.CommandText = @"DELETE FROM genre_artists;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public static Artist Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM artists WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int artistId = 0;
      string artistName = "";
      string artistDescription = "";
      string artistDebut = "";
      bool artistActive = null;

      while(rdr.Read())
      {
        //arguments in rdr methods correspond to index of the table rows
        artistId = rdr.GetInt32(0);
        artistName = rdr.GetString(1);
        artistDebut = rdr.GetString(2);
        artistDescription = rdr.GetString(3);
        artistActive = rdr.GetBool(4);
      }

      Artist foundArtist = new Artist
      (artistName, artistDebut, artistDescription, artistActive, artistId);

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }

      return foundArtist;
    }

    public void AddGenre(Genre newGenre)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO genres_artists
      (genre_id, artist_id) VALUES (@GenreId, @ArtistId);";

      MySqlParameter genre_id = new MySqlParameter();
      genre_id.ParameterName = "@GenreId";
      genre_id.Value = newGenre.GetId();
      cmd.Parameters.Add(genre_id);

      MySqlParameter artist_id = new MySqlParameter();
      artist_id.ParameterName = "@ArtistId";
      artist_id.Value = _id;
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

//note, if non-static we must preface instance with class name to target it
