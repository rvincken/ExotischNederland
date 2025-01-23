namespace ExotischNederland.BusinessLayer;

internal class LocatieService
{
    private readonly DataLayer.LocatieRepository _repository;

    public LocatieService()
    {
        _repository = new DataLayer.LocatieRepository();
    }

    public void RegistreerLocatie
        (string locatienaam, string provincie)
    {
        var locatie = new Model.Locatie
        (
            1, locatienaam, provincie
        );

        _repository.VoegLocatieToe(locatie);
    }

    public List<Model.Locatie> KrijgAlleLocaties()
    {
        return _repository.HaalAlleLocatiesOp();
    }

    public Model.Locatie KrijgLocatieVanId(int id)
    {
        return _repository.HaalLocatieVanIdOp(id);
    }

    public Model.Locatie KrijgLocatieVanLocatieNaam(string locatieNaam)
    {
        return _repository.HaalLocatieVanLocatieNaamOp(locatieNaam);
    }
}