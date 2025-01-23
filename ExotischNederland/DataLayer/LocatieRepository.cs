using Microsoft.Data.Sqlite;

namespace ExotischNederland.DataLayer;

internal class LocatieRepository
{
    private readonly string _connectionString;

    public LocatieRepository()
    {
        string baseDirectory = AppContext.BaseDirectory;
        string databasePath = Path.Combine(baseDirectory, "Database", "ExotischNederland.db");

        _connectionString = $"Data Source={databasePath}";

        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        using var connection = new SqliteConnection(_connectionString);
    }

    public void VoegLocatieToe(Model.Locatie locatie)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string insertQuery = @"
            INSERT INTO Locatie (Locatienaam, Provincie)
            VALUES (@Locatienaam, @Provincie);";

        using var command = new SqliteCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@Locatienaam", locatie.Locatienaam);
        command.Parameters.AddWithValue("@Provincie", locatie.Provincie);

        command.ExecuteNonQuery();
    }

    public List<Model.Locatie> HaalAlleLocatiesOp()
    {
        var locaties = new List<Model.Locatie>();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string selectQuery = @"
            SELECT * FROM Locatie;";

        using var command = new SqliteCommand(selectQuery, connection);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            string locatienaam = reader.GetString(1);
            string provincie = reader.GetString(2);
            var locatie = new Model.Locatie(id, locatienaam, provincie);

            locaties.Add(locatie);
        }
        return locaties;
    }

    public Model.Locatie HaalLocatieVanIdOp(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string selectQuery = @"
            SELECT * FROM Locatie
            WHERE Lid = @Lid;";

        using var command = new SqliteCommand(selectQuery, connection);
        command.Parameters.AddWithValue("@Lid", id);

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new Model.Locatie
            (
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2)
            );
        }

        throw new ArgumentException
        (
            $"LocatieRepository.HaalLocatieVanIdOp - Kon geen Locatie vinden voor ID: {id}"
        );
    }

    public Model.Locatie HaalLocatieVanLocatieNaamOp(string locatieNaam)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string selectQuery = @"
            SELECT * FROM Locatie
            WHERE Locatienaam = @Locatienaam;";

        using var command = new SqliteCommand(selectQuery, connection);
        command.Parameters.AddWithValue("@Locatienaam", locatieNaam);

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new Model.Locatie
            (
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2)
            );
        }

        throw new ArgumentException
        (
            $"LocatieRepository.HaalLocatieVanLocatieNaamOp - Kon geen Locatie vinden voor locatieNaam: {locatieNaam}"
        );
    }
}
