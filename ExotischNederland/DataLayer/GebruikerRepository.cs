
using System.Data;
using ExotischNederland.Model;
using Microsoft.Data.Sqlite;

namespace ExotischNederland.DataLayer;

internal class GebruikerRepository
{
    private readonly string _connectionString = @"Data Source=/database/ExotischNederland.db";

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
        INSERT INTO Gebruiker (Rol, Naam, Geslacht, Geboortejaar, Taal, Land, Email, Telefoonnummer, Weergavenaam, Biografie)
        VALUES (@Rol, @Naam, @Geslacht, @Geboortejaar, @Taal, @Land, @Email, @Telefoonnummer, @Weergavenaam, @Biografie);";

        using var command = new SqliteCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@Rol", gebruiker.Rol);
        command.Parameters.AddWithValue("@Naam", gebruiker.Naam);
        command.Parameters.AddWithValue("@Geslacht", gebruiker.Geslacht.ToString());
        command.Parameters.AddWithValue("@Geboortejaar", gebruiker.Geboortejaar);
        command.Parameters.AddWithValue("@Taal", gebruiker.Taal);
        command.Parameters.AddWithValue("@Land", gebruiker.Land);
        command.Parameters.AddWithValue("@Email", gebruiker.Email);
        command.Parameters.AddWithValue("@Telefoonnummer", gebruiker.Telefoonnummer);
        command.Parameters.AddWithValue("@Weergavenaam", gebruiker.Weergavenaam);
        command.Parameters.AddWithValue("@Biografie", gebruiker.Biografie);

        command.ExecuteNonQuery();
    }

    public List<Model.Gebruiker> HaalAlleGebruikersOp()
    {
        var gebruikers = new List<Model.Gebruiker>();
        
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string selectQuery = @"
        SELECT 
            Gid,
            Rol,
            Naam,
            Geslacht,
            Geboortejaar,
            Taal,
            Land,
            Email,
            Telefoonnummer,
            Weergavenaam,
            Biografie
        FROM Gebruiker;";

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
            string telefoonnummer = reader.GetString(8);
            string weergavenaam = reader.GetString(9);
            string biografie = reader.GetString(10);
            
            gebruikers.Add(new Model.Gebruiker(gid, rol, naam, taal, geboortejaar, land, email, telefoonnummer, weergavenaam, geslacht, biografie)); 
        }

        return gebruikers;
    }

    public Model.Gebruiker HaalGebruikerVanIdOp(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string selectQuery = @"
            SELECT * FROM Gebruiker
            WHERE Gid = @Gid;";

        using var command = new SqliteCommand(selectQuery, connection);
        command.Parameters.AddWithValue("@Gid", id);

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            int gid = reader.GetInt32(reader.GetOrdinal("Gid"));
            string rol = reader.GetString(reader.GetOrdinal("Rol"));
            string naam = reader.GetString(reader.GetOrdinal("Naam"));
            char geslacht = reader.GetChar(reader.GetOrdinal("Geslacht"));
            int geboortejaar = reader.GetInt32(reader.GetOrdinal("Geboortejaar"));
            string taal = reader.GetString(reader.GetOrdinal("Taal"));
            string land = reader.GetString(reader.GetOrdinal("Land"));
            string email = reader.GetString(reader.GetOrdinal("Email"));
            string telefoonnummer = reader.GetString(reader.GetOrdinal("Telefoonnummer"));
            string weergavenaam = reader.GetString(reader.GetOrdinal("Weergavenaam"));
            string biografie = reader.GetString(reader.GetOrdinal("Biografie"));

            var gebruiker = new Model.Gebruiker(gid, rol, naam, taal, geboortejaar, land, email, telefoonnummer, weergavenaam, geslacht, biografie);

            return gebruiker;
        }

        throw new ArgumentException
        (
            $"GebruikerRepository.HaalGebruikerVanIdOp - Kon geen Gebruiker vinden voor ID: {id}"
        );
    }
}