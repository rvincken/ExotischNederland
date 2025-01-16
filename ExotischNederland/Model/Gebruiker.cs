namespace ExotischNederland.Model;

// implementeer:
// weergavenaam, naam, email, biografie, taal, geslacht, geboortejaar, telefoonnummer, land
internal class Gebruiker
{
    private string _naam;
    private string? _geslacht;
    private string _taal;
    private string _email;
    public string Naam
    {
        get{return _naam;}
    }

    //keuze geslachten (niet verplicht)
    public string? Geslacht
    {
        get{return _geslacht;}
        set
        {
            if (value == "m" || value == "v")
            {
                _geslacht = value;
            }
            else
            {
                throw new ArgumentException("geslacht is alleen m/v");
            }
        }
    }
    //keuzen uit talen, kunnen altijd meer worden toegevoegd 
    public string Taal
    {
        get{return _taal;}
        set
        {
            if (value.ToLower() == "nl" || value.ToLower() == "en")
            {
                _taal = value;
            }
            else
            {
                throw new ArgumentException("taal wordt niet ondersteund");
            }
        }

    }
    public string Email
    {
        get{return _email;}
        set
        {
            if (value.Contains('@'))
            {
                _email = value;
            }
            else
            {
                throw new ArgumentException("ongeldig email adress");
            }

        }
    }

    public string Weergavenaam {get;set;}

    public string? Biografie {get;set;}
    public string Geboortejaar {get;set;}
    public int Telefoonnummer {get;set;}
    public string Land {get;set;}
    
    //constructor
    public Gebruiker(string naam, string taal, string geboortejaar, string land, string email, int telefoonnummer, string weergavenaam)
    {
        _naam = naam;
        Taal = taal;
        Geboortejaar = geboortejaar;
        Land = land;
        Email = email;
        Telefoonnummer = telefoonnummer;
        Weergavenaam = weergavenaam;


        

    }




    public void WijzigNaam(string nieuweNaam)
    {
        _naam = nieuweNaam;
    }

    //niet verplichten velden invullen.

    public void SetGeslacht(string geslacht)
    {
        Geslacht = geslacht;
    }
    public void SetBiografie(string nieuweBiografie)
    {
        Biografie = nieuweBiografie;
    }


    
}
