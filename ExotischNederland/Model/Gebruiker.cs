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
    private string _telefoonnummer;
    private string _land;
    private string _rol;
    
    public string Naam
    {
        get{return _naam;}
        private set
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
        private set
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
        private set
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
        private set
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
    private set
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
        private set
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
        private set
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
    public string Telefoonnummer 
    {
        get{return _telefoonnummer;}
        private set
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
        private set
        {
            _land = value;
        }
    }
    public int Id
    {
        get{return _id;}
        private set { _id = value; }
    }

    public string Rol
    {
        get{return _rol;}
        private set
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
            int geboortejaar, string land, string email, string telefoonnummer, 
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
