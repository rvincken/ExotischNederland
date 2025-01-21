using Microsoft.Data.Sqlite;

namespace ExotischNederland.DataLayer;

internal class SoortRepository
{
    private readonly string _connectionString;

    public SoortRepository()
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

    public void VoegSoortToe(Model.Soort soort)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string insertQuery = @"
        INSERT INTO Soort (Sid, WetenschappelijkeNaam, SoortNaam, Type, Categorie, HoeVaakVoorkomt, Oorsprong)
        VALUES (@Sid, @WetenschappelijkeNaam, @SoortNaam, @Type, @Categorie, @HoeVaakVoorkomt, @Oorsprong);";

        using var command = new SqliteCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@Sid", soort.Id);
        command.Parameters.AddWithValue("@WetenschappelijkeNaam", soort.WetenschappelijkeNaam);
        command.Parameters.AddWithValue("@SoortNaam", soort.SoortNaam);
        command.Parameters.AddWithValue("@Type", soort.Type);
        command.Parameters.AddWithValue("@Categorie", soort.Categorie);
        command.Parameters.AddWithValue("@HoeVaakVoorkomt", soort.HoeVaakDeSoortVoorkomt);
        command.Parameters.AddWithValue("@Oorsprong", soort.Oorsprong);

        command.ExecuteNonQuery();
    }

    public List<Model.Soort> HaalAlleSoortenOp()
    {
        var soorten = new List<Model.Soort>();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string selectQuery = @"
        SELECT * FROM Soort;";

        using var command = new SqliteCommand(selectQuery, connection);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            string wetenschappelijkeNaam = reader.GetString(1);
            string soortNaam = reader.GetString(2);
            string type = reader.GetString(3);
            string categorie = reader.GetString(4);
            int hoeVaakDeSoortVoorkomt = reader.GetInt32(5);
            char oorsprong = reader.GetChar(6);

            var soort = new Model.Soort
            (
                id,
                wetenschappelijkeNaam,
                soortNaam,
                type,
                categorie,
                hoeVaakDeSoortVoorkomt,
                oorsprong
            );

            soorten.Add(soort);
        }

        return soorten;
    }

    public Model.Soort HaalSoortVanIdOp(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string selectQuery = @"
            SELECT * FROM Soort
            WHERE Sid = @Sid;";

        using var command = new SqliteCommand(selectQuery, connection);
        command.Parameters.AddWithValue("@Sid", id);

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new Model.Soort
            (
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetString(3),
                reader.GetString(4),
                reader.GetInt32(5),
                reader.GetChar(6)
            );
        }

        throw new ArgumentException
        (
            $"SoortRepository.HaalSoortVanIdOp - Kon geen Soort vinden voor ID: {id}"
        );
    }
}
