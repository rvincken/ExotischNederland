using Microsoft.Data.Sqlite;

namespace ExotischNederland.DataLayer;

internal class WaarnemingRepository
{
    private readonly string _connectionString = @"Data Source=C:\Users\rickv\source\repos\ExotischNederland3\ExotischNederland\Database\ExotischNederland.db";

    public WaarnemingRepository()
    {
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        using var connection = new SqliteConnection(_connectionString);
    }

    public void VoegWaarnemingToe(Model.Waarneming waarneming)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string insertQuery = @"
            INSERT INTO Waarneming (Wid, Gid, Fid, Lid, Sid, Omschrijving, Datum, Tijd)
            VALUES (@Wid, @Gid, @Fid, @Lid, @Sid, @Omschrijving, @Datum, @Tijd);";

        using var command = new SqliteCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@Wid", waarneming.Id);
        command.Parameters.AddWithValue("@Gid", waarneming.WaarnemerId);
        command.Parameters.AddWithValue("@Fid", waarneming.FotoId);
        command.Parameters.AddWithValue("@Lid", waarneming.LocatieId);
        command.Parameters.AddWithValue("@Sid", waarneming.SoortId);
        command.Parameters.AddWithValue("@Omschrijving", waarneming.Omschrijving);
        command.Parameters.AddWithValue("@Datum", waarneming.Datum);
        command.Parameters.AddWithValue("@Tijd", waarneming.Tijd);

        command.ExecuteNonQuery();
    }

    public List<Model.Waarneming> HaalAlleWaarnemingenOp()
    {
        var waarnemingen = new List<Model.Waarneming>();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string selectQuery = @"
            SELECT * FROM Waarneming;";

        using var command = new SqliteCommand(selectQuery, connection);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            int waarnemerId = reader.GetInt32(1);
            int fotoId = reader.GetInt32(2);
            int locatieId = reader.GetInt32(3);
            int soortId = reader.GetInt32(4);
            string omschrijving = reader.GetString(5);
            string datum = reader.GetString(6);
            string tijd = reader.GetString(7);

            waarnemingen.Add(new Model.Waarneming
            (
                id,
                waarnemerId,
                fotoId,
                locatieId,
                soortId,
                omschrijving,
                datum,
                tijd
            ));
        }

        return waarnemingen;
    }
}
