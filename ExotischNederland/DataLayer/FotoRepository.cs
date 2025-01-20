using Microsoft.Data.Sqlite;

namespace ExotischNederland.DataLayer;

internal class FotoRepository
{
    private readonly string _connectionString = @"Data Source=C:\Users\rickv\source\repos\ExotischNederland3\ExotischNederland\Database\ExotischNederland.db";

    public FotoRepository()
    {
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        using var connection = new SqliteConnection(_connectionString);
    }

    public void VoegFotoToe(Model.Foto foto)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string insertQuery = @"
            INSERT INTO Foto (Fid, Afbeelding)
            VALUES (@Fid, @Afbeelding);";

        using var command = new SqliteCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@Fid", foto.Id);
        command.Parameters.AddWithValue("@Afbeelding", foto.Afbeelding);

        command.ExecuteNonQuery();
    }

    public List<Model.Foto> HaalAlleFotosOp()
    {
        var fotos = new List<Model.Foto>();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string selectQuery = @"
            SELECT * FROM Foto;";

        using var command = new SqliteCommand(selectQuery, connection);

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            byte[] imageBytes = (byte[])reader["Afbeelding"];

            fotos.Add(new Model.Foto(id, imageBytes));
        }

        return fotos;
    }
}
