using Microsoft.Data.Sqlite;

namespace ExotischNederland.DataLayer;

internal class LocatieRepository
{
    private readonly string _connectionString = @"../Database/ExotischNederland.db";

    public LocatieRepository()
    {
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
            INSERT INTO Locatie (Lid, Locatienaam, Provincie, Breedtegraad, Lengtegraadt)
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
}
