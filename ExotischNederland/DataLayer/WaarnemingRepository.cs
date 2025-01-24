using ExotischNederland.Model;
using Microsoft.Data.Sqlite;

namespace ExotischNederland.DataLayer;

internal class WaarnemingRepository
{
    private readonly string _connectionString = @"Data Source=/database/ExotischNederland.db";
    BusinessLayer.FotoService fotoService = new BusinessLayer.FotoService();
    BusinessLayer.GebruikerService gebruikerService = new BusinessLayer.GebruikerService();
    BusinessLayer.LocatieService locatieService = new BusinessLayer.LocatieService();
    BusinessLayer.SoortService soortService = new BusinessLayer.SoortService();

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
            INSERT INTO Waarneming (Gid, Fid, Lid, Sid, Omschrijving, Datum, Tijd)
            VALUES (@Gid, @Fid, @Lid, @Sid, @Omschrijving, @Datum, @Tijd);";

        using var command = new SqliteCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@Gid", waarneming.Waarnemer.Id);
        command.Parameters.AddWithValue("@Fid", waarneming.Foto.Id);
        command.Parameters.AddWithValue("@Lid", waarneming.Locatie.Id);
        command.Parameters.AddWithValue("@Sid", waarneming.Soort.Id);
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

            var waarnemer = gebruikerService.KrijgGebruikerVanId(waarnemerId);
            var foto = fotoService.KrijgFotoVanId(fotoId);
            var locatie = locatieService.KrijgLocatieVanId(locatieId);
            var soort = soortService.KrijgSoortVanId(soortId);

            waarnemingen.Add(new Model.Waarneming
            (
                id,
                foto,
                waarnemer,
                locatie,
                soort,
                omschrijving,
                datum,
                tijd
            ));
        }

        return waarnemingen;
    }

    public Model.Waarneming HaalWaarnemingVanIdOp(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string selectQuery = @"
            SELECT * FROM Waarneming
            WHERE Wid = @Wid;";

        using var command = new SqliteCommand(selectQuery, connection);
        command.Parameters.AddWithValue("@Wid", id);

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            int wid = reader.GetInt32(0);
            int waarnemerId = reader.GetInt32(1);
            int fotoId = reader.GetInt32(2);
            int locatieId = reader.GetInt32(3);
            int soortId = reader.GetInt32(4);
            string omschrijving = reader.GetString(5);
            string datum = reader.GetString(6);
            string tijd = reader.GetString(7);

            var waarnemer = gebruikerService.KrijgGebruikerVanId(waarnemerId);
            var foto = fotoService.KrijgFotoVanId(fotoId);
            var locatie = locatieService.KrijgLocatieVanId(locatieId);
            var soort = soortService.KrijgSoortVanId(soortId);

            var waarneming = new Model.Waarneming
            (
                wid,
                foto,
                waarnemer,
                locatie,
                soort,
                omschrijving,
                datum,
                tijd
            );

            return waarneming;
        }

        throw new ArgumentException
        ($"WaarnemingRepository.HaalWaarnemingVanIdOp - Kon geen Waarneming vinden voor Id: {id}");
    }
}
