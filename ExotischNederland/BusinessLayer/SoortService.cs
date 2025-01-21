namespace ExotischNederland.BusinessLayer;

internal class SoortService
{
    private readonly DataLayer.SoortRepository _repository;

    public SoortService()
    {
        _repository = new DataLayer.SoortRepository();
    }

    public void RegistreerSoort(
        int id, string wetenschappelijkeNaam, string soortNaam,
        string type, string categorie, char oorsprong)
    {
        var soort = new Model.Soort
        (
            id,
            wetenschappelijkeNaam,
            soortNaam,
            type,
            categorie,
            oorsprong
        );

        _repository.VoegSoortToe(soort);
    }

    public List<Model.Soort> KrijgAlleSoorten()
    {
        return _repository.HaalAlleSoortenOp();
    }

    public Model.Soort KrijgSoortVanId(int id)
    {
        return _repository.HaalSoortVanIdOp(id);
    }

    public Model.Soort KrijgSoortVanWetenschapNaam(string wetenschapNaam)
    {
        return _repository.HaalSoortVanWetenschapNaamOp(wetenschapNaam);
    }
}
