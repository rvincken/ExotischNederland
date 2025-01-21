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
    (int id, string rol, string naam, string taal, int geboortejaar, string land, string email, string telefoonnummer,
        string weergavenaam, char geslacht, string biografie)
    {
        var gebruiker = new Model.Gebruiker
        (
            id, rol, naam, taal, geboortejaar, land, email, 
            telefoonnummer, weergavenaam, geslacht, biografie
        );
        
        _repository.VoegGebruikerToe(gebruiker);
    }


    public List<Model.Gebruiker> KrijgAlleGebruikers()
    {
        return _repository.HaalAlleGebruikersOp();
    }
}