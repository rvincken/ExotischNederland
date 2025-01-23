namespace ExotischNederland.BusinessLayer;

internal class WaarnemingService
{
    private readonly DataLayer.WaarnemingRepository _repository;

    public WaarnemingService()
    {
        _repository = new DataLayer.WaarnemingRepository();
    }

    public void RegistreerWaarneming
        (int id, int waarnemerId, int fotoId, int locatieId, int soortId,
        string omschrijving, string datum, string tijd)
    {
        var waarneming = new Model.Waarneming
        (   
            id, waarnemerId, fotoId, locatieId, soortId,
            omschrijving, datum, tijd
        );

        _repository.VoegWaarnemingToe(waarneming);
    }

    public List<Model.Waarneming> KrijgAlleWaarnemingen()
    {
        return _repository.HaalAlleWaarnemingenOp();
    }

    public List<Model.Waarneming> FilterWaarneming(List<Model.Soort>soorten)
    {
        return _repository.FilterWaarnemingen(soorten);
    }
}
