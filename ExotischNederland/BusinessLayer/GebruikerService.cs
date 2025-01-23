using ExotischNederland.DataLayer;

namespace ExotischNederland.BusinessLayer;

internal class GebruikerService
{
    private readonly GebruikerRepository _repository;


    public GebruikerService()
    {
        _repository = new GebruikerRepository();
    }

    public void RegistreerGebruiker
    (string rol, string naam, string taal, int geboortejaar, string land, string email, string telefoonnummer,
        string weergavenaam, char geslacht, string biografie)
    {
        var gebruiker = new Model.Gebruiker
        (
            1, rol, naam, taal, geboortejaar, land, email, 
            telefoonnummer, weergavenaam, geslacht, biografie
        );
        
        _repository.VoegGebruikerToe(gebruiker);
    }


    public List<Model.Gebruiker> KrijgAlleGebruikers()
    {
        return _repository.HaalAlleGebruikersOp();
    }

    public Model.Gebruiker KrijgGebruikerVanId(int id)
    {
        return _repository.HaalGebruikerVanIdOp(id);
    }
}