namespace ExotischNederland.BusinessLayer;

internal class WaarnemingService
{
    private readonly DataLayer.WaarnemingRepository _repository;

    public WaarnemingService()
    {
        _repository = new DataLayer.WaarnemingRepository();
    }

    public void RegistreerWaarneming
        (Model.Gebruiker waarnemer, Model.Foto foto, Model.Locatie locatie, Model.Soort soort,
        string omschrijving, string datum, string tijd)
    {
        var waarneming = new Model.Waarneming
        (   
            1, foto, waarnemer, locatie, soort,
            omschrijving, datum, tijd
        );

        _repository.VoegWaarnemingToe(waarneming);
    }

    public List<Model.Waarneming> KrijgAlleWaarnemingen()
    {
        return _repository.HaalAlleWaarnemingenOp();
    }

    public Model.Waarneming KrijgWaarnemingVanId(int id)
    {
        return _repository.HaalWaarnemingVanIdOp(id);
    }
}
