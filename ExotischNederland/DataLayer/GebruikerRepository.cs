
using Microsoft.Data.Sqlite;

namespace ExotischNederland.DataLayer;

internal class GebruikerRepository
{
    private readonly string _connectionString = @".../Bestandslocatie/Database.db";

    public GebruikerRepository()
    {
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        using var connection = new SqliteConnection(_connectionString);
    }

    public void VoegGebruikerToe(Model.Gebruiker gebruiker)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string insertQuery = @"
            INSERT INTO Gebruiker (Gid, Rol, Naam, Geslacht, Geboortejaar, Taal, Email, Telefoonnummer, Weergavenaam, Land, Biografie
            VALUES (@Gid, @Rol, @Naam, @Geslacht, @Geboortejaar, @Taal, @Land, @Email, @Telefoonnummer, @Weergavenaam, @Biografie))";
        
        using var command = new SqliteCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@Gid", gebruiker.Id);
        command.Parameters.AddWithValue("@Rol", gebruiker.Rol);
        command.Parameters.AddWithValue("@Naam", gebruiker.Naam);
        command.Parameters.AddWithValue("@Geslacht", gebruiker.Geslacht);
        command.Parameters.AddWithValue("@Geboortejaar", gebruiker.Geboortejaar);
        command.Parameters.AddWithValue("@Taal", gebruiker.Taal);
        command.Parameters.AddWithValue("@Land", gebruiker.Land);
        command.Parameters.AddWithValue("@Email", gebruiker.Email);
        command.Parameters.AddWithValue("@Telefoonnummer", gebruiker.Telefoonnummer);
        command.Parameters.AddWithValue("@Weergavenaam", gebruiker.Weergavenaam);
        command.Parameters.AddWithValue("@Biografie", gebruiker.Biografie);
        
        command.ExecuteNonQuery();
    }

    public List<Model.Gebruiker> GeefAlleGebruikersWeer()
    {
        var gebruikers = new List<Model.Gebruiker>();
        
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string selectQuery = @"
            SELECT * FROM Gebruiker;";
        
        using var command = new SqliteCommand(selectQuery, connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            int gid = reader.GetInt32(0);
            string rol = reader.GetString(1);
            string naam = reader.GetString(2);
            char geslacht = reader.GetChar(3);
            int geboortejaar = reader.GetInt32(4);
            string taal = reader.GetString(5);
            string land = reader.GetString(6);
            string email = reader.GetString(7);
            long telefoonnummer = reader.GetInt64(8);
            string weergavenaam = reader.GetString(9);
            string biografie = reader.GetString(10);
            
            gebruikers.Add(new Model.Gebruiker(gid, rol, naam, taal, geboortejaar, land, email, telefoonnummer, weergavenaam, geslacht, biografie)); 
        }

        return gebruikers;
    }
}