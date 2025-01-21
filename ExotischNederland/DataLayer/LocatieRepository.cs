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
            INSERT INTO Locatie (Lid, Locatienaam, Provincie, Breedtegraad, Lengtegraad)
            VALUES (@Lid, @Locatienaam, @Provincie, @Breedtegraad, @Lengtegraad);";

        using var command = new SqliteCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@Lid", locatie.Id);
        command.Parameters.AddWithValue("@Locatienaam", locatie.Locatienaam);
        command.Parameters.AddWithValue("@Provincie", locatie.Provincie);
        command.Parameters.AddWithValue("@Breedtegraad", locatie.Breedtegraad);
        command.Parameters.AddWithValue("@Lengtegraad", locatie.Lengtegraad);

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
            double breedtegraad = reader.GetDouble(3);
            double lengtegraad = reader.GetDouble(4);
            var locatie = new Model.Locatie(id, locatienaam, provincie, breedtegraad, lengtegraad);

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
                reader.GetString(2),
                reader.GetDouble(3),
                reader.GetDouble(4)
            );
        }

        throw new ArgumentException
        (
            $"LocatieRepository.HaalLocatieVanIdOp - Kon geen Locatie vinden voor ID: {id}"
        );
    }
}
