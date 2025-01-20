using Microsoft.Data.Sqlite;

namespace ExotischNederland.DataLayer;

internal class SoortRepository
{
    private readonly string _connectionString = @"../Database/ExotischNederland.db";

    public SoortRepository()
    {
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
            INSERT INTO Soort (Sid, WetenschappelijkeNaam, SoortNaam, Type, Categorie, HoeVaakVoorkomt, Oorsprong
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
}
