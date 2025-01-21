using ExotischNederland.Model;
using Microsoft.Data.Sqlite;

namespace ExotischNederland.DataLayer;

internal class FotoRepository
{
    private readonly string _connectionString;

    public FotoRepository()
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

    public void VoegFotoToe(Model.Foto foto)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string insertQuery = @"
            INSERT INTO Foto (Afbeelding)
            VALUES (@Afbeelding);";

        using var command = new SqliteCommand(insertQuery, connection);
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

    public Model.Foto HaalFotoVanIdOp(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string selectQuery = @"
            SELECT * FROM FOto
            WHERE Fid = @Fid;";

        using var command = new SqliteCommand(selectQuery, connection);
        command.Parameters.AddWithValue("@Fid", id);

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            int fid = reader.GetInt32(0);
            byte[] imageBytes = (byte[])reader["Afbeelding"];

            var foto = new Model.Foto(fid, imageBytes);

            return foto;
        }

        throw new ArgumentException
        (
            $"FotoRepository.HaalFotoVanIdOp - Kon geen Foto vinden voor ID: {id}"
        );
    }
}
