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
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool idEquality = this.GetId() == newClient.GetId();

        return (idEquality);

      }
    }

    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client>();
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM client;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        string clientEmail = rdr.GetString(2);
        string clientFirstAppt = rdr.GetString(3);

        Client newClient = new Client
        (clientName, clientEmail, clientFirstAppt, clientId);
        allClients.Add(newClient);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allClients;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;

      cmd.CommandText = @"INSERT INTO client (id, name, email, firstappt)
      VALUES (@clientId, @clientName, @clientEmail, @clientFirstAppt);";

      MySqlParameter id = new MySqlParameter();
      id.ParameterName = "@clientId";
      id.Value = this._id;
      cmd.Parameters.Add(id);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@clientName";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      MySqlParameter email = new MySqlParameter();
      email.ParameterName = "@clientEmail";
      email.Value = this._email;
      cmd.Parameters.Add(email);

      MySqlParameter firstappt = new MySqlParameter();
      firstappt.ParameterName = "@clientFirstAppt";
      firstappt.Value = this._firstappt;
      cmd.Parameters.Add(firstappt);

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
      cmd.CommandText = @"DELETE FROM client;";
      cmd.CommandText = @"DELETE FROM client_stylist;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public static Client Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM client WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int clientId = 0;
      string clientName = "";
      string clientEmail = "";
      string clientFirstAppt = "";

      while(rdr.Read())
      {
        //arguments in rdr methods correspond to index of the table rows
        clientId = rdr.GetInt32(0);
        clientName = rdr.GetString(1);
        clientEmail = rdr.GetString(2);
        clientFirstAppt = rdr.GetString(3);
      }

      Client foundClient = new Client
      (clientName, clientEmail, clientFirstAppt, clientId);

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }

      return foundClient;
    }

    public void AddStylist(Stylist newStylist)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO client_stylist
      (stylist_id, client_id) VALUES (@StylistId, @ClientId);";

      MySqlParameter stylist_id = new MySqlParameter();
      stylist_id.ParameterName = "@StylistId";
      stylist_id.Value = newStylist.GetId();
      cmd.Parameters.Add(stylist_id);

      MySqlParameter client_id = new MySqlParameter();
      client_id.ParameterName = "@ClientId";
      client_id.Value = _id;
      cmd.Parameters.Add(client_id);

      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }

    }

    public List<Stylist> GetStylists()
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT stylist_id FROM client_stylist WHERE client_id = @clientId;";

        MySqlParameter clientIdParameter = new MySqlParameter();
        clientIdParameter.ParameterName = "@clientId";
        clientIdParameter.Value = _id;
        cmd.Parameters.Add(clientIdParameter);

        var rdr = cmd.ExecuteReader() as MySqlDataReader;

        List<int> stylistIds = new List<int> {};
        while(rdr.Read())
        {
            int stylistId = rdr.GetInt32(0);
            stylistIds.Add(stylistId);
        }
        rdr.Dispose(); //can only have one open reader @ at time

        List<Stylist> stylists = new List<Stylist> {};
        foreach (int stylistId in stylistIds)
        {
            var stylistQuery = conn.CreateCommand() as MySqlCommand;
            stylistQuery.CommandText = @"SELECT * FROM stylist WHERE id = @StylistId;";

            MySqlParameter stylistIdParameter = new MySqlParameter();
            stylistIdParameter.ParameterName = "@StylistId";
            stylistIdParameter.Value = stylistId;
            stylistQuery.Parameters.Add(stylistIdParameter);

            var stylistQueryRdr = stylistQuery.ExecuteReader() as MySqlDataReader;
            while(stylistQueryRdr.Read())
            {
                int thisStylistId = stylistQueryRdr.GetInt32(0);
                string stylistName = stylistQueryRdr.GetString(1);
                string stylistEmail = stylistQueryRdr.GetString(2);
                string stylistStartDate = stylistQueryRdr.GetString(3);

                Stylist foundStylist = new Stylist(stylistName, stylistEmail, stylistStartDate, thisStylistId);
                stylists.Add(foundStylist);
            }
            stylistQueryRdr.Dispose();
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return stylists;
    }

  }
}

//note, if non-static we must preface instance with class name to target it
