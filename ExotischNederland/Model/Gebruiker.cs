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
            if (value.ToLower() == "m" || value.ToLower() == "v")
            {
                _geslacht = value.ToLower();
            }
            else
            {
                throw new ArgumentException(
                    $"{this}.Geslacht kan alleen 'm'/'v' zijn," +
                    $"maar een waarde van {value} is gegeven.");
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
                _taal = value.ToLower();
            }
            else
            {
                throw new ArgumentException(
                    $"{this}.Taal kan alleen 'nl'/'en' zijn," +
                    $"maar een waarde van {value} is gegeven.");
            }
        }

    }
    public string Email
    {
        get{return _email;}
        set
        {
            if (value.Contains('@')) // Werk dit nog grondiger uit
            {
                _email = value;
            }
            else
            {
                throw new ArgumentException(
                    $"{this}.Email kan alleen een geldig adres zijn," +
                    $"Maar een waarde van {value} is gegeven.");
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

    // Voeg mogelijke ToString() override toe
}
