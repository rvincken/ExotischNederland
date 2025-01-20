namespace ExotischNederland.Model;
internal class Gebruiker
{
    private string _naam;
    private char _geslacht;
    private string _taal;
    private string _email;
    private string _weergavenaam;
    private int _id;
    private string _biografie;
    private int _geboortejaar;
    private long _telefoonnummer;
    private string _land;
    private string _rol;
    
    
    public string Naam
    {
        get{return _naam;}
        set
        {
            if (value.Length > 2 && value.Length < 40)
            {
                _naam = value;
            }
            else
            {
                throw new ArgumentException("Gebruiker.Naam moet minstens 2 tekens zijn, " + 
                "de ingevoerde naam is 2 of minder tekens");
            }
        }
    }
    public char Geslacht
    {
        get{return _geslacht;}
        set
        {
            value = char.ToLower(value);
            if (value == 'm' || value == 'v')
            {
                _geslacht = value;
            }
            else
            {
                throw new ArgumentException(
                    $"Gebruiker.Geslacht kan alleen 'm'/'v' zijn, " +
                    $"maar een waarde van {value} is gegeven.");
            }
        }
    }
    public string Taal
    {
        get{return _taal;}
        set
        {
            value = value.ToLower();
            if (value == "nl" || value == "en")
            {
                _taal = value;
            }
            else
            {
                throw new ArgumentException(
                    $"Gebruiker.Taal kan alleen 'nl'/'en' zijn, " +
                    $"maar een waarde van {value} is gegeven.");
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
                throw new ArgumentException(
                    $"Gebruiker.Email kan alleen een geldig adres zijn, " +
                    $"maar een waarde van {value} is gegeven.");
            }

        }
    }

    public string Weergavenaam 
    {get{return _weergavenaam;}
    set
        {
            if (value.Length <= 20)
                {
                    _weergavenaam = value;

                }
            else
            {
                throw new ArgumentException(
                $"Gebruiker.Weergavenaam kan maximaal 20 tekens zijn, " +    
                $"maar de weergavenaam is {value.Length} tekens lang");
            }
        }
    }
    public string Biografie 
    {
        get{return _biografie;}
        set
        {
            if (value.Length <= 200)
            {
                _biografie = value;
            }
            else
            {
                throw new ArgumentException(
                    $"Gebruiker.Biografie kan maximaal 200 tekens zijn, " +
                    $"maar de biografie is {value.Length} tekens lang.");
            }
        }
    }
    public int Geboortejaar
    {
        get {return _geboortejaar;}
        set
        {
            if (value <= DateTime.Now.Year)
            {
                _geboortejaar = value;
            }
            else
            {
                throw new ArgumentException 
                (
                    $"Gebruiker.geboortenjaar is in de toekomst"
                );
            }

        }
    }
    public long Telefoonnummer 
    {
        get{return _telefoonnummer;}
        set
        {
            if (value.ToString().Length == 10)
            {
                _telefoonnummer = value;
            }
            else
            {
                throw new ArgumentException("Gebruiker.Telefoonnummer is geen geldig telefoonnummer");
            }
        }
    }
    public string Land 
    {
        get{return _land;}
        set
        {
            _land = value;
        }
    }
    public int Id
    {
        get{return _id;}
        set
        {
            if (value.ToString().Length == 7)
            {
                _id = value;
            }
            else
            {
                throw new ArgumentException("Gebruiker.Id moet 7 nummers zijn, ingevoerde waarden is meer/minder");
            }

        }
    }

    public string Rol
    {
        get{return _rol;}
        set
        {
            if (value == "vrijwilliger" || value == "medewerker")
            {
                _rol = value;
            }
                
        }
    }
    public Gebruiker
        (
            int id, string rol, string naam, string taal, 
            int geboortejaar, string land, string email, long telefoonnummer, 
            string weergavenaam, char geslacht, string biografie
        )
    {
        Id = id;
        Rol = rol;
        Naam = naam;
        Taal = taal;
        Geboortejaar = geboortejaar;
        Land = land;
        Email = email;
        Telefoonnummer = telefoonnummer;
        Weergavenaam = weergavenaam;
        Geslacht = geslacht;
        Biografie = biografie;
    }
    public override string ToString()
    {
        return $"""
        ID: {Id}
        Rol: {Rol}
        Naam: {Naam}
        Geslacht: {Geslacht}
        Geboortejaar: {Geboortejaar}
        Taal: {Taal}
        Email: {Email}
        Telefoonnummer: {Telefoonnummer}
        Weergavenaam: {Weergavenaam}
        Land: {Land}
        Biografie: {Biografie}
        """;

    }
}